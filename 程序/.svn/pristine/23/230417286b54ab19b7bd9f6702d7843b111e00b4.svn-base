using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem_WebApi.Models
{
    public class GetCompanyInfo
    {
        public class ComInfo : ActionResult
        {
            public int StateCode { get; set; }
            public string Reason { set; get; }
            public InfoEntity Info { get; set; }
            public class InfoEntity
            {
                public string LogoUrl { get; set; }
                public string CompanyName { get; set; }
            }

            public override void ExecuteResult(ControllerContext context)
            {


            }
        }

    }
}