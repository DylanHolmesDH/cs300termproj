using ChocAnDatabase.records;
using System;
using System.Collections.Generic;

namespace ChocAnDatabase {
    public class DatabaseWrapper : IDatabaseWrapper {
        Database _database;

        public MemberRecord FetchMember(int id) {
            EnsureDatabaseIsAvailable();

            return _database.FetchMember(id);
        }

        public IList<ConsultationRecord> FetchConsultationRecordsForMember(int memberId) {
            EnsureDatabaseIsAvailable();

            return _database.FetchConsultationsByMember(memberId);
        }

        public ProviderRecord FetchProvider(int id) {
            EnsureDatabaseIsAvailable();

            return _database.FetchProvider(id);
        }

        public ServiceRecord FetchServiceRecord(int serviceNumber) {
            EnsureDatabaseIsAvailable();

            return _database.FetchServiceTemp(serviceNumber); // Remove when fixed
            //return _database.FetchService(serviceNumber);
        }

        public IList<ConsultationRecord> FetchConsultationRecordsForProvider(int providerId) {
            throw new NotImplementedException();
            //EnsureDatabaseIsAvailable();

            //return _database.FetchConsultationByProvider(providerId);
        }

        public List<Record> FetchMembers() {
            EnsureDatabaseIsAvailable();

            return _database.FetchMembers();
        }

        public List<Record> FetchProviders() {
            EnsureDatabaseIsAvailable();

            return _database.FetchProviders();
        }

        private void EnsureDatabaseIsAvailable() {
            if (_database == null)
                _database = new Database();
        }
    }
}
