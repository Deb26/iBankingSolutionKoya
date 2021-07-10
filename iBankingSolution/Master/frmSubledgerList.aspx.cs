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

namespace iBankingSolution.Master
{
    public partial class frmSubledgerList : System.Web.UI.Page
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
            objBO_Finance.Flag = 2;
            objBO_Finance.CUST_ID = null;

            DataTable dt = objBL_Finance.GetSubLedgerMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                RepCCList.DataSource = dt;
                RepCCList.DataBind();

            }
        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int iStID = int.Parse((item.FindControl("lblSL_CODE") as Label).Text);

            Response.Redirect("~/Master/frmSubledgerMaster.aspx?Id=" + iStID);
        }
        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            //objBO_Finance.CUST_ID = ((Button)sender).CommandArgument;
            objBO_Finance.Flag = 3;
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int iStID = int.Parse((item.FindControl("lblLedger_Code") as Label).Text);
            objBO_Finance.CUST_ID = iStID;
            int i = objBL_Finance.InsertUpdateDeleteLedgerMaster(objBO_Finance, out SQLError);
            if (i > 0)
            {

                message = "alert('Delete Successfully')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

                BindGrid();
            }
            else
            {
                message = "alert('Not Deleted. Root Element Exists..')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
        }
    }
}