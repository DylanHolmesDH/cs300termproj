using ChocAnDatabase.records;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Interfaces {
    public interface IDataValidator {
        (bool valid, string errorMessage) ValidateData(ReportData data);
    }
}
