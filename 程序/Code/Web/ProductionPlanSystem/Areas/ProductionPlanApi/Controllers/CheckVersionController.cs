using ProductionPlanSystem.Controllers;
using ProductionPlanSystem.Help;
using ProductionPlanSystem_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using XP_PPS_DAL;

namespace ProductionPlanSystem_WebApi.Controllers
{
    public class CheckVersionController : ApiController
    {
        [System.Web.Http.HttpGet]
        //接口7 获取版本更新信息
        public ActionResult CheckVersion(int TerminalType, int VersionCode)
        {
            CheckVersion v = new CheckVersion();
            try
            {
                string sql1 = "";
                if (LoginController.connType=="sqlserver")
                {
                    sql1 = string.Format("SELECT TOP 1 VersionCode,DownloadUrl from UpdateCatalog where TerminalType={0} order by VersionCode desc",TerminalType);
                }
                else
                {
                    sql1 = string.Format("select VersionCode,DownloadUrl from UpdateCatalog where TerminalType={0} order by VersionCode desc limit 1", TerminalType);
                }

                //DataTable dt = DALHelp.ExecuteDataTable(sql1, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql1);
                if (dt.Rows.Count > 0)
                {
                    v.StateCode = 100;
                    v.Reason = "";
                    int nowVersionCode = int.Parse(dt.Rows[0]["VersionCode"].ToString());
                    if (nowVersionCode > VersionCode)
                    {
                        v.NewVersion = true;
                        v.DownloadUrl = "http://" + AppConfig.IP + dt.Rows[0]["DownloadUrl"].ToString();
                    }
                    else
                    {
                        v.NewVersion = false;
                        v.DownloadUrl = "";
                    }
                }
                else
                {
                    v.StateCode = 100;
                    v.Reason = "获取数据失败";
                    v.NewVersion = false;
                    v.DownloadUrl = "";
                }
            }
            catch (Exception ex)
            {
                v.StateCode = 104;
                v.Reason = ex.Message;
                v.NewVersion = false;
                v.DownloadUrl = "";
            }
            return v;
        }


        private void WriteLog(string strmsg)
        {
            strmsg = $"{strmsg}\r\n";
            using (FileStream fs = System.IO.File.Open("D:\\123.txt", FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] bys = Encoding.Default.GetBytes(strmsg);
                fs.Write(bys, 0, bys.Length);
                fs.Close();
            }
        }
    }
}
