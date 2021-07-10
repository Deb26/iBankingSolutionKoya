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
    public partial class frmBRSReport : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        MyDBDataContext DBConext = new MyDBDataContext();
        DateTime YrStartDt;
        DataTable dtledger;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBranch();
                SetYearStartDate();

                dtpkr_frmDate.Text = YrStartDt.ToString("dd/MM/yyyy");
                dtpkr_toDate.Text = (DateTime.Now).ToString("dd/MM/yyyy");

                BindLedgerCode();
                BindLdgUsingName();
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

        protected void SetYearStartDate()
        {
            var curs = (from unt in DBConext.CODESANDNOs

                        select new
                        {
                            YrStDt = unt.YEAR_START_DT
                        }).FirstOrDefault();
            if (curs != null)
            {

                YrStartDt = Convert.ToDateTime(curs.YrStDt);

            }
        }

        protected void BindLedgerCode()
        {
            try
            {

                objBO_Finance.Flag = 25;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {
                    cmbx_LedgerCode.DataSource = dtledger;
                    cmbx_LedgerCode.DataValueField = "LDG_CODE";
                    cmbx_LedgerCode.DataTextField = "LDG_CODE";
                    cmbx_LedgerCode.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void BindLdgUsingName()
        {
            try
            {

                objBO_Finance.Flag = 25;
                objBO_Finance.CUST_ID = null;
                objBO_Finance.LDG_CODE = "";
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {

                    Cmbx_LedgerName.DataSource = null;
                    Cmbx_LedgerName.DataSource = dtledger;
                    Cmbx_LedgerName.DataValueField = "LDG_CODE";
                    Cmbx_LedgerName.DataTextField = "NOMENCLATURE";

                    Cmbx_LedgerName.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void cmbx_LedgerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCode();
        }

        protected void cmbx_LedgerCode_SelectedIndexChanged(object sender , EventArgs e)
        {
            BindLedgerName();
        }

        protected void BindCode()
        {
            try
            {

                objBO_Finance.Flag = 26;
                objBO_Finance.LDG_CODE = Cmbx_LedgerName.SelectedValue;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {
                    cmbx_LedgerCode.DataSource = dtledger;
                    cmbx_LedgerCode.DataValueField = "LDG_CODE";
                    cmbx_LedgerCode.DataTextField = "LDG_CODE";
                    cmbx_LedgerCode.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void BindLedgerName()
        {
            try
            {

                objBO_Finance.Flag = 26;
                objBO_Finance.CUST_ID = null;
                objBO_Finance.LDG_CODE = cmbx_LedgerCode.SelectedValue;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {

                    Cmbx_LedgerName.DataSource = null;
                    Cmbx_LedgerName.DataSource = dtledger;
                    Cmbx_LedgerName.DataValueField = "LDG_CODE";
                    Cmbx_LedgerName.DataTextField = "NOMENCLATURE";
                    Cmbx_LedgerName.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            FillReportByType();
        }

        protected void FillReportByType()
        {
            if (cmbx_Branch.SelectedValue == "0")
            {
                objBO_Finance.Flag = 1;
            }
            else
            {
                objBO_Finance.Flag = 1;
            }
            objBO_Finance.LDG_CODE = cmbx_LedgerCode.SelectedValue;
            string dta = dtpkr_frmDate.Text;
            objBO_Finance.FDate = DateTime.ParseExact(dta, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string dtaa = dtpkr_toDate.Text;
            objBO_Finance.TDate = DateTime.ParseExact(dtaa, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;

            DataSet dt = objBL_Finance.BRSDetailsReport(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    double OpeningBalance = Convert.ToDouble(dt.Tables[0].Rows[0]["OPBAL"]);
                    double RunningBalance = OpeningBalance;
                    foreach (DataRow dr in dt.Tables[0].Rows)
                    {
                        double CREDIT = Convert.ToDouble(dr["credit"]);
                        double DEBIT = Convert.ToDouble(dr["debit"]);
                        RunningBalance = Math.Round((RunningBalance + DEBIT) - CREDIT);

                        dr["Balance"] = Convert.ToString(Math.Abs(Math.Round((RunningBalance)))) + " DR. ";
                    }
                }


                RepCCList.DataSource = dt;
                RepCCList.DataBind();

            }

            DataTable dtList = new DataTable();
            dtList = dt.Tables[0];
            int X = dtList.Rows.Count;
            Session["dtBRSReport"] = dtList;

        }
    }
}