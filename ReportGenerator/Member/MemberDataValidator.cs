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
            throw new NotImplementedException();
        }
    }
}
