using ChocAnDatabase;
using ReportGenerator.Interfaces;

namespace ReportGenerator.Factory
{
    public interface IReportFactory
    {
        IReport CreateReport(TypeOfReport typeOfReport, IDatabaseWrapper database);
    }
}