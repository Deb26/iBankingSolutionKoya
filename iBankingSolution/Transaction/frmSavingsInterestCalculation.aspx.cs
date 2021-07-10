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
    public partial class frmSavingsInterestCalculation : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showScheme();

            }
        }

        protected void showScheme()
        {
            cmbx_DepositScheme.Items.Add(new ListItem("-- Select Deposit Scheme --", ""));
            cmbx_DepositScheme.AppendDataBoundItems = true;
            String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String strQuery = "select SCHEME , LDG_CODE from DEPOSIT_MASTER where scheme_type = 's'";
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmbx_DepositScheme.DataSource = cmd.ExecuteReader();
                cmbx_DepositScheme.DataTextField = "SCHEME";
                cmbx_DepositScheme.DataValueField = "LDG_CODE";
                cmbx_DepositScheme.DataBind();
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
        protected void cmbxSelectDepositScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String query = "select DM_CODE, LAST_INTEREST_CALCULATION_DATE ,NEXT_INTEREST_CALCULATION_DATE FROM DEPOSIT_MASTER WHERE LDG_CODE = @LDG_CODE";
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@LDG_CODE", cmbx_DepositScheme.SelectedItem.Value);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    txt_DmCode.Text = sdr[0].ToString();
                    dtpkr_lastDate.Text = Convert.ToDateTime(sdr["LAST_INTEREST_CALCULATION_DATE"]).ToString("dd/MM/yyyy");
                    dtpkr_nextDate.Text = Convert.ToDateTime(sdr["NEXT_INTEREST_CALCULATION_DATE"]).ToString("dd/MM/yyyy");
                }

                string dtop = dtpkr_lastDate.Text;
                DateTime FromYear = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                
                string dtw = dtpkr_nextDate.Text;
                DateTime ToYear = DateTime.ParseExact(dtw, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                TimeSpan objTimeSpan = ToYear - FromYear;

                int Years = ToYear.Year - FromYear.Year;

                int month = ToYear.Month - FromYear.Month;

                int TotalMonths = (Years * 12) + month;

                txt_Months.Text = Convert.ToString(TotalMonths);


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

        protected void btnShow_Click(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.DM_CODE = txt_DmCode.Text;
            DataSet dSet = objBL_Finance.GetSavingsInterest(objBO_Finance, out SQLError);
            if (dSet.Tables[0].Rows.Count > 0)
            {
                gv_Interest.DataSource = dSet.Tables[0];
                gv_Interest.DataBind();
            }
            else
            {
                String message = "alert('Data Not Found')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);


            }
        }

        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }

        protected void gv_Interest_PreRender(object sender, EventArgs e)
        {
            MergeRows(gv_Interest);
        }
    }
}