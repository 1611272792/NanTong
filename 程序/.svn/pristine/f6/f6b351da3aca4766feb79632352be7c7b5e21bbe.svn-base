using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem_WebApi.Models
{
    public class GetMessage : ActionResult
    {
        public int StateCode { get; set; }
        public string Reason { get; set; }
        public MessageEntity Message { get; set; }
        public class MessageEntity
        {
            public string Content { get; set; }
            public string Color { get; set; }
            public string Size { get; set; }
        }

        public override void ExecuteResult(ControllerContext context)
        {

        }
    }
}