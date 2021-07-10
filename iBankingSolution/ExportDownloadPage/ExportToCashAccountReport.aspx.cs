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
using System.IO;
using System.Runtime.InteropServices;

namespace iBankingSolution.ExportDownloadPage
{
    public partial class ExportToCashAccountReport : System.Web.UI.Page
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
                dt = (DataTable)System.Web.HttpContext.Current.Session["dtCashAccount"];

                if (dt.Rows.Count > 0)
                { 
                    Response.Clear();
                    crystalReport = new ReportDocument();

                    string path = Server.MapPath(@"../Report/CrystalReports/rptCashAccount.rpt");
                    crystalReport.Load(Server.MapPath(@"../Report/CrystalReports/rptCashAccount.rpt"));
                    crystalReport.FileName = Server.MapPath(@"../Report/CrystalReports/rptCashAccount.rpt");

                    crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                    crystalReport.SetDataSource(dt);

                  
                    crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "CashAccount");

                    //if (Request.QueryString["ID"] == "HTML")
                    //{




                    //    crystalReport.ExportToHttpResponse(ExportFormatType.HTML32, Response, true, "CashAccount");

                    //}
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
        }
    }
}