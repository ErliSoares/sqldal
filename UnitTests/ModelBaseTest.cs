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
using System.Data.DBAccess.Generic.Exceptions;
using System.Data.DBAccess.Generic.Providers.SQL;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass()]
    public class ModelBaseTest
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

        public class ClassWithNullableValueType
        {
            [DALDefaultValue(Value = 10)]
            public int? NullableValueType { get; set; }
            public int NonNullableValueType { get; set; }
            private int PrivateProperty { get; set; }

            [DALParameterDirectionAttribute(Direction = ParameterDirection.Input)]
            public int InputParameter { get; set; }

            [DALParameterDirectionAttribute(Direction = ParameterDirection.Output)]
            public int OutputParameter { get; set; }

            [DALWriteStringFormat(Format = "{0:MM/dd/yyyy hh:mm:ss}")]
            public String Time { get; set; }
        }

        #region IsNullableValueTypeTest
        [TestMethod()]
        public void IsNullableValueTypeTest_True()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            Boolean actual = db.IsNullableValueType(target.GetType(), "NullableValueType", ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)]);
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void IsNullableValueTypeTest_False()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            Boolean actual = db.IsNullableValueType(target.GetType(), "NonNullableValueType", ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)]);
            Assert.AreEqual(false, actual);
        }
        #endregion

        #region GetValueTest
        [TestMethod()]
        public void GetValueTest_NullableValueType_NotNull()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType
            {
                NullableValueType = 5
            };
            object actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetValue(target, target.GetType(), ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)], ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)].FastDynamicAccess, "NullableValueType", null);
            Assert.AreEqual(5, (int)actual);
        }

        [TestMethod()]
        public void GetValueTest_NullableValueType_Null()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType
            {
                NullableValueType = null
            };
            object actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetValue(target, target.GetType(), ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)], ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)].FastDynamicAccess, "NullableValueType", null);
            Assert.AreEqual(null, actual);
        }

        [TestMethod()]
        public void GetValueTest_NullableValueType_Default()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            object actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetValue(target, target.GetType(), ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)], ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)].FastDynamicAccess, "NullableValueType", null);
            Assert.AreEqual(null, actual);
        }

        [TestMethod()]
        public void GetValueTest_NonNullableValueType()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType
            {
                NonNullableValueType = 6
            };
            object actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetValue(target, target.GetType(), ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)], ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)].FastDynamicAccess, "NonNullableValueType", null);
            Assert.AreEqual(6, (int)actual);
        }

        public class ClassWithCustomPropertyReturn
        {
            public String Hello
            {
                get { return "Hello World"; }
            }
        }

        [TestMethod()]
        public void GetValueTest_PropertyCustomReturn()
        {
            ClassWithCustomPropertyReturn target = new ClassWithCustomPropertyReturn();
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            object actual = db.GetValue(target, target.GetType(), ((IDBAccess)db).ModelsData[typeof(ClassWithCustomPropertyReturn)], ((IDBAccess)db).ModelsData[typeof(ClassWithCustomPropertyReturn)].FastDynamicAccess, "Hello", null);
            Assert.AreEqual("Hello World", (String)actual);
        }

        public class ClassWithPrivateGet
        {
            public int Test { private get; set; }
            public String MyString { get; set; }
        }

        [TestMethod()]
        public void PrivateGetAccessor_Test_Fail()
        {
            var target = new ClassWithPrivateGet();
            target.Test = 5;
            target.MyString = "Hello World";

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var ps = db.PrepareParameters(target, true).ToList();
            Assert.IsTrue(ps.Count == 1);
            Assert.AreEqual(target.MyString, ps[0].Value);
        }

        public class ClassWithProtectedGet
        {
            public int Test { protected get; set; }
            public String MyString { get; set; }
        }

        [TestMethod()]
        public void ProtectedGetAccessor_Test_Fail()
        {
            var target = new ClassWithProtectedGet();
            target.Test = 5;
            target.MyString = "Hello World";

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var ps = db.PrepareParameters(target, true).ToList();
            Assert.IsTrue(ps.Count == 1);
            Assert.AreEqual(target.MyString, ps[0].Value);
        }

        public class ClassWithInternalGet
        {
            public int Test { internal get; set; }
        }

        [TestMethod()]
        public void InternalGetAccessor_Test()
        {
            var target = new ClassWithInternalGet();
            target.Test = 5;

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            Assert.AreEqual(5, (int)db.GetValue(target, target.GetType(), ((IDBAccess)db).ModelsData[typeof(ClassWithInternalGet)], ((IDBAccess)db).ModelsData[typeof(ClassWithInternalGet)].FastDynamicAccess, "Test", null));
        }

        public class ClassWithPublicGet
        {
            public int Test { get; set; }
        }

        [TestMethod()]
        public void PublicGetAccessor_Test()
        {
            var target = new ClassWithPublicGet();
            target.Test = 5;

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            Assert.AreEqual(5, (int)db.GetValue(target, target.GetType(), ((IDBAccess)db).ModelsData[typeof(ClassWithPublicGet)], ((IDBAccess)db).ModelsData[typeof(ClassWithPublicGet)].FastDynamicAccess, "Test", null));
        }
        #endregion

        #region GetPropertyTypeTest
        [TestMethod()]
        public void GetPropertyTypeTest_NullableValueType()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            Type expected = typeof(int?);
            Type actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetPropertyType(target.GetType(), "NullableValueType", ((IDBAccess)db).ModelsData[target.GetType()]);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetPropertyTypeTest_NonNullableValueType()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            Type expected = typeof(int);
            Type actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetPropertyType(target.GetType(), "NonNullableValueType", ((IDBAccess)db).ModelsData[target.GetType()]);
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region GetPropertyTest
        [TestMethod()]
        public void GetPropertyTest_NullableValueType_True()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            String property = "NullableValueType";
            FieldInfo expected = target.GetType().GetField(String.Format("<{0}>k__BackingField", property), BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetFieldByPropertyName(target.GetType(), property, ((IDBAccess)db).ModelsData[target.GetType()]);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetPropertyTest_NonNullableValueType_True()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            String property = "NonNullableValueType";
            FieldInfo expected = target.GetType().GetField(String.Format("<{0}>k__BackingField", property), BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetFieldByPropertyName(target.GetType(), property, ((IDBAccess)db).ModelsData[target.GetType()]);
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region GetNullableValueTypeValueTest
        [TestMethod()]
        public void GetNullableValueTypeValueTest_NotNull()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType
            {
                NullableValueType = 5
            };
            object actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetNullableValueTypeValue(target, "NullableValueType", ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)]);
            Assert.AreEqual(5, (int)actual);
        }

        [TestMethod()]
        public void GetNullableValueTypeValueTest_Null()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType
            {
                NullableValueType = null
            };
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            object actual;
            actual = db.GetNullableValueTypeValue(target, "NullableValueType", ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)]);
            Assert.AreEqual(null, actual);
        }

        [TestMethod()]
        public void GetNullableValueTypeValueTest_Default()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            object actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetNullableValueTypeValue(target, "NullableValueType", ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)]);
            Assert.AreEqual(null, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNullableValueTypeValueTest_NonNullableValueType()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            db.GetNullableValueTypeValue(target, "NonNullableValueType", ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)]);
        }
        #endregion

        #region PopulateTest
        [TestMethod()]
        public void PopulateTest()
        {
            var db = new SqlDBAccess();
            db.ValidateForDAL(new ClassWithNullableValueType());

            DateTime now = DateTime.Now;

            var tuple = new ExecuteReadQuickTuple
            {
                ColumnNames = new List<String>
                {
                    "NonNullableValueType",
                    "InputParameter",
                    "Time"
                },
                ColumnTypes = new List<Type>
                {
                    typeof(int),
                    typeof(int),
                    typeof(DateTime)
                },
                DataRows = new List<Object[]> { new Object [] { 5, 6, now } }
            };

            var target = db.PopulateModelBaseEnumeration<ClassWithNullableValueType>(tuple)[0];

            Assert.AreEqual<int>(target.NonNullableValueType, 5);
            Assert.AreEqual<int>(target.InputParameter, 6);
            Assert.AreEqual<String>(target.Time, String.Format("{0:MM/dd/yyyy hh:mm:ss}", now));
        }

        [TestMethod()]
        public void PopulateDALDefaultValuesTest()
        {
            DateTime now = DateTime.Now;

            var db = new SqlDBAccess();
            db.PopulateDefaultValues = true;

            var tuple = new ExecuteReadQuickTuple
            {
                ColumnNames = new List<String>
                {
                    "NonNullableValueType",
                    "InputParameter",
                    "Time"
                },
                ColumnTypes = new List<Type>
                {
                    typeof(int),
                    typeof(int),
                    typeof(DateTime)
                },
                DataRows = new List<Object[]> { new Object [] { 5, 6, now } }
            };

            var target = db.PopulateModelBaseEnumeration<ClassWithNullableValueType>(tuple)[0];

            Assert.AreEqual<int?>(10, target.NullableValueType);
            Assert.AreEqual<int>(5, target.NonNullableValueType);
            Assert.AreEqual<int>(6, target.InputParameter);
            Assert.AreEqual<String>(String.Format("{0:MM/dd/yyyy hh:mm:ss}", now), target.Time);
        }

        public class DALDefaultValueWithDifferentSprocName
        {
            public int ID { get; set; }
            public int Amount { get; set; }
            [DALSQLParameterName(Name = "CustomerName")]
            [DALDefaultValue(Value = "Brian")]
            public String Name { get; set; }
        }

        [TestMethod()]
        public void PopulateDALDefaultValuesTest2()
        {
            DateTime now = DateTime.Now;

            var tuple = new ExecuteReadQuickTuple
            {
                ColumnNames = new List<String>
                {
                    "ID"
                },
                ColumnTypes = new List<Type>
                {
                    typeof(int)
                },
                DataRows = new List<Object[]> { new Object [] { 5 } }
            };

            var db = new SqlDBAccess();
            db.PopulateDefaultValues = true;

            var target = db.PopulateModelBaseEnumeration<DALDefaultValueWithDifferentSprocName>(tuple)[0];

            Assert.AreEqual<String>(target.Name, "Brian");
        }

        public class ModelWithNestedModelsPopulate
        {
            public String String1 { get; set; }
            public int Int1 { get; set; }
            public NestedModelPopulate1 Nest1 { get; set; }
        }

        public class NestedModelPopulate1
        {
            public String GreetingString { get; set; }
            public NestedModelPopulate2 Nest2 { get; set; }
        }

        public class NestedModelPopulate2
        {
            public String InternalString { get; set; }
            public int InternalInt { get; set; }
            public NestedModelPopulate3 Nest3 { get; set; }
        }

        public class NestedModelPopulate3
        {
            public String NewString { get; set; }
            public int NewInt { get; set; }
            public NestedModelPopulate4 Nest4 { get; set; }
        }

        public class NestedModelPopulate4
        {
            public String NewString2 { get; set; }
            public int NewInt2 { get; set; }
        }

        [TestMethod()]
        public void NestedModelBasePopulateTest()
        {
            String String1 = "This is String1";
            int Int1 = 1;
            String GreetingString = "This is GreetingString";
            String InternalString = "This is InternalString";
            int InternalInt = 500;
            String NewString = "This is NewString";
            int NewInt = -1;
            String NewString2 = "This is NewString2";
            int NewInt2 = -2;
            
            var tuple = new ExecuteReadQuickTuple
            {
                ColumnNames = new List<String>
                {
                    "String1",
                    "Int1",
                    "GreetingString",
                    "InternalString",
                    "InternalInt",
                    "NewString",
                    "NewInt",
                    "NewString2",
                    "NewInt2"
                },
                ColumnTypes = new List<Type>
                {
                    typeof(String),
                    typeof(int),
                    typeof(String),
                    typeof(String),
                    typeof(int),
                    typeof(String),
                    typeof(int),
                    typeof(String),
                    typeof(int)
                },
                DataRows = new List<Object[]> { new Object [] { String1, Int1, GreetingString, InternalString, InternalInt, NewString, NewInt, NewString2, NewInt2 } }
            };

            var db = new SqlDBAccess();

            var target = db.PopulateModelBaseEnumeration<ModelWithNestedModelsPopulate>(tuple)[0];

            Assert.AreEqual<String>(String1, target.String1);
            Assert.AreEqual<int>(Int1, target.Int1);
            Assert.AreEqual<String>(GreetingString, target.Nest1.GreetingString);
            Assert.AreEqual<String>(InternalString, target.Nest1.Nest2.InternalString);
            Assert.AreEqual<int>(InternalInt, target.Nest1.Nest2.InternalInt);
            Assert.AreEqual<String>(NewString, target.Nest1.Nest2.Nest3.NewString);
            Assert.AreEqual<int>(NewInt, target.Nest1.Nest2.Nest3.NewInt);
            Assert.AreEqual<String>(NewString2, target.Nest1.Nest2.Nest3.Nest4.NewString2);
            Assert.AreEqual<int>(NewInt2, target.Nest1.Nest2.Nest3.Nest4.NewInt2);
        }

        public class NestedModelCustomConstructor
        {
            public int FirstInt { get; set; }
            public NestedModelCustomConstructorNest Nest { get; set; }

            public NestedModelCustomConstructor()
            {
                this.Nest = new NestedModelCustomConstructorNest
                {
                    TheInt = 5,
                    CustomString = "Hello World"
                };
            }
        }

        public class NestedModelCustomConstructorNest
        {
            public int TheInt { get; set; }
            public String CustomString { get; set; }
        }

        [TestMethod()]
        public void NestedPopulateCustomConstructor()
        {
            var tuple = new ExecuteReadQuickTuple
            {
                ColumnNames = new List<String> { "FirstInt", "TheInt" },
                ColumnTypes = new List<Type> { typeof(int), typeof(int) },
                DataRows = new List<Object[]> { new Object [] { 5, 6 }, new Object [] { 8, 7 } }
            };

            var result = new SqlDBAccess().PopulateModelBaseEnumeration<NestedModelCustomConstructor>(tuple);

            Assert.AreEqual<int>(5, result[0].FirstInt);
            Assert.AreEqual<int>(8, result[1].FirstInt);
            Assert.AreEqual<int>(6, result[0].Nest.TheInt);
            Assert.AreEqual<int>(7, result[1].Nest.TheInt);
            Assert.AreEqual<String>("Hello World", result[0].Nest.CustomString);
            Assert.AreEqual<String>("Hello World", result[1].Nest.CustomString);
        }

        public class ModelWithNestedModelsPopulateIgnoreOne
        {
            public String String1 { get; set; }
            public int Int1 { get; set; }
            public NestedModelPopulateIgnoreOne1 Nest1 { get; set; }
        }

        public class NestedModelPopulateIgnoreOne1
        {
            public String GreetingString { get; set; }
            [DALIgnore]
            public String HelloWorldString { get; set; }
        }

        [TestMethod()]
        public void NestedModelBasePopulateIgnoreOneTest()
        {
            String String1 = "This is String1";
            int Int1 = 1;
            String GreetingString = "This is GreetingString";
            String HelloWorldString = "This is HelloWorldString";
            
            var tuple = new ExecuteReadQuickTuple
            {
                ColumnNames = new List<String>
                {
                    "String1",
                    "Int1",
                    "GreetingString",
                    "HelloWorldString"
                },
                ColumnTypes = new List<Type>
                {
                    typeof(String),
                    typeof(int),
                    typeof(String),
                    typeof(String)
                },
                DataRows = new List<Object[]> { new Object[] { String1, Int1, GreetingString, HelloWorldString } }
            };

            var db = new SqlDBAccess();

            var target = db.PopulateModelBaseEnumeration<ModelWithNestedModelsPopulateIgnoreOne>(tuple)[0];

            Assert.AreEqual<String>(String1, target.String1);
            Assert.AreEqual<int>(Int1, target.Int1);
            Assert.AreEqual<String>(GreetingString, target.Nest1.GreetingString);
            Assert.AreEqual<String>(null, target.Nest1.HelloWorldString);
        }

        [TestMethod()]
        public void ModelWithWriteOnlyPropertiesPopulateTest()
        {
            int ID = 5;
            String WriteOnlyString = "Can't read me";
            Decimal Amount = 56.78m;

            var db = new SqlDBAccess();

            var tuple = new ExecuteReadQuickTuple
            {
                ColumnNames = new List<String>
                {
                    "ID",
                    "WriteOnlyString",
                    "Amount"
                },
                ColumnTypes = new List<Type>
                {
                    typeof(int),
                    typeof(String),
                    typeof(Decimal)
                },
                DataRows = new List<Object[]> { new Object[] { ID, WriteOnlyString, Amount } }
            };

            var target = db.PopulateModelBaseEnumeration<ModelWithWriteOnlyParameters>(tuple)[0];

            Assert.AreEqual(ID, target.ID);
            Assert.AreEqual(WriteOnlyString, target.writeOnlyString);
            Assert.AreEqual(Amount, target.Amount);
        }

        public class ModelWithReadOnlyProperties
        {
            public int ID { get; set; }
            internal String readOnlyString;
            public String ReadOnlyString { get { return readOnlyString; } }
            public Decimal Amount { get; set; }
        }

        [TestMethod()]
        public void ModelWithReadOnlyPropertiesPopulateTest_Pass1()
        {
            int ID = 5;
            String ReadOnlyString = "Can't read me";
            Decimal Amount = 56.78m;

            var db = new SqlDBAccess();

            var tuple = new ExecuteReadQuickTuple
            {
                ColumnNames = new List<String>
                {
                    "ID",
                    "ReadOnlyString",
                    "Amount"
                },
                ColumnTypes = new List<Type>
                {
                    typeof(int),
                    typeof(String),
                    typeof(Decimal)
                },
                DataRows = new List<Object[]> { new Object[] { ID, ReadOnlyString, Amount } }
            };

            var target = db.PopulateModelBaseEnumeration<ModelWithReadOnlyProperties>(tuple)[0];
        }

        public class ModelWithReadOnlyPropertiesNotInput
        {
            public int ID { get; set; }
            internal String readOnlyString;
            [DALIgnoreAttribute]
            public String ReadOnlyString { get { return readOnlyString; } }
            public Decimal Amount { get; set; }
        }

        [TestMethod()]
        public void ModelWithReadOnlyPropertiesPopulateTest_Pass2()
        {
            int ID = 5;
            String ReadOnlyString = "Can't read me";
            Decimal Amount = 56.78m;

            var db = new SqlDBAccess();

            var tuple = new ExecuteReadQuickTuple
            {
                ColumnNames = new List<String>
                {
                    "ID",
                    "ReadOnlyString",
                    "Amount"
                },
                ColumnTypes = new List<Type>
                {
                    typeof(int),
                    typeof(String),
                    typeof(Decimal)
                },
                DataRows = new List<Object[]> { new Object[] { ID, ReadOnlyString, Amount } }
            };

            var target = db.PopulateModelBaseEnumeration<ModelWithReadOnlyPropertiesNotInput>(tuple)[0];
        }
        #endregion

        #region Nested ModelBase parameter prepare
        [TestMethod()]
        public void PrepareNestedModelBase1Pass()
        {
            var target = new NestedPropertyClassAPass
            {
                String1 = "String1",
                String2 = "String2",
                ClassB = new NestedPropertyClassBPass
                {
                    String3 = "String3",
                    String4 = "String4",
                    ClassC = new NestedPropertyClassCPass
                    {
                        String5 = "String5",
                        String6 = "String6"
                    }
                }
            };

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var parameters = db.PrepareParameters(target, true).ToDictionary(p => p.ParameterName, p => p);
            Assert.IsTrue(parameters.ContainsKey("@iString1"));
            Assert.AreEqual("String1", parameters["@iString1"].Value.ToString());
            Assert.IsTrue(parameters.ContainsKey("@iString2"));
            Assert.AreEqual("String2", parameters["@iString2"].Value.ToString());
            Assert.IsTrue(parameters.ContainsKey("@iString3"));
            Assert.AreEqual("String3", parameters["@iString3"].Value.ToString());
            Assert.IsTrue(parameters.ContainsKey("@iString4"));
            Assert.AreEqual("String4", parameters["@iString4"].Value.ToString());
            Assert.IsTrue(!parameters.ContainsKey("@iString5"));
            Assert.IsTrue(parameters.ContainsKey("@iString6"));
            Assert.AreEqual("String6", parameters["@iString6"].Value.ToString());
        }

        [TestMethod()]
        public void PrepareNestedModelBase2Pass()
        {
            var target = new NestedPropertyClassASprocParamNameChangePass
            {
                String1 = "String1",
                String2 = "String2",
                ClassB = new NestedPropertyClassBSprocParamNameChangePass
                {
                    String2 = "String4",
                    String3 = "String3",
                    ClassC = new NestedPropertyClassCSprocParamNameChangePass
                    {
                        String2 = "String5",
                        String3 = "String6"
                    }
                }
            };

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var parameters = db.PrepareParameters(target, true).ToDictionary(p => p.ParameterName, p => p);
            Assert.IsTrue(parameters.ContainsKey("@iString1"));
            Assert.AreEqual("String1", parameters["@iString1"].Value.ToString());
            Assert.IsTrue(parameters.ContainsKey("@iString2"));
            Assert.AreEqual("String2", parameters["@iString2"].Value.ToString());
            Assert.IsTrue(parameters.ContainsKey("@iString3"));
            Assert.AreEqual("String3", parameters["@iString3"].Value.ToString());
            Assert.IsTrue(parameters.ContainsKey("@iString4"));
            Assert.AreEqual("String4", parameters["@iString4"].Value.ToString());
            Assert.IsTrue(parameters.ContainsKey("@oString5"));
            Assert.AreEqual("String5", parameters["@oString5"].Value.ToString());
            Assert.AreEqual<ParameterDirection>(ParameterDirection.Output, parameters["@oString5"].Direction);
            Assert.IsTrue(parameters.ContainsKey("@iString6"));
            Assert.AreEqual("String6", parameters["@iString6"].Value.ToString());
        }
        #endregion

        #region Prepare Parameters
        public class ModelWithNullValues
        {
            public int? NullInt { get; set; }
            public int NonNullInt { get; set; }
            public String TheString { get; set; }
        }

        [TestMethod()]
        public void PrepareParametersWithNullValues_Test()
        {
            var target = new ModelWithNullValues
            {
                NullInt = null,
                NonNullInt = 5,
                TheString = null
            };

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var p = db.PrepareParameters(target, true).ToList();

            Assert.AreEqual(null, p[0].Value);
            Assert.AreEqual(5, (int)p[1].Value);
            Assert.AreEqual(null, p[2].Value);
        }

        public class ModelWithWriteOnlyParameters
        {
            public int ID { get; set; }
            internal String writeOnlyString;
            public String WriteOnlyString { set { writeOnlyString = value; } }
            public Decimal Amount { get; set; }
        }

        [TestMethod()]
        public void PrepareParametersWithWriteOnlyProperties_Test()
        {
            var target = new ModelWithWriteOnlyParameters
            {
                ID = 5,
                WriteOnlyString = "Can't read me",
                Amount = 56.78m
            };

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var ps = db.PrepareParameters(target, true).ToList();
            Assert.AreEqual<int>(2, ps.Count);
            Assert.AreEqual<int>(5, (int)ps[0].Value);
            Assert.AreEqual<String>("56.78", ps[1].Value.ToString());
        }

        [TestMethod()]
        public void PrepareParametersWithStringFormatRead_Test()
        {
            var target = new ClassWithStringFormatRead
            {
                ID = 5,
                Name = "Brian23"
            };

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var ps = db.PrepareParameters(target, true).ToList();
            Assert.AreEqual<int>(2, ps.Count);
            Assert.AreEqual<int>(5, (int)ps[0].Value);
            Assert.AreEqual<String>("Hello, Brian23.", ps[1].Value.ToString());
        }

        public class MyUDTable
        {
            public int ID { get; set; }
            public String Name { get; set; }
            public Decimal Amount { get; set; }
        }

        [TestMethod()]
        public void PrepareTypeWithUDTable()
        {
            var data = new List<MyUDTable>
            {
                new MyUDTable
                {
                    ID = 1,
                    Name = "Brian",
                    Amount = 50.67m
                },
                new MyUDTable
                {
                    ID = 3,
                    Name = "Matt",
                    Amount = 27.36m
                }
            };

            var model = new
            {
                Date = DateTime.Now.Date,
                Custs = data
            };

            var db = new SqlDBAccess();
            var ps = db.PrepareParameters(model).ToList();

            Assert.AreEqual<int>(2, ps.Count());
            Assert.AreEqual<Type>(typeof(DateTime), ps[0].Value.GetType());
            Assert.AreEqual<SqlDbType>(SqlDbType.Structured, ps[1].SqlDbType);
            Assert.AreEqual<Type>(typeof(DataTable), ps[1].Value.GetType());

            var table = (ps[1].Value as DataTable).AsEnumerable().ToList();

            Assert.AreEqual<int>(1, (int)table[0][0]);
            Assert.AreEqual<String>("Brian", (String)table[0][1]);
            Assert.AreEqual<Decimal>(50.67m, (Decimal)table[0][2]);
            Assert.AreEqual<int>(3, (int)table[1][0]);
            Assert.AreEqual<String>("Matt", (String)table[1][1]);
            Assert.AreEqual<Decimal>(27.36m, (Decimal)table[1][2]);
        }

        [TestMethod()]
        public void PrepareNestedParameters()
        {
            var target = new ModelWithNestedModelsPopulateIgnoreOne
            {
                Int1 = 5,
                String1 = "Hello String",
                Nest1 = new NestedModelPopulateIgnoreOne1
                {
                    GreetingString = "GreetingString",
                    HelloWorldString = "HelloWorldString"
                }
            };

            var db = new SqlDBAccess();
            var ps = db.PrepareParameters(target, true).ToList();
            Assert.AreEqual(3, ps.Count);
            Assert.AreEqual("@iString1", ps[0].ParameterName);
            Assert.AreEqual("@iInt1", ps[1].ParameterName);
            Assert.AreEqual("@iGreetingString", ps[2].ParameterName);
            Assert.AreEqual("Hello String", ps[0].Value.ToString());
            Assert.AreEqual(5, ps[1].Value);
            Assert.AreEqual("GreetingString", ps[2].Value.ToString());
        }

        public class ModelWithInheritedParameters : InheritedModelParameters
        {
            public int Int1 { get; set; }
            public String String1 { get; set; }
        }

        public class InheritedModelParameters
        {
            public String GreetingString { get; set; }
            public String HelloWorldString { get; set; }
        }

        [TestMethod()]
        public void PrepareInheritedParameters()
        {
            var target = new ModelWithInheritedParameters
            {
                Int1 = 5,
                String1 = "Hello String",
                GreetingString = "GreetingString",
                HelloWorldString = "HelloWorldString"
            };

            var db = new SqlDBAccess();
            var ps = db.PrepareParameters(target, true).ToList();
            Assert.AreEqual<int>(4, ps.Count);
            Assert.AreEqual<String>("@iString1", ps[1].ParameterName);
            Assert.AreEqual<String>("@iInt1", ps[0].ParameterName);
            Assert.AreEqual<String>("@iGreetingString", ps[2].ParameterName);
            Assert.AreEqual<String>("@iHelloWorldString", ps[3].ParameterName);
            Assert.AreEqual<int>(5, (int)ps[0].Value);
            Assert.AreEqual<String>("Hello String", ps[1].Value.ToString());
            Assert.AreEqual<String>("GreetingString", ps[2].Value.ToString());
            Assert.AreEqual<String>("HelloWorldString", ps[3].Value.ToString());
        }
        #endregion

        #region SetValue
        [TestMethod()]
        public void SetValueTest_NullableType_NotNullValue()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType
            {
                NullableValueType = 1
            };

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var fda = FastDynamicAccess.Get(target);
            String property = "NullableValueType";
            db.SetValue(target, target.GetType(), property, 5, ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)], true, typeof(int?), null, fda.PropertyToArrayIndex[property]);

            Assert.IsTrue(target.NullableValueType == 5);
        }

        [TestMethod()]
        public void SetValueTest_NullableType_NullValue()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType
            {
                NullableValueType = 1
            };

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var fda = FastDynamicAccess.Get(target);
            String property = "NullableValueType";
            db.SetValue(target, target.GetType(), property, null, ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)], true, typeof(int?), null, fda.PropertyToArrayIndex[property]);

            Assert.IsTrue(target.NullableValueType == null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyNotNullableException))]
        public void SetValueTest_NonNullableType_Null()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType
            {
                NonNullableValueType = 1
            };

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            String property = "NonNullableValueType";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property, null, ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)], true, typeof(int?), null, fda.PropertyToArrayIndex[property]);
        }

        [TestMethod()]
        public void SetValueTest_NonNullableType_NotNull()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType
            {
                NonNullableValueType = 1
            };

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            String property = "NonNullableValueType";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property, 6, ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)], true, typeof(int?), null, fda.PropertyToArrayIndex[property]);

            Assert.IsTrue(target.NonNullableValueType == 6);
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyColumnMismatchException))]
        public void SetValueTest_NonNullableType_InvalidTypeIntoModel()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType
            {
                NonNullableValueType = 1
            };

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            String property = "NonNullableValueType";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property, "BadType", ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)], true, typeof(int?), null, fda.PropertyToArrayIndex[property]);
        }

        [TestMethod()]
        public void SetValueTest_SetTimeString_FromDateTime()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();

            DateTime now = DateTime.Now;

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            String property = "Time";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property, now, ((IDBAccess)db).ModelsData[typeof(ClassWithNullableValueType)], true, typeof(DateTime), "{0:MM/dd/yyyy hh:mm:ss}", fda.PropertyToArrayIndex[property]);

            String actual = target.Time;
            String expected = String.Format("{0:MM/dd/yyyy hh:mm:ss}", now);

            Assert.AreEqual(expected, actual);
        }

        public class ModelWithString
        {
            public String HelloString { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyColumnMismatchException))]
        public void SetValueTest_NonStringTypeIntoStringTypeFail()
        {
            ModelWithString target = new ModelWithString();
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            String property = "HelloString";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property, 5, ((IDBAccess)db).ModelsData[typeof(ModelWithString)], true, typeof(String), null, fda.PropertyToArrayIndex[property]);
        }

        public class ModelWithDALStringFormat
        {
            [DALWriteStringFormat(Format = "{0}")]
            public String HelloString { get; set; }
        }

        public class ModelWithDALStringFormat2
        {
            [DALWriteStringFormat(Format = "Hello: {0}")]
            public String HelloString { get; set; }
        }

        [TestMethod()]
        public void SetValueTest_NonStringTypeIntoStringWithDALStringFormatTypePass()
        {
            ModelWithDALStringFormat target = new ModelWithDALStringFormat();
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            String property = "HelloString";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property, 5, ((IDBAccess)db).ModelsData[typeof(ModelWithDALStringFormat)], true, typeof(String), "{0}", fda.PropertyToArrayIndex[property]);

            Assert.AreEqual("5", target.HelloString);
        }

        [TestMethod()]
        public void SetValueTest_NulllIntoStringWithDALStringFormatTypePass()
        {
            var target = new ModelWithDALStringFormat2();
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            String property = "HelloString";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property, null, ((IDBAccess)db).ModelsData[typeof(ModelWithDALStringFormat2)], true, typeof(String), "Hello: {0}", fda.PropertyToArrayIndex[property]);

            Assert.AreEqual("Hello: ", target.HelloString);
        }

        public class SetProperty_CustomSetterIntTimesTwo
        {
            private int _test = 0;
            public int Test
            {
                get { return _test; }
                set { _test = value * 2; }
            }
        }

        [TestMethod()]
        public void SetProperty_CustomSetterIntTimesTwo_Test()
        {
            var target = new SetProperty_CustomSetterIntTimesTwo();
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            String property = "Test";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property, 5, ((IDBAccess)db).ModelsData[typeof(SetProperty_CustomSetterIntTimesTwo)], true, typeof(int), null, fda.PropertyToArrayIndex[property]);

            Assert.AreEqual(10, target.Test);
        }

        public class NestedModelWithCustomSetter
        {
            public SetProperty_CustomSetterIntTimesTwo TheModel { get; set; }
        }
        [TestMethod()]
        public void SetPropertyInNestedModelWithCustomSetter_CustomSetterIntTimesTwo_Test()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Test", typeof(int));
            dt.Rows.Add(5);

            var db = new SqlDBAccess();

            var tuple = new ExecuteReadQuickTuple
            {
                ColumnNames = new List<String>
                {
                    "Test"
                },
                ColumnTypes = new List<Type>
                {
                    typeof(int)
                },
                DataRows = new List<Object[]> { new Object[] { 5 } }
            };

            var target = db.PopulateModelBaseEnumeration<NestedModelWithCustomSetter>(tuple)[0];

            Assert.AreEqual<int>(10, target.TheModel.Test);
        }


        public class SetProperty_NoPublicSetter
        {
            internal int _test = 0;
            public int Test
            {
                get { return _test; }
                private set { _test = value * 2; }
            }

            public String MyString { get; set; }
        }

        [TestMethod()]
        public void SetProperty_NoPublicSetter_Fail_Test()
        {
            var target = new SetProperty_NoPublicSetter();
            var db = new SqlDBAccess();

            String str = "Hello World";

            db.ValidateForDAL(target);
            String property1 = "Test";
            String property2 = "MyString";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property1, 5, ((IDBAccess)db).ModelsData[typeof(SetProperty_NoPublicSetter)], false, typeof(int), null, fda.PropertyToArrayIndex[property1]);
            db.SetValue(target, target.GetType(), property2, str, ((IDBAccess)db).ModelsData[typeof(SetProperty_NoPublicSetter)], true, typeof(String), null, fda.PropertyToArrayIndex[property2]);

            Assert.AreEqual(0, target._test);
            Assert.AreEqual(str, target.MyString);
        }

        public class SetProperty_NoPublicSetter2
        {
            public int Test { get; private set; }
            public String MyString { get; set; }
        }

        [TestMethod()]
        public void SetProperty_NoPublicSetter_Fail_Test2()
        {
            var target = new SetProperty_NoPublicSetter2();
            var db = new SqlDBAccess();

            String str = "Hello World";

            db.ValidateForDAL(target);
            String property1 = "Test";
            String property2 = "MyString";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property1, 5, ((IDBAccess)db).ModelsData[typeof(SetProperty_NoPublicSetter2)], false, typeof(int), null, fda.PropertyToArrayIndex[property1]);
            db.SetValue(target, target.GetType(),property2, str, ((IDBAccess)db).ModelsData[typeof(SetProperty_NoPublicSetter2)], true, typeof(String), null, fda.PropertyToArrayIndex[property2]);

            Assert.AreEqual(default(int), target.Test);
            Assert.AreEqual(str, target.MyString);
        }

        public class SetProperty_ProtectedSetter
        {
            public int Test { get; protected set; }
            public String MyString { get; set; }
        }

        [TestMethod()]
        public void SetProperty_ProtectedSetter_Fail_Test()
        {
            var target = new SetProperty_ProtectedSetter();
            var db = new SqlDBAccess();

            String str = "Hello World";

            db.ValidateForDAL(target);
            String property1 = "Test";
            String property2 = "MyString";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property1, 5, ((IDBAccess)db).ModelsData[typeof(SetProperty_ProtectedSetter)], false, typeof(int), null, fda.PropertyToArrayIndex[property1]);
            db.SetValue(target, target.GetType(), property2, str, ((IDBAccess)db).ModelsData[typeof(SetProperty_ProtectedSetter)], true, typeof(String), null, fda.PropertyToArrayIndex[property2]);

            Assert.AreEqual(0, target.Test);
            Assert.AreEqual(str, target.MyString);
        }

        public class SetProperty_PublicSetter
        {
            public int Test { get; set; }
        }

        [TestMethod()]
        public void SetProperty_PublicSetter_Fail()
        {
            var target = new SetProperty_PublicSetter();
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            String property = "Test";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property, 5, ((IDBAccess)db).ModelsData[typeof(SetProperty_PublicSetter)], true, typeof(int), null, fda.PropertyToArrayIndex[property]);

            Assert.AreEqual(5, target.Test);
        }

        public class SetProperty_InternalSetter
        {
            public int Test { get; internal set; }
        }

        [TestMethod()]
        public void SetProperty_InternalSetter_Fail()
        {
            var target = new SetProperty_InternalSetter();
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            String property = "Test";
            var fda = FastDynamicAccess.Get(target);
            db.SetValue(target, target.GetType(), property, 5, ((IDBAccess)db).ModelsData[typeof(SetProperty_InternalSetter)], true, typeof(int), null, fda.PropertyToArrayIndex[property]);

            Assert.AreEqual(5, target.Test);
        }
        #endregion

        #region Attribute Tests
        #region GetDirectionTest
        [TestMethod()]
        public void GetDirectionTest_Input()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            ParameterDirection expected = ParameterDirection.Input;
            ParameterDirection actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetPropertyDirection(target.GetType(), "InputParameter");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetDirectionTest_Output()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            ParameterDirection expected = ParameterDirection.Output;
            ParameterDirection actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetPropertyDirection(target.GetType(), "OutputParameter");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetDirectionTest_Default()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            ParameterDirection expected = ParameterDirection.Input;
            ParameterDirection actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetPropertyDirection(target.GetType(), "NullableValueType");
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region GetFormatTest
        [TestMethod()]
        public void GetFormatTest_Default()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            String expected = null;
            String actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetPropertyFormat(target.GetType(), "InputParameter");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetFormatTest_FormatIsSet()
        {
            ClassWithNullableValueType target = new ClassWithNullableValueType();
            String expected = "{0:MM/dd/yyyy hh:mm:ss}";
            String actual;
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            actual = db.GetPropertyFormat(target.GetType(), "Time");
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region DALIgnore
        public class ClassForNestedDALIgnore
        {
            public int ID { get; set; }
            public String Name { get; set; }

            [DALIgnore]
            public ClassToDALIgnore TheIgnoredClass { get; set; }
        }

        public class ClassToDALIgnore
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }

        [TestMethod()]
        public void ClassWithConflictingNestedDALIgnore_Test()
        {
            new SqlDBAccess().ValidateForDAL(new ClassForNestedDALIgnore());
        }
        #endregion
        #endregion

        #region ModelBase Validation Tests
        #region Conflicting property names tests
        public class TwoSameSprocNames
        {
            [DALSQLParameterName(Name = "Col3")]
            public String Col1 { get; set; }
            [DALSQLParameterName(Name = "Col3")]
            public String Col2 { get; set; }
        }

        public class PropertyAndSprocShareName
        {
            public String Col1 { get; set; }
            [DALSQLParameterName(Name = "Col1")]
            public String Col2 { get; set; }
        }

        public class SprocNamesPass
        {
            [DALSQLParameterName(Name = "Col3")]
            public String Col1 { get; set; }
            [DALSQLParameterName(Name = "Col4")]
            public String Col2 { get; set; }
            public String Col5 { get; set; }
        }

        public class SprocSameNamesButOneOutputFail
        {
            [DALSQLParameterName(Name = "Col4")]
            [DALParameterDirectionAttribute(Direction = ParameterDirection.Output)]
            public String Col1 { get; set; }
            [DALSQLParameterName(Name = "Col4")]
            public String Col2 { get; set; }
            public String Col5 { get; set; }
        }

        public class SprocSameNamesButOneOutputFail2
        {
            [DALSQLParameterName(Name = "Col4")]
            public String Col1 { get; set; }
            [DALParameterDirectionAttribute(Direction = ParameterDirection.Output)]
            public String Col4 { get; set; }
            public String Col5 { get; set; }
        }

        public class SprocSameNamesButOneIgnoredPass
        {
            [DALSQLParameterName(Name = "Col4")]
            [DALIgnoreAttribute]
            public String Col1 { get; set; }
            [DALSQLParameterName(Name = "Col4")]
            public String Col2 { get; set; }
            public String Col5 { get; set; }
        }

        public class SprocSameNamesButOneIgnoredPass2
        {
            [DALSQLParameterName(Name = "Col4")]
            public String Col1 { get; set; }
            [DALIgnoreAttribute]
            public String Col4 { get; set; }
            public String Col5 { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void SprocSameNamesButOneOutputFailTest()
        {
            new SqlDBAccess().ValidateForDAL(new SprocSameNamesButOneOutputFail());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void SprocSameNamesButOneOutputFailTest2()
        {
            new SqlDBAccess().ValidateForDAL(new SprocSameNamesButOneOutputFail2());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SprocSameNamesButOneIgnoredTest()
        {
            new SqlDBAccess().ValidateForDAL(new SprocSameNamesButOneIgnoredPass());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SprocSameNamesButOneIgnoredTest2()
        {
            new SqlDBAccess().ValidateForDAL(new SprocSameNamesButOneIgnoredPass2());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void TwoSameSprocNamesTest()
        {
            new SqlDBAccess().ValidateForDAL(new TwoSameSprocNames());
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void PropertyAndSprocShareNameTest()
        {
            new SqlDBAccess().ValidateForDAL(new PropertyAndSprocShareName());
        }

        [TestMethod()]
        public void SprocNamesPassTest()
        {
            new SqlDBAccess().ValidateForDAL(new SprocNamesPass());
            Assert.IsTrue(true);
        }
        #endregion

        #region DALStringFormat tests
        public class DALStringFormatFail
        {
            [DALWriteStringFormat(Format = "{0:n0}")]
            public DateTime Col1 { get; set; }
            [DALWriteStringFormat(Format = "{0:n0}")]
            public String Col2 { get; set; }
        }

        public class DALReadStringFormatPass
        {
            [DALReadStringFormat(Format = "{0:n0}")]
            public int Col1 { get; set; }
            [DALReadStringFormat(Format = "{0:n0}")]
            public Decimal Col2 { get; set; }
        }

        public class DALReadStringFormatPassNullable
        {
            [DALReadStringFormat(Format = "{0:n0}")]
            public int Col1 { get; set; }
            [DALReadStringFormat(Format = "{0:n0}")]
            public Decimal? Col2 { get; set; }
        }

        public class DALStringFormatPass
        {
            [DALWriteStringFormat(Format = "{0:n0}")]
            public String Col1 { get; set; }
            [DALWriteStringFormat(Format = "{0:n0}")]
            public String Col2 { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void DALStringFormatFailTest()
        {
            new SqlDBAccess().ValidateForDAL(new DALStringFormatFail());
        }

        [TestMethod()]
        public void DALStringFormatPassTest()
        {
            new SqlDBAccess().ValidateForDAL(new DALStringFormatPass());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void DALReadStringFormatPassTest()
        {
            var target = new DALReadStringFormatPass();
            target.Col1 = 55667;
            target.Col2 = 55667.8M;

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);

            var ps = db.PrepareParameters(target).ToList();
            Assert.IsTrue(ps.Count == 2);
            Assert.IsTrue(ps[0].Value.ToString() == "55,667");
            Assert.IsTrue(ps[1].Value.ToString() == "55,668");
        }

        [TestMethod()]
        public void DALReadStringFormatPassTest_NullableValues()
        {
            var target = new DALReadStringFormatPassNullable();
            target.Col1 = 55667;
            target.Col2 = null;

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);

            var ps = db.PrepareParameters(target).ToList();
            Assert.IsTrue(ps.Count == 2);
            Assert.IsTrue(ps[0].Value.ToString() == "55,667");
            Assert.IsTrue(ps[1].Value.ToString() == "");
        }
        #endregion

        #region DALDefaultValue tests
        public class DALDefaultValueNullFail
        {
            [DALDefaultValue(Value = null)]
            public String Col1 { get; set; }
            public String Col2 { get; set; }
        }

        public class DALDefaultValuePass
        {
            [DALDefaultValue(Value = "First Name")]
            public String Col1 { get; set; }
            public String Col2 { get; set; }
        }

        public class DALDefaultValueIncorrectTypeFail
        {
            [DALDefaultValue(Value = 5)]
            public String Col1 { get; set; }
            public String Col2 { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void DALDefaultValueNullFailTest()
        {
            new SqlDBAccess().ValidateForDAL(new DALDefaultValueNullFail());
        }

        [TestMethod()]
        public void DALDefaultValuePassTest()
        {
            new SqlDBAccess().ValidateForDAL(new DALDefaultValuePass());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void DALDefaultValueIncorrectTypeFailTest()
        {
            new SqlDBAccess().ValidateForDAL(new DALDefaultValueIncorrectTypeFail());
        }

        public class DALDefaultValueNestedTestParent
        {
            public int ID { get; set; }
            public String Name { get; set; }
            public DALDefaultValueNestedTestNest Nest { get; set; }
            [DALDefaultValue(Value = 5)]
            public int DefaultInt { get; set; }
        }

        public class DALDefaultValueNestedTestNest
        {
            [DALDefaultValue(Value = "Hello World")]
            public String HelloWorld { get; set; }
        }

        [TestMethod()]
        public void DALDefaultValueTest()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(String));

            var row = dt.Rows.Add(42, "Answer to everything.");

            var target = new DALDefaultValueNestedTestParent();
            Type type = typeof(DALDefaultValueNestedTestParent);
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var data = ((IDBAccess)db).ModelsData[type];
            var pData = db.GetPopulateData(dt.Rows[0], type);
            var dVData = db.GetDefaultValuesToPopulate(data, pData.ColUpperNames);
            var allNestedPData = db.GetAllNestedTypes(type).ToDictionary(t => t, t => db.GetPopulateData(dt.Rows[0], t));
            var allNestedDVData = db.GetAllNestedTypes(type).ToDictionary(t => t, t => db.GetDefaultValuesToPopulate(((IDBAccess)db).ModelsData[t], pData.ColUpperNames));

            //db.Populate(target, row, data, type, pData.MappedCols, pData.ColUpperNames, pData.ColCount, pData.HasSetters, pData.PropertyTypes, pData.PropertyFormats, allNestedPData, pData.FDAIndexes);
            db.PopulateDefaultModelValues(target, dVData, type, pData.ColUpperNames, data, allNestedDVData);

            Assert.AreEqual(5, target.DefaultInt);
        }

        [TestMethod()]
        public void DALDefaultValueNestedTest()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(String));

            var row = dt.Rows.Add(42, "Answer to everything.");

            var target = new DALDefaultValueNestedTestParent();
            Type type = typeof(DALDefaultValueNestedTestParent);
            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var data = ((IDBAccess)db).ModelsData[type];
            var pData = db.GetPopulateData(dt.Rows[0], typeof(DALDefaultValueNestedTestParent));
            var dVData = db.GetDefaultValuesToPopulate(data, pData.ColUpperNames);
            var allNestedPData = db.GetAllNestedTypes(type).ToDictionary(t => t, t => db.GetPopulateData(dt.Rows[0], t));
            var allNestedDVData = db.GetAllNestedTypes(type).ToDictionary(t => t, t => db.GetDefaultValuesToPopulate(((IDBAccess)db).ModelsData[t], pData.ColUpperNames));

            //db.Populate(target, row, data, type, pData.MappedCols, pData.ColUpperNames, pData.ColCount, pData.HasSetters, pData.PropertyTypes, pData.PropertyFormats, allNestedPData, pData.FDAIndexes);
            db.PopulateDefaultModelValues(target, dVData, type, pData.ColUpperNames, data, allNestedDVData);

            Assert.AreEqual("Hello World", target.Nest.HelloWorld);
        }
        #endregion

        #region Output Parameters
        public class OutputParameterPass
        {
            [DALParameterDirectionAttribute(Direction = ParameterDirection.Output)]
            public String Col1 { get; set; }
        }

        public class OutputParameterDataTableFail
        {
            [DALParameterDirectionAttribute(Direction = ParameterDirection.Output)]
            public DataTable Col1 { get; set; }
        }

        public class DerivesFromUDTable
        {

        }

        public class OutputParameterIEnumUDTableFail2
        {
            [DALParameterDirectionAttribute(Direction = ParameterDirection.Output)]
            public IEnumerable<DerivesFromUDTable> Col1 { get; set; }
        }

        [TestMethod()]
        public void OutputParameterPassTest()
        {
            new SqlDBAccess().ValidateForDAL(new OutputParameterPass());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void OutputParameterDataTableFailTest()
        {
            new SqlDBAccess().ValidateForDAL(new OutputParameterDataTableFail());
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void OutputParameterIEnumUDTableFail2Test()
        {
            new SqlDBAccess().ValidateForDAL(new OutputParameterIEnumUDTableFail2());
        }
        #endregion

        #region Enumerable parameters
        public class ModelWithByteArray
        {
            public int Id { get; set; }
            public Byte[] Data { get; set; }
        }

        public class NonUDTableEnumerationFail
        {
            public IEnumerable<int> Col1 { get; set; }
        }

        public class TestTable
        {
            public int Id { get; set; }
            public String Name { get; set; }
        }

        public class ModelWithIEnumUDTable
        {
            public IEnumerable<TestTable> Tables { get; set; }
        }

        public class ModelWithListUDTable
        {
            public List<TestTable> Tables { get; set; }
        }

        public class ModelWithArrayUDTable
        {
            public TestTable[] Tables { get; set; }
        }

        public class ModelWithListStrings
        {
            public List<String> Strings { get; set; }
        }

        public class ModelWithArrayStrings
        {
            public String[] Strings { get; set; }
        }

        public class ModelWithArrayStringsNonInputPass
        {
            public String HelloString { get; set; }
            [DALIgnoreAttribute]
            public String[] Strings { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyInvalidException))]
        public void NonUDTableEnumerationFailTest()
        {
            new SqlDBAccess().ValidateForDAL(new NonUDTableEnumerationFail());
        }

        [TestMethod()]
        public void ModelWithByteArrayTestPass()
        {
            new SqlDBAccess().ValidateForDAL(new ModelWithByteArray());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void ModelWithIEnumUDTableTestPass()
        {
            new SqlDBAccess().ValidateForDAL(new ModelWithIEnumUDTable());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void ModelWithListUDTableTestPass()
        {
            new SqlDBAccess().ValidateForDAL(new ModelWithListUDTable());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void ModelWithArrayUDTableTestPass()
        {
            new SqlDBAccess().ValidateForDAL(new ModelWithArrayUDTable());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyInvalidException))]
        public void NonUDTableListFailTest()
        {
            new SqlDBAccess().ValidateForDAL(new ModelWithListStrings());
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyInvalidException))]
        public void NonUDTableArrayFailTest()
        {
            new SqlDBAccess().ValidateForDAL(new ModelWithArrayStrings());
        }

        [TestMethod()]
        public void NonUDTableArrayNotInputPassTest()
        {
            new SqlDBAccess().ValidateForDAL(new ModelWithArrayStringsNonInputPass());
        }
        #endregion

        #region Circular reference tests
        public class NestedModelsAPass
        {
            public String NameA { get; set; }
            public NestedModelsBPass ModelB { get; set; }
        }

        public class NestedModelsBPass
        {
            public String NameB { get; set; }
            public NestedModelsCPass ModelC { get; set; }
        }

        public class NestedModelsCPass
        {
            public String NameC { get; set; }
            public NestedModelsDPass ModelD { get; set; }
        }

        public class NestedModelsDPass
        {
            public String NameD { get; set; }
        }

        [TestMethod()]
        public void CircularModelsTestPass()
        {
            new SqlDBAccess().ValidateForDAL(new NestedModelsAPass());
            Assert.IsTrue(true);
        }

        public class NestedModelsAFail4Deep
        {
            public String NameA { get; set; }
            public NestedModelsBFail4Deep ModelB { get; set; }
        }

        public class NestedModelsBFail4Deep
        {
            public String NameB { get; set; }
            public NestedModelsCFail4Deep ModelC { get; set; }
        }

        public class NestedModelsCFail4Deep
        {
            public String NameC { get; set; }
            public NestedModelsDFail4Deep ModelD { get; set; }
        }

        public class NestedModelsDFail4Deep : NestedModelsAFail4Deep
        {
            public String NameD { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void CircularModelsTestFail()
        {
            new SqlDBAccess().ValidateForDAL(new NestedModelsAFail4Deep());
        }

        public class NestedModelsAFail4Deep2
        {
            public String NameA { get; set; }
            public NestedModelsBFail4Deep ModelB { get; set; }
        }

        public class NestedModelsBFail4Deep2
        {
            public String NameB { get; set; }
            public NestedModelsCFail4Deep ModelC { get; set; }
        }

        public class NestedModelsCFail4Deep2
        {
            public String NameC { get; set; }
            public NestedModelsDFail4Deep ModelD { get; set; }
        }

        public class NestedModelsDFail4Deep2 : NestedModelsAFail4Deep2
        {
            public NestedModelsAFail4Deep2 ModelA { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void CircularModelsTestFail2()
        {
            new SqlDBAccess().ValidateForDAL(new NestedModelsAFail4Deep2());
        }

        public class NestedModelsAFailChildContainsParent
        {
            public String NameA { get; set; }
            public NestedModelsBFailChildContainsParent ModelB { get; set; }
        }

        public class NestedModelsBFailChildContainsParent
        {
            public String NameB { get; set; }
            public NestedModelsAFailChildContainsParent ModelA { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void CircularModelsTestChildContainsParentFail()
        {
            new SqlDBAccess().ValidateForDAL(new NestedModelsAFailChildContainsParent());
        }
        #endregion

        #region Nested property names tests
        public class NestedPropertyClassAPass
        {
            public String String1 { get; set; }
            public String String2 { get; set; }
            public NestedPropertyClassBPass ClassB { get; set; }
        }

        public class NestedPropertyClassBPass
        {
            public String String3 { get; set; }
            public String String4 { get; set; }
            public NestedPropertyClassCPass ClassC { get; set; }
        }

        public class NestedPropertyClassCPass
        {
            [DALIgnore]
            public String String5 { get; set; }
            public String String6 { get; set; }
        }

        [TestMethod()]
        public void NestedPropertiesPass()
        {
            new SqlDBAccess().ValidateForDAL(new NestedPropertyClassAPass());
            Assert.IsTrue(true);
        }

        public class NestedPropertyClassAFail
        {
            public String String1 { get; set; }
            public String String2 { get; set; }
            public NestedPropertyClassBFail ClassB { get; set; }
        }

        public class NestedPropertyClassBFail
        {
            public String String2 { get; set; }
            public String String3 { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void NestedPropertiesFail()
        {
            new SqlDBAccess().ValidateForDAL(new NestedPropertyClassAFail());
        }

        public class NestedPropertyClassASprocParamNameChangePass
        {
            public String String1 { get; set; }
            public String String2 { get; set; }
            public NestedPropertyClassBSprocParamNameChangePass ClassB { get; set; }
        }

        public class NestedPropertyClassBSprocParamNameChangePass
        {
            [DALSQLParameterName(Name = "String4")]
            public String String2 { get; set; }
            public String String3 { get; set; }
            public NestedPropertyClassCSprocParamNameChangePass ClassC { get; set; }
        }

        public class NestedPropertyClassCSprocParamNameChangePass
        {
            [DALSQLParameterName(Name = "String5")]
            [DALParameterDirection(Direction = ParameterDirection.Output)]
            public String String2 { get; set; }
            [DALSQLParameterName(Name = "String6")]
            public String String3 { get; set; }
        }

        [TestMethod()]
        public void NestedPropertiesSprocParamNameChangePass()
        {
            new SqlDBAccess().ValidateForDAL(new NestedPropertyClassASprocParamNameChangePass());
            Assert.IsTrue(true);
        }

        public class NestedPropertyClassASprocParamNameChangeFail
        {
            public String String1 { get; set; }
            public String String2 { get; set; }
            public NestedPropertyClassBSprocParamNameChangeFail ClassB { get; set; }
        }

        public class NestedPropertyClassBSprocParamNameChangeFail
        {
            [DALSQLParameterName(Name = "String1")]
            public String String4 { get; set; }
            public String String3 { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyMisconfiguredException))]
        public void NestedPropertiesSprocParamNameChangeFail()
        {
            new SqlDBAccess().ValidateForDAL(new NestedPropertyClassASprocParamNameChangeFail());
        }

        public class NestedPropertyClassASprocParamNotInputPass
        {
            public String String1 { get; set; }
            public String String2 { get; set; }
            public NestedPropertyClassBSprocParamNotInputPass ClassB { get; set; }
        }

        public class NestedPropertyClassBSprocParamNotInputPass
        {
            [DALIgnoreAttribute]
            public String String2 { get; set; }
            public String String3 { get; set; }
        }

        [TestMethod()]
        public void NestedPropertiesSprocParamNotInputPass()
        {
            new SqlDBAccess().ValidateForDAL(new NestedPropertyClassASprocParamNotInputPass());
            Assert.IsTrue(true);
        }

        public class ReadTypeA
        {
            public int ID { get; set; }
            public ReadTypeB B { get; set; }
        }

        public class ReadTypeB
        {
            public int Amount { get; set; }
        }

        [TestMethod()]
        public void ValidateNestedTestByObject()
        {
            var db = new SqlDBAccess();

            var dt = new DataTable();
            dt.Columns.Add("Amount", typeof(int));
            var row = dt.Rows.Add(5);

            db.ValidateForDAL(new ReadTypeA());
            db.PopulateModelBaseEnumeration<ReadTypeA>(new ExecuteReadQuickTuple { DataRows = new List<Object []> { row.ItemArray }, ColumnNames = new List<String> { "Amount" }, ColumnTypes = new List<Type> { typeof(int) } });
        }
        #endregion
        #endregion

        #region Bugs
        public class DeleteFileRecordModel
        {
            public IEnumerable<FileRecordIdsTableType> FileRecordIds { get; set; }
        }

        public class FileRecordIdsTableType
        {
            public int FileRecordId { get; set; }
        }

        [TestMethod()]
        public void BugFileRecordIdsKeyNotFound()
        {
            var target = new DeleteFileRecordModel();
            List<int> ids = new List<int> { 1, 2, 3 };
            target.FileRecordIds = ids.Select(id => new FileRecordIdsTableType { FileRecordId = id });

            var db = new SqlDBAccess();
            db.ValidateForDAL(target);
            var p = db.PrepareParameters(target, true);

            Assert.IsTrue(true);
        }
        #endregion

        #region IQuickRead
        public class UDTableQuickReadPass : IQuickRead
        {
            public int? Int1 { get; set; }
            public int? Int2 { get; set; }
            public int? Int3 { get; set; }
            public int? Int4 { get; set; }
            public int? Int5 { get; set; }

            public Object[] ToObjectArray()
            {
                return new Object[]
		        {
			        null, null, null, null, null
		        };
            }

            public Dictionary<String, Type> GetColumnNamesTypes()
            {
                return new Dictionary<String, Type>
                {
                    {"Int1", typeof(int)},
                    {"Int2", typeof(int)},
                    {"Int3", typeof(int)},
                    {"Int4", typeof(int)},
                    {"Int5", typeof(int)}
                };
            }
        }

        [TestMethod()]
        public void UDTableQuickRead_Test_Pass()
        {
            new SqlDBAccess().ValidateForDAL(new UDTableQuickReadPass());
        }

        public class UDTableDifferingObjectArrayLengths : IQuickRead
        {
            public int? Int1 { get; set; }
            public int? Int2 { get; set; }
            public int? Int3 { get; set; }
            public int? Int4 { get; set; }
            public int? Int5 { get; set; }

            public Object[] ToObjectArray()
            {
                return new Object[]
		        {
			        null, null, null, null
		        };
            }

            public Dictionary<String, Type> GetColumnNamesTypes()
            {
                return new Dictionary<String, Type>
                {
                    {"Int1", typeof(String)},
                    {"Int2", typeof(int?)},
                    {"Int3", typeof(String)},
                    {"Int4", typeof(int?)},
                    {"Int5", typeof(String)}
                };
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(TableQuickReadMisconfiguredException))]
        public void UDTableDifferingObjectArrayLengths_Test_Fail()
        {
            new SqlDBAccess().ValidateForDAL(new UDTableDifferingObjectArrayLengths());
        }

        public class UDTableWithNullableGetObjectTypes : IQuickRead
        {
            public int? Int1 { get; set; }
            public int? Int2 { get; set; }
            public int? Int3 { get; set; }
            public int? Int4 { get; set; }
            public int? Int5 { get; set; }

            public Object[] ToObjectArray()
            {
                return new Object[]
		        {
			        null, null, null, null, null
		        };
            }

            public Dictionary<String, Type> GetColumnNamesTypes()
            {
                return new Dictionary<String, Type>
                {
                    {"Int1", typeof(String)},
                    {"Int2", typeof(int?)},
                    {"Int3", typeof(String)},
                    {"Int4", typeof(int?)},
                    {"Int5", typeof(String)}
                };
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(TableQuickReadMisconfiguredException))]
        public void UDTableWithNullableGetObjectTypes_Test_Fail()
        {
            new SqlDBAccess().ValidateForDAL(new UDTableWithNullableGetObjectTypes());
        }
        #endregion

        #region DB_Access
        public class ClassWithSprocParameterNameChange
        {
            public int ID { get; set; }
            [DALSQLParameterName(Name="CustomerName")]
            public String Name { get; set; }
        }

        public class ClassWithDALIgnore
        {
            public int ID { get; set; }
            [DALIgnore]
            public String Name { get; set; }
        }

        public class ClassWithWriteOnlyProperty
        {
            public int ID { get; set; }
            public String Name { private get; set; }
        }

        public class ClassWithStringFormatRead
        {
            public int ID { get; set; }
            [DALReadStringFormat(Format="Hello, {0}.")]
            public String Name { get; set; }
        }

        [TestMethod()]
        public void PrepareParameters_NullModelInput_Test()
        {
            var db = new SqlDBAccess();
            var ps = db.PrepareParameters(null, true);

            Assert.AreEqual(0, ps.Count());
        }

        [TestMethod()]
        public void PrepareParameters_WithInput_Test()
        {
            var db = new SqlDBAccess();
            Boolean status = false;
            db.Input = new { Status = status };
            var ps = db.PrepareParameters(null, true).ToList();

            Assert.AreEqual(1, ps.Count);
            Assert.AreEqual(status, ps[0].Value);
        }

        [TestMethod()]
        public void PrepareParameters_WithObject_Test()
        {
            var db = new SqlDBAccess();
            Boolean status = false;
            String name = "Brian";
            var ps = db.PrepareParameters(new { Status = status, Name = name }, true).ToList();

            Assert.AreEqual(2, ps.Count);
            Assert.AreEqual(status, ps[0].Value);
            Assert.AreEqual(name, ps[1].Value.ToString());
        }

        [TestMethod()]
        public void PrepareParameters_UDTable_SprocParameterNameChange_Test()
        {
            var db = new SqlDBAccess();
            var model = new
            {
                People = new List<ClassWithSprocParameterNameChange>
                {
                    new ClassWithSprocParameterNameChange
                    {
                        ID = 5,
                        Name = "Brian"
                    },
                    new ClassWithSprocParameterNameChange
                    {
                        ID = 6,
                        Name = "Brian6"
                    }
                }
            };
            db.ValidateForDAL(model);

            var ps = db.PrepareParameters(model).ToList();

            Assert.AreEqual<int>(1, ps.Count);
            var dt = ps[0].Value as DataTable;
            Assert.AreEqual<String>("CustomerName", dt.Columns[1].ColumnName);
        }

        [TestMethod()]
        public void PrepareParameters_UDTable_DALIgnore_Test()
        {
            var db = new SqlDBAccess();
            var model = new
            {
                People = new List<ClassWithDALIgnore>
                {
                    new ClassWithDALIgnore
                    {
                        ID = 5,
                        Name = "Brian"
                    },
                    new ClassWithDALIgnore
                    {
                        ID = 6,
                        Name = "Brian6"
                    }
                }
            };
            db.ValidateForDAL(model);

            var ps = db.PrepareParameters(model).ToList();

            Assert.AreEqual<int>(1, ps.Count);
            var dt = ps[0].Value as DataTable;
            Assert.AreEqual<int>(1, dt.Columns.Count);
            Assert.AreEqual<String>("ID", dt.Columns[0].ColumnName);
        }

        [TestMethod()]
        public void PrepareParameters_UDTable_WriteOnlyProperty_Test()
        {
            var db = new SqlDBAccess();
            var model = new
            {
                People = new List<ClassWithWriteOnlyProperty>
                {
                    new ClassWithWriteOnlyProperty
                    {
                        ID = 5,
                        Name = "Brian"
                    },
                    new ClassWithWriteOnlyProperty
                    {
                        ID = 6,
                        Name = "Brian6"
                    }
                }
            };
            db.ValidateForDAL(model);

            var ps = db.PrepareParameters(model).ToList();

            Assert.AreEqual<int>(1, ps.Count);
            var dt = ps[0].Value as DataTable;
            Assert.AreEqual<int>(1, dt.Columns.Count);
            Assert.AreEqual<String>("ID", dt.Columns[0].ColumnName);
        }

        [TestMethod()]
        public void PrepareParameters_UDTable_StringFormatRead_Test()
        {
            var db = new SqlDBAccess();
            var model = new
            {
                People = new List<ClassWithStringFormatRead>
                {
                    new ClassWithStringFormatRead
                    {
                        ID = 5,
                        Name = "Brian"
                    },
                    new ClassWithStringFormatRead
                    {
                        ID = 6,
                        Name = "Brian6"
                    }
                }
            };
            db.ValidateForDAL(model);

            var ps = db.PrepareParameters(model).ToList();

            Assert.AreEqual<int>(1, ps.Count);
            var dt = ps[0].Value as DataTable;
            Assert.AreEqual<int>(2, dt.Columns.Count);
            Assert.AreEqual<String>("Hello, Brian.", dt.Rows[0][1].ToString());
            Assert.AreEqual<String>("Hello, Brian6.", dt.Rows[1][1].ToString());
        }
        #endregion

        #region Nested Populate
        #endregion
    }
}