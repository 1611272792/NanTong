using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XP_PPS_DAL;
using System.Data;
using ProductionPlanSystem.Help;

namespace ProductionPlanSystem.Controllers
{
    public class InformController : BaseController
    {
        // GET: Inform
        public ActionResult Index()
        {
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

            //读取工序表中的工序
            //cq根据用户管理员权限查看显示的端口
            string str = "";
            if (Role != AppConfig.Role)
            {
                //str = string.Format("select TerminalName,TerminalID from Terminal where UserInfoID='{0}'", userID);
                str = string.Format("select ProcessId,ProcessName from dbo.Process");
            }
            else
            {
                str = "select ProcessId,ProcessName from dbo.Process";
            }
    
            //DataTable dt = DALHelp.ExecuteDataTable(str, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, str);

            if (dt.Rows.Count > 0)
            {
                ViewBag.dt = dt;
            }
            else
            {
                ViewBag.dt = null; 
            }

            string sql2 = "";
 
            
            //通知信息显示
            
            if (Role != AppConfig.Role)
            {
                //sql2 = string.Format("select InformID,TerminalID,UserName,Content,Color,Size,InformTime from Inform i left join UserInfo u on i.UserInfoID=u.UserInfoID where i.UserInfoID='{0}'", userID);
                sql2 = string.Format($@"SELECT t.TerminalID,TerminalName,i.InformID,i.Content,i.Color,i.Size,i.InformTime,u.UserName FROM dbo.Terminal t 
LEFT JOIN dbo.Inform i ON t.TerminalID =i.TerminalID
LEFT JOIN dbo.UserInfo u ON i.UserInfoID=u.UserInfoID where t.UserInfoID LIKE '%,{userID},%' OR t.UserInfoID LIKE '%,{userID}' OR t.UserInfoID LIKE '{userID},%' OR t.UserInfoID='{userID}'");
            }
            else
            {
                sql2 = string.Format(@"SELECT t.TerminalID,TerminalName,i.InformID,i.Content,i.Color,i.Size,i.InformTime,u.UserName FROM dbo.Terminal t 
LEFT JOIN dbo.Inform i ON t.TerminalID =i.TerminalID
LEFT JOIN dbo.UserInfo u ON i.UserInfoID=u.UserInfoID");
            }
            DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sql2);

         
            if (dt2.Rows.Count > 0)
            {
               
                ViewBag.infoShow = dt2;
            }
            else
            {
                ViewBag.infoShow = null;
            }

            return View();
        }
        //更新通知信息
        public ActionResult addInform(string proID, string content,string color,string size,string dataTime) {
            string name;
            if (Session["UserInfoID"] == null)
            {
                return Content("<script>top.location = '/Login/Index';</script>");
            }
            else
            {
                name = Session["UserInfoID"].ToString();
            }
            //查出该工序所有终端id
            string AllTerSql = string.Format($"SELECT TerminalID FROM dbo.Terminal WHERE ProcessId={proID}");
            DataTable allT = ServerOrLit.isDataTable(LoginController.connType, AllTerSql);
            string allTerId = "";
            if (allT.Rows.Count>0)
            {
                for (int i = 0; i < allT.Rows.Count; i++)
                {
                    allTerId +=","+ allT.Rows[i]["TerminalID"].ToString();
                }
                allTerId= allTerId.TrimStart(',');
            }
            else
            {
                allTerId = "0";
            }

            string sql = string.Format($"UPDATE dbo.Inform SET Content='{content}',Color='{color}',Size={size},InformTime='{dataTime}',UserInfoID={name}  WHERE TerminalID IN ({allTerId})" );
            int num = ServerOrLit.isNum(LoginController.connType, sql);
            if (num>0)
            {
                return Content("保存成功");
            }
            else
            {
                return Content("保存失败");
            }  
        }


        //得到需要编辑的内容
        public ActionResult EditInformBySelect(int produID)
        {
            string sqlstr = string.Format("select TerminalID,InformID,UserName,Content,Color,Size,InformTime from Inform i left join UserInfo u on i.UserInfoID=u.UserInfoID where InformID={0}", produID);
            //DataTable dt = DALHelp.ExecuteDataTable(sqlstr, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);

            if (dt.Rows.Count > 0)
            {
                    string produIdter = dt.Rows[0]["TerminalID"].ToString();
                int produId = int.Parse(dt.Rows[0]["InformID"].ToString());             
                string produName = dt.Rows[0]["UserName"].ToString();
                string produVer = dt.Rows[0]["Content"].ToString();
                string produNum = dt.Rows[0]["Color"].ToString();
                string produRea = dt.Rows[0]["Size"].ToString();
              return Content(produId + "." + produName + "." + produIdter + "." + produVer + "." + produNum + "." + produRea);
            }
            else
            {
                return Content("错误提示：没有可编辑的数据！");
            }
        }
        //编辑
        public ActionResult EditProduceline(string TerID,string informID, string informName, string inform, string color, string fontsize)
        {
            //string sql2 = string.Format("select UserInfoID from UserInfo where UserName='{0}'",informName);//查出用户对应的ID
            ////DataTable dtID = DALHelp.ExecuteDataTable(sql2,null);
            //DataTable dtID = ServerOrLit.isDataTable(LoginController.connType, sql2);

            //string userID = dtID.Rows[0]["UserInfoID"].ToString();
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
        
            string sqlstr = string.Format("update Inform set TerminalID='{0}',UserInfoID='{1}',Content='{2}',Color='{3}',Size='{4}',InformTime=CONVERT(varchar(100), GETDATE(), 20)  where InformID={5}", TerID, userID, inform,color,fontsize,informID);
            //int info = DALHelp.ExecuteNonQuery(sqlstr, null);
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

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="procId"></param>
        /// <returns></returns>
        public ActionResult CleanInfo(int InformID) {

            string strSql = string.Format($"UPDATE dbo.Inform SET Content='' WHERE InformID={InformID}");
          int num=  SqlDbHelper.ExecuteNonQuery(strSql);
            if (num>0)
            {
                return Content("清除成功");
            }
            else
            {
                return Content("未清除");
            }
          
        }
        //根据工序显示分布视图
        public ActionResult ShowInfo(int procId) {

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

            //读取工序表中的工序
            //cq根据用户管理员权限查看显示的端口
            string str = "";
            if (Role != AppConfig.Role)
            {
                //str = string.Format("select TerminalName,TerminalID from Terminal where UserInfoID='{0}'", userID);
                str = "select ProcessId,ProcessName from dbo.Process";
            }
            else
            {
                str = "select ProcessId,ProcessName from dbo.Process";
            }

            //DataTable dt = DALHelp.ExecuteDataTable(str, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, str);

            if (dt.Rows.Count > 0)
            {
                ViewBag.dt = dt;
            }
            else
            {
                ViewBag.dt = null;
            }

            string sql2 = "";


            //通知信息显示

            if (Role != AppConfig.Role)
            {
                sql2 = string.Format($@"SELECT t.TerminalID,TerminalName,i.InformID,i.Content,i.Color,i.Size,i.InformTime,u.UserName FROM dbo.Terminal t 
LEFT JOIN dbo.Inform i ON t.TerminalID =i.TerminalID
LEFT JOIN dbo.UserInfo u ON i.UserInfoID=u.UserInfoID  where (t.UserInfoID LIKE '%,{userID},%' OR t.UserInfoID LIKE '%,{userID}' OR t.UserInfoID LIKE '{userID},%' OR t.UserInfoID = '{userID}')");
                if (procId != 0)
                {
                    sql2 += string.Format("  and ProcessId={0}", procId);
                }
                //sql2 = string.Format("select InformID,TerminalID,UserName,Content,Color,Size,InformTime from Inform i left join UserInfo u on i.UserInfoID=u.UserInfoID where i.UserInfoID='{0}'", userID);
            }
            else
            {
                sql2 = string.Format(@"SELECT t.TerminalID,TerminalName,i.InformID,i.Content,i.Color,i.Size,i.InformTime,u.UserName FROM dbo.Terminal t 
LEFT JOIN dbo.Inform i ON t.TerminalID =i.TerminalID
LEFT JOIN dbo.UserInfo u ON i.UserInfoID=u.UserInfoID");
                if (procId!=0)
                {
                    sql2 += string.Format("  WHERE ProcessId={0}",procId);
                }
        
            }
            DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sql2);


            if (dt2.Rows.Count > 0)
            {

                ViewBag.infoShow = dt2;
            }
            else
            {
                ViewBag.infoShow = null;
            }

            return PartialView("_showInfo");
        }

    }
}