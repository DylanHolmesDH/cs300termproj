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
            var ex = Assert.ThrowsException<ApplicationException>(() => _dataValidator.ValidateData(null));

            Assert.AreEqual("Report data object cannot be null", ex.Message);
        }

        [TestMethod]
        public void ReportDataNullSummaryDataInfo()
        {
            ReportData reportData = new ReportData();

            var ex = Assert.ThrowsException<ApplicationException>(() => _dataValidator.ValidateData(reportData));

            Assert.AreEqual("Summary data info cannot be null", ex.Message);
        }
    }
}
