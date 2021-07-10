using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;
using BLL.GeneralBL;

namespace iBankingSolution.Transaction
{
    public partial class frmLoanAcOpening : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;

        protected void Page_Load(object sender, EventArgs e)
        {

            dtpkr_RepayWithinDate.Enabled = false;
            ntxt_InstAmount.Enabled = false;
            txtCashDisbursement.Text = txtLoanAmount.Text;
            txtSHGcashdis.Text = txtSHGtoLoa.Text;

            if (!IsPostBack)
            {
                SetInitialRowForApplicationDetails();
                dtpkr_ApplDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dtpkr_InstStartDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dtpkr_FirstDisbDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dtpkr_LoanSancDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtApplDt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtrepaydt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtAppldtSHG.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtSHGRepayWithDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                GetActivity();
                GetActvityKCC();
                GetActivitySHG();


                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetLoanAccountOpeningEditData();


                }

            }
        }
        protected void GetActivity() //Get Activity Drop Down for NO-FARM LOAN ACCOunt
        {
            objBO_Finance.Flag = 3;
            objBO_Finance.SCHEME_CODE = lblDid.Text;
            DataTable dt = objBL_Finance.GetLoanSchemeMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_Activity.DataSource = dt;
                cmbx_Activity.DataTextField = "SCHEME_NAME";
                cmbx_Activity.DataValueField = "SCHEME_CODE";
                cmbx_Activity.DataBind();
            }
        }

        protected void GetActvityKCC() //Get Activity Drop Down for KCC Loan Account
        {
            objBO_Finance.Flag = 8;
            objBO_Finance.SCHEME_CODE = lblDid.Text;
            DataTable dt = objBL_Finance.GetLoanSchemeMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                txtActivity.DataSource = dt;
                txtActivity.DataTextField = "SCHEME_NAME";
                txtActivity.DataValueField = "SCHEME_CODE";
                txtActivity.DataBind();
            }

        }

        protected void GetActivitySHG() //Get Activity Drop Down For SHG LOAN Account
        {
            objBO_Finance.Flag = 9;
            objBO_Finance.SCHEME_CODE = lblDid.Text;
            DataTable dt = objBL_Finance.GetLoanSchemeMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_txtactivityShg.DataSource = dt;
                cmbx_txtactivityShg.DataTextField = "SCHEME_NAME";
                cmbx_txtactivityShg.DataValueField = "SCHEME_CODE";
                cmbx_txtactivityShg.DataBind();
            }
        }

        protected void GetDepositAmt() //Get Total Deposits Amount for SHG LOAN Account 
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.CUST_ID = ViewState["dtApplicantDetails"];
            objBO_Finance.ENTRYDATE = txtAppldtSHG.Text;
            DataTable dt = objBL_Finance.GetTotalDepositSHG(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                txtSHGtodpamt.Text = "";
                txtSHGtodpamt.DataBind();

            }
        }

        protected void Select_ScanBy(object sender, EventArgs e) //Get Scan By Name 
        {
            String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String query = "SELECT SANC_PER FROM LOAN_SCHEME_MASTER WHERE SCHEME_CODE = @SCHEME_CODE";
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@SCHEME_CODE", cmbx_Activity.SelectedItem.Value);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {

                    txt_SancBy.Text = Convert.ToString(sdr["SANC_PER"]);
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
        protected void GetLoanAccountOpeningEditData()
        {
            objBO_Finance.Flag = 8;
            objBO_Finance.SL_CODE = lblDid.Text;
            DataSet dSet = objBL_Finance.GetAccountsDetails(objBO_Finance, out SQLError);
            if (dSet.Tables[0].Rows.Count > 0)
            {
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";
                btnsubmit.CommandArgument = lblDid.Text;
                btnsubmit1.CommandArgument = lblDid.Text;

                cmbx_AcctType.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["LOAN_TYPE"]);
                cmbx_AcctType_SelectedIndexChanged(cmbx_AcctType, null);
                dtpkr_ApplDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["date_of_opening"]).ToString("dd/MM/yyyy");

                txt_LFNO.Text = Convert.ToString(dSet.Tables[0].Rows[0]["lf_acno"]);
                txtLfNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["lf_acno"]);
                txtSHGLFNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["lf_acno"]);

                cmbx_Activity.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SCHEME_CODE"]);
                txtActivity.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SCHEME_CODE"]);
                cmbx_txtactivityShg.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SCHEME_CODE"]);

                dtpkr_ApplDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["APPL_DT"]).ToString("dd/MM/yyyy");
                txtApplDt.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["APPL_DT"]).ToString("dd/MM/yyyy");
                txtAppldtSHG.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["APPL_DT"]).ToString("dd/MM/yyyy");

                ntxt_SancAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LOAN_AMNT"]);
                txtLoanAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LOAN_AMNT"]);
                txtSHGtoLoa.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LOAN_AMNT"]);

                ntxt_SancAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["NET_LOAN"]);

                ntxt_CashDisbursement.Text = Convert.ToString(dSet.Tables[0].Rows[0]["CASH_DISB"]);
                txtCashDisbursement.Text = Convert.ToString(dSet.Tables[0].Rows[0]["CASH_DISB"]);
                txtSHGcashdis.Text = Convert.ToString(dSet.Tables[0].Rows[0]["CASH_DISB"]);

                ntxt_ROI.Text = Convert.ToString(dSet.Tables[0].Rows[0]["ROI"]);
                txtRateOfIn.Text = Convert.ToString(dSet.Tables[0].Rows[0]["ROI"]);
                txtSHGintrt.Text = Convert.ToString(dSet.Tables[0].Rows[0]["ROI"]);

                txt_SancBy.Text = Convert.ToString(dSet.Tables[0].Rows[0]["SANC_PER"]);

                ntxt_ODIntRate.Text = Convert.ToString(dSet.Tables[0].Rows[0]["OD_ROI"]);
                txtPRI.Text = Convert.ToString(dSet.Tables[0].Rows[0]["OD_ROI"]);
                txtSHGPanelintRet.Text = Convert.ToString(dSet.Tables[0].Rows[0]["OD_ROI"]);

                ntxt_LoanPeriod.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DURATION"]);
                txtLoanPeriod.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DURATION"]);
                txtSHGLoanPeriod.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DURATION"]);

                cmbx_RepayMode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["REPAY_MODE"]);
                cmbx_KCCrepayMode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["REPAY_MODE"]);
                cmbx_SHGRepayMode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["REPAY_MODE"]);


                ntxt_NoOfInst.Text = Convert.ToString(dSet.Tables[0].Rows[0]["NO_OF_INST"]);
                cmbx_IntsAppl.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["INST_APPL"]);
                if (Convert.ToString(dSet.Tables[0].Rows[0]["INST_ST_DATE"]) != "")
                {
                    dtpkr_InstStartDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["INST_ST_DATE"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    dtpkr_InstStartDate.Text = "";
                }
                ntxt_InstAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["INST_AMOUNT"]);
                txtPIAmt.Text = Convert.ToString(dSet.Tables[0].Rows[0]["INST_AMOUNT"]);
                txtSHGinstAmt.Text = Convert.ToString(dSet.Tables[0].Rows[0]["INST_AMOUNT"]);

                if (Convert.ToString(dSet.Tables[0].Rows[0]["SANC_DATE"]) != "")
                {
                    dtpkr_LoanSancDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["SANC_DATE"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    dtpkr_LoanSancDate.Text = "";
                }
                if (Convert.ToString(dSet.Tables[0].Rows[0]["SANC_DT"]) != "")
                {
                    dtpkr_LoanSancDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["SANC_DT"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    dtpkr_LoanSancDate.Text = "";
                }

                txt_LFNO.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LF_NO"]);
                if (Convert.ToString(dSet.Tables[0].Rows[0]["FIRST_DISB_DT"]) != "")
                {
                    dtpkr_FirstDisbDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["FIRST_DISB_DT"]).ToString("dd/MM/yyyy");
                }

                else
                {
                    dtpkr_FirstDisbDate.Text = "";
                }
                txt_Purpose.Text = Convert.ToString(dSet.Tables[0].Rows[0]["P_CODE"]);
                if (Convert.ToString(dSet.Tables[0].Rows[0]["LAST_REP_DT"]) != "")
                {
                    dtpkr_RepayWithinDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["LAST_REP_DT"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    dtpkr_RepayWithinDate.Text = "";
                }
                if (Convert.ToString(dSet.Tables[0].Rows[0]["LAST_REP_DT"]) != "")
                {
                    txtrepaydt.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["LAST_REP_DT"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtrepaydt.Text = "";
                }
                if (Convert.ToString(dSet.Tables[0].Rows[0]["LAST_REP_DT"]) != "")
                {
                    txtSHGRepayWithDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["LAST_REP_DT"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtSHGRepayWithDate.Text = "";
                }
                ntxt_AppliedAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["APP_AMOUNT"]);

                cmbx_ODMode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["ODPR"]);
                cmbx_KCCODmode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["ODPR"]);
                cmbx_SHGODmode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["ODPR"]);
            }

            if (dSet.Tables[1].Rows.Count > 0)
            {
                ViewState["dtApplicantDetails"] = dSet.Tables[1];
                gv_ClientDetails.DataSource = dSet.Tables[1];
                gv_ClientDetails.DataBind();
            }

        }
        protected void SetInitialRowForApplicationDetails()
        {
            DataTable dtApplication = new DataTable();
            DataRow dr = null;

            dtApplication.Columns.Add(new DataColumn("Slno", typeof(Int32)));
            dtApplication.Columns.Add(new DataColumn("CUST_ID", typeof(Int32)));
            dtApplication.Columns.Add(new DataColumn("Name", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("GUARDIAN_NAME", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("VILL_CODE", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("DIS_CODE", typeof(string)));

            dr = dtApplication.NewRow();
            dr["Slno"] = "1";
            dr["CUST_ID"] = 0;
            dr["Name"] = String.Empty;
            dr["GUARDIAN_NAME"] = String.Empty;

            dr["VILL_CODE"] = String.Empty;
            dr["DIS_CODE"] = String.Empty;



            dtApplication.Rows.Add(dr);
            gv_ClientDetails.DataSource = dtApplication;
            gv_ClientDetails.DataBind();

        }
        protected void btnsubmit_Click(object sender, EventArgs e) //For Loan Account Open
        {
            try
            {
                if (cmbx_AcctType.SelectedValue == "General")
                {
                    objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                    objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                    objBO_Finance.SL_CODE = lblDid.Text;
                    objBO_Finance.actype = "l";
                    //objBO_Finance.actype = cmbx_AcctType.SelectedValue;
                    string datejoinDt = dtpkr_ApplDate.Text;
                    DateTime dtval = DateTime.ParseExact(datejoinDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.date_of_opening = dtval;
                    //objBO_Finance.date_of_opening = Convert.ToDateTime(dtpkr_ApplDate.Text);
                    objBO_Finance.last_tran_date = DateTime.Now;
                    objBO_Finance.old_acno = "";
                    objBO_Finance.lf_acno = txt_LFNO.Text;
                    objBO_Finance.EmpCode = "100";
                    objBO_Finance.TERMINAL_ID = "0";

                    objBO_Finance.SCHEME_CODE = cmbx_Activity.SelectedValue;
                    objBO_Finance.AC_STATUS = "Live";
                    string dateapplDt = dtpkr_ApplDate.Text;
                    DateTime dtapp = DateTime.ParseExact(dateapplDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.APPL_DT = dtapp;
                    //objBO_Finance.APPL_DT = Convert.ToDateTime(dtpkr_ApplDate.Text);
                    objBO_Finance.LOAN_AMNT = Convert.ToDouble(ntxt_SancAmount.Text);
                    objBO_Finance.SUBSIDY = Convert.ToDouble("0.00"); //
                    objBO_Finance.NET_LOAN = Convert.ToDouble(ntxt_SancAmount.Text);
                    if (!string.IsNullOrEmpty(ntxt_CashDisbursement.Text))
                    {
                        objBO_Finance.CASH_DISB = Convert.ToDouble(ntxt_CashDisbursement.Text);
                    }
                    objBO_Finance.ROI = Convert.ToDouble(ntxt_ROI.Text);
                    objBO_Finance.OD_ROI = Convert.ToDouble(ntxt_ODIntRate.Text);
                    objBO_Finance.DURATION = ntxt_LoanPeriod.Text;
                    objBO_Finance.REPAY_MODE = cmbx_RepayMode.SelectedValue;
                    objBO_Finance.NO_OF_INST = Convert.ToDouble(ntxt_NoOfInst.Text);
                    //if (!string.IsNullOrEmpty(cmbx_IntsAppl.SelectedValue))
                    //{
                    //    objBO_Finance.INST_APPL = Convert.ToBoolean(cmbx_IntsAppl.SelectedIndex >= 0 ? cmbx_IntsAppl.SelectedValue : "False");
                    //}

                    if (cmbx_IntsAppl.SelectedValue == "Yes")
                        objBO_Finance.INST_APPL = true;
                    else if (cmbx_IntsAppl.SelectedValue == "No")
                        objBO_Finance.INST_APPL = false;


                    string insdt = dtpkr_InstStartDate.Text;
                    DateTime dtins = DateTime.ParseExact(insdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.INST_ST_DATE = dtins;
                    //objBO_Finance.INST_ST_DATE = Convert.ToDateTime(dtpkr_InstStartDate.Text);
                    if (!string.IsNullOrEmpty(ntxt_InstAmount.Text))
                    {
                        objBO_Finance.INST_AMOUNT = Convert.ToDouble(ntxt_InstAmount.Text);
                    }
                    //objBO_Finance.INST_AMOUNT = Convert.ToDouble(ntxt_InstAmount.Text);
                    string sdate = dtpkr_LoanSancDate.Text;
                    DateTime scandate = DateTime.ParseExact(sdate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.SANC_DATE = scandate;
                    //objBO_Finance.SANC_DATE = Convert.ToDateTime(dtpkr_LoanSancDate.Text);
                    objBO_Finance.SANC_PER = txt_SancBy.Text;
                    string scandt = dtpkr_LoanSancDate.Text;
                    DateTime sad = DateTime.ParseExact(scandt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.SANC_DT = sad;
                    //objBO_Finance.SANC_DT = Convert.ToDateTime(dtpkr_LoanSancDate.Text);
                    objBO_Finance.LF_NO = txt_LFNO.Text;
                    string fddt = dtpkr_FirstDisbDate.Text;
                    DateTime firstdt = DateTime.ParseExact(fddt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.FIRST_DISB_DT = firstdt;
                    //objBO_Finance.FIRST_DISB_DT = Convert.ToDateTime(dtpkr_FirstDisbDate.Text);
                    objBO_Finance.P_CODE = txt_Purpose.Text;
                    objBO_Finance.LOAN_TYPE = "General";
                    string lredt = dtpkr_RepayWithinDate.Text;
                    DateTime lastrepdt = DateTime.ParseExact(lredt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.LAST_REP_DT = lastrepdt;
                    //objBO_Finance.LAST_REP_DT = Convert.ToDateTime(dtpkr_RepayWithinDate.Text);
                    objBO_Finance.ASSES = "";
                    objBO_Finance.APP_AMOUNT = Convert.ToDouble(ntxt_AppliedAmount.Text);
                    objBO_Finance.ODPR = cmbx_ODMode.SelectedValue;

                    DataTable dtClientMaster = new DataTable();
                    dtClientMaster.Columns.Add("CUST_ID");
                    foreach (DataRow dr in (ViewState["dtApplicantDetails"] as DataTable).Rows)
                    {
                        dtClientMaster.Rows.Add(dr["CUST_ID"].ToString());
                    }
                    dtClientMaster.AcceptChanges();

                    objBO_Finance.dtClientMaster = dtClientMaster;
                    objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                }
                else if (cmbx_AcctType.SelectedValue == "Farm")
                {
                    objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                    objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                    objBO_Finance.SL_CODE = lblDid.Text;
                    objBO_Finance.actype = "l";
                    //objBO_Finance.actype = cmbx_AcctType.SelectedValue;
                    objBO_Finance.lf_acno = txtLfNo.Text;
                    objBO_Finance.SCHEME_CODE = txtActivity.SelectedValue;
                    string datejoinDt = dtpkr_ApplDate.Text;
                    DateTime dtval = DateTime.ParseExact(datejoinDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.date_of_opening = dtval;
                    string dateapplDt = txtApplDt.Text;
                    DateTime dtapp = DateTime.ParseExact(dateapplDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.APPL_DT = dtapp;
                    objBO_Finance.LOAN_AMNT = Convert.ToDouble(txtLoanAmount.Text);
                    objBO_Finance.NET_LOAN = Convert.ToDouble(txtLoanAmount.Text);
                    objBO_Finance.CASH_DISB = Convert.ToDouble(txtCashDisbursement.Text);
                    objBO_Finance.ROI = Convert.ToDouble(txtRateOfIn.Text);
                    objBO_Finance.OD_ROI = Convert.ToDouble(txtPRI.Text);
                    objBO_Finance.LOAN_TYPE = "Farm";
                    objBO_Finance.DURATION = txtLoanPeriod.Text;
                    string lredt = txtrepaydt.Text;
                    DateTime lastrepdt = DateTime.ParseExact(lredt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.LAST_REP_DT = lastrepdt;
                    objBO_Finance.REPAY_MODE = cmbx_KCCrepayMode.SelectedValue;
                    objBO_Finance.ODPR = cmbx_KCCODmode.SelectedValue;
                    objBO_Finance.INST_AMOUNT = Convert.ToDouble(txtPIAmt.Text);
                    objBO_Finance.last_tran_date = DateTime.Now;
                    objBO_Finance.old_acno = "";
                    objBO_Finance.EmpCode = "100";
                    objBO_Finance.TERMINAL_ID = "0";
                    objBO_Finance.AC_STATUS = "Live";
                    DataTable dtClientMaster = new DataTable();
                    dtClientMaster.Columns.Add("CUST_ID");
                    foreach (DataRow dr in (ViewState["dtApplicantDetails"] as DataTable).Rows)
                    {
                        dtClientMaster.Rows.Add(dr["CUST_ID"].ToString());
                    }
                    dtClientMaster.AcceptChanges();

                    objBO_Finance.dtClientMaster = dtClientMaster;
                    objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                }
                else if (cmbx_AcctType.SelectedValue == "Shg")
                {
                    objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                    objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                    objBO_Finance.SL_CODE = lblDid.Text;
                    objBO_Finance.actype = "l";
                    objBO_Finance.lf_acno = txtSHGLFNo.Text;
                    objBO_Finance.SCHEME_CODE = cmbx_txtactivityShg.SelectedValue;
                    string datejoinDt = txtAppldtSHG.Text;
                    DateTime dtval = DateTime.ParseExact(datejoinDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.date_of_opening = dtval;
                    string dateapplDt = txtAppldtSHG.Text;
                    DateTime dtapp = DateTime.ParseExact(dateapplDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.APPL_DT = dtapp;
                    objBO_Finance.LOAN_AMNT = Convert.ToDouble(txtSHGtoLoa.Text);
                    objBO_Finance.NET_LOAN = Convert.ToDouble(txtSHGtoLoa.Text);
                    objBO_Finance.CASH_DISB = Convert.ToDouble(txtSHGcashdis.Text);
                    objBO_Finance.ROI = Convert.ToDouble(txtSHGintrt.Text);
                    objBO_Finance.OD_ROI = Convert.ToDouble(txtSHGPanelintRet.Text);
                    objBO_Finance.LOAN_TYPE = "Shg";
                    objBO_Finance.DURATION = txtSHGLoanPeriod.Text;
                    string lredt = txtSHGRepayWithDate.Text;
                    DateTime lastrepdt = DateTime.ParseExact(lredt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.LAST_REP_DT = lastrepdt;
                    objBO_Finance.REPAY_MODE = cmbx_SHGRepayMode.SelectedValue;
                    objBO_Finance.ODPR = cmbx_SHGODmode.SelectedValue;
                    objBO_Finance.INST_AMOUNT = Convert.ToDouble(txtSHGinstAmt.Text);
                    objBO_Finance.last_tran_date = DateTime.Now;
                    objBO_Finance.old_acno = "";
                    objBO_Finance.EmpCode = "100";
                    objBO_Finance.TERMINAL_ID = "0";
                    objBO_Finance.AC_STATUS = "Live";
                    DataTable dtClientMaster = new DataTable();
                    dtClientMaster.Columns.Add("CUST_ID");
                    foreach (DataRow dr in (ViewState["dtApplicantDetails"] as DataTable).Rows)
                    {
                        dtClientMaster.Rows.Add(dr["CUST_ID"].ToString());
                    }
                    dtClientMaster.AcceptChanges();

                    objBO_Finance.dtClientMaster = dtClientMaster;
                    objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                }

                int i = objBL_Finance.InsertUpdateDeleteLoanAccountOpening(objBO_Finance, out SQLError);


                if (i > 0)
                {
                    if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                    {

                        MessageBox(this, "Record Inserted Successfully . Allotted Loan Account No is:-" + SQLError);

                        //message = "alert('Save Successfully.')";
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";
                        Label1.Visible = true;
                        DivID.Visible = true;
                        //Label1.Text = "Alloted Account Number is:" + SQLError;

                        objBO_Finance.Flag = 1;
                        ResetControls();
                    }
                    if (btnsubmit1.Text == "Update" || btnsubmit.Text == "Update")
                    {
                        MessageBox(this, "Update Successfully.");
                        //message = "alert('Update Successfully.')";

                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";

                        //ResetControls();
                    }

                    else
                    {
                        MessageBox(this, "Something Wrong Input.");

                        //message = "alert('Something Wrong Input.')";
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
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
        public static void MessageBox(System.Web.UI.Page page, string strMsg) //Message Box For Message
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);
        }
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            btnsubmit1.Text = "Save";
            //gv_ClientDetails.EmptyDataText = String.Empty;
            ntxt_AppliedAmount.Text = String.Empty;
            dtpkr_RepayWithinDate.Text = String.Empty;
            dtpkr_ApplDate.Text = String.Empty;
            txt_LFNO.Text = String.Empty;
            cmbx_Activity.SelectedIndex = -1;
            dtpkr_ApplDate.Text = String.Empty;
            ntxt_SancAmount.Text = String.Empty;
            //ntxt_SubsidyAmount.Text = String.Empty;
            ntxt_SancAmount.Text = String.Empty;
            ntxt_CashDisbursement.Text = String.Empty;
            ntxt_ROI.Text = String.Empty;
            ntxt_ODIntRate.Text = String.Empty;
            ntxt_LoanPeriod.Text = String.Empty;
            cmbx_RepayMode.SelectedIndex = -1;
            ntxt_NoOfInst.Text = String.Empty;
            cmbx_IntsAppl.SelectedIndex = -1;
            dtpkr_InstStartDate.Text = String.Empty;
            ntxt_InstAmount.Text = String.Empty;
            dtpkr_LoanSancDate.Text = String.Empty;
            txt_LFNO.Text = String.Empty;
            dtpkr_FirstDisbDate.Text = String.Empty;
            dtpkr_RepayWithinDate.Text = String.Empty;
            ntxt_AppliedAmount.Text = String.Empty;
            txt_SancBy.Text = String.Empty;

            //For KCC Loan
            txtLfNo.Text = String.Empty;
            //For SHG Loan
            gv_ClientDetails.DataSource = null;
            gv_ClientDetails.DataBind();



        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        private DataTable dtApplicantDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SlNo"),
                    new DataColumn("CUST_ID"),
                    new DataColumn("Name"),
                    new DataColumn("GUARDIAN_NAME"),
                    new DataColumn("VILL_CODE"),
                    new DataColumn("DIS_CODE"),
                });
            return dt;
        }

        protected void ntxt_Applicant_TextChanged(object sender, EventArgs e)
        {

            DataTable dt = dtApplicantDetails();
            for (int i = dt.Rows.Count; i < Convert.ToInt32(ntxt_Applicant.Text); i++)
            {
                {
                    dt.Rows.Add(i + 1, "", "", "", "", "");
                }

                gv_ClientDetails.DataSource = dt;
                gv_ClientDetails.DataBind();
                ViewState["dtApplicantDetails"] = dt;

            }

        }

        protected void CUSTCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.CUST_ID = ((TextBox)sender).Text;
            GridViewRow item = ((TextBox)sender).NamingContainer as GridViewRow;
            DataTable dt = objBL_Finance.GetKYCDetailsRecords(objBO_Finance, out SQLError);
            DataTable dtApplicantDetails = ViewState["dtApplicantDetails"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                var inst = dtApplicantDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                inst.SetField("CUST_ID", Convert.ToString(dt.Rows[0]["CUST_ID"]));
                inst.SetField("Name", Convert.ToString(dt.Rows[0]["Name"]));
                inst.SetField("GUARDIAN_NAME", Convert.ToString(dt.Rows[0]["Guardian_Name"]));
                inst.SetField("VILL_CODE", Convert.ToString(dt.Rows[0]["Vill_Code"]));
                inst.SetField("DIS_CODE", Convert.ToString(dt.Rows[0]["Dist_Code"]));
            }
            else
            {

                message = "Data Not Found";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

                var inst = dtApplicantDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                inst.SetField("CUST_ID", "");
                inst.SetField("Name", "");
                inst.SetField("GUARDIAN_NAME", "");
                inst.SetField("VILL_CODE", "");
                inst.SetField("DIS_CODE", "");
            }
            dtApplicantDetails.AcceptChanges();
            gv_ClientDetails.DataSource = dtApplicantDetails;
            gv_ClientDetails.DataBind();
            ViewState["dtApplicantDetails"] = dtApplicantDetails;

        }

        protected void ntxt_LoanPeriod_TextChanged(object sender, EventArgs e)//Inst for NO-Farm Loan
        {
            string datejoinDt = dtpkr_ApplDate.Text;
            DateTime dtval = DateTime.ParseExact(datejoinDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime formattedmonths = dtval.AddMonths(Int16.Parse(ntxt_LoanPeriod.Text));
            dtpkr_RepayWithinDate.Text = formattedmonths.ToString("dd/MM/yyyy");

            if (ntxt_AppliedAmount.Text == "")
            {
                ntxt_AppliedAmount.Text = "0";
            }
            else
            {
                Convert.ToDecimal(ntxt_AppliedAmount.Text);
            }



            Convert.ToDecimal(ntxt_ROI.Text);
            Convert.ToDecimal(ntxt_LoanPeriod.Text);
            ntxt_InstAmount.Text = Convert.ToString((Convert.ToDecimal(ntxt_AppliedAmount.Text)) * (Convert.ToDecimal(ntxt_ROI.Text)) * (Convert.ToDecimal(ntxt_LoanPeriod.Text)) / 1200);
        }

        protected void txtloanKCC_select(object sender, EventArgs e)//Inst for KCC Loan
        {
            string datejoinDt = txtApplDt.Text;
            DateTime dtval = DateTime.ParseExact(datejoinDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime formattedmonths = dtval.AddMonths(Int16.Parse(txtLoanPeriod.Text));
            txtrepaydt.Text = formattedmonths.ToString("dd/MM/yyyy");

            if (txtLoanAmount.Text == "")
            {
                txtLoanAmount.Text = "0";
            }
            else
            {
                Convert.ToDecimal(txtLoanAmount.Text);
            }



            Convert.ToDecimal(txtRateOfIn.Text);
            Convert.ToDecimal(txtLoanPeriod.Text);
            txtPIAmt.Text = Convert.ToString((Convert.ToDecimal(txtLoanAmount.Text)) * (Convert.ToDecimal(txtRateOfIn.Text)) * (Convert.ToDecimal(txtLoanPeriod.Text)) / 1200);
        }

        protected void txtSHGLoan_Select(object sender, EventArgs e)//Inst for SHG Loan
        {
            string datejoinDt = txtAppldtSHG.Text;
            DateTime dtval = DateTime.ParseExact(datejoinDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DateTime formattedmonths = dtval.AddMonths(Int16.Parse(txtSHGLoanPeriod.Text));
            txtSHGRepayWithDate.Text = formattedmonths.ToString("dd/MM/yyyy");

            if (txtSHGtoLoa.Text == "")
            {
                txtSHGtoLoa.Text = "0";
            }
            else
            {
                Convert.ToDecimal(txtSHGtoLoa.Text);
            }



            Convert.ToDecimal(txtSHGintrt.Text);
            Convert.ToDecimal(txtSHGLoanPeriod.Text);
            txtSHGinstAmt.Text = Convert.ToString((Convert.ToDecimal(txtSHGtoLoa.Text)) * (Convert.ToDecimal(txtSHGintrt.Text)) * (Convert.ToDecimal(txtSHGLoanPeriod.Text)) / 1200);
        }
        protected void itNew_Click(object sender, ImageClickEventArgs e)
        {
            //DataTable dt = new DataTable();
            //DataRow dr = null;
            ////Define the Columns
            //Int32 rowcnt = 0;
            //dt.Columns.Add(new DataColumn("Sl", typeof(Int32)));
            //dt.Columns.Add(new DataColumn("SHG_NAME", typeof(string)));
            //dt.Columns.Add(new DataColumn("Gender", typeof(string)));
            //dt.Columns.Add(new DataColumn("SHG_AGE", typeof(int)));
            //dt.Columns.Add(new DataColumn("SHG_JOIN_DT", typeof(DateTime)));
            //dt.Columns.Add(new DataColumn("Cast", typeof(string)));
            //dt.Columns.Add(new DataColumn("SHG_TYPE_NO", typeof(string)));
            //dt.Columns.Add(new DataColumn("PAN_CARD_NO", typeof(string)));
            //dt.Columns.Add(new DataColumn("AADHAR_NO", typeof(string)));

            //foreach (GridViewRow row in gv_SHGMemberDetails.Rows)
            //{

            //    Label lbl_SlNo = row.FindControl("lbl_SlNo") as Label;
            //    TextBox txtSHGNAME = row.FindControl("txt_Name") as TextBox;
            //    DropDownList cmbxGender = row.FindControl("cmbx_Gender") as DropDownList;
            //    TextBox txtAge = row.FindControl("txt_Age") as TextBox;
            //    TextBox joinDt = row.FindControl("dtpkr_JoinDate") as TextBox;
            //    DropDownList cmbxcast = row.FindControl("cmbx_Cast") as DropDownList;
            //    TextBox txtNo = row.FindControl("txt_No") as TextBox;
            //    TextBox txtPANNo = row.FindControl("txt_PANNo") as TextBox;
            //    TextBox txtAADHARNo = row.FindControl("txt_AADHARNo") as TextBox;

            //    dr = dt.NewRow();

            //    rowcnt = Convert.ToInt32(dt.Rows.Count) + 1;
            //    dr[0] = lbl_SlNo.Text != "" ? lbl_SlNo.Text : "";
            //    dr[1] = txtSHGNAME.Text != "" ? txtSHGNAME.Text : "";
            //    dr[2] = cmbxGender.SelectedItem.Text != "Male" ? cmbxGender.SelectedItem.Text : "";
            //    dr[3] = txtAge.Text != "" ? txtAge.Text : "0";
            //    dr[4] = joinDt.Text != "" ? joinDt.Text : "";
            //    dr[5] = cmbxcast.Text != "APL" ? cmbxcast.Text : "";
            //    dr[6] = txtNo.Text != "" ? txtNo.Text : "";
            //    dr[7] = txtPANNo.Text != "" ? txtPANNo.Text : "";
            //    dr[8] = txtAADHARNo.Text != "" ? txtAADHARNo.Text : "";
            //    dt.Rows.Add(dr);
            //}
            //dt.Rows.Add(rowcnt + 1, 0, String.Empty, 0);
            //gv_SHGMemberDetails.DataSource = dt;
            //gv_SHGMemberDetails.DataBind();
            //ViewState["dtSHGMemberDetails"] = dt;


        }
        protected void itbndelete_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void cmbx_AcctType_SelectedIndexChanged(object sender, EventArgs e) //For Account Select
        {
            lbltype.Text = "Selected Account Type is: " + cmbx_AcctType.SelectedItem.Text;
            if (cmbx_AcctType.SelectedValue == "General")
            {
                PanelForNoFarmLoan.Visible = true;
                PanelForGrid.Visible = true;
                PanelKCCLoan.Visible = false;
                PanelShgLoanAccount.Visible = false;
            }

            else if (cmbx_AcctType.SelectedValue == "Farm")
            {
                PanelForNoFarmLoan.Visible = false;
                PanelForGrid.Visible = true;
                PanelKCCLoan.Visible = true;
                PanelShgLoanAccount.Visible = false;
            }
            else if (cmbx_AcctType.SelectedValue == "Shg")
            {
                PanelForNoFarmLoan.Visible = false;
                PanelKCCLoan.Visible = false;
                PanelShgLoanAccount.Visible = true;
                PanelForGrid.Visible = true;
            }
            else if (cmbx_AcctType.SelectedValue == "s")
            {
                PanelForNoFarmLoan.Visible = false;
                PanelForGrid.Visible = false;
                PanelKCCLoan.Visible = false;
                PanelShgLoanAccount.Visible = false;
            }
        }

        protected void ntxt_applDate(object sender, EventArgs e)
        {
            dtpkr_FirstDisbDate.Text = dtpkr_ApplDate.Text;
        }
        protected void ntxt_SancAmt(object sender, EventArgs e)
        {
            ntxt_CashDisbursement.Text = ntxt_SancAmount.Text;
        }

        protected void cmbx_RepayMode_SelectedIndexChanged(object sender, EventArgs e) //Repay Mode Select For No-Farm Loan
        {
            if (cmbx_RepayMode.SelectedValue == "Monthly Compound" || cmbx_RepayMode.SelectedValue == "Monthly")
            {
                string lredt = dtpkr_ApplDate.Text;
                DateTime dt = DateTime.ParseExact(lredt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime nedt = dt.AddMonths(1);
                dtpkr_InstStartDate.Text = Convert.ToDateTime(nedt).ToString("dd/MM/yyyy");
            }
            else if (cmbx_RepayMode.SelectedValue == "Quarterly" || cmbx_RepayMode.SelectedValue == "Quarterly Compound")
            {
                string lredt = dtpkr_ApplDate.Text;
                DateTime dt = DateTime.ParseExact(lredt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime nedt = dt.AddMonths(3);
                dtpkr_InstStartDate.Text = Convert.ToDateTime(nedt).ToString("dd/MM/yyyy");
            }
            else if (cmbx_RepayMode.SelectedValue == "Half Yearly")
            {
                string lredt = dtpkr_ApplDate.Text;
                DateTime dt = DateTime.ParseExact(lredt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime nedt = dt.AddMonths(6);
                dtpkr_InstStartDate.Text = Convert.ToDateTime(nedt).ToString("dd/MM/yyyy");
            }
            else if (cmbx_RepayMode.SelectedValue == "Yearly" || cmbx_RepayMode.SelectedValue == "Yearly Compound")
            {
                string lredt = dtpkr_ApplDate.Text;
                DateTime dt = DateTime.ParseExact(lredt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime nedt = dt.AddMonths(12);
                dtpkr_InstStartDate.Text = Convert.ToDateTime(nedt).ToString("dd/MM/yyyy");
            }
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

            objBO_Finance.Flag = 8;
            objBO_Finance.SL_CODE = txtsearchkyc.Text;
            DataSet dSet = objBL_Finance.GetAccountsDetails(objBO_Finance, out SQLError);
            if (dSet.Tables[0].Rows.Count > 0)
            {
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";
                btnsubmit.CommandArgument = txtsearchkyc.Text;
                btnsubmit1.CommandArgument = txtsearchkyc.Text;

                cmbx_AcctType.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["LOAN_TYPE"]);
                cmbx_AcctType_SelectedIndexChanged(cmbx_AcctType, null);
                dtpkr_ApplDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["date_of_opening"]).ToString("dd/MM/yyyy");

                txt_LFNO.Text = Convert.ToString(dSet.Tables[0].Rows[0]["lf_acno"]);
                txtLfNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["lf_acno"]);
                txtSHGLFNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["lf_acno"]);

                ///cmbx_Activity.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SCHEME_CODE"]);
                txtActivity.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SCHEME_CODE"]);
                cmbx_txtactivityShg.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SCHEME_CODE"]);

                dtpkr_ApplDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["APPL_DT"]).ToString("dd/MM/yyyy");
                txtApplDt.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["APPL_DT"]).ToString("dd/MM/yyyy");
                txtAppldtSHG.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["APPL_DT"]).ToString("dd/MM/yyyy");

                ntxt_SancAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LOAN_AMNT"]);
                txtLoanAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LOAN_AMNT"]);
                txtSHGtoLoa.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LOAN_AMNT"]);

                ntxt_SancAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["NET_LOAN"]);

                ntxt_CashDisbursement.Text = Convert.ToString(dSet.Tables[0].Rows[0]["CASH_DISB"]);
                txtCashDisbursement.Text = Convert.ToString(dSet.Tables[0].Rows[0]["CASH_DISB"]);
                txtSHGcashdis.Text = Convert.ToString(dSet.Tables[0].Rows[0]["CASH_DISB"]);

                ntxt_ROI.Text = Convert.ToString(dSet.Tables[0].Rows[0]["ROI"]);
                txtRateOfIn.Text = Convert.ToString(dSet.Tables[0].Rows[0]["ROI"]);
                txtSHGintrt.Text = Convert.ToString(dSet.Tables[0].Rows[0]["ROI"]);

                txt_SancBy.Text = Convert.ToString(dSet.Tables[0].Rows[0]["SANC_PER"]);

                ntxt_ODIntRate.Text = Convert.ToString(dSet.Tables[0].Rows[0]["OD_ROI"]);
                txtPRI.Text = Convert.ToString(dSet.Tables[0].Rows[0]["OD_ROI"]);
                txtSHGPanelintRet.Text = Convert.ToString(dSet.Tables[0].Rows[0]["OD_ROI"]);

                ntxt_LoanPeriod.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DURATION"]);
                txtLoanPeriod.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DURATION"]);
                txtSHGLoanPeriod.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DURATION"]);

                cmbx_RepayMode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["REPAY_MODE"]);
                cmbx_KCCrepayMode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["REPAY_MODE"]);
                cmbx_SHGRepayMode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["REPAY_MODE"]);


                ntxt_NoOfInst.Text = Convert.ToString(dSet.Tables[0].Rows[0]["NO_OF_INST"]);
                cmbx_IntsAppl.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["INST_APPL"]);
                if (Convert.ToString(dSet.Tables[0].Rows[0]["INST_ST_DATE"]) != "")
                {
                    dtpkr_InstStartDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["INST_ST_DATE"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    dtpkr_InstStartDate.Text = "";
                }
                ntxt_InstAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["INST_AMOUNT"]);
                txtPIAmt.Text = Convert.ToString(dSet.Tables[0].Rows[0]["INST_AMOUNT"]);
                txtSHGinstAmt.Text = Convert.ToString(dSet.Tables[0].Rows[0]["INST_AMOUNT"]);

                if (Convert.ToString(dSet.Tables[0].Rows[0]["SANC_DATE"]) != "")
                {
                    dtpkr_LoanSancDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["SANC_DATE"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    dtpkr_LoanSancDate.Text = "";
                }
                if (Convert.ToString(dSet.Tables[0].Rows[0]["SANC_DT"]) != "")
                {
                    dtpkr_LoanSancDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["SANC_DT"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    dtpkr_LoanSancDate.Text = "";
                }

                txt_LFNO.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LF_NO"]);
                if (Convert.ToString(dSet.Tables[0].Rows[0]["FIRST_DISB_DT"]) != "")
                {
                    dtpkr_FirstDisbDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["FIRST_DISB_DT"]).ToString("dd/MM/yyyy");
                }

                else
                {
                    dtpkr_FirstDisbDate.Text = "";
                }
                txt_Purpose.Text = Convert.ToString(dSet.Tables[0].Rows[0]["P_CODE"]);
                if (Convert.ToString(dSet.Tables[0].Rows[0]["LAST_REP_DT"]) != "")
                {
                    dtpkr_RepayWithinDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["LAST_REP_DT"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    dtpkr_RepayWithinDate.Text = "";
                }
                if (Convert.ToString(dSet.Tables[0].Rows[0]["LAST_REP_DT"]) != "")
                {
                    txtrepaydt.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["LAST_REP_DT"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtrepaydt.Text = "";
                }
                if (Convert.ToString(dSet.Tables[0].Rows[0]["LAST_REP_DT"]) != "")
                {
                    txtSHGRepayWithDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["LAST_REP_DT"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtSHGRepayWithDate.Text = "";
                }
                ntxt_AppliedAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["APP_AMOUNT"]);

                cmbx_ODMode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["ODPR"]);
                cmbx_KCCODmode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["ODPR"]);
                cmbx_SHGODmode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["ODPR"]);
            }

            if (dSet.Tables[1].Rows.Count > 0)
            {
                ViewState["dtApplicantDetails"] = dSet.Tables[1];
                gv_ClientDetails.DataSource = dSet.Tables[1];
                gv_ClientDetails.DataBind();
            }
        }
    }
}
