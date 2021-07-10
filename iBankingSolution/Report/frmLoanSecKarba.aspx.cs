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
    public partial class frmLoanSecKarba : System.Web.UI.Page
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
                BindSLCODE();
            }
        }

        protected void BindSLCODE()
        {
            try
            {

                DataTable dt = objBL_Finance.LoanSecurityKarbaSlCode(objBO_Finance);
                if (dt.Rows.Count > 0)
                {
                    cmbx_slcode.DataSource = dt;
                    cmbx_slcode.DataValueField = "SL_CODE";
                    cmbx_slcode.DataTextField = "SL_CODE";
                    cmbx_slcode.DataBind();
                    cmbx_slcode.Items.Insert(0, new ListItem("--SELECT--"));
                }
            }
            catch (Exception ex)
            { string msg = ex.Message; }
            finally
            { }

        }

        protected void cmbx_slcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBO_Finance.SL_CODE = cmbx_slcode.SelectedItem.Text;
            DataTable dt = new DataTable();
            dt = objBL_Finance.LoanSecurityKarbaReport(objBO_Finance);
            gv_LoanSecKarba.DataSource = dt;
            gv_LoanSecKarba.DataBind();

            Session["dtKarba"] = dt;
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            if (cmbx_slcode.SelectedItem.Text != "")
            {
                builder = new SqlConnectionStringBuilder(strConn);
                ReportDocument crystalReport = new ReportDocument();
                DataTable dt = new DataTable();

                dt = (DataTable)System.Web.HttpContext.Current.Session["dtKarba"];

                if (dt.Rows.Count > 0)
                {
                    Response.Clear();
                    crystalReport = new ReportDocument();

                    crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/frmLoanSecKarbareport.rpt"));
                    crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/frmLoanSecKarbareport.rpt");

                    crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                    crystalReport.SetDataSource(dt);
                    //crystalReport.SetDataSource(ds);

                    crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "LoanSecurityKarbanama");

                }
            }
        }
    }
}