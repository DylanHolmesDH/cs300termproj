﻿using ChocAnDatabase;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;

namespace ReportGenerator.Member {
    public class MemberDataGetter : IDataGetter {
        private IDatabaseWrapper _database;

        public MemberDataGetter(IDatabaseWrapper database) {
            _database = database;
        }

        public ReportData GetData(int memberId, int daysBack = 7) {
            ReportData reportData = new ReportData();

            var memberRecord = _database.FetchMember(memberId);

            reportData.MemberRecord = memberRecord;

            // Get consultation records

            var consultationRecords = _database.FetchConsultationRecordsForMember(memberId, daysBack);

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
