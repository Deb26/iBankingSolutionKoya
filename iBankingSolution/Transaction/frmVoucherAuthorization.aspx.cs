using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessObject;
using BLL;

namespace iBankingSolution.Transaction
{
    public partial class frmVoucherAuthorization : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        double CCBCode;
        decimal totdeb = 0;
        decimal totcre = 0;
        string previouDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                dtpkr_VoucherDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                userverified();
            }

        }

        protected void userverified()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.UserName = Convert.ToString(Session["UserID"]);
            DataTable dt1 = objBL_Finance.chequeUser(objBO_Finance, out SQLError);
            if (dt1.Rows.Count > 0)
            {
                
                if (Convert.ToString(dt1.Rows[0]["AUTH_FLAG"])  == "Yes")
                {
                    txtnull.Text = Convert.ToString(dt1.Rows[0]["EmpID"]);
                }
                else 
                {
                    Response.Redirect("~/Dashboard.aspx");
                }
            }
        }

        protected void cmbx_VoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            objBO_Finance.Flag = 1;
            string Cust = dtpkr_VoucherDate.Text;
            DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.TDate = Cust1;
            //objBO_Finance.VOUCHER_NO = cmbx_VoucherNo.SelectedValue;
            objBO_Finance.VoucherType = cmbx_VoucherType.SelectedItem.Text;
            dt = objBL_Finance.GETALLVOUCHER(objBO_Finance, out SQLError);
            //previouDate = dt.Rows[0]["VoucherDate"].ToString();
            //CCBCode = Convert.ToDouble(dt.Rows[0]["CCBCodeNew"]);
            //ViewState["PrevDt"] = previouDate;
            GVMTransaction.DataSource = dt;
            GVMTransaction.DataBind();
        }
        protected void GVMTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //first way
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    totdeb += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMT_DEBIT"));
            //    ViewState["totdeb"] = totdeb;
            //}
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[3].Text = totdeb.ToString();
            //}


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    totcre += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMT_CREDIT"));
            //    ViewState["totcre"] = totcre;
            //}
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[4].Text = totcre.ToString();
            //}


        }

        protected void click_btn_Update(object sender, EventArgs e)
        {
            if (cmbx_VoucherType.SelectedValue == "Cash")
            {
                int cnt = 0;
                int i = 0;
                foreach (var item in GVMTransaction.Rows)
                {
                    TextBox voucherNo = GVMTransaction.Rows[cnt].FindControl("txt_voucherNo") as TextBox;
                    TextBox ldgcode = GVMTransaction.Rows[cnt].FindControl("txt_Ldg_Code") as TextBox;
                    TextBox slcode = GVMTransaction.Rows[cnt].FindControl("txt_Sl_Code") as TextBox;
                    TextBox amtdebit = GVMTransaction.Rows[cnt].FindControl("txt_Amt_Debit") as TextBox;
                    TextBox amtcredit = GVMTransaction.Rows[cnt].FindControl("txt_Amt_Credit") as TextBox;
                    TextBox vdate = GVMTransaction.Rows[cnt].FindControl("txt_VoucherDate") as TextBox;
                    TextBox remarks = GVMTransaction.Rows[cnt].FindControl("txt_remarks") as TextBox; 
                    TextBox codenew = GVMTransaction.Rows[cnt].FindControl("txtccbcodenew") as TextBox; 

                    objBO_Finance.Flag = 1;
                    objBO_Finance.VOUCHER_NO = voucherNo.Text;
                    objBO_Finance.LDG_CODE = ldgcode.Text;
                    objBO_Finance.SL_CODE = slcode.Text;
                    objBO_Finance.AMT_DEBIT = Convert.ToDouble(amtdebit.Text);
                    objBO_Finance.AMT_CREDIT = Convert.ToDouble(amtcredit.Text);
                    objBO_Finance.REMARKS = remarks.Text;
                    objBO_Finance.COACode = codenew.Text;
                    objBO_Finance.EMPCODE = txtnull.Text;

                    string OpenDt = vdate.Text;
                    DateTime OpenDt1 = DateTime.ParseExact(OpenDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.date_of_opening = OpenDt1;

                    i = objBL_Finance.SocietyVoucherUpdate(objBO_Finance, out SQLError);
                    cnt = cnt + 1;
                }
                if (i > 0)
                {
                    String message = "alert('Authorization Successfully')";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                }
                else
                {
                    String message = "alert('Unable to Update')";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                }
            }
            else if (cmbx_VoucherType.SelectedValue == "Journal")
            {
                int cnt = 0;
                int i = 0;
                foreach (var item in GVMTransaction.Rows)
                {
                    TextBox voucherNo = GVMTransaction.Rows[cnt].FindControl("txt_voucherNo") as TextBox;
                    TextBox ldgcode = GVMTransaction.Rows[cnt].FindControl("txt_Ldg_Code") as TextBox;
                    TextBox slcode = GVMTransaction.Rows[cnt].FindControl("txt_Sl_Code") as TextBox;
                    TextBox amtdebit = GVMTransaction.Rows[cnt].FindControl("txt_Amt_Debit") as TextBox;
                    TextBox amtcredit = GVMTransaction.Rows[cnt].FindControl("txt_Amt_Credit") as TextBox;
                    TextBox vdate = GVMTransaction.Rows[cnt].FindControl("txt_VoucherDate") as TextBox;
                    TextBox remarks = GVMTransaction.Rows[cnt].FindControl("txt_remarks") as TextBox;
                    TextBox codenew = GVMTransaction.Rows[cnt].FindControl("txtccbcodenew") as TextBox;

                    objBO_Finance.Flag = 2;
                    objBO_Finance.VOUCHER_NO = voucherNo.Text;
                    objBO_Finance.LDG_CODE = ldgcode.Text;
                    objBO_Finance.SL_CODE = slcode.Text;
                    objBO_Finance.AMT_DEBIT = Convert.ToDouble(amtdebit.Text);
                    objBO_Finance.AMT_CREDIT = Convert.ToDouble(amtcredit.Text);
                    objBO_Finance.REMARKS = remarks.Text;
                    objBO_Finance.COACode = codenew.Text;
                    objBO_Finance.EMPCODE = txtnull.Text;

                    string OpenDt = vdate.Text;
                    DateTime OpenDt1 = DateTime.ParseExact(OpenDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.date_of_opening = OpenDt1;

                    i = objBL_Finance.SocietyVoucherUpdate(objBO_Finance, out SQLError);
                    cnt = cnt + 1;
                }
                if (i > 0)
                {
                    String message = "alert('Authorization Successfully')";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                }
                else
                {
                    String message = "alert('Unable to Update')";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                }
            }

        }

        protected void txt_Remers_TextChanged(object sender, EventArgs e)
        {
       
            btn_Update.Visible = true;
        }
    }
}