using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.Models;
using ReportGenerator.Summary;
using System.Collections.Generic;

namespace ReportGenerator.Tests.Summary {
    [TestClass]
    public class SummaryDataValidatorTests {
        SummaryDataValidator _dataValidator;

        [TestInitialize]
        public void Setup() {
            _dataValidator = new SummaryDataValidator();
        }

        [TestMethod]
        public void ValidateData_reportDataNull() {
            var result = _dataValidator.ValidateData(null);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Report data object cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataSummaryDataInfoEmpty() {
            ReportData reportData = new ReportData() {
                SummaryDataInfo = new List<SummaryDataInfo>()
            };

            var result = _dataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Summary report info cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataSummaryInfoProviderNameEmpty() {
            ReportData reportData = new ReportData() {
                SummaryDataInfo = new List<SummaryDataInfo> {
                    new SummaryDataInfo {
                        ProviderName = " ",
                        TotalFee = 20,
                        TotalNumberOfConsultations = 1
                    }
                }
            };

            var result = _dataValidator.ValidateData(reportData);

            Assert.AreEqual("Provider name for summary cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void Validate_reportDataSummary0TotalFee() {
            ReportData reportData = new ReportData() {
                SummaryDataInfo = new List<SummaryDataInfo> {
                    new SummaryDataInfo {
                        ProviderName = "Alex Burbank",
                        TotalFee = 0,
                        TotalNumberOfConsultations = 1
                    }
                }
            };

            var result = _dataValidator.ValidateData(reportData);

            Assert.AreEqual("Cannot print out provider with a fee of $0", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataSummaryInfo1SummaryRecord() {
            ReportData reportData = new ReportData() {
                SummaryDataInfo = new List<SummaryDataInfo> {
                    new SummaryDataInfo {
                        ProviderName = "Alex Burbank",
                        TotalFee = 20,
                        TotalNumberOfConsultations = 1
                    }
                }
            };

            var result = _dataValidator.ValidateData(reportData);

            Assert.AreEqual("", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_reportDataSummaryInfo2SummaryRecords() {
            ReportData reportData = new ReportData() {
                SummaryDataInfo = new List<SummaryDataInfo> {
                    new SummaryDataInfo {
                        ProviderName = "Alex Burbank",
                        TotalFee = 20,
                        TotalNumberOfConsultations = 1
                    },
                    new SummaryDataInfo {
                        ProviderName = "John Smith",
                        TotalFee = 20,
                        TotalNumberOfConsultations = 1
                    }
                }
            };

            var result = _dataValidator.ValidateData(reportData);

            Assert.AreEqual("", result.errorMessage);
        }
    }
}
