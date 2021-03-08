using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;

namespace ReportGenerator.EftData {
    public class EftDataFormatter : IDataFormatter {
        public ReportOutput FormatData(ReportData data) {
            var formattedData = new ReportOutput();

            if (data == null)
                throw new NullReferenceException("Report data object cannot be null");

            formattedData.FileName = "EFT Data.txt";

            formattedData.OutputLines.Add(
                "Provider name                 "
                + "Provider #      "
                + "Total Fee       "
                );
            formattedData.OutputLines.Add("_".PadRight(46, '_'));

            foreach (var eftData in data.EftDataInfo) {
                formattedData.OutputLines.Add(
                    eftData.ProviderName.PadRight(30)
                    + eftData.ProviderNumber.ToString().PadRight(15)
                    + eftData.TotalFee.ToString("C").PadRight(15)
                     );
            }

            return formattedData;
        }
    }
}