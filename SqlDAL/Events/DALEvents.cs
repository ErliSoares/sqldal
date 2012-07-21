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

namespace System.Data.DBAccess.Generic.Events
{
    /// <summary>
    /// Static event class used to catch all events across all instances of IDBAccess classes.
    /// </summary>
    public static class DALEvents
    {
        /// <summary>
        /// DALTrace event handler delegate.
        /// </summary>
        /// <param name="sender">The IDBAccess object which raised the event.</param>
        /// <param name="message">The trace message.</param>
        public delegate void DALTraceHandler(IDBAccess sender, String message);

        public static event DALTraceHandler OnDALTrace;

        /// <summary>
        /// Raises a trace event if it's being listened to.
        /// </summary>
        /// <param name="db">The IDBAccess object which raised the event.</param>
        /// <param name="message">The trace message.</param>
        internal static void RaiseOnDALTrace(this IDBAccess db, String message)
        {
            if (DALEvents.OnDALTrace != null)
                DALEvents.OnDALTrace(db, String.Format("{0}: {1}", db.GetHashCode(), message));
        }
    }
}