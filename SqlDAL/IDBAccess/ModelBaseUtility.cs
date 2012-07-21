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

namespace System.Data.DBAccess.Generic
{
    public static partial class IDBAccessExtensions
    {
        /// <summary>
        /// Writes a trace message to the OnDALTrace event if the consumer is listening at the level passed.
        /// </summary>
        /// <param name="level">The level of the trace (information, debug)</param>
        /// <param name="message">The trace message.</param>
        internal static void WriteTrace(this IDBAccess db, TraceLevel level, String message)
        {
            if ((int)db.TraceOutputLevel <= (int)level)
            {
                db.RaiseOnDALTrace(message);
            }
        }

        /// <summary>
        /// Writes a trace message using a format string to the OnDALTrace event if the consumer is listening at the level passed.
        /// </summary>
        /// <param name="level">The level of the trace (information, debug)</param>
        /// <param name="messageFormat">The String.Format format string.</param>
        /// <param name="args">The String.Format object arguments.</param>
        internal static void WriteTrace(this IDBAccess db, TraceLevel level, String messageFormat, params Object[] args)
        {
            if ((int)db.TraceOutputLevel <= (int)level)
            {
                db.RaiseOnDALTrace(String.Format(messageFormat, args));
            }
        }
    }
}