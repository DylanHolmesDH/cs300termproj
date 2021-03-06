namespace ReportGenerator.Models {
    public class SummaryDataInfo {
        public SummaryDataInfo() {
            ProviderName = string.Empty;
            TotalFee = 0;
            TotalNumberOfConsultations = 0;
        }

        public string ProviderName { get; set; }
        public double TotalFee { get; set; }
        public int TotalNumberOfConsultations { get; set; }
    }
}