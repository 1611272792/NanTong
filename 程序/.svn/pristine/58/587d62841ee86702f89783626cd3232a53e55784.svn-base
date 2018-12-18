using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XP_PPS_DAL;
using ProductionPlanSystem_WebApi.Models;
using System.Web.Mvc;
using static ProductionPlanSystem_WebApi.Models.LineInfos;
using System.IO;
using System.Text;
using ProductionPlanSystem.Help;
using ProductionPlanSystem.Controllers;

namespace ProductionPlanSystem_WebApi.Controllers
{
    public class GetProductLineInfoController : ApiController
    {
        [System.Web.Mvc.HttpGet]
        //接口2 获取产线基本信息
        public ActionResult GetProductLineInfo(int ProductLineID, string AndroidID)
        {
            string macsql = string.Format("select Mac,Time from Terminal where TerminalID={0}", ProductLineID);
            //DataTable mac = DALHelp.ExecuteDataTable(macsql, null);
            DataTable mac = ServerOrLit.isDataTable(LoginController.connType, macsql);
            LineInfos li = new LineInfos();
            LineInfoEntity ll = new LineInfoEntity();
            try
            {
                //string sqlcx = string.Format("select TerminalName,UserName,TerminalPwd from Terminal t inner join UserInfo u on t.UserInfoID=u.UserInfoID where TerminalID={0}", ProductLineID);
                string sqlcx = string.Format("select TerminalName,TerminalPwd from Terminal where TerminalID={0}", ProductLineID);
                //WriteLog("sqlcx:" + sqlcx);
                //DataTable dt = DALHelp.ExecuteDataTable(sqlcx, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlcx);
                //WriteLog("dt:" + dt);
                if (dt?.Rows.Count > 0)
                {
                    if (mac != null)
                    {
                        //如果Mac为空可以获取数据
                        if (mac.Rows[0].Field<string>("Mac") == "" || mac.Rows[0].Field<string>("Mac") == null)
                        {
                            string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            string addmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                            //int add = DALHelp.ExecuteNonQuery(addmac, null);
                            int add = ServerOrLit.isNum(LoginController.connType, addmac);
                            if (add > 0)
                            {
                                li.StateCode = 100;
                                li.Reason = "";
                                li.LineInfo = ll;
                                ll.Name = dt.Rows[0].Field<string>("TerminalName");
                                //ll.Manager = dt.Rows[0]["UserName"].ToString();
                                ll.Password = dt.Rows[0].Field<string>("TerminalPwd");
                            }
                            else
                            {
                                li.StateCode = 100;
                                li.Reason = "查询数据失败！！";
                                li.LineInfo = new LineInfoEntity();
                            }
                        }
                        else if (mac.Rows[0].Field<string>("Mac") != null && mac.Rows[0].Field<string>("Mac") != "")
                        {
                            //如果mac不为空但是mac相等时也可以获取
                            if (mac.Rows[0].Field<string>("Mac") == AndroidID)
                            {
                                li.StateCode = 100;
                                li.Reason = "";
                                li.LineInfo = ll;
                                ll.Name = dt.Rows[0].Field<string>("TerminalName");
                                //ll.Manager = dt.Rows[0]["UserName"].ToString();
                                ll.Password = dt.Rows[0].Field<string>("TerminalPwd");
                                string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                string upmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                                int add = ServerOrLit.isNum(LoginController.connType, upmac);
                            }
                            //如果间隔时间大于5分钟也可以获取
                            else if (mac.Rows[0].Field<string>("Mac") != AndroidID)
                            {
                                DateTime accessTime;
                                TimeSpan ts = TimeSpan.FromDays(1);
                                if (DateTime.TryParse(mac.Rows[0]["Time"].ToString(), out accessTime))
                                {
                                    ts = DateTime.Now - accessTime;
                                }

                                if (ts.TotalMinutes > 5)
                                {
                                    li.StateCode = 100;
                                    li.Reason = "";
                                    li.LineInfo = ll;
                                    ll.Name = dt.Rows[0].Field<string>("TerminalName");
                                    //ll.Manager = dt.Rows[0]["UserName"].ToString();
                                    ll.Password = dt.Rows[0]["TerminalPwd"].ToString();
                                    string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                    string upmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                                    int add = ServerOrLit.isNum(LoginController.connType, upmac);
                                }
                                else
                                {
                                    li.StateCode = 104;
                                    li.Reason = "此产线已被占用！";
                                    li.LineInfo = new LineInfoEntity();
                                }
                            }
                            else
                            {
                                li.StateCode = 104;
                                li.Reason = "此产线已被占用！";
                                li.LineInfo = new LineInfoEntity();
                            }
                        }
                        else
                        {
                            li.StateCode = 104;
                            li.Reason = "此产线已被占用！";
                            li.LineInfo = new LineInfoEntity();
                        }
                    }
                }
                else
                {
                    li.StateCode = 100;
                    li.Reason = "没有查到相关数据！！";
                    li.LineInfo = new LineInfoEntity();
                }
            }
            catch (Exception ex)
            {
                li.StateCode = 104;
                li.Reason = ex.Message;
                li.LineInfo = new LineInfoEntity();
            }
            string sqlzd = string.Format($"EXEC AboutSelect @type='getAllocationName',@TerminalId={ProductLineID}");
            //DataTable dt1 = DALHelp.ExecuteDataTable(sqlzd,null);
            DataTable dt1 = ServerOrLit.isDataTable(LoginController.connType, sqlzd);
            if (dt1?.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i]["AllocationName"].ToString() == "Issue"|| dt1.Rows[i]["AllocationName"].ToString() == "ProcessId"|| dt1.Rows[i]["AllocationName"].ToString() == "TerminalID")
                    {

                    }
                    else
                    {
                        ll.Fields.Add(new Fields() { Field = dt1.Rows[i].Field<string>("AllocationName"), FieldName = dt1.Rows[i].Field<string>("AllocationTitle"), FieldWidth =int.Parse( dt1.Rows[i]["FieldWidth"].ToString() )});
                    }
                }
            }
            return li;
        }

        private void WriteLog(string strmsg)
        {
            strmsg = $"{strmsg}\r\n";
            using (FileStream fs = System.IO.File.Open("D:\\5.txt", FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] bys = Encoding.Default.GetBytes(strmsg);
                fs.Write(bys, 0, bys.Length);
                fs.Close();
            }
        }
    }
}
