using ChocAnDatabase;

namespace ManagerTerminal {
    public interface IServicesFactory {
        ICrudServices CreateCrudServices();
        IDatabaseWrapper CreateDatabaseWrapper();
        IReportServices CreateReportServices();
        ICrudValidator CreateCrudValidator(IDatabaseWrapper databaseWrapper);
        IConverter CreateConverter();
    }
}