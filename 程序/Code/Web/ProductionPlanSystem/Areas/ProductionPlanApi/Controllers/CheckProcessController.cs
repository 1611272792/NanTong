using Newtonsoft.Json;
using ProductionPlanSystem.Areas.ProductionPlanApi.Models;
using ProductionPlanSystem.Controllers;
using ProductionPlanSystem.Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ProductionPlanSystem.Areas.ProductionPlanApi.Controllers
{
    public class CheckProcessController : ApiController
    {
        /// <summary>
        /// 判断当前工序是否还有下一道工序  如果有查询下一个工序的第一个状态
        /// </summary>
        /// <param name="ProcessId"></param>
        /// <param name="StateId"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckProcess(int ProcessId, int StateId)
        {
            CheckProcess cp = new CheckProcess();
            string isin = string.Format($@"SELECT StateId FROM dbo.State WHERE ProcessId={ProcessId} AND StateId={StateId}");
            DataTable mac = ServerOrLit.isDataTable(LoginController.connType, isin);
            if (mac?.Rows.Count>0)
            {
                SqlParameter[] pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@strType", "judgeserial");
                pars[1] = new SqlParameter("@ProcessId", ProcessId);
                DataSet SId = SqlDbHelper.RunProcedure("proc_JudgeSerial", pars, "judgeserial");
                if (SId?.Tables.Count > 0 && SId.Tables[0]?.Rows.Count > 0)
                {
                    //先查询是否是当前工序的最后一个状态
                    if (StateId == int.Parse(SId.Tables[0].Rows[0]["StateId"].ToString()))
                    {
                        SqlParameter[] nextpro = new SqlParameter[2];
                        nextpro[0] = new SqlParameter("@strType", "judgenextProId");
                        nextpro[1] = new SqlParameter("@ProcessId", ProcessId);
                        DataSet NextPId = SqlDbHelper.RunProcedure("proc_JudgeSerial", nextpro, "judgenextProId");
                        //判断是否有下一个工序
                        if (NextPId?.Tables.Count > 0 && NextPId.Tables[0]?.Rows.Count > 0)
                        {
                            if (string.IsNullOrWhiteSpace(NextPId.Tables[0].Rows[0]["NextProcessId"].ToString()) || NextPId.Tables[0].Rows[0]["NextProcessId"].ToString() == "0")
                            {
                                cp.StateCode = 100;
                                cp.ReaSon = "没有下一道工序";
                                cp.NextProcessId = 0;
                                cp.NextStateId = 0;
                            }
                            else
                            {
                                SqlParameter[] nextproinfo = new SqlParameter[2];
                                nextproinfo[0] = new SqlParameter("@strType", "nextProInfo");
                                nextproinfo[1] = new SqlParameter("@ProcessId", NextPId.Tables[0].Rows[0]["NextProcessId"].ToString());
                                DataSet NextPInfo = SqlDbHelper.RunProcedure("proc_JudgeSerial", nextproinfo, "nextProInfo");
                                //查询下一个工序信息
                                if (NextPInfo?.Tables.Count > 0 && NextPInfo.Tables[0]?.Rows.Count > 0)
                                {
                                    cp.StateCode = 100;
                                    cp.ReaSon = "";
                                    cp.NextProcessId = int.Parse(NextPInfo.Tables[0].Rows[0]["ProcessId"].ToString());
                                    cp.NextStateId = int.Parse(NextPInfo.Tables[0].Rows[0]["StateId"].ToString());
                                }
                                else
                                {
                                    cp.StateCode = 100;
                                    cp.ReaSon = "";
                                    cp.NextProcessId = int.Parse(NextPId.Tables[0].Rows[0]["ProcessId"].ToString());
                                    cp.NextStateId = 0;
                                }
                            }
                        }
                        else
                        {
                            cp.StateCode = 100;
                            cp.ReaSon = "没有下一道工序";
                            cp.NextProcessId = 0;
                            cp.NextStateId = 0;
                        }
                    }
                    else
                    {
                        cp.StateCode = 100;
                        cp.ReaSon = "不是最后一个状态";
                        cp.NextProcessId = 0;
                        cp.NextStateId = 0;
                    }
                }
                else
                {
                    cp.StateCode = 100;
                    cp.ReaSon = "暂无状态";
                    cp.NextProcessId = 0;
                    cp.NextStateId = 0;
                }
            }
            else
            {
                cp.StateCode = 104;
                cp.ReaSon = "此状态不属于此工序";
                cp.NextProcessId = 0;
                cp.NextStateId = 0;
            }


            string Json = JsonConvert.SerializeObject(cp);

            return new HttpResponseMessage { Content = new StringContent(Json, Encoding.GetEncoding("UTF-8"), "text/json") };
        }
    }
}
