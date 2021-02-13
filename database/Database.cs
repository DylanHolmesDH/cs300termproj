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
        private String rootPath;

        public Database() {
            rootPath = "./data/";

            Load();
        }

        public List<Record> FetchServices() {
            return this.services;
        }

        public Record FetchMember(int id) {
            foreach (var record in members) {
                if (record.GetInteger("number") == id) {
                    return record;
                }
            }
            return null;
        }

        public void InsertConsultation(Record record) {
            this.consultations.Add(record);
        }

        private void Load() {

            StreamReader inStream = new StreamReader(File.Open(rootPath + "database.db", FileMode.OpenOrCreate, FileAccess.Read));

            
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
            StreamWriter outStream = new StreamWriter(File.Open(rootPath + "database.db", FileMode.Truncate, FileAccess.Write));
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
