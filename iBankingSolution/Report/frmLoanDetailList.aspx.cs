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
using Microsoft.VisualBasic;
using iBankingSolution.Transaction;

namespace iBankingSolution.Report
{
    public partial class frmLoanDetailList : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        DataTable dtledger;
        frmLoan evcon = new frmLoan();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLoanScheme();
                dtpkr_DateAsOn.Text = (DateTime.Now).ToString("dd/MM/yyyy");
                objBO_Finance.Flag = 1;
                DataTable dtBranch = objBL_Finance.GetBranchDetails(objBO_Finance, out SQLError);
                //cmbx_Branch.DataSource = dtBranch;
                //cmbx_Branch.DataBind();
                //cmbx_Branch.SelectedValue = Session["BranchID"].ToString();
            }
        }
        protected void BindLoanScheme()
        {
            try
            {

                objBO_Finance.Flag = 6;
                objBO_Finance.CUST_ID = null;
                dtledger = objBL_Finance.GetLoanSchemeMasterRecords(objBO_Finance, out SQLError);

                if (dtledger.Rows.Count > 0)
                {
                    cmbx_Ledger.DataSource = dtledger;
                    cmbx_Ledger.DataValueField = "SCHEME_CODE";
                    cmbx_Ledger.DataTextField = "SCHEME_NAME";
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

        protected void cmbx_Ledger_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand("Proc_GetLoanSchemeLedgerCode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SchemeCode", Convert.ToString(cmbx_Ledger.SelectedValue));
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

            if (txtLedgCode.Text != "")
            {
                btnShow.Visible = true;
            }
            else
            {

                btnShow.Visible = false;

            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (txtLedgCode.Text != "")
            {
                DataSet1 ds = new DataSet1();
                DateTime LASTCALDT;
                DateTime ODDT;
                DateTime ODDT1;
                Int32 DATEDIFF = 0;
                Int32 DAYSDIFF = 0;
                decimal roi = 0;
                decimal BAL = 0;
                decimal intr = 0;
                int CNTR = 0;

                decimal intdue = 0;
                DateTime dtDate = DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                objBO_Finance.Flag = 1;
                objBO_Finance.SCHEME_CODE = cmbx_Ledger.SelectedValue;
                objBO_Finance.LDG_CODE = txtLedgCode.Text;
                DataTable dtDetailsLoan = new DataTable();
                objBO_Finance.AsOnDate = DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                dtDetailsLoan = objBL_Finance.LoanDetailListReports(objBO_Finance, out SQLError);

                //dtDetailsLoan.Columns.Add("Interset", typeof(System.Decimal));

                //dtDetailsLoan.Columns.Add("Principal", typeof(System.Decimal));

                string intFind = "", Errno2 = "";
                string RepayMode = "";
                int AcNo = 0;
                //DateTime Coldt = "";
                DateTime Coldt = DateTime.ParseExact(dtpkr_DateAsOn.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string[] arr = new string[] { };

                foreach (DataRow row in dtDetailsLoan.Rows)
                {
                    //if (Convert.ToString(dtDetailsLoan.Rows[CNTR]["RepayMode"]) == "MONTHLY" && Convert.ToString(dtDetailsLoan.Rows[CNTR]["ODPR"]) == "NO OD")
                    //{
                     objBO_Finance.Flag = 1;
                     objBO_Finance.SL_CODE = dtDetailsLoan.Rows[CNTR]["AcNo"];
                     objBO_Finance.CollectionDate = dtDate;

                    RepayMode = Convert.ToString(dtDetailsLoan.Rows[CNTR]["RepayMode"]);
                    AcNo = Convert.ToInt32(dtDetailsLoan.Rows[CNTR]["AcNo"]);

                    ////////////////////////////////////////////////////////////////////////////////////////////

                    if (RepayMode == "MONTHLY COMPOUND" || RepayMode == "QUARTERLY COMPOUND" || RepayMode == "HALF YEARLY COMPOUND" || RepayMode == "YEARLY COMPOUND")
                    {
                        Errno2 = "Loo5i";
                        intFind = evcon.MASTER_MODULE_COMPOUND(AcNo, Coldt, "d");
                    }
                    else if (RepayMode == "MONTHLY" || RepayMode == "QUARTERLY" || RepayMode == "HALF YEARLY" || RepayMode == "YEARLY")
                    {
                        Errno2 = "Loo6i";
                        intFind = evcon.MASTER_MODULE_SIMPLE(AcNo, Coldt, "d");
                    }
                    else
                    {
                        Errno2 = "Loo7i";
                        intFind = evcon.MASTER_MODULE_SIMPLE(AcNo, Coldt, "d");
                    }

                    Errno2 = "Loo8i";
                    arr = intFind.Split(',');
                    Errno2 = "Loo9i";

                    if (Convert.ToDecimal(arr[2]) > 0)
                    {
                        arr[1] = (Convert.ToDecimal(arr[1]) + Convert.ToDecimal(arr[0])).ToString();
                        arr[0] = "0";
                    }

                    dtDetailsLoan.Rows[CNTR]["cur_interest"] = (arr[0]).ToString();
                    dtDetailsLoan.Rows[CNTR]["od_interest"] = (arr[1]).ToString();
                    dtDetailsLoan.Rows[CNTR]["cur_prin"] = (arr[5]).ToString();
                    dtDetailsLoan.Rows[CNTR]["od_prin"] = (arr[2]).ToString();

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //    DataTable dtDueDetails = objBL_Finance.GetLoanDueDetails(objBO_Finance, out SQLError);

                    //    if (dtDueDetails.Rows.Count > 0)
                    //    {
                    //    //roi = Convert.ToDecimal(dtDueDetails.Rows[0]["NormalInterest"]);

                    //    //VfODInt = Convert.ToDecimal(dtDueDetails.Rows[0]["ODINTREST"]);
                    //    //vfBalance = Convert.ToDecimal(dtDueDetails.Rows[0]["Principal"]);
                    //    intdue = Convert.ToDecimal(dtDueDetails.Rows[0]["IntDue"]);
                    //}

                    ////if (dtDetailsLoan.Rows.Count > 0)
                    ////{
                    ////   DateTime dtime = Convert.ToDateTime(dtDetailsLoan.Rows[CNTR]["OD_DT"]);
                    ////}
                    //LASTCALDT = Convert.ToDateTime(dtDetailsLoan.Rows[CNTR]["INT_FR_CAL_DT"]);
                    ////ODDT = Convert.ToDateTime(dtDetailsLoan.Rows[CNTR]["OD_DT"]);
                    ////ODDT1 = Convert.ToDateTime(dtDetailsLoan.Rows[CNTR]["OD_DT1"]);

                    ////if (ODDT1 > ODDT)
                    ////{
                    ////    DATEDIFF = Convert.ToInt32(ODDT1 - LASTCALDT);
                    ////}
                    ////else if (ODDT > ODDT1)
                    ////{
                    ////    DATEDIFF = Convert.ToInt32(ODDT - LASTCALDT);
                    ////}
                    ////else if (ODDT == ODDT1)
                    ////{
                    ////    DATEDIFF = Convert.ToInt32(ODDT - LASTCALDT);
                    ////}

                    //DAYSDIFF = Convert.ToInt32((dtDate - LASTCALDT).TotalDays);
                    //roi = Convert.ToDecimal(dtDetailsLoan.Rows[CNTR]["ROI"]);
                    //BAL = Convert.ToDecimal(dtDetailsLoan.Rows[CNTR]["OutstandingAmt"]);
                    //intr = Math.Round(BAL * roi * DAYSDIFF / 36500);
                    ////intdue = Convert.ToDecimal(dtDetailsLoan.Rows[CNTR]["INTCURR"]);
                    //if (intdue > 0)
                    //{
                    //    intr = intr + intdue;
                    //}


                    //row["Interset"] = intr;


                    //}
                    CNTR = CNTR + 1;

                }





                Session["dtDetailsLoan"] = dtDetailsLoan;

                //int count = 0;

                //for (int i = 0; i < dtDetailsLoan.Rows.Count; i++)
                //{
                //    DataRow recRow = dtDetailsLoan.Rows[i];
                //    string a = recRow[7].ToString();
                //    if (recRow[7].ToString() == "0.00" ||recRow[7].ToString() == "0.00 ")
                //    {

                //        recRow.Delete();
                //        dtDetailsLoan.AcceptChanges();

                //    }
                //}

                if (dtDetailsLoan.Rows.Count > 0 )
                {

                    RepCCList.DataSource = dtDetailsLoan;
                    RepCCList.DataBind();

                }

                else
                {
                    RepCCList.DataSource = null;
                    RepCCList.DataBind();
                }
                Session["dtDetailsLoan"] = dtDetailsLoan;
            }
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (cmbx_Ledger.SelectedValue != "0")
            {
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["dtDetailsLoan"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToLoanDetailListReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
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

        //protected void RepCCList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    DataTable dtt = (DataTable)Session["dtDetailsLoan"];

        //    foreach (DataRow dr in dtt.Rows)
        //    {
        //        if (Convert.ToDouble(dr["OutstandingAmt"]) > 0)
        //        {
        //            RepCCList.DataSource = dtt;
        //            RepCCList.DataBind();
        //        }

        //    }
        //}
    }
}