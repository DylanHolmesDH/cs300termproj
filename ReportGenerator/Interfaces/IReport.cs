namespace ReportGenerator.Interfaces {
    public interface IReport {
        IDataGetter DataGetter { get; }
        IDataValidator DataValidator { get; }
        IDataFormatter DataFormatter { get; }

        (bool created, string errorMessage) Generate(int id, int daysBack);
    }
}