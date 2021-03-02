using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Summary {
    public class SummaryDataFormatter : IDataFormatter {
        public ReportOutput FormatData(ReportData data) {

            if (data == null)
                throw new ApplicationException("Report data object cannot be null");

            if (data.SummaryDataInfo.Count == 0)
                throw new ApplicationException("There must be services provided to the member to format data");

            ReportOutput formattedData = new ReportOutput();
            double totalFee = 0;
            int totalProviders = 0;
            int totalConsultations = 0;

            formattedData.FileName = "SummaryReport.txt";

            formattedData.OutputLines.Add("\n");

            formattedData.OutputLines.Add(
                "Provider name                 "
                + "Consultations  "
                + "Fee            " 
                );
            formattedData.OutputLines.Add("_".PadRight(46, '_'));

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

            formattedData.OutputLines.Add("\n");
            formattedData.OutputLines.Add("\n");

            formattedData.OutputLines.Add("Total number of providers for week: " + totalConsultations.ToString());
            formattedData.OutputLines.Add("Total number of consultations for week: " + totalConsultations.ToString());
            formattedData.OutputLines.Add("Total Fee for week: " + totalFee.ToString("C"));

            return formattedData;
        }
    }
}
