using ReportGenerator.Interfaces;
using ReportGenerator.Models;

namespace ReportGenerator {
    public class Report : IReport {
        private readonly IDataGetter _dataGetter;
        private readonly IDataValidator _dataValidator;
        private readonly IDataFormatter _dataFormatter;
        private readonly IReportDistributor _reportDistributor;

        public Report(
            IDataGetter dataGetter,
            IDataValidator dataValidator,
            IDataFormatter dataFormatter,
            IReportDistributor reportDistributor
        ) {
            _dataGetter = dataGetter;
            _dataValidator = dataValidator;
            _dataFormatter = dataFormatter;
            _reportDistributor = reportDistributor;
        }

        public IDataGetter DataGetter {
            get { return _dataGetter; }
        }
        public IDataValidator DataValidator {
            get { return _dataValidator; }
        }

        public IDataFormatter DataFormatter {
            get { return _dataFormatter; }
        }

        public (bool created, string errorMessage) Generate(int id) {
            var data = _dataGetter.GetData(id);

            (bool valid, string errorMessage) result = _dataValidator.ValidateData(data);

            if (!result.valid)
                return result;

            ReportOutput reportOutput = _dataFormatter.FormatData(data);

            return _reportDistributor.DistributeReport(reportOutput);
        }
    }
}