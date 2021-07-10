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
    public partial class frmTrailBalance : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        DataTable DtTransaction;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                SetFromToDate();

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
                dtpkr_frmDate.Text = Convert.ToString(DtStartDt.Rows[0]["STARTDT"]);
                dtpkr_toDate.Text = Convert.ToString(DtStartDt.Rows[0]["ENDDT"]);
            }


        }

        Decimal totalReceipt = 0;
        Decimal totalPayment = 0;
        Decimal totalOpening = 0;
        Decimal totalClosing = 0;
        protected void GetTrans()
        {
            objBO_Finance.Flag = 1;


            objBO_Finance.FDate = DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.TDate = DateTime.ParseExact(dtpkr_toDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (liabilities == true)
            {
                DtTransaction = objBL_Finance.TrailBalanceReportDev(objBO_Finance);
                foreach (DataRow dr in DtTransaction.Rows)
                {
                    double profitbal = Convert.ToDouble(dr["PROFIT_BAL"]);
                    double opbal = Convert.ToDouble(dr["OPENINGBALANCE"]);

                    if (profitbal > 0)
                    {
                        dr["OPENINGBAL"] = Convert.ToString(Math.Round(profitbal)); 
                    }
                    else
                    {
                        dr["OPENINGBAL"] = Convert.ToString(Math.Round(opbal));
                    }
                }
            }

            else if (assets == true)
            {
                DtTransaction = objBL_Finance.TrailBalanceReportAssets(objBO_Finance, out SQLError);
                if (DtTransaction.Rows.Count > 0)
                {
                    foreach (DataRow dr in DtTransaction.Rows)
                    {
                        double profitbal = Convert.ToDouble(dr["OPBAL"]);
                        double opbal = Convert.ToDouble(dr["OPENINGBALANCE"]);

                        if (profitbal > 0)
                        {
                            dr["OPENINGBAL"] = Convert.ToString(Math.Round((profitbal), 2));
                        }
                        else
                        {
                            dr["OPENINGBAL"] = Convert.ToString(Math.Round((opbal), 2));
                        }
                    }
                }
            }

            if (DtTransaction.Rows.Count > 0)
            {
               

                RepCCList.DataSource = DtTransaction;
                RepCCList.DataBind();
            }

            Session["DtTransaction"] = DtTransaction;


            foreach (RepeaterItem item in RepCCList.Items)
            {
                //to get the textbox of each line
                Label Opening = (Label)item.FindControl("lblOpBal");
                Label Receipt = (Label)item.FindControl("lblReceipt");
                Label Payment = (Label)item.FindControl("lblPayment");
                Label Closing = (Label)item.FindControl("lblclosing");

                if (liabilities == true && assets == false)
                {
                    Closing.Text = Convert.ToString(Convert.ToDecimal(Opening.Text) + Convert.ToDecimal(Receipt.Text) - Convert.ToDecimal(Payment.Text));
                }
                else if (liabilities == false && assets == true)
                {
                    Closing.Text = Convert.ToString(Convert.ToDecimal(Opening.Text) - Convert.ToDecimal(Receipt.Text) + Convert.ToDecimal(Payment.Text));
                }


                string re = Opening.Text;
                string re1 = Receipt.Text;
                string trpay = Payment.Text;

                string trdeb = Closing.Text;




                //if (re == "")
                //{
                //    re = "0";
                //}
                //if (re1 == "")
                //{
                //    re1 = "0";
                //}

                //if (trpay == "")
                //{
                //    trpay = "0";
                //}
                //if (trdeb == "")
                //{
                //    trdeb = "0";
                //}

                //trdeb = Convert.ToString(Convert.ToDouble(re) + Convert.ToDouble(re1) - Convert.ToDouble(trpay));

                //convert string to decimal
                totalOpening += Convert.ToDecimal(re);
                totalReceipt += Convert.ToDecimal(re1);
                totalPayment += Convert.ToDecimal(trpay);
                totalClosing += Convert.ToDecimal(trdeb);
            }
            lblOpeningTot.Text = totalOpening.ToString();
            lblReceiptTotal.Text = totalReceipt.ToString();
            lblPaymentTotal.Text = totalPayment.ToString();
            lblClosingTot.Text = totalClosing.ToString();





        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            //Ds_TrialBalance

            if (dtpkr_frmDate.Text != "" && dtpkr_toDate.Text != "" && RepCCList.Items.Count > 0)
            {
                //GetTrans();
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["DtTransaction"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToTrialBalanceReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
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
        Boolean liabilities = false;
        Boolean assets = false;
        protected void btnShow_Click(object sender, EventArgs e)
        {
            liabilities = true;
            assets = false;
            GetTrans();
            Label2.Text = "Trail Balance of Liabilites/Income";


        }

        protected void btnShow1_Click(object sender, EventArgs e)
        {
            assets = true;
            liabilities = false;
            GetTrans();
            Label2.Text = "Trail Balance of Assets/Expenditure";

        }
    }
}