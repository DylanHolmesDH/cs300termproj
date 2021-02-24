namespace ReportGenerator
{
    public enum TypeOfReport
    {
        MemberReport = 1,
        ProviderReport,
        SummaryReport
    }

    public class Processor
    {
        TypeOfReport typeOfReport;
        private readonly Factory.Factory _factory;

        public Processor()
        {
            _factory = new Factory.Factory();
        }

        public (bool created, string errorMessage) GetReportGenerator(int choice, int id)
        {
            typeOfReport = (TypeOfReport)choice;
            var report = _factory.CreateReport(typeOfReport);

            return report.Generate(id);
        }
    }
}