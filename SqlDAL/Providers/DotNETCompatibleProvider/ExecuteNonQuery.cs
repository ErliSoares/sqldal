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
        /// Executes a non query operation against a database using the provided options.
        /// </summary>
        /// <returns>The number of rows affected.</returns>
        public virtual int ExecuteNonQuery()
        {
            return this.ExecuteSql<int>((cmd) =>
            {
                var affectedRows = cmd.ExecuteNonQuery();

                if (this.ExpectedAffectedRows != null && this.ExpectedAffectedRows.Value != affectedRows)
                    this.HandleOnAffectedRowsMismatch(affectedRows, this.Connection, this.PrepareParameters(this.Input, this.PrefixDirection), this.CommandTimeout, this.QueryString);

                return affectedRows;
            });
        }
    }
}