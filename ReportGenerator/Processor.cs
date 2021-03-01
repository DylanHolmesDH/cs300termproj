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

            Database database = new Database();

            var report = _factory.CreateReport(typeOfReport, database);

            return report.Generate(id);
        }
    }
}