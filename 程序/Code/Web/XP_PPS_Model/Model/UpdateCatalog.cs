using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XP_PPS_Model
{
    /// <summary>
    /// 更新目录表
    /// </summary>
    public class UpdateCatalog
    {
        public int UpdateCatalogID { get; set; }

        /// <summary>
        /// 终端类型 0安卓 ||  1 window
        /// </summary>
        public int TerminalType { get; set; }

        /// <summary>
        /// 当前的版本号
        /// </summary>
        public string VersionCode { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; }
    }
}
