using Sunpn.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace XP_PPS_DAL.Help
{
    public class AppConfig
    {
        public static XMLHelper xml = new XMLHelper();
        public static string ConnString
        {
            //E:/IIS发布/PPS/ProductionPlanSystem.sqlite
            get
            {
                try
                {
                    return xml.ReadSunpnNodeValue("ConnString", "Data Source=E:/New/ProductionPlanSystem/ProductionPlanSystem/ProductionPlanSystem.sqlite");
                }
                catch (Exception ex)
                {
                    WriteLog("获取连接字符串失败：" + ex.Message);
                    return "";
                }
            }
            set { xml.UpdateSunpnAttribute("ConnString", value); }
        }

        private static void WriteLog(string strmsg)
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
