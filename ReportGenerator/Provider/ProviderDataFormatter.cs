using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;

namespace ReportGenerator.Provider {
    public class ProviderDataFormatter : IDataFormatter {
        private double _totalFee;
        private int _totalConsultations;

        public ProviderDataFormatter() {
            _totalFee = 0.0;
            _totalConsultations = 0;
        }

        public ReportOutput FormatData(ReportData data) {
            if (data == null)
                throw new ApplicationException("Report data cannot be null");

            ReportOutput formattedData = new ReportOutput();

            string todaysDate = DateTime.Now.ToString("MM-dd-yyyy");

            formattedData.FileName = data.ProviderRecord.Name + todaysDate + ".txt";

            formattedData.OutputLines.Add(data.ProviderRecord.Name);
            formattedData.OutputLines.Add(data.ProviderRecord.Address);
            formattedData.OutputLines.Add(
                data.ProviderRecord.City + ", " 
                + data.ProviderRecord.State + " " 
                + data.ProviderRecord.Zip
                );

            formattedData.OutputLines.Add("");
            formattedData.OutputLines.Add("");

            formattedData.OutputLines.Add("ID: " + data.ProviderRecord.Number.ToString());

            formattedData.OutputLines.Add("");
            formattedData.OutputLines.Add("");

            formattedData.OutputLines.Add(
                "Service date   " 
                + "Date record received    " 
                + "Member name                   " 
                + "Member ID     "
                + "Service ID " 
                + "Fee         "
                );

            data.ProvidedServices.Sort((x, y) => x.DateTimeReceived.CompareTo(y.DateTimeReceived));

            formattedData.OutputLines.Add("_".PadRight(106, '_'));

            foreach (var providedService in data.ProvidedServices) {
                string serviceDate = providedService.ServiceDate.ToString("MM-dd-yyyy");
                string dateReceived = providedService.DateTimeReceived.ToString("MM-dd-yyyy HH:mm:ss");

                _totalFee = _totalFee + providedService.Fee;
                _totalConsultations++;
                
                formattedData.OutputLines.Add(
                serviceDate.PadRight(15)
                + dateReceived.PadRight(24)
                + providedService.MemberName.PadRight(30)
                + providedService.MemberId.ToString().PadRight(14)
                + providedService.ServiceId.ToString().PadRight(11)
                + providedService.Fee.ToString("C").PadRight(12)
                );
            }

            formattedData.OutputLines.Add("");
            formattedData.OutputLines.Add("");

            formattedData.OutputLines.Add("Total consultations: " + _totalConsultations.ToString());
            formattedData.OutputLines.Add("Total fee: " + _totalFee.ToString("C"));

            return formattedData;
        }
    }
}
