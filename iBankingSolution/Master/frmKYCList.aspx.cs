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
using System.Configuration;
using System.Data.SqlClient;

namespace iBankingSolution.Master
{
    public partial class frmKYCList : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void lnkedit_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int iStID = int.Parse((item.FindControl("lblCUST_ID") as Label).Text);

            Response.Redirect("~/Master/frmMasterKYC.aspx?Id=" + iStID);
            //Response.Redirect(GetRouteUrl("frmMasterKYC", null));
        }
       
        protected void LinkDelete_Click(object sender,EventArgs e)
        {

            //objBO_Finance.CUST_ID = ((Button)sender).CommandArgument;
            objBO_Finance.Flag = 3;
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int iStID = int.Parse((item.FindControl("lblCUST_ID") as Label).Text);
            objBO_Finance.CUST_ID = iStID;
           int i = objBL_Finance.InsertUpdateDeleteKYCMaster(objBO_Finance, out SQLError);
            if (i > 0)
            {

                message = "alert('Delete Successfully')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                //objBO_Finance.Flag = 6;
                //RepCCList.DataSource = ViewState["KYCDetails"] = objBL_Finance.GetKYCDetailsRecords(objBO_Finance, out SQLError);
                //RepCCList.DataBind();
            }
            else
            {
                message = "alert('Something Wrong.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
        }
        protected void BindGrid()
        {
            objBO_Finance.Flag = 6;
            objBO_Finance.CUST_ID = null;
            DataTable dt = objBL_Finance.GetKYCDetailsRecordListrec(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                 
                    RepCCList.DataSource = dt;
                    RepCCList.DataBind();
                 
            }


        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbx_ddlsearch.SelectedIndex>0)
            {
                txtsearchkyc.Focus();
            }

        }
        private static DataTable GetData(string query)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        protected void txtsearchkyc_TextChanged(object sender, EventArgs e)
        {
            if (cmbx_ddlsearch.SelectedItem.Text == "CUST_ID")
            {

                objBO_Finance.CUST_ID = Convert.ToInt32(txtsearchkyc.Text);
                DataSet ds = new DataSet();
                ds = objBL_Finance.SearchInKyc(objBO_Finance);

                RepCCList.DataSource = ds;
                RepCCList.DataBind();
            }

            else if (cmbx_ddlsearch.SelectedItem.Text == "NAME")
            {
                objBO_Finance.NAME = txtsearchkyc.Text;
                DataSet ds = new DataSet();
                ds = objBL_Finance.SearchforKYCName(objBO_Finance);

                RepCCList.DataSource = ds;
                RepCCList.DataBind();

            }

        }
    }
}