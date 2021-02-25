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
    public class MemberDataValidatorTests
    {
        private MemberDataValidator _memberDataValidator;

        [TestInitialize]
        public void Setup()
        {
            _memberDataValidator = new MemberDataValidator();
        }

        [TestMethod]
        public void ValidateData_NullArg()
        {
            var result = _memberDataValidator.ValidateData(null);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Report data object cannot be null", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_EmptyMemberRecord()
        {
            ReportData reportData = new ReportData
            {
                MemberRecords = new List<MemberRecord>(),
                ProviderRecords = null,
                ConsultationRecords = null,
                ServiceRecords = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("Member record cannot be empty", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_NoConsultationRecords()
        {
            ReportData reportData = new ReportData
            {
                MemberRecords = new List<MemberRecord>
                {
                    new MemberRecord(new Dictionary<string, object>())
                },
                ProviderRecords = null,
                ConsultationRecords = new List<ConsultationRecord>(),
                ServiceRecords = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(false, result.valid);
            Assert.AreEqual("No consultation records so report cannot be printed", result.errorMessage);
        }

        [TestMethod]
        public void ValidateData_Valid()
        {
            var consultationRecordData = new Dictionary<string, object>();
            consultationRecordData.Add("current_date", new DateTime(2021, 1, 1));
            consultationRecordData.Add("service_date", new DateTime(2021, 2, 1));
            consultationRecordData.Add("member_number", 1);
            consultationRecordData.Add("service_number", 2);
            consultationRecordData.Add("provider_number", 3);
            consultationRecordData.Add("comments", "some comment");

            ReportData reportData = new ReportData
            {
                MemberRecords = new List<MemberRecord>
                {
                    new MemberRecord(new Dictionary<string, object>())
                },
                ProviderRecords = null,
                ConsultationRecords = new List<ConsultationRecord>
                {
                    new ConsultationRecord(consultationRecordData)
                },
                ServiceRecords = null
            };

            var result = _memberDataValidator.ValidateData(reportData);

            Assert.AreEqual(true, result.valid);
            Assert.AreEqual(string.Empty, result.errorMessage);
        }
    }
}
