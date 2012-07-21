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

namespace System.Data.DBAccess.Generic
{
    /// <summary>
    /// Class representing a relationship to use in a related set read.
    /// </summary>
    public class DALRelationship
    {
        /// <summary>
        /// The column name in the parent table.
        /// </summary>
        public String ParentColumn { get; set; }

        /// <summary>
        /// The column name in the child table.
        /// </summary>
        public String ChildColumn { get; set; }

        /// <summary>
        /// The property name to assign the child rows into.
        /// </summary>
        public String ParentProperty { get; set; }

        /// <summary>
        /// The 0 based index of the parent table in the entire set.  Optional.  Only use if you are defining custom relationships (i.e. more than one child to a parent).
        /// </summary>
        public int? ParentTableIndex { get; set; }

        /// <summary>
        /// The 0 based index of the child table in the entire set.  Optional.  Only use if you are defining custom relationships (i.e. more than one child to a parent).
        /// </summary>
        public int? ChildTableIndex { get; set; }
    }
}