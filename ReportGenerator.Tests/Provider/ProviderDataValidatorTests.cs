using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.Provider;
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
        public void ValidateData_reportDataNullReportData() {
            var result = _providerDataValidator.ValidateData(null);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Report data object cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNullProviderRecord() {
            ReportData reportData = new ReportData {
                ProviderRecord = null,
                ProvidedServices = null
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("No provider record", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoProviderName() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = " ",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provider record must contain a name", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoProviderCity() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = " ",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provider record must contain a city", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoProviderState() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = " ",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provider record must contain a state", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoProviderAddress() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = " ",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provider record must contain an address", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataProviderNumber_0() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "111",
                    City = "Blah",
                    Number = 0,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provider ID cannot be 0", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNullProvidedServices() {
            ReportData reportData = new ReportData {
               ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                   Name = "Alex Burbank",
                   Address = "1111",
                   City = "Blah",
                   Number = 7,
                   State = "OR",
                   Zip = 1111
               },
               ProvidedServices = null
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provided services cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoProvidedServices() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = new List<ProvidedService>()
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provided services cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoServiceID() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService() {
                        ServiceId = 0,
                        MemberName = "John Smith",
                        MemberId = 8
                    }
                }
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Service ID for service date 1/1/0001 12:00:00 AM cannot be 0", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoMemberName() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
                        ServiceId = 7,
                        MemberName = " ",
                        MemberId = 8
                    }
                }
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member name for service date 1/1/0001 12:00:00 AM cannot be null or empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoMemberID() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
                        ServiceId = 7,
                        MemberName = "John Smith",
                        MemberId = 0
                    }
                }
            };

            var result = _providerDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member ID for John Smith cannot be 0", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportData1ProvidedService() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
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
        public void ValidateData_reportData2ProvidedServices() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
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
