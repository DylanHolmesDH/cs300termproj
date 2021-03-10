using ChocAnDatabase;

namespace ManagerTerminal {
    public interface IServicesFactory {
        ICrudServices CreateCrudServices();
        IDatabaseWrapper CreateDatabaseWrapper();
        IReportServices CreateReportServices();
        CrudValidator CreateCrudValidator(IDatabaseWrapper databaseWrapper);
        Converter CreateConverter();
    }
}