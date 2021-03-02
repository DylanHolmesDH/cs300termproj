using ChocAnDatabase;
using ReportGenerator.Interfaces;
using ReportGenerator.Member;
using ReportGenerator.Provider;
using System;

namespace ReportGenerator.Factory {
    public class Factory {
        public IReport CreateReport(TypeOfReport typeOfReport, DatabaseWrapper database) {
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

                default:
                    throw new ApplicationException("Not a member or provider report");

            }
        }
    }
}