using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductionPlanSystem.Areas.ProductionPlanApi.Models
{
    public class CheckProcess
    {
        public int StateCode { get; set; }
        public string ReaSon { get; set; }
        public int NextProcessId { get; set; }
        public int NextStateId { get; set; }
    }
}