using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem_WebApi.Models
{
    //获取产线基本信息
    public class Fields
    {
        public string Field { get; set; }
        public string FieldName { get; set; }
        public int FieldWidth { get; set; }
    }

    public class LineInfos:ActionResult
    {
        public int StateCode { get; set; }
        public string Reason { get; set; }
        public LineInfoEntity LineInfo { get; set; }
        public class LineInfoEntity 
        {
            public string Name { get; set; }
            //public string Manager { get; set; }
            public string Password { get; set; }
            public List<Fields> Fields { get; set; } = new List<Fields>();
        }
        public override void ExecuteResult(ControllerContext context)
        {

        }
    }
}