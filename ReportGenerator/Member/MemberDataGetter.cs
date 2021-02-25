using ChocAnDatabase;
using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Member
{
    public class MemberDataGetter : IDataGetter
    {
        private Database _database;

        public MemberDataGetter(Database database)
        {
            _database = database;
        }

        public ReportData GetData(int id)
        {
            var memberRecord = _database.FetchMember(id);

            ReportData reportData = new ReportData();

            //Get provider and service records
            // Convert into ReportData

            return reportData;
        }
    }
}
