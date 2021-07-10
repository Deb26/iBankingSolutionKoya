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
using System.IO;

namespace iBankingSolution.Master
{
    public partial class frmMasterSHGClient : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //ntxt_NoOfMembers_TextChanged();
            txt_GroupName.Focus();
            dtpkr_FormedOn.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            //dtpkr_JoinDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            if (!IsPostBack)
            {
               
                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetSHGEditData();
                }
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fu_Photo.PostedFile != null)
            {
                string FileName = Path.GetFileName(fu_Photo.PostedFile.FileName);
                //Save files to images folder


                fu_Photo.SaveAs(Server.MapPath("/Images/" + FileName));
                this.imgPhoto.ImageUrl = "/Images/" + FileName;
                hiddenImgEmp.Value = "/Images/" + FileName;

            }
        }

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            if (fu_Signature.PostedFile != null)
            {
                string FileName = Path.GetFileName(fu_Signature.PostedFile.FileName);
                //Save files to images folder

                fu_Signature.SaveAs(Server.MapPath("/Images/" + FileName));
                this.ImgSig.ImageUrl = "/Images/" + FileName;
                hiddenImgsign.Value = "/Images/" + FileName;

            }
        }
        protected void GetSHGEditData()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.CUST_ID = lblDid.Text;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

            DataSet dSet = objBL_Finance.GetSHGClientRecords(objBO_Finance, out SQLError);
            if (dSet.Tables[0].Rows.Count > 0)
            {
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";
                btnsubmit.CommandArgument = lblDid.Text;


                btnsubmit.CommandArgument = lblDid.Text;
                txt_GroupName.Text = Convert.ToString(dSet.Tables[0].Rows[0]["NAME"]);
                txt_LeadersName.Text = Convert.ToString(dSet.Tables[0].Rows[0]["GUARDIAN_NAME"]);
                txt_AsstLeadersName.Text = Convert.ToString(dSet.Tables[0].Rows[0]["GUARDIAN"]);
                txt_PostOffice.Text = Convert.ToString(dSet.Tables[0].Rows[0]["PO_CODE"]);
                txt_PoliceStation.Text = Convert.ToString(dSet.Tables[0].Rows[0]["PS_CODE"]);
                txt_Block.Text = Convert.ToString(dSet.Tables[0].Rows[0]["BLK_CODE"]);
                txt_District.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DIS_CODE"]);
                cmbx_ClientStatus.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["CL_STATUS"]);

                if (!string.IsNullOrEmpty(dSet.Tables[0].Rows[0]["DTSCUST"].ToString()))
                {
                    dtpkr_FormedOn.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["DTSCUST"]).ToString("dd/MM/yyyy");
                }
                txt_Village.Text = Convert.ToString(dSet.Tables[0].Rows[0]["VILL_CODE"]);
                cmbx_Category.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["CAT_CODE"]);
                CMBX_SpecialCategory.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SP_CAT_CODE"]);
                txt_Telephone.Text = Convert.ToString(dSet.Tables[0].Rows[0]["TEL_NO"]);
                txt_GramPanchyat.Text = Convert.ToString(dSet.Tables[0].Rows[0]["GP_CODE"]);
                txt_SubDivision.Text = Convert.ToString(dSet.Tables[0].Rows[0]["SU_CODE"]);

                if (!string.IsNullOrEmpty(dSet.Tables[0].Rows[0]["PICTPATH"].ToString()))
                {
                    imgPhoto.ImageUrl = dSet.Tables[0].Rows[0]["PICTPATH"].ToString();
                    hiddenImgEmp.Value = imgPhoto.ImageUrl;
                }
                if (!string.IsNullOrEmpty(dSet.Tables[0].Rows[0]["SIGNPATH"].ToString()))
                {
                    ImgSig.ImageUrl = dSet.Tables[0].Rows[0]["SIGNPATH"].ToString();
                    hiddenImgsign.Value = ImgSig.ImageUrl;
                }
                imgPhoto.ImageUrl = Convert.ToString(dSet.Tables[0].Rows[0]["PICTPATH"]);
                ImgSig.ImageUrl = Convert.ToString(dSet.Tables[0].Rows[0]["SIGNPATH"]);

                ViewState["PrePicPath"] = Convert.ToString(dSet.Tables[0].Rows[0]["PICTPATH"]);
                ViewState["PreSignPath"] = Convert.ToString(dSet.Tables[0].Rows[0]["SIGNPATH"]);
                ntxt_NoOfMembers.Text = Convert.ToString(dSet.Tables[1].Rows.Count);
                if (dSet.Tables[1].Rows.Count > 0)
                {
                    ViewState["SHGMemberDetails"] = dSet.Tables[1];

                    gv_SHGMemberDetails.DataSource = dSet.Tables[1];
                    gv_SHGMemberDetails.DataBind();





                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "alert('Account Not Belongs To This Branch');", true);
                btnsubmit1.Text = "Save";
                btnsubmit.Text = "Save";
            }


        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
            objBO_Finance.CUST_ID = btnsubmit.CommandArgument;
            objBO_Finance.NAME = txt_GroupName.Text.ToUpper();
            objBO_Finance.GuardianName = txt_LeadersName.Text.ToUpper();
            objBO_Finance.Guardian = txt_AsstLeadersName.Text.ToUpper();
            objBO_Finance.POCode = txt_PostOffice.Text.ToUpper();
            objBO_Finance.PSCode = txt_PoliceStation.Text.ToUpper();
            objBO_Finance.BLKCode = txt_Block.Text.ToUpper();
            objBO_Finance.DISCode = txt_District.Text.ToUpper();
            objBO_Finance.CLStatus = cmbx_ClientStatus.SelectedValue;

            string datedb = dtpkr_FormedOn.Text;
            DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            
            objBO_Finance.DTSCust = timedb;
            objBO_Finance.VillCode = txt_Village.Text.ToUpper();
            objBO_Finance.CatCode = cmbx_Category.SelectedValue;
            objBO_Finance.SpCatCode = CMBX_SpecialCategory.SelectedValue;
            objBO_Finance.TelNo = txt_Telephone.Text;
            objBO_Finance.ac_status = "Live";
            objBO_Finance.TERMINAL_ID = "00";
            objBO_Finance.EMPCODE = "10000";
            objBO_Finance.GPCode = txt_GramPanchyat.Text.ToUpper();
            objBO_Finance.SUCode = txt_SubDivision.Text.ToUpper();
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

            DataTable dtSHGMemberDetails = ViewState["SHGMemberDetails"] as DataTable;

            foreach (GridViewRow item in gv_SHGMemberDetails.Rows)
            {

                if (((TextBox)item.FindControl("txt_Name")).Text != "")
                {
                    try
                    {

                        var inst = dtSHGMemberDetails.AsEnumerable().Where(x => Convert.ToString(x["SlNo"]) == ((Label)item.FindControl("lbl_SlNo")).Text).First();

                        inst.SetField("SHG_NAME", ((TextBox)item.FindControl("txt_Name")).Text.ToUpper());
                        inst.SetField("SHG_SEX", ((DropDownList)item.FindControl("cmbx_Gender")).SelectedValue);
                        inst.SetField("SHG_AGE", ((TextBox)item.FindControl("txt_Age")).Text);



                        TextBox dtpkrJoinDate = (TextBox)item.FindControl("dtpkr_JoinDate");
                        string datejoinDt = dtpkrJoinDate.Text;
                        DateTime timejoinDt = DateTime.ParseExact(datejoinDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        inst.SetField("SHG_JOIN_DT", timejoinDt.ToShortDateString());
                        


                        inst.SetField("SHG_TYPE", ((DropDownList)item.FindControl("cmbx_Cast")).SelectedValue);
                        inst.SetField("SHG_TYPE_NO", ((TextBox)item.FindControl("txt_No")).Text.ToUpper());
                        inst.SetField("PAN_CARD_NO", ((TextBox)item.FindControl("txt_PANNo")).Text.ToUpper());
                        inst.SetField("AADHAR_NO", ((TextBox)item.FindControl("txt_AADHARNo")).Text.ToUpper());
                    }
                    catch { }
                }
            }
            objBO_Finance.dtSHGMemberDetails = dtSHGMemberDetails;

            string folderPath = "~/Images/SHGClient/";
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            string PhotofolderPath = folderPath + DateTime.UtcNow.Ticks + Path.GetFileName(fu_Photo.FileName);
            string SignfolderPath = folderPath + DateTime.UtcNow.Ticks + Path.GetFileName(fu_Signature.FileName);

            if (PhotofolderPath != "")
            {
                //string PrePicPath = Server.MapPath(ViewState["PrePicPath"] as string);
                //if (System.IO.File.Exists(PrePicPath))
                //{
                //    System.IO.File.Delete(PrePicPath);
                //}
                //fu_Photo.SaveAs(Server.MapPath(hiddenImgEmp.Value));// + Path.GetFileName(fu_Photo.FileName));
                objBO_Finance.PictPath = PhotofolderPath; //fu_Photo.HasFile ? PhotofolderPath : ViewState["PrePicPath"] as string;
            }
            if (SignfolderPath != "")
            {
                //string PreSignPath = Server.MapPath(ViewState["PreSignPath"] as string);
                //if (System.IO.File.Exists(PreSignPath))
                //{
                //    System.IO.File.Delete(PreSignPath);
                //}
                //fu_Signature.SaveAs(Server.MapPath(hiddenImgsign.Value)); 
                objBO_Finance.SignPath = hiddenImgsign.Value; //fu_Signature.HasFile ? SignfolderPath : ViewState["PreSignPath"] as string; UpdateSHGAccount
            }

            if (btnsubmit.Text == "Save")
            {
                int i = objBL_Finance.InsertUpdateDeleteAccountSHG(objBO_Finance, out SQLError);
                if (i > 0)
                {
                    Label1.Visible = true;
                    DivID.Visible = true;
                    Label1.Text = "Alloted CustId is:" + SQLError;
                    MessageBox(this, "Record Inserted Successfully . Allotted CUST ID is:-" + SQLError);
                    Resetcontrol();

                }
                else
                {
                    String message = "alert('Something Wrong')";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                }

            }
            else if (btnsubmit.Text == "Update")
            {
                int i = objBL_Finance.UpdateSHGAccount(objBO_Finance, out SQLError);
                if (i > 0)
                {
                    Label1.Visible = false;
                    DivID.Visible = false;
                    btnsubmit.Text = "Save";
                    btnsubmit1.Text = "Save";

                    MessageBox(this, "Updated Successfully !");
                    Resetcontrol();

                }
            }
        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        protected void GetSHGDetails()
        {
            objBO_Finance.Flag = 1;

            //objBO_Finance.CUST_ID = cmbx_SHGDetails.SelectedValue;

            DataSet dSet = objBL_Finance.GetSHGClientRecords(objBO_Finance, out SQLError);
            if (dSet.Tables[0].Rows.Count > 0)
            {
                btnsubmit.Text = "Update";
                btnsubmit1.Text = "Update";

                //btnsubmit1.CommandArgument = cmbxSHGDetails.SelectedValue;

                txt_GroupName.Text = Convert.ToString(dSet.Tables[0].Rows[0]["NAME"]);
                txt_LeadersName.Text = Convert.ToString(dSet.Tables[0].Rows[0]["GUARDIAN_NAME"]);
                txt_AsstLeadersName.Text = Convert.ToString(dSet.Tables[0].Rows[0]["GUARDIAN"]);
                txt_PostOffice.Text = Convert.ToString(dSet.Tables[0].Rows[0]["PO_CODE"]);
                txt_PoliceStation.Text = Convert.ToString(dSet.Tables[0].Rows[0]["PS_CODE"]);
                txt_Block.Text = Convert.ToString(dSet.Tables[0].Rows[0]["BLK_CODE"]);
                txt_District.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DIS_CODE"]);
                cmbx_ClientStatus.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["CL_STATUS"]);
                dtpkr_FormedOn.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["DTSCUST"]).ToString("dd/MM/yyyy");
                txt_Village.Text = Convert.ToString(dSet.Tables[0].Rows[0]["VILL_CODE"]);
                cmbx_Category.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["CAT_CODE"]);
                CMBX_SpecialCategory.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["SP_CAT_CODE"]);
                txt_Telephone.Text = Convert.ToString(dSet.Tables[0].Rows[0]["TEL_NO"]);
                txt_GramPanchyat.Text = Convert.ToString(dSet.Tables[0].Rows[0]["GP_CODE"]);
                txt_SubDivision.Text = Convert.ToString(dSet.Tables[0].Rows[0]["SU_CODE"]);
                imgPhoto.ImageUrl = Convert.ToString(dSet.Tables[0].Rows[0]["PICTPATH"]);
                ImgSig.ImageUrl = Convert.ToString(dSet.Tables[0].Rows[0]["SIGNPATH"]);
                ViewState["PrePicPath"] = Convert.ToString(dSet.Tables[0].Rows[0]["PICTPATH"]);
                ViewState["PreSignPath"] = Convert.ToString(dSet.Tables[0].Rows[0]["SIGNPATH"]);
                ntxt_NoOfMembers.Text = Convert.ToString(dSet.Tables[1].Rows.Count);
                if (dSet.Tables[1].Rows.Count > 0)
                {
                    ViewState["SHGMemberDetails"] = dSet.Tables[1];
                    gv_SHGMemberDetails.DataSource = dSet.Tables[1];
                    gv_SHGMemberDetails.DataBind();
                }
            }
            else
            {
                String message = "alert('Data Not Found')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);


            }

        }
        protected void Resetcontrol()
        {
            gv_SHGMemberDetails.DataSource = null;
            gv_SHGMemberDetails.DataBind();

            //TextBox txt_Name = gv_SHGMemberDetails.Rows[0].FindControl("txt_Name") as TextBox;
            //txt_Name.Text = String.Empty;

            //TextBox txt_Age = gv_SHGMemberDetails.Rows[0].FindControl("txt_Age") as TextBox;
            //txt_Age.Text = String.Empty;

            //TextBox dtpkr_JoinDate = gv_SHGMemberDetails.Rows[0].FindControl("dtpkr_JoinDate") as TextBox;
            //dtpkr_JoinDate.Text = String.Empty;

            //TextBox txt_No = gv_SHGMemberDetails.Rows[0].FindControl("txt_No") as TextBox;
            //txt_No.Text = String.Empty;

            //TextBox txt_PANNo = gv_SHGMemberDetails.Rows[0].FindControl("txt_PANNo") as TextBox;
            //txt_PANNo.Text = String.Empty;

            //TextBox txt_AADHARNo = gv_SHGMemberDetails.Rows[0].FindControl("txt_AADHARNo") as TextBox;
            //txt_AADHARNo.Text = String.Empty;

            //DropDownList cmbx_Cast = gv_SHGMemberDetails.Rows[0].FindControl("cmbx_Cast") as DropDownList;
            //cmbx_Cast.SelectedIndex = -1;

            //DropDownList cmbx_Gender = gv_SHGMemberDetails.Rows[0].FindControl("cmbx_Gender") as DropDownList;
            //cmbx_Gender.SelectedIndex = -1;

            imgPhoto.ImageUrl = "";
            ImgSig.ImageUrl = "";
            txt_GroupName.Text = String.Empty;
            txt_LeadersName.Text = String.Empty;
            txt_AsstLeadersName.Text = String.Empty;
            txt_PostOffice.Text = String.Empty;
            txt_PoliceStation.Text = String.Empty;
            txt_Block.Text = String.Empty;
            txt_District.Text = String.Empty;
            cmbx_ClientStatus.SelectedIndex = 0;
            dtpkr_FormedOn.Text = String.Empty;
            txt_Village.Text = String.Empty;
            cmbx_Category.SelectedIndex = -1;
            ntxt_NoOfMembers.Text = String.Empty;
            CMBX_SpecialCategory.SelectedIndex = 0;
            txt_Telephone.Text = String.Empty;
            txt_GramPanchyat.Text = String.Empty;
            txt_SubDivision.Text = String.Empty;

            
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Resetcontrol();
        }
        private DataTable dtMemberDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SlNo"),
                    new DataColumn("SHG_NAME"),
                    new DataColumn("SHG_SEX"),
                    new DataColumn("SHG_AGE"),
                    new DataColumn("SHG_JOIN_DT"),
                    new DataColumn("SHG_TYPE"),
                    new DataColumn("SHG_TYPE_NO"),
                    new DataColumn("PAN_CARD_NO"),
                    new DataColumn("AADHAR_NO")
                });
            return dt;
        }

        //protected void ntxt_NoOfMembers_TextChanged(object sender, EventArgs e)
        //{
        //    DataTable dt = ViewState["SHGMemberDetails"] != null ? ViewState["SHGMemberDetails"] as DataTable : dtMemberDetails();
        //    for (int i = 0; i < Convert.ToInt32(ntxt_NoOfMembers.Text); i++)
        //    {

        //        dt.Rows.Add(i + 1, "", "", "0", DateTime.Now.ToShortDateString(), "", "", "", "");

        //    }

        //    gv_SHGMemberDetails.DataSource = dt;
        //    gv_SHGMemberDetails.DataBind();
        //    ViewState["SHGMemberDetails"] = dt;
        //}

        protected void ntxt_NoOfMembers_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = ViewState["SHGMemberDetails"] != null ? ViewState["SHGMemberDetails"] as DataTable : dtMemberDetails();
            int rows = Convert.ToInt32(ntxt_NoOfMembers.Text.Trim());
            for (int i = 0; i < rows; i++)
            {

                dt.Rows.Add(i + 1, "", "", "0", DateTime.Now.Date.ToString("dd/MM/yyyy"), "", "", "", "");

            }

            gv_SHGMemberDetails.DataSource = dt;
            gv_SHGMemberDetails.DataBind();


            TextBox txt_Name = gv_SHGMemberDetails.Rows[0].FindControl("txt_Name") as TextBox;
            txt_Name.Focus();

            Label1.Visible = false;
            //ViewState["SHGMemberDetails"] = dt;
        }


        protected void btnUpload_Click1(object sender, EventArgs e)
        {

        }

        protected void gv_SHGMemberDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtset1 = (DataTable)ViewState["SHGMemberDetails"];
                if (dtset1 != null)
                {
                    DropDownList Gender = e.Row.FindControl("cmbx_Gender") as DropDownList;
                    DropDownList CAST = e.Row.FindControl("cmbx_Cast") as DropDownList;
                    Gender.SelectedValue = (dtset1.Rows[0]["SHG_SEX"].ToString().Trim());
                    CAST.SelectedValue = (dtset1.Rows[0]["SHG_TYPE"].ToString().Trim());
                }

            }
        }

        protected void itNew_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            Int32 rowcnt = 0;
            dt.Columns.Add(new DataColumn("SlNo", typeof(Int32)));
            dt.Columns.Add(new DataColumn("SHG_NAME", typeof(string)));
            dt.Columns.Add(new DataColumn("Gender", typeof(string)));
            dt.Columns.Add(new DataColumn("SHG_AGE", typeof(int)));
            dt.Columns.Add(new DataColumn("SHG_JOIN_DT", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Cast", typeof(string)));
            dt.Columns.Add(new DataColumn("SHG_TYPE_NO", typeof(string)));
            dt.Columns.Add(new DataColumn("PAN_CARD_NO", typeof(string)));
            dt.Columns.Add(new DataColumn("AADHAR_NO", typeof(string)));

            foreach (GridViewRow row in gv_SHGMemberDetails.Rows)
            {

                Label lbl_SlNo = row.FindControl("lbl_SlNo") as Label;
                TextBox txtSHGNAME = row.FindControl("txt_Name") as TextBox;
                DropDownList cmbxGender = row.FindControl("cmbx_Gender") as DropDownList;
                TextBox txtAge = row.FindControl("txt_Age") as TextBox;
                TextBox joinDt= row.FindControl("dtpkr_JoinDate") as TextBox;
                DropDownList cmbxcast = row.FindControl("cmbx_Cast") as DropDownList;
                TextBox txtNo = row.FindControl("txt_No") as TextBox;
                TextBox txtPANNo = row.FindControl("txt_PANNo") as TextBox;
                TextBox txtAADHARNo = row.FindControl("txt_AADHARNo") as TextBox;

                dr = dt.NewRow();

                rowcnt = Convert.ToInt32(dt.Rows.Count) + 1;
                dr[0] = lbl_SlNo.Text != "" ? lbl_SlNo.Text : "";
                dr[1] = txtSHGNAME.Text != "" ? txtSHGNAME.Text : "";
                dr[2] = cmbxGender.SelectedItem.Text != "Male" ? cmbxGender.SelectedItem.Text : "";
                dr[3] = txtAge.Text != "" ? txtAge.Text : "0";
                dr[4] = joinDt.Text != "" ? joinDt.Text : "";
                dr[5] = cmbxcast.Text != "APL" ? cmbxcast.Text : "";
                dr[6] = txtNo.Text != "" ? txtNo.Text : "";
                dr[7] = txtPANNo.Text != "" ? txtPANNo.Text : "";
                dr[8] = txtAADHARNo.Text != "" ? txtAADHARNo.Text : "";
                dt.Rows.Add(dr);
            }
            dt.Rows.Add(rowcnt + 1, 0, String.Empty, 0);
            gv_SHGMemberDetails.DataSource = dt;
            gv_SHGMemberDetails.DataBind();
            ViewState["dtSHGMemberDetails"] = dt;


        }

        protected void itbndelete_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}