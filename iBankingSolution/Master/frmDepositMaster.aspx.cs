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

namespace iBankingSolution.Master
{
    public partial class frmDepositMaster : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetDepositMasterEditData();

                }
                BindLedger();
            }
        }

        protected void BindLedger()
        {
            try
            {
                objBO_Finance.Flag = 12;
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
        protected void GetDepositMasterEditData()
        {
            try
            {
                objBO_Finance.Flag = 2;
                objBO_Finance.DM_CODE = lblDid.Text;
                DataTable dt = objBL_Finance.GetDepositMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    btnsubmit1.Text = "Update";
                    btnsubmit1.CommandArgument = Convert.ToString(dt.Rows[0]["DM_CODE"]);
                    btnsubmit.CommandArgument = Convert.ToString(dt.Rows[0]["DM_CODE"]);

                    txt_SchemeName.Text = Convert.ToString(dt.Rows[0]["SCHEME"]);
                    cmbx_InterestType.SelectedValue = Convert.ToString(dt.Rows[0]["INT_TYPE"]);
                    cmbx_InterestCalcFrequency.SelectedValue = Convert.ToString(dt.Rows[0]["COMP_PRD"]);
                    //cmbx_Ledger.EnableAutomaticLoadOnDemand = false;
                    //cmbx_Ledger.DataBind();
                    cmbx_Ledger.SelectedValue = Convert.ToString(dt.Rows[0]["LDG_CODE"]);
                    //cmbx_Ledger.EnableAutomaticLoadOnDemand = true;
                    dtpkr_NextIntCalcDate.Text = Convert.ToDateTime(dt.Rows[0]["NEXT_INTEREST_CALCULATION_DATE"]).ToString("dd/MM/yyyy");
                    cmbx_IntCalcBasedOn.SelectedValue = Convert.ToString(dt.Rows[0]["INT_CAL_DATE_STYLE"]);
                    cmbx_SchemeType.SelectedValue = Convert.ToString(dt.Rows[0]["SCHEME_TYPE"]);
                    ntxt_MinBalance.Text = Convert.ToString(dt.Rows[0]["MINBAL_CASH"]) == "" ? "0" : Convert.ToString(dt.Rows[0]["MINBAL_CASH"]);
                    ntxt_MaxWithdraw.Text = Convert.ToString(dt.Rows[0]["MAX_TRAN_PER_MONTH"] == DBNull.Value ? "0" : dt.Rows[0]["MAX_TRAN_PER_MONTH"]);
                    ntxt_InoperativeAfterMonth.Text = Convert.ToString(dt.Rows[0]["INOPERATED_MONTH"] == DBNull.Value ? "0" : dt.Rows[0]["INOPERATED_MONTH"]);
                    ntxt_InoperativeAfterYear.Text = Convert.ToString(dt.Rows[0]["INOPERATED_YR"] == DBNull.Value ? "0" : dt.Rows[0]["INOPERATED_YR"]);
                    ntxt_UnclaimedAfterMonth.Text = Convert.ToString(dt.Rows[0]["UNCLAIMED_MONTH"] == DBNull.Value ? "0" : dt.Rows[0]["UNCLAIMED_MONTH"]);
                    ntxt_UnclaimedAfterYear.Text = Convert.ToString(dt.Rows[0]["UNCLAIMED_YR"] == DBNull.Value ? "0" : dt.Rows[0]["UNCLAIMED_YR"]);
                    ntxt_MinCashDeposit.Text = Convert.ToString(dt.Rows[0]["MIN_CASH_DEP"] == DBNull.Value ? "0" : dt.Rows[0]["MIN_CASH_DEP"]);
                    ntxt_MinIntCalcAmount.Text = Convert.ToString(dt.Rows[0]["INT_AMNT"] == DBNull.Value ? "0" : dt.Rows[0]["INT_AMNT"]);
                }
                else
                {
                    message = "alert('Data Not Found.')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }
            }
            catch (Exception ex)
            {
                String msg = ex.Message;
            }
            finally
            {


            }
        }
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            btnsubmit1.Text = "Save";
            txt_SchemeName.Text = String.Empty;
            cmbx_InterestType.SelectedIndex = 0;
            cmbx_InterestCalcFrequency.SelectedIndex = 0;
            cmbx_Ledger.SelectedIndex = 0;
            dtpkr_NextIntCalcDate.Text = String.Empty;
            cmbx_IntCalcBasedOn.SelectedIndex = 0;
            cmbx_SchemeType.SelectedIndex = 0;
            ntxt_MinBalance.Text = String.Empty;
            ntxt_MaxWithdraw.Text = String.Empty;
            ntxt_InoperativeAfterMonth.Text = String.Empty;
            ntxt_InoperativeAfterYear.Text = String.Empty;
            ntxt_UnclaimedAfterMonth.Text = String.Empty;
            ntxt_UnclaimedAfterYear.Text = String.Empty;
            ntxt_MinCashDeposit.Text = String.Empty;
            chkbx_IsChequeFacility.Checked = false;
            ntxt_MinIntCalcAmount.Text = String.Empty;

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            try
            {
                objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                objBO_Finance.DM_CODE = btnsubmit1.CommandArgument;
                objBO_Finance.DM_CODE = btnsubmit.CommandArgument;
                objBO_Finance.SCHEME = txt_SchemeName.Text;
                objBO_Finance.INT_TYPE = cmbx_InterestType.SelectedValue;
                objBO_Finance.COMP_PRD = cmbx_InterestCalcFrequency.SelectedValue;
                objBO_Finance.LDG_CODE = cmbx_Ledger.SelectedValue;
                objBO_Finance.LAST_INTEREST_CALCULATION_DATE = DateTime.Now;


                string INTERESTDT = dtpkr_NextIntCalcDate.Text;
                DateTime INTERESTDT1 = DateTime.ParseExact(INTERESTDT, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.NEXT_INTEREST_CALCULATION_DATE = INTERESTDT1;
                 
                objBO_Finance.INT_CAL_DATE_STYLE = cmbx_IntCalcBasedOn.SelectedValue;
                objBO_Finance.SCHEME_TYPE = cmbx_SchemeType.SelectedValue;
                objBO_Finance.MINBAL_CASH = Convert.ToDouble(ntxt_MinBalance.Text == "" ? "0" : ntxt_MinBalance.Text);
                objBO_Finance.MINBAL_CHQ = DBNull.Value;
                objBO_Finance.MAX_TRAN_PER_MONTH = Convert.ToDouble(ntxt_MaxWithdraw.Text == "" ? "0" : ntxt_MaxWithdraw.Text);
                objBO_Finance.CHARGES_MINBAL_FALL = DBNull.Value;
                objBO_Finance.MIN_TRANPER_MONTH = DBNull.Value;
                objBO_Finance.INOPERATED_DAYS = DBNull.Value;
                objBO_Finance.INOPERATED_MONTH = Convert.ToDouble(ntxt_InoperativeAfterMonth.Text == "" ? "0" : ntxt_InoperativeAfterMonth.Text);
                objBO_Finance.INOPERATED_YR = Convert.ToDouble(ntxt_InoperativeAfterYear.Text == "" ? "0" : ntxt_InoperativeAfterYear.Text);
                objBO_Finance.UNCLAIMED_DAYS = DBNull.Value;
                objBO_Finance.UNCLAIMED_MONTH = Convert.ToDouble(ntxt_UnclaimedAfterMonth.Text == "" ? "0" : ntxt_UnclaimedAfterMonth.Text);
                objBO_Finance.UNCLAIMED_YR = Convert.ToDouble(ntxt_UnclaimedAfterYear.Text == "" ? "0" : ntxt_UnclaimedAfterYear.Text);
                objBO_Finance.AC_OPEN_AMNT = DBNull.Value;
                objBO_Finance.AC_CLOS_CHRG = DBNull.Value;
                objBO_Finance.MIN_CASH_DEP = Convert.ToDouble(ntxt_MinCashDeposit.Text == "" ? "0" : ntxt_MinCashDeposit.Text);
                objBO_Finance.EMPCODE = "10000";
                objBO_Finance.TERMINAL_ID = "";
                objBO_Finance.CHEQUE_FACILITY = chkbx_IsChequeFacility.Checked;
                objBO_Finance.INT_AMNT = Convert.ToDouble(ntxt_MinIntCalcAmount.Text == "" ? "0" : ntxt_MinIntCalcAmount.Text);
                objBO_Finance.MIN_DAY = DBNull.Value; ;
                objBO_Finance.MAX_WITH = DBNull.Value;
                objBO_Finance.MAX_WITH_MON = DBNull.Value;
                objBO_Finance.MINDAYTR = DBNull.Value;
                int i = objBL_Finance.InsertUpdateDeleteDepositSchemeMaster(objBO_Finance, out SQLError);


                if (i > 0)
                {
                    if (btnsubmit.Text == "Save")
                    {
                        ResetControls();
                        message = "alert('Save Successfully.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";
                        objBO_Finance.Flag = 1;
                        ViewState["DepositSchemeDetails"] = objBL_Finance.GetDepositMasterRecords(objBO_Finance, out SQLError);

                    }
                    if (btnsubmit.Text == "Update")
                    {
                        message = "alert('Update Successfully.')";

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        btnsubmit.Text = "Save";
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
            { }
            finally
            { }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

    }
}