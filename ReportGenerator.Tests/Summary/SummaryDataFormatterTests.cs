using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.Models;
using ReportGenerator.Summary;
using System;
using System.Collections.Generic;

namespace ReportGenerator.Tests.Summary {
    [TestClass]
    public class SummaryDataFormatterTests {
        SummaryDataFormatter _summaryDataFormatter;

        [TestInitialize]
        public void Setup() {
            _summaryDataFormatter = new SummaryDataFormatter();
        }

        [TestMethod]
        public void ReportDataNull() {
            var ex = Assert.ThrowsException<ApplicationException>(() => _summaryDataFormatter.FormatData(null));

            Assert.AreEqual("Report data object cannot be null", ex.Message);
        }

        [TestMethod]
        public void NoSummaryInfo() {
            ReportData reportData = new ReportData {
                SummaryDataInfo = new List<SummaryDataInfo>()
            };

            var ex = Assert.ThrowsException<ApplicationException>(() => _summaryDataFormatter.FormatData(reportData));

            Assert.AreEqual("There must be services provided to the member to format data", ex.Message);
        }

        [TestMethod]
        public void OnceProviderProvidingService() {
            ReportData reportData = new ReportData {
                SummaryDataInfo = new List<SummaryDataInfo> {
                    new SummaryDataInfo {
                        ProviderName = "John Smith",
                        TotalFee = 999.99,
                        TotalNumberOfConsultations = 5
                    }
                }
            };

            var result = _summaryDataFormatter.FormatData(reportData);

            Assert.AreEqual("SummaryReport.txt", result.FileName);

            Assert.AreEqual("\n", result.OutputLines[0]);

            Assert.AreEqual(
                "Provider name".PadRight(30)
                + "Consultations".PadRight(15)
                + "Fee".PadRight(15), result.OutputLines[1]
                );
            Assert.AreEqual("_".PadRight(46, '_'), result.OutputLines[2]);

            Assert.AreEqual(
                "John Smith".PadRight(30) 
                + "5".PadRight(15) 
                + "$999.99".PadRight(15), result.OutputLines[3]
                );
        }

        [TestMethod]
        public void TwoProviderProvidingService()
        {
            ReportData reportData = new ReportData
            {
                SummaryDataInfo = new List<SummaryDataInfo> {
                    new SummaryDataInfo {
                        ProviderName = "John Smith",
                        TotalFee = 999.99,
                        TotalNumberOfConsultations = 5
                    },
                    new SummaryDataInfo {
                        ProviderName = "Alex Burbank",
                        TotalFee = 999.99,
                        TotalNumberOfConsultations = 8
                    },
                }
            };

            var result = _summaryDataFormatter.FormatData(reportData);

            Assert.AreEqual("SummaryReport.txt", result.FileName);

            Assert.AreEqual("\n", result.OutputLines[0]);

            Assert.AreEqual(
                "Provider name".PadRight(30)
                + "Consultations".PadRight(15)
                + "Fee".PadRight(15), result.OutputLines[1]
                );
            Assert.AreEqual("_".PadRight(46, '_'), result.OutputLines[2]);

            Assert.AreEqual(
                "John Smith".PadRight(30)
                + "5".PadRight(15)
                + "$999.99".PadRight(15), result.OutputLines[3]
                );
            Assert.AreEqual(
                "Alex Burbank".PadRight(30)
                + "8".PadRight(15)
                + "$999.99".PadRight(15), result.OutputLines[4]
                );
        }
    }
}
