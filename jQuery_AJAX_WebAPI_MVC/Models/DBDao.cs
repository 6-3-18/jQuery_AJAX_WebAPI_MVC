using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace jQuery_AJAX_WebAPI_MVC.Models
{
    /// <summary>
    /// 2024/05/22
    /// </summary>
    public class DBDao
    {
        private readonly string ConnStr = ConfigurationManager.ConnectionStrings["MSSQL_DBconnect"].ToString();

        public DBDao()
        {

        }

        public DBDao(string connstr)
        {
            this.ConnStr = connstr;
        }

        /// <summary>
        /// 讀取員工基本檔
        /// 2024/05/22
        /// </summary>
        /// <returns></returns>
        public List<EMP> GetEMPs()
        {
            List<EMP> emps = new List<EMP>();
            string str_sql = string.Empty;
            str_sql = @"SELECT* FROM emp";
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlcommand = new SqlCommand(str_sql);
            try
            {
                using (sqlcommand)
                {
                    sqlcommand.Connection = sqlConnection;
                    sqlConnection.Open();

                    SqlDataReader reader = sqlcommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EMP emp = new EMP
                            {
                                Emp_ID = reader.GetInt32(reader.GetOrdinal("Emp_ID")),
                                Emp_Name = reader.GetString(reader.GetOrdinal("Emp_name")),
                                Age = reader.GetInt32(reader.GetOrdinal("Age")),
                                Birthday = reader.GetString(reader.GetOrdinal("Birthday")),
                            };
                            emps.Add(emp);
                        }
                    }
                    else
                    {
                        //Console.WriteLine("查無資料!");
                        DialogResult Result = MessageBox.Show("查無資料!", "Confirm Message");
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());
                DialogResult Result = MessageBox.Show(ex.Message.ToString(), "Confirm Message");
            }
            finally
            {
                sqlConnection.Close();
            }
            return emps;
        }




        /// <summary>
        /// 新增一筆員工基本檔記錄
        /// 2024/05/23
        /// </summary>
        /// <param name="emp"></param>
        public void InsertEMP(EMP emp)
        {
            string str_sql = string.Empty;
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            str_sql = @"INSERT INTO EMP(Emp_Name,Age,Birthday) VALUES (@Emp_Name,@Age,@Birthday)";
            try
            {
                using (SqlCommand sqlcommand = new SqlCommand(str_sql))
                {
                    sqlcommand.Connection = sqlConnection;
                    //  sqlcommand.Parameters.Add(new SqlParameter("@Emp_ID", emp.Emp_ID));
                    sqlcommand.Parameters.Add(new SqlParameter("@Emp_Name", emp.Emp_Name));
                    sqlcommand.Parameters.Add(new SqlParameter("@Age", emp.Age));
                    sqlcommand.Parameters.Add(new SqlParameter("@Birthday", emp.Birthday));
                    sqlConnection.Open();
                    sqlcommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());
                DialogResult Result = MessageBox.Show(ex.Message.ToString(), "Confirm Message");
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        /// <summary>
        /// 以員工代號查詢員工資料
        /// 2024/05/23
        /// </summary>
        /// <param name="emp_id"></param>
        /// <returns></returns>
        public EMP GetEMPbyID(string emp_id)
        {
            EMP emp = new EMP();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            string str_sql = string.Empty;
            str_sql = @"SELECT * FROM emp where EMP_id=@emp_id";
            try
            {
                using (SqlCommand sqlcommand = new SqlCommand(str_sql))
                {
                    sqlcommand.Connection = sqlConnection;
                    sqlcommand.Parameters.Add(new SqlParameter("@emp_id", emp_id));
                    sqlConnection.Open();
                    SqlDataReader reader = sqlcommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            emp = new EMP
                            {
                                Emp_ID = reader.GetInt32(reader.GetOrdinal("Emp_ID")),
                                Emp_Name = reader.GetString(reader.GetOrdinal("Emp_name")),
                                Age = reader.GetInt32(reader.GetOrdinal("Age")),
                                Birthday = reader.GetString(reader.GetOrdinal("Birthday")),
                            };

                        }
                    }
                    else
                    {
                        emp.Emp_Name = "查無到該筆資料!";   //借姓名欄位顯示警告訊息
                    }

                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());
                DialogResult Result = MessageBox.Show(ex.Message.ToString(), "Confirm Message");
            }
            finally
            {
                sqlConnection.Close();
            }
            return emp;
        }

        /// <summary>
        ///  以員工代號修改員工資料
        ///  2024/05/23
        /// </summary>
        /// <param name="emp"></param>
        public void UpdateEMP(EMP emp)
        {
            SqlConnection sqlconnection = new SqlConnection(ConnStr);
            string str_sql = string.Empty;
            str_sql = @"UPDATE emp SET emp_name =@Emp_Name,age=@Age,birthday=@Birthday WHERE emp_id = @Emp_id ";
            SqlCommand sqlcommand = new SqlCommand(str_sql);
            try
            {
                using (sqlcommand)
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.Parameters.Add(new SqlParameter("@Emp_Name", emp.Emp_Name));
                    sqlcommand.Parameters.Add(new SqlParameter("@Age", emp.Age));
                    sqlcommand.Parameters.Add(new SqlParameter("@Birthday", emp.Birthday));
                    sqlcommand.Parameters.Add(new SqlParameter("@Emp_id", emp.Emp_ID));
                    sqlconnection.Open();
                    sqlcommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());
                DialogResult Result = MessageBox.Show(ex.Message.ToString(), "Confirm Message");
            }
            finally
            {
                sqlconnection.Close();
            }


        }

        /// <summary>
        ///  以員工代號刖除一筆員工資料
        ///  2024/05/23
        /// </summary>
        /// <param name="emp_id"></param>
        public void DeleteEMPbyID(string emp_id)
        {
            SqlConnection sqlconnection = new SqlConnection(ConnStr);
            string str_sql = string.Empty;
            str_sql = @"DELETE FROM emp WHERE emp_id = @Emp_id ";
            SqlCommand sqlcommand = new SqlCommand(str_sql);
            try
            {
                using (sqlcommand)
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.Parameters.Add(new SqlParameter("@Emp_id", emp_id));
                    sqlconnection.Open();
                    sqlcommand.ExecuteNonQuery();
                    sqlconnection.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());
                DialogResult Result = MessageBox.Show(ex.Message.ToString(), "Confirm Message");
            }
            finally
            {
                sqlconnection.Close();
            }

        }
    }

}