using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.Member;
using System;
using ChocAnDatabase.records;
using System.Collections.Generic;
using ReportGenerator.Models;

namespace ReportGenerator.Tests.Member {
    [TestClass]
    public class MemberDataFormatterTests {
        private MemberDataFormatter _memberDataFormatter;

        [TestInitialize]
        public void Setup() {
            _memberDataFormatter = new MemberDataFormatter();
        }

        [TestMethod]
        public void FormatData_NullArg() {
            var ex = Assert.ThrowsException<ApplicationException>(() => _memberDataFormatter.FormatData(null));
            Assert.AreEqual("Report data cannot be null", ex.Message);
        }

        [TestMethod]
        public void FormatData_NoServicesProvided() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                }
            };

            var result = _memberDataFormatter.FormatData(reportData);
            Assert.AreEqual(10, result.OutputLines.Count);

            Assert.AreEqual("Alex_Burbank " + DateTime.Now.ToString("MM-dd-yyyy") + ".txt", result.FileName);

            Assert.AreEqual(reportData.MemberRecord.Name, result.OutputLines[0]);
            Assert.AreEqual(reportData.MemberRecord.Address, result.OutputLines[1]);
            Assert.AreEqual(reportData.MemberRecord.City + ", " + reportData.MemberRecord.State + " " +
               reportData.MemberRecord.Zip, result.OutputLines[2]);

            Assert.AreEqual("", result.OutputLines[3]);
            Assert.AreEqual("", result.OutputLines[4]);

            Assert.AreEqual("ID: 000000007", result.OutputLines[5]);

            Assert.AreEqual("", result.OutputLines[6]);
            Assert.AreEqual("", result.OutputLines[7]);

            Assert.AreEqual("Service date   " + "Provider name                 "
                + "Service name             ", result.OutputLines[8]);
            Assert.AreEqual("______________________________________________________________________"
                , result.OutputLines[9]);
        }

        [TestMethod]
        public void FormatData_valid1ServiceProvided() {
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
                        ServiceDate = new DateTime(1,1, 1),
                        ServiceName = "AA"
                    }
                }
            };

            var result = _memberDataFormatter.FormatData(reportData);
            Assert.AreEqual(11, result.OutputLines.Count);

            Assert.AreEqual("Alex_Burbank " + DateTime.Now.ToString("MM-dd-yyyy") + ".txt", result.FileName);

            Assert.AreEqual(reportData.MemberRecord.Name, result.OutputLines[0]);
            Assert.AreEqual(reportData.MemberRecord.Address, result.OutputLines[1]);
            Assert.AreEqual(reportData.MemberRecord.City + ", " + reportData.MemberRecord.State + " " +
               reportData.MemberRecord.Zip, result.OutputLines[2]);

            Assert.AreEqual("", result.OutputLines[3]);
            Assert.AreEqual("", result.OutputLines[4]);

            Assert.AreEqual("ID: 000000007", result.OutputLines[5]);

            Assert.AreEqual("", result.OutputLines[6]);
            Assert.AreEqual("", result.OutputLines[7]);

            Assert.AreEqual("Service date   " + "Provider name                 "
                + "Service name             ", result.OutputLines[8]);
            Assert.AreEqual("______________________________________________________________________"
                , result.OutputLines[9]);
            Assert.AreEqual("01-01-0001     " + "John Smith                    "
                + "AA                       ", result.OutputLines[10]);
        }

        [TestMethod]
        public void FormatData_valid2ServicesProvided() {
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
                        ServiceDate = new DateTime(2001, 2, 28),
                        ServiceName = "AA"
                    },
                    new ProvidedService {
                        ProviderName = "John Smith",
                        ServiceDate = new DateTime(2001, 2, 26),
                        ServiceName = "BB"
                    }
                }
            };

            var result = _memberDataFormatter.FormatData(reportData);
            Assert.AreEqual(12, result.OutputLines.Count);

            Assert.AreEqual("Alex_Burbank " + DateTime.Now.ToString("MM-dd-yyyy") + ".txt", result.FileName);

            Assert.AreEqual(reportData.MemberRecord.Name, result.OutputLines[0]);
            Assert.AreEqual(reportData.MemberRecord.Address, result.OutputLines[1]);
            Assert.AreEqual(reportData.MemberRecord.City + ", " + reportData.MemberRecord.State + " " +
               reportData.MemberRecord.Zip, result.OutputLines[2]);

            Assert.AreEqual("", result.OutputLines[3]);
            Assert.AreEqual("", result.OutputLines[4]);

            Assert.AreEqual("ID: 000000007", result.OutputLines[5]);

            Assert.AreEqual("", result.OutputLines[6]);
            Assert.AreEqual("", result.OutputLines[7]);

            Assert.AreEqual("Service date   " + "Provider name                 "
                + "Service name             ", result.OutputLines[8]);
            Assert.AreEqual("______________________________________________________________________"
                , result.OutputLines[9]);
            Assert.AreEqual("02-26-2001     " + "John Smith                    "
                + "BB                       ", result.OutputLines[10]);
            Assert.AreEqual("02-28-2001     " + "John Smith                    "
                + "AA                       ", result.OutputLines[11]);
        }
    }
}
