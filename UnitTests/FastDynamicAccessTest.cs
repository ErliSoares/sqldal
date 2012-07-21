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
using System.Data.DBAccess.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    
    
    /// <summary>
    ///This is a test class for FastDynamicAccessTest and is intended
    ///to contain all FastDynamicAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FastDynamicAccessTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal class MyClass
        {
            internal String TestString { get; set; }
            internal Boolean TestBoolean { get; set; }
            internal Byte TestByte { get; set; }
            internal short TestShort { get; set; }
            internal int TestInt { get; set; }
            internal float TestFloat { get; set; }
            internal Double TestDouble { get; set; }
            internal Decimal TestDecimal { get; set; }
            internal DateTime TestDateTime { get; set; }
            internal TimeSpan TestTimeSpan { get; set; }

            internal Byte[] TestByteArray { get; set; }

            internal Boolean? TestNullableBoolean { get; set; }
            internal Byte? TestNullableByte { get; set; }
            internal short? TestNullableShort { get; set; }
            internal int? TestNullableInt { get; set; }
            internal float? TestNullableFloat { get; set; }
            internal Double? TestNullableDouble { get; set; }
            internal Decimal? TestNullableDecimal { get; set; }
            internal DateTime? TestNullableDateTime { get; set; }
            internal TimeSpan? TestNullableTimeSpan { get; set; }
        }

        #region Getters
        [TestMethod()]
        public void GetStringTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestString = "Hello World" };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestString");

            Assert.AreEqual<String>("Hello World", (String)retValue);
        }

        [TestMethod()]
        public void GetBooleanTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestBoolean = true };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestBoolean");

            Assert.AreEqual<Boolean>(true, (Boolean)retValue);
        }

        [TestMethod()]
        public void GetByteTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestByte = 5 };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestByte");

            Assert.AreEqual<Byte>(5, (Byte)retValue);
        }

        [TestMethod()]
        public void GetShortTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestShort = 5 };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestShort");

            Assert.AreEqual<short>(5, (short)retValue);
        }

        [TestMethod()]
        public void GetIntTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestInt = 5 };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestInt");

            Assert.AreEqual<int>(5, (int)retValue);
        }

        [TestMethod()]
        public void GetFloatTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestFloat = 5f };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestFloat");

            Assert.AreEqual<float>(5f, (float)retValue);
        }

        [TestMethod()]
        public void GetDoubleTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestDouble = 5d };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestDouble");

            Assert.AreEqual<Double>(5d, (Double)retValue);
        }

        [TestMethod()]
        public void GetDecimalTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestDecimal = 5m };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestDecimal");

            Assert.AreEqual<Decimal>(5m, (Decimal)retValue);
        }

        [TestMethod()]
        public void GetDateTimeTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestDateTime = new DateTime(1900, 3, 11) };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestDateTime");

            Assert.AreEqual<DateTime>(new DateTime(1900, 3, 11), (DateTime)retValue);
        }

        [TestMethod()]
        public void GetTimeSpanTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestTimeSpan = new TimeSpan(18, 21, 31) };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestTimeSpan");

            Assert.AreEqual<TimeSpan>(new TimeSpan(18, 21, 31), (TimeSpan)retValue);
        }

        [TestMethod()]
        public void GetByteArrayTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestByteArray = new Byte[] { 7, 8, 9 } };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestByteArray");

            Assert.AreEqual<int>(3, ((Byte[])retValue).Length);
            Assert.AreEqual<Byte[]>(m.TestByteArray, (Byte[])retValue);
            Assert.AreEqual<Byte>(7, ((Byte[])retValue)[0]);
            Assert.AreEqual<Byte>(8, ((Byte[])retValue)[1]);
            Assert.AreEqual<Byte>(9, ((Byte[])retValue)[2]);
        }

        [TestMethod()]
        public void GetByteArray_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestByteArray = null };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestByteArray");

            Assert.AreEqual<Byte[]>(null, (Byte[])retValue);
        }

        [TestMethod()]
        public void GetBooleanNullableTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableBoolean = true };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableBoolean");

            Assert.AreEqual<Boolean>(true, (Boolean)retValue);
        }

        [TestMethod()]
        public void GetByteNullableTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableByte = 5 };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableByte");

            Assert.AreEqual<Byte>(5, (Byte)retValue);
        }

        [TestMethod()]
        public void GetShortNullableTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableShort = 5 };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableShort");

            Assert.AreEqual<short>(5, (short)retValue);
        }

        [TestMethod()]
        public void GetIntNullableTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableInt = 5 };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableInt");

            Assert.AreEqual<int>(5, (int)retValue);
        }

        [TestMethod()]
        public void GetFloatNullableTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableFloat = 5f };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableFloat");

            Assert.AreEqual<float>(5f, (float)retValue);
        }

        [TestMethod()]
        public void GetDoubleNullableTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableDouble = 5d };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableDouble");

            Assert.AreEqual<Double>(5d, (Double)retValue);
        }

        [TestMethod()]
        public void GetDecimalNullableTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableDecimal = 5m };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableDecimal");

            Assert.AreEqual<Decimal>(5m, (Decimal)retValue);
        }

        [TestMethod()]
        public void GetDateTimeNullableTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableDateTime = new DateTime(1900, 3, 11) };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableDateTime");

            Assert.AreEqual<DateTime>(new DateTime(1900, 3, 11), (DateTime)retValue);
        }

        [TestMethod()]
        public void GetTimeSpanNullableTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableTimeSpan = new TimeSpan(18, 21, 31) };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableTimeSpan");

            Assert.AreEqual<TimeSpan>(new TimeSpan(18, 21, 31), (TimeSpan)retValue);
        }

        [TestMethod()]
        public void GetBooleanNullableNull_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableBoolean = null };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableBoolean");

            Assert.AreEqual<Boolean?>(null, (Boolean?)retValue);
        }

        [TestMethod()]
        public void GetByteNullableNull_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableByte = null };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableByte");

            Assert.AreEqual<Byte?>(null, (Byte?)retValue);
        }

        [TestMethod()]
        public void GetShortNullableNull_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableShort = null };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableShort");

            Assert.AreEqual<short?>(null, (short?)retValue);
        }

        [TestMethod()]
        public void GetIntNullableNull_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableInt = null };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableInt");

            Assert.AreEqual<int?>(null, (int?)retValue);
        }

        [TestMethod()]
        public void GetFloatNullableNull_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableFloat = null };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableFloat");

            Assert.AreEqual<float?>(null, (float?)retValue);
        }

        [TestMethod()]
        public void GetDoubleNullableNull_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableDouble = null };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableDouble");

            Assert.AreEqual<Double?>(null, (Double?)retValue);
        }

        [TestMethod()]
        public void GetDecimalNullableNull_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableDecimal = null };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableDecimal");

            Assert.AreEqual<Decimal?>(null, (Decimal?)retValue);
        }

        [TestMethod()]
        public void GetDateTimeNullableNull_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableDateTime = null };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableDateTime");

            Assert.AreEqual<DateTime?>(null, (DateTime?)retValue);
        }

        [TestMethod()]
        public void GetTimeSpanNullableNull_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass { TestNullableTimeSpan = null };
            var fda = FastDynamicAccess.Get(type);

            Object retValue = fda.Get(m, "TestNullableTimeSpan");

            Assert.AreEqual<TimeSpan?>(null, (TimeSpan?)retValue);
        }
        #endregion

        #region Setters
        [TestMethod()]
        public void SetStringTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = "Hello World";

            Assert.AreEqual<String>(null, m.TestString);

            fda.Set(m, "TestString", testValue);

            Assert.AreEqual<String>(testValue, m.TestString);
        }

        [TestMethod()]
        public void SetBooleanTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = true;

            Assert.AreEqual<Boolean>(false, m.TestBoolean);

            fda.Set(m, "TestBoolean", testValue);

            Assert.AreEqual<Boolean>(testValue, m.TestBoolean);
        }

        [TestMethod()]
        public void SetByteTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = (Byte)10;

            Assert.AreEqual<Byte>(0, m.TestByte);

            fda.Set(m, "TestByte", testValue);

            Assert.AreEqual<int>(testValue, m.TestByte);
        }

        [TestMethod()]
        public void SetIntTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = 10;

            Assert.AreEqual<int>(0, m.TestInt);

            fda.Set(m, "TestInt", testValue);

            Assert.AreEqual<int>(testValue, m.TestInt);
        }

        [TestMethod()]
        public void SetFloatTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = 10.5f;

            Assert.AreEqual<float>(0, m.TestFloat);

            fda.Set(m, "TestFloat", testValue);

            Assert.AreEqual<float>(testValue, m.TestFloat);
        }

        [TestMethod()]
        public void SetDoubleTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = 10.5d;

            Assert.AreEqual<Double>(0, m.TestDouble);

            fda.Set(m, "TestDouble", testValue);

            Assert.AreEqual<Double>(testValue, m.TestDouble);
        }

        [TestMethod()]
        public void SetDecimalTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = 10.5m;

            Assert.AreEqual<Decimal>(0, m.TestDecimal);

            fda.Set(m, "TestDecimal", testValue);

            Assert.AreEqual<Decimal>(testValue, m.TestDecimal);
        }

        [TestMethod()]
        public void SetDateTimeTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = DateTime.Now;

            Assert.AreEqual<DateTime>(new DateTime(), m.TestDateTime);

            fda.Set(m, "TestDateTime", testValue);

            Assert.AreEqual<DateTime>(testValue, m.TestDateTime);
        }

        [TestMethod()]
        public void SetTimeSpanTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = DateTime.Now.TimeOfDay;

            Assert.AreEqual<TimeSpan>(new TimeSpan(), m.TestTimeSpan);

            fda.Set(m, "TestTimeSpan", testValue);

            Assert.AreEqual<TimeSpan>(testValue, m.TestTimeSpan);
        }

        [TestMethod()]
        public void SetByteArrayTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = new Byte[] { 7, 8, 9 };

            Assert.AreEqual<Byte[]>(null, m.TestByteArray);

            fda.Set(m, "TestByteArray", testValue);

            Assert.AreEqual<int>(3, m.TestByteArray.Length);
            Assert.AreEqual<Byte[]>(testValue, m.TestByteArray);
            Assert.AreEqual<Byte>(testValue[0], m.TestByteArray[0]);
            Assert.AreEqual<Byte>(testValue[1], m.TestByteArray[1]);
            Assert.AreEqual<Byte>(testValue[2], m.TestByteArray[2]);
        }

        [TestMethod()]
        public void SetByteArray_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            Byte[] testValue = null;

            Assert.AreEqual<Byte[]>(null, m.TestByteArray);

            fda.Set(m, "TestByteArray", testValue);

            Assert.AreEqual<Byte[]>(testValue, m.TestByteArray);
        }

        [TestMethod()]
        public void SetNullableBooleanTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = true;

            Assert.AreEqual<Boolean?>(null, m.TestNullableBoolean);

            fda.Set(m, "TestNullableBoolean", testValue);

            Assert.AreEqual<Boolean?>(testValue, m.TestNullableBoolean);
        }

        [TestMethod()]
        public void SetNullableBoolean_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            Boolean? testValue = null;

            Assert.AreEqual<Boolean?>(null, m.TestNullableBoolean);

            fda.Set(m, "TestNullableBoolean", testValue);

            Assert.AreEqual<Boolean?>(testValue, m.TestNullableBoolean);
        }

        [TestMethod()]
        public void SetNullableByteTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = (Byte?)5;

            Assert.AreEqual<Byte?>(null, m.TestNullableByte);

            fda.Set(m, "TestNullableByte", testValue);

            Assert.AreEqual<Byte?>(testValue, m.TestNullableByte);
        }

        [TestMethod()]
        public void SetNullableByte_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            Byte? testValue = null;

            Assert.AreEqual<Byte?>(null, m.TestNullableByte);

            fda.Set(m, "TestNullableByte", testValue);

            Assert.AreEqual<Byte?>(testValue, m.TestNullableByte);
        }

        [TestMethod()]
        public void SetNullableShortTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = (short?)5;

            Assert.AreEqual<short?>(null, m.TestNullableShort);

            fda.Set(m, "TestNullableShort", testValue);

            Assert.AreEqual<short?>(testValue, m.TestNullableShort);
        }

        [TestMethod()]
        public void SetNullableShort_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            short? testValue = null;

            Assert.AreEqual<short?>(null, m.TestNullableShort);

            fda.Set(m, "TestNullableShort", testValue);

            Assert.AreEqual<short?>(testValue, m.TestNullableShort);
        }

        [TestMethod()]
        public void SetNullableIntTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = (int?)5;

            Assert.AreEqual<int?>(null, m.TestNullableInt);

            fda.Set(m, "TestNullableInt", testValue);

            Assert.AreEqual<int?>(testValue, m.TestNullableInt);
        }

        [TestMethod()]
        public void SetNullableint_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            int? testValue = null;

            Assert.AreEqual<int?>(null, m.TestNullableInt);

            fda.Set(m, "TestNullableInt", testValue);

            Assert.AreEqual<int?>(testValue, m.TestNullableInt);
        }

        [TestMethod()]
        public void SetNullableFloatTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = (float?)5;

            Assert.AreEqual<float?>(null, m.TestNullableFloat);

            fda.Set(m, "TestNullableFloat", testValue);

            Assert.AreEqual<float?>(testValue, m.TestNullableFloat);
        }

        [TestMethod()]
        public void SetNullablefloat_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            float? testValue = null;

            Assert.AreEqual<float?>(null, m.TestNullableFloat);

            fda.Set(m, "TestNullableFloat", testValue);

            Assert.AreEqual<float?>(testValue, m.TestNullableFloat);
        }

        [TestMethod()]
        public void SetNullableDoubleTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = (Double?)5;

            Assert.AreEqual<Double?>(null, m.TestNullableDouble);

            fda.Set(m, "TestNullableDouble", testValue);

            Assert.AreEqual<Double?>(testValue, m.TestNullableDouble);
        }

        [TestMethod()]
        public void SetNullableDouble_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            Double? testValue = null;

            Assert.AreEqual<Double?>(null, m.TestNullableDouble);

            fda.Set(m, "TestNullableDouble", testValue);

            Assert.AreEqual<Double?>(testValue, m.TestNullableDouble);
        }

        [TestMethod()]
        public void SetNullableDecimalTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = (Decimal?)5;

            Assert.AreEqual<Decimal?>(null, m.TestNullableDecimal);

            fda.Set(m, "TestNullableDecimal", testValue);

            Assert.AreEqual<Decimal?>(testValue, m.TestNullableDecimal);
        }

        [TestMethod()]
        public void SetNullableDecimal_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            Decimal? testValue = null;

            Assert.AreEqual<Decimal?>(null, m.TestNullableDecimal);

            fda.Set(m, "TestNullableDecimal", testValue);

            Assert.AreEqual<Decimal?>(testValue, m.TestNullableDecimal);
        }

        [TestMethod()]
        public void SetNullableDateTimeTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = (DateTime?)DateTime.Now;

            Assert.AreEqual<DateTime?>(null, m.TestNullableDateTime);

            fda.Set(m, "TestNullableDateTime", testValue);

            Assert.AreEqual<DateTime?>(testValue, m.TestNullableDateTime);
        }

        [TestMethod()]
        public void SetNullableDateTime_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            DateTime? testValue = null;

            Assert.AreEqual<DateTime?>(null, m.TestNullableDateTime);

            fda.Set(m, "TestNullableDateTime", testValue);

            Assert.AreEqual<DateTime?>(testValue, m.TestNullableDateTime);
        }

        [TestMethod()]
        public void SetNullableTimeSpanTest()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            var testValue = (TimeSpan?)DateTime.Now.TimeOfDay;

            Assert.AreEqual<TimeSpan?>(null, m.TestNullableTimeSpan);

            fda.Set(m, "TestNullableTimeSpan", testValue);

            Assert.AreEqual<TimeSpan?>(testValue, m.TestNullableTimeSpan);
        }

        [TestMethod()]
        public void SetNullableTimeSpan_Null_Test()
        {
            Type type = typeof(MyClass);
            var m = new MyClass();
            var fda = FastDynamicAccess.Get(type);
            TimeSpan? testValue = null;

            Assert.AreEqual<TimeSpan?>(null, m.TestNullableTimeSpan);

            fda.Set(m, "TestNullableTimeSpan", testValue);

            Assert.AreEqual<TimeSpan?>(testValue, m.TestNullableTimeSpan);
        }
        #endregion
    }
}