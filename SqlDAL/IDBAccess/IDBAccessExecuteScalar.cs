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

namespace System.Data.DBAccess.Generic
{
    public static partial class IDBAccessExtensions
    {
        /// <summary>
        /// Executes a scalar return operation against a SQL database using the provided options.
        /// </summary>
        /// <typeparam name="T">The type of object to cast the return value to.</typeparam>
        /// <returns>The scalar value returned by the SQL server casted to a T.</returns>
        public static T ExecuteScalar<T>(this IDBAccess db)
        {
            return db.ExecuteScalar().CastToT<T>();
        }

        #region ExecuteScalarAsync
        private static void ExecuteScalarCallback(this IDBAccess db, Object value, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteScalarCallbackDelegate callback)
        {
            if (callback != null)
                callback(value);
        }

        private static void ExecuteScalarCallback<T>(this IDBAccess db, T value, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteScalarCallbackDelegate<T> callback)
        {
            if (callback != null)
                callback(value);
        }
        /// <summary>
        /// Executes an asynchronous scalar return operation against a SQL database using the provided options.
        /// </summary>
        /// <typeparam name="T">The type of object to cast the return value to.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type T.  If this is null, no callback will be made.</param>
        public static void ExecuteScalarAsync<T>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteScalarCallbackDelegate<T> callback = null)
        {
            new Task(() =>
            {
                var value = db.ExecuteScalar<T>();
                db.ExecuteScalarCallback<T>(value, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous scalar return operation against a SQL database using the provided options.
        /// </summary>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type Object.  If this is null, no callback will be made.</param>
        public static void ExecuteScalarAsync(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteScalarCallbackDelegate callback = null)
        {
            new Task(() =>
            {
                var value = db.ExecuteScalar();
                db.ExecuteScalarCallback(value, callback);
            }).Start();
        }
        #endregion

        #region ExecuteScalarEnumeration
        /// <summary>
        /// Performs a read operation against a SQL database assuming each returned row contains one column.  Returns an enumeration of each row casted to a T.
        /// </summary>
        /// <typeparam name="T">The type of object to cast the return values to.</typeparam>
        /// <returns>An enumeration of the first column of each row casted to a T.</returns>
        public static List<T> ExecuteScalarEnumeration<T>(this IDBAccess db)
        {
            return db.GenerateScalarEnumeration<T>(db.ExecuteReadQuick().DataRows);
        }

        /// <summary>
        /// Returns an enumeration of the first object in each array.
        /// </summary>
        /// <typeparam name="T">The type to convert each value to.</typeparam>
        /// <param name="rows">The data rows.</param>
        /// <returns>A list of each value converted to a T.</returns>
        internal static List<T> GenerateScalarEnumeration<T>(this IDBAccess db, List<Object[]> rows)
        {
            var ret = new List<T>(rows.Count);
            for (int i = 0; i < rows.Count; i++)
                ret.Add(rows[i][0].CastToT<T>());

            return ret;    
        }
        #endregion
        #region ExecuteScalarEnumerationAsync
        private static void ExecuteScalarEnumerationCallback<T>(this IDBAccess db, List<T> values, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteScalarEnumerationCallbackDelegate<T> callback)
        {
            if (callback != null)
                callback(values);
        }
        /// <summary>
        /// Performs an asynchronous read operation against a SQL database assuming each returned row contains one column.  Returns an enumeration of each row casted to a T.
        /// </summary>
        /// <typeparam name="T">The type of object to cast the return values to.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type List T.  If this is null, no callback will be made.</param>
        public static void ExecuteScalarEnumerationAsync<T>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteScalarEnumerationCallbackDelegate<T> callback = null)
        {
            new Task(() =>
            {
                var values = db.ExecuteScalarEnumeration<T>();
                db.ExecuteScalarEnumerationCallback<T>(values, callback);
            }).Start();
        }
        #endregion
    }
}