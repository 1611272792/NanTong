using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem.Controllers
{
    public class BaseController : Controller
    {
      
        public BaseController()
        {
          
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (Session["UserId"] != null && Session["UserInfoID"] != null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.HttpContext.Response.Write("<script language=javascript>this.parent.window.location.href='http://" + filterContext.HttpContext.Request.Url.Authority + "';</script> ");

                base.OnActionExecuting(filterContext);
            }
        }
    }
}