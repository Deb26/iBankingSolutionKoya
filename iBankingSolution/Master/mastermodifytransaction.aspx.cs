using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BusinessObject;
using BLL;
using System.Collections;
using System.Globalization;
using BLL.GeneralBL;
using System.Data;
using System.Data.SqlClient;

namespace iBankingSolution.Master
{
    public partial class mastermodifytransaction : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        double CCBCode;
        decimal totdeb = 0;
        decimal totcre = 0;
        string previouDate;
        double DebitAmount;

        public static SqlConnection sqlConn()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            return new SqlConnection(cs);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                dtpkrr_VoucherDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

        }

        protected void click_btn_delete(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 2;
            objBO_Finance.VOUCHER_NO = cmbxx_VoucherNo.SelectedValue;
            objBO_Finance.VoucherType = cmbxx_VoucherType.SelectedValue;
            int i = objBL_Finance.ModifyTransaction(objBO_Finance, out SQLError);
            if (i > 0)
            {
                string message = "Successfully Deleted";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                string message = "Something Wrong. Error Details";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }

        protected void GVMTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbxx_VoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            objBO_Finance.Flag = 2;
            //objBO_Finance.CCB_DATE = Convert.ToDateTime(dtpkr_VoucherDate.Text);

            string dtp = dtpkrr_VoucherDate.Text;
            DateTime Cust1 = DateTime.ParseExact(dtp, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.CCB_DATE = Cust1;


            objBO_Finance.VoucherType = cmbxx_VoucherType.SelectedItem.Text;
            ds = objBL_Finance.getVoucherNo(objBO_Finance, out SQLError);

            cmbxx_VoucherNo.DataSource = ds;
            cmbxx_VoucherNo.DataTextField = "VOUCHER_NO";
            cmbxx_VoucherNo.DataValueField = "VOUCHER_NO";
            cmbxx_VoucherNo.Items.Insert(0, "--Select Voucher--");
            cmbxx_VoucherNo.DataBind();
        }

        protected void cmbx_VoucherNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            objBO_Finance.Flag = 1;
            objBO_Finance.VOUCHER_NO = cmbxx_VoucherNo.SelectedValue;
            objBO_Finance.VoucherType = cmbxx_VoucherType.SelectedItem.Text;
            dt = objBL_Finance.GetAllTransDetails(objBO_Finance, out SQLError);
            previouDate = dt.Rows[0]["VoucherDate"].ToString();
            CCBCode = Convert.ToDouble(dt.Rows[0]["CCBCodeNew"]);
            ViewState["PrevDt"] = previouDate;
            GVMTTransaction.DataSource = dt;
            GVMTTransaction.DataBind();
        }

        protected void GVMTransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            GVMTTransaction.PageIndex = e.NewPageIndex;
            GVMTTransaction.DataSource = dt;
            GVMTTransaction.DataBind();
        }

        protected void GVMTransaction_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void GVMTransaction_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DataTable dt = new DataTable();
            GVMTTransaction.EditIndex = -1;
            GVMTTransaction.DataBind();
        }

        protected void GVMTransaction_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = new DataTable();
            objBO_Finance.Flag = 1;
            Label id = GVMTTransaction.Rows[e.RowIndex].FindControl("VoucherCode") as Label;
            TextBox DEBIT = GVMTTransaction.Rows[e.RowIndex].FindControl("txt_totdeb") as TextBox;
            TextBox CREDIT = GVMTTransaction.Rows[e.RowIndex].FindControl("txt_totcre") as TextBox;

            objBO_Finance.VoucherType = cmbxx_VoucherType.SelectedItem.Text;
            objBO_Finance.VOUCHER_NO = id.ToString();

            int i = objBL_Finance.ModifyTransaction(objBO_Finance, out SQLError);
            GVMTTransaction.EditIndex = -1;
            GVMTTransaction.DataSource = dt;
            GVMTTransaction.DataBind();
        }

        protected void GVMTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //first way
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                totdeb += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMT_DEBIT"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = totdeb.ToString();
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totcre += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMT_CREDIT"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = totcre.ToString();
            }
        }

        protected void GVMTransaction_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DataTable dt = new DataTable();
            objBO_Finance.Flag = 1;

            int i = objBL_Finance.ModifyTransaction(objBO_Finance, out SQLError);
            if (i > 0)
            { }
            GVMTTransaction.EditIndex = e.NewEditIndex;
            GVMTTransaction.DataSource = dt;
            GVMTTransaction.DataBind();
        }



        protected void txtsavecomm_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            
            foreach (var item in GVMTTransaction.Rows)
            {

                TextBox txt_VoucherDate = GVMTTransaction.Rows[cnt].FindControl("txt_VoucherCode") as TextBox;
                Int32 key = Convert.ToInt32(GVMTTransaction.DataKeys[cnt].Value.ToString());

                objBO_Finance.Flag = 1;
                objBO_Finance.VoucherType = cmbxx_VoucherType.SelectedItem.Text;
                objBO_Finance.VOUCHER_NO = cmbxx_VoucherNo.SelectedItem.Text;
                objBO_Finance.Comments = Convert.ToString(txtcoment.Text);
                if (txtcoment.Text != "")
                {
                    objBO_Finance.SL_FLAG = true;
                }
                else
                {
                    objBO_Finance.SL_FLAG = false;
                }
                string datedb = txt_VoucherDate.Text;
                DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.VoucherDate = timedb;



                objBO_Finance.CCBCode = key;

                int i = objBL_Finance.ModifyTransactionComments(objBO_Finance, out SQLError);
                if (i > 0)
                {

                    String message = "alert('Updated Successfully')";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                }

                cnt = cnt + 1;
            }

        }
    }
}
