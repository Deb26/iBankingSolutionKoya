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
    public partial class frmSubledgerMaster : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            txt_SubLedgerName.Focus();
            if (!IsPostBack)
            {

                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetSubLedgerEditData();

                }
                BindGroup();
            }
        }
        protected void BindGroup()
        {
            try
            {
                objBO_Finance.Flag = 4;
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
        protected void GetSubLedgerEditData()
        {
            try
            {

                objBO_Finance.Flag = 4;
                objBO_Finance.SL_CODE = lblDid.Text;
                DataTable dt = objBL_Finance.GetSubLedgerMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    btnsubmit1.Text = "Update";

                    btnsubmit1.CommandArgument = Convert.ToString(dt.Rows[0]["sl_code"]);
                    btnsubmit.CommandArgument = Convert.ToString(dt.Rows[0]["sl_code"]);

                    cmbx_Ledger.SelectedValue = Convert.ToString(dt.Rows[0]["LDG_CODE"]);
                    txt_SubLedgerName.Text = Convert.ToString(dt.Rows[0]["sl_name"]);
                    txt_MemsID.Text = Convert.ToString(dt.Rows[0]["MEM_ID"]);
                    txt_GSTINNo.Text = Convert.ToString(dt.Rows[0]["GSTINNO"]);
                    txt_Address.Text = Convert.ToString(dt.Rows[0]["Address"]);
                    ntxt_Debit.Text = Convert.ToString(Convert.ToDouble(dt.Rows[0]["ACT_OP_DR"]) != 0 ? dt.Rows[0]["ACT_OP_DR"] : 0);
                    ntxt_Credit.Text = Convert.ToString(Convert.ToDouble(dt.Rows[0]["ACT_OP_CR"]) != 0 ? dt.Rows[0]["ACT_OP_CR"] : 0);
                    ntxt_Debit.Enabled = false;
                    ntxt_Credit.Enabled = false;

                }
                else
                {
                    message = "alert('Data Not Found.')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
            }

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                objBO_Finance.SL_CODE = btnsubmit.CommandArgument;
                objBO_Finance.SL_CODE = btnsubmit1.CommandArgument;
                objBO_Finance.actype = "g";
                objBO_Finance.ac_status = "Live";
                objBO_Finance.date_of_opening = DateTime.Now;
                objBO_Finance.last_tran_date = DateTime.Now;

                objBO_Finance.sl_Name = txt_SubLedgerName.Text;
                objBO_Finance.LDG_CODE = cmbx_Ledger.SelectedValue;
                objBO_Finance.MemNo = txt_MemsID.Text;
                objBO_Finance.GSTINNo = txt_GSTINNo.Text;
                objBO_Finance.Address1 = txt_Address.Text;
                if (!string.IsNullOrEmpty(ntxt_Credit.Text)) 
                    objBO_Finance.ACT_OP_CR = Convert.ToDouble(ntxt_Credit.Text);
                else
                objBO_Finance.ACT_OP_CR = 0;
                if (!string.IsNullOrEmpty(ntxt_Debit.Text))
                    objBO_Finance.ACT_OP_DR = Convert.ToDouble(ntxt_Debit.Text);
                else
                    objBO_Finance.ACT_OP_DR = 0;
                


                if (btnsubmit.Text == "Save")
                {

                    int i = objBL_Finance.InsertSubLedgerMaster(objBO_Finance, out SQLError);
                    if (i > 0)
                    {
                        ResetControls();
                        message = "alert('Save Successfully.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

                        objBO_Finance.Flag = 2;
                        ViewState["SubLedgerDetails"] = objBL_Finance.GetSubLedgerMasterRecords(objBO_Finance, out SQLError);
                    }
                    else
                    {

                        message = "alert('Something Wrong Input.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);


                    }
                }

                    
                if (btnsubmit.Text == "Update")
                    {
                    int i = objBL_Finance.InsertUpdateDeleteSubLedgerMaster(objBO_Finance, out SQLError);
                    if (i > 0)
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
                string msg = ex.Message;
            }
            finally
            {

            }

             
        }
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            ntxt_Credit.Text = String.Empty;
            ntxt_Debit.Text = String.Empty;
            txt_SubLedgerName.Text = String.Empty;
            cmbx_Ledger.SelectedIndex = 0;
            txt_MemsID.Text = String.Empty;
            txt_GSTINNo.Text = String.Empty;
            txt_Address.Text = String.Empty;
            ntxt_Credit.Enabled = true;
            ntxt_Debit.Enabled = true;


        }
         

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControls();
        }
    }
}