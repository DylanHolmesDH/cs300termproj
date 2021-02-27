using ChocAnDatabase.records;
using ReportGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Interfaces
{
    public interface IDataGetter
    {
        ReportData GetData(int id);
    }
}
