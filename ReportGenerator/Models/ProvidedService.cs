using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Models {
    public class ProvidedService {
        public ProvidedService() {
            ProviderName = string.Empty;
            ServiceName = string.Empty;
        }

        public DateTime ServiceDate { get; set; }
        public DateTime dateTimeReceived { get; set; }
        public string ProviderName { get; set; }
        public string MemberName { get; set; }
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Fee { get; set; }
    }
}
