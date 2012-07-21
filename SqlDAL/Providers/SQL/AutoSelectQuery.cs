/*
Copyright 2012 Brian Adams

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.Collections.Generic;
using System.Linq;

namespace System.Data.DBAccess.Generic.Providers.SQL
{
    internal class AutoSelectPart
    {
        internal String TableName { get; set; }
        internal String ColumnName { get; set; }
    }

    internal class AutoSelectTable
    {
        internal String TableName { get; set; }
        internal List<AutoSelectJoin> Joins { get; set; }

        internal List<String> GetAllJoinableTables()
        {
            return this.GetAllJoinableTables(new List<String>()).ToList();
        }

        private IEnumerable<String> GetAllJoinableTables(List<String> joinedTables)
        {
            foreach (var j in this.Joins.Where(j => !joinedTables.Contains(j.Table.TableName)))
            {
                var name = j.Table.TableName;
                joinedTables.Add(name);
                yield return name;

                foreach (var t in j.Table.GetAllJoinableTables(joinedTables).Reverse())
                    yield return t;
            }
        }

        internal List<AutoSelectJoin> GetJoinPath(AutoSelectTable destTable)
        {
            return GetJoins(destTable, new List<AutoSelectJoin>()).Reverse().ToList();
        }

        private IEnumerable<AutoSelectJoin> GetJoins(AutoSelectTable destTable, List<AutoSelectJoin> joins)
        {
            var one = this.Joins.FirstOrDefault(j => j.Table == destTable);
            if (one != null) return joins.Union(new List<AutoSelectJoin> { one });

            foreach (var j in this.Joins)
            {
                var thisJoins = j.Table.GetJoins(destTable, joins);
                if (thisJoins != null)
                    return thisJoins.Union(new List<AutoSelectJoin> { j });
            }

            return new List<AutoSelectJoin>();
        }
    }

    internal class AutoSelectJoin
    {
        internal AutoSelectTable Table { get; set; }
        internal String ColumnName { get; set; }
        internal String ParentTableName { get; set; }
        internal String ParentColumnName { get; set; }
    }

    public class FKRelationship
    {
        public String ParentTable { get; set; }
        public String ParentColumn { get; set; }
        public String ReferencedTable { get; set; }
        public String ReferencedColumn { get; set; }
    }

    internal static class AutoSelectQuery
    {
        private static Dictionary<String, List<AutoSelectTable>> ConnectionTables { get; set; }
        private const String GetTableNamesSQL = "SELECT Name FROM sys.tables WITH (NOLOCK)";
        private const String GetFKRelationshipsSQL = @"SELECT OBJECT_NAME(fkc.parent_object_id) [ParentTable],cp.name [ParentColumn],OBJECT_NAME(fkc.referenced_object_id) [ReferencedTable],cr.name [ReferencedColumn]
FROM sys.foreign_key_columns fkc WITH (NOLOCK)
JOIN sys.columns cp WITH (NOLOCK) ON fkc.parent_object_id = cp.object_id AND cp.column_id = fkc.parent_column_id
JOIN sys.columns cr WITH (NOLOCK) ON fkc.referenced_object_id = cr.object_id AND cr.column_id = fkc.referenced_column_id";

        static AutoSelectQuery()
        {
            AutoSelectQuery.ConnectionTables = new Dictionary<String, List<AutoSelectTable>>();
        }

        internal static String GetSelectQuery(String connectionString, String input, Boolean nolock)
        {
            String nolockString = (nolock) ? " WITH (NOLOCK)" : "";
            var parts = input.Split(',').Select(p =>
                {
                    var pieces = p.Split('.');
                    return new AutoSelectPart
                    {
                        TableName = pieces[0],
                        ColumnName = pieces[1]
                    };
                });

            List<AutoSelectTable> tables;
            if (!AutoSelectQuery.ConnectionTables.TryGetValue(connectionString, out tables))
            {
                tables = new List<AutoSelectTable>();
                var db = new SqlDBAccess(connectionString);
                db.IsStoredProcedure = false;
                db.QueryString = GetTableNamesSQL;
                var tableNames = db.ExecuteScalarEnumeration<String>();
                db.QueryString = GetFKRelationshipsSQL;
                var fks = db.ExecuteRead<FKRelationship>();
                tables = tableNames.Select(t => new AutoSelectTable
                                          {
                                              TableName = t
                                          }).ToList();

                foreach (var t in tables)
                {
                    t.Joins = fks.Where(fk => fk.ParentTable == t.TableName)
                                                         .Select(fk => new AutoSelectJoin
                                                         {
                                                             ColumnName = fk.ReferencedColumn,
                                                             Table = tables.Single(tbl => tbl.TableName == fk.ReferencedTable),
                                                             ParentColumnName = fk.ParentColumn,
                                                             ParentTableName = fk.ParentTable
                                                         }).ToList();
                }

                AutoSelectQuery.ConnectionTables.Add(connectionString, tables);
            }

            var distinctTableNames = parts.Select(p => p.TableName).Distinct().ToList();

            var query = new List<String> { "SELECT " + String.Join(",", parts.Select(p => String.Format("[{0}].[{1}]", p.TableName, p.ColumnName))) };

            var eligibleTables = tables.Where(t => distinctTableNames.Intersect(t.GetAllJoinableTables().Union(new List<String> { t.TableName })).Count() == distinctTableNames.Count).ToList();
            var allJoins = new Dictionary<String, List<String>>();
            
            foreach (var t in eligibleTables)
                allJoins.Add(t.TableName, distinctTableNames.Intersect(t.GetAllJoinableTables()).ToList());
            
            var min = allJoins.Min(kvp => kvp.Value.Count);
            var firstTable = eligibleTables.Single(t => t.TableName == allJoins.First(kvp => kvp.Value.Count == min).Key);
            //var firstTableName = distinctTableNames.First(t => tables.Single(tbl => tbl.TableName == t).GetAllJoinableTables(tables).Intersect(distinctTableNames).Count() == distinctTableNames.Count - 1);
            //var firstTable = tables.Single(t => t.TableName == firstTableName);
            var joins = new List<AutoSelectJoin>();
            foreach (var t in distinctTableNames.Where(t => t != firstTable.TableName))
            {
                joins.AddRange(firstTable.GetJoinPath(tables.Single(tbl => tbl.TableName == t)));
            }

            query.Add(String.Format("FROM [{0}]{1}", firstTable.TableName, nolockString));
            query.AddRange(joins.Distinct().Select(j => String.Format("JOIN [{0}]{4} ON [{1}].[{2}] = [{0}].[{3}]", j.Table.TableName, j.ParentTableName, j.ParentColumnName, j.ColumnName, nolockString)));   
            
            return String.Join(Environment.NewLine, query);
        }
    }
}