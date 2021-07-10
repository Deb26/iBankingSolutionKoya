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
    public partial class frmTradingAcReport : System.Web.UI.Page
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
                //dtpkr_frmDate.Text = (DateTime.Now).ToString("dd/MM/yyyy");

            }
        }
        protected void SetFromToDate()
        {

            DataTable DtStartDt = new DataTable();
            objBO_Finance.Flag = 1;
            DateTime dtfr;
            DateTime dtTo;

            DtStartDt = objBL_Finance.GetYearStartEndDt(objBO_Finance);

            if (DtStartDt.Rows.Count > 0)
            {
                dtpkr_frmDate.Text = Convert.ToString(DtStartDt.Rows[0]["ENDDT"]);
            }
        }
        protected void GetTrans()
        {
            objBO_Finance.Flag = 1;




            objBO_Finance.Flag = 1;
            objBO_Finance.ReportType = "PL";
            objBO_Finance.FDate = DateTime.Now;
            objBO_Finance.TDate = DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DataTable dSetTradingAcDetails = objBL_Finance.GetTRReportData(objBO_Finance, out SQLError);

            if (dSetTradingAcDetails.Rows.Count > 0)
            {
                RepCCList.DataSource = dSetTradingAcDetails;
                RepCCList.DataBind();
            }
            Session["dSetTradingAcDetails"] = dSetTradingAcDetails;
            
            Decimal totalReceipt = 0;
            Decimal totalPayment = 0;
            DataTable dtt = (DataTable)Session["dSetTradingAcDetails"];
            if (dtt.Rows.Count > 0)
            {
                RepCCList.DataSource = dtt;
                RepCCList.DataBind();



                foreach (RepeaterItem item in RepCCList.Items)
                {

                    Label Opening = (Label)item.FindControl("lblLCurYr");
                    Label Receipt = (Label)item.FindControl("lblRCurYr");


                    string re = Opening.Text;
                    string re1 = Receipt.Text;


                    if (re == "")
                    {
                        re = "0";
                    }
                    if (re1 == "")
                    {
                        re1 = "0";
                    }

                    totalReceipt += Convert.ToDecimal(re);                                                                 
                    totalPayment += Convert.ToDecimal(re1);


                }
                lblLeftTotal.Text = Math.Abs(totalReceipt).ToString();
                lblRightTotal.Text = Math.Abs(totalPayment).ToString();


                if ((totalPayment - totalReceipt) > 0)
                {
                    lblprofit.Text = Math.Abs((totalPayment - totalReceipt)).ToString();
                    lblLeftTotal.Text = Math.Abs(Math.Abs(totalReceipt) + Math.Abs(totalPayment - totalReceipt)).ToString();


                    if (lblprofit.Text != "")
                    {
                        objBO_Finance.Flag = 1;
                        objBO_Finance.Balance = Convert.ToDouble(lblprofit.Text);
                        string dta = dtpkr_frmDate.Text;
                        objBO_Finance.FDate = DateTime.ParseExact(dta, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        int i = objBL_Finance.InsertYearTradingBalance(objBO_Finance, out SQLError);
                    }
                }
                else
                {
                    lblloss.Text = Math.Abs((totalPayment - totalReceipt)).ToString();
                    lblRightTotal.Text = Math.Abs(Math.Abs(totalPayment) + Math.Abs(totalPayment - totalReceipt)).ToString();

                    if (lblloss.Text != "")
                    {
                        objBO_Finance.Flag = 2;
                        objBO_Finance.Balance = Convert.ToDouble(lblloss.Text);
                        string dta = dtpkr_frmDate.Text;
                        objBO_Finance.FDate = DateTime.ParseExact(dta, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        int i = objBL_Finance.InsertYearTradingBalance(objBO_Finance, out SQLError);
                    }
                }

            }


        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (dtpkr_frmDate.Text != "" && RepCCList.Items.Count > 0)
            {
                //GetTrans();
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["dSetTradingAcDetails"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToTradingAccount.aspx?ID=" + ddlExportReport.SelectedItem.Text;
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
            Label2.Text = "Trading Account";
        }
    }

}