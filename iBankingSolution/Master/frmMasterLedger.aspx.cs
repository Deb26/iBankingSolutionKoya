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
using System.Data.SqlClient;
using System.Configuration;

namespace iBankingSolution.Master
{
    public partial class frmMasterLedger : System.Web.UI.Page
    {
        String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            txt_LedgerName.Focus();
            if (!IsPostBack)
            {

                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetLedgerEditData();

                }
                BindGroup();
                InterestIssuedReceived();
                InterestPayableReceivable();
                //InterestIssued();
                // InterestPayble();
            }
        }
        protected void GetLedgerEditData()
        {
            objBO_Finance.Flag = 2;
            objBO_Finance.LDG_CODE = lblDid.Text;
            DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                txt_LedgerCode.Enabled = false;
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";
                btnsubmit.CommandArgument = Convert.ToString(dt.Rows[0]["LDG_CODE"]);
                btnsubmit1.CommandArgument = Convert.ToString(dt.Rows[0]["LDG_CODE"]);
                txt_LedgerCode.Text = Convert.ToString(dt.Rows[0]["LDG_CODE"]);
                txt_LedgerName.Text = Convert.ToString(dt.Rows[0]["NOMENCLATURE"]);
                cmbx_Group.SelectedValue = Convert.ToString(dt.Rows[0]["GROUPCODE"]);
                chkbx_IsSubledger.Checked = Convert.ToBoolean(dt.Rows[0]["SL_FLAG"] != DBNull.Value ? dt.Rows[0]["SL_FLAG"] : 0);
                chkbx_IsSubledger.Checked = Convert.ToBoolean(dt.Rows[0]["COST_FLAG"] != DBNull.Value ? dt.Rows[0]["COST_FLAG"] : 0);
                cmbx_Type.SelectedValue = Convert.ToString(dt.Rows[0]["TYPE"]);
                rdobtn_LedgerType.SelectedValue = Convert.ToString(dt.Rows[0]["CASH_BANK"]);
                //cmbx_InterestIssued.Text = Convert.ToString(Convert.ToDouble(dt.Rows[0]["INT_PAID"] != DBNull.Value ? dt.Rows[0]["INT_PAID"] : 0));
                cmbx_InterestIssued.SelectedValue = Convert.ToString(Convert.ToDouble(dt.Rows[0]["INT_PAID"] != DBNull.Value ? dt.Rows[0]["INT_PAID"] : 0));
                ntxt_Debit.Text = Convert.ToString(Convert.ToDouble(dt.Rows[0]["ACT_OP_DR"]) != 0 ? dt.Rows[0]["ACT_OP_DR"] : 0);
                ntxt_Credit.Text = Convert.ToString(Convert.ToDouble(dt.Rows[0]["ACT_OP_CR"]) != 0 ? dt.Rows[0]["ACT_OP_CR"] : 0);
                cmbx_InterestPayble.Text = Convert.ToString(Convert.ToDouble(dt.Rows[0]["INT_PAYABLE"]) !=0 ? dt.Rows[0]["INT_PAYABLE"] : 0);
                //txt_AddWith.Text = Convert.ToString(dt.Rows[0]["ADD_WITH"]);
            }
            else
            {
                message = "alert('Data Not Found.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

                 
            }

        }
        protected void BindGroup()
        {
            try
            {
                objBO_Finance.Flag = 3;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetGroupMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_Group.DataSource = dt;
                    cmbx_Group.DataValueField = "GROUP_CODE";
                    cmbx_Group.DataTextField = "GROUP_NAME";
                    cmbx_Group.DataBind();

                }
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void InterestIssuedReceived()
        {
            try
            {
                objBO_Finance.Flag = 16;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetLEDGERMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_InterestIssued.DataSource = dt;
                    cmbx_InterestIssued.DataValueField = "LDG_CODE";
                    cmbx_InterestIssued.DataTextField = "NOMENCLATURE";
                    cmbx_InterestIssued.DataBind();
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void InterestPayableReceivable()
        {
            try
            {
                objBO_Finance.Flag = 17;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetInterestPaybleRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_InterestPayble.DataSource = dt;
                    cmbx_InterestPayble.DataValueField = "LDG_CODE";
                    cmbx_InterestPayble.DataTextField = "NOMENCLATURE";
                    cmbx_InterestPayble.DataBind();
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }
        //protected void InterestIssued()
        //{
        //    try
        //    {
                
        //        DataTable dt = objBL_Finance.GetnterestIssued(objBO_Finance, out SQLError);
        //        if (dt.Rows.Count > 0)
        //        {
        //            cmbx_InterestIssued.DataSource = dt;
        //            cmbx_InterestIssued.DataValueField = "ldg_code";
        //            cmbx_InterestIssued.DataTextField = "Nomenclature";
        //            cmbx_InterestIssued.DataBind();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //    }
        //}
        //protected void InterestPayble()
        //{
        //    try
        //    {
                
                 
        //        DataTable dt = objBL_Finance.GetnterestReceived(objBO_Finance, out SQLError);
        //        if (dt.Rows.Count > 0)
        //        {
        //            cmbx_InterestPayble.DataSource = dt;
        //            cmbx_InterestPayble.DataValueField = "ldg_code";
        //            cmbx_InterestPayble.DataTextField = "Nomenclature";
        //            cmbx_InterestPayble.DataBind();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //    }
        //}
        protected void ResetControls()
        {
            txt_LedgerName.Text = String.Empty;
            //txt_AddWith.Text = String.Empty;
            txt_LedgerCode.Text = String.Empty;
            cmbx_Group.SelectedIndex = 0;
            chkbx_IsSubledger.Checked = false;
            chkbx_IsSubledger.Checked = false;
            cmbx_Type.SelectedIndex = 0;
            
            rdobtn_LedgerType.SelectedIndex = 0;
            cmbx_InterestIssued.SelectedIndex = 0;
            cmbx_InterestPayble.SelectedIndex = 0;
            //cmbx_InterestIssued.SelectedValue = -1;
            ntxt_Debit.Text = String.Empty;
            ntxt_Credit.Text = String.Empty;
            //cmbx_InterestPayble.SelectedValue = ;
            //txt_AddWith.Text = String.Empty;

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                
                objBO_Finance.LDG_CODE = txt_LedgerCode.Text;
                objBO_Finance.NOMENCLATURE = txt_LedgerName.Text;
                objBO_Finance.GROUP_CODE = cmbx_Group.SelectedValue;
                objBO_Finance.SL_FLAG = chkbx_IsSubledger.Checked;
                objBO_Finance.COST_FLAG = chkbx_IsSubledger.Checked;
                objBO_Finance.LINDEX = 0;
                objBO_Finance.TYPE = cmbx_Type.SelectedValue;
                objBO_Finance.CASH_BANK = rdobtn_LedgerType.SelectedValue;
                objBO_Finance.INT_PAID = Convert.ToString(cmbx_InterestIssued.Text) != "" ? Convert.ToDouble(cmbx_InterestIssued.Text) : 0;  
                objBO_Finance.ACT_OP_DR = Convert.ToString(ntxt_Debit.Text) != "" ? Convert.ToDouble(ntxt_Debit.Text) : 0;
                objBO_Finance.ACT_OP_CR = Convert.ToString(ntxt_Credit.Text) != "" ? Convert.ToDouble(ntxt_Credit.Text) : 0;
                objBO_Finance.INT_PAYBLE = Convert.ToString(cmbx_InterestPayble.Text) != "" ? Convert.ToDouble(cmbx_InterestPayble.Text) : 0;
                objBO_Finance.RBI_CODE = "";
                //objBO_Finance.ADD_WITH = txt_AddWith.Text;
                objBO_Finance.EMPCODE = "";
                objBO_Finance.TERMINAL_ID = "";
                int i = objBL_Finance.InsertUpdateDeleteLedgerMaster(objBO_Finance, out SQLError);

                if (i > 0)
                {
                    if (btnsubmit.Text == "Save")
                    {
                        ResetControls();
                        message = "alert('Save Successfully.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        txt_LedgerCode.Enabled = true;
                        objBO_Finance.Flag = 1;
                        ViewState["LedgerDetails"] = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);

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
            {
                string msg = ex.Message;
            }
            finally
            {

            }
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void txt_LedgerCode_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT LDG_CODE FROM LEDGER_MASTER WHERE LDG_CODE ='" + txt_LedgerCode.Text + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                message = "alert('Ledger Code is Exists.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                ResetControls();
            }
            
        }
    }
}