using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem.Areas.ProductionPlanApi.Models
{
    public class GetProductionPwd: ActionResult
    {
        public int StateCode { get; set; }
        public string Reason { get; set; }
        public string pwd { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {

        }
    }
}