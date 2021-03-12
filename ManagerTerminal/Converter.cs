using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ManagerTerminal {
    public class Converter : IConverter {
        public MemberRecord ConvertRecordToMemberRecord(UserInterfaceRecord userInterfaceRecord) {

            var dictionary = new Dictionary<string, object>();
            dictionary.Add("name", userInterfaceRecord.Name);
            dictionary.Add("number", userInterfaceRecord.Number);
            dictionary.Add("address", userInterfaceRecord.Address);
            dictionary.Add("city", userInterfaceRecord.City);
            dictionary.Add("state", userInterfaceRecord.State);
            dictionary.Add("zip", userInterfaceRecord.Zip);
            MemberRecord memberRecord = new MemberRecord(dictionary);

            return memberRecord;
        }

        public ProviderRecord ConvertRecordToProviderRecord(UserInterfaceRecord userInterfaceRecord) {

            var dictionary = new Dictionary<string, object>();
            dictionary.Add("name", userInterfaceRecord.Name);
            dictionary.Add("number", userInterfaceRecord.Number);
            dictionary.Add("address", userInterfaceRecord.Address);
            dictionary.Add("city", userInterfaceRecord.City);
            dictionary.Add("state", userInterfaceRecord.State);
            dictionary.Add("zip", userInterfaceRecord.Zip);
            ProviderRecord providerRecord = new ProviderRecord(dictionary);

            return providerRecord;
        }
    }
}