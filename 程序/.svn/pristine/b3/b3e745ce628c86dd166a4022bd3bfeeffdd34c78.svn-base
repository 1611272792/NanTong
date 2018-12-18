using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductionPlanSystem.Areas.ProductionPlanApi.Models
{
    public class ProcessTerminal
    {
        public int StateCode { get; set; }
        public string ReaSon { get; set; }
        public List<ProcessInfo> ProcessList { get; set; }
    }
    public class ProcessInfo
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public List<TerminalInfo> TerminalList { get; set; }

    }
    public class TerminalInfo {
        public int TerminalID { get; set; }
        public string TerminalName { get; set; }
        public int PageTime { get; set; }
    }
}