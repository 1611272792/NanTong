using Newtonsoft.Json;
using ProductionPlanSystem.Controllers;
using ProductionPlanSystem.Help;
using ProductionPlanSystem_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using XP_PPS_DAL;

namespace ProductionPlanSystem.Areas.ProductionPlanApi.Controllers
{
    public class AlterProductionDataController : ApiController
    {
        //[System.Web.Mvc.HttpGet]  
        [System.Web.Http.HttpGet]
        public ActionResult AlterProductionData(int ProductLineID,string AndroidID, string NewData)
        {
            //WriteLog("come :" + ProductLineID);
            //WriteLog("newdata:" + NewData);
            //string NewData = "{\"ProductionPlanID\":\"2\",\"ProductionPlanName\":\"fafafadfs\" , \"ProductionPlanVersion\":\"生产型号\" , \"c1\":\"111\",\"c2\":\"222\",\"TerminalID\":\"3\"}";
            AlterProductionData apd = new AlterProductionData();
            NewDatas nd = JsonConvert.DeserializeObject<NewDatas>(NewData);
            //WriteLog("nd:" + nd.ProductionPlanName);
            //WriteLog("nd:" + nd.ProductionPlanID);
            //WriteLog("nd:" + nd.TerminalID);
            string upsql = "";
            string macsql = string.Format("select Mac,Time from Terminal where TerminalID={0}", ProductLineID);
            //DataTable mac = DALHelp.ExecuteDataTable(macsql, null);
            DataTable mac = ServerOrLit.isDataTable(LoginController.connType, macsql);
            //如果mac为空可以获取数据
            if (mac == null || mac.Rows[0]["Mac"].ToString() == "" || mac.Rows[0]["Mac"] == null)
            {
                string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string addmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                //int add = DALHelp.ExecuteNonQuery(addmac, null);
                int add = ServerOrLit.isNum(LoginController.connType, addmac);
             
                if (add > 0)
                {
                    int info = 0;
                    if (!string.IsNullOrWhiteSpace(nd.NextProcessId) && !string.IsNullOrWhiteSpace(nd.NextTerminalId) && !string.IsNullOrWhiteSpace(nd.NextStateId))
                    {
                        SqlParameter[] par = new SqlParameter[12];
                        par[0] = new SqlParameter("@sqlType", "updProfinish");
                        par[1] = new SqlParameter("@produName", nd.ProductionPlanName);//订单名称
                        par[2] = new SqlParameter("@produVer", nd.ProductionPlanVersion);//备注
                        par[3] = new SqlParameter("@produNum", nd.PlanNum);//订单数量
                        par[4] = new SqlParameter("@nextState", nd.NextStateId);//下一道工序的状态id
                        par[5] = new SqlParameter("@nextProcessId", nd.NextProcessId);//下一道工序id
                        par[6] = new SqlParameter("@nextTerId", nd.NextTerminalId);//下一道工序终端id
                        par[7] = new SqlParameter("@State", nd.State);//状态id
                        par[8] = new SqlParameter("@produTer", nd.TerminalID);//终端id
                        par[9] = new SqlParameter("@ProcessId", nd.ProcessId);//工序id
                        par[10] = new SqlParameter("@ProId", nd.ProductionPlanID);//计划id
                        par[11] = new SqlParameter("@issus", 1);//发放状态默认为1
                        SqlDbHelper.RunProcedure("proc_UpdateProc", par, out info);
                    }
                    else if (CheckState(nd.ProcessId, nd.State))
                    {
                        string sqlstr = string.Format($@"UPDATE ProductionPlan SET  ProductionPlanName = '{nd.ProductionPlanName}',ProductionPlanVersion = '{nd.ProductionPlanVersion}',PlanNum = '{nd.PlanNum}',RealNum=1,EndTime =  CONVERT(varchar(200), GETDATE(), 20),State = {nd.State},TerminalID = '{nd.TerminalID}',ProcessId='{nd.ProcessId}'  WHERE ProductionPlanID = '{nd.ProductionPlanID}'");
                        info = SqlDbHelper.ExecuteNonQuery(sqlstr);
                    }
                    else
                    {
                        string sqlstr = string.Format($@"UPDATE ProductionPlan SET  ProductionPlanName = '{nd.ProductionPlanName}',ProductionPlanVersion = '{nd.ProductionPlanVersion}',PlanNum = '{nd.PlanNum}',State = {nd.State},TerminalID = '{nd.TerminalID}',ProcessId='{nd.ProcessId}'  WHERE ProductionPlanID = '{nd.ProductionPlanID}'");
                        info = SqlDbHelper.ExecuteNonQuery(sqlstr);
                    }
                    //WriteLog("upsql" + upsql);
                    //int dt = DALHelp.ExecuteNonQuery(upsql, null);
                    //int dt = ServerOrLit.isNum(LoginController.connType, upsql);
                    //WriteLog("dt:" + dt); 
                    if (info > 0)
                    {
                        apd.StateCode = 100;
                        apd.ReaSon = "";
                    }
                    else
                    {
                        apd.StateCode = 104;
                        apd.ReaSon = "更新数据失败";
                    }
                }
                else
                {
                    apd.StateCode = 104;
                    apd.ReaSon = "更新数据失败";
                }
            }
            else if (mac != null && mac?.Rows[0]["Mac"] != null && mac?.Rows[0]["Mac"].ToString() != "")
            {
                //如果mac不为空但是mac相等时也可以获取
                if (mac.Rows[0]["Mac"].ToString() == AndroidID)
                {
                    int info = 0;
                    if (!string.IsNullOrWhiteSpace(nd.NextProcessId) && !string.IsNullOrWhiteSpace(nd.NextTerminalId) && !string.IsNullOrWhiteSpace(nd.NextStateId))
                    {
                        SqlParameter[] par = new SqlParameter[12];
                        par[0] = new SqlParameter("@sqlType", "updProfinish");
                        par[1] = new SqlParameter("@produName", nd.ProductionPlanName);//订单名称
                        par[2] = new SqlParameter("@produVer", nd.ProductionPlanVersion);//备注
                        par[3] = new SqlParameter("@produNum", nd.PlanNum);//订单数量
                        par[4] = new SqlParameter("@nextState", nd.NextStateId);//下一道工序的状态id
                        par[5] = new SqlParameter("@nextProcessId", nd.NextProcessId);//下一道工序id
                        par[6] = new SqlParameter("@nextTerId", nd.NextTerminalId);//下一道工序终端id
                        par[7] = new SqlParameter("@State", nd.State);//状态id
                        par[8] = new SqlParameter("@produTer", nd.TerminalID);//终端id
                        par[9] = new SqlParameter("@ProcessId", nd.ProcessId);//工序id
                        par[10] = new SqlParameter("@ProId", nd.ProductionPlanID);//计划id
                        par[11] = new SqlParameter("@issus", 1);//发放状态默认为1
                        SqlDbHelper.RunProcedure("proc_UpdateProc", par, out info);

                    }
                    else if (CheckState(nd.ProcessId, nd.State))
                    {
                        string sqlstr = string.Format($@"UPDATE ProductionPlan SET  ProductionPlanName = '{nd.ProductionPlanName}',ProductionPlanVersion = '{nd.ProductionPlanVersion}',PlanNum = '{nd.PlanNum}',RealNum=1,EndTime =  CONVERT(varchar(200), GETDATE(), 20),State = {nd.State},TerminalID = '{nd.TerminalID}',ProcessId='{nd.ProcessId}'  WHERE ProductionPlanID = '{nd.ProductionPlanID}'");
                        info = SqlDbHelper.ExecuteNonQuery(sqlstr);
                    }
                    else
                    {
                        string sqlstr = string.Format($@"UPDATE ProductionPlan SET  ProductionPlanName = '{nd.ProductionPlanName}',ProductionPlanVersion = '{nd.ProductionPlanVersion}',PlanNum = '{nd.PlanNum}',State = {nd.State},TerminalID = '{nd.TerminalID}',ProcessId='{nd.ProcessId}'  WHERE ProductionPlanID = '{nd.ProductionPlanID}'");
                        info = SqlDbHelper.ExecuteNonQuery(sqlstr);
                    }
                    //WriteLog("upsql" + upsql);
                    //int dt = DALHelp.ExecuteNonQuery(upsql, null);
                    //int dt = ServerOrLit.isNum(LoginController.connType, upsql);
                    //WriteLog("dt:" + dt); 
                    if (info > 0)
                    {
                        apd.StateCode = 100;
                        apd.ReaSon = "";
                    }
                    else
                    {
                        apd.StateCode = 104;
                        apd.ReaSon = "更新数据失败";
                    }
                    string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    string upmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                    int add = ServerOrLit.isNum(LoginController.connType, upmac);
                }
                //如果间隔时间大于5分钟也可以获取
                else if (mac != null && mac.Rows[0]["Mac"].ToString() != AndroidID)
                {
                    DateTime accessTime;
                    TimeSpan ts = TimeSpan.FromDays(1);
                    if (DateTime.TryParse(mac.Rows[0]["Time"].ToString(), out accessTime))
                    {
                        ts = DateTime.Now - accessTime;
                    }
                    if (ts.TotalMinutes > 5)
                    {
                        int info = 0;
                        if (!string.IsNullOrWhiteSpace(nd.NextProcessId) && !string.IsNullOrWhiteSpace(nd.NextTerminalId) && !string.IsNullOrWhiteSpace(nd.NextStateId))
                        {
                            SqlParameter[] par = new SqlParameter[12];
                            par[0] = new SqlParameter("@sqlType", "updProfinish");
                            par[1] = new SqlParameter("@produName", nd.ProductionPlanName);//订单名称
                            par[2] = new SqlParameter("@produVer", nd.ProductionPlanVersion);//备注
                            par[3] = new SqlParameter("@produNum", nd.PlanNum);//订单数量
                            par[4] = new SqlParameter("@nextState", nd.NextStateId);//下一道工序的状态id
                            par[5] = new SqlParameter("@nextProcessId", nd.NextProcessId);//下一道工序id
                            par[6] = new SqlParameter("@nextTerId", nd.NextTerminalId);//下一道工序终端id
                            par[7] = new SqlParameter("@State", nd.State);//状态id
                            par[8] = new SqlParameter("@produTer", nd.TerminalID);//终端id
                            par[9] = new SqlParameter("@ProcessId", nd.ProcessId);//工序id
                            par[10] = new SqlParameter("@ProId", nd.ProductionPlanID);//计划id
                            par[11] = new SqlParameter("@issus", 1);//发放状态默认为1
                            SqlDbHelper.RunProcedure("proc_UpdateProc", par, out info);
                        }
                        else if (CheckState(nd.ProcessId, nd.State))
                        {
                            string sqlstr = string.Format($@"UPDATE ProductionPlan SET  ProductionPlanName = '{nd.ProductionPlanName}',ProductionPlanVersion = '{nd.ProductionPlanVersion}',PlanNum = '{nd.PlanNum}',RealNum=1,EndTime =  CONVERT(varchar(200), GETDATE(), 20),State = {nd.State},TerminalID = '{nd.TerminalID}',ProcessId='{nd.ProcessId}'  WHERE ProductionPlanID = '{nd.ProductionPlanID}'");
                            info = SqlDbHelper.ExecuteNonQuery(sqlstr);
                        }
                        else
                        {
                            string sqlstr = string.Format($@"UPDATE ProductionPlan SET  ProductionPlanName = '{nd.ProductionPlanName}',ProductionPlanVersion = '{nd.ProductionPlanVersion}',PlanNum = '{nd.PlanNum}',State = {nd.State},TerminalID = '{nd.TerminalID}',ProcessId='{nd.ProcessId}'  WHERE ProductionPlanID = '{nd.ProductionPlanID}'");
                            info = SqlDbHelper.ExecuteNonQuery(sqlstr);
                        }
                        //WriteLog("upsql" + upsql);
                        //int dt = DALHelp.ExecuteNonQuery(upsql, null);
                        //int dt = ServerOrLit.isNum(LoginController.connType, upsql);
                        //WriteLog("dt:" + dt); 
                        if (info > 0)
                        {
                            apd.StateCode = 100;
                            apd.ReaSon = "";
                        }
                        else
                        {
                            apd.StateCode = 104;
                            apd.ReaSon = "更新数据失败";
                        }
                        string timenow = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        string upmac = string.Format("update Terminal set Mac='{0}',Time='{1}' where TerminalID={2}", AndroidID, timenow, ProductLineID);
                        int add = ServerOrLit.isNum(LoginController.connType, upmac);
                    }
                    else
                    {
                        apd.StateCode = 104;
                        apd.ReaSon = "更新数据失败";
                    }
                }
                else
                {
                    apd.StateCode = 104;
                    apd.ReaSon = "更新数据失败";
                }
            }
            else
            {
                apd.StateCode = 104;
                apd.ReaSon = "更新数据失败";
            }
            return apd;
        }

        private void WriteLog(string strmsg)
        {
            strmsg = $"{strmsg}\r\n";
            using (FileStream fs = System.IO.File.Open("D:\\9.txt", FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] bys = Encoding.Default.GetBytes(strmsg);
                fs.Write(bys, 0, bys.Length);
                fs.Close();
            }
        }


        /// <summary>
        /// 验证该状态是否为最后一条状态
        /// </summary>
        /// <param name="ProcessId"></param>
        /// <param name="StateId"></param>
        /// <returns></returns>
        public bool CheckState(int ProcessId, int StateId)
        {

            int state = Convert.ToInt32(SqlDbHelper.ExecuteDataTable($"EXEC dbo.proc_UpdateProc @sqlType = N'checkState',@ProcessId ={ProcessId}").Rows[0]["StateId"]);
            if (StateId == state)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
