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
using iBankingSolution.Report.CrystalReports;
using System.Configuration;
using System.Data.SqlClient;
using BLL.Master;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace iBankingSolution.Report
{
    public partial class frmMisDepositReturn : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        MyDBDataContext DBConext = new MyDBDataContext();
        DateTime YrStartDt;
        DataTable dtledger;
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            FillReportByType();
        }

        protected void FillReportByType()
        {
            objBO_Finance.Flag = 1;

            string dta = dtpkr_frmDate.Text;
            objBO_Finance.FDate = DateTime.ParseExact(dta, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int i = objBL_Finance.InsertDepositReturnTable(objBO_Finance, out SQLError);
            if (i > 0)
            {
                objBO_Finance.Flag = 2;
                DataSet dt = objBL_Finance.GETDEPOSITRETURN(objBO_Finance, out SQLError);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    RepCCList.DataSource = dt;
                    RepCCList.DataBind();
                }

                DataTable dtList = new DataTable();
                dtList = dt.Tables[0];
                int X = dtList.Rows.Count;
                Session["dtMISDEPOSITReport"] = dtList;
            }
            else
            {
                String message = "alert('Something Went Wrong')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }

        }
        protected void cmbx_SelectMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            int month = Convert.ToInt32(cmbx_selectmonth.SelectedValue);
            var startOfMonth = new DateTime(now.Year, month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now.Year, month);
            var lastDay = new DateTime(now.Year, month, DaysInMonth);
            string lastdt = Convert.ToString(lastDay.ToString("dd/MM/yyyy"));
            dtpkr_frmDate.Text = Convert.ToString(lastdt);
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            
                builder = new SqlConnectionStringBuilder(strConn);
                ReportDocument crystalReport = new ReportDocument();
                DataTable dt = new DataTable();

                dt = (DataTable)System.Web.HttpContext.Current.Session["dtMISDEPOSITReport"];

                if (dt.Rows.Count > 0)
                {
                    Response.Clear();
                    crystalReport = new ReportDocument();

                    crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptMisDepositReturn.rpt"));
                    crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptMisDepositReturn.rpt");
                    crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                    crystalReport.SetDataSource(dt);
                    crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "DepositReturn");

                }
            
        }
    }
}