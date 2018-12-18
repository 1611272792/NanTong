using ProductionPlanSystem.Areas.ProductionPlanApi.Models;
using ProductionPlanSystem.Controllers;
using ProductionPlanSystem.Help;
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

namespace ProductionPlanSystem.Areas.ProductionPlanApi.Controllers
{
    public class GetProductLineController : ApiController
    {
        [System.Web.Mvc.HttpGet]
        // 接口1 获取全部产线
        public ActionResult GetProductLine()
        {
            Lines ls = new Lines();
            try
            {
                string sql = string.Format("select TerminalID,TerminalName from Terminal");
                //DataTable dt = DALHelp.ExecuteDataTable(sql, null);
                DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);
                if (dt?.Rows.Count > 0)
                {
                    ls.StateCode = 100;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i < dt.Rows.Count - 1)
                        {
                            if (LoginController.connType=="sqlserver")
                            {
                                ls.ProductLines.Add(new Line() { ID = int.Parse(dt.Rows[i]["TerminalID"].ToString()), Name = dt.Rows[i]["TerminalName"].ToString() });
                            }
                            else
                            {
                                ls.ProductLines.Add(new Line() { ID = (int)dt.Rows[i].Field<long>("TerminalID"), Name = dt.Rows[i]["TerminalName"].ToString() });
                            }
                        }
                        else
                        {
                            if (LoginController.connType == "sqlserver")
                            {
                                ls.ProductLines.Add(new Line() { ID = int.Parse(dt.Rows[i]["TerminalID"].ToString()), Name = dt.Rows[i]["TerminalName"].ToString() });
                            }
                            else
                            {
                                ls.ProductLines.Add(new Line() { ID = (int)dt.Rows[i].Field<long>("TerminalID"), Name = dt.Rows[i]["TerminalName"].ToString() });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ls.StateCode = 104;
                WriteLog("错误GetProductLine：" + e.Message);
            }
            return ls;
        }

        private void WriteLog(string strmsg)
        {
            strmsg = $"{strmsg}\r\n";
            using (FileStream fs = System.IO.File.Open("D:\\2.txt", FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] bys = Encoding.Default.GetBytes(strmsg);
                fs.Write(bys, 0, bys.Length);
                fs.Close();
            }
        }
    }
}
