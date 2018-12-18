﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using XP_PPS_Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using XP_PPS_DAL;
using System.IO;
using ProductionPlanSystem.Help;
using System.Data.SqlClient;

namespace ProductionPlanSystem.Controllers
{
    public class MonitoringController : BaseController
    {

        // GET: Monitoring
        public ActionResult Index()
        {
            //查询表头数据
            string sqltitle = string.Format("SELECT  AllocationName,AllocationTitle,[Type]  from Allocation WHERE IsActive=1 order by Serial asc");
            //DataTable dt2 = DALHelp.ExecuteDataTable(sqltitle, null);
            DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sqltitle);

            if (dt2.Rows.Count > 0)
            {
                ViewBag.title = dt2;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["AllocationName"].ToString() == "TerminalID")
                    {
                        ViewBag.TerName = dt2.Rows[i]["AllocationTitle"];//终端名
                    }
                }
            }
            else
            {
                ViewBag.title = null;
            }

            string userID;
            string Role;
            if (Session["UserInfoID"] == null || Session["Role"] == null)
            {
                return Content("<script>top.location = '/Login/Index';</script>");
            }
            else
            {
                userID = Session["UserInfoID"].ToString();//登录人id
                Role = Session["Role"].ToString();
            }
            string str = "";
            if (Role != AppConfig.Role)
            {
                str = string.Format($"select TerminalName,TerminalID,UserInfoID from Terminal where (UserInfoID LIKE '%,{userID},%'   OR UserInfoID LIKE '%,{userID}' OR UserInfoID LIKE '{userID},%' OR UserInfoID='{userID}')");
            }
            else  //当用户为管理员时
            {
                str = string.Format("select TerminalName,TerminalID from Terminal");
            }
            //DataTable dtter = DALHelp.ExecuteDataTable(str, null);
            DataTable dtter = ServerOrLit.isDataTable(LoginController.connType, str);

            if (dtter.Rows.Count > 0)
            {
                List<Terminal> terminal = new List<Terminal>();
                //把终端id转换为终端名
                for (int i = 0; i < dtter.Rows.Count; i++)
                {
                    Terminal ter = new Terminal();
                    ter.TerminalID = int.Parse(dtter.Rows[i]["TerminalID"].ToString());
                    ter.TerminalName = dtter.Rows[i]["TerminalName"].ToString();
                    terminal.Add(ter);
                }
                ViewBag.terlist = terminal;
                ViewBag.dt = dtter;
            }
            else
            {
                ViewBag.dt = null;
            }
            string sql = "";
            if (Role != AppConfig.Role)
            {

                sql = string.Format($"select * from DataReport where TerminalID in(select TerminalID from Terminal where (UserInfoID LIKE '%,{userID},%'   OR UserInfoID LIKE '%,{userID}' OR UserInfoID LIKE '{userID},%' OR UserInfoID='{userID}'))and Issue=1 and Clear=0");
            }
            else  //为最高管理员时
            {
                sql = string.Format("select * from DataReport where Issue=1 and Clear=0");
            }
            //DataTable dt = DALHelp.ExecuteDataTable(sql, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);

            if (dt.Rows.Count > 0)
            {
                ViewBag.producetion = dt;
            }
            else
            {
                ViewBag.producetion = null;
            }
       
            #region 选择区域
            SqlParameter[] values = new SqlParameter[1];
            values[0] = new SqlParameter("@strType", "process");
            //获取工序信息
            DataSet info = SqlDbHelper.RunProcedure("proc_ProcessSet", values, "process");
            if (info.Tables[0]?.Rows.Count > 0)
            {
                ViewBag.pinfo = info.Tables[0];
            }
            else
            {
                ViewBag.pinfo = null;
            }
            #endregion

            #region 选择项目
            DataTable prodt = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'pjinfo'");
            if (dt?.Rows.Count > 0)
            {
                ViewBag.pjinfo = prodt;
            }
            else
            {
                ViewBag.pjinfo = null;
            }
            #endregion
            return View();
        }

        //监控分布视图
        public ActionResult Index1(int selectTerminalID) {

            string sqltitle = "";
            if (selectTerminalID == 0)
            {
                sqltitle = string.Format($"EXEC AboutSelect @type='NoTerminalId'");
            }
            else
            {
                sqltitle = string.Format($"EXEC AboutSelect @type='getAllocationName',@TerminalId={selectTerminalID}");

            }
            ////查询表头数据
            //string sqltitle = string.Format("select  AllocationName,AllocationTitle  from Allocation where IsActive=1  order by Serial asc");
            //DataTable dt2 = DALHelp.ExecuteDataTable(sqltitle, null);
            DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sqltitle);

            if (dt2.Rows.Count > 0)
            {
                ViewBag.title = dt2;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["AllocationName"].ToString() == "TerminalID")
                    {
                        ViewBag.TerName = dt2.Rows[i]["AllocationTitle"];//终端名
                    }
                }
            }
            else
            {
                ViewBag.title = null;
            }
            string userID;
            string Role;
            if (Session["UserInfoID"] == null || Session["Role"] == null)
            {
                return Content("<script>top.location = '/Login/Index';</script>");
            }
            else
            {
                userID = Session["UserInfoID"].ToString();//登录人id
                Role = Session["Role"].ToString();
            }

            string str = "";
            if (Role != AppConfig.Role)
            {
                str = string.Format($"select TerminalName,TerminalID,UserInfoID from Terminal where (UserInfoID LIKE '%,{userID},%'   OR UserInfoID LIKE '%,{userID}' OR UserInfoID LIKE '{userID},%' OR UserInfoID='{userID}')");
            }
            else  //当用户为管理员时
            {
                str = string.Format("select TerminalName,TerminalID from Terminal");
            }
            //DataTable dtter = DALHelp.ExecuteDataTable(str, null);
            DataTable dtter = ServerOrLit.isDataTable(LoginController.connType, str);

            if (dtter.Rows.Count > 0)
            {
                List<Terminal> terminal = new List<Terminal>();
                //把终端id转换为终端名
                for (int i = 0; i < dtter.Rows.Count; i++)
                {
                    Terminal ter = new Terminal();
                    ter.TerminalID = int.Parse(dtter.Rows[i]["TerminalID"].ToString());
                    ter.TerminalName = dtter.Rows[i]["TerminalName"].ToString();
                    terminal.Add(ter);
                }
                ViewBag.terlist = terminal;
                ViewBag.dt = dtter;
            }
            else
            {
                ViewBag.dt = null;
            }
            string sql = "";
            //if (selectTerminalID == 0)//选择查看全部时
            //{
                //if (Role != AppConfig.Role)
                //{

                //    sql = string.Format($"select * from DataReport where Issue=1 and Clear=0 and  TerminalID in(select TerminalID from Terminal where (UserInfoID LIKE '%,{userID},%'   OR UserInfoID LIKE '%,{userID}' OR UserInfoID LIKE '{userID},%' OR UserInfoID='{userID}'))");
                //}
                //else  //为最高管理员时
                //{
                    sql = string.Format("select * from DataReport where Issue=1 and Clear=0");
            //    }
            //}
            //else
            //{
            //    sql = string.Format("select * from DataReport where Issue=1 and Clear=0 ");
            //}

            //DataTable dt = DALHelp.ExecuteDataTable(sql, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);


            if (dt.Rows.Count > 0)
            {
                ViewBag.producetion = dt;
            }
            else
            {
                ViewBag.producetion = null;
            }
            #region 选择工序
            SqlParameter[] values = new SqlParameter[1];
            values[0] = new SqlParameter("@strType", "process");
            //获取工序信息
            DataSet info = SqlDbHelper.RunProcedure("proc_ProcessSet", values, "process");
            if (info.Tables[0]?.Rows.Count > 0)
            {
                ViewBag.pinfo = info.Tables[0];
            }
            else
            {
                ViewBag.pinfo = null;
            }
            #endregion

            #region 选择项目
            DataTable prodt = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'pjinfo'");
            if (dt?.Rows.Count > 0)
            {
                ViewBag.pjinfo = prodt;
            }
            else
            {
                ViewBag.pjinfo = null;
            }
            #endregion
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            return PartialView("_MoniteLine", dt);

        }
        //得到需要编辑的内容
        public ActionResult EditProducelineBySelect(int produID, int TrNum)
        {
            string sqlstr = string.Format("select * from ProductionPlan where ProductionPlanID={0}", produID);
            //DataTable dt = DALHelp.ExecuteDataTable(sqlstr, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);

            if (dt.Rows.Count > 0)
            {
                int produId = int.Parse(dt.Rows[0]["ProductionPlanID"].ToString());
                string produName = dt.Rows[0]["ProductionPlanName"].ToString();
                string produVer = dt.Rows[0]["ProductionPlanVersion"].ToString();
                string produNum = dt.Rows[0]["PlanNum"].ToString();
                string produRea = dt.Rows[0]["RealNum"].ToString();
                string produStart = DateTime.Parse(dt.Rows[0]["StartTime"].ToString()).ToString("yyyy-MM-dd");//转换为datetime标准格式 HH:mm:ss
                string produEnd = "";
                if (dt.Rows[0]["EndTime"].ToString().Length > 0)
                {
                    produEnd = DateTime.Parse(dt.Rows[0]["EndTime"].ToString()).ToString("yyyy-MM-dd");
                }
                else
                {
                    produEnd = null;
                }

                string produState = dt.Rows[0]["State"].ToString();
                string produTer = dt.Rows[0]["TerminalID"].ToString();
                string Csum = "";//c1_c20的数据
                for (int i = 10; i <= TrNum+1 ; i++)
                {
                    Csum = Csum + dt.Rows[0][i + 1].ToString() + ",";
                }
                return Content(produId + "," + produName + "," + produVer + "," + produNum + "," + produRea + "," + produStart + "," + produEnd + "," + produState + "," + produTer + "," + Csum);
            }
            else
            {
                return Content("no");
            }
        }
        //编辑
        public ActionResult EditProduceline(string produName, string produVer, string produNum, string produRea, string produStart, string produEnd, int produState, string produTer,int processId, int produID, string FieldValue, string NextProcessId, string NextTerminalId, string NextStateId)
        {
            //查询表头数据
            string sqltitle = string.Format("select  AllocationName,AllocationTitle  from Allocation order by Serial asc");
            //DataTable dt2 = DALHelp.ExecuteDataTable(sqltitle, null);
            DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sqltitle);

            if (dt2.Rows.Count > 0)
            {
                ViewBag.title = dt2;
            }
            else
            {
                ViewBag.title = null;
            }
            int info = 0;
            if (!string.IsNullOrWhiteSpace(NextProcessId) && !string.IsNullOrWhiteSpace(NextTerminalId) && !string.IsNullOrWhiteSpace(NextStateId))
            {
                SqlParameter[] par = new SqlParameter[12];
                par[0] = new SqlParameter("@sqlType", "updProfinish");
                par[1] = new SqlParameter("@produName", produName);//订单名称
                par[2] = new SqlParameter("@produVer", produVer);//备注
                par[3] = new SqlParameter("@produNum", produNum);//订单数量
                par[4] = new SqlParameter("@nextState", NextStateId);//下一道工序的状态id
                par[5] = new SqlParameter("@nextProcessId", NextProcessId);//下一道工序id
                par[6] = new SqlParameter("@nextTerId", NextTerminalId);//下一道工序终端id
                par[7] = new SqlParameter("@State", produState);//状态id
                par[8] = new SqlParameter("@produTer", produTer);//终端id
                par[9] = new SqlParameter("@ProcessId", processId);//工序id
                par[10] = new SqlParameter("@ProId", produID);//计划id
                par[11] = new SqlParameter("@issus", 1);//发放状态
                SqlDbHelper.RunProcedure("proc_UpdateProc", par, out info);
            }
            else if (CheckState(processId, produState))
            {
                string sqlstr = string.Format($@"UPDATE ProductionPlan SET  ProductionPlanName = '{produName}',ProductionPlanVersion = '{produVer}',PlanNum = '{produNum}',RealNum=1,EndTime =  CONVERT(varchar(200), GETDATE(), 20),State = {produState},TerminalID = '{produTer}',ProcessId='{processId}'  WHERE ProductionPlanID = '{produID}'");
                info = SqlDbHelper.ExecuteNonQuery(sqlstr);
            }
            else
            {
                string sqlstr = string.Format($@"UPDATE ProductionPlan SET  ProductionPlanName = '{produName}',ProductionPlanVersion = '{produVer}',PlanNum = '{produNum}',State = {produState},TerminalID = '{produTer}',ProcessId='{processId}'  WHERE ProductionPlanID = '{produID}'");
                info = SqlDbHelper.ExecuteNonQuery(sqlstr);
            }


            return PartialView("_MoniteLine");
            
            
        }
        //清空 
        public ActionResult Clear2(int selectTerminalID) {
            string userID;
            string Role;
            if (Session["UserInfoID"] == null || Session["Role"] == null)
            {
                return Content("<script>top.location = '/Login/Index';</script>");
            }
            else
            {
                userID = Session["UserInfoID"].ToString();//登录人id
                Role = Session["Role"].ToString();
            }
            string sqlclear = "";
            if (Role != AppConfig.Role)
            {
                if (selectTerminalID == 0)
                {
                    sqlclear = string.Format($"Update  ProductionPlan set Clear=1 where Issue=1 and TerminalID in (select TerminalID from Terminal where (UserInfoID LIKE '%,{userID},%'   OR UserInfoID LIKE '%,{userID}' OR UserInfoID LIKE '{userID},%' OR UserInfoID='{userID}')) and State=1");
                }
                else
                {
                    sqlclear = string.Format("Update  ProductionPlan set Clear=1 where ProductionPlan.TerminalID ='{0}' and State=1", selectTerminalID);
                }   
            }
            else
            {
                if (selectTerminalID == 0)
                {
                    sqlclear = string.Format("Update  ProductionPlan set Clear=1 where Issue=1 and State=1");
                }
                else
                {
                    sqlclear = string.Format("Update  ProductionPlan set Clear=1 where Issue=1 and State=1 and TerminalID='{0}'", selectTerminalID);
                }

            }
            //int dtClear = DALHelp.ExecuteNonQuery(sqlclear, null);
            int dtClear = ServerOrLit.isNum(LoginController.connType, sqlclear);
            if (dtClear > 0)
            {
                return Content("清除成功");
            }
            else
            {
                return Content("清除失败");
            }

        }

        //批量完成
        public ActionResult AllSuccess(string allid) {
            
            var ids = allid.Substring(0, allid.LastIndexOf(','));
            string sqlall = string.Format("update Productionplan set Clear=1,Issue=2  where ProductionPlanID in ({0})", ids);
            try
            {
                //int numsIss = DALHelp.ExecuteNonQuery(sqlall, null);
                int numsIss = ServerOrLit.isNum(LoginController.connType, sqlall);
            }
            catch (Exception)
            {
                return Content("异常");
            }
            return Content("批量清空成功！");
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