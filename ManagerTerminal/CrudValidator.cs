using ChocAnDatabase;
using System.Collections.Generic;

namespace ManagerTerminal {
    public class CrudValidator : ICrudValidator {
        private readonly IDatabaseWrapper _database;

        public CrudValidator(IDatabaseWrapper database) {
            _database = database;
        }

        public (bool successful, string errorMessage) AreAllFieldsFilledIn(string stringRecord) {
            int addressIndex = 1;

            var recordArray = stringRecord.Split(',');

            if (recordArray.Length != 2)
                return (false, "Needs to have 2 fields. Name, Address");

            foreach (var i in recordArray)
                if (string.IsNullOrWhiteSpace(i))
                    return (false, "Field cannot be empty");

            var address = recordArray[addressIndex].Split(' ');

            if (address.Length != 4)
                return (false, "Needs to have 4 fields. The full address must be specified: Address, City, State, and Zip");

            foreach (var i in address)
                if (string.IsNullOrWhiteSpace(i))
                    return (false, "Field cannot be empty");

            return (true, "");
        }

        public (bool exists, string errorMessage) DoesMemberExistInDatabase(bool shouldExist = true, int id = 0, string name = "") {

            if (shouldExist) {
                var memberRecord = _database.FetchMember(id);

                if (memberRecord == null)
                    return (false, "Member record does not exist");
            }
            else {
                var memberRecord = _database.FetchMemberByName(name);

                if (memberRecord != null)
                    return (false, "Member record already exists");
            }

            return (true, "");
        }

        public (bool exists, string errorMessage) DoesProviderExistInDatabase(bool shouldExist = true, int id = 0, string name = "") {
            if (shouldExist) {
                var memberRecord = _database.FetchProvider(id);

                if (memberRecord == null)
                    return (false, "Provider record does not exist");
            }
            else {
                var memberRecord = _database.FetchProviderByName(name);

                if (memberRecord != null)
                    return (false, "Provider record already exists");
            }

            return (true, "");
        }
    }
}