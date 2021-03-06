using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;

namespace ReportGenerator.Tests {
    [TestClass]
    public class ReportTests {
        private Mock<IDataGetter> _dataGetterMock;
        private Mock<IDataValidator> _dataValidatorMock;
        private Mock<IDataFormatter> _dataFormatterMock;
        private Mock<IReportDistributor> _reportDistributorMock;

        private Report _report;

        [TestInitialize]
        public void Setup() {
            _dataGetterMock = new Mock<IDataGetter>();
            _dataValidatorMock = new Mock<IDataValidator>();
            _dataFormatterMock = new Mock<IDataFormatter>();
            _reportDistributorMock = new Mock<IReportDistributor>();

            _report = new Report(
                _dataGetterMock.Object,
                _dataValidatorMock.Object,
                _dataFormatterMock.Object,
                _reportDistributorMock.Object
                );
        }

        [TestMethod]
        public void Generate_valid() {
            var reportOutput = new ReportOutput();
            var reportData = new ReportData();

            _dataGetterMock.Setup(c => c.GetData(3)).Returns(reportData);

            _dataValidatorMock.Setup(c => c.ValidateData(reportData)).Returns((true, ""));

            _dataFormatterMock.Setup(c => c.FormatData(reportData)).Returns(reportOutput);

            _reportDistributorMock.Setup(c => c.DistributeReport(reportOutput)).Returns((true, ""));
                
            var result = _report.Generate(3);

            Assert.AreEqual(true, result.created);
            Assert.AreEqual("", result.errorMessage);

            _dataGetterMock.Verify(c => c.GetData(3), Times.Once);

            _dataValidatorMock.Verify(c => c.ValidateData(reportData), Times.Once);

            _dataFormatterMock.Verify(c => c.FormatData(reportData), Times.Once);

            _reportDistributorMock.Verify(c => c.DistributeReport(reportOutput), Times.Once);
        }

        [TestMethod]
        public void Generate_invalid() {
            var reportOutput = new ReportOutput();
            var reportData = new ReportData();

            _dataGetterMock.Setup(c => c.GetData(3)).Returns(reportData);

            _dataValidatorMock.Setup(c => c.ValidateData(reportData)).Returns((false, ""));

            var result = _report.Generate(3);

            Assert.AreEqual(false, result.created);
            Assert.AreEqual("", result.errorMessage);

            _dataGetterMock.Verify(c => c.GetData(3), Times.Once);

            _dataValidatorMock.Verify(c => c.ValidateData(reportData), Times.Once);
        }
    }
}
