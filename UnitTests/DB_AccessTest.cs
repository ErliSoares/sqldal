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
using System.Data.DBAccess.Generic.Providers.DotNETCompatibleProvider;
using System.Data.DBAccess.Generic.Providers.SQL;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass()]
    public class DB_AccessTest
    {
        private static String DBUsername { get { return ""; } }
        private static String ConnectionString { get { return ""; } }
        private static SqlDBAccess NewDB { get { return new SqlDBAccess(ConnectionString); } }
        private int NumOfReadResults { get { return 5; } }

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
        private static Object LockObject = new Object();
        private static void QueryComplete(SqlDBAccess db, System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALQueryCompleteEventArgs e)
        {
            lock (LockObject)
            {
                String queryTime = String.Format("Query Method: {1}{0}Query String: {2}{0}Total Seconds: {3}{0}{0}", Environment.NewLine, e.QueryMethod, e.QueryString, e.TimeElapsed.TotalSeconds);
                File.AppendAllText(@"C:\\querytimes.txt", queryTime);
            }
        }
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            //DALEvents.OnQueryComplete += QueryComplete;

            String SetupDB =
@"TRUNCATE TABLE ReadType1
TRUNCATE TABLE ReadType2
TRUNCATE TABLE ReadType3
TRUNCATE TABLE ReadType4
TRUNCATE TABLE ReadType5
TRUNCATE TABLE ReadType6
TRUNCATE TABLE ReadType7
TRUNCATE TABLE ReadType8
TRUNCATE TABLE ReadType9
TRUNCATE TABLE ReadType10
TRUNCATE TABLE ReadType11
TRUNCATE TABLE ReadType12
TRUNCATE TABLE ReadType13
TRUNCATE TABLE ReadType14
TRUNCATE TABLE ReadType15
TRUNCATE TABLE ReadType16
TRUNCATE TABLE ReadType17

INSERT INTO ReadType1 (ID,Name) VALUES (1,'R1-1')
INSERT INTO ReadType1 (ID,Name) VALUES (2,'R1-2')
INSERT INTO ReadType1 (ID,Name) VALUES (3,'R1-3')
INSERT INTO ReadType1 (ID,Name) VALUES (4,'R1-4')
INSERT INTO ReadType1 (ID,Name) VALUES (5,'R1-5')

INSERT INTO ReadType2 (ID,Name) VALUES (1,'R2-1')
INSERT INTO ReadType2 (ID,Name) VALUES (2,'R2-2')

INSERT INTO ReadType3 (ID,Name) VALUES (1,'R3-1')
INSERT INTO ReadType3 (ID,Name) VALUES (2,'R3-2')
INSERT INTO ReadType3 (ID,Name) VALUES (3,'R3-3')

INSERT INTO ReadType4 (ID,Name) VALUES (1,'R4-1')
INSERT INTO ReadType4 (ID,Name) VALUES (2,'R4-2')
INSERT INTO ReadType4 (ID,Name) VALUES (3,'R4-3')
INSERT INTO ReadType4 (ID,Name) VALUES (4,'R4-4')

INSERT INTO ReadType5 (ID,Name) VALUES (1,'R5-1')
INSERT INTO ReadType5 (ID,Name) VALUES (2,'R5-2')
INSERT INTO ReadType5 (ID,Name) VALUES (3,'R5-3')
INSERT INTO ReadType5 (ID,Name) VALUES (4,'R5-4')
INSERT INTO ReadType5 (ID,Name) VALUES (5,'R5-5')

INSERT INTO ReadType6 (ID,Name) VALUES (1,'R6-1')
INSERT INTO ReadType6 (ID,Name) VALUES (2,'R6-2')
INSERT INTO ReadType6 (ID,Name) VALUES (3,'R6-3')
INSERT INTO ReadType6 (ID,Name) VALUES (4,'R6-4')
INSERT INTO ReadType6 (ID,Name) VALUES (5,'R6-5')
INSERT INTO ReadType6 (ID,Name) VALUES (6,'R6-6')

INSERT INTO ReadType7 (ID,Name) VALUES (1,'R7-1')
INSERT INTO ReadType7 (ID,Name) VALUES (2,'R7-2')
INSERT INTO ReadType7 (ID,Name) VALUES (3,'R7-3')
INSERT INTO ReadType7 (ID,Name) VALUES (4,'R7-4')
INSERT INTO ReadType7 (ID,Name) VALUES (5,'R7-5')
INSERT INTO ReadType7 (ID,Name) VALUES (6,'R7-6')
INSERT INTO ReadType7 (ID,Name) VALUES (7,'R7-7')

INSERT INTO ReadType8 (ID,Name) VALUES (1,'R8-1')
INSERT INTO ReadType8 (ID,Name) VALUES (2,'R8-2')
INSERT INTO ReadType8 (ID,Name) VALUES (3,'R8-3')
INSERT INTO ReadType8 (ID,Name) VALUES (4,'R8-4')
INSERT INTO ReadType8 (ID,Name) VALUES (5,'R8-5')
INSERT INTO ReadType8 (ID,Name) VALUES (6,'R8-6')
INSERT INTO ReadType8 (ID,Name) VALUES (7,'R8-7')
INSERT INTO ReadType8 (ID,Name) VALUES (8,'R8-8')

INSERT INTO ReadType9 (ID,Name) VALUES (1,'R9-1')
INSERT INTO ReadType9 (ID,Name) VALUES (2,'R9-2')
INSERT INTO ReadType9 (ID,Name) VALUES (3,'R9-3')
INSERT INTO ReadType9 (ID,Name) VALUES (4,'R9-4')
INSERT INTO ReadType9 (ID,Name) VALUES (5,'R9-5')
INSERT INTO ReadType9 (ID,Name) VALUES (6,'R9-6')
INSERT INTO ReadType9 (ID,Name) VALUES (7,'R9-7')
INSERT INTO ReadType9 (ID,Name) VALUES (8,'R9-8')
INSERT INTO ReadType9 (ID,Name) VALUES (9,'R9-9')

INSERT INTO ReadType10 (ID,Name) VALUES (1,'R10-1')
INSERT INTO ReadType10 (ID,Name) VALUES (2,'R10-2')
INSERT INTO ReadType10 (ID,Name) VALUES (3,'R10-3')
INSERT INTO ReadType10 (ID,Name) VALUES (4,'R10-4')
INSERT INTO ReadType10 (ID,Name) VALUES (5,'R10-5')
INSERT INTO ReadType10 (ID,Name) VALUES (6,'R10-6')
INSERT INTO ReadType10 (ID,Name) VALUES (7,'R10-7')
INSERT INTO ReadType10 (ID,Name) VALUES (8,'R10-8')
INSERT INTO ReadType10 (ID,Name) VALUES (9,'R10-9')
INSERT INTO ReadType10 (ID,Name) VALUES (10,'R10-10')

INSERT INTO ReadType11 (ID,Name) VALUES (1,'R11-1')
INSERT INTO ReadType11 (ID,Name) VALUES (2,'R11-2')
INSERT INTO ReadType11 (ID,Name) VALUES (3,'R11-3')
INSERT INTO ReadType11 (ID,Name) VALUES (4,'R11-4')
INSERT INTO ReadType11 (ID,Name) VALUES (5,'R11-5')
INSERT INTO ReadType11 (ID,Name) VALUES (6,'R11-6')
INSERT INTO ReadType11 (ID,Name) VALUES (7,'R11-7')
INSERT INTO ReadType11 (ID,Name) VALUES (8,'R11-8')
INSERT INTO ReadType11 (ID,Name) VALUES (9,'R11-9')
INSERT INTO ReadType11 (ID,Name) VALUES (10,'R11-10')
INSERT INTO ReadType11 (ID,Name) VALUES (11,'R11-11')

INSERT INTO ReadType12 (ID,Name) VALUES (1,'R12-1')
INSERT INTO ReadType12 (ID,Name) VALUES (2,'R12-2')
INSERT INTO ReadType12 (ID,Name) VALUES (3,'R12-3')
INSERT INTO ReadType12 (ID,Name) VALUES (4,'R12-4')
INSERT INTO ReadType12 (ID,Name) VALUES (5,'R12-5')
INSERT INTO ReadType12 (ID,Name) VALUES (6,'R12-6')
INSERT INTO ReadType12 (ID,Name) VALUES (7,'R12-7')
INSERT INTO ReadType12 (ID,Name) VALUES (8,'R12-8')
INSERT INTO ReadType12 (ID,Name) VALUES (9,'R12-9')
INSERT INTO ReadType12 (ID,Name) VALUES (10,'R12-10')
INSERT INTO ReadType12 (ID,Name) VALUES (11,'R12-11')
INSERT INTO ReadType12 (ID,Name) VALUES (12,'R12-12')

INSERT INTO ReadType13 (ID,Name) VALUES (1,'R13-1')
INSERT INTO ReadType13 (ID,Name) VALUES (2,'R13-2')
INSERT INTO ReadType13 (ID,Name) VALUES (3,'R13-3')
INSERT INTO ReadType13 (ID,Name) VALUES (4,'R13-4')
INSERT INTO ReadType13 (ID,Name) VALUES (5,'R13-5')
INSERT INTO ReadType13 (ID,Name) VALUES (6,'R13-6')
INSERT INTO ReadType13 (ID,Name) VALUES (7,'R13-7')
INSERT INTO ReadType13 (ID,Name) VALUES (8,'R13-8')
INSERT INTO ReadType13 (ID,Name) VALUES (9,'R13-9')
INSERT INTO ReadType13 (ID,Name) VALUES (10,'R13-10')
INSERT INTO ReadType13 (ID,Name) VALUES (11,'R13-11')
INSERT INTO ReadType13 (ID,Name) VALUES (12,'R13-12')
INSERT INTO ReadType13 (ID,Name) VALUES (13,'R13-13')

INSERT INTO ReadType14 (ID,Name) VALUES (1,'R14-1')
INSERT INTO ReadType14 (ID,Name) VALUES (2,'R14-2')
INSERT INTO ReadType14 (ID,Name) VALUES (3,'R14-3')
INSERT INTO ReadType14 (ID,Name) VALUES (4,'R14-4')
INSERT INTO ReadType14 (ID,Name) VALUES (5,'R14-5')
INSERT INTO ReadType14 (ID,Name) VALUES (6,'R14-6')
INSERT INTO ReadType14 (ID,Name) VALUES (7,'R14-7')
INSERT INTO ReadType14 (ID,Name) VALUES (8,'R14-8')
INSERT INTO ReadType14 (ID,Name) VALUES (9,'R14-9')
INSERT INTO ReadType14 (ID,Name) VALUES (10,'R14-10')
INSERT INTO ReadType14 (ID,Name) VALUES (11,'R14-11')
INSERT INTO ReadType14 (ID,Name) VALUES (12,'R14-12')
INSERT INTO ReadType14 (ID,Name) VALUES (13,'R14-13')
INSERT INTO ReadType14 (ID,Name) VALUES (14,'R14-14')

INSERT INTO ReadType15 (ID,Name) VALUES (1,'R15-1')
INSERT INTO ReadType15 (ID,Name) VALUES (2,'R15-2')
INSERT INTO ReadType15 (ID,Name) VALUES (3,'R15-3')
INSERT INTO ReadType15 (ID,Name) VALUES (4,'R15-4')
INSERT INTO ReadType15 (ID,Name) VALUES (5,'R15-5')
INSERT INTO ReadType15 (ID,Name) VALUES (6,'R15-6')
INSERT INTO ReadType15 (ID,Name) VALUES (7,'R15-7')
INSERT INTO ReadType15 (ID,Name) VALUES (8,'R15-8')
INSERT INTO ReadType15 (ID,Name) VALUES (9,'R15-9')
INSERT INTO ReadType15 (ID,Name) VALUES (10,'R15-10')
INSERT INTO ReadType15 (ID,Name) VALUES (11,'R15-11')
INSERT INTO ReadType15 (ID,Name) VALUES (12,'R15-12')
INSERT INTO ReadType15 (ID,Name) VALUES (13,'R15-13')
INSERT INTO ReadType15 (ID,Name) VALUES (14,'R15-14')
INSERT INTO ReadType15 (ID,Name) VALUES (15,'R15-15')

INSERT INTO ReadType16 (ID,Name) VALUES (1,'R16-1')
INSERT INTO ReadType16 (ID,Name) VALUES (2,'R16-2')
INSERT INTO ReadType16 (ID,Name) VALUES (3,'R16-3')
INSERT INTO ReadType16 (ID,Name) VALUES (4,'R16-4')
INSERT INTO ReadType16 (ID,Name) VALUES (5,'R16-5')
INSERT INTO ReadType16 (ID,Name) VALUES (6,'R16-6')
INSERT INTO ReadType16 (ID,Name) VALUES (7,'R16-7')
INSERT INTO ReadType16 (ID,Name) VALUES (8,'R16-8')
INSERT INTO ReadType16 (ID,Name) VALUES (9,'R16-9')
INSERT INTO ReadType16 (ID,Name) VALUES (10,'R16-10')
INSERT INTO ReadType16 (ID,Name) VALUES (11,'R16-11')
INSERT INTO ReadType16 (ID,Name) VALUES (12,'R16-12')
INSERT INTO ReadType16 (ID,Name) VALUES (13,'R16-13')
INSERT INTO ReadType16 (ID,Name) VALUES (14,'R16-14')
INSERT INTO ReadType16 (ID,Name) VALUES (15,'R16-15')
INSERT INTO ReadType16 (ID,Name) VALUES (16,'R16-16')

INSERT INTO ReadType17 (ID,Name) VALUES (1,'R17-1')
INSERT INTO ReadType17 (ID,Name) VALUES (2,'R17-2')
INSERT INTO ReadType17 (ID,Name) VALUES (3,'R17-3')
INSERT INTO ReadType17 (ID,Name) VALUES (4,'R17-4')
INSERT INTO ReadType17 (ID,Name) VALUES (5,'R17-5')
INSERT INTO ReadType17 (ID,Name) VALUES (6,'R17-6')
INSERT INTO ReadType17 (ID,Name) VALUES (7,'R17-7')
INSERT INTO ReadType17 (ID,Name) VALUES (8,'R17-8')
INSERT INTO ReadType17 (ID,Name) VALUES (9,'R17-9')
INSERT INTO ReadType17 (ID,Name) VALUES (10,'R17-10')
INSERT INTO ReadType17 (ID,Name) VALUES (11,'R17-11')
INSERT INTO ReadType17 (ID,Name) VALUES (12,'R17-12')
INSERT INTO ReadType17 (ID,Name) VALUES (13,'R17-13')
INSERT INTO ReadType17 (ID,Name) VALUES (14,'R17-14')
INSERT INTO ReadType17 (ID,Name) VALUES (15,'R17-15')
INSERT INTO ReadType17 (ID,Name) VALUES (16,'R17-16')
INSERT INTO ReadType17 (ID,Name) VALUES (17,'R17-17')


TRUNCATE TABLE Update1
TRUNCATE TABLE Update2
TRUNCATE TABLE Update3
TRUNCATE TABLE Update4
TRUNCATE TABLE Update5
TRUNCATE TABLE Update6
TRUNCATE TABLE Update7
TRUNCATE TABLE Update8
TRUNCATE TABLE Update9
TRUNCATE TABLE Update10
TRUNCATE TABLE Update11
TRUNCATE TABLE Update12
TRUNCATE TABLE Update13
TRUNCATE TABLE Update14
TRUNCATE TABLE Update15
TRUNCATE TABLE Update16
TRUNCATE TABLE Update17
TRUNCATE TABLE Update18
TRUNCATE TABLE Update19
TRUNCATE TABLE Update20
TRUNCATE TABLE Update21

INSERT INTO Update1 (ID,Name) VALUES (1,'U1-1')
INSERT INTO Update1 (ID,Name) VALUES (2,'U1-2')
INSERT INTO Update1 (ID,Name) VALUES (3,'U1-3')
INSERT INTO Update1 (ID,Name) VALUES (4,'U1-4')
INSERT INTO Update1 (ID,Name) VALUES (5,'U1-5')

INSERT INTO Update2 (ID,Name) VALUES (1,'U2-1')
INSERT INTO Update2 (ID,Name) VALUES (2,'U2-2')
INSERT INTO Update2 (ID,Name) VALUES (3,'U2-3')
INSERT INTO Update2 (ID,Name) VALUES (4,'U2-4')
INSERT INTO Update2 (ID,Name) VALUES (5,'U2-5')

INSERT INTO Update3 (ID,Name) VALUES (1,'U3-1')
INSERT INTO Update3 (ID,Name) VALUES (2,'U3-2')
INSERT INTO Update3 (ID,Name) VALUES (3,'U3-3')
INSERT INTO Update3 (ID,Name) VALUES (4,'U3-4')
INSERT INTO Update3 (ID,Name) VALUES (5,'U3-5')

INSERT INTO Update4 (ID,Name) VALUES (1,'U4-1')
INSERT INTO Update4 (ID,Name) VALUES (2,'U4-2')
INSERT INTO Update4 (ID,Name) VALUES (3,'U4-3')
INSERT INTO Update4 (ID,Name) VALUES (4,'U4-4')
INSERT INTO Update4 (ID,Name) VALUES (5,'U4-5')

INSERT INTO Update5 (ID,Name) VALUES (1,'U5-1')
INSERT INTO Update5 (ID,Name) VALUES (2,'U5-2')
INSERT INTO Update5 (ID,Name) VALUES (3,'U5-3')
INSERT INTO Update5 (ID,Name) VALUES (4,'U5-4')
INSERT INTO Update5 (ID,Name) VALUES (5,'U5-5')

INSERT INTO Update6 (ID,Name) VALUES (1,'U6-1')
INSERT INTO Update6 (ID,Name) VALUES (2,'U6-2')
INSERT INTO Update6 (ID,Name) VALUES (3,'U6-3')
INSERT INTO Update6 (ID,Name) VALUES (4,'U6-4')
INSERT INTO Update6 (ID,Name) VALUES (5,'U6-5')

INSERT INTO Update7 (ID,Name) VALUES (1,'U7-1')
INSERT INTO Update7 (ID,Name) VALUES (2,'U7-2')
INSERT INTO Update7 (ID,Name) VALUES (3,'U7-3')
INSERT INTO Update7 (ID,Name) VALUES (4,'U7-4')
INSERT INTO Update7 (ID,Name) VALUES (5,'U7-5')

INSERT INTO Update8 (ID,Name) VALUES (1,'U8-1')
INSERT INTO Update8 (ID,Name) VALUES (2,'U8-2')
INSERT INTO Update8 (ID,Name) VALUES (3,'U8-3')
INSERT INTO Update8 (ID,Name) VALUES (4,'U8-4')
INSERT INTO Update8 (ID,Name) VALUES (5,'U8-5')

INSERT INTO Update9 (ID,Name) VALUES (1,'U9-1')
INSERT INTO Update9 (ID,Name) VALUES (2,'U9-2')
INSERT INTO Update9 (ID,Name) VALUES (3,'U9-3')
INSERT INTO Update9 (ID,Name) VALUES (4,'U9-4')
INSERT INTO Update9 (ID,Name) VALUES (5,'U9-5')

INSERT INTO Update10 (ID,Name) VALUES (1,'U10-1')
INSERT INTO Update10 (ID,Name) VALUES (2,'U10-2')
INSERT INTO Update10 (ID,Name) VALUES (3,'U10-3')
INSERT INTO Update10 (ID,Name) VALUES (4,'U10-4')
INSERT INTO Update10 (ID,Name) VALUES (5,'U10-5')

INSERT INTO Update11 (ID,Name) VALUES (1,'U11-1')
INSERT INTO Update11 (ID,Name) VALUES (2,'U11-2')
INSERT INTO Update11 (ID,Name) VALUES (3,'U11-3')
INSERT INTO Update11 (ID,Name) VALUES (4,'U11-4')
INSERT INTO Update11 (ID,Name) VALUES (5,'U11-5')

INSERT INTO Update12 (ID,Name) VALUES (1,'U12-1')
INSERT INTO Update12 (ID,Name) VALUES (2,'U12-2')
INSERT INTO Update12 (ID,Name) VALUES (3,'U12-3')
INSERT INTO Update12 (ID,Name) VALUES (4,'U12-4')
INSERT INTO Update12 (ID,Name) VALUES (5,'U12-5')

INSERT INTO Update13 (ID,Name) VALUES (1,'U13-1')
INSERT INTO Update13 (ID,Name) VALUES (2,'U13-2')
INSERT INTO Update13 (ID,Name) VALUES (3,'U13-3')
INSERT INTO Update13 (ID,Name) VALUES (4,'U13-4')
INSERT INTO Update13 (ID,Name) VALUES (5,'U13-5')

INSERT INTO Update14 (ID,Name) VALUES (1,'U14-1')
INSERT INTO Update14 (ID,Name) VALUES (2,'U14-2')
INSERT INTO Update14 (ID,Name) VALUES (3,'U14-3')
INSERT INTO Update14 (ID,Name) VALUES (4,'U14-4')
INSERT INTO Update14 (ID,Name) VALUES (5,'U14-5')

INSERT INTO Update15 (ID,Name) VALUES (1,'U15-1')
INSERT INTO Update15 (ID,Name) VALUES (2,'U15-2')
INSERT INTO Update15 (ID,Name) VALUES (3,'U15-3')
INSERT INTO Update15 (ID,Name) VALUES (4,'U15-4')
INSERT INTO Update15 (ID,Name) VALUES (5,'U15-5')

INSERT INTO Update16 (ID,Name) VALUES (1,'U16-1')
INSERT INTO Update16 (ID,Name) VALUES (2,'U16-2')
INSERT INTO Update16 (ID,Name) VALUES (3,'U16-3')
INSERT INTO Update16 (ID,Name) VALUES (4,'U16-4')
INSERT INTO Update16 (ID,Name) VALUES (5,'U16-5')

INSERT INTO Update17 (ID,Name) VALUES (1,'U17-1')
INSERT INTO Update17 (ID,Name) VALUES (2,'U17-2')
INSERT INTO Update17 (ID,Name) VALUES (3,'U17-3')
INSERT INTO Update17 (ID,Name) VALUES (4,'U17-4')
INSERT INTO Update17 (ID,Name) VALUES (5,'U17-5')

INSERT INTO Update18 (ID,Name) VALUES (1,'U18-1')
INSERT INTO Update18 (ID,Name) VALUES (2,'U18-2')
INSERT INTO Update18 (ID,Name) VALUES (3,'U18-3')
INSERT INTO Update18 (ID,Name) VALUES (4,'U18-4')
INSERT INTO Update18 (ID,Name) VALUES (5,'U18-5')

INSERT INTO Update19 (ID,Name) VALUES (1,'U19-1')
INSERT INTO Update19 (ID,Name) VALUES (2,'U19-2')
INSERT INTO Update19 (ID,Name) VALUES (3,'U19-3')
INSERT INTO Update19 (ID,Name) VALUES (4,'U19-4')
INSERT INTO Update19 (ID,Name) VALUES (5,'U19-5')

INSERT INTO Update20 (ID,Name) VALUES (1,'U20-1')
INSERT INTO Update20 (ID,Name) VALUES (2,'U20-2')
INSERT INTO Update20 (ID,Name) VALUES (3,'U20-3')
INSERT INTO Update20 (ID,Name) VALUES (4,'U20-4')
INSERT INTO Update20 (ID,Name) VALUES (5,'U20-5')

INSERT INTO Update21 (ID,Name) VALUES (1,'U21-1')
INSERT INTO Update21 (ID,Name) VALUES (2,'U21-2')
INSERT INTO Update21 (ID,Name) VALUES (3,'U21-3')
INSERT INTO Update21 (ID,Name) VALUES (4,'U21-4')
INSERT INTO Update21 (ID,Name) VALUES (5,'U21-5')
";

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = SetupDB;
            db.ExecuteNonQuery();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            //DALEvents.OnQueryComplete -= QueryComplete;
        }
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

        #region PrepareCommand

        /* These tests don't directly call prepare command, but every call in these tests uses it.
           Since these tests do raw reading these tests also account for raw reading unit tests */

        private const String BasicRead = "SELECT * FROM ReadType1 WITH (NOLOCK)";
        private const String BasicRead2 = "SELECT * FROM ReadType2 WITH (NOLOCK)";
        private const String BasicRead3 = "SELECT * FROM ReadType3 WITH (NOLOCK)";
        private const String BasicRead4 = "SELECT * FROM ReadType4 WITH (NOLOCK)";
        private const String BasicRead5 = "SELECT * FROM ReadType5 WITH (NOLOCK)";
        private const String BasicRead6 = "SELECT * FROM ReadType6 WITH (NOLOCK)";
        private const String BasicRead7 = "SELECT * FROM ReadType7 WITH (NOLOCK)";
        private const String BasicRead8 = "SELECT * FROM ReadType8 WITH (NOLOCK)";
        private const String BasicReadFilteredByID = "SELECT * FROM ReadType1 WHERE ID = @iID";
        private const String BasicReadFilteredByIDWithoutPrefix = "SELECT * FROM ReadType1 WHERE ID = @ID";
        private const String BasicReadSproc = "stp_QueryReadType1";

        private const String BasicScalarRead = "SELECT Name FROM ReadType1 WITH (NOLOCK)";
        private const String BasicScalarReadFilteredByID = "SELECT Name FROM ReadType1 WHERE ID = @iID";

        private const String BasicSetRead =
@"SELECT * FROM ReadType1 WITH (NOLOCK)
SELECT * FROM ReadType2 WITH (NOLOCK)
SELECT * FROM ReadType3 WITH (NOLOCK)
SELECT * FROM ReadType4 WITH (NOLOCK)
SELECT * FROM ReadType5 WITH (NOLOCK)
SELECT * FROM ReadType6 WITH (NOLOCK)
SELECT * FROM ReadType7 WITH (NOLOCK)
SELECT * FROM ReadType8 WITH (NOLOCK)
SELECT * FROM ReadType9 WITH (NOLOCK)
SELECT * FROM ReadType10 WITH (NOLOCK)
SELECT * FROM ReadType11 WITH (NOLOCK)
SELECT * FROM ReadType12 WITH (NOLOCK)
SELECT * FROM ReadType13 WITH (NOLOCK)
SELECT * FROM ReadType14 WITH (NOLOCK)
SELECT * FROM ReadType15 WITH (NOLOCK)
SELECT * FROM ReadType16 WITH (NOLOCK)
SELECT * FROM ReadType17 WITH (NOLOCK)";

        private const String BasicSetReadFilteredByID =
@"SELECT * FROM ReadType1 WHERE ID = @iID
SELECT * FROM ReadType2 WHERE ID = @iID
SELECT * FROM ReadType3 WHERE ID = @iID
SELECT * FROM ReadType4 WHERE ID = @iID
SELECT * FROM ReadType5 WHERE ID = @iID
SELECT * FROM ReadType6 WHERE ID = @iID
SELECT * FROM ReadType7 WHERE ID = @iID
SELECT * FROM ReadType8 WHERE ID = @iID
SELECT * FROM ReadType9 WHERE ID = @iID
SELECT * FROM ReadType10 WHERE ID = @iID
SELECT * FROM ReadType11 WHERE ID = @iID
SELECT * FROM ReadType12 WHERE ID = @iID
SELECT * FROM ReadType13 WHERE ID = @iID
SELECT * FROM ReadType14 WHERE ID = @iID
SELECT * FROM ReadType15 WHERE ID = @iID
SELECT * FROM ReadType16 WHERE ID = @iID";

        private const String WaitDelayTwoSeconds = "WAITFOR DELAY '00:00:02'";

        private const String Update1 = "UPDATE Update1 SET ID = 5 WHERE ID = 1";
        private const String Reset1 = @"TRUNCATE TABLE Update1
INSERT INTO Update1 (ID,Name) VALUES (1,'U1-1')
INSERT INTO Update1 (ID,Name) VALUES (2,'U1-2')
INSERT INTO Update1 (ID,Name) VALUES (3,'U1-3')
INSERT INTO Update1 (ID,Name) VALUES (4,'U1-4')
INSERT INTO Update1 (ID,Name) VALUES (5,'U1-5')";
        private const String Update2 = "UPDATE Update2 SET ID = 5 WHERE ID = 1";
        private const String Reset2 = @"TRUNCATE TABLE Update2
INSERT INTO Update2 (ID,Name) VALUES (1,'U2-1')
INSERT INTO Update2 (ID,Name) VALUES (2,'U2-2')
INSERT INTO Update2 (ID,Name) VALUES (3,'U2-3')
INSERT INTO Update2 (ID,Name) VALUES (4,'U2-4')
INSERT INTO Update2 (ID,Name) VALUES (5,'U2-5')";
        private const String Update3 = "UPDATE Update3 SET ID = 5 WHERE ID = 1";
        private const String Reset3 = @"TRUNCATE TABLE Update3
INSERT INTO Update3 (ID,Name) VALUES (1,'U3-1')
INSERT INTO Update3 (ID,Name) VALUES (2,'U3-2')
INSERT INTO Update3 (ID,Name) VALUES (3,'U3-3')
INSERT INTO Update3 (ID,Name) VALUES (4,'U3-4')
INSERT INTO Update3 (ID,Name) VALUES (5,'U3-5')";
        private const String Update4 = "UPDATE Update4 SET ID = 5 WHERE ID = 1";
        private const String Reset4 = @"TRUNCATE TABLE Update4
INSERT INTO Update4 (ID,Name) VALUES (1,'U4-1')
INSERT INTO Update4 (ID,Name) VALUES (2,'U4-2')
INSERT INTO Update4 (ID,Name) VALUES (3,'U4-3')
INSERT INTO Update4 (ID,Name) VALUES (4,'U4-4')
INSERT INTO Update4 (ID,Name) VALUES (5,'U4-5')";
        private const String Update4_1 = "UPDATE Update4 SET ID = 5 WHERE ID = 1";
        private const String Update4_2 = "UPDATE Update4 SET ID = 5 WHERE ID = 2";
        private const String Update5_1 = "UPDATE Update5 SET ID = 5 WHERE ID = 1";
        private const String Update5_2 = "UPDATE Update5 SET ID = 5 WHERE ID = 2";
        private const String Update6_1 = "UPDATE Update6 SET ID = 5 WHERE ID = 1";
        private const String Update6_2 = "UPDATE Update6 SET ID = 5 WHERE ID = 2";
        private const String Update7_1 = "UPDATE Update7 SET ID = 5 WHERE ID = 1";
        private const String Update7_2 = "UPDATE Update7 SET ID = 5 WHERE ID = 2";
        private const String Update8_1 = "UPDATE Update8 SET ID = 5 WHERE ID = 1";
        private const String Update8_2 = "UPDATE Update8 SET ID = 5 WHERE ID = 2";
        private const String Update9_1 = "UPDATE Update9 SET ID = 5 WHERE ID = 1";
        private const String Update9_2 = "UPDATE Update9 SET ID = 5 WHERE ID = 2";
        private const String SelectUpdate1WhereID5 = "SELECT * FROM Update1 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate1WhereID5Locking = "SELECT * FROM Update1 WHERE ID = 5";
        private const String SelectUpdate2WhereID5 = "SELECT * FROM Update2 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate2WhereID5Locking = "SELECT * FROM Update2 WHERE ID = 5";
        private const String SelectUpdate3WhereID5 = "SELECT * FROM Update3 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate3WhereID5Locking = "SELECT * FROM Update3 WHERE ID = 5";
        private const String SelectUpdate4WhereID5 = "SELECT * FROM Update4 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate5WhereID5 = "SELECT * FROM Update5 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate6WhereID5 = "SELECT * FROM Update6 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate7WhereID5 = "SELECT * FROM Update7 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate8WhereID5 = "SELECT * FROM Update8 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate9WhereID5 = "SELECT * FROM Update9 WITH (NOLOCK) WHERE ID = 5";
        private const String Update10 = "SET NOCOUNT OFF; UPDATE Update10 SET ID = 5 WHERE ID = 1";
        private const String Update11 = "SET NOCOUNT OFF; UPDATE Update11 SET ID = 5 WHERE ID = 1";
        private const String Update12 = "SET NOCOUNT OFF; UPDATE Update12 SET ID = 6 WHERE ID = @iID";
        private const String Update13 = "SET NOCOUNT OFF; UPDATE Update13 SET ID = 6 WHERE ID = @iID";
        private const String SelectUpdate10WhereID5 = "SELECT * FROM Update10 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate11WhereID5 = "SELECT * FROM Update11 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate12FilteredByID = "SELECT * FROM Update12 WITH (NOLOCK) WHERE ID = @iID";
        private const String SelectUpdate13FilteredByID = "SELECT * FROM Update13 WITH (NOLOCK) WHERE ID = @iID";
        private const String Update14 = "SET NOCOUNT OFF; UPDATE Update14 SET ID = 5 WHERE ID = 1";
        private const String Update15 = "SET NOCOUNT OFF; UPDATE Update15 SET ID = 5 WHERE ID = 1";
        private const String Update16 = "SET NOCOUNT OFF; UPDATE Update16 SET ID = 6 WHERE ID = @iID";
        private const String Update17 = "SET NOCOUNT OFF; UPDATE Update17 SET ID = 6 WHERE ID = @iID";
        private const String SelectUpdate14WhereID5 = "SELECT * FROM Update14 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate15WhereID5 = "SELECT * FROM Update15 WITH (NOLOCK) WHERE ID = 5";
        private const String SelectUpdate16FilteredByID = "SELECT * FROM Update16 WITH (NOLOCK) WHERE ID = @iID";
        private const String SelectUpdate17FilteredByID = "SELECT * FROM Update17 WITH (NOLOCK) WHERE ID = @iID";
        private const String Update18 = "SET NOCOUNT OFF; UPDATE Update18 SET ID = 6 WHERE ID = @iID";
        private const String Update19 = "UPDATE Update19 SET ID = 6";
        private const String Update20 = "SET NOCOUNT OFF; UPDATE Update20 SET ID = 6";
        private const String Update21 = "SET NOCOUNT OFF; UPDATE Update21 SET ID = 6";

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ConnectionString_Missing_Fail()
        {
            new SqlDBAccess().ExecuteRead();
        }

        [TestMethod()]
        public void ConnectionString_UsingDefault()
        {
            var db = NewDB;
            db.QueryString = BasicRead;
            db.IsStoredProcedure = false;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        public void QueryString_PassedIn()
        {
            var db = NewDB;
            db.QueryString = BasicRead;
            db.IsStoredProcedure = false;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        public void QueryString_Default()
        {
            var db = NewDB;
            db.QueryString = BasicRead;
            db.IsStoredProcedure = false;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        public void Input_Null()
        {
            var db = NewDB;
            db.QueryString = BasicRead;
            db.IsStoredProcedure = false;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        public void Input_PassObject()
        {
            var db = NewDB;
            db.QueryString = BasicReadFilteredByID;
            db.IsStoredProcedure = false;
            db.Input = new { ID = 5 };
            var result = db.ExecuteRead();
            Assert.AreEqual(1, result.Rows.Count);
        }

        [TestMethod()]
        public void Input_DefaultNull()
        {
            var db = NewDB;
            db.QueryString = BasicRead;
            db.IsStoredProcedure = false;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        public void Input_DefaultObject()
        {
            var db = NewDB;
            db.QueryString = BasicReadFilteredByID;
            db.IsStoredProcedure = false;
            db.Input = new { ID = 5 };
            var result = db.ExecuteRead();
            Assert.AreEqual(1, result.Rows.Count);
        }

        [TestMethod()]
        public void StoredProcedure_DefaultTrue_QueryIsSproc()
        {
            var db = NewDB;
            db.QueryString = BasicReadSproc;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void StoredProcedure_DefaultTrue_QueryIsNotSproc()
        {
            var db = NewDB;
            db.QueryString = BasicRead;
            var result = db.ExecuteRead();
        }

        [TestMethod()]
        public void StoredProcedure_DefaultFalse_QueryIsSproc()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadSproc;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        public void StoredProcedure_DefaultFalse_QueryIsNotSproc()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        public void StoredProcedure_PassedInTrue_QueryIsSproc()
        {
            var db = NewDB;
            db.QueryString = BasicReadSproc;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void StoredProcedure_PassedInTrue_QueryIsNotSproc()
        {
            var db = NewDB;
            db.QueryString = BasicRead;
            var result = db.ExecuteRead();
        }

        [TestMethod()]
        public void StoredProcedure_PassedInFalse_QueryIsSproc()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadSproc;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        public void StoredProcedure_PassedInFalse_QueryIsNotSproc()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadSproc;
            var result = db.ExecuteRead();
            Assert.AreEqual(NumOfReadResults, result.Rows.Count);
        }

        [TestMethod()]
        public void PrefixDirection_Default_True()
        {
            var db = NewDB;
            db.QueryString = BasicReadFilteredByID;
            db.Input = new { ID = 5 };
            db.IsStoredProcedure = false;
            var result = db.ExecuteRead();
            Assert.AreEqual(1, result.Rows.Count);
        }

        [TestMethod()]
        public void PrefixDirection_Default_False()
        {
            var db = NewDB;
            db.QueryString = BasicReadFilteredByIDWithoutPrefix;
            db.IsStoredProcedure = false;
            db.Input = new { ID = 5 };
            db.PrefixDirection = false;
            var result = db.ExecuteRead();
            Assert.AreEqual(1, result.Rows.Count);
        }

        [TestMethod()]
        public void PrefixDirection_PassedIn_True()
        {
            var db = NewDB;
            db.QueryString = BasicReadFilteredByID;
            db.Input = new { ID = 5 };
            db.IsStoredProcedure = false;
            db.PrefixDirection = true;
            var result = db.ExecuteRead();
            Assert.AreEqual(1, result.Rows.Count);
        }

        [TestMethod()]
        public void PrefixDirection_PassedIn_False()
        {
            var db = NewDB;
            db.QueryString = BasicReadFilteredByIDWithoutPrefix;
            db.Input = new { ID = 5 };
            db.IsStoredProcedure = false;
            db.PrefixDirection = false;
            var result = db.ExecuteRead();
            Assert.AreEqual(1, result.Rows.Count);
        }

        [TestMethod()]
        public void Timeout_Default_Pass()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = WaitDelayTwoSeconds;
            var result = db.ExecuteRead();
        }

        [TestMethod()]
        public void Timeout_Default_Pass2()
        {
            var db = NewDB;
            db.CommandTimeout = 3;
            db.QueryString = WaitDelayTwoSeconds;
            db.IsStoredProcedure = false;
            var result = db.ExecuteRead();
        }

        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void Timeout_Default_Fail()
        {
            var db = NewDB;
            db.CommandTimeout = 1;
            db.QueryString = WaitDelayTwoSeconds;
            db.IsStoredProcedure = false;
            var result = db.ExecuteRead();
        }

        [TestMethod()]
        public void Timeout_PassedIn_Pass()
        {
            var db = NewDB;
            db.QueryString = WaitDelayTwoSeconds;
            db.CommandTimeout = 30;
            db.IsStoredProcedure = false;
            var result = db.ExecuteRead();
        }

        [TestMethod()]
        public void Timeout_PassedIn_Pass2()
        {
            var db = NewDB;
            db.CommandTimeout = 3;
            db.QueryString = WaitDelayTwoSeconds;
            db.IsStoredProcedure = false;
            var result = db.ExecuteRead();
        }

        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void Timeout_PassedIn_Fail()
        {
            var db = NewDB;
            db.CommandTimeout = 1;
            db.IsStoredProcedure = false;
            db.QueryString = WaitDelayTwoSeconds;
            var result = db.ExecuteRead();
        }

        [TestMethod()]
        public void Transaction_Default()
        {
            var db = NewDB;
            db.QueryString = Update1;
            db.IsStoredProcedure = false;
            db.BeginTransaction();

            var db2 = NewDB;
            db2.QueryString = Reset1;
            db2.IsStoredProcedure = false;
            db2.ExecuteNonQuery();

            db.ExecuteNonQuery();

            db.QueryString = SelectUpdate1WhereID5;
            Assert.AreEqual(2, db.ExecuteRead().Rows.Count);

            db.CommitTransaction();

            var db3 = NewDB;
            db3.QueryString = SelectUpdate1WhereID5Locking;
            db3.IsStoredProcedure = false;
            Assert.AreEqual(2, db3.ExecuteRead().Rows.Count);
        }

        [TestMethod()]
        public void Transaction_Default_Rollback()
        {
            var db = NewDB;
            db.QueryString = Update2;
            db.IsStoredProcedure = false;
            db.BeginTransaction();

            var db2 = NewDB;
            db2.QueryString = Reset2;
            db2.IsStoredProcedure = false;
            db2.ExecuteNonQuery();
            db.ExecuteNonQuery();

            db.QueryString = SelectUpdate2WhereID5;

            Assert.AreEqual(2, db.ExecuteRead().Rows.Count);

            db.RollbackTransaction();

            var db3 = NewDB;
            db3.QueryString = SelectUpdate2WhereID5Locking;
            db3.IsStoredProcedure = false;

            Assert.AreEqual(1, db3.ExecuteRead().Rows.Count);
        }

        [TestMethod()]
        public void Transaction_PassedIn()
        {
            var db = NewDB;
            db.Connection = new SqlConnection(ConnectionString);
            db.Connection.Open();
            db.QueryString = Update3;
            db.IsStoredProcedure = false;


            var db2 = NewDB;
            db2.QueryString = Reset3;
            db2.IsStoredProcedure = false;
            db2.ExecuteNonQuery();

            db.Transaction = db.Connection.BeginTransaction();
            db.ExecuteNonQuery();

            db.QueryString = SelectUpdate3WhereID5;

            Assert.AreEqual(2, db.ExecuteRead().Rows.Count);

            db.Transaction.Commit();

            var db3 = NewDB;
            db3.QueryString = SelectUpdate3WhereID5Locking;
            db3.IsStoredProcedure = false;
            Assert.AreEqual(2, db3.ExecuteRead().Rows.Count);
        }

        [TestMethod()]
        public void Transaction_PassedIn_Rollback()
        {
            var db = NewDB;
            db.Connection = new SqlConnection(ConnectionString);
            db.Connection.Open();
            db.QueryString = Update4;
            db.IsStoredProcedure = false;

            var db2 = NewDB;
            db2.QueryString = Reset4;
            db2.IsStoredProcedure = false;
            db2.ExecuteNonQuery();


            db.Transaction = db.Connection.BeginTransaction();
            db.ExecuteNonQuery();

            db.QueryString = SelectUpdate4WhereID5;
            Assert.AreEqual(2, db.ExecuteRead().Rows.Count);

            db.Transaction.Rollback();

            var db3 = NewDB;
            db3.QueryString = SelectUpdate4WhereID5;
            db3.IsStoredProcedure = false;
            Assert.AreEqual(1, db3.ExecuteRead().Rows.Count);
        }

        private SqlConnection Connection_Test_DB_Access1;
        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void Connection_This_Piece_Fails1()
        {
            Connection_Test_DB_Access1 = new SqlConnection(ConnectionString);
            var db = new SqlDBAccess();
            db.QueryString = WaitDelayTwoSeconds;
            db.IsStoredProcedure = false;
            db.CommandTimeout = 1;
            db.Connection = Connection_Test_DB_Access1;
            db.OnQueryException += new System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALSqlExceptionHandler(Connection_This_Piece_Passes1);
            db.CommandTimeout = 1;
            try
            {
                var result = db.ExecuteRead();
            }
            finally { }
        }

        private void Connection_This_Piece_Passes1(SqlDBAccess sender, System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALSqlExceptionEventArgs e)
        {
            Assert.AreEqual(Connection_Test_DB_Access1, e.Connection);
        }

        private SqlDBAccess Connection_Test_DB_Access2;
        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void Connection_This_Piece_Fails2()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            Connection_Test_DB_Access2 = new SqlDBAccess();
            Connection_Test_DB_Access2.Connection = conn;
            Connection_Test_DB_Access2.OnQueryException += new System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALSqlExceptionHandler(Connection_This_Piece_Passes2);
            Connection_Test_DB_Access2.CommandTimeout = 1;
            Connection_Test_DB_Access2.QueryString = WaitDelayTwoSeconds;
            Connection_Test_DB_Access2.IsStoredProcedure = false;
            try
            {
                var result = Connection_Test_DB_Access2.ExecuteRead();
            }
            finally { }
        }

        private void Connection_This_Piece_Passes2(SqlDBAccess sender, System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALSqlExceptionEventArgs e)
        {
            Assert.AreEqual(Connection_Test_DB_Access2.Connection, e.Connection);
        }
        #endregion

        #region Transactions
        [TestMethod()]
        public void Transaction_LastSavePoint_Null()
        {
            var db = NewDB;
            db.BeginTransaction();
            var lastSave = db.LastSavePoint;

            Assert.AreEqual(null, lastSave);
        }

        [TestMethod()]
        public void Transaction_LastSavePoint_Populated()
        {
            var db = NewDB;
            db.BeginTransaction();
            db.QueryString = BasicRead;
            db.IsStoredProcedure = false;
            db.ExecuteRead();
            db.SaveTransaction("First");
            var lastSave = db.LastSavePoint;

            Assert.AreEqual<String>("First", lastSave);

            db.CommitTransaction();
        }

        [TestMethod()]
        public void Transaction_LastSavePoint_Count0()
        {
            var db = NewDB;
            db.BeginTransaction();

            Assert.AreEqual<int>(0, db.SavePoints.Count);
        }

        [TestMethod()]
        public void Transaction_LastSavePoint_Count2()
        {
            var db = NewDB;
            db.BeginTransaction();
            db.QueryString = BasicRead;
            db.IsStoredProcedure = false;
            db.ExecuteRead();
            db.SaveTransaction("First");
            db.SaveTransaction("Second");

            Assert.AreEqual<int>(2, db.SavePoints.Count);

            db.CommitTransaction();
        }

        [TestMethod()]
        public void Transaction_InTransaction_True1()
        {
            var db = NewDB;
            db.BeginTransaction();

            Assert.AreEqual<Boolean>(true, db.InTransaction);
        }

        [TestMethod()]
        public void Transaction_InTransaction_False()
        {
            var db = NewDB;

            Assert.AreEqual<Boolean>(false, db.InTransaction);
        }

        [TestMethod()]
        public void Transaction_InTransaction_True2()
        {
            var db = NewDB;
            db.BeginTransaction();
            db.QueryString = BasicRead;
            db.IsStoredProcedure = false;
            db.ExecuteRead();

            Assert.AreEqual<Boolean>(true, db.InTransaction);

            db.CommitTransaction();
        }

        [TestMethod()]
        public void Transaction_NextIsTransaction_True()
        {
            var db = NewDB;
            db.BeginTransaction();

            Assert.AreEqual<Boolean>(true, db.NextIsTransaction);
        }

        [TestMethod()]
        public void Transaction_NextIsTransaction_False1()
        {
            var db = NewDB;
            db.BeginTransaction();
            db.QueryString = BasicRead;
            db.IsStoredProcedure = false;
            db.ExecuteRead();

            Assert.AreEqual<Boolean>(false, db.NextIsTransaction);

            db.CommitTransaction();
        }

        [TestMethod()]
        public void Transaction_NextIsTransaction_False2()
        {
            var db = NewDB;

            Assert.AreEqual<Boolean>(false, db.NextIsTransaction);
        }

        [TestMethod()]
        public void Transaction_SavePoints_RollbackToLastSavePoint()
        {
            var db = NewDB;
            db.AutoRollback = true;
            db.AutoRollbackCompletely = false;
            db.IsStoredProcedure = false;
            db.QueryString = SelectUpdate4WhereID5;

            int count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(1, count);

            db.BeginTransaction();

            db.QueryString = Update4_1;
            db.ExecuteNonQuery();
            db.SaveTransaction("One");

            db.QueryString = SelectUpdate4WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(2, count);

            db.QueryString = Update4_2;
            db.ExecuteNonQuery();

            db.QueryString = SelectUpdate4WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(3, count);

            db.RollbackToLastSavePoint();
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(2, count);

            db.CommitTransaction();
        }

        [TestMethod()]
        public void Transaction_SavePoints_RollbackToNamedSavePoint()
        {
            var db = NewDB;
            db.AutoRollback = true;
            db.AutoRollbackCompletely = false;
            db.IsStoredProcedure = false;
            db.QueryString = SelectUpdate5WhereID5;

            int count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(1, count);

            db.BeginTransaction();

            db.QueryString = Update5_1;
            db.ExecuteNonQuery();
            db.SaveTransaction("One");

            db.QueryString = SelectUpdate5WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(2, count);

            db.QueryString = Update5_2;
            db.ExecuteNonQuery();
            db.SaveTransaction("Two");

            db.QueryString = SelectUpdate5WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(3, count);

            db.RollbackTransaction("One");
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(2, count);

            db.CommitTransaction();
        }

        [TestMethod()]
        public void Transaction_SavePoints_RollbackCompletely()
        {
            var db = NewDB;
            db.AutoRollback = true;
            db.AutoRollbackCompletely = true;
            db.IsStoredProcedure = false;
            db.QueryString = SelectUpdate6WhereID5;

            int count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(1, count);

            db.BeginTransaction();

            db.QueryString = Update6_1;
            db.ExecuteNonQuery();
            db.SaveTransaction("One");

            db.QueryString = SelectUpdate6WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(2, count);

            db.QueryString = Update6_2;
            db.ExecuteNonQuery();
            db.SaveTransaction("Two");

            db.QueryString = SelectUpdate6WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(3, count);

            db.RollbackTransaction();
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(1, count);
        }

        [TestMethod()]
        public void Transaction_SavePoints_AutoRollbackToLastSavePoint()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.AutoRollback = true;
            db.AutoRollbackCompletely = false;

            db.QueryString = SelectUpdate7WhereID5;
            int count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(1, count);

            db.BeginTransaction();

            db.QueryString = Update7_1;
            db.ExecuteNonQuery();
            db.SaveTransaction("One");

            db.QueryString = SelectUpdate7WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(2, count);

            db.QueryString = Update7_2;
            db.ExecuteNonQuery();

            db.QueryString = SelectUpdate7WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(3, count);

            try
            {
                db.QueryString = "";
                db.ExecuteRead();
            }
            catch { }

            db.QueryString = SelectUpdate7WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(2, count);

            db.CommitTransaction();
        }

        [TestMethod()]
        public void Transaction_SavePoints_AutoRollbackToLastSavePoint_NoSavePoint()
        {
            var db = NewDB;
            db.AutoRollback = true;
            db.AutoRollbackCompletely = false;
            db.IsStoredProcedure = false;

            db.QueryString = SelectUpdate7WhereID5;
            int count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(1, count);

            db.BeginTransaction();
            db.QueryString = Update7_1;
            db.ExecuteNonQuery();

            db.QueryString = SelectUpdate7WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(2, count);

            db.QueryString = Update7_2;
            db.ExecuteNonQuery();

            db.QueryString = SelectUpdate7WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(3, count);

            try
            {
                db.QueryString = "";
                db.ExecuteRead();
            }
            catch { }

            db.QueryString = SelectUpdate7WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(1, count);
        }

        [TestMethod()]
        public void Transaction_SavePoints_AutoRollbackCompletely()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.AutoRollback = true;
            db.AutoRollbackCompletely = true;

            db.QueryString = SelectUpdate8WhereID5;
            int count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(1, count);

            db.AutoRollbackCompletely = true;
            db.BeginTransaction();

            db.QueryString = Update8_1;
            db.ExecuteNonQuery();
            db.SaveTransaction("One");

            db.QueryString = SelectUpdate8WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(2, count);

            db.QueryString = Update8_2;
            db.ExecuteNonQuery();
            db.SaveTransaction("Two");

            db.QueryString = SelectUpdate8WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(3, count);

            try
            {
                db.QueryString = "";
                db.ExecuteRead();
            }
            catch { }

            db.QueryString = SelectUpdate8WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(1, count);
        }

        [TestMethod()]
        public void Transaction_SavePoints_AutoRollbackFalse()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.AutoRollback = false;

            db.QueryString = SelectUpdate9WhereID5;
            int count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(1, count);

            db.BeginTransaction(autoRollback: false);

            db.QueryString = Update9_1;
            db.ExecuteNonQuery();
            db.SaveTransaction("One");

            db.QueryString = SelectUpdate9WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(2, count);

            db.QueryString = Update9_2;
            db.ExecuteNonQuery();
            db.SaveTransaction("Two");

            db.QueryString = SelectUpdate9WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(3, count);

            try
            {
                db.QueryString = "";
                db.ExecuteRead();
            }
            catch { }

            db.QueryString = SelectUpdate9WhereID5;
            count = db.ExecuteRead().Rows.Count;
            Assert.AreEqual<int>(3, count);

            db.CommitTransaction();
        }

        [TestMethod()]
        [ExpectedException(typeof(TransactionInProgressException))]
        public void Transaction_TransactionInProgress_Fail()
        {
            var db = NewDB;
            db.BeginTransaction();
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            db.ExecuteRead();

            db.BeginTransaction();
        }

        [TestMethod()]
        [ExpectedException(typeof(TransactionNotInProgressException))]
        public void Transaction_NoTransactionToCommit_Fail()
        {
            var db = NewDB;
            db.CommitTransaction();
        }

        [TestMethod()]
        [ExpectedException(typeof(TransactionNotInProgressException))]
        public void Transaction_NoTransactionToRollback1_Fail()
        {
            var db = NewDB;
            db.RollbackTransaction();
        }

        [TestMethod()]
        [ExpectedException(typeof(TransactionNotInProgressException))]
        public void Transaction_NoTransactionToRollback2_Fail()
        {
            var db = NewDB;
            db.RollbackTransaction("First");
        }

        [TestMethod()]
        [ExpectedException(typeof(TransactionSavePointNotFoundException))]
        public void Transaction_SavePointNotFound_Fail()
        {
            var db = NewDB;
            db.BeginTransaction();
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            db.ExecuteRead();
            db.SaveTransaction("First");
            db.RollbackTransaction("Second");
        }

        [TestMethod()]
        [ExpectedException(typeof(TransactionNotInProgressException))]
        public void Transaction_NoTransactionToSave_Fail()
        {
            var db = NewDB;
            db.SaveTransaction("First");
        }

        [TestMethod()]
        [ExpectedException(typeof(TransactionSavePointAlreadyExistsException))]
        public void Transaction_TransactionAlreadyHasSavePoint_Fail()
        {
            var db = NewDB;
            db.BeginTransaction();
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            db.ExecuteRead();
            db.SaveTransaction("First");
            db.SaveTransaction("First");
        }

        [TestMethod()]
        [ExpectedException(typeof(TransactionNoSavePointsFoundException))]
        public void Transaction_TransactionRollbackToLastSavePoint_Fail()
        {
            var db = NewDB;
            db.BeginTransaction();
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            db.ExecuteRead();
            db.RollbackToLastSavePoint();
        }
        #endregion

        #region Queries
        #region ReadTypeReturn classes
        public class ReadType1Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType2Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType3Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType4Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType5Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType6Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType7Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType8Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType9Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType10Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType11Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType12Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType13Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType14Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType15Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType16Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        public class ReadType17Return
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }
        #endregion

        #region ExecuteRead
        [TestMethod()]
        public void ExecuteRead_Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.Input = new List<SqlParameter>
            {
                new SqlParameter("@iID", 1)
            };
            db.QueryString = BasicReadFilteredByID;

            var rows = db.ExecuteRead<ReadType1Return>();
            Assert.AreEqual<int>(1, rows.Count);
            Assert.AreEqual<int>(1, rows[0].ID);
            Assert.AreEqual<String>("R1-1", rows[0].Name);
        }

        public class TestClass
        {
            public String Name { get; set; }
        }

        [TestMethod()]
        public void ExecuteRead_ReturnHasLessPropertiesThanColumns_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            var actual = db.ExecuteRead<TestClass>();

            Assert.AreEqual<int>(5, actual.Count);
            Assert.AreEqual<String>("R1-1", actual[0].Name);
            Assert.AreEqual<String>("R1-2", actual[1].Name);
            Assert.AreEqual<String>("R1-3", actual[2].Name);
            Assert.AreEqual<String>("R1-4", actual[3].Name);
            Assert.AreEqual<String>("R1-5", actual[4].Name);
        }

        [TestMethod()]
        public void ExecuteReadGeneric_NoReturnRows_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = "SELECT * FROM ReadType1 WITH (NOLOCK) WHERE NAme = 'R90-90'";
            var actual = db.ExecuteRead<TestClass>();

            Assert.AreEqual<int>(0, actual.Count);
        }
        #endregion

        #region ExecuteReadAsync
        [TestMethod()]
        public void ExecuteRead_Async_Raw_NoInput_Test()
        {
            Boolean done = false;
            DataTable dt = new DataTable();

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            db.ExecuteReadAsync((r) => { dt = r; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(5, dt.Rows.Count);
            Assert.AreEqual<int>(1, (int)dt.Rows[0]["ID"]);
            Assert.AreEqual<String>("R1-1", (String)dt.Rows[0]["Name"]);
        }

        [TestMethod()]
        public void ExecuteRead_Async_Generic_NoInput_Test()
        {
            Boolean done = false;
            List<ReadType1Return> rows = new List<ReadType1Return>();

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            db.ExecuteReadAsync<ReadType1Return>((r) => { rows = r; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(5, rows.Count);
            Assert.AreEqual<int>(1, rows[0].ID);
            Assert.AreEqual<String>("R1-1", (String)rows[0].Name);
        }

        [TestMethod()]
        public void ExecuteRead_Async_Raw_Input_Test()
        {
            Boolean done = false;
            DataTable dt = new DataTable();

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadFilteredByID;
            db.Input = new { ID = 1 };
            db.ExecuteReadAsync((r) => { dt = r; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, dt.Rows.Count);
            Assert.AreEqual<int>(1, (int)dt.Rows[0]["ID"]);
            Assert.AreEqual<String>("R1-1", (String)dt.Rows[0]["Name"]);
        }

        [TestMethod()]
        public void ExecuteRead_Async_Generic_Input_Test()
        {
            Boolean done = false;
            List<ReadType1Return> rows = new List<ReadType1Return>();

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadFilteredByID;
            db.Input = new { ID = 1 };
            db.ExecuteReadAsync<ReadType1Return>((r) => { rows = r; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, rows.Count);
            Assert.AreEqual<int>(1, rows[0].ID);
            Assert.AreEqual<String>("R1-1", rows[0].Name);
        }
        #endregion

        #region ExecuteReadSingle
        [TestMethod()]
        public void ExecuteReadSingle_Raw_NoInput_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            var row = db.ExecuteReadSingle();

            Assert.AreEqual<int>(1, (int)row["ID"]);
            Assert.AreEqual<String>("R1-1", (String)row["Name"]);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Generic_NoInput_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            var row = db.ExecuteReadSingle<ReadType1Return>();

            Assert.AreEqual<int>(1, row.ID);
            Assert.AreEqual<String>("R1-1", row.Name);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Raw_Input_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadFilteredByID;
            db.Input = new { ID = 1 };
            var row = db.ExecuteReadSingle();

            Assert.AreEqual<int>(1, (int)row["ID"]);
            Assert.AreEqual<String>("R1-1", (String)row["Name"]);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Generic_Input_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadFilteredByID;
            db.Input = new { ID = 1 };
            var row = db.ExecuteReadSingle<ReadType1Return>();

            Assert.AreEqual<int>(1, row.ID);
            Assert.AreEqual<String>("R1-1", row.Name);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Generic_Input_NoResult_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadFilteredByID;
            db.Input = new { ID = 6 };
            var row = db.ExecuteReadSingle<ReadType1Return>();

            Assert.AreEqual<ReadType1Return>(null, row);
        }
        #endregion

        #region ExecuteReadSingleAsync
        [TestMethod()]
        public void ExecuteReadSingle_Async_Raw_NoInput_Test()
        {
            Boolean done = false;
            DataRow row = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            db.ExecuteReadSingleAsync((r) => { row = r; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, (int)row["ID"]);
            Assert.AreEqual<String>("R1-1", (String)row["Name"]);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Async_Generic_NoInput_Test()
        {
            Boolean done = false;
            ReadType1Return row = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicRead;
            db.ExecuteReadSingleAsync<ReadType1Return>((r) => { row = r; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, row.ID);
            Assert.AreEqual<String>("R1-1", row.Name);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Async_Raw_Input_Test()
        {
            Boolean done = false;
            DataRow row = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadFilteredByID;
            db.Input = new { ID = 1 };
            db.ExecuteReadSingleAsync((r) => { row = r; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, (int)row["ID"]);
            Assert.AreEqual<String>("R1-1", (String)row["Name"]);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Async_Generic_Input_Test()
        {
            Boolean done = false;
            ReadType1Return row = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicReadFilteredByID;
            db.Input = new { ID = 1 };
            db.ExecuteReadSingleAsync<ReadType1Return>((r) => { row = r; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, row.ID);
            Assert.AreEqual<String>("R1-1", row.Name);
        }
        #endregion

        //need to do
        #region ExecuteSetRead
        List<Type> setreadmodels = new List<Type>
            {
                typeof(ReadType1Return),
                typeof(ReadType2Return),
                typeof(ReadType3Return),
                typeof(ReadType4Return),
                typeof(ReadType5Return),
                typeof(ReadType6Return),
                typeof(ReadType7Return),
                typeof(ReadType8Return),
                typeof(ReadType9Return),
                typeof(ReadType10Return),
                typeof(ReadType11Return),
                typeof(ReadType12Return),
                typeof(ReadType13Return),
                typeof(ReadType14Return),
                typeof(ReadType15Return),
                typeof(ReadType16Return),
                typeof(ReadType17Return)
            };
        #region IModel
        [TestMethod()]
        public void ExecuteSetRead_2Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_3Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_4Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_5Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_6Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_7Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_8Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_9Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_10Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_11Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_12Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables.Table12.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_13Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables.Table12.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[12], tables.Table13.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_14Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return, ReadType14Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables.Table12.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[12], tables.Table13.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[13], tables.Table14.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_15Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return, ReadType14Return, ReadType15Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables.Table12.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[12], tables.Table13.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[13], tables.Table14.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[14], tables.Table15.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_16Generic_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return, ReadType14Return, ReadType15Return, ReadType16Return>();

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables.Table12.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[12], tables.Table13.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[13], tables.Table14.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[14], tables.Table15.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[15], tables.Table16.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_17Generic_Test()
        {
            var types = new Type[]
            {
                typeof(ReadType1Return),
                typeof(ReadType2Return),
                typeof(ReadType3Return),
                typeof(ReadType4Return),
                typeof(ReadType5Return),
                typeof(ReadType6Return),
                typeof(ReadType7Return),
                typeof(ReadType8Return),
                typeof(ReadType9Return),
                typeof(ReadType10Return),
                typeof(ReadType11Return),
                typeof(ReadType12Return),
                typeof(ReadType13Return),
                typeof(ReadType14Return),
                typeof(ReadType15Return),
                typeof(ReadType16Return),
                typeof(ReadType17Return)
            };
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead(types);

            Assert.AreEqual<Type>(setreadmodels[0], tables[0].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables[1].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables[2].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables[3].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables[4].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables[5].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables[6].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables[7].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables[8].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables[9].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables[10].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables[11].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[12], tables[12].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[13], tables[13].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[14], tables[14].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[15], tables[15].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[16], tables[16].GetIEnumerableGenericType());
        }
        #endregion

        #region Raw
        [TestMethod()]
        public void ExecuteSetRead_Raw_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            var tables = db.ExecuteSetRead();

            Assert.AreEqual<int>(17, tables.Tables.Count);
            Assert.AreEqual<int>(5, tables.Tables[0].Rows.Count);

            for (int i = 1; i < tables.Tables.Count; i++)
            {
                Assert.AreEqual<int>(i + 1, tables.Tables[i].Rows.Count);
            }
        }
        #endregion

        #region IModelAsync
        [TestMethod()]
        public void ExecuteSetRead_Async_2Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_3Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_4Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_5Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_6Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_7Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_8Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_9Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_10Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_11Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_12Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables.Table12.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_13Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables.Table12.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[12], tables.Table13.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_14Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return, ReadType14Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return, ReadType14Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables.Table12.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[12], tables.Table13.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[13], tables.Table14.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_15Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return, ReadType14Return, ReadType15Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return, ReadType14Return, ReadType15Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables.Table12.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[12], tables.Table13.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[13], tables.Table14.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[14], tables.Table15.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_16Generic_Test()
        {
            Boolean done = false;
            DALTuple<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return, ReadType14Return, ReadType15Return, ReadType16Return> tables = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return, ReadType3Return, ReadType4Return, ReadType5Return, ReadType6Return, ReadType7Return, ReadType8Return, ReadType9Return, ReadType10Return, ReadType11Return, ReadType12Return, ReadType13Return, ReadType14Return, ReadType15Return, ReadType16Return>((ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables.Table1.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables.Table2.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables.Table3.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables.Table4.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables.Table5.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables.Table6.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables.Table7.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables.Table8.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables.Table9.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables.Table10.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables.Table11.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables.Table12.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[12], tables.Table13.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[13], tables.Table14.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[14], tables.Table15.GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[15], tables.Table16.GetIEnumerableGenericType());
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_17Generic_Test()
        {
            Boolean done = false;
            List<List<Object>> tables = null;

            var types = new Type[]
            {
                typeof(ReadType1Return),
                typeof(ReadType2Return),
                typeof(ReadType3Return),
                typeof(ReadType4Return),
                typeof(ReadType5Return),
                typeof(ReadType6Return),
                typeof(ReadType7Return),
                typeof(ReadType8Return),
                typeof(ReadType9Return),
                typeof(ReadType10Return),
                typeof(ReadType11Return),
                typeof(ReadType12Return),
                typeof(ReadType13Return),
                typeof(ReadType14Return),
                typeof(ReadType15Return),
                typeof(ReadType16Return),
                typeof(ReadType17Return)
            };

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync(types, (ds) => { tables = ds; done = true; });

            while (!done) Thread.Sleep(1);

            Assert.AreEqual<Type>(setreadmodels[0], tables[0].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[1], tables[1].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[2], tables[2].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[3], tables[3].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[4], tables[4].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[5], tables[5].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[6], tables[6].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[7], tables[7].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[8], tables[8].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[9], tables[9].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[10], tables[10].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[11], tables[11].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[12], tables[12].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[13], tables[13].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[14], tables[14].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[15], tables[15].GetIEnumerableGenericType());
            Assert.AreEqual<Type>(setreadmodels[16], tables[16].GetIEnumerableGenericType());
        }
        #endregion

        #region RawAsync
        [TestMethod()]
        public void ExecuteSetRead_Async_Raw_Test()
        {
            Boolean done = false;
            DataSet dset = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicSetRead;
            db.ExecuteSetReadAsync((ds) => { dset = ds; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(17, dset.Tables.Count);
            Assert.AreEqual<int>(5, dset.Tables[0].Rows.Count);

            for (int i = 1; i < dset.Tables.Count; i++)
            {
                Assert.AreEqual<int>(i + 1, dset.Tables[i].Rows.Count);
            }
        }
        #endregion
        #endregion

        #region ExecuteRelatedSetRead
        #region Sync
        public class RelatedSetReadReadType
        {
            public int ParentID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }

            public List<RelatedSetReadReadType> ChildTypes { get; set; }
        }

        public class RelatedSetReadReadTypeChildIsRuntimeType
        {
            public int ParentID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }

            public List<Object> ChildTypes { get; set; }
        }

        public class RelatedSetReadReadTypeListWrongType
        {
            public int ParentID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }

            public List<ReadType1Return> ChildTypes { get; set; }
        }

        public class RelatedSetReadReadType_NotAList
        {
            public int ParentID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }

            public Object ChildTypes { get; set; }
        }

        public class ReadTypeObjectList
        {
            public int ParentID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }

            public List<Object> ChildTypes { get; set; }
        }

        private String GetSelectStatements(int num)
        {
            var selectStatements = new List<String>(num);
            for (int i = 1; i <= num; i++)
            {
                selectStatements.Add(String.Format("SELECT TOP 2 {0} [ParentID],* FROM ReadType{1}", i - 1, i));
            }

            return String.Join(Environment.NewLine, selectStatements);
        }

        private List<DALRelationship> GetRelationships(int num)
        {
            var rels = new List<DALRelationship>(num);
            for (int i = 1; i < num; i++)
            {
                rels.Add(new DALRelationship
                {
                    ChildColumn = "ParentID",
                    ParentColumn = "ID",
                    ParentProperty = "ChildTypes"
                });
            }

            return rels;
        }

        private void AssertGenericRelatedTableCorrect(List<RelatedSetReadReadType> children, int depth)
        {
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                Assert.AreEqual<int>(i + 1, child.ID);
                Assert.AreEqual<String>(String.Format("R{0}-{1}", depth, i + 1), child.Name);

                var childChildren = child.ChildTypes;
                if (childChildren != null)
                    this.AssertGenericRelatedTableCorrect(childChildren, depth + 1);
            }
        }

        private void AssertRelatedTableCorrect(List<Object> children, int depth)
        {
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                var fda = FastDynamicAccess.Get(child);
                Assert.AreEqual<int>(i + 1, fda.Get<int>(child, "ID"));
                Assert.AreEqual<String>(String.Format("R{0}-{1}", depth, i + 1), fda.Get<String>(child, "Name"));

                var childChildren = fda.GetList(child, "ChildTypes");
                if (childChildren != null)
                    this.AssertRelatedTableCorrect(childChildren, depth + 1);
            }
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_2GenericParams_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_2GenericParams_RuntimeType_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<Object, Object>(relationships);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_3GenericParams_Test()
        {
            int size = 3;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_4GenericParams_Test()
        {
            int size = 4;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_5GenericParams_Test()
        {
            int size = 5;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_6GenericParams_Test()
        {
            int size = 6;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_7GenericParams_Test()
        {
            int size = 7;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_8GenericParams_Test()
        {
            int size = 8;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_9GenericParams_Test()
        {
            int size = 9;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_10GenericParams_Test()
        {
            int size = 10;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_11GenericParams_Test()
        {
            int size = 11;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_12GenericParams_Test()
        {
            int size = 12;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_13GenericParams_Test()
        {
            int size = 13;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_14GenericParams_Test()
        {
            int size = 14;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_15GenericParams_Test()
        {
            int size = 15;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_16GenericParams_Test()
        {
            int size = 16;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_17_Test()
        {
            int size = 17;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            var types = new Type[]
            {
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object)
            };

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead(relationships, types);
            this.AssertRelatedTableCorrect(relationship[0], 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_17_KnownTypes_Test()
        {
            int size = 17;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            var types = new Type[]
            {
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList)
            };

            db.QueryString = sql;
            var relationship = db.ExecuteRelatedSetRead(relationships, types);
            this.AssertRelatedTableCorrect(relationship[0], 1);
        }

        #region Custom Relationships
        public class RelatedSetReadCustRel1
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel2
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel3
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel4
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel5
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }

            public List<RelatedSetReadCustRel1> CustRel1Objects { get; set; }
            public List<RelatedSetReadCustRel2> CustRel2Objects { get; set; }
            public List<RelatedSetReadCustRel3> CustRel3Objects { get; set; }
            public List<RelatedSetReadCustRel4> CustRel4Objects { get; set; }
            public List<RelatedSetReadCustRel6> CustRel6Objects { get; set; }
            public List<RelatedSetReadCustRel7> CustRel7Objects { get; set; }
            public List<RelatedSetReadCustRel8> CustRel8Objects { get; set; }
            public List<RelatedSetReadCustRel9> CustRel9Objects { get; set; }
            public List<RelatedSetReadCustRel10> CustRel10Objects { get; set; }
            public List<RelatedSetReadCustRel11> CustRel11Objects { get; set; }
            public List<RelatedSetReadCustRel12> CustRel12Objects { get; set; }
            public List<RelatedSetReadCustRel13> CustRel13Objects { get; set; }
            public List<RelatedSetReadCustRel14> CustRel14Objects { get; set; }
            public List<RelatedSetReadCustRel15> CustRel15Objects { get; set; }
            public List<RelatedSetReadCustRel16> CustRel16Objects { get; set; }
        }

        public class RelatedSetReadNonGenericCustRel5
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }

            public List<Object> CustRel1Objects { get; set; }
            public List<Object> CustRel2Objects { get; set; }
            public List<Object> CustRel3Objects { get; set; }
            public List<Object> CustRel4Objects { get; set; }
            public List<Object> CustRel6Objects { get; set; }
            public List<Object> CustRel7Objects { get; set; }
            public List<Object> CustRel8Objects { get; set; }
            public List<Object> CustRel9Objects { get; set; }
            public List<Object> CustRel10Objects { get; set; }
            public List<Object> CustRel11Objects { get; set; }
            public List<Object> CustRel12Objects { get; set; }
            public List<Object> CustRel13Objects { get; set; }
            public List<Object> CustRel14Objects { get; set; }
            public List<Object> CustRel15Objects { get; set; }
            public List<Object> CustRel16Objects { get; set; }
            public List<Object> CustRel17Objects { get; set; }
        }

        public class RelatedSetReadCustRel6
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel7
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel8
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel9
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel10
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel11
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel12
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel13
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel14
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel15
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel16
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public class RelatedSetReadCustRel17
        {
            public int ParentID { get; set; }
            public int ThisID { get; set; }
            public int ID { get; set; }
            public String Name { get; set; }
        }
        
        [TestMethod()]
        public void ExecuteRelatedSetRead_CustomRelationships_Test()
        {
            String sql = @"SELECT TOP 1 5 [ParentID],1 [ThisID], * FROM ReadType1
SELECT 5 [ParentID],2 [ThisID],* FROM ReadType2
SELECT 5 [ParentID],3 [ThisID],* FROM ReadType3
SELECT 5 [ParentID],4 [ThisID],* FROM ReadType4
SELECT 1 [ParentID],5 [ThisID],* FROM ReadType5
SELECT 5 [ParentID],6 [ThisID],* FROM ReadType6
SELECT 5 [ParentID],7 [ThisID],* FROM ReadType7
SELECT 5 [ParentID],8 [ThisID],* FROM ReadType8
SELECT 5 [ParentID],9 [ThisID],* FROM ReadType9
SELECT 5 [ParentID],10 [ThisID],* FROM ReadType10
SELECT 5 [ParentID],11 [ThisID],* FROM ReadType11
SELECT 5 [ParentID],12 [ThisID],* FROM ReadType12
SELECT 5 [ParentID],13 [ThisID],* FROM ReadType13
SELECT 5 [ParentID],14 [ThisID],* FROM ReadType14
SELECT 5 [ParentID],15 [ThisID],* FROM ReadType15
SELECT 5 [ParentID],16 [ThisID],* FROM ReadType16";

            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel1Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 0
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel2Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 1
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel3Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 2
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel14Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 13
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel4Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 3
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel7Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 6
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel6Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 5
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel8Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 7
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel11Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 10
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel9Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 8
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel10Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 9
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel13Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 12
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel15Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 14
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel12Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 11
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel16Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 15
                }
            };

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = sql;
            var tuple = db.ExecuteRelatedSetRead<RelatedSetReadCustRel1, RelatedSetReadCustRel2, RelatedSetReadCustRel3, RelatedSetReadCustRel4, RelatedSetReadCustRel5, RelatedSetReadCustRel6, RelatedSetReadCustRel7, RelatedSetReadCustRel8, RelatedSetReadCustRel9, RelatedSetReadCustRel10, RelatedSetReadCustRel11, RelatedSetReadCustRel12, RelatedSetReadCustRel13, RelatedSetReadCustRel14, RelatedSetReadCustRel15, RelatedSetReadCustRel16>(relationships);
            //since each return set has a different number of objects, getting the count is good enough.
            //if the counts match, the correct type was put into the correct list
            //since the child index is what is being switched on, the parent order should not matter.
            //this one test should be good enough to make sure things are going into the correct spots
            foreach (var objs in tuple.Table5)
            {
                Assert.AreEqual<int>(1, objs.CustRel1Objects.Count);
                Assert.AreEqual<int>(2, objs.CustRel2Objects.Count);
                Assert.AreEqual<int>(3, objs.CustRel3Objects.Count);
                Assert.AreEqual<int>(4, objs.CustRel4Objects.Count);
                Assert.AreEqual<int>(6, objs.CustRel6Objects.Count);
                Assert.AreEqual<int>(7, objs.CustRel7Objects.Count);
                Assert.AreEqual<int>(8, objs.CustRel8Objects.Count);
                Assert.AreEqual<int>(9, objs.CustRel9Objects.Count);
                Assert.AreEqual<int>(10, objs.CustRel10Objects.Count);
                Assert.AreEqual<int>(11, objs.CustRel11Objects.Count);
                Assert.AreEqual<int>(12, objs.CustRel12Objects.Count);
                Assert.AreEqual<int>(13, objs.CustRel13Objects.Count);
                Assert.AreEqual<int>(14, objs.CustRel14Objects.Count);
                Assert.AreEqual<int>(15, objs.CustRel15Objects.Count);
                Assert.AreEqual<int>(16, objs.CustRel16Objects.Count);
            }
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_CustomRelationships_NonGeneric_Test()
        {
            String sql = @"SELECT TOP 1 5 [ParentID],1 [ThisID], * FROM ReadType1
SELECT 5 [ParentID],2 [ThisID],* FROM ReadType2
SELECT 5 [ParentID],3 [ThisID],* FROM ReadType3
SELECT 5 [ParentID],4 [ThisID],* FROM ReadType4
SELECT 1 [ParentID],5 [ThisID],* FROM ReadType5
SELECT 5 [ParentID],6 [ThisID],* FROM ReadType6
SELECT 5 [ParentID],7 [ThisID],* FROM ReadType7
SELECT 5 [ParentID],8 [ThisID],* FROM ReadType8
SELECT 5 [ParentID],9 [ThisID],* FROM ReadType9
SELECT 5 [ParentID],10 [ThisID],* FROM ReadType10
SELECT 5 [ParentID],11 [ThisID],* FROM ReadType11
SELECT 5 [ParentID],12 [ThisID],* FROM ReadType12
SELECT 5 [ParentID],13 [ThisID],* FROM ReadType13
SELECT 5 [ParentID],14 [ThisID],* FROM ReadType14
SELECT 5 [ParentID],15 [ThisID],* FROM ReadType15
SELECT 5 [ParentID],16 [ThisID],* FROM ReadType16
SELECT 5 [ParentID],17 [ThisID],* FROM ReadType17";

            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel1Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 0
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel2Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 1
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel3Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 2
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel14Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 13
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel4Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 3
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel7Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 6
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel6Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 5
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel17Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 16
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel8Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 7
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel11Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 10
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel9Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 8
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel10Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 9
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel13Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 12
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel15Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 14
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel12Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 11
                },
                new DALRelationship
                {
                    ParentColumn = "ThisID",
                    ParentProperty = "CustRel16Objects",
                    ParentTableIndex = 4,
                    ChildColumn = "ParentID",
                    ChildTableIndex = 15
                }
            };

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = sql;

            var types = new Type[]
            {
                typeof(RelatedSetReadCustRel1),
                typeof(RelatedSetReadCustRel2),
                typeof(RelatedSetReadCustRel3),
                typeof(RelatedSetReadCustRel4),
                typeof(RelatedSetReadNonGenericCustRel5),
                typeof(RelatedSetReadCustRel6),
                typeof(RelatedSetReadCustRel7),
                typeof(RelatedSetReadCustRel8),
                typeof(RelatedSetReadCustRel9),
                typeof(RelatedSetReadCustRel10),
                typeof(RelatedSetReadCustRel11),
                typeof(RelatedSetReadCustRel12),
                typeof(RelatedSetReadCustRel13),
                typeof(RelatedSetReadCustRel14),
                typeof(RelatedSetReadCustRel15),
                typeof(RelatedSetReadCustRel16),
                typeof(RelatedSetReadCustRel17)
            };

            var tables = db.ExecuteRelatedSetRead(relationships, types);
            //since each return set has a different number of objects, getting the count is good enough.
            //if the counts match, the correct type was put into the correct list
            //since the child index is what is being switched on, the parent order should not matter.
            //this one test should be good enough to make sure things are going into the correct spots
            var fda = FastDynamicAccess.Get(tables[4][0]);
            foreach (var objs in tables[4])
            {
                Assert.AreEqual<int>(1, fda.GetList(objs, "CustRel1Objects").Count);
                Assert.AreEqual<int>(2, fda.GetList(objs, "CustRel2Objects").Count);
                Assert.AreEqual<int>(3, fda.GetList(objs, "CustRel3Objects").Count);
                Assert.AreEqual<int>(4, fda.GetList(objs, "CustRel4Objects").Count);
                Assert.AreEqual<int>(6, fda.GetList(objs, "CustRel6Objects").Count);
                Assert.AreEqual<int>(7, fda.GetList(objs, "CustRel7Objects").Count);
                Assert.AreEqual<int>(8, fda.GetList(objs, "CustRel8Objects").Count);
                Assert.AreEqual<int>(9, fda.GetList(objs, "CustRel9Objects").Count);
                Assert.AreEqual<int>(10, fda.GetList(objs, "CustRel10Objects").Count);
                Assert.AreEqual<int>(11, fda.GetList(objs, "CustRel11Objects").Count);
                Assert.AreEqual<int>(12, fda.GetList(objs, "CustRel12Objects").Count);
                Assert.AreEqual<int>(13, fda.GetList(objs, "CustRel13Objects").Count);
                Assert.AreEqual<int>(14, fda.GetList(objs, "CustRel14Objects").Count);
                Assert.AreEqual<int>(15, fda.GetList(objs, "CustRel15Objects").Count);
                Assert.AreEqual<int>(16, fda.GetList(objs, "CustRel16Objects").Count);
                Assert.AreEqual<int>(17, fda.GetList(objs, "CustRel17Objects").Count);
            }
        }

        [TestMethod()]
        public void ExecuteRelatedSetRead_ExplicitType_And_RuntimeType_Pass_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            db.QueryString = sql;

            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ChildColumn = "ParentID",
                    ParentColumn = "ID",
                    ParentProperty = "ChildTypes"
                }
            };

            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadTypeChildIsRuntimeType, Object>(relationships);
        }
        #endregion
        #endregion

        #region Async
        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_2GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_3GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 3;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_4GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 4;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_5GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 5;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_6GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 6;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_7GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 7;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_8GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 8;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_9GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 9;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_10GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 10;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_11GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 11;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_12GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 12;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_13GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 13;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_14GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 14;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_15GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 15;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_16GenericParams_Test()
        {
            Boolean done = false;
            DALTuple<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType> relationship = null;

            int size = 16;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync<RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType, RelatedSetReadReadType>(relationships, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertGenericRelatedTableCorrect(relationship.Table1, 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_17_Test()
        {
            Boolean done = false;
            List<List<Object>> relationship = null;

            int size = 17;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            var types = new Type[]
            {
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object),
                typeof(Object)
            };

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync(relationships, types, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertRelatedTableCorrect(relationship[0], 1);
        }

        [TestMethod()]
        public void ExecuteRelatedSetReadAsync_17_KnownTypes_Test()
        {
            Boolean done = false;
            List<List<Object>> relationship = null;

            int size = 17;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = GetRelationships(size);

            var types = new Type[]
            {
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList),
                typeof(ReadTypeObjectList)
            };

            db.QueryString = sql;

            db.ExecuteRelatedSetReadAsync(relationships, types, (r) => { relationship = r; done = true; });
            while (!done) Thread.Sleep(1);

            this.AssertRelatedTableCorrect(relationship[0], 1);
        }
        #endregion

        #region Exceptions
        [TestMethod()]
        [ExpectedException(typeof(DALRelationshipMisconfiguredException))]
        public void ExecuteRelatedSetRead_RelationshipMisconfigured_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ChildTableIndex = 0,
                    ParentTableIndex = 0
                }
            };

            db.QueryString = sql;

            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
        }

        [TestMethod()]
        [ExpectedException(typeof(DALRelationshipMisconfiguredException))]
        public void ExecuteRelatedSetRead_RelationshipMisconfigured_NonGeneric_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ChildTableIndex = 0,
                    ParentTableIndex = 0
                }
            };

            var types = new Type[]
            {
                typeof(RelatedSetReadReadType),
                typeof(RelatedSetReadReadType)
            };

            db.QueryString = sql;

            var relationship = db.ExecuteRelatedSetRead(relationships, types);
        }

        [TestMethod()]
        [ExpectedException(typeof(DALRelationshipParentPropertyMissingException))]
        public void ExecuteRelatedSetRead_ParentChildPropertyMissing_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ChildColumn = "ParentID",
                    ParentColumn = "ID",
                    ParentProperty = "ChildTypes2"
                }
            };

            db.QueryString = sql;

            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, RelatedSetReadReadType>(relationships);
        }

        [TestMethod()]
        [ExpectedException(typeof(DALRelationshipParentPropertyMissingException))]
        public void ExecuteRelatedSetRead_ParentChildPropertyMissing_NonGeneric_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ChildColumn = "ParentID",
                    ParentColumn = "ID",
                    ParentProperty = "ChildTypes2"
                }
            };

            var types = new Type[]
            {
                typeof(RelatedSetReadReadType),
                typeof(RelatedSetReadReadType)
            };

            db.QueryString = sql;

            var relationship = db.ExecuteRelatedSetRead(relationships, types);
        }

        [TestMethod()]
        [ExpectedException(typeof(DALRelationshipParentPropertyNotAListException))]
        public void ExecuteRelatedSetRead_ParentChildPropertyNotAList_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ChildColumn = "ParentID",
                    ParentColumn = "ID",
                    ParentProperty = "ChildTypes"
                }
            };

            db.QueryString = sql;

            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType_NotAList, RelatedSetReadReadType_NotAList>(relationships);
        }

        [TestMethod()]
        [ExpectedException(typeof(DALRelationshipParentPropertyNotAListException))]
        public void ExecuteRelatedSetRead_ParentChildPropertyNotAList_NonGeneric_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ChildColumn = "ParentID",
                    ParentColumn = "ID",
                    ParentProperty = "ChildTypes"
                }
            };

            var types = new Type[]
            {
                typeof(RelatedSetReadReadType_NotAList),
                typeof(RelatedSetReadReadType_NotAList)
            };

            db.QueryString = sql;

            var relationship = db.ExecuteRelatedSetRead(relationships, types);
        }

        [TestMethod()]
        [ExpectedException(typeof(DALRelationshipParentPropertyListIncorrectTypeException))]
        public void ExecuteRelatedSetRead_ParentChildPropertyListWrongType_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ChildColumn = "ParentID",
                    ParentColumn = "ID",
                    ParentProperty = "ChildTypes"
                }
            };

            db.QueryString = sql;

            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadTypeListWrongType, RelatedSetReadReadTypeListWrongType>(relationships);
        }

        [TestMethod()]
        [ExpectedException(typeof(DALRelationshipParentPropertyListIncorrectTypeException))]
        public void ExecuteRelatedSetRead_ParentChildPropertyListWrongType_NonGeneric_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ChildColumn = "ParentID",
                    ParentColumn = "ID",
                    ParentProperty = "ChildTypes"
                }
            };

            var types = new Type[]
            {
                typeof(RelatedSetReadReadTypeListWrongType),
                typeof(RelatedSetReadReadTypeListWrongType)
            };

            db.QueryString = sql;

            var relationship = db.ExecuteRelatedSetRead(relationships, types);
        }

        [TestMethod()]
        [ExpectedException(typeof(DALRelationshipParentPropertyListIncorrectTypeException))]
        public void ExecuteRelatedSetRead_ExplicitType_And_RuntimeType_Fail_Test()
        {
            int size = 2;
            var db = NewDB;
            db.IsStoredProcedure = false;
            var sql = GetSelectStatements(size);
            var relationships = new List<DALRelationship>
            {
                new DALRelationship
                {
                    ChildColumn = "ParentID",
                    ParentColumn = "ID",
                    ParentProperty = "ChildTypes"
                }
            };

            db.QueryString = sql;

            var relationship = db.ExecuteRelatedSetRead<RelatedSetReadReadType, Object>(relationships);
        }
        #endregion
        #endregion

        #region ExecuteScalar
        [TestMethod()]
        public void ExecuteScalar_Raw_NoInput_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarRead;
            var value = db.ExecuteScalar();

            Assert.AreEqual<String>("R1-1", (String)value);
        }

        [TestMethod()]
        public void ExecuteScalar_Generic_NoInput_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarRead;
            var value = db.ExecuteScalar<String>();

            Assert.AreEqual<String>("R1-1", value);
        }

        [TestMethod()]
        public void ExecuteScalar_Raw_Input_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarReadFilteredByID;
            db.Input = new { ID = 3 };
            var value = db.ExecuteScalar();

            Assert.AreEqual<String>("R1-3", (String)value);
        }

        [TestMethod()]
        public void ExecuteScalar_Generic_Input_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarReadFilteredByID;
            db.Input = new { ID = 3 };
            var value = db.ExecuteScalar<String>();

            Assert.AreEqual<String>("R1-3", value);
        }

        [TestMethod()]
        public void CastExecuteScalarToT_Null_Test()
        {
            Object obj = null;
            Assert.AreEqual<Object>(null, obj.CastToT<int?>());
        }
        #endregion

        #region ExecuteScalarAsync
        [TestMethod()]
        public void ExecuteScalar_Async_Raw_NoInput_Test()
        {
            Boolean done = false;
            Object value = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarRead;
            db.ExecuteScalarAsync((o) => { value = o; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<String>("R1-1", (String)value);
        }

        [TestMethod()]
        public void ExecuteScalar_Async_Generic_NoInput_Test()
        {
            Boolean done = false;
            String value = "";

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarRead;
            db.ExecuteScalarAsync<String>((o) => { value = o; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<String>("R1-1", value);
        }

        [TestMethod()]
        public void ExecuteScalar_Async_Raw_Input_Test()
        {
            Boolean done = false;
            Object value = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarReadFilteredByID;
            db.Input = new { ID = 3 };
            db.ExecuteScalarAsync((o) => { value = o; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<String>("R1-3", (String)value);
        }

        [TestMethod()]
        public void ExecuteScalar_Async_Generic_Input_Test()
        {
            Boolean done = false;
            String value = "";

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarReadFilteredByID;
            db.Input = new { ID = 3 };
            db.ExecuteScalarAsync<String>((o) => { value = o; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<String>("R1-3", value);
        }
        #endregion

        #region ExecuteScalarEnumeration
        [TestMethod()]
        public void ExecuteScalarEnumeration_Async_Generic_NoInput_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarRead;
            var values = db.ExecuteScalarEnumeration<String>();

            Assert.AreEqual<int>(5, values.Count);
            for (int i = 0; i < values.Count; i++)
                Assert.AreEqual<String>(String.Format("R1-{0}", i + 1), values[i]);
        }

        [TestMethod()]
        public void ExecuteScalarEnumeration_Async_Generic_Input_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarReadFilteredByID;
            db.Input = new { ID = 3 };
            var values = db.ExecuteScalarEnumeration<String>();

            Assert.AreEqual<int>(1, values.Count);
            Assert.AreEqual<String>("R1-3", values[0]);
        }
        #endregion

        #region ExecuteScalarEnumerationAsync
        [TestMethod()]
        public void ExecuteScalarEnumeration_Generic_NoInput_Test()
        {
            Boolean done = false;
            var values = new List<String>();

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarRead;
            db.ExecuteScalarEnumerationAsync<String>((o) => { values = o; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(5, values.Count);
            for (int i = 0; i < values.Count; i++)
                Assert.AreEqual<String>(String.Format("R1-{0}", i + 1), values[i]);
        }

        [TestMethod()]
        public void ExecuteScalarEnumeration_Generic_Input_Test()
        {
            Boolean done = false;
            var values = new List<String>();

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = BasicScalarReadFilteredByID;
            db.Input = new { ID = 3 };
            db.ExecuteScalarEnumerationAsync<String>((o) => { values = o; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, values.Count);
            Assert.AreEqual<String>("R1-3", values[0]);
        }
        #endregion

        #region ExecuteNonQuery
        [TestMethod()]
        public void ExecuteNonQuery_Raw_NoInput_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = Update10;

            var db2 = NewDB;
            db2.IsStoredProcedure = false;
            db2.QueryString = SelectUpdate10WhereID5;

            int rowsAffected = db.ExecuteNonQuery();
            int count = db2.ExecuteRead().Rows.Count;

            Assert.AreEqual<int>(1, rowsAffected);
            Assert.AreEqual<int>(2, count);
        }

        [TestMethod()]
        public void ExecuteNonQuery_Generic_NoInput_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = Update11;

            var db2 = NewDB;
            db2.IsStoredProcedure = false;
            db2.QueryString = SelectUpdate11WhereID5;

            int rowsAffected = db.ExecuteNonQuery();
            int count = db2.ExecuteRead().Rows.Count;

            Assert.AreEqual<int>(1, rowsAffected);
            Assert.AreEqual<int>(2, count);
        }

        [TestMethod()]
        public void ExecuteNonQuery_Raw_Input_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = Update12;
            db.Input = new { ID = 3 };

            var db2 = NewDB;
            db2.IsStoredProcedure = false;
            db2.QueryString = SelectUpdate12FilteredByID;
            db2.Input = new { ID = 6 };

            int rowsAffected = db.ExecuteNonQuery();
            var row = db2.ExecuteReadSingle<ReadType1Return>();

            Assert.AreEqual<int>(1, rowsAffected);
            Assert.AreEqual<String>("U12-3", row.Name);
        }

        [TestMethod()]
        public void ExecuteNonQuery_Generic_Input_Test()
        {
            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = Update13;
            db.Input = new { ID = 3 };

            var db2 = NewDB;
            db2.IsStoredProcedure = false;
            db2.QueryString = SelectUpdate13FilteredByID;
            db2.Input = new { ID = 6 };

            int rowsAffected = db.ExecuteNonQuery();
            var row = db2.ExecuteReadSingle<ReadType1Return>();

            Assert.AreEqual<int>(1, rowsAffected);
            Assert.AreEqual<String>("U13-3", row.Name);
        }

        [TestMethod()]
        [ExpectedException(typeof(DALException))]
        public void ExecuteNonQuery_AffectedRowsMismatch_Test()
        {
            var db = NewDB;
            db.ExpectedAffectedRows = 20;
            db.IsStoredProcedure = false;
            db.QueryString = Update18;
            db.Input = new { ID = 3 };
            db.OnAffectedRowsMismatch += (s, e) =>
            {
                Assert.AreEqual<int>(1, e.AffectedRows);
                throw new DALException("");
            };

            int rowsAffected = db.ExecuteNonQuery();
        }
        #endregion

        #region ExecuteNonQueryAsync
        [TestMethod()]
        public void ExecuteNonQuery_Async_Raw_NoInput_Test()
        {
            Boolean done = false;
            int rowsAffected = 0;
            int count = 0;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = Update14;

            var db2 = NewDB;
            db2.QueryString = SelectUpdate14WhereID5;
            db2.IsStoredProcedure = false;

            db.ExecuteNonQueryAsync((ra) => { rowsAffected = ra; count = db2.ExecuteRead().Rows.Count; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, rowsAffected);
            Assert.AreEqual<int>(2, count);
        }

        [TestMethod()]
        public void ExecuteNonQuery_Async_Generic_NoInput_Test()
        {
            Boolean done = false;
            int rowsAffected = 0;
            int count = 0;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = Update15;

            var db2 = NewDB;
            db2.IsStoredProcedure = false;
            db2.QueryString = SelectUpdate15WhereID5;
            db.ExecuteNonQueryAsync((ra) => { rowsAffected = ra; count = db2.ExecuteRead().Rows.Count; done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, rowsAffected);
            Assert.AreEqual<int>(2, count);
        }

        [TestMethod()]
        public void ExecuteNonQuery_Async_Raw_Input_Test()
        {
            Boolean done = false;
            int rowsAffected = 0;
            ReadType1Return row = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = Update16;
            db.Input = new { ID = 3 };

            var db2 = NewDB;
            db2.IsStoredProcedure = false;
            db2.QueryString = SelectUpdate16FilteredByID;
            db2.Input = new { ID = 6 };

            db.ExecuteNonQueryAsync((ra) => { rowsAffected = ra; row = db2.ExecuteReadSingle<ReadType1Return>(); done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, rowsAffected);
            Assert.AreEqual<String>("U16-3", row.Name);
        }

        [TestMethod()]
        public void ExecuteNonQuery_Async_Generic_Input_Test()
        {
            Boolean done = false;
            int rowsAffected = 0;
            ReadType1Return row = null;

            var db = NewDB;
            db.IsStoredProcedure = false;
            db.QueryString = Update17;
            db.Input = new { ID = 3 };

            var db2 = NewDB;
            db2.IsStoredProcedure = false;
            db2.QueryString = SelectUpdate17FilteredByID;
            db2.Input = new { ID = 6 };


            db.ExecuteNonQueryAsync((ra) => { rowsAffected = ra; row = db2.ExecuteReadSingle<ReadType1Return>(); done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, rowsAffected);
            Assert.AreEqual<String>("U17-3", row.Name);
        }
        #endregion
        #endregion

        #region ConnectionClosing
        [TestMethod()]
        public void ConnectionsBeingClosed_ExecuteRead_Test()
        {
            Task[] tasks = new Task[4];

            for (int i = 0; i < 4; i++)
            {
                tasks[i] = new Task(() =>
                {
                    var db = NewDB;
                    for (int j = 0; j < 10000; j++)
                    {
                        db.QueryString = BasicRead;
                        db.IsStoredProcedure = false;
                        db.ExecuteRead<ReadType1Return>();
                    }
                });

                tasks[i].Start();
            }

            Task.WaitAll(tasks);

            var db2 = NewDB;
            db2.QueryString = String.Format("sp_who '{0}'", DB_AccessTest.DBUsername);
            db2.IsStoredProcedure = false;
            Assert.IsTrue(db2.ExecuteRead().Rows.Count < 50);
        }

        [TestMethod()]
        public void ConnectionsBeingClosed_ExecuteScalar_Test()
        {
            Task[] tasks = new Task[4];

            for (int i = 0; i < 4; i++)
            {
                tasks[i] = new Task(() =>
                {
                    var db = NewDB;
                    for (int j = 0; j < 10000; j++)
                    {
                        db.IsStoredProcedure = false;
                        db.QueryString = BasicScalarRead;
                        db.ExecuteScalar<String>();
                    }
                });

                tasks[i].Start();
            }

            Task.WaitAll(tasks);
            var db2 = NewDB;
            db2.QueryString = String.Format("sp_who '{0}'", DB_AccessTest.DBUsername);
            db2.IsStoredProcedure = false;
            Assert.IsTrue(db2.ExecuteRead().Rows.Count < 50);
        }

        [TestMethod()]
        public void ConnectionsBeingClosed_ExecuteSetRead_Test()
        {
            Task[] tasks = new Task[4];

            for (int i = 0; i < 4; i++)
            {
                tasks[i] = new Task(() =>
                {
                    var db = NewDB;
                    for (int j = 0; j < 10000; j++)
                    {
                        db.IsStoredProcedure = false;
                        db.QueryString = BasicSetRead;
                        db.ExecuteSetRead();
                    }
                });

                tasks[i].Start();
            }

            Task.WaitAll(tasks);
            var db2 = NewDB;
            db2.QueryString = String.Format("sp_who '{0}'", DB_AccessTest.DBUsername);
            db2.IsStoredProcedure = false;
            Assert.IsTrue(db2.ExecuteRead().Rows.Count < 50);
        }

        [TestMethod()]
        public void ConnectionsBeingClosed_ExecuteNonQuery_Test()
        {
            Task[] tasks = new Task[4];
            for (int i = 0; i < 4; i++)
            {
                tasks[i] = new Task(() =>
                {
                    var db = NewDB;
                    for (int j = 0; j < 10000; j++)
                    {
                        db.IsStoredProcedure = false;
                        db.QueryString = Update19;
                        db.ExecuteNonQuery();
                    }
                });

                tasks[i].Start();
            }

            Task.WaitAll(tasks);
            var db2 = NewDB;
            db2.QueryString = String.Format("sp_who '{0}'", DB_AccessTest.DBUsername);
            db2.IsStoredProcedure = false;
            Assert.IsTrue(db2.ExecuteRead().Rows.Count < 50);
        }
        #endregion

        #region Events
        private String RaiseError =
@"RAISERROR (N'Test Error.', -- Message text.
           10, -- Severity,
           1, -- State,
           N'number', -- First argument.
           5); -- Second argument.";
        private SqlDBAccess Events_OnException_DB;
        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void OnException_Test()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            Events_OnException_DB = new SqlDBAccess();
            Events_OnException_DB.Connection = conn;
            Events_OnException_DB.QueryString = RaiseError;
            Events_OnException_DB.Input = new ReadType1Return();
            Events_OnException_DB.IsStoredProcedure = false;
            Events_OnException_DB.OnException += new System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALExceptionHandler(OnException_Test_Handler);
            try
            {
                var result = Events_OnException_DB.ExecuteScalarEnumeration<int>();
            }
            finally { }
        }

        private void OnException_Test_Handler(DotNETCompatibleProvider<SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataAdapter, SqlException> sender, System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALExceptionEventArgs e)
        {
            Assert.AreEqual<DotNETCompatibleProvider<SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataAdapter, SqlException>>(Events_OnException_DB, sender);
            Assert.AreEqual<IDbConnection>(e.Connection, e.Connection);
            Assert.AreEqual<Type>(typeof(SqlException), e.Exception.GetType());
            Assert.AreEqual<String>("ReadType1Return", e.InputModelType);
            Assert.AreEqual<int>(2, e.InputParameters.ToNameValueDictionary().Count);
            Assert.AreEqual<String>(RaiseError, e.QueryString);
            Assert.AreEqual<Object>(Events_OnException_DB.Input, e.Input);
#if DEBUG
            //only works in debug.  outside of the unit test this will work in release mode.
            Assert.AreEqual<String>("ExecuteScalarEnumeration", e.QueryMethod);
#endif
            Assert.AreEqual<int>(30, e.Timeout);
            Assert.AreEqual<Object>(null, e.Transaction);
        }

        private SqlDBAccess Events_OnException_Static_DB;
        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void OnException_Static_Test()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            Events_OnException_Static_DB = new SqlDBAccess();
            Events_OnException_Static_DB.Connection = conn;
            Events_OnException_Static_DB.QueryString = RaiseError;
            Events_OnException_Static_DB.Input = new ReadType1Return();
            Events_OnException_Static_DB.IsStoredProcedure = false;
            SqlDBAccess.OnExceptionStatic += new System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALExceptionHandler(OnException_Static_Test_Handler);
            Events_OnException_Static_DB.CommandTimeout = 1;
            try
            {
                var result = Events_OnException_Static_DB.ExecuteScalarEnumeration<int>();
            }
            finally
            {
                SqlDBAccess.OnExceptionStatic -= OnException_Static_Test_Handler;
            }
        }

        private void OnException_Static_Test_Handler(DotNETCompatibleProvider<SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataAdapter, SqlException> sender, System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALExceptionEventArgs e)
        {
            Assert.AreEqual<DotNETCompatibleProvider<SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataAdapter, SqlException>>(Events_OnException_Static_DB, sender);
            Assert.AreEqual<IDbConnection>(e.Connection, e.Connection);
            Assert.AreEqual<Type>(typeof(SqlException), e.Exception.GetType());
            Assert.AreEqual<String>("ReadType1Return", e.InputModelType);
            Assert.AreEqual<int>(2, e.InputParameters.ToNameValueDictionary().Count);
            Assert.AreEqual<String>(RaiseError, e.QueryString);
            Assert.AreEqual<Object>(Events_OnException_Static_DB.Input, e.Input);
#if DEBUG
            //only works in debug.  outside of the unit test this will work in release mode.
            Assert.AreEqual<String>("ExecuteScalarEnumeration", e.QueryMethod);
#endif
            Assert.AreEqual<int>(1, e.Timeout);
            Assert.AreEqual<Object>(null, e.Transaction);
        }

        private SqlDBAccess Events_OnSqlException_DB;
        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void OnSqlException_Test()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            Events_OnSqlException_DB = new SqlDBAccess();
            Events_OnSqlException_DB.Connection = conn;
            Events_OnSqlException_DB.OnQueryException += new System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALSqlExceptionHandler(OnSqlException_Test_Handler);
            Events_OnSqlException_DB.CommandTimeout = 1;
            Events_OnSqlException_DB.QueryString = RaiseError;
            Events_OnSqlException_DB.Input = new ReadType1Return();
            Events_OnSqlException_DB.IsStoredProcedure = false;
            try
            {
                var result = Events_OnSqlException_DB.ExecuteScalarEnumeration<int>();
            }
            finally { }
        }

        private void OnSqlException_Test_Handler(SqlDBAccess sender, System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALSqlExceptionEventArgs e)
        {
            Assert.AreEqual<DotNETCompatibleProvider<SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataAdapter, SqlException>>(Events_OnSqlException_DB, sender);
            Assert.AreEqual<IDbConnection>(e.Connection, e.Connection);
            Assert.AreEqual<Type>(typeof(SqlException), e.Exception.GetType());
            Assert.AreEqual<String>("ReadType1Return", e.InputModelType);
            Assert.AreEqual<int>(2, e.InputParameters.ToNameValueDictionary().Count);
            Assert.AreEqual<String>(RaiseError, e.QueryString);
            Assert.AreEqual<Object>(Events_OnSqlException_DB.Input, e.Input);
            //does not work in release mode in unit test for some reason.  confirmed to work outside of unit test in release mode.
#if DEBUG
            Assert.AreEqual<String>("ExecuteScalarEnumeration", e.QueryMethod);
#endif
            Assert.AreEqual<int>(1, e.Timeout);
            Assert.AreEqual<Object>(null, e.Transaction);
        }

        private SqlDBAccess Events_OnSqlException_Static_DB;
        [TestMethod()]
        [ExpectedException(typeof(SqlException))]
        public void OnSqlException_Static_Test()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            Events_OnSqlException_Static_DB = new SqlDBAccess();
            Events_OnSqlException_Static_DB.Connection = conn;
            Events_OnSqlException_Static_DB.QueryString = RaiseError;
            Events_OnSqlException_Static_DB.Input = new ReadType1Return();
            Events_OnSqlException_Static_DB.IsStoredProcedure = false;
            SqlDBAccess.OnQueryExceptionStatic += new System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALSqlExceptionHandler(OnSqlException_Static_Test_Handler);
            Events_OnSqlException_Static_DB.CommandTimeout = 1;
            try
            {
                var result = Events_OnSqlException_Static_DB.ExecuteScalarEnumeration<int>();
            }
            finally
            {
                SqlDBAccess.OnQueryExceptionStatic -= OnSqlException_Static_Test_Handler;
            }
        }

        private void OnSqlException_Static_Test_Handler(SqlDBAccess sender, System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALSqlExceptionEventArgs e)
        {
            Assert.AreEqual<SqlDBAccess>(Events_OnSqlException_Static_DB, sender);
            Assert.AreEqual<IDbConnection>(e.Connection, e.Connection);
            Assert.AreEqual<Type>(typeof(SqlException), e.Exception.GetType());
            Assert.AreEqual<String>("ReadType1Return", e.InputModelType);
            Assert.AreEqual<int>(2, e.InputParameters.ToNameValueDictionary().Count);
            Assert.AreEqual<String>(RaiseError, e.QueryString);
            Assert.AreEqual<Object>(Events_OnSqlException_Static_DB.Input, e.Input);
            //does not work in release mode in unit test for some reason.  confirmed to work outside of unit test in release mode.
#if DEBUG
            Assert.AreEqual<String>("ExecuteScalarEnumeration", e.QueryMethod);
#endif
            Assert.AreEqual<int>(1, e.Timeout);
            Assert.AreEqual<Object>(null, e.Transaction);
        }

        private SqlDBAccess Events_OnAffectedRowsMismatch_DB;
        [TestMethod()]
        public void OnAffectedRowsMismatch_Test()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            Events_OnAffectedRowsMismatch_DB = new SqlDBAccess();
            Events_OnAffectedRowsMismatch_DB.Connection = conn;
            Events_OnAffectedRowsMismatch_DB.OnAffectedRowsMismatch += new System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALAffectedRowsMismatchHandler(OnAffectedRowsMismatchException_Test_Handler);
            Events_OnAffectedRowsMismatch_DB.CommandTimeout = 1;
            Events_OnAffectedRowsMismatch_DB.ExpectedAffectedRows = 7;
            Events_OnAffectedRowsMismatch_DB.QueryString = Update20;
            Events_OnAffectedRowsMismatch_DB.IsStoredProcedure = false;
            try
            {
                var result = Events_OnAffectedRowsMismatch_DB.ExecuteNonQuery();
            }
            finally { }
        }

        private void OnAffectedRowsMismatchException_Test_Handler(DotNETCompatibleProvider<SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataAdapter, SqlException> sender, System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALAffectedRowsMismatchEventArgs e)
        {
            Assert.AreEqual<DotNETCompatibleProvider<SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataAdapter, SqlException>>(Events_OnAffectedRowsMismatch_DB, sender);
            Assert.AreEqual<int>(5, e.AffectedRows);
            Assert.AreEqual<int>(7, e.ExpectedAffectedRows);
            Assert.AreEqual<IDbConnection>(e.Connection, e.Connection);
            Assert.AreEqual<String>(null, e.InputModelType);
            Assert.AreEqual<int>(0, e.InputParameters.ToNameValueDictionary().Count);
            Assert.AreEqual<String>(Update20, e.QueryString);
            Assert.AreEqual<String>("ExecuteNonQuery", e.QueryMethod);
            Assert.AreEqual<int>(1, e.Timeout);
            Assert.AreEqual<Object>(null, e.Transaction);
            Assert.AreEqual<Object>(Events_OnAffectedRowsMismatch_DB.Input, e.Input);
        }

        private SqlDBAccess Events_OnAffectedRowsMismatch_Static_DB;
        [TestMethod()]
        public void OnAffectedRowsMismatch_Static_Test()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            Events_OnAffectedRowsMismatch_Static_DB = new SqlDBAccess();
            Events_OnAffectedRowsMismatch_Static_DB.Connection = conn;
            SqlDBAccess.OnAffectedRowsMismatchStatic += new System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALAffectedRowsMismatchHandler(OnAffectedRowsMismatchException_Static_Test_Handler);
            Events_OnAffectedRowsMismatch_Static_DB.CommandTimeout = 1;
            Events_OnAffectedRowsMismatch_Static_DB.ExpectedAffectedRows = 6;
            Events_OnAffectedRowsMismatch_Static_DB.QueryString = Update21;
            Events_OnAffectedRowsMismatch_Static_DB.IsStoredProcedure = false;
            try
            {
                var result = Events_OnAffectedRowsMismatch_Static_DB.ExecuteNonQuery();
            }
            finally
            {
                SqlDBAccess.OnAffectedRowsMismatchStatic -= OnAffectedRowsMismatchException_Static_Test_Handler;
            }
        }

        private void OnAffectedRowsMismatchException_Static_Test_Handler(DotNETCompatibleProvider<SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataAdapter, SqlException> sender, System.Data.DBAccess.Generic.Providers.SQL.SqlDBAccess.DALAffectedRowsMismatchEventArgs e)
        {
            Assert.AreEqual<DotNETCompatibleProvider<SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataAdapter, SqlException>>(Events_OnAffectedRowsMismatch_Static_DB, sender);
            Assert.AreEqual<int>(5, e.AffectedRows);
            Assert.AreEqual<int>(6, e.ExpectedAffectedRows);
            Assert.AreEqual<IDbConnection>(e.Connection, e.Connection);
            Assert.AreEqual<String>(null, e.InputModelType);
            Assert.AreEqual<int>(0, e.InputParameters.ToNameValueDictionary().Count);
            Assert.AreEqual<String>(Update21, e.QueryString);
            Assert.AreEqual<String>("ExecuteNonQuery", e.QueryMethod);
            Assert.AreEqual<int>(1, e.Timeout);
            Assert.AreEqual<Object>(null, e.Transaction);
            Assert.AreEqual<Object>(Events_OnAffectedRowsMismatch_Static_DB.Input, e.Input);
        }
        #endregion

        #region Output parameters
        public class OutputTestClass
        {
            public int ID { get; set; }
            [DALParameterDirection(Direction = ParameterDirection.Output)]
            public String Name { get; set; }
        }

        private const String OutputQueryString = "stp_QueryReadType1WithOutput";

        [TestMethod()]
        public void ExecuteRead_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteRead();
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteRead_Generic_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteRead<ReadType1Return>();
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteRead_Async_Output_Test()
        {
            var db = NewDB;
            Boolean done = false;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteReadAsync((o) => { done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteRead_Async_Generic_Output_Test()
        {
            var db = NewDB;
            Boolean done = false;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteReadAsync<ReadType1Return>((o) => { done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteReadSingle();
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Generic_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteReadSingle<ReadType1Return>();
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Async_Output_Test()
        {
            var db = NewDB;
            Boolean done = false;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteReadSingleAsync((o) => { done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteReadSingle_Async_Generic_Output_Test()
        {
            var db = NewDB;
            Boolean done = false;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteReadSingleAsync<ReadType1Return>((o) => { done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteSetRead_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteSetRead();
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteSetRead_Generic_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteSetRead<ReadType1Return, ReadType2Return>();
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            Boolean done = false;
            db.ExecuteSetReadAsync((o) => { done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteSetRead_Async_Generic_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            Boolean done = false;
            db.ExecuteSetReadAsync<ReadType1Return, ReadType2Return>((o) => { done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteScalar_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteScalar();
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteScalar_Generic_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteScalar<int>();
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteScalar_Async_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            Boolean done = false;
            db.ExecuteScalarAsync((o) => { done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteScalar_Async_Generic_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            Boolean done = false;
            db.ExecuteScalarAsync<int>((o) => { done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteScalarEnumeration_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteScalarEnumeration<int>();
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteScalarEnumeration_Async_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            Boolean done = false;
            db.ExecuteScalarEnumerationAsync<int>((o) => { done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteNonQuery_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            db.ExecuteNonQuery();
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }

        [TestMethod()]
        public void ExecuteNonQuery_Async_Output_Test()
        {
            var db = NewDB;
            db.QueryString = OutputQueryString;
            db.Input = new OutputTestClass();
            Boolean done = false;
            db.ExecuteNonQueryAsync((o) => { done = true; });

            while (!done) Thread.Sleep(1);
            Assert.AreEqual<int>(1, db.OutputValues.Keys.Count);
            Assert.IsTrue(db.OutputValues.ContainsKey("@oName"));
            Assert.AreEqual<String>("R1-3", (String)db.OutputValues["@oName"]);
        }
        #endregion

        #region UDTable type
        public class UDTableTestModel
        {
            public List<UDTable> IDs { get; set; }
        }

        public class UDTable
        {
            public int ID { get; set; }
        }

        [TestMethod()]
        public void QueryWithUDTableType_Test()
        {
            var model = new UDTableTestModel
            {
                IDs = new List<UDTable>
                {
                    new UDTable { ID = 2 },
                    new UDTable { ID = 3 },
                    new UDTable { ID = 5 },
                }
            };

            var db = NewDB;
            db.QueryString = "stp_QueryReadType1WithTableType";
            db.Input = model;
            var names = db.ExecuteScalarEnumeration<String>();

            Assert.AreEqual<int>(3, names.Count);
            Assert.AreEqual<String>("R1-2", names[0]);
            Assert.AreEqual<String>("R1-3", names[1]);
            Assert.AreEqual<String>("R1-5", names[2]);
        }

        [TestMethod()]
        public void QueryWithUDTable_AnonType_Test()
        {
            var ints = new List<int>
            {
                2,
                3,
                5
            };

            var db = NewDB;
            db.QueryString = "stp_QueryReadType1WithTableType";
            db.Input = new { IDs = ints.Select(i => new { ID = i }) };
            var names = db.ExecuteScalarEnumeration<String>();

            Assert.AreEqual<int>(3, names.Count);
            Assert.AreEqual<String>("R1-2", names[0]);
            Assert.AreEqual<String>("R1-3", names[1]);
            Assert.AreEqual<String>("R1-5", names[2]);
        }

        [TestMethod()]
        public void QueryWithUDTableType_Empty_Test()
        {
            var model = new UDTableTestModel
            {
                IDs = new List<UDTable>()
            };

            var db = NewDB;
            db.QueryString = "stp_QueryReadType1WithTableType";
            db.Input = model;
            var names = db.ExecuteScalarEnumeration<String>();

            Assert.AreEqual<int>(0, names.Count);
        }

        [TestMethod()]
        public void QueryWithUDTable_Empty_AnonType_Test()
        {
            var ints = new List<int>();

            var db = NewDB;
            db.QueryString = "stp_QueryReadType1WithTableType";
            db.Input = new { IDs = ints.Select(i => new { ID = i }) };
            var names = db.ExecuteScalarEnumeration<String>();

            Assert.AreEqual<int>(0, names.Count);
        }
        #endregion

        #region Case Insensitivity
        public class CIReadType1
        {
            public int id { get; set; }
            public CIReadType1Nested nest { get; set; }
            [DALSQLParameterName(Name = "Name2")]
            public String NaME { get; set; }
        }

        public class CIReadType1Nested
        {
            public String NAME { get; set; }
        }

        [TestMethod()]
        public void ExecuteRead_ColumnNames_DifferentCase_Test()
        {
            var db = NewDB;
            db.ValidateForDAL(new CIReadType1());

            db.IsStoredProcedure = false;
            db.QueryString = "SELECT ID,Name FROM ReadType1 WITH (NOLOCK)";
            var results = db.ExecuteRead<CIReadType1>();

            Assert.AreEqual<int>(0, results.Count(r => r.id == 0));
            Assert.AreEqual<int>(5, results.Count(r => r.NaME == null));
            Assert.AreEqual<int>(0, results.Count(r => r.nest.NAME == null));
        }

        public class CICaseInsensitiveConflict
        {
            public int id { get; set; }
            public String Name { get; set; }
            public String NaME { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyInvalidException))]
        public void CaseInsensitive_NameConflict_Test()
        {
            NewDB.ValidateForDAL(new CICaseInsensitiveConflict());
        }

        public class CICaseInsensitiveOneIgnored
        {
            public int id { get; set; }
            public String Name { get; set; }
            [DALIgnore]
            public String NaME { get; set; }
        }

        [TestMethod()]
        public void CaseInsensitive_OneIgnored_Test()
        {
            NewDB.ValidateForDAL(new CICaseInsensitiveOneIgnored());
        }

        public class CICaseInsensitiveOneDifferentSqlName
        {
            public int id { get; set; }
            public String Name { get; set; }
            [DALSQLParameterName(Name="Name2")]
            public String NaME { get; set; }
        }

        [TestMethod()]
        public void CaseInsensitive_OneDifferentSqlName_Test()
        {
            NewDB.ValidateForDAL(new CICaseInsensitiveOneDifferentSqlName());
        }

        public class CICaseInsensitiveConflictFromDALSQLParameterNameAttribute
        {
            [DALSQLParameterName(Name="NAME")]
            public int id { get; set; }
            public String Name { get; set; }
            [DALSQLParameterName(Name = "Name2")]
            public String NaME { get; set; }
        }

        [TestMethod()]
        [ExpectedException(typeof(ModelPropertyInvalidException))]
        public void CaseInsensitive_ConflictFromDALSQLParameterNameAttribute_Test()
        {
            NewDB.ValidateForDAL(new CICaseInsensitiveConflictFromDALSQLParameterNameAttribute());
        }
        #endregion
    }
}