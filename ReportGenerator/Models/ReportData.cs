using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ReportGenerator.Models {
    public class ReportData {
        public ReportData() {
            MemberRecord = null;
            ProvidedServices = new List<ProvidedService>();
            ProvidedServices = new List<ProvidedService>();
            SummaryDataInfo = new List<SummaryDataInfo>();
            EftDataInfo = new List<EftDataInfo>();
        }

        public MemberRecord MemberRecord { get; set; }
        public ProviderRecord ProviderRecord { get; set; }
        public List<ProvidedService> ProvidedServices { get; set; }
        public List<SummaryDataInfo> SummaryDataInfo { get; set; }
        public List<EftDataInfo> EftDataInfo { get; set; }
    }
}