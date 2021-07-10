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
    public partial class frmInvestmentAcOpening : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                SetInitialRowForCertificaterDetails();

                //dtpkr_dateopen.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetInvestmentAccountOpeningEditData();


                }

            }
            Image1.Attributes.Add("style", "cursor: pointer;");
        }
        protected void GetInvestmentAccountOpeningEditData()
        {
            try
            {
                objBO_Finance.Flag = 1;
                objBO_Finance.SL_CODE = lblDid.Text;
                DataSet dSet = objBL_Finance.GetInvestmentAccountDetails(objBO_Finance, out SQLError);

                if (dSet.Tables[0].Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    btnsubmit.CommandArgument = lblDid.Text;
                    btnsubmit1.Text = "Update";
                    btnsubmit1.CommandArgument = lblDid.Text;
                    dtpkr_dateopen.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["DATE_OF_OPENING"]).ToString("dd/MM/yyyy");
                    cmbx_InvestmentType.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["CERT_TYPE"]);
                    cmbx_EffectedAcct.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["INV_TYPE"]);
                    //ntxt_TotCertificate.Text = "1";

                    cmbx_InvestmentType_SelectedIndexChanged(cmbx_InvestmentType, null);
                    cmbx_DepositScheme.DataBind();
                    cmbx_DepositScheme.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SCHEME_CODE"]);


                    txt_PeriodsinMonth.Text = Convert.ToString(dSet.Tables[0].Rows[0]["PRD_MONTH"]);
                    txt_PeriodsInDays.Text = Convert.ToString(dSet.Tables[0].Rows[0]["PRD_DAYS"]);
                    ntxt_ROI.Text = Convert.ToString(dSet.Tables[0].Rows[0]["ROI"]);
                    ntxt_DepositAmt.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DEPOSIT_AMNT"]);
                    ntxt_MaturityAmt.Text = Convert.ToString(dSet.Tables[0].Rows[0]["MATURITY_AMNT"]);
                    dtpkr_MaturityDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["MATURITY_DATE"]).ToString("dd/MM/yyyy");

                    DataTable dt = dtCertificaterDetails();
                    dt.Rows.Add
                        (
                            1,
                            dSet.Tables[0].Rows[0]["RECIPT_NO"],
                            dSet.Tables[0].Rows[0]["CERTIFICATE_NO"],
                            Convert.ToDateTime(dSet.Tables[0].Rows[0]["DATE_OF_PURCHASE"]).ToString("dd/MM/yyyy"),
                            dSet.Tables[0].Rows[0]["AC_NO"],
                            dSet.Tables[0].Rows[0]["BANK_NAME"]
                        );
                    dt.AcceptChanges();
                    GVApplicantDtl.DataSource = dt;
                    GVApplicantDtl.DataBind();
                    ViewState["dtCertificaterDetails"] = dt;
                }
                else
                {
                    message = "Data Not found";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);

                }
            }
            catch (Exception ex)
            {

            }
            finally
            { }
        }
        protected void SetInitialRowForCertificaterDetails()
        {
            DataTable dtApplication = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dtApplication.Columns.Add(new DataColumn("Slno", typeof(Int32)));
            dtApplication.Columns.Add(new DataColumn("ReceiptNo", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("CertificateNo", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("DateOfPurchase", typeof(string)));

            dtApplication.Columns.Add(new DataColumn("AcctNo", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("BankName", typeof(string)));


            dr = dtApplication.NewRow();
            dr["Slno"] = "1";
            dr["ReceiptNo"] = String.Empty;
            dr["CertificateNo"] = String.Empty;
            dr["DateOfPurchase"] = String.Empty;
            dr["AcctNo"] = String.Empty;
            dr["BankName"] = String.Empty;


            dtApplication.Rows.Add(dr);
            GVApplicantDtl.DataSource = dtApplication;
            GVApplicantDtl.DataBind();

        }
        private DataTable dtCertificaterDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SlNo"),
                    new DataColumn("ReceiptNo"),
                    new DataColumn("CertificateNo"),
                    new DataColumn("DateOfPurchase"),
                    new DataColumn("AcctNo"),
                    new DataColumn("BankName")
                });
            return dt;
        }
        //protected void ntxt_TotCertificate_TextChanged(object sender, EventArgs e)
        //{
        //    DataTable dt = ViewState["dtCertificaterDetails"] != null ? ViewState["dtCertificaterDetails"] as DataTable : dtCertificaterDetails();
        //    for (int i = 0; i < Convert.ToInt32(ntxt_TotCertificate.Text); i++)
        //    {
        //        dt.Rows.Add(i + 1, "", "", DateTime.Now.ToString("dd/MM/yyyy"), "", "");
        //    }
        //    GVApplicantDtl.DataSource = dt;
        //    GVApplicantDtl.DataBind();
        //    ViewState["dtCertificaterDetails"] = dt;
        //}
        protected void CalculateMaturityDate()
        {
            CalculateMaturity CalcMat = new CalculateMaturity();
            if (txt_PeriodsInDays.Text == "")
            {
                txt_PeriodsInDays.Text = "0";
            }
            if (txt_PeriodsinMonth.Text == "")
            {
                txt_PeriodsinMonth.Text = "0";
            }
            string dtop = dtpkr_dateopen.Text;
            DateTime dtopen = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//102852
            dtpkr_MaturityDate.Text = CalcMat.CalculateMaturityDate(dtopen, Convert.ToInt32(txt_PeriodsinMonth.Text), Convert.ToInt32(txt_PeriodsInDays.Text)).ToString("dd/MM/yyyy");
            //dtpkr_MaturityDate.Text = CalcMat.CalculateMaturityDate(Convert.ToDateTime(dtpkr_dateopen.Text), Convert.ToInt32(txt_PeriodsinMonth.Text), Convert.ToInt32(txt_PeriodsInDays.Text)).ToString("dd/MM/yyyy");

        }
        //protected void CalculateMaturityDate()
        //{
        //    CalculateMaturity CalcMat = new CalculateMaturity();
        //    if (txt_PeriodsInDays.Text == "")
        //    {
        //        txt_PeriodsInDays.Text = "0";
        //    }
        //    if (txt_PeriodsinMonth.Text == "")
        //    {
        //        txt_PeriodsinMonth.Text = "0";
        //    }
        //    dtpkr_MaturityDate.Text = CalcMat.CalculateMaturityDate(Convert.ToDateTime(dtpkr_dateopen.Text), Convert.ToInt32(txt_PeriodsinMonth.Text), Convert.ToInt32(txt_PeriodsInDays.Text)).ToString("dd/MM/yyyy");

        //}
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            btnsubmit1.Text = "Save";
            GVApplicantDtl.DataSource = null;
            GVApplicantDtl.DataBind();
            txt_PeriodsinMonth.Text = String.Empty;
            txt_PeriodsInDays.Text = String.Empty;
            ntxt_ROI.Text = String.Empty;
            ntxt_DepositAmt.Text = String.Empty;
            ntxt_MaturityAmt.Text = String.Empty;
            cmbx_DepositScheme.SelectedIndex = -1;
            dtpkr_dateopen.Text = String.Empty;
            cmbx_InvestmentType.SelectedIndex = -1;
            cmbx_EffectedAcct.SelectedIndex = -1;

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow item in GVApplicantDtl.Rows)
                {
                    if (((TextBox)item.FindControl("txt_ReceiptNo")).Text != "")
                    {
                        try
                        {
                            objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                            objBO_Finance.SL_CODE = btnsubmit.CommandArgument;
                            objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                            objBO_Finance.SL_CODE = btnsubmit1.CommandArgument;

                            objBO_Finance.RECIPT_NO = ((TextBox)item.FindControl("txt_ReceiptNo")).Text;
                            objBO_Finance.CERTIFICATE_NO = ((TextBox)item.FindControl("txt_CertificateNo")).Text;

                            if (!string.IsNullOrEmpty(((TextBox)item.FindControl("dtpkr_DateOfPurchase")).Text))
                            {
                                string dp = ((TextBox)item.FindControl("dtpkr_DateOfPurchase")).Text;
                                DateTime purchasedt = DateTime.ParseExact(dp, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                objBO_Finance.DATE_OF_PURCHASE = purchasedt;
                            }
                            //objBO_Finance.DATE_OF_PURCHASE = Convert.ToDateTime(((TextBox)item.FindControl("dtpkr_DateOfPurchase")).Text);
                            objBO_Finance.PRD_MONTH = txt_PeriodsinMonth.Text;
                            objBO_Finance.PRD_DAYS = txt_PeriodsInDays.Text;
                            objBO_Finance.ROI = Convert.ToDouble(ntxt_ROI.Text);
                            objBO_Finance.DEPOSIT_AMNT = Convert.ToDouble(ntxt_DepositAmt.Text);
                            objBO_Finance.MATURITY_AMNT = Convert.ToDouble(ntxt_MaturityAmt.Text);

                            if (!string.IsNullOrEmpty(dtpkr_MaturityDate.Text))
                            {
                                string mdt = dtpkr_MaturityDate.Text;
                                DateTime maturitydt = DateTime.ParseExact(mdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                objBO_Finance.MATURITY_DATE = maturitydt;
                            }
                            //objBO_Finance.MATURITY_DATE = Convert.ToDateTime(dtpkr_MaturityDate.Text);
                            objBO_Finance.AC_NO = ((TextBox)item.FindControl("txt_AcctNo")).Text;
                            objBO_Finance.TERMINAL_ID = "0";
                            objBO_Finance.EMPCODE = "10000";
                            objBO_Finance.AC_STATUS = "Live";
                            objBO_Finance.BANK_NAME = ((TextBox)item.FindControl("txt_BankName")).Text;
                            objBO_Finance.SCHEME_CODE = cmbx_DepositScheme.SelectedValue;
                            objBO_Finance.LDG_CODE = cmbx_DepositScheme.SelectedValue;

                            if (!string.IsNullOrEmpty(dtpkr_dateopen.Text))
                            {
                                string od = dtpkr_dateopen.Text;
                                DateTime openingdt = DateTime.ParseExact(od, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                objBO_Finance.date_of_opening = openingdt;
                            }
                            //objBO_Finance.date_of_opening = Convert.ToDateTime(dtpkr_dateopen.Text);
                            objBO_Finance.CERT_TYPE = cmbx_InvestmentType.SelectedValue;
                            objBO_Finance.INV_TYPE = cmbx_EffectedAcct.SelectedValue;
                            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                            int i = objBL_Finance.InsertUpdateDeleteInvestmentOpening(objBO_Finance, out SQLError);


                            if (i > 0)
                            {
                                if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                                {

                                    //message = "alert('Save Successfully.')";
                                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                                    MessageBox(this, "Record Inserted Successfully . Allotted Investement Account No is:-" + SQLError);
                                    btnsubmit.Text = "Save";
                                    btnsubmit1.Text = "Save";
                                    Label1.Visible = true;
                                    DivID.Visible = true;
                                    Label1.Text = "Alloted Investement Account No is:" + SQLError;

                                    objBO_Finance.Flag = 1;
                                    ResetControls();
                                    //message = "alert('Successfully Saved')";
                                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

                                }
                                if (btnsubmit1.Text == "Update" || btnsubmit.Text == "Save")
                                {
                                    message = "alert('Update Successfully.')";

                                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                                    btnsubmit.Text = "Save";
                                    btnsubmit1.Text = "Save";

                                    ResetControls();
                                }

                                else
                                {

                                    message = "alert('Something Wrong Input.')";
                                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                                }
                            }

                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {

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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void cmbx_AcctType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txt_ReceiptNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txt_PeriodsinMonth_TextChanged(object sender, EventArgs e)
        {
            if (txt_PeriodsinMonth.Text != "")
            {
                txt_PeriodsInDays.Enabled = false;
                txt_PeriodsinMonth.Enabled = true;
                txt_PeriodsInDays.Text = String.Empty;
            }
            else
            {
                txt_PeriodsInDays.Enabled = true;
                txt_PeriodsinMonth.Enabled = false;
                txt_PeriodsinMonth.Text = String.Empty;
            }
        }

        protected void txt_PeriodsInDays_TextChanged(object sender, EventArgs e)
        {
            if (txt_PeriodsInDays.Text != "")
            {
                txt_PeriodsinMonth.Enabled = false;
                txt_PeriodsInDays.Enabled = true;
                txt_PeriodsinMonth.Text = String.Empty;
            }
            else
            {
                txt_PeriodsinMonth.Enabled = true;
                txt_PeriodsInDays.Enabled = false;
                txt_PeriodsInDays.Text = String.Empty;
            }
        }

        protected void ntxt_DepositAmt_TextChanged(object sender, EventArgs e)
        {
            if (txt_PeriodsInDays.Text == "")
            {
                txt_PeriodsInDays.Text = "0";
            }
            if (txt_PeriodsinMonth.Text == "")
            {
                txt_PeriodsinMonth.Text = "0";
            }
            CalculateMaturity CalcMat = new CalculateMaturity();
            ntxt_MaturityAmt.Text = Convert.ToString(CalcMat.MaturityCalculation(Convert.ToString(cmbx_InvestmentType.SelectedValue), Convert.ToDouble(ntxt_DepositAmt.Text), Convert.ToInt32(txt_PeriodsinMonth.Text), Convert.ToInt32(txt_PeriodsInDays.Text), Convert.ToDouble(ntxt_ROI.Text)));
            CalculateMaturityDate();
        }

        protected void cmbx_InvestmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbx_InvestmentType.SelectedIndex > 0)
            {

                objBO_Finance.Flag = 6;
                objBO_Finance.SCHEME_TYPE = cmbx_InvestmentType.SelectedValue;
                cmbx_DepositScheme.DataSource = objBL_Finance.GetDepositMasterRecords(objBO_Finance, out SQLError);
                cmbx_DepositScheme.DataTextField = "SCHEME";
                cmbx_DepositScheme.DataValueField = "LDG_CODE";
                cmbx_DepositScheme.DataBind();
            }
        }

        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {

            txt_PeriodsInDays.Enabled = true;
            txt_PeriodsinMonth.Enabled = true;



        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)//SELECT DROP DOWN
        {
            if (cmbx_ddlsearch.SelectedIndex > 0)
            {
                txtsearchkyc.Focus();
            }

        }

        protected void txtsearchAccount_TextChanged(object sender, EventArgs e) //EDIT Account
        {
            try
            {
                objBO_Finance.Flag = 1;
                objBO_Finance.SL_CODE = txtsearchkyc.Text;
                DataSet dSet = objBL_Finance.GetInvestmentAccountDetails(objBO_Finance, out SQLError);

                if (dSet.Tables[0].Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    btnsubmit.CommandArgument = txtsearchkyc.Text;
                    btnsubmit1.Text = "Update";
                    btnsubmit1.CommandArgument = txtsearchkyc.Text;
                    dtpkr_dateopen.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["DATE_OF_OPENING"]).ToString("dd/MM/yyyy");
                    cmbx_InvestmentType.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["CERT_TYPE"]);
                    cmbx_EffectedAcct.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["INV_TYPE"]);
                    //ntxt_TotCertificate.Text = "1";

                    cmbx_InvestmentType_SelectedIndexChanged(cmbx_InvestmentType, null);
                    cmbx_DepositScheme.DataBind();
                    cmbx_DepositScheme.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SCHEME_CODE"]);


                    txt_PeriodsinMonth.Text = Convert.ToString(dSet.Tables[0].Rows[0]["PRD_MONTH"]);
                    txt_PeriodsInDays.Text = Convert.ToString(dSet.Tables[0].Rows[0]["PRD_DAYS"]);
                    ntxt_ROI.Text = Convert.ToString(dSet.Tables[0].Rows[0]["ROI"]);
                    ntxt_DepositAmt.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DEPOSIT_AMNT"]);
                    ntxt_MaturityAmt.Text = Convert.ToString(dSet.Tables[0].Rows[0]["MATURITY_AMNT"]);
                    dtpkr_MaturityDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["MATURITY_DATE"]).ToString("dd/MM/yyyy");

                    DataTable dt = dtCertificaterDetails();
                    dt.Rows.Add
                        (
                            1,
                            dSet.Tables[0].Rows[0]["RECIPT_NO"],
                            dSet.Tables[0].Rows[0]["CERTIFICATE_NO"],
                            Convert.ToDateTime(dSet.Tables[0].Rows[0]["DATE_OF_PURCHASE"]).ToString("dd/MM/yyyy"),
                            dSet.Tables[0].Rows[0]["AC_NO"],
                            dSet.Tables[0].Rows[0]["BANK_NAME"]
                        );
                    dt.AcceptChanges();
                    GVApplicantDtl.DataSource = dt;
                    GVApplicantDtl.DataBind();
                    ViewState["dtCertificaterDetails"] = dt;
                }
                else
                {
                    message = "Data Not found";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);

                }
            }
            catch (Exception ex)
            {

            }
            finally
            { }
        }
    }
}