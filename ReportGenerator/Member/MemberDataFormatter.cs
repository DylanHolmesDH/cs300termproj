using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Member
{
    public class MemberDataFormatter : IDataFormatter
    {
        public ReportOutput FormatData(ReportData data)
        {
            if (data == null)
                throw new ApplicationException("Report data object cannot be null");

            if (data.ProvidedServices.Count == 0)
                throw new ApplicationException("There must be consultation records to format data");

            ReportOutput formattedData = new ReportOutput();

            var memberRecord = data.MemberRecord;

            formattedData.FileName = "Member" + memberRecord.Name + "Report.txt";

            formattedData.OutputLines.Add("Member name: " + memberRecord.Name + " Member number: " + 
                memberRecord.Number);

            formattedData.OutputLines.Add("Address, City, State, Zip: " + memberRecord.Address + " " + 
                memberRecord.City + " " + memberRecord.State + " " + memberRecord.Zip);

            foreach (var providedService in data.ProvidedServices)
            {
                formattedData.OutputLines.Add("\tService record: " + providedService.ServiceDate + 
                    ", " + providedService.ProviderName + ", " + providedService.ServiceName);
            }

            return formattedData;
        }
    }
}
