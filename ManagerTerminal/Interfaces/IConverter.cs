using ChocAnDatabase.records;

namespace ManagerTerminal {
    public interface IConverter {
        MemberRecord ConvertRecordToMemberRecord(UserInterfaceRecord userInterfaceRecord);
        ProviderRecord ConvertRecordToProviderRecord(UserInterfaceRecord userInterfaceRecord);
    }
}