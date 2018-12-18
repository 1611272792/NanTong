using ProductionPlanSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using XP_PPS_DAL;

namespace ProductionPlanSystem.Help
{
    public static class ServerOrLit
    {

        /// <summary>
        ///查询  
        /// </summary>
        /// <param name="数据类型"></param>
        /// <param name="sql查询语句"></param>
        /// <returns>一个DataTable</returns>
        public static DataTable isDataTable(string serverOrLite,string sqlstr) {
            DataTable dt = serverOrLite == "sqlserver" ? SqlDbHelper.ExecuteDataTable(sqlstr) : DALHelp.ExecuteDataTable(sqlstr, null);
            return dt;
        }

        /// <summary>
        ///增删改   
        /// </summary>
        /// <param name="数据类型"></param>
        /// <param name="sql查询语句"></param>
        /// <returns>所影响的行数</returns>
        public static int isNum(string serverOrLite, string sqlstr)
        {
            int num = serverOrLite == "sqlserver" ? SqlDbHelper.ExecuteNonQuery(sqlstr) : DALHelp.ExecuteNonQuery(sqlstr, null);
            return num;
        }
    }
}