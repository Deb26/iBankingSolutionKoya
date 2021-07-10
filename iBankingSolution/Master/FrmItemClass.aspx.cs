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
    public partial class FrmItemClass : System.Web.UI.Page
    {
        MyDBDataContext dbContext = new MyDBDataContext();
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetItemMasterClassEditData();

                }
               
                 
            }
        }
      
        protected void GetItemMasterClassEditData()
        {
            objBO_Finance.Flag = 7;
            objBO_Finance.Item_Code = lblDid.Text;
            DataTable dt = objBL_Finance.GetItemMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";
                btnsubmit.CommandArgument = Convert.ToString(dt.Rows[0]["categoryid"]);
                btnsubmit1.CommandArgument = Convert.ToString(dt.Rows[0]["categoryid"]);


                txtItemName.Text = Convert.ToString(dt.Rows[0]["NAME"]) != "" ? Convert.ToString(dt.Rows[0]["NAME"]) : "";
            }
            else
            {
                message = "alert('Data Not found.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
            {
                AddNewNewItemClass();
            }

            else if(btnsubmit.Text == "Update" || btnsubmit1.Text == "Update")
            {
                UpdateItemClass();
            }
        }


        private void UpdateItemClass()
        {
             
           
            //Get Single course which need to update  
            ITEM_CLASS objItem = dbContext.ITEM_CLASSes.Single(item => item.categoryId == Convert.ToDouble(lblDid.Text));
            //Field which will be update  
            objItem.NAME = Convert.ToString(txtItemName.Text);
            // executes the appropriate commands to implement the changes to the database  
            dbContext.SubmitChanges();
            string message = "alert('Updated Successfully.')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            txtItemName.Text = "";


        }
        private void AddNewNewItemClass()
        {
            //Data maping object to our database  
             
            ITEM_CLASS ObjItem = new ITEM_CLASS();
            ObjItem.NAME = txtItemName.Text;
                 
            //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
            dbContext.ITEM_CLASSes.InsertOnSubmit(ObjItem);
            // executes the appropriate commands to implement the changes to the database  
            dbContext.SubmitChanges();
           string message = "alert('Saved Successfully.')";
           ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            txtItemName.Text = "";
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void itbnEdit_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void itbndelete_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void GVItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GVItem_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GVItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
    }
}