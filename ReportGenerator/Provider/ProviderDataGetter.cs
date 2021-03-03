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

        public ReportData GetData(int providerId) {
            ReportData reportData = new ReportData();

            var providerRecord = _database.FetchProvider(providerId);

            reportData.ProviderRecord = providerRecord;

            // Get consultation records

            var consultationRecords = _database.FetchConsultationRecordsForProvider(providerId);

            if (consultationRecords != null) {
                foreach (var consultationRecord in consultationRecords) {
                    var providedService = new ProvidedService();

                    var memberRecord = _database.FetchMember(consultationRecord.MemberNumber);
                    var serviceRecord = _database.FetchServiceRecord(consultationRecord.ServiceNumber);

                    providedService.ServiceDate = consultationRecord.ServiceDate;
                    providedService.DateTimeReceived = consultationRecord.CurrentDate;
                    providedService.MemberName = memberRecord.Name;
                    providedService.MemberId = memberRecord.Number;
                    providedService.ServiceId = serviceRecord.Number;
                    providedService.Fee = serviceRecord.Fee;

                    reportData.ProvidedServices.Add(providedService);
                }
            }

            return reportData;
        }
    }
}
