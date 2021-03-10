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
        public void CreateReport_reportCreated() {
            (bool created, string errorMessage) valid = (true, "");

            _reportFactoryMock.Setup(c => c.CreateProcessor()).Returns(_processorMock.Object);
            _processorMock.Setup(c => c.GenerateReport((TypeOfReport)1, _databaseWrapperMock.Object, _reportFactoryMock.Object, 1, _daysBack)).Returns(valid);
            var result = _reportServices.CreateReport(_databaseWrapperMock.Object, _reportFactoryMock.Object, 1, 1);

            Assert.AreEqual(true, result.created);
            Assert.AreEqual("", result.errorMessage);

            _reportFactoryMock.Verify(c => c.CreateProcessor(), Times.Once);
            _processorMock.Verify(c => c.GenerateReport((TypeOfReport)1, _databaseWrapperMock.Object, _reportFactoryMock.Object, 1, _daysBack), Times.Once);

            _reportFactoryMock.VerifyNoOtherCalls();
            _processorMock.VerifyNoOtherCalls();
        }
    }
}
