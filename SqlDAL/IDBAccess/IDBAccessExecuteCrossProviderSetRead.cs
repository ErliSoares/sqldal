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
using System.Threading.Tasks;
using System.Data.DBAccess.Generic.Exceptions;

namespace System.Data.DBAccess.Generic
{
    public static partial class IDBAccessExtensions
    {
        #region BeginCrossProviderRead
        /// <summary>
        /// Begins constructing a cross provider set read with an arbitary number of return types.  These types will all be runtime types.
        /// </summary>
        /// <param name="db"></param>
        /// <returns>The initial CPSegment object.</returns>
        public static CPSegment BeginCrossProviderRead(this IDBAccess db)
        {
            return new CPSegment(db);
        }

        /// <summary>
        /// Beings constructing a cross provider set read with an arbitray number of return types.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="types">The types representing the resulting data tables.</param>
        /// <returns>The initial CPSegment object.</returns>
        public static CPSegment BeginCrossProviderRead(this IDBAccess db, IEnumerable<Type> types)
        {
            return new CPSegment(db, types.ToArray());
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1> BeginCrossProviderRead<T1>(this IDBAccess db)
        where T1 : class, new()
        {
            return new CPSegment<T1>
            {
                DBAccess = db,
                Tables = 1
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2> BeginCrossProviderRead<T1, T2>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
        {
            return new CPSegment<T1, T2>
            {
                DBAccess = db,
                Tables = 2
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3> BeginCrossProviderRead<T1, T2, T3>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return new CPSegment<T1, T2, T3>
            {
                DBAccess = db,
                Tables = 3
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4> BeginCrossProviderRead<T1, T2, T3, T4>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return new CPSegment<T1, T2, T3, T4>
            {
                DBAccess = db,
                Tables = 4
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5> BeginCrossProviderRead<T1, T2, T3, T4, T5>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            return new CPSegment<T1, T2, T3, T4, T5>
            {
                DBAccess = db,
                Tables = 5
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            return new CPSegment<T1, T2, T3, T4, T5, T6>
            {
                DBAccess = db,
                Tables = 6
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6, T7>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            return new CPSegment<T1, T2, T3, T4, T5, T6, T7>
            {
                DBAccess = db,
                Tables = 7
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6, T7, T8>(this IDBAccess db)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            return new CPSegment<T1, T2, T3, T4, T5, T6, T7, T8>
            {
                DBAccess = db,
                Tables = 8
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
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
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IDBAccess db)
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
            return new CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9>
            {
                DBAccess = db,
                Tables = 9
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
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
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IDBAccess db)
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
            return new CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
            {
                DBAccess = db,
                Tables = 10
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
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
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IDBAccess db)
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
            return new CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
            {
                DBAccess = db,
                Tables = 11
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
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
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IDBAccess db)
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
            return new CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
            {
                DBAccess = db,
                Tables = 12
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
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
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IDBAccess db)
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
            return new CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
            {
                DBAccess = db,
                Tables = 13
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
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
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IDBAccess db)
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
            return new CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
            {
                DBAccess = db,
                Tables = 14
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
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
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IDBAccess db)
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
            return new CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
            {
                DBAccess = db,
                Tables = 15
            };
        }

        /// <summary>
        /// Begins constructing a cross provider set read.
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
        /// <param name="db">The IDBAccess object which reads the first data table.</param>
        /// <returns>A CrossProviderSetRead definition object.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> BeginCrossProviderRead<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IDBAccess db)
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
            return new CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
            {
                DBAccess = db,
                Tables = 16
            };
        }
        #endregion

        #region JoinWith
        #region Weakly Typed
        /// <summary>
        /// Appends an arbitrary number of data tables to the existing queries.  These data tables automatically are mapped to runtime types.
        /// </summary>
        /// <param name="cps"></param>
        /// <param name="db">The IDBAccess object associated with the segment.</param>
        /// <returns>The CPSegment object.</returns>
        public static CPSegment JoinWith(this CPSegment cps, IDBAccess db)
        {
            cps.AppendCrossProviderSetReadSegment(db);
            return cps;
        }

        /// <summary>
        /// Appends an arbitrary number of data tables to the existing queries.
        /// </summary>
        /// <param name="cps"></param>
        /// <param name="db">The IDBAccess object associated with the segment.</param>
        /// <param name="types">The types representing the resulting data tables.</param>
        /// <returns>The CPSegment object.</returns>
        public static CPSegment JoinWith(this CPSegment cps, IDBAccess db, IEnumerable<Type> types)
        {
            cps.AppendCrossProviderSetReadSegment(db, types.ToArray());
            return cps;
        }
        #endregion

        #region 1
        /// <summary>
        /// Appends a single data table to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2> JoinWith<T1, T2>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2> segment)
            where T1 : class, new()
            where T2 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2>, CPSegment<T2>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3> JoinWith<T1, T2, T3>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3>, CPSegment<T2, T3>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4> JoinWith<T1, T2, T3, T4>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4>, CPSegment<T2, T3, T4>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5> JoinWith<T1, T2, T3, T4, T5>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5>, CPSegment<T2, T3, T4, T5>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6> JoinWith<T1, T2, T3, T4, T5, T6>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6>, CPSegment<T2, T3, T4, T5, T6>>(db, 5, segment);
        }

        /// <summary>
        /// Appends 6 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7> JoinWith<T1, T2, T3, T4, T5, T6, T7>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6, T7> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7>, CPSegment<T2, T3, T4, T5, T6, T7>>(db, 6, segment);
        }

        /// <summary>
        /// Appends 7 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6, T7, T8> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8>, CPSegment<T2, T3, T4, T5, T6, T7, T8>>(db, 7, segment);
        }

        /// <summary>
        /// Appends 8 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9>, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9>>(db, 8, segment);
        }

        /// <summary>
        /// Appends 9 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10>>(db, 9, segment);
        }

        /// <summary>
        /// Appends 10 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(db, 10, segment);
        }

        /// <summary>
        /// Appends 11 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(db, 11, segment);
        }

        /// <summary>
        /// Appends 12 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(db, 12, segment);
        }

        /// <summary>
        /// Appends 13 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(db, 13, segment);
        }

        /// <summary>
        /// Appends 14 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(db, 14, segment);
        }

        /// <summary>
        /// Appends 15 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1> cps, IDBAccess db, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(db, 15, segment);
        }
        #endregion

        #region 2
        /// <summary>
        /// Appends a single data table to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3> JoinWith<T1, T2, T3>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3>, CPSegment<T3>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4> JoinWith<T1, T2, T3, T4>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4>, CPSegment<T3, T4>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5> JoinWith<T1, T2, T3, T4, T5>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5>, CPSegment<T3, T4, T5>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6> JoinWith<T1, T2, T3, T4, T5, T6>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6>, CPSegment<T3, T4, T5, T6>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7> JoinWith<T1, T2, T3, T4, T5, T6, T7>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6, T7> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7>, CPSegment<T3, T4, T5, T6, T7>>(db, 5, segment);
        }

        /// <summary>
        /// Appends 6 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6, T7, T8> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8>, CPSegment<T3, T4, T5, T6, T7, T8>>(db, 6, segment);
        }

        /// <summary>
        /// Appends 7 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6, T7, T8, T9> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9>, CPSegment<T3, T4, T5, T6, T7, T8, T9>>(db, 7, segment);
        }

        /// <summary>
        /// Appends 8 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10>>(db, 8, segment);
        }

        /// <summary>
        /// Appends 9 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11>>(db, 9, segment);
        }

        /// <summary>
        /// Appends 10 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(db, 10, segment);
        }

        /// <summary>
        /// Appends 11 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(db, 11, segment);
        }

        /// <summary>
        /// Appends 12 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(db, 12, segment);
        }

        /// <summary>
        /// Appends 13 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(db, 13, segment);
        }

        /// <summary>
        /// Appends 14 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2> cps, IDBAccess db, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(db, 14, segment);
        }
        #endregion

        #region 3
        /// <summary>
        /// Appends a single data table to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4> JoinWith<T1, T2, T3, T4>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4>, CPSegment<T4>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5> JoinWith<T1, T2, T3, T4, T5>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5>, CPSegment<T4, T5>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6> JoinWith<T1, T2, T3, T4, T5, T6>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6>, CPSegment<T4, T5, T6>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7> JoinWith<T1, T2, T3, T4, T5, T6, T7>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6, T7> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7>, CPSegment<T4, T5, T6, T7>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6, T7, T8> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8>, CPSegment<T4, T5, T6, T7, T8>>(db, 5, segment);
        }

        /// <summary>
        /// Appends 6 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6, T7, T8, T9> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9>, CPSegment<T4, T5, T6, T7, T8, T9>>(db, 6, segment);
        }

        /// <summary>
        /// Appends 7 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6, T7, T8, T9, T10> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, CPSegment<T4, T5, T6, T7, T8, T9, T10>>(db, 7, segment);
        }

        /// <summary>
        /// Appends 8 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11>>(db, 8, segment);
        }

        /// <summary>
        /// Appends 9 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11, T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11, T12>>(db, 9, segment);
        }

        /// <summary>
        /// Appends 10 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(db, 10, segment);
        }

        /// <summary>
        /// Appends 11 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(db, 11, segment);
        }

        /// <summary>
        /// Appends 12 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(db, 12, segment);
        }

        /// <summary>
        /// Appends 13 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3> cps, IDBAccess db, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(db, 13, segment);
        }
        #endregion

        #region 4
        /// <summary>
        /// Appends a single data table to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5> JoinWith<T1, T2, T3, T4, T5>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5>, CPSegment<T5>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6> JoinWith<T1, T2, T3, T4, T5, T6>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6>, CPSegment<T5, T6>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7> JoinWith<T1, T2, T3, T4, T5, T6, T7>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6, T7> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7>, CPSegment<T5, T6, T7>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6, T7, T8> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8>, CPSegment<T5, T6, T7, T8>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6, T7, T8, T9> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9>, CPSegment<T5, T6, T7, T8, T9>>(db, 5, segment);
        }

        /// <summary>
        /// Appends 6 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6, T7, T8, T9, T10> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, CPSegment<T5, T6, T7, T8, T9, T10>>(db, 6, segment);
        }

        /// <summary>
        /// Appends 7 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6, T7, T8, T9, T10, T11> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, CPSegment<T5, T6, T7, T8, T9, T10, T11>>(db, 7, segment);
        }

        /// <summary>
        /// Appends 8 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6, T7, T8, T9, T10, T11, T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T5, T6, T7, T8, T9, T10, T11, T12>>(db, 8, segment);
        }

        /// <summary>
        /// Appends 9 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6, T7, T8, T9, T10, T11, T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T5, T6, T7, T8, T9, T10, T11, T12, T13>>(db, 9, segment);
        }

        /// <summary>
        /// Appends 10 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(db, 10, segment);
        }

        /// <summary>
        /// Appends 11 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(db, 11, segment);
        }

        /// <summary>
        /// Appends 12 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4> cps, IDBAccess db, CPSegment<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(db, 12, segment);
        }
        #endregion

        #region 5
        /// <summary>
        /// Appends a single data table to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6> JoinWith<T1, T2, T3, T4, T5, T6>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6>, CPSegment<T6>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7> JoinWith<T1, T2, T3, T4, T5, T6, T7>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6, T7> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7>, CPSegment<T6, T7>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6, T7, T8> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8>, CPSegment<T6, T7, T8>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6, T7, T8, T9> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9>, CPSegment<T6, T7, T8, T9>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6, T7, T8, T9, T10> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, CPSegment<T6, T7, T8, T9, T10>>(db, 5, segment);
        }

        /// <summary>
        /// Appends 6 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6, T7, T8, T9, T10, T11> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, CPSegment<T6, T7, T8, T9, T10, T11>>(db, 6, segment);
        }

        /// <summary>
        /// Appends 7 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6, T7, T8, T9, T10, T11, T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T6, T7, T8, T9, T10, T11, T12>>(db, 7, segment);
        }

        /// <summary>
        /// Appends 8 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6, T7, T8, T9, T10, T11, T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T6, T7, T8, T9, T10, T11, T12, T13>>(db, 8, segment);
        }

        /// <summary>
        /// Appends 9 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6, T7, T8, T9, T10, T11, T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T6, T7, T8, T9, T10, T11, T12, T13, T14>>(db, 9, segment);
        }

        /// <summary>
        /// Appends 10 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(db, 10, segment);
        }

        /// <summary>
        /// Appends 11 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5> cps, IDBAccess db, CPSegment<T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(db, 11, segment);
        }
        #endregion

        #region 6
        /// <summary>
        /// Appends a single data table to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7> JoinWith<T1, T2, T3, T4, T5, T6, T7>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, IDBAccess db, CPSegment<T7> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7>, CPSegment<T7>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, IDBAccess db, CPSegment<T7, T8> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8>, CPSegment<T7, T8>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, IDBAccess db, CPSegment<T7, T8, T9> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9>, CPSegment<T7, T8, T9>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, IDBAccess db, CPSegment<T7, T8, T9, T10> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, CPSegment<T7, T8, T9, T10>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, IDBAccess db, CPSegment<T7, T8, T9, T10, T11> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, CPSegment<T7, T8, T9, T10, T11>>(db, 5, segment);
        }

        /// <summary>
        /// Appends 6 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, IDBAccess db, CPSegment<T7, T8, T9, T10, T11, T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T7, T8, T9, T10, T11, T12>>(db, 6, segment);
        }

        /// <summary>
        /// Appends 7 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, IDBAccess db, CPSegment<T7, T8, T9, T10, T11, T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T7, T8, T9, T10, T11, T12, T13>>(db, 7, segment);
        }

        /// <summary>
        /// Appends 8 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, IDBAccess db, CPSegment<T7, T8, T9, T10, T11, T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T7, T8, T9, T10, T11, T12, T13, T14>>(db, 8, segment);
        }

        /// <summary>
        /// Appends 9 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, IDBAccess db, CPSegment<T7, T8, T9, T10, T11, T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T7, T8, T9, T10, T11, T12, T13, T14, T15>>(db, 9, segment);
        }

        /// <summary>
        /// Appends 10 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, IDBAccess db, CPSegment<T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(db, 10, segment);
        }
        #endregion

        #region 7
        /// <summary>
        /// Appends a single data table to the existing queries.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, IDBAccess db, CPSegment<T8> segment)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8>, CPSegment<T8>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, IDBAccess db, CPSegment<T8, T9> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9>, CPSegment<T8, T9>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, IDBAccess db, CPSegment<T8, T9, T10> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, CPSegment<T8, T9, T10>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, IDBAccess db, CPSegment<T8, T9, T10, T11> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, CPSegment<T8, T9, T10, T11>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, IDBAccess db, CPSegment<T8, T9, T10, T11, T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T8, T9, T10, T11, T12>>(db, 5, segment);
        }

        /// <summary>
        /// Appends 6 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, IDBAccess db, CPSegment<T8, T9, T10, T11, T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T8, T9, T10, T11, T12, T13>>(db, 6, segment);
        }

        /// <summary>
        /// Appends 7 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, IDBAccess db, CPSegment<T8, T9, T10, T11, T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T8, T9, T10, T11, T12, T13, T14>>(db, 7, segment);
        }

        /// <summary>
        /// Appends 8 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, IDBAccess db, CPSegment<T8, T9, T10, T11, T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T8, T9, T10, T11, T12, T13, T14, T15>>(db, 8, segment);
        }

        /// <summary>
        /// Appends 9 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, IDBAccess db, CPSegment<T8, T9, T10, T11, T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T8, T9, T10, T11, T12, T13, T14, T15, T16>>(db, 9, segment);
        }
        #endregion

        #region 8
        /// <summary>
        /// Appends a single data table to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, IDBAccess db, CPSegment<T9> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9>, CPSegment<T9>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, IDBAccess db, CPSegment<T9, T10> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, CPSegment<T9, T10>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, IDBAccess db, CPSegment<T9, T10, T11> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, CPSegment<T9, T10, T11>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, IDBAccess db, CPSegment<T9, T10, T11, T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T9, T10, T11, T12>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, IDBAccess db, CPSegment<T9, T10, T11, T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T9, T10, T11, T12, T13>>(db, 5, segment);
        }

        /// <summary>
        /// Appends 6 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, IDBAccess db, CPSegment<T9, T10, T11, T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T9, T10, T11, T12, T13, T14>>(db, 6, segment);
        }

        /// <summary>
        /// Appends 7 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, IDBAccess db, CPSegment<T9, T10, T11, T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T9, T10, T11, T12, T13, T14, T15>>(db, 7, segment);
        }

        /// <summary>
        /// Appends 8 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, IDBAccess db, CPSegment<T9, T10, T11, T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T9, T10, T11, T12, T13, T14, T15, T16>>(db, 8, segment);
        }
        #endregion

        #region 9
        /// <summary>
        /// Appends a single data table to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, IDBAccess db, CPSegment<T10> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, CPSegment<T10>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, IDBAccess db, CPSegment<T10, T11> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, CPSegment<T10, T11>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, IDBAccess db, CPSegment<T10, T11, T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T10, T11, T12>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, IDBAccess db, CPSegment<T10, T11, T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T10, T11, T12, T13>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, IDBAccess db, CPSegment<T10, T11, T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T10, T11, T12, T13, T14>>(db, 5, segment);
        }

        /// <summary>
        /// Appends 6 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, IDBAccess db, CPSegment<T10, T11, T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T10, T11, T12, T13, T14, T15>>(db, 6, segment);
        }

        /// <summary>
        /// Appends 7 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, IDBAccess db, CPSegment<T10, T11, T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T10, T11, T12, T13, T14, T15, T16>>(db, 7, segment);
        }
        #endregion

        #region 10
        /// <summary>
        /// Appends a single data table to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> cps, IDBAccess db, CPSegment<T11> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, CPSegment<T11>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> cps, IDBAccess db, CPSegment<T11, T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T11, T12>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> cps, IDBAccess db, CPSegment<T11, T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T11, T12, T13>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> cps, IDBAccess db, CPSegment<T11, T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T11, T12, T13, T14>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> cps, IDBAccess db, CPSegment<T11, T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T11, T12, T13, T14, T15>>(db, 5, segment);
        }

        /// <summary>
        /// Appends 6 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> cps, IDBAccess db, CPSegment<T11, T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T11, T12, T13, T14, T15, T16>>(db, 6, segment);
        }
        #endregion

        #region 11
        /// <summary>
        /// Appends a single data table to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> cps, IDBAccess db, CPSegment<T12> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, CPSegment<T12>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> cps, IDBAccess db, CPSegment<T12, T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T12, T13>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> cps, IDBAccess db, CPSegment<T12, T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T12, T13, T14>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> cps, IDBAccess db, CPSegment<T12, T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T12, T13, T14, T15>>(db, 4, segment);
        }

        /// <summary>
        /// Appends 5 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> cps, IDBAccess db, CPSegment<T12, T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T12, T13, T14, T15, T16>>(db, 5, segment);
        }
        #endregion

        #region 12
        /// <summary>
        /// Appends a single data table to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> cps, IDBAccess db, CPSegment<T13> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, CPSegment<T13>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> cps, IDBAccess db, CPSegment<T13, T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T13, T14>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> cps, IDBAccess db, CPSegment<T13, T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T13, T14, T15>>(db, 3, segment);
        }

        /// <summary>
        /// Appends 4 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> cps, IDBAccess db, CPSegment<T13, T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T13, T14, T15, T16>>(db, 4, segment);
        }
        #endregion

        #region 13
        /// <summary>
        /// Appends a single data table to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> cps, IDBAccess db, CPSegment<T14> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, CPSegment<T14>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> cps, IDBAccess db, CPSegment<T14, T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T14, T15>>(db, 2, segment);
        }

        /// <summary>
        /// Appends 3 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> cps, IDBAccess db, CPSegment<T14, T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T14, T15, T16>>(db, 3, segment);
        }
        #endregion

        #region 14
        /// <summary>
        /// Appends a single data table to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> cps, IDBAccess db, CPSegment<T15> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, CPSegment<T15>>(db, 1, segment);
        }

        /// <summary>
        /// Appends 2 data tables to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteSetRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data tables.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> cps, IDBAccess db, CPSegment<T15, T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T15, T16>>(db, 2, segment);
        }
        #endregion

        #region 15
        /// <summary>
        /// Appends a single data table to the existing queries.
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
        /// <param name="cps">The existing queries definition object.</param>
        /// <param name="db">The provider object on which to perform an ExecuteRead.</param>
        /// <param name="segment">The segment type to add.</param>
        /// <returns>A cross provider set read definition object which includes the additional data table.</returns>
        public static CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> JoinWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> cps, IDBAccess db, CPSegment<T16> segment)
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
            return cps.AppendCrossProviderSetReadSegment<CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>, CPSegment<T16>>(db, 1, segment);
        }
        #endregion
        #endregion

        #region AsDataSet
        /// <summary>
        /// Returns object lists representing a cross provider set read.
        /// </summary>
        /// <param name="cps"></param>
        /// <returns>The object lists.</returns>
        public static List<List<Object>> AsDataSet(this CPSegment cps)
        {
            return cps.AsDataSet(null);
        }

        /// <summary>
        /// Returns object lists representing a cross provider set read.
        /// </summary>
        /// <param name="cps"></param>
        /// <param name="parentColumns">The optional parent column names.</param>
        /// <returns>The object lists.</returns>
        private static List<List<Object>> AsDataSet(this CPSegment cps, List<List<String>> parentColumns)
        {
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            List<List<Object>> tables = new List<List<Object>>();

            var tmp = cps;
            var iTask = 0;
            var i = 0;
            var types = tmp.GetAllTypes();

            do
            {
                var oldI = i;
                var resultI = 0;
                var tableCount = tmp.Tables;
                if (tableCount == -1)
                    tableCount = tasks[iTask].Result.Count;
                for (; i < tableCount + oldI; i++)
                {
                    tables.Add(tmp.DBAccess.PopulateModelBaseEnumeration(tasks[iTask].Result[resultI++], types.Any() ? types[i] : typeof(Object), parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null));
                }

                iTask++;
                tmp = tmp.Next as CPSegment;
            } while (tmp != null);

            return tables;
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2> AsDataSet<T1, T2>(this CPSegment<T1, T2> cps, List<List<String>> parentColumns = null)
            where T1 : class, new()
            where T2 : class, new()
        {
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2>
            {
                Table1 = table1,
                Table2 = table2,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3> AsDataSet<T1, T2, T3>(this CPSegment<T1, T2, T3> cps, List<List<String>> parentColumns = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4> AsDataSet<T1, T2, T3, T4>(this CPSegment<T1, T2, T3, T4> cps, List<List<String>> parentColumns = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5> AsDataSet<T1, T2, T3, T4, T5>(this CPSegment<T1, T2, T3, T4, T5> cps, List<List<String>> parentColumns = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6> AsDataSet<T1, T2, T3, T4, T5, T6>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, List<List<String>> parentColumns = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7> AsDataSet<T1, T2, T3, T4, T5, T6, T7>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, List<List<String>> parentColumns = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();
            var table7 = new List<T7>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 6: table7 = tmp.DBAccess.PopulateModelBaseEnumeration<T7>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6, T7>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                Table7 = table7,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8> AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, List<List<String>> parentColumns = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();
            var table7 = new List<T7>();
            var table8 = new List<T8>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 6: table7 = tmp.DBAccess.PopulateModelBaseEnumeration<T7>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 7: table8 = tmp.DBAccess.PopulateModelBaseEnumeration<T8>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                Table7 = table7,
                Table8 = table8,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, List<List<String>> parentColumns = null)
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
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();
            var table7 = new List<T7>();
            var table8 = new List<T8>();
            var table9 = new List<T9>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 6: table7 = tmp.DBAccess.PopulateModelBaseEnumeration<T7>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 7: table8 = tmp.DBAccess.PopulateModelBaseEnumeration<T8>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 8: table9 = tmp.DBAccess.PopulateModelBaseEnumeration<T9>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                Table7 = table7,
                Table8 = table8,
                Table9 = table9,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> cps, List<List<String>> parentColumns = null)
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
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();
            var table7 = new List<T7>();
            var table8 = new List<T8>();
            var table9 = new List<T9>();
            var table10 = new List<T10>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 6: table7 = tmp.DBAccess.PopulateModelBaseEnumeration<T7>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 7: table8 = tmp.DBAccess.PopulateModelBaseEnumeration<T8>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 8: table9 = tmp.DBAccess.PopulateModelBaseEnumeration<T9>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 9: table10 = tmp.DBAccess.PopulateModelBaseEnumeration<T10>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                Table7 = table7,
                Table8 = table8,
                Table9 = table9,
                Table10 = table10,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> cps, List<List<String>> parentColumns = null)
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
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();
            var table7 = new List<T7>();
            var table8 = new List<T8>();
            var table9 = new List<T9>();
            var table10 = new List<T10>();
            var table11 = new List<T11>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 6: table7 = tmp.DBAccess.PopulateModelBaseEnumeration<T7>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 7: table8 = tmp.DBAccess.PopulateModelBaseEnumeration<T8>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 8: table9 = tmp.DBAccess.PopulateModelBaseEnumeration<T9>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 9: table10 = tmp.DBAccess.PopulateModelBaseEnumeration<T10>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 10: table11 = tmp.DBAccess.PopulateModelBaseEnumeration<T11>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                Table7 = table7,
                Table8 = table8,
                Table9 = table9,
                Table10 = table10,
                Table11 = table11,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> cps, List<List<String>> parentColumns = null)
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
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();
            var table7 = new List<T7>();
            var table8 = new List<T8>();
            var table9 = new List<T9>();
            var table10 = new List<T10>();
            var table11 = new List<T11>();
            var table12 = new List<T12>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 6: table7 = tmp.DBAccess.PopulateModelBaseEnumeration<T7>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 7: table8 = tmp.DBAccess.PopulateModelBaseEnumeration<T8>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 8: table9 = tmp.DBAccess.PopulateModelBaseEnumeration<T9>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 9: table10 = tmp.DBAccess.PopulateModelBaseEnumeration<T10>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 10: table11 = tmp.DBAccess.PopulateModelBaseEnumeration<T11>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 11: table12 = tmp.DBAccess.PopulateModelBaseEnumeration<T12>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                Table7 = table7,
                Table8 = table8,
                Table9 = table9,
                Table10 = table10,
                Table11 = table11,
                Table12 = table12,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> cps, List<List<String>> parentColumns = null)
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
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();
            var table7 = new List<T7>();
            var table8 = new List<T8>();
            var table9 = new List<T9>();
            var table10 = new List<T10>();
            var table11 = new List<T11>();
            var table12 = new List<T12>();
            var table13 = new List<T13>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 6: table7 = tmp.DBAccess.PopulateModelBaseEnumeration<T7>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 7: table8 = tmp.DBAccess.PopulateModelBaseEnumeration<T8>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 8: table9 = tmp.DBAccess.PopulateModelBaseEnumeration<T9>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 9: table10 = tmp.DBAccess.PopulateModelBaseEnumeration<T10>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 10: table11 = tmp.DBAccess.PopulateModelBaseEnumeration<T11>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 11: table12 = tmp.DBAccess.PopulateModelBaseEnumeration<T12>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 12: table13 = tmp.DBAccess.PopulateModelBaseEnumeration<T13>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                Table7 = table7,
                Table8 = table8,
                Table9 = table9,
                Table10 = table10,
                Table11 = table11,
                Table12 = table12,
                Table13 = table13,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> cps, List<List<String>> parentColumns = null)
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
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();
            var table7 = new List<T7>();
            var table8 = new List<T8>();
            var table9 = new List<T9>();
            var table10 = new List<T10>();
            var table11 = new List<T11>();
            var table12 = new List<T12>();
            var table13 = new List<T13>();
            var table14 = new List<T14>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 6: table7 = tmp.DBAccess.PopulateModelBaseEnumeration<T7>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 7: table8 = tmp.DBAccess.PopulateModelBaseEnumeration<T8>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 8: table9 = tmp.DBAccess.PopulateModelBaseEnumeration<T9>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 9: table10 = tmp.DBAccess.PopulateModelBaseEnumeration<T10>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 10: table11 = tmp.DBAccess.PopulateModelBaseEnumeration<T11>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 11: table12 = tmp.DBAccess.PopulateModelBaseEnumeration<T12>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 12: table13 = tmp.DBAccess.PopulateModelBaseEnumeration<T13>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 13: table14 = tmp.DBAccess.PopulateModelBaseEnumeration<T14>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                Table7 = table7,
                Table8 = table8,
                Table9 = table9,
                Table10 = table10,
                Table11 = table11,
                Table12 = table12,
                Table13 = table13,
                Table14 = table14,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> cps, List<List<String>> parentColumns = null)
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
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();
            var table7 = new List<T7>();
            var table8 = new List<T8>();
            var table9 = new List<T9>();
            var table10 = new List<T10>();
            var table11 = new List<T11>();
            var table12 = new List<T12>();
            var table13 = new List<T13>();
            var table14 = new List<T14>();
            var table15 = new List<T15>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 6: table7 = tmp.DBAccess.PopulateModelBaseEnumeration<T7>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 7: table8 = tmp.DBAccess.PopulateModelBaseEnumeration<T8>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 8: table9 = tmp.DBAccess.PopulateModelBaseEnumeration<T9>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 9: table10 = tmp.DBAccess.PopulateModelBaseEnumeration<T10>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 10: table11 = tmp.DBAccess.PopulateModelBaseEnumeration<T11>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 11: table12 = tmp.DBAccess.PopulateModelBaseEnumeration<T12>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 12: table13 = tmp.DBAccess.PopulateModelBaseEnumeration<T13>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 13: table14 = tmp.DBAccess.PopulateModelBaseEnumeration<T14>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 14: table15 = tmp.DBAccess.PopulateModelBaseEnumeration<T15>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                Table7 = table7,
                Table8 = table8,
                Table9 = table9,
                Table10 = table10,
                Table11 = table11,
                Table12 = table12,
                Table13 = table13,
                Table14 = table14,
                Table15 = table15,
                DBAccess = cps.GetProviderPerTable()
            };
        }

        /// <summary>
        /// Populates a DALTuple object using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <returns>The tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> cps, List<List<String>> parentColumns = null)
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
            var tasks = cps.GetTasks();

            foreach (var t in tasks)
                t.Start();

            Task.WaitAll(tasks);

            var table1 = new List<T1>();
            var table2 = new List<T2>();
            var table3 = new List<T3>();
            var table4 = new List<T4>();
            var table5 = new List<T5>();
            var table6 = new List<T6>();
            var table7 = new List<T7>();
            var table8 = new List<T8>();
            var table9 = new List<T9>();
            var table10 = new List<T10>();
            var table11 = new List<T11>();
            var table12 = new List<T12>();
            var table13 = new List<T13>();
            var table14 = new List<T14>();
            var table15 = new List<T15>();
            var table16 = new List<T16>();

            CPSegmentBase tmp = cps;
            var iTask = 0;
            var i = 0;

            do
            {
                var oldI = i;
                var resultI = 0;
                for (; i < tmp.Tables + oldI; i++)
                {
                    switch (i)
                    {
                        case 0: table1 = tmp.DBAccess.PopulateModelBaseEnumeration<T1>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 1: table2 = tmp.DBAccess.PopulateModelBaseEnumeration<T2>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 2: table3 = tmp.DBAccess.PopulateModelBaseEnumeration<T3>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 3: table4 = tmp.DBAccess.PopulateModelBaseEnumeration<T4>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 4: table5 = tmp.DBAccess.PopulateModelBaseEnumeration<T5>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 5: table6 = tmp.DBAccess.PopulateModelBaseEnumeration<T6>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 6: table7 = tmp.DBAccess.PopulateModelBaseEnumeration<T7>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 7: table8 = tmp.DBAccess.PopulateModelBaseEnumeration<T8>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 8: table9 = tmp.DBAccess.PopulateModelBaseEnumeration<T9>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 9: table10 = tmp.DBAccess.PopulateModelBaseEnumeration<T10>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 10: table11 = tmp.DBAccess.PopulateModelBaseEnumeration<T11>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 11: table12 = tmp.DBAccess.PopulateModelBaseEnumeration<T12>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 12: table13 = tmp.DBAccess.PopulateModelBaseEnumeration<T13>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 13: table14 = tmp.DBAccess.PopulateModelBaseEnumeration<T14>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 14: table15 = tmp.DBAccess.PopulateModelBaseEnumeration<T15>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                        case 15: table16 = tmp.DBAccess.PopulateModelBaseEnumeration<T16>(tasks[iTask].Result[resultI++], parentColumns == null ? null : parentColumns.Count > i ? parentColumns[i] : null); break;
                    }
                }

                iTask++;
                tmp = tmp.Next;
            } while (tmp != null);

            return new DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
            {
                Table1 = table1,
                Table2 = table2,
                Table3 = table3,
                Table4 = table4,
                Table5 = table5,
                Table6 = table6,
                Table7 = table7,
                Table8 = table8,
                Table9 = table9,
                Table10 = table10,
                Table11 = table11,
                Table12 = table12,
                Table13 = table13,
                Table14 = table14,
                Table15 = table15,
                Table16 = table16,
                DBAccess = cps.GetProviderPerTable()
            };
        }
        #endregion

        #region AsRelatedDataSet
        /// <summary>
        /// Performs a related cross provider set read using.
        /// </summary>
        /// <param name="cps"></param>
        /// <param name="relationships">The DALRelationship objects to apply to the data set.</param>
        /// <returns>The lists of objects.</returns>
        public static List<List<Object>> AsRelatedDataSet(this CPSegment cps, List<DALRelationship> relationships)
        {
            var totalTables = cps.GetTotalTableCount();

            if (totalTables > 0 && relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var returnTypes = cps.GetAllTypes();
            var parents = cps.DBAccess.GetParentPropertyLists(returnTypes.Count > 0 ? returnTypes.Count : relationships.Count, relationships);

            var providers = cps.GetProviders();

            var tables = cps.AsDataSet(parents);

            cps.DBAccess.SetRelationships(relationships, tables, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2> AsRelatedDataSet<T1, T2>(this CPSegment<T1, T2> cps, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
        {
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3> AsRelatedDataSet<T1, T2, T3>(this CPSegment<T1, T2, T3> cps, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4> AsRelatedDataSet<T1, T2, T3, T4>(this CPSegment<T1, T2, T3, T4> cps, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5> AsRelatedDataSet<T1, T2, T3, T4, T5>(this CPSegment<T1, T2, T3, T4, T5> cps, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6> AsRelatedDataSet<T1, T2, T3, T4, T5, T6>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7> AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, T7, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8> AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, List<DALRelationship> relationships)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, Object, Object, Object, Object, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, List<DALRelationship> relationships)
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
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, Object, Object, Object, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> cps, List<DALRelationship> relationships)
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
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Object, Object, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> cps, List<DALRelationship> relationships)
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
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Object, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> cps, List<DALRelationship> relationships)
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
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Object, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> cps, List<DALRelationship> relationships)
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
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Object, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> cps, List<DALRelationship> relationships)
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
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Object, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> cps, List<DALRelationship> relationships)
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
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Object>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <returns>The related tuple.</returns>
        public static DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> cps, List<DALRelationship> relationships)
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
            var totalTables = cps.GetTotalTableCount();

            if (relationships.Count > totalTables - 1)
                throw new DALException(String.Format("Too many relationships ({0}) defined for a related set read size of {1}.", relationships.Count, totalTables));

            cps.DBAccess.EnsureRelationshipsHaveIndexes(relationships);
            var parents = cps.DBAccess.GetParentPropertyLists(totalTables, relationships);
            var tables = cps.AsDataSet(parents);

            tables.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(relationships, cps.GetAllModelDataDictionaries());

            return tables;
        }
        #endregion

        #region Async
        #region AsDataSet
        /// <summary>
        /// Populates object lists using the cross provider set read definition object.
        /// </summary>
        /// <param name="cps"></param>
        /// <param name="callback">The optional callback.</param>
        public static void AsDataSetAsync(this CPSegment cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadObjectsCallbackDelegate callback = null)
        {
            new Task(() =>
            {
                var tuple = cps.AsDataSet();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2>(this CPSegment<T1, T2> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2> callback = null)
            where T1 : class, new()
            where T2 : class, new()
        {
            new Task(() =>
            {
                var tuple = cps.AsDataSet<T1, T2>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3>(this CPSegment<T1, T2, T3> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            new Task(() =>
            {
                var tuple = cps.AsDataSet<T1, T2, T3>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4>(this CPSegment<T1, T2, T3, T4> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            new Task(() =>
            {
                var tuple = cps.AsDataSet<T1, T2, T3, T4>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5>(this CPSegment<T1, T2, T3, T4, T5> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            new Task(() =>
            {
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            new Task(() =>
            {
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7> callback = null)
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
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6, T7>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8> callback = null)
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
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback = null)
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
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback = null)
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
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback = null)
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
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type o the second data table.</typeparam>
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback = null)
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
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback = null)
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
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback = null)
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
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback = null)
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
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Populates a DALTuple object asynchronously using the cross provider set read definition object.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> cps, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback = null)
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
                var tuple = cps.AsDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
                if (callback != null)
                    callback(tuple);
            }).Start();
        }
        #endregion
        #region AsRelatedDataSet
        /// <summary>
        /// Executtes a cross provider set read and returns a related data set asynchronously.
        /// </summary>
        /// <param name="cps"></param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        /// <param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync(this CPSegment cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadObjectsCallbackDelegate callback = null)
        {
            new Task(() =>
            {
                var tuple = cps.AsRelatedDataSet(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2>(this CPSegment<T1, T2> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2> callback = null)
            where T1 : class, new()
            where T2 : class, new()
        {
            new Task(() =>
            {
                var tuple = cps.AsRelatedDataSet<T1, T2>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3>(this CPSegment<T1, T2, T3> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            new Task(() =>
            {
                var tuple = cps.AsRelatedDataSet<T1, T2, T3>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4>(this CPSegment<T1, T2, T3, T4> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            new Task(() =>
            {
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5>(this CPSegment<T1, T2, T3, T4, T5> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            new Task(() =>
            {
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6>(this CPSegment<T1, T2, T3, T4, T5, T6> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6> callback = null)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            new Task(() =>
            {
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(this CPSegment<T1, T2, T3, T4, T5, T6, T7> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7> callback = null)
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
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
        /// </summary>
        /// <typeparam name="T1">The type of the first data table.</typeparam>
        /// <typeparam name="T2">The type of the second data table.</typeparam>
        /// <typeparam name="T3">The type of the third data table.</typeparam>
        /// <typeparam name="T4">The type of the fourth data table.</typeparam>
        /// <typeparam name="T5">The type of the fifth data table.</typeparam>
        /// <typeparam name="T6">The type of the sixth data table.</typeparam>
        /// <typeparam name="T7">The type of the seventh data table.</typeparam>
        /// <typeparam name="T8">The type of the eigth data table.</typeparam>
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8> callback = null)
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
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback = null)
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
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback = null)
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
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback = null)
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
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback = null)
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
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback = null)
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
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback = null)
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
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback = null)
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
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }

        /// <summary>
        /// Executes a cross provider set read and returns a related data set asynchronously.
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
        /// <param name="cps">The cross provider set read definition to execute.</param>
        /// <param name="relationships">The DALRelationship objects to apply to the dataset.</param>
        ///<param name="callback">The optional callback.</param>
        public static void AsRelatedDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this CPSegment<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> cps, List<DALRelationship> relationships, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback = null)
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
                var tuple = cps.AsRelatedDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(relationships);
                if (callback != null)
                    callback(tuple);
            }).Start();
        }
        #endregion
        #endregion
    }
}