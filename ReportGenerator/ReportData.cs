using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ReportGenerator
{
    public class ReportData
    {
        public List<MemberRecord> MemberRecords { get; set; }
        public List<ProviderRecord> ProviderRecords { get; set; }
        public List<ServiceRecord> ServiceRecords { get; set; }
        public List<ConsultationRecord> ConsultationRecords { get; set; }
    }
}