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


namespace iBankingSolution.Transaction
{
    public partial class frmLoanDisbursment : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        protected void Page_Load(object sender, EventArgs e)
        {
             

            if (!IsPostBack)
            {
                cmbx_TransferAcNo.Enabled = false;
                GetDetailsLoan();
                //GetVoucherNo();

                dtpkr_DisbDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //dtpkr_JournalDate.Text = DateTime.Now;
            }

        }
        protected void GetDetailsLoan()
        {

            objBO_Finance.Flag = 1;

            DataTable dt = objBL_Finance.GetsubLedgerDetailsLoan(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_AcctNo.DataSource = dt;
                cmbx_AcctNo.DataTextField = "SL_CODE";
                cmbx_AcctNo.DataValueField = "SL_CODE";
                cmbx_AcctNo.DataBind();
                cmbx_AcctNo.Items.Insert(0, "-- Select Loan Account Number --");
            }

        }
        protected void ResetControls()
        {
            cmbx_AcctNo.SelectedIndex = -1;
            ntxt_NetDisb.Text = String.Empty;
            dtpkr_DisbDate.Text = String.Empty;
            //dtpkr_JournalDate.Text = String.Empty;

            dtpkr_NewRepayDate.Text = String.Empty;
            ntxt_NewROI.Text = String.Empty;
            ntxt_NewODROI.Text = String.Empty;
            ntxt_NewSancAmt.Text = String.Empty;
            //chkbx_NewDisburse.Checked = false;
            //fs_AcTransfer.Visible = false;
            GVMember.DataSource = null;
            GVMember.DataBind();
            GV_PREDISBLOAN.DataSource = null;
            GV_PREDISBLOAN.DataBind();
            txt_AcctHead.Text = String.Empty;
            cmbx_TransferAcNo.Items.Clear();
            txt_VoucherNo.Text = String.Empty;
            ntxt_SancCash.Text = String.Empty;
            ntxt_SancTotal.Text = String.Empty;

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
            if (ntxt_NetDisb.Text != "" && dtpkr_DisbDate.Text != "")
            {

                try
                {
                    objBO_Finance.Flag = 1;
                    objBO_Finance.SL_CODE = cmbx_AcctNo.SelectedValue;

                    string ddt = dtpkr_DisbDate.Text;
                    DateTime disdt = DateTime.ParseExact(ddt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.disb_date = disdt;
                    //objBO_Finance.disb_date = Convert.ToDateTime(rdobtn_Type.Items[0].Value == "Cash" ? dtpkr_DisbDate.Text : dtpkr_DisbDate.Text);
            
                    objBO_Finance.disb_amnt = Convert.ToDouble(ntxt_NetDisb.Text);
                    objBO_Finance.INS_TYPE = rdobtn_Type.SelectedValue;
                    objBO_Finance.CASH = Convert.ToDouble(ntxt_NetDisb.Text);
                    objBO_Finance.MAT = 0.00;
                    objBO_Finance.INS = 0.00;
                    objBO_Finance.SHAREAMT = Convert.ToDouble(txtShareAmount.Text);
                    objBO_Finance.SHARESLCODE = txtShareAccount.Text;
                    objBO_Finance.CROPAMT = Convert.ToDouble(txtcropinsu.Text);
                    objBO_Finance.MISAMT = Convert.ToDouble(txtmiscellaneous.Text);
                    objBO_Finance.TRANSFER_TO = cmbx_TransferAcNo.SelectedIndex >= 0 ? cmbx_TransferAcNo.SelectedValue : null;

                    if (dtpkr_NewRepayDate.Text != "")
                    {
                        string repdt = dtpkr_NewRepayDate.Text;
                        DateTime newrptdt = DateTime.ParseExact(repdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objBO_Finance.NewRepayDate = newrptdt;
                    }
                    else
                    {
                        objBO_Finance.NewRepayDate = string.IsNullOrEmpty(dtpkr_NewRepayDate.Text) ? (DateTime?)null : Convert.ToDateTime(dtpkr_NewRepayDate.Text);
                    }
                    
                    //objBO_Finance.NewRepayDate = string.IsNullOrEmpty(dtpkr_NewRepayDate.Text) ? (DateTime?)null : Convert.ToDateTime(dtpkr_NewRepayDate.Text);
                    objBO_Finance.NewROI = ntxt_NewROI.Text != "" ? Convert.ToDouble(ntxt_NewROI.Text) : 0;
                    objBO_Finance.NewODROI = ntxt_NewODROI.Text != "" ? Convert.ToDouble(ntxt_NewODROI.Text) : 0;
                    objBO_Finance.NewDisburseAmt = ntxt_NewSancAmt.Text != "" ? Convert.ToDouble(ntxt_NewSancAmt.Text) : 0;
                    if (chkbx_NewDisburse.Checked == true)
                    {
                        objBO_Finance.NewDisbFlag = true;
                    }
                    else
                    {
                        objBO_Finance.NewDisbFlag = false;
                    }

                    int i = objBL_Finance.InsertUpdateLoanDisbursement(objBO_Finance, out SQLError);
                    if (i > 0)
                    {
                        if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                        {

                            MsgBox("Disbursment Save Successfully.");

                            //MessageBox(this, "Disbursment Save Successfully.");
                            //message = "alert('Disbursment Save Successfully.')";
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                            btnsubmit.Text = "Save";
                            btnsubmit1.Text = "Save";
                            Label1.Visible = true;
                            DivID.Visible = true;
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

            else
            {
                message = "alert('Please Enter Valid Data')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                ntxt_NetDisb.Focus();

            }
        }

        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void cmbx_AcctNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objBO_Finance.Flag = 3;
                objBO_Finance.SL_CODE = cmbx_AcctNo.SelectedValue;
                DataSet dSet = objBL_Finance.GetLoanAccountDetails(objBO_Finance, out SQLError);
                ViewState["slCode"] = dSet.Tables[2];
                String Loantype = String.Empty;
                if (dSet.Tables[0].Rows.Count > 0)
                {
                    txt_AcctHead.Text = Convert.ToString(dSet.Tables[0].Rows[0]["SCHEME_NAME"]);
                    ntxt_SancTotal.Text = Convert.ToString(Convert.ToDouble(dSet.Tables[0].Rows[0]["NET_LOAN"]));
                    ntxt_SancCash.Text = Convert.ToString(Convert.ToDouble(dSet.Tables[0].Rows[0]["NET_LOAN"]));
                    ntxt_NetDisb.Text = Convert.ToString(Convert.ToDouble(dSet.Tables[0].Rows[0]["NET_LOAN"]));
                    Loantype = Convert.ToString(dSet.Tables[0].Rows[0]["LOAN_TYPE"]);

                    //ntxt_PreDisbTotal.Text = Convert.ToString(dSet.Tables[1].Rows.Count > 0 ? Convert.ToDouble(dSet.Tables[1].Rows[0]["DISB_AMNT"]) : 0.00);
                    //ntxt_PreDisbCash.Text = Convert.ToString(dSet.Tables[1].Rows.Count > 0 ? Convert.ToDouble(dSet.Tables[1].Rows[0]["CASH"]) : 0.00);
                }
                //if (dSet.Tables[2].Rows.Count > 0)
                //{
                //    //cmbx_TransferAcNo.Items.Clear();
                //    //cmbx_TransferAcNo.DataSource = dSet.Tables[2];
                //    //cmbx_TransferAcNo.DataValueField = "sl_code";
                //    //cmbx_TransferAcNo.DataTextField = "sl_code";
                //    //cmbx_TransferAcNo.DataBind();
                //}
               

                if (dSet.Tables[3].Rows.Count > 0)
                {
                    GV_PREDISBLOAN.DataSource = dSet.Tables[3];
                    GV_PREDISBLOAN.DataBind();
                }
                else
                {
                    GV_PREDISBLOAN.DataSource = null;
                    GV_PREDISBLOAN.DataBind();
                }

                if (dSet.Tables[4].Rows.Count > 0)
                {
                    GVMember.DataSource = dSet.Tables[4];
                    GVMember.DataBind();
                }
                else
                {
                    GVMember.DataSource = null;
                    GVMember.DataBind();
                }


                if (Loantype == "Farm")
                {
                    chkbx_NewDisburse.Enabled = true;
                    PanelNewDis.Visible = true;
                    chkbx_NewDisburse.Visible = true;
                    Div4.Visible = true;
                    Div5.Visible = false;
                    Panel1.Visible = false;

                    if (dSet.Tables[5].Rows.Count > 0)
                    {
                        txtShareAccount.Text = Convert.ToString(dSet.Tables[5].Rows[0]["SHGACNO"]);
                        txtShareAmount.Text = Convert.ToString(dSet.Tables[5].Rows[0]["BALANCE"]);
                        txtcropinsu.Text = Convert.ToString(dSet.Tables[5].Rows[0]["CROPINVEST"]);
                        txtmiscellaneous.Text = Convert.ToString(dSet.Tables[5].Rows[0]["MISAMT"]);
                    }
                    
                }
                else
                {
                    
                    chkbx_NewDisburse.Enabled = false;
                    chkbx_NewDisburse.Checked = false;
                    PanelNewDis.Visible = false;
                    chkbx_NewDisburse.Visible = false;
                    Div4.Visible = false;
                    Div5.Visible = false;
                    Panel1.Visible = false;
                }



            }
            catch (Exception ex)
            {

            }
            finally
            {

            }


        }

        protected void rdobtn_Type_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (rdobtn_Type.Text == "Cash")
            {
                cmbx_TransferAcNo.SelectedIndex = -1;
                //DataTable dtt = (DataTable)ViewState["slCode"];
                 cmbx_TransferAcNo.Items.Clear();
                //cmbx_TransferAcNo.DataSource = dtt;
                //cmbx_TransferAcNo.DataValueField = "sl_code";
                //cmbx_TransferAcNo.DataTextField = "sl_code";
                //cmbx_TransferAcNo.DataBind();
                //cmbx_TransferAcNo.Items.Insert(0, "000");
                cmbx_TransferAcNo.Enabled = false;

            }
            
            else if (rdobtn_Type.Text == "A/c Trans")
            {
                DataTable dtt = (DataTable)ViewState["slCode"];
                cmbx_TransferAcNo.Items.Clear();
                cmbx_TransferAcNo.DataSource = dtt;
                cmbx_TransferAcNo.DataValueField = "sl_code";
                cmbx_TransferAcNo.DataTextField = "sl_code";
                cmbx_TransferAcNo.DataBind();
                cmbx_TransferAcNo.Items.Insert(0, "A/c No");
                cmbx_TransferAcNo.Enabled = true;
            }
            
            



        }

        protected void GV_PREDISBLOAN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double total = 0;

            foreach (GridViewRow item in GV_PREDISBLOAN.Rows)
            {
                Label lbldisbamt = (Label)item.FindControl("lbldisbamt");
                Label lbltotal = (Label)item.FindControl("lbltotal");
                total = total + Convert.ToDouble(lbldisbamt.Text);
                lbltotal.Text = Convert.ToString(total);

            }



        }
        protected void ntxtSanctionAmt_TextChange(object sender, EventArgs e)
        {
            if (txtShareAmount.Text != "")
            {
                txtDeductionAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(((Convert.ToDecimal(ntxt_NewSancAmt.Text) * 5) / 100)) - Convert.ToDecimal(txtShareAmount.Text)));
            }
            else
            {
                txtDeductionAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(((Convert.ToDecimal(ntxt_NewSancAmt.Text) * 5) / 100)) - Convert.ToDecimal("0")));
            }
        }

        protected void chkbx_NewDisburse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbx_NewDisburse.Checked == true)
            {
                PanelNewDis.Enabled = true;
                dtpkr_NewRepayDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                Div5.Visible = true;
                Panel1.Visible = true;
            }
            else
            {
                PanelNewDis.Enabled = false;

                Div5.Visible = false;
                Panel1.Visible = false;

            }
        }

        protected void ntxt_NetDisb_TextChanged(object sender, EventArgs e)
        {
  
            //if (Convert.ToDouble(ntxt_NetDisb.Text) > Convert.ToDouble(ntxt_SancTotal.Text))
            //{
            //    message = "alert('Amount Disbursed In Excess Of Sanctioned Amount.')";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            //    ntxt_NetDisb.Text = String.Empty;
            //    ntxt_NetDisb.Focus();

            //    return;
            //}
        }

        protected void dtpkr_DisbDate_TextChanged(object sender, EventArgs e)
        {
            GetVoucherNo();
            //string insdt = dtpkr_DisbDate.Text;
            //DateTime dtins = DateTime.ParseExact(insdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //objBO_Finance.disb_date = dtins;
            ////objBO_Finance.disb_date = Convert.ToDateTime(dtpkr_DisbDate.Text);
            //DataSet dSet = objBL_Finance.GenVoucherNo(objBO_Finance);

            //if (dSet.Tables[0].Rows.Count > 0)
            //{

            //    txt_VoucherNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["VoucherNo"]);
            //}
            //else
            //{
            //    txt_VoucherNo.Text = "";
            //}


        }

        protected void GetVoucherNo ()
        {
            string insdt = dtpkr_DisbDate.Text;
            DateTime dtins = DateTime.ParseExact(insdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.disb_date = dtins;
            //objBO_Finance.disb_date = Convert.ToDateTime(dtpkr_DisbDate.Text);
            DataSet dSet = objBL_Finance.GenVoucherNo(objBO_Finance);

            if (dSet.Tables[0].Rows.Count > 0)
            {

                txt_VoucherNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["VoucherNo"]);
            }
            else
            {
                txt_VoucherNo.Text = "";
            }
        }
    }
}