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
    public partial class LoanAcctDetails : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;

        DataTable dtledger;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtpkr_DateAsOn.Text = (DateTime.Now).ToString("dd/MM/yyyy");
                objBO_Finance.Flag = 1;
                DataTable dtloan = objBL_Finance.GetsubLedgerDetailsLoan(objBO_Finance, out SQLError);
                cmbx_LoanAcNo.DataSource = dtloan;
                cmbx_LoanAcNo.DataTextField = "SL_CODE";
            }
                cmbx_LoanAcNo.DataValueField = "SL_CODE";
                cmbx_LoanAcNo.DataBind();
                
        }

        protected void GenerateLoanAccountDetails()
        {
            Decimal vfNormalInt = 0;
            Decimal VfODInt = 0;
            Decimal vfPrincipal = 0;
            DataSet1 ds = new DataSet1();
            DataTable dt = ds.proc_LoanAccountDetails;
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = cmbx_LoanAcNo.SelectedValue;
            objBO_Finance.CollectionDate = DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture); 
            DataTable dSetLoanAccountDetails = objBL_Finance.GetLoanAccountDetailLoans(objBO_Finance, out SQLError);

       

            DataTable dtDueDetails= objBL_Finance.GetLoanDueDetails(objBO_Finance, out SQLError);

            if(dtDueDetails.Rows.Count>0)
            {
                vfNormalInt = Convert.ToDecimal(dtDueDetails.Rows[0]["NormalInterest"]);

                VfODInt = Convert.ToDecimal(dtDueDetails.Rows[0]["ODINTREST"]);
                vfPrincipal = Convert.ToDecimal(dtDueDetails.Rows[0]["Principal"]);
            }

            //adding new column in the existing data table
            dSetLoanAccountDetails.Columns.Add("NormalInt", typeof(System.Decimal));
            dSetLoanAccountDetails.Columns.Add("ODInterest", typeof(System.Decimal));
            dSetLoanAccountDetails.Columns.Add("DuePrincipal", typeof(System.Decimal));

            foreach (DataRow row in dSetLoanAccountDetails.Rows)
            {
                //need to set value to NewColumn column
                row["NormalInt"] = vfNormalInt;   // or set it to some other value
                row["ODInterest"] = VfODInt;
                row["DuePrincipal"] = vfPrincipal;
            }

     

            Session["dtLoanAccountDetails"] = dSetLoanAccountDetails;



        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (cmbx_LoanAcNo.SelectedItem.Text != "")
            {
                GenerateLoanAccountDetails();
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["dtLoanAccountDetails"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToloanActDetailsReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
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