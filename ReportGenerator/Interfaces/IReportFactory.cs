using ChocAnDatabase;
using ReportGenerator.Interfaces;

namespace ReportGenerator.Factory {
    public interface IReportFactory {
        IProcessor CreateProcessor();
        IReport CreateReport(TypeOfReport typeOfReport, IDatabaseWrapper database);
    }
}