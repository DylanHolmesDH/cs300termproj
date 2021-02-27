using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ReportGenerator.Models {
    public class ReportData {
        public ReportData() {
            MemberRecord = null;
            ProvidedServices = null;
        }

        public MemberRecord MemberRecord { get; set; }
        public ProviderRecord ProviderRecord { get; set; }
        public List<ProvidedService> ProvidedServices { get; set; }
    }
}