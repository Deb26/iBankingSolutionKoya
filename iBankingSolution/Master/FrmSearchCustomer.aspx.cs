using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessObject;
using BLL;
using System.Configuration;

namespace iBankingSolution.Master
{
    

    public partial class FrmSearchCustomer : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;


        public static SqlCredential DBConnect { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbx_category.SelectedIndex != 0)
            {
                txttype.Visible = true;
                btnsearch.Visible = true;
                txttype.Text = String.Empty;
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            if (cmbx_category.SelectedItem.Text == "ID")
            {
                objBO_Finance.CUST_ID = Convert.ToString(txttype.Text);
                objBO_Finance.NAME = "";
              
            }
            else if (cmbx_category.SelectedItem.Text == "NAME")
            {
                objBO_Finance.CUST_ID = 0;
                objBO_Finance.NAME = Convert.ToString(txttype.Text);
              
            }

            dt = objBL_Finance.searchclient(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                GVSupplier.DataSource = dt;
                GVSupplier.DataBind();
            }
        }

        protected void txt_CUST_ID_TextChanged(object sender, EventArgs e)
        {


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
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string CUST_Id = GVSupplier.DataKeys[e.Row.RowIndex].Value.ToString();
                string SqlQuery = "SELECT CASE WHEN SM.ACTYPE='s' then 'Savings' WHEN SM.ACTYPE='sh' then 'Share'  WHEN SM.ACTYPE='r' then 'Recurring'  WHEN SM.ACTYPE='fd' then 'Fixed Deposit'  WHEN SM.ACTYPE='mis' then 'MIS'  WHEN SM.ACTYPE='cc' then 'Deposit Certificate' end ACTYPE ,SM.AC_STATUS,SM.OLD_ACNO,SM.SL_CODE FROM SUBLEDGER_MASTER SM INNER JOIN CLIENT_MASTER CM ON CM.SL_CODE = SM.SL_CODE INNER JOIN CLIENT_REGISTER CR ON CR.CUST_ID = CM.CUST_ID WHERE CR.CUST_ID ='" + CUST_Id + "'";
                GridView gvOrders = e.Row.FindControl("GVChild") as GridView;

                gvOrders.DataSource = GetData(SqlQuery);
                gvOrders.DataBind();
            }
        }

       
        protected void LinkButton1_Click(object sender, EventArgs e)
        {


        }

        protected void GVSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            btnsearch_Click(sender, e);
            GVSupplier.PageIndex = e.NewPageIndex;
        }
    }

}

