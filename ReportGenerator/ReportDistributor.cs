using ReportGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReportGenerator
{
    public class ReportDistributor : IReportDistributor
    {
        public (bool created, string errorMessage) DistributeReport(ReportOutput reportOutput)
        {
            if (reportOutput is null)
                throw new System.ArgumentNullException(nameof(reportOutput));

            if (reportOutput.OutputLines.Count <= 0)
                return (false, "No output lines");

            if (reportOutput.FileName.Length == 0)
                return (false, "No filename for report");

            // Open up a file with the name from the reportOutput
            string path = @"c:\users\ryzen\Desktop\" + reportOutput.FileName;

            StreamWriter streamWriter = new StreamWriter(path);

            streamWriter.WriteLine(reportOutput.FileName);
            streamWriter.WriteLine("***************");

            // Write all lines to the file
            foreach (var line in reportOutput.OutputLines)
            {
                streamWriter.WriteLine("\t" + line);
            }

            // Close the file
            streamWriter.Close();

            return (true, "Report generated at " + path);
        }
    }
}
