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

namespace iBankingSolution.Transaction
{
    public partial class frmModifyTransaction : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        double CCBCode;
        decimal totdeb = 0;
        decimal totcre = 0;
        string previouDate;

        public static SqlConnection sqlConn()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            return new SqlConnection(cs);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtpkr_VoucherDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

        }

        protected void click_btn_delete(object sender, EventArgs e)
        {
            //objBO_Finance.Flag = 2;
            //objBO_Finance.VOUCHER_NO = cmbx_VoucherNo.SelectedValue;
            //objBO_Finance.VoucherType = cmbx_VoucherType.Text;
            int cnt = 0;
            int i = 0;

            foreach (var item in GVMTransaction.Rows)
            {
                Label CCBCodeNew = GVMTransaction.Rows[cnt].FindControl("lblCCBCode") as Label;

                objBO_Finance.Flag = 2;
                objBO_Finance.VOUCHER_NO = cmbx_VoucherNo.SelectedValue;
                objBO_Finance.VoucherType = cmbx_VoucherType.Text;
                objBO_Finance.CCBCode = Convert.ToDouble(CCBCodeNew.Text);
                i = objBL_Finance.ModifyTransaction(objBO_Finance, out SQLError);
                cnt = cnt + 1;
            }

            //int i = objBL_Finance.ModifyTransaction(objBO_Finance, out SQLError);
            if (i > 0)
            {
                string message = "Successfully Deleted.";
                lblStatus.Text = "Successfully Deleted.";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                ClearField();
            }
            else
            {
                string message = "Unable to Delete!!";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                
            }

        }

        protected void GVMTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbx_VoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            objBO_Finance.Flag = 2;
            //objBO_Finance.CCB_DATE = Convert.ToDateTime(dtpkr_VoucherDate.Text);

            string dtp = dtpkr_VoucherDate.Text;
            DateTime Cust1 = DateTime.ParseExact(dtp, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.CCB_DATE = Cust1;


            objBO_Finance.VoucherType = cmbx_VoucherType.SelectedItem.Text;
            ds = objBL_Finance.getVoucherNo(objBO_Finance, out SQLError);

            cmbx_VoucherNo.DataSource = ds;
            cmbx_VoucherNo.DataTextField = "VOUCHER_NO";
            cmbx_VoucherNo.DataValueField = "VOUCHER_NO";
            cmbx_VoucherNo.Items.Insert(0, "--Select Voucher--");
            cmbx_VoucherNo.DataBind();
        }

        protected void cmbx_VoucherNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            objBO_Finance.Flag = 1;
            objBO_Finance.VOUCHER_NO = cmbx_VoucherNo.SelectedValue;
            objBO_Finance.VoucherType = cmbx_VoucherType.SelectedItem.Text;
            dt = objBL_Finance.GetAllTransDetails(objBO_Finance, out SQLError);
            previouDate = dt.Rows[0]["VoucherDate"].ToString();
            CCBCode = Convert.ToDouble(dt.Rows[0]["CCBCodeNew"]);
            ViewState["PrevDt"] = previouDate;
            GVMTransaction.DataSource = dt;
            GVMTransaction.DataBind();
        }

        //protected void GVMTransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //    GVMTransaction.PageIndex = e.NewPageIndex;
        //    GVMTransaction.DataSource = dt;
        //    GVMTransaction.DataBind();
        //}


        //protected void GVMTransaction_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        //{

        //}

        //protected void GVMTransaction_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //    GVMTransaction.EditIndex = -1;
        //    GVMTransaction.DataBind();
        //}

        //protected void GVMTransaction_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //    objBO_Finance.Flag = 1;
        //    Label id = GVMTransaction.Rows[e.RowIndex].FindControl("VoucherCode") as Label;
        //    TextBox DEBIT = GVMTransaction.Rows[e.RowIndex].FindControl("txt_totdeb") as TextBox;
        //    TextBox CREDIT = GVMTransaction.Rows[e.RowIndex].FindControl("txt_totcre") as TextBox;

        //    objBO_Finance.VoucherType = cmbx_VoucherType.SelectedItem.Text;
        //    objBO_Finance.VOUCHER_NO = id.ToString();

        //    int i = objBL_Finance.ModifyTransaction(objBO_Finance, out SQLError);
        //    GVMTransaction.EditIndex = -1;
        //    GVMTransaction.DataSource = dt;
        //    GVMTransaction.DataBind();
        //}

        protected void GVMTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //first way
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                totdeb += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMT_DEBIT"));
                ViewState["totdeb"] = totdeb;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = totdeb.ToString();
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totcre += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMT_CREDIT"));
                ViewState["totcre"] = totcre;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = totcre.ToString();
            }


        }

        //protected void GVMTransaction_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //    objBO_Finance.Flag = 1;

        //    int i = objBL_Finance.ModifyTransaction(objBO_Finance, out SQLError);
        //    if (i > 0)
        //    { }
        //    GVMTransaction.EditIndex = e.NewEditIndex;
        //    GVMTransaction.DataSource = dt;
        //    GVMTransaction.DataBind();
        //}

        protected void ResetControl()
        {
            GVMTransaction.DataSource = null;
            GVMTransaction.DataBind();
            cmbx_VoucherType.SelectedIndex = -1;
            cmbx_VoucherNo.SelectedIndex = -1;
        }
        protected void click_btn_Update(object sender, EventArgs e)
        {
            int cnt = 0;
            string prevdate = (string)ViewState["PrevDt"];

            int result = 0;
            foreach (var item in GVMTransaction.Rows)
            {
                Label CCBCodeNew = GVMTransaction.Rows[cnt].FindControl("lblCCBCode") as Label;
                TextBox LdgCode = GVMTransaction.Rows[cnt].FindControl("txt_Ldg_Code") as TextBox;
                TextBox SlCode = GVMTransaction.Rows[cnt].FindControl("txt_Sl_Code") as TextBox;
                TextBox Amt_Debit = GVMTransaction.Rows[cnt].FindControl("txt_Amt_Debit") as TextBox;
                TextBox Amt_Credit = GVMTransaction.Rows[cnt].FindControl("txt_Amt_Credit") as TextBox;
                TextBox txt_VoucherDate = GVMTransaction.Rows[cnt].FindControl("txt_VoucherCode") as TextBox;

                //TextBox txt_totdeb = GVMTransaction.Rows[cnt].FindControl("txt_totdeb") as TextBox;
                //TextBox txt_totcre = GVMTransaction.Rows[cnt].FindControl("txt_totcre") as TextBox;

                TextBox lblTotalDebit = GVMTransaction.FooterRow.FindControl("txt_totdeb") as TextBox;
                TextBox lblTotalCredit = GVMTransaction.FooterRow.FindControl("txt_totcre") as TextBox;

                Int32 key = Convert.ToInt32(GVMTransaction.DataKeys[cnt].Value.ToString());
               
                objBO_Finance.Flag = 1;
                objBO_Finance.VoucherType = cmbx_VoucherType.SelectedItem.Text;
                objBO_Finance.VOUCHER_NO = cmbx_VoucherNo.SelectedItem.Text;
                objBO_Finance.LDG_CODE = LdgCode.Text;
                objBO_Finance.SL_CODE = SlCode.Text;
                objBO_Finance.DebitAmount = Convert.ToString(Amt_Debit.Text) != "" ? Convert.ToDouble(Amt_Debit.Text) : 0;
                objBO_Finance.CreditAmount = Convert.ToString(Amt_Credit.Text) != "" ? Convert.ToDouble(Amt_Credit.Text) : 0;

                string datedb = txt_VoucherDate.Text;
                DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.VoucherDate = timedb;

                string datedb1 = prevdate;
                DateTime timedb1 = DateTime.ParseExact(datedb1, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.PrevVoucherDate = timedb1;

                objBO_Finance.CCBCode = Convert.ToDouble(CCBCodeNew.Text);


                decimal dr = (decimal)ViewState["totdeb"];
                decimal Cr = (decimal)ViewState["totcre"];

                Label txt_totdeb = (Label)GVMTransaction.FooterRow.FindControl("txt_totdeb");
                Label txttotcre = (Label)GVMTransaction.FooterRow.FindControl("txt_totcre");

                if (txt_totdeb.Text == txttotcre.Text)
                {
                    result = objBL_Finance.ModifyTransactionNew(objBO_Finance, out SQLError);
                   
                }

                else
                {
                    String message = "alert('Debit and Credit value must be Equal..Unable to Save')";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                    return;
                }

                cnt = cnt + 1;
            }

            if (result > 0)
            {

                String message = "alert('Updated Successfully')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                lblStatus.Text = "Updated Successfully.";
                ClearField();
            }
            else
            {
                String message = "alert('Unable to Update')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                lblStatus.Text = "Unable to Update.";
            }

        }

        protected void ClearField()
        {
            dtpkr_VoucherDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            cmbx_VoucherNo.SelectedIndex = -1;
            cmbx_VoucherType.SelectedIndex = -1;
            GVMTransaction.DataSource = null;
            GVMTransaction.DataBind();

        }
        protected void txt_Ldg_Code_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txLdgCode = (TextBox)currentrow.FindControl("txt_Ldg_Code");
            TextBox txt_Sl_Code = (TextBox)currentrow.FindControl("txt_Sl_Code");

            if (txLdgCode.Text != "")
            {
                txt_Sl_Code.Text = "";
            }

            Label txt_totdeb = (Label)GVMTransaction.FooterRow.FindControl("txt_totdeb");
            txt_totdeb.Text = Convert.ToString(Convert.ToDouble(ViewState["totdeb"]));
            Label txttotcre = (Label)GVMTransaction.FooterRow.FindControl("txt_totcre");
            txttotcre.Text = Convert.ToString(Convert.ToDouble(ViewState["totcre"]));



        }

        protected void txt_Sl_Code_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txLdgCode = (TextBox)currentrow.FindControl("txt_Ldg_Code");
            TextBox txt_Sl_Code = (TextBox)currentrow.FindControl("txt_Sl_Code");

            if (txt_Sl_Code.Text != "")
            {
                txLdgCode.Text = "";
            }

            Label txt_totdeb = (Label)GVMTransaction.FooterRow.FindControl("txt_totdeb");
            txt_totdeb.Text = Convert.ToString(Convert.ToDouble(ViewState["totdeb"]));
            Label txttotcre = (Label)GVMTransaction.FooterRow.FindControl("txt_totcre");
            txttotcre.Text = Convert.ToString(Convert.ToDouble(ViewState["totcre"]));

        }



        protected void txt_Amt_Debit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txtAmt_Debit = (TextBox)currentrow.FindControl("txt_Amt_Debit");

            TextBox txt_Amt_Credit = (TextBox)currentrow.FindControl("txt_Amt_Credit");

            double totDR = 0;
            double totCR = 0;
            if (txtAmt_Debit.Text != "")
            {
                txt_Amt_Credit.Text = "0";
            }

           

            for (int i = 0; i < GVMTransaction.Rows.Count; i++)
            {
                TextBox DebitAmt = GVMTransaction.Rows[i].FindControl("txt_Amt_Debit") as TextBox;
                TextBox CredittAmt = GVMTransaction.Rows[i].FindControl("txt_Amt_Credit") as TextBox;

                totDR = Convert.ToDouble(totDR) + Convert.ToDouble(DebitAmt.Text);
                totCR = Convert.ToDouble(totCR) + Convert.ToDouble(CredittAmt.Text);
            }

            Label txt_totdeb = (Label)GVMTransaction.FooterRow.FindControl("txt_totdeb");
            txt_totdeb.Text =Convert.ToString(totDR) + ".00";

            Label txttotcre = (Label)GVMTransaction.FooterRow.FindControl("txt_totcre");
            txttotcre.Text = Convert.ToString(totCR) + ".00";

        }

        protected void txt_Amt_Credit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txtAmt_Debit = (TextBox)currentrow.FindControl("txt_Amt_Debit");
            TextBox txt_Amt_Credit = (TextBox)currentrow.FindControl("txt_Amt_Credit");
            double totDR1 = 0;
            double totCR1 = 0;

            if (txt_Amt_Credit.Text != "")
            {
                txtAmt_Debit.Text = "0";
            }

            for (int i = 0; i < GVMTransaction.Rows.Count; i++)
            {
                TextBox DebitAmt = GVMTransaction.Rows[i].FindControl("txt_Amt_Debit") as TextBox;
                TextBox CredittAmt = GVMTransaction.Rows[i].FindControl("txt_Amt_Credit") as TextBox;

                totDR1 = Convert.ToDouble(totDR1) + Convert.ToDouble(DebitAmt.Text);
                totCR1 = Convert.ToDouble(totCR1) + Convert.ToDouble(CredittAmt.Text);
            }

            Label txt_totdeb = (Label)GVMTransaction.FooterRow.FindControl("txt_totdeb");
            txt_totdeb.Text = Convert.ToString(totDR1) + ".00";

            Label txttotcre = (Label)GVMTransaction.FooterRow.FindControl("txt_totcre");
            txttotcre.Text = Convert.ToString(totCR1) + ".00";

        }
    }
}