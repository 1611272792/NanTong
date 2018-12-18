using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem_WebApi.Models
{
    public class AlterProductionData : ActionResult
    {
        public int StateCode { get; set; }
        public string ReaSon { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {

        }
    }

    public class NewDatas
    {
        public string ProductionPlanID { get; set; }
        public string ProductionPlanName { get; set; }
        public string ProductionPlanVersion { get; set; }
        public string PlanNum { get; set; }
        public string RealNum { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int State { get; set; }
        public string TerminalID { get; set; }
        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }
        public string c4 { get; set; }
        public string c5 { get; set; }
        public string c6 { get; set; }
        public string c7 { get; set; }
        public string c8 { get; set; }
        public string c9 { get; set; }
        public string c10 { get; set; }
        public string c11 { get; set; }
        public string c12 { get; set; }
        public string c13 { get; set; }
        public string c14 { get; set; }
        public string c15 { get; set; }
        public string c16 { get; set; }
        public string c17 { get; set; }
        public string c18 { get; set; }
        public string c19 { get; set; }
        public string c20 { get; set; }
        public int ProcessId { get; set; }
        public string NextProcessId { get; set; }
        public string NextTerminalId { get; set; }
        public string NextStateId { get; set; }

    }
}