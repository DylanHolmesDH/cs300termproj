using ReportGenerator.Models;

namespace ReportGenerator.EftData {
    public class EftDataValidator {
        public (bool valid, string errorMessage) ValidateData(ReportData data) {
            if (data == null)
                return (false, "Report data object cannot be null");

            if (data.EftDataInfo.Count == 0)
                return (false, "No EFT data");

            foreach (var eftDataRecord in data.EftDataInfo) {
                if (eftDataRecord.ProviderNumber == 0)
                    return (false, "No provider ID");

                if (string.IsNullOrWhiteSpace(eftDataRecord.ProviderName))
                    return (false, "No provider name associated with ID: " + eftDataRecord.ProviderNumber.ToString().PadLeft(9, '0'));

                if (eftDataRecord.TotalFee == 0)
                    return (false, "Fee cannot be 0 for provider ID of " + eftDataRecord.ProviderNumber.ToString().PadLeft(9, '0'));
            }

            return (true, "");
        }
    }
}
