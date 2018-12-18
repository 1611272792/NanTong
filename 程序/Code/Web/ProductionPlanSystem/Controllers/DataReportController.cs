using System.Collections.Generic;
using System.Web.Mvc;
using XP_PPS_DAL;
using System.Data;
using XP_PPS_Model;
using ProductionPlanSystem.Help;

namespace ProductionPlanSystem.Controllers
{
    public class DataReportController : BaseController
    {        
        // GET: DataReport
        public ActionResult Index()
        {
            //判断是管理员or普通用户
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
            //分页
            string mysqls = "";
            string pagesql = "";//查询总行数
            string exceltable = "";//导出数据
            //主页面加载时的数据默认每页10条数据
            if (Role == "管理员")
            {
                //mysqls = string.Format("select * from ProductionPlan p inner join Terminal t on p.TerminalID=t.TerminalID");
                mysqls = string.Format("select * from DataReport where ProductionPlanID not in(select ProductionPlanID from DataReport order by ProductionPlanID limit 0,0) order by ProductionPlanID limit 0,10");
                exceltable = string.Format("select * from DataReport");
                pagesql = string.Format("select count(*) from DataReport");
            }
            else if (Role == "产线负责人")
            {
                ViewBag.Role = 1;
                //mysqls = string.Format("select ProductionPlanID,ProductionPlanName,ProductionPlanVersion,PlanNum,RealNum,StartTime,EndTime,State,TerminalName from ProductionPlan p inner join Terminal t on p.TerminalID=t.TerminalID where t.TerMinalID=(select TerMinalID from Terminal where UserInfoID={0})", userid);
                //mysqls = string.Format("select * from ProductionPlan p inner join Terminal t on p.TerminalID = t.TerminalID where t.TerMinalID=(select TerMinalID from Terminal where UserInfoID={0})", userid);
                exceltable = mysqls = string.Format("select * from DataReport where TerminalID=(select TerminalID from Terminal where UserInfoID={0})", userid);
                pagesql = string.Format("select count(*) from DataReport where TerminalID=(select TerminalID from Terminal where UserInfoID={0})", userid);
                string b = string.Format("select ProductionPlanID from DataReport where TerminalID=(select TerminalID from Terminal where UserInfoID={0})", userid);
                mysqls += string.Format(" and ProductionPlanID not in(" + b + " order by ProductionPlanID limit 0) order by ProductionPlanID limit 10");
            }
            //DataTable dt = DALHelp.ExecuteDataTable(mysqls, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, mysqls);
            //DataTable exdt = DALHelp.ExecuteDataTable(exceltable,null);
            DataTable exdt = ServerOrLit.isDataTable(LoginController.connType, exceltable);

            if (dt.Rows.Count > 0 && exdt.Rows.Count > 0)
            {
                //DataTable page = DALHelp.ExecuteDataTable(pagesql, null);
                DataTable page = ServerOrLit.isDataTable(LoginController.connType, pagesql);

                //判断余数
                int hang = int.Parse(page.Rows[0]["count(*)"].ToString());
                int yu = int.Parse(page.Rows[0]["count(*)"].ToString()) % 10;
                int pagenum;
                if (yu == 0)
                {
                    pagenum = int.Parse(page.Rows[0]["count(*)"].ToString()) / 10;
                }
                else
                {
                    pagenum = int.Parse(page.Rows[0]["count(*)"].ToString()) / 10 + 1;
                }
                ViewBag.hang = hang;//行
                ViewBag.lastpage = pagenum;//页
                ViewBag.indexdata = dt;//数据
                ViewBag.extable = exdt;
            }
            else
            {
                ViewBag.hang = 0;
                ViewBag.lastpage = 0;
                ViewBag.indexdata = null;
                Session["datatable"] = null;
            }

            //第一次加载时的下拉框数据
            string sql = "";
            if (Role == "管理员")
            {
                sql = string.Format("select * from Terminal");
            }
            else if (Role == "产线负责人")
            {
                sql = string.Format("select * from Terminal where UserInfoID = {0}", userid);
            }
            //产线下拉框数据
            //DataTable dt1 = DALHelp.ExecuteDataTable(sql, null);
            DataTable dt1 = ServerOrLit.isDataTable(LoginController.connType, sql);

            ViewBag.terlist1 = dt1;
            if (dt1.Rows.Count > 0)
            {
                List<Terminal> terminal = new List<Terminal>();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    Terminal ter = new Terminal();
                    ter.TerminalID = int.Parse(dt1.Rows[i]["TerminalID"].ToString());
                    ter.TerminalName = dt1.Rows[i]["TerminalName"].ToString();
                    terminal.Add(ter);
                }
                ViewBag.terlist = terminal;
            }

            //第一次加载数据时的表头数据
            string sqltitle = string.Format("select * from Allocation order by Serial asc");
            //DataTable dt2 = DALHelp.ExecuteDataTable(sqltitle, null);
            DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sqltitle);

            if (dt2.Rows.Count > 0)
            {
                ViewBag.title = dt2;
            }
            else
            {
                ViewBag.title = null;
            }
            return View();
        }



        //public ActionResult Index()
        //{
        //    string userid = Session["UserInfoID"].ToString();//登录人id
        //    string Role = PeopleRole();
        //    string mysqls = "";
        //    string pagesql = "";//查询总行数
        //    //主页面加载时的数据默认每页10条数据
        //    if (Role == "管理员")
        //    {
        //        //mysqls = string.Format("select * from ProductionPlan p inner join Terminal t on p.TerminalID=t.TerminalID");
        //        mysqls = string.Format("select * from DataReport where ProductionPlanID not in(select ProductionPlanID from DataReport order by ProductionPlanID limit 0,0) order by ProductionPlanID limit 0,10");
        //        pagesql = string.Format("select count(*) from DataReport");
        //    }
        //    else if (Role == "产线负责人")
        //    {
        //        ViewBag.Role = 1;
        //        //mysqls = string.Format("select ProductionPlanID,ProductionPlanName,ProductionPlanVersion,PlanNum,RealNum,StartTime,EndTime,State,TerminalName from ProductionPlan p inner join Terminal t on p.TerminalID=t.TerminalID where t.TerMinalID=(select TerMinalID from Terminal where UserInfoID={0})", userid);
        //        //mysqls = string.Format("select * from ProductionPlan p inner join Terminal t on p.TerminalID = t.TerminalID where t.TerMinalID=(select TerMinalID from Terminal where UserInfoID={0})", userid);
        //        mysqls = string.Format("select * from DataReport where TerminalID=(select TerminalID from Terminal where UserInfoID={0})", userid);
        //        pagesql = string.Format("select count(*) from DataReport where TerminalID=(select TerminalID from Terminal where UserInfoID={0})", userid);
        //        string b = string.Format("select ProductionPlanID from DataReport where TerminalID=(select TerminalID from Terminal where UserInfoID={0})", userid);
        //        mysqls += string.Format(" and ProductionPlanID not in(" + b + " order by ProductionPlanID limit 0) order by ProductionPlanID limit 10");
        //    }
        //    DataTable dt = DALHelp.ExecuteDataTable(mysqls, null);
        //    if (dt.Rows.Count > 0)
        //    {

        //        ViewBag.indexdata = dt;
        //        string JsonString = string.Empty;
        //        JsonString = JsonConvert.SerializeObject(dt);
        //    }
        //    else
        //    {
        //        ViewBag.hang = 0;
        //        ViewBag.lastpage = 0;
        //        ViewBag.indexdata = null;
        //        Session["datatable"] = null;
        //    }

        //    //第一次加载时的下拉框数据
        //    string sql = "";
        //    if (Role == "管理员")
        //    {
        //        sql = string.Format("select * from Terminal");
        //    }
        //    else if (Role == "产线负责人")
        //    {
        //        sql = string.Format("select * from Terminal where UserInfoID = {0}", userid);
        //    }
        //    //产线下拉框数据
        //    DataTable dt1 = DALHelp.ExecuteDataTable(sql, null);
        //    ViewBag.terlist1 = dt1;
        //    if (dt1.Rows.Count > 0)
        //    {
        //        List<Terminal> terminal = new List<Terminal>();
        //        for (int i = 0; i < dt1.Rows.Count; i++)
        //        {
        //            Terminal ter = new Terminal();
        //            ter.TerminalID = int.Parse(dt1.Rows[i]["TerminalID"].ToString());
        //            ter.TerminalName = dt1.Rows[i]["TerminalName"].ToString();
        //            terminal.Add(ter);
        //        }
        //        ViewBag.terlist = terminal;
        //    }

        //    //第一次加载数据时的表头数据
        //    string sqltitle = string.Format("select * from Allocation order by Serial asc");
        //    DataTable dt2 = DALHelp.ExecuteDataTable(sqltitle, null);
        //    if (dt2.Rows.Count > 0)
        //    {
        //        //ViewBag.title = dt2;
        //        string JsonStrings = string.Empty;
        //        JsonStrings = JsonConvert.SerializeObject(dt2);
        //        ViewBag.title = JsonStrings;
        //    }
        //    else
        //    {
        //        ViewBag.title = null;
        //    }
        //    return View();
        //}

        public List<ProductionPlan> SelectByXX(string mysqls)
        {
            //DataTable dt = DALHelp.ExecuteDataTable(mysqls, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, mysqls);

            List<ProductionPlan> datareport = new List<ProductionPlan>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProductionPlan dr = new ProductionPlan();
                dr.ProductionPlanID = int.Parse(dt.Rows[i]["ProductionPlanID"].ToString());
                dr.ProductionPlanName = dt.Rows[i]["ProductionPlanName"].ToString();
                dr.PlanNum = dt.Rows[i]["PlanNum"].ToString();
                dr.RealNum = dt.Rows[i]["RealNum"].ToString();
                dr.StartTime = dt.Rows[i]["StartTime"].ToString();
                dr.EndTime = dt.Rows[i]["EndTime"].ToString();
                dr.State = dt.Rows[i]["State"].ToString();
                dr.TerminalID = dt.Rows[i]["TerminalID"].ToString();
                datareport.Add(dr);
            }
            return datareport;
        }

        [HttpGet]
        public ActionResult SelectAll(string aa, string bb, string line, string version,int now,int every)
        {
            string sqltitle = string.Format("select * from Allocation order by Serial asc");
            //DataTable dt2 = DALHelp.ExecuteDataTable(sqltitle, null);
            DataTable dt2 = ServerOrLit.isDataTable(LoginController.connType, sqltitle);

            if (dt2.Rows.Count > 0)
            {
                ViewBag.title = dt2;
            }
            else
            {
                ViewBag.title = null;
            }
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
            //分页
            string mysqls = "";//总数据
            string pagesql = "";//总行数
            string b = "";//需要刨除的数据
            string extable = "";
            int zz = (now - 1) * every;
            if (Role == "管理员")
            {
                if (aa == "" && bb == "")
                {
                    if (line == "0")
                    {
                        //等于0时查询所有产线
                        //mysqls = string.Format("select * from ProductionPlan p inner join Terminal t on p.TerminalID = t.TerminalID where p.ProductionPlanVersion like '%{0}%'", version);
                        extable=mysqls = string.Format("select * from DataReport where ProductionPlanVersion like '%{0}%'",version);
                        pagesql= string.Format("select count(*) from DataReport where ProductionPlanVersion like '%{0}%'", version);
                        b = string.Format("select ProductionPlanID from DataReport where ProductionPlanVersion like '%{0}%'",version);
                    }
                    else
                    {
                        //mysqls = string.Format("select * from ProductionPlan p inner join Terminal t on p.TerminalID = t.TerminalID where p.ProductionPlanVersion like '%{0}%' and t.TerminalID='{1}'", version, line);
                        extable = mysqls = string.Format("select * from DataReport where ProductionPlanVersion like '%{0}%' and TerminalID='{1}'",version,line);
                        pagesql = string.Format("select count(*) from DataReport where ProductionPlanVersion like '%{0}%' and TerminalID='{1}'", version, line);
                        b = string.Format("select ProductionPlanID from DataReport where ProductionPlanVersion like '%{0}%' and TerminalID='{1}'", version, line);
                    }
                }
                else
                {
                    if (line == "0")
                    {
                        //mysqls = string.Format("select * from ProductionPlan p inner join Terminal t on p.TerminalID = t.TerminalID where p.StartTime>='{0}' and p.StartTime<='{1}' and p.ProductionPlanVersion like '%{2}%'", aa, bb, version);
                        extable = mysqls = string.Format("select * from DataReport where StartTime>='{0}' and StartTime<='{1}' and ProductionPlanVersion like '%{2}%'",aa,bb,version);
                        pagesql = string.Format("select count(*) from DataReport where StartTime>='{0}' and StartTime<='{1}' and ProductionPlanVersion like '%{2}%'", aa, bb, version);
                        b = string.Format("select ProductionPlanID from DataReport where StartTime>='{0}' and StartTime<='{1}' and ProductionPlanVersion like '%{2}%'", aa, bb, version);
                     }
                    else
                    {
                        //mysqls = string.Format("select * from ProductionPlan p inner join Terminal t on p.TerminalID = t.TerminalID where p.StartTime>='{0}' and p.StartTime<='{1}' and p.ProductionPlanVersion like '%{2}%' and t.TerminalID='{3}'", aa, bb, version, line);
                        extable = mysqls = string.Format("select * from DataReport where StartTime>='{0}' and StartTime<='{1}' and ProductionPlanVersion like '%{2}%' and TerminalID='{3}'", aa, bb, version, line);
                        pagesql = string.Format("select count(*) from DataReport where StartTime>='{0}' and StartTime<='{1}' and ProductionPlanVersion like '%{2}%' and TerminalID='{3}'", aa, bb, version, line);
                        b = string.Format("select ProductionPlanID from DataReport where StartTime>='{0}' and StartTime<='{1}' and ProductionPlanVersion like '%{2}%' and TerminalID='{3}'", aa, bb, version, line);
                     }
                }
            }
            else if (Role == "产线负责人")
            {
                if (aa == "" && bb == "")
                {
                    //mysqls = string.Format("select * from ProductionPlan p inner join Terminal t on p.TerminalID = t.TerminalID where p.ProductionPlanVersion like '%{0}%' and t.TerminalID='{1}' and t.UserInfoID='{2}'", version, line,userid);
                    extable = mysqls = string.Format("select * from DataReport where ProductionPlanVersion like '%{0}%' and TerminalID = '{1}' and UserInfoID = '{2}'", version, line,userid);
                    pagesql = string.Format("select count(*) from DataReport where ProductionPlanVersion like '%{0}%' and TerminalID = '{1}' and UserInfoID = '{2}'", version, line, userid);
                    b = string.Format("select ProductionPlanID from DataReport where ProductionPlanVersion like '%{0}%' and TerminalID = '{1}' and UserInfoID = '{2}' and ProductionPlanVersion like '%{3}%' and TerminalID='{4}'", version, line, userid,version,line);
                }
                else
                {
                    //mysqls = string.Format("select * from ProductionPlan p inner join Terminal t on p.TerminalID = t.TerminalID where p.StartTime>='{0}' and p.StartTime<='{1}' and p.ProductionPlanVersion like '%{2}%' and t.TerminalID='{3}' and t.UserInfoID='{4}'", aa, bb, version, line,userid);
                    extable = mysqls = string.Format("select * from DataReport where StartTime>='{0}' and StartTime<='{1}' and ProductionPlanVersion like '%{2}%' and TerminalID='{3}' and UserInfoID='{4}'", aa, bb, version, line, userid);
                    pagesql = string.Format("select count(*) from DataReport where StartTime>='{0}' and StartTime<='{1}' and ProductionPlanVersion like '%{2}%' and TerminalID='{3}' and UserInfoID='{4}'", aa, bb, version, line, userid);
                    b = string.Format("select ProductionPlanID from DataReport where StartTime >= '{0}' and StartTime <= '{1}' and ProductionPlanVersion like '%{2}%' and TerminalID = '{3}' and UserInfoID = '{4}'", aa, bb, version, line, userid);
                }
            }
            mysqls += string.Format(" and ProductionPlanID not in(" + b + " order by ProductionPlanID limit {0}) order by ProductionPlanID limit {1}", zz, every);

            //DataTable dt = DALHelp.ExecuteDataTable(mysqls, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, mysqls);

            //DataTable exdt = DALHelp.ExecuteDataTable(extable,null);
            DataTable exdt = ServerOrLit.isDataTable(LoginController.connType, extable);

            if (dt.Rows.Count > 0)
            {
                //DataTable page = DALHelp.ExecuteDataTable(pagesql, null);
                DataTable page = ServerOrLit.isDataTable(LoginController.connType, pagesql);

                //判断余数
                int hang = int.Parse(page.Rows[0]["count(*)"].ToString());
                int yu = int.Parse(page.Rows[0]["count(*)"].ToString()) % every;
                int pagenum;
                if (yu == 0)
                {
                    pagenum = int.Parse(page.Rows[0]["count(*)"].ToString()) / every;
                }
                else
                {
                    pagenum = int.Parse(page.Rows[0]["count(*)"].ToString()) / every + 1;
                }
                ViewBag.hang = hang;
                ViewBag.lastpage = pagenum;//总页数
                ViewBag.selectdata = dt;
                ViewBag.extable = exdt;
            }
            else
            {
                //ViewBag.hang = 0;
                //ViewBag.lastpage = 0;
                ViewBag.selectdata = null;
                Session["datatable"] = null;
            }
            return PartialView("_MyReport", dt);
        }


        //判断是否为管理员或产线负责人
        public string PeopleRole(string userid, string Role1)
        {
            //string LoginAccount;
            //string userid;
            //if (Session["UserId"] == null || Session["UserInfoID"] == null)
            //{
            //    return "账号过期请重新登陆！";
            //}
            //else
            //{
            //    LoginAccount = Session["UserId"].ToString();
            //    userid = Session["UserInfoID"].ToString();
            //}          
            //string sqlstr = string.Format("select * from UserInfo where UserInfoID={0}", userid);
            //DataTable admindt = DALHelp.ExecuteDataTable(sqlstr, null);
            string IsPeople = "";
            //如果用户账号为admin则是管理员
            if (Role1 == Help.AppConfig.Role)
            {
                IsPeople = "管理员";
            }
            else
            {
                //如果此用户有负责的产线则为产线负责人
                string sqlstr2 = string.Format("select * from Terminal where UserInfoID={0}", userid);
                DataTable linedt = ServerOrLit.isDataTable(LoginController.connType, sqlstr2);

                //DataTable linedt = DALHelp.ExecuteDataTable(sqlstr2, null);
                if (linedt.Rows.Count > 0)
                {
                    IsPeople = "产线负责人";
                    ViewBag.Role = 1;
                }
            }
            return IsPeople;
        }
    }
}