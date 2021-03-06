using ChocAnDatabase;

namespace ManagerTerminal {
    public class ServicesFactory : IServicesFactory {
        public IReportServices CreateReportServices() {
            return new ReportServices();
        }

        public IDatabaseWrapper CreateDatabaseWrapper() {
            return new DatabaseWrapper();
        }
    }
}
