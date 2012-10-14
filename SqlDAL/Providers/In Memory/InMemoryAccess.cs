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
using System.Linq;
using System.Data.DBAccess.Generic.Exceptions;
using System.Collections;

namespace System.Data.DBAccess.Generic.Providers.In_Memory
{
    public class InMemoryAccess<T> : IDBAccess, ICustomPopulate, IEnumerable<T>
        where T : class, new()
    {
        /// <summary>
        /// Constructor taking an enumeration.
        /// </summary>
        /// <param name="source">The objects to use as the data source.</param>
        public InMemoryAccess(IEnumerable<T> source) : this(source, null)
        { }

        /// <summary>
        /// Constructor taking an enumeration and a tableName.
        /// </summary>
        /// <param name="source">The objects to use as the data source.</param>
        /// <param name="tableName">The table name to apply to an ExecuteRead.</param>
        public InMemoryAccess(IEnumerable<T> source, String tableName)
        {
            this.m_Source = source.ToList();
            this.m_TableName = tableName;
            ((IDBAccess)this).ModelsData = new Dictionary<Type, ModelData>();
            this.ValidateForDAL(typeof(T));
        }

        /// <summary>
        /// The objects.
        /// </summary>
        private List<T> m_Source;

        /// <summary>
        /// The table name to apply to an ExecuteRead.
        /// </summary>
        private String m_TableName;

        /// <summary>
        /// Performs and ExecuteReadQuick against the data source.  This simply packs the object enumeration into a list of object arrays.  There is one object array which has one element, the souce list.
        /// </summary>
        /// <returns>The ExecuteReadQuickTuple object with the source list and table name set.</returns>
        ExecuteReadQuickTuple IDBAccess.ExecuteReadQuick()
        {
            return new ExecuteReadQuickTuple
            {
                ColumnNames = null,
                ColumnTypes = null,
                TableName = this.m_TableName,
                DataRows = new List<Object[]> { new Object[] { this.m_Source } }
            };
        }

        /// <summary>
        /// Populates a model base enumeration using the souce list.
        /// </summary>
        /// <typeparam name="T1">The type of each element in the list.</typeparam>
        /// <param name="tuple">The ExecuteReadQuickTuple object.  This is not used by this provider.</param>
        /// <param name="parentChildPropertyName">The parentChildPropertyName string list.  This is not used by this provider.</param>
        /// <returns>The list of objects from the tuple.</returns>
        List<T1> ICustomPopulate.PopulateModelBaseEnumeration<T1>(ExecuteReadQuickTuple tuple, List<string> parentChildPropertyName)
        {
            if (typeof(T1) != typeof(T))
                throw new Exception(String.Format("Type passed to PopulateModelBaseEnumeration '{0}' does not match the type on the InMemoryAccess class '{1}'.", typeof(T1), typeof(T)));

            //T1 will be the same type of T on the class, but the compiler doesn't know that
            return this.m_Source as List<T1>;
        }

        /// <summary>
        /// Populates a list of objects using the source list.
        /// </summary>
        /// <param name="tuple">The ExecuteReadQuickTuple object.  This is by used by this provider.</param>
        /// <param name="modelType">The type of each object in the list.  This is by used by this provider.</param>
        /// <param name="parentChildPropertyName">The parentChildPropertyName string list.  This is not used by this provider.</param>
        /// <returns></returns>
        List<Object> ICustomPopulate.PopulateModelBaseEnumeration(ExecuteReadQuickTuple tuple, Type modelType, List<string> parentChildPropertyName)
        {
            return this.m_Source.OfType<Object>().ToList();
        }

        /// <summary>
        /// Executes an ExecuteScalar against the objects source.  This returns the first element from the enumeration.
        /// </summary>
        /// <returns>The first element from the enumeration.</returns>
        public Object ExecuteScalar()
        {
            return this.m_Source.First();
        }

        /// <summary>
        /// Executes a read operation against the objects source.  This returns the objects.
        /// </summary>
        /// <returns>The list of objects.</returns>
        public List<T> ExecuteRead()
        {
            return this.m_Source;
        }

        /// <summary>
        /// ModelsData not supported for InMemory provider.
        /// </summary>
        Dictionary<Type, ModelData> IDBAccess.ModelsData { get; set; }

        #region Unsupported interface functions
        /// <summary>
        /// ExecuteNonQuery not supported for InMemory provider
        /// </summary>
        /// <returns>Throws NotImplementedException.</returns>
        public int ExecuteNonQuery()
        {
            throw new NotImplementedException("ExecuteNonQuery not supported for InMemory provider.");
        }

        /// <summary>
        /// ExecuteSetReadQuick not supported for InMemory provider.
        /// </summary>
        /// <returns>Throws NotImplementedException.</returns>
        List<ExecuteReadQuickTuple> IDBAccess.ExecuteSetReadQuick()
        {
            throw new NotImplementedException("ExecuteSetReadQuick not supported for InMemory provider.");
        }

        /// <summary>
        /// PopulateDevaultValues not supported for InMemory provider.
        /// </summary>
        bool IDBAccess.PopulateDefaultValues
        {
            get { throw new NotImplementedException("PopulateDevaultValues not supported for InMemory provider."); }
            set { throw new NotImplementedException("PopulateDefaultValues not supported for InMemory provider."); }
        }

        /// <summary>
        /// TraceOutputLevel not supported for InMemory provider.
        /// </summary>
        TraceLevel IDBAccess.TraceOutputLevel
        {
            get { throw new NotImplementedException("TraceOutputLevel not supported for InMemory provider."); }
            set { throw new NotImplementedException("TraceOutputLevel not supported for InMemory provider."); }
        }
        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            return this.m_Source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this.m_Source as IEnumerable).GetEnumerator();
        }
    }
}