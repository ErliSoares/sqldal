/*Copyright 2012 Brian Adams

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.*/

namespace System.Data.DBAccess.Generic.Exceptions
{
    /// <summary>
    /// Base DALException
    /// </summary>
    public class DALException : Exception
    {
        /// <summary>
        /// Base DALException
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public DALException(String message) : base(message) { }
    }

    #region ModelBase exceptions
    /// <summary>
    /// Thrown when:
    /// A property type is an enumeration of anything but a class or Byte array.
    /// A property does not have a public or internal set/get accessor during a read/write operation.
    /// There are two or more properties with the same name (case insensitive).
    /// </summary>
    public sealed class ModelPropertyInvalidException : DALException
    {
        /// <summary>
        /// Thrown when:
        /// A property type is an enumeration of anything but a class or Byte array.
        /// A property does not have a public or internal set/get accessor during a read/write operation.
        /// There are two or more properties with the same name (case insensitive).
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public ModelPropertyInvalidException(String message) : base(message) { }
    }

    /// <summary>
    /// Thrown when a model contains properties whose attributes are not configured properly.
    /// </summary>
    public sealed class ModelPropertyMisconfiguredException : DALException
    {
        /// <summary>
        /// Thrown when a model contains properties whose attributes are not configured properly.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public ModelPropertyMisconfiguredException(String message) : base(message) { }
    }

    /// <summary>
    /// Thrown when a value from a SQL query cannot be assigned to a model property.
    /// </summary>
    public sealed class ModelPropertyColumnMismatchException : DALException
    {
        /// <summary>
        /// Thrown when a value from a SQL query cannot be assigned to a model property.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public ModelPropertyColumnMismatchException(String message) : base(message) { }
    }

    /// <summary>
    /// Thrown when an attempt to assign null to a non-nullable property happens.
    /// </summary>
    public sealed class ModelPropertyNotNullableException : DALException
    {
        /// <summary>
        /// Thrown when an attempt to assign null to a non-nullable property happens.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public ModelPropertyNotNullableException(String message) : base(message) { }
    }
    #endregion

    #region UDTable exceptions
    /// <summary>
    /// Thrown when the contract functions of a QuickRead table class are not returning as expected.
    /// </summary>
    public sealed class TableQuickReadMisconfiguredException : DALException
    {
        /// <summary>
        /// Thrown when the contract functions of a QuickRead table class are not returning as expected.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public TableQuickReadMisconfiguredException(String message) : base(message) { }
    }
    #endregion

    #region Transaction exceptions
    /// <summary>
    /// Thrown when a transaction is started with one already in progress.
    /// </summary>
    public sealed class TransactionInProgressException : DALException
    {
        /// <summary>
        /// Thrown when a transaction is started with one already in progress.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public TransactionInProgressException(String message) : base(message) { }
    }

    /// <summary>
    /// Thrown when an action is attempted against a transaction when none is in progress.
    /// </summary>
    public sealed class TransactionNotInProgressException : DALException
    {
        /// <summary>
        /// Thrown when an action is attempted against a transaction when none is in progress.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public TransactionNotInProgressException(String message) : base(message) { }
    }

    /// <summary>
    /// Thrown when an action is attempted on a save point which does not exist.
    /// </summary>
    public sealed class TransactionSavePointNotFoundException : DALException
    {
        /// <summary>
        /// Thrown when an action is attempted on a save point which does not exist.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public TransactionSavePointNotFoundException(String message) : base(message) { }
    }

    /// <summary>
    /// Thrown when a save is attempted with a save point name that already exists.
    /// </summary>
    public sealed class TransactionSavePointAlreadyExistsException : DALException
    {
        /// <summary>
        /// Thrown when a save is attempted with a save point name that already exists.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public TransactionSavePointAlreadyExistsException(String message) : base(message) { }
    }

    /// <summary>
    /// Thrown when RollbackToLastSavePoint is called and there are no save points.
    /// </summary>
    public sealed class TransactionNoSavePointsFoundException : DALException
    {
        /// <summary>
        /// Thrown when RollbackToLastSavePoint is called and there are no save points.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public TransactionNoSavePointsFoundException(String message) : base(message) { }
    }
    #endregion

    #region DALRelationship
    /// <summary>
    /// Thrown when a custom relationship is passed with the same parent and child table index.
    /// </summary>
    public sealed class DALRelationshipMisconfiguredException : DALException
    {
        /// <summary>
        /// Thrown when a custom relationship is passed with the same parent and child table index.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public DALRelationshipMisconfiguredException(String message) : base(message) { }
    }

    /// <summary>
    /// Thrown when the parent table does not have the specified property for child objects.
    /// </summary>
    public sealed class DALRelationshipParentPropertyMissingException : DALException
    {
        /// <summary>
        /// Thrown when the parent table does not have the specified property for child objects.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public DALRelationshipParentPropertyMissingException(String message) : base(message) { }
    }

    /// <summary>
    /// Thrown when the parent table's children property is not a list.
    /// </summary>
    public sealed class DALRelationshipParentPropertyNotAListException : DALException
    {
        /// <summary>
        /// Thrown when the parent table's children property is not a list.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public DALRelationshipParentPropertyNotAListException(String message) : base(message) { }
    }

    /// <summary>
    /// Thrown when the parent table's children property list is of the incorrect type.
    /// </summary>
    public sealed class DALRelationshipParentPropertyListIncorrectTypeException : DALException
    {
        /// <summary>
        /// Thrown when the parent table's children property list is of the incorrect type.
        /// </summary>
        /// <param name="message">The message associated with the exception.</param>
        public DALRelationshipParentPropertyListIncorrectTypeException(String message) : base(message) { }
    }
    #endregion
}