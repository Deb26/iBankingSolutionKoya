using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BLL;
using BusinessObject;

namespace iBankingSolution.Transaction
{
    public partial class frmCreditLimitConfiguration : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtduedate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                showScheme();

            }
        }
        protected void showScheme()
        {
            cmbx_LoanScheme.Items.Add(new ListItem("-- Select Loan Scheme --", ""));
            cmbx_LoanScheme.AppendDataBoundItems = true;
            String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String strQuery = "select SCHEME_NAME, SCHEME_CODE from LOAN_SCHEME_MASTER where LOAN_TYPE = 'Farm'";
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmbx_LoanScheme.DataSource = cmd.ExecuteReader();
                cmbx_LoanScheme.DataTextField = "SCHEME_NAME";
                cmbx_LoanScheme.DataValueField = "SCHEME_CODE";
                cmbx_LoanScheme.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        protected void btnsubmit1_Click(object sender, EventArgs e)
        {

        }
    }
}