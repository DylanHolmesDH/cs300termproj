using ChocAnDatabase;
using ChocAnDatabase.records;

namespace ManagerTerminal {
    public class CrudValidator : ICrudValidator {
        private readonly IDatabaseWrapper _database;

        public CrudValidator(IDatabaseWrapper database) {
            _database = database;
        }

        public (bool successful, string errorMessage) AreAllFieldsFilledIn(MemberRecord memberRecord, bool checkForId) {
            if (memberRecord == null)
                return (false, "Member record is null");

            if (checkForId && memberRecord.Number <= 0)
                return (false, "Needs to have a valid number");

            if (string.IsNullOrWhiteSpace(memberRecord.Name))
                return (false, "Needs to have a name");

            if (memberRecord.Name.Length > 25)
                return (false, "Name cannot be over 25 characters long");

            if (string.IsNullOrWhiteSpace(memberRecord.Address))
                return (false, "Needs to have an address");

            if (memberRecord.Address.Length > 25)
                return (false, "Street address cannot be over 25 characters long");

            if (string.IsNullOrWhiteSpace(memberRecord.City))
                return (false, "Needs to have a city");

            if (memberRecord.City.Length > 14)
                return (false, "City cannot be over 14 characters long");

            if (string.IsNullOrWhiteSpace(memberRecord.State))
                return (false, "Needs to have a state");

            if (memberRecord.State.Length > 2)
                return (false, "State initials are to be provided! 2 characters (Ex: OR)");

            if (memberRecord.Zip < 10000 || memberRecord.Zip > 99999)
                return (false, "Needs to have a 5 digit zip code");

            return (true, "");
        }

        public (bool successful, string errorMessage) AreAllFieldsFilledIn(ProviderRecord providerRecord, bool checkForId) {
            if (providerRecord == null)
                return (false, "Provider record is null");
            
            if (checkForId && providerRecord.Number <= 0)
                return (false, "Needs to have a valid number");

            if (string.IsNullOrWhiteSpace(providerRecord.Name))
                return (false, "Needs to have a name");

            if (providerRecord.Name.Length > 25)
                return (false, "Name cannot be over 25 characters long");

            if (string.IsNullOrWhiteSpace(providerRecord.Address))
                return (false, "Needs to have an address");

            if (providerRecord.Address.Length > 25)
                return (false, "Street address cannot be over 25 characters long");

            if (string.IsNullOrWhiteSpace(providerRecord.City))
                return (false, "Needs to have a city");

            if (providerRecord.City.Length > 14)
                return (false, "City cannot be over 14 characters long");

            if (string.IsNullOrWhiteSpace(providerRecord.State))
                return (false, "Needs to have a state");

            if (providerRecord.State.Length > 2)
                return (false, "State initials are to be provided! 2 characters (Ex: OR)");

            if (providerRecord.Zip < 10000 || providerRecord.Zip > 99999)
                return (false, "Needs to have a 5 digit zip code");

            return (true, "");
        }

        public (bool exists, string errorMessage) DoesMemberExistInDatabase(bool shouldExist = true, int id = 0, string name = "") {
            if (shouldExist) {
                var memberRecord = _database.FetchMember(id);

                if (memberRecord == null)
                    return (false, "Member record does not exist");

                return (true, "");
            }
            else {
                var memberRecord = _database.FetchMemberByName(name);

                if (memberRecord != null)
                    return (true, "Member record already exists");

                return (false, "");
            }
        }

        public (bool exists, string errorMessage) DoesProviderExistInDatabase(bool shouldExist = true, int id = 0, string name = "") {
            if (shouldExist) {
                var memberRecord = _database.FetchProvider(id);

                if (memberRecord == null)
                    return (false, "Provider record does not exist");

                return (true, "");
            }
            else {
                var memberRecord = _database.FetchProviderByName(name);

                if (memberRecord != null)
                    return (true, "Provider record already exists");

                return (false, "");
            }
        }

        public (bool successful, string errorMessage) RemovalIdValid(int id) {
            if (id <= 0)
                return (false, "Needs to have a valid number");

            return (true, "");
        }
    }
}