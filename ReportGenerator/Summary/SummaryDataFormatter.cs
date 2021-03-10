using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;

namespace ReportGenerator.Summary {
    public class SummaryDataFormatter : IDataFormatter {
        public ReportOutput FormatData(ReportData data) {
            if (data == null)
                throw new NullReferenceException("Report data object cannot be null");

            ReportOutput formattedData = new ReportOutput();
            double totalFee = 0;
            int totalProviders = 0;
            int totalConsultations = 0;

            formattedData.FileName = "Summary Report.txt";

            formattedData.OutputLines.Add("");

            formattedData.OutputLines.Add(
                "Provider name                 "
                + "Consultations  "
                + "Fee            " 
                );
            formattedData.OutputLines.Add("_".PadRight(57, '_'));

            foreach (var providerConsultations in data.SummaryDataInfo)
            {
                totalFee = totalFee + providerConsultations.TotalFee;
                totalProviders++;
                totalConsultations = totalConsultations + providerConsultations.TotalNumberOfConsultations;

                formattedData.OutputLines.Add(
                    providerConsultations.ProviderName.PadRight(30)
                    + providerConsultations.TotalNumberOfConsultations.ToString().PadRight(15)
                    + providerConsultations.TotalFee.ToString("C").PadRight(15)
                     );
            }

            formattedData.OutputLines.Add("_".PadRight(57, '_'));
            formattedData.OutputLines.Add("_".PadRight(57, '_'));

            formattedData.OutputLines.Add(
                    totalProviders.ToString().PadRight(30)
                    + totalConsultations.ToString().PadRight(15)
                    + totalFee.ToString("C").PadRight(15)
                     );

            return formattedData;
        }
    }
}
