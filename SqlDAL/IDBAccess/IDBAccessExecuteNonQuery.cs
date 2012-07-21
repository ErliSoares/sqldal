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

using System.Threading.Tasks;

namespace System.Data.DBAccess.Generic
{
    public static partial class IDBAccessExtensions
    {
        #region ExecuteNonQueryAsync
        private static void ExecuteNonQueryCallback(this IDBAccess db, int affectedRows, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteNonQueryCallbackDelegate callback)
        {
            if (callback != null)
                callback(affectedRows);
        }
        /// <summary>
        /// Executes an asynchronous non query operation against a SQL database using the provided options.
        /// </summary>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type int.  If this is null, no callback will be made.</param>
        public static void ExecuteNonQueryAsync(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteNonQueryCallbackDelegate callback = null)
        {
            new Task(() =>
            {
                var affectedRows = db.ExecuteNonQuery();
                db.ExecuteNonQueryCallback(affectedRows, callback);
            }).Start();
        }
        #endregion
    }
}