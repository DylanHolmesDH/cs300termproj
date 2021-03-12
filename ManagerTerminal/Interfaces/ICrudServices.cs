using ChocAnDatabase;

namespace ManagerTerminal {
    public interface ICrudServices {
        (bool successful, string errorMessage) DoCrudAction(IDatabaseWrapper database, TypeOfCrudAction typeOfCrudAction, UserInterfaceRecord userInterfaceRecord, IServicesFactory servicesFactory);
        TypeOfCrudAction DetermineTypeOfCrudAction(int optionNumber);
    }
}