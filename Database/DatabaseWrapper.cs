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

        private void EnsureDatabaseIsAvailable() {
            if (_database == null)
                _database = new Database();
        }

        public void AddMember(MemberRecord record) {
            EnsureDatabaseIsAvailable();

            _database.InsertMember(record);
        }

        public void Save() {
            EnsureDatabaseIsAvailable();

            _database.Save();
        }

        public void AddProvider(ProviderRecord record) {
            EnsureDatabaseIsAvailable();

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

        public void UpdateProvider(ProviderRecord providerRecord) {
            throw new NotImplementedException();
        }

        public void UpdateMember(MemberRecord memberRecord) {
            EnsureDatabaseIsAvailable();

           
        }

        public void RemoveMember(int number) {
            throw new NotImplementedException();
        }

        public void RemoveProvider(int number) {
            throw new NotImplementedException();
        }
    }
}
