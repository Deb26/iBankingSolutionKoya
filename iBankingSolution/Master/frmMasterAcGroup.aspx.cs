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
    public partial class frmMasterAcGroup : System.Web.UI.Page
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
                    GetGroupAccountEditData();
                   
                }
                BindGroup();
            }

            
        }
        protected void BindGroup()
        {

            objBO_Finance.Flag = 3;
            objBO_Finance.CUST_ID = null;
            DataTable dt = objBL_Finance.GetGroupMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {

                ddlParentGrp.DataSource = dt;
                ddlParentGrp.DataValueField = "GROUP_CODE";
                ddlParentGrp.DataTextField = "GROUP_NAME";
                ddlParentGrp.DataBind();

            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
            objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
            objBO_Finance.GROUP_CODE = btnsubmit.CommandArgument;
            objBO_Finance.GROUP_CODE = btnsubmit1.CommandArgument;
            objBO_Finance.GROUP_NAME = txtGroupName.Text;
            objBO_Finance.GROUP_TYPE = rdobtn_GroupType.SelectedValue;
            if (rdobtn_GroupType.SelectedValue.ToLower() == "m")
            {
                objBO_Finance.LINK = DBNull.Value;
            }
            else
            {
                objBO_Finance.LINK = ddlParentGrp.SelectedValue;
            }
            objBO_Finance.FA_TYPE = ddlPrimaryFA.SelectedValue;
            objBO_Finance.FA_TYPE2 = cmbx_SecondaryFAType.SelectedValue;
            objBO_Finance.NESTING_LEVEL = Convert.ToInt32("0");
            objBO_Finance.GINDEX = Convert.ToDouble(ntxt_Index.Text);
            objBO_Finance.D = chkbx_Depriciation.Checked;
            objBO_Finance.A = chkbx_Appropiation.Checked;
            objBO_Finance.P = chkbx_Provisioning.Checked;
            //objBO_Finance.OTHERS = chkbx_Other.Checked;
            objBO_Finance.AINDEX = Convert.ToInt32("0");
            objBO_Finance.EMPCODE = "0";
            objBO_Finance.TERMINAL_ID = "0";
            //objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            int i = objBL_Finance.InsertUpdateDeleteGroupMaster(objBO_Finance, out SQLError);
            if (i > 0)
            {
                if (btnsubmit.Text == "Save")
                {
                    ResetControls();
                    message = "alert('Save Successfully.')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

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
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            chkbx_Depriciation.Checked = false;
            chkbx_Appropiation.Checked = false;
            chkbx_Provisioning.Checked = false;
            //chkbx_Other.Checked = false;
            ntxt_Index.Text = String.Empty;
            ddlParentGrp.SelectedIndex = 0;
            ddlPrimaryFA.SelectedIndex = 0;
            cmbx_SecondaryFAType.SelectedIndex = 0;
            txtGroupName.Text = String.Empty;
            rdobtn_GroupType.SelectedIndex = 0;

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void rdobtn_GroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdobtn_GroupType.SelectedValue == "s")
            {
                ddlParentGrp.Enabled = true;
            }
            else
            {
                ddlParentGrp.ClearSelection();
                ddlParentGrp.Enabled = false;
            }
        }
        protected void GetGroupAccountEditData()
        {
            objBO_Finance.Flag = 2;
            objBO_Finance.GROUP_CODE = lblDid.Text;

            DataTable dt = objBL_Finance.GetGroupMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";
                btnsubmit.CommandArgument = Convert.ToString(dt.Rows[0]["GROUP_CODE"]);
                btnsubmit1.CommandArgument = Convert.ToString(dt.Rows[0]["GROUP_CODE"]);
                txtGroupName.Text = Convert.ToString(dt.Rows[0]["GROUP_NAME"]);
                rdobtn_GroupType.SelectedValue = Convert.ToString(dt.Rows[0]["GROUP_TYPE"]);
                if (rdobtn_GroupType.SelectedValue == "s")
                {

                    ddlParentGrp.SelectedValue = Convert.ToString(dt.Rows[0]["LINK"]);
                    ddlParentGrp.Enabled = true;
                }
                else if (rdobtn_GroupType.SelectedValue == "m")
                {

                    ddlParentGrp.ClearSelection();
                    ddlParentGrp.Enabled = false;
                }
                ddlPrimaryFA.SelectedValue = Convert.ToString(dt.Rows[0]["FA_TYPE"]);
                cmbx_SecondaryFAType.SelectedValue = Convert.ToString(dt.Rows[0]["FA_TYPE2"]);

                Convert.ToString(dt.Rows[0]["NESTING_LEVEL"]);
                ntxt_Index.Text = Convert.ToString(Convert.ToDouble(dt.Rows[0]["GINDEX"]));
                chkbx_Depriciation.Checked = Convert.ToBoolean(dt.Rows[0]["D"]);
                chkbx_Appropiation.Checked = Convert.ToBoolean(dt.Rows[0]["A"]);
                chkbx_Provisioning.Checked = Convert.ToBoolean(dt.Rows[0]["P"]);
                //chkbx_Other.Checked = Convert.ToBoolean(dt.Rows[0]["OTHERS"]);
            }
            else
            {
                message = "alert('Data Not Found.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);



            }

        }
        protected void ddlPrimaryFA_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Code");
            if (ddlPrimaryFA.SelectedItem.Text == "Balance Sheet")
            {
                dt.Clear();
                dt.Rows.Add("Assets");
                dt.Rows.Add("Liabilities");
                cmbx_SecondaryFAType.DataTextField = "Code";
                cmbx_SecondaryFAType.DataValueField = "Code";
                cmbx_SecondaryFAType.DataSource = dt;
                cmbx_SecondaryFAType.DataBind();
            }
            if (ddlPrimaryFA.SelectedItem.Text == "Profit & Loss A/c")
            {
                dt.Clear();
                dt.Rows.Add("Income");
                dt.Rows.Add("Expenditure");
                cmbx_SecondaryFAType.DataTextField = "Code";
                cmbx_SecondaryFAType.DataValueField = "Code";
                cmbx_SecondaryFAType.DataSource = dt;
                cmbx_SecondaryFAType.DataBind();
            }
            if (ddlPrimaryFA.SelectedItem.Text == "Trading A/c")
            {
                dt.Clear();
                dt.Rows.Add("Sale");
                dt.Rows.Add("Purchase");
                cmbx_SecondaryFAType.DataTextField = "Code";
                cmbx_SecondaryFAType.DataValueField = "Code";
                cmbx_SecondaryFAType.DataSource = dt;
                cmbx_SecondaryFAType.DataBind();
            }
        }

        protected void cmbx_SecondaryFAType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}