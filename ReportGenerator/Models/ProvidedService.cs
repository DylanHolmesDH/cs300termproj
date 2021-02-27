using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Models
{
    public class ProvidedService
    {
        public DateTime ServiceDate { get; set; }
        public string ProviderName { get; set; }
        public string ServiceName { get; set; }
    }
}
