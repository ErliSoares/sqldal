/*Copyright 2012 Brian Adams

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.*/

using System.Collections.Generic;

namespace System.Data.DBAccess.Generic.Events
{
    /// <summary>
    /// Base DAL event args class.
    /// </summary>
    public class DALEventArgs : EventArgs
    {
        /// <summary>
        /// The statement that was executing when the event was thrown.
        /// </summary>
        public String QueryString { get; internal set; }

        /// <summary>
        /// The connection object being used when the event was thrown.
        /// </summary>
        public IDbConnection Connection { get; internal set; }

        /// <summary>
        /// If the execution was happening in a transaction, this will be set to the transaction object.
        /// </summary>
        public IDbTransaction Transaction { get; internal set; }

        /// <summary>
        /// The command timeout.
        /// </summary>
        public int Timeout { get; internal set; }

        /// <summary>
        /// The SQL parameters that were used for the query.
        /// </summary>
        public IEnumerable<IDbDataParameter> InputParameters { get; internal set; }

        /// <summary>
        /// The type of query run.  ExecuteRead, ExecuteReadSingle, ExecuteSetRead, ExecuteScalar, ExecuteScalarEnumeration or ExecuteNonQuery.
        /// </summary>
        public String QueryMethod { get; internal set; }

        /// <summary>
        /// If an input class was passed, the type of class that was used to generate the SQL parameters.
        /// </summary>
        public String InputModelType { get; internal set; }

        /// <summary>
        /// If an input class was passed, this is the input object.
        /// </summary>
        public Object Input { get; internal set; }
    }
}