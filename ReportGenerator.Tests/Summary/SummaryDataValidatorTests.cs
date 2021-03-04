using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.Models;
using ReportGenerator.Summary;
using System;
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
        public void ReportDataNull() {
            var result = _dataValidator.ValidateData(null);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Report data object cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ReportDataSummaryDataInfoNull() {
            ReportData reportData = new ReportData() {
                SummaryDataInfo = null
            };

            var result = _dataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Summary report info cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ReportDataSummaryDataInfoEmpty() {
            ReportData reportData = new ReportData() {
                SummaryDataInfo = new List<SummaryDataInfo>()
            };

            var result = _dataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Summary report info cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ReportDataSummaryInfoProviderNameEmpty() {
            ReportData reportData = new ReportData() {
                SummaryDataInfo = new List<SummaryDataInfo> {
                    new SummaryDataInfo {
                        ProviderName = " ",
                        TotalFee = 0,
                        TotalNumberOfConsultations = 1
                    }
                }
            };

            var result = _dataValidator.ValidateData(reportData);

            Assert.AreEqual("Provider name for summary cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ReportDataSummaryInfo1SummaryRecord() {
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

            Assert.AreEqual("", result.errorMessage);
        }

        [TestMethod]
        public void ReportDataSummaryInfo2SummaryRecords() {
            ReportData reportData = new ReportData() {
                SummaryDataInfo = new List<SummaryDataInfo> {
                    new SummaryDataInfo {
                        ProviderName = "Alex Burbank",
                        TotalFee = 0,
                        TotalNumberOfConsultations = 1
                    },
                    new SummaryDataInfo {
                        ProviderName = "John Smith",
                        TotalFee = 0,
                        TotalNumberOfConsultations = 1
                    }
                }
            };

            var result = _dataValidator.ValidateData(reportData);

            Assert.AreEqual("", result.errorMessage);
        }
    }
}
