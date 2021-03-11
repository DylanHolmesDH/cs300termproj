using ChocAnDatabase;
using ReportGenerator;
using ReportGenerator.Factory;

namespace ManagerTerminal {
    public interface IReportServices {
        (bool created, string errorMessage) CreateReport(IDatabaseWrapper databaseWrapper, IReportFactory reportFactory, TypeOfReport typeOfReport, int id, int daysBack = 7);
        TypeOfReport DetermineTypeOfReport(int optionNumber);
    }
}