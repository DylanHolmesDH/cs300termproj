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
                return (false, "Member record cannot be null");

            if (string.IsNullOrWhiteSpace(data.MemberRecord.Name))
                return (false, "Member record must contain a name");

            if (string.IsNullOrWhiteSpace(data.MemberRecord.City))
                return (false, "Member record must contain a city");

            if (string.IsNullOrWhiteSpace(data.MemberRecord.State))
                return (false, "Member record must contain a state");

            if (string.IsNullOrWhiteSpace(data.MemberRecord.Address))
                return (false, "Member record must contain an address");

            if (data.ProvidedServices == null)
                return (false, "Provided services cannot be null");

            if (data.ProvidedServices.Count == 0)
                return (false, "Provided services cannot be empty");

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
