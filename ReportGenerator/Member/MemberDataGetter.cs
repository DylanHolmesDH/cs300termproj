using ChocAnDatabase;
using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Member {
    public class MemberDataGetter : IDataGetter {
        private IDatabaseWrapper _database;

        public MemberDataGetter(IDatabaseWrapper database) {
            _database = database;
        }

        public ReportData GetData(int memberId) {
            ReportData reportData = new ReportData();

            var memberRecord = _database.FetchMember(memberId);

            reportData.MemberRecord = memberRecord;

            // Get consultation records

            var consultationRecords = _database.FetchConsultationRecordsForMember(memberId);

            if (consultationRecords != null) {
                foreach (var consultationRecord in consultationRecords) {
                    var providedService = new ProvidedService();

                    var providerRecord = _database.FetchProvider(consultationRecord.ProviderNumber);
                    var serviceRecord = _database.FetchServiceRecord(consultationRecord.ServiceNumber);

                    providedService.ServiceDate = consultationRecord.ServiceDate;
                    providedService.ProviderName = providerRecord.Name;
                    providedService.ServiceName = serviceRecord.Name;

                    reportData.ProvidedServices.Add(providedService);
                }
            }

            return reportData;
        }
    }
}
