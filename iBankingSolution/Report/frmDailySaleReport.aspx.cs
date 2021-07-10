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

namespace iBankingSolution.Report
{
    public partial class frmDailySaleReport : System.Web.UI.Page
    {
        MyDBDataContext dbContext = new MyDBDataContext();
        DateTime YrStartDt;
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string Session = "";
        string SQLError = string.Empty;
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GetYearStDate()
        {

            var YrStDt = (from Cod in dbContext.CODESANDNOs

                          select new
                          {
                              StDt = Cod.YEAR_START_DT,
                              sess = Cod.Session


                          }).SingleOrDefault();

            if (YrStDt != null)
            {
                YrStartDt = Convert.ToDateTime(YrStDt.StDt);
                Session = YrStDt.sess;
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GetYearStDate();

            objBO_Finance.Flag = 1;
            objBO_Finance.AsOnDate = DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
           
            objBO_Finance.ASSES = Session;
            //objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;

            DataTable StockDt = objBL_Finance.StockDetailsReport(objBO_Finance, out SQLError);

            ViewState["StockDTl"] = StockDt;

            DataTable StockDataTable = new DataTable();
            StockDataTable = StockDt.Copy();

            StockDataTable.Columns.Remove("LISCENCEE_NAME");
            StockDataTable.Columns.Remove("LISCENCEE_ADDRESS");
            StockDataTable.Columns.Remove("AsonDate");

            if (StockDataTable.Rows.Count > 0)
            {

                GvStockDetails.DataSource = StockDataTable;
                GvStockDetails.DataBind();
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            builder = new SqlConnectionStringBuilder(strConn);
            ReportDocument crystalReport = new ReportDocument();
            DataTable dt = new DataTable();

            dt = (DataTable)ViewState["StockDTl"];
            //(DataTable)System.Web.HttpContext.Current.Session["dtCashAccount"];

            if (dt.Rows.Count > 0)
            {
                Response.Clear();
                crystalReport = new ReportDocument();

                string path = Server.MapPath(@"../Report/CrystalReports/rptStockDailySalesReport.rpt");
                crystalReport.Load(Server.MapPath(@"../Report/CrystalReports/rptStockDailySalesReport.rpt"));
                crystalReport.FileName = Server.MapPath(@"../Report/CrystalReports/rptStockDailySalesReport.rpt");

                crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                crystalReport.SetDataSource(dt);
                crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "DailySaleReport");
                Response.End();
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);

            }
        }

        protected void GvStockDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvStockDetails.PageIndex = e.NewPageIndex;
            btnShow_Click(sender, e);
        }
    }
}