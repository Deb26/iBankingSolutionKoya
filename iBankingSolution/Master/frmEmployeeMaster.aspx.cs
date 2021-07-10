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
    public partial class frmEmployeeMaster : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtpkr_SanctionDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dtpkr_DOB.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dtpkr_DOJ.Text = DateTime.Now.ToString("dd/MM/yyyy");
                dtpkr_DOR.Text = DateTime.Now.ToString("dd/MM/yyyy");

                BindEmployee();
                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetEmployeeData();

                }

            }
        }
        protected void GetEmployeeData()
        {
            objBO_Finance.Flag = 2;
            objBO_Finance.EmpCode = lblDid.Text;
            DataTable dt = objBL_Finance.GetEmployeeMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                Btn_Save.Text = "Update";
                Btn_Save.CommandArgument = Convert.ToString(dt.Rows[0]["EMPCODE"]);
                txt_Name.Text = Convert.ToString(dt.Rows[0]["Name"]);
                txt_Address.Text = Convert.ToString(dt.Rows[0]["Addr"]);
                txt_PinCode.Text = Convert.ToString(dt.Rows[0]["PIN"]);
                txt_Phone.Text = Convert.ToString(dt.Rows[0]["PHONE"]);
                txt_Department.Text = Convert.ToString(dt.Rows[0]["DEPT"]);
                txt_Designation.Text = Convert.ToString(dt.Rows[0]["DESIG"]);
                dtpkr_DOJ.Text = Convert.ToDateTime(dt.Rows[0]["DOJ"]).ToString("dd/MM/yyyy");
                txt_FathersName.Text = Convert.ToString(dt.Rows[0]["FNAME"]);
                txt_SanctionNo.Text = Convert.ToString(dt.Rows[0]["ARSANO"]);
                txt_EducationalQualification.Text = Convert.ToString(dt.Rows[0]["EDU_QUF"]);
                cmbx_ARCSSanction.SelectedValue = Convert.ToString(dt.Rows[0]["ARCS"]);
                dtpkr_DOR.Text = Convert.ToDateTime(dt.Rows[0]["DATE_OF_RETIR"]).ToString("dd/MM/yyyy");
                dtpkr_SanctionDate.Text = Convert.ToDateTime(dt.Rows[0]["ARSADT"]).ToString("dd/MM/yyyy");
                dtpkr_DOB.Text = Convert.ToDateTime(dt.Rows[0]["DATE_OF_BIRTH"]).ToString("dd/MM/yyyy");

                cmbx_ReportsTo.DataBind();
                cmbx_ReportsTo.SelectedValue = Convert.ToString(dt.Rows[0]["REP_TO"]);
                
                cmbx_MaritalStatus.SelectedValue = Convert.ToString(dt.Rows[0]["M_STATUS"]);
                cmbx_Sex.SelectedValue = Convert.ToString(dt.Rows[0]["SEX"]);
                imgEmp.ImageUrl = Convert.ToString(dt.Rows[0]["PICTPATH"]);
                ViewState["PrePicPath"] = Convert.ToString(dt.Rows[0]["PICTPATH"]);
            }
            else
            {
                message = "alert('Data Not Found.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                
            }
        }
        protected void BindEmployee()
        {
            objBO_Finance.Flag = 4;
            objBO_Finance.CUST_ID = null;

            DataTable dt = objBL_Finance.GetEmployeeMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_ReportsTo.DataSource = dt;
                cmbx_ReportsTo.DataValueField = "EMPCODE";
                cmbx_ReportsTo.DataTextField = "NAME";
                cmbx_ReportsTo.DataBind();

            }

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

        protected void GVLoan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GVLoan_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GVLoan_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fu_Photo.PostedFile != null)
            {
                string FileName = Path.GetFileName(fu_Photo.PostedFile.FileName);
                //Save files to images folder


                fu_Photo.SaveAs(Server.MapPath("/Images/" + FileName));
                this.imgEmp.ImageUrl = "/Images/" + FileName;
                hiddenImgEmp.Value = "/Images/" + FileName;




            }
        }
        protected void ResetControls()
        {
            Btn_Save.Text = "Save";
            txt_Name.Text = String.Empty;
            txt_Address.Text = String.Empty;
            txt_PinCode.Text = String.Empty;
            txt_Phone.Text = String.Empty;
            txt_Department.Text = String.Empty;
            txt_Designation.Text = String.Empty;
            dtpkr_DOJ.Text = String.Empty;
            txt_FathersName.Text = String.Empty;
            txt_SanctionNo.Text = String.Empty;
            txt_EducationalQualification.Text = String.Empty;
            cmbx_ARCSSanction.SelectedIndex = 0;
            dtpkr_DOR.Text = String.Empty;
            dtpkr_SanctionDate.Text = String.Empty;
            dtpkr_DOB.Text = String.Empty;
            cmbx_ReportsTo.SelectedIndex = 0;
            cmbx_MaritalStatus.SelectedIndex = 0;
            dtpkr_DOB.Text = String.Empty;
            cmbx_Sex.SelectedIndex = 0;
            hiddenImgEmp.Value = "";


        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            objBO_Finance.Flag = Btn_Save.Text == "Save" ? 1 : 2;
            objBO_Finance.EMPCODE = Btn_Save.CommandArgument;
            objBO_Finance.Name = txt_Name.Text;
            objBO_Finance.Address1 = txt_Address.Text;
            objBO_Finance.PINCode = txt_PinCode.Text;
            objBO_Finance.PHONE = txt_Phone.Text;
            objBO_Finance.DEPT = txt_Department.Text;
            objBO_Finance.DESIG = txt_Designation.Text;
            string dtoj = dtpkr_DOJ.Text;
            DateTime dtofj = DateTime.ParseExact(dtoj, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.DOJ = dtofj;
            //objBO_Finance.DOJ = Convert.ToDateTime(dtpkr_DOJ.Text);
            objBO_Finance.FNAME = txt_FathersName.Text;
            objBO_Finance.ARSANO = txt_SanctionNo.Text;
            objBO_Finance.EDU_QUF = txt_EducationalQualification.Text;
            objBO_Finance.ARCS = cmbx_ARCSSanction.SelectedValue;

            string dtor = dtpkr_DOR.Text;
            DateTime dateor = DateTime.ParseExact(dtor, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.DATE_OF_RETIR = dateor;
            //objBO_Finance.DATE_OF_RETIR = Convert.ToDateTime(dtpkr_DOR.Text);

            string dts = dtpkr_SanctionDate.Text;
            DateTime sandt = DateTime.ParseExact(dts, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.ARSADT = sandt;
            //objBO_Finance.ARSADT = Convert.ToDateTime(dtpkr_SanctionDate.Text);

            string dtdob = dtpkr_DOB.Text;
            DateTime dtofbirth = DateTime.ParseExact(dtdob, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.DATE_OF_BIRTH = dtofbirth;
            //objBO_Finance.DATE_OF_BIRTH = Convert.ToDateTime(dtpkr_DOB.Text);
            objBO_Finance.REP_TO = cmbx_ReportsTo.SelectedValue;
            objBO_Finance.M_STATUS = cmbx_MaritalStatus.SelectedValue;

            
            objBO_Finance.DOB = dtofbirth;
            //objBO_Finance.DOB = Convert.ToDateTime(dtpkr_DOB.Text);
            objBO_Finance.SEX = cmbx_Sex.SelectedValue;
            objBO_Finance.TERMINAL_ID = "";
            objBO_Finance.EmpCode = "";
            objBO_Finance.PictPath = hiddenImgEmp.Value;

            //string folderPath = "~/UploadedImage/Employee/";
            //if (!Directory.Exists(Server.MapPath(folderPath)))
            //{
            //    Directory.CreateDirectory(Server.MapPath(folderPath));
            //}

            //folderPath = folderPath + DateTime.UtcNow.Ticks + Path.GetFileName(fu_Photo.FileName);
            //objBO_Finance.PictPath = fu_Photo.HasFile ? folderPath : ViewState["PrePicPath"] as string;
            int i = objBL_Finance.InsertUpdateDeleteEmployeeMaster(objBO_Finance, out SQLError);

            if (i > 0)
            {
                if (Btn_Save.Text == "Save")
                {
                    ResetControls();
                    message = "alert('Save Successfully.')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    Btn_Save.Text = "Save";

                    objBO_Finance.Flag = 1;
                    objBL_Finance.GetEmployeeMasterRecords(objBO_Finance, out SQLError);

                }
                if (Btn_Save.Text == "Update")
                {
                    message = "alert('Update Successfully.')";

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    Btn_Save.Text = "Save";

                    ResetControls();
                }

            }
            else
            {

                message = "alert('Something Wrong Input.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}