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

using System.Collections;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.DBAccess.Generic
{
    /// <summary>
    /// Provides custom XML serialization for runtime types.
    /// </summary>
    public abstract class DALRuntimeTypeBase : IXmlSerializable
    {
        /// <summary>
        /// The element name for this type to be used for XML Serialization.
        /// </summary>

        //Ignore so it's not made into a parameter and written into
        [XmlIgnore]
        [DALIgnore]
        public abstract String TableName { get; }

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
            this.WriteXml(writer, this.TableName);
        }

        internal void WriteXml(XmlWriter writer, String tableName = null)
        {
            var fda = FastDynamicAccess.Get(this);
            var properties = fda.PropertyToArrayIndex;

            //if empty or null string write class type
            if (String.IsNullOrWhiteSpace(tableName))
                tableName = this.GetType().Name;

            writer.WriteStartElement(tableName);
            foreach (var p in properties.Where(p => !IsXMLIgnore(p.Key, this.GetType())))
            {
                //empty string makes null values come out as tag
                Object obj = fda.Get(this, p.Value) ?? "";

                if (obj.IsIEnumerable() && (obj as IEnumerable).GetIEnumerableGenericType().DerivesFromType(typeof(DALRuntimeTypeBase)))
                    obj = new DALRuntimeTypeList((obj as IEnumerable).OfType<DALRuntimeTypeBase>().ToList());

                if (obj as DALRuntimeTypeBase != null)
                {
                    //write property name as "table name"
                    if (String.IsNullOrWhiteSpace(tableName) && String.IsNullOrWhiteSpace((obj as DALRuntimeTypeBase).TableName))
                        (obj as DALRuntimeTypeBase).WriteXml(writer, p.Key);
                    else
                        //otherwise use whatever tablename this runtime type has set
                        (obj as DALRuntimeTypeBase).WriteXml(writer);
                }
                else if (obj as DALRuntimeTypeList != null)
                {
                    writer.WriteStartElement(GetXmlAttributeName(p.Key, this.GetType()));
                    (obj as DALRuntimeTypeList).WriteXml(writer);
                    writer.WriteEndElement();
                }
                else
                    writer.WriteElementString(GetXmlAttributeName(p.Key, this.GetType()), obj.ToString());
            }
            writer.WriteEndElement();
        }

        private Boolean IsXMLIgnore(String propertyName, Type type)
        {
            return Attribute.GetCustomAttribute(type.GetProperty(propertyName), typeof(XmlIgnoreAttribute)) != null;
        }

        private String GetXmlAttributeName(String propertyName, Type type)
        {
            var attr = Attribute.GetCustomAttribute(type.GetProperty(propertyName), typeof(XmlAttributeAttribute)) as XmlAttributeAttribute;

            if (attr != null)
                return attr.AttributeName;
            else
                return propertyName;
        }
        #endregion
    }
}