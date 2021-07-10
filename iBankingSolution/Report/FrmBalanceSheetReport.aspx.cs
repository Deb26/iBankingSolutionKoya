using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Collections;
using System.Globalization;
using iBankingSolution.Report.CrystalReports;
using System.Configuration;
using System.Data.SqlClient;
using BLL.Master;

namespace iBankingSolution.Report
{
    public partial class FrmBalanceSheetReport : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        DataTable DtTransaction;
        Decimal totalTransaction = 0;
        Decimal totalIncome = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtpkr_frmDate.Text = (DateTime.Now).ToString("dd/MM/yyyy");

            }
        }
        protected void GetTrans()
        {
            objBO_Finance.Flag = 1;




            objBO_Finance.Flag = 1;
            objBO_Finance.ReportType = "PL";
            objBO_Finance.FDate = DateTime.Now;
            objBO_Finance.TDate = DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DataTable dSetBalanceSheetDetails = objBL_Finance.GetBSReportData(objBO_Finance, out SQLError);

            if (dSetBalanceSheetDetails.Rows.Count > 0)
            {
                RepCCList.DataSource = dSetBalanceSheetDetails;
                RepCCList.DataBind();
            }
            Session["dSetBalanceSheetDetails"] = dSetBalanceSheetDetails;

            Decimal totalReceipt = 0;
            Decimal totalPayment = 0;
            Decimal totallcurren = 0;
            Decimal totalrcurren = 0;
            Decimal totallprevious = 0;
            Decimal totalrprevious = 0;

            DataTable dtt = (DataTable)Session["dSetBalanceSheetDetails"];
            if (dtt.Rows.Count > 0)
            {
                RepCCList.DataSource = dtt;
                RepCCList.DataBind();



                foreach (RepeaterItem item in RepCCList.Items)
                {

                    Label Opening = (Label)item.FindControl("LblLamt");
                    Label Receipt = (Label)item.FindControl("LblRamt");

                    Label Lcurrent = (Label)item.FindControl("lblLCurYr");
                    Label Rcurrent = (Label)item.FindControl("lblRCurYr");

                    Label Lprevious = (Label)item.FindControl("lblLPreYr");
                    Label Rprevious = (Label)item.FindControl("lblRPreYr");

                    string re = Opening.Text;
                    string re1 = Receipt.Text;

                    string re2 = Lcurrent.Text;
                    string re3 = Rcurrent.Text;

                    string re4 = Lprevious.Text;
                    string re5 = Rprevious.Text;


                    if (re == "")
                    {
                        re = "0";
                    }
                    if (re1 == "")
                    {
                        re1 = "0";
                    }
                    if (re2 == "")
                    {
                        re2 = "0";
                    }
                    if (re3 == "")
                    {
                        re3 = "0";
                    }
                    if (re4 == "")
                    {
                        re4 = "0";
                    }
                    if (re5 == "")
                    {
                        re5 = "0";
                    }

                    totalReceipt += Convert.ToDecimal(re);
                    totalPayment += Convert.ToDecimal(re1);
                    totallcurren += Convert.ToDecimal(re2);
                    totalrcurren += Convert.ToDecimal(re3);
                    totallprevious += Convert.ToDecimal(re4);
                    totalrprevious += Convert.ToDecimal(re5);


                }
                lblLeftTotal.Text = totalReceipt.ToString();
                lblRightTotal.Text = totalPayment.ToString();
                lblLeftcurrent.Text = totallcurren.ToString();
                lblRightcurrent.Text = totalrcurren.ToString();
                lblLeftprevious.Text = totallprevious.ToString();
                lblRightprevious.Text = totalrprevious.ToString();
            }


        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (dtpkr_frmDate.Text != "" && RepCCList.Items.Count > 0)
            {
                //GetTrans();
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["dSetBalanceSheetDetails"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToBalanceSheet.aspx?ID=" + ddlExportReport.SelectedItem.Text;
                    string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=0,top=0,resizable=yes');";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", s, true);
                }
                else
                {
                    message = "alert('No Data Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }

            else
            {
                string message1 = "alert('Click On Buttons to Generate!!')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            GetTrans();
            Label2.Text = "Balance Sheet";
        }
    }
}