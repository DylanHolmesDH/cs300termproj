using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Provider {
    public class ProviderDataFormatter : IDataFormatter {
        public ReportOutput FormatData(ReportData data) {
            if (data == null)
                throw new ApplicationException("Report data cannot be null");

            if (data.ProvidedServices.Count == 0)
                throw ApplicationException("There must be at least one service provided to ")

            ReportOutput formattedData = new ReportOutput();

            return formattedData;
        }
    }
}
