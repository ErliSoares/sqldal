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

namespace System.Data.DBAccess.Generic
{
    /// <summary>
    /// Class which represents DataRow objects and their columns.
    /// </summary>
    public class ExecuteReadQuickTuple
    {
        /// <summary>
        /// The data rows as object arrays.
        /// </summary>
        public List<Object[]> DataRows { get; set; }

        /// <summary>
        /// The list of column names in the same order as the indexes in the DataRows arrays.
        /// </summary>
        public List<String> ColumnNames { get; set; }

        /// <summary>
        /// The list of column data types in the same order as the indexes in the DataRows arrays.
        /// </summary>
        public List<Type> ColumnTypes { get; set; }

        /// <summary>
        /// A name given to the table which populated this tuple object.  This is used for proper XML and JSON serialization, and can be left null if that functionality is not needed.
        /// </summary>
        public String TableName { get; set; }
    }
}