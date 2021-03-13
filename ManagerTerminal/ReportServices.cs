using ChocAnDatabase;
using ReportGenerator;
using ReportGenerator.Factory;

namespace ManagerTerminal {
    public class ReportServices : IReportServices {
        public TypeOfReport DetermineTypeOfReport(int optionNumber) {
            switch (optionNumber) {
                case 1:
                    return TypeOfReport.MemberReport;
                case 2:
                    return TypeOfReport.ProviderReport;
                case 3:
                    return TypeOfReport.SummaryReport;
                case 4:
                    return TypeOfReport.EftData;
                default:
                    return TypeOfReport.Unknown;
            }
        }

        public (bool created, string errorMessage) CreateReport(IDatabaseWrapper databaseWrapper, IReportFactory reportFactory, TypeOfReport typeOfReport, int id, int daysBack = 7) {
            var processor = reportFactory.CreateProcessor(); // This is how you call the report generator

            (bool created, string errorMessage) result = processor.GenerateReport(typeOfReport, databaseWrapper, reportFactory, id, daysBack);

            return result;
        }
    }
}
