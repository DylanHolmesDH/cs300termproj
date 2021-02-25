using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Member
{
    public class MemberDataFormatter : IDataFormatter
    {
        public ReportOutput FormatData(ReportData data)
        {
            if (data == null)
                throw new ApplicationException("Report data object cannot be null");

            ReportOutput formattedData = new ReportOutput();

            return formattedData;
        }
    }
}
