using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XP_PPS_Model
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Author
    {
        public int AuthorId { get; set; }
        /// <summary>
        /// 功能块名称
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int p_id { get; set; }
        /// <summary>
        /// 项目地址
        /// </summary>
        public string url { get; set; }

        //图标
        public string image { get; set; }

        public int Serial { get; set; }
    }
}
