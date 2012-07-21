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

using System.Data.DBAccess.Generic.Providers.DotNETCompatibleProvider;
using MySql.Data.MySqlClient;

namespace System.Data.DBAccess.Generic.Providers.MySQL
{
    /// <summary>
    /// A class providing functionality for interacting with data souces using a MySQL connection.
    /// </summary>
    public sealed class MySQLAccess : DotNETCompatibleProvider<MySqlConnection, MySqlCommand, MySqlParameter, MySqlTransaction, MySqlDataAdapter, MySqlException>
    {
        #region Constructors
        /// <summary>
        /// Instantiates a new MySQLAccess class.
        /// </summary>
        public MySQLAccess()
            : this(null)
        { }

        /// <summary>
        /// Instantiates a new MySQLAccess class with the given connection string.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        public MySQLAccess(String connString)
            : this(connString, null)
        { }

        /// <summary>
        /// Instantiates a new MySQLAccess class with the given options.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        /// <param name="sqlStatement">The SQL statement to execute.</param>
        public MySQLAccess(String connString, String sqlStatement)
            : this(connString, sqlStatement, null)
        { }

        /// <summary>
        /// Instantiates a new MySQLAccess class with the given options.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        /// <param name="sqlStatement">The SQL statement to execute.</param>
        /// <param name="input">An object representing the input parameters for the query.</param>
        public MySQLAccess(String connString, String sqlStatement, Object input)
            : base(connString, sqlStatement, input)
        { }
        #endregion

        /// <summary>
        /// Generates a MySQL connection string using the provided options.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="database">The database name.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static String GetMySQLConnectionString(String server, String database, String username, String password)
        {
            return MySQLAccess.GetMySQLConnectionString(server, database, 0, username, password);
        }

        /// <summary>
        /// Generates a MySQL connection string using the provided options.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="database">The database name.</param>
        /// <param name="port">The port to use.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static String GetMySQLConnectionString(String server, String database, int port, String username, String password)
        {
            return String.Format("Server={0};{1}Database={2};Uid={3};Pwd={4}", server, port == 0 ? "" : String.Format("Port={0};", port), database, username, password);
        }

        /// <summary>
        /// Generates a MySQL connection string using named pipes and the provided options.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="database">The database name.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static String GetMySQLConnectionStringWithNamedPipes(String server, String database, String username, String password)
        {
            return MySQLAccess.GetMySQLConnectionString(server, database, -1, username, password);
        }
    }
}