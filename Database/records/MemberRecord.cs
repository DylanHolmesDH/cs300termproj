using System;
using System.Collections.Generic;

namespace ChocAnDatabase.records {
    public class MemberRecord : Record {
        private String name, address, city, state;
        private Boolean validated;
        private int number, zip;

        public MemberRecord(Dictionary<string, object> keyValues) : base(keyValues) {
            this.Name = GetString("name");
            this.Address = GetString("address");
            this.City = GetString("city");
            this.State = GetString("state");
            this.Zip = GetInteger("zip");
            this.Number = GetInteger("number");
            this.Validated = GetBoolean("validated");
        }

        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public int Number { get => number; set => number = value; }
        public int Zip { get => zip; set => zip = value; }
        public Boolean Validated { get => validated; set => validated = value; }


        public override string ToString() {
            return name + ";" + number + ";" + address + ";" + city + ";" + state + ";" + zip + ";" + validated;
        }

        public static MemberRecord FromString(String line) {
            String[] data = line.Trim().Split(';');
            Dictionary<String, Object> dataMap = new Dictionary<string, object>();
            dataMap.Add("name", data[0]);
            dataMap.Add("number", int.Parse(data[1]));
            dataMap.Add("address", data[2]);
            dataMap.Add("city", data[3]);
            dataMap.Add("state", data[4]);
            dataMap.Add("zip", int.Parse(data[5]));
            dataMap.Add("validated", Boolean.Parse(data[6]));


            return new MemberRecord(dataMap);
        }
    }
}
