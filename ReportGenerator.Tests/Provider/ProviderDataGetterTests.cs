using ChocAnDatabase;
using ChocAnDatabase.records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Models;
using ReportGenerator.Provider;
using System;
using System.Collections.Generic;

namespace ReportGenerator.Tests.Provider {
    [TestClass]
    public class ProviderDataGetterTests {
        private Mock<IDatabaseWrapper> _databaseMock;
        private ProviderDataGetter _providerDataGetter;

        [TestInitialize]
        public void Setup() {
            _databaseMock = new Mock<IDatabaseWrapper>();

            _providerDataGetter = new ProviderDataGetter(_databaseMock.Object);
        }

        [TestMethod]
        public void GetData_nullProviderRecord() {
            var consultationRecords = new List<ConsultationRecord>();

            _databaseMock.Setup(c => c.FetchProvider(3)).Returns((ProviderRecord)null);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(3)).Returns(consultationRecords);

            var result = _providerDataGetter.GetData(3);

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.IsNull(result.ProviderRecord);

            _databaseMock.Verify(c => c.FetchProvider(3), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(3), Times.Once);
        }

        [TestMethod]
        public void GetData_emptyConsultationRecords() {
            var consultationRecords = new List<ConsultationRecord>();

            _databaseMock.Setup(c => c.FetchProvider(3)).Returns((ProviderRecord)null);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(3)).Returns(consultationRecords);

            var result = _providerDataGetter.GetData(3);

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.IsNull(result.ProviderRecord);

            _databaseMock.Verify(c => c.FetchProvider(3), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(3), Times.Once);
            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_oneConsultationRecord_EmptyProviderRecord() {
            var providerRecord = new ProviderRecord(new Dictionary<string, object>());

            var consultationRecord = new Dictionary<string, object>();
            consultationRecord.Add("current_date", new DateTime(2021, 2, 1));
            consultationRecord.Add("service_date", new DateTime(2021, 1, 1));
            consultationRecord.Add("member_number", 4);
            consultationRecord.Add("service_number", 5);
            consultationRecord.Add("provider_number", 6);

            var consultationRecords = new List<ConsultationRecord> {
                new ConsultationRecord(consultationRecord)
            };

            var memberRecord = new Dictionary<string, object>();
            memberRecord.Add("name", "John Smith");
            memberRecord.Add("address", "111");
            memberRecord.Add("city", "Blah");
            memberRecord.Add("state", "OR");
            memberRecord.Add("zip", 1111);
            memberRecord.Add("number", 4);

            var serviceRecord = new Dictionary<string, object>();
            serviceRecord.Add("name", "AA");
            serviceRecord.Add("number", 5);
            serviceRecord.Add("fee", 100.00);

            _databaseMock.Setup(c => c.FetchProvider(1)).Returns(providerRecord);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(1)).Returns(consultationRecords);
            _databaseMock.Setup(c => c.FetchMember(4)).Returns(new MemberRecord(memberRecord));
            _databaseMock.Setup(c => c.FetchServiceRecord(5)).Returns(new ServiceRecord(serviceRecord));

            var result = _providerDataGetter.GetData(1);

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.AreEqual(providerRecord, result.ProviderRecord);

            Assert.AreEqual(1, result.ProvidedServices.Count);

            var providedService = result.ProvidedServices[0];

            Assert.AreEqual(new DateTime(2021, 1, 1), providedService.ServiceDate);
            Assert.AreEqual("John Smith", providedService.MemberName);
            Assert.AreEqual(5, providedService.ServiceId);
            Assert.AreEqual(4, providedService.MemberId);
            Assert.AreEqual(new DateTime(2021, 2, 1), providedService.DateTimeReceived);
            Assert.AreEqual(100, providedService.Fee);

            _databaseMock.Verify(c => c.FetchProvider(1), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(1), Times.Once);
            _databaseMock.Verify(c => c.FetchMember(4), Times.Once);
            _databaseMock.Verify(c => c.FetchServiceRecord(5), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_twoConsultationRecords() {
            var providerRecord = new ProviderRecord(new Dictionary<string, object>()) {
                Name = "Alex Burbank",
                Number = 1,
                City = "Blah",
                State = "OR",
                Address = "1111",
                Zip = 1234,
            };

            var consultationRecord1 = new Dictionary<string, object>();
            consultationRecord1.Add("current_date", new DateTime(2021, 2, 1));
            consultationRecord1.Add("service_date", new DateTime(2021, 1, 1));
            consultationRecord1.Add("member_number", 4);
            consultationRecord1.Add("service_number", 5);
            consultationRecord1.Add("provider_number", 6);

            var consultationRecord2 = new Dictionary<string, object>();
            consultationRecord2.Add("current_date", new DateTime(2021, 2, 1));
            consultationRecord2.Add("service_date", new DateTime(2021, 1, 1));
            consultationRecord2.Add("member_number", 9);
            consultationRecord2.Add("service_number", 11);
            consultationRecord2.Add("provider_number", 6);

            var consultationRecords = new List<ConsultationRecord> {
                new ConsultationRecord(consultationRecord1),
                new ConsultationRecord(consultationRecord2)
            };

            var memberRecord1 = new Dictionary<string, object>();
            memberRecord1.Add("name", "John Smith");
            memberRecord1.Add("address", "111");
            memberRecord1.Add("city", "Blah");
            memberRecord1.Add("state", "OR");
            memberRecord1.Add("zip", 1111);
            memberRecord1.Add("number", 4);

            var memberRecord2 = new Dictionary<string, object>();
            memberRecord2.Add("name", "Alex Burbank");
            memberRecord2.Add("address", "111");
            memberRecord2.Add("city", "Blah");
            memberRecord2.Add("state", "OR");
            memberRecord2.Add("zip", 1111);
            memberRecord2.Add("number", 9);

            var serviceRecord1 = new Dictionary<string, object>();
            serviceRecord1.Add("name", "AA");
            serviceRecord1.Add("number", 5);
            serviceRecord1.Add("fee", 100.00);

            var serviceRecord2 = new Dictionary<string, object>();
            serviceRecord2.Add("name", "BB");
            serviceRecord2.Add("number", 11);
            serviceRecord2.Add("fee", 320.10);

            _databaseMock.Setup(c => c.FetchProvider(1)).Returns(providerRecord);

            _databaseMock.Setup(c => c.FetchConsultationRecordsForProvider(1)).Returns(consultationRecords);

            _databaseMock.Setup(c => c.FetchMember(4)).Returns(new MemberRecord(memberRecord1));
            _databaseMock.Setup(c => c.FetchMember(9)).Returns(new MemberRecord(memberRecord2));

            _databaseMock.Setup(c => c.FetchServiceRecord(5)).Returns(new ServiceRecord(serviceRecord1));
            _databaseMock.Setup(c => c.FetchServiceRecord(11)).Returns(new ServiceRecord(serviceRecord2));

            var result = _providerDataGetter.GetData(1);

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.AreEqual(providerRecord, result.ProviderRecord);
            Assert.AreEqual(providerRecord.Name, result.ProviderRecord.Name);
            Assert.AreEqual(providerRecord.Number, result.ProviderRecord.Number);
            Assert.AreEqual(providerRecord.City, result.ProviderRecord.City);
            Assert.AreEqual(providerRecord.State, result.ProviderRecord.State);
            Assert.AreEqual(providerRecord.Zip, result.ProviderRecord.Zip);

            Assert.AreEqual(2, result.ProvidedServices.Count);

            var providedService1 = result.ProvidedServices[0];
            var providedService2 = result.ProvidedServices[1];

            Assert.AreEqual(new DateTime(2021, 1, 1), providedService1.ServiceDate);
            Assert.AreEqual("John Smith", providedService1.MemberName);
            Assert.AreEqual(5, providedService1.ServiceId);
            Assert.AreEqual(4, providedService1.MemberId);
            Assert.AreEqual(new DateTime(2021, 2, 1), providedService1.DateTimeReceived);
            Assert.AreEqual(100, providedService1.Fee);

            Assert.AreEqual(new DateTime(2021, 1, 1), providedService2.ServiceDate);
            Assert.AreEqual("Alex Burbank", providedService2.MemberName);
            Assert.AreEqual(11, providedService2.ServiceId);
            Assert.AreEqual(9, providedService2.MemberId);
            Assert.AreEqual(new DateTime(2021, 2, 1), providedService2.DateTimeReceived);
            Assert.AreEqual(320.10, providedService2.Fee);

            _databaseMock.Verify(c => c.FetchProvider(1), Times.Once);

            _databaseMock.Verify(c => c.FetchConsultationRecordsForProvider(1), Times.Once);

            _databaseMock.Verify(c => c.FetchMember(4), Times.Once);
            _databaseMock.Verify(c => c.FetchMember(9), Times.Once);

            _databaseMock.Verify(c => c.FetchServiceRecord(5), Times.Once);
            _databaseMock.Verify(c => c.FetchServiceRecord(11), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }
    }
}
