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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace System.Data.DBAccess.Generic.RuntimeClass
{
    public sealed class SerializableLINQGrouping<TKey, TElement> : SerializableLINQGrouping, IEnumerable<IGrouping<TKey, TElement>>
    {
        /// <summary>
        /// DO NOT USE.  FRAMEWORK REQUIRES A PUBLIC PARAMETERLESS CONSTRUCTOR.
        /// </summary>
        public SerializableLINQGrouping()
        { }

        internal SerializableLINQGrouping(IEnumerable<IGrouping<TKey, TElement>> groups)
        {
            this.m_Groups = groups.ToList();
        }

        /// <summary>
        /// DO NOT USE.  ONLY FOR INTERNAL USE BUT THE FRAMEWORK REQUIRES THIS TO BE PUBLIC.
        /// 
        /// Used as a version of SerializableLINQGrouping(IEnumerable IGrouping TKey, TElement groups) that can be called from a weakly typed source.
        /// </summary>
        public SerializableLINQGrouping(List<Object> groups)
        {
            this.m_Groups = new List<IGrouping<TKey, TElement>>();

            //trick to put weakly typed objects of a known strong type into a strongly typed collection
            var weaklyTyped = this.m_Groups as System.Collections.IList;
            foreach (var o in groups)
                weaklyTyped.Add(o);
        }

        private List<IGrouping<TKey, TElement>> m_Groups;

        internal List<Object> Groups { get { return this.m_Groups.OfType<Object>().ToList(); } }

        #region IXmlSerializable
        public override Xml.Schema.XmlSchema GetSchema()
        {
            return new Xml.Schema.XmlSchema();
        }

        public override void ReadXml(Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public override void WriteXml(Xml.XmlWriter writer)
        {
            var keyFDA = FastDynamicAccess.Get(typeof(TKey));
            var listElemName = typeof(TElement).Name;
            //foreach group
            foreach (var g in this.m_Groups)
            {
                writer.WriteStartElement(String.Format("{0}Group", typeof(TElement).Name));
                //write the key as separate properties
                writer.WriteStartElement("Key");
                foreach (var p in keyFDA.PropertyToArrayIndex)
                {
                    writer.WriteElementString(p.Key, keyFDA.Get(g.Key, p.Value).ToString());
                }
                writer.WriteEndElement();

                //write the list
                writer.WriteStartElement(String.Format("ArrayOf{0}", typeof(TElement).Name));
                foreach (var e in g.ToList())
                {
                    writer.WriteRaw(e.SerializeToXML(null, true));
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
        #endregion

        #region IEnumerable
        public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        {
            return this.m_Groups.GetEnumerator();
        }

        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return (this.m_Groups as Collections.IEnumerable).GetEnumerator();
        }
        #endregion

        public override SerializableLINQGrouping ToNonAnonymousLINQGrouping()
        {
            Type keyType = typeof(TKey);
            Type valueType = typeof(TElement);

            // no work to be done
            if (!keyType.IsAnonymousType() && !valueType.IsAnonymousType())
                return this;

            List<Object> newKeyObjs = new List<Object>();
            List<List<Object>> newValueObjs = new List<List<Object>>();

            //create non anonymous types where needed
            if (keyType.IsAnonymousType())
            {
                foreach (var g in this.m_Groups)
                {
                    newKeyObjs.Add(g.Key.ToNonAnonymousType());
                }
            }
            else
            {
                newKeyObjs = this.m_Groups.Select(g => g.Key).OfType<Object>().ToList();
            }

            if (valueType.IsAnonymousType())
            {
                foreach (var g in this.m_Groups)
                {
                    newValueObjs.Add(g.ToList().Select(o => o.ToNonAnonymousType()).ToList());
                }
            }
            else
            {
                newValueObjs = this.m_Groups.Select(g => g.ToList().OfType<Object>().ToList()).ToList();
            }

            //types for new SerializableLINQGrouping<TKey, TElement> that we will create
            Type newKeyType = keyType.IsAnonymousType() ? newKeyObjs.First().GetType() : keyType;
            Type newValueType = valueType.IsAnonymousType() ? newValueObjs[0][0].GetType() : valueType;

            //make a new generic type with the types we need
            Type retObjectType = typeof(SerializableLINQGrouping<,>).MakeGenericType(new Type[] { newKeyType, newValueType });

            //get a DALIGrouping generic type.  This is needed so that we can populate grouping objects,
            //functionality that is normally hidden because any MS classes implementing IGrouping<TKey, TElement> are internal to .NET.
            var groupingType = typeof(DALIGrouping<,>).MakeGenericType(new Type[] { newKeyType, newValueType });

            //get the actual group objects.  instantiate new DALIGrouping objects passing in the key and that key's values
            var groupingObjs = newKeyObjs.Select((k, i) => Activator.CreateInstance(groupingType, k, newValueObjs[i])).ToList();

            //create a new instance of this class calling the List Object constructor
            Object retObject = Activator.CreateInstance(retObjectType, groupingObjs);

            return (SerializableLINQGrouping)retObject;
        }
    }
}