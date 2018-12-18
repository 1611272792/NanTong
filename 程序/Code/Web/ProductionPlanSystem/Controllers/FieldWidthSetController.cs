using ProductionPlanSystem.Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionPlanSystem.Controllers
{
    public class FieldWidthSetController : Controller
    {
        // GET: FieldWidthSet
        public ActionResult Index()
        {
            string fieldsql = string.Format("SELECT AllocationID,AllocationTitle,FieldWidth FROM dbo.Allocation WHERE AndroidIsActive=1 ");
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, fieldsql);
            if (dt.Rows?.Count>0)
            {
                ViewBag.fieldinfo = dt;
            }
            else
            {
                ViewBag.fieldinfo = null;
            }
            return View();
        }

        //编辑,得到编辑的内容
        public ActionResult AllocationEditBySelect(string allocationid)
        {
            if (allocationid != "" || allocationid != null)
            {
                string sqlstr = string.Format("SELECT * FROM ALLOCATION where AllocationID='{0}'", allocationid);//根据id找到需要编辑的内容
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqlstr);
                //DataTable dt = DALHelp.ExecuteDataTable(sqlstr, null);
                if (dt.Rows?.Count > 0)
                {
                    string Aid = dt.Rows[0]["AllocationID"].ToString();
                    string FieldWidth = dt.Rows[0]["FieldWidth"].ToString();
                    return Content(Aid + "," + FieldWidth);
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
        public ActionResult AllocationEdit(string aid, string fieldwidth)
        {
            if (aid != "" && aid != null && fieldwidth != null && fieldwidth != "")
            {
                string sql = string.Format($"update Allocation set FieldWidth='{fieldwidth}' where AllocationID={aid}");
                //int dt = DALHelp.ExecuteNonQuery(sql, null);
                int dt = ServerOrLit.isNum(LoginController.connType, sql);
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

    }
}