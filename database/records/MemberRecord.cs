using System;
using System.Collections.Generic;

namespace ChocAnDatabase.records {
    public class MemberRecord : Record {
        private String name, address, city, state;
        private int number, zip;
        public MemberRecord(Dictionary<string, object> keyValues) : base(keyValues) {

        }
    }
}
