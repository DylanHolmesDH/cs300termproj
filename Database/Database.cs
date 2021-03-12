using System;
using System.IO;
using System.Collections.Generic;
using ChocAnDatabase.records;

namespace ChocAnDatabase {
    public class Database {
        private List<Record> members = new List<Record>();
        private List<Record> providers = new List<Record>();
        private List<Record> services = new List<Record>();
        private List<Record> consultations = new List<Record>();
        private String pathToFile;

        public Database(String pathToFile) {
            this.pathToFile = pathToFile;

            Load();
        }

        public List<Record> FetchServices() {
            return this.services;
        }

        public List<Record> FetchMembers() {
            return this.members;
        }

        public List<Record> FetchProviders() {
            return this.providers;
        }

        public List<Record> FetchConsultations() {
            return this.consultations;
        }

        public Record FetchRecordByNumber(int id, List<Record> records) {
            foreach (var record in records) {
                if (((Record) record).GetInteger("number") == id) {
                    return record;
                }
            }
            return null;
        }

        public MemberRecord FetchMember(int id) {
            return (MemberRecord) FetchRecordByNumber(id, members);
        }

        public ServiceRecord FetchService(int id) {
            return (ServiceRecord) FetchRecordByNumber(id, services);
        }

        public ProviderRecord FetchProvider(int id) {
            return (ProviderRecord) FetchRecordByNumber(id, providers);
        }

        public ProviderRecord FetchProviderByName(String name) {
            foreach (Record record in providers) {
                if (record.GetString("name").Equals(name)) {
                    return (ProviderRecord)record;
                }
            }
            return null;
        }

        public MemberRecord FetchMemberByName(String name) {
            foreach (Record record in members) {
                if (record.GetString("name").Equals(name)) {
                    return (MemberRecord)record;
                }
            }
            return null;
        }

        public ConsultationRecord FetchConsultation(int memberID, int serviceID, int providerID, DateTime recDate, DateTime serviceDate) {
            foreach (var record in consultations) {
                ConsultationRecord rec = (ConsultationRecord)record;
                if (
                    rec.RecordDate == recDate 
                    && rec.ServiceDate == serviceDate
                    && rec.MemberNumber == memberID
                    && rec.ServiceNumber == serviceID 
                    && rec.ProviderNumber == providerID
                    ) {
                    return rec;
                }
            }
            return null;
        }

        /// <summary>
        /// Fetches Consulation Records by the specified Provider ID, filtered by an amount of days back.
        /// </summary>
        /// <param name="providerID">The ProviderID to filter Records by.</param>
        /// <param name="daysBack">The amount of days back to fetch Records, I.E. 7 days back</param>
        /// <returns>A list of Consulation Records belonging to the specified providerID and filtered by daysBack.</returns>
        public List<ConsultationRecord> FetchConsultationsByProvider(int providerID, int daysBack) {
            var records = new List<ConsultationRecord>();
            DateTime filter = DateTime.Now.AddDays(-daysBack);

            foreach (var rec in consultations) {
                ConsultationRecord record = (ConsultationRecord)rec;

                if (record.ProviderNumber == providerID && DateTime.Compare(record.RecordDate, filter) >= 0) {
                    records.Add(record);
                }
            }

            return records;
        }
        /// <summary>
        /// Fetches Consulation Records by the specified memberID, filtered by an amount of days back.
        /// </summary>
        /// <param name="memberID">The MemberID to filter Records by.</param>
        /// <param name="daysBack">The amount of days back to fetch Records, I.E. 7 days back</param>
        /// <returns>A list of Consulation Records belonging to the specified memberID and filtered by daysBack.</returns> 
        public List<ConsultationRecord> FetchConsultationsByMember(int memberID, int daysBack) {
            var records = new List<ConsultationRecord>();
            DateTime filter = DateTime.Now.AddDays(-daysBack);
            foreach (var rec in consultations) {
                ConsultationRecord record = (ConsultationRecord)rec;

                if (record.MemberNumber == memberID && DateTime.Compare(record.RecordDate, filter) >= 0) {
                    records.Add(record);
                }
            }

            return records;
        }

        public int GetNexAvailableMemberNumber() {
            var counter = 0; // Runs through all the number options
            while (true) {
                counter++;

                bool found = false;
                foreach (Record record in members) { // Checks to see if the number in "count" is currently taken
                    if (record.GetInteger("number").Equals(counter)) {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    break;
            }

            return counter;
        }

        public int GetNexAvailableProviderNumber() {
            var counter = 0; // Runs through all the number options
            while (true) {
                counter++;

                bool found = false;
                foreach (Record record in providers) { // Checks to see if the number in "count" is currently taken
                    if (record.GetInteger("number").Equals(counter)) {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    return counter;
            }
        }

        public void InsertMember(MemberRecord record) {
            this.members.Add(record);
        }

        public void InsertService(ServiceRecord record) {
            this.services.Add(record);
        }

        public void InsertProvider(ProviderRecord record) {
            this.providers.Add(record);
        }

        public void InsertConsultation(ConsultationRecord record) {
            this.consultations.Add(record);
        }

        public void UpdateMember(MemberRecord newRecord) {
            var oldRecord = FetchMember(newRecord.Number);

            this.members.Remove(oldRecord);
            InsertMember(newRecord);
        }

        public void UpdateProvider(ProviderRecord newRecord) {
            var oldRecord = FetchProvider(newRecord.Number);

            this.providers.Remove(oldRecord);
            InsertProvider(newRecord);
        }

        public void RemoveMember(int id) {
            var record = FetchMember(id);

            this.members.Remove(record);
        }

        public void RemoveProvider(int id) {
            var record = FetchProvider(id);

            this.providers.Remove(record);
        }


        private void Load() {

            StreamReader inStream = new StreamReader(File.Open(pathToFile, FileMode.OpenOrCreate, FileAccess.Read));

            
            String activeType = "";
            while (!inStream.EndOfStream) {
                String line = inStream.ReadLine();
                if (line.Equals("")) {
                    continue;
                }
                if (line.StartsWith("*")) {
                    activeType = line.Substring(1, line.IndexOf(':') - 1);
                    continue;
                }
                switch (activeType) {
                    case "MEMBERS":
                        members.Add(MemberRecord.FromString(line));
                        break;
                    case "PROVIDERS":
                           providers.Add(ProviderRecord.FromString(line));
                        break;
                    case "SERVICES":
                        services.Add(ServiceRecord.FromString(line));
                        break;
                    case "CONSULTATIONS":
                        consultations.Add(ConsultationRecord.FromString(line));
                        break;
                    default:
                        break;
                }
            }
            inStream.Close();
        }


        public void Save() {
            StreamWriter outStream = new StreamWriter(File.Open(pathToFile, FileMode.Truncate, FileAccess.Write));
            outStream.WriteLine("*MEMBERS:");
            foreach (var memb in members) {
                outStream.WriteLine(memb.ToString());
            }
            outStream.WriteLine("*PROVIDERS:");

            foreach (var provider in providers) {
                outStream.WriteLine(provider.ToString());
            }
            outStream.WriteLine("*SERVICES:");

            foreach (var serv in services) {
                outStream.WriteLine(serv.ToString());
            }
            outStream.WriteLine("*CONSULTATIONS:");

            foreach (var con in consultations) {
                outStream.WriteLine(con.ToString());
            }

            outStream.Flush();
            outStream.Close();
        }
    }
}
