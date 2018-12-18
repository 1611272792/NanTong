using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XP_PPS_Model.Model
{
   public class ListInfo
    {
        public List<ProcessList> ListProcess;
        public List<Terminal> ListTerminal;
    }

    public class ProcessList
    {
        public string ProcessId { get; set; }
        public string ProcessName { get; set; }
    }

    public class Terminal
    {
        public string TerminalId { get; set; }
        public string TerminalName { get; set; }
    }
}
