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
    public static partial class IDBAccessExtensions
    {
        #region ExecuteSetRead
        #region ModelBase populate helpers
        /// <summary>
        /// Generates model enumerations for a return set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2> GenerateModelEnumerations<T1, T2>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
            where T1 : class, new()
            where T2 : class, new()
        {
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null };
            return new DALTuple<T1, T2>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1]),
                DBAccess = db.ToList(2)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3> GenerateModelEnumerations<T1, T2, T3>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null };
            return new DALTuple<T1, T2, T3>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2]),
                DBAccess = db.ToList(3)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4> GenerateModelEnumerations<T1, T2, T3, T4>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null };
            return new DALTuple<T1, T2, T3, T4>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3]),
                DBAccess = db.ToList(4)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5> GenerateModelEnumerations<T1, T2, T3, T4, T5>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4]),
                DBAccess = db.ToList(5)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5]),
                DBAccess = db.ToList(6)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6, T7> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6, T7>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5], parentChildPropertyNames[5]),
                Table7 = db.PopulateModelBaseEnumeration<T7>(tables[6]),
                DBAccess = db.ToList(7)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5], parentChildPropertyNames[5]),
                Table7 = db.PopulateModelBaseEnumeration<T7>(tables[6], parentChildPropertyNames[6]),
                Table8 = db.PopulateModelBaseEnumeration<T8>(tables[7]),
                DBAccess = db.ToList(8)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
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
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
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
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5], parentChildPropertyNames[5]),
                Table7 = db.PopulateModelBaseEnumeration<T7>(tables[6], parentChildPropertyNames[6]),
                Table8 = db.PopulateModelBaseEnumeration<T8>(tables[7], parentChildPropertyNames[7]),
                Table9 = db.PopulateModelBaseEnumeration<T9>(tables[8]),
                DBAccess = db.ToList(9)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
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
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
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
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5], parentChildPropertyNames[5]),
                Table7 = db.PopulateModelBaseEnumeration<T7>(tables[6], parentChildPropertyNames[6]),
                Table8 = db.PopulateModelBaseEnumeration<T8>(tables[7], parentChildPropertyNames[7]),
                Table9 = db.PopulateModelBaseEnumeration<T9>(tables[8], parentChildPropertyNames[8]),
                Table10 = db.PopulateModelBaseEnumeration<T10>(tables[9]),
                DBAccess = db.ToList(10)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
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
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
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
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5], parentChildPropertyNames[5]),
                Table7 = db.PopulateModelBaseEnumeration<T7>(tables[6], parentChildPropertyNames[6]),
                Table8 = db.PopulateModelBaseEnumeration<T8>(tables[7], parentChildPropertyNames[7]),
                Table9 = db.PopulateModelBaseEnumeration<T9>(tables[8], parentChildPropertyNames[8]),
                Table10 = db.PopulateModelBaseEnumeration<T10>(tables[9], parentChildPropertyNames[9]),
                Table11 = db.PopulateModelBaseEnumeration<T11>(tables[10]),
                DBAccess = db.ToList(11)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
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
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
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
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null, null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5], parentChildPropertyNames[5]),
                Table7 = db.PopulateModelBaseEnumeration<T7>(tables[6], parentChildPropertyNames[6]),
                Table8 = db.PopulateModelBaseEnumeration<T8>(tables[7], parentChildPropertyNames[7]),
                Table9 = db.PopulateModelBaseEnumeration<T9>(tables[8], parentChildPropertyNames[8]),
                Table10 = db.PopulateModelBaseEnumeration<T10>(tables[9], parentChildPropertyNames[9]),
                Table11 = db.PopulateModelBaseEnumeration<T11>(tables[10], parentChildPropertyNames[10]),
                Table12 = db.PopulateModelBaseEnumeration<T12>(tables[11]),
                DBAccess = db.ToList(12)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
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
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
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
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null, null, null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5], parentChildPropertyNames[5]),
                Table7 = db.PopulateModelBaseEnumeration<T7>(tables[6], parentChildPropertyNames[6]),
                Table8 = db.PopulateModelBaseEnumeration<T8>(tables[7], parentChildPropertyNames[7]),
                Table9 = db.PopulateModelBaseEnumeration<T9>(tables[8], parentChildPropertyNames[8]),
                Table10 = db.PopulateModelBaseEnumeration<T10>(tables[9], parentChildPropertyNames[9]),
                Table11 = db.PopulateModelBaseEnumeration<T11>(tables[10], parentChildPropertyNames[10]),
                Table12 = db.PopulateModelBaseEnumeration<T12>(tables[11], parentChildPropertyNames[11]),
                Table13 = db.PopulateModelBaseEnumeration<T13>(tables[12]),
                DBAccess = db.ToList(13)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
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
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
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
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5], parentChildPropertyNames[5]),
                Table7 = db.PopulateModelBaseEnumeration<T7>(tables[6], parentChildPropertyNames[6]),
                Table8 = db.PopulateModelBaseEnumeration<T8>(tables[7], parentChildPropertyNames[7]),
                Table9 = db.PopulateModelBaseEnumeration<T9>(tables[8], parentChildPropertyNames[8]),
                Table10 = db.PopulateModelBaseEnumeration<T10>(tables[9], parentChildPropertyNames[9]),
                Table11 = db.PopulateModelBaseEnumeration<T11>(tables[10], parentChildPropertyNames[10]),
                Table12 = db.PopulateModelBaseEnumeration<T12>(tables[11], parentChildPropertyNames[11]),
                Table13 = db.PopulateModelBaseEnumeration<T13>(tables[12], parentChildPropertyNames[12]),
                Table14 = db.PopulateModelBaseEnumeration<T14>(tables[13]),
                DBAccess = db.ToList(14)
            };
        }

        /// <summary>
        /// Generates model enumerations for a return set.
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
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
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
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5], parentChildPropertyNames[5]),
                Table7 = db.PopulateModelBaseEnumeration<T7>(tables[6], parentChildPropertyNames[6]),
                Table8 = db.PopulateModelBaseEnumeration<T8>(tables[7], parentChildPropertyNames[7]),
                Table9 = db.PopulateModelBaseEnumeration<T9>(tables[8], parentChildPropertyNames[8]),
                Table10 = db.PopulateModelBaseEnumeration<T10>(tables[9], parentChildPropertyNames[9]),
                Table11 = db.PopulateModelBaseEnumeration<T11>(tables[10], parentChildPropertyNames[10]),
                Table12 = db.PopulateModelBaseEnumeration<T12>(tables[11], parentChildPropertyNames[11]),
                Table13 = db.PopulateModelBaseEnumeration<T13>(tables[12], parentChildPropertyNames[12]),
                Table14 = db.PopulateModelBaseEnumeration<T14>(tables[13], parentChildPropertyNames[13]),
                Table15 = db.PopulateModelBaseEnumeration<T15>(tables[14]),
                DBAccess = db.ToList(15)
            };
        }
        
        /// <summary>
        /// Generates model enumerations for a return set.
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
        /// <param name="db"></param>
        /// <param name="tables">The tables.</param>
        /// <param name="parentChildPropertyNames">Optional.  The parent property names for a related set.</param>
        /// <returns>The tuple object.</returns>
        internal static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IDBAccess db, List<ExecuteReadQuickTuple> tables, List<List<String>> parentChildPropertyNames = null)
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
            if (parentChildPropertyNames == null) parentChildPropertyNames = new List<List<String>> { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
            {
                Table1 = db.PopulateModelBaseEnumeration<T1>(tables[0], parentChildPropertyNames[0]),
                Table2 = db.PopulateModelBaseEnumeration<T2>(tables[1], parentChildPropertyNames[1]),
                Table3 = db.PopulateModelBaseEnumeration<T3>(tables[2], parentChildPropertyNames[2]),
                Table4 = db.PopulateModelBaseEnumeration<T4>(tables[3], parentChildPropertyNames[3]),
                Table5 = db.PopulateModelBaseEnumeration<T5>(tables[4], parentChildPropertyNames[4]),
                Table6 = db.PopulateModelBaseEnumeration<T6>(tables[5], parentChildPropertyNames[5]),
                Table7 = db.PopulateModelBaseEnumeration<T7>(tables[6], parentChildPropertyNames[6]),
                Table8 = db.PopulateModelBaseEnumeration<T8>(tables[7], parentChildPropertyNames[7]),
                Table9 = db.PopulateModelBaseEnumeration<T9>(tables[8], parentChildPropertyNames[8]),
                Table10 = db.PopulateModelBaseEnumeration<T10>(tables[9], parentChildPropertyNames[9]),
                Table11 = db.PopulateModelBaseEnumeration<T11>(tables[10], parentChildPropertyNames[10]),
                Table12 = db.PopulateModelBaseEnumeration<T12>(tables[11], parentChildPropertyNames[11]),
                Table13 = db.PopulateModelBaseEnumeration<T13>(tables[12], parentChildPropertyNames[12]),
                Table14 = db.PopulateModelBaseEnumeration<T14>(tables[13], parentChildPropertyNames[13]),
                Table15 = db.PopulateModelBaseEnumeration<T15>(tables[14], parentChildPropertyNames[14]),
                Table16 = db.PopulateModelBaseEnumeration<T16>(tables[15]),
                DBAccess = db.ToList(16)
            };
        }

        #endregion
        #region Query Functions
        #region IModel
        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2> ExecuteSetRead<T1, T2>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3> ExecuteSetRead<T1, T2, T3>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4> ExecuteSetRead<T1, T2, T3, T4>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5> ExecuteSetRead<T1, T2, T3, T4, T5>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6> ExecuteSetRead<T1, T2, T3, T4, T5, T6>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7> ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8> ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
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
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IDBAccess db)
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
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
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
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IDBAccess db)
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
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
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
        /// <param name="queryString">The stored procedure name or query string to execute.</param>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IDBAccess db)
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
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
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
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IDBAccess db)
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
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
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
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IDBAccess db)
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
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
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
        /// <param name="queryString">The stored procedure name or query string to execute.</param>
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IDBAccess db)
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
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
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
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IDBAccess db)
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
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
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
        /// <returns>A DALTuple object containing the return tables.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IDBAccess db)
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
            var tables = db.ExecuteSetReadQuick();
            return db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(tables);
        }

        /// <summary>
        /// Executes a read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <param name="returnTypes">An enumeration of the types returned by the ExecuteSetRead command.</param>
        /// <returns>A List&lt;List&lt;Object&gt;&gt;. Call .OfType on each list in order to cast it back to the correct model type.</returns>
        public static List<List<Object>> ExecuteSetRead(this IDBAccess db, IEnumerable<Type> returnTypes)
        {
            var tables = db.ExecuteSetReadQuick();
            return tables.Select((t, i) => db.PopulateModelBaseEnumeration(t, returnTypes.Skip(i).First())).ToList();
        }
        #endregion
        #region IModelAsync
        private static void ExecuteSetReadCallbackGeneric<T1, T2>(this IDBAccess db, DALTuple<T1, T2> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2> callback)
            where T1 : class, new()
            where T2 : class, new()
        {
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3>(this IDBAccess db, DALTuple<T1, T2, T3> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3> callback)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4>(this IDBAccess db, DALTuple<T1, T2, T3, T4> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4> callback)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5> callback)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6> callback)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6, T7> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7> callback)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6, T7, T8> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8> callback)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
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
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
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
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback)
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
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback)
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
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback)
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
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
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
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback)
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
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IDBAccess db, DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> tables, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback)
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
            if (callback != null)
            {
                callback(tables);
            }
        }

        private static void ExecuteSetReadObjectsCallback(this IDBAccess db, List<List<Object>> tuples, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadObjectsCallbackDelegate callback)
        {
            if (callback != null)
                callback(tuples);
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 2 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2> callback = null)
            where T1 : class, new()
            where T2 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2>();
                db.ExecuteSetReadCallbackGeneric<T1, T2>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 3 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 4 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 5 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 6 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 7 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6, T7>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 8 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 9 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback = null)
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
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 10 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback = null)
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
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 11 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback = null)
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
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 12 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback = null)
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
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 13 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback = null)
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
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 14 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback = null)
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
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
        /// <typeparam name="T15">The type representing the 15th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of 15 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback = null)
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
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eigth return data set.</typeparam>
        /// <typeparam name="T9">The type representing the ninth return data set.</typeparam>
        /// <typeparam name="T10">The type representing the 10th return data set.</typeparam>
        /// <typeparam name="T11">The type representing the 11th return data set.</typeparam>
        /// <typeparam name="T12">The type representing the 12th return data set.</typeparam>
        /// <typeparam name="T13">The type representing the 13th return data set.</typeparam>
        /// <typeparam name="T14">The type representing the 14th return data set.</typeparam>
        /// <typeparam name="T15">The type representing the 15th return data set.</typeparam>
        /// <typeparam name="T16">The type representing the 16th return data set.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of a 16 type DALTuple.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback = null)
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
            new Task(() =>
            {
                var sets = db.ExecuteSetRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
                db.ExecuteSetReadCallbackGeneric<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(sets, callback);
            }).Start();
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database which results in multiple sets being returned.
        /// </summary>
        /// <param name="returnTypes">An enumeration of the types returned by the ExecuteSetRead command.</param>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type List&lt;List&lt;Object&gt;&gt;.  If this is null, no callback will be made.</param>
        public static void ExecuteSetReadAsync(this IDBAccess db, IEnumerable<Type> returnTypes, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadObjectsCallbackDelegate callback = null)
        {
            new Task(() =>
            {
                var tables = db.ExecuteSetReadQuick();
                db.ExecuteSetReadObjectsCallback(tables.Select((t, i) => db.PopulateModelBaseEnumeration(t, returnTypes.Skip(i).First())).ToList(), callback);
            }).Start();
        }

        #endregion
        #endregion
        #endregion
    }
}