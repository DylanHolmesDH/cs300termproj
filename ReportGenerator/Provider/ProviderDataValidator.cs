using ReportGenerator.Interfaces;
using ReportGenerator.Models;

namespace ReportGenerator.Provider {
    public class ProviderDataValidator : IDataValidator {
        public (bool valid, string errorMessage) ValidateData(ReportData data) {
            if (data == null)
                return (false, "Report data object cannot be null");

            if (data.ProviderRecord == null)
                return (false, "No provider record");

            if (string.IsNullOrWhiteSpace(data.ProviderRecord.Name))
                return (false, "Provider record must contain a name");

            if (string.IsNullOrWhiteSpace(data.ProviderRecord.City))
                return (false, "Provider record must contain a city");

            if (string.IsNullOrWhiteSpace(data.ProviderRecord.State))
                return (false, "Provider record must contain a state");

            if (string.IsNullOrWhiteSpace(data.ProviderRecord.Address))
                return (false, "Provider record must contain an address");

            if (data.ProviderRecord.Number == 0)
                return (false, "Provider ID cannot be 0");

            if (data.ProvidedServices == null)
                return (false, "Provided services cannot be null");

            if (data.ProvidedServices.Count == 0)
                return (false, "Provided services cannot be empty");

            foreach (var providedService in data.ProvidedServices) {
                if (providedService.ServiceId == 0)
                    return (false, $"Service ID for service date {providedService.ServiceDate} cannot be 0");

                if (string.IsNullOrWhiteSpace(providedService.MemberName))
                    return (false, $"Member name for service date {providedService.ServiceDate} cannot be null or empty");
            
                if (providedService.MemberId == 0)
                    return (false, $"Member ID for {providedService.MemberName} cannot be 0");
            }

            return (true, "");
        }
    }
}
