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
    public interface IDBAccess
    {
        #region Methods
        /// <summary>
        /// Performs an ExecuteRead against the provider.  The result of which will be transformed into objects.
        /// </summary>
        /// <returns></returns>
        ExecuteReadQuickTuple ExecuteReadQuick();

        /// <summary>
        /// Executes a NonQuery against the provider.
        /// </summary>
        /// <returns></returns>
        int ExecuteNonQuery();

        /// <summary>
        /// Executes a Scalar read operation against the provider.
        /// </summary>
        /// <returns></returns>
        Object ExecuteScalar();

        /// <summary>
        /// Performs an ExecuteSetRead against the provider.  The result of which will be transformed into objects.
        /// </summary>
        /// <returns></returns>
        List<ExecuteReadQuickTuple> ExecuteSetReadQuick();
        #endregion

        #region Properties
        /// <summary>
        /// Sets whether or not model properties not returned by a query should be defaulted to the value defined in the [DALDefaultValue] attribute.
        /// </summary>
        Boolean PopulateDefaultValues { get; set; }

        /// <summary>
        /// The level of trace information to output.
        /// </summary>
        TraceLevel TraceOutputLevel { get; set; }

        /// <summary>
        /// Dictionary of cached model data used by the DAL.
        /// </summary>
        Dictionary<Type, ModelData> ModelsData { get; set; }
        #endregion
    }
}