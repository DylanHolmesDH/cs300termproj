using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;

namespace ReportGenerator.Member {
    public class MemberDataFormatter : IDataFormatter {
        public ReportOutput FormatData(ReportData data) {
            if (data == null)
                throw new ApplicationException("Report data cannot be null");

            ReportOutput formattedData = new ReportOutput();

            formattedData.FileName = data.MemberRecord.Name + ".txt";

            formattedData.OutputLines.Add(data.MemberRecord.Name);
            formattedData.OutputLines.Add(data.MemberRecord.Address);
            formattedData.OutputLines.Add(
                data.MemberRecord.City + " " 
                + data.MemberRecord.State + " "
                + data.MemberRecord.Zip
                );

            formattedData.OutputLines.Add("\n");
            formattedData.OutputLines.Add("\n");
            formattedData.OutputLines.Add("\n");

            formattedData.OutputLines.Add(data.MemberRecord.Number.ToString());

            formattedData.OutputLines.Add("\n");

            formattedData.OutputLines.Add(
                "Service date   " 
                + "Provider name                 " 
                + "Service name             "
                );
            formattedData.OutputLines.Add("_".PadRight(70, '_'));

            foreach (var providedService in data.ProvidedServices) {
                string serviceDate = providedService.ServiceDate.Date.ToString("MM-dd-yyyy");

                formattedData.OutputLines.Add(
                    serviceDate.PadRight(15) 
                    + providedService.ProviderName.PadRight(30)
                    + providedService.ServiceName.PadRight(25)
                    );
            }

            return formattedData;
        }
    }
}
