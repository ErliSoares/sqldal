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
using System.Data.Odbc;

namespace System.Data.DBAccess.Generic.Providers.ODBC
{
    /// <summary>
    /// A class providing functionality for interacting with data souces using an ODBC connection.
    /// </summary>
    public sealed class ODBCAccess : DotNETCompatibleProvider<OdbcConnection, OdbcCommand, OdbcParameter, OdbcTransaction, OdbcDataAdapter, OdbcException>
    {
        #region Constructors
        /// <summary>
        /// Instantiates a new ODBCAccess class.
        /// </summary>
        public ODBCAccess()
            : this(null)
        { }

        /// <summary>
        /// Instantiates a new ODBCAccess class with the given connection string.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        public ODBCAccess(String connString)
            : this(connString, null)
        { }

        /// <summary>
        /// Instantiates a new ODBCAccess class with the given options.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        /// <param name="sqlStatement">The SQL statement to execute.</param>
        public ODBCAccess(String connString, String sqlStatement)
            : this(connString, sqlStatement, null)
        { }

        /// <summary>
        /// Instantiates a new ODBCAccess class with the given options.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        /// <param name="sqlStatement">The SQL statement to execute.</param>
        /// <param name="input">An object representing the input parameters for the query.</param>
        public ODBCAccess(String connString, String sqlStatement, Object input)
            : base(connString, sqlStatement, input)
        { }
        #endregion

        /// <summary>
        /// Gets a connection string for Excel spreadsheet access via the JET driver.
        /// </summary>
        /// <param name="file">The xsl file to open.</param>
        /// <param name="useHeaderRow">True or false to use the first row as a header.</param>
        /// <param name="intermixedAsText">True or false to count intermixed data types in a column as all text.</param>
        /// <return>The connection string.</return>
        public static String GetExcel2003JETConnectionString(String file, Boolean useHeaderRow = true, Boolean intermixedAsText = true)
        {
            return String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR={1};IMEX={2}\";", file, useHeaderRow ? "Yes" : "No", intermixedAsText ? "1" : "0");
        }

        /// <summary>
        /// Gets a connection string for Excel spreadsheet access via Microsoft Access Database Engine.  Requires Microsoft Access Database Engine 2007/2010 to be installed.
        /// </summary>
        /// <param name="file">The xsl or xslx file to open.</param>
        /// <param name="useHeaderRow">True or false to use the first row as a header.</param>
        /// <param name="intermixedAsText">True or false to count intermixed data types in a column as all text.</param>
        /// <return>The connection string.</return>
        public static String GetExcelOfficeDatabaseEngineConnectionString(String file, Boolean useHeaderRow = true, Boolean intermixedAsText = true)
        {
            return String.Format("Driver={{Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)}};DBQ={0};Extended Properties=\"HDR={1};IMEX={1}\"", file, useHeaderRow ? "Yes" : "No", intermixedAsText ? "1" : "0");
        }

        /// <summary>
        /// Gets a connection string for reading from a flat text file via the JET driver.
        /// </summary>
        /// <param name="directory">The directory where the csv file resides.</param>
        /// <param name="useHeaderRow">True or false to use the first row as a header.</param>
        /// <param name="delimited">True or false to read the csv file as delimited or fixed width fields.</param>
        /// <return>The connection string.</return>
        public static String GetCSVJETConnectionString(String directory, Boolean useHeaderRow = true, Boolean delimited = true)
        {
            return String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"text;HDR={1};FMT={2}\";", directory, useHeaderRow ? "Yes" : "No", delimited ? "Delimited" : "Fixed");
        }

        /// <summary>
        /// Gets a connection string for reading from a flat text file.
        /// </summary>
        /// <param name="directory">The directory where the csv file resides.</param>
        /// <param name="fileExts">Comma delimited String of file extensions to consider for opening.</param>
        /// <return>The connection string.</return>
        public static String GetCSVDotNETConnectionString(String directory, String fileExts)
        {
            return String.Format("Driver={{Microsoft Text Driver (*.txt; *.csv)}};Dbq={0};Extensions={1};", directory, fileExts);
        }
    }
}