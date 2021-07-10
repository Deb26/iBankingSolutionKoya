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
    public partial class frmItemMasterList : System.Web.UI.Page
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
            objBO_Finance.Flag = 6;
            objBO_Finance.CUST_ID = null;


            DataTable dt = objBL_Finance.GetItemMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                RepCCList.DataSource = dt;
                RepCCList.DataBind();

            }
        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int iStID = int.Parse((item.FindControl("lblItemCode") as Label).Text);

            Response.Redirect("~/Master/FrmItemMaster.aspx?Id=" + iStID);
        }

        protected void LinkDelete_Click(object sender, EventArgs e)
        {

            objBO_Finance.Flag = 3;
            objBO_Finance.SCHEME_CODE = ((Button)sender).CommandArgument;
            int i = objBL_Finance.InsertUpdateDeleteItemMaster(objBO_Finance, out SQLError);
            if (i > 0)
            {
                message = "alert('Delete Successfully')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                objBO_Finance.Flag = 1;
                RepCCList.DataSource = ViewState["ItemDetails"] = objBL_Finance.GetItemMasterRecords(objBO_Finance, out SQLError);
                RepCCList.DataBind();
            }
            else
            {
                message = "alert('Not Deleted. Root Element Exists..')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }

        }
    }
}