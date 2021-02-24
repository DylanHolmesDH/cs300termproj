using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ReportGenerator
{
    public class ReportData
    {
        public List<MemberRecord> MemberRecordList { get; set; }
        public List<ProviderRecord> ProviderRecordList { get; set; }
        public List<ServiceRecord> ServiceRecords { get; set; }
        public List<ProviderRecord> ConsultationRecord { get; set; }
    }
}