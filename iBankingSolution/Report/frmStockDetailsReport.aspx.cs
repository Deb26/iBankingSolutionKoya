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
    public partial class frmStockDetailsReport : System.Web.UI.Page
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
            if(!IsPostBack)
            {
                LoadCategory();
               
            }
        }

        protected void LoadCategory()
        {

            var ItmCat = (from itm in dbContext.ITEM_CLASSes

                              select new
                              {
                                  ID = itm.CODE,
                                  Name = itm.NAME

                              }).ToList();

            if (ItmCat.Count>0)
            {
                cmbx_Category.DataSource = ItmCat.ToList();
                cmbx_Category.DataTextField = "Name";
                cmbx_Category.DataValueField = "ID";
                cmbx_Category.DataBind();
                cmbx_Category.Items.Insert(0, new ListItem("--Please Select--", "0"));
       

            }

        }
        protected void GetYearStDate()
        {

            var YrStDt = (from Cod in dbContext.CODESANDNOs

                          select new
                          {
                              StDt = Cod.YEAR_START_DT,
                              sess=Cod.Session


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

            objBO_Finance.Flag = 6;
            objBO_Finance.AsOnDate = DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.GROUP_CODE = cmbx_Category.SelectedValue;
            objBO_Finance.ASSES = Session;
            //objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;

            DataTable StockDt = objBL_Finance.StockDetailsReport(objBO_Finance, out SQLError);

            ViewState["StockDTl"] = StockDt;

            DataTable StockDataTable = new DataTable();
            StockDataTable = StockDt.Copy();

            StockDataTable.Columns.Remove("LISCENCEE_NAME");
            StockDataTable.Columns.Remove("LISCENCEE_ADDRESS");
            StockDataTable.Columns.Remove("AsonDate");

            if (StockDataTable.Rows.Count> 0)
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

                    string path = Server.MapPath(@"../Report/CrystalReports/rptStockDetailReport.rpt");
                    crystalReport.Load(Server.MapPath(@"../Report/CrystalReports/rptStockDetailReport.rpt"));
                    crystalReport.FileName = Server.MapPath(@"../Report/CrystalReports/rptStockDetailReport.rpt");

                    crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                    crystalReport.SetDataSource(dt);


                    crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "StockDetailList");
                 
                    //else if (Request.QueryString["ID"] == "WORD")
                    //{
                    //    crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "CashAccount");
                    //}
                    //else if (Request.QueryString["ID"] == "TEXT")
                    //{
                    //    crystalReport.ExportToHttpResponse(ExportFormatType.Text, Response, true, "CashAccount");
                    //}
                    //else
                    //{
                    //    crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "CashAccount");
                    //}
                    Response.End();
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                
            }
        }

        protected void GvStockDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvStockDetails.PageIndex = e.NewPageIndex;

            btnShow_Click(sender,e);
        }
    }
}