using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem.Areas.ProductionPlanApi.Models
{
    public class Line
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Lines : ActionResult
    {
        public int StateCode { get; set; }
        public string ReaSon { get; set; }
        public List<Line> ProductLines { get; set; } = new List<Line>();

        public override void ExecuteResult(ControllerContext context)
        {

        }
    }
}