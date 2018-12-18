using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XP_PPS_Model;

namespace XP_PPS_DAL
{
    public class FirmInfoDAL
    {
        public static int Update(FirmInfo model)
        {
            //DbType.Int32,4
            string sql = "update FirmInfo set LogoUrl=@LogoUrl,CompanyName=@CompanyName where FirmInfoID=@FirmInfoID";
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@LogoUrl", DbType.String),
                new SQLiteParameter("@CompanyName", DbType.String),
                new SQLiteParameter("@FirmInfoID",DbType.Int32,4)
            }; ;
            parameters[0].Value = model.LogoUrl;
            parameters[1].Value = model.CompanyName;
            parameters[2].Value = model.FirmInfoID;
            int sum = DALHelp.ExecuteNonQuery(sql, parameters);
            return sum;
        }
        public static SQLiteDataReader Select()
        {
            //DbType.Int32,4
            string sql = "Select * from FirmInfo";
            SQLiteDataReader sum = DALHelp.ExecuteReader(sql, null);
            return sum;
        }
    }
}
