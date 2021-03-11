using ChocAnDatabase;

namespace ManagerTerminal {
    public class ServicesFactory : IServicesFactory {
        public ICrudServices CreateCrudServices() {
            return new CrudServices();
        }

        public IReportServices CreateReportServices() {
            return new ReportServices();
        }

        public IDatabaseWrapper CreateDatabaseWrapper() {
            return new DatabaseWrapper();
        }

        public ICrudValidator CreateCrudValidator(IDatabaseWrapper databaseWrapper) {
            return new CrudValidator(databaseWrapper);
        }

        public IConverter CreateConverter() {
            return new Converter();
        }
    }
}
