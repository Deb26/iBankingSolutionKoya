using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace iBankingSolution.Transaction
{
    public partial class PassBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static object GetClientDetails(string SlCode, string OldAcNo)
        {
            DataTable dsInst = new DataTable();

            string output = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);

            // SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
            SqlCommand cmd = new SqlCommand("PRC_PASSBOOK", con);
            DataTable dtReceipt = new DataTable();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "GETACCOUNTDETAILS");
            cmd.Parameters.AddWithValue("@SL_CODE", SlCode);
            cmd.Parameters.AddWithValue("@OLD_ACNO", OldAcNo);
            cmd.CommandTimeout = 0;
            SqlDataAdapter Adap = new SqlDataAdapter();
            Adap.SelectCommand = cmd;
            Adap.Fill(dsInst);
            cmd.Dispose();
            con.Dispose();

            if (dsInst != null && dsInst.Rows.Count > 0)
            {
                output = Convert.ToString(dsInst.Rows[0]["SL_CODE"]) + "~" + Convert.ToString(dsInst.Rows[0]["OLD_ACNO"]) + "~" + Convert.ToString(dsInst.Rows[0]["P_DATE"]) + "~" + Convert.ToString(dsInst.Rows[0]["LINE"]);
            }

            return output;
        }
        [WebMethod]
        public static object GetPassbookDetails(string SlCode)
        {
            DataTable dsInst = new DataTable();

            string output = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);

            // SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
            SqlCommand cmd = new SqlCommand("PRC_PASSBOOK", con);
            DataTable dtReceipt = new DataTable();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "GETPASSBOOKPRINT");
            cmd.Parameters.AddWithValue("@SL_CODE", SlCode);
            cmd.CommandTimeout = 0;
            SqlDataAdapter Adap = new SqlDataAdapter();
            Adap.SelectCommand = cmd;
            Adap.Fill(dsInst);
            cmd.Dispose();
            con.Dispose();

            List<Passbooks> lst = new List<Passbooks>();

            if (dsInst != null && dsInst.Rows.Count > 0)
            {
                lst = (from DataRow dr in dsInst.Rows
                       select new Passbooks()
                       {
                           T_DATE = Convert.ToString(dr["T_DATE"]),
                           Type = Convert.ToString(dr["Type"]),
                           Narration2 = Convert.ToString(dr["Narration2"]),
                           Withdrwal = Convert.ToString(dr["AMT_DEBIT"]),
                           Deposit = Convert.ToString(dr["AMT_CREDIT"]),
                           Balance = Convert.ToString(dr["Balance"])
                       }).ToList();
            }

            return lst;
        }

        [WebMethod]
        public static object SavePassbookDetails(string SlCode, string Line)
        {
            DataTable dsInst = new DataTable();

            string output = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);

            // SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
            SqlCommand cmd = new SqlCommand("PRC_PASSBOOK", con);
            DataTable dtReceipt = new DataTable();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SAVEPASSBOOKPRINT");
            cmd.Parameters.AddWithValue("@SL_CODE", SlCode);
            cmd.Parameters.AddWithValue("@LINE", Line);

            cmd.CommandTimeout = 0;
            SqlDataAdapter Adap = new SqlDataAdapter();
            Adap.SelectCommand = cmd;
            Adap.Fill(dsInst);
            cmd.Dispose();
            con.Dispose();

            return "";
        }
    }

    public class Passbooks
    {
        public string T_DATE { get; set; }
        public string Type { get; set; }
        public string Narration2 { get; set; }
        public string Withdrwal { get; set; }
        public string Deposit { get; set; }
        public string Balance { get; set; }


    }
}