using System;
using System.Data;
using BusinessObject;
using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iBankingSolution.Transaction
{
    public partial class frmShareDividendCalculation : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        DataTable dtledger;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtpkr_DateAsOn.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnShow_Click(object sender , EventArgs e)
        {
            FillReport();
            DataTable dt = (DataTable)Session["dtDividendCal"];
            //GridFixedDtlList.Visible = true;
            //GridFixedDeposit.DataSource = dt;
            //GridFixedDeposit.DataBind();
            RepCCListShare.Visible = true;
            RepCCListShare.DataSource = dt;
            RepCCListShare.DataBind();
        }

        protected void GridFixedDeposit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtDividendCal"];
            //GridFixedDeposit.PageIndex = e.NewPageIndex;
            //GridFixedDeposit.DataSource = dt;
            //GridFixedDeposit.DataBind();
        }
        protected void FillReport()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.ACTYPE = "sh";
            objBO_Finance.AsOnDate = DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DataTable dtDividendCal = new DataTable();
            dtDividendCal = objBL_Finance.ShareDividendCalculation(objBO_Finance, out SQLError);

            foreach (DataRow dr in dtDividendCal.Rows)
            {

                double Balance = Convert.ToDouble(dr["BALANCE"]);
                double Roi = Convert.ToDouble(txtPercentage.Text);
                double MinAmt = Convert.ToDouble(txtMinAmt.Text);

                if (Balance >= MinAmt)
                {
                    dr["Divident_Amt"] = Math.Round((Balance * Roi) / 100);
                }
            }
            Session["dtDividendCal"] = dtDividendCal;
        }
    }
}