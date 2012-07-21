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
    public abstract class DALTupleBase<T1>
        where T1 : class, new()
    {
        protected List<FastDynamicAccess> m_fdas;

        /// <summary>
        /// List representing the first return data set.
        /// </summary>
        public List<T1> Table1 { get; internal set; }

        #region JSON
        private Dictionary<Boolean, String> m_JSON = new Dictionary<Boolean, String>();
        /// <summary>
        /// Serializes an object to a JSON string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The JSON string.</returns>
        public String SerializeToJSON(Boolean includeTableName = false)
        {
            String json;
            if (!this.m_JSON.TryGetValue(includeTableName, out json))
            {
                json = this.Table1.SerializeToJSON(includeTableName);
                this.m_JSON.Add(includeTableName, json);
            }

            return json;
        }
        #endregion

        #region XML
        private Dictionary<String, String> m_XML = new Dictionary<String, String>();

        /// <summary>
        /// Serializes and object to an XML string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="rootElementName">The optional root element name.  If this is a runtime type it will default to the TableName.  If it is a list of runtime types it will default to DALRuntimeTypeBase.</param>
        /// <returns>The XML string.</returns>
        public String SerializeToXML(String rootElementName = null)
        {
            String xml;
            String key = rootElementName ?? "";
            if (!this.m_XML.TryGetValue(key, out xml))
            {
                xml = this.Table1.SerializeToXML(rootElementName);
                this.m_XML.Add(key, xml);
            }

            return xml;
        }
        #endregion

        internal String FirstTableName { get; set; }

        internal List<IDBAccess> DBAccess { get; set; }
    }

    /// <summary>
    /// DALTuple representing 2 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    public sealed class DALTuple<T1, T2> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
    {
        internal int Count { get { return 2; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 3 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
    {
        internal int Count { get { return 3; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 4 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
    {
        internal int Count { get { return 4; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 5 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
    {
        internal int Count { get { return 5; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 6 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
    {
        internal int Count { get { return 6; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 7 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6, T7> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
    {
        internal int Count { get { return 7; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// List representing the seventh return data set.
        /// </summary>
        public List<T7> Table7 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the seventh return data set.
        /// </summary>
        public FastDynamicAccess FDATable7 { get { return this.FDAs[6]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6)),
                        FastDynamicAccess.Get(typeof(T7))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 8 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
    /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6, T7, T8> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
    {
        internal int Count { get { return 8; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// List representing the seventh return data set.
        /// </summary>
        public List<T7> Table7 { get; internal set; }

        /// <summary>
        /// List representing the eigth return data set.
        /// </summary>
        public List<T8> Table8 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the seventh return data set.
        /// </summary>
        public FastDynamicAccess FDATable7 { get { return this.FDAs[6]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the eigth return data set.
        /// </summary>
        public FastDynamicAccess FDATable8 { get { return this.FDAs[7]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6)),
                        FastDynamicAccess.Get(typeof(T7)),
                        FastDynamicAccess.Get(typeof(T8))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 9 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
    /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
    /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
        where T9 : class, new()
    {
        internal int Count { get { return 9; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// List representing the seventh return data set.
        /// </summary>
        public List<T7> Table7 { get; internal set; }

        /// <summary>
        /// List representing the eigth return data set.
        /// </summary>
        public List<T8> Table8 { get; internal set; }

        /// <summary>
        /// List representing the ninth return data set.
        /// </summary>
        public List<T9> Table9 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the seventh return data set.
        /// </summary>
        public FastDynamicAccess FDATable7 { get { return this.FDAs[6]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the eigth return data set.
        /// </summary>
        public FastDynamicAccess FDATable8 { get { return this.FDAs[7]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the ninth return data set.
        /// </summary>
        public FastDynamicAccess FDATable9 { get { return this.FDAs[8]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6)),
                        FastDynamicAccess.Get(typeof(T7)),
                        FastDynamicAccess.Get(typeof(T8)),
                        FastDynamicAccess.Get(typeof(T9))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 10 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
    /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
    /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
    /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
        where T9 : class, new()
        where T10 : class, new()
    {
        internal int Count { get { return 10; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// List representing the seventh return data set.
        /// </summary>
        public List<T7> Table7 { get; internal set; }

        /// <summary>
        /// List representing the eigth return data set.
        /// </summary>
        public List<T8> Table8 { get; internal set; }

        /// <summary>
        /// List representing the ninth return data set.
        /// </summary>
        public List<T9> Table9 { get; internal set; }

        /// <summary>
        /// List representing the 10th return data set.
        /// </summary>
        public List<T10> Table10 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the seventh return data set.
        /// </summary>
        public FastDynamicAccess FDATable7 { get { return this.FDAs[6]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the eigth return data set.
        /// </summary>
        public FastDynamicAccess FDATable8 { get { return this.FDAs[7]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the ninth return data set.
        /// </summary>
        public FastDynamicAccess FDATable9 { get { return this.FDAs[8]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 10th return data set.
        /// </summary>
        public FastDynamicAccess FDATable10 { get { return this.FDAs[9]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6)),
                        FastDynamicAccess.Get(typeof(T7)),
                        FastDynamicAccess.Get(typeof(T8)),
                        FastDynamicAccess.Get(typeof(T9)),
                        FastDynamicAccess.Get(typeof(T10))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 11 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
    /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
    /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
    /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
    /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
        where T9 : class, new()
        where T10 : class, new()
        where T11 : class, new()
    {
        internal int Count { get { return 11; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// List representing the seventh return data set.
        /// </summary>
        public List<T7> Table7 { get; internal set; }

        /// <summary>
        /// List representing the eigth return data set.
        /// </summary>
        public List<T8> Table8 { get; internal set; }

        /// <summary>
        /// List representing the ninth return data set.
        /// </summary>
        public List<T9> Table9 { get; internal set; }

        /// <summary>
        /// List representing the 10th return data set.
        /// </summary>
        public List<T10> Table10 { get; internal set; }

        /// <summary>
        /// List representing the 11th return data set.
        /// </summary>
        public List<T11> Table11 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the seventh return data set.
        /// </summary>
        public FastDynamicAccess FDATable7 { get { return this.FDAs[6]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the eigth return data set.
        /// </summary>
        public FastDynamicAccess FDATable8 { get { return this.FDAs[7]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the ninth return data set.
        /// </summary>
        public FastDynamicAccess FDATable9 { get { return this.FDAs[8]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 10th return data set.
        /// </summary>
        public FastDynamicAccess FDATable10 { get { return this.FDAs[9]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 11th return data set.
        /// </summary>
        public FastDynamicAccess FDATable11 { get { return this.FDAs[10]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6)),
                        FastDynamicAccess.Get(typeof(T7)),
                        FastDynamicAccess.Get(typeof(T8)),
                        FastDynamicAccess.Get(typeof(T9)),
                        FastDynamicAccess.Get(typeof(T10)),
                        FastDynamicAccess.Get(typeof(T11))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 12 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
    /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
    /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
    /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
    /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
    /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
        where T9 : class, new()
        where T10 : class, new()
        where T11 : class, new()
        where T12 : class, new()
    {
        internal int Count { get { return 12; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// List representing the seventh return data set.
        /// </summary>
        public List<T7> Table7 { get; internal set; }

        /// <summary>
        /// List representing the eigth return data set.
        /// </summary>
        public List<T8> Table8 { get; internal set; }

        /// <summary>
        /// List representing the ninth return data set.
        /// </summary>
        public List<T9> Table9 { get; internal set; }

        /// <summary>
        /// List representing the 10th return data set.
        /// </summary>
        public List<T10> Table10 { get; internal set; }

        /// <summary>
        /// List representing the 11th return data set.
        /// </summary>
        public List<T11> Table11 { get; internal set; }

        /// <summary>
        /// List representing the 12th return data set.
        /// </summary>
        public List<T12> Table12 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the seventh return data set.
        /// </summary>
        public FastDynamicAccess FDATable7 { get { return this.FDAs[6]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the eigth return data set.
        /// </summary>
        public FastDynamicAccess FDATable8 { get { return this.FDAs[7]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the ninth return data set.
        /// </summary>
        public FastDynamicAccess FDATable9 { get { return this.FDAs[8]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 10th return data set.
        /// </summary>
        public FastDynamicAccess FDATable10 { get { return this.FDAs[9]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 11th return data set.
        /// </summary>
        public FastDynamicAccess FDATable11 { get { return this.FDAs[10]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 12th return data set.
        /// </summary>
        public FastDynamicAccess FDATable12 { get { return this.FDAs[11]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6)),
                        FastDynamicAccess.Get(typeof(T7)),
                        FastDynamicAccess.Get(typeof(T8)),
                        FastDynamicAccess.Get(typeof(T9)),
                        FastDynamicAccess.Get(typeof(T10)),
                        FastDynamicAccess.Get(typeof(T11)),
                        FastDynamicAccess.Get(typeof(T12))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 13 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
    /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
    /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
    /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
    /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
    /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
    /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
        where T9 : class, new()
        where T10 : class, new()
        where T11 : class, new()
        where T12 : class, new()
        where T13 : class, new()
    {
        internal int Count { get { return 13; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// List representing the seventh return data set.
        /// </summary>
        public List<T7> Table7 { get; internal set; }

        /// <summary>
        /// List representing the eigth return data set.
        /// </summary>
        public List<T8> Table8 { get; internal set; }

        /// <summary>
        /// List representing the ninth return data set.
        /// </summary>
        public List<T9> Table9 { get; internal set; }

        /// <summary>
        /// List representing the 10th return data set.
        /// </summary>
        public List<T10> Table10 { get; internal set; }

        /// <summary>
        /// List representing the 11th return data set.
        /// </summary>
        public List<T11> Table11 { get; internal set; }

        /// <summary>
        /// List representing the 12th return data set.
        /// </summary>
        public List<T12> Table12 { get; internal set; }

        /// <summary>
        /// List representing the 13th return data set.
        /// </summary>
        public List<T13> Table13 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the seventh return data set.
        /// </summary>
        public FastDynamicAccess FDATable7 { get { return this.FDAs[6]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the eigth return data set.
        /// </summary>
        public FastDynamicAccess FDATable8 { get { return this.FDAs[7]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the ninth return data set.
        /// </summary>
        public FastDynamicAccess FDATable9 { get { return this.FDAs[8]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 10th return data set.
        /// </summary>
        public FastDynamicAccess FDATable10 { get { return this.FDAs[9]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 11th return data set.
        /// </summary>
        public FastDynamicAccess FDATable11 { get { return this.FDAs[10]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 12th return data set.
        /// </summary>
        public FastDynamicAccess FDATable12 { get { return this.FDAs[11]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 13th return data set.
        /// </summary>
        public FastDynamicAccess FDATable13 { get { return this.FDAs[12]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6)),
                        FastDynamicAccess.Get(typeof(T7)),
                        FastDynamicAccess.Get(typeof(T8)),
                        FastDynamicAccess.Get(typeof(T9)),
                        FastDynamicAccess.Get(typeof(T10)),
                        FastDynamicAccess.Get(typeof(T11)),
                        FastDynamicAccess.Get(typeof(T12)),
                        FastDynamicAccess.Get(typeof(T13))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 14 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
    /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
    /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
    /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
    /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
    /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
    /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
    /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
        where T9 : class, new()
        where T10 : class, new()
        where T11 : class, new()
        where T12 : class, new()
        where T13 : class, new()
        where T14 : class, new()
    {
        internal int Count { get { return 14; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// List representing the seventh return data set.
        /// </summary>
        public List<T7> Table7 { get; internal set; }

        /// <summary>
        /// List representing the eigth return data set.
        /// </summary>
        public List<T8> Table8 { get; internal set; }

        /// <summary>
        /// List representing the ninth return data set.
        /// </summary>
        public List<T9> Table9 { get; internal set; }

        /// <summary>
        /// List representing the 10th return data set.
        /// </summary>
        public List<T10> Table10 { get; internal set; }

        /// <summary>
        /// List representing the 11th return data set.
        /// </summary>
        public List<T11> Table11 { get; internal set; }

        /// <summary>
        /// List representing the 12th return data set.
        /// </summary>
        public List<T12> Table12 { get; internal set; }

        /// <summary>
        /// List representing the 13th return data set.
        /// </summary>
        public List<T13> Table13 { get; internal set; }

        /// <summary>
        /// List representing the 14th return data set.
        /// </summary>
        public List<T14> Table14 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the seventh return data set.
        /// </summary>
        public FastDynamicAccess FDATable7 { get { return this.FDAs[6]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the eigth return data set.
        /// </summary>
        public FastDynamicAccess FDATable8 { get { return this.FDAs[7]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the ninth return data set.
        /// </summary>
        public FastDynamicAccess FDATable9 { get { return this.FDAs[8]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 10th return data set.
        /// </summary>
        public FastDynamicAccess FDATable10 { get { return this.FDAs[9]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 11th return data set.
        /// </summary>
        public FastDynamicAccess FDATable11 { get { return this.FDAs[10]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 12th return data set.
        /// </summary>
        public FastDynamicAccess FDATable12 { get { return this.FDAs[11]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 13th return data set.
        /// </summary>
        public FastDynamicAccess FDATable13 { get { return this.FDAs[12]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 14th return data set.
        /// </summary>
        public FastDynamicAccess FDATable14 { get { return this.FDAs[13]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6)),
                        FastDynamicAccess.Get(typeof(T7)),
                        FastDynamicAccess.Get(typeof(T8)),
                        FastDynamicAccess.Get(typeof(T9)),
                        FastDynamicAccess.Get(typeof(T10)),
                        FastDynamicAccess.Get(typeof(T11)),
                        FastDynamicAccess.Get(typeof(T12)),
                        FastDynamicAccess.Get(typeof(T13)),
                        FastDynamicAccess.Get(typeof(T14))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 15 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
    /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
    /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
    /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
    /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
    /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
    /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
    /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
    /// <typeparam name="T15">The type representing the 15th return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
        where T9 : class, new()
        where T10 : class, new()
        where T11 : class, new()
        where T12 : class, new()
        where T13 : class, new()
        where T14 : class, new()
        where T15 : class, new()
    {
        internal int Count { get { return 15; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// List representing the seventh return data set.
        /// </summary>
        public List<T7> Table7 { get; internal set; }

        /// <summary>
        /// List representing the eigth return data set.
        /// </summary>
        public List<T8> Table8 { get; internal set; }

        /// <summary>
        /// List representing the ninth return data set.
        /// </summary>
        public List<T9> Table9 { get; internal set; }

        /// <summary>
        /// List representing the 10th return data set.
        /// </summary>
        public List<T10> Table10 { get; internal set; }

        /// <summary>
        /// List representing the 11th return data set.
        /// </summary>
        public List<T11> Table11 { get; internal set; }

        /// <summary>
        /// List representing the 12th return data set.
        /// </summary>
        public List<T12> Table12 { get; internal set; }

        /// <summary>
        /// List representing the 13th return data set.
        /// </summary>
        public List<T13> Table13 { get; internal set; }

        /// <summary>
        /// List representing the 14th return data set.
        /// </summary>
        public List<T14> Table14 { get; internal set; }

        /// <summary>
        /// List representing the 15th return data set.
        /// </summary>
        public List<T15> Table15 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the seventh return data set.
        /// </summary>
        public FastDynamicAccess FDATable7 { get { return this.FDAs[6]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the eigth return data set.
        /// </summary>
        public FastDynamicAccess FDATable8 { get { return this.FDAs[7]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the ninth return data set.
        /// </summary>
        public FastDynamicAccess FDATable9 { get { return this.FDAs[8]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 10th return data set.
        /// </summary>
        public FastDynamicAccess FDATable10 { get { return this.FDAs[9]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 11th return data set.
        /// </summary>
        public FastDynamicAccess FDATable11 { get { return this.FDAs[10]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 12th return data set.
        /// </summary>
        public FastDynamicAccess FDATable12 { get { return this.FDAs[11]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 13th return data set.
        /// </summary>
        public FastDynamicAccess FDATable13 { get { return this.FDAs[12]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 14th return data set.
        /// </summary>
        public FastDynamicAccess FDATable14 { get { return this.FDAs[13]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 15th return data set.
        /// </summary>
        public FastDynamicAccess FDATable15 { get { return this.FDAs[14]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6)),
                        FastDynamicAccess.Get(typeof(T7)),
                        FastDynamicAccess.Get(typeof(T8)),
                        FastDynamicAccess.Get(typeof(T9)),
                        FastDynamicAccess.Get(typeof(T10)),
                        FastDynamicAccess.Get(typeof(T11)),
                        FastDynamicAccess.Get(typeof(T12)),
                        FastDynamicAccess.Get(typeof(T13)),
                        FastDynamicAccess.Get(typeof(T14)),
                        FastDynamicAccess.Get(typeof(T15))
                    };
                }

                return m_fdas;
            }
        }
    }

    /// <summary>
    /// DALTuple representing 16 return sets.
    /// </summary>
    /// <typeparam name="T1">The type representing the first return data set.</typeparam>
    /// <typeparam name="T2">The type representing the second return data set.</typeparam>
    /// <typeparam name="T3">The type representing the third return data set.</typeparam>
    /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
    /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
    /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
    /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
    /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
    /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
    /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
    /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
    /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
    /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
    /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
    /// <typeparam name="T15">The type representing the 15th return data set.</typeparam>
    /// <typeparam name="T16">The type representing the 16th return data set.</typeparam>
    public sealed class DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : DALTupleBase<T1>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
        where T9 : class, new()
        where T10 : class, new()
        where T11 : class, new()
        where T12 : class, new()
        where T13 : class, new()
        where T14 : class, new()
        where T15 : class, new()
        where T16 : class, new()
    {
        internal int Count { get { return 16; } }

        /// <summary>
        /// List representing the second return data set.
        /// </summary>
        public List<T2> Table2 { get; internal set; }

        /// <summary>
        /// List representing the third return data set.
        /// </summary>
        public List<T3> Table3 { get; internal set; }

        /// <summary>
        /// List representing the fourth return data set.
        /// </summary>
        public List<T4> Table4 { get; internal set; }

        /// <summary>
        /// List representing the fifth return data set.
        /// </summary>
        public List<T5> Table5 { get; internal set; }

        /// <summary>
        /// List representing the sixth return data set.
        /// </summary>
        public List<T6> Table6 { get; internal set; }

        /// <summary>
        /// List representing the seventh return data set.
        /// </summary>
        public List<T7> Table7 { get; internal set; }

        /// <summary>
        /// List representing the eigth return data set.
        /// </summary>
        public List<T8> Table8 { get; internal set; }

        /// <summary>
        /// List representing the ninth return data set.
        /// </summary>
        public List<T9> Table9 { get; internal set; }

        /// <summary>
        /// List representing the 10th return data set.
        /// </summary>
        public List<T10> Table10 { get; internal set; }

        /// <summary>
        /// List representing the 11th return data set.
        /// </summary>
        public List<T11> Table11 { get; internal set; }

        /// <summary>
        /// List representing the 12th return data set.
        /// </summary>
        public List<T12> Table12 { get; internal set; }

        /// <summary>
        /// List representing the 13th return data set.
        /// </summary>
        public List<T13> Table13 { get; internal set; }

        /// <summary>
        /// List representing the 14th return data set.
        /// </summary>
        public List<T14> Table14 { get; internal set; }

        /// <summary>
        /// List representing the 15th return data set.
        /// </summary>
        public List<T15> Table15 { get; internal set; }

        /// <summary>
        /// List representing the 16th return data set.
        /// </summary>
        public List<T16> Table16 { get; internal set; }

        /// <summary>
        /// FastDynamicAccess object representing the type of the first return data set.
        /// </summary>
        public FastDynamicAccess FDATable1 { get { return this.FDAs[0]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the second return data set.
        /// </summary>
        public FastDynamicAccess FDATable2 { get { return this.FDAs[1]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the third return data set.
        /// </summary>
        public FastDynamicAccess FDATable3 { get { return this.FDAs[2]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fourth return data set.
        /// </summary>
        public FastDynamicAccess FDATable4 { get { return this.FDAs[3]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the fifth return data set.
        /// </summary>
        public FastDynamicAccess FDATable5 { get { return this.FDAs[4]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the sixth return data set.
        /// </summary>
        public FastDynamicAccess FDATable6 { get { return this.FDAs[5]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the seventh return data set.
        /// </summary>
        public FastDynamicAccess FDATable7 { get { return this.FDAs[6]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the eigth return data set.
        /// </summary>
        public FastDynamicAccess FDATable8 { get { return this.FDAs[7]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the ninth return data set.
        /// </summary>
        public FastDynamicAccess FDATable9 { get { return this.FDAs[8]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 10th return data set.
        /// </summary>
        public FastDynamicAccess FDATable10 { get { return this.FDAs[9]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 11th return data set.
        /// </summary>
        public FastDynamicAccess FDATable11 { get { return this.FDAs[10]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 12th return data set.
        /// </summary>
        public FastDynamicAccess FDATable12 { get { return this.FDAs[11]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 13th return data set.
        /// </summary>
        public FastDynamicAccess FDATable13 { get { return this.FDAs[12]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 14th return data set.
        /// </summary>
        public FastDynamicAccess FDATable14 { get { return this.FDAs[13]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 15th return data set.
        /// </summary>
        public FastDynamicAccess FDATable15 { get { return this.FDAs[14]; } }

        /// <summary>
        /// FastDynamicAccess object representing the type of the 16th return data set.
        /// </summary>
        public FastDynamicAccess FDATable16 { get { return this.FDAs[15]; } }

        /// <summary>
        /// List of FastDynamicAccess objets representing each return data set.
        /// </summary>
        public List<FastDynamicAccess> FDAs
        {
            get
            {
                if (m_fdas == null)
                {
                    m_fdas = new List<FastDynamicAccess>
                    {
                        FastDynamicAccess.Get(typeof(T1)),
                        FastDynamicAccess.Get(typeof(T2)),
                        FastDynamicAccess.Get(typeof(T3)),
                        FastDynamicAccess.Get(typeof(T4)),
                        FastDynamicAccess.Get(typeof(T5)),
                        FastDynamicAccess.Get(typeof(T6)),
                        FastDynamicAccess.Get(typeof(T7)),
                        FastDynamicAccess.Get(typeof(T8)),
                        FastDynamicAccess.Get(typeof(T9)),
                        FastDynamicAccess.Get(typeof(T10)),
                        FastDynamicAccess.Get(typeof(T11)),
                        FastDynamicAccess.Get(typeof(T12)),
                        FastDynamicAccess.Get(typeof(T13)),
                        FastDynamicAccess.Get(typeof(T14)),
                        FastDynamicAccess.Get(typeof(T15)),
                        FastDynamicAccess.Get(typeof(T16))
                    };
                }

                return m_fdas;
            }
        }
    }
}