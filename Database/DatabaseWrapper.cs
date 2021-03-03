using ChocAnDatabase.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnDatabase
{
    public class DatabaseWrapper : IDatabaseWrapper
    {
        public MemberRecord FetchMember(int id)
        {
            throw new NotImplementedException();
        }

        public IList<ConsultationRecord> FetchConsultationRecordsForMember(int memberId)
        {
            throw new NotImplementedException();
        }

        public ProviderRecord FetchProvider(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceRecord FetchServiceRecord(int serviceNumber)
        {
            throw new NotImplementedException();
        }

        public IList<ConsultationRecord> FetchConsultationRecordsForProvider(int providerId)
        {
            throw new NotImplementedException();
        }
    }
}
