using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sunpn.XML;

namespace ProductionPlanSystem.Help
{
    public class AppConfig
    {
        public static XMLHelper xml = new XMLHelper();

        //管理员id
        public static string Role
        {
            get { return xml.ReadSunpnNodeValue("Role", "管理员"); }
            set { xml.UpdateSunpnAttribute("Role", ""); }
        }

        public static string IP
        {
            get { return xml.ReadSunpnNodeValue("IP", "192.168.15.139:3256"); }
            set { xml.UpdateSunpnAttribute("IP", ""); }
        }

       
    }
}