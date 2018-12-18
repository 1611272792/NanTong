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
    public class FirmInfoBLL
    {
        public static int Update(FirmInfo model)
        {
            int sum = FirmInfoDAL.Update(model);
            return sum;
        }
        public static SQLiteDataReader Select()
        {
            SQLiteDataReader sum = FirmInfoDAL.Select();
            return sum;
        }
    }
}
