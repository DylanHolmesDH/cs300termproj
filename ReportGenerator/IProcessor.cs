using ChocAnDatabase;
using ReportGenerator.Factory;

namespace ReportGenerator {
    public interface IProcessor {
        (bool created, string errorMessage) GenerateReport(TypeOfReport typeOfReport, IDatabaseWrapper databaseWrapper, IReportFactory factory, int id);
    }
}