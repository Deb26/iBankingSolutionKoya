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
    public partial class frmStock_Update : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;
        decimal totqty = 0;
        decimal totrate = 0;
        double value;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtasondt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }



        protected void txtasondt_TextChanged(object sender, EventArgs e)
        {


            objBO_Finance.Flag = 1;
            objBO_Finance.MCLASS = Convert.ToString(cmbx_itemclass.SelectedItem.Text);
  

            string Cust = txtasondt.Text;
            DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.ASON = Cust1;


            
            DataSet ds = new DataSet();
            ds = objBL_Finance.getStockDetails(objBO_Finance);

            gv_stock.DataSource = ds;
            gv_stock.DataBind();
        }

        protected void txt_rate_TextChanged(object sender, EventArgs e)
        {


            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txtQty = (TextBox)currentRow.FindControl("txt_qty");
            TextBox txtRate = (TextBox)currentRow.FindControl("txt_rate");

            Label tolValue = (Label)currentRow.FindControl("lbl_value");
            value = Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtRate.Text);
            tolValue.Text = Convert.ToString(value);


        }

        protected void gv_stock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totqty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOT_QTY"));
                totrate += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Stock_Rate"));
                value = Convert.ToDouble(totqty * totrate);

                Label lb = (Label)e.Row.FindControl("lbl_value");
                lb.Text = Convert.ToString(value);
            }
        }

        protected void ResetControl()
        {
            gv_stock.DataSource = null;
            gv_stock.DataBind();
            txtasondt.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void btnsubmit1_Click(object sender, EventArgs e)
        {

            int cnt = 0;
            int result = 0;
            string datedb = txtasondt.Text;
            DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            foreach (var item in gv_stock.Rows)
            {
                Label Code = gv_stock.Rows[cnt].FindControl("lbl_code") as Label;
                TextBox Qty = gv_stock.Rows[cnt].FindControl("txt_qty") as TextBox;
                TextBox Rate = gv_stock.Rows[cnt].FindControl("txt_rate") as TextBox;
                Label Value = gv_stock.Rows[cnt].FindControl("lbl_value") as Label;

                objBO_Finance.ASON = timedb;
                objBO_Finance.TOT_QTY = Qty.Text != "" ? Convert.ToDouble(Qty.Text) : 0;
                objBO_Finance.Item_Code = Code.Text;
                objBO_Finance.Stock_Rate = Rate.Text != "" ? Convert.ToDecimal(Rate.Text) : 0;
                objBO_Finance.Stock_Value = Value.Text != "" ? Convert.ToDecimal(Value.Text) : 0;

                result = objBL_Finance.Stock_Updateform(objBO_Finance);


                cnt = cnt + 1;
            }

            if (result > 0)
            {

                message = "Updated Successfully";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

            }
            else
            {
                message = "Update Failed";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
        }
    }
}