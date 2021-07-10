using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class CommonDataBaseOperation
    {
        SqlConnection con = DBConnection.sqlConn();
        SqlCommand cmd;
        SqlDataAdapter dataAdaper;
        DataTable dataTable;
        DataSet dataSet;
        int i = 0;
        private object HttpContext;

        /// <summary>
        /// Insert, Update and Delete Operation into DB using Stored Procedure
        /// </summary>
        /// <param name="commandText">The Name of the Stored Procedure</param>
        /// <param name="InParameters">The Hashtable of the SP Input Parameters. Where Key Properties for Parameter Name and Value Properties for Parameter Value</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        /// <returns>Integer</returns>
        public int InsertUpdateDelete(string commandText, Hashtable InParameters, out string SQLError)
        {
            cmd = new SqlCommand(commandText, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SQLError = string.Empty;
            try
            {
                foreach (DictionaryEntry parameter in InParameters)
                {
                    cmd.Parameters.AddWithValue(Convert.ToString(parameter.Key), parameter.Value);
                }
                //cmd.Parameters.AddWithValue("@BranchID", HttpContext.Current.Session["BranchID"]);
                cmd.Parameters.Add("@Error", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Error"].Direction = ParameterDirection.Output;
                if (ConnectionState.Closed == 0)
                {
                    con.Open();
                }
                i = cmd.ExecuteNonQuery();
                SQLError = (string)cmd.Parameters["@ERROR"].Value;
                
            }
            catch (Exception exp)
            {
                SQLError = exp.Message;
            }
            finally
            {
                con.Close();
            }
            return i;
        }

        /// <summary>
        /// Returns a DataTable from DB using Stored Procedure
        /// </summary>
        /// <param name="commandText">The Name of the Stored Procedure</param>
        /// <param name="InParameters">The Hashtable of the SP Input Parameters. Where Key Properties for Parameter Name and Value Properties for Parameter Value</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        /// <returns>Datatable</returns>
        public DataTable GetDataTable(string commandText, Hashtable InParameters, out string SQLError)
        {
            cmd = new SqlCommand(commandText, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry parameter in InParameters)
            {
                cmd.Parameters.AddWithValue(Convert.ToString(parameter.Key), parameter.Value);
            }
            //cmd.Parameters.AddWithValue("@BranchID", HttpContext.Current.Session["BranchID"]);
            cmd.Parameters.Add("@Error", SqlDbType.NVarChar, 500);
            cmd.Parameters["@Error"].Direction = ParameterDirection.Output;
            dataAdaper = new SqlDataAdapter(cmd);
            SQLError = (string)cmd.Parameters["@Error"].Value;
            dataTable = new DataTable();
            dataAdaper.Fill(dataTable);
            return dataTable;
        }

        /// <summary>
        /// Returns a DataTable from DB using Stored Procedure
        /// </summary>
        /// <param name="commandText">The Name of the Stored Procedure</param>
        /// <param name="InParameters">The Hashtable of the SP Input Parameters. Where Key Properties for Parameter Name and Value Properties for Parameter Value</param>
        /// <returns>Datatable</returns>
        public DataTable GetDataTable(string commandText, Hashtable InParameters)
        {
            cmd = new SqlCommand(commandText, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry parameter in InParameters)
            {
                cmd.Parameters.AddWithValue(Convert.ToString(parameter.Key), parameter.Value);
            }
            //cmd.Parameters.AddWithValue("@BranchID", HttpContext.Current.Session["BranchID"]);
            dataAdaper = new SqlDataAdapter(cmd);
            dataTable = new DataTable();
            dataAdaper.Fill(dataTable);
            return dataTable;
        }

        /// <summary>
        /// Returns a DataSet from DB using Stored Procedure
        /// </summary>
        /// <param name="commandText">The Name of the Stored Procedure</param>
        /// <param name="InParameters">The Hashtable of the SP Input Parameters. Where Key Properties for Parameter Name and Value Properties for Parameter Value</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string commandText, Hashtable InParameters, out string SQLError)
        {
            cmd = new SqlCommand(commandText, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry parameter in InParameters)
            {
                cmd.Parameters.AddWithValue(Convert.ToString(parameter.Key), parameter.Value);
            }
            //cmd.Parameters.AddWithValue("@BranchID", HttpContext.Current.Session["BranchID"]);
            cmd.Parameters.Add("@Error", SqlDbType.NVarChar, 500);
            cmd.Parameters["@Error"].Direction = ParameterDirection.Output;
            dataAdaper = new SqlDataAdapter(cmd);
            SQLError = (string)cmd.Parameters["@Error"].Value;
            dataSet = new DataSet();
            dataAdaper.Fill(dataSet);
            return dataSet;
        }

        /// <summary>
        /// Returns a DataSet from DB using Stored Procedure
        /// </summary>
        /// <param name="commandText">The Name of the Stored Procedure</param>
        /// <param name="InParameters">The Hashtable of the SP Input Parameters. Where Key Properties for Parameter Name and Value Properties for Parameter Value</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string commandText, Hashtable InParameters)
        {
            cmd = new SqlCommand(commandText, con);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (DictionaryEntry parameter in InParameters)
            {
                cmd.Parameters.AddWithValue(Convert.ToString(parameter.Key), parameter.Value);
            }
            //cmd.Parameters.AddWithValue("@BranchID", HttpContext.Current.Session["BranchID"]);
            dataAdaper = new SqlDataAdapter(cmd);
            dataSet = new DataSet();
            dataAdaper.Fill(dataSet);
            return dataSet;
        }

    }
}
