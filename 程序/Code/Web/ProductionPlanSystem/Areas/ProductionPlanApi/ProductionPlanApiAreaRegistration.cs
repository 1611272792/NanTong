using System.Web.Mvc;

namespace ProductionPlanSystem.Areas.ProductionPlanApi
{
    public class ProductionPlanApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProductionPlanApi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProductionPlanApi_default",
                "ProductionPlanApi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}