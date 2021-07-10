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
    public partial class frmNeftRtgs : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        Decimal Comm = 0;
        Int32 t;
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                GetBankName();
                txttdt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                panelneft.Visible = true;
                btnsubmit1.Visible = true;
                txtbenificiaryAcNo.Attributes["value"] = txtbenificiaryAcNo.Text;
                //GetGridData();

                //GetTime();
                //if (t >= 15)
                //{
                //    panelneft.Visible = false;
                //    btnsubmit1.Visible = false;

                //    lblmessage.Text = "NEFT/RTGS Transaction will be available during 9 am to 3 pm.";
                //}

                //else
                //{
                //    panelneft.Visible = true;
                //    btnsubmit1.Visible = true;

                //}

            }

            string Password = txtbenificiaryAcNo.Text;
            txtbenificiaryAcNo.Attributes.Add("value", Password);
        }
        public class DataObject
        {
            public string Name { get; set; }
        }


        public class Class1
        {
            private const string URL = "https://www.ipksapiwbscb.net/WBSCBAPI/resources/OUTNEFT/QUEUE";
            private string key = "?api_key=123";

            //static void NeftRtgs(string[] args)
            //{
            //    HttpClient client = new HttpClient();
            //    client.BaseAddress = new Uri(URL);
            //    client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage response = client.GetAsync(key).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
            //        foreach (var d in dataObjects)
            //        {
            //            Console.WriteLine("{0}", d.Name);
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //    }
            //    client.Dispose();
            //}
        }
        protected void GetTime()
        {
            //DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            //DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

            DateTime utcTime = DateTime.UtcNow;


            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); //
            t = localTime.Hour;

        }

        protected void GetBankName()
        {
            objBO_Finance.Flag = 1;
            DataTable dt = objBL_Finance.GetBank(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_BankName.DataSource = dt;
                cmbx_BankName.DataTextField = "BANKNAME";
                cmbx_BankName.DataValueField = "BANKNAME";
                cmbx_BankName.DataBind();
            }
        }

        protected void cmbx_BankName_SelectedIndexChanged (object sender, EventArgs e)
        {
            BranchName();
        }

        protected void BranchName()
        {
            objBO_Finance.Flag = 2;
            objBO_Finance.BANKNAME = cmbx_BankName.SelectedValue;
            DataTable dt = objBL_Finance.GetBankBranch(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_BranchName.DataSource = dt;
                cmbx_BranchName.DataTextField = "BRANCHNAME";
                cmbx_BranchName.DataValueField = "BRANCHNAME";
                cmbx_BranchName.DataBind();
            }
        }

        protected void cmbx_BranchName_SelectedIndexChanged (object sender , EventArgs e)
        {
            objBO_Finance.Flag = 4;
            objBO_Finance.BANKNAME = cmbx_BankName.SelectedValue;
            objBO_Finance.BRANCHNAME = cmbx_BranchName.SelectedValue;
            DataSet dt1 = objBL_Finance.GetIFSCCODE(objBO_Finance, out SQLError);
            if (dt1.Tables[0].Rows.Count > 0)
            {
                txtifsCode.Text = Convert.ToString(dt1.Tables[0].Rows[0]["IFSCCode"]);
            }

        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {

            txttotalAmount.Text = Convert.ToString(Convert.ToDecimal(txtcommissionRs.Text) + Convert.ToDecimal(txtamount.Text));
        }
        protected void txtAccountNo_TextChanged (object sender , EventArgs e)
        {
            DataSet data = objBL_Finance.GetAccountDetailsForNEFT(5, txtsenderAcNo.Text, out SQLError);
            if (data.Tables[0].Rows.Count > 0)
            {
                txtsenderName.Text = Convert.ToString(data.Tables[0].Rows[0]["NAME"]);
                txtsenderAddress.Text = Convert.ToString(data.Tables[0].Rows[0]["ADDRESS"]);
                txtMobileNo.Text = Convert.ToString(data.Tables[0].Rows[0]["PHONE"]);
                txtSlCode.Text = Convert.ToString(data.Tables[0].Rows[0]["slcode"]);
                txtavailablebalance.Text = Convert.ToString(data.Tables[0].Rows[0]["BALANCE"]);
                txtpacsid.Text = Convert.ToString(data.Tables[0].Rows[0]["SOCIETYCODE"]);
            }
            else
            {
                MessageBox(this, "No Data Found !! ");
            }
            
        }
        protected void txtSlCode_TextChanged(object sender, EventArgs e)
        {
            DataSet data = objBL_Finance.GetAccountDetailsForNEFT(3, txtSlCode.Text, out SQLError);
            if (data.Tables[0].Rows.Count > 0)
            {
                txtsenderName.Text = Convert.ToString(data.Tables[0].Rows[0]["NAME"]);
                txtsenderAddress.Text = Convert.ToString(data.Tables[0].Rows[0]["ADDRESS"]);
                txtMobileNo.Text = Convert.ToString(data.Tables[0].Rows[0]["PHONE"]);
                txtsenderAcNo.Text = Convert.ToString(data.Tables[0].Rows[0]["ifsc_code"]);
                txtavailablebalance.Text = Convert.ToString(data.Tables[0].Rows[0]["BALANCE"]);
                txtpacsid.Text = Convert.ToString(data.Tables[0].Rows[0]["SOCIETYCODE"]);
            }
            else
            {
                MessageBox(this, "No Data Found !! ");
            }
        }

        protected  void txtCbenacno_selectindex(object sender, EventArgs e)
        {
            if (txtbenificiaryAcNo.Text == txtconfirmBen.Text)
            {
                txtbenificiaryAcNo.Text = txtconfirmBen.Text;
            }
            else
            {
                MessageBox(this, "Account Number Not Matched ");
            }
        }
        protected void txtifscCode_TextChanged(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 7;
            objBO_Finance.IFSCCODE = txtifsCode.Text;
            DataTable dt = objBL_Finance.GetBankBranchBYIFSC(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_BranchName.DataSource = dt;
                cmbx_BranchName.DataTextField = "BRANCHNAME";
                cmbx_BranchName.DataValueField = "BRANCHNAME";
                cmbx_BranchName.DataBind();
            }

            objBO_Finance.Flag = 6;
            objBO_Finance.IFSCCODE = txtifsCode.Text;
            DataTable dt1 = objBL_Finance.GetBankBYIFSC(objBO_Finance, out SQLError);
            if (dt1.Rows.Count > 0)
            {
                cmbx_BankName.DataSource = dt1;
                cmbx_BankName.DataTextField = "BRANCHNAME";
                cmbx_BankName.DataValueField = "BRANCHNAME";
                cmbx_BankName.DataBind();
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
        protected void btnSave_Click (object sender , EventArgs e)
        {
            try
            {
                //objBO_Finance.Flag = btnsubmit1.Text == "Next" ? 1 : 2;
                objBO_Finance.Flag = 1;
                objBO_Finance.SL_CODE = txtSlCode.Text;
                objBO_Finance.SB_SL_CODE = txtsenderAcNo.Text;
                objBO_Finance.sl_Name = Convert.ToString(txtsenderName.Text.ToUpper());
                objBO_Finance.APP_AMOUNT = Convert.ToDouble(txtamount.Text);
                objBO_Finance.COMM = Convert.ToDouble(txtcommissionRs.Text);
                objBO_Finance.AMOUNT = Convert.ToDouble(txttotalAmount.Text);
                objBO_Finance.AC_NO = txtbenificiaryAcNo.Text;
                objBO_Finance.NAME = Convert.ToString(txtbeneficiaryName.Text.ToUpper());
                objBO_Finance.IFSC = Convert.ToString(txtifsCode.Text);
                objBO_Finance.BANKNAME = cmbx_BankName.SelectedValue;
                objBO_Finance.BRANCHNAME = cmbx_BranchName.SelectedValue;
                objBO_Finance.Address1 = Convert.ToString(txtsenderAddress.Text);
                objBO_Finance.Address2 = Convert.ToString(txtbenificiaryAddress.Text);
                objBO_Finance.MODEL_NO = Convert.ToString(txtMobileNo.Text);
                string Cust = txttdt.Text;
                DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.DTSCust = Cust1;
                objBO_Finance.EMPCODE = Convert.ToString(Session["EmpID"]);
                objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

                int i = objBL_Finance.InsertUpdateNEFTRTGS(objBO_Finance, out SQLError);
                if (i > 0)
                {

                    MessageBox(this, "Record Inserted Successfully .");
                    Response.Redirect("~\\Transaction\\frmNEFTRTGSNEXT.aspx");

                }
                else
                {

                    String message = "alert('Something Went Wrong')";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                }
            }
            catch (Exception ex)
            {
               
                MessageBox(this, ex.Message);
                
            }
            finally
            {

            }
        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
    }
}