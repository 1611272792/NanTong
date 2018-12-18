using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XP_PPS_Model;
using XP_PPS_DAL;
using System.Data.SQLite;

namespace XP_PPS_BLL
{
    public class UserInfoBLL
    {
        public static int Add(UserInfo model)
        {
            int sum = UserInfoDAL.Add(model);
            return sum;
        }
        public static int Update(UserInfo model)
        {
            int sum = UserInfoDAL.Update(model);
            return sum;
        }
        public static int Delete(UserInfo model)
        {
            int sum = UserInfoDAL.Delete(model);
            return sum;
        }
        public static SQLiteDataReader Select()
        {
            SQLiteDataReader sum = UserInfoDAL.Select();
            return sum;
        }
        public static SQLiteDataReader Login(UserInfo model)
        {
            SQLiteDataReader sum = UserInfoDAL.Login(model);
            return sum;
        }
    }
}
