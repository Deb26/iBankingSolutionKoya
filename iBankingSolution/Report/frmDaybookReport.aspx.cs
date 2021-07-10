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

namespace iBankingSolution.Report
{
    public partial class frmDaybookReport : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;

        DataTable dtledger;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBranch();
                dtpkr_DateAsOn.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
                //objBO_Finance.Flag = 1;
                //DataTable dtBranch = objBL_Finance.GetBranchDetails(objBO_Finance, out SQLError);
                //cmbx_Branch.DataSource = dtBranch;
                //cmbx_Branch.DataBind();
                //cmbx_Branch.SelectedValue = Session["BranchID"].ToString();
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

        protected void GenerateDayBook()
        {
            if (cmbx_Branch.SelectedValue == "0")
            {
                objBO_Finance.Flag = 2;
            }
            else
            {
                objBO_Finance.Flag = 1;
            }
            DataSet1 ds = new DataSet1();
            DataTable dt = ds.Daybook_Report;
            //objBO_Finance.Flag = 1;

            string dta = dtpkr_DateAsOn.Text;
            objBO_Finance.AsOnDate = DateTime.ParseExact(dta, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;
            //objBO_Finance.AsOnDate = DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //if (!string.IsNullOrEmpty(cmbx_Branch.SelectedValue))
            //{

            //}
            DataSet dtDayBook = objBL_Finance.DayBookReport(objBO_Finance, out SQLError);

            DataTable dtbook = new DataTable();
            dtbook = dtDayBook.Tables[0];
            int X = dtbook.Rows.Count;
            Session["dtDaybook"] = dtbook;
            
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {

            if (dtpkr_DateAsOn.Text != "")
            {
                //GenerateDayBook();
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["dtDaybook"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToDayBookReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
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
        Decimal totalReceipt = 0;
        Decimal totalPayment = 0;
        Decimal totalTrReceipt = 0;
        Decimal totalTRPayment = 0;
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GenerateDayBook();
           
            DataTable dtt = (DataTable)Session["dtDaybook"];
            if (dtt.Rows.Count > 0)
            {

                RepCCList.DataSource = dtt;
                RepCCList.DataBind();

                lblopeningbal.Text = Convert.ToString(dtt.Rows[0]["OpeningBal"]);
                lblclosingBal.Text = Convert.ToString(dtt.Rows[0]["ClosingBal"]);


                foreach (RepeaterItem item in RepCCList.Items)
                {
                    //to get the textbox of each line
                    Label Pay = (Label)item.FindControl("LblAMT_Payment");
                    Label rec = (Label)item.FindControl("lblTrReceipt");
                    Label trPay = (Label)item.FindControl("lblTrPayment");
                    Label trDeb = (Label)item.FindControl("lbltramtDebit");
                    
                        


                    string re = Pay.Text;
                    string re1 = rec.Text;
                    string trpay = trPay.Text;
                    string trdeb = trDeb.Text;
                    if (re == "")
                    {
                        re = "0";
                    }
                    if (re1 == "")
                    {
                        re1 = "0";
                    }

                    if (trpay == "")
                    {
                        trpay = "0";
                    }
                    if (trdeb == "")
                    {
                        trdeb = "0";
                    }



                    //convert string to decimal
                    totalReceipt += Convert.ToDecimal(re);
                    totalPayment += Convert.ToDecimal(re1);
                    totalTrReceipt += Convert.ToDecimal(trpay);
                    totalTRPayment += Convert.ToDecimal(trdeb);
                }
                lblReceipttotal.Text = totalReceipt.ToString();
                lblPaymenttotal.Text = totalPayment.ToString();
                lbltrrecAmt.Text = totalTrReceipt.ToString();
                lbltrPayAmt.Text = totalTRPayment.ToString();
                if(lblopeningbal.Text=="")
                {
                    lblopeningbal.Text = "0";
                }
                if (lblclosingBal.Text == "")
                {
                    lblclosingBal.Text = "0";
                }
                lblGRTReceipt.Text = Convert.ToString(totalReceipt + Convert.ToDecimal(lblopeningbal.Text));
                lblGRTPayment.Text = Convert.ToString(totalPayment + Convert.ToDecimal(lblclosingBal.Text));


            }
             

        }
       
        protected void RepCCList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           

        }
    }
}