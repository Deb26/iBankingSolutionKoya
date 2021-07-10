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
using BLL.GeneralBL;
using System.Data.SqlClient;

namespace iBankingSolution.Report
{
    public partial class RdlcClientList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void txtCustID_TextChanged(object sender, EventArgs e)
        {

        }
        private void BindReport()
        {

        }

        protected void GridClientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridClientList.PageIndex = e.NewPageIndex;
            GridClientList.DataSource = System.Web.HttpContext.Current.Session["ClientListReport"];
            GridClientList.DataBind();
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            objBO_Finance.CUST_ID = txtCustID.Text;
            dt = objBL_Finance.GetClientLists(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                System.Web.HttpContext.Current.Session["ClientListReport"] = dt;
                GridClientList.DataSource = dt;
                GridClientList.DataBind();
            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                GridClientList.DataSource = dt;
                GridClientList.DataBind();
                int columncount = GridClientList.Rows[0].Cells.Count;
                GridClientList.Rows[0].Cells.Clear();
                GridClientList.Rows[0].Cells.Add(new TableCell());
                GridClientList.Rows[0].Cells[0].ColumnSpan = columncount;
                GridClientList.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            String message = String.Empty;
            if (System.Web.HttpContext.Current.Session["ClientListReport"] != null)
            {
                string url = "../ExportDownloadPage/ExportToClientListReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
                string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=0,top=0,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", s, true);
            }
            else
            {
                message = "alert('No Data Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
    }
}