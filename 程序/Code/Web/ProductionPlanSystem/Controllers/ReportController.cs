﻿using System.Collections.Generic;
using System.Web.Mvc;
using XP_PPS_DAL;
using System.Data;
using XP_PPS_Model;
using Newtonsoft.Json;
using ProductionPlanSystem.Help;
using System.Data.SqlClient;

namespace ProductionPlanSystem.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Report
        public ActionResult Index()
        {
            //判断是管理员还是普通用户
            string userid;
            string Role1;
            if (Session["UserInfoID"] == null || Session["Role"] == null)
            {
                return Content("<script>top.location = '/Login/Index';</script>");
            }
            else
            {
                userid = Session["UserInfoID"].ToString();//登录人id
                Role1 = Session["Role"].ToString();
            }
            string Role = PeopleRole(userid, Role1);

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

            #region 项目下拉框
            //首页加载的时候给项目下拉框赋值
            DataTable xm = SqlDbHelper.ExecuteDataTable($"EXEC dbo.[proc_Project] @strType = 'pjinfo'");
            if (xm?.Rows.Count > 0)
            {
                ViewBag.pjinfo = xm;
            }
            else
            {
                ViewBag.pjinfo = null;
            }
            #endregion
            //总行数
            string countsql = "";
            countsql = string.Format($"EXEC Pro_AllInfo @strType='SelectCount'");
 
            DataTable count = ServerOrLit.isDataTable(LoginController.connType, countsql);
            if (count?.Rows.Count > 0)
            {
                ViewBag.count = count.Rows[0].Field<int>("Count");
            }
            else
            {
                ViewBag.count = 0;
            }

            //第一次加载数据时的表头数据
            string sqltitle = string.Format($@"EXEC [proc_ProcessSet] @strType='FirstTitle'");
            //DataTable dt2 = DALHelp.ExecuteDataTable(sqltitle, null);
            DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sqltitle);

            if (dt2?.Rows.Count > 0)
            {
                ViewBag.title = dt2;
            }
            else
            {
                ViewBag.title = null;
            }
            return View();
        }

        #region 通过工序Id获取其对应的终端
        /// <summary>
        /// 通过工序Id获取其对应的终端
        /// </summary>
        /// <param name="ProcessId">终端Id</param>
        /// <returns></returns>
        public string GetProcessTerminal(string ProcessId)
        {
            string userid;
            string Role1;
            if (Session["UserInfoID"] == null || Session["Role"] == null)
            {
                return null;
            }
            else
            {
                userid = Session["UserInfoID"].ToString();//登录人id
                Role1 = Session["Role"].ToString();
            }
            string Role = PeopleRole(userid, Role1);
            string sqlTerminal = "";
            if (Role == "管理员")
            {
                sqlTerminal = string.Format($@"EXEC [proc_ProcessSet] @strType='getTerminal',@ProcessId='{ProcessId}'");
            }
            else if (Role == "产线负责人")
            {
                sqlTerminal = string.Format($@"EXEC [proc_ProcessSet] @strType='getTerminal',@ProcessId='{ProcessId}',@userid='{userid}'");
            }
            DataTable Terminal = ServerOrLit.isDataTable(LoginController.connType, sqlTerminal);
            string Json = "";
            if (Terminal?.Rows.Count > 0)
            {
                Json = JsonConvert.SerializeObject(Terminal);
            }
            return Json;
        }
        #endregion

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="line">终端Id</param>
        /// <param name="ProjectId">项目Id</param>
        /// <param name="ProductId">产品Id</param>
        /// <param name="pagecount">当前页码数</param>
        /// <param name="size">每页显示数</param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult SelectAll(string starttime, string endtime, string line, string ProjectNo, string ProjectName, string ProductName, string ProductSize,string Customer, string pagecount, string size)
        {

            ViewBag.title = GetTitle(line);
           //判断是管理员还是普通用户
           string userid;
            string Role1;
            if (Session["UserInfoID"] == null || Session["Role"] == null)
            {
                return Content("<script>top.location = '/Login/Index';</script>");
            }
            else
            {
                userid = Session["UserInfoID"].ToString();//登录人id
                Role1 = Session["Role"].ToString();
            }

            DataTable dt = GetInfo(starttime, endtime, ProjectNo, ProjectName, ProductName, ProductSize,Customer, pagecount, size);
            DataTable dt1 = ExcelData(starttime, endtime, ProjectNo, ProjectName, ProductName, ProductSize,Customer, pagecount, size);
            if (dt?.Rows.Count > 0)
            {
                ViewBag.selectdata = dt;
                ViewBag.extable = dt;
                Session["datatable1"] = dt1;
            }
            else
            {
                ViewBag.selectdata = null;
                Session["datatable"] = null;
            }

            //Response.Buffer = true;
            //Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            //Response.Expires = 0;
            //Response.CacheControl = "no-cache";
            return PartialView("_Report", dt);
        }

        /// <summary>
        /// 获取图表信息返回json数据
        /// </summary>
        /// <param name="starttime">开始时间</param>        
        /// <param name="endtime">结束时间</param>
        /// <param name="line">终端Id</param>
        /// <param name="ProjectId">项目Id</param>
        /// <param name="ProductId">产品Id</param>
        /// <param name="pagecount">当前页码数</param>
        /// <param name="size">每页显示数</param>
        /// <returns></returns>  n     
        [HttpGet]
        public string SelectAll1(string starttime, string endtime, string line, string ProjectNo, string ProjectName, string ProductName, string ProductSize, string Customer, string pagecount, string size)
        {
            ViewBag.title = GetTitle(line);
            //判断是管理员还是普通用户
            string userid;
            string Role1;
            if (Session["UserInfoID"] == null || Session["Role"] == null)
            {
                return "<script>top.location = '/Login/Index';</script>";
            }
            else
            {
                userid = Session["UserInfoID"].ToString();//登录人id
                Role1 = Session["Role"].ToString();
            }
            DataTable dt = GetInfo(starttime, endtime, ProjectNo, ProjectName, ProductName, ProductSize,Customer, pagecount, size);
            string Json = JsonConvert.SerializeObject(dt);
            return Json;
        }

        /// <summary>
        /// 根据不同的终端ID获取其区域对应的表头
        /// </summary>
        /// <param name="line">终端ID</param>
        /// <returns></returns>
        public DataTable GetTitle(string line)
        {
            string sqltitle = "";
            if (line == "0")
            {
                sqltitle = string.Format($"EXEC AboutSelect @type='NoTerminalId'");
            }
            else
            {
                sqltitle = string.Format($"EXEC AboutSelect @type='getAllocationName',@TerminalId={line}");

            }

            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqltitle);

            if (dt?.Rows.Count > 0)
            {
                return dt;

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 多条件查询获取数据
        /// </summary>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="ProjectId">项目Id</param>
        /// <param name="ProductId">产品Id</param>
        /// <param name="pagecount">当前页</param>
        /// <param name="size">每页显示数</param>
        /// <returns></returns>
        public DataTable GetInfo(string starttime, string endtime, string ProjectNo, string ProjectName, string ProductName, string ProductSize, string Customer, string pagecount, string size)
        {
            //分页
            string mysqls = "";//总数据
            mysqls = string.Format($"EXEC Pro_AllInfo @strType='SelectAll',@ISTime='{starttime}',@IETime='{endtime}',@ProjectNo='{ProjectNo}',@ProjectName='{ProjectName}',@ProductName='{ProductName}',@ProductSizeName='{ProductSize}',@Customer='{Customer}',@pagecount={pagecount},@size={size}");

            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, mysqls);
            if (dt?.Rows.Count>0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public DataTable ExcelData(string starttime, string endtime, string ProjectNo, string ProjectName, string ProductName, string ProductSize, string Customer, string pagecount, string size)
        {
            //分页
            string mysqls = "";//总数据
            mysqls = string.Format($"EXEC Pro_AllInfo @strType='excel1',@ISTime='{starttime}',@IETime='{endtime}',@ProjectNo='{ProjectNo}',@ProjectName='{ProjectName}',@ProductName='{ProductName}',@ProductSizeName='{ProductSize}',@Customer='{Customer}',@pagecount={pagecount},@size={size}");

            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, mysqls);
            if (dt?.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }       

        /// <summary>
        /// 判断是否为管理员或产线负责人
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <param name="Role1">Role1</param>
        /// <returns></returns>
        public string PeopleRole(string userid, string Role1)
        {
            string IsPeople = "";
            //如果用户账号为admin则是管理员
            if (Role1 == Help.AppConfig.Role)
            {
                IsPeople = "管理员";
            }
            else
            {
                //如果此用户有负责的产线则为产线负责人
                string sqlstr2 = string.Format($"EXEC [proc_ProcessSet] @strType='IsAdmin',@userid='{userid}'");
                DataTable linedt = ServerOrLit.isDataTable(LoginController.connType, sqlstr2);

                //DataTable linedt = DALHelp.ExecuteDataTable(sqlstr2, null);
                if (linedt?.Rows.Count > 0)
                {
                    IsPeople = "产线负责人";
                    ViewBag.Role = 1;
                }
            }
            return IsPeople;
        } 
    }
}