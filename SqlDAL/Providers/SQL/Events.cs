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
using System.Data.DBAccess.Generic.Events;
using System.Data.SqlClient;

namespace System.Data.DBAccess.Generic.Providers.SQL
{
    public sealed partial class SqlDBAccess : IDBAccess
    {
        #region Event declarations
        #region Basic events
        /// <summary>
        /// Class for DALSqlException event info.
        /// </summary>
        public sealed class DALSqlExceptionEventArgs : DALEventArgs
        {
            /// <summary>
            /// The SqlException that was thrown.
            /// </summary>
            public SqlException Exception { get; internal set; }

            /// <summary>
            /// Boolean value indicating whether or not custom error messages were returned the by SQL server.
            /// </summary>
            public Boolean HasCustomErrors { get; internal set; }

            /// <summary>
            /// The list of custom errors returned by the call.
            /// </summary>
            public List<String> CustomErrors { get; internal set; }
        }
        #endregion
        #region Delegates

        /// <summary>
        /// OnSqlException event handler delegate.
        /// </summary>
        /// <param name="sender">The IDBAccess object which raised the event.</param>
        /// <param name="e">The SqlException event arguments.</param>
        public delegate void DALSqlExceptionHandler(SqlDBAccess sender, DALSqlExceptionEventArgs e);
        #endregion
        #endregion
        #region Instance events
        /// <summary>
        /// Fires when a SqlException is raised by a query.
        /// </summary>
        new public event DALSqlExceptionHandler OnQueryException;
        /// <summary>
        /// Fires when a query operation completes.
        /// </summary>

        /// <summary>
        /// Raises a sql exception event if it's being listened to.
        /// </summary>
        /// <param name="e">The event args.</param>
        private void RaiseOnSqlException(DALSqlExceptionEventArgs e)
        {
            SqlDBAccess.RaiseOnQueryException(this, e);

            if (OnQueryException != null)
                OnQueryException(this, e);

            base.RaiseOnException(new DALExceptionEventArgs
            {
                Connection = e.Connection,
                Exception = e.Exception,
                InputModelType = e.InputModelType,
                InputParameters = e.InputParameters,
                QueryMethod = e.QueryMethod,
                QueryString = e.QueryString,
                Timeout = e.Timeout,
                Transaction = e.Transaction,
                Input = this.Input
            });

            return;
        }
        #endregion
        #region Static events
        /// <summary>
        /// Fires when a SqlException is raised by a query.
        /// </summary>
        new public static event DALSqlExceptionHandler OnQueryExceptionStatic;

        /// <summary>
        /// Raises a sql exception event if it's being listened to.
        /// </summary>
        /// <param name="db">The IDBAccess object which raised the event.</param>
        /// <param name="e">The event args.</param>
        internal static void RaiseOnQueryException(SqlDBAccess db, DALSqlExceptionEventArgs e)
        {
            if (SqlDBAccess.OnQueryExceptionStatic != null)
                SqlDBAccess.OnQueryExceptionStatic(db, e);
        }
        #endregion
    }
}