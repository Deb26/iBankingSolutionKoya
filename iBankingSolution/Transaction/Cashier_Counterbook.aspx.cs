using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using System.Data.SqlClient;
using BLL;
using System.Collections;
using System.Globalization;
using BLL.GeneralBL;
using System.Configuration;
using System.Web.Services;
using System.Net.Http;
using System.Web.Configuration;

namespace iBankingSolution.Transaction
{
    public partial class Cashier_Counterbook : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                BindLedger();
                BindtransferACNo();

                dtpkr_EntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //dtpkr_DateOfInstrument.Text = DateTime.Now.ToString("dd/MM/yyyy");

            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }
        protected void BindtransferACNo() 
        {
            try
            {
                objBO_Finance.Flag = 1;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetAllAcctNo(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_TransferTo.DataSource = dt;
                    cmbx_TransferTo.DataValueField = "sl_code";
                    cmbx_TransferTo.DataTextField = "sl_code";
                    cmbx_TransferTo.DataBind();

                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {

            }

        }
        protected void BindLedger()
        {
            try
            {
                objBO_Finance.Flag = 3;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_Ledger.DataSource = dt;
                    cmbx_Ledger.DataValueField = "LDG_CODE";
                    cmbx_Ledger.DataTextField = "NOMENCLATURE";
                    cmbx_Ledger.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        //Get Data Using SL_CODE
        //[WebMethod]
        //public static object GetDetailsSLCODE(string SlCode)
        //{
        //    //DataTable dsInst = new DataTable();
        //    DataSet dsInst = new DataSet();

        //    string output = "";
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("USP_GETACCOUNTHOLDERSDETAILSBYSL_CODE", con);
        //    DataTable dtReceipt = new DataTable();

        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@FLAG", 5);
        //    cmd.Parameters.AddWithValue("@SL_CODE", SlCode);
        //    cmd.Parameters.AddWithValue("@ERROR", "");
        //    cmd.CommandTimeout = 0;
        //    SqlDataAdapter Adap = new SqlDataAdapter();
        //    Adap.SelectCommand = cmd;
        //    Adap.Fill(dsInst);
        //    cmd.Dispose();
        //    con.Dispose();

        //    var dictionary = new Dictionary<string, object>();
        //    dictionary.Add("oldacno", Convert.ToString(dsInst.Tables[1].Rows[0]["OLD_ACNO"]));
           
        //    return dictionary;
        //}
        protected void cmbx_AccountNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbx_EntryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbx_EntryType.SelectedItem.Text == "Receipt")
            {
                //cmbx_InstrumentType.Items.Clear();
                //cmbx_InstrumentType.Items.Add("DepositSlip");
                //cmbx_InstrumentType.Items.Add("Receipt");
                //cmbx_InstrumentType.Items.Add("Voucher");
                //cmbx_InstrumentType.Items.Add("Others");
                ResetControlsSLCode();
                lbl_AmountCredited.Text = "Amount Debited";
                lbl_AmountDebited.Text = "Amount Credited";
                ntxt_AmountCredited.Enabled = false;

            }
            else if (cmbx_EntryType.SelectedItem.Text == "Payment")
            {
                //cmbx_InstrumentType.Items.Clear();
                //cmbx_InstrumentType.Items.Add("WithdrawalSlip");
                //cmbx_InstrumentType.Items.Add("Voucher");
                //cmbx_InstrumentType.Items.Add("Cheque");
                //cmbx_InstrumentType.Items.Add("Others");
                ResetControlsSLCode();
                lbl_AmountCredited.Text = "Amount Credited";
                lbl_AmountDebited.Text = "Amount Debited";
                ntxt_AmountCredited.Enabled = false;

            }

        }

        protected void ResetControls()
        {
            ntxt_AmountDebited.Text = String.Empty;
            txt_AcctNo.Text = String.Empty;
            ntxt_AmountCredited.Text = String.Empty;
            ////dtpkr_EntryDate.Text = String.Empty;
            
            btnsubmit1.Text = "Save";
            txt_Narration.Text = String.Empty;
            cmbx_EntryType.SelectedIndex = -1;
            //Txt_Pan.Text = String.Empty;
            //Txt_Adhar.Text = String.Empty;
            //txt_Voter.Text = String.Empty;
            txt_AcctStatus.Text = String.Empty;
            txt_ActualBalance.Text = "";
            txt_AcctType.Text = "";
            //cmbx_InstrumentType.SelectedIndex = -1;
            //txt_InstrumentNo.Text = String.Empty;
            lv_AcctHolders.DataSource = null;
            lv_AcctHolders.DataBind();
            cmbx_TransferTo.SelectedIndex = -1;
            cmbx_Ledger.SelectedIndex = -1;
            cmbx_InterestIssueFrom.SelectedIndex = -1;
            ntxt_AvailableBalance.Text = String.Empty;
            txtCBS_AcNo.Text = String.Empty;
            txt_OldAcctNo.Text = String.Empty;
            //dtpkr_EntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


        }
        string dtpkrEntryDate = "";
        protected void btnsubmit_Click(object sender, EventArgs e)
        {


            //dtpkr_EntryDate.Text = dtpkr.Value;
            //CultureInfo provider = CultureInfo.InvariantCulture;
            //DateTime dateTimeObj = DateTime.ParseExact(dtpkr_EntryDate.Text, "yyyy-MM-dd", provider);
            //dtpkr_EntryDate.Text = dateTimeObj.ToString("dd/MM/yyyy");

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[]
                {
                new DataColumn("LDG_CODE"),
                new DataColumn("SL_CODE"),
                new DataColumn("AMT_DEBIT"),
                new DataColumn("AMT_CREDIT")

                });
                if (cmbx_EntryType.SelectedValue == "r")
                {
                    dt.Rows.Add(DBNull.Value, "990000", ntxt_AmountDebited.Text, 0m);
                    dt.Rows.Add(DBNull.Value, txt_AcctNo.Text, 0m, ntxt_AmountDebited.Text);
                }
                else
                {
                    dt.Rows.Add(DBNull.Value, "990000", 0m, ntxt_AmountDebited.Text);
                    dt.Rows.Add(DBNull.Value, txt_AcctNo.Text, ntxt_AmountDebited.Text, 0m);
                }
                //objBO_Finance.dtLedgerDetails = dt;

                objBO_Finance.dtLedgerDetails = dt;
                objBO_Finance.CCB_DATE = DateTime.ParseExact(dtpkr_EntryDate.Text.Trim(), @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);// Convert.ToDateTime(dtpkr_EntryDate.Text);
          
                objBO_Finance.VOUCHER_NO = btnsubmit1.CommandArgument;
                objBO_Finance.NARRATION1 = txt_Narration.Text;
                objBO_Finance.NARRATION2 = txt_Narration.Text;
                objBO_Finance.PAYMENT_RECEIPT = cmbx_EntryType.SelectedValue;
                objBO_Finance.TYPEOFINSTRUMENT = "0";
                objBO_Finance.INSTRUMENTNO = "0";
                objBO_Finance.DT_ON_INS = System.DateTime.Now;
                objBO_Finance.DAILY_SRL = "0";
                objBO_Finance.ENTRY_FR = 1;
                objBO_Finance.EMPCODE = Convert.ToString(Session["EmpID"]);
                objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                objBO_Finance.Flag = 1;
                int i = objBL_Finance.InsertUpdateDeleteCashierCounterBook(objBO_Finance, out SQLError);


                if (i > 0)
                {
                    if (  btnsubmit1.Text == "Save")
                    {
                        if (txtphone.Text != "")
                        {
                            sendSMS();
                        }
                        ResetControls();
                        message = "alert('Save Successfully.')";
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        //MessageBox(this, "Record Save Successfully . Allotted Voucher is:-" + SQLError);
                        MessageBox(this, "Record Save Successfully . Allotted Voucher is:-" + SQLError);

                        btnsubmit1.Text = "Save";
                        Label1.Visible = false;
                        DivID.Visible = true;
                        Label1.Text = "Alloted Voucher is:" + SQLError;

                        objBO_Finance.Flag = 1;
                        ResetControls();

                    }
                    if (btnsubmit1.Text == "Update" )
                    {
                        message = "alert('Update Successfully.')";

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                      
                        btnsubmit1.Text = "Save";

                        ResetControls();
                    }


                }
                else
                {

                    message = "alert('Something Wrong Input.')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {

            }


        }
        public string sendSMS()
        {
            String SMSmessage = "";
            if (cmbx_EntryType.SelectedValue == "r")
            {
                string societyname = WebConfigurationManager.AppSettings["societyname"];

                SMSmessage = "(" + societyname + ")" + ": Your A/c " + txt_AcctNo.Text + " is credited Rs " + ntxt_AmountDebited.Text + " on " + dtpkr_EntryDate.Text + ". A/c Balance is Rs " + ntxt_AvailableBalance.Text + ".";
            }
            else
            {
                string societyname = WebConfigurationManager.AppSettings["societyname"];

                SMSmessage = "(" + societyname + ")" + ": Your A/c " + txt_AcctNo.Text + " is debited Rs " + ntxt_AmountDebited.Text + " on " + dtpkr_EntryDate.Text + ".A / c Balance is Rs " + ntxt_AvailableBalance.Text + ".";
            }


            // Get User details form Webconfig
            string SMSUserName = WebConfigurationManager.AppSettings["SMSUser"];
            string SMSPass = WebConfigurationManager.AppSettings["SMSPass"];
            string SMSSender = WebConfigurationManager.AppSettings["SMSSender"];

            // Set SMS URL 
            string URL = WebConfigurationManager.AppSettings["SMSURLInit"];
            string urlParameters = "?username=" + SMSUserName + "&password=" + SMSPass + "&sender=" + SMSSender + "&to=" + txtphone.Text + "&message=" + SMSmessage;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);



            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                return "Success";
            }
            else
            {
                return "not";
            }
        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cashier_Counterbook.aspx");
        }

        protected void dtpkr_CollectionDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void AmtTextChange()
        {
            ntxt_AmountCredited.Text = ntxt_AmountDebited.Text;
            if (cmbx_EntryType.SelectedItem.Text == "Receipt")
            {
                ntxt_AvailableBalance.Text = Convert.ToString(Convert.ToDouble(txt_ActualBalance.Text) + Convert.ToDouble(ntxt_AmountDebited.Text));
            }
            else
            {
                ntxt_AvailableBalance.Text = Convert.ToString(Convert.ToDouble(txt_ActualBalance.Text) - Convert.ToDouble(ntxt_AmountDebited.Text));
            }
        }
        /*
        protected void txt_OldAcctNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //dtpkr_EntryDate.Text = dtpkr.Value;
                //CultureInfo provider = CultureInfo.InvariantCulture;
                //DateTime dateTimeObj = DateTime.ParseExact(dtpkr_EntryDate.Text, "yyyy-MM-dd", provider);
                //dtpkr_EntryDate.Text = dateTimeObj.ToString("dd/MM/yyyy");

                DataSet data = objBL_Finance.GetAccountHoldersDetailsBySL_CODE(1, txt_AcctNo.Text, DateTime.ParseExact(dtpkr_EntryDate.Text, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), out SQLError);
                if (data.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(data.Tables[1].Rows[0]["ac_status"]) == "Live")
                    {
                        lv_AcctHolders.DataSource = data.Tables[0];
                        lv_AcctHolders.DataBind();
                        txt_AcctType.Text = Convert.ToString(data.Tables[1].Rows[0]["actype"]);
                        txt_AcctStatus.Text = Convert.ToString(data.Tables[1].Rows[0]["ac_status"]);
                        txt_AcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["sl_code"]);
                        ViewState["ActualBalance"] = txt_ActualBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
                        ntxt_AvailableBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
                        ntxt_AmountDebited.Text = ntxt_AmountCredited.Text;
                        txtCBS_AcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["CBSAcNo"]);
                        ////Txt_Adhar.Text = Convert.ToString(data.Tables[0].Rows[0]["ADHAR_NO"]);
                        ////txt_Voter.Text = Convert.ToString(data.Tables[0].Rows[0]["VOTER_CARD_NO"]);
                        ////Txt_Pan.Text = Convert.ToString(data.Tables[0].Rows[0]["PAN_CARD_NO"]);

                        //if (cmbx_EntryType.SelectedValue == "r")
                        //{
                        //    //ntxt_AmountDebited.Text = Double.MaxValue;
                        //    //ntxt_AmountCredited.MaxValue = Double.MaxValue;
                        //    ntxt_AmountDebited.Text = ntxt_AmountCredited.Text;
                        //}
                        //else
                        //{
                        //    //ntxt_AmountDebited.MaxValue = Convert.ToDouble(ViewState["ActualBalance"] != null ? ViewState["ActualBalance"] : "0.00");
                        //    //ntxt_AmountCredited.MaxValue = Convert.ToDouble(ViewState["ActualBalance"] != null ? ViewState["ActualBalance"] : "0.00");
                        //}
                    }
                    else
                    {
                        message = "alert('Account is Closed.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        txt_AcctNo.Text = "";
                    }
                }
                else
                {
                    message = "alert('Invalid Account.')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    txt_AcctNo.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }*/
        protected void txt_OldAcctNo_TextChanged(object sender, EventArgs e) //New 
        {
            try
            {
                DataSet data = objBL_Finance.GetAccountHoldersDetailsBySL_CODEOLD(1, txt_OldAcctNo.Text, Convert.ToString(Session["BranchID"]), DateTime.ParseExact(dtpkr_EntryDate.Text, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), out SQLError);
                if (data.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(data.Tables[1].Rows[0]["ac_status"]) == "Live")
                    {
                        lv_AcctHolders.DataSource = data.Tables[0];
                        lv_AcctHolders.DataBind();
                        txt_AcctType.Text = Convert.ToString(data.Tables[1].Rows[0]["actype"]);
                        txt_AcctStatus.Text = Convert.ToString(data.Tables[1].Rows[0]["ac_status"]);
                        txt_AcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["sl_code"]);
                        txt_OldAcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["old_acno"]);
                        ViewState["ActualBalance"] = txt_ActualBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
                        ntxt_AvailableBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
                        ntxt_AmountDebited.Text = ntxt_AmountCredited.Text;
                        txtCBS_AcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["CBSAcNo"]);
                        txtBranchName.Text = Convert.ToString(data.Tables[4].Rows.Count > 0 ? data.Tables[5].Rows[0]["BranchName"] : "");
                        txtphone.Text = Convert.ToString(data.Tables[6].Rows.Count > 0 ? data.Tables[6].Rows[0]["TEL_NO"] : "");
                        //Txt_Adhar.Text = Convert.ToString(data.Tables[0].Rows[0]["ADHAR_NO"]);
                        //txt_Voter.Text = Convert.ToString(data.Tables[0].Rows[0]["VOTER_CARD_NO"]);
                        //Txt_Pan.Text = Convert.ToString(data.Tables[0].Rows[0]["PAN_CARD_NO"]);

                        //if (cmbx_EntryType.SelectedValue == "r")
                        //{
                        //    //ntxt_AmountDebited.Text = Double.MaxValue;
                        //    //ntxt_AmountCredited.MaxValue = Double.MaxValue;
                        //    ntxt_AmountDebited.Text = ntxt_AmountCredited.Text;
                        //}
                        //else
                        //{
                        //    //ntxt_AmountDebited.MaxValue = Convert.ToDouble(ViewState["ActualBalance"] != null ? ViewState["ActualBalance"] : "0.00");
                        //    //ntxt_AmountCredited.MaxValue = Convert.ToDouble(ViewState["ActualBalance"] != null ? ViewState["ActualBalance"] : "0.00");
                        //}
                    }
                    else
                    {
                        message = "alert('Account is Closed.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        txt_AcctNo.Text = "";
                    }
                }
                else
                {
                    MessageBox(this, "Invalid Account !");
                    //message = "alert('Invalid Account.')";
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    txt_OldAcctNo.Text = "";
                    txt_AcctNo.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        protected void ResetControlsSLCode()
        {
            ntxt_AmountDebited.Text = String.Empty;
            txt_AcctNo.Text = String.Empty;
            ntxt_AmountCredited.Text = String.Empty;
            //Txt_Pan.Text = String.Empty;
            //Txt_Adhar.Text = String.Empty;
            //txt_Voter.Text = String.Empty;
            txt_Narration.Text = String.Empty;
            txt_ActualBalance.Text = String.Empty;
            txt_AcctType.Text = String.Empty;
            txt_AcctStatus.Text = String.Empty;
            //ntxt_AvailableBalance.Text = String.Empty;
            //cmbx_EntryType.SelectedIndex = -1;
        }
        /*
        protected void txt_AcctNo_TextChanged(object sender, EventArgs e)
        {
            //ResetControlsSLCode();

            //dtpkr_EntryDate.Text = dtpkr.Value;
            //CultureInfo provider = CultureInfo.InvariantCulture;
            //DateTime dateTimeObj = DateTime.ParseExact(dtpkr_EntryDate.Text, "yyyy-MM-dd", provider);
            //dtpkr_EntryDate.Text = dateTimeObj.ToString("dd/MM/yyyy");

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {
                DataSet data = objBL_Finance.GetAccountHoldersDetailsBySL_CODE(1, txt_AcctNo.Text, DateTime.ParseExact(dtpkr_EntryDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture), out SQLError);
                ViewState["pictdata"] = data;
                if (data.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(data.Tables[1].Rows[0]["ac_status"]) == "Live")
                    {
                        lv_AcctHolders.DataSource = data.Tables[0];
                        lv_AcctHolders.DataBind();
                        txt_AcctType.Text = Convert.ToString(data.Tables[1].Rows[0]["actype"]);
                        txt_AcctStatus.Text = Convert.ToString(data.Tables[1].Rows[0]["ac_status"]);
                        txt_OldAcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["old_acno"]);
                        ViewState["ActualBalance"] = txt_ActualBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
                        ntxt_AvailableBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
                        ntxt_AmountDebited.Text = ntxt_AmountCredited.Text;
                        txtCBS_AcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["CBSAcNo"]);
                        //Txt_Adhar.Text = Convert.ToString(data.Tables[0].Rows[0]["ADHAR_NO"]);
                        //txt_Voter.Text = Convert.ToString(data.Tables[0].Rows[0]["VOTER_CARD_NO"]);
                        //Txt_Pan.Text = Convert.ToString(data.Tables[0].Rows[0]["PAN_CARD_NO"]);





                        //if (cmbx_EntryType.SelectedValue == "r")
                        //{
                        //    //ntxt_AmountDebited.MaxValue = Double.MaxValue;
                        //    //ntxt_AmountCredited.MaxValue = Double.MaxValue;
                        //}
                        //else
                        //{
                        //    //ntxt_AmountDebited.MaxValue = Convert.ToDouble(ViewState["ActualBalance"] != null ? ViewState["ActualBalance"] : "0.00");
                        //    //ntxt_AmountCredited.MaxValue = Convert.ToDouble(ViewState["ActualBalance"] != null ? ViewState["ActualBalance"] : "0.00");
                        //}
                    }
                    else
                    {

                        message = "alert('Account is Closed.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        txt_AcctNo.Text = "";
                    }
                }
                else
                {
                    message = "alert('Invalid Account.')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    txt_AcctNo.Text = "";
                }
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            }
        }*/
        protected void txtCBS_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet data = objBL_Finance.GetAccountHoldersDetailsBySL_CODE(6, txtCBS_AcNo.Text, Convert.ToString(Session["BranchID"]), DateTime.ParseExact(dtpkr_EntryDate.Text, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), out SQLError);
                if (data.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(data.Tables[1].Rows[0]["ac_status"]) == "Live")
                    {
                        lv_AcctHolders.DataSource = data.Tables[0];
                        lv_AcctHolders.DataBind();
                        txt_AcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["sl_code"]);
                        txt_OldAcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["old_acno"]);
                        txt_AcctType.Text = Convert.ToString(data.Tables[1].Rows[0]["actype"]);
                        txt_AcctStatus.Text = Convert.ToString(data.Tables[1].Rows[0]["ac_status"]);
                        //txt_AcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["sl_code"]);
                        ViewState["ActualBalance"] = txt_ActualBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
                        ntxt_AvailableBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
                        ntxt_AmountDebited.Text = ntxt_AmountCredited.Text;
                        txtCBS_AcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["CBSAcNo"]);
                        txtBranchName.Text = Convert.ToString(data.Tables[4].Rows.Count > 0 ? data.Tables[5].Rows[0]["BranchName"] : "");
                        txtphone.Text = Convert.ToString(data.Tables[6].Rows.Count > 0 ? data.Tables[6].Rows[0]["TEL_NO"] : "");
                    }
                    else
                    {
                        message = "alert('Account is Closed.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        txt_AcctNo.Text = "";
                    }
                }
                else
                {
                    MessageBox(this, "Invalid Account !");
                    //message = "alert('Invalid Account.')";
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    txt_AcctNo.Text = "";
                }
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void txt_AcctNo_TextChanged(object sender, EventArgs e) //Txt Account Number SL_CODE
        {
            try
            {
                DataSet data = objBL_Finance.GetAccountHoldersDetailsBySL_CODE(5, txt_AcctNo.Text, Convert.ToString(Session["BranchID"]), DateTime.ParseExact(dtpkr_EntryDate.Text, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), out SQLError);
                if (data.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(data.Tables[1].Rows[0]["ac_status"]) == "Live")
                    {
                        lv_AcctHolders.DataSource = data.Tables[0];
                        lv_AcctHolders.DataBind();
                        txt_AcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["sl_code"]);
                        txt_OldAcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["old_acno"]);
                        txt_AcctType.Text = Convert.ToString(data.Tables[1].Rows[0]["actype"]);
                        txt_AcctStatus.Text = Convert.ToString(data.Tables[1].Rows[0]["ac_status"]);
                        //txt_AcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["sl_code"]);
                        ViewState["ActualBalance"] = txt_ActualBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
                        ntxt_AvailableBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
                        ntxt_AmountDebited.Text = ntxt_AmountCredited.Text;
                        txtCBS_AcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["CBSAcNo"]);
                        txtBranchName.Text = Convert.ToString(data.Tables[4].Rows.Count > 0 ? data.Tables[5].Rows[0]["BranchName"] : "");
                    }
                    else
                    {
                        message = "alert('Account is Closed.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        txt_AcctNo.Text = "";
                    }
                }
                else
                {
                    MessageBox(this, "Invalid Account !");
                    //message = "alert('Invalid Account.')";
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    txt_AcctNo.Text = "";
                }
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        //ResetControlsSLCode();

        //DataSet data = objBL_Finance.GetAccountHoldersDetailsBySL_CODE(1, txt_AcctNo.Text, DateTime.ParseExact(dtpkr_EntryDate.Text.Trim(), @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), out SQLError);
        //if (data.Tables[1].Rows.Count > 0)
        //{
        //    if (Convert.ToString(data.Tables[1].Rows[0]["ac_status"]) == "Live")
        //    {
        //        lv_AcctHolders.DataSource = data.Tables[0];
        //        lv_AcctHolders.DataBind();
        //        txt_AcctType.Text = Convert.ToString(data.Tables[1].Rows[0]["actype"]);
        //        txt_AcctStatus.Text = Convert.ToString(data.Tables[1].Rows[0]["ac_status"]);
        //        txt_OldAcctNo.Text = Convert.ToString(data.Tables[1].Rows[0]["old_acno"]);
        //        ViewState["ActualBalance"] = txt_ActualBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
        //        ntxt_AvailableBalance.Text = Convert.ToString(data.Tables[2].Rows.Count > 0 ? data.Tables[2].Rows[0]["ActBalance"] : "0.00");
        //        ntxt_AmountDebited.Text = ntxt_AmountCredited.Text;
        //        txtCBS_AcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["CBSAcNo"]);
        //        //Txt_Adhar.Text = Convert.ToString(data.Tables[0].Rows[0]["ADHAR_NO"]);
        //        //txt_Voter.Text = Convert.ToString(data.Tables[0].Rows[0]["VOTER_CARD_NO"]);
        //        //Txt_Pan.Text = Convert.ToString(data.Tables[0].Rows[0]["PAN_CARD_NO"]);
        //        //if (cmbx_EntryType.SelectedValue == "r")
        //        //{
        //        //    //ntxt_AmountDebited.MaxValue = Double.MaxValue;
        //        //    //ntxt_AmountCredited.MaxValue = Double.MaxValue;
        //        //}
        //        //else
        //        //{
        //        //    //ntxt_AmountDebited.MaxValue = Convert.ToDouble(ViewState["ActualBalance"] != null ? ViewState["ActualBalance"] : "0.00");
        //        //    //ntxt_AmountCredited.MaxValue = Convert.ToDouble(ViewState["ActualBalance"] != null ? ViewState["ActualBalance"] : "0.00");
        //        //}
        //    }
        //    else
        //    {

        //        message = "alert('Account is Closed.')";
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
        //        txt_AcctNo.Text = "";
        //    }
        //}
        //else
        //{
        //    message = "alert('Invalid Account.')";
        //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
        //    txt_AcctNo.Text = "";
        //}


        protected void ntxt_AmountDebited_TextChanged(object sender, EventArgs e)
        {
            if (cmbx_EntryType.SelectedValue == "p")
            {
                if (txt_AcctType.Text == "Savings")
                {
                    if (Convert.ToDouble(ntxt_AvailableBalance.Text) > 0)
                    {
                        if (Convert.ToDouble(ntxt_AvailableBalance.Text) > 500 && Convert.ToDouble(ntxt_AmountDebited.Text) < Convert.ToDouble(ntxt_AvailableBalance.Text))
                        {
                            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
                            {
                                if (ntxt_AmountDebited.Text != "")
                                {
                                    AmtTextChange();
                                }
                                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                            }
                        }
                        else
                        {
                            MessageBox(this, "Sorry ! Transaction Is Not Possible Due To The Min Balance Below RS. 500");
                        }
                    }
                    else
                    {
                        MessageBox(this, "Sorry ! Transaction Is Not Possible Due To The Min Balance Below RS. 0.00");
                    }
                }
                else if (txt_AcctType.Text == "Deposit Certificate" || txt_AcctType.Text == "Fixed Deposite" || txt_AcctType.Text == "RECURRING DEPOSITE" || txt_AcctType.Text == "MIS DEPOSITE" || txt_AcctType.Text == "HOME SAVINGS")
                {
                    
                    Type cstype = this.GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('Entry Type Can't Be Payment For');" + txt_AcctType.Text;
                        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                    }
                }
            }
            else if (cmbx_EntryType.SelectedValue == "r")
            {
                if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
                {
                    if (ntxt_AmountDebited.Text != "")
                    {
                        AmtTextChange();
                    }
                    Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                }
            }
        }

       

        protected void lv_AcctHolders_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            /*
            DataSet dt = (DataSet)ViewState["pictdata"];
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image photo = (Image)e.Item.FindControl("Photo");
                Image sign = (Image)e.Item.FindControl("Sign");

                if (!string.IsNullOrEmpty(dt.Tables[0].Rows[0]["PICTPATH"].ToString()))
                {
                    photo.ImageUrl = dt.Tables[0].Rows[0]["PICTPATH"].ToString();
                    //hiddenImgEmp.Value = imgEmp.ImageUrl;
                }
                if (!string.IsNullOrEmpty(dt.Tables[0].Rows[0]["SIGNPATH"].ToString()))
                {
                    sign.ImageUrl = dt.Tables[0].Rows[0]["SIGNPATH"].ToString();
                    //hiddenImgsign.Value = ImgSig.ImageUrl;
                }
            }*/
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            decimal amt2000 = txtrs2000.Text != "" ? Convert.ToDecimal(txtrs2000.Text) : 0;
            decimal amt500 = txtrs500.Text != "" ? Convert.ToDecimal(txtrs500.Text) : 0;
            decimal amt200 = txtrs200.Text != "" ? Convert.ToDecimal(txtrs200.Text) : 0;
            decimal amt100 = txtrs100.Text != "" ? Convert.ToDecimal(txtrs100.Text) : 0;
            decimal amt50 = txtrs50.Text != "" ? Convert.ToDecimal(txtrs50.Text) : 0;
            decimal amt20 = txtrs20.Text != "" ? Convert.ToDecimal(txtrs20.Text) : 0;
            decimal amt10 = txtrs10.Text != "" ? Convert.ToDecimal(txtrs10.Text) : 0;
            decimal amt5 = txtrs5.Text != "" ? Convert.ToDecimal(txtrs5.Text) : 0;
            decimal amt2 = txtrs2.Text != "" ? Convert.ToDecimal(txtrs2.Text) : 0;
            decimal amt1 = txtrs1.Text != "" ? Convert.ToDecimal(txtrs1.Text) : 0;

            calculate2000.Text = Convert.ToString(2000 * amt2000);
            decimal c2000 = calculate2000.Text != "" ? Convert.ToDecimal(calculate2000.Text) : 0;
            calculate500.Text = Convert.ToString(500 * amt500);
            decimal c500 = calculate500.Text != "" ? Convert.ToDecimal(calculate500.Text) : 0;
            calculate200.Text = Convert.ToString(200 * amt200);
            decimal c200 = calculate200.Text != "" ? Convert.ToDecimal(calculate200.Text) : 0;
            calculate100.Text = Convert.ToString(100 * amt100);
            decimal c100 = calculate100.Text != "" ? Convert.ToDecimal(calculate100.Text) : 0;
            calculate50.Text = Convert.ToString(50 * amt50);
            decimal c50 = calculate50.Text != "" ? Convert.ToDecimal(calculate50.Text) : 0;
            calculate20.Text = Convert.ToString(20 * amt20);
            decimal c20 = calculate20.Text != "" ? Convert.ToDecimal(calculate20.Text) : 0;
            calculate10.Text = Convert.ToString(10 * amt10);
            decimal c10 = calculate10.Text != "" ? Convert.ToDecimal(calculate10.Text) : 0;
            calculate5.Text = Convert.ToString(5 * amt5);
            decimal c5 = calculate5.Text != "" ? Convert.ToDecimal(calculate5.Text) : 0;
            calculate2.Text = Convert.ToString(2 * amt2);
            decimal c2 = calculate2.Text != "" ? Convert.ToDecimal(calculate2.Text) : 0;
            calculate1.Text = Convert.ToString(1 * amt1);
            decimal c1 = calculate1.Text != "" ? Convert.ToDecimal(calculate1.Text) : 0;

            calculatetotamt.Text = Convert.ToString(c2000 + c500 + c200 + c100 + c50 + c20 + c10 + c5 + c2 + c1);




            if (ViewState["AmountDebited"] != null)
            {
                if ((Double)ViewState["AmountDebited"] == Convert.ToDouble(calculatetotamt.Text))
                {
                    //message = "alert('Given Amount is Equal.')";
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    ntxt_AmountDebited.Text = calculatetotamt.Text;
                }
                else
                {
                    message = "alert('Given Amount is not Equal to Enter Amount.')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }


                try
                {
                    //objBO_Finance.SL_CODE = (int)ViewState["SL_CODE"];
                    objBO_Finance.SL_CODE = txtslcode.Text;
                    objBO_Finance.rs2000 = amt2000;
                    objBO_Finance.rs500 = amt500;
                    objBO_Finance.rs200 = amt200;
                    objBO_Finance.rs100 = amt100;
                    objBO_Finance.rs50 = amt50;
                    objBO_Finance.rs20 = amt20;
                    objBO_Finance.rs10 = amt10;
                    objBO_Finance.rs5 = amt5;
                    objBO_Finance.rs2 = amt2;
                    objBO_Finance.rs1 = amt1;
                    objBO_Finance.DENO_TYPE = cmbx_EntryType.SelectedValue;
                    objBO_Finance.EntryDate = System.DateTime.Now;
                    objBO_Finance.VOUCHER_NO = "";

                    objBO_Finance.DenomTotalAmount = Convert.ToInt32(calculatetotamt.Text);


                    int i = objBL_Finance.InsertUpdate_Denomination(objBO_Finance);
                    if (i > 0)
                    {
                        message = "alert('Save Denom.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    }

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }

            }
        }
    }
}


