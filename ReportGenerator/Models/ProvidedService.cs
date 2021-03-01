using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Models {
    public class ProvidedService {
        public ProvidedService() {
            ServiceName = string.Empty;
            ServiceId = int.MinValue;
            ProviderName = string.Empty;
            MemberName = string.Empty;
            MemberId = int.MinValue;
            Fee = int.MinValue;
        }

        public DateTime ServiceDate { get; set; }
        public string ServiceName { get; set; }
        public int ServiceId { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public string ProviderName { get; set; }
        public string MemberName { get; set; }
        public int MemberId { get; set; }
        public double Fee { get; set; }
    }
}
