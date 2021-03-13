using ChocAnDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator;
using ReportGenerator.Factory;
using ReportGenerator.Interfaces;

namespace ManagerTerminal.Tests {
    [TestClass]
    public class ReportServicesTests {
        private Mock<IReportFactory> _reportFactoryMock;
        private Mock<IProcessor> _processorMock;
        private Mock<IDatabaseWrapper> _databaseWrapperMock;
        private IReportServices _reportServices;
        private int _daysBack = 7;

        [TestInitialize]
        public void Setup() {
            _reportFactoryMock = new Mock<IReportFactory>();
            _processorMock = new Mock<IProcessor>();
            _databaseWrapperMock = new Mock<IDatabaseWrapper>();
            _reportServices = new ReportServices();
        }

        [TestMethod]
        public void DetermineTypeOfReport_Member() {
            var result = _reportServices.DetermineTypeOfReport(1);

            Assert.IsInstanceOfType(result, typeof(TypeOfReport));
            Assert.AreEqual(result, TypeOfReport.MemberReport);
        }

        [TestMethod]
        public void DetermineTypeOfReport_Provider() {
            var result = _reportServices.DetermineTypeOfReport(2);

            Assert.IsInstanceOfType(result, typeof(TypeOfReport));
            Assert.AreEqual(result, TypeOfReport.ProviderReport);
        }

        [TestMethod]
        public void DetermineTypeOfReport_Summary() {
            var result = _reportServices.DetermineTypeOfReport(3);

            Assert.IsInstanceOfType(result, typeof(TypeOfReport));
            Assert.AreEqual(result, TypeOfReport.SummaryReport);
        }

        [TestMethod]
        public void DetermineTypeOfReport_Eft() {
            var result = _reportServices.DetermineTypeOfReport(4);

            Assert.IsInstanceOfType(result, typeof(TypeOfReport));
            Assert.AreEqual(result, TypeOfReport.EftData);
        }


        [TestMethod]
        public void DetermineTypeOfReport_Unknown() {
            var result = _reportServices.DetermineTypeOfReport(0);

            Assert.IsInstanceOfType(result, typeof(TypeOfReport));
            Assert.AreEqual(result, TypeOfReport.Unknown);
        }

        [TestMethod]
        public void CreateReport_reportCreated() {
            (bool created, string errorMessage) valid = (true, "");

            _reportFactoryMock.Setup(c => c.CreateProcessor()).Returns(_processorMock.Object);
            _processorMock.Setup(c => c.GenerateReport((TypeOfReport)1, _databaseWrapperMock.Object, _reportFactoryMock.Object, 1, _daysBack)).Returns(valid);
            var result = _reportServices.CreateReport(_databaseWrapperMock.Object, _reportFactoryMock.Object, (TypeOfReport)1, 1);

            Assert.AreEqual(true, result.created);
            Assert.AreEqual("", result.errorMessage);

            _reportFactoryMock.Verify(c => c.CreateProcessor(), Times.Once);
            _processorMock.Verify(c => c.GenerateReport((TypeOfReport)1, _databaseWrapperMock.Object, _reportFactoryMock.Object, 1, _daysBack), Times.Once);

            _reportFactoryMock.VerifyNoOtherCalls();
            _processorMock.VerifyNoOtherCalls();
        }
    }
}
