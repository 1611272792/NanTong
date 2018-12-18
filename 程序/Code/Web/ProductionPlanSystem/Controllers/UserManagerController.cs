using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using XP_PPS_DAL;
using System.Data.SQLite;
using XP_PPS_Model;
using ProductionPlanSystem.Help;

namespace ProductionPlanSystem.Controllers
{
    public class UserManagerController : BaseController
    {
        // GET: UserManager
        public ActionResult Index()
        {
            string Role;
            string userid;
            if (Session["Role"] == null || Session["UserInfoID"] == null)
            {
                return Content("<script>top.location = '/Login/Index';</script>");
            }
            else
            {
                Role = Session["Role"].ToString();
                userid = Session["UserInfoID"].ToString();
            }
            //用户组
            string sqlstr = string.Format("select * from UserGroups");
            //DataTable dt = DALHelp.ExecuteDataTable(sqlstr, null);
            //DataTable dt = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr) : DALHelp.ExecuteDataTable(sqlstr, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);
            if (dt.Rows.Count > 0)
            {
                ViewBag.userGroup = dt;
            }
            else
            {
                ViewBag.userGroup = null;
            }
            //用户
            string sqlstr2 = "";
            if (Role == Help.AppConfig.Role)
            {//管理员
                sqlstr2 = string.Format("select * from UserInfo");
                //DataTable dt2 = DALHelp.ExecuteDataTable(sqlstr2, null);
                //DataTable dt2 = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr2) : DALHelp.ExecuteDataTable(sqlstr2, null);
                DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sqlstr2);
                if (dt2.Rows.Count > 0)
                {
                    ViewBag.user = dt2;
                }
                else
                {
                    ViewBag.user = null;
                }
                return View();
            }
            else
            {//普通用户
                string sqlstr3 = string.Format("select * from UserInfo where UserInfoID={0}", userid);
                //DataTable dt3 = DALHelp.ExecuteDataTable(sqlstr3, null);
                //DataTable dt3 = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr3) : DALHelp.ExecuteDataTable(sqlstr3, null);
                DataTable dt3 = ServerOrLit.isDataTable(LoginController.connType, sqlstr3);
                if (dt3.Rows.Count > 0)
                {
                    ViewBag.Role = 1;
                    ViewBag.user = dt3;
                }
                else
                {
                    ViewBag.user = null;
                }
                return View();
            }
        }


        //添加用户
        public ActionResult AddUserManager(string userId, string userName, string userPwd, string idCard, string userGroupsID)
        {
            if (userId == "" || userName == "" || userPwd == "" || userGroupsID == "")
            {
                return Content("添加失败");
            }           
            else
            {
                string isin = string.Format("select LoginAccount from UserInfo where LoginAccount='{0}'", userId);
                //DataTable dt = DALHelp.ExecuteDataTable(isin,null);
                //DataTable dt = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(isin) : DALHelp.ExecuteDataTable(isin, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, isin);
                if (dt.Rows.Count>0)
                {
                    return Content("此用户已存在，请重新注册！");
                }
                else
                {
                    UserInfo ui = new UserInfo();
                    string sqlstr = string.Format("insert into UserInfo (LoginAccount,LoginPassword,UserName,UserGroupsID,IDCard) values('{0}','{1}','{2}','{3}','{4}')", userId, userPwd, userName, userGroupsID, idCard);
                    //int sum = DALHelp.ExecuteNonQuery(sqlstr, null);
                    //int sum = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteNonQuery(sqlstr) : DALHelp.ExecuteNonQuery(sqlstr, null);
                    int sum = ServerOrLit.isNum(LoginController.connType, sqlstr);
                    if (sum > 0)
                    {
                        return Content("添加成功");
                    }
                    else
                    {
                        return Content("添加失败");
                    }
                }            
            }
        }

        //删除
        public ActionResult DeleteUserManager(string uid)
        {
            if (uid == "")
            {
                return Content("删除失败");
            }
            else
            {
                uid = uid.Substring(0, uid.Length - 1);
                var a = uid.Split(',');
                string isin = "select UserInfoID from Terminal where UserInfoID in(";
                for (int i = 0; i < a.Length; i++)
                {
                    if (i != a.Length - 1)
                    {
                        isin += "'" + a[i] + "',";
                    }
                    else
                    {
                        isin += "'" + a[i] + "'";
                    }
                }
                isin += ")";
                //DataTable dt = DALHelp.ExecuteDataTable(isin,null);
                //DataTable dt = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(isin) : DALHelp.ExecuteDataTable(isin, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, isin);
                if (dt.Rows.Count > 0)
                {
                    return Content("您选择的用户拥有负责的终端，暂时无法删除！");
                }
                else
                {
                    string sqlstr = "delete from UserInfo where UserInfoID in(";

                    for (int i = 0; i < a.Length; i++)
                    {
                        if (i != a.Length - 1)
                        {
                            sqlstr += "'" + a[i] + "',";
                        }
                        else
                        {
                            sqlstr += "'" + a[i] + "'";
                        }
                    }
                    sqlstr += ")";
                    //int sum = DALHelp.ExecuteNonQuery(sqlstr, null);
                    //int sum = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteNonQuery(sqlstr) : DALHelp.ExecuteNonQuery(sqlstr, null);
                    int sum = ServerOrLit.isNum(LoginController.connType, sqlstr);

                    if (sum > 0)
                    {
                        return Content("删除成功");
                    }
                    else
                    {
                        return Content("删除失败");
                    }
                }
            }
        }

        //编辑,得到编辑的内容
        public ActionResult UserManagerEditBySelect(string uid)
        {
            if (uid == "" || uid == null)
            {
                return Content("no");
            }
            else
            {
                string sqlstr = string.Format("select * from UserInfo where UserInfoID='{0}'", uid);//根据id找到需要编辑的内容
                //DataTable dt = DALHelp.ExecuteDataTable(sqlstr, null);
                //DataTable dt = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr) : DALHelp.ExecuteDataTable(sqlstr, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);
                if (dt.Rows.Count > 0)
                {
                    string userid = dt.Rows[0]["UserInfoID"].ToString();
                    string username = dt.Rows[0]["UserName"].ToString();
                    string userlogin = dt.Rows[0]["LoginAccount"].ToString();
                    string userpwd = dt.Rows[0]["LoginPassword"].ToString();
                    string card = dt.Rows[0]["IDCard"].ToString();
                    string usergroupsid = dt.Rows[0]["UserGroupsID"].ToString();
                    return Content(userid + "," + username + "," + userlogin + "," + userpwd + "," + usergroupsid + "," + card);
                }
                else
                {
                    return Content("no");
                }
            }
        }

        //编辑
        public ActionResult UserManagerEdit(string userid, string username, string userpwd, string usergid, string card)
        {
            if (userid == "" || username == "" || userpwd == "" || usergid == "")
            {
                return Content("修改失败");
            }
            else
            {
                string sqlstr = string.Format("update UserInfo set UserGroupsID='{0}',UserName='{4}',LoginPassword='{3}',IDCard='{2}' where UserInfoID='{1}'", usergid, userid, card, userpwd, username);
                //int sum = DALHelp.ExecuteNonQuery(sqlstr, null);
                //int sum = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteNonQuery(sqlstr) : DALHelp.ExecuteNonQuery(sqlstr, null);
                int sum = ServerOrLit.isNum(LoginController.connType, sqlstr);
                if (sum > 0)
                {
                    return Content("修改成功");
                }
                else
                {
                    return Content("修改失败");
                }
            }
        }

        //查看详情
        public ActionResult UserManagerDetial(string uid)
        {
            if (uid == "")
            {
                return Content("no");
            }
            else
            {
                string sqlstr = string.Format("select * from UserInfo where UserInfoID='{0}'", uid);//根据id找到需要编辑的内容
                //DataTable dt = DALHelp.ExecuteDataTable(sqlstr, null);
                //DataTable dt = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr) : DALHelp.ExecuteDataTable(sqlstr, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);
                if (dt.Rows.Count > 0)
                {
                    string userid = dt.Rows[0]["UserName"].ToString();
                    string username = dt.Rows[0]["LoginAccount"].ToString();
                    string userpwd = dt.Rows[0]["LoginPassword"].ToString();
                    int usergroupsid = int.Parse(dt.Rows[0]["UserGroupsID"].ToString());
                    string idcard = dt.Rows[0]["IDCard"].ToString();
                    return Content(userid + "," + username + "," + userpwd + "," + usergroupsid + "," + idcard);
                }
                else
                {
                    return Content("no");
                }
            }
        }
    }
}
