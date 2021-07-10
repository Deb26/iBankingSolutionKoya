using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iBankingSolution.Report.CrystalReports;
using BusinessObject;
using BLL;
using System.Data;
using System.Data.SqlClient;

namespace iBankingSolution.Report
{
    public partial class frmInvestmentDetailsDepositReport : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        DataTable dtledger;
        protected void Page_Load(object sender, EventArgs e)
        {
            dtpkr_DateAsOn.Text = (DateTime.Now).ToString("dd/MM/yyyy");
            objBO_Finance.Flag = 1;
            //DataTable dtBranch = objBL_Finance.GetBranchDetails(objBO_Finance, out SQLError);
        }

        protected void FillReportByType() //Fill Report//
        {
            DataSet1 ds = new DataSet1();
            DataTable dt = ds.Deposit_Details;
            objBO_Finance.Flag = 2;
            objBO_Finance.actype = cmbx_AcctType.SelectedValue;
            objBO_Finance.LDG_CODE = ""; //txtLedgCode.Text;
            DataTable dtDetailsDeposit = new DataTable();
            objBO_Finance.AsOnDate = DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (cmbx_AcctType.SelectedValue == "s" || cmbx_AcctType.SelectedValue == "sh")
            {
                dtDetailsDeposit = objBL_Finance.SavingsDetailsDepositReports(objBO_Finance, out SQLError);
            }
            if (cmbx_AcctType.SelectedValue == "fd")
            {
                dtDetailsDeposit = objBL_Finance.FixedDepositDetailsReports(objBO_Finance, out SQLError);
            }
            if (cmbx_AcctType.SelectedValue == "mis")
            {
                dtDetailsDeposit = objBL_Finance.MisDetailsListReports(objBO_Finance, out SQLError);
            }

            if (cmbx_AcctType.SelectedValue == "cc")
            {
                dtDetailsDeposit = objBL_Finance.DepositCirtificateDetailsReports(objBO_Finance, out SQLError);


                foreach (DataRow dr in dtDetailsDeposit.Rows)
                {
                    double DepositAmt = Convert.ToDouble(dr["FACE_VALUE"]);
                    double RoI = Convert.ToDouble(dr["ROI"]);


                    DateTime OpeningDate = Convert.ToDateTime(dr["DATE_OF_OPENING"]);
                    
                    double DateDiffInDays = (DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) - OpeningDate).TotalDays;

                    int NoOfQuarter = Convert.ToInt32(Math.Floor(DateDiffInDays / 90));
                    int RemainingDays = Convert.ToInt32(DateDiffInDays % 90);
                    double QuarterInterest = DepositAmt * Math.Pow((1 + RoI / 400), NoOfQuarter);
                    double RemainingInterest = ((QuarterInterest) * RoI * RemainingDays) / 36500;
                    dr["Interest"] = Math.Round(QuarterInterest + RemainingInterest) - DepositAmt;

                    
                }
            }
            if (cmbx_AcctType.SelectedValue == "r")
            {
                dtDetailsDeposit = objBL_Finance.RecurringDepositeDetailsReports(objBO_Finance, out SQLError);


                foreach (DataRow dr in dtDetailsDeposit.Rows)
                {

                    double vfTotAmt = Convert.ToDouble(dr["TotAmt"]);
                    double DepositAmt = Convert.ToDouble(dr["BALANCE"]);
                    double RoI = Convert.ToDouble(dr["ROI"]);
                    double TotInstPaid = 0, TotIns = 0, TotAmt = 0, TotInt = 0;

                    TotInstPaid = vfTotAmt / DepositAmt;
                    TotIns = TotInstPaid + 1;
                    TotAmt = TotIns * TotInstPaid;
                    TotInt = Math.Round(vfTotAmt * TotIns * RoI / 2400);
                    dr["Interest"] = TotInt;


                }
            }


            Session["dtDetailsDeposit"] = dtDetailsDeposit;


        }
        
        protected void btnShow_Click(object sender, EventArgs e) // Button Show 
        {

            if (cmbx_AcctType.SelectedValue != "0")
            {
                FillReportByType();
                DataTable dt = (DataTable)Session["dtDetailsDeposit"];
                if (cmbx_AcctType.SelectedValue == "s" ||  cmbx_AcctType.SelectedValue == "sh")
                {
                    GridFixedDtlList.Visible = false;
                    //RepeaterControls.Visible = true;
                    //RepCCList.DataSource = dt;
                    //RepCCList.DataBind();
                }
                else if (cmbx_AcctType.SelectedValue == "fd" || cmbx_AcctType.SelectedValue == "cc" || cmbx_AcctType.SelectedValue == "r" || cmbx_AcctType.SelectedValue == "mis")
                {
                    GridFixedDtlList.Visible = true;
                    // RepeaterControls.Visible = false;
                     GridFixedDeposit.DataSource = dt;
                    GridFixedDeposit.DataBind();
                }

                else if (cmbx_AcctType.SelectedValue == "current" || cmbx_AcctType.SelectedValue == "others")
                {
                    GridFixedDeposit.DataSource = dt;
                    GridFixedDeposit.DataBind();

                }


                Session["Reporttype"] = cmbx_AcctType.SelectedValue;
            }
            else
            {
                //FillReportByLdgCode();
                DataTable dt1 = (DataTable)Session["ReportByLdgCode"];


            }
        }
        protected void btnDownload_Click(object sender, EventArgs e) //For Download Report//
        {

            if (cmbx_AcctType.SelectedValue != "0")
            {
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["dtDetailsDeposit"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToDetailsDepositReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
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
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["ReportByLdgCode"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToDetailsDepositLedgerReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
                    string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=0,top=0,resizable=yes');";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", s, true);
                }
                else
                {
                    message = "alert('No Data Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
        }
    }
}


        