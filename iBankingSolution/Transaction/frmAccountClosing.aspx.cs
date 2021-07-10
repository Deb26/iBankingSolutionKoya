using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Collections;
using System.Globalization;
using BLL.GeneralBL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace iBankingSolution.Transaction
{
    public partial class frmAccountClosing : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        double DepositAmt = 0;
        String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            ntxt_RenewalROI.Enabled = false;
            txt_RePrdinMonth.Enabled = false;
            txt_RePrdInDays.Enabled = false;
            
            
            //dtpkr_WithdrawlDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            if (!IsPostBack)
            {
                dtpkr_RenewalDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dtpkr_RenewalAdjustmentDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
        }

        protected void btnsubmit_Click (object sender , EventArgs e)
        {
            DateTime MaturityDate;
            string dtop = txtdtOpening.Text;
            DateTime dtopen = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime OpeningDate = Convert.ToDateTime(txtdtOpening.Text);
            string dtw = dtpkr_WithdrawlDate.Text;
            DateTime dtwith = DateTime.ParseExact(dtw, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (txtacctype.Text == "Savings" || txtacctype.Text == "Share Account" || txtacctype.Text == "SHG DEPOSITE")
            {
                MaturityDate = System.DateTime.Now;
                double CLBAL = objBL_Finance.GetOPCLBAL(1, txtAcNo.Text, dtwith, out SQLError);
                txt_MaturityAmt.Text = Convert.ToString(CLBAL);
                txtIntAdj.Text = Convert.ToString(Convert.ToDouble(txt_MaturityAmt.Text) - Convert.ToDouble(txtdepoamt.Text));
            }
            double InstAmt = Convert.ToDouble(ViewState["InstAmt"]);
            double TotDepoAmt = txt_RenewalDepotAmt.Text != "" ? Convert.ToDouble(txt_RenewalDepotAmt.Text) : 0.00;
            //double MaturityAmt = Convert.ToDouble(ViewState["MaturityAmt"]);
            double DEPO_PRD_M = txt_RePrdinMonth.Text != "" ? Convert.ToDouble(txt_RePrdinMonth.Text) : 0.00;
            //double DEPO_PRD_D = txt_RePrdInDays.Text != "" ? Convert.ToDouble(txt_RePrdInDays.Text) : 0.00;
            double ROI = ntxt_RenewalROI.Text != "" ? Convert.ToDouble(ntxt_RenewalROI.Text) : 0.00;
            //string dtop = txtdtOpening.Text;
            //DateTime dtopen = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//102852
            ////string dtw = dtpkr_WithdrawlDate.Text;
            ////DateTime dtwith = DateTime.ParseExact(dtw, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            double periodindays = Math.Abs(((dtopen - dtwith).TotalDays));

            
            //else
            //{
            //    double d = Math.Abs(((dtopen - dtwith).TotalDays));
            //}


            ////double periodindays = Math.Abs((Convert.ToDateTime(txtdtOpening.Text) - Convert.ToDateTime(dtpkr_WithdrawlDate.Text)).TotalDays);//
            double periodinmonth = Math.Abs(((dtopen.Year - dtwith.Year) * 12) + dtopen.Month - dtwith.Month);
            ////double periodinmonth = Math.Abs(((Convert.ToDateTime(txtdtOpening.Text).Year - Convert.ToDateTime(dtpkr_WithdrawlDate.Text).Year) * 12) + Convert.ToDateTime(txtdtOpening.Text).Month - Convert.ToDateTime(dtpkr_WithdrawlDate.Text).Month);//



            if (txtdepositScheme.Text == "Fixed Deposite")
            {
                string dtm = dtpkr_RenewalMatDt.Text;
                DateTime dtMaturity = DateTime.ParseExact(dtm, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //DateTime dtMaturity = Convert.ToDateTime(txtdtofMaturiry.Text);
                //DateTime dtWithdral = Convert.ToDateTime(dtpkr_WithdrawlDate.Text);

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
            else if (txtdepositScheme.Text == "Recurring Deposite")
            {
                double MaturityAmt = Convert.ToDouble(ViewState["MaturityAmt"]);
                double TotIns = TotDepoAmt / InstAmt;
                double Totnt = 0;
                double RemainingMonth = periodinmonth - TotIns;
                double RemainingTotInt = 0;
                if (DEPO_PRD_M >= TotIns)
                {
                    Totnt = TotIns * (TotIns + 1);
                    Totnt = Totnt / (12 * 2);
                    Totnt = (Totnt * InstAmt) * (ROI / 100);
                    txtIntAdj.Text = Convert.ToString(Math.Round(Totnt));
                    txt_MaturityAmt.Text = Convert.ToString(TotDepoAmt + Math.Round(Totnt));
                }
                else if (DEPO_PRD_M == TotIns)
                {
                    txtIntAdj.Text = Convert.ToString(Convert.ToDouble(MaturityAmt) - Convert.ToDouble(TotDepoAmt));
                    txt_MaturityAmt.Text = Convert.ToString(MaturityAmt);
                }
                if (RemainingMonth > 0)
                {
                    RemainingTotInt = Math.Round(TotDepoAmt * RemainingMonth * ROI / 1200);
                    Totnt = Totnt + RemainingTotInt;
                    txtIntAdj.Text = Convert.ToString(Math.Round(Totnt));
                    txt_MaturityAmt.Text = Convert.ToString(Convert.ToDouble(TotDepoAmt) + Math.Round(Totnt));
                }

            }
            else if (txtdepositScheme.Text == "Deposit Certificate")
            {
                string dtm = dtpkr_RenewalMatDt.Text;
                DateTime dtMaturity = DateTime.ParseExact(dtm, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

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

                    //select sl_code from subledger_master where ac__Type = 's' and ac_status = 'Live' and sl_code in (select sl_code from client_master where cust_id = @cust_id)
                    //int NoOfQuarter = Convert.ToIghernt32(Math.Floor(periodinmonth / 3));
                    //int RemainingDays = Convert.ToInt32(periodinmonth % 3);

                    double QuarterInterest = InstAmt * Math.Pow((1 + ROI / 400), totprd);
                    double RemainingInterest = ((QuarterInterest) * ROI * remdate) / 36500;
                    txtIntAdj.Text = Convert.ToString(Math.Round(QuarterInterest + RemainingInterest) - InstAmt);
                    txt_MaturityAmt.Text = Convert.ToString(Math.Round(QuarterInterest + RemainingInterest));
                }


            }
            else if (txtdepositScheme.Text == "HOME SAVINGS")
            {

                txtiadjested.Text = Convert.ToString(Math.Round((Convert.ToDouble(txtdepoamtt.Text) * Convert.ToDouble(ntxt_RenewalROII.Text)) / 100));
                txtmamt.Text = Convert.ToString(Math.Round(Convert.ToDouble(txtdepoamtt.Text) + Convert.ToDouble(txtiadjested.Text)));
            }
            //else
            //{

            //    //double CLBAL = objBL_Finance.GetOPCLBAL(2, txtAcNo.Text, Convert.ToDateTime(dtpkr_WithdrawlDate.Text), out SQLError);
            //    double CLBAL = objBL_Finance.GetOPCLBAL(2, txtAcNo.Text, dtwith, out SQLError);
            //    txt_MaturityAmt.Text = Convert.ToString(CLBAL);
            //}


            //txtIntAdj.Text = Convert.ToString(Convert.ToDouble(txt_MaturityAmt.Text) - Convert.ToDouble(txtdepoamt.Text));

            DataSet data = objBL_Finance.MatchBranchCode(2, txtAcNo.Text , Convert.ToString(Session["BranchID"]), out SQLError);
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
            
        }

        //protected void btnsubmit_Click(object sender, EventArgs e)
        //{
        //    DateTime MaturityDate;
        //    DateTime OpeningDate = Convert.ToDateTime(txtdtOpening.Text);
        //    if (txtacctype.Text == "Savings" || txtacctype.Text == "Home Savings" || txtacctype.Text == "Suspense Deposite" || txtacctype.Text == "JLG Deposite" || txtacctype.Text == "Share Account" || txtacctype.Text == "SHG DEPOSITE")
        //    {
        //        MaturityDate = System.DateTime.Now;
        //        //SqlConnection con = new SqlConnection(strConnString);
        //        //SqlCommand cmd = new SqlCommand("Proc_GetScheme", con);
        //        //cmd.CommandType = CommandType.StoredProcedure;
        //        //cmd.Parameters.AddWithValue("@MBalance", Convert.ToString(txt_MaturityAmt.Text));
        //        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        //DataSet ds = new DataSet();
        //        //using (con)
        //        //{
        //        //    con.Open();
        //        //    SqlDataReader result = cmd.ExecuteReader();
        //        //    result.Read();
        //        //    if (result.HasRows)
        //        //    {
        //        //        txt_MaturityAmt.Text = result.GetString(0);
        //        //    }
        //        //}
        //    }
        //    else
        //    {

        //        MaturityDate = Convert.ToDateTime(dtpkr_RenewalMatDt.Text);
        //    }
        //    double InstAmt = Convert.ToDouble(ViewState["InstAmt"]);
        //    double TotDepoAmt = txt_RenewalDepotAmt.Text != "" ? Convert.ToDouble(txt_RenewalDepotAmt.Text) : 0.00;
        //    double MaturityAmt = Convert.ToDouble(ViewState["MaturityAmt"]);
        //    double DEPO_PRD_M = txt_RePrdinMonth.Text != "" ? Convert.ToDouble(txt_RePrdinMonth.Text) : 0.00;
        //    double DEPO_PRD_D = txt_RePrdInDays.Text != "" ? Convert.ToDouble(txt_RePrdInDays.Text) : 0.00;
        //    double ROI = ntxt_RenewalROI.Text != "" ? Convert.ToDouble(ntxt_RenewalROI.Text) : 0.00;
        //    string dtop = txtdtOpening.Text;
        //    DateTime dtopen = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//102852
        //    string dtw = dtpkr_WithdrawlDate.Text;
        //    DateTime dtwith = DateTime.ParseExact(dtw, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //    double periodindays = Math.Abs((dtopen - dtwith).TotalDays);
        //    //double periodindays = Math.Abs((Convert.ToDateTime(txtdtOpening.Text) - Convert.ToDateTime(dtpkr_WithdrawlDate.Text)).TotalDays);//
        //    double periodinmonth = Math.Abs(((dtopen.Year - dtwith.Year) * 12) + dtopen.Month - dtwith.Month);
        //    //double periodinmonth = Math.Abs(((Convert.ToDateTime(txtdtOpening.Text).Year - Convert.ToDateTime(dtpkr_WithdrawlDate.Text).Year) * 12) + Convert.ToDateTime(txtdtOpening.Text).Month - Convert.ToDateTime(dtpkr_WithdrawlDate.Text).Month);//


        //    if (txtdepositScheme.Text == "Fixed Deposite")
        //    {
        //        txt_MaturityAmt.Text = Convert.ToString(Math.Round(TotDepoAmt + (periodindays * ROI * TotDepoAmt / 36500)));
        //    }
        //    else if (txtdepositScheme.Text == "Recurring Deposite")
        //    {
        //        double TotIns = TotDepoAmt / InstAmt;
        //        double Totnt = 0;
        //        double RemainingMonth = periodinmonth - TotIns;
        //        double RemainingTotInt = 0;
        //        if (DEPO_PRD_M >= TotIns)
        //        {
        //            Totnt = TotIns * (TotIns + 1);
        //            Totnt = Totnt / (12 * 2);
        //            Totnt = (Totnt * InstAmt) * (ROI / 100);
        //            txtIntAdj.Text = Convert.ToString(Math.Round(Totnt));
        //            txt_MaturityAmt.Text = Convert.ToString(TotDepoAmt + Math.Round(Totnt));
        //        }
        //        else if (DEPO_PRD_M == TotIns)
        //        {
        //            txtIntAdj.Text = Convert.ToString(Convert.ToDouble(MaturityAmt) - Convert.ToDouble(TotDepoAmt));
        //            txt_MaturityAmt.Text = Convert.ToString(MaturityAmt);
        //        }
        //        if (RemainingMonth > 0)
        //        {
        //            RemainingTotInt = Math.Round(TotDepoAmt * RemainingMonth * ROI / 1200);
        //            Totnt = Totnt + RemainingTotInt;
        //            txtIntAdj.Text = Convert.ToString(Math.Round(Totnt));
        //            txt_MaturityAmt.Text = Convert.ToString(Convert.ToDouble(TotDepoAmt) + Math.Round(Totnt));
        //        }

        //    }
        //    else if (txtdepositScheme.Text == "Deposit Certificate")
        //    {
        //        int NoOfQuarter = Convert.ToInt32(Math.Floor(periodindays / 90));
        //        int RemainingDays = Convert.ToInt32(periodindays % 90);
        //        double QuarterInterest = InstAmt * Math.Pow((1 + ROI / 400), NoOfQuarter);
        //        double RemainingInterest = ((QuarterInterest) * ROI * RemainingDays) / 36500;
        //        txt_MaturityAmt.Text = Convert.ToString(Math.Round(QuarterInterest + RemainingInterest));
        //    }
        //    else if (txtdepositScheme.Text == "Home Savings")
        //    {

        //    }
        //    else
        //    {

        //        //double CLBAL = objBL_Finance.GetOPCLBAL(2, txtAcNo.Text, Convert.ToDateTime(dtpkr_WithdrawlDate.Text), out SQLError);
        //        double CLBAL = objBL_Finance.GetOPCLBAL(2, txtAcNo.Text, dtwith , out SQLError);
        //        txt_MaturityAmt.Text = Convert.ToString(CLBAL);
        //    }
        //    txtIntAdj.Text = Convert.ToString(Convert.ToDouble(txt_MaturityAmt.Text) - Convert.ToDouble(txtdepoamt.Text));
        //    btn_CloseAccount.Visible = true;
        //}



        protected void btnsubmit2_Click(object sender, EventArgs e)
        {
            btnsubmit_Click(sender, e);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Transaction/frmAccountClosing.aspx");
        }

        protected void txtOldAcNo_TextChanged(object sender, EventArgs e)
        {


            DataSet data = objBL_Finance.GetAccountHoldersDetailsBySL_CODEOLD(1, txtOldAcNo.Text, Convert.ToString(Session["BranchID"]), DateTime.Now, out SQLError);
            if (data.Tables[1].Rows.Count > 0)
            {
                btnsubmit1.Enabled = true;
                if (Convert.ToString(data.Tables[1].Rows[0]["ac_status"]) == "Live")
                {
                    btnsubmit1.Enabled = true;
                    lv_AcctHolders.DataSource = data.Tables[0];
                    lv_AcctHolders.DataBind();
                    txtAcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["sl_code"]);
                    txtOldAcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["old_acno"]);
                    txtdtOpening.Text = Convert.ToString(data.Tables[1].Rows[0]["date_of_opening"]);
                    txtdepositScheme.Text = Convert.ToString(data.Tables[1].Rows[0]["actype"]);
                    //ViewState["ActualBalance"] = ntxt_DepositAmount.DbValue = data.Tables[2].Rows.Count > 0 ? Math.Abs(Convert.ToDouble(data.Tables[1].Rows[0]["DEPOSIT_AMOUNT"]) - Math.Abs(Convert.ToDouble(data.Tables[2].Rows[0]["ActBalance"]))) : data.Tables[1].Rows[0]["DEPOSIT_AMOUNT"];
                    ViewState["ActualBalance"] = txtdepoamt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[2].Rows[0]["ActBalance"])));
                    ViewState["InstAmt"] = txt_RenewalDepotAmt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[1].Rows[0]["DEPOSIT_AMOUNT"])));
                    ViewState["MaturityAmt"] = ntxt_RMaturityAmt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[1].Rows[0]["MATURITY_AMT"])));
                    txt_RePrdinMonth.Text = Convert.ToString(data.Tables[1].Rows[0]["DEPO_PRD_M"]);
                    txt_RePrdInDays.Text = Convert.ToString(data.Tables[1].Rows[0]["DEPO_PRD_D"]);
                    ntxt_RenewalROI.Text = Convert.ToString(data.Tables[1].Rows[0]["PERCENTAGE"]);
                    //ntxt_AROI.DbValue = data.Tables[1].Rows[0]["PERCENTAGE"];
                    dtpkr_RenewalMatDt.Text = Convert.ToString(data.Tables[1].Rows[0]["DATE_OF_MATURITY"]);
                    dtpkr_WithdrawlDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtIntrissuedFrCode.Text = Convert.ToString(data.Tables[1].Rows[0]["INT_PAIDLDG"]);
                    txtIntrissuedFrLedgerName.Text = Convert.ToString(data.Tables[1].Rows[0]["INTPaidLdgName"]);
                    cmbx_TransferAcNo.Items.Clear();
                    cmbx_TransferAcNo.DataSource = data.Tables[3];
                    cmbx_TransferAcNo.DataBind();

                    //if (Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "Savings" || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "JLG Deposite"
                    //    || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "SHG Deposite" || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "Suspense Deposite"
                    //    || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "No Frill Deposite")
                    //{
                    //    fs_InterestIssued.Attributes.Add("style", "visibility:hidden;");
                    //    //fs_RenewalInstraction.Attributes.Add("style", "visibility:hidden;");

                    //    PanelCertificate.Visible = false;
                    //    PanelRenew.Visible = false;
                    //}




                    //else if (txtacctype.Text == "MIS Deposite" || txtacctype.Text == "Deposit Certificate" || txtacctype.Text == "Recurring Deposite" || txtacctype.Text == "Investment" || txtacctype.Text == "No Frill Deposite" || txtacctype.Text == "Fixed Deposite")
                    //{
                    //    //PanelCertificate.Visible = true;
                    //    PanelCertificateTwo.Visible = true;
                    //    PanelRenew.Visible = true;
                    //}
                    //else if (txtacctype.Text == "Share Account")
                    //{
                    //    txtRateofint.Text = Convert.ToString(data.Tables[1].Rows[0]["PERCENTAGE"]);
                    //    //txtapplROI.Text = Convert.ToString(dt.Tables[6].Rows[0]["ROI"]);
                    //    txt_MaturityAmt.Text = Convert.ToString(data.Tables[1].Rows[0]["MATURITY_AMT"]);
                    //}
                    ////else
                    ////{
                    ////    fs_InterestIssued.Attributes.Add("style", "visibility:visible;");
                    ////    //fs_RenewalInstraction.Attributes.Add("style", "visibility:visible;");
                    ////}
                    //else
                    //{
                    //    btnsubmit1.Enabled = false;
                    //    message = "Account is Closed";
                    //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                    //    txtAcNo.Text = "";
                    //}
                    if (Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "JLG DEPOSITE"
                         || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "SUSPENSE DEPOSITE"
                        || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "No Frill Deposite")
                    {
                        fs_InterestIssued.Attributes.Add("style", "visibility:hidden;");
                        fs_RenewalInstraction.Attributes.Add("style", "visibility:hidden;");

                        PanelCertificate.Visible = true;
                        PanelRenew.Visible = false;
                        PanelRenewalInstruction.Visible = false;
                        btnsubmit1.Enabled = false;
                    }
                    else if (txtacctype.Text == "MIS DEPOSITE" || txtacctype.Text == "Deposit Certificate" || txtacctype.Text == "RECURRING DEPOSITE" || txtacctype.Text == "Investment" || txtacctype.Text == "NO FRILL DEPOSITE" || txtacctype.Text == "Fixed Deposite" || txtacctype.Text == "HOME SAVINGS")
                    {
                        //PanelCertificate.Visible = true;
                        PanelCertificateTwo.Visible = true;
                        //PanelRenew.Visible = true;
                        maturityPanel.Visible = true;
                        btnsubmit1.Enabled = false;
                        fs_InterestIssued.Visible = true;

                    }
                    else if (txtacctype.Text == "Share Account" || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "Savings" || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "SHG DEPOSITE")
                    {
                        txtRateofint.Text = Convert.ToString(data.Tables[1].Rows[0]["PERCENTAGE"]);
                        //txtapplROI.Text = Convert.ToString(dt.Tables[6].Rows[0]["ROI"]);
                        txt_MaturityAmt.Text = Convert.ToString(data.Tables[1].Rows[0]["MATURITY_AMT"]);
                        PanelCertificate.Visible = false;
                        maturityPanel.Visible = true;
                        btnsubmit1.Enabled = false;
                        // PanelCertificateTwo.Visible = true;
                        //if (cmbx_Type.SelectedValue == "R" || cmbx_Type.SelectedValue == "RP")
                        //{
                        //    PanelRenewalInstruction.Visible = false;
                        //}



                    }
                    else
                    {
                        fs_InterestIssued.Attributes.Add("style", "visibility:visible;");
                        //fs_RenewalInstraction.Attributes.Add("style", "visibility:visible;");
                    }

                }
                else
                {
                    btnsubmit1.Enabled = false;
                    MessageBox(this, "Account is Closed");
                    //message = "Account is Closed";
                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                    txtAcNo.Text = "";
                }


            }

            else
            {
                btnsubmit1.Enabled = false;
                message = "Invalid Account";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                txtAcNo.Text = "";


            }
            btn_CloseAccount.Visible = false;
        }
            


        

        protected void lv_AcctHolder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtAcNo_TextChanged(object sender, EventArgs e)
        {
            DataSet data = objBL_Finance.GetAccountHoldersDetailsBySL_CODE(5, txtAcNo.Text, Convert.ToString(Session["BranchID"]) , DateTime.Now, out SQLError);
            if (data.Tables[1].Rows.Count > 0)
            {
                btnsubmit1.Enabled = true;
                if (Convert.ToString(data.Tables[1].Rows[0]["ac_status"]) == "Live")
                {
                    btnsubmit1.Enabled = true;
                    lv_AcctHolders.DataSource = data.Tables[0];
                    lv_AcctHolders.DataBind();
                    txtAcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["sl_code"]);
                    txtOldAcNo.Text = Convert.ToString(data.Tables[1].Rows[0]["old_acno"]);
                    txtdtOpening.Text = Convert.ToString(data.Tables[1].Rows[0]["date_of_opening"]);
                    txtdepositScheme.Text = Convert.ToString(data.Tables[1].Rows[0]["actype"]);
                    txtacctype.Text= Convert.ToString(data.Tables[1].Rows[0]["actype"]);
                    //ViewState["ActualBalance"] = ntxt_DepositAmount.DbValue = data.Tables[2].Rows.Count > 0 ? Math.Abs(Convert.ToDouble(data.Tables[1].Rows[0]["DEPOSIT_AMOUNT"]) - Math.Abs(Convert.ToDouble(data.Tables[2].Rows[0]["ActBalance"]))) : data.Tables[1].Rows[0]["DEPOSIT_AMOUNT"];
                    ViewState["ActualBalance"] = txtdepoamt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[2].Rows[0]["ActBalance"])));
                    ViewState["ActualBalance"] = txtdepoamtt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[2].Rows[0]["ActBalance"])));
                    ViewState["InstAmt"] = txt_RenewalDepotAmt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[1].Rows[0]["DEPOSIT_AMOUNT"])));
                    ViewState["MaturityAmt"] = ntxt_RMaturityAmt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[1].Rows[0]["MATURITY_AMT"])));
                    ViewState["MaturityAmt"] = ntxt_RMaturityAmtt.Text = Convert.ToString(Math.Abs(Convert.ToDouble(data.Tables[1].Rows[0]["MATURITY_AMT"])));
                    txt_RePrdinMonth.Text = Convert.ToString(data.Tables[1].Rows[0]["DEPO_PRD_M"]);
                    txt_RePrdinMonths.Text = Convert.ToString(data.Tables[1].Rows[0]["DEPO_PRD_M"]);
                    txt_RePrdInDays.Text = Convert.ToString(data.Tables[1].Rows[0]["DEPO_PRD_D"]);
                    txt_RePrdInDay.Text = Convert.ToString(data.Tables[1].Rows[0]["DEPO_PRD_D"]);
                    ntxt_RenewalROI.Text = Convert.ToString(data.Tables[1].Rows[0]["PERCENTAGE"]);
                    ntxt_RenewalROII.Text = Convert.ToString(data.Tables[1].Rows[0]["PERCENTAGE"]);
                    //ntxt_AROI.DbValue = data.Tables[1].Rows[0]["PERCENTAGE"];
                    dtpkr_RenewalMatDt.Text = Convert.ToString(data.Tables[1].Rows[0]["DATE_OF_MATURITY"]);
                    dtpkr_RenewalMatDtt.Text = Convert.ToString(data.Tables[1].Rows[0]["DATE_OF_MATURITY"]);
                    dtpkr_WithdrawlDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtwdt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtIntrissuedFrCode.Text = Convert.ToString(data.Tables[1].Rows[0]["INT_PAIDLDG"]);
                    txtIntrissuedFrCodee.Text = Convert.ToString(data.Tables[1].Rows[0]["INT_PAIDLDG"]); 
                    txtIntrissuedFrLedgerName.Text = Convert.ToString(data.Tables[1].Rows[0]["INTPaidLdgName"]);
                    txtIntrissuedFrLedgerNamee.Text = Convert.ToString(data.Tables[1].Rows[0]["INTPaidLdgName"]); 
                    //txtpenalIntRecCode.Text = Convert.ToString(data.Tables[1].Rows[0]["PEN_INT_RES_ACC"]); 
                    //txtpenalIntRecLedgerName.Text = Convert.ToString(data.Tables[1].Rows[0]["Penalintnomenclature"]); 
                    cmbx_TransferAcNo.Items.Clear();
                    cmbx_TransferAcNo.DataSource = data.Tables[3];
                    cmbx_TransferAcNo.DataBind();

                    //if (Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "Savings" || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "JLG DEPOSITE"
                    //    || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "SHG DEPOSITE" || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "SUSPENSE DEPOSITE"
                    //    || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "No Frill Deposite")
                    //{
                    //    fs_InterestIssued.Attributes.Add("style", "visibility:hidden;");
                    //    fs_RenewalInstraction.Attributes.Add("style", "visibility:hidden;");

                    //    PanelCertificate.Visible = true;
                    //    PanelRenew.Visible = false;

                    //}

                    if (Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "JLG DEPOSITE"
                         || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "SUSPENSE DEPOSITE"
                        || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "No Frill Deposite")
                    {
                        fs_InterestIssued.Attributes.Add("style", "visibility:hidden;");
                        fs_RenewalInstraction.Attributes.Add("style", "visibility:hidden;");

                        PanelCertificate.Visible = true;
                        PanelRenew.Visible = false;
                        PANELHOMESAVINGS.Visible = false;

                        PanelRenewalInstruction.Visible = false;
                        btnsubmit1.Enabled = false;
                        btnsubmit.Enabled = false;
                        panelhomesavingmaturity.Visible = false;
                    }




                    else if (txtacctype.Text == "MIS DEPOSITE" || txtacctype.Text == "Deposit Certificate" 
                             || txtacctype.Text == "RECURRING DEPOSITE" || txtacctype.Text == "Investment" 
                             || txtacctype.Text == "NO FRILL DEPOSITE" || txtacctype.Text == "Fixed Deposite")
                    {
                         //PanelCertificate.Visible = true;
                         PanelCertificateTwo.Visible = true;
                         //PanelRenew.Visible = true;
                         maturityPanel.Visible = true;
                        
                         fs_InterestIssued.Visible = true;
                        btnsubmit1.Enabled = false;
                        btnsubmit.Enabled = false;
                        PANELHOMESAVINGS.Visible = false;
                        panelhomesavingmaturity.Visible = false;


                    }
                    else if (txtacctype.Text == "Share Account" || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "Savings" || Convert.ToString(data.Tables[1].Rows[0]["actype"]) == "SHG DEPOSITE")
                    {
                        txtRateofint.Text = Convert.ToString(data.Tables[1].Rows[0]["PERCENTAGE"]);
                        //txtapplROI.Text = Convert.ToString(dt.Tables[6].Rows[0]["ROI"]);
                        txt_MaturityAmt.Text = Convert.ToString(data.Tables[1].Rows[0]["MATURITY_AMT"]);
                        PanelCertificate.Visible = false;
                        maturityPanel.Visible = true;
                        btnsubmit1.Enabled = false;
                        btnsubmit.Enabled = false;
                        PANELHOMESAVINGS.Visible = false;
                        panelhomesavingmaturity.Visible = false;
                        // PanelCertificateTwo.Visible = true;
                        //if (cmbx_Type.SelectedValue == "R" || cmbx_Type.SelectedValue == "RP")
                        //{
                        //    PanelRenewalInstruction.Visible = false;
                        //}



                    }
                    else if (txtacctype.Text == "HOME SAVINGS")
                    {
                        PANELHOMESAVINGS.Visible = true;
                        PanelCertificateTwo.Visible = false;
                        maturityPanel.Visible = false;
                        fs_InterestIssued.Visible = true;
                        btnsubmit1.Enabled = false;
                        btnsubmit.Enabled = false;
                        panelhomesavingmaturity.Visible = true;

                        
                    }
                    else
                    {
                        fs_InterestIssued.Attributes.Add("style", "visibility:visible;");
                        //fs_RenewalInstraction.Attributes.Add("style", "visibility:visible;");
                    }
                }
                else
                {
                    btnsubmit1.Enabled = false;
                    MessageBox(this, "Account is Closed");
                    //message = "Account is Closed";
                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                    txtAcNo.Text = "";
                }
            }




            else
            {
                MessageBox(this, "Account Not Belongs To This Branch !");
                //btnsubmit1.Enabled = false;
                //message = "Invalid Account";
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                txtAcNo.Text = "";


            }
            btn_CloseAccount.Visible = false;

        }

        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }

        protected void txtacctype_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void txtapplHROI_TextChanged(object sender , EventArgs e)
        {
            txtIntAdj.Text = Convert.ToString(Math.Round((Convert.ToDouble(txtdepoamtt.Text) * Convert.ToDouble(ntxt_RenewalROII.Text)) / 100));
            txt_MaturityAmt.Text = Convert.ToString(Math.Round(Convert.ToDouble(txtdepoamtt) + Convert.ToDouble(txtIntAdj.Text)));
        }

        protected void txtapplROI_TextChanged(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = txtAcNo.Text;
            objBO_Finance.ROI = Convert.ToDouble(txtapplROI.Text);
            objBO_Finance.AsOnDate = System.DateTime.Now;
            DataSet dt = objBL_Finance.GetnterestCalc(objBO_Finance, out SQLError);
            if (txtapplROI.Text != "" && txtAcNo.Text != "")
            {
                //txtAcNo.Text = Convert.ToString(dt.Tables[0].Rows[0]["SL_CODE"]);

                txt_MaturityAmt.Text = Convert.ToString(dt.Tables[0].Rows[0]["TOTMATURITY_AMT"]);
            }

        }
        private void MsgBox(string sMessage)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + sMessage + "');";
            msg += "</script>";
            Response.Write(msg);
        }

        protected void btn_CloseAccount_Click(object sender, EventArgs e)
        {
            if (ddlMaturityInst.SelectedValue == "")
            {

                message = "Select Maturty Instruction";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }

            if (txtacctype.Text == "HOME SAVINGS")
            {
                objBO_Finance.Flag = 1;
                objBO_Finance.SL_CODE = txtAcNo.Text;
                objBO_Finance.DEPOSIT_AMNT = Convert.ToDouble(txtdepoamtt.Text);
                objBO_Finance.Pen_ROI = 0;
                objBO_Finance.MaturityInstruction = cmbx_dropdown.SelectedValue;
                objBO_Finance.SB_ACNO = cmbx_transfer.SelectedValue;
                objBO_Finance.TakeInt = cmbx_TakeInterest.SelectedValue;//
                objBO_Finance.TYPE = cmbx_dropdown.SelectedValue;
                
                if (!string.IsNullOrEmpty(ntxt_RenewalROII.Text))
                {
                    objBO_Finance.RenewalROI = Convert.ToDouble(ntxt_RenewalROII.Text);//hm
                }
                if (!string.IsNullOrEmpty(dtpkr_RenewalMatDtt.Text))//hm
                {
                    string rmdt = dtpkr_RenewalMatDtt.Text;
                    DateTime rmaturitydt = DateTime.ParseExact(rmdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.RenewalMaturityDate = rmaturitydt;
                }
                objBO_Finance.RenewalPeriodsInMonth = txt_RePrdinMonths.Text != "" ? Convert.ToDouble(txt_RePrdinMonths.Text) : 0.00;//hm
                objBO_Finance.RenewalPeriodsInDays = txt_RePrdinMonth.Text != "" ? Convert.ToDouble(txt_RePrdInDay.Text) : 0.00;
                objBO_Finance.InterestAdjusted = txtiadjested.Text != "" ? Convert.ToDouble(txtiadjested.Text) : 0.00;

                objBO_Finance.Maturityamount = txtmamt.Text != "" ? Convert.ToDouble(txtmamt.Text) : 0.00;

                if (!string.IsNullOrEmpty(txtwdt.Text))
                {
                    string wdt = txtwdt.Text;
                    DateTime withdrawldt = DateTime.ParseExact(wdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.WithdrawlDate = withdrawldt;
                }
                objBO_Finance.actype = txtacctype.Text;
                objBO_Finance.EMPCODE = "";
                objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

            }
            else
            {
                objBO_Finance.Flag = 1;
                objBO_Finance.SL_CODE = txtAcNo.Text;
                objBO_Finance.DEPOSIT_AMNT = Convert.ToDouble(txtdepoamt.Text);
                objBO_Finance.Pen_ROI = 0;
                objBO_Finance.Applied_Int = txtapplROI.Text != "" ? Convert.ToDouble(txtapplROI.Text) : 0.00;
                objBO_Finance.MaturityInstruction = ddlMaturityInst.SelectedValue;
                objBO_Finance.SB_ACNO = cmbx_TransferAcNo.SelectedValue;
                objBO_Finance.TakeInt = cmbx_TakeInterest.SelectedValue;
                objBO_Finance.TYPE = ddlMaturityInst.SelectedValue;

                string rdt = dtpkr_RenewalDate.Text;
                DateTime renewdt = DateTime.ParseExact(rdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.RenewalDate = renewdt;
                //objBO_Finance.RenewalDate = Convert.ToDateTime(dtpkr_RenewalDate.Text);
                string rdft = dtpkr_RenewalAdjustmentDate.Text;
                DateTime readjdt = DateTime.ParseExact(rdft, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.RenewalAdjustmentDate = readjdt;
                //objBO_Finance.RenewalAdjustmentDate = Convert.ToDateTime(dtpkr_RenewalAdjustmentDate.Text);
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
                //objBO_Finance.RenewalMaturityDate = Convert.ToDateTime(dtpkr_RenewalMatDt.Text);
                objBO_Finance.RenewalMaturityAmt = ntxt_RMaturityAmt.Text != "" ? Convert.ToDouble(ntxt_RMaturityAmt.Text) : 0.00;

                objBO_Finance.InterestCreditedTillDate = DateTime.Now;
                objBO_Finance.InterestAdjusted = txtIntAdj.Text != "" ? Convert.ToDouble(txtIntAdj.Text) : 0.00;
                objBO_Finance.PenalInterest = 0;
                //objBO_Finance.EMPCODE = "";
                //objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

                if (!string.IsNullOrEmpty(dtpkr_WithdrawlDate.Text))
                {
                    string wdt = dtpkr_WithdrawlDate.Text;
                    DateTime withdrawldt = DateTime.ParseExact(wdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.WithdrawlDate = withdrawldt;
                }
                //objBO_Finance.WithdrawlDate = Convert.ToDateTime(dtpkr_WithdrawlDate.Text);
                objBO_Finance.Maturityamount = txt_MaturityAmt.Text != "" ? Convert.ToDouble(txt_MaturityAmt.Text) : 0.00;
                objBO_Finance.actype = txtacctype.Text;
                objBO_Finance.EMPCODE = "";
                objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            }
            
            int i = objBL_Finance.InsertUpdateDeleteAccountClosing(objBO_Finance, out SQLError);
            if (i > 0)
            {
                

                MsgBox("Successfuly Account Closed");
                btn_CloseAccount.Visible = false;



            }
            else
            {

                message = "Close Not Successful. Error Details:  " + SQLError + "";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
            btnCancel_Click(sender, e);
        }

        protected void btn_CloseAccount2_Click(object sender, EventArgs e)
        {
            btn_CloseAccount_Click(sender, e);
            btnCancel_Click(sender, e);
        }

        protected void cmbx_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbx_TransferAcNo.Enabled = true;
            if (ddlMaturityInst.SelectedValue == "CASH WITHDRAWL")
            {
                //cmbx_IntTransferAccountTo.Text = String.Empty;
                cmbx_IntTransferAccountTo.Enabled = false;
                btnsubmit1.Enabled = true;
                btnsubmit1.Visible = true;
                btnsubmit.Enabled = true;
                btnsubmit.Visible = true;
                
            }
            else if (ddlMaturityInst.SelectedValue == "A/C TRANSFER")
            {

                cmbx_IntTransferAccountTo.Enabled = true;
                GetInterestTransFerTo();
                btnsubmit1.Enabled = true;
                btnsubmit1.Visible = true;
                btnsubmit.Enabled = true;
                btnsubmit.Visible = true;
            }
            else if (ddlMaturityInst.SelectedValue == "R" || ddlMaturityInst.SelectedValue == "RP")
            {
                PanelRenewalInstruction.Visible = true; 
            }
        }

        protected void cmbx_Typehome_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbx_dropdown.SelectedValue == "CASH WITHDRAWL")
            {
                //cmbx_IntTransferAccountTo.Text = String.Empty;
                cmbx_transfer.Enabled = false;
                btnsubmit1.Enabled = true;
                btnsubmit1.Visible = true;
                btnsubmit.Enabled = true;
                btnsubmit.Visible = true;

            }
            else if (cmbx_dropdown.SelectedValue == "A/C TRANSFER")
            {

                cmbx_transfer.Enabled = true;
                GetTransferAccount();
                btnsubmit1.Enabled = true;
                btnsubmit1.Visible = true;
                btnsubmit.Enabled = true;
                btnsubmit.Visible = true;
            }
        }

        protected void GetTransferAccount()
        {
      
            DataSet dt = objBL_Finance.GetAccountNo(2, txtAcNo.Text, out SQLError);

            if (dt.Tables[0].Rows.Count > 0)
            {

                cmbx_transfer.DataSource = dt;
                cmbx_transfer.DataTextField = "SL_CODE";
                cmbx_transfer.DataValueField = "SL_CODE";
                cmbx_transfer.DataBind();


            }
            else
            {
                cmbx_transfer.DataSource = null;
                cmbx_transfer.DataBind();


            }
        }
        protected void GetInterestTransFerTo()
        {
            
            DataSet dt = objBL_Finance.GetAccountNo(2, txtAcNo.Text , out SQLError);

            if (dt.Tables[0].Rows.Count > 0)
            {
                
                cmbx_IntTransferAccountTo.DataSource = dt;
                cmbx_IntTransferAccountTo.DataTextField = "SL_CODE";
                cmbx_IntTransferAccountTo.DataValueField = "SL_CODE";
                cmbx_IntTransferAccountTo.DataBind();
                

            }
            else
            {
                cmbx_IntTransferAccountTo.DataSource = null;
                cmbx_IntTransferAccountTo.DataBind();


            }
            

        }
        protected void intadj_selectedvalue(object sender , EventArgs e)
        {
            //txtiadjested.Text = Convert.ToString(Math.Round((Convert.ToDouble(txtdepoamtt.Text) * Convert.ToDouble(ntxt_RenewalROII.Text)) / 100));
            txtmamt.Text = Convert.ToString(Math.Round(Convert.ToDouble(txtdepoamtt.Text) + Convert.ToDouble(txtiadjested.Text)));
        }

    }
}