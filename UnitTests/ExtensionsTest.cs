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

using System.Data.DBAccess.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Data.DBAccess.Generic.Providers.SQL;
using System.Text.RegularExpressions;

namespace UnitTests
{


    /// <summary>
    ///This is a test class for ExtensionsTest and is intended
    ///to contain all ExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExtensionsTest
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


        //This test class uses the NORTHWIND database.


        private String GetXMLFileFullPath(String fileName)
        {
            return String.Format("{0}\\XML\\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        private String GetJSONFileFullPath(String fileName)
        {
            return String.Format("{0}\\JSON\\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        private SqlDBAccess NewDB { get { return new SqlDBAccess(""); } }

        private DALTuple<Customer, Order, OrderDetail> ExecuteKnownTypesQuery(List<String> tableNames = null)
        {
            var db = NewDB;
            db.IsStoredProcedure = false;

            var rels = new List<DALRelationship>
	        {
		        new DALRelationship
		        {
			        ChildColumn = "CustomerClass.CustomerID",
			        ParentColumn = "CustomerClass.CustomerID",
			        ParentProperty = "Orders"
		        },
		        new DALRelationship
		        {
			        ChildColumn = "OrderClass.OrderID",
			        ParentColumn = "OrderID",
			        ParentProperty = "OrderDetails"
		        }
	        };

            if (tableNames != null)
                db.XMLTableNames = tableNames;

            db.QueryString = @"SELECT * FROM Customers ORDER BY CustomerID
	                           SELECT * FROM Orders ORDER BY CustomerID
	                           SELECT * FROM [Order Details]";

            return db.ExecuteRelatedSetRead<Customer, Order, OrderDetail>(rels);
        }

        private List<List<Object>> ExecuteUnknownTypesQuery(List<String> tableNames = null)
        {
            var db = NewDB;
            db.IsStoredProcedure = false;

            var rels = new List<DALRelationship>
	        {
		        new DALRelationship
		        {
			        ChildColumn = "CustomerID",
			        ParentColumn = "CustomerID",
			        ParentProperty = "Orders"
		        },
		        new DALRelationship
		        {
			        ChildColumn = "OrderID",
			        ParentColumn = "OrderID",
			        ParentProperty = "OrderDetails"
		        }
	        };

            if (tableNames != null)
                db.XMLTableNames = tableNames;

            db.QueryString = @"SELECT * FROM Customers ORDER BY CustomerID
	                           SELECT * FROM Orders ORDER BY CustomerID
	                           SELECT * FROM [Order Details]";

            return db.ExecuteRelatedSetRead(rels);
        }

        private Object GetIGroupingTestSet()
        {
            return new
            {
                Products = new List<Object>
                {
                    new
                    {
                        ID = 1,
                        Description = "Widget",
                        Discontinued = false,
                        LastSale = new DateTime(2007, 1, 1, 12, 0, 0),
                        Test = 
                            new
                            {
                                Name = "Brian",
                                Age = 27
                            }
                    },
                    new
                    {
                        ID = 999,
                        Description = "Test",
                        Discontinued = false,
                        LastSale = new DateTime(2007, 9, 10, 9, 6, 25),
                        Test =
                            new
                            {
                                Name = "Kyle",
                                Age = 25
                            }
                    },
                    new
                    {
                        ID = 0,
                        Description = "Brian Test Product",
                        Discontinued = true,
                        LastSale = new DateTime(2011, 6, 16, 11, 30, 45),
                        Tests = new List<Object>
                        {
                            new
                            {
                                Test = new
                                {
                                    Name = "Nathan",
                                    Age = 18
                                }
                            },
                            new
                            {
                               Test = new
                                {
                                    Name = "Julie LOLOLOLOL",
                                    Age = 17
                                }
                            }
                        }
                    },
                }
            };
        }

        [TestMethod()]
        public void LINQIGroupingAnonymousKeyAnonymousElement()
        {
            String expected = File.ReadAllText(GetXMLFileFullPath("LINQIGroupingAnonymousKeyAnonymousElement.xml"));

            //replace this regex in expected and actual because the runtime type name will be different each time
            var runtimeTypeRegex = new Regex("Runtime_assembly_\\d+_module_type");
            var objs = GetIGroupingTestSet();

            var groups = objs.GetValue<List<Object>>("Products").GroupBy(p => new { Text = "This is the key", Discontinued = p.GetValue<Boolean>("Discontinued") });
            Assert.AreEqual<String>(runtimeTypeRegex.Replace(expected, ""), runtimeTypeRegex.Replace(groups.SerializeToXML("Products"), ""));
        }
        
        [TestMethod()]
        public void RelatedSetReadXMLSerializationKnownTypesRootElement()
        {
            String expected = File.ReadAllText(GetXMLFileFullPath("ExecuteRelatedSetReadXMLKnownTypesTableNamesRootElement.xml"));

            Assert.AreEqual<String>(expected, ExecuteKnownTypesQuery().SerializeToXML("Customers"));
        }

        [TestMethod()]
        public void RelatedSetReadXMLSerializationKnownTypes()
        {
            String expected = File.ReadAllText(GetXMLFileFullPath("ExecuteRelatedSetReadXMLKnownTypesTableNames.xml"));

            Assert.AreEqual<String>(expected, ExecuteKnownTypesQuery().SerializeToXML());
        }

        [TestMethod()]
        public void RelatedSetReadXMLSerializationUnknownNumberUnkownTypes()
        {
            String expected = File.ReadAllText(GetXMLFileFullPath("RelatedSetReadXMLSerializationUnknownNumberUnkownTypes.xml"));

            //replace this regex in expected and actual because the runtime type name will be different each time
            var runtimeTypeRegex = new Regex("Runtime_assembly_\\d+_module_type");

            Assert.AreEqual<String>(runtimeTypeRegex.Replace(expected, ""), runtimeTypeRegex.Replace(ExecuteUnknownTypesQuery()[0].SerializeToXML(), ""));
        }

        [TestMethod()]
        public void RelatedSetReadXMLSerializationUnknownNumberUnkownTypesTableNames()
        {
            String expected = File.ReadAllText(GetXMLFileFullPath("RelatedSetReadXMLSerializationUnknownNumberUnkownTypesTableNames.xml"));

            Assert.AreEqual<String>(expected, ExecuteUnknownTypesQuery(new List<String> { "Customer", "Order", "OrderDetail" })[0].SerializeToXML());
        }

        [TestMethod()]
        public void RelatedSetReadXMLSerializationUnknownNumberUnkownTypesTableNamesRootElement()
        {
            String expected = File.ReadAllText(GetXMLFileFullPath("RelatedSetReadXMLSerializationUnknownNumberUnkownTypesTableNamesRootElement.xml"));

            Assert.AreEqual<String>(expected, ExecuteUnknownTypesQuery(new List<String> { "Customer", "Order", "OrderDetail" })[0].SerializeToXML("Customers"));
        }

        [TestMethod()]
        public void IsUserTypeTest()
        {
            foreach (var t in Extensions.s_DotNETTypes)
                Assert.IsFalse(t.IsUserType());

            Assert.IsTrue(typeof(Customer).IsUserType());
            Assert.IsTrue(new { Status = false }.GetType().IsUserType());
            Assert.IsFalse(typeof(IEnumerable<int>).IsUserType());
            Assert.IsFalse(typeof(List<int>).IsUserType());
        }

        [TestMethod()]
        public void RelatedSetReadJSONSerializationKnownTypes()
        {
            String expected = File.ReadAllText(GetJSONFileFullPath("RelatedSetReadJSONKnownTypes.json"));

            Assert.AreEqual<String>(expected, ExecuteKnownTypesQuery().SerializeToJSON());
        }

        [TestMethod()]
        public void RelatedSetReadJSONSerializationUnknownTypes()
        {
            String expected = File.ReadAllText(GetJSONFileFullPath("RelatedSetReadJSONUnknownTypes.json"));
            
            Assert.AreEqual<String>(expected, ExecuteUnknownTypesQuery()[0].SerializeToJSON());
        }

        [TestMethod()]
        public void RelatedSetReadJSONSerializationKnownTypesTableNames()
        {
            String expected = File.ReadAllText(GetJSONFileFullPath("RelatedSetReadJSONKnownTypesTableNames.json"));

            Assert.AreEqual<String>(expected, ExecuteKnownTypesQuery(new List<String> { "Customer", "Order", "OrderDetail" }).SerializeToJSON(true));
        }

        [TestMethod()]
        public void RelatedSetReadJSONSerializationUnknownTypesTableNames()
        {
            String expected = File.ReadAllText(GetJSONFileFullPath("RelatedSetReadJSONUnknownTypesTableNames.json"));
            
            Assert.AreEqual<String>(expected, ExecuteUnknownTypesQuery(new List<String> { "Customer", "Order", "OrderDetail" })[0].SerializeToJSON(true));
        }

        [TestMethod()]
        public void LINQIGroupingAnonymousKeyAnonymousElementJSON()
        {
            String expected = File.ReadAllText(GetJSONFileFullPath("LINQIGroupingAnonymousKeyAnonymousElement.json"));

            var objs = GetIGroupingTestSet();
            var groups = objs.GetValue<List<Object>>("Products").GroupBy(p => new { Text = "This is the key", Discontinued = p.GetValue<Boolean>("Discontinued") });
            Assert.AreEqual<String>(expected, groups.SerializeToJSON());
        }
    }

    public class CustomerIDClass
    {
        public String CustomerID { get; set; }
    }

    public class OrderIDClass
    {
        public int OrderID { get; set; }
    }

    public class Customer
    {
        public CustomerIDClass CustomerClass { get; set; }
        public String CompanyName { get; set; }
        public String ContactName { get; set; }
        public String ContactTitle { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Region { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
        public String Phone { get; set; }
        public String Fax { get; set; }
        [DALIgnore]
        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public CustomerIDClass CustomerClass { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public Decimal Freight { get; set; }
        public String ShipName { get; set; }
        public String ShipAddress { get; set; }
        public String ShipCity { get; set; }
        public String ShipRegion { get; set; }
        public String ShipPostalCode { get; set; }
        public String ShipCountry { get; set; }
        [DALIgnore]
        public List<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderDetail
    {
        public OrderIDClass OrderClass { get; set; }
        public int ProductID { get; set; }
        public Decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
    }
}