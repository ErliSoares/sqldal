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

using System;
using System.Collections.Generic;
using System.Linq;

namespace TableClassGenerator
{
    public class Column
    {
        public String Name { get; set; }
        public String Type { get; set; }

        public static Column GetColumn(String name, List<String> types, Boolean nullable)
        {
            String colType = types.First(t => colTypes.ContainsKey(t));
            colType = colTypes[colType.ToLower()];
            String nullableType;

            if (nullable && nullableValueTypes.TryGetValue(colType.ToLower(), out nullableType))
                colType = nullableType;

            return new Column
            {
                Name = name,
                Type = colType
            };
        }

        private static Dictionary<String, String> nullableValueTypes = new Dictionary<String, String>
        {
            { "int", "int?" },
            { "long", "long?" },
            { "float", "float?" },
            { "double", "double?" },
            { "short", "short?" },
            { "decimal", "decimal?" },
            { "byte", "byte?" },
            { "bool", "bool?" },
            { "datetime", "DateTime?" },
            { "timespan", "TimeSpan?" }
        };

        private static Dictionary<String, String> colTypes = new Dictionary<String, String>
        {
            { "image", "Byte[]" },
            { "text", "String" },
            { "uniqueidentifier", "String" },
            { "date", "DateTime" },
            { "time", "TimeSpan" },
            { "datetime2", "DateTime" },
            { "datetimeoffset", "DateTimeOffset" },
            { "tinyint", "byte" },
            { "smallint", "short" },
            { "int", "int" },
            { "smalldatetime", "DateTime" },
            { "real", "double" },
            { "money", "decimal" },
            { "datetime", "DateTime" },
            { "float", "float" },
            { "sql_variant", "Object" },
            { "ntext", "String" },
            { "bit", "Boolean" },
            { "decimal", "decimal" },
            { "numeric", "decimal" },
            { "smallmoney", "decimal" },
            { "bigint", "long" },
            { "varbinary", "Byte[]" },
            { "varchar", "String" },
            { "binary", "Byte[]" },
            { "char", "String" },
            { "nvarchar", "String" },
            { "nchar", "String" },
            { "xml", "String" },
            { "timestamp", "long" }
        };
    }
}