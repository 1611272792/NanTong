using ProductionPlanSystem_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using static ProductionPlanSystem_WebApi.Models.GetCompanyInfo.ComInfo;
using XP_PPS_DAL;
using static ProductionPlanSystem_WebApi.Models.GetCompanyInfo;
using ProductionPlanSystem.Help;
using ProductionPlanSystem.Controllers;

namespace ProductionPlanSystem_WebApi.Controllers
{
    public class GetCompanyInfoController : ApiController
    {

        [System.Web.Mvc.HttpGet]
        // 接口5 获取公司信息
        public ActionResult GetCompanyInfo()
        {
            ComInfo ci = new ComInfo();
            InfoEntity ie = new InfoEntity();
            try
            {
                string sql = string.Format("select * from FirmInfo where FirmInfoID=1");
                //DataTable dt = DALHelp.ExecuteDataTable(sql, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);
                if (dt?.Rows.Count > 0)
                {
                    ci.StateCode = 100;
                    ci.Reason = "";
                    ci.Info = ie;
                    ie.LogoUrl = "http://" + AppConfig.IP + dt.Rows[0]["LogoUrl"].ToString();
                    ie.CompanyName = dt.Rows[0]["CompanyName"].ToString();
                }
                else
                {
                    ci.StateCode = 100;
                    ci.Reason = "获取数据失败";
                    ci.Info = new InfoEntity();
                }
            }
            catch (Exception ex)
            {
                ci.StateCode = 104;
                ci.Reason = ex.Message;
                ci.Info = new InfoEntity();
            }
            return ci;
        }

    }
}
