using ProductionPlanSystem.Controllers;
using ProductionPlanSystem.Help;
using ProductionPlanSystem_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using XP_PPS_DAL;
using static ProductionPlanSystem_WebApi.Models.GetMessage;

namespace ProductionPlanSystem_WebApi.Controllers
{
    public class GetMessageController : ApiController
    {
        [System.Web.Mvc.HttpGet]
        //接口6：获取通知
        public ActionResult GetMessage(int ProductLineID, string AndroidID)
        {
            GetMessage mg = new GetMessage();
            MessageEntity me = new MessageEntity();
            try
            {
                string sql = "";
                if (LoginController.connType == "sqlserver")
                {
                    sql = string.Format("select top 1 * from Inform where TerminalID like '%{0}%' order by InformTime desc", ProductLineID);
                }
                else
                {
                    sql = string.Format("select * from Inform where TerminalID like '%{0}%' order by InformTime desc limit 0,1", ProductLineID);
                }
                //DataTable dt = DALHelp.ExecuteDataTable(sql, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);
                if (dt?.Rows.Count > 0)
                {
                    string macsql = string.Format("select Mac,Time from Terminal where TerminalID={0}", ProductLineID);
                    //DataTable mac = DALHelp.ExecuteDataTable(macsql, null);
                    DataTable mac = ServerOrLit.isDataTable(LoginController.connType, macsql);
                    if (mac != null)
                    { //如果Mac为空可以获取数据
                        if (mac.Rows[0]["Mac"].ToString() == "" || mac.Rows[0]["Mac"] == null)
                        {
                            string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            string addmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                            //int add = DALHelp.ExecuteNonQuery(addmac, null);
                            int add = ServerOrLit.isNum(LoginController.connType, addmac);
                            if (add > 0)
                            {
                                mg.StateCode = 100;
                                mg.Reason = "";
                                mg.Message = me;
                                me.Content = dt.Rows[0]["Content"].ToString();
                                me.Color = dt.Rows[0]["Color"].ToString();
                                me.Size = dt.Rows[0]["Size"].ToString();
                            }
                            else
                            {
                                mg.StateCode = 100;
                                mg.Reason = "";
                                mg.Message = me;
                                me.Content = "";
                                me.Color = "";
                                me.Size = "0";
                            }
                        }
                        else if (mac.Rows[0]["Mac"] != null || mac.Rows[0]["Mac"].ToString() != "")
                        {
                            //如果mac不为空但是mac相等时也可以获取
                            if (mac.Rows[0]["Mac"].ToString() == AndroidID)
                            {
                                mg.StateCode = 100;
                                mg.Reason = "";
                                mg.Message = me;
                                me.Content = dt.Rows[0]["Content"].ToString();
                                me.Color = dt.Rows[0]["Color"].ToString();
                                me.Size = dt.Rows[0]["Size"].ToString();
                                string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                string upmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                                int add = ServerOrLit.isNum(LoginController.connType, upmac);
                            }
                            else if (mac.Rows[0]["Mac"].ToString() != AndroidID)
                            {
                                DateTime accessTime;
                                TimeSpan ts = TimeSpan.FromDays(1);
                                if (DateTime.TryParse(mac.Rows[0]["Time"].ToString(), out accessTime))
                                {
                                    ts = DateTime.Now - accessTime;
                                }
                                if (ts.TotalMinutes > 5)
                                {
                                    mg.StateCode = 100;
                                    mg.Reason = "";
                                    mg.Message = me;
                                    me.Content = dt.Rows[0]["Content"].ToString();
                                    me.Color = dt.Rows[0]["Color"].ToString();
                                    me.Size = dt.Rows[0]["Size"].ToString();
                                    string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                    string upmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                                    int add = ServerOrLit.isNum(LoginController.connType, upmac);
                                }
                                else
                                {
                                    mg.StateCode = 104;
                                    mg.Reason = "程序正在被使用";
                                    mg.Message = me;
                                    me.Content = "";
                                    me.Color = "";
                                    me.Size = "0";
                                }
                            }
                            else
                            {
                                mg.StateCode = 104;
                                mg.Reason = "程序正在被使用";
                                mg.Message = me;
                                me.Content = "";
                                me.Color = "";
                                me.Size = "0";
                            }
                        }
                        else
                        {
                            mg.StateCode = 104;
                            mg.Reason = "程序正在被使用";
                            mg.Message = me;
                            me.Content = "";
                            me.Color = "";
                            me.Size = "0";
                        }
                    }
                }
                else
                {
                    mg.StateCode = 100;
                    mg.Reason = "";
                    mg.Message = me;
                    me.Content = "";
                    me.Color = "";
                    me.Size = "0";
                }
            }
            catch (Exception ex)
            {

                mg.StateCode = 104;
                mg.Reason = ex.Message;
                mg.Message = new MessageEntity();
            }
            //}
            return mg;
        }
    }
}
