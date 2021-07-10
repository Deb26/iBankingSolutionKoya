using System;
using System.Data;
using BusinessObject;
using BLL;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace iBankingSolution.Transaction
{
    public partial class frmDetailListShare : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        DataTable dtledger;
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtpkr_DateAsOn.Text = DateTime.Now.ToString("dd/MM/yyyy");

            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            FillReportByType();
        }

        protected void FillReportByType()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.ACTYPE = "sh";
            objBO_Finance.AsOnDate = DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DataSet dt = objBL_Finance.ShareDetailReport(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                RepCCList.DataSource = dt;
                RepCCList.DataBind();

            }

            DataTable dtList = new DataTable();
            dtList = dt.Tables[0];
            int X = dtList.Rows.Count;
            Session["dtShareDetailList"] = dtList;
            
        }

        protected void btnDownload_Click (object sender , EventArgs e)
        {
            FillReportByType();
            if (dtpkr_DateAsOn.Text != "")
            {
                builder = new SqlConnectionStringBuilder(strConn);
                ReportDocument crystalReport = new ReportDocument();
                DataTable dt = new DataTable();

                dt = (DataTable)System.Web.HttpContext.Current.Session["dtShareDetailList"];

                if (dt.Rows.Count > 0)
                {
                    Response.Clear();
                    crystalReport = new ReportDocument();

                    crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptDetailListShare.rpt"));
                    crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptDetailListShare.rpt");

                    crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                    crystalReport.SetDataSource(dt);
                    //crystalReport.SetDataSource(ds);

                    crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "DetailListShare");
                    //crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "DetailListShare");
                    //crystalReport.ExportToHttpResponse(ExportFormatType.Excel, Response, true, "DetailListShare");

                }
                //String message = String.Empty;
                //if (System.Web.HttpContext.Current.Session["dtShareDetailList"] != null)
                //{
                //    string url = "../ExportDownloadPage/ExportToDayBookReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
                //    string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=0,top=0,resizable=yes');";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", s, true);
                //}
                //else
                //{
                //    message = "alert('No Data Found')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //}
            }
        }
    }
}