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
    public partial class FrmItemMaster : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;
        MyDBDataContext dbContext = new MyDBDataContext();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetItemMasterEditData();

                }
                BindpurchareLedger();
                BindSalesLedger();
                GetUnit();
                GetItemClass();

            }
        }

        protected void GetItemClass()
        {
            objBO_Finance.Flag = 2;

            DataTable dt = objBL_Finance.GetItemDetails(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_ItemClass.DataSource = dt;
                cmbx_ItemClass.DataTextField = "NAME";
                cmbx_ItemClass.DataValueField = "CODE";
                cmbx_ItemClass.DataBind();
                cmbx_ItemClass.Items.Insert(0, new ListItem("--- Please Select ---", "0"));
            }

            //var Unit = (from cod in dbContext.ITEM_CLASSes

            //            select new
            //            {
            //                Code = cod.categoryId,
            //                Name = cod.NAME


            //            }).ToList();

            //if (Unit.Count > 0)
            //{
            //    cmbx_ItemClass.DataSource = Unit.ToList();
            //    cmbx_ItemClass.DataValueField = "Code";
            //    cmbx_ItemClass.DataTextField = "Name";
            //    cmbx_ItemClass.DataBind();
            //    cmbx_ItemClass.Items.Insert(0, "-- Select --");
            //}
        }
        protected void GetUnit()
        {

            var Unit = (from cod in dbContext.MEASURING_UNITs

                         select new
                         {
                             Code = cod.Code,
                             Name = cod.Name


                         }).ToList();

            if (Unit.Count > 0)
            {
                cmbx_UoM.DataSource = Unit.ToList();
                cmbx_UoM.DataValueField = "Code";
                cmbx_UoM.DataTextField = "Name";
                cmbx_UoM.DataBind();
                cmbx_UoM.Items.Insert(0, "-- Select --");
            }

        }

        protected void GetItemMasterEditData()
        {
            objBO_Finance.Flag = 2;
            objBO_Finance.Item_Code = lblDid.Text;
            DataTable dt = objBL_Finance.GetItemMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";
                btnsubmit.CommandArgument = Convert.ToString(dt.Rows[0]["Code"]);
                btnsubmit1.CommandArgument = Convert.ToString(dt.Rows[0]["Code"]);

                txt_ItemName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
                txt_HSNNO.Text = Convert.ToString(dt.Rows[0]["HSN_NO"]);
                //cmbx_PurchaseLedger.EnableAutomaticLoadOnDemand = false;
                //cmbx_PurchaseLedger.DataBind();
                //cmbx_SaleLedger.EnableAutomaticLoadOnDemand = false;
                //cmbx_SaleLedger.DataBind();
                cmbx_PurchaseLedger.SelectedValue = Convert.ToString(dt.Rows[0]["PUR_LDG"]);
                cmbx_SaleLedger.SelectedValue = Convert.ToString(dt.Rows[0]["SALE_LDG"]);
                cmbx_UoM.SelectedValue = Convert.ToString(dt.Rows[0]["MU"]);
                cmbx_ItemClass.SelectedValue = Convert.ToString(dt.Rows[0]["MCLASS"]);
               // ntxt_OpeningSTock.Text = Convert.ToString(dt.Rows[0]["TOT_QTY"]);
                //ntxt_StockValue.Text = Convert.ToString(Convert.ToDouble(dt.Rows[0]["TOT_QTY"] == DBNull.Value ? 0 : dt.Rows[0]["TOT_QTY"]) * Convert.ToDouble(dt.Rows[0]["ROL"] == DBNull.Value ? 0 : dt.Rows[0]["ROL"]));
                //ntxt_ItemSaleValue.Text = Convert.ToString(dt.Rows[0]["ROL"]);
                ntxt_CGST.Text = Convert.ToString(dt.Rows[0]["CGST_RATE"]);
                ntxt_SGST.Text = Convert.ToString(dt.Rows[0]["SGST_RATE"]);
                cmbx_ItemClass.Text = Convert.ToString(dt.Rows[0]["MCLASS"]) != "" ? Convert.ToString(dt.Rows[0]["MCLASS"]) : "";  
            }
            else
            {
                message = "alert('Data Not found.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
        }
        protected void BindSalesLedger()
        {
            try
            {
                objBO_Finance.Flag = 8;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_SaleLedger.DataSource = dt;
                    cmbx_SaleLedger.DataValueField = "LDG_CODE";
                    cmbx_SaleLedger.DataTextField = "NOMENCLATURE";
                    cmbx_SaleLedger.DataBind();
                }
            }
            catch (Exception ex)
            { string msg = ex.Message; }
            finally
            { }

        }
        protected void BindpurchareLedger()
        {

            try
            {
                objBO_Finance.Flag = 7;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_PurchaseLedger.DataSource = dt;
                    cmbx_PurchaseLedger.DataValueField = "LDG_CODE";
                    cmbx_PurchaseLedger.DataTextField = "NOMENCLATURE";
                    cmbx_PurchaseLedger.DataBind();
                }
            }
            catch (Exception ex)
            { string msg = ex.Message; }
            finally
            { }
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

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                objBO_Finance.Item_Code = btnsubmit1.CommandArgument;
                objBO_Finance.NAME = txt_ItemName.Text;
                //objBO_Finance.ROL = Convert.ToDouble(ntxt_ItemSaleValue.Text);
                objBO_Finance.MU = cmbx_UoM.SelectedValue;
                objBO_Finance.LDG_CODE = "";
                objBO_Finance.MCLASS = Convert.ToString(cmbx_ItemClass.SelectedValue);
                objBO_Finance.PUR_LDG = cmbx_PurchaseLedger.SelectedValue;
             
                objBO_Finance.SALE_LDG = cmbx_SaleLedger.SelectedValue;
                //objBO_Finance.TOT_QTY = Convert.ToDouble(ntxt_OpeningSTock.Text);
                objBO_Finance.HSNNO = txt_HSNNO.Text;
                objBO_Finance.CGST = Convert.ToDouble(ntxt_CGST.Text);
                objBO_Finance.SGST = Convert.ToDouble(ntxt_SGST.Text);
                //objBO_Finance.PurchaseValue = txt_purvalue.Text!="" ? Convert.ToDecimal(txt_purvalue.Text):0;
                objBO_Finance.BranchID= Convert.ToInt32(Session["BranchID"]);
                int i = objBL_Finance.InsertUpdateDeleteItemMaster(objBO_Finance, out SQLError);



                if (i > 0)
                {
                    if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                    {
                        //ResetControls();
                        message = "alert('Save Successfully.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";

                        objBO_Finance.Flag = 1;
                        ViewState["ItemDetails"] = objBL_Finance.GetItemMasterRecords(objBO_Finance, out SQLError);

                    }
                    if (btnsubmit1.Text == "Update" || btnsubmit.Text == "Save")
                    {
                        message = "alert('Update Successfully.')";

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";

                        ResetControls();
                    }

                    else
                    {

                        message = "alert('Something Wrong Input.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    }
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {

            }
        }
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            btnsubmit1.Text = "Save";
            ntxt_CGST.Text = String.Empty;
            ntxt_SGST.Text = String.Empty;
            txt_HSNNO.Text = String.Empty;
            //ntxt_OpeningSTock.Text = String.Empty;
           // ntxt_ItemSaleValue.Text = String.Empty;
            cmbx_ItemClass.SelectedIndex = -1;
            cmbx_PurchaseLedger.SelectedIndex = -1;
            cmbx_SaleLedger.SelectedIndex = -1;
            cmbx_UoM.SelectedIndex = -1;
           // ntxt_ItemSaleValue.Text = String.Empty;
            txt_ItemName.Text = String.Empty;

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

        protected void getGroupname()
        {
            //DataSet ds = new DataSet();
            //ds = objBL_Finance.get_Group_Item(objBO_Finance);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    cmbx_ItemClass.DataSource = ds;
            //    cmbx_ItemClass.DataValueField = "ITEMCATEGORY_ID";
            //    cmbx_ItemClass.DataTextField = "CATEGORY_NAME";
            //    cmbx_ItemClass.DataBind();
            //}
        }
    }
}