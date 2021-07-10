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
    public partial class GSTPayableReceiable : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;
        string SQLError = string.Empty;
        DateTime YrStartDt;
        string Session = "";
        MyDBDataContext dbContext = new MyDBDataContext();
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
              
                Session = YrStDt.sess;
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GetYearStDate();
            objBO_Finance.Flag = 7;
            objBO_Finance.FDate = DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.TDate = DateTime.ParseExact(dtpkr_ToDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.ASSES = Session;


            //objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;

            DataTable StockDt = objBL_Finance.StockDetailsReport(objBO_Finance, out SQLError);

            ViewState["StockDTl"] = StockDt;

            DataTable StockDataTable = new DataTable();
            StockDataTable = StockDt.Copy();

            StockDataTable.Columns.Remove("LISCENCEE_NAME");
            StockDataTable.Columns.Remove("LISCENCEE_ADDRESS");
            StockDataTable.Columns.Remove("FromDate");
            StockDataTable.Columns.Remove("ToDate");

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

                string path = Server.MapPath(@"../Report/CrystalReports/rptGSTPaybleReceibleReport.rpt");
                crystalReport.Load(Server.MapPath(@"../Report/CrystalReports/rptGSTPaybleReceibleReport.rpt"));
                crystalReport.FileName = Server.MapPath(@"../Report/CrystalReports/rptGSTPaybleReceibleReport.rpt");

                crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                crystalReport.SetDataSource(dt);
                crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "GSTPayableReceiableReport");
                Response.End();
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);

            }
        }
    }
}