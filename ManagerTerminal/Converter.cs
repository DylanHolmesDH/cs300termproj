using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ManagerTerminal {
    public class Converter : IConverter {
        public MemberRecord ConvertRecordToMemberRecord(UserInterfaceRecord userInterfaceRecord) {

            MemberRecord memberRecord = new MemberRecord(new Dictionary<string, object>()) {
                Name = userInterfaceRecord.Name,
                Number = userInterfaceRecord.Number,
                Address = userInterfaceRecord.Address,
                City = userInterfaceRecord.City,
                State = userInterfaceRecord.State,
                Zip = userInterfaceRecord.Zip
            };

            return memberRecord;
        }

        public ProviderRecord ConvertRecordToProviderRecord(UserInterfaceRecord userInterfaceRecord) {

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