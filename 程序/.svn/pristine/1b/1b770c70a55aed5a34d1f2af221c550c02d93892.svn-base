using Newtonsoft.Json;
using ProductionPlanSystem.Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XP_PPS_DAL;
namespace ProductionPlanSystem.Controllers
{
    public class UpdateCatalogController : BaseController
    {
        // GET: UpdateCatelog
        public ActionResult Index()
        {
            //if (Session["UserId"] == null)
            //{
            //    return Content("<script>top.location = '/Login/Index';</script>");
            //}
            //else
            //{
                //查询当前版本（最新）
                string sql = "";
                if (LoginController.connType=="sqlserver")
                {
                      sql = string.Format("select top 1 * from UpdateCatalog where TerminalType=1 order by VersionCode desc");
                }
                else
                {
                    sql = string.Format("select * from UpdateCatalog where TerminalType=1 order by VersionCode desc limit 1");
                }
                //DataTable dt = DALHelp.ExecuteDataTable(sql, null); 
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);
                if (dt.Rows.Count > 0)
                {
                    Session["VersionCode"] = dt.Rows[0]["VersionCode"];
                    Session["DownloadUrl"] = dt.Rows[0]["DownloadUrl"];
                }
                else
                {
                    Session["VersionCode"] = null;
                    Session["DownloadUrl"] = null;
                }
                return View();
            //}
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase fileUrl,int TerminalType)
        {
            string aa = "";
            if (fileUrl == null)
            {
                aa = "selecct";
            }
            else
            {
                string filename = fileUrl.FileName;
                if (filename.LastIndexOf("\\") > -1)//兼容ie
                {
                    filename = filename.Substring(filename.LastIndexOf("\\") + 1);
                }
                string[] a = filename.Split('.');
                //BMP（位图）、JPG、JPEG、PNG、GIF
                if (TerminalType == 0)
                {
                    if (a[1] == "apk")
                    {
                        //当前版本号
                        int VersionCode;
                        string codesql = "";
                        if (LoginController.connType=="sqlserver")
                        {
                            codesql = string.Format("select top 1  VersionCode from UpdateCatalog where TerminalType={0} order by VersionCode desc", TerminalType);

                        }
                        else
                        {
                            codesql = string.Format("select VersionCode from UpdateCatalog where TerminalType={0} order by VersionCode desc limit 1", TerminalType);
                        }
                      
                        //DataTable dt = DALHelp.ExecuteDataTable(codesql, null);
                        DataTable dt = ServerOrLit.isDataTable(LoginController.connType, codesql);
                        //根据端口查询最新版本号如果为空则版本号为1否则就在之前版本号上加一
                        if (dt.Rows.Count > 0)
                        {
                            VersionCode = int.Parse(dt.Rows[0]["VersionCode"].ToString()) + 1;
                        }
                        else
                        {
                            VersionCode = 1;
                        }                        
                        fileUrl.SaveAs(Server.MapPath("~/Content/Files/") + filename);
                        string filenames = "/Content/Files/" + filename;
                        string fileurl = string.Format("insert into UpdateCatalog (TerminalType,VersionCode,DownloadUrl) values({0},{1},'{2}')", TerminalType, VersionCode, filenames);
                        //int info = DALHelp.ExecuteNonQuery(fileurl, null);
                        int info = ServerOrLit.isNum(LoginController.connType, fileurl);
                        if (info > 0)
                        {
                            aa = "ok";
                        }
                        else
                        {
                            aa = "no";
                        }
                    }
                    else
                    {
                        aa = "abuok";
                    }
                }
                else
                {
                    if (a[1] == "exe")
                    {
                        //当前版本号
                        int VersionCode;
                        string codesql = "";
                        if (LoginController.connType == "sqlserver")
                        {
                            codesql = string.Format(" select top 1 VersionCode from UpdateCatalog where TerminalType={0} order by VersionCode desc", TerminalType);

                        }
                        else
                        {
                            codesql = string.Format("select VersionCode from UpdateCatalog where TerminalType={0} order by VersionCode desc limit 1", TerminalType);
                        }
                        //string codesql = string.Format("select VersionCode from UpdateCatalog where TerminalType={0} order by VersionCode desc limit 1", TerminalType);
                        //DataTable dt = DALHelp.ExecuteDataTable(codesql, null);
                        DataTable dt = ServerOrLit.isDataTable(LoginController.connType, codesql);
                        //根据端口查询最新版本号如果为空则版本号为1否则就在之前版本号上加一
                        if (dt.Rows.Count > 0)
                        {
                            VersionCode = int.Parse(dt.Rows[0]["VersionCode"].ToString()) + 1;
                        }
                        else
                        {
                            VersionCode = 1;
                        }
                        fileUrl.SaveAs(Server.MapPath("~/Content/Files/") + filename);
                        string filenames = "/Content/Files/" + filename;
                        string fileurl = string.Format("insert into UpdateCatalog (TerminalType,VersionCode,DownloadUrl) values({0},{1},'{2}')", TerminalType, VersionCode, filenames);
                        //int info = DALHelp.ExecuteNonQuery(fileurl, null);
                        int info = ServerOrLit.isNum(LoginController.connType, fileurl);

                        if (info > 0)
                        {
                            aa = "ok";
                        }
                        else
                        {
                            aa = "no";
                        }
                    }
                    else
                    {
                        aa = "wbuok";
                    }
                }

            }
            ViewBag.mess = aa;
            return View();
        }


        [HttpPost]
        //根据不同端口显示对应的最新版本
        public string choose(int TerminalType)
        {
            string sql = "";
            if (LoginController.connType == "sqlserver")
            {
                sql = string.Format("select  top 1 * from UpdateCatalog where TerminalType={0} order by VersionCode desc", TerminalType);
            }
            else
            {
                sql = string.Format("select * from UpdateCatalog where TerminalType={0} order by VersionCode desc limit 1", TerminalType);
            }
            //string sql = string.Format("select * from UpdateCatalog where TerminalType={0} order by VersionCode desc limit 1", TerminalType);
            //DataTable dt = DALHelp.ExecuteDataTable(sql, null);
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);
            string Json = "";
            if (dt.Rows.Count > 0)
            {
                Json = JsonConvert.SerializeObject(dt);//返回json格式数据由前台接收
            }
            return Json;
        }
    }
}