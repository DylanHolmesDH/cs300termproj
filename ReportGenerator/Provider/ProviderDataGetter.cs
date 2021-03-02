using ChocAnDatabase;
using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Provider {
    public class ProviderDataGetter : IDataGetter {
        private IDatabaseWrapper _database;

        public ProviderDataGetter(IDatabaseWrapper database) {
            _database = database;
        }

        public ReportData GetData(int id) {
            throw new NotImplementedException();
        }
    }
}
