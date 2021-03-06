using ChocAnDatabase;
using ReportGenerator;
using ReportGenerator.Factory;

namespace ManagerTerminal {
    public class ReportServices : IReportServices {
        public (bool created, string errorMessage) CreateReport(IDatabaseWrapper databaseWrapper, IReportFactory reportFactory, int choice, int id) {
            var typeOfReport = (TypeOfReport)choice;

            // This is how you call the report generator
            var processor = reportFactory.CreateProcessor();

            (bool created, string errorMessage) result = processor.GenerateReport(typeOfReport, databaseWrapper, reportFactory, id);

            return result;
        }
    }
}
