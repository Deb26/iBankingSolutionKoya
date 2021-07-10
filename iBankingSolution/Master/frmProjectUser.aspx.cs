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
    public partial class frmProjectUser : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                txtvalidtill.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BindBranch();
                BindEmployee();
                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetUserMasterData();

                }
               
            }
        }
        protected void BindEmployee()
        {
            objBO_Finance.Flag = 4;
            objBO_Finance.CUST_ID = null;

            DataTable dt = objBL_Finance.GetEmployeeMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                ddlUserName.DataSource = dt;
                ddlUserName.DataValueField = "EMPCODE";
                ddlUserName.DataTextField = "NAME";
                ddlUserName.DataBind();

            }

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
            objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
            objBO_Finance.UserCode = btnsubmit1.CommandArgument;
            objBO_Finance.UserCode = btnsubmit.CommandArgument;
            objBO_Finance.UserName = txtUserID.Text;
            objBO_Finance.UPassword = txtpassword.Text;
            objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;
            objBO_Finance.EMPCODE = ddlUserName.SelectedValue;
            objBO_Finance.IsAdmin = ddlUserLevel.SelectedValue;
            //objBO_Finance.DEPT = cmbx_Department.SelectedValue;
            objBO_Finance.DEPT = txt_Department.Text;
            objBO_Finance.DESIG = txtDesignation.Text;
            objBO_Finance.PHONE = txtPhone.Text;
            objBO_Finance.Email = txtEmail.Text;
            objBO_Finance.Auth = cmbx_Authorization.SelectedValue;
            objBO_Finance.EduCode = txtemp.Text;
            //objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

            string Cust = txtvalidtill.Text;
            DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.ValidTill = Cust1;

            int i = objBL_Finance.InsertUpdateDeleteUserMaster(objBO_Finance, out SQLError);

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
                    ViewState["LoanSchemeDetails"] = objBL_Finance.GetUserMasterRecords(objBO_Finance, out SQLError);

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
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            btnsubmit1.Text = "Save";
            txtUserID.Text = String.Empty;
            txtpassword.Text = String.Empty;
            cmbx_Branch.SelectedIndex = 0;
            ddlUserName.SelectedIndex = 0;
            ddlUserLevel.SelectedIndex = 0;
            //cmbx_Department.SelectedIndex = 0;
            txt_Department.Text = String.Empty;
            txtDesignation.Text = String.Empty;
            txtPhone.Text = String.Empty;
            txtEmail.Text = String.Empty;
        }
        protected void GetUserMasterData()
        {
            objBO_Finance.Flag = 3;
            objBO_Finance.UserId = lblDid.Text;
            DataTable dt = objBL_Finance.GetUserMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";
                btnsubmit1.CommandArgument = Convert.ToString(dt.Rows[0]["UserId"]);
                btnsubmit.CommandArgument = Convert.ToString(dt.Rows[0]["UserId"]);
                ddlUserName.SelectedValue = Convert.ToString(dt.Rows[0]["EMPCODE"]);
                //cmbx_Department.SelectedValue = Convert.ToString(dt.Rows[0]["DEPARTMENT"]);
                txt_Department.Text = Convert.ToString(dt.Rows[0]["DEPARTMENT"]);
                txtDesignation.Text = Convert.ToString(dt.Rows[0]["DESIGNATION"]);
                txtEmail.Text = Convert.ToString(dt.Rows[0]["EMAIL"]);
                txtPhone.Text = Convert.ToString(dt.Rows[0]["PHONE"]);
                txtaddress.Text = Convert.ToString(dt.Rows[0]["ADDRESS"]);

                txtUserID.Text = Convert.ToString(dt.Rows[0]["USERNAME"]);
                txtvalidtill.Text = Convert.ToString(dt.Rows[0]["Validtill"]);
                ddlUserLevel.SelectedValue = Convert.ToString(dt.Rows[0]["IsAdmin"]);
                cmbx_Branch.SelectedValue = Convert.ToString(dt.Rows[0]["BranchName"]);

            }
            else
            {
                message = "alert('Data Not Found.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
        }
        protected void BindBranch()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.CUST_ID = null;

            DataTable dt = objBL_Finance.GetBranchDetails(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_Branch.DataSource = dt;
                cmbx_Branch.DataValueField = "BranchID";
                cmbx_Branch.DataTextField = "BranchName";
                cmbx_Branch.DataBind();

            }
        }

        protected void cmbx_UserDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.NAME = ddlUserName.SelectedValue;
            DataTable dt = objBL_Finance.GetRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                txt_Department.Text = Convert.ToString(dt.Rows[0]["DEPARTMENT"]);
                txtDesignation.Text = Convert.ToString(dt.Rows[0]["DESIGNATION"]);
                //txtEmail.Text = Convert.ToString(dt.Rows[0]["EMAIL"]);
                txtPhone.Text = Convert.ToString(dt.Rows[0]["PHONE"]);
                txtaddress.Text = Convert.ToString(dt.Rows[0]["ADDRESS"]);
                txtemp.Text = Convert.ToString(dt.Rows[0]["EMPCODE"]);
            }
        }

        protected void cmbx_Auth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserLevel.SelectedValue == "Admin")
            {
                cmbx_Authorization.Enabled = true;
            }
            else
            {
                cmbx_Authorization.Enabled = false;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void itbnEdit_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void itbndelete_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void GVUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GVUser_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GVUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
    }
}