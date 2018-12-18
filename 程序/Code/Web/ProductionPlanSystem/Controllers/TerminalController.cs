using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using XP_PPS_Model;
using XP_PPS_DAL;
using ProductionPlanSystem.Help;
using System.Data.SqlClient;

namespace ProductionPlanSystem.Controllers
{
    public class TerminalController : BaseController
    {
        // GET: Terminal
        public ActionResult Index()
        {
           
            //获取所有工序
            string procSql = string.Format("SELECT * FROM dbo.Process");
            DataTable proctd = ServerOrLit.isDataTable(LoginController.connType, procSql);
            if (proctd.Rows.Count>0)
            {
                ViewBag.proc = proctd;
            }
            else
            {
                ViewBag.proc = null;
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


            string sqlstr2 = "";//获取责任人
            if (Role != AppConfig.Role)
            {
                sqlstr2 = string.Format($"select UserName,UserInfoID from UserInfo  where UserInfoID LIKE '%,{userID},%'   OR UserInfoID LIKE '%,{userID}' OR UserInfoID LIKE '{userID},%' OR UserInfoID='{userID}'");
            }
            else
            {
                sqlstr2 = string.Format("select UserName,UserInfoID from UserInfo");
            }


            //DataTable td = DALHelp.ExecuteDataTable(sqlstr2, null);
            DataTable td = ServerOrLit.isDataTable(LoginController.connType, sqlstr2);

            if (td.Rows.Count > 0)
            {
                ViewBag.adduser = td;
            }
                  
            string sqlstr = null;
            if (Role != AppConfig.Role) 
            {
                ViewBag.Role = 1;
                   sqlstr = string.Format($"select TerminalID,TerminalName, UserInfoID as UserName,TerminalPwd,ProcessId,PageTime  from Terminal  where (UserInfoID LIKE '%,{userID},%'   OR UserInfoID LIKE '%,{userID}' OR UserInfoID LIKE '{userID},%' OR UserInfoID='{userID}')");
            }
            else//当用户为管理员时
            {
                ViewBag.Role = 2;
                sqlstr = string.Format("select TerminalID,TerminalName, UserInfoID as UserName,TerminalPwd,ProcessId,PageTime  from Terminal ");
            }
            //DataTable dt = DALHelp.ExecuteDataTable(sqlstr, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);
          
            if (dt.Rows.Count > 0)
            {
                //把用户id转换为用户名
                string userSql = "";//查询对应的用户名
                DataTable userDt = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] userName = dt.Rows[i]["UserName"].ToString().Split(',');
                    dt.Rows[i]["UserName"] = "";
                    for (int j = 0; j < userName.Length; j++)
                    {
                        userSql = string.Format($"SELECT UserName FROM dbo.UserInfo WHERE UserInfoID={userName[j]}");
                        userDt = ServerOrLit.isDataTable(LoginController.connType, userSql);
                        if (userDt.Rows.Count>0)
                        {
                            userName[j] = userDt.Rows[0]["UserName"].ToString();
                        }
                        else
                        {
                            userName[j] = "";
                        }
                        dt.Rows[i]["UserName"] +=","+ userName[j];
                    }
                    dt.Rows[i]["UserName"] = dt.Rows[i]["UserName"].ToString().TrimStart(',').TrimEnd(',');//去掉前后符号
                }
                ViewBag.terminal = dt;
            }
            else
            {
                ViewBag.terminal = null;
            }
            return View();
        }

        /// <summary>
        /// 主页面分布视图
        /// </summary>
        /// <param name="selectProcID"></param>
        /// <returns></returns>
        public ActionResult Index1(string selectProcID)
        {
            //获取所有工序
            string procSql = string.Format("SELECT * FROM dbo.Process");
            DataTable proctd = ServerOrLit.isDataTable(LoginController.connType, procSql);
            if (proctd.Rows.Count > 0)
            {
                ViewBag.proc = proctd;
            }
            else
            {
                ViewBag.proc = null;
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


            string sqlstr2 = "";//获取责任人
            if (Role != AppConfig.Role)
            {
                sqlstr2 = string.Format("select UserName,UserInfoID from UserInfo  where UserInfoID={0}", userID);
            }
            else
            {
                sqlstr2 = string.Format("select UserName,UserInfoID from UserInfo");
            }


            //DataTable td = DALHelp.ExecuteDataTable(sqlstr2, null);
            DataTable td = ServerOrLit.isDataTable(LoginController.connType, sqlstr2);

            if (td.Rows.Count > 0)
            {
                ViewBag.adduser = td;
            }

            string sqlstr = null;
            if (Role != AppConfig.Role)
            {
                ViewBag.Role = 1;
                sqlstr = string.Format($"select TerminalID,TerminalName, UserInfoID as UserName,TerminalPwd,ProcessId,PageTime  from Terminal  where (UserInfoID LIKE '%,{userID},%'   OR UserInfoID LIKE '%,{userID}' OR UserInfoID LIKE '{userID},%' OR UserInfoID='{userID}')");
            }
            else//当用户为管理员时
            {
                ViewBag.Role = 2;
                sqlstr = string.Format($"select TerminalID,TerminalName, UserInfoID as UserName,TerminalPwd,ProcessId,PageTime  from Terminal  where 1 = 1 ");
            }
            if (selectProcID != "0")  //如果为0则查询全部，否则如下
            {
                sqlstr += string.Format($"  and  ProcessId={selectProcID}");
            }
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);

            if (dt.Rows.Count > 0)
            {
                //把用户id转换为用户名
                string userSql = "";//查询对应的用户名
                DataTable userDt = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] userName = dt.Rows[i]["UserName"].ToString().Split(',');
                    dt.Rows[i]["UserName"] = "";
                    for (int j = 0; j < userName.Length; j++)
                    {
                        userSql = string.Format($"SELECT UserName FROM dbo.UserInfo WHERE UserInfoID={userName[j]}");
                        userDt = ServerOrLit.isDataTable(LoginController.connType, userSql);
                        if (userDt.Rows.Count > 0)
                        {
                            userName[j] = userDt.Rows[0]["UserName"].ToString();
                        }
                        else
                        {
                            userName[j] = "";
                        }
                        dt.Rows[i]["UserName"] += "," + userName[j];
                    }
                    dt.Rows[i]["UserName"] = dt.Rows[i]["UserName"].ToString().TrimStart(',').TrimEnd(',');//去掉前后符号
                }
                ViewBag.terminal = dt;
            }
            else
            {
                ViewBag.terminal = null;
            }
            return PartialView("_TerAndProc");
        }
        
        //删除
        public ActionResult DeleteTerminal(string termId)
        {
            var termid = termId.Substring(0, termId.LastIndexOf(','));
            string sqlstr = string.Format("delete from Terminal where TerminalID in({0})", termid);
            //int info = DALHelp.ExecuteNonQuery(sqlstr,null);
            int info = ServerOrLit.isNum(LoginController.connType, sqlstr);
            if (info > 0 )
            {
                updateTermId(termid);

                return Content("删除成功");
            }
            else
            {
                return Content("删除失败");
            }
        }

        /// <summary>
        /// 删除通知发放终端
        /// chenqi
        /// 2018.04.18
        /// </summary>
        /// <param name="termId">删除终端id</param>
        public void updateTermId(string termId)
        {
            
            string[] strs = termId.Split(',');
            foreach(string str in strs)
            {
                string sql = string.Format("select InformId,TerminalID from Inform where TerminalID like '%{0}%'", str);
                //DataTable dt = DALHelp.ExecuteDataTable(sql, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);
                if (dt == null) return;
                if (dt.Rows.Count == 0) return;

                for (int i=0;i<dt.Rows.Count;i++)
                {
                    string strsTermId = dt.Rows[i][1].ToString();
                    if (strsTermId.Contains(str+','))
                    {
                        strsTermId = strsTermId.Replace(str + ",", "");
                    }
                    else
                    {
                        strsTermId = strsTermId.Replace(str, "").TrimEnd(',').TrimStart(',');
                    }

                    string updtesql = string.Format("update Inform set TerminalID= '{0}' where InformId={1}", strsTermId,dt.Rows[i][0].ToString());
                    //int info = DALHelp.ExecuteNonQuery(updtesql, null);
                    int info = ServerOrLit.isNum(LoginController.connType, updtesql);
                    if (info > 0)
                    {
                        //dt.Rows.RemoveAt(i);
                        //DataRow dr = dt.NewRow();
                        //dr[0] = dt.Rows[i][0];
                        //dr[1]=
                        //dt.Rows.InsertAt
                    }
                }
                
            }

        }
        //得到需要编辑的内容 
        public ActionResult EditTerminalBySelect(int terId)
        {
            string sqlstr = string.Format("select TerminalID,TerminalName,UserInfoID,TerminalPwd,ProcessId,PageTime  from Terminal  where TerminalID={0}", terId);
            //DataTable dt = DALHelp.ExecuteDataTable(sqlstr,null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);
            if (dt.Rows.Count > 0)
            {
                int terminId = int.Parse(dt.Rows[0]["TerminalId"].ToString());
                string terminName = dt.Rows[0]["TerminalName"].ToString();
                string userinfid = dt.Rows[0]["UserInfoID"].ToString();
                string terminPwd = dt.Rows[0]["TerminalPwd"].ToString();
                string processId = dt.Rows[0]["ProcessId"].ToString();
                string PageTime = dt.Rows[0]["PageTime"].ToString();
                return Content(terminId + "." + terminName + "." + userinfid + "." + terminPwd+"."+ processId + "." + PageTime);
            }
            else
            {
                return Content("no");
            }
        }
        //编辑
        public ActionResult EditTerminal(int terId, string terPwd,string terName,string userID, string ProcId,int PageTime)
        {
            terPwd = "";//不需要密码的时候
            string sqlstr = string.Format($"UPDATE dbo.Terminal SET   TerminalName='{terName}',UserInfoID='{userID}',TerminalPwd='{terPwd}',ProcessId={ProcId},PageTime={PageTime} WHERE TerminalID={terId}");
            int info = ServerOrLit.isNum(LoginController.connType, sqlstr);

            if (info > 0)
            {
                return Content("修改成功");
            }
            else
            {
                return Content("修改失败");
            }

        }

        //添加
        public ActionResult AddTerminal(string TerminalName, string userinfo,string termPwd,string procId,int PageTime)
        {
            //try
            //{
            termPwd = "";//不需要密码
            int info = 0;
            SqlParameter[] par = new SqlParameter[5];
            par[0] = new SqlParameter("@TerminalName", TerminalName);
            par[1] = new SqlParameter("@userinfo", userinfo); 
            par[2] = new SqlParameter("@termPwd", termPwd); 
            par[3] = new SqlParameter("@procId", procId); 
            par[4] = new SqlParameter("@PageTime", PageTime);
            SqlDbHelper.RunProcedure("proc_UpdateTerminal", par, out info);
                if (info > 0)
                {
                    return Content("添加成功");
                }
                else
                {
                    return Content("添加失败");
                }
            //}
            //catch (Exception)
            //{

            //    return Content("添加失败");
            //}


        }
    }
}