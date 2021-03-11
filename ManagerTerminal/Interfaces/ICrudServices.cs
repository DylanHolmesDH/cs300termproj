using ChocAnDatabase;

namespace ManagerTerminal {
    public interface ICrudServices {
        (bool successful, string errorMessage) DoCrudAction(IDatabaseWrapper database, TypeOfCrudAction typeOfCrudAction, UserInterfaceRecord userInterfaceRecord, IServicesFactory servicesFactory, int nextIdAvailable);
        TypeOfCrudAction DetermineTypeOfCrudAction(int optionNumber);
    }
}