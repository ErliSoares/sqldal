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
    /// Interface to declare that a class is a quick populate class.
    /// </summary>
    public interface IQuickPopulate
    {
        /// <summary>
        /// Populates a Model using the user defined Populate method.  You should manually assign each member of a model which needs to be assigned a value based on the DataRow passed in.  This is the only function that will be called when populating the object.
        /// </summary>
        /// <param name="dataRows">The data rows returned by the SQL server.</param>
        /// <param name="colIndexes">Dictionary of column names to dr indexes.</param>
        List<T> DALPopulate<T>(List<object[]> dataRows, Dictionary<string, int> colIndexes) where T : class, new();
    }
}