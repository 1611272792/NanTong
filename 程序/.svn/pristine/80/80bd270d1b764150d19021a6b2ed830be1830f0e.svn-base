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
    public static class UserInfoDAL
    {
        public static int Add(UserInfo model)
        {
            //DbType.Int32,4
            string sql = "insert into UserInfo (LoginAccount, LoginPassword) values (@LoginAccount,@LoginPassword)";
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@LoginAccount", DbType.String),
                    new SQLiteParameter("@LoginPassword", DbType.String)}; ;
            parameters[0].Value = model.LoginAccount;
            parameters[1].Value = model.LoginPassword;
            int sum = DALHelp.ExecuteNonQuery(sql, parameters);
            return sum;
        }
        public static int Update(UserInfo model)
        {
            //DbType.Int32,4
            string sql = "update UserInfo set LoginPassword=@LoginPassword where UserInfoID=@UserInfoID";
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@LoginPassword", DbType.String),
                new SQLiteParameter("@UserInfoID", DbType.Int32,4)}; ;
            parameters[0].Value = model.LoginPassword;
            parameters[1].Value = model.UserInfoID;
            int sum = DALHelp.ExecuteNonQuery(sql, parameters);
            return sum;
        }
        public static int Delete(UserInfo model)
        {
            //DbType.Int32,4
            string sql = "delete from UserInfo where UserInfoID=@UserInfoID";
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@UserInfoID", DbType.Int32,4)}; ;
            parameters[0].Value = model.UserInfoID;
            int sum = DALHelp.ExecuteNonQuery(sql, parameters);
            return sum;
        }
        //ExecuteDataTable
        public static SQLiteDataReader Select()
        {
            //DbType.Int32,4
            string sql = "Select * from UserInfo";
            SQLiteDataReader sum = DALHelp.ExecuteReader(sql, null);
            return sum;
        }
        public static SQLiteDataReader Login(UserInfo model)
        {
            string sql = "Select * from UserInfo where LoginAccount=@LoginAccount and LoginPassword=@LoginPassword";
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@LoginAccount", DbType.String),
                    new SQLiteParameter("@LoginPassword", DbType.String)}; ;
            parameters[0].Value = model.LoginAccount;
            parameters[1].Value = model.LoginPassword;

            SQLiteDataReader sum = DALHelp.ExecuteReader(sql, parameters);
            return sum;
        }
    }
}
