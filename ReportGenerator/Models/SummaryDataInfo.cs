using System.Collections.Generic;

namespace ReportGenerator.Models
{
    public class SummaryDataInfo
    {
        public SummaryDataInfo()
        {
            ProviderName = string.Empty;
            TotalFee = int.MinValue;
            TotalNumberOfConsultations = int.MinValue;
        }

        public string ProviderName { get; set; }
        public double TotalFee { get; set; }
        public int TotalNumberOfConsultations { get; set; }
    }
}