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
using System.Threading.Tasks;

namespace System.Data.DBAccess.Generic.Providers.DotNETCompatibleProvider
{
    public abstract partial class DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException> : IDBAccess
        where TConnection : class, IDbConnection, new()
        where TCommand : class, IDbCommand, new()
        where TParameter : class, IDbDataParameter, new()
        where TTransaction : class, IDbTransaction
        where TDataAdapter : IDbDataAdapter, new()
        where TException : Exception
    {
        /// <summary>
        /// Executes a read operation against a database using the provided options.
        /// </summary>
        /// <returns>The DataTable object.</returns>
        public virtual DataTable ExecuteRead()
        {
            return this.ExecuteSql<DataTable>((cmd) =>
            {
                DataTable dt = new DataTable();
                //if not passed a connection and we're not in a transaction then we should close the connection when we're done
                Boolean useDefault = this.Connection == null;

                CommandBehavior behavior = CommandBehavior.Default;
                if (useDefault && this.Transaction == null)
                    behavior = CommandBehavior.CloseConnection;

                using (var r = cmd.ExecuteReader(behavior))
                {
                    dt.Load(r);
                }

                return dt;
            });
        }

        /// <summary>
        /// Executes a read operation against a database using the provided options and an IDataReader object.
        /// </summary>
        /// <returns>An ExecuteReadQuickTuple object.</returns>
        ExecuteReadQuickTuple IDBAccess.ExecuteReadQuick()
        {
            return this.ExecuteSql<ExecuteReadQuickTuple>((cmd) =>
            {
                var rows = new List<Object[]>();
                var colNames = new List<String>();
                var colTypes = new List<Type>();
                //if not passed a connection and we're not in a transaction then we should close the connection when we're done
                Boolean useDefault = this.Connection == null;

                CommandBehavior behavior = CommandBehavior.Default;
                if (useDefault && this.Transaction == null)
                    behavior |= CommandBehavior.CloseConnection;

                String tableName = this.XMLTableNames == null || !this.XMLTableNames.Any() ? "" : this.XMLTableNames[0];
                using (var r = cmd.ExecuteReader(behavior))
                {
                    int colCount = r.FieldCount;
                    for (int i = 0; i < colCount; i++)
                    {
                        colNames.Add(r.GetName(i));
                        colTypes.Add(r.GetFieldType(i));
                    }

                    if (this.GetOriginalCallingFunctionName() == "ExecuteReadSingle")
                    {
                        while (r.Read())
                        {
                            Object[] itemsArray = new Object[colCount];
                            r.GetValues(itemsArray);
                            rows.Add(itemsArray);

                            break;
                        }
                    }
                    else
                    {
                        while (r.Read())
                        {
                            Object[] itemsArray = new Object[colCount];
                            r.GetValues(itemsArray);
                            rows.Add(itemsArray);
                        }
                    }
                }

                return new ExecuteReadQuickTuple
                {
                    DataRows = rows,
                    ColumnNames = colNames,
                    ColumnTypes = colTypes,
                    TableName = tableName
                };
            });
        }

        #region ExecuteReadAsync
        private void ExecuteReadCallback(DataTable dt, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteReadCallbackDelegate callback)
        {
            if (callback != null)
                callback(dt);
        }

        /// <summary>
        /// Executes an asynchronous read operation against a database using the provided options.
        /// </summary>
        /// <typeparam name="T">The type of output models to return.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type List DataRow.  If this is null, no callback will be made.</param>
        public void ExecuteReadAsync(System.Data.DBAccess.Generic.AsyncDelegates.ExecuteReadCallbackDelegate callback = null)
        {
            new Task(() =>
            {
                var rows = this.ExecuteRead();
                this.ExecuteReadCallback(rows, callback);
            }).Start();
        }
        #endregion
        #region ExecuteReadSingle
        /// <summary>
        /// Executes a read operation against a database using the provided options and returns the first row from the result.
        /// </summary>
        /// <typeparam name="T">The type of output models to return.</typeparam>
        /// <returns>The first DataRow from the result.  Null is returned if the data set was empty.</returns>
        public virtual DataRow ExecuteReadSingle()
        {
            var dt = this.ExecuteRead();
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
        #endregion
        #region ExecuteReadSingleAsync
        /// <summary>
        /// Executes an asynchronous read operation against a database using the provided options.
        /// </summary>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type DataRow.  If this is null, no callback will be made.</param>
        public void ExecuteReadSingleAsync(System.Data.DBAccess.Generic.AsyncDelegates.ExecuteReadSingleCallbackDelegate callback = null)
        {
            new Task(() =>
            {
                var row = this.ExecuteReadSingle();
                this.ExecuteReadSingleCallback(row, callback);
            }).Start();
        }

        private void ExecuteReadSingleCallback(DataRow row, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteReadSingleCallbackDelegate callback)
        {
            if (callback != null)
                callback(row);
        }
        #endregion
    }
}