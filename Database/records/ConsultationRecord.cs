using System;
using System.Collections.Generic;

namespace ChocAnDatabase.records {
    public class ConsultationRecord : Record {
        private DateTime recordDate, serviceDate;
        private int memberNumber, serviceNumber, providerNumber;
        private String comments;

        public ConsultationRecord(Dictionary<string, object> keyValues) : base(keyValues) {
            this.recordDate = (DateTime) Get("record_date");
            this.serviceDate = (DateTime) Get("service_date");

            this.memberNumber = GetInteger("member_number");
            this.serviceNumber = GetInteger("service_number");
            this.providerNumber = GetInteger("provider_number");

            this.comments = GetString("comments");
        }

        public DateTime RecordDate { get => recordDate; set => recordDate = value; }
        public DateTime ServiceDate { get => serviceDate; set => serviceDate = value; }
        public int MemberNumber { get => memberNumber; set => memberNumber = value; }
        public int ServiceNumber { get => serviceNumber; set => serviceNumber = value; }
        public int ProviderNumber { get => providerNumber; set => providerNumber = value; }
        public string Comments { get => comments; set => comments = value; }


        public override string ToString() {
            return recordDate + ";" + serviceDate + ";" + providerNumber + ";" + memberNumber + ";" + serviceNumber + ";" + comments;
        }

        public static ConsultationRecord FromString(String line) {
            String[] data = line.Trim().Split(';');
            Dictionary<String, Object> dataMap = new Dictionary<string, object>();

            dataMap.Add("record_date", DateTime.Parse(data[0]));
            dataMap.Add("service_date", DateTime.Parse(data[1]));
            dataMap.Add("provider_number", int.Parse(data[2]));
            dataMap.Add("member_number", int.Parse(data[3]));
            dataMap.Add("service_number", int.Parse(data[4]));
            dataMap.Add("comments", data[5]);

            return new ConsultationRecord(dataMap);
        }
    }
}
