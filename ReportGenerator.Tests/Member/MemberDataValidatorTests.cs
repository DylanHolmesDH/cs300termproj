using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Member;
using System;
using ChocAnDatabase.records;
using System.Collections.Generic;
using ReportGenerator.Models;

namespace ReportGenerator.Tests.Member {
    [TestClass]
    public class MemberDataValidatorTests {
        private MemberDataValidator _memberDataValidator;

        [TestInitialize]
        public void Setup() {
            _memberDataValidator = new MemberDataValidator();
        }

        [TestMethod]
        public void ValidateData_NullReportData() {
            var result = _memberDataValidator.ValidateData(null);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Report data object cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NullMemberRecord() {
            ReportData reportData = new ReportData
            {
                MemberRecord = null,
                ProvidedServices = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member record cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NullProvidedServices() {
            ReportData reportData = new ReportData
            {
               MemberRecord = new MemberRecord(new Dictionary<string, object>()),
               ProvidedServices = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provided services cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoProvidedServiceProperties() {
            ReportData reportData = new ReportData
            {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService>
                {
                    new ProvidedService()
                }
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provider name for service date 1/1/0001 12:00:00 AM cannot be null or empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoProviderName() {
            ReportData reportData = new ReportData
            {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService>
                {
                    new ProvidedService
                    {
                        ServiceName = "AB"
                    }
                }
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provider name for service date 1/1/0001 12:00:00 AM cannot be null or empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoServiceName() {
            ReportData reportData = new ReportData
            {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService>
                {
                    new ProvidedService
                    {
                        ProviderName = "John Smith",
                    }
                }
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Service name for service date 1/1/0001 12:00:00 AM cannot be null or empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoProvidedServices() {
            ReportData reportData = new ReportData
            {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService>()
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(true, result.valid);
            Assert.AreEqual(string.Empty, result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_1ProvidedService() {
            ReportData reportData = new ReportData
            {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService>
                {
                    new ProvidedService
                    {
                        ServiceDate = DateTime.Now,
                        ServiceName = "AA",
                        ProviderName = "John Smith"
                    }
                }
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(true, result.valid);
            Assert.AreEqual(string.Empty, result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_2ProvidedServices() {
            ReportData reportData = new ReportData
            {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService>
                {
                    new ProvidedService
                    {
                        ServiceDate = DateTime.Now,
                        ServiceName = "AA",
                        ProviderName = "John Smith"
                    },
                    new ProvidedService
                    {
                        ServiceDate = DateTime.Now,
                        ServiceName = "AB",
                        ProviderName = "John Smith"
                    }
                }
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(true, result.valid);
            Assert.AreEqual(string.Empty, result.errorMessage);
        }
    }
}
