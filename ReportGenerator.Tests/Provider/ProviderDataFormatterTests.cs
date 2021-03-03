using ChocAnDatabase.records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.Models;
using ReportGenerator.Provider;
using System;
using System.Collections.Generic;

namespace ReportGenerator.Tests.Provider {
    [TestClass]
    public class ProviderDataFormatterTests {
        private ProviderDataFormatter _providerDataFormatter;

        [TestInitialize]
        public void Setup() {
            _providerDataFormatter = new ProviderDataFormatter();
        }

        [TestMethod]
        public void ReportDataNull() {
            var ex = Assert.ThrowsException<ApplicationException>(() => _providerDataFormatter.FormatData(null));
            Assert.AreEqual("Report data cannot be null", ex.Message);
        }

        [TestMethod]
        public void ReportData1ProvidedService() {
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
                        Fee = 999.99,
                        DateTimeReceived = new DateTime(1, 1, 1, 13, 0, 0),
                        ServiceDate = new DateTime(1, 1, 1),
                        ServiceId = 1,
                        MemberId = 7,
                        MemberName = "John Smith"
                    }
                }
            };

            var result = _providerDataFormatter.FormatData(reportData);

            Assert.AreEqual("Alex Burbank", result.OutputLines[0]);
            Assert.AreEqual("1111", result.OutputLines[1]);
            Assert.AreEqual("Blah OR 1111", result.OutputLines[2]);

            Assert.AreEqual("\n", result.OutputLines[3]);
            Assert.AreEqual("\n", result.OutputLines[4]);
            Assert.AreEqual("\n", result.OutputLines[5]);

            Assert.AreEqual("7", result.OutputLines[6]);

            Assert.AreEqual("\n", result.OutputLines[7]);

            Assert.AreEqual(
                "Service date".PadRight(15)
                + "Date record received".PadRight(24)
                + "Member name".PadRight(30)
                + "Member ID".PadRight(14)
                + "Service ID".PadRight(11)
                + "Fee".PadRight(12), result.OutputLines[8]
                );

            Assert.AreEqual("_".PadRight(106, '_'), result.OutputLines[9]);

            Assert.AreEqual(
                "01-01-0001".PadRight(15) 
                + "01-01-0001 13:00:00".PadRight(24)
                + "John Smith".PadRight(30)
                + "7".PadRight(14)
                + "1".PadRight(11)
                + "$999.99".PadRight(12), result.OutputLines[10]
                );
        }

        [TestMethod]
        public void ReportData2ProvidedService() {
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
                        Fee = 999.99,
                        DateTimeReceived = new DateTime(1, 1, 1, 13, 0, 0),
                        ServiceDate = new DateTime(1, 1, 1),
                        ServiceId = 1,
                        MemberId = 7,
                        MemberName = "John Smith"
                    },
                    new ProvidedService {
                        Fee = 999.99,
                        DateTimeReceived = new DateTime(1, 1, 1, 13, 0, 0),
                        ServiceDate = new DateTime(1, 1, 1),
                        ServiceId = 1,
                        MemberId = 7,
                        MemberName = "John Smith"
                    }
                }
            };

            var result = _providerDataFormatter.FormatData(reportData);

            Assert.AreEqual("Alex Burbank", result.OutputLines[0]);
            Assert.AreEqual("1111", result.OutputLines[1]);
            Assert.AreEqual("Blah OR 1111", result.OutputLines[2]);

            Assert.AreEqual("\n", result.OutputLines[3]);
            Assert.AreEqual("\n", result.OutputLines[4]);
            Assert.AreEqual("\n", result.OutputLines[5]);

            Assert.AreEqual("7", result.OutputLines[6]);

            Assert.AreEqual("\n", result.OutputLines[7]);

            Assert.AreEqual(
                "Service date".PadRight(15)
                + "Date record received".PadRight(24)
                + "Member name".PadRight(30)
                + "Member ID".PadRight(14)
                + "Service ID".PadRight(11)
                + "Fee".PadRight(12), result.OutputLines[8]
                );

            Assert.AreEqual("_".PadRight(106, '_'), result.OutputLines[9]);

            Assert.AreEqual(
                "01-01-0001".PadRight(15)
                + "01-01-0001 13:00:00".PadRight(24)
                + "John Smith".PadRight(30)
                + "7".PadRight(14)
                + "1".PadRight(11)
                + "$999.99".PadRight(12), result.OutputLines[10]
                );

            Assert.AreEqual(
                "01-01-0001".PadRight(15)
                + "01-01-0001 13:00:00".PadRight(24)
                + "John Smith".PadRight(30)
                + "7".PadRight(14)
                + "1".PadRight(11)
                + "$999.99".PadRight(12), result.OutputLines[11]
                );
        }

    }
}
