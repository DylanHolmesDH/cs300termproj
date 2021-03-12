using ChocAnDatabase.records;
using System.Collections.Generic;

namespace ChocAnDatabase {
    public class DatabaseWrapper : IDatabaseWrapper {
        Database _database;

        private void EnsureDatabaseIsAvailable() {
            if (_database == null)
                _database = new Database("./database.db");
        }

        public MemberRecord FetchMember(int id) {
            EnsureDatabaseIsAvailable();

            return _database.FetchMember(id);
        }

        public IList<ConsultationRecord> FetchConsultationRecordsForMember(int memberId, int daysBack) {
            EnsureDatabaseIsAvailable();

            return _database.FetchConsultationsByMember(memberId, daysBack);
        }

        public ProviderRecord FetchProvider(int id) {
            EnsureDatabaseIsAvailable();

            return _database.FetchProvider(id);
        }

        public ServiceRecord FetchServiceRecord(int serviceNumber) {
            EnsureDatabaseIsAvailable();

            return _database.FetchService(serviceNumber);
        }

        public IList<ConsultationRecord> FetchConsultationRecordsForProvider(int providerId, int daysBack) {
            EnsureDatabaseIsAvailable();

            return _database.FetchConsultationsByProvider(providerId, daysBack);
        }

        public List<Record> FetchMembers() {
            EnsureDatabaseIsAvailable();

            return _database.FetchMembers();
        }

        public List<Record> FetchProviders() {
            EnsureDatabaseIsAvailable();

            return _database.FetchProviders();
        }

        public void AddMember(MemberRecord record) {
            EnsureDatabaseIsAvailable();
            var number = _database.GetNexAvailableMemberNumber();

            record.Number = number;

            _database.InsertMember(record);
        }

        public void AddProvider(ProviderRecord record) {
            EnsureDatabaseIsAvailable();

            var number = _database.GetNexAvailableProviderNumber();

            record.Number = number;

            _database.InsertProvider(record);
        }

        public MemberRecord FetchMemberByName(string name) {
            EnsureDatabaseIsAvailable();

            return _database.FetchMemberByName(name);
        }

        public ProviderRecord FetchProviderByName(string name) {
            EnsureDatabaseIsAvailable();

            return _database.FetchProviderByName(name);
        }

        public void UpdateMember(MemberRecord memberRecord) {
            EnsureDatabaseIsAvailable();

            _database.UpdateMember(memberRecord);
        }

        public void UpdateProvider(ProviderRecord providerRecord) {
            EnsureDatabaseIsAvailable();

            _database.UpdateProvider(providerRecord);
        }

        public void RemoveMember(int id) {
            EnsureDatabaseIsAvailable();

            _database.RemoveMember(id);
        }

        public void RemoveProvider(int id) {
            EnsureDatabaseIsAvailable();

            _database.RemoveProvider(id);
        }

        public void Save() {
            EnsureDatabaseIsAvailable();

            _database.Save();
        }
    }
}
