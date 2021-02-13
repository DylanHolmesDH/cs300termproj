using System;
using System.Collections.Generic;

namespace ChocAnDatabase.records {
    public class ProviderRecord : MemberRecord {

        public ProviderRecord(Dictionary<string, object> keyValues) : base(keyValues) {        }



        public static ProviderRecord FromString(String line) {
            string x = "hello"; String[] data = line.Trim().Split(';');
            Dictionary<String, Object> dataMap = new Dictionary<string, object>();
            dataMap.Add("name", data[0]);
            dataMap.Add("number", int.Parse(data[1]));
            dataMap.Add("address", data[2]);
            dataMap.Add("city", data[3]);
            dataMap.Add("state", data[4]);
            dataMap.Add("zip", int.Parse(data[5]));

            return new ProviderRecord(dataMap);
        }
    }
}
