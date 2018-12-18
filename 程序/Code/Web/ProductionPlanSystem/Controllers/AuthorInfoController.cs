using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XP_PPS_BLL;
using XP_PPS_Model;

namespace ProductionPlanSystem.Controllers
{
    public class AuthorInfoController : Controller
    {
        // GET: AuthorInfo
        public ActionResult Index()
        {
            List<UserGroups> list = new List<UserGroups>();
            SQLiteDataReader reader=UserGroupsBLL.Select();
            while (reader.Read())
            {
                UserGroups model = new UserGroups();
                model.UserGroupsID = Convert.ToInt32(reader["UserGroupsID"]);
                model.UserGroupsName = reader["UserGroupsName"].ToString();
                model.AuthorId = reader["AuthorId"].ToString();
                list.Add(model);
            }
            ViewBag.list = list;
            return View();
        }
    }
}