using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Collections;
using System.Globalization;
using BLL.GeneralBL;
using System.Configuration;
using System.Data.SqlClient;



namespace iBankingSolution.Transaction
{
    public partial class frmGeneralTransaction : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetBranchName();

            if (!IsPostBack)
            {
                BindLedger();
                BindLedgerName();
                //BindSubLedgerName();
                dtpkr_EntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //dtpkr_DateOfInstrument.Text = DateTime.Now.ToString("dd/MM/yyyy");

            }
        }
        protected void BindLedger()
        {
            try
            {
                objBO_Finance.Flag = 22;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_PayToLedger.DataSource = dt;
                    cmbx_PayToLedger.DataValueField = "LDG_CODE";
                    cmbx_PayToLedger.DataTextField = "NOMENCLATURE";
                    cmbx_PayToLedger.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void GetBranchName()
        {
            try
            {
                DataSet data = objBL_Finance.GetBranchNameByBranchCode(1, Convert.ToString(Session["BranchID"]),  out SQLError);
                if (data.Tables[0].Rows.Count > 0)
                {
                    txtBranchName.Text = Convert.ToString(data.Tables[0].Rows[0]["BranchName"]); 
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void GetAcNo()
        {
            try
            {
                DataSet data = objBL_Finance.GetAcNo(23, cmbx_SubLedger.SelectedValue, out SQLError);
                if (data.Tables[0].Rows.Count > 0)
                {
                    txtslcode.Text = Convert.ToString(data.Tables[0].Rows[0]["SL_CODE"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void BindLedgerName()
        {
            try
            {
                objBO_Finance.Flag = 13;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_PayToLedgerName.DataSource = dt;
                    cmbx_PayToLedgerName.DataValueField = "LDG_CODE";
                    cmbx_PayToLedgerName.DataTextField = "NOMENCLATURE";
                    cmbx_PayToLedgerName.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void BindSubLedgerName()
        {
            try
            {
                objBO_Finance.Flag = 13;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_SubLedger.DataSource = dt;
                    cmbx_SubLedger.DataValueField = "LDG_CODE";
                    cmbx_SubLedger.DataTextField = "SL_NAME";
                    cmbx_SubLedger.DataBind();
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }
        private void MsgBox(string sMessage)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + sMessage + "');";
            msg += "</script>";
            Response.Write(msg);
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable dt = new DataTable();
                //dt.Columns.AddRange(new DataColumn[]
                //{
                //new DataColumn("LDG_CODE"),
                //new DataColumn("SL_CODE"),
                //new DataColumn("AMT_DEBIT"),
                //new DataColumn("AMT_CREDIT")

                //});
                //object LDG_CODE, SL_CODE;
                //if (Convert.ToDecimal(cmbx_SubLedger.SelectedValue == "" ? "0" : cmbx_SubLedger.SelectedValue) > 0)
                //{
                //    LDG_CODE = DBNull.Value;
                //    SL_CODE = cmbx_SubLedger.SelectedValue;
                //}
                //else
                //{
                //    LDG_CODE = cmbx_PayToLedger.SelectedValue;
                //    SL_CODE = DBNull.Value;
                //}
                //if (cmbx_EntryType.SelectedValue == "r")
                //{
                //    dt.Rows.Add(DBNull.Value, "990000", ntxt_AmountDebited.Text, 0m);
                //    dt.Rows.Add(LDG_CODE, SL_CODE, 0m, ntxt_AmountDebited.Text);
                //}
                //else
                //{
                //    dt.Rows.Add(DBNull.Value, "990000", 0m, ntxt_AmountDebited.Text);
                //    dt.Rows.Add(LDG_CODE, SL_CODE, ntxt_AmountDebited.Text, 0m);
                //}
                //objBO_Finance.dtLedgerDetails = dt;

               

                if (cmbx_EntryType.SelectedValue == "p")
                {
                    objBO_Finance.TR_TYPE = cmbx_EntryType.SelectedValue;
                    if (txtslcode.Text != "")
                    {
                        objBO_Finance.SL_CODE = txtslcode.Text;
                        objBO_Finance.LDG_CODE = "0";

                    }
                    else
                    {
                        objBO_Finance.LDG_CODE = cmbx_PayToLedger.SelectedValue;
                        objBO_Finance.SL_CODE = "0";
                    }


                    objBO_Finance.AMT_DEBIT = Convert.ToDouble(ntxt_AmountDebited.Text);
                    objBO_Finance.AMT_CREDIT = Convert.ToDouble(ntxt_AmountCredited.Text);

                    string Cust = dtpkr_EntryDate.Text;
                    DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.CCB_DATE = Cust1;


                    //objBO_Finance.CCB_DATE = Convert.ToDateTime(dtpkr_EntryDate.Text);


                    objBO_Finance.VOUCHER_NO = btnsubmit.CommandArgument;
                    objBO_Finance.VOUCHER_NO = btnsubmit1.CommandArgument;
                    objBO_Finance.NARRATION1 = txt_Comments.Text;
                    //objBO_Finance.NARRATION2 = txt_Comments.Text;
                    //objBO_Finance.PAYMENT_RECEIPT = cmbx_EntryType.SelectedValue;
                    //objBO_Finance.TYPEOFINSTRUMENT = "DepositSlip";
                    // objBO_Finance.INSTRUMENTNO = "";
                    //objBO_Finance.DT_ON_INS = DateTime.Now;
                    //objBO_Finance.DAILY_SRL = "0";
                    //objBO_Finance.ENTRY_FR = 2;
                    objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                    objBO_Finance.EmpCode = Convert.ToString(Session["EmpID"]); 
                    objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 3;
                    objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 3;
                }
                else if (cmbx_EntryType.SelectedValue == "r")
                {
                    objBO_Finance.TR_TYPE = cmbx_EntryType.SelectedValue;
                    if (txtslcode.Text != "")
                    {
                        objBO_Finance.SL_CODE = txtslcode.Text;
                        objBO_Finance.LDG_CODE = "0";

                    }
                    else
                    {
                        objBO_Finance.LDG_CODE = cmbx_PayToLedger.SelectedValue;
                        objBO_Finance.SL_CODE = "0";
                    }


                    objBO_Finance.AMT_DEBIT = Convert.ToDouble(ntxt_AmountDebited.Text);
                    objBO_Finance.AMT_CREDIT = Convert.ToDouble(ntxt_AmountCredited.Text);

                    string Cust = dtpkr_EntryDate.Text;
                    DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.CCB_DATE = Cust1;


                    //objBO_Finance.CCB_DATE = Convert.ToDateTime(dtpkr_EntryDate.Text);


                    objBO_Finance.VOUCHER_NO = btnsubmit.CommandArgument;
                    objBO_Finance.VOUCHER_NO = btnsubmit1.CommandArgument;
                    objBO_Finance.NARRATION1 = txt_Comments.Text;
                    //objBO_Finance.NARRATION2 = txt_Comments.Text;
                    //objBO_Finance.PAYMENT_RECEIPT = cmbx_EntryType.SelectedValue;
                    //objBO_Finance.TYPEOFINSTRUMENT = "DepositSlip";
                    // objBO_Finance.INSTRUMENTNO = "";
                    //objBO_Finance.DT_ON_INS = DateTime.Now;
                    //objBO_Finance.DAILY_SRL = "0";
                    //objBO_Finance.ENTRY_FR = 2;
                    objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                    objBO_Finance.EmpCode = Convert.ToString(Session["EmpID"]); 
                    objBO_Finance.Flag = btnsubmit.Text == "Save" ? 2 : 3;
                    objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 2 : 3;
                }

                

                int i = objBL_Finance.InsertUpdateDeleteCashPayOthers(objBO_Finance, out SQLError);
                if (i > 0)
                {
                    if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                    {
                        MsgBox("Record Save Successfully . Allotted Voucher is:- " + SQLError);
                        //ResetControls();
                        //message = "alert('Save Successfully.')";
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        //btnsubmit.Text = "Save";
                        //btnsubmit1.Text = "Save";
                        //Label1.Visible = true;
                        //DivID.Visible = true;
                        //Label1.Text = "Alloted CustId is:" + SQLError;

                        //objBO_Finance.Flag = 1;

                        //ResetControls();
                        //MessageBox(this, "Record Inserted Successfully . Alloted Voucher No is:-" + SQLError);
                        //message = "alert('Save Successfully.')";
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        // MessageBox(this, "Record Save Successfully . Allotted Voucher is:-" + SQLError);
                        //MessageBox(this, "Record Save Successfully . Allotted Voucher is:-" + SQLError);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";
                        Label1.Visible = false;
                        DivID.Visible = true;
                        Label1.Text = "Alloted Voucher is:" + SQLError;

                        objBO_Finance.Flag = 1;
                        ResetControls();


                    }
                    if (btnsubmit1.Text == "Update" || btnsubmit.Text == "Update")
                    {
                        message = "alert('Update Successfully.')";

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";

                        ResetControls();
                    }

                    else
                    {

                        //message = "alert('Something Wrong Input.')";
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    }
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
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }


        protected void ResetControls()
        {
            //cmbx_InstrumentType.SelectedIndex = -1;

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
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


                lblCredited.Text = "Amount Debited";
                lblDebited.Text = "Amount Credited";
                ntxt_AmountCredited.Enabled = false;

                ntxt_AmountCredited.Attributes.Add("placeholder", "Amount Debited ");
                ntxt_AmountDebited.Attributes.Add("placeholder", "Amount Credited");
            }
            else if (cmbx_EntryType.SelectedItem.Text == "Payment")
            {
                //cmbx_InstrumentType.Items.Clear();
                //cmbx_InstrumentType.Items.Add("WithdrawalSlip");
                //cmbx_InstrumentType.Items.Add("Voucher");
                //cmbx_InstrumentType.Items.Add("Cheque");
                //cmbx_InstrumentType.Items.Add("Others");

                ntxt_AmountCredited.Attributes.Add("placeholder", "Amount Credited ");
                ntxt_AmountDebited.Attributes.Add("placeholder", "Amount Debited");
                lblDebited.Text = "Amount Debited";
                lblCredited.Text = "Amount Credited";
                ntxt_AmountCredited.Enabled = false;

            }
        }

        protected void cmbx_PayToLedger_SelectedIndexChanged(object sender, EventArgs e) //FOR LDG_CODE
        {

            SelectLedgerName();
            Getsubledgername();
            ///GetAcNo();
            //SelectSubLedgerName();
            //SelectSubLedgerName();


            //objBO_Finance.Flag = 13;
            //objBO_Finance.LDG_CODE = cmbx_PayToLedger.SelectedValue;

            //bool IsSUbLedger = Convert.ToInt32(objBL_Finance.GetSubLedgerMasterRecords(objBO_Finance, out SQLError).Rows[0][0]) > 0;
            //cmbx_SubLedger.Enabled = IsSUbLedger;
            //DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
            //if (dt.Rows.Count > 0)
            //{
            //    cmbx_SubLedger.DataSource = dt;
            //    cmbx_SubLedger.DataValueField = "LDG_CODE";
            //    cmbx_SubLedger.DataTextField = "sl_name";
            //    cmbx_SubLedger.DataBind();

            //}
        }

        protected void SelectSubLedgerName()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String query = "select DM_CODE FROM DEPOSIT_MASTER WHERE LDG_CODE = @LDG_CODE";
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@LDG_CODE", cmbx_PayToLedger.SelectedItem.Value);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmbx_SubLedger.Text = sdr[0].ToString();
                    
                }

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
        protected void SelectLedgerName()
        {

            try
            {
                DataTable dtledger = new DataTable();
                objBO_Finance.Flag = 11;
                objBO_Finance.CUST_ID = null;
                objBO_Finance.LDG_CODE = cmbx_PayToLedger.SelectedValue;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);

                if (dtledger.Rows.Count > 0)
                {

                    //cmbx_PayToLedgerName.DataSource = null;
                    cmbx_PayToLedgerName.DataSource = dtledger;
                    cmbx_PayToLedgerName.DataValueField = "LDG_CODE";
                    cmbx_PayToLedgerName.DataTextField = "NOMENCLATURE";
                    cmbx_PayToLedgerName.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void Getsubledgername()
        {
            try
            {
                DataTable dtsledger = new DataTable();
                objBO_Finance.Flag = 21;
                objBO_Finance.CUST_ID = null;
                objBO_Finance.LDG_CODE = cmbx_PayToLedger.SelectedValue;
                dtsledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);

                if (dtsledger.Rows.Count > 0)
                {
                    cmbx_SubLedger.DataSource = dtsledger;
                    //cmbx_SubLedger.DataValueField = "LDG_CODE";
                    cmbx_SubLedger.DataValueField = "sl_name";
                    cmbx_SubLedger.DataTextField = "sl_name";
                    cmbx_SubLedger.DataBind();
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }
       


        protected void SelectLedgerCode()
        {
            
        }

        protected void ntxt_AmountDebited_TextChanged(object sender, EventArgs e)
        {
            ntxt_AmountCredited.Text = ntxt_AmountDebited.Text;

            LBLwords.Text= ConvertNumbertoWords(Convert.ToInt32(Convert.ToDouble(ntxt_AmountDebited.Text)));
            LBLwords.Text =  LBLwords.Text + " Only";


        }

        public static string ConvertNumbertoWords(int number)
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        protected void cmbx_PayToLedgerName_SelectedIndexChanged(object sender, EventArgs e)//FOR LDG_NAME
        {
            
            try
            {
                DataTable dtledger = new DataTable();
                objBO_Finance.Flag = 11;
                objBO_Finance.CUST_ID = null;
                objBO_Finance.LDG_CODE = cmbx_PayToLedgerName.SelectedValue;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);

                if (dtledger.Rows.Count > 0)
                {

                    //cmbx_PayToLedgerName.DataSource = null;
                    cmbx_PayToLedger.DataSource = dtledger;
                    cmbx_PayToLedger.DataValueField = "LDG_CODE";
                    cmbx_PayToLedger.DataTextField = "LDG_CODE";
                    cmbx_PayToLedger.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            Getsubledgername();
            //GetAcNo();

        }


        protected void cmbx_SubLedger_SelectedIndexChanged(object sender , EventArgs e)
        {
            GetAcNo();
        }
        
    }
}