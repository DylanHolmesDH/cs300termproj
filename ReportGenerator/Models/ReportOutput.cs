﻿using System.Collections.Generic;

namespace ReportGenerator.Models {
    public class ReportOutput {
        public ReportOutput() {
            FileName = string.Empty;
            OutputLines = new List<string>();
        }

        public string FileName { get; set; }
        public IList<string> OutputLines { get; set; }
    }
}