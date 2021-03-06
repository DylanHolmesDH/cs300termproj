using ChocAnDatabase;
using ReportGenerator.Factory;

namespace ManagerTerminal {
    public interface IReportServices {
        (bool created, string errorMessage) CreateReport(IDatabaseWrapper databaseWrapper, IReportFactory reportFactory, int choice, int id);
    }
}