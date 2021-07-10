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
    public partial class frmItemClassList : System.Web.UI.Page
    {
        MyDBDataContext dbContext = new MyDBDataContext();
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!IsPostBack)
            {
                GetItemClass();
            }
        }


        protected void GetItemClass()
        {
            var Class = (from cod in dbContext.ITEM_CLASSes

                         select new
                         {
                             id = cod.categoryId,
                             Name = cod.NAME


                         }).ToList();

            if (Class.Count>0)
            {
                RepCCList.DataSource = Class.ToList();
                RepCCList.DataBind();
            }
        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int iStID = int.Parse((item.FindControl("lblItemID") as Label).Text);

            Response.Redirect("~/Master/FrmItemClass.aspx?Id=" + iStID);
        }

        protected void LinkDelete_Click(object sender, EventArgs e)
        {

            objBO_Finance.Flag = 4;
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int iStID = int.Parse((item.FindControl("lblItemID") as Label).Text);

            objBO_Finance.Item_Code = iStID;
            int i = objBL_Finance.InsertUpdateDeleteItemMaster(objBO_Finance, out SQLError);
            if (i > 0)
            {
                message = "alert('Delete Successfully')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                objBO_Finance.Flag = 8;
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