using ReportGenerator.Models;

namespace ReportGenerator.Interfaces {
    public interface IDataFormatter {
        ReportOutput FormatData(ReportData data);
    }
}
