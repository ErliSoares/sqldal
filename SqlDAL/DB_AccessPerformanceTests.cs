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
using System.Data.DBAccess.Generic.Providers.SQL;
using System.Diagnostics;
using System.Linq;

namespace System.Data.DBAccess.Generic.Benchmarking
{
    public class TestCase : IQuickRead
    {
        public String Name { get; set; }
        public String TotalFieldsPopulated { get; set; }
        public Double TimeTaken { get; set; }
        public String ThousandsOfFieldsPerSecond { get; set; }

        public Object[] ToObjectArray()
        {
            return new Object[]
            {
                this.Name,
                int.Parse((this.TotalFieldsPopulated ?? "0").Replace(",", "")),
                this.TimeTaken,
                int.Parse((this.ThousandsOfFieldsPerSecond == "Infinity" ? "0" : this.ThousandsOfFieldsPerSecond ?? "0").Replace(",", ""))
            };
        }

        public Dictionary<String, Type> GetColumnNamesTypes()
        {
            return new Dictionary<string, Type> { { "Name", typeof(String) }, { "TotalFieldsPopulated", typeof(int) }, { "TimeTaken", typeof(Double) }, { "ThousandsOfFieldsPerSecond", typeof(int) } };
        }
    }

    public static class PerformanceTests
    {
        public delegate void OnTestCompletedHandler(String testName);
        public static event OnTestCompletedHandler OnTestCompleted;

        private static void RaiseOnTestCompleted(String testName)
        {
            if (PerformanceTests.OnTestCompleted != null)
                PerformanceTests.OnTestCompleted(testName);
        }

        public static TestCase ExecuteReadGenericTest<T>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
            where T : class, new()
        {
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var db = new SqlDBAccess();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.PopulateModelBaseEnumeration<T>(sampleData);
                }));
            }

            int fieldsInReturnType = input.GetType().GetProperties().Count();

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnType, times);
        }

        public static TestCase ExecuteScalarEnumerationTest<T>(String name, List<Object[]> sampleData, int iterationsPerRun, int timesToRun)
        {
            var db = new SqlDBAccess();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateScalarEnumeration<T>(sampleData);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, 1, times);
        }

        public static TestCase UDTableTest(String name, Object input, int iterationsPerRun, int timesToRun)
        {
            List<Object> inputValues = new List<Object>(iterationsPerRun);
            for (int i = 0; i < iterationsPerRun; i++)
                inputValues.Add(input);

            var db = new SqlDBAccess();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.CreateDataTableFromModelProperty(inputValues);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, ((IDBAccess)db).ModelsData[input.GetType()].AllNestedModelFields.Count, times);
        }

        #region ExecuteSetRead
        public static TestCase ExecuteSetReadGenericTest<T1, T2>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
            where T1 : class, new()
            where T2 : class, new()
        {
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
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
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
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
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
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
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
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
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
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
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
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
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
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
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(String name, Object input, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
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
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);

            var tables = new List<ExecuteReadQuickTuple>
            {
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData,
                sampleData
            };
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = tables.Sum(t => t.ColumnNames.Count);

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(tables);
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteSetReadGenericTest<T>(String name, Object input, Type[] returnTypes, Boolean populateDefaultValues, int iterationsPerRun, int timesToRun)
            where T : class, new()
        {
            ExecuteReadQuickTuple sampleData = ConvertObjectToExecuteReadTuple(input, iterationsPerRun);
            var db = new SqlDBAccess();

            List<TimeSpan> times = new List<TimeSpan>();

            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    foreach (var rt in returnTypes)
                    {
                        db.PopulateModelBaseEnumeration(sampleData, rt).OfType<T>().ToList();
                    }
                }));
            }

            return GetTestCaseReturnValue(name, iterationsPerRun, timesToRun, returnTypes.Sum(rt => rt.GetProperties().Count()), times);
        }
        #endregion

        #region ExecuteRelatedSetRead
        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
            where T1 : class, new()
            where T2 : class, new()
        {
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2>(tables);
                    tuple.SetRelationships<T1, T2, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3>(tables);
                    tuple.SetRelationships<T1, T2, T3, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
        {
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
        {
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            var start = DateTime.Now;
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, Object, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
        {
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, Object, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
            where T5 : class, new()
            where T6 : class, new()
            where T7 : class, new()
            where T8 : class, new()
        {
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, Object, Object, Object, Object, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
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
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, Object, Object, Object, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
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
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Object, Object, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
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
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Object, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
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
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Object, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
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
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Object, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
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
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Object, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
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
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Object>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadGenericTest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
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
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                {
                    db.EnsureRelationshipsHaveIndexes(relationships);
                    var parents = db.GetParentPropertyLists(tables.Count, relationships);
                    var tuple = db.GenerateModelEnumerations<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(tables);
                    tuple.SetRelationships<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(relationships);
                }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }

        public static TestCase ExecuteRelatedSetReadTest(String name, List<DALRelationship> relationships, List<ExecuteReadQuickTuple> tables, List<Type> returnTypes, Boolean populateDefaultValues, int timesToRun, int childrenPerParent)
        {
            var db = new SqlDBAccess();

            int fieldsInReturnTypes = (int)tables.Select((t, i) => t.ColumnNames.Count * Math.Pow(childrenPerParent, i + 1)).Sum();

            List<TimeSpan> times = new List<TimeSpan>();

            for (int i = 0; i < timesToRun; i++)
            {
                times.Add(PerformanceTests.RunAndTimeAction(() =>
                    {
                        db.EnsureRelationshipsHaveIndexes(relationships);
                        var parents = db.GetParentPropertyLists(tables.Count, relationships);
                        var relTables = tables.Select((t, k) => db.PopulateModelBaseEnumeration(t, returnTypes[k], parents[k])).ToList();
                        db.SetRelationships(relationships, relTables);
                    }));
            }

            return GetTestCaseReturnValue(name, 1, timesToRun, fieldsInReturnTypes, times);
        }
        #endregion

        private static TimeSpan RunAndTimeAction(Action a)
        {
            var s = new Stopwatch();
            s.Start();
            a();
            s.Stop();
            return s.Elapsed;
        }

        public static List<Object[]> ConvertObjectToDataRows(Object input, int iterations)
        {
            var properties = input.GetType().GetProperties();
            var obj = properties.Select(p => p.GetValue(input, null)).ToArray();

            var objs = new List<Object[]>();

            for (int i = 0; i < iterations; i++)
                objs.Add(obj);

            return objs;
        }

        private static Dictionary<String, ExecuteReadQuickTuple> m_tuplesCache = new Dictionary<String, ExecuteReadQuickTuple>();
        private static ExecuteReadQuickTuple ConvertObjectToExecuteReadTuple(Object input, int iterations)
        {
            ExecuteReadQuickTuple tuple;
            if (!m_tuplesCache.TryGetValue(String.Format("{0}{1}", input.GetType(), iterations), out tuple))
            {
                var properties = input.GetType().GetProperties();
                var obj = properties.Select(p => p.GetValue(input, null)).ToArray();

                var objs = new List<Object[]>();

                for (int i = 0; i < iterations; i++)
                    objs.Add(obj);

                m_tuplesCache.Add(String.Format("{0}{1}", input.GetType(), iterations), new ExecuteReadQuickTuple
                {
                    DataRows = objs,
                    ColumnNames = properties.Select(p => p.Name).ToList(),
                    ColumnTypes = properties.Select(p => p.PropertyType.IsNullableValueType() ? p.PropertyType.GetNullableUnderlyingType() : p.PropertyType).ToList()
                });
            }

            return m_tuplesCache[String.Format("{0}{1}", input.GetType(), iterations)];
        }

        private static TestCase GetTestCaseReturnValue(String name, long iterationsPerRun, long timesToRun, long totalFields, List<TimeSpan> times)
        {
            PerformanceTests.RaiseOnTestCompleted(name);

            var top = times.OrderBy(t => t.TotalSeconds).First();
            var totalSeconds = top.TotalSeconds;

            return new TestCase
            {
                Name = name,
                TimeTaken = totalSeconds,
                ThousandsOfFieldsPerSecond = String.Format("{0:n0}", iterationsPerRun * totalFields / totalSeconds / 1000),
                TotalFieldsPopulated = String.Format("{0:n0}", iterationsPerRun * totalFields)
            };
        }
    }
}