using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator;
using ReportGenerator.Interfaces;
using System;

namespace ReportGenerator.Tests
{
    [TestClass]
    public class ReportDistributorTests
    {
        private IReportDistributor _reportDistributor;

        [TestInitialize]
        public void Setup()
        {
            _reportDistributor = new ReportDistributor();
        }

        [TestMethod]
        public void DistributeReport_nullObject()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _reportDistributor.DistributeReport(null));
        }

        [TestMethod]
        public void CreateFile_EmptyReportObjectWithFileName()
        {
            ReportOutput reportOutput = new ReportOutput
            {
                FileName = "junk"
            };

            var result = _reportDistributor.DistributeReport(reportOutput);

            Assert.AreEqual(false, result.created);
            Assert.AreEqual("No output lines in report", result.errorMessage);
        }

        [TestMethod]
        public void CreateFile_NoFileName()
        {
            ReportOutput reportOutput = new ReportOutput
            {
                OutputLines = { "John Smith", "Number", "City", "State" }
            };

            var result = _reportDistributor.DistributeReport(reportOutput);

            Assert.AreEqual(false, result.created);
            Assert.AreEqual("No filename for report", result.errorMessage);
        }
    }
}
