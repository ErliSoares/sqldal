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
        /// <returns>The resulting DataSet.</returns>
        public virtual DataSet ExecuteSetRead()
        {
            return this.ExecuteSql<DataSet>((cmd) =>
            {
                DataSet ds = new DataSet();
                TDataAdapter adapter = new TDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);

                return ds;
            });
        }

        /// <summary>
        /// Executes a read operation against a database using the provided options.
        /// </summary>
        /// <returns>A list of tuples representing each return table.</returns>
        List<ExecuteReadQuickTuple> IDBAccess.ExecuteSetReadQuick()
        {
            return this.ExecuteSql<List<ExecuteReadQuickTuple>>((cmd) =>
            {
                var ret = new List<ExecuteReadQuickTuple>();

                //if not passed a connection and we're not in a transaction then we should close the connection when we're done
                Boolean useDefault = this.Connection == null;

                CommandBehavior behavior = CommandBehavior.Default;
                if (useDefault && this.Transaction == null)
                    behavior |= CommandBehavior.CloseConnection;

                int xmlTableIndex = 0;

                using (var r = cmd.ExecuteReader(behavior))
                {
                    do
                    {
                        int colCount = r.FieldCount;
                        var rows = new List<Object[]>();
                        var colNames = new List<String>();
                        var colTypes = new List<Type>();

                        for (int i = 0; i < colCount; i++)
                        {
                            colNames.Add(r.GetName(i));
                            colTypes.Add(r.GetFieldType(i));
                        }

                        while (r.Read())
                        {
                            Object[] itemsArray = new Object[colCount];
                            r.GetValues(itemsArray);
                            rows.Add(itemsArray);
                        }

                        ret.Add(new ExecuteReadQuickTuple
                        {
                            DataRows = rows,
                            ColumnNames = colNames,
                            ColumnTypes = colTypes,
                            TableName = this.XMLTableNames == null || this.XMLTableNames.Count <= xmlTableIndex ? "" : this.XMLTableNames[xmlTableIndex++]
                        });
                    } while (r.NextResult());

                    return ret;
                }
            });
        }

        #region RawAsync
        private void ExecuteSetReadCallback(DataSet ds, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackDelegate callback)
        {
            if (callback != null)
                callback(ds);
        }

        /// <summary>
        /// Executes an asynchronous read operation against a database using the provided options.
        /// </summary>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type List&lt;List&lt;DataRow&gt;&gt;.  If this is null, no callback will be made.</param>
        public void ExecuteSetReadAsync(System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackDelegate callback = null)
        {
            new Task(() =>
            {
                var ds = this.ExecuteSetRead();
                this.ExecuteSetReadCallback(ds, callback);
            }).Start();
        }
        #endregion
    }
}