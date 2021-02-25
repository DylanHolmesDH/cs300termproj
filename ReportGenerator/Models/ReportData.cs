using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ReportGenerator.Models
{
    public class ReportData
    {
        public MemberRecord MemberRecord { get; set; }
        public List<ProvidedService> ProvidedServices { get; set; }
    }
}