namespace ManagerTerminal {
    public interface ICrudValidator {
        (bool successful, string errorMessage) AreAllFieldsFilledIn(string stringRecord);
        (bool exists, string errorMessage) DoesMemberExistInDatabase(bool shouldExist = true, int id = 0, string name = "");
        (bool exists, string errorMessage) DoesProviderExistInDatabase(bool shouldExist = true, int id = 0, string name = "");
    }
}