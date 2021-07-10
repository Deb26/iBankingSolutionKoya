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
    public partial class ExportToDetailsDepositReport : System.Web.UI.Page
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
                dt = (DataTable)System.Web.HttpContext.Current.Session["dtDetailsDeposit"];
                String ReportType = "";
                ReportType = Convert.ToString(Session["Reporttype"]);
                if (dt.Rows.Count > 0)
                {
                    Response.Clear();
                    crystalReport = new ReportDocument();

                    if (ReportType == "s"|| ReportType == "shg" || ReportType == "sh" || ReportType == "nf" || ReportType == "sus" || ReportType == "jlg")
                    {
                        crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptDetailsDepositReport.rpt"));
                        crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptDetailsDepositReport.rpt");
                    }
                    else if (ReportType == "fd")
                    {
                        crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptfixedDepositDetailList.rpt"));
                        crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptfixedDepositDetailList.rpt");
                    }
                    else if (ReportType == "cc")
                    {
                        crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/DepositCertificateDetailListReport.rpt"));
                        crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/DepositCertificateDetailListReport.rpt");
                    }
                    else if (ReportType == "r")
                    {
                        crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptRecurringDepositDetailListReport.rpt"));
                        crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptRecurringDepositDetailListReport.rpt");
                    }
                    else if (ReportType == "mis")
                    {
                        crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptmisDepositDetailListReport.rpt"));
                        crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptmisDepositDetailListReport.rpt");
                    }




                    crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                    crystalReport.SetDataSource(dt);
                    if (Request.QueryString["ID"] == "WORD")
                    {
                        crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "Detail List");
                    }
                    else if (Request.QueryString["ID"] == "PDF")
                    {
                        crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Detail List");
                    }
                    else if (Request.QueryString["ID"] == "EXCEL")
                    {
                        crystalReport.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, dt.Rows[0]["LicenseName"].ToString());
                    }
                    else
                    {
                        crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, dt.Rows[0]["LicenseName"].ToString());
                    }
                    Response.End();
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                }
            }
        }
    }
}