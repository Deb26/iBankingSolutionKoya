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
    public partial class frmLoanSchemeMaster : System.Web.UI.Page
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
                    GetLoanSchmeMasterEditData();

                }
                BindAdjustmentLedger();
            }
        }
        protected void GetLoanSchmeMasterEditData()
        {

            objBO_Finance.Flag = 2;
            objBO_Finance.SCHEME_CODE = lblDid.Text;
            DataTable dt = objBL_Finance.GetLoanSchemeMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";
                btnsubmit1.CommandArgument = Convert.ToString(dt.Rows[0]["SCHEME_CODE"]);
                btnsubmit.CommandArgument = Convert.ToString(dt.Rows[0]["SCHEME_CODE"]);
                txt_SchemeName.Text = Convert.ToString(dt.Rows[0]["SCHEME_NAME"]);
                cmbx_LoanType.SelectedValue = Convert.ToString(dt.Rows[0]["LOAN_TYPE"]);
                //cmbx_AdjustmentLedger.EnableAutomaticLoadOnDemand = false;
                cmbx_AdjustmentLedger.DataBind();
                //cmbx_IntReceivedLdg.EnableAutomaticLoadOnDemand = false;
                cmbx_IntReceivedLdg.DataBind();
                //cmbx_ODIntLdg.EnableAutomaticLoadOnDemand = false;
                cmbx_ODIntLdg.DataBind();
                //cmbx_ODIntReceivedLdg.EnableAutomaticLoadOnDemand = false;
                cmbx_ODIntReceivedLdg.DataBind();
                cmbx_AdjustmentLedger.SelectedValue = Convert.ToString(dt.Rows[0]["LDG_CODE"]);
                cmbx_NPAAplicable.SelectedValue = Convert.ToString(dt.Rows[0]["NPA_APP"]);
                cmbx_ODIntAplicable.SelectedValue = Convert.ToString(dt.Rows[0]["OD_APP"]);
                cmbx_SanctionBy.SelectedValue = Convert.ToString(dt.Rows[0]["SANC_PER"]);
                cmbx_IntReceivedLdg.SelectedValue = Convert.ToString(dt.Rows[0]["ODPRIN_LDG_CODE"]);
                cmbx_ODIntLdg.SelectedValue = Convert.ToString(dt.Rows[0]["ODINT_LDG_CODE"]);
                cmbx_ODIntReceivedLdg.SelectedValue = Convert.ToString(dt.Rows[0]["ODINTR_LDG_CODE"]);
            }
            else
            {
                message = "alert('Data Not Found.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
        }
        protected void BindAdjustmentLedger()
        {

            try
            {
                objBO_Finance.Flag = 9;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_AdjustmentLedger.DataSource = dt;
                    cmbx_AdjustmentLedger.DataValueField = "LDG_CODE";
                    cmbx_AdjustmentLedger.DataTextField = "NOMENCLATURE";
                    cmbx_AdjustmentLedger.DataBind();

                    cmbx_IntReceivedLdg.DataSource = dt;
                    cmbx_IntReceivedLdg.DataValueField = "LDG_CODE";
                    cmbx_IntReceivedLdg.DataTextField = "NOMENCLATURE";
                    cmbx_IntReceivedLdg.DataBind();

                    cmbx_ODIntLdg.DataSource = dt;
                    cmbx_ODIntLdg.DataValueField = "LDG_CODE";
                    cmbx_ODIntLdg.DataTextField = "NOMENCLATURE";
                    cmbx_ODIntLdg.DataBind();

                    cmbx_ODIntReceivedLdg.DataSource = dt;
                    cmbx_ODIntReceivedLdg.DataValueField = "LDG_CODE";
                    cmbx_ODIntReceivedLdg.DataTextField = "NOMENCLATURE";
                    cmbx_ODIntReceivedLdg.DataBind();


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
            txt_SchemeName.Text = String.Empty;
            cmbx_AdjustmentLedger.SelectedIndex = 0;
            cmbx_LoanType.SelectedIndex = 0;
            cmbx_IntReceivedLdg.SelectedIndex = 0;
            cmbx_ODIntReceivedLdg.SelectedIndex = 0;
            cmbx_ODIntLdg.SelectedIndex = 0;
            cmbx_NPAAplicable.SelectedIndex = 0; 
            cmbx_ODIntAplicable.SelectedIndex = 0;
            cmbx_SanctionBy.SelectedIndex = 0;

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
            objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
            objBO_Finance.SCHEME_CODE = btnsubmit1.CommandArgument;
            objBO_Finance.SCHEME_CODE = btnsubmit.CommandArgument;
            objBO_Finance.SCHEME_NAME = txt_SchemeName.Text;
            objBO_Finance.LDG_CODE = cmbx_AdjustmentLedger.SelectedValue;
            objBO_Finance.LOAN_TERM = "";
            objBO_Finance.INST_APPL = true;
            objBO_Finance.REPAY_MODE = "";
            objBO_Finance.MON_PRD = 0;
            objBO_Finance.SHR_AMNT = 0;
            objBO_Finance.SUB_AMNT = 0;
            objBO_Finance.ACT_CODE = 0;
            objBO_Finance.LOAN_TYPE = cmbx_LoanType.SelectedValue;
            objBO_Finance.ODINTR_LDG_CODE = cmbx_IntReceivedLdg.SelectedValue;
            objBO_Finance.ODPRIN_LDG_CODE = cmbx_ODIntReceivedLdg.SelectedValue;
            objBO_Finance.ODINT_LDG_CODE = cmbx_ODIntLdg.SelectedValue;
            objBO_Finance.NPA_APP = cmbx_NPAAplicable.SelectedValue;
            objBO_Finance.OD_APP = cmbx_ODIntAplicable.SelectedValue;
            objBO_Finance.SANC_APP = "BOARD";
            objBO_Finance.SANC_PER = cmbx_SanctionBy.SelectedValue;
            int i = objBL_Finance.InsertUpdateDeleteLoanSchemeMaster(objBO_Finance, out SQLError);

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
                    ViewState["LoanSchemeDetails"] = objBL_Finance.GetLoanSchemeMasterRecords(objBO_Finance, out SQLError);

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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        
        

        
         

        

         
    }
}