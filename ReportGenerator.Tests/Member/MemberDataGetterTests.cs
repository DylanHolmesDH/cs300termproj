using ChocAnDatabase;
using ChocAnDatabase.records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Interfaces;
using ReportGenerator.Member;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;

namespace ReportGenerator.Tests.Member {
    [TestClass]
    public class MemberDataGetterTests {
        private Mock<IDatabaseWrapper> _databaseMock;
        private MemberDataGetter _memberDataGetter;

        [TestInitialize]
        public void Setup()
        {
            _databaseMock = new Mock<IDatabaseWrapper>();

            _memberDataGetter = new MemberDataGetter(_databaseMock.Object);
        }

        [TestMethod]
        public void GetData_NullMemberRecord()
        {
            var consultationRecords = new List<ConsultationRecord>();

            _databaseMock.Setup(c => c.FetchMember(3)).Returns((MemberRecord)null);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForMember(3)).Returns(consultationRecords);

            var result = _memberDataGetter.GetData(3);

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.IsNull(result.MemberRecord);

            _databaseMock.Verify(c => c.FetchMember(3), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForMember(3), Times.Once);
        }

        [TestMethod]
        public void GetData_EmptyConsultationRecords()
        {
            var consultationRecords = new List<ConsultationRecord>();

            _databaseMock.Setup(c => c.FetchMember(3)).Returns((MemberRecord) null);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForMember(3)).Returns(consultationRecords);

            var result = _memberDataGetter.GetData(3);

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.IsNull(result.MemberRecord);

            _databaseMock.Verify(c => c.FetchMember(3), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForMember(3), Times.Once);
            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_oneConsultationRecord_EmptyMemberRecord()
        {
            var memberRecord = new MemberRecord(new Dictionary<string, object>());

            var consultationRecord = new Dictionary<string, object>();
            consultationRecord.Add("current_date", new DateTime(2021, 2, 1));
            consultationRecord.Add("service_date", new DateTime(2021, 1, 1));
            consultationRecord.Add("member_number", 4);
            consultationRecord.Add("service_number", 5);
            consultationRecord.Add("provider_number", 6);

            var consultationRecords = new List<ConsultationRecord> {
                new ConsultationRecord(consultationRecord)
            };

            var providerRecord = new Dictionary<string, object>();
            providerRecord.Add("name", "John Smith");
            providerRecord.Add("address", "111");
            providerRecord.Add("city", "Blah");
            providerRecord.Add("state", "OR");
            providerRecord.Add("zip", 1111);
            providerRecord.Add("number", 1);

            var serviceRecord = new Dictionary<string, object>();
            serviceRecord.Add("name", "AA");
            serviceRecord.Add("number", 5);
            serviceRecord.Add("fee", 100.00);

            _databaseMock.Setup(c => c.FetchMember(3)).Returns(memberRecord);
            _databaseMock.Setup(c => c.FetchConsultationRecordsForMember(3)).Returns(consultationRecords);
            _databaseMock.Setup(c => c.FetchProvider(6)).Returns(new ProviderRecord(providerRecord));
            _databaseMock.Setup(c => c.FetchServiceRecord(5)).Returns(new ServiceRecord(serviceRecord));

            var result = _memberDataGetter.GetData(3);

            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.AreEqual(memberRecord, result.MemberRecord);

            Assert.AreEqual(1, result.ProvidedServices.Count);

            var providedService = result.ProvidedServices[0];

            Assert.AreEqual(new DateTime(2021, 1, 1), providedService.ServiceDate);
            Assert.AreEqual("John Smith", providedService.ProviderName);
            Assert.AreEqual("AA", providedService.ServiceName);

            _databaseMock.Verify(c => c.FetchMember(3), Times.Once);
            _databaseMock.Verify(c => c.FetchConsultationRecordsForMember(3), Times.Once);
            _databaseMock.Verify(c => c.FetchProvider(6), Times.Once);
            _databaseMock.Verify(c => c.FetchServiceRecord(5), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetData_twoConsultationRecord()
        {
            // Variable setups
            var memberRecord = new MemberRecord(new Dictionary<string, object>())
            {
                Name = "Alex Burbank",
                Address = "1111",
                City = "Blah",
                Number = 7,
                State = "OR",
                Zip = 1111
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
            consultationRecord2.Add("member_number", 4);
            consultationRecord2.Add("service_number", 8);
            consultationRecord2.Add("provider_number", 2);


            var consultationRecords = new List<ConsultationRecord> {
                new ConsultationRecord(consultationRecord1),
                new ConsultationRecord(consultationRecord2)
            };

            var providerRecord1 = new Dictionary<string, object>();
            providerRecord1.Add("name", "John Smith");
            providerRecord1.Add("address", "111");
            providerRecord1.Add("city", "Blah");
            providerRecord1.Add("state", "OR");
            providerRecord1.Add("zip", 1111);
            providerRecord1.Add("number", 6);

            var providerRecord2 = new Dictionary<string, object>();
            providerRecord2.Add("name", "Alex Burbank");
            providerRecord2.Add("address", "111");
            providerRecord2.Add("city", "Blah");
            providerRecord2.Add("state", "OR");
            providerRecord2.Add("zip", 1111);
            providerRecord2.Add("number", 3);

            var serviceRecord1 = new Dictionary<string, object>();
            serviceRecord1.Add("name", "AA");
            serviceRecord1.Add("number", 5);
            serviceRecord1.Add("fee", 100.00);

            var serviceRecord2 = new Dictionary<string, object>();
            serviceRecord2.Add("name", "AB");
            serviceRecord2.Add("number", 5);
            serviceRecord2.Add("fee", 100.00);

            // Mock setups
            _databaseMock.Setup(c => c.FetchMember(3)).Returns(memberRecord);

            _databaseMock.Setup(c => c.FetchConsultationRecordsForMember(3)).Returns(consultationRecords);
           
            _databaseMock.Setup(c => c.FetchProvider(6)).Returns(new ProviderRecord(providerRecord1));
            _databaseMock.Setup(c => c.FetchProvider(2)).Returns(new ProviderRecord(providerRecord2));
            
            _databaseMock.Setup(c => c.FetchServiceRecord(5)).Returns(new ServiceRecord(serviceRecord1));
            _databaseMock.Setup(c => c.FetchServiceRecord(8)).Returns(new ServiceRecord(serviceRecord2));
            
            // Action
            var result = _memberDataGetter.GetData(3);

            // Asserts
            Assert.IsInstanceOfType(result, typeof(ReportData));
            Assert.AreEqual(memberRecord, result.MemberRecord);

            Assert.AreEqual(2, result.ProvidedServices.Count);

            var providedService1 = result.ProvidedServices[0];

            Assert.AreEqual(new DateTime(2021, 1, 1), providedService1.ServiceDate);
            Assert.AreEqual("John Smith", providedService1.ProviderName);
            Assert.AreEqual("AA", providedService1.ServiceName);

            var providedService2 = result.ProvidedServices[1];

            Assert.AreEqual(new DateTime(2021, 1, 1), providedService2.ServiceDate);
            Assert.AreEqual("Alex Burbank", providedService2.ProviderName);
            Assert.AreEqual("AB", providedService2.ServiceName);

            // Mock verification
            _databaseMock.Verify(c => c.FetchMember(3), Times.Once);

            _databaseMock.Verify(c => c.FetchConsultationRecordsForMember(3), Times.Once);

            _databaseMock.Verify(c => c.FetchProvider(6), Times.Once);
            _databaseMock.Verify(c => c.FetchProvider(2), Times.Once);
            
            _databaseMock.Verify(c => c.FetchServiceRecord(5), Times.Once);
            _databaseMock.Verify(c => c.FetchServiceRecord(8), Times.Once);

            _databaseMock.VerifyNoOtherCalls();
        }
    }
}
