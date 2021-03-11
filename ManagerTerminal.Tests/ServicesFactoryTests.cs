using ChocAnDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManagerTerminal.Tests {
    [TestClass]
    public class ServicesFactoryTests {
        private IServicesFactory _servicesFactory;
        private IDatabaseWrapper _databaseWrapper;

        [TestInitialize]
        public void Setup() {
            _servicesFactory = new ServicesFactory();
            _databaseWrapper = new DatabaseWrapper();
        }

        [TestMethod]
        public void CreateCrudServices() {
            var result = _servicesFactory.CreateCrudServices();

            Assert.IsInstanceOfType(result, typeof(ICrudServices));
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

        [TestMethod]
        public void CreateCrudValidator() {
            var result = _servicesFactory.CreateCrudValidator(_databaseWrapper);

            Assert.IsInstanceOfType(result, typeof(ICrudValidator));
        }


        [TestMethod]
        public void CreateConverter() {
            var result = _servicesFactory.CreateConverter();

            Assert.IsInstanceOfType(result, typeof(IConverter));
        }
    }
}
