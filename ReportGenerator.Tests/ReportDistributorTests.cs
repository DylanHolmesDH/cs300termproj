using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;

namespace ReportGenerator.Tests {
    [TestClass]
    public class ReportDistributorTests {
        private IReportDistributor _reportDistributor;
        private string _path;

        [TestInitialize]
        public void Setup() {
            _reportDistributor = new ReportDistributor();
            _path = string.Empty;
        }

        [TestMethod]
        public void DistributeReport_nullObject() {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _reportDistributor.DistributeReport(null));
        }

        [TestMethod]
        public void CreateFile_NoFileName() {
            ReportOutput reportOutput = new ReportOutput
            {
                OutputLines = { "John Smith", "Number", "City", "State" }
            };

            var result = _reportDistributor.DistributeReport(reportOutput);

            Assert.AreEqual(false, result.created);
            Assert.AreEqual("No filename for report", result.errorMessage);
        }

        [TestMethod]
        public void CreateFile_EmptyReportObjectWithFileName()
        {
            ReportOutput reportOutput = new ReportOutput
            {
                FileName = "junk.txt"
            };

            var result = _reportDistributor.DistributeReport(reportOutput);

            Assert.AreEqual(false, result.created);
            Assert.AreEqual("No output lines in report", result.errorMessage);
        }

        //[TestMethod]
        //public void CreateFile_ReportObjectWithFileName()
        //{
        //    ReportOutput reportOutput = new ReportOutput
        //    {
        //        FileName = "junk.txt",
        //        OutputLines = {
        //            "Service date   " + "Provider name                 "+ "Service name             ",
        //            "______________________________________________________________________",
        //            "01-01-0001     John Smith                    AA                       "
        //        }
        //    };

        //    _path = @"c:\users\ryzen\Desktop\" + reportOutput.FileName;

        //    var result = _reportDistributor.DistributeReport(reportOutput);

        //    Assert.AreEqual(true, result.created);
        //    Assert.AreEqual("", result.errorMessage);
        //}
    }
}
