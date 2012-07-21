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
    /// Interface to declare a table type is a quick read table.
    /// </summary>
    public interface IQuickRead
    {
        /// <summary>
        /// Populates a UDTable using the user defined QuickRead method.  You must manually populate an object array consisting of member of the UDTable which needs to be passed on to the user defined table type in SQL.  This is the only function that will be called when reading the object.  Any property attributes on the class will not be applied.
        /// </summary>
        /// <returns>Array of objects which are to be added to the DataTable SQL parameter.</returns>
        Object[] ToObjectArray();

        /// <summary>
        /// Returns a dictionary of column names and types which correspond to the objects returned by the ToObjectArray method.
        /// </summary>
        /// <returns>A dictionary containing the column names and types corresponding to the ToObjectArray method.  The order of items in the dictionary should be the same as the order of items returned by ToObjectArray.</returns>
        Dictionary<String, Type> GetColumnNamesTypes();
    }
}