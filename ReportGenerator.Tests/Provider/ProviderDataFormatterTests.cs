using ChocAnDatabase.records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportGenerator.Models;
using ReportGenerator.Provider;
using System;
using System.Collections.Generic;

namespace ReportGenerator.Tests.Provider {
    [TestClass]
    public class ProviderDataFormatterTests {
        private ProviderDataFormatter _providerDataFormatter;

        [TestInitialize]
        public void Setup() {
            _providerDataFormatter = new ProviderDataFormatter();
        }

        [TestMethod]
        public void ReportDataNull() {
            Assert.ThrowsException<ApplicationException>(() => _providerDataFormatter.FormatData(null));
        }

        [TestMethod]
        public void ReportDataNoProvidedServices() {
            ReportData reportData = new ReportData {
                MemberRecord = new MemberRecord(new Dictionary<string, object>()),
                ProvidedServices = new List<ProvidedService>()
            };
        }
    }
}
