using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.EftData;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;

namespace ReportGenerator.Tests.EftData {
    [TestClass]
    public class EftDataFormatterTests {
        EftDataFormatter _eftDataFormatter;

        [TestInitialize]
        public void Setup() {
            _eftDataFormatter = new EftDataFormatter();
        }

        [TestMethod]
        public void FormatData_ReportDataNull() {
            var ex = Assert.ThrowsException<NullReferenceException>(() => _eftDataFormatter.FormatData(null));

            Assert.AreEqual("Report data object cannot be null", ex.Message);
        }

        [TestMethod]
        public void FormatData_ReportDataNoEftData() {
            ReportData reportData = new ReportData {
                EftDataInfo = new List<EftDataInfo>()
            };

            var result = _eftDataFormatter.FormatData(reportData);

            Assert.AreEqual("EFT Data.txt", result.FileName);

            Assert.AreEqual(2, result.OutputLines.Count);

            Assert.AreEqual("Provider name                 "
                + "Provider #      "
                + "Total Fee       ", result.OutputLines[0]
                );
            Assert.AreEqual("_".PadRight(46, '_'), result.OutputLines[1]);
        }

        [TestMethod]
        public void FormatData_ReportData1RecordInEftData() {
            ReportData reportData = new ReportData {
                EftDataInfo = new List<EftDataInfo> {
                    new EftDataInfo {
                        ProviderName = "John Smith",
                        ProviderNumber = 7,
                        TotalFee = 100
                    }
                }
            };

            var result = _eftDataFormatter.FormatData(reportData);

            Assert.AreEqual("EFT Data.txt", result.FileName);

            Assert.AreEqual(3, result.OutputLines.Count);

            Assert.AreEqual("Provider name                 "
                + "Provider #      "
                + "Total Fee       ", result.OutputLines[0]
                );
            Assert.AreEqual("_".PadRight(46, '_'), result.OutputLines[1]);

            Assert.AreEqual("John Smith".PadRight(30) 
                + "7".PadRight(15) 
                + "$100.00".PadRight(15), result.OutputLines[2]
                );
        }

        [TestMethod]
        public void FormatData_ReportData2RecordsInEftData() {
            ReportData reportData = new ReportData {
                EftDataInfo = new List<EftDataInfo> {
                    new EftDataInfo {
                        ProviderName = "John Smith",
                        ProviderNumber = 7,
                        TotalFee = 100
                    },
                    new EftDataInfo {
                        ProviderName = "Alex Burbank",
                        ProviderNumber = 1,
                        TotalFee = 200.01
                    }
                }
            };

            var result = _eftDataFormatter.FormatData(reportData);

            Assert.AreEqual("EFT Data.txt", result.FileName);

            Assert.AreEqual(4, result.OutputLines.Count);

            Assert.AreEqual("Provider name                 "
                + "Provider #      "
                + "Total Fee       ", result.OutputLines[0]
                );
            Assert.AreEqual("_".PadRight(46, '_'), result.OutputLines[1]);

            Assert.AreEqual("John Smith".PadRight(30)
                + "7".PadRight(15)
                + "$100.00".PadRight(15), result.OutputLines[2]
                );

            Assert.AreEqual("Alex Burbank".PadRight(30)
                + "1".PadRight(15)
                + "$200.01".PadRight(15), result.OutputLines[3]
                );
        }
    }
}
