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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.DBAccess.Generic
{
    /// <summary>
    /// Class which handles the special case of serializing a list of runtime types.
    /// </summary>
    public sealed class DALRuntimeTypeList : IXmlSerializable
    {
        /// <summary>
        /// DO NOT USE.  PARAMETERLESS CONSTRUCTOR FOR THE XML SERIALIZER.
        /// </summary>
        public DALRuntimeTypeList()
        {

        }

        internal DALRuntimeTypeList(List<DALRuntimeTypeBase> objects)
        {
            this.RuntimeTypeObjects = objects;
        }

        /// <summary>
        /// The list of runtime type objects to be serialized
        /// </summary>
        internal List<DALRuntimeTypeBase> RuntimeTypeObjects { get; set; }

        #region IXmlSerializable Members
        /// <summary>
        /// Returns null for this implementation.
        /// </summary>
        /// <returns>Null.</returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="reader">Throws NotImplementedException.</param>
        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes xml to the provide XMLWriter object.
        /// </summary>
        /// <param name="writer">The XMLWriter object.</param>
        public void WriteXml(XmlWriter writer)
        {
            foreach (var o in this.RuntimeTypeObjects)
            {
                o.WriteXml(writer);
            }
        }
        #endregion
    }
}