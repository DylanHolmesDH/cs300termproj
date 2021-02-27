using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Member {
    public class MemberDataValidator : IDataValidator {
        public (bool valid, string errorMessage) ValidateData(ReportData data) {
            if (data == null)
                return (false, "Report data object cannot be null");

            if (data.MemberRecord == null)
                return (false, "Member record cannot be empty");

            if (data.ProvidedServices == null)
                return (false, "Provided services cannot be null");

            foreach (var providedService in data.ProvidedServices) {
                if (string.IsNullOrWhiteSpace(providedService.ProviderName))
                    return (false, $"Provider name for service date {providedService.ServiceDate} cannot be null or empty");

                if (string.IsNullOrWhiteSpace(providedService.ServiceName))
                    return (false, $"Service name for service date {providedService.ServiceDate} cannot be null or empty");
            }

            return (true, string.Empty);
        }
    }
}
