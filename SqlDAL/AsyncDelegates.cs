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
    /// Async callback delegates.
    /// </summary>
    public sealed partial class AsyncDelegates
    {
        /// <summary>
        /// Async callback for ExecuteRead.
        /// </summary>
        /// <param name="rows">The rows which resulted from the ExecuteRead query.</param>
        public delegate void ExecuteReadCallbackDelegate(DataTable dt);

        /// <summary>
        /// Async callback for ExecuteRead generic.
        /// </summary>
        /// <typeparam name="T">The type of output models to return.</typeparam>
        /// <param name="rows">The rows which resulted from the ExecuteRead query.</param>
        public delegate void ExecuteReadCallbackDelegate<T>(List<T> rows) where T : class, new();

        /// <summary>
        /// Async callback for ExecuteReadSingle.
        /// </summary>
        /// <param name="row">The row which resulted from the ExecuteReadSingle query.</param>
        public delegate void ExecuteReadSingleCallbackDelegate(DataRow row);

        /// <summary>
        /// Async callback for ExecuteReadSingle generic.
        /// </summary>
        /// <typeparam name="T">The type of output model to return.</typeparam>
        /// <param name="row">The row which resulted from the ExecuteReadSingle query.</param>
        public delegate void ExecuteReadSingleCallbackDelegate<T>(T row) where T : class, new();

        /// <summary>
        /// Async callback for JSON and XML ExecuteRelatedSetRead functions.
        /// </summary>
        /// <param name="text">The JSON/XML string.</param>
        public delegate void ExecuteRelatedSetReadAsStringCallback(String text);

        /// <summary>
        /// Async callback for ExecuteSetRead.
        /// </summary>
        /// <param name="tables">The DataRow collections which resulted from the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackDelegate(DataSet ds);

        /// <summary>
        /// Async callback for ExecuteSetRead with more than 16 tables of strongly typed objects.
        /// </summary>
        /// <param name="tables">The Object collections which resulted from the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadObjectsCallbackDelegate(List<List<Object>> sets);

        /// <summary>
        /// Async callback for ExecuteSetRead with 2 generic arguments.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2>(DALTuple<T1, T2> tables)
            where T1 : class, new()
            where T2 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 3 generic arguments.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3>(DALTuple<T1, T2, T3> tables)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 4 generic arguments.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4>(DALTuple<T1, T2, T3, T4> tables)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 5 generic arguments.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5>(DALTuple<T1, T2, T3, T4, T5> tables)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 6 generic arguments.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6>(DALTuple<T1, T2, T3, T4, T5, T6> tables)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 7 generic arguments.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7>(DALTuple<T1, T2, T3, T4, T5, T6, T7> tables)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 8 generic arguments.
        /// </summary>
        /// <typeparam name="T1">The type representing the first return data set.</typeparam>
        /// <typeparam name="T2">The type representing the second return data set.</typeparam>
        /// <typeparam name="T3">The type representing the third return data set.</typeparam>
        /// <typeparam name="T4">The type representing the fourth return data set.</typeparam>
        /// <typeparam name="T5">The type representing the fifth return data set.</typeparam>
        /// <typeparam name="T6">The type representing the sixth return data set.</typeparam>
        /// <typeparam name="T7">The type representing the seventh return data set.</typeparam>
        /// <typeparam name="T8">The type representing the eighth return data set.</typeparam>
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8>(DALTuple<T1, T2, T3, T4, T5, T6, T7, T8> tables)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 9 generic arguments.
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
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> tables)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 10 generic arguments.
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
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> tables)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
            where T9 : class, new()
            where T10 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 11 generic arguments.
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
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> tables)
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
            where T11 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 12 generic arguments.
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
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> tables)
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
            where T12 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 13 generic arguments.
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
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> tables)
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
            where T13 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 14 generic arguments.
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
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> tables)
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
            where T14 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 15 generic arguments.
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
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> tables)
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
            where T15 : class, new();

        /// <summary>
        /// Async callback for ExecuteSetRead with 16 generic arguments.
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
        /// <param name="tables">The DALTuple containing the tables returned by the ExecuteSetRead query.</param>
        public delegate void ExecuteSetReadCallbackGenericDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(DALTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> tables)
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
            where T16 : class, new();

        /// <summary>
        /// Async callback for ExecuteScalar.
        /// </summary>
        /// <param name="value">The object returned by the ExecuteScalar query.</param>
        public delegate void ExecuteScalarCallbackDelegate(Object value);

        /// <summary>
        /// Async callback for ExecuteScalar generic.
        /// </summary>
        /// <typeparam name="T">The type of object to cast the return value to.</typeparam>
        /// <param name="value">The object of type T returned by the ExecuteScalar query.</param>
        public delegate void ExecuteScalarCallbackDelegate<T>(T value);

        /// <summary>
        /// Async callback for ExecuteScalarEnumeration.
        /// </summary>
        /// <typeparam name="T">The type of object to cast the return values to.</typeparam>
        /// <param name="values">The list of objects of type T returned by the ExecuteScalarEnumeration query.</param>
        public delegate void ExecuteScalarEnumerationCallbackDelegate<T>(List<T> values);

        /// <summary>
        /// Async callback for ExecuteNonQuery.
        /// </summary>
        /// <param name="rowsAffected">The rows affected by the ExecuteNonQuery.</param>
        public delegate void ExecuteNonQueryCallbackDelegate(int rowsAffected);
    }
}