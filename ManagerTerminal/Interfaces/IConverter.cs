using ChocAnDatabase.records;

namespace ManagerTerminal {
    public interface IConverter {
        MemberRecord ConvertRecordToMemberRecord(UserInterfaceRecord userInterfaceRecord, int nextIdAvailable);
        ProviderRecord ConvertRecordToProviderRecord(UserInterfaceRecord userInterfaceRecord, int nextIdAvailable);
    }
}