using System;

namespace ReportGenerator.Models {
    public class ProvidedService {
        public ProvidedService() {
            ServiceName = string.Empty;
            ServiceId = 0;
            ProviderName = string.Empty;
            MemberName = string.Empty;
            MemberId = 0;
            Fee = 0;
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
