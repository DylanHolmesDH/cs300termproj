using ChocAnDatabase.records;

namespace ManagerTerminal {
    public interface ICrudValidator {
        (bool successful, string errorMessage) AreAllFieldsFilledIn(MemberRecord memberRecord, bool checkForId);
        (bool successful, string errorMessage) AreAllFieldsFilledIn(ProviderRecord providerRecord, bool checkForId);
        (bool exists, string errorMessage) DoesMemberExistInDatabase(bool shouldExist = true, int id = 0, string name = "");
        (bool exists, string errorMessage) DoesProviderExistInDatabase(bool shouldExist = true, int id = 0, string name = "");
        (bool successful, string errorMessage) RemoveFieldsValid(int id);
    }
}