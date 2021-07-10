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

namespace iBankingSolution.Transaction
{
    public partial class frmLoanAccountOpeningList : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            objBO_Finance.Flag = 7;
            objBO_Finance.CUST_ID = null;
            objBO_Finance.LOAN_TYPE = "l";

            DataSet dt = objBL_Finance.GetLoanAccountsDetail(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                RepCCList.DataSource = dt;
                RepCCList.DataBind();

            }
        }
        protected void lnkedit_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            Int64 iStID = Int64.Parse((item.FindControl("lblsl_code") as Label).Text);

            Response.Redirect("~/Transaction/frmLoanAcOpening.aspx?Id=" + iStID);
        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg) //Message Box For Message
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);
        }
        protected void LinkDelete_Click(object sender, EventArgs e)
        {

            objBO_Finance.Flag = 3;
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int SLCode = int.Parse((item.FindControl("lblsl_code") as Label).Text);
            objBO_Finance.SL_CODE = SLCode;

            int i = objBL_Finance.InsertUpdateDeleteLoanAccountOpening(objBO_Finance, out SQLError);
            if (i > 0)
            {
                MessageBox(this, "Delete Successfully " + SQLError);
                //message = "alert('Delete Successfully')";
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                objBO_Finance.Flag = 1;

                RepCCList.DataBind();
                //Response.Redirect("~/Transaction/frmLoanAccountOpeningList.aspx");
            }
            else
            {
                message = "alert('Not Deleted. Root Element Exists..')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }

        }
    }
}