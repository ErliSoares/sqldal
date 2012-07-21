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

namespace System.Data.DBAccess.Generic
{
    /// <summary>
    /// Class used by the DAL to store information related to how a data set will be read into a model type.  This is used to cache the relationship between data set and model for quicker population.
    /// </summary>
    internal class PopulateData
    {
        /// <summary>
        /// The model property names in repsective order to the column names.
        /// </summary>
        internal List<String> MappedCols { get; set; }

        /// <summary>
        /// The names of the columns capitalized.
        /// </summary>
        internal List<String> ColUpperNames { get; set; }

        /// <summary>
        /// The number of columns in the data set.
        /// </summary>
        internal int ColCount { get; set; }

        /// <summary>
        /// List of booleans representing whether or not the model property has an accessible setter.
        /// </summary>
        internal List<Boolean> HasSetters { get; set; }

        /// <summary>
        /// List of property types.
        /// </summary>
        internal List<Type> PropertyTypes { get; set; }

        /// <summary>
        /// List of property write String.Format format strings.
        /// </summary>
        internal List<String> PropertyFormats { get; set; }

        internal List<int> FDAIndexes { get; set; }
    }
}