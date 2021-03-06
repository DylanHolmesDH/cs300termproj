using ReportGenerator.Models;

namespace ReportGenerator.Interfaces {
    public interface IDataValidator {
        (bool valid, string errorMessage) ValidateData(ReportData data);
    }
}
