using ChocAnDatabase;
using ChocAnDatabase.records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportGenerator.Interfaces;
using ReportGenerator.Member;
using System;

namespace ReportGenerator.Tests.Member {
    [TestClass]
    public class MemberDataGetterTests {
        //private Mock<Database> _databaseMock;
        //private MemberDataGetter _memberDataGetter;

        //[TestInitialize]
        //public void Setup()
        //{
        //    _databaseMock = new Mock<Database>();

        //    _memberDataGetter = new MemberDataGetter(_databaseMock.Object);
        //}

        //[TestMethod]
        //public void GetDataNull()
        //{
        //    _databaseMock.Setup(c => c.FetchMember(3)).Returns((Record) null);

        //    var result = _memberDataGetter.GetData(3);

        //    _databaseMock.Verify(c => c.FetchMember(3), Times.Once);
        //}
    }
}
