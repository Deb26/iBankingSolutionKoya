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
    public partial class frmLoanAccountOpening : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            ntxt_Applicant.Focus();
            dtpkr_RepayWithinDate.Enabled = false;
            ntxt_InstAmount.Enabled = false;
            dtpkr_ApplDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            dtpkr_LoanSancDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            dtpkr_FirstDisbDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            dtpkr_InstStartDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (!IsPostBack)
            {
                SetInitialRowForApplicationDetails();

                GetActivity();
                //GetInterestTransFerTo();

                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetLoanAccountOpeningEditData();


                }

            }
        }
        protected void GetActivity()
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
        protected void GetLoanAccountOpeningEditData()
        {
            objBO_Finance.Flag = 8;
            objBO_Finance.SL_CODE = lblDid.Text;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            DataSet dSet = objBL_Finance.GetAccountsDetails(objBO_Finance, out SQLError);
            if (dSet.Tables[0].Rows.Count > 0)
            {
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";
                btnsubmit.CommandArgument = lblDid.Text;
                btnsubmit1.CommandArgument = lblDid.Text;

                dtpkr_ApplDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["date_of_opening"]).ToString("dd/MM/yyyy");
                txt_LFNO.Text = Convert.ToString(dSet.Tables[0].Rows[0]["lf_acno"]);
                cmbx_Activity.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SCHEME_CODE"]);
                dtpkr_ApplDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["APPL_DT"]).ToString("dd/MM/yyyy");
                ntxt_SancAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LOAN_AMNT"]);
                // ntxt_SubsidyAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["SUBSIDY"]);
                ntxt_SancAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["NET_LOAN"]);
                ntxt_CashDisbursement.Text = Convert.ToString(dSet.Tables[0].Rows[0]["CASH_DISB"]);
                ntxt_ROI.Text = Convert.ToString(dSet.Tables[0].Rows[0]["ROI"]);
                ntxt_ODIntRate.Text = Convert.ToString(dSet.Tables[0].Rows[0]["OD_ROI"]);
                ntxt_LoanPeriod.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DURATION"]);
                cmbx_RepayMode.SelectedValue = Convert.ToString(Convert.ToString(dSet.Tables[0].Rows[0]["REPAY_MODE"]));
                ntxt_NoOfInst.Text = Convert.ToString(dSet.Tables[0].Rows[0]["NO_OF_INST"]);
                cmbx_IntsAppl.SelectedValue = Convert.ToString(Convert.ToString(dSet.Tables[0].Rows[0]["INST_APPL"]));
                if (Convert.ToString(dSet.Tables[0].Rows[0]["INST_ST_DATE"]) != "")
                {
                    dtpkr_InstStartDate.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["INST_ST_DATE"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    dtpkr_InstStartDate.Text = "";
                }
                ntxt_InstAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["INST_AMOUNT"]);
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
                ntxt_AppliedAmount.Text = Convert.ToString(dSet.Tables[0].Rows[0]["APP_AMOUNT"]);
                cmbx_ODMode.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["ODPR"]);
            }
            else
            {
                MessageBox(this, "Account Not Belongs To This Branch !");
            }

            if (dSet.Tables[1].Rows.Count > 0)
            {
                ViewState["dtApplicantDetails"] = dSet.Tables[1];
                gv_ClientDetails.DataSource = dSet.Tables[1];
                gv_ClientDetails.DataBind();
            }

        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        protected void SetInitialRowForApplicationDetails()
        {
            DataTable dtApplication = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dtApplication.Columns.Add(new DataColumn("Slno", typeof(Int32)));
            dtApplication.Columns.Add(new DataColumn("CUST_ID", typeof(Int32)));
            dtApplication.Columns.Add(new DataColumn("Name", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("GUARDIAN_NAME", typeof(string)));

            //dtApplication.Columns.Add(new DataColumn("PO_CODE", typeof(string)));
            //dtApplication.Columns.Add(new DataColumn("PS_CODE", typeof(string)));
            //dtApplication.Columns.Add(new DataColumn("BLK_CODE", typeof(string)));

            dtApplication.Columns.Add(new DataColumn("VILL_CODE", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("DIS_CODE", typeof(string)));
            //dtApplication.Columns.Add(new DataColumn("SEX", typeof(string)));
            //dtApplication.Columns.Add(new DataColumn("REL_CODE", typeof(string)));
            //dtApplication.Columns.Add(new DataColumn("PROF_CODE", typeof(string)));

            dr = dtApplication.NewRow();
            dr["Slno"] = "1";
            dr["CUST_ID"] = 0;
            dr["Name"] = String.Empty;
            dr["GUARDIAN_NAME"] = String.Empty;
            //dr["PO_CODE"] = String.Empty;
            //dr["PS_CODE"] = String.Empty;
            //dr["BLK_CODE"] = String.Empty;
            dr["VILL_CODE"] = String.Empty;
            dr["DIS_CODE"] = String.Empty;

            //dr["SEX"] = String.Empty;
            //dr["REL_CODE"] = String.Empty;
            //dr["PROF_CODE"] = String.Empty;

            dtApplication.Rows.Add(dr);
            gv_ClientDetails.DataSource = dtApplication;
            gv_ClientDetails.DataBind();

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                objBO_Finance.SL_CODE = lblDid.Text;
                objBO_Finance.actype = "l";
                string datejoinDt = dtpkr_ApplDate.Text;
                DateTime dtval = DateTime.ParseExact(datejoinDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.date_of_opening = dtval;
                //objBO_Finance.date_of_opening = Convert.ToDateTime(dtpkr_ApplDate.Text);
                objBO_Finance.last_tran_date = DateTime.Now;
                objBO_Finance.old_acno = "";
                objBO_Finance.lf_acno = txt_LFNO.Text;
                objBO_Finance.EmpCode = "10000";
                objBO_Finance.TERMINAL_ID = "0";

                objBO_Finance.SCHEME_CODE = cmbx_Activity.SelectedValue;
                objBO_Finance.AC_STATUS = "Live";
                string dateapplDt = dtpkr_ApplDate.Text;
                DateTime dtapp = DateTime.ParseExact(dateapplDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.APPL_DT = dtapp;
                //objBO_Finance.APPL_DT = Convert.ToDateTime(dtpkr_ApplDate.Text);
                objBO_Finance.LOAN_AMNT = Convert.ToDouble(ntxt_SancAmount.Text);
                //objBO_Finance.SUBSIDY = Convert.ToDouble(ntxt_SubsidyAmount.Text);
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
                //objBO_Finance.INST_APPL = Convert.ToBoolean(cmbx_IntsAppl.SelectedValue);
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
                //objBO_Finance.SANC_PER = "";
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
                //objBO_Finance.LOAN_TYPE = "";
                string lredt = dtpkr_RepayWithinDate.Text;
                DateTime lastrepdt = DateTime.ParseExact(lredt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.LAST_REP_DT = lastrepdt;
                //objBO_Finance.LAST_REP_DT = Convert.ToDateTime(dtpkr_RepayWithinDate.Text);
                //objBO_Finance.ASSES = "";
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
                int i = objBL_Finance.InsertUpdateDeleteLoanAccountOpening(objBO_Finance, out SQLError);


                if (i > 0)
                {
                    if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                    {
                        ResetControls();
                        message = "alert('Save Successfully.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";
                        Label1.Visible = true;
                        DivID.Visible = true;
                        Label1.Text = "Alloted CustId is:" + SQLError;

                        objBO_Finance.Flag = 1;

                        //Response.Redirect("frmLoanAccountOpening.aspx");
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

                        message = "alert('Something Wrong Input.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
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
                    //new DataColumn("PO_CODE"),
                    //new DataColumn("PS_CODE"),
                    //new DataColumn("BLK_CODE"),
                    new DataColumn("VILL_CODE"),
                    new DataColumn("DIS_CODE"),
                    //new DataColumn("SEX"),
                    //new DataColumn("REL_CODE"),
                    //new DataColumn("PROF_CODE")
                });
            return dt;
        }
        protected void ntxt_Applicant_TextChanged(object sender, EventArgs e)
        {
            //DataTable dt = ViewState["dtApplicantDetails"] != null ? ViewState["dtApplicantDetails"] as DataTable : dtApplicantDetails();
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
                //inst.SetField("PO_CODE", Convert.ToString(dt.Rows[0]["PO_Code"]));
                //inst.SetField("PS_CODE", Convert.ToString(dt.Rows[0]["PS_Code"]));
                //inst.SetField("BLK_CODE", Convert.ToString(dt.Rows[0]["BLK_Code"]));
                inst.SetField("VILL_CODE", Convert.ToString(dt.Rows[0]["Vill_Code"]));
                inst.SetField("DIS_CODE", Convert.ToString(dt.Rows[0]["Dist_Code"]));
                //inst.SetField("SEX", Convert.ToString(dt.Rows[0]["Sex"]));
                //inst.SetField("REL_CODE", Convert.ToString(dt.Rows[0]["Rel_Code"]));
                //inst.SetField("PROF_CODE", Convert.ToString(dt.Rows[0]["Prof_Code"]));
            }
            else
            {

                message = "Data Not Found";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

                var inst = dtApplicantDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                inst.SetField("CUST_ID", "");
                inst.SetField("Name", "");
                inst.SetField("GUARDIAN_NAME", "");
                //inst.SetField("PO_CODE", "");
                //inst.SetField("PS_CODE", "");
                //inst.SetField("BLK_CODE", "");
                inst.SetField("VILL_CODE", "");
                inst.SetField("DIS_CODE", "");
                //inst.SetField("SEX", "");
                //inst.SetField("REL_CODE", "");
                //inst.SetField("PROF_CODE", "");
            }
            dtApplicantDetails.AcceptChanges();
            gv_ClientDetails.DataSource = dtApplicantDetails;
            gv_ClientDetails.DataBind();
            ViewState["dtApplicantDetails"] = dtApplicantDetails;

        }

        protected void ntxt_LoanPeriod_TextChanged(object sender, EventArgs e)
        {
            string datejoinDt = dtpkr_ApplDate.Text;
            DateTime dtval = DateTime.ParseExact(datejoinDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            
            //DateTime dtval = DateTime.Parse(dtpkr_ApplDate.Text);
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
    }
}
