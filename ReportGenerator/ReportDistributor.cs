using ReportGenerator.Interfaces;
using ReportGenerator.Models;
using System.IO;

namespace ReportGenerator {
    public class ReportDistributor : IReportDistributor {
        public (bool created, string errorMessage) DistributeReport(ReportOutput reportOutput) {
            if (reportOutput is null)
                throw new System.ArgumentNullException(nameof(reportOutput));

            if (reportOutput.OutputLines.Count <= 0)
                return (false, "No output lines in report");

            if (reportOutput.FileName.Length == 0)
                return (false, "No filename for report");

            // Open up a file with the name from the reportOutput
            string path = "./Reports/" + reportOutput.FileName;

            StreamWriter streamWriter = new StreamWriter(File.Create(path));

            // Write all lines to the file
            foreach (var line in reportOutput.OutputLines) {
                streamWriter.WriteLine(line);
            }

            // Close the file
            streamWriter.Close();

            return (true, "");
        }
    }
}
