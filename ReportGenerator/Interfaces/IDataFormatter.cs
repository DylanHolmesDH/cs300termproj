﻿using ChocAnDatabase.records;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Interfaces
{
    public interface IDataFormatter
    {
        ReportOutput FormatData(ReportData data);
    }
}
