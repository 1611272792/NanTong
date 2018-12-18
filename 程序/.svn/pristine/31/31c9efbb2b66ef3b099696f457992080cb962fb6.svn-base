using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Text;
using XP_PPS_DAL.Help;

namespace XP_PPS_DAL
{
    public static class DALHelp
    {

        //public static string constr = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "ProductionPlanSystem.sqlite";
        //public static string constr = "Data Source=E:/ProductionPlanSystem/ProductionPlanSystem/ProductionPlanSystem.sqlite";
        public static string constr = AppConfig.ConnString;
        //public static string constr = "Data Source=" + System.Web.HttpContext.Current.Server.MapPath("/") + "ProductionPlanSystem.sqlite";

        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 

        /// </summary> 

        /// <param name="sql">要执行的增删改的SQL语句</param> 

        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 

        /// <returns></returns> 

        public static int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)

        {

            int affectedRows = 0;

            using (SQLiteConnection connection = new SQLiteConnection(constr))

            {

                connection.Open();

                using (DbTransaction transaction = connection.BeginTransaction())

                {

                    using (SQLiteCommand command = new SQLiteCommand(connection))

                    {

                        command.CommandText = sql;

                        if (parameters != null)

                        {

                            command.Parameters.AddRange(parameters);

                        }

                        affectedRows = command.ExecuteNonQuery();

                    }

                    transaction.Commit();

                }

            }

            return affectedRows;

        }

        /// <summary> 

        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 

        /// </summary> 

        /// <param name="sql">要执行的查询语句</param> 

        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 

        /// <returns></returns> 

        public static SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)

        {
            SQLiteConnection connection = new SQLiteConnection(constr);

            SQLiteCommand command = new SQLiteCommand(sql, connection);

            if (parameters != null)

            {

                command.Parameters.AddRange(parameters);

            }
            try
            {
                WriteLog(constr);
                

                connection.Open();
                //while (reader.Read())
                //    Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
                //Console.ReadLine();
                WriteLog(constr);
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                WriteLog("ExecuteReader错误" + ex.Message);
            }

            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary> 

        /// 执行一个查询语句，返回一个包含查询结果的DataTable 

        /// </summary> 

        /// <param name="sql">要执行的查询语句</param> 

        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 

        /// <returns></returns> 

        public static DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)

        {

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(constr))

                {

                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))

                    {

                        if (parameters != null)

                        {

                            command.Parameters.AddRange(parameters);

                        }

                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                        DataTable data = new DataTable();

                        adapter.Fill(data);
                        //WriteLog(constr);
                        return data;

                    }

                }
            }
            catch (Exception ex)
            {
                //if(string.IsNullOrWhiteSpace(constr))
                //{
                //    WriteLog("连接字符串为空");
                //}
                //else
                //{
                //    WriteLog($"constr:{constr}");
                //}
                WriteLog($"错误1{(string.IsNullOrWhiteSpace(constr)? "连接字符串为空":constr)}:"+ex.Message);
                throw;
            }



        }

        /// <summary> 

        /// 执行一个查询语句，返回查询结果的第一行第一列 

        /// </summary> 

        /// <param name="sql">要执行的查询语句</param> 

        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 

        /// <returns></returns> 

        public static Object ExecuteScalar(string sql, SQLiteParameter[] parameters)

        {

            using (SQLiteConnection connection = new SQLiteConnection(constr))

            {

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))

                {

                    if (parameters != null)

                    {

                        command.Parameters.AddRange(parameters);

                    }

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                    DataTable data = new DataTable();

                    adapter.Fill(data);

                    return data;

                }

            }

        }

        /// <summary> 

        /// 查询数据库中的所有数据类型信息 

        /// </summary> 

        /// <returns></returns> 

        public static DataTable GetSchema()

        {

            using (SQLiteConnection connection = new SQLiteConnection(constr))

            {

                connection.Open();

                DataTable data = connection.GetSchema("TABLES");

                connection.Close();

                //foreach (DataColumn column in data.Columns) 

                //{ 

                //        Console.WriteLine(column.ColumnName); 

                //} 

                return data;

            }

        }

        private static void WriteLog(string strmsg)
        {
            strmsg = $"{strmsg}\r\n";
            using (FileStream fs = System.IO.File.Open("D:\\2.txt", FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] bys = Encoding.Default.GetBytes(strmsg);
                fs.Write(bys, 0, bys.Length);
                fs.Close();
            }
        }
    }
}
