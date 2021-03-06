﻿using ChocAnDatabase;
using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System.Collections.Generic;

namespace ReportGenerator.Summary {
    public class SummaryDataGetter : IDataGetter {
        private IDatabaseWrapper _databaseWrapper;

        public SummaryDataGetter(IDatabaseWrapper databaseWrapper) {
            _databaseWrapper = databaseWrapper;
        }
        public ReportData GetData(int id = 0, int daysBack = 7) {
            List<Record> providerRecords = _databaseWrapper.FetchProviders();

            ReportData reportData = new ReportData();

            if (providerRecords != null) {
                foreach (var providerRecord in providerRecords) {
                    var providerNumber = (int)providerRecord.Get("number");
                    double totalFeeForProvider = 0;

                    IList<ConsultationRecord> consultationRecords = _databaseWrapper.FetchConsultationRecordsForProvider(providerNumber, daysBack);

                    if (consultationRecords != null) {
                        foreach (var consultationRecord in consultationRecords) {
                            var serviceRecord = _databaseWrapper.FetchServiceRecord(consultationRecord.ServiceNumber);

                            totalFeeForProvider = totalFeeForProvider + serviceRecord.Fee;
                        }

                        if (totalFeeForProvider != 0) {
                            SummaryDataInfo summaryDataInfo = new SummaryDataInfo {
                                ProviderName = (string)providerRecord.Get("name"),
                                TotalFee = totalFeeForProvider,
                                TotalNumberOfConsultations = consultationRecords.Count
                            };

                            reportData.SummaryDataInfo.Add(summaryDataInfo);
                        }
                    }
                }
            }

            return reportData;
        }
    }
}
