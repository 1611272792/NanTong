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
    public static class UserGroupsDAL
    {
        public static int Add(UserGroups model)
        {
            //DbType.Int32,4
            string sql = "insert into UserGroups (UserGroupsName,AuthorId) values (@UserGroupsName,@AuthorId)";
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@UserGroupsName", DbType.String),
                    new SQLiteParameter("@AuthorId", DbType.String)}; ;
            parameters[0].Value = model.UserGroupsName;
            parameters[1].Value = model.AuthorId;
            int sum = DALHelp.ExecuteNonQuery(sql, parameters);
            return sum;
        }
        public static int UpdateName(UserGroups model)
        {
            //DbType.Int32,4
            string sql = "update UserGroups set UserGroupsName=@UserGroupsName where UserGroupsID=@UserGroupsID";
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@UserGroupsName", DbType.String),
                new SQLiteParameter("@UserGroupsID", DbType.Int32,4)}; ;
            parameters[0].Value = model.UserGroupsName;
            parameters[1].Value = model.UserGroupsID;
            int sum = DALHelp.ExecuteNonQuery(sql, parameters);
            return sum;
        }
        public static int UpdateAuthorId(UserGroups model)
        {
            //DbType.Int32,4
            string sql = "update UserGroups set AuthorId=@AuthorId where UserGroupsID=@UserGroupsID";
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@AuthorId", DbType.String),
                new SQLiteParameter("@UserGroupsID", DbType.Int32,4)}; ;
            parameters[0].Value = model.AuthorId;
            parameters[1].Value = model.UserGroupsID;
            int sum = DALHelp.ExecuteNonQuery(sql, parameters);
            return sum;
        }
        public static int Delete(UserGroups model)
        {
            //DbType.Int32,4
            string sql = "delete from UserGroups where UserGroupsID=@UserGroupsID";
            SQLiteParameter[] parameters = {
                new SQLiteParameter("@UserGroupsID", DbType.Int32,4)}; ;
            parameters[0].Value = model.UserGroupsID;
            int sum = DALHelp.ExecuteNonQuery(sql, parameters);
            return sum;
        }
        public static SQLiteDataReader Select()
        {
            //DbType.Int32,4
            string sql = "Select * from UserGroups";
            SQLiteDataReader sum = DALHelp.ExecuteReader(sql, null);
            return sum;
        }
    }
}
