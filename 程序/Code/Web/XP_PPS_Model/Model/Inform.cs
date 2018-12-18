using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XP_PPS_Model
{
    /// <summary>
    /// 通知管理
    /// </summary>
    public class Inform
    {
        public int InformID { get; set; }

        /// <summary>
        /// 发放者
        /// </summary>
        public int UserInfoID { get; set; }

        /// <summary>
        /// 通知的具体内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 通知显示的颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 通知显示的大小
        /// </summary>
        public int Size { get; set; }
        
        /// <summary>
        /// 发放时间
        /// </summary>
        public DateTime InformTime { get; set; }
         
    }
}
