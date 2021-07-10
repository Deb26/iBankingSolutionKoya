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
    public partial class frm_DailyScroll_ASON : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                GetBranch();
               txt_dailyscroll.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }

        }

        protected void GetBranch()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            DataTable dt = objBL_Finance.GetBranch(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_Branch.DataSource = dt;
                cmbx_Branch.DataTextField = "BranchName";
                cmbx_Branch.DataValueField = "BranchID";
                cmbx_Branch.DataBind();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (cmbx_Branch.SelectedValue == "0")
            {
                objBO_Finance.Flag = 2;
            }
            else
            {
                objBO_Finance.Flag = 1;
            }

  
            string datedb = txt_dailyscroll.Text;
            DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.CCB_DATE = timedb;
            objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;
            DataTable dt = new DataTable();
            dt = objBL_Finance.dailyscrollbydate(objBO_Finance);
            gv_dailyscroll.DataSource = dt;
            gv_dailyscroll.DataBind();
            DSRPanel.Visible = true;
            Session["dtScroll"] = dt;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (txt_dailyscroll.Text != "")
            {

                builder = new SqlConnectionStringBuilder(strConn);
                ReportDocument crystalReport = new ReportDocument();
                DataTable dt = new DataTable();

                dt = (DataTable)System.Web.HttpContext.Current.Session["dtScroll"];

                if (dt.Rows.Count > 0)
                {
                    Response.Clear();
                    crystalReport = new ReportDocument();

                    crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptDailyScrollOnDate.rpt"));
                    crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptDailyScrollOnDate.rpt");

                    //crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptDetailListShare.rpt"));
                    //crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptDetailListShare.rpt");



                    crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                    crystalReport.SetDataSource(dt);
                    //crystalReport.SetDataSource(ds);
                   
                        crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "DailyScroll");
                  
                }
            }
        }
    }
}