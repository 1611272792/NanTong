using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XP_PPS_DAL;
using XP_PPS_Model;

namespace XP_PPS_BLL
{
    public static class UserGroupsBLL
    {
        public static int Add(UserGroups model)
        {
            int sum = UserGroupsDAL.Add(model);
            return sum;
        }
        public static int UpdateName(UserGroups model)
        {
            int sum = UserGroupsDAL.UpdateName(model);
            return sum;
        }
        public static int UpdateAuthorId(UserGroups model)
        {
            int sum = UserGroupsDAL.UpdateAuthorId(model);
            return sum;
        }
        public static int Delete(UserGroups model)
        {
            int sum = UserGroupsDAL.Delete(model);
            return sum;
        }
        public static SQLiteDataReader Select()
        {
            SQLiteDataReader sum = UserGroupsDAL.Select();
            return sum;
        }
    }
}
