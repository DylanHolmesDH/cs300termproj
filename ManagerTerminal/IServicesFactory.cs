using ChocAnDatabase;

namespace ManagerTerminal {
    public interface IServicesFactory {
        IDatabaseWrapper CreateDatabaseWrapper();
        IReportServices CreateReportServices();
    }
}