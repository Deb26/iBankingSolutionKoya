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
    public partial class frmPassBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        

        [WebMethod]
        public static object GetClientDetails(string SlCode, string OldAcNo)
        {
            //string SlCode = "100175";
            //string OldAcNo = "SB-567";

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

       //Print Passbook License Details
        [WebMethod]
        public static object GetPassbookLicenseDetails(string SlCode)
        {
            DataTable dsInst = new DataTable();

            string output = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
            SqlCommand cmd = new SqlCommand("PROC_PASSBOOKLICENSE", con);
            DataTable dtReceipt = new DataTable();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "PRINTPASSBOOKLICENSE");
            cmd.Parameters.AddWithValue("@SL_CODE", SlCode);
            cmd.CommandTimeout = 0;
            SqlDataAdapter Adap = new SqlDataAdapter();
            Adap.SelectCommand = cmd;
            Adap.Fill(dsInst);
            cmd.Dispose();
            con.Dispose();

            var dictionary = new Dictionary<string, object>();
            dictionary.Add("lisenceName", Convert.ToString(dsInst.Rows[0]["LISCENCEE_NAME"]));
            dictionary.Add("lisenceaddress", Convert.ToString(dsInst.Rows[0]["LISCENCEE_ADDRESS1"]));
            dictionary.Add("cbsacno", Convert.ToString(dsInst.Rows[0]["CBSACNO"]));
            dictionary.Add("acno", Convert.ToString(dsInst.Rows[0]["ACNO"]));
            dictionary.Add("acholdername", Convert.ToString(dsInst.Rows[0]["ACHOLDERNAME"]));
            dictionary.Add("gurdianname", Convert.ToString(dsInst.Rows[0]["GURDIAN_NAME"]));
            dictionary.Add("phoneno", Convert.ToString(dsInst.Rows[0]["PHONE_NO"]));
            dictionary.Add("micrno", Convert.ToString(dsInst.Rows[0]["MICRNo"]));
            dictionary.Add("societybankbranch", Convert.ToString(dsInst.Rows[0]["SocietyBankBranch"]));
            dictionary.Add("societybankbranchaddress", Convert.ToString(dsInst.Rows[0]["SocietyBankBranchAddress"]));
            dictionary.Add("ifsccode", Convert.ToString(dsInst.Rows[0]["IFSCCODE"]));
            dictionary.Add("address", Convert.ToString(dsInst.Rows[0]["ADDRESS"]));
            dictionary.Add("pancardno", Convert.ToString(dsInst.Rows[0]["PANCARDNO"]));


            return dictionary;
        }

    }

    public class PassbookLi
    {
        public string LISCENCEE_NAME { get; set; }
    }




    //public class Passbooks
    //{
    //    public string LISCENCEE_NAME { get; set; }
    //    public string T_DATE { get; set; }
    //    public string Type { get; set; }
    //    public string Narration2 { get; set; }
    //    public string Withdrwal { get; set; }
    //    public string Deposit { get; set; }
    //    public string Balance { get; set; }


    //}
}
