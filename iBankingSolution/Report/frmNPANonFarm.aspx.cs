using System;
using BusinessObject;
using BLL;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Configuration;

namespace iBankingSolution.Report
{
    public partial class frmNPANonFarm : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        DataTable dt = new DataTable();
        string SQLError = string.Empty;
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetScheme();
                dtpkr_AsonDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        protected void GetScheme()
        {
            objBO_Finance.Flag = 12;

            DataTable dt = objBL_Finance.GetLoanSchemeMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_SelectScheme.DataSource = dt;
                cmbx_SelectScheme.DataTextField = "SCHEME";
                cmbx_SelectScheme.DataValueField = "SCHEME_CODE";
                cmbx_SelectScheme.DataBind();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            FillReportByType();
        }

        protected void FillReportByType()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.SCHEME_CODE = cmbx_SelectScheme.SelectedValue;
            string dta = dtpkr_AsonDate.Text;
            objBO_Finance.AsOnDate = DateTime.ParseExact(dta, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DataSet dt = objBL_Finance.GenerateReportForFarm(objBO_Finance, out SQLError);

            foreach (DataRow dr in dt.Tables[0].Rows)
            {

                DateTime ODDT = Convert.ToDateTime(dr["OD_DT"]);
                //DateTime ODDT1 = Convert.ToDateTime(dr["OD_DT1"]);
                //DateTime ODDT1 = Convert.ToDateTime(Convert.ToDateTime(string.IsNullOrEmpty(Convert.ToDateTime(dr["OD_DT1"]))) ? (DateTime?)null : Convert.ToDateTime(dr["OD_DT1"]));
                double Amount = Convert.ToDouble(dr["OutstandingAmt"]);
                double ROI = Convert.ToDouble(dr["ROI"]);
                double OD_ROI = Convert.ToDouble(dr["OD_ROI"]);

                DateTime frdt = Convert.ToDateTime(dr["INT_FR_CAL_DT"]);

                DateTime issuedt = Convert.ToDateTime(dr["ISSUE_DATE"]);
                DateTime issuedt1 = Convert.ToDateTime(dr["ISSUE_DATE1"]);

                if (issuedt > issuedt1)
                {
                    dr["ISSUEDT"] = issuedt;
                }
                else if (issuedt1 > issuedt)
                {
                    dr["ISSUEDT"] = issuedt1;
                }
                else if (issuedt == issuedt1)
                {
                    dr["ISSUEDT"] = issuedt1;
                }

                string dtc = dtpkr_AsonDate.Text;
                DateTime tdt = DateTime.ParseExact(dtc, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                double OD_DateDiffInDays = (Convert.ToDateTime(tdt) - Convert.ToDateTime(frdt)).TotalDays;

                dr["overduedt"] = ODDT.AddDays(90);

                DateTime ot = ODDT.AddDays(90);
                double DateDiffInDays = Convert.ToDouble((Convert.ToDateTime(frdt) - Convert.ToDateTime(ODDT)).TotalDays);

                dr["Interest"] = Math.Abs(Math.Round((Amount * ROI * DateDiffInDays) / 36500));
                dr["drIntOverDue"] = Math.Abs(Math.Round((Amount * OD_ROI * OD_DateDiffInDays) / 36500));

                double dtdiff = Convert.ToDouble((Convert.ToDateTime(ot) - Convert.ToDateTime(tdt)).TotalDays);

                DateTime three = ot.AddYears(3);
                double dtthree = Convert.ToDouble((Convert.ToDateTime(three) - Convert.ToDateTime(ot)).TotalDays);

                DateTime four = ot.AddYears(4);
                double dtfour = Convert.ToDouble((Convert.ToDateTime(four) - Convert.ToDateTime(ot)).TotalDays);

                DateTime six = ot.AddYears(6);
                double dtsix = Convert.ToDouble((Convert.ToDateTime(six) - Convert.ToDateTime(ot)).TotalDays);

                if (90 > dtdiff)
                {
                    dr["UptoDays"] = Amount;
                }
                else if (90 <= dtdiff && dtdiff < dtthree)
                {
                    dr["uptothree"] = Amount;
                }
                else if (dtthree <= dtdiff && dtdiff < dtfour)
                {
                    dr["D1"] = Amount;
                }
                else if (dtfour <= dtdiff && dtdiff < dtsix)
                {
                    dr["D2"] = Amount;
                }
                else if (dtdiff >= dtsix)
                {
                    dr["D3"] = Amount;
                }

                //if (ODDT1 > ODDT)
                //{
                //    dr["overduedt"] = ODDT1.AddDays(90);

                //    DateTime ot = ODDT1.AddDays(90);
                //    double DateDiffInDays = Convert.ToDouble((Convert.ToDateTime(frdt) - Convert.ToDateTime(ODDT1)).TotalDays);

                //    dr["Interest"] = Math.Abs(Math.Round((Amount * ROI * DateDiffInDays) / 36500));
                //    dr["drIntOverDue"] = Math.Abs(Math.Round((Amount * OD_ROI * OD_DateDiffInDays) / 36500));

                //    double dtdiff = Convert.ToDouble((Convert.ToDateTime(ot) - Convert.ToDateTime(tdt)).TotalDays);


                //    DateTime three = ot.AddYears(3);
                //    double dtthree = Convert.ToDouble((Convert.ToDateTime(three) - Convert.ToDateTime(ot)).TotalDays);

                //    DateTime four = ot.AddYears(4);
                //    double dtfour = Convert.ToDouble((Convert.ToDateTime(four) - Convert.ToDateTime(ot)).TotalDays);

                //    DateTime six = ot.AddYears(6);
                //    double dtsix = Convert.ToDouble((Convert.ToDateTime(six) - Convert.ToDateTime(ot)).TotalDays);


                //    if (90 > dtdiff)
                //    {
                //        dr["UptoDays"] = Amount;
                //    }
                //    else if (90 <= dtdiff && dtdiff < dtthree)
                //    {
                //        dr["uptothree"] = Amount;
                //    }
                //    else if (dtthree <= dtdiff && dtdiff < dtfour)
                //    {
                //        dr["D1"] = Amount;
                //    }
                //    else if (dtfour <= dtdiff && dtdiff < dtsix)
                //    {
                //        dr["D2"] = Amount;
                //    }
                //    else if (dtdiff >= dtsix)
                //    {
                //        dr["D3"] = Amount;
                //    }

                //}
                //else
                //{
                //    dr["overduedt"] = ODDT.AddDays(90);

                //    DateTime ot = ODDT.AddDays(90);
                //    double DateDiffInDays = Convert.ToDouble((Convert.ToDateTime(frdt) - Convert.ToDateTime(ODDT)).TotalDays);

                //    dr["Interest"] = Math.Abs(Math.Round((Amount * ROI * DateDiffInDays) / 36500));
                //    dr["drIntOverDue"] = Math.Abs(Math.Round((Amount * OD_ROI * OD_DateDiffInDays) / 36500));

                //    double dtdiff = Convert.ToDouble((Convert.ToDateTime(ot) - Convert.ToDateTime(tdt)).TotalDays);

                //    DateTime three = ot.AddYears(3);
                //    double dtthree = Convert.ToDouble((Convert.ToDateTime(three) - Convert.ToDateTime(ot)).TotalDays);

                //    DateTime four = ot.AddYears(4);
                //    double dtfour = Convert.ToDouble((Convert.ToDateTime(four) - Convert.ToDateTime(ot)).TotalDays);

                //    DateTime six = ot.AddYears(6);
                //    double dtsix = Convert.ToDouble((Convert.ToDateTime(six) - Convert.ToDateTime(ot)).TotalDays);

                //    if (90 > dtdiff)
                //    {
                //        dr["UptoDays"] = Amount;
                //    }
                //    else if (90 <= dtdiff && dtdiff < dtthree)
                //    {
                //        dr["uptothree"] = Amount;
                //    }
                //    else if (dtthree <= dtdiff && dtdiff < dtfour)
                //    {
                //        dr["D1"] = Amount;
                //    }
                //    else if (dtfour <= dtdiff && dtdiff < dtsix)
                //    {
                //        dr["D2"] = Amount;
                //    }
                //    else if (dtdiff >= dtsix)
                //    {
                //        dr["D3"] = Amount;
                //    }

                //}

            }


            if (dt.Tables[0].Rows.Count > 0)
            {
                RepCCList.DataSource = dt;
                RepCCList.DataBind();

            }

            DataTable dtList = new DataTable();
            dtList = dt.Tables[0];
            int X = dtList.Rows.Count;
            Session["dtNPANONFarm"] = dtList;

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            FillReportByType();

            builder = new SqlConnectionStringBuilder(strConn);
            ReportDocument crystalReport = new ReportDocument();
            DataTable dt = new DataTable();
            dt = (DataTable)System.Web.HttpContext.Current.Session["dtNPANONFarm"];
            if (dt.Rows.Count > 0)
            {
                Response.Clear();
                crystalReport = new ReportDocument();

                crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptNPAFarm.rpt"));
                crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptNPAFarm.rpt");

                crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                crystalReport.SetDataSource(dt);
                crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "NPAFarmReport");

            }

        }
    }
}