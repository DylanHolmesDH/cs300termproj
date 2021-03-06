using ReportGenerator.Models;

namespace ReportGenerator.Interfaces {
    public interface IDataGetter {
        ReportData GetData(int id);
    }
}
