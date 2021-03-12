using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void ValidateData_reportDataNullReportData() {
            var result = _memberDataValidator.ValidateData(null);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Report data object cannot be null", result.errorMessage);
        }


        [TestMethod]
        public void ValidateData_reportDataNullProviderRecord() {
            ReportData reportData = new ReportData {
                MemberRecord = null,
                ProvidedServices = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("No member record", result.errorMessage);
        }


        [TestMethod]
        public void ValidateData_reportDataNoMemberName() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = " ",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member record must contain a name", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoMemberCity() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = " ",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member record must contain a city", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoMemberState() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = " ",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member record must contain a state", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoMemberAddress() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = " ",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member record must contain an address", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataMemberNumber_0() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "111",
                    City = "Blah",
                    Number = 0,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member ID cannot be 0", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNullProvidedServices() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provided services cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoProvidedServices() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = new List<ProvidedService>()
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provided services cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoProviderName() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
                        ServiceName = "AB"
                    }
                }
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Provider name for service date 1/1/0001 12:00:00 AM cannot be null or empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataNoServiceName() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
                        ProviderName = "John Smith",
                    }
                }
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Service name for service date 1/1/0001 12:00:00 AM cannot be null or empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportData1ProvidedService() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
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
        public void ValidateData_reportData2ProvidedServices() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
                ProvidedServices = new List<ProvidedService> {
                    new ProvidedService {
                        ServiceDate = DateTime.Now,
                        ServiceName = "AA",
                        ProviderName = "John Smith"
                    },
                    new ProvidedService {
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
