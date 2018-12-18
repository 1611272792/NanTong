using ProductionPlanSystem.Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem.Controllers
{
    public class ProcessSetController : Controller
    {
        // GET: ProcessSet
        public ActionResult Index()
        {
            
            //获取区域信息
            DataTable info = SqlDbHelper.ExecuteDataTable("EXEC proc_ProcessSet @strType='process'");
            if (info?.Rows.Count>0)
            {
                for (int i = 0; i < info.Rows.Count; i++)
                {
                    if (info.Rows[i]["FieldsId"].ToString()!="暂无")
                    {
                    info.Rows[i]["FieldsId"] = getFieldsName(info.Rows[i]["FieldsId"].ToString());
                }
                 
                }
              
                ViewBag.pinfo = info;
            }
            else
            {
                ViewBag.pinfo = null;
            }
            //获取工序信息
            //string sqlStr = string.Format("SELECT AllocationID,AllocationName,AllocationTitle FROM dbo.Allocation WHERE Type='pro'");
            string sqlStr = string.Format("EXEC proc_ProcessSet @strType='SelectProcess'");
            DataTable ds = SqlDbHelper.ExecuteDataTable(sqlStr);
            if (ds?.Rows.Count>0)
            {
                ViewBag.pro = ds;
            }
            else
            {
                ViewBag.pro = null;
            }
            return View();
        }

        #region 添加区域
        /// <summary>
        /// 添加区域
        /// </summary>
        /// <param name="ProcessName">区域名</param>
        /// <param name="NextProcessId">下一个区域id</param>
        /// <returns></returns>
        public ActionResult AddProcess(string ProcessName, string NextProcessId,string FieldsID)
        {
            #region 验证
            if (string.IsNullOrWhiteSpace(ProcessName.Trim()))
            {
                return Content("区域名不能为空！");
            }
            if (string.IsNullOrWhiteSpace(FieldsID.Trim()))
            {
                return Content("请选择工序！");
            }
            #endregion

            FieldsID= FieldsID.TrimStart(',');
            SqlParameter[] pars = new SqlParameter[4];
            pars[0] = new SqlParameter("@strType", "addProcess");
            pars[1] = new SqlParameter("@ProcessName", ProcessName);
            pars[2] = new SqlParameter("@NextProcessId", NextProcessId);
            pars[3] = new SqlParameter("@FieldsID", FieldsID);
            int rows;
            int info = SqlDbHelper.RunProcedure("proc_ProcessSet", pars, out rows);
            if (rows > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("添加失败");
            }
        }
        #endregion

        #region 删除区域
        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="ProcessId">区域Id</param>
        /// <returns></returns>
        public ActionResult DelProcess(string ProcessId)
        {
            if (string.IsNullOrWhiteSpace(ProcessId))
            {
                return Content("ProcessId Is Null");
            }
            string ProcessIds = ProcessId.Substring(0, ProcessId.Length - 1);
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@strType", "delProcess");
            pars[1] = new SqlParameter("@ProcessIds", ProcessIds);
            //获取第一个工序中的状态信息
            int rows;
            int delcstate = SqlDbHelper.RunProcedure("proc_ProcessSet", pars, out rows);
            if (rows > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 获取需要编辑的区域信息
        /// <summary>
        /// 获取需要编辑的区域信息
        /// </summary>
        /// <param name="PId">区域Id</param>
        /// <returns></returns>
        public ActionResult GetUpdProcessInfo(int PId)
        {
            if (string.IsNullOrWhiteSpace(PId.ToString()))
            {
                return Content("区域id不能为空");
            }
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@strType", "GetUpdProcessInfo");
            pars[1] = new SqlParameter("@ProcessId", PId);
            //获取第一个工序中的状态信息
            DataSet ProcessInfo = SqlDbHelper.RunProcedure("proc_ProcessSet", pars, "GetUpdProcessInfo");
            if (ProcessInfo?.Tables.Count > 0 && ProcessInfo.Tables[0]?.Rows.Count > 0)
            {
                string ProcessId = ProcessInfo.Tables[0].Rows[0]["ProcessId"].ToString();
                string ProcessName = ProcessInfo.Tables[0].Rows[0]["ProcessName"].ToString();
                string NextProcessId = ProcessInfo.Tables[0].Rows[0]["NextProcessId"].ToString();
                string FieldsId= ProcessInfo.Tables[0].Rows[0]["FieldsId"].ToString();
                return Content(ProcessId + "." + ProcessName + "." + NextProcessId + "." + FieldsId);
            }
            else
            {
                return null;
            }
        }
        #endregion


        #region 编辑区域
        /// <summary>
        /// 编辑区域
        /// </summary>
        /// <param name="ProcessName">区域名称</param>
        /// <param name="NextProcessId">下个区域Id</param>
        /// <returns></returns>
        public ActionResult EditProcess(int ProcessId,string ProcessName, int NextProcessId,string FieldsID)
        {
            #region 验证
            if (string.IsNullOrWhiteSpace(ProcessId.ToString().Trim()))
            {
                return Content("区域id不能为空");
            }
            if (string.IsNullOrWhiteSpace(ProcessName.Trim()))
            {
                return Content("区域名不能为空");
            }
            #endregion
            FieldsID = FieldsID.TrimStart(',');
            SqlParameter[] pars = new SqlParameter[5];
            pars[0] = new SqlParameter("@strType", "editProcess");
            pars[1] = new SqlParameter("@ProcessId", ProcessId);
            pars[2] = new SqlParameter("@ProcessName", ProcessName);
            pars[3] = new SqlParameter("@NextProcessId", NextProcessId);
            pars[4] = new SqlParameter("@FieldsID", FieldsID);
            int rows;
            int info = SqlDbHelper.RunProcedure("proc_ProcessSet", pars, out rows);
            if (rows > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion


        /// <summary>
        /// 获取区域id对应的区域名称
        /// </summary>
        /// <param name="fieldsId"></param>
        /// <returns></returns>
        public string getFieldsName(string fieldsId) {
            string FieldsName = "";
            string strSql = string.Format($"SELECT AllocationTitle FROM dbo.Allocation WHERE AllocationID IN ({fieldsId})");
            //string strSql = string.Format($"EXEC proc_ProcessSet @strType='GetProName',@someField={fieldsId}");
            DataTable ds = SqlDbHelper.ExecuteDataTable(strSql);
            if (ds?.Rows.Count>0)
            {
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    FieldsName += ds.Rows[i]["AllocationTitle"]+",";
                } 

            }
            return FieldsName.TrimEnd(',');
        }
    }
}