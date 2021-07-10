using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using BusinessObject;
using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace iBankingSolution.Transaction
{
    public partial class frmNEFTRTGSNEXT : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        Decimal Comm = 0;
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                txttdt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                GetGridData();
                SetInitialRow();
                DisplalyGrid();


            }
        }
        
        protected void GetGridData()
        {
            //objBO_Finance.Flag = 5;
            //string Cust = txttdt.Text;
            //DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //objBO_Finance.DTSCust = Cust1;
            //DataSet dt = objBL_Finance.GetNEFTGrid(objBO_Finance, out SQLError);
            //if (dt.Tables[0].Rows.Count > 0)
            //{
            //    RepCCList.DataSource = dt;
            //    RepCCList.DataBind();

            //}
        }
        protected void gridActs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gridActs_RowDeleting(object sender, EventArgs e)
        {

        }
        protected void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("SocietyCode", typeof(String)));
            dt.Columns.Add(new DataColumn("SocietyACNo", typeof(String)));

            dt.Columns.Add(new DataColumn("SenderName", typeof(String)));
            dt.Columns.Add(new DataColumn("SenderCBSAcNo", typeof(String)));
            dt.Columns.Add(new DataColumn("Amount", typeof(String)));
            dt.Columns.Add(new DataColumn("comm", typeof(String)));
            dt.Columns.Add(new DataColumn("Benf_Name", typeof(String)));
            dt.Columns.Add(new DataColumn("Benf_AcNo", typeof(String)));
            dt.Columns.Add(new DataColumn("IFSCCode", typeof(String)));
            dt.Columns.Add(new DataColumn("BankName", typeof(String)));
            dt.Columns.Add(new DataColumn("BranchName", typeof(String)));

            dt.Columns.Add(new DataColumn("SenderAddress", typeof(String)));
            dt.Columns.Add(new DataColumn("BenificiaryAddress", typeof(String)));
            dt.Columns.Add(new DataColumn("MobileNo", typeof(String)));
            dt.Columns.Add(new DataColumn("EntryDate", typeof(String)));
            dt.Columns.Add(new DataColumn("EntryStatus", typeof(String)));


            dr = dt.NewRow();
            dr["SocietyCode"] = String.Empty;
            dr["SenderName"] = String.Empty;
            dr["SocietyACNo"] = String.Empty;
            dr["Amount"] = String.Empty;
            dr["comm"] = String.Empty;
            dr["Benf_Name"] = String.Empty;
            dr["Benf_AcNo"] = String.Empty;
            dr["IFSCCode"] = String.Empty;
            dr["BankName"] = String.Empty;
            dr["BranchName"] = String.Empty;

            dr["SenderAddress"] = String.Empty;
            dr["BenificiaryAddress"] = String.Empty;
            dr["MobileNo"] = String.Empty;
            dr["EntryDate"] = string.Empty;
            dr["EntryStatus"] = string.Empty;

            dt.Rows.Add(dr);


            ViewState["dt"] = dt;
            BindGrid();

        }
        protected void BindGrid()
        {
            gridActs.DataSource = ViewState["dt"] as DataTable;
            gridActs.DataBind();
            //lblcount.Text = "No of Records: " + gridActs.Rows.Count;
        }
        private void DisplalyGrid()
        {
            string Cust = txttdt.Text;
            DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.DTSCust = Cust1;
            //SqlConnection con = new SqlConnection(strConnString);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", 8);
            cmd.Parameters.AddWithValue("@ENTRYDT", Cust1);
            //cmd.Parameters.AddWithValue("@EntryStatus", "PROCESSING");
            cmd.Parameters.AddWithValue("@Error", "");

            cmd.CommandText = "usp_GETBANKDETAILS";
            cmd.Connection = con;
            try
            {

                con.Open();
                gridActs.EmptyDataText = "No Records Found";
                gridActs.DataSource = cmd.ExecuteReader();


                gridActs.DataBind();
                // lblcount.Text = "No of Records: " + gridActs.Rows.Count;


            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                con.Close();
                con.Dispose();

            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            int i = 0;
           
            
            foreach (var item in gridActs.Rows)
            {
                TextBox txtRemarks = gridActs.Rows[cnt].FindControl("txtremarks") as TextBox;
                TextBox txtacno = gridActs.Rows[cnt].FindControl("txtcbsacno") as TextBox;

                string Cust = txttdt.Text;
                DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                objBO_Finance.Flag = 2;
                objBO_Finance.REMARKS = txtRemarks.Text;
                objBO_Finance.SB_ACNO = txtacno.Text;
                objBO_Finance.ENTRYDATE = Convert.ToString(Cust1);
               
                i = objBL_Finance.updateNEFTRTGS(objBO_Finance, out SQLError);
                cnt = cnt + 1;
            }
            if (i > 0)
            {
                String message = "alert('Updated Successfully')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
            else
            {
                String message = "alert('Unable to Update')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
            //SqlCommand cmd = new SqlCommand("usp_UpdateNEFTRTGS", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Flag", 3);
            //cmd.Parameters.AddWithValue("@Error", "");
            //con.Open();
            //int result = cmd.ExecuteNonQuery();
            //con.Close();
            ////Deletion complete
            //string Msg = "";
            //DataTable dt = new DataTable();
            //int Count = 1;
            //string SlNo = "";
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
            //foreach (GridViewRow row in gridActs.Rows)
            //{

            //    TextBox txtscode = (TextBox)row.FindControl("txtscode");
            //    TextBox txtsACNO = (TextBox)row.FindControl("txtsACNO");

            //    TextBox txtcbsacno = (TextBox)row.FindControl("txtcbsacno");
            //    TextBox txtsendername = (TextBox)row.FindControl("txtsendername");

            //    TextBox txtAmt = (TextBox)row.FindControl("txtAmt");
            //    TextBox txtbenName = (TextBox)row.FindControl("txtbenName");
            //    TextBox txtBenAcNo = (TextBox)row.FindControl("txtBenAcNo");
            //    TextBox txtIFSCcode = (TextBox)row.FindControl("txtIFSCcode");
            //    TextBox txtBankName = (TextBox)row.FindControl("txtBankName");
            //    TextBox txtBranchName = (TextBox)row.FindControl("txtBranchName");
            //    TextBox txtsAddress = (TextBox)row.FindControl("txtsAddress");
            //    TextBox txtbenAddress = (TextBox)row.FindControl("txtbenAddress");
            //    TextBox txtMobileno = (TextBox)row.FindControl("txtMobileno");
            //    TextBox txtremarks = (TextBox)row.FindControl("txtremarks");
            //    TextBox txtEntrydt = (TextBox)row.FindControl("txtEntrydt");
            //    TextBox txtstatus = (TextBox)row.FindControl("txtstatus");
            //    //if (row.Cells[0].Text.Trim() != "&nbsp;")
            //    //{
            //    //    SlNo = row.Cells[0].Text.Trim();
            //    //}
            //    //else

            //    //{
            //    //    SlNo = "";
            //    //}


            //    SqlCommand com = new SqlCommand("insert into CCB_OutwardEntry( SocietyCode , SocietyACNo , SenderCBSAcNo , SenderName , Amount ,  Benf_Name , Benf_AcNo ,  IFSCCode, BankName, BranchName, SenderAddress, BenificiaryAddress, MobileNo , REMARKS , EntryDate , EntryStatus) " +
            //        "values ( '" + txtscode.Text.Trim() + "','" + Convert.ToString(txtsACNO.Text) + "', '" + Convert.ToString(txtcbsacno.Text) + "', '" + Convert.ToString(txtsendername.Text) + "','" + Convert.ToDouble(txtAmt.Text) + "','" + Convert.ToString(txtbenName.Text) + "','" + Convert.ToString(txtBenAcNo.Text) + "', '" + Convert.ToString(txtIFSCcode.Text) + "','" + Convert.ToString(txtBankName.Text) + "','" + Convert.ToString(txtBranchName.Text) + "','" + Convert.ToString(txtsAddress.Text) + "','" + Convert.ToString(txtbenAddress.Text) + "','" + Convert.ToString(txtMobileno.Text) + "','" + Convert.ToString(txtremarks.Text) + "','" + Convert.ToDateTime(txtEntrydt.Text) + "','SEND')", con);



            //        con.Open();
            //        int r = com.ExecuteNonQuery();

            //        con.Close();

            //        Count = Count + 1;


            //}
            ////MessageBox(this, "Record Inserted Successfully .");
            //String message = "alert('Record Inserted Successfully .')";
            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
    }
}