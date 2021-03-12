using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ChocAnDatabase.records;
using System.Collections.Generic;
using System;

namespace ChocAnDatabase.Tests {

    [TestClass]
    public class DatabaseTests {

        private string dbContents;
        private string fileName;
        private Database database;

        [TestInitialize]
        public void Setup() {
            dbContents = "*MEMBERS:\n" +
                "Dylan;000000001;Address;Hillsboro;OR;97124;True\n" +
                "Alix;000000002;Address;Portland;OR;97123;False\n" +
                "*PROVIDERS:\n" +
                "TUALITY;000000001;OTHER ADDRESS;Beaverton;OR;97123\n*SERVICES:\n" +
                "*SERVICES:\n" +
                "AEROBICS;000001;75\n";

            fileName = "./tmp_db.db";

            File.AppendAllText(fileName, dbContents);
            database = new Database(fileName);
        }

        [TestCleanup]
        public void CleanUp() {
            File.Delete(fileName);
        }

        [TestMethod]
        public void TestValidMemberRetrieval() {
            MemberRecord rec = database.FetchMember(000000001);
            Assert.IsNotNull(rec);
            Assert.AreEqual(rec.Validated, true);
        }

        [TestMethod]
        public void TestSuspendedMemberRetrieval() {
            MemberRecord rec = database.FetchMember(000000002);
            Assert.IsNotNull(rec);
            Assert.AreEqual(rec.Validated, false);
        }

        [TestMethod]
        public void TestNonExistantMemberRetrieval() {
            MemberRecord rec = database.FetchMember(000005003);
            Assert.IsNull(rec);
        }

        [TestMethod]
        public void TestProviderRetrieval() {
            ProviderRecord rec = database.FetchProvider(000000001);
            Assert.IsNotNull(rec);
        }

        [TestMethod]
        public void TestNonExistantProviderRetrieval() {
            ProviderRecord rec = database.FetchProvider(000000003);
            Assert.IsNull(rec);
        }

        [TestMethod]
        public void TestNonExistantServiceRetrieval() {
            ServiceRecord rec = database.FetchService(000000003);
            Assert.IsNull(rec);
        }

        [TestMethod]
        public void TestServiceRetrieval() {
            ServiceRecord rec = database.FetchService(000001);
            Assert.IsNotNull(rec);
        }

        [TestMethod]
        public void TestFetchMemberByName() {
            MemberRecord rec = database.FetchMemberByName("Dylan");
            Assert.IsNotNull(rec);
            Assert.AreEqual("Dylan", rec.Name);
        }

        [TestMethod]
        public void TestFetchProviderByName() {
            ProviderRecord rec = database.FetchProviderByName("TUALITY");
            Assert.IsNotNull(rec);
            Assert.AreEqual("TUALITY", rec.Name);
        }

        [TestMethod]
        public void TestInsertAndFetchConsulationRecord() {
            var data = new Dictionary<string, object>();
            DateTime date = DateTime.Now;
            data.Add("record_date", date);
            data.Add("service_date", date);

            data.Add("provider_number", 000000001);
            data.Add("member_number", 000000001);

            data.Add("comments", "None");
            data.Add("service_number", 000000001);

            ConsultationRecord record = new ConsultationRecord(data);

            Assert.IsNotNull(record);
            database.InsertConsultation(record);

            record = database.FetchConsultation(1, 1, 1, date, date);
            Assert.IsNotNull(record);

            Assert.AreEqual(1, record.ServiceNumber);
            Assert.AreEqual(1, record.MemberNumber);
            Assert.AreEqual(1, record.ProviderNumber);

        }

        [TestMethod]
        public void TestDatabaseSave() {
            database.Save();
        }
    }
}
