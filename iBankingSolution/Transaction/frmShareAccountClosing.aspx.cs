using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using BusinessObject;
using BLL;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iBankingSolution.Transaction
{
    public partial class frmShareAccountClosing : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        double DepositAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtpkr_WithdrawlDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTransferAccount.Enabled = false;
            }
        }

        protected void cmbx_Type_SelectedIndexChanged(object sender, EventArgs e)//For Maturity Instruction Drop Down
        {
            if (ddlMaturityInst.SelectedValue == "CASH WITHDRAWL")
            {
                //btn_CloseAccount2.Visible = true;
                //btn_CloseAccount.Visible = true;
                
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
                txtTransferAccount.Enabled = false;
            }
            else if (ddlMaturityInst.SelectedValue == "A/C TRANSFER")
            {
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
                //btn_CloseAccount2.Visible = true;
                //btn_CloseAccount.Visible = true;
                txtTransferAccount.Enabled = true;
            }
            else if (ddlMaturityInst.SelectedValue == "select")
            {
                btn_CloseAccount2.Visible = false;
                btn_CloseAccount.Visible = false;
                txtTransferAccount.Enabled = false;
            }
        }

        protected void btn_CloseAccount_Click(object sender, EventArgs e)
        {
            if (ddlMaturityInst.SelectedValue == "")
            {

                message = "Select Maturty Instruction";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = txtAcNo.Text;
            objBO_Finance.Pen_ROI = 0;
            objBO_Finance.MaturityInstruction = ddlMaturityInst.SelectedValue;

            if (!string.IsNullOrEmpty(txtTransferAccount.Text))
            {
                objBO_Finance.SB_ACNO = txtTransferAccount.Text;
            }

            objBO_Finance.TYPE = ddlMaturityInst.SelectedValue;

            objBO_Finance.PenalInterest = 0;
            if (!string.IsNullOrEmpty(dtpkr_WithdrawlDate.Text))
            {
                string wdt = dtpkr_WithdrawlDate.Text;
                DateTime withdrawldt = DateTime.ParseExact(wdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.WithdrawlDate = withdrawldt;
            }

            objBO_Finance.Maturityamount = txt_MaturityAmt.Text != "" ? Convert.ToDouble(txt_MaturityAmt.Text) : 0.00;
            objBO_Finance.actype = txtacctype.Text;
            objBO_Finance.EMPCODE = "";
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

            int i = objBL_Finance.ShareAccountClosing(objBO_Finance, out SQLError);
            if (i > 0)
            {
                btn_CloseAccount.Visible = false;
                MessageBox(this, "Account Closed Successfully");
                ResetControls();
                //message = "Successfuly Account Closed";
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                //ScriptManager.RegisterClientScriptBlock(SQLError, 300, 200, "Successful", "redirect");
            }
            else
            {
                MessageBox(this, "Close Not Successful. Error Details: " + SQLError + "");
                ResetControls();
                //message = "Close Not Successful. Error Details:  " + SQLError + "";
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
            //btnCancel_Click(sender, e);
        }
        protected void btn_CloseAccount2_Click(object sender, EventArgs e)
        {
            btn_CloseAccount_Click(sender, e);
        }

        protected void txtAcNo_TextChanged(object sender, EventArgs e)//Get Account Details through sl_code
        {
            GetMaturityAmt();
            DataSet data = objBL_Finance.GetAccountHoldersDetailsBySL_CODE(5, txtAcNo.Text, Convert.ToString(Session["BranchID"]) ,DateTime.Now, out SQLError);
            if (data.Tables[1].Rows.Count > 0)
            {

                if (Convert.ToString(data.Tables[1].Rows[0]["ac_status"]) == "Live")
                {

                    lv_AcctHolders.DataSource = data.Tables[0];
                    lv_AcctHolders.DataBind();
                    txtAcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["sl_code"]);
                    txtOldAcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["old_acno"]);
                    txtdtOpening.Text = Convert.ToString(data.Tables[1].Rows[0]["date_of_opening"]);
                    txtacctype.Text = Convert.ToString(data.Tables[1].Rows[0]["actype"]);

                    if (Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "Share Account")
                    {
                        maturityPanel.Visible = true;

                    }

                }
                else
                {
                    maturityPanel.Visible = false;

                    message = "Account is Closed";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                    txtAcNo.Text = "";
                }
            }




            else
            {
                MessageBox(this, "Account Not Belongs To This Branch !");

                //message = "Invalid Account";
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                txtAcNo.Text = "";


            }
            btn_CloseAccount.Visible = false;

        }

        protected void GetMaturityAmt()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = txtAcNo.Text;
            objBO_Finance.AsOnDate = System.DateTime.Now;
            DataSet dt = objBL_Finance.GetMaturityAmt(objBO_Finance, out SQLError);
            if (txtAcNo.Text != "")
            {

                txt_MaturityAmt.Text = Convert.ToString(dt.Tables[0].Rows[0]["MATURITYAMT"]);
            }
        }

        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        protected void ResetControls()
        {
            txtOldAcNo.Text = String.Empty;
            txtAcNo.Text = String.Empty;
            txtdtOpening.Text = String.Empty;
            txtacctype.Text = String.Empty;
            txtdtLastTrnsDt.Text = String.Empty;
            ddlMaturityInst.SelectedIndex = -1;
            txtTransferAccount.Text = String.Empty;
            txt_MaturityAmt.Text = String.Empty;
        }
    }
}