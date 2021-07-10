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
    public partial class frmMasterSocietySetup : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadForm();
                txtSocietyCode.Enabled = false;
                txtGSTNo.Enabled = false;
                txtSocCurAcNo.Enabled = false;

            }
        }

        protected void itNew_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            Int32 rowcnt = 0;
            dt.Columns.Add(new DataColumn("SlNo", typeof(Int32)));
            dt.Columns.Add(new DataColumn("BranchCode", typeof(Int32)));
            dt.Columns.Add(new DataColumn("BranchName", typeof(string)));
            dt.Columns.Add(new DataColumn("CashInHand", typeof(Decimal)));
            dt.Columns.Add(new DataColumn("BrType", typeof(string)));

            foreach (GridViewRow row in GVBranch.Rows)
            {

                Label lbl_SlNo = row.FindControl("lbl_SlNo") as Label;
                TextBox txtBrCode = row.FindControl("txtBrCode") as TextBox;
                TextBox txtBranchName = row.FindControl("txtBranchName") as TextBox;
                TextBox txtCashInHand = row.FindControl("txtCashInHand") as TextBox;
                DropDownList cmbxbrtype = row.FindControl("cmbx_BrType") as DropDownList;

                dr = dt.NewRow();

                rowcnt = Convert.ToInt32(dt.Rows.Count) + 1;
                dr[0] = lbl_SlNo.Text != "" ? lbl_SlNo.Text : ""; ;
                dr[1] = txtBrCode.Text != "" ? txtBrCode.Text : "";
                dr[2] = txtBranchName.Text != "" ? txtBranchName.Text : "";
                dr[3] = txtCashInHand.Text != "" ? txtCashInHand.Text : "";
                dr[4] = cmbxbrtype.SelectedValue != "" ? cmbxbrtype.SelectedValue : "";

                dt.Rows.Add(dr);
            }
            dt.Rows.Add(rowcnt + 1, 0, String.Empty, 0);
            GVBranch.DataSource = dt;
            GVBranch.DataBind();
            ViewState["dtBranchDetails"] = dt;
            

        }

        protected void itbndelete_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void AddSocietyData()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.LISCENCEE_NAME = txtSocietyName.Text;
            objBO_Finance.LISCENCEE_ADDRESS1 = txtAddress1.Text;
            objBO_Finance.LISCENCEE_ADDRESS2 = txtAddress2.Text;
            objBO_Finance.STATE_CODE = Convert.ToDouble(txtStateCode.Text);
            objBO_Finance.DIST_CODE = Convert.ToDouble(txtDistrictCode.Text);
            objBO_Finance.SOCIETY_CODE = Convert.ToDouble(txtSocietyCode.Text);
            objBO_Finance.GSTINNo = txtGSTNo.Text;

            objBO_Finance.BANK_NAME = txtBankName.Text;
            objBO_Finance.BankBranchName = txtBankBrachname.Text;
            objBO_Finance.BankBranchAddress = txtBankBranchAddress.Text;
            objBO_Finance.MICR = txtMicrCode.Text;
            objBO_Finance.IFSC = txtIFSCCode.Text;
            objBO_Finance.RegdNo = txtRegdNo.Text;
            objBO_Finance.SocietyAcno = txtSocCurAcNo.Text;

            string Regddt = txtRegdDate.Text;
            DateTime RegdSt = DateTime.ParseExact(Regddt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.RegdDate = RegdSt;


            string dateST = txtYrStartDate.Text;
            DateTime timeSt = DateTime.ParseExact(dateST, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.YEAR_START_DT = timeSt;

            //objBO_Finance.YEAR_START_DT = Convert.ToDateTime(txtYrStartDate.Text);

            string date = txtYrEndDate.Text;
            DateTime time = DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.YEAR_END_DT = time;

            string passbkdt = txtpassbkprintdt.Text;
            DateTime timep = DateTime.ParseExact(passbkdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.PassbookprintDate = timep;
            objBO_Finance.CSPAcno = Convert.ToDouble(txtcspacno.Text);
            objBO_Finance.CommLedger = Convert.ToDouble(txtcommldg.Text);
            objBO_Finance.SuspenseLdg = Convert.ToInt32(txtsuspenseldg.Text);



            DataTable dtBranchDetails = (ViewState["dtBranchDetails"] as DataTable).Copy();
            foreach (GridViewRow item in GVBranch.Rows)
            {
                if (((TextBox)item.FindControl("txtBrCode")).Text != "")
                {
                    try
                    {
                        var inst = dtBranchDetails.AsEnumerable().Where(x => Convert.ToString(x["SlNo"]) == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                        inst.SetField("BranchName", ((TextBox)item.FindControl("txtBranchName")).Text);
                        inst.SetField("BranchCode", ((TextBox)item.FindControl("txtBrCode")).Text);
                        inst.SetField("CashInHand", ((TextBox)item.FindControl("txtCashInHand")).Text);
                        inst.SetField("BrType", ((DropDownList)item.FindControl("cmbx_BrType")).SelectedValue);
                    }
                    catch (Exception ex) { }
                }
            }
            objBO_Finance.BranchDetails = dtBranchDetails;
         

            int i = objBL_Finance.InsertInitialSettings(objBO_Finance, out SQLError);

            if (i > 0)
            {

                String message = "alert('Update Successfully')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                LoadForm();
            }
            else
            {
                //rwm_Alert.RadAlert("Somthing Wrong. Error Details:  " + SQLError + "", 300, 200, "Error", "");
                String message = "alert('Somthing Wrong')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
        }
        protected void UpdateSociety()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.LISCENCEE_NAME = txtSocietyName.Text;
            objBO_Finance.LISCENCEE_ADDRESS1 = txtAddress1.Text;
            objBO_Finance.LISCENCEE_ADDRESS2 = txtAddress2.Text;
            objBO_Finance.STATE_CODE = Convert.ToDouble(txtStateCode.Text);
            objBO_Finance.DIST_CODE = Convert.ToDouble(txtDistrictCode.Text);
            objBO_Finance.SOCIETY_CODE = Convert.ToDouble(txtSocietyCode.Text);
            objBO_Finance.GSTINNo = txtGSTNo.Text;
            objBO_Finance.CROPAMT = Convert.ToDouble(txtcropinvest.Text);
            objBO_Finance.MISAMT = Convert.ToDouble(txtmiscellaneous.Text);
            objBO_Finance.BANK_NAME = txtBankName.Text;
            objBO_Finance.BankBranchName = txtBankBrachname.Text;
            objBO_Finance.BankBranchAddress = txtBankBranchAddress.Text;
            objBO_Finance.MICR = txtMicrCode.Text;
            objBO_Finance.IFSC = txtIFSCCode.Text;
            objBO_Finance.RegdNo = txtRegdNo.Text;
            objBO_Finance.SocietyAcno = txtSocCurAcNo.Text;
            string Regddt = txtRegdDate.Text;
            DateTime RegdSt = DateTime.ParseExact(Regddt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.RegdDate = RegdSt;


            //objBO_Finance.YEAR_START_DT = Convert.ToDateTime(txtYrStartDate.Text);


            string dateSt = txtYrStartDate.Text;
            DateTime timeSt = DateTime.ParseExact(dateSt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.YEAR_START_DT = timeSt;


            string date = txtYrEndDate.Text;
            DateTime time = DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.YEAR_END_DT = time;

            string passbkdt = txtpassbkprintdt.Text;
            DateTime timep = DateTime.ParseExact(passbkdt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.PassbookprintDate = timep;
            objBO_Finance.CSPAcno = Convert.ToDouble(txtcspacno.Text);
            objBO_Finance.CommLedger = Convert.ToDouble(txtcommldg.Text);
            objBO_Finance.SuspenseLdg = Convert.ToInt32(txtsuspenseldg.Text);

            DataTable dtBranchDetails = (ViewState["dtBranchDetails"] as DataTable).Copy();
            foreach (GridViewRow item in GVBranch.Rows)
            {
                if (((TextBox)item.FindControl("txtBrCode")).Text != "")
                {
                    try
                    {
                        var inst = dtBranchDetails.AsEnumerable().Where(x => Convert.ToString(x["SlNo"]) == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                        inst.SetField("BranchName", ((TextBox)item.FindControl("txtBranchName")).Text);
                        inst.SetField("BranchCode", ((TextBox)item.FindControl("txtBrCode")).Text);
                        inst.SetField("CashInHand", ((TextBox)item.FindControl("txtCashInHand")).Text);
                        inst.SetField("BrType", ((DropDownList)item.FindControl("cmbx_BrType")).SelectedValue);
                    }
                    catch (Exception ex) { throw; }
                }
            }
            objBO_Finance.BranchDetails = dtBranchDetails;
            int i = objBL_Finance.InsertUpdateInitialSettings(objBO_Finance, out SQLError);
            if (i > 0)
            {

                String message = "alert('Update Successfully')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                LoadForm();
            }
            else
            {
                //rwm_Alert.RadAlert("Somthing Wrong. Error Details:  " + SQLError + "", 300, 200, "Error", "");
                String message = "alert('Something went Wrong')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
        }
        private void LoadForm()
        {
            objBO_Finance.Flag = 1;

            DataSet dSet = objBL_Finance.GetInitialSettings(objBO_Finance, out SQLError);
            if (dSet.Tables[0].Rows.Count > 0)
            {
                txtSocietyName.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LISCENCEE_NAME"]);
                txtAddress1.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LISCENCEE_ADDRESS1"]);
                txtAddress2.Text = Convert.ToString(dSet.Tables[0].Rows[0]["LISCENCEE_ADDRESS2"]);
                txtStateCode.Text = Convert.ToString(dSet.Tables[0].Rows[0]["STATE_CODE"]);
                txtDistrictCode.Text = Convert.ToString(dSet.Tables[0].Rows[0]["DIST_CODE"]);
                txtSocietyCode.Text = Convert.ToString(dSet.Tables[0].Rows[0]["SOCIETY_CODE"]);
                txtGSTNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["GSTINNO"]);
                txtcropinvest.Text = Convert.ToString(dSet.Tables[0].Rows[0]["CropInvestmentAmt"]);
                txtmiscellaneous.Text = Convert.ToString(dSet.Tables[0].Rows[0]["MiscellaneousAmt"]);
                txtBankName.Text = Convert.ToString(dSet.Tables[0].Rows[0]["SocietyBankName"]);
                txtBankBrachname.Text = Convert.ToString(dSet.Tables[0].Rows[0]["SocietyBankBranch"]);
                txtBankBranchAddress.Text= Convert.ToString(dSet.Tables[0].Rows[0]["SocietyBankBranchAddress"]);
                txtIFSCCode.Text = Convert.ToString(dSet.Tables[0].Rows[0]["IFSCCODE"]);
                txtMicrCode.Text = Convert.ToString(dSet.Tables[0].Rows[0]["MICRNo"]);
                txtSocCurAcNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["CurrAcNo"]);
                txtRegdNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["RegdNo"]);
                txtRegdDate.Text = Convert.ToString(dSet.Tables[0].Rows[0]["RegdDate"]);

                DateTime StDate = Convert.ToDateTime(dSet.Tables[0].Rows[0]["YEAR_START_DT"]);
                DateTime EndDate = Convert.ToDateTime(dSet.Tables[0].Rows[0]["YEAR_END_DT"]);
                DateTime date = (StDate);
                DateTime date1 = (EndDate);
                txtYrStartDate.Text = date.ToString("dd/MM/yyyy");
                txtYrEndDate.Text = date1.ToString("dd/MM/yyyy");
                txtSocietyCode.Enabled = false;

                DateTime PRINTPASSDate = Convert.ToDateTime(dSet.Tables[0].Rows[0]["PRINTPASSBKDATE"]);
                DateTime date3 = (PRINTPASSDate);
                txtpassbkprintdt.Text = date3.ToString("dd/MM/yyyy");
                txtcspacno.Text= Convert.ToString(dSet.Tables[0].Rows[0]["CSPAcNo"]);
                txtcommldg.Text = Convert.ToString(dSet.Tables[0].Rows[0]["CommLdg"]);
                txtsuspenseldg.Text= Convert.ToString(dSet.Tables[0].Rows[0]["SUSPENSE_LDG"]);
                txtSession.Text = Convert.ToString(dSet.Tables[0].Rows[0]["Session"]);
                txtSocCurAcNo.Enabled = false;
                txtGSTNo.Enabled = false;

            }
            GVBranch.DataSource = dSet.Tables[1];
            GVBranch.DataBind();
            ViewState["dtBranchDetails"] = dSet.Tables[1];

            if (dSet.Tables[1].Rows.Count > 0)
            {
                int I = 0;
                foreach (GridViewRow dataitem in GVBranch.Rows)
                {
                    DropDownList cmbxbrtype = dataitem.FindControl("cmbx_BrType") as DropDownList;
                    cmbxbrtype.DataSource = dSet.Tables[1];
                    cmbxbrtype.DataTextField = "BrType";
                    cmbxbrtype.DataValueField = "BrType";
                    cmbxbrtype.DataBind();
                    cmbxbrtype.SelectedValue = Convert.ToString(dSet.Tables[1].Rows[I]["BrType"]);
                    
                    I = I + 1;
                }
            }

            btnUpdate1.Visible = true;
            btnsubmit.Visible = false;
            btnsubmit1.Visible = false;
            btnUpdate.Visible = true;
            //else
            //{
            //    SetInitialRowForBranch();
            //}


        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateSociety();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Master/frmMasterSocietySetup.aspx");
        }

        protected void GVTerms_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }
        private void SetInitialRowForBranch()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("Slno", typeof(Int32)));
            dt.Columns.Add(new DataColumn("BranchCode", typeof(Int32)));
            dt.Columns.Add(new DataColumn("BranchName", typeof(string)));
            dt.Columns.Add(new DataColumn("CashInHand", typeof(Decimal)));


            dr = dt.NewRow();
            dr["Slno"] = "1";
            dr["BranchCode"] = 0;
            dr["BranchName"] = String.Empty;
            dr["CashInHand"] = 0;


            dt.Rows.Add(dr);
            GVBranch.DataSource = dt;
            GVBranch.DataBind();

        }

        protected void GVBranch_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = ViewState["dtBranchDetails"] as DataTable;
            dt.Rows.RemoveAt(e.RowIndex);
            GVBranch.DataSource = dt;
            GVBranch.DataBind();
        }

        protected void btnsubmit1_Click(object sender, EventArgs e)
        {
            AddSocietyData();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            AddSocietyData();
        }
    }
}