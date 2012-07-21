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
using System.Threading.Tasks;

namespace System.Data.DBAccess.Generic
{
    #region CPSegment
    /// <summary>
    /// Internal usage.  Matches an IDBAccess object with a true/false if it's being used for a set read or an execute read.
    /// </summary>
    internal class ProviderAndMethodTuple
    {
        /// <summary>
        /// The IDBAccess object.
        /// </summary>
        internal IDBAccess Provider { get; set; }

        /// <summary>
        /// True/False if it's a set read or an execute read.
        /// </summary>
        internal Boolean IsSetRead { get; set; }
    }

    /// <summary>
    /// A base class providing common functionality for all CPSegment types.
    /// </summary>
    public abstract class CPSegmentBase
    {
        /// <summary>
        /// The IDBAccess object to use for this segment.
        /// </summary>
        internal IDBAccess DBAccess { get; set; }

        /// <summary>
        /// True/False whether or not this is a set read or an execute read.
        /// </summary>
        internal Boolean IsSetRead { get { return this.Tables > 1; } }

        /// <summary>
        /// The number of tables in this segment.
        /// </summary>
        internal int Tables { get; set; }

        /// <summary>
        /// The next segment.
        /// </summary>
        internal CPSegmentBase Next { get; private set; }

        /// <summary>
        /// Gets the last set read in this cross provider operation.
        /// </summary>
        /// <returns>The CPSegmentBase object at the end of this operation.</returns>
        private CPSegmentBase GetLast()
        {
            var tmp = this;
            while (tmp.Next != null)
                tmp = tmp.Next;

            return tmp;
        }

        /// <summary>
        /// Appends a CPSegment object to the end of this set read.
        /// </summary>
        /// <param name="cpsrb">The new set read.</param>
        internal void Add(CPSegmentBase cpsrb)
        {
            this.GetLast().Next = cpsrb;
        }

        /// <summary>
        /// Gets the total number of tables in the cross provider set read.
        /// </summary>
        /// <returns>The number of tables.</returns>
        internal int GetTotalTableCount()
        {
            int count = 0;

            var tmp = this;
            while (tmp != null)
            {
                count += tmp.Tables;
                tmp = tmp.Next;
            }

            return count;
        }

        /// <summary>
        /// Adds all children of the provided set read base to this.
        /// </summary>
        /// <param name="cpsrbWithChildren"></param>
        protected void AddAllChildren(CPSegmentBase cpsrbWithChildren)
        {
            cpsrbWithChildren = cpsrbWithChildren.Next;

            while (cpsrbWithChildren != null)
            {
                this.Add(cpsrbWithChildren);

                cpsrbWithChildren = cpsrbWithChildren.Next;
            }
        }

        /// <summary>
        /// Copies the properties and all children from one CPSegmentBase to another.
        /// </summary>
        /// <param name="dest">The destination CPSegmentBase.</param>
        internal void CopyTo(CPSegmentBase dest)
        {
            dest.DBAccess = this.DBAccess;
            dest.Tables = this.Tables;

            dest.AddAllChildren(this);
        }

        /// <summary>
        /// Gets all providers in this set read.
        /// </summary>
        /// <returns>The list of providers and the method to execute.</returns>
        internal List<ProviderAndMethodTuple> GetProviders()
        {
            var ret = new List<ProviderAndMethodTuple>();

            ret.Add(new ProviderAndMethodTuple
            {
                Provider = this.DBAccess,
                IsSetRead = this.IsSetRead
            });

            var tmp = this;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;

                ret.Add(new ProviderAndMethodTuple
                {
                    Provider = tmp.DBAccess,
                    IsSetRead = tmp.IsSetRead
                });
            }

            return ret;
        }

        /// <summary>
        /// Gets a list of IDBAccess objects, one instance per table.  If there are 15 total tables, this will return 15 IDBAccess objects.  The same IDBAccess object will be emitted multiple times for segments which are set reads.
        /// </summary>
        /// <returns>The list of IDBaccess objects.</returns>
        internal List<IDBAccess> GetProviderPerTable()
        {
            var ret = new List<IDBAccess>();

            var tmp = this;
            do
            {
                ret.AddRange(tmp.DBAccess.ToList(tmp.Tables));
                tmp = tmp.Next;
            } while (tmp != null);

            return ret;
        }

        /// <summary>
        /// Gets a list of all tasks to run for this cross provider read.  There is one task per provider.
        /// </summary>
        /// <returns>The list of Task objects.</returns>
        internal Task<List<ExecuteReadQuickTuple>>[] GetTasks()
        {
            var providers = this.GetProviders();
            var tasks = new Task<List<ExecuteReadQuickTuple>>[providers.Count];

            for (int i = 0; i < providers.Count; i++)
            {
                int providerIndex = i;
                if (providers[providerIndex].IsSetRead)
                {
                    tasks[i] = new Task<List<ExecuteReadQuickTuple>>(() =>
                    {
                        return providers[providerIndex].Provider.ExecuteSetReadQuick();
                    });
                }
                else
                {
                    tasks[providerIndex] = new Task<List<ExecuteReadQuickTuple>>(() =>
                    {
                        return new List<ExecuteReadQuickTuple> { providers[providerIndex].Provider.ExecuteReadQuick() };
                    });
                }
            }

            return tasks;
        }

        /// <summary>
        /// Appends a cross provider set read segment to this one.
        /// </summary>
        /// <typeparam name="T">The type of return.  It will be a CPSegment object with generic aguments.</typeparam>
        /// <typeparam name="NType">The type of the new segment.  It will be a CPSegment object with generic arguments.</typeparam>
        /// <param name="db">The IDBAccess object to associate with the segment.</param>
        /// <param name="tables">The number of tables this segment returns.</param>
        /// <param name="newSegment">The new segment.</param>
        /// <returns>The new CPSegment object with the additional generic types.</returns>
        internal T AppendCrossProviderSetReadSegment<T, NType>(IDBAccess db, int tables, NType newSegment)
            where T : CPSegmentBase, new()
            where NType : CPSegmentBase, new()
        {
            var ret = new T();
            this.CopyTo(ret);

            newSegment.DBAccess = db;
            newSegment.Tables = tables;

            ret.Add(newSegment);

            return ret;
        }

        /// <summary>
        /// Gets a ModelData dictionary containing all types in the cross provider set read.
        /// </summary>
        /// <returns>The ModelData dictionary.</returns>
        internal Dictionary<Type, ModelData> GetAllModelDataDictionaries()
        {
            var allModelDataKVP = this.GetProviders().SelectMany(p => p.Provider.ModelsData).ToList();
            var allModelDataKeys = allModelDataKVP.Select(kvp => kvp.Key).Distinct().ToList();
            return allModelDataKeys.ToDictionary(t => t, t => allModelDataKVP.First(kvp => kvp.Key == t).Value);
        }
    }

    /// <summary>
    /// A class representing a cross provider set read operation that is not strongly typed.
    /// </summary>
    public sealed class CPSegment : CPSegmentBase
    {
        /// <summary>
        /// Instatiates a new CPSegment type using the given IDBAccess object.  This will have no defined types, they will all be runtime types.
        /// </summary>
        /// <param name="db">The IDBAccess object associated with this segment.</param>
        internal CPSegment(IDBAccess db) : this(db, null)
        { }

        /// <summary>
        /// Instantiates a new CPSegment type.
        /// </summary>
        /// <param name="db">The IDBAcess object associated with this segment.</param>
        /// <param name="types">The known types.</param>
        internal CPSegment(IDBAccess db, Type [] types) : this(db, types ?? new Type[] { }, types == null ? -1 : types.Length)
        { }

        /// <summary>
        /// Instantiates a new CPSegment type.
        /// </summary>
        /// <param name="db">The IDBAccess object associated with this segment.</param>
        /// <param name="types">The known types.</param>
        /// <param name="tables">The number of tables.</param>
        private CPSegment(IDBAccess db, Type [] types, int tables)
        {
            this.DBAccess = db;
            this.Types = types;
            this.Tables = tables;
        }

        internal Type[] Types { get; set; }

        /// <summary>
        /// Copies this segment into a new segment.  Used to effectively convert one CPSegment type to another.
        /// </summary>
        /// <param name="dest"></param>
        internal void CopyTo(CPSegment dest)
        {
            dest.DBAccess = this.DBAccess;
            dest.Tables = this.Tables;
            dest.Types = this.Types;

            dest.AddAllChildren(this);
        }

        /// <summary>
        /// Gets all types represented by the cross provider set read.
        /// </summary>
        /// <returns>The list of types.</returns>
        internal List<Type> GetAllTypes()
        {
            //If the first type does not have any, then all types are runtime types.  Return an empty list.
            if (!this.Types.Any())
                return new List<Type>();

            var types = new List<Type>(this.GetTotalTableCount());

            var tmp = this;

            while (tmp != null)
            {
                types.AddRange(tmp.Types);
                tmp = tmp.Next as CPSegment;
            }

            return types;
        }

        /// <summary>
        /// Appends a cross provider segment of runtime types to this segment.
        /// </summary>
        /// <param name="db">The IDBAccess object associated with the segment.</param>
        internal void AppendCrossProviderSetReadSegment(IDBAccess db)
        {
            if (this.Types.Any())
                throw new ArgumentException("Cannot append runtime types to known times in a cross provider set read.");
            this.Add(new CPSegment(db));
        }

        /// <summary>
        /// Appends a cross provider segment of an abitrary number of types to this segment.
        /// </summary>
        /// <param name="db">The IDBAccess object associated with the segment.</param>
        /// <param name="types">The known types.</param>
        internal void AppendCrossProviderSetReadSegment(IDBAccess db, Type[] types)
        {
            if (!this.Types.Any())
                throw new ArgumentException("Cannot append known types to runtime types in a cross provider set read.");
            this.Add(new CPSegment(db, types));
        }
    }

    /// <summary>
    /// A class representing a cross provider set read operation with 1 result set.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    public sealed class CPSegment<T1> : CPSegmentBase
        where T1 : class, new()
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 2 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    public sealed class CPSegment<T1, T2> : CPSegmentBase
        where T1 : class, new()
        where T2 : class, new()
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 3 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3> : CPSegmentBase
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 4 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4> : CPSegmentBase
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 5 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5> : CPSegmentBase
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 6 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6> : CPSegmentBase
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 7 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    /// <typeparam name="T7">The type of the seventh data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6, T7> : CPSegmentBase
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 8 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    /// <typeparam name="T7">The type of the seventh data table.</typeparam>
    /// <typeparam name="T8">The type of the eigth data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> : CPSegmentBase
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 9 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    /// <typeparam name="T7">The type of the seventh data table.</typeparam>
    /// <typeparam name="T8">The type of the eigth data table.</typeparam>
    /// <typeparam name="T9">The type of the ninth data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> : CPSegmentBase
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new()
        where T5 : class, new()
        where T6 : class, new()
        where T7 : class, new()
        where T8 : class, new()
        where T9 : class, new()
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 10 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    /// <typeparam name="T7">The type of the seventh data table.</typeparam>
    /// <typeparam name="T8">The type of the eigth data table.</typeparam>
    /// <typeparam name="T9">The type of the ninth data table.</typeparam>
    /// <typeparam name="T10">The type of the 10th data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : CPSegmentBase
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
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 11 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    /// <typeparam name="T7">The type of the seventh data table.</typeparam>
    /// <typeparam name="T8">The type of the eigth data table.</typeparam>
    /// <typeparam name="T9">The type of the ninth data table.</typeparam>
    /// <typeparam name="T10">The type of the 10th data table.</typeparam>
    /// <typeparam name="T11">The type of the 11th data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : CPSegmentBase
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
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 12 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    /// <typeparam name="T7">The type of the seventh data table.</typeparam>
    /// <typeparam name="T8">The type of the eigth data table.</typeparam>
    /// <typeparam name="T9">The type of the ninth data table.</typeparam>
    /// <typeparam name="T10">The type of the 10th data table.</typeparam>
    /// <typeparam name="T11">The type of the 11th data table.</typeparam>
    /// <typeparam name="T12">The type of the 12th data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : CPSegmentBase
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
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 13 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    /// <typeparam name="T7">The type of the seventh data table.</typeparam>
    /// <typeparam name="T8">The type of the eigth data table.</typeparam>
    /// <typeparam name="T9">The type of the ninth data table.</typeparam>
    /// <typeparam name="T10">The type of the 10th data table.</typeparam>
    /// <typeparam name="T11">The type of the 11th data table.</typeparam>
    /// <typeparam name="T12">The type of the 12th data table.</typeparam>
    /// <typeparam name="T13">The type of the 13th data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : CPSegmentBase
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
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 14 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    /// <typeparam name="T7">The type of the seventh data table.</typeparam>
    /// <typeparam name="T8">The type of the eigth data table.</typeparam>
    /// <typeparam name="T9">The type of the ninth data table.</typeparam>
    /// <typeparam name="T10">The type of the 10th data table.</typeparam>
    /// <typeparam name="T11">The type of the 11th data table.</typeparam>
    /// <typeparam name="T12">The type of the 12th data table.</typeparam>
    /// <typeparam name="T13">The type of the 13th data table.</typeparam>
    /// <typeparam name="T14">The type of the 14th data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : CPSegmentBase
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
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 15 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    /// <typeparam name="T7">The type of the seventh data table.</typeparam>
    /// <typeparam name="T8">The type of the eigth data table.</typeparam>
    /// <typeparam name="T9">The type of the ninth data table.</typeparam>
    /// <typeparam name="T10">The type of the 10th data table.</typeparam>
    /// <typeparam name="T11">The type of the 11th data table.</typeparam>
    /// <typeparam name="T12">The type of the 12th data table.</typeparam>
    /// <typeparam name="T13">The type of the 13th data table.</typeparam>
    /// <typeparam name="T14">The type of the 14th data table.</typeparam>
    /// <typeparam name="T15">The type of the 15th data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : CPSegmentBase
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
    { }

    /// <summary>
    /// A class representing a cross provider set read operation with 16 result sets.
    /// </summary>
    /// <typeparam name="T1">The type of the first data table.</typeparam>
    /// <typeparam name="T2">The type of the second data table.</typeparam>
    /// <typeparam name="T3">The type of the third data table.</typeparam>
    /// <typeparam name="T4">The type of the fourth data table.</typeparam>
    /// <typeparam name="T5">The type of the fifth data table.</typeparam>
    /// <typeparam name="T6">The type of the sixth data table.</typeparam>
    /// <typeparam name="T7">The type of the seventh data table.</typeparam>
    /// <typeparam name="T8">The type of the eigth data table.</typeparam>
    /// <typeparam name="T9">The type of the ninth data table.</typeparam>
    /// <typeparam name="T10">The type of the 10th data table.</typeparam>
    /// <typeparam name="T11">The type of the 11th data table.</typeparam>
    /// <typeparam name="T12">The type of the 12th data table.</typeparam>
    /// <typeparam name="T13">The type of the 13th data table.</typeparam>
    /// <typeparam name="T14">The type of the 14th data table.</typeparam>
    /// <typeparam name="T15">The type of the 15th data table.</typeparam>
    /// <typeparam name="T16">The type of the 16th data table.</typeparam>
    public sealed class CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : CPSegmentBase
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
    { }
    #endregion

    #region GenericTypeTuple
    #region GenericTypeTuple Static Helpers
    public static class GenericTypeTuple
    {
        static GenericTypeTuple()
        {
            GenericTypeTuple.Objects1 = new CPSegment<Object>();
            GenericTypeTuple.Objects2 = new CPSegment<Object, Object>();
            GenericTypeTuple.Objects3 = new CPSegment<Object, Object, Object>();
            GenericTypeTuple.Objects4 = new CPSegment<Object, Object, Object, Object>();
            GenericTypeTuple.Objects5 = new CPSegment<Object, Object, Object, Object, Object>();
            GenericTypeTuple.Objects6 = new CPSegment<Object, Object, Object, Object, Object, Object>();
            GenericTypeTuple.Objects7 = new CPSegment<Object, Object, Object, Object, Object, Object, Object>();
            GenericTypeTuple.Objects8 = new CPSegment<Object, Object, Object, Object, Object, Object, Object, Object>();
            GenericTypeTuple.Objects9 = new CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object>();
            GenericTypeTuple.Objects10 = new CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>();
            GenericTypeTuple.Objects11 = new CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>();
            GenericTypeTuple.Objects12 = new CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>();
            GenericTypeTuple.Objects13 = new CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>();
            GenericTypeTuple.Objects14 = new CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>();
            GenericTypeTuple.Objects15 = new CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>();
        }

        /// <summary>
        /// An instance of a GenericTupleType of 1 object.
        /// </summary>
        public static CPSegment<Object> Objects1 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 2 objects.
        /// </summary>
        public static CPSegment<Object, Object> Objects2 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 3 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object> Objects3 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 4 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object> Objects4 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 5 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object> Objects5 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 6 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object, Object> Objects6 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 7 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object, Object, Object> Objects7 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 8 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object, Object, Object, Object> Objects8 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 9 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object> Objects9 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 10 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object> Objects10 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 11 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object> Objects11 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 12 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object> Objects12 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 13 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object> Objects13 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 14 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object> Objects14 { get; set; }

        /// <summary>
        /// An instance of a GenericTupleType of 15 objects.
        /// </summary>
        public static CPSegment<Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object> Objects15 { get; set; }
    }
    #endregion
    #endregion
}