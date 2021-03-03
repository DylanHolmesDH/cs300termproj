using ChocAnDatabase;

namespace ReportGenerator
{
    public class Processor {
        TypeOfReport typeOfReport;
        private readonly Factory.Factory _factory;

        public Processor() {
            _factory = new Factory.Factory();
        }

        public (bool created, string errorMessage) GetReportGenerator(int choice, int id) {
            typeOfReport = (TypeOfReport)choice;

            var database = new DatabaseWrapper();

            var report = _factory.CreateReport(typeOfReport, database);

            return report.Generate(id);
        }
    }
}