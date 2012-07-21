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

using System.Data.DBAccess.Generic.Events;

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
        #region Event declarations
        #region Basic events
        /// <summary>
        /// Class for DALException event info.
        /// </summary>
        public sealed class DALExceptionEventArgs : DALEventArgs
        {
            /// <summary>
            /// The exception that was thrown.
            /// </summary>
            public Exception Exception { get; internal set; }
        }

        /// <summary>
        /// Class for DALQueryException event info.
        /// </summary>
        public sealed class DALQueryExceptionEventArgs : DALEventArgs
        {
            /// <summary>
            /// The TException that was thrown.
            /// </summary>
            public TException Exception { get; internal set; }
        }

        /// <summary>
        /// Class for DALAffectedRowsMismatch event info.
        /// </summary>
        public sealed class DALAffectedRowsMismatchEventArgs : DALEventArgs
        {
            /// <summary>
            /// The actual number of affected rows.
            /// </summary>
            public int AffectedRows { get; internal set; }

            /// <summary>
            /// The expected number of affected rows.
            /// </summary>
            public int ExpectedAffectedRows { get; internal set; }
        }

        /// <summary>
        /// Class for DALQueryComplete event info
        /// </summary>
        public sealed class DALQueryCompleteEventArgs : DALEventArgs
        {
            /// <summary>
            /// The amount of time the query took to complete.
            /// </summary>
            public TimeSpan TimeElapsed { get; set; }
        }
        #endregion
        #region Delegates
        /// <summary>
        /// Base DAL event handler delegate.
        /// </summary>
        /// <param name="sender">The IDBAccess object which raised the event.</param>
        /// <param name="e">The DALEventArgs object.</param>
        public delegate void DALEventHandler(DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException> sender, DALEventArgs e);

        /// <summary>
        /// OnException event handler delegate.
        /// </summary>
        /// <param name="sender">The IDBAccess object which raised the event.</param>
        /// <param name="e">The Exception event arguments.</param>
        public delegate void DALExceptionHandler(DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException> sender, DALExceptionEventArgs e);

        /// <summary>
        /// OnQueryException event handler delegate.
        /// </summary>
        /// <param name="sender">The IDBAccess object which raised the event.</param>
        /// <param name="e">The QueryException event arguments.</param>
        public delegate void DALQueryExceptionHandler(DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException> sender, DALQueryExceptionEventArgs e);

        /// <summary>
        /// OnAffectedRowsMismatch event handler delegate.
        /// </summary>
        /// <param name="sender">The DotNETCompatibleProvider object which raised the event.</param>
        /// <param name="e">The affected rows mismatch event arguments.</param>
        public delegate void DALAffectedRowsMismatchHandler(DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException> sender, DALAffectedRowsMismatchEventArgs e);

        /// <summary>
        /// QueryComplete event handler delegate
        /// </summary>
        /// <param name="sender">The DotNETCompatibleProvider object which raised the event.</param>
        /// <param name="e">The QueryComplete event args.</param>
        public delegate void DALQueryCompleteHandler(DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException> sender, DALQueryCompleteEventArgs e);
        #endregion
        #endregion
        #region Instance events
        /// <summary>
        /// Fires when an Exception is raised by a query.
        /// </summary>
        public event DALExceptionHandler OnException;
        /// <summary>
        /// Fires when a TException is raised by a query.
        /// </summary>
        public event DALQueryExceptionHandler OnQueryException;
        /// <summary>
        /// Fires when a query operation completes.
        /// </summary>
        public event DALQueryCompleteHandler OnQueryComplete;
        /// <summary>
        /// Fires when an update statement's affected rows count does not match the value set in ExpectedAffectedRows.
        /// </summary>
        public event DALAffectedRowsMismatchHandler OnAffectedRowsMismatch;

        /// <summary>
        /// Raises an exception event if it's being listened to.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected virtual void RaiseOnException(DALExceptionEventArgs e)
        {
            DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.RaiseOnException(this, e);

            if (OnException != null)
                OnException(this, e);
        }

        /// <summary>
        /// Raises an OnQueryException event if it's being listened to.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected virtual void RaiseOnQueryException(DALQueryExceptionEventArgs e)
        {
            DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.RaiseOnQueryException(this, e);

            if (OnQueryException != null)
                OnQueryException(this, e);

            this.RaiseOnException(new DALExceptionEventArgs
            {
                Connection = e.Connection,
                Exception = e.Exception,
                InputModelType = e.InputModelType,
                InputParameters = e.InputParameters,
                QueryMethod = e.QueryMethod,
                QueryString = e.QueryString,
                Timeout = e.Timeout,
                Transaction = e.Transaction,
                Input = e.Input
            });

            return;
        }

        /// <summary>
        /// Raises an affected rows mismatch event if it's being listened to.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected virtual void RaiseOnAffectedRowsMismatch(DALAffectedRowsMismatchEventArgs e)
        {
            if (this.ExpectedAffectedRows != null)
            {
                DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.RaiseOnAffectedRowsMismatch(this, e);

                if (OnAffectedRowsMismatch != null)
                    OnAffectedRowsMismatch(this, e);
            }
        }

        /// <summary>
        /// Raises a QueryComplete event if it's being listened to.
        /// </summary>
        /// <param name="e">The event args.</param>
        protected virtual void RaiseOnQueryComplete(DALQueryCompleteEventArgs e)
        {
            DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.RaiseOnQueryComplete(this, e);

            if (OnQueryComplete != null)
                OnQueryComplete(this, e);
        }
        #endregion
        #region Static events
        /// <summary>
        /// Fires when an Exception is raised by a query.
        /// </summary>
        public static event DALExceptionHandler OnExceptionStatic;
        /// <summary>
        /// Fires when a TException is raised by a query.
        /// </summary>
        public static event DALQueryExceptionHandler OnQueryExceptionStatic;
        /// <summary>
        /// Fires when a query operation completes.
        /// </summary>
        public static event DALQueryCompleteHandler OnQueryCompleteStatic;
        /// <summary>
        /// Fires when an update statement's affected rows count does not match the value set in ExpectedAffectedRows.
        /// </summary>
        public static event DALAffectedRowsMismatchHandler OnAffectedRowsMismatchStatic;

        /// <summary>
        /// Raises an exception event if it's being listened to.
        /// </summary>
        /// <param name="db">The IDBAccess object which raised the event.</param>
        /// <param name="e">The event args.</param>
        internal static void RaiseOnException(DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException> db, DALExceptionEventArgs e)
        {
            db.PerformAutoRollback();

            if (DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.OnExceptionStatic != null)
                DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.OnExceptionStatic(db, e);
        }
        /// <summary>
        /// Raises an OnQueryException event if it's being listened to.
        /// </summary>
        /// <param name="db">The IDBAccess object which raised the event.</param>
        /// <param name="e">The event args.</param>
        internal static void RaiseOnQueryException(DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException> db, DALQueryExceptionEventArgs e)
        {
            if (DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.OnQueryExceptionStatic != null)
                DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.OnQueryExceptionStatic(db, e);
        }

        /// <summary>
        /// Raises an affected rows mismatch event if it's being listened to.
        /// </summary>
        /// <param name="db">The IDBAccess object which raised the event.</param>
        /// <param name="e">The event args.</param>
        internal static void RaiseOnAffectedRowsMismatch(DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException> db, DALAffectedRowsMismatchEventArgs e)
        {
            if (DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.OnAffectedRowsMismatchStatic != null)
                DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.OnAffectedRowsMismatchStatic(db, e);
        }

        /// <summary>
        /// Raises a QueryComplete event if it's being listened to.
        /// </summary>
        /// <param name="db">The IDBAccess object which raised the event.</param>
        /// <param name="e">The event args.</param>
        internal static void RaiseOnQueryComplete(DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException> db, DALQueryCompleteEventArgs e)
        {
            if (DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.OnQueryCompleteStatic != null)
                DotNETCompatibleProvider<TConnection, TCommand, TParameter, TTransaction, TDataAdapter, TException>.OnQueryCompleteStatic(db, e);
        }
        #endregion
    }
}