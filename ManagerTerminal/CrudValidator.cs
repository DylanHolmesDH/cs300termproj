﻿using ChocAnDatabase;
using ChocAnDatabase.records;

namespace ManagerTerminal {
    public class CrudValidator : ICrudValidator {
        private readonly IDatabaseWrapper _database;

        public CrudValidator(IDatabaseWrapper database) {
            _database = database;
        }

        public (bool successful, string errorMessage) AreAllFieldsFilledIn(MemberRecord memberRecord, bool checkForId) {
            if (checkForId && memberRecord.Number <= 0)
                return (false, "Needs to have a valid number");

            if (!string.IsNullOrWhiteSpace(memberRecord.Name))
                return (false, "Needs to have a name");

            if (!string.IsNullOrWhiteSpace(memberRecord.Address))
                return (false, "Needs to have an address");

            if (!string.IsNullOrWhiteSpace(memberRecord.City))
                return (false, "Needs to have a city");

            if (!string.IsNullOrWhiteSpace(memberRecord.State))
                return (false, "Needs to have a state");

            if (memberRecord.Zip == 0)
                return (false, "Needs to have a zip code");

            return (true, "");
        }

        public (bool successful, string errorMessage) AreAllFieldsFilledIn(ProviderRecord providerRecord, bool checkForId) {
            if (checkForId && providerRecord.Number <= 0)
                return (false, "Needs to have a valid number");

            if (!string.IsNullOrWhiteSpace(providerRecord.Name))
                return (false, "Needs to have a name");

            if (!string.IsNullOrWhiteSpace(providerRecord.Address))
                return (false, "Needs to have an address");

            if (!string.IsNullOrWhiteSpace(providerRecord.City))
                return (false, "Needs to have a city");

            if (!string.IsNullOrWhiteSpace(providerRecord.State))
                return (false, "Needs to have a state");

            if (providerRecord.Zip == 0)
                return (false, "Needs to have a zip code");

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

        public (bool successful, string errorMessage) RemoveFieldsValid(int id) {
            if (id <= 0)
                return (false, "Needs to have a valid number");

            return (true, "");
        }
    }
}