using ChocAnDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.Interfaces;
using ReportGenerator.Member;
using ReportGenerator.Provider;
using ReportGenerator.Summary;
using System;

namespace ReportGenerator.Tests.Factory {
    [TestClass]
    public class ReportFactoryTests {
        private ReportGenerator.Factory.IReportFactory _factory;
        private IDatabaseWrapper _database;

        [TestInitialize]
        public void Setup() {
            _factory = new ReportGenerator.Factory.ReportFactory();

            //TODO: Fix database for unit testing because path in constructor
            _database = null;
        }

        [TestMethod]
        public void CreateReport_Unknown() {
           var ex = Assert.ThrowsException<ApplicationException>(() => _factory.CreateReport(TypeOfReport.Unknown, _database));
            Assert.AreEqual("Not a member or provider report", ex.Message);
        }

        [TestMethod]
        public void CreateReport_Member() {
            var result = _factory.CreateReport(TypeOfReport.MemberReport, _database);

            Assert.IsInstanceOfType(result, typeof(IReport));
            Assert.IsInstanceOfType(result.DataGetter, typeof(MemberDataGetter));
            Assert.IsInstanceOfType(result.DataValidator, typeof(MemberDataValidator));
            Assert.IsInstanceOfType(result.DataFormatter, typeof(MemberDataFormatter));
        }

        [TestMethod]
        public void CreateReport_Provider() {
            var result = _factory.CreateReport(TypeOfReport.ProviderReport, _database);

            Assert.IsInstanceOfType(result, typeof(IReport));
            Assert.IsInstanceOfType(result.DataGetter, typeof(ProviderDataGetter));
            Assert.IsInstanceOfType(result.DataValidator, typeof(ProviderDataValidator));
            Assert.IsInstanceOfType(result.DataFormatter, typeof(ProviderDataFormatter));
        }

        [TestMethod]
        public void CreateReport_Summary() {
            var result = _factory.CreateReport(TypeOfReport.SummaryReport, _database);

            Assert.IsInstanceOfType(result, typeof(IReport));
            Assert.IsInstanceOfType(result.DataGetter, typeof(SummaryDataGetter));
            Assert.IsInstanceOfType(result.DataValidator, typeof(SummaryDataValidator));
            Assert.IsInstanceOfType(result.DataFormatter, typeof(SummaryDataFormatter));
        }

        [TestMethod]
        public void CreateProcessor() {
            var result = _factory.CreateProcessor();

            Assert.IsInstanceOfType(result, typeof(IProcessor));
        }
    }
}
