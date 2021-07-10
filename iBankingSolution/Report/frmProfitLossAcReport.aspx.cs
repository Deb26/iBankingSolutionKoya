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
    public partial class frmProfitLossAcReport : System.Web.UI.Page
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

        protected void btnShow_Click(object sender, EventArgs e)
        {

            GetTrans();
            Label2.Text = "Profit and Loss Account";
        }
        protected void GetTrans()
        {
            objBO_Finance.Flag = 1;


            //objBO_Finance.FDate = DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            objBO_Finance.Flag = 1;
            objBO_Finance.ReportType = "PL";
            objBO_Finance.FDate = DateTime.Now;
            objBO_Finance.TDate = DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DataTable dSetPLDetails = objBL_Finance.GetPLReportData(objBO_Finance, out SQLError);

            if (dSetPLDetails.Rows.Count > 0)
            {
                RepCCList.DataSource = dSetPLDetails;
                RepCCList.DataBind();

                //Decimal totalReceipt = 0;
                //Decimal totalPayment = 0;
                //foreach (RepeaterItem item in RepCCList.Items)
                //{
                //    //to get the textbox of each line
                //    //Label Pay = (Label)item.FindControl("LAMT");
                //    //Label rec = (Label)item.FindControl("RAMT");
                //    lblLeftTotal.Text = Convert.ToString(dSetPLDetails.Rows[0]["LAMT"]);
                //    lblRightTotal.Text = Convert.ToString(dSetPLDetails.Rows[0]["RAMT"]);

                //    string re = lblLeftTotal.Text;
                //    string re1 = lblRightTotal.Text;


                //    if (re == "")
                //    {
                //        re = "0";
                //    }
                //    if (re1 == "")
                //    {
                //        re1 = "0";
                //    }

                //    totalReceipt += Convert.ToDecimal(re);
                //    totalPayment += Convert.ToDecimal(re1);


                //}
                //lblLeftTotal.Text = totalReceipt.ToString();
                //lblRightTotal.Text = totalPayment.ToString();
            }
            Session["dSetPLDetails"] = dSetPLDetails;

            Decimal totalReceipt = 0;
            Decimal totalPayment = 0;
            DataTable dtt = (DataTable)Session["dSetPLDetails"];
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
                }
                else
                {
                    lblloss.Text = Math.Abs((totalPayment - totalReceipt)).ToString();
                    lblRightTotal.Text = Math.Abs(Math.Abs(totalPayment) + Math.Abs(totalPayment - totalReceipt)).ToString();
                    
                }
            }

        }
        protected void RepCCList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            DataTable dtt = (DataTable)Session["dSetPLDetails"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                //Reference the Repeater Item.
                Label lefttotal = (Label)e.Item.FindControl("lblLeftTotal");
                Label Righttotal = (Label)e.Item.FindControl("lblRightTotal");
                Label lblLCurYr = (Label)e.Item.FindControl("lblLCurYr");
                Label lblRCurYr = (Label)e.Item.FindControl("lblRCurYr");
                Int32 CurYrLeftTotal = 0;
                Int32 CurYrRIGTTotal = 0;

                foreach (RepeaterItem item in RepCCList.Items)
                {

                    CurYrLeftTotal = CurYrLeftTotal + Convert.ToInt32(lblLCurYr.Text);
                    CurYrRIGTTotal = CurYrRIGTTotal + Convert.ToInt32(lblRCurYr.Text);


                }
            }

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

            if (dtpkr_frmDate.Text != "" && RepCCList.Items.Count > 0)
            {
                //GetTrans();
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["dSetPLDetails"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToProfit&LossReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
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
    }
}