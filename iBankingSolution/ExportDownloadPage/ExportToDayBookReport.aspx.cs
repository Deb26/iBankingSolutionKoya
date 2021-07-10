using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace iBankingSolution.ExportDownloadPage
{
    public partial class ExportToDayBookReport : System.Web.UI.Page
    {
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                builder = new SqlConnectionStringBuilder(strConn);
                ReportDocument crystalReport = new ReportDocument();
                DataTable dt = new DataTable();
                dt = (DataTable)System.Web.HttpContext.Current.Session["dtdaybook"];
              

                if (dt.Rows.Count > 0)
                {
                    Response.Clear();
                    crystalReport = new ReportDocument();

                    crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/NewDayBookReport.rpt"));
                    crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/NewDayBookReport.rpt");
                    //crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptDayBookReports.rpt"));
                    //crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptDayBookReports.rpt");

                    crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                    crystalReport.SetDataSource(dt);
                    if (Request.QueryString["ID"] == "WORD")
                    {
                        crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "DayBook");
                    }
                    else if (Request.QueryString["ID"] == "PDF")
                    {
                        crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "DayBook");
                    }
                    else if (Request.QueryString["ID"] == "EXCEL")
                    {
                        crystalReport.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "DayBook");
                    }
                    else
                    {
                        crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "DayBook");
                    }
                    Response.End();
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                }
            }
        }
    }
}