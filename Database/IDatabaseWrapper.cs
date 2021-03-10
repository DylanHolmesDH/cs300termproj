using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ChocAnDatabase {
    public interface IDatabaseWrapper {
        MemberRecord FetchMember(int id);
        IList<ConsultationRecord> FetchConsultationRecordsForMember(int memberId, int daysBack);
        IList<ConsultationRecord> FetchConsultationRecordsForProvider(int providerId, int daysBack);
        ProviderRecord FetchProvider(int id);
        void AddMember(MemberRecord record);
        void Save();
        ServiceRecord FetchServiceRecord(int serviceNumber);
        void AddProvider(ProviderRecord record);
        List<Record> FetchProviders();
        List<Record> FetchMembers();
        MemberRecord FetchMemberByName(string name);
        ProviderRecord FetchProviderByName(string name);
    }
}
