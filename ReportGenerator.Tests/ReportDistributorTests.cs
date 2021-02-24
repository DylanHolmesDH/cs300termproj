using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator;

namespace ReportGenerator.Tests
{
    [TestClass]
    public class ReportDistributorTests
    {
        private ReportDistributor _reportDistributor;

        [TestInitialize]
        public void Setup()
        {
            _reportDistributor = new ReportDistributor();
        }

        [TestMethod]
        public void CreateFile_nullReportObject()
        {
            var result = _reportDistributor.DistributeReport(null);

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void CreateFile_EmptyReportObjectWithFileName()
        {
            ReportOutput reportOutput = new ReportOutput
            {
                FileName = "junk"
            };

            var result = _reportDistributor.DistributeReport(reportOutput);

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void CreateFile_NoFileName()
        {
            ReportOutput reportOutput = new ReportOutput
            {
                OutputLines = { "John Smith", "Number", "City", "State" }
            };

            var result = _reportDistributor.DistributeReport(reportOutput);

            Assert.AreEqual("", result);
        }
    }
}
