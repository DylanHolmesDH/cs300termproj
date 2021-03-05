using ChocAnDatabase;
using ChocAnDatabase.records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Models;
using ReportGenerator.Summary;
using System;
using System.Collections.Generic;

namespace ReportGenerator.Tests.Summary {
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

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_NoProviderRecords() {
            _databaseMock.Setup(c => c.FetchProviders()).Returns(new List<Record>());

            var result = _summaryDataGetter.GetData();

            Assert.IsInstanceOfType(result, typeof(ReportData));

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

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(7), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_1ProviderNoConsultationRecords() {
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
            Assert.AreEqual(1, result.SummaryDataInfo.Count);

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
            Assert.AreEqual(1, result.SummaryDataInfo.Count);

            Assert.AreEqual("John Smith", summaryRecord.ProviderName);
            Assert.AreEqual(1, summaryRecord.TotalNumberOfConsultations);
            Assert.AreEqual(999.99, summaryRecord.TotalFee);

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(7), Times.Once);
            _databaseMock.Verify(c => c.FetchServiceRecord(5), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_1Provider2ConsultationRecordsForEach() {
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

            Dictionary<string, object> consultationRecord1ForProvider = new Dictionary<string, object>();

            consultationRecord1ForProvider.Add("current_date", new DateTime(2021, 1, 2));
            consultationRecord1ForProvider.Add("service_date", new DateTime(2020, 1, 1));
            consultationRecord1ForProvider.Add("member_number", 6);
            consultationRecord1ForProvider.Add("service_number", 5);
            consultationRecord1ForProvider.Add("provider_number", 7);

            Dictionary<string, object> consultationRecord2ForProvider = new Dictionary<string, object>();

            consultationRecord2ForProvider.Add("current_date", new DateTime(2021, 1, 2));
            consultationRecord2ForProvider.Add("service_date", new DateTime(2020, 1, 1));
            consultationRecord2ForProvider.Add("member_number", 10);
            consultationRecord2ForProvider.Add("service_number", 5);
            consultationRecord2ForProvider.Add("provider_number", 7);

            var consultationRecordsForProvider1 = new List<ConsultationRecord>() {
                new ConsultationRecord(consultationRecord1ForProvider),
                new ConsultationRecord(consultationRecord2ForProvider)
            };

            ServiceRecord serviceRecord = new ServiceRecord(new Dictionary<string, object>()) {
                Fee = 100,
                Name = "BC",
                Number = 5
            };

            _databaseMock.Setup(c => c.FetchProviders()).Returns(providerRecords);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(7)).Returns(consultationRecordsForProvider1);
            _databaseMock.Setup(c => c.FetchServiceRecord(5)).Returns(serviceRecord);

            var result = _summaryDataGetter.GetData();

            var summaryRecord1 = result.SummaryDataInfo[0];

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.AreEqual(1, result.SummaryDataInfo.Count);

            Assert.AreEqual("John Smith", summaryRecord1.ProviderName);
            Assert.AreEqual(2, summaryRecord1.TotalNumberOfConsultations);
            Assert.AreEqual(200.00, summaryRecord1.TotalFee);

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(7), Times.Once);
            _databaseMock.Verify(c => c.FetchServiceRecord(5), Times.Exactly(2));

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_2ProvidersNoConsultationRecordsForEach() {
            Dictionary<string, object> providerRecord1 = new Dictionary<string, object>();
            providerRecord1.Add("name", "John Smith");
            providerRecord1.Add("number", 7);
            providerRecord1.Add("address", "1111");
            providerRecord1.Add("city", "Blah");
            providerRecord1.Add("state", "OR");
            providerRecord1.Add("zip", 111);

            Dictionary<string, object> providerRecord2 = new Dictionary<string, object>();
            providerRecord2.Add("name", "John Smith");
            providerRecord2.Add("number", 7);
            providerRecord2.Add("address", "1111");
            providerRecord2.Add("city", "Blah");
            providerRecord2.Add("state", "OR");
            providerRecord2.Add("zip", 111);

            List<Record> providerRecords = new List<Record> {
                new Record(providerRecord1),
                new Record(providerRecord2)
            };

            _databaseMock.Setup(c => c.FetchProviders()).Returns(providerRecords);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(7)).Returns(new List<ConsultationRecord>());

            var result = _summaryDataGetter.GetData();

            var summaryRecord = result.SummaryDataInfo[0];
            var summaryRecord2 = result.SummaryDataInfo[1];

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.AreEqual(2, result.SummaryDataInfo.Count);

            Assert.AreEqual("John Smith", summaryRecord.ProviderName);
            Assert.AreEqual(0, summaryRecord.TotalNumberOfConsultations);
            Assert.AreEqual(0, summaryRecord.TotalFee);

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(7), Times.Exactly(2));

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_2Providers1ConsultationRecordForEach() {
            Dictionary<string, object> providerRecord1 = new Dictionary<string, object>();
            providerRecord1.Add("name", "John Smith");
            providerRecord1.Add("number", 7);
            providerRecord1.Add("address", "1111");
            providerRecord1.Add("city", "Blah");
            providerRecord1.Add("state", "OR");
            providerRecord1.Add("zip", 111);

            Dictionary<string, object> providerRecord2 = new Dictionary<string, object>();
            providerRecord2.Add("name", "Alex Burbank");
            providerRecord2.Add("number", 12);
            providerRecord2.Add("address", "111");
            providerRecord2.Add("city", "Blah");
            providerRecord2.Add("state", "OR");
            providerRecord2.Add("zip", 111);

            List<Record> providerRecords = new List<Record> {
                new Record(providerRecord1),
                new Record(providerRecord2)
            };

            Dictionary<string, object> consultationRecordForProvider1 = new Dictionary<string, object>();

            consultationRecordForProvider1.Add("current_date", new DateTime(2021, 1, 2));
            consultationRecordForProvider1.Add("service_date", new DateTime(2020, 1, 1));
            consultationRecordForProvider1.Add("member_number", 6);
            consultationRecordForProvider1.Add("service_number", 5);
            consultationRecordForProvider1.Add("provider_number", 7);

            var consultationRecordsForProvider1 = new List<ConsultationRecord>() {
                new ConsultationRecord(consultationRecordForProvider1),
            };

            Dictionary<string, object> consultationRecordForProvider2 = new Dictionary<string, object>();

            consultationRecordForProvider2.Add("current_date", new DateTime(2021, 1, 2));
            consultationRecordForProvider2.Add("service_date", new DateTime(2020, 1, 1));
            consultationRecordForProvider2.Add("member_number", 6);
            consultationRecordForProvider2.Add("service_number", 5);
            consultationRecordForProvider2.Add("provider_number", 12);

            var consultationRecordsForProvider2 = new List<ConsultationRecord>() {
                new ConsultationRecord(consultationRecordForProvider2),
            };

            ServiceRecord serviceRecord = new ServiceRecord(new Dictionary<string, object>()) {
                Fee = 100,
                Name = "BC",
                Number = 5
            };

            _databaseMock.Setup(c => c.FetchProviders()).Returns(providerRecords);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(7)).Returns(consultationRecordsForProvider1);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(12)).Returns(consultationRecordsForProvider2);
            _databaseMock.Setup(c => c.FetchServiceRecord(5)).Returns(serviceRecord);

            var result = _summaryDataGetter.GetData();

            var summaryRecord1 = result.SummaryDataInfo[0];
            var summaryRecord2 = result.SummaryDataInfo[1];

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.AreEqual(2, result.SummaryDataInfo.Count);

            Assert.AreEqual("John Smith", summaryRecord1.ProviderName);
            Assert.AreEqual(1, summaryRecord1.TotalNumberOfConsultations);
            Assert.AreEqual(100.00, summaryRecord1.TotalFee);

            Assert.AreEqual("Alex Burbank", summaryRecord2.ProviderName);
            Assert.AreEqual(1, summaryRecord2.TotalNumberOfConsultations);
            Assert.AreEqual(100.00, summaryRecord2.TotalFee);

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(7), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(12), Times.Once);
            _databaseMock.Verify(c => c.FetchServiceRecord(5), Times.Exactly(2));

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_2Providers2ConsultationRecordsForEach() {
            Dictionary<string, object> providerRecord1 = new Dictionary<string, object>();
            providerRecord1.Add("name", "John Smith");
            providerRecord1.Add("number", 7);
            providerRecord1.Add("address", "1111");
            providerRecord1.Add("city", "Blah");
            providerRecord1.Add("state", "OR");
            providerRecord1.Add("zip", 111);

            Dictionary<string, object> providerRecord2 = new Dictionary<string, object>();
            providerRecord2.Add("name", "Alex Burbank");
            providerRecord2.Add("number", 15);
            providerRecord2.Add("address", "1111");
            providerRecord2.Add("city", "Blah");
            providerRecord2.Add("state", "OR");
            providerRecord2.Add("zip", 111);

            List<Record> providerRecords = new List<Record> {
                new Record(providerRecord1),
                new Record(providerRecord2)
            };

            Dictionary<string, object> consultationRecord1ForProvider1 = new Dictionary<string, object>();

            consultationRecord1ForProvider1.Add("current_date", new DateTime(2021, 1, 2));
            consultationRecord1ForProvider1.Add("service_date", new DateTime(2020, 1, 1));
            consultationRecord1ForProvider1.Add("member_number", 6);
            consultationRecord1ForProvider1.Add("service_number", 5);
            consultationRecord1ForProvider1.Add("provider_number", 7);

            Dictionary<string, object> consultationRecord2ForProvider1 = new Dictionary<string, object>();

            consultationRecord2ForProvider1.Add("current_date", new DateTime(2021, 1, 2));
            consultationRecord2ForProvider1.Add("service_date", new DateTime(2020, 1, 1));
            consultationRecord2ForProvider1.Add("member_number", 10);
            consultationRecord2ForProvider1.Add("service_number", 5);
            consultationRecord2ForProvider1.Add("provider_number", 7);

            var consultationRecordsForProvider1 = new List<ConsultationRecord>() {
                new ConsultationRecord(consultationRecord1ForProvider1),
                new ConsultationRecord(consultationRecord2ForProvider1)
            };

            Dictionary<string, object> consultationRecord1ForProvider2 = new Dictionary<string, object>();

            consultationRecord1ForProvider2.Add("current_date", new DateTime(2021, 1, 2));
            consultationRecord1ForProvider2.Add("service_date", new DateTime(2020, 1, 1));
            consultationRecord1ForProvider2.Add("member_number", 6);
            consultationRecord1ForProvider2.Add("service_number", 5);
            consultationRecord1ForProvider2.Add("provider_number", 15);

            Dictionary<string, object> consultationRecord2ForProvider2 = new Dictionary<string, object>();

            consultationRecord2ForProvider2.Add("current_date", new DateTime(2021, 1, 2));
            consultationRecord2ForProvider2.Add("service_date", new DateTime(2020, 1, 1));
            consultationRecord2ForProvider2.Add("member_number", 10);
            consultationRecord2ForProvider2.Add("service_number", 5);
            consultationRecord2ForProvider2.Add("provider_number", 15);

            var consultationRecordsForProvider2 = new List<ConsultationRecord>() {
                new ConsultationRecord(consultationRecord1ForProvider2),
                new ConsultationRecord(consultationRecord2ForProvider2)
            };

            ServiceRecord serviceRecord = new ServiceRecord(new Dictionary<string, object>()) {
                Fee = 100,
                Name = "BC",
                Number = 5
            };

            _databaseMock.Setup(c => c.FetchProviders()).Returns(providerRecords);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(7)).Returns(consultationRecordsForProvider1);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(15)).Returns(consultationRecordsForProvider2);
            _databaseMock.Setup(c => c.FetchServiceRecord(5)).Returns(serviceRecord);

            var result = _summaryDataGetter.GetData();

            var summaryRecord1 = result.SummaryDataInfo[0];
            var summaryRecord2 = result.SummaryDataInfo[1];

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.AreEqual(2, result.SummaryDataInfo.Count);

            Assert.AreEqual("John Smith", summaryRecord1.ProviderName);
            Assert.AreEqual(2, summaryRecord1.TotalNumberOfConsultations);
            Assert.AreEqual(200.00, summaryRecord1.TotalFee);

            Assert.AreEqual("Alex Burbank", summaryRecord2.ProviderName);
            Assert.AreEqual(2, summaryRecord2.TotalNumberOfConsultations);
            Assert.AreEqual(200.00, summaryRecord2.TotalFee);

            _databaseMock.Verify(c => c.FetchProviders(), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(7), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(15), Times.Once);
            _databaseMock.Verify(c => c.FetchServiceRecord(5), Times.Exactly(4));

            _databaseMock.VerifyNoOtherCalls();
        }
    }
}
