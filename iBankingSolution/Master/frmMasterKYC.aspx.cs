using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Data;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace iBankingSolution.MasterPages
{


    public partial class frmMasterKYC : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string ImgImp = String.Empty;
        string ImgSign = String.Empty;
        string vfMiddleName = String.Empty;
        Boolean editflag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFirstName.Focus();
            if (!IsPostBack)
            {
                txtDOB.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtDateSinceCustomer.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                //dtpkr_CloseDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetKCCEditData();
                }
                MainView.ActiveViewIndex = 0;
            }
        }
        string vfFirstName = "";
        string vfLastName = "";
        protected void GetKCCEditData()
        {

            bool containsInt = lblDid.Text.Any(char.IsDigit);
            if (containsInt == true)
            {
                try
                {
                    objBO_Finance.Flag = 1;
                    objBO_Finance.CUST_ID = lblDid.Text;
                    lblSession.Text = Convert.ToString(Session["BranchID"]);
                    objBO_Finance.BranchID = Convert.ToInt32(lblSession.Text);
                    editflag = true;
                    DataTable dt = objBL_Finance.GetKYCDetailsRecords(objBO_Finance, out SQLError);
                    if (dt.Rows.Count > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave1.Text = "Update";
                        btnSave.CommandArgument = lblDid.Text;
                        btnSave1.CommandArgument = lblDid.Text;
                        txtFirstName.Text = Convert.ToString(dt.Rows[0]["F_NAME"]);
                        vfFirstName = Convert.ToString(dt.Rows[0]["F_NAME"]);
                        txtMiddleName.Text = Convert.ToString(dt.Rows[0]["M_NAME"]);
                        vfMiddleName = Convert.ToString(dt.Rows[0]["M_NAME"]);

                        txtLastName.Text = Convert.ToString(dt.Rows[0]["L_NAME"]);
                        vfLastName = Convert.ToString(dt.Rows[0]["L_NAME"]); ;
                        txtFullName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
                        txtFatherHusName.Text = Convert.ToString(dt.Rows[0]["GUARDIAN_NAME"]);
                        txt_PostOffice.Text = Convert.ToString(dt.Rows[0]["PO_CODE"]);
                        txt_PoliceStation.Text = Convert.ToString(dt.Rows[0]["PS_CODE"]);
                        txt_Block.Text = Convert.ToString(dt.Rows[0]["BLK_CODE"]);
                        txt_District.Text = Convert.ToString(dt.Rows[0]["DIS_CODE"]);
                        cmbx_ClientStatus.SelectedValue = Convert.ToString(dt.Rows[0]["CL_STATUS"]).Trim();
                        cmbx_Gender.SelectedValue = Convert.ToString(dt.Rows[0]["SEX"]).Trim();
                        cmbx_Religion.SelectedValue = Convert.ToString(dt.Rows[0]["REL_CODE"]);
                        cmbx_Profession.SelectedValue = Convert.ToString(dt.Rows[0]["PROF_CODE"]);

                        if (!string.IsNullOrEmpty(dt.Rows[0]["DTSCUST"].ToString()))
                        {
                            txtDateSinceCustomer.Text = Convert.ToDateTime(dt.Rows[0]["DTSCUST"]).ToString("dd/MM/yyyy");
                        }

                        txt_MembershipNo.Text = Convert.ToString(dt.Rows[0]["MEMNO"]);
                        txt_Village.Text = Convert.ToString(dt.Rows[0]["VILL_CODE"]);
                        cmbx_Category.SelectedValue = Convert.ToString(dt.Rows[0]["CAT_CODE"]);
                        cmbx_SpecialCategory.SelectedValue = Convert.ToString(dt.Rows[0]["SP_CAT_CODE"]);
                        txt_Telephone.Text = Convert.ToString(dt.Rows[0]["TEL_NO"]);

                        txt_LandHolding.Text = Convert.ToString(dt.Rows[0]["LAND_HOLDING"]);

                        if (txt_LandHolding.Text == "0.00")
                        {
                            cmbx_LandLess.SelectedValue = "2";
                            txt_LandHolding.Text = "0";
                        }
                        else
                        {
                            cmbx_LandLess.SelectedValue = "1";
                        }
                        cmbx_MembershipStatus.SelectedValue = Convert.ToString(dt.Rows[0]["AC_STATUS"]);
                        txt_Municipality.Text = Convert.ToString(dt.Rows[0]["GP_CODE"]);
                        txt_SubDivision.Text = Convert.ToString(dt.Rows[0]["SU_CODE"]);
                        cmbEducationalQualification.SelectedValue = Convert.ToString(dt.Rows[0]["EDU_CODE"]);
                        txt_PAN.Text = Convert.ToString(dt.Rows[0]["PAN_CARD_NO"]);
                        txt_RationCardNo.Text = Convert.ToString(dt.Rows[0]["RATION_CARD_NO"]);
                        txt_Votar.Text = Convert.ToString(dt.Rows[0]["VOTER_CARD_NO"]);
                        txt_Passport.Text = Convert.ToString(dt.Rows[0]["PASSPORT_NO"]);
                        cmbx_ClientType.SelectedValue = Convert.ToString(dt.Rows[0]["CLIENT_TYPE"]);
                        txt_BPLNo.Text = Convert.ToString(dt.Rows[0]["BPL_NO"]);
                        ntxt_MonthlyIncome.Text = Convert.ToString(dt.Rows[0]["MONTHLY_INCOME"]);
                        txtDOB.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
                        txtAge.Text = Convert.ToString(Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(txtDOB.Text.Substring(txtDOB.Text.Length - 4)));
                        cmbx_LandLess.SelectedValue = Convert.ToString(dt.Rows[0]["LAND_TYPE"]);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["CLOSE_DT"].ToString()))
                        {
                            dtpkr_CloseDate.Text = Convert.ToDateTime(dt.Rows[0]["CLOSE_DT"]).ToString("dd/MM/yyyy"); ;
                        }
                        txt_CloseCause.Text = Convert.ToString(dt.Rows[0]["CAUSE"]);
                        txt_SchoolCer.Text = Convert.ToString(dt.Rows[0]["SCHCER"]);
                        txt_AdharNo.Text = Convert.ToString(dt.Rows[0]["ADHAR_NO"]);
                        txt_KCCCardNo.Text = Convert.ToString(dt.Rows[0]["KCC_CARD"]);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["PICTPATH"].ToString()))
                        {
                            imgEmp.ImageUrl = dt.Rows[0]["PICTPATH"].ToString();
                            hiddenImgEmp.Value = imgEmp.ImageUrl;
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["SIGNPATH"].ToString()))
                        {
                            ImgSig.ImageUrl = dt.Rows[0]["SIGNPATH"].ToString();
                            hiddenImgsign.Value = ImgSig.ImageUrl;
                        }

                    }
                    else
                    {
                        MessageBox(this, "Account Not Belongs To This Branch !");
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "alert('Cust ID not Found');", true);
                        btnSave.Text = "Save";
                        btnSave1.Text = "Save";
                    }
                }
                catch (Exception ex)
                {
                    string smg = ex.Message;
                }
                finally
                {

                }
            }



        }
        protected void Resetcontrol()
        {
            txtFullName.Text = String.Empty;
            txtFirstName.Text = String.Empty;
            txtMiddleName.Text = String.Empty;

            txtLastName.Text = String.Empty;
            txtFatherHusName.Text = String.Empty;
            txt_PostOffice.Text = String.Empty;

            txt_PoliceStation.Text = String.Empty;
            txt_Block.Text = String.Empty;
            txt_District.Text = String.Empty;
            cmbx_ClientStatus.SelectedIndex = 0;
            cmbx_Gender.SelectedIndex = 0;
            cmbx_Religion.SelectedIndex = 0;
            cmbx_Profession.SelectedIndex = 0;

            txtDateSinceCustomer.Text = String.Empty;
            txt_MembershipNo.Text = String.Empty;
            txt_Village.Text = String.Empty;

            cmbx_Category.SelectedIndex = 0;
            cmbx_SpecialCategory.SelectedIndex = 0;

            txt_Telephone.Text = String.Empty;
            txt_LandHolding.Text = String.Empty;
            cmbx_MembershipStatus.SelectedIndex = 0;
            cmbx_ClientType.SelectedIndex = 0;
            txt_Municipality.Text = String.Empty;
            txt_SubDivision.Text = String.Empty;

            txt_PAN.Text = String.Empty;

            txt_RationCardNo.Text = String.Empty;
            txt_Votar.Text = String.Empty;
            txt_Passport.Text = String.Empty;
            txt_BPLNo.Text = String.Empty;
            ntxt_MonthlyIncome.Text = String.Empty;

            txtDOB.Text = String.Empty;
            txtAge.Text = String.Empty;

            txt_CloseCause.Text = String.Empty;
            txt_SchoolCer.Text = String.Empty;

            txt_AdharNo.Text = String.Empty;
            txt_KCCCardNo.Text = String.Empty;
            cmbx_LandLess.SelectedIndex = 0;
            imgEmp.ImageUrl = null;
            ImgSig.ImageUrl = null;
            cmbEducationalQualification.SelectedIndex = 0;


        }

        private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath) //For Generate Thumb
        {


            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {
                var newWidth = (int)(image.Width * scaleFactor);
                var newHeight = (int)(image.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e) //For Save Button
        {
          
            try
            {
                objBO_Finance.Flag = btnSave.Text == "Save" ? 1 : 2;
                objBO_Finance.CUST_ID = btnSave.CommandArgument;
                // Convert.ToString(txtFirstName.Text) + " " + Convert.ToString(txtMiddleName.Text) + " " + Convert.ToString(txtMiddleName.Text);
                objBO_Finance.FName = txtFirstName.Text.ToUpper();
                //objBO_Finance.MName = txtMiddleName.Text.ToUpper();
                if (!string.IsNullOrEmpty(txtMiddleName.Text.ToUpper()))
                    objBO_Finance.MName = Convert.ToString(txtMiddleName.Text.ToUpper());
                else
                    objBO_Finance.MName = null;
                objBO_Finance.LName = txtLastName.Text.ToUpper();
                objBO_Finance.Name = Convert.ToString(txtFullName.Text.Trim()).ToUpper();
                if (!string.IsNullOrEmpty(txtFatherHusName.Text.ToUpper()))
                    objBO_Finance.GuardianName = Convert.ToString(txtFatherHusName.Text.ToUpper());
                else
                    objBO_Finance.GuardianName = null;
                //objBO_Finance.GuardianName = txtFatherHusName.Text.ToUpper();
                //objBO_Finance.Guardian = "";
                //objBO_Finance.SurName = "";
                //objBO_Finance.MidName = "";
                objBO_Finance.POCode = txt_PostOffice.Text.ToUpper();
                if (!string.IsNullOrEmpty(txt_PoliceStation.Text.ToUpper()))
                    objBO_Finance.PSCode = Convert.ToString(txt_PoliceStation.Text.ToUpper());
                else
                    objBO_Finance.PSCode = null;
                //objBO_Finance.PSCode = txt_PoliceStation.Text;
                //objBO_Finance.BLKCode = txt_Block.Text;
                if (!string.IsNullOrEmpty(txt_Block.Text.ToUpper()))
                    objBO_Finance.BLKCode = Convert.ToString(txt_Block.Text.ToUpper());
                else
                    objBO_Finance.BLKCode = null;
                //objBO_Finance.DISCode = txt_District.Text;
                if (!string.IsNullOrEmpty(txt_District.Text.ToUpper()))
                    objBO_Finance.DISCode = Convert.ToString(txt_District.Text.ToUpper());
                else
                    objBO_Finance.DISCode = null;
                objBO_Finance.CLStatus = cmbx_ClientStatus.SelectedValue;
                objBO_Finance.Nominee = "";
                objBO_Finance.Sex = cmbx_Gender.SelectedValue;
                objBO_Finance.RELCode = cmbx_Religion.SelectedValue;
                objBO_Finance.PROFCode = cmbx_Profession.SelectedValue;

                if (txtDateSinceCustomer.Text != "")
                {
                    string Cust = txtDateSinceCustomer.Text;
                    DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.DTSCust = Cust1;
                }
                else
                {
                    objBO_Finance.DTSCust = string.IsNullOrEmpty(txtDateSinceCustomer.Text) ? (DateTime?)null : Convert.ToDateTime(txtDateSinceCustomer.Text);
                }

                //objBO_Finance.MemNo = txt_MembershipNo.Text;
                if (!string.IsNullOrEmpty(txt_MembershipNo.Text.ToUpper()))
                    objBO_Finance.MemNo = Convert.ToString(txt_MembershipNo.Text.ToUpper());
                else
                    objBO_Finance.MemNo = null;
                //objBO_Finance.VillCode = txt_Village.Text;
                if (!string.IsNullOrEmpty(txt_Village.Text.ToUpper()))
                    objBO_Finance.VillCode = Convert.ToString(txt_Village.Text.ToUpper());
                else
                    objBO_Finance.VillCode = null;
                objBO_Finance.CatCode = cmbx_Category.SelectedValue;
                objBO_Finance.SpCatCode = cmbx_SpecialCategory.SelectedValue;
                //objBO_Finance.TelNo = txt_Telephone.Text;
                if (!string.IsNullOrEmpty(txt_Telephone.Text))
                    objBO_Finance.TelNo = Convert.ToString(txt_Telephone.Text);
                else
                    objBO_Finance.TelNo = null;
                objBO_Finance.LandHolding1 = txt_LandHolding.Text != "" ? Convert.ToDouble(txt_LandHolding.Text) : 0;
                objBO_Finance.AcStatus = cmbx_MembershipStatus.SelectedValue;
                objBO_Finance.TrailTOT = "";
                objBO_Finance.TerminalID = "";
                objBO_Finance.EmpCode = "100";

                //objBO_Finance.GPCode = txt_Municipality.Text;
                if (!string.IsNullOrEmpty(txt_Municipality.Text.ToUpper()))
                    objBO_Finance.GPCode = Convert.ToString(txt_Municipality.Text.ToUpper());
                else
                    objBO_Finance.GPCode = null;
                //objBO_Finance.SUCode = txt_SubDivision.Text;
                if (!string.IsNullOrEmpty(txt_SubDivision.Text.ToUpper()))
                    objBO_Finance.SUCode = Convert.ToString(txt_SubDivision.Text.ToUpper());
                else
                    objBO_Finance.SUCode = null;
                objBO_Finance.EduCode = cmbEducationalQualification.SelectedValue;
                //objBO_Finance.PANCardNo = txt_PAN.Text;
                if (!string.IsNullOrEmpty(txt_PAN.Text.ToUpper()))
                    objBO_Finance.PANCardNo = Convert.ToString(txt_PAN.Text.ToUpper());
                else
                    objBO_Finance.PANCardNo = null;
                //objBO_Finance.RationCardNo = txt_RationCardNo.Text;
                if (!string.IsNullOrEmpty(txt_RationCardNo.Text.ToUpper()))
                    objBO_Finance.RationCardNo = Convert.ToString(txt_RationCardNo.Text.ToUpper());
                else
                    objBO_Finance.RationCardNo = null;
                //objBO_Finance.VotarCardNo = txt_Votar.Text;
                if (!string.IsNullOrEmpty(txt_Votar.Text.ToUpper()))
                    objBO_Finance.VotarCardNo = Convert.ToString(txt_Votar.Text.ToUpper());
                else
                    objBO_Finance.VotarCardNo = null;
                //objBO_Finance.PassportNo = txt_Passport.Text;
                if (!string.IsNullOrEmpty(txt_Passport.Text.ToUpper()))
                    objBO_Finance.PassportNo = Convert.ToString(txt_Passport.Text.ToUpper());
                else
                    objBO_Finance.PassportNo = null;
                objBO_Finance.ClientType = cmbx_ClientType.SelectedValue;
                //objBO_Finance.BPLNo = txt_BPLNo.Text;
                if (!string.IsNullOrEmpty(txt_BPLNo.Text.ToUpper()))
                    objBO_Finance.BPLNo = Convert.ToString(txt_BPLNo.Text);
                else
                    objBO_Finance.BPLNo = null;
                objBO_Finance.MonthlyIncome = ntxt_MonthlyIncome.Text != "" ? Convert.ToDouble(ntxt_MonthlyIncome.Text) : 0.00;


                string datedb = txtDOB.Text;
                DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.DOB = timedb;


                objBO_Finance.Age = txtAge.Text != null ? Convert.ToDouble(txtAge.Text) : 0.00;
                objBO_Finance.PINCode = "";
                objBO_Finance.LandType = cmbx_LandLess.SelectedValue;

                if (dtpkr_CloseDate.Text != "")
                {
                    string date = dtpkr_CloseDate.Text;
                    DateTime time = DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.CloseDT = time;
                }
                else
                {
                    objBO_Finance.CloseDT = string.IsNullOrEmpty(dtpkr_CloseDate.Text) ? (DateTime?)null : Convert.ToDateTime(dtpkr_CloseDate.Text);
                }

                //objBO_Finance.Cause = txt_CloseCause.Text;
                if (!string.IsNullOrEmpty(txt_CloseCause.Text.ToUpper()))
                    objBO_Finance.Cause = Convert.ToString(txt_CloseCause.Text);
                else
                    objBO_Finance.Cause = null;
                objBO_Finance.ReciCER = "";
                //objBO_Finance.SchCER = txt_SchoolCer.Text;
                if (!string.IsNullOrEmpty(txt_SchoolCer.Text.ToUpper()))
                    objBO_Finance.SchCER = Convert.ToString(txt_SchoolCer.Text);
                else
                    objBO_Finance.SchCER = null;
                //objBO_Finance.AdharNo = txt_AdharNo.Text;
                if (!string.IsNullOrEmpty(txt_AdharNo.Text.ToUpper()))
                    objBO_Finance.AdharNo = Convert.ToString(txt_AdharNo.Text.ToUpper());
                else
                    objBO_Finance.AdharNo = null;

                if (!string.IsNullOrEmpty(txt_KCCCardNo.Text.ToUpper()))
                    objBO_Finance.KCCCard = Convert.ToString(txt_KCCCardNo.Text.ToUpper());
                else
                    objBO_Finance.KCCCard = null;
                //objBO_Finance.KCCCard = txt_KCCCardNo.Text;
                objBO_Finance.FSTName = "";
                objBO_Finance.LSTName = "";
                objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]); 

                string folderPathPict = "~/UploadedImage/KYC/Pict";
                string folderPathSign = "~/UploadedImage/KYC/Sign";

                if (!Directory.Exists(Server.MapPath(folderPathPict)))

                {
                    Directory.CreateDirectory(Server.MapPath(folderPathPict));
                }


                folderPathPict = folderPathPict + Path.GetFileName(ProjectFileUpload.FileName); //THIS LINE WAS ENABLED


                ////string targetPath = folderPathPict;
                ////Stream strm = ProjectFileUpload.PostedFile.InputStream;
                ////var targetFile = targetPath;
                //////Based on scalefactor image size will vary
                ////GenerateThumbnails(0.5, strm, targetFile);

                //HIDDEN IMAGE FIELD VALUE

                objBO_Finance.PictPath = ProjectFileUpload.HasFile ? folderPathPict : ViewState["PrePictPath"] as string;  //folderPath +  Path.GetFileName(ProjectFileUpload.FileName); //hiddenImgEmp.Value;

                if (!Directory.Exists(Server.MapPath(folderPathSign)))

                {
                    Directory.CreateDirectory(Server.MapPath(folderPathSign));
                }

                folderPathSign = folderPathSign + Path.GetFileName(FileSignatureUpload.FileName);

                //HIDDEN IMAGE FIELD VALUE

                ////string targetPathsign = folderPathSign;
                ////Stream strm1 = FileSignatureUpload.PostedFile.InputStream;
                ////var targetFilesign = targetPathsign;
                //////Based on scalefactor image size will vary
                ////GenerateThumbnails(0.5, strm1, targetFilesign);


                //THIS BELOW LINE WAS ENABLED

                objBO_Finance.SignPath = FileSignatureUpload.HasFile ? folderPathSign : ViewState["PreSignPath"] as string;  //folderPath +  Path.GetFileName(FileSignatureUpload.FileName); 

                //hiddenImgsign.Value;
                
                int i = objBL_Finance.InsertUpdateDeleteKYCMaster(objBO_Finance, out SQLError);
                if (i > 0)
                {



                    if (ProjectFileUpload.HasFile)
                    {
                        string completePath = Server.MapPath(ViewState["PrePicPath"] as string);
                        if (System.IO.File.Exists(completePath))
                        {
                            System.IO.File.Delete(completePath);
                        }
                        ProjectFileUpload.SaveAs(Server.MapPath(folderPathPict));// + Path.GetFileName(fu_Photo.FileName)); 

                    }

                    if (FileSignatureUpload.HasFile)
                    {
                        string completePathSign = Server.MapPath(ViewState["PreSignPath"] as string);
                        if (System.IO.File.Exists(completePathSign))
                        {
                            System.IO.File.Delete(completePathSign);
                        }
                        FileSignatureUpload.SaveAs(Server.MapPath(folderPathSign));// + Path.GetFileName(fu_Photo.FileName)); 
                    }


                    if (btnSave.Text == "Save")
                    {

                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully . Allotted CUST ID is ')" + SQLError, true);

                        MessageBox(this, "Record Inserted Successfully . Allotted CUST ID is:-" + SQLError);
                    }
                    else if (btnSave.Text == "Update")
                    {

                        MessageBox(this, "Updated Successfully !");

                    }
                    Resetcontrol();
                    Label1.Visible = true;
                    DivID.Visible = true;
                    Label1.Text = "Alloted CustId:" + SQLError;

                }
                else
                {

                    String message = "alert('Something Went Wrong')";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                }
            }
            catch (Exception ex)
            {
                //string msg = ex.Message;
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", msg, true);
                MessageBox(this, ex.Message);
                //Response.Write(ex.Message);
            }
            finally
            {

            }

        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }

        protected void UserAddressbtn_Click(object sender, EventArgs e)
        {

        }

        protected void btnUserExperience_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddUserDocument_Click(object sender, EventArgs e)
        {

        }

        protected void btnUserQualification_Click(object sender, EventArgs e)
        {

        }

        protected void lkRemove_Click(object sender, EventArgs e)
        {

        }
        protected void lkQualificationRemove_Click(object sender, EventArgs e)
        {

        }
        protected void lkDocumentRemove_Click(object sender, EventArgs e)
        {

        }


        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        protected void txtDOB_TextChanged(object sender, EventArgs e)
        {
            string dateSt = txtDOB.Text;
            DateTime timeSt = DateTime.ParseExact(dateSt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DateTime dob = timeSt;
            int age = CalculateAge(dob);
            txtAge.Text = Convert.ToString(age);

            cmbx_Gender.Focus();
        }

        protected void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (editflag == true)
            {

                txtFullName.Text = Convert.ToString(txtFirstName.Text) + " " + vfMiddleName + " " + vfLastName;
            }
        }

        protected void txtMiddleName_TextChanged(object sender, EventArgs e)
        {
            if (editflag == true)
            {
                if (vfMiddleName == "")
                {
                    vfMiddleName = txtMiddleName.Text;

                    txtFullName.Text = txtFirstName.Text + " " + vfMiddleName + " " + txtLastName.Text;
                }
            }
            else
            {
                txtFullName.Text = txtFirstName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text;
            }
        }

        protected void txtLastName_TextChanged(object sender, EventArgs e)
        {
            if (editflag == true)
            {
                txtFullName.Text = "";
                txtFullName.Text = txtFirstName.Text + " " + vfMiddleName + " " + txtLastName.Text;
            }
            else
            {
                txtFullName.Text = "";

                txtFullName.Text = txtFirstName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text;
                txtFatherHusName.Focus();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (ProjectFileUpload.PostedFile != null)
            {
                string FileName = Path.GetFileName(ProjectFileUpload.PostedFile.FileName);
                //Save files to images folder


                ProjectFileUpload.SaveAs(Server.MapPath("/Images/" + FileName));
                this.imgEmp.ImageUrl = "/Images/" + FileName;
                hiddenImgEmp.Value = "/Images/" + FileName;

            }


        }

            protected void btnUpload1_Click(object sender, EventArgs e)
            {
                if (FileSignatureUpload.PostedFile != null)
                {
                    string FileName = Path.GetFileName(FileSignatureUpload.PostedFile.FileName);
                    //Save files to images folder

                    FileSignatureUpload.SaveAs(Server.MapPath("/Images/" + FileName));
                    this.ImgSig.ImageUrl = "/Images/" + FileName;
                    hiddenImgsign.Value = "/Images/" + FileName;

                }
            }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]

        public static List<string> SearchVillage(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct Vill_Code from CLIENT_REGISTER where " + "Vill_Code like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> Village = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Village.Add(sdr["Vill_Code"].ToString());
                        }
                    }
                    conn.Close();
                    return Village;
                }
            }
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchMunicipalty(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct GP_CODE from CLIENT_REGISTER where " + "GP_CODE like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> Municipalt = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Municipalt.Add(sdr["GP_CODE"].ToString());
                        }
                    }
                    conn.Close();
                    return Municipalt;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchBlock(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct BLK_CODE from CLIENT_REGISTER where " + "BLK_CODE like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> Block = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Block.Add(sdr["BLK_CODE"].ToString());
                        }
                    }
                    conn.Close();
                    return Block;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchPost(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct PO_CODE from CLIENT_REGISTER where " + "PO_CODE like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> Post = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Post.Add(sdr["PO_CODE"].ToString());
                        }
                    }
                    conn.Close();
                    return Post;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchPoliceSt(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct PS_CODE from CLIENT_REGISTER where " + "PS_CODE like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> PoliceSt = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            PoliceSt.Add(sdr["PS_CODE"].ToString());
                        }
                    }
                    conn.Close();
                    return PoliceSt;
                }
            }
        }



        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchDivision(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct SU_CODE from CLIENT_REGISTER where " + "SU_CODE like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> Division = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Division.Add(sdr["SU_CODE"].ToString());
                        }
                    }
                    conn.Close();
                    return Division;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchDistrict(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct DIS_CODE from CLIENT_REGISTER where " + "DIS_CODE like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> dist = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            dist.Add(sdr["DIS_CODE"].ToString());
                        }
                    }
                    conn.Close();
                    return dist;
                }
            }
        }

        protected void txtAge_TextChanged(object sender, EventArgs e)
        {
            if (txtDOB.Text == "" && txtAge.Text != "")
            {
                String dob = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtDOB.Text = (Convert.ToDateTime(dob).AddYears(-Convert.ToInt32(txtAge.Text))).ToString("dd/MM/yyyy");
            }
        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 0;
            btnSave.Visible = false;
            btnSave1.Visible = false;

        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 1;
            btnSave.Visible = false;
            btnSave1.Visible = false;
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 2;
            btnSave.Visible = false;
            btnSave1.Visible = false;
        }

        protected void Tab4_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 3;
            btnSave.Visible = true;
            btnSave1.Visible = true;
        }

        protected void btnNext1_Click(object sender, EventArgs e)
        {

        }

        protected void btnNext2_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "")
            {
                txtFirstName.Focus();
                return;
            }
            else if (txtLastName.Text == "")
            {
                txtLastName.Focus();
                return;
            }
            else if (txtDOB.Text == "")
            {
                txtDOB.Focus();
                return;
            }
            else if (cmbx_Gender.SelectedIndex == 0)
            {
                cmbx_Gender.Focus();
                return;
            }
            else if (cmbx_Category.SelectedIndex == 0)
            {
                cmbx_Category.Focus();
                return;

            }
            else if (cmbx_SpecialCategory.SelectedIndex == 0)
            {
                cmbx_SpecialCategory.Focus();
                return;
            }
            //else if (txtDateSinceCustomer.Text == "")
            //{
            //    txtDateSinceCustomer.Focus();
            //    return;

            //}
            txt_KCCCardNo.Focus();
            MainView.ActiveViewIndex = 1;
        }

        protected void btnNext3_Click(object sender, EventArgs e)
        {
            txt_Village.Focus();
            MainView.ActiveViewIndex = 2;
        }

        protected void btnNext4_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 3;
            btnSave.Visible = true;
            btnSave1.Visible = true;
        }

        protected void cmbx_MembershipStatus_SelectIndex (object sender , EventArgs e)
        {
            if (cmbx_MembershipStatus.SelectedValue == "Active")
            {
                dtpkr_CloseDate.Enabled = false;
                txt_CloseCause.Enabled = false;
            }
            else if (cmbx_MembershipStatus.SelectedValue == "Close")
            {
                dtpkr_CloseDate.Enabled = true;
                txt_CloseCause.Enabled = true;
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)//SELECT DROP DOWN
        {
            if (cmbx_ddlsearch.SelectedIndex > 0)
            {
                txtsearchkyc.Focus();
            }

        }

        protected void txtsearchkyc_TextChanged(object sender , EventArgs e) //EDIT KYC DATA 
        {
            try
            {
                objBO_Finance.Flag = 1;
                objBO_Finance.CUST_ID = txtsearchkyc.Text;
                lblSession.Text = Convert.ToString(Session["BranchID"]);
                objBO_Finance.BranchID = Convert.ToInt32(lblSession.Text);
                editflag = true;
                DataTable dt = objBL_Finance.GetKYCDetailsRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    btnSave.Text = "Update";
                    btnSave1.Text = "Update";
                    btnSave.CommandArgument = txtsearchkyc.Text;
                    btnSave1.CommandArgument = txtsearchkyc.Text;
                    txtFirstName.Text = Convert.ToString(dt.Rows[0]["F_NAME"]);
                    vfFirstName = Convert.ToString(dt.Rows[0]["F_NAME"]);
                    txtMiddleName.Text = Convert.ToString(dt.Rows[0]["M_NAME"]);
                    vfMiddleName = Convert.ToString(dt.Rows[0]["M_NAME"]);

                    txtLastName.Text = Convert.ToString(dt.Rows[0]["L_NAME"]);
                    vfLastName = Convert.ToString(dt.Rows[0]["L_NAME"]); ;
                    txtFullName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
                    txtFatherHusName.Text = Convert.ToString(dt.Rows[0]["GUARDIAN_NAME"]);
                    txt_PostOffice.Text = Convert.ToString(dt.Rows[0]["PO_CODE"]);
                    txt_PoliceStation.Text = Convert.ToString(dt.Rows[0]["PS_CODE"]);
                    txt_Block.Text = Convert.ToString(dt.Rows[0]["BLK_CODE"]);
                    txt_District.Text = Convert.ToString(dt.Rows[0]["DIS_CODE"]);
                    cmbx_ClientStatus.SelectedValue = Convert.ToString(dt.Rows[0]["CL_STATUS"]).Trim();
                    cmbx_Gender.SelectedValue = Convert.ToString(dt.Rows[0]["SEX"]).Trim();
                    cmbx_Religion.SelectedValue = Convert.ToString(dt.Rows[0]["REL_CODE"]);
                    cmbx_Profession.SelectedValue = Convert.ToString(dt.Rows[0]["PROF_CODE"]);

                    if (!string.IsNullOrEmpty(dt.Rows[0]["DTSCUST"].ToString()))
                    {
                        txtDateSinceCustomer.Text = Convert.ToDateTime(dt.Rows[0]["DTSCUST"]).ToString("dd/MM/yyyy");
                    }

                    txt_MembershipNo.Text = Convert.ToString(dt.Rows[0]["MEMNO"]);
                    txt_Village.Text = Convert.ToString(dt.Rows[0]["VILL_CODE"]);
                    cmbx_Category.SelectedValue = Convert.ToString(dt.Rows[0]["CAT_CODE"]);
                    cmbx_SpecialCategory.SelectedValue = Convert.ToString(dt.Rows[0]["SP_CAT_CODE"]);
                    txt_Telephone.Text = Convert.ToString(dt.Rows[0]["TEL_NO"]);

                    txt_LandHolding.Text = Convert.ToString(dt.Rows[0]["LAND_HOLDING"]);

                    if (txt_LandHolding.Text == "0.00")
                    {
                        cmbx_LandLess.SelectedValue = "2";
                        txt_LandHolding.Text = "0";
                    }
                    else
                    {
                        cmbx_LandLess.SelectedValue = "1";
                    }
                    cmbx_MembershipStatus.SelectedValue = Convert.ToString(dt.Rows[0]["AC_STATUS"]);
                    txt_Municipality.Text = Convert.ToString(dt.Rows[0]["GP_CODE"]);
                    txt_SubDivision.Text = Convert.ToString(dt.Rows[0]["SU_CODE"]);
                    cmbEducationalQualification.SelectedValue = Convert.ToString(dt.Rows[0]["EDU_CODE"]);
                    txt_PAN.Text = Convert.ToString(dt.Rows[0]["PAN_CARD_NO"]);
                    txt_RationCardNo.Text = Convert.ToString(dt.Rows[0]["RATION_CARD_NO"]);
                    txt_Votar.Text = Convert.ToString(dt.Rows[0]["VOTER_CARD_NO"]);
                    txt_Passport.Text = Convert.ToString(dt.Rows[0]["PASSPORT_NO"]);
                    cmbx_ClientType.SelectedValue = Convert.ToString(dt.Rows[0]["CLIENT_TYPE"]);
                    txt_BPLNo.Text = Convert.ToString(dt.Rows[0]["BPL_NO"]);
                    ntxt_MonthlyIncome.Text = Convert.ToString(dt.Rows[0]["MONTHLY_INCOME"]);
                    txtDOB.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
                    txtAge.Text = Convert.ToString(Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(txtDOB.Text.Substring(txtDOB.Text.Length - 4)));
                    cmbx_LandLess.SelectedValue = Convert.ToString(dt.Rows[0]["LAND_TYPE"]);
                    if (!string.IsNullOrEmpty(dt.Rows[0]["CLOSE_DT"].ToString()))
                    {
                        dtpkr_CloseDate.Text = Convert.ToDateTime(dt.Rows[0]["CLOSE_DT"]).ToString("dd/MM/yyyy"); ;
                    }
                    txt_CloseCause.Text = Convert.ToString(dt.Rows[0]["CAUSE"]);
                    txt_SchoolCer.Text = Convert.ToString(dt.Rows[0]["SCHCER"]);
                    txt_AdharNo.Text = Convert.ToString(dt.Rows[0]["ADHAR_NO"]);
                    txt_KCCCardNo.Text = Convert.ToString(dt.Rows[0]["KCC_CARD"]);
                    if (!string.IsNullOrEmpty(dt.Rows[0]["PICTPATH"].ToString()))
                    {
                        imgEmp.ImageUrl = dt.Rows[0]["PICTPATH"].ToString();
                        hiddenImgEmp.Value = imgEmp.ImageUrl;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SIGNPATH"].ToString()))
                    {
                        ImgSig.ImageUrl = dt.Rows[0]["SIGNPATH"].ToString();
                        hiddenImgsign.Value = ImgSig.ImageUrl;
                    }

                }
                else
                {
                    MessageBox(this, "Account Not Belongs To This Branch !");
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "alert('Cust ID not Found');", true);
                    btnSave.Text = "Save";
                    btnSave1.Text = "Save";
                }
            }
            catch (Exception ex)
            {
                string smg = ex.Message;
            }
            finally
            {

            }

        }
    }

}