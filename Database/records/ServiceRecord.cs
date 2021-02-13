using System;
using System.Collections.Generic;

namespace ChocAnDatabase.records {
    public class ServiceRecord : Record {
        private String name;
        private int number;
        private Double fee;

        public ServiceRecord(Dictionary<string, object> keyValues) : base(keyValues) {
            this.Name = GetString("name");
            this.Number = GetInteger("number");
            this.Fee = GetDouble("fee");
        }

        public string Name { get => name; set => name = value; }
        public int Number { get => number; set => number = value; }
        public double Fee { get => fee; set => fee = value; }

        public override string ToString() {
            return Name + ";" + Number + ";" + fee;
        }

        public static ServiceRecord FromString(String line) {
            String[] data = line.Trim().Split(';');
            Dictionary<String, Object> dataMap = new Dictionary<string, object>();
            dataMap.Add("name", data[0]);
            dataMap.Add("number", int.Parse(data[1]));
            dataMap.Add("fee", double.Parse(data[2]));

            return new ServiceRecord(dataMap);
        }
    }
}
