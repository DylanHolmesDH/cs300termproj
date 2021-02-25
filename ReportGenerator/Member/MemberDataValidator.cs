using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Member
{
    public class MemberDataValidator : IDataValidator
    {
        public (bool valid, string errorMessage) ValidateData(ReportData data)
        {
            if (data == null)
                return (false, "Report data object cannot be null");

            if (data.MemberRecords.Count == 0)
                return (false, "Member record cannot be empty");

            if (data.ConsultationRecords.Count == 0)
                return (false, "No consultation records so report cannot be printed");

            return (true, string.Empty);
        }
    }
}
