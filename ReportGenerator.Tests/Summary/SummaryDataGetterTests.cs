using ChocAnDatabase;
using ChocAnDatabase.records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Models;
using ReportGenerator.Summary;
using System;
using System.Collections.Generic;

namespace ReportGenerator.Tests.Summary
{
    [TestClass]
    public class SummaryDataGetterTests {
        private Mock<IDatabaseWrapper> _databaseMock;
        private SummaryDataGetter _summaryDataGetter;

        [TestInitialize]
        public void Setup() {
            _databaseMock = new Mock<IDatabaseWrapper>();
            _summaryDataGetter = new SummaryDataGetter(_databaseMock.Object);
        }

        [TestMethod]
        public void GetData_NullProviderRecord() {
            _databaseMock.Setup(c => c.FetchProviders()).Returns((List<Record>)null);

            var result = _summaryDataGetter.GetData();

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.IsNull(result.ProviderRecord);

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_1ProviderNullConsultationRecords() {
            Dictionary<string, object> providerRecord = new Dictionary<string, object>();

            providerRecord.Add("name", "John Smith");
            providerRecord.Add("number", 7);
            providerRecord.Add("address", "1111");
            providerRecord.Add("city", "Blah");
            providerRecord.Add("state", "OR");
            providerRecord.Add("zip", 111);

            List<Record> providerRecords = new List<Record> {
                new Record(providerRecord)
            };

            _databaseMock.Setup(c => c.FetchProviders()).Returns(providerRecords);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(7)).Returns((List<ConsultationRecord>)null);

            var result = _summaryDataGetter.GetData();

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.IsNull(result.ProviderRecord);

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(7), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_1Provider0ConsultationRecords()
        {
            Dictionary<string, object> providerRecord = new Dictionary<string, object>();

            providerRecord.Add("name", "John Smith");
            providerRecord.Add("number", 7);
            providerRecord.Add("address", "1111");
            providerRecord.Add("city", "Blah");
            providerRecord.Add("state", "OR");
            providerRecord.Add("zip", 111);

            List<Record> providerRecords = new List<Record> {
                new Record(providerRecord)
            };

            _databaseMock.Setup(c => c.FetchProviders()).Returns(providerRecords);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(7)).Returns(new List<ConsultationRecord>());

            var result = _summaryDataGetter.GetData();

            var summaryRecord = result.SummaryDataInfo[0];

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.IsNull(result.ProviderRecord);

            Assert.AreEqual("John Smith", summaryRecord.ProviderName);
            Assert.AreEqual(0, summaryRecord.TotalNumberOfConsultations);
            Assert.AreEqual(0, summaryRecord.TotalFee);

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(7), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_1Provider1ConsultationRecord() {
            Dictionary<string, object> providerRecord = new Dictionary<string, object>();

            providerRecord.Add("name", "John Smith");
            providerRecord.Add("number", 7);
            providerRecord.Add("address", "1111");
            providerRecord.Add("city", "Blah");
            providerRecord.Add("state", "OR");
            providerRecord.Add("zip", 111);

            List<Record> providerRecords = new List<Record> {
                new Record(providerRecord)
            };

            Dictionary<string, object> consultationRecord = new Dictionary<string, object>();

            consultationRecord.Add("current_date", new DateTime(2021, 1, 2));
            consultationRecord.Add("service_date", new DateTime(2020, 1, 1));
            consultationRecord.Add("member_number", 6);
            consultationRecord.Add("service_number", 5);
            consultationRecord.Add("provider_number", 7);

            var consultationRecords = new List<ConsultationRecord>() {
                new ConsultationRecord(consultationRecord)
            };

            ServiceRecord serviceRecord = new ServiceRecord(new Dictionary<string, object>()) {
                Fee = 999.99,
                Name = "BC",
                Number = 5
            };

            _databaseMock.Setup(c => c.FetchProviders()).Returns(providerRecords);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(7)).Returns(consultationRecords);
            _databaseMock.Setup(c => c.FetchServiceRecord(5)).Returns(serviceRecord);

            var result = _summaryDataGetter.GetData();

            var summaryRecord = result.SummaryDataInfo[0];

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.IsNull(result.ProviderRecord);

            Assert.AreEqual("John Smith", summaryRecord.ProviderName);
            Assert.AreEqual(1, summaryRecord.TotalNumberOfConsultations);
            Assert.AreEqual(999.99, summaryRecord.TotalFee);

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(7), Times.Once);
            _databaseMock.Verify(c => c.FetchServiceRecord(5), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }
    }
}
