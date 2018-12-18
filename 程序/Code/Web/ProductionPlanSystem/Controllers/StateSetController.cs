using ProductionPlanSystem.Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XP_PPS_Model.Model;

namespace ProductionPlanSystem.Controllers
{
    public class StateSetController : Controller
    {
        // GET: StateSet
        public ActionResult Index()
        {
            SqlParameter[] values = new SqlParameter[1];
            values[0] = new SqlParameter("@strType", "process");
            //获取工序信息
            DataSet info = SqlDbHelper.RunProcedure("proc_StateSet", values, "process");
            if (info.Tables.Count > 0 && info.Tables[0]?.Rows.Count > 0)
            {
                ViewBag.pinfo = info.Tables[0];
                //首页加载信息(StateInfo方法)
                DataTable dt = StateInfo(0);
                ViewBag.pstate = dt;
            }
            else
            {
                ViewBag.pinfo = null;
            }

            return View();
        }

        #region 根据ProcessId获取对应工序的状态信息
        /// <summary>
        /// 根据ProcessId获取对应工序的状态信息
        /// </summary>
        /// <param name="ProcessId">所属工序Id</param>
        /// <returns></returns>
        public DataTable StateInfo(int ProcessId)
        {
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@strType", "pstate");
            pars[1] = new SqlParameter("@ProcessId", ProcessId);
            //获取第一个工序中的状态信息
            DataSet procstate = SqlDbHelper.RunProcedure("proc_StateSet", pars, "pstate");
            if (procstate?.Tables.Count > 0 && procstate.Tables[0]?.Rows.Count > 0)
            {
                return procstate.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 分部视图_ProcessState根据ProcessId获取对应工序的状态信息
        /// <summary>
        /// 分部视图_ProcessState根据ProcessId获取对应工序的状态信息
        /// </summary>
        /// <param name="ProcessId">所属工序Id</param>
        /// <returns></returns>
        public ActionResult ShareState(int ProcessId)
        {
            DataTable dt = StateInfo(ProcessId);
            ViewBag.pstate = dt;
            return PartialView("_ProcessState", dt);
        }
        #endregion

        #region 添加工序状态
        /// <summary>
        /// 添加工序状态
        /// </summary>
        /// <param name="StateName">状态名</param>
        /// <param name="ProcessId">所属工序Id</param>
        /// <param name="StateSerial">序号</param>
        /// <param name="StateColor">颜色</param>
        /// <returns></returns>
        public ActionResult AddProcessState(string StateName, int ProcessId, int StateSerial, string StateColor)
        {
            #region 验证
            if (string.IsNullOrWhiteSpace(StateName.Trim()))
            {
                return Content("StateName Is Null");
            }
            if (string.IsNullOrWhiteSpace(ProcessId.ToString().Trim()))
            {
                return Content("ProcessId Is Null");
            }
            if (string.IsNullOrWhiteSpace(StateSerial.ToString().Trim()))
            {
                return Content("StateSerial Is Null");
            }
            if (string.IsNullOrWhiteSpace(StateColor.Trim()))
            {
                return Content("StateColor Is Null");
            }
            #endregion

            SqlParameter[] pars = new SqlParameter[5];
            pars[0] = new SqlParameter("@strType", "addProcessState");
            pars[1] = new SqlParameter("@StateName", StateName);
            pars[2] = new SqlParameter("@ProcessId", ProcessId);
            pars[3] = new SqlParameter("@StateSerial", StateSerial);
            pars[4] = new SqlParameter("@StateColor", StateColor);
            int rows;
            int info = SqlDbHelper.RunProcedure("proc_StateSet", pars, out rows);
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

        #region 删除状态
        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="StateId">状态Id</param>
        /// <returns></returns>
        public ActionResult DelProcessState(string StateId)
        {
            if (string.IsNullOrWhiteSpace(StateId))
            {
                return Content("StateId Is Null");
            }
            string StateIds = StateId.Substring(0, StateId.Length - 1);
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@strType", "delstate");
            pars[1] = new SqlParameter("@StateIds", StateIds);
            //获取第一个工序中的状态信息
            int rows;
            int delcstate = SqlDbHelper.RunProcedure("proc_StateSet", pars, out rows);
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

        #region 获取需要编辑的状态信息
        /// <summary>
        /// 获取需要编辑的状态信息
        /// </summary>
        /// <param name="SId">状态Id</param>
        /// <returns></returns>
        public ActionResult GetUpdStateInfo(int SId)
        {
            if (string.IsNullOrWhiteSpace(SId.ToString()))
            {
                return Content("StateId Is Null");
            }
            SqlParameter[] pars = new SqlParameter[2];
            pars[0] = new SqlParameter("@strType", "GetUpdStateInfo");
            pars[1] = new SqlParameter("@StateId", SId);
            //获取第一个工序中的状态信息
            DataSet StateInfo = SqlDbHelper.RunProcedure("proc_StateSet", pars, "GetUpdStateInfo");
            if (StateInfo?.Tables.Count > 0 && StateInfo.Tables[0]?.Rows.Count > 0)
            {
                string StateId = StateInfo.Tables[0].Rows[0]["StateId"].ToString();
                string ProcessId = StateInfo.Tables[0].Rows[0]["ProcessId"].ToString();
                string StateName = StateInfo.Tables[0].Rows[0]["StateName"].ToString();
                string StateSerial = StateInfo.Tables[0].Rows[0]["StateSerial"].ToString();
                string StateColor = StateInfo.Tables[0].Rows[0]["StateColor"].ToString();
                return Content(StateId + "," + ProcessId + "," + StateName + "," + StateSerial + "," + StateColor);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 编辑工序状态
        /// <summary>
        /// 编辑工序状态
        /// </summary>
        /// <param name="StateId">状态Id</param>
        /// <param name="StateName">状态名称</param>
        /// <param name="StateSerial">状态顺序</param>
        /// <param name="StateColor">状态颜色</param>
        /// <returns></returns>
        public ActionResult EditProcessState(int StateId, string StateName, int StateSerial, string StateColor)
        {
            #region 验证
            if (string.IsNullOrWhiteSpace(StateId.ToString().Trim()))
            {
                return Content("StateId Is Null");
            }
            if (string.IsNullOrWhiteSpace(StateName.ToString().Trim()))
            {
                return Content("StateName Is Null");
            }
            if (string.IsNullOrWhiteSpace(StateSerial.ToString().Trim()))
            {
                return Content("StateSerial Is Null");
            }
            if (string.IsNullOrWhiteSpace(StateColor.Trim()))
            {
                return Content("StateColor Is Null");
            }
            #endregion
            SqlParameter[] pars = new SqlParameter[5];
            pars[0] = new SqlParameter("@strType", "editProcessState");
            pars[1] = new SqlParameter("@StateId", StateId);
            pars[2] = new SqlParameter("@StateName", StateName);
            pars[3] = new SqlParameter("@StateSerial", StateSerial);
            pars[4] = new SqlParameter("@StateColor", StateColor);
            int rows;
            int info = SqlDbHelper.RunProcedure("proc_StateSet", pars, out rows);
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
    }
}