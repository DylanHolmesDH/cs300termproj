using ReportGenerator.Interfaces;
using ReportGenerator.Models;

namespace ReportGenerator.Summary {
    public class SummaryDataValidator : IDataValidator {
        public (bool valid, string errorMessage) ValidateData(ReportData data) {
            if (data == null)
                return (false, "Report data object cannot be null");

            if (data.SummaryDataInfo.Count == 0)
                return (false, "Summary report info cannot be empty");

            foreach (var summaryRecord in data.SummaryDataInfo) {
                if (string.IsNullOrWhiteSpace(summaryRecord.ProviderName))
                    return (false, "Provider name for summary cannot be empty");
            }
  
            return (true, "");
        }
    }
}
