using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Provider {
    public class ProviderDataValidator : IDataValidator {
        public (bool valid, string errorMessage) ValidateData(ReportData data) {
            if (data == null)
                return (false, "Report data object cannot be null");

            if (data.ProviderRecord == null)
                return (false, "Provider record cannot be null");

            if (data.ProvidedServices == null)
                return (false, "Provided services cannot be null");

            foreach (var providedService in data.ProvidedServices) {
                if (providedService.ServiceId == int.MinValue)
                    return (false, $"Service ID for service date {providedService.ServiceDate} cannot be empty");

                if (string.IsNullOrWhiteSpace(providedService.MemberName))
                    return (false, $"Member name for service date {providedService.ServiceDate} cannot be null or empty");
            
                if (providedService.MemberId == int.MinValue)
                    return (false, $"Member ID for {providedService.MemberName} cannot be empty");

                if (providedService.Fee == int.MinValue)
                    return (false, $"Service fee for service date {providedService.ServiceDate} cannot be empty");
            }

            return (true, "");
        }
    }
}
