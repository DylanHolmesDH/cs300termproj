using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ReportGenerator.Models {
    public class ReportData {
        public ReportData() {
            MemberRecord = null;
            ProvidedServices = null;
            ProvidedServices = new List<ProvidedService>();
            SummaryDataInfo = new List<SummaryDataInfo>();
        }

        public MemberRecord MemberRecord { get; set; }
        public ProviderRecord ProviderRecord { get; set; }
        public List<ProvidedService> ProvidedServices { get; set; }
        public List<SummaryDataInfo> SummaryDataInfo { get; set; }
    }
}