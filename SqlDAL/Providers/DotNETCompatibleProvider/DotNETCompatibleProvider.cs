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
using System.Linq;
using System.Diagnostics;

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
        #region Constructors
        /// <summary>
        /// Instantiates a new IDBAccess class.
        /// </summary>
        protected DotNETCompatibleProvider()
            : this(null)
        { }

        /// <summary>
        /// Instantiates a new IDBAccess class with the given connection string.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        protected DotNETCompatibleProvider(String connString)
            : this(connString, null)
        { }

        /// <summary>
        /// Instantiates a new IDBAccess class with the given options.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        /// <param name="queryString">The SQL statement to execute.</param>
        protected DotNETCompatibleProvider(String connString, String queryString)
            : this(connString, queryString, null)
        { }

        /// <summary>
        /// Instantiates a new IDBAccess class with the given options.
        /// </summary>
        /// <param name="connString">The connection string to use for queries.</param>
        /// <param name="queryString">The SQL statement to execute.</param>
        /// <param name="input">An object representing the input parameters for the query.</param>
        protected DotNETCompatibleProvider(String connString, String queryString, Object input)
            : this(connString, queryString, input, DEFAULT_TIMEOUT, DEFAULT_IS_SPROC, DEFAULT_PREFIX_DIRECTION, DEFAULT_INPUT_PREFIX, DEFAULT_OUTPUT_PREFIX, DEFAULT_POPULATE_DEFAULT_VALUES)
        { }

        /// <summary>
        /// Private constructor which accepts all settings for the IDBAccess object.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <param name="queryString">The query string.</param>
        /// <param name="input">The input object.</param>
        /// <param name="timeout">The command timeout.</param>
        /// <param name="isStoredProcedure">The isStoredProcedure setting.</param>
        /// <param name="prefixDirection">The prefix direction setting.</param>
        /// <param name="inputPrefix">The input prefix string.</param>
        /// <param name="outputPrefix">The output prefix string.</param>
        /// <param name="populateDefaultValues">The populate default values setting.</param>
        private DotNETCompatibleProvider(String connString, String queryString, Object input, int timeout, Boolean isStoredProcedure, Boolean prefixDirection, String inputPrefix, String outputPrefix, Boolean populateDefaultValues)
        {
            this.ConnectionString = connString;
            this.QueryString = queryString;
            this.Input = input;
            this.CommandTimeout = timeout;
            this.IsStoredProcedure = isStoredProcedure;
            this.PrefixDirection = prefixDirection;
            this.InputParameterPrefix = inputPrefix;
            this.OutputParameterPrefix = outputPrefix;
            this.PopulateDefaultValues = populateDefaultValues;
            this.TraceOutputLevel = TraceLevel.NONE;

            this.OutputValues = new Dictionary<String, Object>();
            ((IDBAccess)this).ModelsData = new Dictionary<Type, ModelData>();
            this.ColumnToPropertyMappings = new Dictionary<DataTable, Dictionary<Type, Dictionary<String, String>>>();
        }
        #endregion

        #region Default values
        /// <summary>
        /// The default CommandTimeout value if none is set.  30 seconds.
        /// </summary>
        internal const int DEFAULT_TIMEOUT = 30;

        /// <summary>
        /// The default input prefix string if none is set.  @i
        /// </summary>
        internal const String DEFAULT_INPUT_PREFIX = "@i";

        /// <summary>
        /// The default output prefix string if none is set.  @o
        /// </summary>
        internal const string DEFAULT_OUTPUT_PREFIX = "@o";

        /// <summary>
        /// The default setting whether or not the query is a stored proc if none is set.  True.
        /// </summary>
        internal const Boolean DEFAULT_IS_SPROC = true;

        /// <summary>
        /// The default setting for including the direction prefix if none is set.  True.
        /// </summary>
        internal const Boolean DEFAULT_PREFIX_DIRECTION = true;

        /// <summary>
        /// The default populate default values setting if none is set.  False.
        /// </summary>
        internal const Boolean DEFAULT_POPULATE_DEFAULT_VALUES = false;

        /// <summary>
        /// The default auto rollback setting for transactions if none is set.  True.
        /// </summary>
        internal const Boolean DEFAULT_AUTO_ROLLBACK = true;

        /// <summary>
        /// Empty model used in the case that input == null when preparing parameters.
        /// </summary>
        internal static readonly Object EmptyModel = new { };
        #endregion

        #region Properties
        /// <summary>
        /// Maps column names to property names.
        /// </summary>
        internal Dictionary<DataTable, Dictionary<Type, Dictionary<String, String>>> ColumnToPropertyMappings { get; private set; }

        /// <summary>
        /// Sets whether or not model properties not returned by a query should be defaulted to the value defined in the [DALDefaultValue] attribute.
        /// </summary>
        public Boolean PopulateDefaultValues { get; set; }

        /// <summary>
        /// The level of trace information to output.
        /// </summary>
        public TraceLevel TraceOutputLevel { get; set; }

        /// <summary>
        /// Default value for the prefix direction setting used by queries. Defaults to true.
        /// </summary>
        public Boolean PrefixDirection { get; set; }

        /// <summary>
        /// The string to be prefixed to the input parameter names when prefixing is enabled.  Defaults to "@i".
        /// </summary>
        public String InputParameterPrefix { get; set; }

        /// <summary>
        /// The string to be prefixed to the output parameter names when prefixing is enabled.  Defaults to "@o".
        /// </summary>
        public String OutputParameterPrefix { get; set; }

        /// <summary>
        /// Dictionary of cached model data used by the DAL.
        /// </summary>
        Dictionary<Type, ModelData> IDBAccess.ModelsData { get; set; }

        /// <summary>
        /// Any NonQuery action will throw an event if the number of affected rows does not match this property. Defaults to null (disabled).
        /// </summary>
        public int? ExpectedAffectedRows { get; set; }

        /// <summary>
        /// The connection string to use if no connection object is passed to the query functions.
        /// </summary>
        public String ConnectionString { get; set; }

        /// <summary>
        /// The query string to be used.
        /// </summary>
        public String QueryString { get; set; }

        /// <summary>
        /// The object to be used as input to the query.  This can be any non framework class or an enumeration of IDbParameter object.
        /// </summary>
        public Object Input { get; set; }

        /// <summary>
        /// The name of the last model type used when preparing parameters.  This is used for giving back this information in exception events.
        /// </summary>
        protected String LastInputType { get; set; }

        /// <summary>
        /// The connection object to use if there is no connection is passed to the query functions.  This takes presedence over the ConnectringString property.
        /// </summary>
        public TConnection Connection { get; set; }

        /// <summary>
        /// List of names to assign runtime types when serializing to XML.
        /// </summary>
        public List<String> XMLTableNames { get; set; }

        /// <summary>
        /// The command timeout value in seconds. Defaults to 30 seconds.
        /// </summary>
        public int CommandTimeout { get; set; }

        /// <summary>
        /// Any output parameters that result from a will be populated in this dictionary. This is read only.
        /// </summary>
        public Dictionary<String, Object> OutputValues { get; protected set; }

        /// <summary>
        /// Default value for the stored procedure setting used by queries. Defaults to true.
        /// </summary>
        public Boolean IsStoredProcedure { get; set; }
        #endregion

        #region Transactions
        /// <summary>
        /// The transaction object being used for queries.
        /// </summary>
        internal TTransaction Transaction { get; set; }

        /// <summary>
        /// The isolation level of the transaction.  Defaults to IsolationLevel.ReadCommitted.
        /// </summary>
        public IsolationLevel Isolation { get; protected set; }

        /// <summary>
        /// True/False if the next query to run should start a transaction.
        /// </summary>
        internal Boolean NextIsTransaction { get; set; }

        /// <summary>
        /// Sets whether or not a query exception will trigger and auto rollback of the transaction if one is in progress.  Defaults to true.
        /// </summary>
        public Boolean? AutoRollback { get; set; }

        /// <summary>
        /// Returns whether or not a transaction is currently taking place.
        /// </summary>
        public Boolean InTransaction { get { return this.Transaction != null || this.NextIsTransaction; } }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <param name="autoRollback">Specifies whether or not Exceptions should cause the DAL to automatically roll back.  The name of the last save point will be used if available.  Defaults to true.</param>
        public virtual void BeginTransaction(Boolean? autoRollback = null)
        {
            this.BeginTransaction(IsolationLevel.ReadCommitted, autoRollback ?? this.AutoRollback ?? DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.DEFAULT_AUTO_ROLLBACK);
        }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <param name="iso">The isolation level under which the transaction should run.</param>
        /// <param name="autoRollback">Specifies whether or not Exceptions should cause the DAL to automatically roll back.  The name of the last save point will be used if available.  Defaults to true.</param>
        /// <exception cref="TransactionInProgressException">Thrown if a transaction is already in progress.</exception>
        public virtual void BeginTransaction(IsolationLevel iso, Boolean? autoRollback = null)
        {
            if (this.Transaction != null)
                throw new TransactionInProgressException("There is already a transaction in progress.");

            this.Isolation = iso;
            this.NextIsTransaction = true;
            this.AutoRollback = autoRollback ?? this.AutoRollback ?? DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.DEFAULT_AUTO_ROLLBACK;

            this.WriteTrace(TraceLevel.INFORMATION, "Beginning transaction with the options:{0}Isolation: {1}{0}Auto rollback: {3}",
                                               Environment.NewLine,
                                               this.Isolation,
                                               this.AutoRollback);
        }

        /// <summary>
        /// Commits the database transaction.
        /// </summary>
        /// <exception cref="TransactionNotInProgressException">Thrown if a transaction is not in progress.</exception>
        public virtual void CommitTransaction()
        {
            if (this.Transaction == null)
                throw new TransactionNotInProgressException("There is no transaction to commit.");

            this.WriteTrace(TraceLevel.INFORMATION, "Committing transaction against {0}", this.Transaction.Connection.ConnectionString);

            this.Transaction.Commit();
            this.Transaction.Dispose();
            this.Transaction = null;
        }

        /// <summary>
        /// Rolls back a transaction from a pending state.
        /// </summary>
        /// <exception cref="TransactionNotInProgressException">Thrown if a transaction is not in progress.</exception>
        public virtual void RollbackTransaction()
        {
            if (this.Transaction == null)
                throw new TransactionNotInProgressException("There is no transaction to rollback.");

            this.WriteTrace(TraceLevel.INFORMATION, "Rolling back transaction against {0}", this.Transaction.Connection.ConnectionString);

            this.Transaction.Rollback();
            this.Transaction.Dispose();
            this.Transaction = null;
        }

        /// <summary>
        /// Performs an auto rollback on exception if there is a transaction to rollback.
        /// </summary>
        protected internal virtual void PerformAutoRollback()
        {
            if (this.Transaction == null || !this.AutoRollback.Value)
                return;

            this.WriteTrace(TraceLevel.INFORMATION, "Performing auto rollback of transaction against {0}", this.Transaction.Connection.ConnectionString);
            this.RollbackTransaction();
        }
        #endregion

        #region Parameter helpers
        /// <summary>
        /// Creates an IDbDataParamter enumeration based on the provided options.
        /// </summary>
        /// <param name="input">The input object which contains the input values.  Defaults to null.</param>
        /// <param name="prefixDirection">True or false to prefix a string to the beginning of parameter names.  Defaults to true or the setting in the IDBAccess class.</param>
        /// <param name="inputPrefix">The string to prefix to input paramter names when using the prefix direction option.  Defaults to "i" or the setting in the IDBAccess class.</param>
        /// <param name="outputPrefix">The string to prefix to output parameter names when using the prefix direction option.  Defaults to "o" or the setting in the IDBAccess class.</param>
        /// <returns>The IDbDataParameters.</returns>
        public virtual IEnumerable<TParameter> PrepareParameters(Object input, Boolean? prefixDirection = null, String inputPrefix = null, String outputPrefix = null)
        {
            input = input ?? this.Input ?? DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.EmptyModel;

            this.LastInputType = input.GetType().Name;
            if (this.LastInputType == DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.EmptyModel.GetType().Name)
                this.LastInputType = null;

            this.WriteTrace(TraceLevel.INFORMATION, "Preparing parameters with the following options:{0}Prefix direction: {1}{0}Input prefix: {2}{0}Output prefix: {3}{0}",
                                               Environment.NewLine,
                                               prefixDirection ?? this.PrefixDirection,
                                               inputPrefix ?? this.InputParameterPrefix,
                                               outputPrefix ?? this.OutputParameterPrefix);

            if (input is IEnumerable<TParameter>)
            {
                this.WriteTrace(TraceLevel.DEBUG, "Input is already an IDbDataParameter enumeration.  Returning.");
                return input as IEnumerable<TParameter>;
            }

            // if the model's not been seen before, validate
            if (!((IDBAccess)this).ModelsData.ContainsKey(input.GetType()))
            {
                this.WriteTrace(TraceLevel.DEBUG, "Validating type for DAL usage.");
                this.ValidateForDAL(input);
            }

            ModelData data = ((IDBAccess)this).ModelsData[input.GetType()];

            var parameters = this.GetSprocInputNamesValues(input);
            this.WriteTrace(TraceLevel.DEBUG, "Input values from class:{0}{1}",
                                               Environment.NewLine,
                                               String.Join(Environment.NewLine, parameters.Select(p => String.Format("{0}: {1}", p.Key, p.Value))));

            var ret = parameters.SelectMany(p => this.PrepareSingleParameter(data, input, prefixDirection, p.Key, p.Value, inputPrefix, outputPrefix)).ToList();

            this.WriteTrace(TraceLevel.INFORMATION, "{2} values:{0}{1}",
                                   Environment.NewLine,
                                   String.Join(Environment.NewLine, ret.Select(p => String.Format("{0}: {1}", p.ParameterName, p.Value))),
                                   typeof(TParameter).Name);

            return ret;
        }

        // this returns an enumeration in case that the "single" parameter is a nested ModelBase, this will return parameters for the entire nested model
        /// <summary>
        /// Converts a model property into an IDbDataParameter or enumeration of parameters if the model property was a nested model.
        /// </summary>
        /// <param name="data">The ModelData object of the input type.</param>
        /// <param name="input">The model which contains the property to prepare.</param>
        /// <param name="prefixDirection">True/False if parameter directions should be prefixed to the parameter names.  Defaults to what is set on the class.</param>
        /// <param name="propertyName">The name of the property to prepare.</param>
        /// <param name="value">The value of the property.</param>
        /// <param name="inputPrefix">The string to prefix to the beginning of input parameter names if prefixDirection is being used.  Defaults to what is set on the class.</param>
        /// <param name="outputPrefix">The string to prefix to the beginning of output parameter names if prefixDirection is being used.  Defaults to what is set on the class.</param>
        /// <returns>The prepared parameter or parameters.</returns>
        protected virtual IEnumerable<TParameter> PrepareSingleParameter(ModelData data, Object input, Boolean? prefixDirection, String propertyName, Object value, String inputPrefix = null, String outputPrefix = null)
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

            this.WriteTrace(TraceLevel.DEBUG, "IDbDataParameter name: {0}", name);

            // create parameter's value
            if (parameterIsDataTable)
            {
                throw new ModelPropertyInvalidException("Table value parameters only supported by SQL.");
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

            this.WriteTrace(TraceLevel.DEBUG, "Parameter's type: {0}", type);

            var retval = new List<TParameter>
            {
                new TParameter
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
        #endregion

        #region Query helper functions
        /// <summary>
        /// Wrapper for all query functions.  Handles raising of query exceptions and query timing info.
        /// </summary>
        /// <typeparam name="T">The return type of the sqlAction function.</typeparam>
        /// <param name="sqlAction">The function to perform on the command object.</param>
        /// <returns>The type defined by the sqlAction function.</returns>
        protected virtual T ExecuteSql<T>(Func<TCommand, T> sqlAction)
        {
            this.LastInputType = (this.Input ?? DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.EmptyModel).GetType().Name;
            if (this.LastInputType == DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.EmptyModel.GetType().Name)
                this.LastInputType = null;

            this.WriteTrace(TraceLevel.INFORMATION, "Performing {0}.  Query: {1}", this.GetOriginalCallingFunctionName(), this.QueryString);
            Boolean useDefault = this.Connection == null;
            using (var cmd = this.PrepareCommand())
            {
                this.Connection = (TConnection)cmd.Connection;

                if (useDefault && this.Transaction == null)
                    this.WriteTrace(TraceLevel.DEBUG, "Closing connection after query.");
                else
                    this.WriteTrace(TraceLevel.DEBUG, "Transaction or user managed connection.  Leaving connection open after query.");

                try
                {
                    var s = new Stopwatch();
                    s.Start();
                    var retValue = sqlAction(cmd);
                    s.Stop();
                    this.RaiseOnQueryComplete(new DALQueryCompleteEventArgs
                    {
                        Connection = cmd.Connection,
                        InputModelType = this.LastInputType,
                        InputParameters = cmd.Parameters.OfType<TParameter>().ToList(),
                        QueryMethod = this.GetOriginalCallingFunctionName(),
                        QueryString = this.QueryString,
                        TimeElapsed = s.Elapsed,
                        Timeout = cmd.CommandTimeout,
                        Transaction = cmd.Transaction
                    });
                    this.OutputValues = cmd.Parameters.ToNameValueDictionary(ParameterDirection.Output);
                    return retValue;

                }
                catch (TException oex)
                {
                    this.HandleOnQueryException(oex, this.Connection, cmd.Parameters.OfType<TParameter>().ToList(), this.CommandTimeout, this.QueryString);
                    throw;
                }
                catch (Exception ex)
                {
                    this.HandleOnException(ex, this.Connection, cmd.Parameters.OfType<TParameter>().ToList(), this.CommandTimeout, this.QueryString);
                    throw;
                }
                finally
                {
                    if (useDefault && this.Transaction == null)
                    {
                        this.Connection.Close();
                        this.Connection = null;
                    }   
                }
            }
        }

        /// <summary>
        /// Raises the OnQueryException event with the provided information.
        /// </summary>
        /// <param name="ex">The TException object.</param>
        /// <param name="conn">The TConnection object on which the exception happened.</param>
        /// <param name="parameters">The parameters used in the query.</param>
        /// <param name="timeout">The command timeout value used.</param>
        /// <param name="queryString">The query string which caused the exception.</param>
        protected virtual void HandleOnQueryException(TException ex, TConnection conn, IEnumerable<TParameter> parameters, int? timeout, String queryString)
        {
            this.RaiseOnQueryException(new DALQueryExceptionEventArgs
            {
                Connection = conn,
                Transaction = null,
                QueryMethod = this.GetOriginalCallingFunctionName(),
                Exception = ex,
                InputParameters = parameters,
                Timeout = timeout ?? this.CommandTimeout,
                QueryString = queryString,
                InputModelType = this.GetModelBaseTypeFromOriginalCallingFunction(),
                Input = this.Input
            });
        }

        /// <summary>
        /// Raises the OnException event with the provided information.
        /// </summary>
        /// <param name="ex">The TException object.</param>
        /// <param name="conn">The TConnection object on which the exception happened.</param>
        /// <param name="parameters">The parameters used in the query.</param>
        /// <param name="timeout">The command timeout value used.</param>
        /// <param name="queryString">The query string which caused the exception.</param>
        protected virtual void HandleOnException(Exception ex, TConnection conn, IEnumerable<TParameter> parameters, int? timeout, String queryString)
        {
            this.RaiseOnException(new DALExceptionEventArgs
            {
                Connection = conn,
                Transaction = null,
                QueryMethod = this.GetOriginalCallingFunctionName(),
                Exception = ex,
                InputParameters = parameters,
                Timeout = timeout ?? this.CommandTimeout,
                QueryString = queryString,
                InputModelType = this.GetModelBaseTypeFromOriginalCallingFunction(),
                Input = this.Input
            });
        }

        /// <summary>
        /// Raises the OnAffectedRowsMismatch event with the provided information.
        /// </summary>
        /// <param name="affectedRows">The actual number of affected rows.</param>
        /// <param name="conn">The connection on which the mismatch occured.</param>
        /// <param name="parameters">The parameters which caused the mismatch.</param>
        /// <param name="timeout">The command timeout.</param>
        /// <param name="queryString">The query for which the mismatch occured.</param>
        protected virtual void HandleOnAffectedRowsMismatch(int affectedRows, TConnection conn, IEnumerable<TParameter> parameters, int? timeout, String queryString)
        {
            this.RaiseOnAffectedRowsMismatch(new DALAffectedRowsMismatchEventArgs
            {
                Connection = conn,
                Transaction = null,
                QueryMethod = this.GetOriginalCallingFunctionName(),
                AffectedRows = affectedRows,
                ExpectedAffectedRows = this.ExpectedAffectedRows.Value,
                InputParameters = parameters,
                Timeout = timeout ?? this.CommandTimeout,
                QueryString = queryString,
                InputModelType = this.GetModelBaseTypeFromOriginalCallingFunction(),
                Input = this.Input
            });
        }

        /// <summary>
        /// Creates a TCommand object based off the query options.
        /// </summary>
        /// <returns>The prepared TCommand object.</returns>
        /// <exception cref="ArgumentException">Thrown if the connection object passed is null and the ConnectionString property is not set on the class.</exception>
        protected virtual TCommand PrepareCommand()
        {
            this.WriteTrace(TraceLevel.INFORMATION, "Preparing command for query: {0}", this.QueryString);

            Boolean useDefault = this.Connection == null;
            if (useDefault && String.IsNullOrWhiteSpace(this.ConnectionString))
                throw new ArgumentException("The 'Connection' property can not be null or empty if the IDBAccess.ConnectionString property has not been set.");
            else if (useDefault && this.Transaction == null) // use default new connection if we're not in a transaction
            {
                this.WriteTrace(TraceLevel.DEBUG, "Using new connection for the query.");
                this.Connection = new TConnection();
                this.Connection.ConnectionString = this.ConnectionString;
            }
            else if (this.Transaction != null)
            {
                this.WriteTrace(TraceLevel.DEBUG, "In a transaction, using the transaction's connection for the query.");
                this.Connection = (TConnection)this.Transaction.Connection; // use the transaction's connection
            }

            TCommand cmd = new TCommand();
            cmd.CommandText = this.QueryString;
            cmd.Connection = this.Connection;
            cmd.CommandTimeout = this.CommandTimeout;

            this.WriteTrace(TraceLevel.DEBUG, "Command timeout: {0}", this.CommandTimeout);

            if (this.Connection.State == ConnectionState.Closed)
            {
                this.WriteTrace(TraceLevel.DEBUG, "Opening connection to: {0}", this.Connection.ConnectionString);
                this.Connection.Open();
            }

            if (this.NextIsTransaction && this.Transaction != null)
                this.NextIsTransaction = false;

            if (this.NextIsTransaction)
            {
                this.WriteTrace(TraceLevel.DEBUG, "Beginning transaction on connection.");
                this.Transaction = (TTransaction)this.Connection.BeginTransaction(this.Isolation);
                this.NextIsTransaction = false;
            }

            cmd.CommandType = (this.IsStoredProcedure) ? CommandType.StoredProcedure : CommandType.Text;
            this.WriteTrace(TraceLevel.DEBUG, "Command type: {0}", cmd.CommandType);
            cmd.Transaction = this.Transaction;

            this.WriteTrace(TraceLevel.DEBUG, "Adding parameters to command.");
            foreach (var p in this.PrepareParameters(this.Input, this.PrefixDirection))
                cmd.Parameters.Add(p);

            return cmd;
        }
        #endregion

        /// <summary>
        /// Returns the last input type that is set by the prepare parameters function.  This lets us give the last model type back in an exception event.
        /// </summary>
        /// <returns>The name of the last model type used as input.</returns>
        internal String GetModelBaseTypeFromOriginalCallingFunction()
        {
            return this.LastInputType;
        }
    }
}