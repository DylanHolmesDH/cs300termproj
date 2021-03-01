using ChocAnDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.Member;
using ReportGenerator.Provider;
using System;

namespace ReportGenerator.Tests.Factory
{
    [TestClass]
    public class FactoryTests
    {
        private ReportGenerator.Factory.Factory _factory;
        private Database _database;

        [TestInitialize]
        public void Setup()
        {
            _factory = new ReportGenerator.Factory.Factory();

            //TODO: Fix database for unit testing because path in constructor
            _database = null;
        }

        [TestMethod]
        public void CreateReport_Unknown()
        {
           var ex = Assert.ThrowsException<ApplicationException>(() => _factory.CreateReport(TypeOfReport.Unknown, _database));
            Assert.AreEqual("Not a member or provider report", ex.Message);
        }

        [TestMethod]
        public void CreateReport_Member()
        {
            var result = _factory.CreateReport(TypeOfReport.MemberReport, _database);

            Assert.IsInstanceOfType(result, typeof(Report));
            Assert.IsInstanceOfType(result.DataGetter, typeof(MemberDataGetter));
            Assert.IsInstanceOfType(result.DataValidator, typeof(MemberDataValidator));
            Assert.IsInstanceOfType(result.DataFormatter, typeof(MemberDataFormatter));
        }

        [TestMethod]
        public void CreateReport_Provider()
        {
            var result = _factory.CreateReport(TypeOfReport.ProviderReport, _database);

            Assert.IsInstanceOfType(result, typeof(Report));
            Assert.IsInstanceOfType(result.DataGetter, typeof(ProviderDataGetter));
            Assert.IsInstanceOfType(result.DataValidator, typeof(ProviderDataValidator));
            Assert.IsInstanceOfType(result.DataFormatter, typeof(ProviderDataFormatter));
        }

    }
}
