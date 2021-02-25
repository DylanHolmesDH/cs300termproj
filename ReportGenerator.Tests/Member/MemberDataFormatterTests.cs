using ChocAnDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Member;
using System;
using ChocAnDatabase.records;
using System.Collections.Generic;
using ReportGenerator.Models;

namespace ReportGenerator.Tests.Member
{
    [TestClass]
    public class MemberDataFormatterTests
    {
        private MemberDataFormatter _memberDataFormatter;
        private Mock<Database> _databaseMock;

        [TestInitialize]
        public void Setup()
        {
            _memberDataFormatter = new MemberDataFormatter();
        }

        [TestMethod]
        public void FormatData_NullArg()
        {
            var ex = Assert.ThrowsException<ApplicationException>(() => _memberDataFormatter.FormatData(null));
        }

        [TestMethod]
        public void FormatData_NoConsultationRecords()
        {
            ReportData reportData = new ReportData
            {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService>()
            };

            var ex = Assert.ThrowsException<ApplicationException>(() => _memberDataFormatter.FormatData(reportData));
            Assert.AreEqual("There must be consultation records to format data", ex.Message);
        }
    }
}
