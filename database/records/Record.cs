using System;
using System.Collections.Generic;

namespace ChocAnDatabase.records {
    public class Record {

        private Dictionary<String, Object> keyValues;

        public Record(Dictionary<String, Object> keyValues) {
            this.keyValues = keyValues;
        }

        public int GetInteger(String key) {
            if (!keyValues.ContainsKey(key)) {
                return int.MinValue;
            }
            return (int)keyValues[key];
        }

        public Object Get(String key) {
            if (!keyValues.ContainsKey(key)) {
                return null;
            }
            return keyValues[key];
        }

        public Double GetDouble(String key) {
            if (!keyValues.ContainsKey(key)) {
                return Double.MinValue;
            }
            return (Double)keyValues[key];
        }

        public String GetString(String key) {
            if (!keyValues.ContainsKey(key)) {
                return "";
            }
            return (String)keyValues[key];
        }

        public void Add(String key, Object value) {
            if (keyValues.ContainsKey(key)) {
                keyValues[key] = value;
                return;
            }
            keyValues.Add(key, value);
        }

    }
}
