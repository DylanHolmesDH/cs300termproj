using ChocAnDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Member;
using System;
using ChocAnDatabase.records;
using System.Collections.Generic;

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

        //[TestMethod]
        //public void FormatData_NoConsultationRecords()
        //{
        //    ReportData reportData = new ReportData
        //    {
        //        MemberRecords = new List<MemberRecord>(),
        //        ProviderRecords = null,
        //        ConsultationRecords = new List<ConsultationRecord>(),
        //        ServiceRecords = null
        //    };

        //    var ex = Assert.ThrowsException<ApplicationException>(() => _memberDataFormatter.FormatData(reportData));
        //    Assert.AreEqual("No consultation records so report cannot be printed", ex.Message);
        //}
    }
}
