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
        public void FormatData_reportDataNull() {
            var ex = Assert.ThrowsException<ApplicationException>(() => _providerDataFormatter.FormatData(null));
            Assert.AreEqual("Report data cannot be null", ex.Message);
        }

        [TestMethod]
        public void FormatData_reportDataNoServicesProvided() {
            ReportData reportData = new ReportData {
                ProviderRecord = new ProviderRecord(new Dictionary<string, object>()) {
                    Name = "Alex Burbank",
                    Address = "1111",
                    City = "Blah",
                    Number = 7,
                    State = "OR",
                    Zip = 1111
                },
            };

            var result = _providerDataFormatter.FormatData(reportData);
            Assert.AreEqual(14, result.OutputLines.Count);

            Assert.AreEqual("Alex_Burbank " + DateTime.Now.ToString("MM-dd-yyyy") + ".txt", result.FileName);

            Assert.AreEqual("Alex Burbank", result.OutputLines[0]);
            Assert.AreEqual("1111", result.OutputLines[1]);
            Assert.AreEqual("Blah, OR 1111", result.OutputLines[2]);

            Assert.AreEqual("", result.OutputLines[3]);
            Assert.AreEqual("", result.OutputLines[4]);

            Assert.AreEqual("ID: 000000007", result.OutputLines[5]);

            Assert.AreEqual("", result.OutputLines[6]);
            Assert.AreEqual("", result.OutputLines[7]);

            Assert.AreEqual(
                "Service date".PadRight(15)
                + "Date record received".PadRight(24)
                + "Member name".PadRight(30)
                + "Member ID".PadRight(14)
                + "Service ID".PadRight(11)
                + "Fee".PadRight(12), result.OutputLines[8]
                );

            Assert.AreEqual("_".PadRight(106, '_'), result.OutputLines[9]);

            Assert.AreEqual("", result.OutputLines[10]);
            Assert.AreEqual("", result.OutputLines[11]);

            Assert.AreEqual("Total consultations: 0", result.OutputLines[12]);
            Assert.AreEqual("Total fee: $0.00", result.OutputLines[13]);
        }


        [TestMethod]
        public void FormatData_reportData1ServiceProvided() {
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
            Assert.AreEqual(15, result.OutputLines.Count);

            Assert.AreEqual("Alex_Burbank " + DateTime.Now.ToString("MM-dd-yyyy") + ".txt", result.FileName);

            Assert.AreEqual("Alex Burbank", result.OutputLines[0]);
            Assert.AreEqual("1111", result.OutputLines[1]);
            Assert.AreEqual("Blah, OR 1111", result.OutputLines[2]);

            Assert.AreEqual("", result.OutputLines[3]);
            Assert.AreEqual("", result.OutputLines[4]);

            Assert.AreEqual("ID: 000000007", result.OutputLines[5]);

            Assert.AreEqual("", result.OutputLines[6]);
            Assert.AreEqual("", result.OutputLines[7]);

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

            Assert.AreEqual("", result.OutputLines[11]);
            Assert.AreEqual("", result.OutputLines[12]);

            Assert.AreEqual("Total consultations: 1", result.OutputLines[13]);
            Assert.AreEqual("Total fee: $999.99", result.OutputLines[14]);
        }

        [TestMethod]
        public void FormatData_reportData2ServicesProvided() {
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
                        DateTimeReceived = new DateTime(2004, 1, 14, 13, 00, 00),
                        ServiceDate = new DateTime(2003, 1, 17),
                        ServiceId = 1,
                        MemberId = 7,
                        MemberName = "John Smith"
                    },
                    new ProvidedService {
                        Fee = 999.99,
                        DateTimeReceived = new DateTime(2001, 1, 25, 13, 00, 00),
                        ServiceDate = new DateTime(2001, 1, 28),
                        ServiceId = 1,
                        MemberId = 7,
                        MemberName = "John Smith"
                    }
                }
            };

            var result = _providerDataFormatter.FormatData(reportData);
            Assert.AreEqual(16, result.OutputLines.Count);

            Assert.AreEqual("Alex_Burbank " + DateTime.Now.ToString("MM-dd-yyyy") + ".txt", result.FileName);

            Assert.AreEqual("Alex Burbank", result.OutputLines[0]);
            Assert.AreEqual("1111", result.OutputLines[1]);
            Assert.AreEqual("Blah, OR 1111", result.OutputLines[2]);

            Assert.AreEqual("", result.OutputLines[3]);
            Assert.AreEqual("", result.OutputLines[4]);

            Assert.AreEqual("ID: 000000007", result.OutputLines[5]);

            Assert.AreEqual("", result.OutputLines[6]);
            Assert.AreEqual("", result.OutputLines[7]);

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
                "01-28-2001".PadRight(15)
                + "01-25-2001 13:00:00".PadRight(24)
                + "John Smith".PadRight(30)
                + "7".PadRight(14)
                + "1".PadRight(11)
                + "$999.99".PadRight(12), result.OutputLines[10]
                );

            Assert.AreEqual(
                "01-17-2003".PadRight(15)
                + "01-14-2004 13:00:00".PadRight(24)
                + "John Smith".PadRight(30)
                + "7".PadRight(14)
                + "1".PadRight(11)
                + "$999.99".PadRight(12), result.OutputLines[11]
                );

            Assert.AreEqual("", result.OutputLines[12]);
            Assert.AreEqual("", result.OutputLines[13]);

            Assert.AreEqual("Total consultations: 2", result.OutputLines[14]);
            Assert.AreEqual("Total fee: $1,999.98", result.OutputLines[15]);
        }
    }
}
