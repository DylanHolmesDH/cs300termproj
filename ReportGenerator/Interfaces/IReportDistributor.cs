using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Interfaces {
    public interface IReportDistributor {
        (bool created, string errorMessage) DistributeReport(ReportOutput reportOutput);
    }
}
