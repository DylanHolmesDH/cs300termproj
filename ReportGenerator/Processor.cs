﻿using ChocAnDatabase;
using ReportGenerator.Factory;
using ReportGenerator.Interfaces;

namespace ReportGenerator {
    public class Processor : IProcessor {
        public (bool created, string errorMessage) GenerateReport(TypeOfReport typeOfReport, IDatabaseWrapper databaseWrapper, IReportFactory factory, int id, int daysBack = 7) {
            if (databaseWrapper is null)
                throw new System.ArgumentNullException(nameof(databaseWrapper));

            if (factory is null)
                throw new System.ArgumentNullException(nameof(factory));

            var report = factory.CreateReport(typeOfReport, databaseWrapper);

            return report.Generate(id, daysBack);
        }
    }
}