using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using BusinessObject;
using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace iBankingSolution.Transaction
{
    public partial class frmBankLedgerBalanceUpdate : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        Decimal Comm = 0;
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                //txttdt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                //GetGridData();
                SetInitialRow();
                DisplalyGrid();


            }
        }

        protected void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("LDG_CODE", typeof(String)));
            dt.Columns.Add(new DataColumn("NOMENCLATURE", typeof(String)));

            dt.Columns.Add(new DataColumn("ACT_OP_CR", typeof(String)));
            dt.Columns.Add(new DataColumn("ACT_OP_DR", typeof(String)));
            dt.Columns.Add(new DataColumn("txtbranchid", typeof(String)));

            dr = dt.NewRow();
            dr["LDG_CODE"] = String.Empty;
            dr["NOMENCLATURE"] = String.Empty;
            dr["ACT_OP_CR"] = String.Empty;
            dr["ACT_OP_DR"] = String.Empty;
            dr["txtbranchid"] = String.Empty;

            dt.Rows.Add(dr);


            ViewState["dt"] = dt;
            BindGrid();

        }
        protected void BindGrid()
        {
            gridActs.DataSource = ViewState["dt"] as DataTable;
            gridActs.DataBind();
            //lblcount.Text = "No of Records: " + gridActs.Rows.Count;
        }
        private void DisplalyGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", 6);
            cmd.Parameters.AddWithValue("@Error", "");

            cmd.CommandText = "usp_GETBANKDETAILS";
            cmd.Connection = con;
            try
            {

                con.Open();
                gridActs.EmptyDataText = "No Records Found";
                gridActs.DataSource = cmd.ExecuteReader();


                gridActs.DataBind();
                // lblcount.Text = "No of Records: " + gridActs.Rows.Count;


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
        protected void click_btn_Update(object sender, EventArgs e)
        {
            int cnt = 0;
            int i = 0;
            foreach (var item in gridActs.Rows)
            {
                TextBox LdgCode = gridActs.Rows[cnt].FindControl("txtldgcode") as TextBox;
                TextBox nomenclature = gridActs.Rows[cnt].FindControl("txtnomenclature") as TextBox;
                TextBox actopcr = gridActs.Rows[cnt].FindControl("txtactopcr") as TextBox;
                TextBox actopdr = gridActs.Rows[cnt].FindControl("txtactopdr") as TextBox;
                TextBox branchid = gridActs.Rows[cnt].FindControl("txtbranchid") as TextBox;

                objBO_Finance.Flag = 1;
                objBO_Finance.LDG_CODE = LdgCode.Text;
                objBO_Finance.NOMENCLATURE = Convert.ToString(nomenclature.Text);
                objBO_Finance.ACT_OP_CR = Convert.ToDouble(actopcr.Text);
                objBO_Finance.ACT_OP_DR = Convert.ToDouble(actopdr.Text);
                objBO_Finance.BranchCode = branchid.Text;

                 i = objBL_Finance.BankLDGBalanceUpdate(objBO_Finance, out SQLError);
                cnt = cnt + 1;
            }
            if (i > 0)
            {
                String message = "alert('Updated Successfully')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
            else
            {
                String message = "alert('Unable to Update')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
        }
    }
}