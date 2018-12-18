using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ProductionPlanSystem.Areas.ProductionPlanApi.Models;
using System.Data;
using XP_PPS_DAL;
using ProductionPlanSystem.Help;
using ProductionPlanSystem.Controllers;

namespace ProductionPlanSystem.Areas.ProductionPlanApi.Controllers
{
    public class GetProductionPwdController : ApiController
    {
        public ActionResult GetProductionPwd(int ProductLineID, string AndroidID)
        {
            GetProductionPwd gpp = new GetProductionPwd();
            try
            {
                string pwdsql = string.Format("select TerminalPwd,Mac,Time from Terminal where TerminalID={0}", ProductLineID);
                //DataTable dt = DALHelp.ExecuteDataTable(pwdsql, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, pwdsql);
                if (dt.Rows.Count > 0)
                {
                    //如果mac为空可以获取
                    if (dt.Rows[0]["Mac"].ToString() == "" || dt.Rows[0]["Mac"] == null)
                    {
                        string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        string addmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                        //int add = DALHelp.ExecuteNonQuery(addmac, null);
                        int add = ServerOrLit.isNum(LoginController.connType, addmac);
                        if (add > 0)
                        {
                            gpp.StateCode = 100;
                            gpp.Reason = "";
                            gpp.pwd = dt.Rows[0]["TerminalPwd"].ToString();
                        }
                        else
                        {
                            gpp.StateCode = 100;
                            gpp.Reason = "获取数据失败";
                            gpp.pwd = "";
                        }
                    }
                    else if (dt.Rows[0]["Mac"] != null || dt.Rows[0]["Mac"].ToString() != "")
                    {
                        //如果mac不为空但是mac相等时也可以获取
                        if (dt.Rows[0]["Mac"].ToString() == AndroidID)
                        {
                            gpp.StateCode = 100;
                            gpp.Reason = "";
                            gpp.pwd = dt.Rows[0]["TerminalPwd"].ToString();
                            string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            string upmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                            int add = ServerOrLit.isNum(LoginController.connType, upmac);
                        }
                        //如果间隔时间大于5分钟也可以获取
                        else if (dt.Rows[0]["Mac"].ToString() != AndroidID)
                        {
                            TimeSpan ts = DateTime.Now - DateTime.Parse(dt.Rows[0]["Time"].ToString());
                            if (ts.TotalMinutes > 5)
                            {
                                gpp.StateCode = 100;
                                gpp.Reason = "";
                                gpp.pwd = dt.Rows[0]["TerminalPwd"].ToString();
                                string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                string upmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                                int add = ServerOrLit.isNum(LoginController.connType, upmac);
                            }
                            else
                            {
                                gpp.StateCode = 104;
                                gpp.Reason = "此产线已被占用";
                                gpp.pwd = "";
                            }
                        }
                        else
                        {
                            gpp.StateCode = 104;
                            gpp.Reason = "此产线已被占用";
                            gpp.pwd = "";
                        }
                    }
                    else
                    {
                        gpp.StateCode = 104;
                        gpp.Reason = "此产线已被占用";
                        gpp.pwd = "";
                    }
                }
                else
                {
                    gpp.StateCode = 100;
                    gpp.Reason = "获取数据失败";
                    gpp.pwd = "";
                }
            }
            catch (Exception ex)
            {
                gpp.StateCode = 104;
                gpp.Reason = ex.Message;
                gpp.pwd = "";
            }
            return gpp;
        }
    }
}
