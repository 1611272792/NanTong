using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem_WebApi.Models
{
    public class CheckVersion : ActionResult
    {
        public int StateCode { get; set; }
        public string Reason { get; set; }
        public bool NewVersion { get; set; }
        public string DownloadUrl { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {

        }
    }
}