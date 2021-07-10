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
    public partial class frmLoanCollectionByScheme : System.Web.UI.Page
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
                BindSCHEMECODE();
            }

        }

        protected void cmbx_schemecode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void BindSCHEMECODE()
        {
            try
            {

                DataTable dt = objBL_Finance.LoanCollectionFormbySchemeCode(objBO_Finance);
                if (dt.Rows.Count > 0)
                {
                    cmbx_schemecode.DataSource = dt;
                    cmbx_schemecode.DataValueField = "SCHEME_CODE";
                    cmbx_schemecode.DataTextField = "SCHEME_NAME";
                    cmbx_schemecode.DataBind();
                    cmbx_schemecode.Items.Insert(0, new ListItem("--SELECT--"));
                }
            }
            catch (Exception ex)
            { string msg = ex.Message; }
            finally
            { }

        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            objBO_Finance.SCHEME_CODE = cmbx_schemecode.SelectedValue;

            objBO_Finance.FDate = DateTime.ParseExact(txtformdate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture); //Convert.ToDateTime(txtformdate.Text);
            objBO_Finance.TDate = DateTime.ParseExact(txttodate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);//Convert.ToDateTime(txttodate.Text);


            DataTable dt = new DataTable();
            dt = objBL_Finance.LoanCollectionForm(objBO_Finance);
            GV_LOANSCHEMEWISE.DataSource = dt;
            GV_LOANSCHEMEWISE.DataBind();
            Session["dtLoanSchemeWise"] = dt;
        }

        protected void btndownload_Click(object sender, EventArgs e)
        {
            if (cmbx_schemecode.SelectedValue != "")
            {
                builder = new SqlConnectionStringBuilder(strConn);
                ReportDocument crystalReport = new ReportDocument();
                DataTable dt = new DataTable();

                dt = (DataTable)System.Web.HttpContext.Current.Session["dtLoanSchemeWise"];

                if (dt.Rows.Count > 0)
                {
                    Response.Clear();
                    crystalReport = new ReportDocument();

                    crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptLoanCollectSchemeWise.rpt"));
                    crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptLoanCollectSchemeWise.rpt");

                    crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                    crystalReport.SetDataSource(dt);
                    //crystalReport.SetDataSource(ds);

                    crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "LoanCollectSchemeWise");

                }
            }
        }
    }
}
