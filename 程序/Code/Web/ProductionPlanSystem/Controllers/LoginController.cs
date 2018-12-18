﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XP_PPS_DAL;
using XP_PPS_Model;
using XP_PPS_BLL;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Text;
using ProductionPlanSystem.Help;
using System.Configuration;
using System.Management;
using SchoolWeb.Models;

namespace ProductionPlanSystem.Controllers
{
    public class LoginController : Controller
    {
        public static readonly string connType = ConfigurationManager.ConnectionStrings["connType"].ConnectionString;
        // GET: Login
        public ActionResult Index()
        {
            string Jqm = SelectJqm();
            //    Jqm = Convert.ToBase64String(Encoding.Default.GetBytes(Jqm));//把机器码加密为base64
            //string sqls = string.Format("select keys from Keys where keys='{0}'",Jqm);
            //DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqls);
           

            return View();
        }
        public ActionResult Login(string UserName, string UserPwd, string UserCode)
        {
            try
            {
                if (UserName == "" || UserPwd == "" || UserCode == "")
                {
                    return Content("nokong");
                }
                else
                {
                    string code = Session["code"].ToString();
                   
                    if (!code.Equals(UserCode))
                    {
                        return Content("yno"); 
                    }
                    else
                    {
                        string sqlrole = string.Format("select UserGroupsName from UserGroups where UserGroupsID=(select UserGroupsID from UserInfo where LoginAccount='{0}')", UserName);
                        DataTable dtrole = ServerOrLit.isDataTable(LoginController.connType, sqlrole);
                      
                        
                        if (dtrole.Rows.Count > 0)
                        {
                            Session["Role"] = dtrole.Rows[0]["UserGroupsName"].ToString();
                        }
                        string sql = string.Format("select UserName from UserInfo where LoginAccount='{0}'", UserName);
                        DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sql);
                        if (dt.Rows.Count > 0)
                        {
                            Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                        }
                      
                        if (connType == "sqlserver")
                        {
                            //数据库为sqlserver时
                            string sqls =string.Format("Select * from UserInfo where LoginAccount='{0}' and LoginPassword='{1}'",UserName,UserPwd);
                            DataTable ds = SqlDbHelper.ExecuteDataTable(sqls);
                            if (ds.Rows.Count>0)
                            {
                                Session["UserId"] = ds.Rows[0]["LoginAccount"];
                                Session["LoginPwd"] = ds.Rows[0]["LoginPassword"];
                                Session["UserInfoID"] = ds.Rows[0]["UserInfoID"];
                                return Content("ok");
                            }
                            else
                            {
                                return Content("yhmno");
                            }
                        }
                        else
                        {
                            //数据库为Sqlite时
                            UserInfo model = new UserInfo();
                            model.LoginAccount = UserName;
                            model.LoginPassword = /*MD5Service.GetMD5CodeToString(*/UserPwd;
                            SQLiteDataReader reader = UserInfoBLL.Login(model);
                            bool Falsh = false;
                            while (reader.Read())
                            {
                                model.UserInfoID = Convert.ToInt32(reader["UserInfoID"]);
                                Falsh = true;
                            }
                            if (Falsh == true)
                            {
                             
                                Session["UserId"] = model.LoginAccount;
                                Session["LoginPwd"] = model.LoginPassword;
                                Session["UserInfoID"] = model.UserInfoID;
                                return Content("ok");
                            }
                            else
                            {
                                return Content("yhmno");
                            }
                        }
                      
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog("错误：" + e.Message);
                return Content("yhmno");
            }
        }

        //验证码控制器代码
        public ActionResult ShowValidateCode()
        {
            ValidateCode ValidateCode = new ValidateCode();
            string code = ValidateCode.CreateValidateCode(4);//生成验证码，传几就是几位验证码
            Session["code"] = code;
            byte[] buffer = ValidateCode.CreateValidateGraphic(code);//把验证码画到画布
            return File(buffer, "image/jpeg");
        }
        private void WriteLog(string strmsg)
        {
            strmsg = $"{strmsg}\r\n";
            using (FileStream fs = System.IO.File.Open("D:\\3.txt", FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] bys = Encoding.Default.GetBytes(strmsg);
                fs.Write(bys, 0, bys.Length);
                fs.Close();
            }
        }

        ///   <summary> 
        ///   获取cpu序列号     
        ///   </summary> 
        ///   <returns> string </returns> 
        public string GetCpuInfo()
        {
            string cpuInfo = "";
            try
            {
                using (ManagementClass cimobject = new ManagementClass("Win32_Processor"))
                {
                    ManagementObjectCollection moc = cimobject.GetInstances();

                    foreach (ManagementObject mo in moc)
                    {
                        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                        mo.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cpuInfo.ToString();
        }
        //获取机器码
        public string SelectJqm()
        {
            MachineCode c = new MachineCode();
            string strCpu = c.GetCpuInfo() + c.GetMoAddress();//+c.GetHDid()
            return strCpu;
            //try
            //{
            //    string strCpu = null;
            //    ManagementClass myCpu = new ManagementClass("win32_Processor");
            //    ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            //    using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = myCpuConnection.GetEnumerator())
            //    {
            //        if (enumerator.MoveNext())
            //        {
            //            ManagementObject myObject = (ManagementObject)enumerator.Current;
            //            strCpu = myObject.Properties["Processorid"].Value.ToString();
            //        }
            //    }
            //    string cpuNum = GetCpuInfo();
            //    return strCpu+cpuNum;
            //}
            //catch (Exception)
            //{
            //    return "获取错误";
            //}

        }
        //是否激活状态
        public ActionResult keyState()
        {
            //return Content("success");
            string sqls = string.Format("select keys from Keys");
            DataTable dt = ServerOrLit.isDataTable(LoginController.connType, sqls);
            //判断数据库是否存在激活码
            if (dt.Rows.Count > 0)
            {
                try
                {
                    string orgStr = Encoding.Default.GetString(Convert.FromBase64String(dt.Rows[0][0].ToString()));//从数据库解码2018/6/1_10_BFEBFBFF000906E9_gHEREdsfsdewHDFS=
                    string[] jiqima = orgStr.Split('_');//获取保存的的机器码
                    Session["TermialNumber"] = jiqima[1];//保存终端数量
                                                         //Session["validTime"] = jiqima[0];//保存有效时间
                    DateTime nowtime = DateTime.Now;
                    TimeSpan timeNum = DateTime.Parse(jiqima[0].ToString()) - nowtime;
                    if (SelectJqm() == jiqima[2])
                    {
                        if (timeNum.Days < 0)
                        {
                            //时间到期删除激活码
                            deleteKeys();
                            //ServerOrLit.isNum(LoginController.connType, "delete from Keys");
                            return Content("noTime");
                        }
                        else
                        {
                            return Content("success");
                        }

                    }
                    else
                    {
                        deleteKeys();
                        return Content("error");
                    }
                }
                catch (Exception)
                {
                    deleteKeys();
                    throw;
                }


            }
            else
            {
                return Content(SelectJqm());
            }

        }

       
        //添加激活码到数据库
        public ActionResult addKeys(string Keys)
        {
            try
            {
                string jieKeys = Encoding.Default.GetString(Convert.FromBase64String(Keys));//获取输入的激活码解码状态
                string[] jiqima = jieKeys.Split('_');//获取保存的的机器码
                 
                if (jiqima[2] == SelectJqm())
                {
                    string sqls = string.Format("insert into Keys(keys) values('{0}')", Keys);
                    int info = ServerOrLit.isNum(LoginController.connType, sqls);
                    return Content("恭喜！激活成功");
                }
                else
                {
                    deleteKeys();
                    return Content("激活码有误");
                   
                }
            }
            catch (Exception)
            {
                deleteKeys();
                return Content("激活码有误");
           
            }
          
        }
        //删除激活码
        public void deleteKeys() {
            ServerOrLit.isNum(LoginController.connType, "delete from Keys");
        }
    }
}