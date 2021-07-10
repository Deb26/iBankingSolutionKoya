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
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Web.Services;

namespace iBankingSolution.Report
{
    public partial class frmDetailsDepositReport : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        DataTable dtledger;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               
                BindLedger();
                dtpkr_DateAsOn.Text = (DateTime.Now).ToString("dd/MM/yyyy");
                objBO_Finance.Flag = 1;
                DataTable dtBranch = objBL_Finance.GetBranchDetails(objBO_Finance, out SQLError);
                //cmbx_Branch.DataSource = dtBranch;
                //cmbx_Branch.DataBind();
                //cmbx_Branch.SelectedValue = Session["BranchID"].ToString();
            }
        }
        protected void BindLedger()
        {
            try
            {

                objBO_Finance.Flag = 10;
                objBO_Finance.CUST_ID = null;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {
                    cmbx_Ledger.DataSource = dtledger;
                    cmbx_Ledger.DataValueField = "LDG_CODE";
                    cmbx_Ledger.DataTextField = "NOMENCLATURE";
                    cmbx_Ledger.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void RepCCList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                Label lbldateofopening = (Label)e.Item.FindControl("lbldateofopening");
                Label LblROI = (Label)e.Item.FindControl("LblROI");
                Label lblduration = (Label)e.Item.FindControl("lblduration");
                Label lblMaturityAmt = (Label)e.Item.FindControl("lblMaturityAmt");
                Label LblMaturityDate = (Label)e.Item.FindControl("LblMaturityDate");
                if (cmbx_AcctType.SelectedValue == "s")
                {
                    lbldateofopening.Visible = false;
                    LblROI.Visible = false;
                    lblduration.Visible = false;
                    lblMaturityAmt.Visible = false;
                    LblMaturityDate.Visible = false;

                }

            }
        }
        protected void FillReportByType()
        {
            DataSet1 ds = new DataSet1();
            DataTable dt = ds.Deposit_Details;
            objBO_Finance.Flag = 1;
            objBO_Finance.actype = cmbx_AcctType.SelectedValue;
            objBO_Finance.LDG_CODE = txtLedgCode.Text;
            DataTable dtDetailsDeposit = new DataTable();
            objBO_Finance.AsOnDate = DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (cmbx_AcctType.SelectedValue == "s" || cmbx_AcctType.SelectedValue == "sh" || cmbx_AcctType.SelectedValue == "nf" || cmbx_AcctType.SelectedValue == "sus" || cmbx_AcctType.SelectedValue == "jlg")
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

            if(cmbx_AcctType.SelectedValue == "shg")
            {
                dtDetailsDeposit = objBL_Finance.SHGDetailsDepositReports(objBO_Finance, out SQLError);  
            }
            if(cmbx_AcctType.SelectedValue == "d")
            {
                dtDetailsDeposit = objBL_Finance.HomeSavingsDetailsDepositReports(objBO_Finance, out SQLError);
            }

            if (cmbx_AcctType.SelectedValue == "cc")
            {
                dtDetailsDeposit = objBL_Finance.DepositCirtificateDetailsReports(objBO_Finance, out SQLError);

                
                foreach (DataRow dr in dtDetailsDeposit.Rows)
                {
                    
                    double DepositAmt = Convert.ToDouble(dr["BALANCE"]);
                    double RoI = Convert.ToDouble(dr["ROI"]);

                    DateTime OpeningDate = Convert.ToDateTime(dr["Opening Date"]);
                    double DateDiffInDays = (DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) - OpeningDate).TotalDays;

                    int NoOfQuarter = Convert.ToInt32(Math.Floor(DateDiffInDays / 90));
                    int RemainingDays = Convert.ToInt32(DateDiffInDays % 90);
                    double QuarterInterest = DepositAmt * Math.Pow((1 + RoI / 400), NoOfQuarter);
                    double RemainingInterest = ((QuarterInterest) * RoI * RemainingDays) / 36500;
                    //dr["Interest"] = string.Format("{0:0.00}", Math.Round(QuarterInterest + RemainingInterest) - DepositAmt);
                    dr["Interest"] =   Math.Round(QuarterInterest + RemainingInterest) - DepositAmt;
                }
            }
            if (cmbx_AcctType.SelectedValue == "r")
            {
                dtDetailsDeposit = objBL_Finance.RecurringDepositeDetailsReports(objBO_Finance, out SQLError);


                foreach (DataRow dr in dtDetailsDeposit.Rows)
                {
                     
                    double vfTotAmt = Convert.ToDouble(dr["TotAmt"]);
                    double DepositAmt = Convert.ToDouble(dr["BALANCE"]);

                    if (vfTotAmt == 0 && DepositAmt == 0)
                    {
                        dr["Interest"] = 0;
                    }
                    else
                    {
                        double RoI = Convert.ToDouble(dr["ROI"]);
                        double TotInstPaid = 0, TotIns = 0, TotAmt = 0, TotInt = 0;

                        TotInstPaid = vfTotAmt / DepositAmt;

                        TotIns = TotInstPaid + 1;
                        TotAmt = TotIns * TotInstPaid;
                        TotInt = Math.Round(vfTotAmt * TotIns * RoI / 2400);
                        dr["Interest"] = TotInt;
                    }
                   
                }
            }

            //DataTable dtDetailsDeposit = objBL_Finance.DetailsDepositReports(objBO_Finance, out SQLError);


            //foreach (DataRow dr in dtDetailsDeposit.Rows)
            //{

            //    if (cmbx_AcctType.SelectedValue == "s" || cmbx_AcctType.SelectedValue == "jlg" || cmbx_AcctType.SelectedValue == "shg" || cmbx_AcctType.SelectedValue == "sus" || cmbx_AcctType.SelectedValue == "nf")
            //    {
            //        //dtDetailsDeposit.Columns.Remove("Opening Date");
            //        //dtDetailsDeposit.Columns.Remove("R.O.I.(%)");
            //        //dtDetailsDeposit.Columns.Remove("Duration");
            //        //dtDetailsDeposit.Columns.Remove("Maturity Amt");
            //        //dtDetailsDeposit.Columns.Remove("Maturity Date");
            //        Session["dtDetailsDeposit"] = dtDetailsDeposit;

            //    }

            //    //double vfTotAmt = Convert.ToDouble(dr["TotAmt"]);
            //    //double DepositAmt = Convert.ToDouble(dr["DEPOSIT_AMOUNT"]);
            //    //double RoI = Convert.ToDouble(dr["ROI"]);
            //    //double TotInstPaid = 0, TotIns = 0, TotAmt = 0, TotInt = 0;
            //    //DateTime OpeningDate = Convert.ToDateTime(dr["Opening Date"]);
            //    //double DateDiffInDays = (Convert.ToDateTime(dtpkr_DateAsOn.Text) - OpeningDate).TotalDays;
            //    //if (cmbx_AcctType.SelectedValue == "r")
            //    //{
            //    //    TotInstPaid = vfTotAmt / DepositAmt;
            //    //    TotIns = TotInstPaid + 1;
            //    //    TotAmt = TotIns * TotInstPaid;
            //    //    TotInt = Math.Round(vfTotAmt * TotIns * RoI / 2400);
            //    //    dr["Interest"] = string.Format("{0:0.00}", TotInt);
            //    //}
            //    //if (cmbx_AcctType.SelectedValue == "cc")
            //    //{
            //    //    int NoOfQuarter = Convert.ToInt32(Math.Floor(DateDiffInDays / 90));
            //    //    int RemainingDays = Convert.ToInt32(DateDiffInDays % 90);
            //    //    double QuarterInterest = DepositAmt * Math.Pow((1 + RoI / 400), NoOfQuarter);
            //    //    double RemainingInterest = ((QuarterInterest) * RoI * RemainingDays) / 36500;
            //    //    dr["Interest"] = string.Format("{0:0.00}", Math.Round(QuarterInterest + RemainingInterest) - DepositAmt);
            //    //}
            //}


            //dtDetailsDeposit.AsEnumerable().ToList().ForEach(dr => dr.SetField("Name of the A/c Holder", Convert.ToString(dr["Name of the A/c Holder"]).Replace("&", "&amp;")));
            //dtDetailsDeposit.Columns.Remove("DEPOSIT_AMOUNT");
            //dtDetailsDeposit.Columns.Remove("TotAmt");
            Session["dtDetailsDeposit"] = dtDetailsDeposit;


        }
        private void FillReportByLdgCode()
        {
            DataSet1 ds = new DataSet1();
            DataTable dt = ds.Subledger_Details;
            //objBO_Finance.Flag = 1;
            objBO_Finance.LDG_CODE = cmbx_Ledger.SelectedValue;
            objBO_Finance.AsOnDate = DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DataTable dtDetailsDeposit = objBL_Finance.SubLedgerDetailsReports(objBO_Finance, out SQLError);
            Session["ReportByLdgCode"] = dtDetailsDeposit;
            //table.DataSource = dt;

        }

        
        protected void btnShow_Click(object sender, EventArgs e)
        {
             
            if (cmbx_AcctType.SelectedValue != "0")
            {
                FillReportByType();
                DataTable dt = (DataTable)Session["dtDetailsDeposit"];
                if (cmbx_AcctType.SelectedValue == "s" || cmbx_AcctType.SelectedValue == "d" || cmbx_AcctType.SelectedValue == "shg" || cmbx_AcctType.SelectedValue == "sh" || cmbx_AcctType.SelectedValue == "nf" || cmbx_AcctType.SelectedValue == "sus" || cmbx_AcctType.SelectedValue == "jlg")
                {
                    GridFixedDtlList.Visible = false;
                    RepeaterControls.Visible = true;
                    RepCCList.DataSource = dt;
                    RepCCList.DataBind();
                }
                else if (cmbx_AcctType.SelectedValue == "fd" || cmbx_AcctType.SelectedValue == "cc" || cmbx_AcctType.SelectedValue == "r" || cmbx_AcctType.SelectedValue == "mis")
                {
                    GridFixedDtlList.Visible = true;
                    RepeaterControls.Visible = false;
                    GridFixedDeposit.DataSource = dt;
                    GridFixedDeposit.DataBind();
                }

                else if(cmbx_AcctType.SelectedValue == "shg")
                {
                    GridFixedDeposit.DataSource = dt;
                    GridFixedDeposit.DataBind();

                }

                
                Session["Reporttype"] = cmbx_AcctType.SelectedValue;
            }
            else
            {
                FillReportByLdgCode();
                DataTable dt1 = (DataTable)Session["ReportByLdgCode"];


            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
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

        protected void cmbx_Ledger_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand("Proc_GetSchemeLedgerCode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SchemeName", Convert.ToString(cmbx_Ledger.SelectedValue));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                con.Open();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtLedgCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["ldg_code"]);
                }
                else
                {
                    txtLedgCode.Text = "0";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                //write error message
            }

            if (cmbx_AcctType.SelectedValue != "0")
            {
                btnShow.Visible = true;
            }
            else
            {

                btnShow.Visible = false;
                //FillReportByLdgCode();
                //DataTable dt1 = (DataTable)Session["ReportByLdgCode"];
            }
        }

        protected void cmbx_AcctType_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand("Proc_GetScheme", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SchemeType", Convert.ToString(cmbx_AcctType.SelectedValue));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                con.Open();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbx_Ledger.DataSource = ds;
                    cmbx_Ledger.DataTextField = "scheme";
                    cmbx_Ledger.DataValueField = "scheme";
                    cmbx_Ledger.DataBind();
                }
                else
                {
                    cmbx_Ledger.DataSource = null;
                    cmbx_Ledger.DataBind();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                //write error message
            }
        }

        protected void GridFixedDeposit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtDetailsDeposit"];
            GridFixedDeposit.PageIndex = e.NewPageIndex;
            GridFixedDeposit.DataSource = dt;
            GridFixedDeposit.DataBind();
        }

        //protected void RepCCList_PreRender(object sender, EventArgs e)
        //{
        //    foreach (RepeaterItem item in RepCCList.Items)
        //    {
        //        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        //        {

        //            Label lbldateofopening = (Label)item.FindControl("lbldateofopening");
        //            Label LblROI = (Label)item.FindControl("LblROI");
        //            Label lblduration = (Label)item.FindControl("lblduration");
        //            Label lblMaturityAmt = (Label)item.FindControl("lblMaturityAmt");
        //            Label LblMaturityDate = (Label)item.FindControl("LblMaturityDate");
        //            if (cmbx_AcctType.SelectedValue == "s")
        //            {
        //                lbldateofopening.Visible = false;
        //                LblROI.Visible = false;
        //                lblduration.Visible = false;
        //                lblMaturityAmt.Visible = false;
        //                LblMaturityDate.Visible = false;

        //            }

        //        }
        //    }
        //}
    }
}