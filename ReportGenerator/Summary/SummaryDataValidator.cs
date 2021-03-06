using ReportGenerator.Interfaces;
using ReportGenerator.Models;

namespace ReportGenerator.Summary {
    public class SummaryDataValidator : IDataValidator {
        public (bool valid, string errorMessage) ValidateData(ReportData reportData) {
            if (reportData == null)
                return (false, "Report data object cannot be null");

            if (reportData.SummaryDataInfo == null)
               return (false, "Summary report info cannot be null");

            if (reportData.SummaryDataInfo.Count == 0)
                return (false, "Summary report info cannot be empty");

            foreach (var summaryRecord in reportData.SummaryDataInfo) {
                if (string.IsNullOrWhiteSpace(summaryRecord.ProviderName))
                    return (false, "Provider name for summary cannot be empty");
            }
  
            return (true, "");
        }
    }
}
