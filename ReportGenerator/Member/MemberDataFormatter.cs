using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;

namespace ReportGenerator.Member {
    public class MemberDataFormatter : IDataFormatter {
        public ReportOutput FormatData(ReportData data) {
            if (data == null)
                throw new ApplicationException("Report data cannot be null");

            ReportOutput formattedData = new ReportOutput();

            string todaysDate = DateTime.Now.ToString("MM-dd-yyyy");

            formattedData.FileName = data.MemberRecord.Name.Replace(' ', '_') + " " + todaysDate + ".txt";

            formattedData.OutputLines.Add(data.MemberRecord.Name);
            formattedData.OutputLines.Add(data.MemberRecord.Address);
            formattedData.OutputLines.Add(
                data.MemberRecord.City + ", " 
                + data.MemberRecord.State + " "
                + data.MemberRecord.Zip
                );

            formattedData.OutputLines.Add("");
            formattedData.OutputLines.Add("");

            formattedData.OutputLines.Add("ID: " + data.MemberRecord.Number.ToString().PadLeft(9, '0'));

            formattedData.OutputLines.Add("");
            formattedData.OutputLines.Add("");

            formattedData.OutputLines.Add(
                "Service date   " 
                + "Provider name                 " 
                + "Service name             "
                );
            formattedData.OutputLines.Add("_".PadRight(70, '_'));

            data.ProvidedServices.Sort((x, y) => x.ServiceDate.CompareTo(y.ServiceDate));

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
