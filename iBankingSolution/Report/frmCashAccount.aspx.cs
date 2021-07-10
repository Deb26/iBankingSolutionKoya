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
using System.Configuration;
using System.Data.SqlClient;
using BLL.Master;
using iBankingSolution.Report.CrystalReports;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Runtime.InteropServices;

namespace iBankingSolution.Report
{
    public partial class frmCashAccount : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        DataTable dtledger;
        //ReportDocument rpt = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBranch();
                objBO_Finance.Flag = 1;
                //DataTable dtBranch = objBL_Finance.GetBranchDetails(objBO_Finance, out SQLError);
                //cmbx_Branch.DataSource = dtBranch;
                //cmbx_Branch.DataValueField = "BranchID";
                //cmbx_Branch.DataTextField = "BranchName";
                //cmbx_Branch.DataBind();
                //cmbx_Branch.SelectedValue = Session["BranchID"].ToString();
                dtpkr_frmDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); //Convert.ToString(Convert.ToDateTime("1 Apr " + (DateTime.Now.Month >= 4 ? DateTime.Now.Year : DateTime.Now.Year - 1)));
                dtpkr_toDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

        protected void Page_Unload(object sender, EventArgs e)
        {
            //if (rpt != null)
            //{
            //    rpt.Close();
            //    rpt.Dispose();
            //}
        }
        protected void ShowCashAccount()
        {
            try
            {
                DataSet1 ds = new DataSet1();

                DataTable dt = ds.Ds_CashAccount;
                objBO_Finance.Flag = 1;

                //objBO_Finance.FDate = Convert.ToDateTime(dtpkr_frmDate.Text);
                //objBO_Finance.TDate = Convert.ToDateTime(dtpkr_toDate.Text);

                string datedb = dtpkr_frmDate.Text;
                DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.FDate = timedb;

                string datedb1 = dtpkr_toDate.Text;
                DateTime timedb1 = DateTime.ParseExact(datedb1, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.TDate = timedb1;


                objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;
                DataTable dtCashAccount = objBL_Finance.CashAccountReport(objBO_Finance, out SQLError);
                for (int i = 0; i < dtCashAccount.Rows.Count; i++)
                {
                    dt.Rows.Add
                               (

                                   dtCashAccount.Rows[i]["gindex"],
                                   dtCashAccount.Rows[i]["group_name"],
                                   dtCashAccount.Rows[i]["ldg_code"],
                                   dtCashAccount.Rows[i]["nomenclature"],
                                   dtCashAccount.Rows[i]["receipt"],
                                   dtCashAccount.Rows[i]["pgindex"],
                                   dtCashAccount.Rows[i]["pgroup_name"],
                                   dtCashAccount.Rows[i]["pldg_code"],
                                   dtCashAccount.Rows[i]["pnomenclature"],
                                   dtCashAccount.Rows[i]["payment"],

                                   dtCashAccount.Rows[i]["LISCENCEE_NAME"],
                                   dtCashAccount.Rows[i]["LISCENCEE_ADDRESS1"],
                                   dtCashAccount.Rows[i]["OPBAL"],
                                   dtCashAccount.Rows[i]["CLBAL"],
                                   dtCashAccount.Rows[i]["FromDate"],
                                   dtCashAccount.Rows[i]["ToDate"]
                               );
                }
                //DataTable dtReceipt = dt.AsEnumerable().Where(x => Convert.ToDecimal(x["receipt"]) > 0).CopyToDataTable();
                //DataTable dtPayment = dt.AsEnumerable().Where(x => Convert.ToDecimal(x["payment"]) > 0).CopyToDataTable();
                //int RGindex = dtReceipt.AsEnumerable().Select(x => x["gindex"]).Distinct().Count();
                //int PGindex = dtPayment.AsEnumerable().Select(x => x["gindex"]).Distinct().Count();

                //int RVariance = 0, Pvariance = 0;
                //if (RGindex > PGindex)
                //{
                //    Pvariance = RGindex - PGindex;
                //}
                //else
                //{
                //    RVariance = PGindex - RGindex;
                //}

                //if (dtPayment.Rows.Count + RVariance > dtReceipt.Rows.Count + Pvariance)
                //{
                //    for (int i = dtReceipt.Rows.Count; i < dtPayment.Rows.Count + RVariance - 1; i++)
                //    {
                //        dtReceipt.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);
                //    }
                //}
                //else
                //{
                //    for (int i = dtPayment.Rows.Count; i < dtReceipt.Rows.Count + Pvariance - 1; i++)
                //    {
                //        dtPayment.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);
                //    }
                //}
                Session["dtCashAccount"] = dtCashAccount;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {

            if (dtpkr_frmDate.Text != "" && dtpkr_toDate.Text != "")
            {
                ShowCashAccount();
                //DataTable dt = new DataTable();
                //dt = (DataTable)Session["dtCashAccount"];

                //ReportDocument rpt = new ReportDocument();
                //rpt.Load(Server.MapPath(@"~/Report/CrystalReports/rptCashAccount.rpt"));
                //rpt.SetDataSource(dt);
                //CrystalReportViewer1.ReportSource = rpt;

                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["dtCashAccount"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToCashAccountReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
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

        protected void btnShow_Click(object sender, EventArgs e)
        {
            ShowCashAccount();
            DataSet dt = new DataSet();
            dt.Tables.Add((DataTable)Session["dtCashAccount"]);

            //dt = (DataSet)Session["dtCashAccount"];



            //rpt.Load(Server.MapPath(@"/Report/CrystalReports/rptCashAccount.rpt"));
            //rpt.FileName = Server.MapPath(@"/Report/CrystalReports/rptCashAccount.rpt"); 

            //rpt.SetDataSource(dt);
            RepCCList.DataSource = dt;
            RepCCList.DataBind();


            Decimal totalReceipt = 0;
            Decimal totalPayment = 0;
            Decimal totalTrReceipt = 0;
            Decimal totalTRPayment = 0;
            DataTable dtt = (DataTable)Session["dtCashAccount"];
            if (dtt.Rows.Count > 0)
            {

                RepCCList.DataSource = dtt;
                RepCCList.DataBind();

                lblopeningbal.Text = Convert.ToString(dtt.Rows[0]["OPBAL"]);
                lblclosingBal.Text = Convert.ToString(dtt.Rows[0]["CLBAL"]);

                foreach (RepeaterItem item in RepCCList.Items)
                {
                    //to get the textbox of each line
                    Label Pay = (Label)item.FindControl("lbl_payment");
                    Label rec = (Label)item.FindControl("lblrecipt");






                    string re = Pay.Text;
                    string re1 = rec.Text;


                    if (re == "")
                    {
                        re = "0";
                    }
                    if (re1 == "")
                    {
                        re1 = "0";
                    }





                    //convert string to decimal
                    totalReceipt += Convert.ToDecimal(re);
                    totalPayment += Convert.ToDecimal(re1);


                }
                lblReceipttotal.Text = totalReceipt.ToString();
                lblPaymenttotal.Text = totalPayment.ToString();


                if (lblopeningbal.Text == "")
                {
                    lblopeningbal.Text = "0";
                }
                if (lblclosingBal.Text == "")
                {
                    lblclosingBal.Text = "0";
                }
                lblGRTReceipt.Text = Convert.ToString(totalPayment + Convert.ToDecimal(lblopeningbal.Text));

                lblGRTPayment.Text = Convert.ToString(totalReceipt + Convert.ToDecimal(lblclosingBal.Text));

                //lblGRTPayment.Text = Convert.ToString((Convert.ToDecimal(lblopeningbal.Text) + totalPayment) - totalReceipt);
                
            }
        }

    }
}

        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    //if (rpt != null)
        //    //{
        //    //    rpt.Close();
        //    //    rpt.Dispose();
        //    //}
        //}
        //protected void ShowCashAccount()
        //{
        //    try
        //    {
        //        if (cmbx_Branch.SelectedValue == "0")
        //        {
        //            objBO_Finance.Flag = 2;
        //        }
        //        else
        //        {
        //            objBO_Finance.Flag = 1;
        //        }

        //        DataSet1 ds = new DataSet1();

        //        DataTable dt = ds.Ds_CashAccount;
        //        //objBO_Finance.Flag = 1;

        //        //objBO_Finance.FDate = Convert.ToDateTime(dtpkr_frmDate.Text);
        //        //objBO_Finance.TDate = Convert.ToDateTime(dtpkr_toDate.Text);

        //        string datedb = dtpkr_frmDate.Text;
        //        DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //        objBO_Finance.FDate = timedb;

        //        string datedb1 = dtpkr_toDate.Text;
        //        DateTime timedb1 = DateTime.ParseExact(datedb1, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //        objBO_Finance.TDate = timedb1;


        //        objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;
        //        DataSet dtCashAccount = objBL_Finance.CashAccountReport(objBO_Finance, out SQLError);
        //        //DataTable dtCashAccount = objBL_Finance.CashAccountReport(objBO_Finance, out SQLError);

        //        DataTable dtCash = new DataTable();
        //        dtCash = dtCashAccount.Tables[0];
        //        int X = dtCash.Rows.Count;
        //        Session["dtCashAc"] = dtCash;
        //        //for (int i = 0; i < dtCashAccount.Rows.Count; i++)
        //        //{
        //        //    dt.Rows.Add
        //        //               (

        //        //                   dtCashAccount.Rows[i]["gindex"],
        //        //                   dtCashAccount.Rows[i]["group_name"],
        //        //                   dtCashAccount.Rows[i]["ldg_code"],
        //        //                   dtCashAccount.Rows[i]["nomenclature"],
        //        //                   dtCashAccount.Rows[i]["receipt"],
        //        //                   dtCashAccount.Rows[i]["pgindex"],
        //        //                   dtCashAccount.Rows[i]["pgroup_name"],
        //        //                   dtCashAccount.Rows[i]["pldg_code"],
        //        //                   dtCashAccount.Rows[i]["pnomenclature"],
        //        //                   dtCashAccount.Rows[i]["payment"],

        //        //                   dtCashAccount.Rows[i]["LISCENCEE_NAME"],
        //        //                   dtCashAccount.Rows[i]["LISCENCEE_ADDRESS1"],
        //        //                   dtCashAccount.Rows[i]["OPBAL"],
        //        //                   dtCashAccount.Rows[i]["CLBAL"],
        //        //                   dtCashAccount.Rows[i]["FromDate"],
        //        //                   dtCashAccount.Rows[i]["ToDate"]
        //        //               );
        //        //}
        //        //DataTable dtReceipt = dt.AsEnumerable().Where(x => Convert.ToDecimal(x["receipt"]) > 0).CopyToDataTable();
        //        //DataTable dtPayment = dt.AsEnumerable().Where(x => Convert.ToDecimal(x["payment"]) > 0).CopyToDataTable();
        //        //int RGindex = dtReceipt.AsEnumerable().Select(x => x["gindex"]).Distinct().Count();
        //        //int PGindex = dtPayment.AsEnumerable().Select(x => x["gindex"]).Distinct().Count();

        //        //int RVariance = 0, Pvariance = 0;
        //        //if (RGindex > PGindex)
        //        //{
        //        //    Pvariance = RGindex - PGindex;
        //        //}
        //        //else
        //        //{
        //        //    RVariance = PGindex - RGindex;
        //        //}

        //        //if (dtPayment.Rows.Count + RVariance > dtReceipt.Rows.Count + Pvariance)
        //        //{
        //        //    for (int i = dtReceipt.Rows.Count; i < dtPayment.Rows.Count + RVariance - 1; i++)
        //        //    {
        //        //        dtReceipt.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    for (int i = dtPayment.Rows.Count; i < dtReceipt.Rows.Count + Pvariance - 1; i++)
        //        //    {
        //        //        dtPayment.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);
        //        //    }
        //        //}
        //        Session["dtCashAccount"] = dtCashAccount;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //    }
        //}
        //protected void btnDownload_Click(object sender, EventArgs e)
        //{

        //    if (dtpkr_frmDate.Text != "" && dtpkr_toDate.Text != "")
        //    {
        //        ShowCashAccount();
        //        DataTable dt = new DataTable();
        //        //dt = (DataTable)Session["dtCashAccount"];
        //        dt = (DataTable)Session["dtCashAc"];

        //        ReportDocument rpt = new ReportDocument();
        //        rpt.Load(Server.MapPath(@"~/Report/CrystalReports/rptCashAccount.rpt"));
        //        rpt.SetDataSource(dt);
        //        //CrystalReportViewer1.ReportSource = rpt;

        //        String message = String.Empty;
        //        if (System.Web.HttpContext.Current.Session["dtCashAc"] != null)
        //        {
        //            string url = "../ExportDownloadPage/ExportToCashAccountReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
        //            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=0,top=0,resizable=yes');";
        //            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", s, true);
        //        }
        //        else
        //        {
        //            message = "alert('No Data Found')";
        //            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //        }
        //    }
        //}

        //Decimal totalReceipt = 0;
        //Decimal totalPayment = 0;
        //Decimal totalTrReceipt = 0;
        //Decimal totalTRPayment = 0;

        //protected void btnShow_Click(object sender, EventArgs e)
        //{
        //    //ShowCashAccount();
        //    //DataSet dt = new DataSet();
        //    //dt.Tables.Add((DataTable)Session["dtCashAccount"]);

        //    ////dt = (DataSet)Session["dtCashAccount"];



        //    ////rpt.Load(Server.MapPath(@"/Report/CrystalReports/rptCashAccount.rpt"));
        //    ////rpt.FileName = Server.MapPath(@"/Report/CrystalReports/rptCashAccount.rpt"); 

        //    ////rpt.SetDataSource(dt);
        //    //RepCCList.DataSource = dt;
        //    //RepCCList.DataBind();


        //    ShowCashAccount();

        //    DataTable dtt = (DataTable)Session["dtCashAc"];
        //    if (dtt.Rows.Count > 0)
        //    {

        //        RepCCList.DataSource = dtt;
        //        RepCCList.DataBind();

        //        lblopeningbal.Text = Convert.ToString(dtt.Rows[0]["OpeningBal"]);
        //        lblclosingBal.Text = Convert.ToString(dtt.Rows[0]["ClosingBal"]);

        //        foreach (RepeaterItem item in RepCCList.Items)
        //        {
        //            //to get the textbox of each line
        //            Label Pay = (Label)item.FindControl("lbl_payment");
        //            Label rec = (Label)item.FindControl("lblrecipt");






        //            string re = Pay.Text;
        //            string re1 = rec.Text;


        //            if (re == "")
        //            {
        //                re = "0";
        //            }
        //            if (re1 == "")
        //            {
        //                re1 = "0";
        //            }





        //            //convert string to decimal
        //            totalReceipt += Convert.ToDecimal(re);
        //            totalPayment += Convert.ToDecimal(re1);


        //        }
        //        lblReceipttotal.Text = totalReceipt.ToString();
        //        lblPaymenttotal.Text = totalPayment.ToString();


        //        if (lblopeningbal.Text == "")
        //        {
        //            lblopeningbal.Text = "0";
        //        }
        //        if (lblclosingBal.Text == "")
        //        {
        //            lblclosingBal.Text = "0";
        //        }
        //        lblGRTReceipt.Text = Convert.ToString(totalPayment + Convert.ToDecimal(lblopeningbal.Text));

        //        //lblGRTPayment.Text = Convert.ToString(totalPayment + Convert.ToDecimal(lblclosingBal.Text));

        //        lblGRTPayment.Text = Convert.ToString((Convert.ToDecimal(lblopeningbal.Text) + totalPayment) - totalReceipt);

        //    }
        //}
    
