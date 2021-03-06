using ChocAnDatabase;
using ReportGenerator.Interfaces;
using ReportGenerator.Member;
using ReportGenerator.Provider;
using ReportGenerator.Summary;
using System;

namespace ReportGenerator.Factory {
    public class ReportFactory : IReportFactory {
        public IReport CreateReport(TypeOfReport typeOfReport, IDatabaseWrapper database) {
            IReportDistributor reportDistributor = new ReportDistributor();

            switch (typeOfReport) {
                case TypeOfReport.MemberReport:
                    return new Report(
                        new MemberDataGetter(database),
                        new MemberDataValidator(),
                        new MemberDataFormatter(),
                        reportDistributor);
                case TypeOfReport.ProviderReport:
                    return new Report(
                        new ProviderDataGetter(database),
                        new ProviderDataValidator(),
                        new ProviderDataFormatter(),
                        reportDistributor);
                case TypeOfReport.SummaryReport:
                    return new Report(
                        new SummaryDataGetter(database),
                        new SummaryDataValidator(),
                        new SummaryDataFormatter(),
                        reportDistributor);

                default:
                    throw new ApplicationException("Not a member or provider report");

            }
        }

        public IProcessor CreateProcessor() {
            return new Processor();
        }
    }
}