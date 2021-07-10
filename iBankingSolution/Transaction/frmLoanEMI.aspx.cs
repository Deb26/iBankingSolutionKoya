using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;



namespace iBankingSolution.Transaction
{
    public partial class frmLoanEMI : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetLoanAccountNo();
            }
                
        }

        protected void cmbx_AccountNo_SelectedIndexChanged(object sender , EventArgs e)
        {
            String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String query = "select LOAN_AMNT , ROI , DURATION , REPAY_MODE , APPL_DT from SUBLEDGER_DETAILS_LOAN WHERE SL_CODE = @SL_CODE";
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@SL_CODE", cmbx_AccountNo.SelectedItem.Value);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    txtLAc.Text = sdr[0].ToString();
                    txtRoi.Text = Convert.ToString(sdr["ROI"]);
                    txtPeriod.Text = Convert.ToString(sdr["DURATION"]);
                    txtRepayMode.Text = Convert.ToString(sdr["REPAY_MODE"]);
                    txtopeningdt.Text = Convert.ToDateTime(sdr["APPL_DT"]).ToString("dd/MM/yyyy");
                    
                }

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

        protected void btnShow_Click(object sender , EventArgs e)
        {

        }
        protected void GetLoanAccountNo()
        {

            objBO_Finance.Flag = 1;

            DataTable dt = objBL_Finance.GetsubLedgerDetailsLoan(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_AccountNo.DataSource = dt;
                cmbx_AccountNo.DataTextField = "SL_CODE";
                cmbx_AccountNo.DataValueField = "SL_CODE";

                cmbx_AccountNo.DataBind();
                cmbx_AccountNo.Items.Insert(0, "-- Select A Loan Account Number --");

            }

        }
    }
}