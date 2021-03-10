using ChocAnDatabase.records;
using System;
using System.Collections.Generic;

namespace ManagerTerminal {
    public class Converter : IConverter {
        public MemberRecord ConvertRecordToMemberRecord(string stringRecord, int nextIdAvailable) {
            int zipIndex = 3;
            int addressIndex = 1;

            string[] recordArray = stringRecord.Split(',');

            if (recordArray.Length != 2)
                throw new ApplicationException("Not all fields filled in");

            var addressArray = recordArray[addressIndex].Split(' ');

            if (addressArray.Length != 4)
                throw new ApplicationException("Not all fields filled in");

            int zip = 0;
            int exponent = 5;

            foreach (var i in addressArray[zipIndex]) {
                if (i >= '0' && i <= '9')
                    zip = (i - '0') * (int)Math.Pow(10, exponent - 1) + zip;

                exponent--;
            }

            ProviderRecord providerRecord = new ProviderRecord(new Dictionary<string, object>()) {
                Name = recordArray[0],
                Address = addressArray[1],
                City = addressArray[2],
                State = addressArray[3],
                Number = nextIdAvailable,
                Zip = zip
            };

            return providerRecord;
        }

        public ProviderRecord ConvertRecordToProviderRecord(string stringRecord, int nextIdAvailable) {
            int addressIndex = 1;
            int zipIndex = 3;

            string[] recordArray = stringRecord.Split(',');

            if (recordArray.Length != 2)
                throw new ApplicationException("Not all fields filled in");

            var addressArray = recordArray[addressIndex].Split(' ');

            if (addressArray.Length != 4)
                throw new ApplicationException("Not all fields filled in");

            int zip = 0;
            int exponent = 5;

            foreach (var i in addressArray[zipIndex]) {
                if (i >= '0' && i <= '9')
                    zip = (i - '0') * (int)Math.Pow(10, exponent - 1) + zip;

                exponent--;
            }

            ProviderRecord providerRecord = new ProviderRecord(new Dictionary<string, object>()) {
                Name = recordArray[0],
                Address = addressArray[1],
                City = addressArray[2],
                State = addressArray[3],
                Number = nextIdAvailable,
                Zip = zip
            };

            return providerRecord;
        }
    }
}