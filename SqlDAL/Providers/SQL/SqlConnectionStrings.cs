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

namespace System.Data.DBAccess.Generic.Providers.SQL
{
    public sealed partial class SqlDBAccess : IDBAccess
    {
        /// <summary>
        /// Gets a connection string for a SQL server.
        /// </summary>
        /// <param name="server">The SQL server to which to connect.</param>
        /// <param name="initialCatalog">The database to open.</param>
        /// <param name="username">The username with which to connect.</param>
        /// <param name="password">The password with which to connect.</param>
        /// <returns>The connection string.</returns>
        public static String GetConnectionString(String server, String initialCatalog, String username, String password)
        {
            return String.Format("server={0};initial catalog={1};uid={2};pwd={3}", server, initialCatalog, username, password);
        }

        /// <summary>
        /// Gets a connection string for a SQL server using integrated security.
        /// </summary>
        /// <param name="server">The SQL server to which to connect.</param>
        /// <param name="initialCatalog">The database to open.</param>
        /// <returns>The connection string.</returns>
        public static String GetConnectionStringWithIntegratedSecurity(String server, String initialCatalog)
        {
            return String.Format("server={0};initial catalog={1};integrated security=true", server, initialCatalog);
        }
    }
}