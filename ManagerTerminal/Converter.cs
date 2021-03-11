using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ManagerTerminal {
    public class Converter : IConverter {
        public MemberRecord ConvertRecordToMemberRecord(UserInterfaceRecord userInterfaceRecord, int nextIdAvailable) {

            MemberRecord providerRecord = new MemberRecord(new Dictionary<string, object>()) {
                Name = userInterfaceRecord.Name,
                Address = userInterfaceRecord.Address,
                City = userInterfaceRecord.City,
                State = userInterfaceRecord.State,
                Number = userInterfaceRecord.Number,
                Zip = userInterfaceRecord.Zip
            };

            return providerRecord;
        }

        public ProviderRecord ConvertRecordToProviderRecord(UserInterfaceRecord userInterfaceRecord, int nextIdAvailable) {

            ProviderRecord providerRecord = new ProviderRecord(new Dictionary<string, object>()) {
                Name = userInterfaceRecord.Name,
                Address = userInterfaceRecord.Address,
                City = userInterfaceRecord.City,
                State = userInterfaceRecord.State,
                Number = userInterfaceRecord.Number,
                Zip = userInterfaceRecord.Zip
            };

            return providerRecord;
        }
    }
}