using ChocAnDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Factory;
using ReportGenerator.Interfaces;
using System;

namespace ReportGenerator.Tests {
    [TestClass]
    public class ProcessorTests {
        private Processor _processor;
        private Mock<IReportFactory> _factoryMock;
        private Mock<IDatabaseWrapper> _databaseWrapperMock;
        private Mock<IReport> _reportMock;

        [TestInitialize]
        public void Setup() {
            _processor = new Processor();
            _factoryMock = new Mock<IReportFactory>();
            _databaseWrapperMock = new Mock<IDatabaseWrapper>();
            _reportMock = new Mock<IReport>();
        }

        [TestMethod]
        public void DistributeReport_nullDatabaseWrapper() {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _processor.GenerateReport(TypeOfReport.MemberReport, null, _factoryMock.Object, 5));
            Assert.AreEqual("Value cannot be null.\r\nParameter name: databaseWrapper", ex.Message);

            VerifyNoOtherCalls();
        }

        [TestMethod]
        public void DistributeReport_nullFactory() {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _processor.GenerateReport(TypeOfReport.MemberReport, _databaseWrapperMock.Object, null, 5));
            Assert.AreEqual("Value cannot be null.\r\nParameter name: factory", ex.Message);

            VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateFile_EmptyReportObjectWithFileName() {
            _factoryMock.Setup(c => c.CreateReport(TypeOfReport.MemberReport, _databaseWrapperMock.Object)).Returns(_reportMock.Object);
            _reportMock.Setup(c => c.Generate(5)).Returns((true, "Blah"));

            var result = _processor.GenerateReport(TypeOfReport.MemberReport, _databaseWrapperMock.Object, _factoryMock.Object, 5);

            Assert.AreEqual(true, result.created);
            Assert.AreEqual("Blah", result.errorMessage);

            _factoryMock.Verify(c => c.CreateReport(TypeOfReport.MemberReport, _databaseWrapperMock.Object), Times.Once);
            _reportMock.Verify(c => c.Generate(5), Times.Once);

            VerifyNoOtherCalls();
        }

        private void VerifyNoOtherCalls() {
            _factoryMock.VerifyNoOtherCalls();
            _reportMock.VerifyNoOtherCalls();
            _databaseWrapperMock.VerifyNoOtherCalls();
        }
    }
}
