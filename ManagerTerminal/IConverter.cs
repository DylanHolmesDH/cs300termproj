using ChocAnDatabase.records;

namespace ManagerTerminal
{
    public interface IConverter
    {
        MemberRecord ConvertRecordToMemberRecord(string stringRecord, int nextIdAvailable);
        ProviderRecord ConvertRecordToProviderRecord(string stringRecord, int nextIdAvailable);
    }
}