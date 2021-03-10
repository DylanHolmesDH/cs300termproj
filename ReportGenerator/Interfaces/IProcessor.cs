using ChocAnDatabase;
using ReportGenerator.Factory;

namespace ReportGenerator.Interfaces {
    public interface IProcessor {
        (bool created, string errorMessage) GenerateReport(TypeOfReport typeOfReport, IDatabaseWrapper databaseWrapper, IReportFactory factory, int id, int daysBack);
    }
}