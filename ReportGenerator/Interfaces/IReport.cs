namespace ReportGenerator.Interfaces {
    public interface IReport {
        (bool created, string errorMessage) Generate(int id);
    }
}