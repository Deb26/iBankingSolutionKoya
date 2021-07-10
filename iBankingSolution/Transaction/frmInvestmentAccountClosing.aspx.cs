using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace iBankingSolution.Transaction
{
    public partial class frmInvestmentAccountClosing : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        double DepositAmt = 0;
        DataTable dtledger;
        protected void Page_Load(object sender, EventArgs e)
        {
            ntxt_RenewalROI.Enabled = false;
            if (!IsPostBack)
            {
                cmbx_IntTransferAccountTo.Enabled = false;
                dtpkr_WithdrawlDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
        }
        //Get Details By Using OLD Account Number//
        protected void txtOldAcNo_TextChanged(object sender, EventArgs e)
        {


            DataSet data = objBL_Finance.GetAccountHoldersDetailsBySL_CODEOLD(1, txtOldAcNo.Text, Convert.ToString(Session["BranchID"]), DateTime.Now, out SQLError);
            if (data.Tables[1].Rows.Count > 0)
            {
                btnsubmit1.Enabled = true;
                if (Convert.ToString(data.Tables[1].Rows[0]["ac_status"]) == "Live")
                {
                    btnsubmit1.Enabled = true;
                    
                    txtAcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["sl_code"]);
                    txtOldAcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["old_acno"]);
                    txtdtOpening.Text = Convert.ToString(data.Tables[1].Rows[0]["date_of_opening"]);
                    //txtdepositScheme.Text = Convert.ToString(data.Tables[1].Rows[0]["actype"]);

                    //ViewState["ActualBalance"] = txtdepoamt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[2].Rows[0]["ActBalance"])));
                    ViewState["InstAmt"] = txt_RenewalDepotAmt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[1].Rows[0]["DEPOSIT_AMOUNT"])));
                    ViewState["MaturityAmt"] = ntxt_RMaturityAmt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[1].Rows[0]["MATURITY_AMT"])));
                    txt_RePrdinMonth.Text = Convert.ToString(data.Tables[1].Rows[0]["DEPO_PRD_M"]);
                    txt_RePrdInDays.Text = Convert.ToString(data.Tables[1].Rows[0]["DEPO_PRD_D"]);
                    ntxt_RenewalROI.Text = Convert.ToString(data.Tables[1].Rows[0]["PERCENTAGE"]);

                    dtpkr_RenewalMatDt.Text = Convert.ToString(data.Tables[1].Rows[0]["DATE_OF_MATURITY"]);
                    //dtpkr_WithdrawlDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtIntrissuedFrCode.Text = Convert.ToString(data.Tables[1].Rows[0]["INT_PAIDLDG"]);
                    txtIntrissuedFrLedgerName.Text = Convert.ToString(data.Tables[1].Rows[0]["INTPaidLdgName"]);
                    //cmbx_TransferAcNo.Items.Clear();
                    //cmbx_TransferAcNo.DataSource = data.Tables[3];
                    //cmbx_TransferAcNo.DataBind();
                    
                    
                    
                    if (txtacctype.Text == "INVESTMENT")
                    {
                        PanelCertificateTwo.Visible = true;
                    }
                }
            }
            else
            {
                MessageBox(this, "Account Not Belongs To This Branch !");
            }
        }

        //Get Details By Using Account Number//
        protected void txtAcNo_TextChanged(object sender, EventArgs e)
        {
            DataSet data = objBL_Finance.GetInvestmentAccountHoldersDetailsBySL_CODE(1, txtAcNo.Text, DateTime.Now, out SQLError);
            if (data.Tables[0].Rows.Count > 0)
            {
                btnsubmit1.Enabled = true;
                if (Convert.ToString(data.Tables[0].Rows[0]["ac_status"]) == "Live")
                {
                    btnsubmit1.Enabled = true;
                    
                    txtAcNo.Text = Convert.ToString(data.Tables[0].Rows[0]["sl_code"]);
                    txtOldAcNo.Text = Convert.ToString(data.Tables[0].Rows[0]["old_acno"]);
                    txtdtOpening.Text = Convert.ToString(data.Tables[0].Rows[0]["date_of_opening"]);

                    txtacctype.Text = Convert.ToString(data.Tables[0].Rows[0]["actype"]);

                    ViewState["InstAmt"] = txt_RenewalDepotAmt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[0].Rows[0]["DEPOSIT_AMOUNT"])));
                    
                    ViewState["MaturityAmt"] = ntxt_RMaturityAmt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[0].Rows[0]["MATURITY_AMT"])));
                    
                    txt_RePrdinMonth.Text = Convert.ToString(data.Tables[0].Rows[0]["DEPO_PRD_M"]);
                    
                    txt_RePrdInDays.Text = Convert.ToString(data.Tables[0].Rows[0]["DEPO_PRD_D"]);
                    
                    
                    ntxt_RenewalROI.Text = Convert.ToString(data.Tables[0].Rows[0]["PERCENTAGE"]);

                    dtpkr_RenewalMatDt.Text = Convert.ToString(data.Tables[0].Rows[0]["DATE_OF_MATURITY"]);

                    //dtpkr_RenewalMatDt.Text = Convert.ToDateTime(data.Tables[0].Rows[0]["DATE_OF_MATURITY"]);

                    txtIntrissuedFrCode.Text = Convert.ToString(data.Tables[0].Rows[0]["INTPAIDLDG_CODE"]);

                    txtIntrissuedFrLedgerName.Text = Convert.ToString(data.Tables[0].Rows[0]["INTPaidLdgName"]);
                    

                    //cmbx_TransferAcNo.Items.Clear();
                    //cmbx_TransferAcNo.DataSource = data.Tables[3];
                    //cmbx_TransferAcNo.DataBind();

                    if (txtacctype.Text == "Fixed Deposite")
                    {
                        PanelCertificateTwo.Visible = true;
                        maturityPanel.Visible = true;
                    }

                    if (txtacctype.Text == "Deposit Certificate")
                    {
                        PanelCertificateTwo.Visible = true;
                        maturityPanel.Visible = true;
                    }

                    if (txtacctype.Text == "RECURRING DEPOSITE")
                    {
                        PanelCertificateTwo.Visible = true;
                        maturityPanel.Visible = true;
                    }


                }
                else
                {
                    MessageBox(this, "Account is not found " + SQLError);
                    ResetResetControl();
                }
            }
            else
            {
                MessageBox(this, "Account Not Belongs To This Branch !");
            }
             
        }

        protected void ShowAccount()
        {
            try
            {

                objBO_Finance.Flag = 15;
                objBO_Finance.CUST_ID = null;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {
                    cmbx_IntTransferAccountTo.DataSource = dtledger;
                    cmbx_IntTransferAccountTo.DataValueField = "LDGTRF";
                    cmbx_IntTransferAccountTo.DataTextField = "Nomenclature";
                    cmbx_IntTransferAccountTo.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void ShowTransferAccount()
        {
            cmbx_IntTransferAccountTo.Items.Add(new ListItem("-- Select Transfer Account --", ""));
            cmbx_IntTransferAccountTo.AppendDataBoundItems = true;
            String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String strQuery = "select convert (varchar , LDG_CODE) + ' - ' + NOMENCLATURE as Nomenclature  from LEDGER_MASTER where CASH_BANK = '2'";
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmbx_IntTransferAccountTo.DataSource = cmd.ExecuteReader();
                cmbx_IntTransferAccountTo.DataTextField = "Nomenclature";
                cmbx_IntTransferAccountTo.DataValueField = "Nomenclature";
                cmbx_IntTransferAccountTo.DataBind();
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
        protected void txtacctype_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cmbx_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlMaturityInst.SelectedValue == "CASH WITHDRAWL")
            {
                btnsubmit.Visible = true;
                btnsubmit1.Visible = true;
                btn_CloseAccount.Visible = false;
                //ShowTransferAccount();
                //ShowAccount();
                cmbx_IntTransferAccountTo.SelectedIndex = -1;
                cmbx_IntTransferAccountTo.Enabled = false;
            }
            else if (ddlMaturityInst.SelectedValue == "A/C TRANSFER")
            {
                btnsubmit.Visible = true;
                btnsubmit1.Visible = true;
                btn_CloseAccount.Visible = false;
                //ShowTransferAccount();
                ShowAccount();
                cmbx_IntTransferAccountTo.Enabled = true;
            }
            else if (ddlMaturityInst.SelectedValue == "Select")
            {
                btnsubmit1.Visible = false;
                btnsubmit.Visible = false;
                btn_CloseAccount.Visible = false;
                cmbx_IntTransferAccountTo.Enabled = false;
                cmbx_IntTransferAccountTo.SelectedIndex = -1;
            }
        }
        protected void btnCancel_Click (object sender , EventArgs e)
        {
            Response.Redirect("frmInvestmentAccountClosing.aspx");
        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg) //Message Box For Message
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);
        }

        protected void ResetResetControl()
        {
            btn_CloseAccount.Text = "CloseAccount";
            btn_CloseAccount.Text = "CloseAccount";
            txtAcNo.Text = String.Empty;
            txtdtOpening.Text = String.Empty;
            txtacctype.Text = String.Empty;
            txtdtLastTrnsDt.Text = String.Empty;
            ntxt_RenewalROI.Text = String.Empty;
            dtpkr_RenewalMatDt.Text = String.Empty;
            txt_RePrdinMonth.Text = String.Empty;
            txt_RePrdInDays.Text = String.Empty;
            ntxt_RMaturityAmt.Text = String.Empty;
            txt_RenewalDepotAmt.Text = String.Empty;
            txtIntrissuedFrCode.Text = String.Empty;
            txtIntrissuedFrLedgerName.Text = String.Empty;
            ddlMaturityInst.SelectedIndex = -1;
            cmbx_IntTransferAccountTo.SelectedIndex = -1;
            txtIntAdj.Text = String.Empty;
            txttds.Text = String.Empty;
            txt_MaturityAmt.Text = String.Empty;
            dtpkr_WithdrawlDate.Text = String.Empty;
            
        }

        protected void btn_CloseAccount2_Click (object sender , EventArgs e ) //For InvestMent Account Closing 
        {
            if (ddlMaturityInst.SelectedValue == "")
            {

                //message = "alert('Select Maturty Instruction')";
                MessageBox(this, "Select Maturty Instruction" + SQLError);
                //message = "Select Maturty Instruction";
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = txtAcNo.Text;
           
            objBO_Finance.MaturityInstruction = ddlMaturityInst.SelectedValue;
            objBO_Finance.TYPE = ddlMaturityInst.SelectedValue;
            objBO_Finance.RenewalPeriodsInMonth = txt_RePrdinMonth.Text != "" ? Convert.ToDouble(txt_RePrdinMonth.Text) : 0.00;
            objBO_Finance.RenewalPeriodsInDays = txt_RePrdinMonth.Text != "" ? Convert.ToDouble(txt_RePrdInDays.Text) : 0.00;
            objBO_Finance.RenewalROI = Convert.ToDouble(ntxt_RenewalROI.Text);

            if (!string.IsNullOrEmpty(txt_RenewalDepotAmt.Text))
            {
                objBO_Finance.RenewalDepositAmt = Convert.ToDouble(txt_RenewalDepotAmt.Text);
            }

            if (!string.IsNullOrEmpty(dtpkr_RenewalMatDt.Text))
            {
                string rmdt = dtpkr_RenewalMatDt.Text;
                DateTime rmaturitydt = DateTime.ParseExact(rmdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.RenewalMaturityDate = rmaturitydt;
            }
            objBO_Finance.RenewalMaturityAmt = ntxt_RMaturityAmt.Text != "" ? Convert.ToDouble(ntxt_RMaturityAmt.Text) : 0.00;

            objBO_Finance.InterestCreditedTillDate = DateTime.Now;
            objBO_Finance.InterestAdjusted = txtIntAdj.Text != "" ? Convert.ToDouble(txtIntAdj.Text) : 0.00;
            
            if (!string.IsNullOrEmpty(dtpkr_WithdrawlDate.Text))
            {
                string wdt = dtpkr_WithdrawlDate.Text;
                DateTime withdrawldt = DateTime.ParseExact(wdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.WithdrawlDate = withdrawldt;
            }
            objBO_Finance.LDGTRF = cmbx_IntTransferAccountTo.SelectedValue;
            objBO_Finance.INTPAIDLDG_CODE = txtIntrissuedFrCode.Text;
            objBO_Finance.Maturityamount = txt_MaturityAmt.Text != "" ? Convert.ToDouble(txt_MaturityAmt.Text) : 0.00;
            objBO_Finance.actype = txtacctype.Text;
            objBO_Finance.EMPCODE = "";
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

            int i = objBL_Finance.InvestmentAccountClosing(objBO_Finance, out SQLError);
            if (i > 0)
            {
                btn_CloseAccount.Visible = false;
                //message = "alert('Successfuly Account Closed')";
                MessageBox(this, "Successfuly Account Closed" + SQLError);
                ResetResetControl();
                btnsubmit1.Visible = false;
                btnsubmit.Visible = false;
                btn_CloseAccount.Visible = false;
                cmbx_IntTransferAccountTo.Enabled = false;
                PanelCertificateTwo.Visible = false;
                maturityPanel.Visible = false;

                //message = "Successfuly Account Closed";
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
            else
            {
                //message = "alert('Close Not Successful')";
                MessageBox(this, "Close Not Successful. Error Details: " + SQLError + "");
                ResetResetControl();
                btnsubmit1.Visible = false;
                btnsubmit.Visible = false;
                btn_CloseAccount.Visible = false;
                cmbx_IntTransferAccountTo.Enabled = false;
                PanelCertificateTwo.Visible = false;
                maturityPanel.Visible = false;

                //message = "Close Not Successful. Error Details:  " + SQLError + "";
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
        }

        protected void btnsubmit_Click (object sender , EventArgs e) // For Maturity Calculation
        {
            if (txtacctype.Text == "Fixed Deposite") //Maturity Calculation For Fixed Deposite
            {
                string dtop = txtdtOpening.Text;
                DateTime dtopen = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                string dtm = dtpkr_RenewalMatDt.Text;
                DateTime dtMaturity = DateTime.ParseExact(dtm, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                string dtw = dtpkr_WithdrawlDate.Text;
                DateTime dtwith = DateTime.ParseExact(dtw, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                double TotDepoAmt = txt_RenewalDepotAmt.Text != "" ? Convert.ToDouble(txt_RenewalDepotAmt.Text) : 0.00;

                double ROI = ntxt_RenewalROI.Text != "" ? Convert.ToDouble(ntxt_RenewalROI.Text) : 0.00;

                double periodindays = Math.Abs(((dtopen - dtwith).TotalDays));

                if (dtMaturity < dtwith)
                {
                    txt_MaturityAmt.Text = ntxt_RMaturityAmt.Text;
                    txtIntAdj.Text = Convert.ToString(Convert.ToDouble(txt_MaturityAmt.Text) - TotDepoAmt);
                }
                else
                {
                    txt_MaturityAmt.Text = Convert.ToString(Math.Round(TotDepoAmt + (periodindays * ROI * TotDepoAmt / 36500)));
                    txtIntAdj.Text = Convert.ToString(Convert.ToDouble(txt_MaturityAmt.Text) - TotDepoAmt);
                }
            }
            else if (txtacctype.Text == "Deposit Certificate") //Maturity Calculation For Deposite Certificate
            {
                string dtop = txtdtOpening.Text;
                DateTime dtopen = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                string dtm = dtpkr_RenewalMatDt.Text;
                DateTime dtMaturity = DateTime.ParseExact(dtm, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                string dtw = dtpkr_WithdrawlDate.Text;
                DateTime dtwith = DateTime.ParseExact(dtw, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                double TotDepoAmt = txt_RenewalDepotAmt.Text != "" ? Convert.ToDouble(txt_RenewalDepotAmt.Text) : 0.00;

                double ROI = ntxt_RenewalROI.Text != "" ? Convert.ToDouble(ntxt_RenewalROI.Text) : 0.00;

                double InstAmt = Convert.ToDouble(ViewState["InstAmt"]);

                double periodindays = Math.Abs(((dtopen - dtwith).TotalDays));
               
                if (dtMaturity < dtwith)
                {
                    txt_MaturityAmt.Text = ntxt_RMaturityAmt.Text;
                    txtIntAdj.Text = Convert.ToString(Convert.ToDouble(txt_MaturityAmt.Text) - TotDepoAmt);
                }
                else
                {
                    DateTime dt = DateTime.Now;
                    DateTime nedt = dt.AddMonths(3);

                    double totprd = 0;
                    double remdate = 0;
                    while (dtopen < dtwith)
                    {
                        
                        if (Math.Abs(((dtopen.Year - dtwith.Year) * 12) + dtopen.Month - dtwith.Month) >= 3)
                        {
                            dtopen = dtopen.AddMonths(3);
                            totprd = totprd + 1;
                        }
                        else
                        {
                            remdate = Math.Abs(((dtopen - dtwith).TotalDays));
                            break;
                        }



                    }
                    double QuarterInterest = InstAmt * Math.Pow((1 + ROI / 400), totprd);
                    double RemainingInterest = ((QuarterInterest) * ROI * remdate) / 36500;
                    txtIntAdj.Text = Convert.ToString(Math.Round(QuarterInterest + RemainingInterest) - InstAmt);
                    txt_MaturityAmt.Text = Convert.ToString(Math.Round(QuarterInterest + RemainingInterest));
                }


            }

            else if (txtacctype.Text == "RECURRING DEPOSITE") ///Calculate Maturity Amount For Recurring Deposite
            {

            }

            DataSet data = objBL_Finance.MatchBranchCode(2, txtAcNo.Text, Convert.ToString(Session["BranchID"]), out SQLError);
            if (data.Tables[0].Rows.Count > 0)
            {
                lblSession.Text = Convert.ToString(data.Tables[0].Rows[0]["SOCIETY_BR_CODE"]);
            }

            if (Convert.ToString(Session["BranchID"]) == lblSession.Text)
            {
                btn_CloseAccount.Visible = true;
                btn_CloseAccount2.Visible = true;
            }
            else
            {
                MessageBox(this, "Account Not Belongs To This Branch !");
            }

            //btn_CloseAccount.Visible = true;
        }
        
    }
}