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

namespace System.Data.DBAccess.Generic
{
    /// <summary>
    /// Trace level.
    /// </summary>
    public enum TraceLevel
    {
        /// <summary>
        /// Outputs the most trace information.  Extremely detailed.
        /// </summary>
        DEBUG = 1,

        /// <summary>
        /// Outputs information related to the logic flow.
        /// </summary>
        INFORMATION = 2,

        /// <summary>
        /// Outputs no trace information.  This is the default.
        /// </summary>
        NONE = 3
    }
}