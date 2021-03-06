using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ChocAnDatabase {
    public interface IDatabaseWrapper {
        MemberRecord FetchMember(int id);
        IList<ConsultationRecord> FetchConsultationRecordsForMember(int memberId);
        IList<ConsultationRecord> FetchConsultationRecordsForProvider(int providerId);
        ProviderRecord FetchProvider(int id);
        ServiceRecord FetchServiceRecord(int serviceNumber);
        List<Record> FetchProviders();
        List<Record> FetchMembers();
    }
}
