using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Summary
{
    public class SummaryDataValidator : IDataValidator
    {
        public (bool valid, string errorMessage) ValidateData(ReportData reportData)
        {
            if (reportData == null)
                throw new ApplicationException("Report data object cannot be null");

            if (reportData.SummaryDataInfo == null)
                throw new ApplicationException("Summary data info cannot be null");

            return (true, "");
        }
    }
}
