using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SQLite;
using XP_PPS_DAL;
using System.Data;
using XP_PPS_Model;
using XP_PPS_BLL;
using ProductionPlanSystem.Help;

namespace ProductionPlanSystem.Controllers
{
    public class UserGroupManagerController : BaseController
    {
        // GET: UserGroupManager 
        public ActionResult Index()
        {
            //if (Session["UserId"] == null)
            //{
            //    return Content("<script>top.location = '/Login/Index';</script>");
            //}
            //else
            //{
                string sqlstr = string.Format("select * from UserGroups");
                //DataTable dt = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr) : DALHelp.ExecuteDataTable(sqlstr, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);

                if (dt.Rows.Count > 0)
                {
                    ViewBag.usergroup = dt;
                }
                else
                {
                    ViewBag.usegroup = null;
                }
                return View();
            //}
        }

        //添加用户组
        public ActionResult AddGroup(string UserGroupsName, string AuthorName)
        {
            if (UserGroupsName != "" && AuthorName != "")
            {
                string isin = string.Format("select UserGroupsName from UserGroups where UserGroupsName='{0}'", UserGroupsName);
           
                //DataTable dt = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(isin) : DALHelp.ExecuteDataTable(isin, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, isin);
                if (dt.Rows.Count > 0)
                {
                    return Content("此用户组已存在，请重新选择名称！");
                }
                else
                {
                    if (LoginController.connType == "sqlserver")
                    {
                        string sql = string.Format("insert into UserGroups (UserGroupsName,AuthorId) values ('{0}','{1}')", UserGroupsName, AuthorName);
                        int sum = SqlDbHelper.ExecuteNonQuery(sql);
                        if (sum > 0)
                        {
                            return Content("添加成功");
                        }
                        else
                        {
                            return Content("添加失败");
                        }
                    }
                    else
                    {
                        UserGroups ug = new UserGroups();
                        ug.UserGroupsName = UserGroupsName;
                        ug.AuthorId = AuthorName;
                        int sum = UserGroupsBLL.Add(ug);
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
            else
            {
                return Content("请填写完整信息！");
            }
        }

        //删除用户组
        public ActionResult DeleteGroup(string groupId)
        {
            if (groupId != "" && groupId != null)
            {
                string gids = groupId.Substring(0, groupId.Length - 1);
                var a = gids.Split(',');
                string sql = string.Format("select * from UserGroups ug inner join UserInfo u on ug.UserGroupsID=u.UserGroupsID and u.UserGroupsID in(");
                for (int i = 0; i < a.Length; i++)
                {
                    if (i != a.Length - 1)
                    {
                        sql += "'" + a[i] + "',";
                    }
                    else
                    {
                        sql += "'" + a[i] + "'";
                    }
                }
                sql += string.Format(")");
                //DataTable dt = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sql) : DALHelp.ExecuteDataTable(sql, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);

                if (dt.Rows.Count > 0)
                {
                    return Content("您所选择的用户组权限有用户正在使用，暂时无法删除！");
                }
                else
                {
                    string sqlstr = "delete from UserGroups where UserGroupsID in(";
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
            else
            {
                return Content("删除失败");
            }
        }

        //修改用户组
        public ActionResult UpdateGroup(string UserGroupsName, string AuthorName, string AuthorId)
        {
            if (UserGroupsName!=null&&UserGroupsName!=""&&AuthorName!=null&&AuthorName!=""&&AuthorId!=null&&AuthorId!="")
            {
                string sqlstr = string.Format("update UserGroups set UserGroupsName='{0}',AuthorId='{1}' where UserGroupsID={2}", UserGroupsName, AuthorName, int.Parse(AuthorId));
               
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
            else
            {
                return Content("修改失败");
            }
        }

        //修改树形结构
        public ActionResult GetClientsDataJson2(string usergrid)
        {
            if (usergrid == null)
            {
                usergrid = "1";
            }

            string sqlstr3 = string.Format("select * from Author where U_ID =0 and IsActive=1");
            //DataTable dt3 = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr3) : DALHelp.ExecuteDataTable(sqlstr3, null);
            DataTable dt3 = ServerOrLit.isDataTable(LoginController.connType, sqlstr3);

            string info3 = GetTree2(dt3, 0, usergrid);
            return Content(info3);
        }

        public string GetTree2(DataTable dt, int pid, string gid)
        {
            //用户组表
            string sqlstr3 = string.Format("select * from UserGroups where UserGroupsID={0}", int.Parse(gid));
            //DataTable dt3 = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr3) : DALHelp.ExecuteDataTable(sqlstr3, null);
            DataTable dt3 = ServerOrLit.isDataTable(LoginController.connType, sqlstr3);
            //权限表
            string sqlstr2 = string.Format("select * from Author");
         
            //DataTable dt2 = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr2) : DALHelp.ExecuteDataTable(sqlstr2,null);
            DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sqlstr2);
            string sbb = "[";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int id = int.Parse(dt.Rows[i]["AuthorId"].ToString());
                    string text = dt.Rows[i]["AuthorName"].ToString();
                    sbb += "{\"id\":" + id + ",\"text\":\"" + text + "\"";

                    if (id != 0)
                    {
                        string sqlstr4 = string.Format("select * from Author where U_ID = {0}  and IsActive=1", id);
                        //DataTable dt4 = DALHelp.ExecuteDataTable(sqlstr4, null);
                        //DataTable dt4 = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr4) : DALHelp.ExecuteDataTable(sqlstr4, null);
                        DataTable dt4 = ServerOrLit.isDataTable(LoginController.connType, sqlstr4);
                        if (dt4.Rows.Count > 0)
                        {
                            //代表有子节点
                            sbb += ",\"children\":[";
                            for (int ii = 0; ii < dt4.Rows.Count; ii++)
                            {
                                int id2 = int.Parse(dt4.Rows[ii]["AuthorId"].ToString());
                                string text2 = dt4.Rows[ii]["AuthorName"].ToString();
                                if (ii + 1 == dt4.Rows.Count)
                                {
                                    sbb += "{\"id\":" + id2 + ",\"text\":\"" + text2 + "\",\"checked\":" + (dt3.Rows[0]["AuthorId"].ToString().Contains("," + dt4.Rows[ii]["AuthorId"].ToString() + ",")).ToString().ToLower() + "}";
                                }
                                else
                                {
                                    sbb += "{\"id\":" + id2 + ",\"text\":\"" + text2 + "\",\"checked\":" + (dt3.Rows[0]["AuthorId"].ToString().Contains("," + dt4.Rows[ii]["AuthorId"].ToString() + ",")).ToString().ToLower() + "},";
                                }

                            }
                            if (i + 1 == dt.Rows.Count)
                            {
                                sbb += "]}";
                            }
                            else
                            {
                                sbb += "]},";
                            }
                        }
                    }
                }
            }
            sbb += "]";
            return sbb;
        }

        //树形结构数据
        public ActionResult GetClientsDataJson()
        {
            string sqlstr = string.Format("select * from Author where U_ID =0  and IsActive=1");
            //DataTable sum = DALHelp.ExecuteDataTable(sqlstr, null);
            //DataTable sum = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr) : DALHelp.ExecuteDataTable(sqlstr, null);
            DataTable sum = ServerOrLit.isDataTable(LoginController.connType, sqlstr);
            string info = GetTree(sum, 0);
            return Content(info);
        }

        public string GetTree(DataTable dt, int pid)
        {
            string sbb = "[";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int id = int.Parse(dt.Rows[i]["AuthorId"].ToString());
                    string text = dt.Rows[i]["AuthorName"].ToString();
                    sbb += "{\"id\":" + id + ",\"text\":\"" + text + "\"";

                    if (id != 0)
                    {
                        string sqlstr = string.Format("select * from Author where U_ID = {0}  and IsActive=1", id);
                        //DataTable dt2 = DALHelp.ExecuteDataTable(sqlstr, null);
                        //DataTable dt2 = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr) : DALHelp.ExecuteDataTable(sqlstr, null);
                        DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sqlstr);
                        if (dt2.Rows.Count > 0)
                        {
                            //代表有子节点
                            sbb += ",\"children\":[";
                            for (int ii = 0; ii < dt2.Rows.Count; ii++)
                            {
                                int id2 = int.Parse(dt2.Rows[ii]["AuthorId"].ToString());
                                string text2 = dt2.Rows[ii]["AuthorName"].ToString();
                                if (ii + 1 == dt2.Rows.Count)
                                {
                                    sbb += "{\"id\":" + id2 + ",\"text\":\"" + text2 + "\"}";
                                }
                                else
                                {
                                    sbb += "{\"id\":" + id2 + ",\"text\":\"" + text2 + "\"},";
                                }

                            }
                            if (i + 1 == dt.Rows.Count)
                            {
                                sbb += "]}";
                            }
                            else
                            {
                                sbb += "]},";
                            }
                        }
                    }
                }
            }
            sbb += "]";
            return sbb;
        }

        //查询修改数据显示
        public ActionResult SelectEditData(int gid)
        {
            if (gid.ToString() == "")
            {
                return Content("no");
            }
            string sqlstr = string.Format("select * from UserGroups where UserGroupsID={0}", gid);
            //DataTable dt = DALHelp.ExecuteDataTable(sqlstr,null);
            //DataTable dt = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr) : DALHelp.ExecuteDataTable(sqlstr, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);
            if (dt.Rows.Count > 0)
            {
                return Content(dt.Rows[0]["UserGroupsID"].ToString() + "/" + dt.Rows[0]["UserGroupsName"].ToString() + "/" + dt.Rows[0]["AuthorId"].ToString());
            }
            else
            {
                return Content("no");
            }
        }
    }
}