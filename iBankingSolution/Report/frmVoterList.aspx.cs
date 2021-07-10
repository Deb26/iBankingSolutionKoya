using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iBankingSolution.Report
{
    public partial class frmVoterList : System.Web.UI.Page
    {
        MyDBDataContext dbContext = new MyDBDataContext();
        String VillageCode = "";
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindVillage();
               
            }
        }

        protected void BindVillage()
        {
            //with distinct Values
            ddlvillage.DataSource = dbContext.CLIENT_REGISTERs.GroupBy(c => c.VILL_CODE)
                                    .Select(g => g.First());

            ddlvillage.DataTextField = "Vill_Code";
            ddlvillage.DataValueField = "Cust_ID";
            ddlvillage.DataBind();
            ddlvillage.Items.Insert(0, new ListItem("--All--", ""));
             
        }
        protected void VoterListBind()
        {

            if (ddlvillage.SelectedItem.Text!= "--All--")
            {
                GVMemberDetail.DataSource = (from CR in dbContext.CLIENT_REGISTERs
                                             where CR.VILL_CODE == VillageCode
                                             orderby CR.NAME descending

                                             select new
                                             {
                                                 CUST_ID = CR.CUST_ID,
                                                 NAME = CR.NAME,
                                                 GUARDIAN_NAME = CR.GUARDIAN_NAME,
                                                 BLK_CODE = CR.BLK_CODE,
                                                 VILL_CODE = CR.VILL_CODE,
                                                 PO_CODE = CR.PO_CODE,
                                                 PS_CODE = CR.PS_CODE,
                                                 DIS_CODE = CR.DIS_CODE,
                                                 Pin = CR.PIn


                                             });

            }
            else
            {
                GVMemberDetail.DataSource = (from CR in dbContext.CLIENT_REGISTERs
                                             
                                             orderby CR.NAME descending

                                             select new
                                             {
                                                 CUST_ID = CR.CUST_ID,
                                                 NAME = CR.NAME,
                                                 GUARDIAN_NAME = CR.GUARDIAN_NAME,
                                                 BLK_CODE = CR.BLK_CODE,
                                                 VILL_CODE = CR.VILL_CODE,
                                                 PO_CODE = CR.PO_CODE,
                                                 PS_CODE = CR.PS_CODE,
                                                 DIS_CODE = CR.DIS_CODE,
                                                 Pin = CR.PIn


                                             });
            }
            GVMemberDetail.DataBind();
        }

        
        protected void ddlvillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                VillageCode = ddlvillage.SelectedItem.Text;
                VoterListBind();
           
             
        }

        protected void GVMemberDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
             
                GVMemberDetail.PageIndex = e.NewPageIndex;
                VoterListBind();
            

        }
    }
}