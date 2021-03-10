using ChocAnDatabase;

namespace ManagerTerminal {
    public class CrudServices : ICrudServices {
        public TypeOfCrudAction DetermineTypeOfCrudAction(int optionNumber) {
            switch (optionNumber) {
                case 4:
                    return TypeOfCrudAction.AddMember;
                case 5:
                    return TypeOfCrudAction.AddProvider;
                case 6:
                    return TypeOfCrudAction.AddMember;
                case 7:
                    return TypeOfCrudAction.AddMember;
                case 8:
                    return TypeOfCrudAction.AddMember;
                case 9:
                    return TypeOfCrudAction.AddMember;
                default:
                    return TypeOfCrudAction.Unknown;
            }
        }

        public (bool successful, string errorMessage) DoCrudAction(IDatabaseWrapper database, TypeOfCrudAction typeOfCrudAction, string stringRecord, IServicesFactory servicesFactory, int nextIdAvailable) {

            var validator = servicesFactory.CreateCrudValidator(database);
            var converter = servicesFactory.CreateConverter();

            switch (typeOfCrudAction) {
                case TypeOfCrudAction.Unknown:
                    return (false, "Unknown");
                case TypeOfCrudAction.AddMember:
                    var memberRecordResult = validator.AreAllFieldsFilledIn(stringRecord);

                    if (!memberRecordResult.successful)
                        return (false, memberRecordResult.errorMessage);

                    var memberRecord = converter.ConvertRecordToMemberRecord(stringRecord, nextIdAvailable);
                    var memberExists = validator.DoesMemberExistInDatabase(false, 0, memberRecord.Name);

                    if (memberExists.exists)
                        return (false, memberExists.errorMessage);

                    database.AddMember(memberRecord);
                    break;
                case TypeOfCrudAction.AddProvider:
                    var providerRecordResult = validator.AreAllFieldsFilledIn(stringRecord);

                    if (!providerRecordResult.successful)
                        return (false, providerRecordResult.errorMessage);

                    var providerRecord = converter.ConvertRecordToProviderRecord(stringRecord, nextIdAvailable);
                    var providerExists = validator.DoesProviderExistInDatabase(false, 0, providerRecord.Name);

                    if (providerExists.exists)
                        return (false, providerExists.errorMessage);

                    database.AddProvider(providerRecord);
                    break;
                default:
                    return (false, "No such action");
            }

            return (true, "");
        }
    }
}