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
using System.Data;
using System.Data.DBAccess.Generic;
using System.Data.DBAccess.Generic.Providers.SQL;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{


    /// <summary>
    ///This is a test class for RuntimeClassConverterTest and is intended
    ///to contain all RuntimeClassConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RuntimeClassConverterTest
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


        private static String ConnectionString { get { return ""; } }
        private static SqlDBAccess NewDB { get { return new SqlDBAccess(ConnectionString); } }
        private const String BasicRead = "SELECT * FROM ReadType1 WITH (NOLOCK)";
        private const String BasicReadNoRows = "SELECT * FROM ReadType1 WITH (NOLOCK) WHERE ID > 5";

        [TestMethod()]
        public void ToRuntimeClassListTest()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;

            var classes = db.ExecuteRead<Object>();

            Assert.AreEqual<int>(5, classes.Count);

            var first = classes.First();
            var fda = FastDynamicAccess.Get(first.GetType());
            Assert.AreEqual<int>(3, first.GetType().GetProperties().Count());
            Assert.AreEqual<String>("R1-1", fda.Get<String>(first, "Name"));
            Assert.AreEqual<int>(1, fda.Get<int>(first, "ID"));
        }

        [TestMethod()]
        public void ToRuntimeClassListTest_NoRows()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadNoRows;

            var classes = db.ExecuteRead<Object>();

            Assert.AreEqual<int>(0, classes.Count);
        }

        [TestMethod()]
        public void CreateTypeTest_NoProperties()
        {
            var actual = new List<Object[]>().GetRuntimeType("", null, null, null);
            //will have one property TableName
            Assert.AreEqual<int>(1, actual.GetProperties().Length);
        }

        [TestMethod()]
        public void CreateTypeTest_AllNonNullable()
        {
            List<String> colNames = new List<String>
            {
                "String",
                "int",
                "intnull",
                "float",
                "floatnull",
                "short",
                "shortnull",
                "double",
                "doublenull",
                "decimal",
                "decimalnull",
                "byte",
                "bytenull",
                "bool",
                "boolnull",
                "bytearray",
                "datetime",
                "datetimenull",
                "timespan",
                "timespannull",
                "String2"
            };

            List<Type> colTypes = new List<Type>
            {
                typeof(String),
                typeof(int),
                typeof(int),
                typeof(float),
                typeof(float),
                typeof(short),
                typeof(short),
                typeof(double),
                typeof(double),
                typeof(decimal),
                typeof(decimal),
                typeof(byte),                
                typeof(byte),
                typeof(bool),
                typeof(bool),
                typeof(byte[]),
                typeof(DateTime),
                typeof(DateTime),
                typeof(TimeSpan),
                typeof(TimeSpan),
                typeof(String),
            };

            var rows = new List<Object[]> { new Object[] { "", 5, 5, 5f, 5f, 5, 5, 5, 5, 5, 5, 5, 5, true, true, new Byte[] { }, DateTime.Now, DateTime.Now, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay, "2" } };

            var actual = rows.GetRuntimeType("TestNoNulls", colNames, colTypes, null);
            var ps = actual.GetProperties();
            Assert.AreEqual<int>(22, ps.Length);
            Assert.AreEqual<Type>(typeof(String), actual.GetProperty("String").PropertyType);
            Assert.AreEqual<Type>(typeof(String), actual.GetProperty("String2").PropertyType);
            Assert.AreEqual<Type>(typeof(int), actual.GetProperty("int").PropertyType);
            Assert.AreEqual<Type>(typeof(int), actual.GetProperty("intnull").PropertyType);
            Assert.AreEqual<Type>(typeof(float), actual.GetProperty("float").PropertyType);
            Assert.AreEqual<Type>(typeof(float), actual.GetProperty("floatnull").PropertyType);
            Assert.AreEqual<Type>(typeof(short), actual.GetProperty("short").PropertyType);
            Assert.AreEqual<Type>(typeof(short), actual.GetProperty("shortnull").PropertyType);
            Assert.AreEqual<Type>(typeof(double), actual.GetProperty("double").PropertyType);
            Assert.AreEqual<Type>(typeof(double), actual.GetProperty("doublenull").PropertyType);
            Assert.AreEqual<Type>(typeof(decimal), actual.GetProperty("decimal").PropertyType);
            Assert.AreEqual<Type>(typeof(decimal), actual.GetProperty("decimalnull").PropertyType);
            Assert.AreEqual<Type>(typeof(byte), actual.GetProperty("byte").PropertyType);
            Assert.AreEqual<Type>(typeof(byte), actual.GetProperty("bytenull").PropertyType);
            Assert.AreEqual<Type>(typeof(byte[]), actual.GetProperty("bytearray").PropertyType);
            Assert.AreEqual<Type>(typeof(bool), actual.GetProperty("bool").PropertyType);
            Assert.AreEqual<Type>(typeof(bool), actual.GetProperty("boolnull").PropertyType);
            Assert.AreEqual<Type>(typeof(DateTime), actual.GetProperty("datetime").PropertyType);
            Assert.AreEqual<Type>(typeof(DateTime), actual.GetProperty("datetimenull").PropertyType);
            Assert.AreEqual<Type>(typeof(TimeSpan), actual.GetProperty("timespan").PropertyType);
            Assert.AreEqual<Type>(typeof(TimeSpan), actual.GetProperty("timespannull").PropertyType);
        }

        [TestMethod()]
        public void CreateTypeTest_WithNullable()
        {
            List<String> colNames = new List<String>
            {
                "String",
                "int",
                "intnull",
                "float",
                "floatnull",
                "short",
                "shortnull",
                "double",
                "doublenull",
                "decimal",
                "decimalnull",
                "byte",
                "bytenull",
                "bool",
                "boolnull",
                "bytearray",
                "datetime",
                "datetimenull",
                "timespan",
                "timespannull",
                "String2"
            };

            List<Type> colTypes = new List<Type>
            {
                typeof(String),
                typeof(int),
                typeof(int),
                typeof(float),
                typeof(float),
                typeof(short),
                typeof(short),
                typeof(double),
                typeof(double),
                typeof(decimal),
                typeof(decimal),
                typeof(byte),                
                typeof(byte),
                typeof(bool),
                typeof(bool),
                typeof(byte[]),
                typeof(DateTime),
                typeof(DateTime),
                typeof(TimeSpan),
                typeof(TimeSpan),
                typeof(String),
            };

            var rows = new List<Object[]> { new Object[] { "", 5, null, 5f, null, 5, null, 5, null, 5, null, 5, null, true, null, new Byte[] { }, DateTime.Now, null, DateTime.Now.TimeOfDay, null, "2" } };

            var actual = rows.GetRuntimeType("TestWithNulls", colNames, colTypes, null);
            var ps = actual.GetProperties();
            Assert.AreEqual<int>(22, ps.Length);
            Assert.AreEqual<Type>(typeof(String), actual.GetProperty("String").PropertyType);
            Assert.AreEqual<Type>(typeof(String), actual.GetProperty("String2").PropertyType);
            Assert.AreEqual<Type>(typeof(int), actual.GetProperty("int").PropertyType);
            Assert.AreEqual<Type>(typeof(int?), actual.GetProperty("intnull").PropertyType);
            Assert.AreEqual<Type>(typeof(float), actual.GetProperty("float").PropertyType);
            Assert.AreEqual<Type>(typeof(float?), actual.GetProperty("floatnull").PropertyType);
            Assert.AreEqual<Type>(typeof(short), actual.GetProperty("short").PropertyType);
            Assert.AreEqual<Type>(typeof(short?), actual.GetProperty("shortnull").PropertyType);
            Assert.AreEqual<Type>(typeof(double), actual.GetProperty("double").PropertyType);
            Assert.AreEqual<Type>(typeof(double?), actual.GetProperty("doublenull").PropertyType);
            Assert.AreEqual<Type>(typeof(decimal), actual.GetProperty("decimal").PropertyType);
            Assert.AreEqual<Type>(typeof(decimal?), actual.GetProperty("decimalnull").PropertyType);
            Assert.AreEqual<Type>(typeof(byte), actual.GetProperty("byte").PropertyType);
            Assert.AreEqual<Type>(typeof(byte?), actual.GetProperty("bytenull").PropertyType);
            Assert.AreEqual<Type>(typeof(byte[]), actual.GetProperty("bytearray").PropertyType);
            Assert.AreEqual<Type>(typeof(bool), actual.GetProperty("bool").PropertyType);
            Assert.AreEqual<Type>(typeof(bool?), actual.GetProperty("boolnull").PropertyType);
            Assert.AreEqual<Type>(typeof(DateTime), actual.GetProperty("datetime").PropertyType);
            Assert.AreEqual<Type>(typeof(DateTime?), actual.GetProperty("datetimenull").PropertyType);
            Assert.AreEqual<Type>(typeof(TimeSpan), actual.GetProperty("timespan").PropertyType);
            Assert.AreEqual<Type>(typeof(TimeSpan?), actual.GetProperty("timespannull").PropertyType);
        }
    }
}