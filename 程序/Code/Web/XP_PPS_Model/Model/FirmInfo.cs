using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XP_PPS_Model
{
    /// <summary>
    /// 公司信息表
    /// </summary>
    public class FirmInfo
    {
        public int FirmInfoID { get; set; }

        /// <summary>
        /// 公司logo路径
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// 公司名字
        /// </summary>
        public string CompanyName { get; set; }
    }
}
