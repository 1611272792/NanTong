using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using XP_PPS_DAL;
using ProductionPlanSystem.Help;

namespace ProductionPlanSystem.Controllers
{

    public class AllocationSetController : BaseController
    {
        // GET: AllocationSet
        public ActionResult Index()
        {          
                string sql = string.Format("SELECT * FROM ALLOCATION");
                //DataTable dt = DALHelp.ExecuteDataTable(sql, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);
                //DataTable dt = LoginController.connType == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sql) : DALHelp.ExecuteDataTable(sql, null);

                if (dt.Rows.Count > 0)
                {
                    ViewBag.allocation = dt;
                }
                else
                {
                    ViewBag.allocation = null;
                }

                //获取下拉字段名
                string sql2 = "";
                if (LoginController.connType=="sqlserver")
                {
                    sql2 = string.Format("SELECT name FROM syscolumns WHERE id=Object_Id('ProductionPlan')");
                }
                else
                {
                    sql2 = string.Format("PRAGMA table_info([ProductionPlan])");
                }
                DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sql2);
                //DataTable dt2 = DALHelp.ExecuteDataTable(sql2, null);
                ViewBag.ziduan = dt2;
                return View();         
        }


        //编辑,得到编辑的内容
        public ActionResult AllocationEditBySelect(string allocationid)
        {
            if (allocationid!=""||allocationid!=null)
            {
                string sqlstr = string.Format("SELECT * FROM ALLOCATION where AllocationID='{0}'", allocationid);//根据id找到需要编辑的内容
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);
                //DataTable dt = DALHelp.ExecuteDataTable(sqlstr, null);
                if (dt.Rows.Count > 0)
                {
                    string Aid = dt.Rows[0]["AllocationID"].ToString();
                    string AName = dt.Rows[0]["AllocationName"].ToString();
                    string ATitle = dt.Rows[0]["AllocationTitle"].ToString();
                    string Serial = dt.Rows[0]["Serial"].ToString();
                    return Content(Aid + "," + AName + "," + ATitle + "," + Serial);
                }
                else
                {
                    return Content("no");
                }
            }
            else
            {
                return Content("no");
            }

        }


        //编辑
        public ActionResult AllocationEdit(string aid, string aname, string atitle, string serial)
        {
            if (aid != "" && aid != null && aname != "" && aid != null && atitle != null && atitle != "" && serial != null && serial != "")
            {
                string sql = string.Format("update Allocation set AllocationName='{0}',AllocationTitle='{1}',Serial={2} where AllocationID={3}", aname, atitle, serial, aid);
                //int dt = DALHelp.ExecuteNonQuery(sql, null);
                int dt = ServerOrLit.isNum(LoginController.connType,sql);
                if (dt > 0)
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

        public ActionResult DeleteUserManager(string aid)
        {
            if (aid != null && aid != "")
            {
                aid = aid.Substring(0, aid.Length - 1);
                var a = aid.Split(',');
                string sqlstr = "delete from Allocation where AllocationID in(";

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
            else
            {
                return Content("删除失败");
            }
        }

        //添加
        public ActionResult AddField(string filedName, string filedHead)
        {
            if (filedName != null && filedName != "" && filedHead != null && filedHead != "")
            {
                string isname = string.Format("select AllocationName from Allocation where AllocationName='{0}'", filedName);
                //DataTable dt = DALHelp.ExecuteDataTable(isname, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, isname);

                if (dt.Rows.Count > 0)
                {
                    return Content("已配置此字段");
                }
                else
                {   //查询最后的序号
                    string xuhao = string.Format("select Serial from Allocation order by Serial desc");
                    //DataTable dtxuhao = DALHelp.ExecuteDataTable(xuhao, null);
                    DataTable dtxuhao = ServerOrLit.isDataTable(LoginController.connType, xuhao);
                    int serial = int.Parse(dtxuhao.Rows[0][0].ToString()) + 1;

                    string sqlstr = string.Format("INSERT INTO Allocation (AllocationName,AllocationTitle,IsActive,Serial,[Type])VALUES('{0}','{1}',1,'{2}','pro')", filedName, filedHead, serial);
                    //int info = DALHelp.ExecuteNonQuery(sqlstr, null);
                    int info = ServerOrLit.isNum(LoginController.connType, sqlstr);


                    if (info > 0)
                    {
                        return Content("添加成功");
                    }
                    else
                    {
                        return Content("添加失败");
                    }
                }
            }
            else
            {
                return Content("添加失败");
            }
        }
    }
}