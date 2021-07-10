using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class DataLayer
    {
        private static DataLayer _This;

        private DataLayer()
        {
            strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        }

        public string strConn;

        public static DataLayer GetInstance()
        {
            if (_This == null)
                _This = new DataLayer();
            return _This;
        }

        public int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, null);
        }

        public int ExecuteNonQuery(string sql, SqlParameter[] p)
        {
            int retval = 0;

            SqlConnection cnn = new SqlConnection(strConn);
            cnn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (p != null)
                {
                    for (int i = 0; i <= p.Length - 1; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                retval = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cnn.Close();
            }
            return retval;
        }

        public object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, null);
        }

        public object ExecuteScalar(string sql, SqlParameter[] p)
        {
            object retval = null;
            SqlConnection cnn = new SqlConnection(strConn);
            cnn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (p != null)
                {
                    for (int i = 0; i <= p.Length - 1; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                retval = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cnn.Close();
            }
            return retval;
        }

        public SqlDataReader ExecuteReader(string sql)
        {
            return ExecuteReader(sql, null);
        }

        public SqlDataReader ExecuteReader(string sql, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            cnn.Open();
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (p != null)
                {
                    for (int i = 0; i <= p.Length - 1; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                // cnn.Close()
            }
            return reader;

        }

        public SqlDataReader ExecuteReaderVer2(string sql)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            cnn.Open();
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public DataTable ExecuteDataTable(string sql)
        {
            return ExecuteDataTable(sql, null);
        }

        public DataTable ExecuteDataTable(string sql, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            cnn.Open();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                if (p != null)
                {
                    for (int i = 0; i <= p.Length - 1; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cnn.Close();
            }
            return dt;
        }



        public DataSet ExecuteSet(string sql)
        {
            return ExecuteDataset(sql, null);
        }

        public DataSet ExecuteDataset(string sql, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            cnn.Open();
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                if (p != null)
                {
                    for (int i = 0; i <= p.Length - 1; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cnn.Close();
            }
            return ds;
        }
    }
}
