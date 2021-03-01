using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Provider;
using System;
using ChocAnDatabase.records;
using System.Collections.Generic;
using ReportGenerator.Models;

namespace ReportGenerator.Tests.Provider {
    [TestClass]
    public class ProviderDataValidatorTests {
        private ProviderDataValidator _providerDataValidator;

        [TestInitialize]
        public void Setup() {
            _providerDataValidator = new ProviderDataValidator();
        }

        [TestMethod]
        public void ValidateData_NullReportData() {
            var result = _providerDataValidator.ValidateData(null);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Report data object cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NullProviderRecord() {
            ReportData reportData = new ReportData {
                ProviderRecord = null,
                ProvidedServices = null
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provider record cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NullProvidedServices() {
            ReportData reportData = new ReportData {
               ProviderRecord = new ProviderRecord(new Dictionary<string, object>()),
               ProvidedServices = null
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provided services cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoProvidedServiceProperties() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService()
                }
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Service ID for service date 1/1/0001 12:00:00 AM cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoServiceID() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService()
                }
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Service ID for service date 1/1/0001 12:00:00 AM cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoMemberName() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
                        ServiceId = 7,
                    }
                }
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member name for service date 1/1/0001 12:00:00 AM cannot be null or empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoMemberID()
        {
            ReportData reportData = new ReportData
            {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
                        ServiceId = 7,
                        MemberName = "John Smith"
                    }
                }
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member ID for John Smith cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoFee()
        {
            ReportData reportData = new ReportData
            {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
                        ServiceId = 7,
                        MemberName = "John Smith",
                        MemberId = 1
                    }
                }
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Service fee for service date 1/1/0001 12:00:00 AM cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoProvidedServices() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService>()
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(true, result.valid);
            Assert.AreEqual(string.Empty, result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_1ProvidedService() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
                        ServiceId = 7,
                        MemberName = "John Smith",
                        MemberId = 1,
                        Fee = 999.99
                    }
                }
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(true, result.valid);
            Assert.AreEqual(string.Empty, result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_2ProvidedServices() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
                        ServiceId = 7,
                        MemberName = "John Smith",
                        MemberId = 1,
                        Fee = 999.99
                    },
                    new ProvidedService {
                        ServiceId = 8,
                        MemberName = "John Smith",
                        MemberId = 1,
                        Fee = 200
                    }
                }
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(true, result.valid);
            Assert.AreEqual(string.Empty, result.errorMessage);
        }
    }
}
