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
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Data;

namespace iBankingSolution.Transaction
{
    public partial class frmDeleteRepayment : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                btnDelete.Visible = false;
                lbldel.Visible = false;
            }
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {

            lbldel.Visible = true;
            btnDelete.Visible = true;
            string datedb = dtpkr_EntryDate.Text;
            DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime timedb =  DateTime.ParseExact(dtpkr_EntryDate.Text.Trim(), @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.CollectionDate = timedb;


            if (cmbx_EntryType.SelectedItem.Text == "Repayment")
            {
                objBO_Finance.Flag = 1;
                DataSet dSet = objBL_Finance.GetRepaymentDisbDetails(objBO_Finance, out SQLError);

                if (dSet.Tables[0].Rows.Count > 0)
                {
                    GVTransDetails.DataSource = dSet;
                    GVTransDetails.DataBind();
                    lblcnt.Text = " Total No of Records:" + GVTransDetails.Rows.Count;
                }
                else
                {
                    GVTransDetails.DataSource = null;
                    GVTransDetails.DataBind();
                    lblcnt.Text = "";
                }

            }
            else if (cmbx_EntryType.SelectedItem.Text == "Disbursement")
            {
                objBO_Finance.Flag = 2;
                DataSet dSet = objBL_Finance.GetRepaymentDisbDetails(objBO_Finance, out SQLError);
                if (dSet.Tables[0].Rows.Count > 0)
                {
                    GVTransDetails.DataSource = dSet;
                    GVTransDetails.DataBind();
                    lblcnt.Text = " Total No of Records:" + GVTransDetails.Rows.Count;
                }
                else
                {
                    GVTransDetails.DataSource = null;
                    GVTransDetails.DataBind();
                    lblcnt.Text = "";
                }

            }

            else
            {
                string message = "alert('Select Entry Type.')";

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (GridViewRow item in GVTransDetails.Rows)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                String InstrumentNo = "";
                
                if (chkSelect.Checked == true)
                {
                    if (cmbx_EntryType.SelectedItem.Text == "Repayment")
                    {
                        Label VoucherNo = (Label)item.FindControl("lblVoucherNo");
                        objBO_Finance.Flag = 1;
                        objBO_Finance.VOUCHER_NO = VoucherNo.Text;

                        DataTable dt= objBL_Finance.GetInstrumentNoLoanRepay(objBO_Finance, out SQLError);
                        if(dt.Rows.Count>0)
                        {

                            InstrumentNo = Convert.ToString(dt.Rows[0]["ENTRY_NO"]);
                        }
                        
                        objBO_Finance.Flag = 1;
                        objBO_Finance.VOUCHER_NO = VoucherNo.Text;
                        objBO_Finance.INS_NO = InstrumentNo;

                        i = objBL_Finance.DeleteLoanRepaymentDisbursement(objBO_Finance, out SQLError);

                    }


                    if (cmbx_EntryType.SelectedItem.Text == "Disbursement")
                    {
                        Label VoucherNo = (Label)item.FindControl("lblVoucherNo");
                        objBO_Finance.Flag = 2;
                        objBO_Finance.VOUCHER_NO = VoucherNo.Text;
                        

                        i = objBL_Finance.DeleteLoanRepaymentDisbursement(objBO_Finance, out SQLError);

                      }



                }

                //else
                //{
                //    string message = "alert('Select the Voucher to Delete.')";
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                

                //}

            }
            if (i > 0)
            {
                btnshow_Click(sender, e);
                string message = "alert('Deleted Successfully.')";

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                GVTransDetails.DataSource = null;
                GVTransDetails.DataBind();
                cmbx_EntryType.SelectedIndex = -1;
                dtpkr_EntryDate.Text = String.Empty;
                lblcnt.Text = "";
                btnDelete.Visible = false;
                lbldel.Visible = false;
            }

            else

            {
                string message = "alert('Unable to Delete.')";

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);


            }

        }

        protected void GVTransDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow item in GVTransDetails.Rows)
            {
                Label lbldisbamt = (Label)item.FindControl("lbldisbAmt");
                Label lblPrincipal = (Label)item.FindControl("lblPrincipal");
                Label lblInterest = (Label)item.FindControl("lblInterest");

                if (lbldisbamt.Text == "0")
                {
                    lbldisbamt.Text = "";

                }

                else if (lblPrincipal.Text == "0")
                {
                    lblPrincipal.Text = "";
                }
                else if (lblInterest.Text == "0")
                {
                    lblInterest.Text = "";
                }

            }


        }
    }
}