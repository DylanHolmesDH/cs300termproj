using ReportGenerator.Models;

namespace ReportGenerator.Interfaces {
    public interface IReportDistributor {
        (bool created, string errorMessage) DistributeReport(ReportOutput reportOutput);
    }
}
