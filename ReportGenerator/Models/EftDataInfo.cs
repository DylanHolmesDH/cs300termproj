namespace ReportGenerator.Models
{
    public class EftDataInfo
    {
        public EftDataInfo()
        {
            ProviderName = string.Empty;
            ProviderNumber = 0;
            TotalFee = 0;
        }

        public string ProviderName { get; set; }
        public int ProviderNumber { get; set; }
        public double TotalFee { get; set; }

    }
}