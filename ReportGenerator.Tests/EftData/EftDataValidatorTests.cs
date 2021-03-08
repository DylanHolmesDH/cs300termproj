using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.EftData;
using ReportGenerator.Models;
using System.Collections.Generic;

namespace ReportGenerator.Tests.EftData {
    [TestClass]
    public class EftDataValidatorTests {
        EftDataValidator _eftDataValidator;

        [TestInitialize]
        public void Setup() {
            _eftDataValidator = new EftDataValidator();
        }

        [TestMethod]
        public void ValidateData_ReportDataNull() {
            var result = _eftDataValidator.ValidateData((ReportData)null);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Report data object cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_ReportDataNoEftDataInfo() {
            ReportData reportData = new ReportData() {
                EftDataInfo = new List<EftDataInfo>()
            };

            var result = _eftDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("No EFT data", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_ReportData0ProviderNumber() {
            ReportData reportData = new ReportData() {
                EftDataInfo = new List<EftDataInfo> {
                    new EftDataInfo {
                        ProviderName = "Alex Burbank",
                        ProviderNumber = 0,
                        TotalFee = 20
                    }
                }
            };

            var result = _eftDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("No provider ID", result.errorMessage);
        }


        [TestMethod]
        public void ValidateData_ReportDataNoProviderName() {
            ReportData reportData = new ReportData() {
                EftDataInfo = new List<EftDataInfo> {
                    new EftDataInfo {
                        ProviderNumber = 7,
                        TotalFee = 20
                    }
                }
            };

            var result = _eftDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("No provider name associated with ID: 000000007", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_ReportData0Fee() {
            ReportData reportData = new ReportData() {
                EftDataInfo = new List<EftDataInfo> {
                    new EftDataInfo {
                        ProviderName = "Alex Burbank",
                        ProviderNumber = 7,
                        TotalFee = 0
                    }
                }
            };

            var result = _eftDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Fee cannot be 0 for provider ID of 000000007", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_ReportData1Record() {
            ReportData reportData = new ReportData() {
                EftDataInfo = new List<EftDataInfo> {
                    new EftDataInfo {
                        ProviderName = "Alex Burbank",
                        ProviderNumber = 7,
                        TotalFee = 100
                    }
                }
            };

            var result = _eftDataValidator.ValidateData(reportData);

            Assert.AreEqual(true, result.valid);
            Assert.AreEqual("", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_ReportData2Records() {
            ReportData reportData = new ReportData() {
                EftDataInfo = new List<EftDataInfo> {
                    new EftDataInfo {
                        ProviderName = "Alex Burbank",
                        ProviderNumber = 7,
                        TotalFee = 100
                    },
                    new EftDataInfo {
                        ProviderName = "John Smith",
                        ProviderNumber = 1,
                        TotalFee = 999
                    }
                }
            };

            var result = _eftDataValidator.ValidateData(reportData);

            Assert.AreEqual(true, result.valid);
            Assert.AreEqual("", result.errorMessage);
        }
    }
}
