using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;


namespace iBankingSolution.Report
{
    public partial class frmDepositReturn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
           
                BindGridview();
                
            }
        }
        protected void BindGridview()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("rowid", typeof(int));
            dt.Columns.Add("productname", typeof(string));
            dt.Columns.Add("price", typeof(string));
            DataRow dr = dt.NewRow();
            dr["rowid"] = 1;
            dr["productname"] = string.Empty;
            dr["price"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["Curtbl"] = dt;
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
        }
        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtName");
                        TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtPrice");
                        drCurrentRow = dt.NewRow();
                        drCurrentRow["rowid"] = i + 1;
                        dt.Rows[i - 1]["productname"] = txtname.Text;
                        dt.Rows[i - 1]["price"] = txtprice.Text;
                        rowIndex++;
                    }
                    dt.Rows.Add(drCurrentRow);
                    ViewState["Curtbl"] = dt;
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState Value is Null");
            }
            SetOldData();
        }
        private void SetOldData()
        {
            int rowIndex = 0;
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtName");
                        TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtPrice");
                        txtname.Text = dt.Rows[i]["productname"].ToString();
                        txtprice.Text = dt.Rows[i]["price"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtbl"] = dt;
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();


                    for (int i = 0; i < gvDetails.Rows.Count - 1; i++)
                    {
                        gvDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetOldData();
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                DataRow drCurrentRow = null;
                Button clickedbutton = (Button)sender; 
                GridViewRow row = (GridViewRow)clickedbutton.NamingContainer; 
                int rowIndex = Convert.ToInt32(row.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtbl"] = dt;
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();


                    for (int i = 0; i < gvDetails.Rows.Count - 1; i++)
                    {
                        gvDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetOldData();
                }
            }
        }
    }
}