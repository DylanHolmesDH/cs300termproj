﻿using ChocAnDatabase;
using ChocAnDatabase.records;
namespace ManagerTerminal {
    public class CrudServices : ICrudServices {
        public TypeOfCrudAction DetermineTypeOfCrudAction(int optionNumber) {
            switch (optionNumber) {
                case 5:
                    return TypeOfCrudAction.AddMember;
                case 6:
                    return TypeOfCrudAction.AddProvider;
                case 7:
                    return TypeOfCrudAction.UpdateMember;
                case 8:
                    return TypeOfCrudAction.UpdateProvider;
                case 9:
                    return TypeOfCrudAction.RemoveMember;
                case 10:
                    return TypeOfCrudAction.RemoveProvider;
                default:
                    return TypeOfCrudAction.Unknown;
            }
        }

        public (bool successful, string errorMessage) DoCrudAction(IDatabaseWrapper database, TypeOfCrudAction typeOfCrudAction, UserInterfaceRecord userInterfaceRecord, IServicesFactory servicesFactory) {

            var validator = servicesFactory.CreateCrudValidator(database);
            var converter = servicesFactory.CreateConverter();

            MemberRecord memberRecord;
            ProviderRecord providerRecord;
            (bool successful, string errorMessage) allFieldsFilled;
            (bool exists, string errorMessage) recordExists;

            switch (typeOfCrudAction) {
                case TypeOfCrudAction.Unknown:
                    return (false, "Unknown");
                case TypeOfCrudAction.AddMember:
                    memberRecord = converter.ConvertRecordToMemberRecord(userInterfaceRecord);
                   
                    allFieldsFilled = validator.AreAllFieldsFilledIn(memberRecord, false);

                    if (!allFieldsFilled.successful)
                        return allFieldsFilled;

                    recordExists = validator.DoesMemberExistInDatabase(false, 0, memberRecord.Name);

                    if (recordExists.exists)
                        return (false, recordExists.errorMessage);

                    database.AddMember(memberRecord);
                    database.Save();
                    break;
                case TypeOfCrudAction.AddProvider:
                    providerRecord = converter.ConvertRecordToProviderRecord(userInterfaceRecord);

                    allFieldsFilled = validator.AreAllFieldsFilledIn(providerRecord, false);

                    if (!allFieldsFilled.successful)
                        return allFieldsFilled;

                    recordExists = validator.DoesProviderExistInDatabase(false, 0, providerRecord.Name);

                    if (recordExists.exists)
                        return (false, recordExists.errorMessage);

                    database.AddProvider(providerRecord);
                    database.Save();
                    break;
                case TypeOfCrudAction.UpdateMember:
                    memberRecord = converter.ConvertRecordToMemberRecord(userInterfaceRecord);

                    allFieldsFilled = validator.AreAllFieldsFilledIn(memberRecord, true);

                    if (!allFieldsFilled.successful)
                        return allFieldsFilled;

                    recordExists = validator.DoesMemberExistInDatabase(true, memberRecord.Number);

                    if (!recordExists.exists)
                        return (false, recordExists.errorMessage);

                    database.UpdateMember(memberRecord);
                    database.Save();
                    break;
                case TypeOfCrudAction.UpdateProvider:
                    providerRecord = converter.ConvertRecordToProviderRecord(userInterfaceRecord);

                    allFieldsFilled = validator.AreAllFieldsFilledIn(providerRecord, true);

                    if (!allFieldsFilled.successful)
                        return allFieldsFilled;

                    recordExists = validator.DoesProviderExistInDatabase(true, providerRecord.Number);

                    if (!recordExists.exists)
                        return (false, recordExists.errorMessage);

                    database.UpdateProvider(providerRecord);
                    database.Save();
                    break;
                case TypeOfCrudAction.RemoveMember:
                    allFieldsFilled = validator.RemovalIdValid(userInterfaceRecord.Number);

                    if (!allFieldsFilled.successful)
                        return allFieldsFilled;

                    recordExists = validator.DoesMemberExistInDatabase(true, userInterfaceRecord.Number);

                    if (!recordExists.exists)
                        return (false, recordExists.errorMessage);

                    database.RemoveMember(userInterfaceRecord.Number);
                    database.Save();
                    break;
                case TypeOfCrudAction.RemoveProvider:
                    allFieldsFilled = validator.RemovalIdValid(userInterfaceRecord.Number);

                    if (!allFieldsFilled.successful)
                        return allFieldsFilled;

                    recordExists = validator.DoesProviderExistInDatabase(true, userInterfaceRecord.Number);

                    if (!recordExists.exists)
                        return (false, recordExists.errorMessage);

                    database.RemoveProvider(userInterfaceRecord.Number);
                    database.Save();
                    break;
                default:
                    return (false, "No such action");
            }

            return (true, "");
        }
    }
}