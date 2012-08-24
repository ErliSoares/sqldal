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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Data.DBAccess.Generic.RuntimeClass;
using System.Reflection.Emit;

namespace System.Data.DBAccess.Generic
{
    /// <summary>
    /// Extenion methods used by the DAL.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Dictioanry of mappings between CLR types and Sql types.
        /// </summary>
        private static Dictionary<Type, DbType> m_typeToDBType;

        /// <summary>
        /// Static constructor.  Initializes the type mapping dictionary.
        /// </summary>
        static Extensions()
        {
            m_typeToDBType = new Dictionary<Type, DbType>();
        }

        /// <summary>
        /// Converts any SqlParameters with a value of null to DB.Null.
        /// </summary>
        /// <param name="parameters">The SqlParameter collection to convert.</param>
        public static void ConvertNullValuesToDBNull(this IEnumerable<SqlParameter> parameters)
        {
            foreach (var p in parameters.Where(p => p.Value == null))
                p.Value = DBNull.Value;
        }

        /// <summary>
        /// Nullable type.
        /// </summary>
        private static readonly Type m_nullableType = typeof(Nullable<>);

        /// <summary>
        /// Returns if the type is a nullable value type or not.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True or false.</returns>
        internal static Boolean IsNullableValueType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == m_nullableType;
        }

        /// <summary>
        /// Gets the value type of the nullable value type.
        /// </summary>
        /// <param name="type">The nullable value type.</param>
        /// <returns>The type.</returns>
        internal static Type GetNullableUnderlyingType(this Type type)
        {
            if (!type.IsNullableValueType())
                return type;

            return type.GetGenericArguments().First();
        }

        /*There are much better ways to get the type of an enumeration (GetGenericArguments)
         * But they fail if the enumeration is a LINQ query operator (if the user didn't force evaluation on the query)
         * This works in all cases
         */

        /// <summary>
        /// Gets the type of each element in an enumeration.
        /// </summary>
        /// <param name="collection">The enumeration.</param>
        /// <returns>The type.</returns>
        internal static Type GetIEnumerableGenericType(this IEnumerable collection)
        {
            if (!collection.OfType<Object>().Any())
                return collection.GetType().GetIEnumerableGenericType();

            return collection.OfType<Object>().First().GetType();
        }


        /*There may be better ways in certain cases to get the type of an enumeration (GetGenericArguments)
         * But they fail if the enumeration is a LINQ query operator (if the user didn't force evaluation on the query)
         * This works in all cases
         */

        /// <summary>
        /// Gets the type of each element in an enumerable type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type.</returns>
        /// <exception cref="ArgumentException">Thrown if the type passed is not of type System.Collections.IEnumerable.</exception>
        internal static Type GetIEnumerableGenericType(this Type type)
        {
            if (!type.IsIEnumerable())
                throw new ArgumentException(String.Format("Type passed '{0}' is not of type System.Collections.IEnumerable", type));
            if (type.IsArray)
                return type.GetElementType();
            else
                return type.GetMethod("GetEnumerator").ReturnType.GetMethod("get_Current").ReturnType;
        }

        /// <summary>
        /// Returns if an object is a "UDTable" enumeration.  This means an enumeration of user classes.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>True or False.</returns>
        internal static Boolean IsUDTableEnumeration(this Object obj)
        {
            return obj.GetType().IsUDTableEnumeration();
        }

        /// <summary>
        /// Returns if a type is a "UDTable" enumeration.  This means an enumeration of user classes.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True or False.</returns>
        internal static Boolean IsUDTableEnumeration(this Type type)
        {
            if (!type.IsIEnumerable())
                return false;

            if (type.IsArray)
                return type.GetElementType().IsUserType() || type.GetElementType().IsAnonymousType();
            else
                return type.GetIEnumerableGenericType().IsUserType() || type.GetIEnumerableGenericType().IsAnonymousType();
        }

        internal static List<Type> s_DotNETTypes = new List<Type>
        {
            typeof(int),
            typeof(int?),
            typeof(long),
            typeof(long?),
            typeof(byte),
            typeof(byte?),
            typeof(short),
            typeof(short?),
            typeof(float),
            typeof(float?),
            typeof(double),
            typeof(double?),
            typeof(decimal),
            typeof(decimal?),
            typeof(TimeSpan),
            typeof(TimeSpan?),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(string),
            typeof(bool),
            typeof(bool?)
        };

        /// <summary>
        /// Mapping of CLR value types to their nullable value type.
        /// </summary>
        internal static Dictionary<Type, Type> s_NullableTypeLookup = new Dictionary<Type, Type>
        {
            { typeof(int), typeof(int?) },
            { typeof(long), typeof(long?) },
            { typeof(float), typeof(float?) },
            { typeof(double), typeof(double?) },
            { typeof(short), typeof(short?) },
            { typeof(decimal), typeof(decimal?) },
            { typeof(byte), typeof(byte?) },
            { typeof(bool), typeof(bool?) },
            { typeof(DateTime), typeof(DateTime?) },
            { typeof(TimeSpan), typeof(TimeSpan?) }
        };

        /// <summary>
        /// Returns true/false if the type is a user type or not (if it's NOT a framework type).
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True or False.</returns>
        internal static Boolean IsUserType(this Type type)
        {
            return type.IsAnonymousType() || !Extensions.s_DotNETTypes.Contains(type) && !type.DerivesInterface(typeof(IEnumerable));
        }

        /// <summary>
        /// Returns if the type is an anonymous type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True or False.</returns>
        internal static bool IsAnonymousType(this Type type)
        {
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                && type.IsGenericType && type.Name.Contains("AnonymousType")
                && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }

        /// <summary>
        /// Returns if the object derives from a base type.
        /// </summary>
        /// <param name="type">The object in question.</param>
        /// <param name="baseType">The base type.</param>
        /// <returns>True or False.</returns>
        internal static Boolean DerivesFromType(this Object obj, Type baseType)
        {
            return obj.GetType().DerivesFromType(baseType);
        }

        /// <summary>
        /// Returns if the type derives from a base type.
        /// </summary>
        /// <param name="type">The type in question.</param>
        /// <param name="baseType">The base type.</param>
        /// <returns>True or False.</returns>
        internal static Boolean DerivesFromType(this Type type, Type baseType)
        {
            if (type == baseType) return true;

            Type bType = type.BaseType;
            while (bType != null)
            {
                if (bType == baseType) return true;
                bType = bType.BaseType;
            }

            return false;
        }

        /// <summary>
        /// Returns if the type derives an interface.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="interfaceType">The interface type.</param>
        /// <returns>True or False.</returns>
        /// <exception cref="ArgumentException">Thrown if the interface type passed is not an interface.</exception>
        internal static Boolean DerivesInterface(this Object obj, Type interfaceType)
        {
            return obj.GetType().DerivesInterface(interfaceType);
        }

        /// <summary>
        /// Returns if the type derives an interface.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="interfaceType">The interface type.</param>
        /// <returns>True or False.</returns>
        /// <exception cref="ArgumentException">Thrown if the interface type passed is not an interface.</exception>
        internal static Boolean DerivesInterface(this Type type, Type interfaceType)
        {
            if (!interfaceType.IsInterface)
                throw new ArgumentException(String.Format("Parameter interfaceType '{0}' is not an interface type.", interfaceType.GetType()));

            return type.GetInterfaces().Any(i => i == interfaceType);
        }

        /// <summary>
        /// Returns if the type is an enumerable type or not.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True or False.</returns>
        internal static Boolean IsIEnumerable(this Type type)
        {
            return type != typeof(String) && type.GetInterfaces().Any(i => i == typeof(System.Collections.IEnumerable));
        }

        /// <summary>
        /// Returns if the object is an enumeration or not.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>True or False.</returns>
        internal static Boolean IsIEnumerable(this Object obj)
        {
            return obj.GetType().IsIEnumerable();
        }

        /// <summary>
        /// Converts a CLR type to a SQL type.
        /// </summary>
        /// <param name="type">The CLR type.</param>
        /// <returns>The SQL type.</returns>
        internal static DbType ToDbType(this Type type)
        {
            if (!m_typeToDBType.ContainsKey(type))
            {
                // types which do not automatically convert
                if (type == typeof(TimeSpan))
                {
                    m_typeToDBType.Add(type, DbType.Time);
                }
                else if (type == typeof(Byte[]))
                {
                    m_typeToDBType.Add(type, DbType.Binary);
                }
                else
                {
                    SqlParameter p = new SqlParameter();
                    TypeConverter tc = TypeDescriptor.GetConverter(p.DbType);

                    if (type.IsNullableValueType())
                    {
                        return type.GetNullableUnderlyingType().ToDbType();
                    }
                    else if (tc.CanConvertFrom(type))
                    {
                        p.DbType = (DbType)tc.ConvertFrom(type.Name);
                    }
                    else
                    {
                        try
                        {
                            p.DbType = (DbType)tc.ConvertFrom(type.Name);
                        }
                        catch (Exception) { }
                    }

                    m_typeToDBType.Add(type, p.DbType);
                }
            }

            return m_typeToDBType[type];
        }

        /// <summary>
        /// Turns an IDataParameterCollection into a dictionary of the parameter names and values.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The dictionary.</returns>
        internal static Dictionary<String, Object> ToNameValueDictionary(this IDataParameterCollection parameters)
        {
            return parameters.OfType<IDbDataParameter>().ToNameValueDictionary();
        }

        /// <summary>
        /// Turns an IDataParameterCollection of the given direction into a dictionary of the parameter names and values.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="direction">The parameter direction to filter on.</param>
        /// <returns>The dictionary.</returns>
        internal static Dictionary<String, Object> ToNameValueDictionary(this IDataParameterCollection parameters, ParameterDirection direction)
        {
            return parameters.OfType<IDbDataParameter>().ToNameValueDictionary(direction);
        }

        /// <summary>
        /// Turns an enumeration of IDbParameters into a dictionary of the parameter names and values.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The dictionary.</returns>
        internal static Dictionary<String, Object> ToNameValueDictionary(this IEnumerable<IDataParameter> parameters)
        {
            return parameters.ToDictionary(p => p.ParameterName, p => p.Value);
        }

        /// <summary>
        /// Turns an enumeration of IDbParameters of the given direction into a dictionary of the parameter names and values.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="direction">The parameter direction to filter on.</param>
        /// <returns>The dictionary.</returns>
        internal static Dictionary<String, Object> ToNameValueDictionary(this IEnumerable<IDataParameter> parameters, ParameterDirection direction)
        {
            return parameters.Where(p => p.Direction == direction).ToDictionary(p => p.ParameterName, p => p.Value);
        }

        /// <summary>
        /// Gets a hash code of the column structure with a given property name into which the data belonging to these columns will be stored.
        /// </summary>
        /// <param name="cols">Pre determined dictionary of column names and data types.</param>
        /// <param name="parentChildPropertyName">The parent property name.</param>
        /// <returns>The hash code.</returns>
        internal static String GetUniqueIdentifier(this Dictionary<String, Type> cols, String tableName, List<String> parentChildPropertyName = null)
        {
            String parents = (parentChildPropertyName == null) ? "" : String.Join("", parentChildPropertyName);
            return (String.Join("", cols.Select(c => String.Format("{0}{1}", c.Key, c.Value.FullName))) + tableName + parents).GenerateHash();
        }

        /// <summary>
        /// Generates a hash code of a string.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <returns>The hash code.</returns>
        internal static String GenerateHash(this String input)
        {
            byte[] hash;
            byte[] binaryData;

            using (HashAlgorithm hashAlg = new SHA1Managed())
            {
                using (Stream str = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(input)))
                {
                    hash = hashAlg.ComputeHash(str);

                    str.Position = 0;
                    binaryData = new byte[(int)str.Length];
                    str.Read(binaryData, 0, binaryData.Length);
                }
            }

            return binaryData.GenerateHash();
        }

        /// <summary>
        /// Generates a hash code of a Byte[]
        /// </summary>
        /// <param name="data">The Byte[]</param>
        /// <returns>The hash code.</returns>
        private static String GenerateHash(this Byte[] data)
        {
            return BitConverter.ToString(new SHA1Managed().ComputeHash(data));
        }

        /// <summary>
        /// Gets a columns from a datarow object while doing the appropriate DBNull.Value check.
        /// </summary>
        /// <typeparam name="T">The type which should be returned.</typeparam>
        /// <param name="dr">The data row from which to read.</param>
        /// <param name="colName">The column name to read.</param>
        /// <returns>The value of the column cast to a T.</returns>
        public static T GetValue<T>(this DataRow dr, String colName)
        {
            //if null, get the default value of type T.
            //if it's null, we can cast this to null
            //set ret to null and return it

            return dr[colName].CastToT<T>();
        }

        /// <summary>
        /// Casts an object into the type T.
        /// </summary>
        /// <typeparam name="T">The type to which to cast.</typeparam>
        /// <param name="value">The value to cast.</param>
        /// <returns>The casted value.</returns>
        public static T CastToT<T>(this Object value)
        {
            if (value == DBNull.Value || value == null)
            {
                T val = default(T);
                if (val == null)
                    return val;
            }

            //else try to cast to t.  if the type provided is incorrect this will throw an exception
            return (T)value;
        }

        /// <summary>
        /// Divides an enumeration up into pages.
        /// </summary>
        /// <typeparam name="T">The type of element the enumeration contains.</typeparam>
        /// <param name="source">The enumeration to page.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>An enumeration of IEnumerablePage objects.</returns>
        public static IEnumerable<IEnumerablePage<T>> AsPaged<T>(this IEnumerable<T> source, int pageSize)
        {
            if (pageSize < 1)
            {
                throw new ArgumentException("Page size must be greater than 0.");
            }

            int totalCount = source.Count();
            int currPageCount = 0;

            T[] currPage;
            if (pageSize > totalCount)
                currPage = new T[totalCount];
            else
                currPage = new T[pageSize];

            int currPageNum = 1;

            if (source is List<T>)
            {
                var list = source as List<T>;

                for (int i = 0; i < list.Count; i++)
                {
                    currPage[currPageCount] = list[i];
                    currPageCount++;

                    if (currPageCount % pageSize == 0)
                    {
                        yield return new IEnumerablePage<T>
                        {
                            Page = currPageNum,
                            Items = currPage
                        };

                        currPageNum++;

                        if (totalCount - currPageCount < pageSize)
                            currPage = new T[totalCount - currPageCount];
                        else
                            currPage = new T[pageSize];

                        currPageCount = 0;
                    }
                }
            }
            else if (source is Array)
            {
                var array = source as T[];

                for (int i = 0; i < array.Length; i++)
                {
                    currPage[currPageCount] = array[i];
                    currPageCount++;

                    if (currPageCount % pageSize == 0)
                    {
                        yield return new IEnumerablePage<T>
                        {
                            Page = currPageNum,
                            Items = currPage
                        };

                        currPageNum++;

                        if (totalCount - currPageCount < pageSize)
                            currPage = new T[totalCount - currPageCount];
                        else
                            currPage = new T[pageSize];

                        currPageCount = 0;
                    }
                }
            }
            else
            {
                foreach (var item in source)
                {
                    currPage[currPageCount] = item;
                    currPageCount++;

                    if (currPageCount % pageSize == 0)
                    {
                        yield return new IEnumerablePage<T>
                        {
                            Page = currPageNum,
                            Items = currPage
                        };

                        currPageNum++;

                        if (totalCount - currPageCount < pageSize)
                            currPage = new T[totalCount - currPageCount];
                        else
                            currPage = new T[pageSize];

                        currPageCount = 0;
                    }
                }
            }

            //if left over items in a smaller page
            if (currPageCount % pageSize != 0)
            {
                yield return new IEnumerablePage<T>
                {
                    Page = currPageNum,
                    Items = currPage
                };
            }
        }

        /// <summary>
        /// Duplicates the given IDBaccess object count number of times.  This is used to give each return type its corresponding IDBaccess object in a cross provider set read.
        /// </summary>
        /// <param name="db">The IDBAccess object.</param>
        /// <param name="count">The number of times to repeat it.</param>
        /// <returns>A list of IDBAccess objects.</returns>
        internal static List<IDBAccess> ToList(this IDBAccess db, int count)
        {
            var ret = new List<IDBAccess>(count);
            for (int i = 0; i < count; i++)
                ret.Add(db);

            return ret;
        }

        #region Event helper functions
        /// <summary>
        /// Gets the original query function that was called.  This is only meant to be called inside of an IDBAccess class to alter behavior depending on what the first public API call was.
        /// </summary>
        /// <returns>The name of the function.</returns>
        public static String GetOriginalCallingFunctionName(this IDBAccess db)
        {
            return db.GetOriginalCallingFunctionName(db.GetType().Name);
        }

        /// <summary>
        /// Gets the original query function that was called.
        /// </summary>
        /// <param name="className">The name of the originating class.</param>
        /// <returns>The name of the function.</returns>
        private static String GetOriginalCallingFunctionName(this IDBAccess db, String className)
        {
            return db.GetOriginalCallingFunction(className).Name;
        }

        /// <summary>
        /// Gets the original calling function.
        /// </summary>
        /// <param name="className">The originating class.</param>
        /// <returns>The MethodBase of the original calling function.</returns>
        private static MethodBase GetOriginalCallingFunction(this IDBAccess db, String className)
        {
            return new StackTrace().GetFrames().Last(sf => sf.GetMethod().DeclaringType.Name == className || sf.GetMethod().DeclaringType.Name == "IDBAccessExtensions" || sf.GetMethod().DeclaringType.Name == "DotNETCompatibleProvider`6").GetMethod();
        }
        #endregion

        #region JSON
        /// <summary>
        /// Serializes an object to a JSON string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The JSON string.</returns>
        public static String SerializeToJSON(this Object obj, Boolean includeTableName = false)
        {
            var jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new RuntimeTypePropertyJSONConverter(includeTableName) });
            jss.MaxJsonLength = int.MaxValue;
            return jss.Serialize(obj);
        }

        public static String SerializeToJSON<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> obj, Boolean includeTableName = false)
        {
            var jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new RuntimeTypePropertyJSONConverter(includeTableName) });
            jss.MaxJsonLength = int.MaxValue;
            return jss.Serialize(obj.ToSerializableGrouping().ToNonAnonymousLINQGrouping());
        }
        #endregion

        #region XML
        /// <summary>
        /// Serializes and object to an XML string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="rootElementName">The optional root element name.  If this is a runtime type it will default to the TableName.  If it is a list of runtime types it will default to DALRuntimeTypeBase.</param>
        /// <returns>The XML string.</returns>
        public static String SerializeToXML(this Object obj, String rootElementName = null)
        {
            return obj.SerializeToXML(rootElementName, false);
        }

        /// <summary>
        /// Serializes and object to an XML string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="rootElementName">The optional root element name.  If this is a runtime type it will default to the TableName.  If it is a list of runtime types it will default to DALRuntimeTypeBase.</param>
        /// <returns>The XML string.</returns>
        public static String SerializeToXML<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> obj, String rootElementName = null)
        {
            return obj.ToSerializableGrouping().SerializeToXML(rootElementName, false);
        }

        internal static String SerializeToXML(this Object obj, String rootElementName, Boolean omitXmlDeclaration)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;
            settings.CloseOutput = true;
            settings.CheckCharacters = true;
            settings.OmitXmlDeclaration = omitXmlDeclaration;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");


            using (var sw = new UTF8StringWriter())
            {
                using (var xw = XmlWriter.Create(sw, settings))
                {
                    if (obj is DALRuntimeTypeBase)
                    {
                        //omit namespaces as well
                        if (omitXmlDeclaration)
                        {
                            new XmlSerializer(typeof(DALRuntimeTypeBase), new XmlAttributeOverrides(), new Type[] { obj.GetType() }, new XmlRootAttribute(rootElementName ?? (obj as DALRuntimeTypeBase).TableName), "").Serialize(xw, obj, ns);
                        }
                        else
                        {
                            new XmlSerializer(typeof(DALRuntimeTypeBase), new XmlAttributeOverrides(), new Type[] { obj.GetType() }, new XmlRootAttribute(rootElementName ?? (obj as DALRuntimeTypeBase).TableName), "").Serialize(xw, obj);
                        }
                    }
                    else if (obj.GetType().IsIEnumerable() && obj.GetType().GetIEnumerableGenericType() == typeof(Object))
                    {
                        var baseObjs = ((List<Object>)obj).OfType<DALRuntimeTypeBase>().ToList();
                        //omit namespaces as well
                        if (omitXmlDeclaration)
                        {
                            new XmlSerializer(typeof(DALRuntimeTypeList), new XmlAttributeOverrides(), new Type[] { }, new XmlRootAttribute(rootElementName), "").Serialize(xw, new DALRuntimeTypeList(baseObjs), ns);
                        }
                        else
                        {
                            new XmlSerializer(typeof(DALRuntimeTypeList), new XmlAttributeOverrides(), new Type[] { }, new XmlRootAttribute(rootElementName), "").Serialize(xw, new DALRuntimeTypeList(baseObjs));
                        }
                    }
                    else if (obj.GetType().IsAnonymousType())
                    {
                        return obj.ToNonAnonymousType().SerializeToXML(rootElementName, omitXmlDeclaration);
                    }
                    else if (obj.GetType().IsIEnumerable() && obj.GetType().GetIEnumerableGenericType().IsAnonymousType())
                    {
                        return (obj as IEnumerable).OfType<Object>().Select(o => o.ToNonAnonymousType()).ToList().SerializeToXML(rootElementName, omitXmlDeclaration);
                    }
                    else if (obj is SerializableLINQGrouping)
                    {
                        var nonAnon = (obj as SerializableLINQGrouping).ToNonAnonymousLINQGrouping();
                        var nonAnonType = nonAnon.GetType();
                        //omit namespaces as well
                        if (omitXmlDeclaration)
                        {
                            new XmlSerializer(nonAnonType, new XmlAttributeOverrides(), new Type[] { nonAnonType }, new XmlRootAttribute(rootElementName), "").Serialize(xw, nonAnon, ns);
                        }
                        else
                        {
                            new XmlSerializer(nonAnonType, new XmlAttributeOverrides(), new Type[] { nonAnonType }, new XmlRootAttribute(rootElementName), "").Serialize(xw, nonAnon);
                        }
                    }
                    else
                    {
                        //omit namespaces as well
                        if (omitXmlDeclaration)
                        {
                            new XmlSerializer(obj.GetType(), new XmlAttributeOverrides(), new Type[] { obj.GetType() }, new XmlRootAttribute(rootElementName), "").Serialize(xw, obj, ns);
                        }
                        else
                        {
                            new XmlSerializer(obj.GetType(), new XmlAttributeOverrides(), new Type[] { obj.GetType() }, new XmlRootAttribute(rootElementName), "").Serialize(xw, obj);
                        }
                    }

                    return sw.ToString();
                }
            }
        }

        private static Boolean IsLINQGrouping(this Object obj)
        {
            return obj.GetType().IsLINQGrouping();
        }

        private static Boolean IsLINQGrouping(this Type type)
        {
            var i = type.GetInterfaces().FirstOrDefault();
            if (i == null)
                return false;

            return String.Format("{0}.{1}", i.Namespace, i.Name) == "System.Linq.IGrouping`2";
        }

        private static IGroupingTypes GetIGroupingTypes(Type type)
        {
            if (!type.IsLINQGrouping())
                throw new ArgumentException(String.Format("Passed type '{0}' is not an IGrouping type.", type));

            var args = type.GetGenericArguments();

            return new IGroupingTypes
            {
                KeyType = args[0],
                ElementType = args[1]
            };
        }

        private class IGroupingTypes
        {
            public Type KeyType { get; set; }
            public Type ElementType { get; set; }
        }

        internal static SerializableLINQGrouping<TKey, TElement> ToSerializableGrouping<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> group)
        {
            return new SerializableLINQGrouping<TKey, TElement>(group);
        }
        #endregion

        /// <summary>
        /// Gets the value of an XAttribute, returning null if the attribute is null.
        /// </summary>
        /// <param name="attr">The XAttribute from which to retrieve the value.</param>
        /// <returns>The value.</returns>
        internal static String GetValueSafe(this XAttribute attr)
        {
            if (attr == null)
                return null;

            return attr.Value;
        }

        /// <summary>
        /// Gets the value of an XElement, returning null if the element is null.
        /// </summary>
        /// <param name="elem">The XElement from which to retrieve the value.</param>
        /// <returns>The value.</returns>
        internal static String GetValueSafe(this XElement elem)
        {
            if (elem == null)
                return null;

            return elem.Value;
        }

        /// <summary>
        /// Gets a value from an object via FastDynamicAccess.  This is slower but more convenient to use than going through an FDA object.
        /// </summary>
        /// <param name="obj">The object to read from.</param>
        /// <param name="propertyName">The property to read.</param>
        /// <returns>The value.</returns>
        public static Object GetValue(this Object obj, String propertyName)
        {
            return FastDynamicAccess.Get(obj).Get(obj, propertyName);
        }

        /// <summary>
        /// Gets a value from an object via FastDynamicAccess.  This is slower but more convenient to use than going through an FDA object.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="obj">The object to read from.</param>
        /// <param name="propertyName">The property to read.</param>
        /// <returns>The value cast to a T.</returns>
        public static T GetValue<T>(this Object obj, String propertyName)
        {
            return FastDynamicAccess.Get(obj).Get<T>(obj, propertyName);
        }

        /// <summary>
        /// Gets a value from an object via FastDynamicAccess.  This is slower but more convenient to use than going through an FDA object.
        /// </summary>
        /// <param name="obj">The object to read from.</param>
        /// <param name="propertyIndex">The index of the property in the FDA object.</param>
        /// <returns>The value.</returns>
        public static Object GetValue(this Object obj, int propertyIndex)
        {
            return FastDynamicAccess.Get(obj).Get(obj, propertyIndex);
        }

        /// <summary>
        /// Gets a value from an object via FastDynamicAccess.  This is slower but more convenient to use than going through an FDA object.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="obj">The object to read from.</param>
        /// <param name="propertyIndex">The index of the property in the FDA object.</param>
        /// <returns>The value cast to a T.</returns>
        public static T GetValue<T>(this Object obj, int propertyIndex)
        {
            return FastDynamicAccess.Get(obj).Get<T>(obj, propertyIndex);
        }

        /// <summary>
        /// Sets a value in an object via FastDynamicAccess.  This is slower but more convienent to use than going through an FDA object.
        /// </summary>
        /// <param name="obj">The object to set.</param>
        /// <param name="propertyName">The property to set.</param>
        /// <param name="value">The value to set.</param>
        public static void SetValue(this Object obj, String propertyName, Object value)
        {
            FastDynamicAccess.Get(obj).Set(obj, propertyName, value);
        }

        /// <summary>
        /// Sets a value in an object via FastDynamicAccess.  This is slower but more convienent to use than going through an FDA object.
        /// </summary>
        /// <param name="obj">The object to set.</param>
        /// <param name="propertyIndex">The index of the property in the FDA object.</param>
        /// <param name="value">The value to set.</param>
        public static void SetValue(this Object obj, int propertyIndex, Object value)
        {
            FastDynamicAccess.Get(obj).Set(obj, propertyIndex, value);
        }

        /// <summary>
        /// Used to serialize to a UTF8 encoded string.
        /// </summary>
        private class UTF8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }

        internal static void DeclareLocal<T>(this ILGenerator il)
        {
            il.DeclareLocal(typeof(T));
        }
    }

    /// <summary>
    /// Represents a page of a strongly typed enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IEnumerablePage<T>
    {
        /// <summary>
        /// The page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The items in the page.
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}