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

using System.Collections;
using System.Collections.Generic;
using System.Data.DBAccess.Generic.Exceptions;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace System.Data.DBAccess.Generic
{
    public static partial class IDBAccessExtensions
    {
        #region Helpers
        /// <summary>
        /// Callback helper for JSON/XML async functions.
        /// </summary>
        /// <param name="text">The JSON/XML string.</param>
        /// <param name="callback">The callback.</param>
        private static void ExecuteRelatedSetReadStringAsyncCallback(this IDBAccess db, String text, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteRelatedSetReadAsStringCallback callback)
        {
            if (callback != null)
                callback(text);
        }

        /// <summary>
        /// Gets the value of the relationship property from an object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="relationship">The property or path to property with period separations.</param>
        /// <returns>The object.</returns>
        private static Object GetRelationshipValue(this Object o, String relationship)
        {
            var parts = relationship.Split('.');
            var fda = FastDynamicAccess.Get(o);
            if (!fda.PropertyToArrayIndex.ContainsKey(parts[0]))
                throw new DALRelationshipParentPropertyMissingException(String.Format("Bad relationship '{0}' defined to object type '{1}'.", relationship, o.GetType()));

            Object retObj = fda.Get(o, parts[0]);

            for (int i = 1; i < parts.Length; i++)
            {
                var piece = parts[i];
                var ifda = FastDynamicAccess.Get(retObj);

                if (!ifda.PropertyToArrayIndex.ContainsKey(piece))
                    throw new DALRelationshipParentPropertyMissingException(String.Format("Bad relationship '{0}' defined to object type '{1}'.", relationship, o.GetType()));

                retObj = ifda.Get(retObj, piece);
            }

            return retObj;
        }

        /// <summary>
        /// Relates child objects to a parent table.
        /// </summary>
        /// <typeparam name="T">The type of the child objects.</typeparam>
        /// <param name="parentTable">The parent table.</param>
        /// <param name="childGrouping">The child groups.</param>
        /// <param name="parentFDA">The parent FDA.</param>
        /// <param name="rel">The relationship.</param>
        private static void RelateTables<T>(this IDBAccess db, List<Object> parentTable, IEnumerable<IGrouping<Object, Object>> childGrouping, FastDynamicAccess parentFDA, DALRelationship rel, Type parentIEnumerableType)
            where T : class, new()
        {
            var parentWithChildren = from p in parentTable
                                     join c in childGrouping on p.GetRelationshipValue(rel.ParentColumn).ToString() equals c.Key.ToString()
                                     select new
                                     {
                                         Parent = p,
                                         Children = c.OfType<T>().ToList()
                                     };

            foreach (var p in parentWithChildren)
                parentFDA.Set(p.Parent, rel.ParentProperty, typeof(T) == typeof(Object) && parentIEnumerableType.DerivesFromType(typeof(DALRuntimeTypeBase)) ? p.Children.OfType<DALRuntimeTypeBase>().ToList() as Object : p.Children);
        }

        /// <summary>
        /// One function to set relationships for up to 16 tables.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
        /// <typeparam name="T15">The type representing the 15th return data set.</typeparam>
        /// <typeparam name="T16">The type representing the 16th return data set.</typeparam>
        /// <param name="relationships">The relationships.</param>
        /// <param name="tuple">The DALTuple which resulted from the query.</param>
        /// <param name="data">Optional ModelData dictionary.  Will be a union of all dictionaries from all providers.  Only used when called from CPSegment.AsRelatedSetRead.</param>
        internal static void SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this DALTupleBase<T1> tuple, List<DALRelationship> relationships, Dictionary<Type, ModelData> data = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
            where T15 : class, new()
            where T16 : class, new()
        {
            for (int i = 0; i < relationships.Count; i++)
            {
                int parentIndex = i;
                int childIndex = parentIndex + 1;
                if (relationships[i].ParentTableIndex.HasValue)
                {
                    parentIndex = relationships[i].ParentTableIndex.Value;
                    childIndex = relationships[i].ChildTableIndex.Value;
                }

                parentIndex++;
                childIndex++;

                if (parentIndex == childIndex)
                    throw new DALRelationshipMisconfiguredException("Bad relationship.  Parent table cannot have a child table of itself.");

                var tupleFDA = FastDynamicAccess.Get(tuple);
                var parentTable = tupleFDA.GetList(tuple, String.Format("Table{0}", parentIndex));
                var childTable = tupleFDA.GetList(tuple, String.Format("Table{0}", childIndex));

                if (!parentTable.Any() || !childTable.Any()) continue;

                var parentFDA = FastDynamicAccess.Get(parentTable.First());
                var childFDA = FastDynamicAccess.Get(childTable.First());
                var rel = relationships[i];

                var parentType = parentTable.First().GetType();
                PropertyInfo parentPropertyInfo;

                var modelData = data ?? tuple.DBAccess[i].ModelsData;

                if (!modelData[parentType].ModelProperties.TryGetValue(rel.ParentProperty, out parentPropertyInfo))
                    throw new DALRelationshipParentPropertyMissingException(String.Format("Parent object type {0} does not have a property {1}. Parent Index {2} Child Index {3}", parentTable.First().GetType(), rel.ParentProperty, rel.ParentTableIndex, rel.ChildTableIndex));

                var parentPropertyType = parentPropertyInfo.PropertyType;
                if (!parentPropertyType.IsIEnumerable() || !parentPropertyType.DerivesInterface(typeof(IList)))
                    throw new DALRelationshipParentPropertyNotAListException(String.Format("Parent relationship property {0} in type {1} is not a List<>.",
                                   rel.ParentColumn,
                                   parentType));

                var childType = childTable.First().GetType();
                var parentEnumerableType = parentPropertyType.GetIEnumerableGenericType();
                //if the types aren't equal and
                //the child doesn't derive from DALRuntimeTypeBase and the parent isn't DALRuntimeTypeBase (not a fully runtime scenario)
                //      or the parent type isn't object... mixed scenarios
                if (childType != parentEnumerableType && ((!childType.DerivesFromType(typeof(DALRuntimeTypeBase)) || parentEnumerableType != typeof(DALRuntimeTypeBase)) && parentEnumerableType != typeof(Object)))
                    throw new DALRelationshipParentPropertyListIncorrectTypeException(String.Format("Child object type {0} does not match the object type expected in the parent list property {1}.  Parent Column {2} Child Column {3}.  Parent Index {4} Child Index {5}.",
                                                       childType,
                                                       parentPropertyType.GetIEnumerableGenericType(),
                                                       rel.ParentColumn,
                                                       rel.ChildColumn,
                                                       rel.ParentTableIndex,
                                                       rel.ChildTableIndex));

                var childGrouping = childTable.GroupBy(c => c.GetRelationshipValue(rel.ChildColumn));

                #region switch
                switch (childIndex)
                {
                    case 1:
                        tuple.DBAccess[0].RelateTables<T1>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 2:
                        tuple.DBAccess[0].RelateTables<T2>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 3:
                        tuple.DBAccess[0].RelateTables<T3>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 4:
                        tuple.DBAccess[0].RelateTables<T4>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 5:
                        tuple.DBAccess[0].RelateTables<T5>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 6:
                        tuple.DBAccess[0].RelateTables<T6>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 7:
                        tuple.DBAccess[0].RelateTables<T7>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 8:
                        tuple.DBAccess[0].RelateTables<T8>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 9:
                        tuple.DBAccess[0].RelateTables<T9>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 10:
                        tuple.DBAccess[0].RelateTables<T10>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 11:
                        tuple.DBAccess[0].RelateTables<T11>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 12:
                        tuple.DBAccess[0].RelateTables<T12>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 13:
                        tuple.DBAccess[0].RelateTables<T13>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 14:
                        tuple.DBAccess[0].RelateTables<T14>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 15:
                        tuple.DBAccess[0].RelateTables<T15>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                    case 16:
                        tuple.DBAccess[0].RelateTables<T16>(parentTable, childGrouping, parentFDA, rel, parentEnumerableType);
                        break;
                }
                #endregion
            }
        }

        /// <summary>
        /// Sets the relationships for a result set > 17 query.
        /// </summary>
        /// <param name="relationships">The relationships.</param>
        /// <param name="sets">The list of object lists representing the return set.</param>
        /// <param name="data">Optional ModelData dictionary.  Will be a union of all dictionaries from all providers.  Only used when called from CPSegment.AsRelatedSetRead.</param>
        internal static void SetRelationships(this IDBAccess db, List<DALRelationship> relationships, List<List<Object>> sets, Dictionary<Type, ModelData> data = null)
        {
            for (int i = 0; i < relationships.Count; i++)
            {
                int parentIndex = i;
                int childIndex = parentIndex + 1;
                if (relationships[i].ParentTableIndex.HasValue)
                {
                    parentIndex = relationships[i].ParentTableIndex.Value;
                    childIndex = relationships[i].ChildTableIndex.Value;
                }

                if (parentIndex == childIndex)
                    throw new DALRelationshipMisconfiguredException("Bad relationship.  Parent table cannot have a child table of itself.");

                var parentTable = sets[parentIndex];
                var childTable = sets[childIndex];
                if (!parentTable.Any() || !childTable.Any()) continue;

                var parentFDA = FastDynamicAccess.Get(parentTable.First());
                var childFDA = FastDynamicAccess.Get(childTable.First());
                var rel = relationships[i];

                var parentType = parentTable.First().GetType();
                PropertyInfo parentPropertyInfo;

                if (data == null)
                    data = db.ModelsData;

                if (!data[parentType].ModelProperties.TryGetValue(rel.ParentProperty, out parentPropertyInfo))
                    throw new DALRelationshipParentPropertyMissingException(String.Format("Parent object type {0} does not have a property {1}. Parent Index {2} Child Index {3}", parentTable.First().GetType(), rel.ParentProperty, rel.ParentTableIndex, rel.ChildTableIndex));

                var parentPropertyType = parentPropertyInfo.PropertyType;
                if (!parentPropertyType.IsIEnumerable() || !parentPropertyType.DerivesInterface(typeof(IList)))
                    throw new DALRelationshipParentPropertyNotAListException(String.Format("Parent relationship property {0} in type {1} is not a List<>.",
                                   rel.ParentColumn,
                                   parentType));

                var parentEnumerationType = parentPropertyType.GetIEnumerableGenericType();

                if (parentEnumerationType != typeof(DALRuntimeTypeBase) && parentEnumerationType != typeof(Object))
                    throw new DALRelationshipParentPropertyListIncorrectTypeException(String.Format("Parent relationship property {0} in type {1} is a List<{2}>.  It must be a List<Object>.",
                                                       rel.ParentColumn,
                                                       parentType,
                                                       parentEnumerationType));

                var childGrouping = childTable.GroupBy(c => c.GetRelationshipValue(rel.ChildColumn));

                var parentsWithChildren = from p in parentTable
                                          join c in childGrouping on p.GetRelationshipValue(rel.ParentColumn).ToString() equals c.Key.ToString()
                                          select new
                                          {
                                              Parent = p,
                                              Children = c.ToList()
                                          };

                foreach (var p in parentsWithChildren)
                    parentFDA.Set(p.Parent, rel.ParentProperty, p.Children.First() is DALRuntimeTypeBase ? p.Children.OfType<DALRuntimeTypeBase>().ToList() as Object : p.Children as Object);
            }
        }

        /// <summary>
        /// Gets a list of parent properties for each table.  This means which relationships claim each table as the parent index.
        /// </summary>
        /// <param name="tableCount">The number of tables returned by the query.</param>
        /// <param name="relationships">The list of relationships.</param>
        /// <returns>A list of string lists representing the parent properties of each table.</returns>
        internal static List<List<String>> GetParentPropertyLists(this IDBAccess db, int tableCount, List<DALRelationship> relationships)
        {
            List<List<String>> parents = new List<List<String>>(tableCount);
            for (int i = 0; i < tableCount; i++)
                parents.Add(relationships.Where(r => r.ParentTableIndex == i).Select(r => r.ParentProperty).ToList());

            return parents;
        }

        /// <summary>
        /// Ensures each relationship has Parent/Child table index values populated.  If they weren't populated it means the user did not specify them and we will assume the relationship for each table is 1 -> 2, 2 -> 3, etc. etc.
        /// </summary>
        /// <param name="relationships">The relationships.</param>
        internal static void EnsureRelationshipsHaveIndexes(this IDBAccess db, List<DALRelationship> relationships)
        {
            if (!relationships.Any()) return;
            //custom relationships
            if (relationships[0].ParentTableIndex.HasValue) return;

            //default 1 -> 2, 2 -> 3 etc. etc.
            for (int i = 0; i < relationships.Count; i++)
            {
                relationships[i].ParentTableIndex = i;
                relationships[i].ChildTableIndex = i + 1;
            }
        }
        #endregion
        #region IModel

        #region IModel
        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2> ExecuteRelatedSetRead<T1, T2>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2>(tables, parents);

            tuple.SetRelationships<T1, T2, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3> ExecuteRelatedSetRead<T1, T2, T3>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4> ExecuteRelatedSetRead<T1, T2, T3, T4>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5> ExecuteRelatedSetRead<T1, T2, T3, T4, T5>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, Object, Object, Object, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Object, Object, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Object, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Object, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <param name="queryString">The stored procedure name or query string to execute.</param>
        /// <param name="input">The input object.  This must be an object of type ModelBase, List&lt;SqlParameter&gt;, or an anonymous type.</param>
        /// <param name="storedProcedure">True of False flag representing whether or not this is a stored procedure call.</param>
        /// <param name="prefixDirection">True or False to prefix the input/output parameter prefix to each parameter name.</param>
        /// <param name="populateDefaultValues">If true, any output model properties which are not included in the return from the call are filled in using the value set in their default value attributes.  This option is ignored if the output IModel inherits the IQuickPopulate interface.</param>
        /// <param name="conn">The SQL connection with which to perform the operation.</param>
        /// <param name="timeout">The command timeout.  Defaults to 30 seconds.</param>
        /// <param name="tran">An optional transaction to use for the query.  If this is supplied, the transaction connection will be used and the conn parameter will be ignored.</param>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Object, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Object, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
        /// <typeparam name="T15">The type representing the 15th return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
            where T15 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);


            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Object>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
        /// <typeparam name="T15">The type representing the 15th return data set.</typeparam>
        /// <typeparam name="T16">The type representing the 16th return data set.</typeparam>
        /// <param name="queryString">The stored procedure name or query string to execute.</param>
        /// <param name="input">The input object.  This must be an object of type ModelBase, List&lt;SqlParameter&gt;, or an anonymous type.</param>
        /// <param name="storedProcedure">True of False flag representing whether or not this is a stored procedure call.</param>
        /// <param name="prefixDirection">True or False to prefix the input/output parameter prefix to each parameter name.</param>
        /// <param name="populateDefaultValues">If true, any output model properties which are not included in the return from the call are filled in using the value set in their default value attributes.  This option is ignored if the output IModel inherits the IQuickPopulate interface.</param>
        /// <param name="conn">The SQL connection with which to perform the operation.</param>
        /// <param name="timeout">The command timeout.  Defaults to 30 seconds.</param>
        /// <param name="tran">An optional transaction to use for the query.  If this is supplied, the transaction connection will be used and the conn parameter will be ignored.</param>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IDBAccess db, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
            where T15 : class, new()
            where T16 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();

            if (relationships.Count > tables.Count - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read of size {1}.", relationships.Count, tables.Count));

            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(tables.Count, relationships);

            var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(tables, parents);

            tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(relationships);

            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned using runtime types.
        /// </summary>
        /// <param name="queryString">The stored procedure name or query string to execute.</param>
        /// <param name="input">The input object.  This must be an object of type ModelBase, List&lt;SqlParameter&gt;, or an anonymous type.</param>
        /// <param name="storedProcedure">True of False flag representing whether or not this is a stored procedure call.</param>
        /// <param name="prefixDirection">True or False to prefix the input/output parameter prefix to each parameter name.</param>
        /// <param name="populateDefaultValues">If true, any output model properties which are not included in the return from the call are filled in using the value set in their default value attributes.  This option is ignored if the output IModel inherits the IQuickPopulate interface.</param>
        /// <param name="conn">The SQL connection with which to perform the operation.</param>
        /// <param name="timeout">The command timeout.  Defaults to 30 seconds.</param>
        /// <param name="tran">An optional transaction to use for the query.  If this is supplied, the transaction connection will be used and the conn parameter will be ignored.</param>
        /// <returns>A List of a List of runtime type objects.</returns>
        public static List<List<Object>> ExecuteRelatedSetRead(this IDBAccess db, List<DALRelationship> relationships)
        {
            db.EnsureRelationshipsHaveIndexes(relationships);

            var tables = db.ExecuteSetReadQuick();

            var parents = db.GetParentPropertyLists(tables.Count(), relationships);

            var retList = tables.Select((t, i) => db.PopulateModelBaseEnumeration(t, typeof(Object), parents[i])).ToList();

            db.SetRelationships(relationships, retList);

            return retList;
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <param name="returnTypes">An enumeration of the types returned by the ExecuteSetRead command.</param>
        /// <param name="queryString">The stored procedure name or query string to execute.</param>
        /// <param name="input">The input object.  This must be an object of type ModelBase, List&lt;SqlParameter&gt;, or an anonymous type.</param>
        /// <param name="storedProcedure">True of False flag representing whether or not this is a stored procedure call.</param>
        /// <param name="prefixDirection">True or False to prefix the input/output parameter prefix to each parameter name.</param>
        /// <param name="populateDefaultValues">If true, any output model properties which are not included in the return from the call are filled in using the value set in their default value attributes.  This option is ignored if the output IModel inherits the IQuickPopulate interface.</param>
        /// <param name="conn">The SQL connection with which to perform the operation.</param>
        /// <param name="timeout">The command timeout.  Defaults to 30 seconds.</param>
        /// <param name="tran">An optional transaction to use for the query.  If this is supplied, the transaction connection will be used and the conn parameter will be ignored.</param>
        /// <returns>A List&lt;List&lt;Object&gt;&gt;. Call .OfType on each list in order to cast it back to the correct model type.</returns>
        public static List<List<Object>> ExecuteRelatedSetRead(this IDBAccess db, List<DALRelationship> relationships, IEnumerable<Type> returnTypes)
        {
            db.EnsureRelationshipsHaveIndexes(relationships);
            var parents = db.GetParentPropertyLists(returnTypes.Count(), relationships);

            var tables = db.ExecuteSetReadQuick();
            var retList = tables.Select((t, i) => db.PopulateModelBaseEnumeration(t, returnTypes.Skip(i).First(), parents[i])).ToList();

            db.SetRelationships(relationships, retList);

            return retList;
        }
        #endregion

        #region IModelAsync
        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 2 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2> callback = null)
            where T1 : class, new()
            where T2 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 3 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 4 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 5 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 6 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 7 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6, T7>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 8 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 9 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 10 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 11 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 12 type DALTuple.  If this is null, no callback will be made.</param>
        /// <param name="queryString">The stored procedure name or query string to execute.</param>
        /// <param name="input">The input object.  This must be an object of type ModelBase, List&lt;SqlParameter&gt;, or an anonymous type.</param>
        /// <param name="storedProcedure">True of False flag representing whether or not this is a stored procedure call.</param>
        /// <param name="prefixDirection">True or False to prefix the input/output parameter prefix to each parameter name.</param>
        /// <param name="populateDefaultValues">If true, any output model properties which are not included in the return from the call are filled in using the value set in their default value attributes.  This option is ignored if the output IModel inherits the IQuickPopulate interface.</param>
        /// <param name="conn">The SQL connection with which to perform the operation.</param>
        /// <param name="timeout">The command timeout.  Defaults to 30 seconds.</param>
        /// <param name="tran">An optional transaction to use for the query.  If this is supplied, the transaction connection will be used and the conn parameter will be ignored.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 13 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 14 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
        /// <typeparam name="T15">The type representing the 15th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of 15 type DALTuple.  If this is null, no callback will be made.</param>
        /// <param name="queryString">The stored procedure name or query string to execute.</param>
        /// <param name="input">The input object.  This must be an object of type ModelBase, List&lt;SqlParameter&gt;, or an anonymous type.</param>
        /// <param name="storedProcedure">True of False flag representing whether or not this is a stored procedure call.</param>
        /// <param name="prefixDirection">True or False to prefix the input/output parameter prefix to each parameter name.</param>
        /// <param name="populateDefaultValues">If true, any output model properties which are not included in the return from the call are filled in using the value set in their default value attributes.  This option is ignored if the output IModel inherits the IQuickPopulate interface.</param>
        /// <param name="conn">The SQL connection with which to perform the operation.</param>
        /// <param name="timeout">The command timeout.  Defaults to 30 seconds.</param>
        /// <param name="tran">An optional transaction to use for the query.  If this is supplied, the transaction connection will be used and the conn parameter will be ignored.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
            where T15 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
        /// <typeparam name="T15">The type representing the 15th return data set.</typeparam>
        /// <typeparam name="T16">The type representing the 16th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 16 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteRelatedSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IDBAccess db, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new()
            where T11 : class, new()
            where T12 : class, new()
            where T13 : class, new()
            where T14 : class, new()
            where T15 : class, new()
            where T16 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(relationships);
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <param name="returnTypes">An enumeration of the types returned by the ExecuteSetRead command.</param>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type List&lt;List&lt;Object&gt;&gt;.  If this is null, no callback will be made.</param>
        /// <param name="queryString">The stored procedure name or query string to execute.</param>
        /// <param name="input">The input object.  This must be an object of type ModelBase, List&lt;SqlParameter&gt;, or an anonymous type.</param>
        /// <param name="storedProcedure">True of False flag representing whether or not this is a stored procedure call.</param>
        /// <param name="prefixDirection">True or False to prefix the input/output parameter prefix to each parameter name.</param>
        /// <param name="populateDefaultValues">If true, any output model properties which are not included in the return from the call are filled in using the value set in their default value attributes.  This option is ignored if the output IModel inherits the IQuickPopulate interface.</param>
        /// <param name="conn">The SQL connection with which to perform the operation.</param>
        /// <param name="timeout">The command timeout.  Defaults to 30 seconds.</param>
        /// <param name="tran">An optional transaction to use for the query.  If this is supplied, the transaction connection will be used and the conn parameter will be ignored.</param>
        public static void ExecuteRelatedSetReadAsync(this IDBAccess db, List<DALRelationship> relationships, IEnumerable<Type> returnTypes, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadObjectsCallbackDelegate callback = null)
        {
            new Task(() =>
            {
                var sets = db.ExecuteRelatedSetRead(relationships, returnTypes);
                db.ExecuteSetReadObjectsCallback(sets, callback);
            }).Start();
        }

        #endregion
        #endregion
    }
}