using ChocAnDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManagerTerminal.Tests {
    [TestClass]
    public class ServicesFactoryTests {
        private ServicesFactory _servicesFactory;

        [TestInitialize]
        public void Setup() {
            _servicesFactory = new ServicesFactory();
        }

        [TestMethod]
        public void CreateReportServices() {
            var result = _servicesFactory.CreateReportServices();

            Assert.IsInstanceOfType(result, typeof(IReportServices));
        }

        [TestMethod]
        public void CreateDataWrapper() {
            var result = _servicesFactory.CreateDatabaseWrapper();

            Assert.IsInstanceOfType(result, typeof(IDatabaseWrapper));
        }
    }
}
