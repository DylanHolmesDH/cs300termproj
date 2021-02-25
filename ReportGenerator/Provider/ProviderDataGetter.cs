using ChocAnDatabase;
using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Provider
{
    public class ProviderDataGetter : IDataGetter
    {
        private Database _database;

        public ProviderDataGetter(Database database)
        {
            _database = database;
        }

        public ReportData GetData(int id)
        {
            throw new NotImplementedException();
        }
    }
}
