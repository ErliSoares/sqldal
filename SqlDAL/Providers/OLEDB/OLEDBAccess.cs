using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.DBAccess.Generic.Providers.DotNETCompatibleProvider;

namespace System.Data.DBAccess.Generic.Providers.OLEDB
{
    public class OLEDBAccess : DotNETCompatibleProvider<OleDbConnection, OleDbCommand, OleDbParameter, OleDbTransaction, OleDbDataAdapter, OleDbException>
    {
        #region Constructors
        /// <summary>
        /// Instantiates a new ODBCAccess class.
        /// </summary>
        public OLEDBAccess()
            : this(null)
        { }

        /// <summary>
        /// Instantiates a new ODBCAccess class with the given connection string.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        public OLEDBAccess(String connString)
            : this(connString, null)
        { }

        /// <summary>
        /// Instantiates a new ODBCAccess class with the given options.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        /// <param name="sqlStatement">The SQL statement to execute.</param>
        public OLEDBAccess(String connString, String sqlStatement)
            : this(connString, sqlStatement, null)
        { }

        /// <summary>
        /// Instantiates a new ODBCAccess class with the given options.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        /// <param name="sqlStatement">The SQL statement to execute.</param>
        /// <param name="input">An object representing the input parameters for the query.</param>
        public OLEDBAccess(String connString, String sqlStatement, Object input)
            : base(connString, sqlStatement, input)
        { }
        #endregion
    }
}
