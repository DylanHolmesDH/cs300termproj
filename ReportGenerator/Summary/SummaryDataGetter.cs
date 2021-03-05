using ChocAnDatabase;
using ChocAnDatabase.records;
using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Summary {
    public class SummaryDataGetter : IDataGetter {
        private IDatabaseWrapper _databaseWrapper;

        public SummaryDataGetter(IDatabaseWrapper databaseWrapper) {
            _databaseWrapper = databaseWrapper;
        }
        public ReportData GetData(int id = 0) {
            List<Record> providerRecords = _databaseWrapper.FetchProviders();
            double totalFeeForProvider = 0;

            ReportData reportData = new ReportData();

            if (providerRecords != null) {
                foreach (var providerRecord in providerRecords) {
                    var providerNumber = (int)providerRecord.Get("number");

                    IList<ConsultationRecord> consultationRecords = _databaseWrapper.FetchConsultationRecordsForProvider(providerNumber);

                    if (consultationRecords != null) {
                        foreach (var consultationRecord in consultationRecords) {
                            var serviceRecord = _databaseWrapper.FetchServiceRecord(consultationRecord.ServiceNumber);

                            totalFeeForProvider = totalFeeForProvider + serviceRecord.Fee;
                        }

                        SummaryDataInfo summaryDataInfo = new SummaryDataInfo {
                            ProviderName = (string)providerRecord.Get("name"),
                            TotalFee = totalFeeForProvider,
                            TotalNumberOfConsultations = consultationRecords.Count
                        };

                        reportData.SummaryDataInfo.Add(summaryDataInfo);
                    }
                }
            }

            return reportData;
        }
    }
}
