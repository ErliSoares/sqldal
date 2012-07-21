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
using System.Data.DBAccess.Generic.Exceptions;
using System.Data.DBAccess.Generic.Providers.DotNETCompatibleProvider;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.DBAccess.Generic.Providers.SQL
{
    /// <summary>
    /// A class providing functionality for interacting with a SQL database.
    /// </summary>
    public sealed partial class SqlDBAccess : DotNETCompatibleProvider<SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataAdapter, SqlException>
    {
        internal const Boolean DEFAULT_AUTO_ROLLBACK_COMPLETE = false;

        /// <summary>
        /// Messges returned by the RAISERROR method on the SQL server from the last query. This is read only.
        /// </summary>
        public List<String> CustomSqlErrors { get; private set; }

        /// <summary>
        /// Returns true if RAISERROR messages were thrown during the last call. This is read only.
        /// </summary>
        public Boolean HasCustomError { get { return this.CustomSqlErrors.Any(); } }

        #region Customizable Settings and constructors

        /// <summary>
        /// Instantiates a new SqlDBAccess class.
        /// </summary>
        public SqlDBAccess()
            : this(null)
        { }

        /// <summary>
        /// Instantiates a new SqlDBAccess class with the given connection string.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        public SqlDBAccess(String connString)
            : this(connString, null)
        { }

        /// <summary>
        /// Instantiates a new SqlDBAccess class with the given options.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        /// <param name="sqlStatement">The SQL statement to execute.</param>
        public SqlDBAccess(String connString, String sqlStatement)
            : this(connString, sqlStatement, null)
        { }

        /// <summary>
        /// Instantiates a new SqlDBAccess class with the given options.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        /// <param name="sqlStatement">The SQL statement to execute.</param>
        /// <param name="input">An object representing the input parameters for the query.</param>
        public SqlDBAccess(String connString, String sqlStatement, Object input)
            : base(connString, sqlStatement, input)
        {
            this.AutoRollbackCompletely = SqlDBAccess.DEFAULT_AUTO_ROLLBACK_COMPLETE;
            this.CustomSqlErrors = new List<String>();
            this.SavePoints = new Stack<String>();
        }

        #endregion
        #region Transactions
        /// <summary>
        /// Stack of transaction save points.
        /// </summary>
        internal Stack<String> SavePoints { get; set; }

        /// <summary>
        /// The transation name.
        /// </summary>
        internal String TransactionName { get; set; }

        /// <summary>
        /// Name of the last transaction save point.  Used by rollback to last save point functionality.
        /// </summary>
        internal String LastSavePoint
        {
            get { return this.SavePoints.FirstOrDefault(); }
        }

        /// <summary>
        /// Set to true to always rollback completely on auto rollback regardless of if any save points are available.
        /// </summary>
        public Boolean? AutoRollbackCompletely { get; set; }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <param name="autoRollBackCompletely">Specifies whether or not the transaction should be completely auto rolled back on exceptions or just to the last save point.  Defaults to false (rollback to last save point).</param>
        /// <param name="autoRollback">Specifies whether or not exceptions should cause the DAL to automatically rollback.  The name of the last save point will be used if available.  Defaults to true.</param>
        public void BeginTransaction(Boolean? autoRollBackCompletely = null, Boolean? autoRollback = null)
        {
            this.BeginTransaction(null, autoRollBackCompletely ?? this.AutoRollbackCompletely ?? SqlDBAccess.DEFAULT_AUTO_ROLLBACK_COMPLETE, autoRollback ?? this.AutoRollback ?? SqlDBAccess.DEFAULT_AUTO_ROLLBACK);
        }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <param name="iso">The isolation level under which the transaction should run.</param>
        /// <param name="autoRollBackCompletely">Specifies whether or not the transaction should be completely auto rolled back on exceptions or just to the last save point.  Defaults to true.</param>
        /// <param name="autoRollback">Specifies whether or not Exceptions should cause the DAL to automatically roll back.  The name of the last save point will be used if available.  Defaults to true.</param>
        public void BeginTransaction(IsolationLevel iso, Boolean? autoRollBackCompletely = null, Boolean? autoRollback = null)
        {
            this.BeginTransaction(iso, null, autoRollBackCompletely ?? this.AutoRollbackCompletely ?? SqlDBAccess.DEFAULT_AUTO_ROLLBACK_COMPLETE, autoRollback ?? this.AutoRollback ?? SqlDBAccess.DEFAULT_AUTO_ROLLBACK);
        }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <param name="transactionName">The name of the transaction.</param>
        /// <param name="autoRollBackCompletely">Specifies whether or not the transaction should be completely auto rolled back on exceptions or just to the last save point.  Defaults to true.</param>
        /// <param name="autoRollback">Specifies whether or not Exceptions should cause the DAL to automatically roll back.  The name of the last save point will be used if available.  Defaults to true.</param>
        public void BeginTransaction(String transactionName, Boolean? autoRollBackCompletely = null, Boolean? autoRollback = null)
        {
            this.BeginTransaction(IsolationLevel.ReadCommitted, transactionName, autoRollBackCompletely ?? this.AutoRollbackCompletely ?? SqlDBAccess.DEFAULT_AUTO_ROLLBACK_COMPLETE, autoRollback ?? this.AutoRollback ?? SqlDBAccess.DEFAULT_AUTO_ROLLBACK);
        }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <param name="iso">The isolation level under which the transaction should run.</param>
        /// <param name="transactionName">The name of the transaction.</param>
        /// <param name="autoRollBackCompletely">Specifies whether or not the transaction should be completely auto rolled back on exceptions or just to the last save point.  Defaults to true.</param>
        /// <param name="autoRollback">Specifies whether or not Exceptions should cause the DAL to automatically roll back.  The name of the last save point will be used if available.  Defaults to true.</param>
        /// <exception cref="TransactionInProgressException">Thrown if a transaction is already in progress.</exception>
        public void BeginTransaction(IsolationLevel iso, String transactionName, Boolean? autoRollBackCompletely = null, Boolean? autoRollback = null)
        {
            if (this.Transaction != null)
                throw new TransactionInProgressException("There is already a transaction in progress.");

            this.Isolation = iso;
            this.TransactionName = transactionName;
            this.NextIsTransaction = true;
            this.AutoRollback = autoRollback ?? this.AutoRollback ?? SqlDBAccess.DEFAULT_AUTO_ROLLBACK;
            this.AutoRollbackCompletely = autoRollBackCompletely ?? this.AutoRollbackCompletely ?? SqlDBAccess.DEFAULT_AUTO_ROLLBACK_COMPLETE;
            this.SavePoints.Clear();

            this.WriteTrace(TraceLevel.INFORMATION, "Beginning transaction with the options:{0}Isolation: {1}{0}Transaction Name: {2}{0}Auto rollback: {3}{0}Auto rollback completely: {4}",
                                               Environment.NewLine,
                                               this.Isolation,
                                               this.TransactionName,
                                               this.AutoRollback,
                                               this.AutoRollbackCompletely);
        }

        /// <summary>
        /// Rolls back a transaction from a pending state.
        /// </summary>
        /// <param name="savePointName">The name of the transaction to roll back, or the savepoint to which to roll back.</param>
        /// <exception cref="TransactionNotInProgress">Thrown if there is no transaction in progress.</exception>
        /// <exception cref="TransactionSavePointNotFoundException">Thrown if the supplied savepoint was not found.</exception>
        public void RollbackTransaction(String transactionName)
        {
            if (this.Transaction == null)
                throw new TransactionNotInProgressException("There is no transaction to rollback.");

            if (!this.SavePoints.Any(sp => sp == transactionName))
                throw new TransactionSavePointNotFoundException(String.Format("There is no save point with the name '{0}' associated with this transaction.", transactionName));

            this.WriteTrace(TraceLevel.INFORMATION, "Rolling back transaction to save point {0} against {1}", transactionName, this.Transaction.Connection.ConnectionString);

            this.Transaction.Rollback(transactionName);
            while (this.SavePoints.Pop() != transactionName) ;
        }

        /// <summary>
        /// Creates a savepoint in the transaction that can be used to roll back a part of the transaction, and specifies the savepoint name.
        /// </summary>
        /// <param name="savePointName">The name of the savepoint.</param>
        /// <exception cref="TransactionNotInProgress">Thrown if there is no transaction in progress.</exception>
        /// <exception cref="TransactionSavePointNotFoundException">Thrown if the supplied savepoint was not found.</exception>
        public void SaveTransaction(String savePointName)
        {
            if (this.Transaction == null)
                throw new TransactionNotInProgressException("There is no transaction to save.");

            if (this.SavePoints.Any(sp => sp == savePointName))
                throw new TransactionSavePointAlreadyExistsException(String.Format("There is already a save point with the name '{0}' associated with this transaction.", savePointName));

            this.WriteTrace(TraceLevel.INFORMATION, "Creating save point named {0} for transaction against {1}", savePointName, this.Transaction.Connection.ConnectionString);

            this.Transaction.Save(savePointName);
            this.SavePoints.Push(savePointName);
        }

        /// <summary>
        /// Rolls the transaction back to the last save point.
        /// </summary>
        /// <exception cref="TransactionNoSavePointsFoundException">Thrown if there are no save points associated with the transaction.</exception>
        public void RollbackToLastSavePoint()
        {
            if (this.LastSavePoint == null)
                throw new TransactionNoSavePointsFoundException("No save point to roll back.");

            this.WriteTrace(TraceLevel.INFORMATION, "Rolling back transaction to last save point named {0} against {1}", this.LastSavePoint, this.Transaction.Connection.ConnectionString);

            this.RollbackTransaction(this.LastSavePoint);
        }

        /// <summary>
        /// Performs an auto rollback on exception if there is a transaction to rollback.
        /// </summary>
        protected internal override void PerformAutoRollback()
        {
            if (this.Transaction == null || !this.AutoRollback.Value)
                return;

            this.WriteTrace(TraceLevel.INFORMATION, "Performing auto rollback of transaction against {0}", this.Transaction.Connection.ConnectionString);

            if (this.AutoRollbackCompletely.Value)
            {
                this.RollbackTransaction();
            }
            else if (this.AutoRollback.Value)
            {
                //do we have a name to rollback to?
                if (this.LastSavePoint == null)
                    this.RollbackTransaction();
                else
                    this.RollbackToLastSavePoint();
            }
        }
        #endregion

        public String GetDynamicSelectStatement(String input, Boolean nolock = true)
        {
            return AutoSelectQuery.GetSelectQuery(this.ConnectionString, input, nolock);
        }

        #region Parameter helper Functions
        /// <summary>
        /// Creates a DataTable object from an enumeration of objects.
        /// </summary>
        /// <param name="value">The enumeration.</param>
        /// <returns>The DataTable object.</returns>
        internal DataTable CreateDataTableFromModelProperty(Object value)
        {
            //Enumerable, make it into a DataTable parameter
            var enumeration = value as System.Collections.IEnumerable;
            var dt = new DataTable();
            var udtType = enumeration.GetIEnumerableGenericType();

            this.WriteTrace(TraceLevel.DEBUG, "Creating DataTable for type {0}.", udtType);

            if (!((IDBAccess)this).ModelsData.ContainsKey(udtType))
            {
                this.WriteTrace(TraceLevel.DEBUG, "Validating type for DAL usage.");
                //this is because an anonymous type does not have a parameterless constructor.
                if (udtType.IsAnonymousType())
                {
                    this.WriteTrace(TraceLevel.DEBUG, "Anonymous type, Validating with uninitialized object.");
                    this.ValidateForDAL(FormatterServices.GetUninitializedObject(udtType));
                }
                else
                    this.ValidateForDAL(Activator.CreateInstance(udtType));
            }

            // need to enumerate over all properties and add them as columns
            #region Manually populate

            dt.BeginLoadData();
            if (udtType.DerivesInterface(typeof(IQuickRead)))
            {
                var eOfIQuickRead = enumeration.OfType<IQuickRead>();
                this.WriteTrace(TraceLevel.DEBUG, "Quick read type.  Adding columns from GetColumnNamesTypes function.");
                dt.Columns.AddRange(eOfIQuickRead.First().GetColumnNamesTypes().Select(cnt => new DataColumn(cnt.Key, cnt.Value)).ToArray());

                this.WriteTrace(TraceLevel.DEBUG, "Adding {0} row{1} to data table.", eOfIQuickRead.Count(), eOfIQuickRead.Count() == 1 ? "" : "s");
                foreach (IQuickRead row in eOfIQuickRead)
                    dt.Rows.Add(row.ToObjectArray());
            }
            else
            {
                this.WriteTrace(TraceLevel.DEBUG, "Non quick read type.  Generating columns from model cache.");
                dt.Columns.AddRange(this.GetSprocInputNamesTypes(udtType, ((IDBAccess)this).ModelsData[udtType]).Select(kvp => new DataColumn(this.GetPropertySprocParameterName(udtType, kvp.Key), (kvp.Value.IsNullableValueType() ? kvp.Value.GetGenericArguments()[0] : kvp.Value))).ToArray());

                this.WriteTrace(TraceLevel.DEBUG, "Building lists required for getting model properties.");
                var data = ((IDBAccess)this).ModelsData[udtType];
                var props = data.AllNestedModelPropertyNames.Where(p => data.ModelPropertiesAccessors[data.SprocParamterNameToModelPropertyName[p]].HasGetter).ToList();
                var modelPropertyNames = props.Select(p => data.SprocParamterNameToModelPropertyName[p]).ToList();
                var modelPropertyFormats = modelPropertyNames.Select(p => data.ModelReadStringFormats[p]).ToList();

                var eObjs = enumeration.OfType<Object>();

                this.WriteTrace(TraceLevel.DEBUG, "Adding {0} row{1} to data table.", eObjs.Count(), eObjs.Count() == 1 ? "" : "s");
                foreach (Object row in eObjs)
                    dt.Rows.Add(this.ToObjectArray(row, udtType, data, modelPropertyNames, modelPropertyFormats));
            }
            dt.EndLoadData();
            #endregion

            return dt;
        }

        /// <summary>
        /// Converts a model property into a SqlParameter or enumeration of parameters if the model property was a nested model.
        /// </summary>
        /// <param name="data">The ModelData object of the input type.</param>
        /// <param name="input">The model which contains the property to prepare.</param>
        /// <param name="prefixDirection">True/False if parameter directions should be prefixed to the parameter names.  Defaults to what is set on the class.</param>
        /// <param name="propertyName">The name of the property to prepare.</param>
        /// <param name="value">The value of the property.</param>
        /// <param name="inputPrefix">The string to prefix to the beginning of input parameter names if prefixDirection is being used.  Defaults to what is set on the class.</param>
        /// <param name="outputPrefix">The string to prefix to the beginning of output parameter names if prefixDirection is being used.  Defaults to what is set on the class.</param>
        /// <returns>The prepared parameter or parameters.</returns>
        protected override IEnumerable<SqlParameter> PrepareSingleParameter(ModelData data, Object input, Boolean? prefixDirection, String propertyName, Object value, String inputPrefix = null, String outputPrefix = null)
        {
            this.WriteTrace(TraceLevel.INFORMATION, "Preparing property {0}: {1} of type {2}", propertyName, value, value == null ? null : value.GetType());

            Type modelType = input.GetType();

            String name = this.GetPropertySprocParameterName(modelType, propertyName);

            prefixDirection = prefixDirection ?? this.PrefixDirection;

            if (value != null && this.IsNestedProperty(modelType, propertyName))  // nested ModelBase
            {
                this.WriteTrace(TraceLevel.INFORMATION, "Property is a nested model.  Preparing the nested model's properties.");
                return this.PrepareParameters(value, prefixDirection);
            }

            Object sqlValue;
            DbType type = DbType.AnsiString;

            Boolean parameterIsDataTable = value != null && value.IsUDTableEnumeration();

            // create parameter's name
            if (prefixDirection.Value)
            {
                if (this.GetPropertyDirection(modelType, propertyName) == ParameterDirection.Input)
                    name = inputPrefix ?? this.InputParameterPrefix + name;
                else
                    name = outputPrefix ?? this.OutputParameterPrefix + name;
            }
            else
                name = '@' + name;

            this.WriteTrace(TraceLevel.DEBUG, "SqlParameter name: {0}", name);

            // create parameter's value
            if (parameterIsDataTable)
            {
                this.WriteTrace(TraceLevel.DEBUG, "Parameter is a data table parameter.  Preparing a DataTable object.");
                sqlValue = this.CreateDataTableFromModelProperty(value);
            }
            else
            {
                if (this.IsNullableValueType(modelType, propertyName, data))
                {
                    this.WriteTrace(TraceLevel.DEBUG, "Property is nullable value type.");
                    sqlValue = this.GetNullableValueTypeValue(input, propertyName, data);
                }
                else
                    sqlValue = value;

                // string.format to apply?
                String format = ((IDBAccess)this).ModelsData[modelType].ModelReadStringFormats[propertyName];
                if (format != null)
                {
                    this.WriteTrace(TraceLevel.DEBUG, "Applying string format to property value: {0}", format);
                    sqlValue = String.Format(format, sqlValue ?? "");
                }

                // set parameter's type
                type = this.GetPropertyType(modelType, propertyName, null).ToDbType();
            }

            // set parameter's direction
            ParameterDirection dir = this.GetPropertyDirection(modelType, propertyName);

            this.WriteTrace(TraceLevel.DEBUG, "Parameter direction: {0}", dir);
            this.WriteTrace(TraceLevel.DEBUG, "Parameter value: {0}", sqlValue);

            if (parameterIsDataTable)
            {
                this.WriteTrace(TraceLevel.DEBUG, "Parameter's SQL type: {0}", SqlDbType.Structured);
                var retval = new List<SqlParameter>
                {
                    new SqlParameter
                    {
                        ParameterName = name,
                        Value = sqlValue,
                        SqlDbType = SqlDbType.Structured,
                        Direction = dir
                    }
                };

                if (sqlValue == null)
                    retval[0].Size = -1;

                return retval;
            }
            else
            {
                this.WriteTrace(TraceLevel.DEBUG, "Parameter's SQL type: {0}", type);

                var retval = new List<SqlParameter>
                {
                    new SqlParameter
                    {
                        ParameterName = name,
                        Value = sqlValue,
                        DbType = type,
                        Direction = dir
                    }
                };

                if (sqlValue == null)
                    retval[0].Size = -1;

                return retval;
            }
        }
        #endregion

        #region Query helper functions
        /// <summary>
        /// Raises the OnSqlException event with the provided information.
        /// </summary>
        /// <param name="sqlex">The SqlException object.</param>
        /// <param name="conn">The SqlConnection object on which the exception happened.</param>
        /// <param name="parameters">The parameters used in the query.</param>
        /// <param name="timeout">The command timeout value used.</param>
        /// <param name="queryString">The query string which caused the exception.</param>
        protected override void HandleOnQueryException(SqlException sqlex, SqlConnection conn, IEnumerable<SqlParameter> parameters, int? timeout, String queryString)
        {
            var custErrors = sqlex.Errors.OfType<SqlError>().Where(se => se.Number == 50000);
            this.CustomSqlErrors = custErrors.Select(se => se.Message).ToList();

            this.RaiseOnSqlException(new DALSqlExceptionEventArgs
            {
                Connection = conn,
                Transaction = null,
                QueryMethod = this.GetOriginalCallingFunctionName(),
                Exception = sqlex,
                InputParameters = parameters,
                Timeout = timeout ?? this.CommandTimeout,
                QueryString = queryString,
                InputModelType = this.GetModelBaseTypeFromOriginalCallingFunction(),
                HasCustomErrors = custErrors.Any(),
                CustomErrors = this.CustomSqlErrors,
                Input = this.Input
            });
        }
        #endregion
    }
}