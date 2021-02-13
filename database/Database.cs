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
                String[] data = line.Trim().Split(';');
                Dictionary<String, Object> dataMap = new Dictionary<string, object>();
                switch (activeType) {
                    case "MEMBERS":
                        dataMap.Add("name", data[0]);
                        dataMap.Add("number", int.Parse(data[1]));
                        dataMap.Add("address", data[2]);
                        dataMap.Add("city", data[3]);
                        dataMap.Add("state", data[4]);
                        dataMap.Add("zip", data[5]);

                        members.Add(new Record(dataMap));

                        break;
                    case "PROVIDERS":
                        dataMap.Add("name", data[0]);
                        dataMap.Add("number", int.Parse(data[1]));
                        dataMap.Add("address", data[2]);
                        dataMap.Add("city", data[3]);
                        dataMap.Add("state", data[4]);
                        dataMap.Add("zip", data[5]);

                        providers.Add(new Record(dataMap));
                        break;
                    case "SERVICES":
                        dataMap.Add("name", data[0]);
                        dataMap.Add("number", int.Parse(data[1]));
                        dataMap.Add("fee", data[2]);

                        services.Add(new Record(dataMap));
                        break;
                    case "CONSULTATIONS":
                        dataMap.Add("current_date", DateTime.Parse(data[0]));
                        dataMap.Add("service_date", DateTime.Parse(data[1]));
                        dataMap.Add("provider_number", int.Parse(data[2]));
                        dataMap.Add("member_number", int.Parse(data[3]));
                        dataMap.Add("service_number", int.Parse(data[4]));
                        dataMap.Add("comments", data[5]);

                        consultations.Add(new Record(dataMap));
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
                outStream.WriteLine(memb.Get("name") + ";" + memb.Get("number") + ";"
                    + memb.Get("address") + ";" + memb.Get("city") + ";"
                    + memb.Get("state") + ";" + memb.Get("zip"));
            }
            outStream.WriteLine("*PROVIDERS:");

            foreach (var provider in providers) {
                outStream.WriteLine(provider.Get("name") + ";" + provider.Get("number") + ";"
                    + provider.Get("address") + ";" + provider.Get("city") + ";"
                    + provider.Get("state") + ";" + provider.Get("zip"));
            }
            outStream.WriteLine("*SERVICES:");

            foreach (var serv in services) {
                outStream.WriteLine(serv.Get("name") + ";" + serv.Get("number") + ";"
                    + serv.Get("fee"));
            }
            outStream.WriteLine("*CONSULTATIONS:");

            foreach (var con in consultations) {
                outStream.WriteLine(con.Get("current_date") + ";" + con.Get("service_date") + ";"
                    + con.Get("provider_number") + ";" + con.Get("member_number") + ";"
                    + con.Get("service_number") + ";" + con.Get("comments"));
            }

            outStream.Flush();
            outStream.Close();
        }
    }
}
