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
using BLL.GeneralBL;


namespace iBankingSolution.Transaction
{
    public partial class frmAccountOpening : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        protected void Page_Load(object sender, EventArgs e)
        {
            //IntroDetails.Visible = true;
            //dtpkr_dateopen.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //dtpkr_MaturityDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (!IsPostBack)
            {
                //IntroDetails.Visible = true;
                dtpkr_dateopen.Text = DateTime.Now.ToString("dd/MM/yyyy");
                SetInitialRowForApplicationDetails();
                SetInitalRowForNominee();
                SetInitalRowForAuthorizeSign();
                GetIntroducerAcNo();
                GetInterestTransFerTo();
                GetEmpName();
                lbltype.Text = "Choose Account Type:";


                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetAccountOpeningEditData();


                }

            }
        }
        protected void GetAccountOpeningEditData()
        {
            try
            {
                objBO_Finance.Flag = 5;
                objBO_Finance.SL_CODE = lblDid.Text;
                objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                DataSet dSet = objBL_Finance.GetAccountsDetails(objBO_Finance, out SQLError);
                if (dSet.Tables[0].Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    btnsubmit1.Text = "Update";
                    btnsubmit1.CommandArgument = lblDid.Text;
                    cmbx_AcctType.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["actype"]);
                    cmbx_AcctType_SelectedIndexChanged(cmbx_AcctType, null);
                    dtpkr_dateopen.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["date_of_opening"]).ToString("dd/MM/yyyy");
                    txt_OldACNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["old_acno"]);
                    txtCBSAcNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["IFC_CODE"]);
                    cmbx_empname.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["AGN"]);
                    //ntxt_TotApplicant.MinValue = 0;
                    ntxt_TotApplicant.Text = Convert.ToString(dSet.Tables[1].Rows.Count);
                    if (dSet.Tables[2].Rows.Count > 0)
                    {
                        //objBO_Finance.Flag = 3;
                        //objBO_Finance.SCHEME_TYPE = cmbx_AcctType.SelectedValue;
                        //cmbx_DepositScheme.DataSource = objBL_Finance.GetDepositMasterRecords(objBO_Finance, out SQLError);
                        //cmbx_DepositScheme.EnableAutomaticLoadOnDemand = false;
                        cmbx_DepositScheme.DataBind();
                        cmbx_DepositScheme.SelectedValue = Convert.ToString(dSet.Tables[2].Rows[0]["DM_CODE"]);
                        //cmbx_DepositScheme.EnableAutomaticLoadOnDemand = true;
                        cmbx_Category.SelectedValue = Convert.ToString(dSet.Tables[2].Rows[0]["CATAGORY"]);
                        cmbx_IntroAcNo.SelectedValue = Convert.ToString(dSet.Tables[2].Rows[0]["INTRO_ACNO"]);
                        txt_IntroName.Text = Convert.ToString(dSet.Tables[2].Rows[0]["INTRO_NAME"]);
                        txt_IntroAddress.Text = Convert.ToString(dSet.Tables[2].Rows[0]["INTRO_ADDRESS"]);
                        txt_IntroPhone.Text = Convert.ToString(dSet.Tables[2].Rows[0]["INTRO_PHONE"]);

                        ntxt_DepositAmt.Text = Convert.ToString(dSet.Tables[2].Rows[0]["DEPOSIT_AMOUNT"]);
                        ntxt_MaturityAmt.Text = Convert.ToString(dSet.Tables[2].Rows[0]["MATURITY_AMT"]);
                        ntxt_ROI.Text = Convert.ToString(dSet.Tables[2].Rows[0]["PERCENTAGE"]);
                        if (Convert.ToString(dSet.Tables[2].Rows[0]["DATE_OF_MATURITY"]) != "")
                        {
                            dtpkr_MaturityDate.Text = Convert.ToDateTime(dSet.Tables[2].Rows[0]["DATE_OF_MATURITY"]).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            dtpkr_MaturityDate.Text = "";
                        }
                        cmbx_IntTransferredTo.SelectedValue = dSet.Tables[2].Rows[0]["INT_GOES_TO"].ToString();
                        txt_PeriodsInDays.Text = dSet.Tables[2].Rows[0]["DEPO_PRD_D"].ToString();
                        txt_PeriodsinMonth.Text = dSet.Tables[2].Rows[0]["DEPO_PRD_M"].ToString();
                    }
                    if (dSet.Tables[1].Rows.Count > 0)
                    {
                        ViewState["dtKYCDetails"] = dSet.Tables[1];
                        rgv_ClientKYC.DataSource = dSet.Tables[1];
                        rgv_ClientKYC.DataBind();
                    }
                    if (dSet.Tables[3].Rows.Count > 0)
                    {
                        txtNoofNominee.Text = Convert.ToString(dSet.Tables[3].Rows.Count);
                        ViewState["dtNomineeDetailsTable"] = dSet.Tables[3];
                        GVNominee.DataSource = dSet.Tables[3];
                        GVNominee.DataBind();
                    }
                    if (dSet.Tables[4].Rows.Count > 0)
                    {
                        ViewState["dtAuthDetailsTable"] = dSet.Tables[4];
                        GVIntroducer.DataSource = dSet.Tables[4];
                        GVIntroducer.DataBind();
                    }
                    int I = 0;
                    foreach (GridViewRow dataitem in GVIntroducer.Rows)
                    {
                        DropDownList cmbxAuthName = dataitem.FindControl("cmbx_AuthName") as DropDownList;
                        DropDownList cmbxStatus = dataitem.FindControl("cmbx_Status") as DropDownList;

                        cmbxAuthName.DataSource = dSet.Tables[1];
                        cmbxAuthName.DataTextField = "Name";
                        cmbxAuthName.DataValueField = "CUST_ID";
                        cmbxAuthName.DataBind();
                        cmbxAuthName.SelectedValue = Convert.ToString(dSet.Tables[4].Rows[I]["name"]);
                        cmbxStatus.SelectedValue = Convert.ToString(dSet.Tables[4].Rows[I]["status"]);
                        I = I + 1;

                    }
                }
                else
                {
                    MessageBox(this, "Account Not Belongs To This Branch !");
                    //message = "alert('Data Not Found')";
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }
            }

            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void GetInterestTransFerTo()
        {
            objBO_Finance.Flag = 1;

            DataTable dt = objBL_Finance.GetAccountNos(objBO_Finance, out SQLError);

            if (dt.Rows.Count > 0)
            {
                cmbx_IntTransferredTo.DataSource = dt;
                cmbx_IntTransferredTo.DataTextField = "sl_code";
                cmbx_IntTransferredTo.DataValueField = "sl_code";
                cmbx_IntTransferredTo.DataBind();


            }
            else
            {
                cmbx_IntTransferredTo.DataSource = null;
                cmbx_IntTransferredTo.DataBind();


            }

        }
        protected void GetIntroducerAcNo()
        {
            objBO_Finance.Flag = 1;

            DataTable dt = objBL_Finance.GetAccountNos(objBO_Finance, out SQLError);

            if (dt.Rows.Count > 0)
            {
                cmbx_IntroAcNo.DataSource = dt;
                cmbx_IntroAcNo.DataTextField = "sl_code";
                cmbx_IntroAcNo.DataValueField = "sl_code";
                cmbx_IntroAcNo.DataBind();


            }
            else
            {
                cmbx_IntroAcNo.DataSource = null;
                cmbx_IntroAcNo.DataBind();


            }

        }
        protected void GetDepositeScheme()
        {
            objBO_Finance.Flag = 3;
            objBO_Finance.SCHEME_TYPE = cmbx_AcctType.Text.ToString();
            DataTable dt = objBL_Finance.GetDepositMasterRecords(objBO_Finance, out SQLError);

            if (dt.Rows.Count > 0)
            {
                cmbx_DepositScheme.DataSource = dt;
                cmbx_DepositScheme.DataTextField = "SCHEME";
                cmbx_DepositScheme.DataValueField = "DM_CODE";
                cmbx_DepositScheme.DataBind();
                //cmbx_DepositScheme.Items.Add(new ListItem("Select", "0", true));

            }
            else
            {
                cmbx_DepositScheme.DataSource = null;
                cmbx_DepositScheme.DataBind();


            }
        }

        protected void GetEmpName()
        {
            objBO_Finance.Flag = 7;
            
            DataTable dt = objBL_Finance.GetEmpName(objBO_Finance, out SQLError);

            if (dt.Rows.Count > 0)
            {
                cmbx_empname.DataSource = dt;
                cmbx_empname.DataTextField = "NAME";
                cmbx_empname.DataValueField = "EMPCODE";
                cmbx_empname.DataBind();
                //cmbx_DepositScheme.Items.Add(new ListItem("Select", "0", true));

            }
            else
            {
                cmbx_empname.DataSource = null;
                cmbx_empname.DataBind();


            }
        }


        protected void MIS_INT_DET(object sender , EventArgs e)
        {
            string OpenDt = dtpkr_dateopen.Text;
            DateTime OpenDt1 = DateTime.ParseExact(OpenDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.date_of_opening = OpenDt1;

            DateTime dt = DateTime.Now;
            int priod = Int32.Parse(txt_PeriodsinMonth.Text);
            DateTime nedt = dt.AddMonths(priod);
            
            
            while (OpenDt1 <= nedt)
            {
                OpenDt1 = OpenDt1.AddMonths(1);
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            DataTable dtNomineeDetailsTable = new DataTable();
            DataTable dtAuthDetailsTable = new DataTable();
            try
            {
                objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                objBO_Finance.SL_CODE = btnsubmit.CommandArgument;
                objBO_Finance.SL_CODE = btnsubmit1.CommandArgument;
                objBO_Finance.actype = cmbx_AcctType.SelectedValue;
                objBO_Finance.ac_status = "Live";


                string OpenDt = dtpkr_dateopen.Text;
                DateTime OpenDt1 = DateTime.ParseExact(OpenDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.date_of_opening = OpenDt1;
                
                objBO_Finance.last_tran_date = OpenDt1;



                objBO_Finance.rec_int = 0;
                objBO_Finance.old_acno = txt_OldACNo.Text;
                objBO_Finance.lf_acno = txt_OldACNo.Text;
                objBO_Finance.EmpCode = "0";
                objBO_Finance.TERMINAL_ID = "0";
                objBO_Finance.Pen_ROI = 0;
                objBO_Finance.EMPCODE = cmbx_empname.SelectedValue;

                objBO_Finance.dm_code = cmbx_DepositScheme.SelectedValue;

                if (cmbx_Operation.SelectedValue != "0")
                {
                    objBO_Finance.operation = cmbx_Operation.SelectedValue;
                }
                else
                {
                    objBO_Finance.operation = "";
                }
                objBO_Finance.intro_acno = Convert.ToString(cmbx_IntroAcNo.SelectedValue) != "" ? Convert.ToString(cmbx_IntroAcNo.SelectedValue) : "";
                objBO_Finance.intro_name = txt_IntroName.Text != "" ? Convert.ToString(txt_IntroName.Text) : "";
                objBO_Finance.intro_phone = txt_IntroPhone.Text;
                objBO_Finance.intro_address = txt_IntroAddress.Text;

                objBO_Finance.deposit_amount = ntxt_DepositAmt.Text != "" ? Convert.ToDouble(ntxt_DepositAmt.Text) : 0;
                objBO_Finance.depo_prd_m = txt_PeriodsinMonth.Text != "" ? Convert.ToInt32(txt_PeriodsinMonth.Text) : 0;
                objBO_Finance.depo_prd_d = txt_PeriodsInDays.Text != "" ? Convert.ToString(txt_PeriodsInDays.Text) : "0";
                objBO_Finance.percentage = ntxt_ROI.Text != "" ? Convert.ToDouble(ntxt_ROI.Text) : 0;
                objBO_Finance.int_goes_to = cmbx_IntTransferredTo.SelectedValue!="" ? Convert.ToInt32(cmbx_IntTransferredTo.SelectedValue) : 0;

                //if (cmbx_IntTransferredTo.SelectedValue != "")

                //{
                //    objBO_Finance.int_goes_to = Convert.ToDouble(cmbx_IntTransferredTo.SelectedValue);
                //}
                //else
                //{
                //    objBO_Finance.int_goes_to = 0;
                //}

                if (dtpkr_MaturityDate.Text == "")
                {
                    objBO_Finance.date_of_maturity = null;
                }
                else
                {
                    string matDt = dtpkr_MaturityDate.Text;
                    DateTime matDt1 = DateTime.ParseExact(matDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.date_of_maturity = matDt1;

                    
                }

                objBO_Finance.maturity_amt = ntxt_MaturityAmt.Text != "" ? Convert.ToDecimal(ntxt_MaturityAmt.Text) : 0;
                objBO_Finance.int_type = cmbx_IntType.SelectedValue;

                objBO_Finance.MATURITYAMT = ntxt_MaturityAmt.Text != "" ? Convert.ToDecimal(ntxt_MaturityAmt.Text) : 0;
                objBO_Finance.FROMDATE = OpenDt1;

                if (txt_PeriodsinMonth.Text != "")
                {
                    int priod = Int32.Parse(txt_PeriodsinMonth.Text);
                    objBO_Finance.ENDDATE = OpenDt1.AddMonths(priod);
                    objBO_Finance.FROMDT = OpenDt1;
                }
                


                //if (Convert.ToString(cmbx_IntType.SelectedValue) != "")
                //{
                //    objBO_Finance.int_type = Convert.ToString(cmbx_IntType.SelectedValue);
                //}
                //else
                //{
                //    objBO_Finance.int_type = "0";
                //}
                objBO_Finance.category = Convert.ToString(cmbx_Category.SelectedValue);
                
                objBO_Finance.last_wt_date = OpenDt1;
                DataTable dtClientMaster = new DataTable();
                dtClientMaster.Columns.Add("Cust_ID");
                foreach (DataRow dr in (ViewState["dtKYCDetails"] as DataTable).Rows)
                {
                    dtClientMaster.Rows.Add(dr["Cust_ID"].ToString());
                }

                dtClientMaster.AcceptChanges();

                if (txtNoofNominee.Text != "")
                {
                    dtNomineeDetailsTable = (ViewState["dtNomineeDetailsTable"] as DataTable).Copy();

                }
                else
                {
                    dtNomineeDetailsTable = DTNoimeeDetails();
                    dtNomineeDetailsTable.Rows.Add(1, "", "", "", "", "", "", "", "");
                    GVNominee.DataSource = dtNomineeDetailsTable;
                    GVNominee.DataBind();
                    ViewState["dtNomineeDetailsTable"] = dtNomineeDetailsTable;
                    dtNomineeDetailsTable = (ViewState["dtNomineeDetailsTable"] as DataTable).Copy();
                }


                foreach (GridViewRow dataitem in GVNominee.Rows)
                {
                    TextBox txtnomn_Name = dataitem.FindControl("nomn_NameTextBox") as TextBox;

                    if (txtnomn_Name.Text != "")
                    {
                        try
                        {
                            var inst = dtNomineeDetailsTable.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)dataitem.FindControl("lbl_SlNo")).Text).First();


                            inst.SetField("nomn_Name", ((TextBox)dataitem.FindControl("nomn_NameTextBox")).Text);
                            inst.SetField("nomn_Address", ((TextBox)dataitem.FindControl("txtnomnaddress")).Text);
                            inst.SetField("nom_relation", ((DropDownList)dataitem.FindControl("cmbx_nom_Relation")).SelectedValue);
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                        }
                    }


                }

                if (txt_IntroName.Text != "")
                {
                     dtAuthDetailsTable = (ViewState["dtAuthDetailsTable"] as DataTable).Copy();
                }

                else
                {
                    dtAuthDetailsTable = DTAuthDetails();
                    dtAuthDetailsTable.Rows.Add(1, "", "", "");
                    GVIntroducer.DataSource = dtAuthDetailsTable;
                    GVIntroducer.DataBind();
                    ViewState["dtAuthDetailsTable"] = dtAuthDetailsTable;
                    dtAuthDetailsTable = (ViewState["dtAuthDetailsTable"] as DataTable).Copy();
                }
                foreach (GridViewRow item in GVIntroducer.Rows)
                {
                    DropDownList cmbxAuthName = item.FindControl("cmbx_AuthName") as DropDownList;
                    if (cmbxAuthName.SelectedValue != "")
                    {
                        try
                        {
                            var inst = dtAuthDetailsTable.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                            inst.SetField("Name", ((DropDownList)item.FindControl("cmbx_AuthName")).SelectedValue);
                            //inst.SetField("Designation", ((TextBox)item.FindControl("DesignationTextBox")).Text);
                            inst.SetField("Status", ((DropDownList)item.FindControl("cmbx_Status")).SelectedValue);
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                        }
                    }

                    else
                    {
                        try
                        {
                            var inst = dtAuthDetailsTable.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                            inst.SetField("Name", ((DropDownList)item.FindControl("cmbx_AuthName")).SelectedValue);
                            //inst.SetField("Designation", ((TextBox)item.FindControl("DesignationTextBox")).Text);
                            inst.SetField("Status", ((DropDownList)item.FindControl("cmbx_Status")).SelectedValue);
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                        }

                    }
                }

                //dtAuthDetailsTable.Columns.Remove("SlNo");


                if (dtNomineeDetailsTable.Rows.Count > 0)
                {
                    dtNomineeDetailsTable.Columns.Remove("SlNo");
                }
                dtClientMaster.AsEnumerable().Where(x => Convert.ToString(x["CUST_ID"]) == "").ToList().ForEach(dr => dr.Delete());

               

                dtNomineeDetailsTable.AsEnumerable().Where(x => Convert.ToString(x["nomn_Name"]) == "").ToList().ForEach(dr => dr.Delete());


                dtAuthDetailsTable.AsEnumerable().Where(x => Convert.ToString(x["Name"]) == "").ToList().ForEach(dr => dr.Delete());

                objBO_Finance.dtClientMaster = dtClientMaster;
                objBO_Finance.dtAuthDetailsTable = dtAuthDetailsTable;
                objBO_Finance.dtNomineeDetailsTable = dtNomineeDetailsTable;
                objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                objBO_Finance.IFCCODE = txtCBSAcNo.Text;


                int i = objBL_Finance.InsertUpdateDeleteAccountOpening(objBO_Finance, out SQLError);

                if (i > 0)
                {
                    if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                    {

                       
                        MessageBox(this, "Record Inserted Successfully . Allotted Account Number is:-" + SQLError);

                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";
                        Label1.Visible = true;
                        DivID.Visible = true;
                        Label1.Text = "Alloted Account Number is:" + SQLError;
                        ResetControls();
                        objBO_Finance.Flag = 1;


                    }
                    if (btnsubmit1.Text == "Update" || btnsubmit.Text == "Update")
                    {
                        message = "alert('Update Successfully.')";

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";

                        ResetControls();
                    }

                   
                }
                else
                {

                    message = "alert('Something Wrong Input.')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }


            }
            catch (Exception ex)
            {
                throw;
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
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            btnsubmit1.Text = "Save";
            //cmbx_AcctType.SelectedIndex = -1;
            dtpkr_dateopen.Text = String.Empty;
            dtpkr_dateopen.Text = String.Empty;
            txt_OldACNo.Text = String.Empty;
            txt_OldACNo.Text = String.Empty;
            cmbx_DepositScheme.SelectedIndex = -1;
            cmbx_Operation.SelectedIndex = -1;
            cmbx_IntroAcNo.SelectedIndex = -1;
            txt_IntroName.Text = String.Empty;
            txt_IntroPhone.Text = String.Empty;
            txt_IntroAddress.Text = String.Empty;
            ntxt_DepositAmt.Text = String.Empty;
            txt_PeriodsinMonth.Text = String.Empty;
            ntxt_ROI.Text = String.Empty;
            cmbx_IntTransferredTo.SelectedIndex = -1;
            dtpkr_MaturityDate.Text = String.Empty;
            ntxt_MaturityAmt.Text = String.Empty;
            cmbx_IntType.SelectedIndex = -1;
            cmbx_Category.SelectedIndex = -1;
            dtpkr_dateopen.Text = String.Empty;

            //GVIntroducer.DataSource = null;
            //GVIntroducer.DataBind();
            DropDownList cmbx_AuthName = GVIntroducer.Rows[0].FindControl("cmbx_AuthName") as DropDownList;
            cmbx_AuthName.SelectedIndex = -1;

            DropDownList cmbx_Status = GVIntroducer.Rows[0].FindControl("cmbx_Status") as DropDownList;
            cmbx_Status.SelectedIndex = -1;


            //GVNominee.DataSource = null;
            //GVNominee.DataBind();

            TextBox nomn_NameTextBox = GVNominee.Rows[0].FindControl("nomn_NameTextBox") as TextBox;
            nomn_NameTextBox.Text = String.Empty;

            TextBox txtnomnaddress = GVNominee.Rows[0].FindControl("txtnomnaddress") as TextBox;
            txtnomnaddress.Text = String.Empty;

            DropDownList cmbx_nom_Relation = GVNominee.Rows[0].FindControl("cmbx_nom_Relation") as DropDownList;
            cmbx_nom_Relation.SelectedIndex = -1;


            //rgv_ClientKYC.DataSource = null;
            //rgv_ClientKYC.DataBind();

            TextBox CUSTCodeTextBox = rgv_ClientKYC.Rows[0].FindControl("CUSTCodeTextBox") as TextBox;
            CUSTCodeTextBox.Text = String.Empty;

            TextBox txtName = rgv_ClientKYC.Rows[0].FindControl("txtName") as TextBox;
            txtName.Text = String.Empty;

            TextBox txtGuardianName = rgv_ClientKYC.Rows[0].FindControl("txtGuardianName") as TextBox;
            txtGuardianName.Text = String.Empty;

            TextBox txtPO = rgv_ClientKYC.Rows[0].FindControl("txtPO") as TextBox;
            txtPO.Text = String.Empty;

            TextBox txtPS = rgv_ClientKYC.Rows[0].FindControl("txtPS") as TextBox;
            txtPS.Text = String.Empty;

            TextBox txtblock = rgv_ClientKYC.Rows[0].FindControl("txtblock") as TextBox;
            txtblock.Text = String.Empty;

            TextBox txtvillage = rgv_ClientKYC.Rows[0].FindControl("txtvillage") as TextBox;
            txtvillage.Text = String.Empty;

            TextBox txtDistrict = rgv_ClientKYC.Rows[0].FindControl("txtDistrict") as TextBox;
            txtDistrict.Text = String.Empty;

            TextBox txtSex = rgv_ClientKYC.Rows[0].FindControl("txtSex") as TextBox;
            txtSex.Text = String.Empty;

            txt_PeriodsInDays.Enabled = true;
            txt_PeriodsinMonth.Enabled = true;
            ntxt_TotApplicant.Text = String.Empty;
            txtNoofNominee.Text = String.Empty;
            txtCBSAcNo.Text = String.Empty;
        }
        protected void SetInitalRowForNominee()
        {
            DataTable dtNominee = new DataTable();
            DataRow drNominee = null;
            //Define the Columns
            dtNominee.Columns.Add(new DataColumn("Slno", typeof(Int32)));
            dtNominee.Columns.Add(new DataColumn("nomn_name", typeof(string)));
            dtNominee.Columns.Add(new DataColumn("nomn_address", typeof(string)));
            dtNominee.Columns.Add(new DataColumn("cmbx_nom_Relation", typeof(string)));

            drNominee = dtNominee.NewRow();
            drNominee["Slno"] = "1";
            drNominee["nomn_name"] = String.Empty;
            drNominee["nomn_address"] = String.Empty;
            drNominee["cmbx_nom_Relation"] = String.Empty;

            dtNominee.Rows.Add(drNominee);
            GVNominee.DataSource = dtNominee;
            GVNominee.DataBind();
        }

        protected void SetInitalRowForAuthorizeSign()
        {
            DataTable dtSign = new DataTable();
            DataRow drSign = null;
            //Define the Columns
            dtSign.Columns.Add(new DataColumn("Slno", typeof(Int32)));
            dtSign.Columns.Add(new DataColumn("Name", typeof(string)));
            //dtSign.Columns.Add(new DataColumn("Designation", typeof(string)));
            dtSign.Columns.Add(new DataColumn("cmbx_Status", typeof(string)));

            drSign = dtSign.NewRow();
            drSign["Slno"] = "1";
            drSign["Name"] = String.Empty;
            //drSign["Designation"] = String.Empty;
            drSign["cmbx_Status"] = String.Empty;


            dtSign.Rows.Add(drSign);
            GVIntroducer.DataSource = dtSign;
            GVIntroducer.DataBind();

        }
        protected void SetInitialRowForApplicationDetails()
        {
            DataTable dtApplication = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dtApplication.Columns.Add(new DataColumn("Slno", typeof(Int32)));
            dtApplication.Columns.Add(new DataColumn("CUST_ID", typeof(Int32)));
            dtApplication.Columns.Add(new DataColumn("Name", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("GUARDIAN_NAME", typeof(string)));

            dtApplication.Columns.Add(new DataColumn("PO_CODE", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("PS_CODE", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("BLK_CODE", typeof(string)));

            dtApplication.Columns.Add(new DataColumn("VILL_CODE", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("DIS_CODE", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("SEX", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("REL_CODE", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("PROF_CODE", typeof(string)));

            dr = dtApplication.NewRow();
            dr["Slno"] = "1";
            dr["CUST_ID"] = 0;
            dr["Name"] = String.Empty;
            dr["GUARDIAN_NAME"] = String.Empty;
            dr["PO_CODE"] = String.Empty;
            dr["PS_CODE"] = String.Empty;
            dr["BLK_CODE"] = String.Empty;
            dr["VILL_CODE"] = String.Empty;
            dr["DIS_CODE"] = String.Empty;

            dr["SEX"] = String.Empty;
            dr["REL_CODE"] = String.Empty;
            dr["PROF_CODE"] = String.Empty;

            dtApplication.Rows.Add(dr);
            rgv_ClientKYC.DataSource = dtApplication;
            rgv_ClientKYC.DataBind();

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        private DataTable DTKYCDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SlNo"),
                    new DataColumn("CUST_ID"),
                    new DataColumn("Name"),
                    new DataColumn("GUARDIAN_NAME"),
                    new DataColumn("PO_CODE"),
                    new DataColumn("PS_CODE"),
                    new DataColumn("BLK_CODE"),
                    new DataColumn("VILL_CODE"),
                    new DataColumn("DIS_CODE"),
                    new DataColumn("SEX"),
                    new DataColumn("REL_CODE"),
                    new DataColumn("PROF_CODE")
                });
            return dt;
        }
        private DataTable DTNoimeeDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SlNo"),
                    new DataColumn("sl_code"),
                    new DataColumn("nomn_name"),
                    new DataColumn("nomn_address"),
                    new DataColumn("nom_minor"),
                    new DataColumn("age"),
                    new DataColumn("nom_relation"),
                    new DataColumn("bank_code"),
                    new DataColumn("branch_code")
                });
            return dt;
        }
        private DataTable DTAuthDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SlNo"),
                    new DataColumn("sl_code"),
                    new DataColumn("name"),
                    //new DataColumn("designation"),
                    new DataColumn("status")
                });
            return dt;
        }
        protected void ntxt_TotApplicant_TextChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(ntxt_TotApplicant.Text) > 0)
            {
                rgv_ClientKYC.Enabled = true;

                DataTable dtKYCDetails = DTKYCDetails();
                DataTable dtAuthDetailsTable = DTAuthDetails();
                int InitialRowsCount = dtKYCDetails.Rows.Count;
                for (int i = 0; i < Convert.ToInt32(ntxt_TotApplicant.Text); i++)
                {
                    dtKYCDetails.Rows.Add(i + 1, "", "", "", "", "", "", "", "", "", "", "");

                    if (Convert.ToInt32(ntxt_TotApplicant.Text) + 0 > 0)
                    {
                        dtAuthDetailsTable.Rows.Add(i + 1, "", "", "");
                    }
                }


                rgv_ClientKYC.DataSource = dtKYCDetails;
                rgv_ClientKYC.DataBind();
                ViewState["dtKYCDetails"] = dtKYCDetails;
                GVIntroducer.Enabled = true; //Convert.ToInt32(ntxt_TotApplicant.Text) + 0 > 1;
                GVIntroducer.DataSource = dtAuthDetailsTable;
                GVIntroducer.DataBind();
                ViewState["dtAuthDetailsTable"] = dtAuthDetailsTable;

                TextBox CUSTCodeTextBox = rgv_ClientKYC.Rows[0].FindControl("CUSTCodeTextBox") as TextBox;
                CUSTCodeTextBox.Focus();
            }
        }

        protected void cmbx_IntroAcNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = objBL_Finance.GetAccountHoldersDetailsBySL_CODE(2, cmbx_IntroAcNo.SelectedValue, Convert.ToString(Session["BranchID"]) , null, out SQLError).Tables[0];
            if (dt.Rows.Count > 0)
            {
                txt_IntroName.Text = Convert.ToString(dt.Rows[0]["sl_name"]);
                txt_IntroAddress.Text = "";
                txt_IntroPhone.Text = "";
            }
        }

        protected void CUSTCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 7;

            objBO_Finance.CUST_ID = ((TextBox)sender).Text;
            GridViewRow item = ((TextBox)sender).NamingContainer as GridViewRow;

            DataTable dt = objBL_Finance.GetKYCDetailsRecords(objBO_Finance, out SQLError);
            DataTable dtKYCDetails = ViewState["dtKYCDetails"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                var inst = dtKYCDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                inst.SetField("CUST_ID", Convert.ToString(dt.Rows[0]["CUST_ID"]));
                inst.SetField("Name", Convert.ToString(dt.Rows[0]["Name"]));
                inst.SetField("GUARDIAN_NAME", Convert.ToString(dt.Rows[0]["Guardian_Name"]));
                inst.SetField("PO_CODE", Convert.ToString(dt.Rows[0]["PO_Code"]));
                inst.SetField("PS_CODE", Convert.ToString(dt.Rows[0]["PS_Code"]));
                inst.SetField("BLK_CODE", Convert.ToString(dt.Rows[0]["BLK_Code"]));
                inst.SetField("VILL_CODE", Convert.ToString(dt.Rows[0]["Vill_Code"]));
                inst.SetField("DIS_CODE", Convert.ToString(dt.Rows[0]["Dist_Code"]));
                inst.SetField("SEX", Convert.ToString(dt.Rows[0]["Sex"]));
                
                foreach (GridViewRow dataitem in GVIntroducer.Rows)
                {
                    DropDownList cmbxAuthName = dataitem.FindControl("cmbx_AuthName") as DropDownList;
                    cmbxAuthName.DataSource = dtKYCDetails;
                    cmbxAuthName.DataTextField = "Name";
                    cmbxAuthName.DataValueField = "CUST_ID";
                    cmbxAuthName.DataBind();
                }
            }
            else
            {


                String message = "alert('Data Not Found')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);

                var inst = dtKYCDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                inst.SetField("CUST_ID", "");
                inst.SetField("Name", "");
                inst.SetField("GUARDIAN_NAME", "");
                inst.SetField("PO_CODE", "");
                inst.SetField("PS_CODE", "");
                inst.SetField("BLK_CODE", "");
                inst.SetField("VILL_CODE", "");
                inst.SetField("DIS_CODE", "");
                inst.SetField("SEX", "");
                inst.SetField("REL_CODE", "");
                inst.SetField("PROF_CODE", "");
            }

            rgv_ClientKYC.DataSource = dtKYCDetails;
            rgv_ClientKYC.DataBind();
            ViewState["dtKYCDetails"] = dtKYCDetails;
        }

        protected void cmbx_AcctType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDepositeScheme();
            lbltype.Text = "Selected Account Type is: " + cmbx_AcctType.SelectedItem.Text;
            if (cmbx_AcctType.SelectedIndex > 0)
            {
                pnlmain.Visible = true;
            }
            if (cmbx_AcctType.SelectedValue == "en")
            {
                //cmbx_Ledger.Enabled = true;
                cmbx_DepositScheme.Enabled = false;
                cmbx_Category.Enabled = false;
                Inttype.Visible = true;
                cmbx_Operation.Enabled = false;
                cmbx_IntroAcNo.Enabled = false;
                txt_IntroName.Enabled = false;
                txt_IntroAddress.Enabled = false;
                txt_IntroPhone.Enabled = false;
                txtCBSAcNo.Enabled = false;
                cmbx_empname.Enabled = false;
            }
            else
            {



            }
            if (cmbx_AcctType.Text == "cc" || cmbx_AcctType.Text == "fd" || cmbx_AcctType.Text == "r" || cmbx_AcctType.Text == "d" || cmbx_AcctType.Text == "mis")
            {
                Inttype.Visible = true;
                IntroDetails.Visible = true;
                txtCBSAcNo.Enabled = false;
                cmbx_empname.Enabled = false;
            }
            else
            {
                IntroDetails.Visible = false;
            }
            if (cmbx_AcctType.Text == "s")
            {
                PanelOperationAccountDetails.Visible = true;
                IntroDetails.Visible = true;
                cmbx_empname.Enabled = false;
                txtCBSAcNo.Enabled = true;
                Inttype.Visible = false;
                //Inttype.Visible = false;
            }
            else
            {
                PanelOperationAccountDetails.Visible = false;
            }

            if (cmbx_AcctType.Text == "jlg" || cmbx_AcctType.Text == "sus" || cmbx_AcctType.Text == "nf")
            {
                Inttype.Visible = false;
                IntroDetails.Visible = true;
                cmbx_empname.Enabled = false;
                //txtCBSAcNo.Enabled = false;
            }
            else
            { 
                //Inttype.Visible = true;
                //IntroDetails.Visible = true;
                //cmbx_empname.Enabled = true;
                //txtCBSAcNo.Enabled = false;
            }
        }

        protected void txtNoofNominee_TextChanged(object sender, EventArgs e)
        {
            DataTable dtNomineeDetailsTable = DTNoimeeDetails();
            for (int i = 0; i < Convert.ToInt32(txtNoofNominee.Text); i++)
            {
                dtNomineeDetailsTable.Rows.Add(i + 1, "", "", "", "", "", "", "", "");
            }

            GVNominee.DataSource = dtNomineeDetailsTable;
            GVNominee.DataBind();
            ViewState["dtNomineeDetailsTable"] = dtNomineeDetailsTable;

            TextBox nomn_NameTextBox = GVNominee.Rows[0].FindControl("nomn_NameTextBox") as TextBox;
            nomn_NameTextBox.Focus();
        }

        protected void ntxt_ROI_TextChanged(object sender, EventArgs e)
        {
            if (txt_PeriodsinMonth.Text != "" && ntxt_ROI.Text != "" && ntxt_DepositAmt.Text != "")
            {
                if (txt_PeriodsInDays.Text == "")
                {
                    txt_PeriodsInDays.Text = "0";
                }
                if (txt_PeriodsinMonth.Text == "")
                {
                    txt_PeriodsinMonth.Text = "0";
                }
                CalculateMaturity CalcMat = new CalculateMaturity();
                ntxt_MaturityAmt.Text = Convert.ToString(CalcMat.MaturityCalculation(Convert.ToString(cmbx_AcctType.SelectedValue), Convert.ToDouble(ntxt_DepositAmt.Text), Convert.ToInt32(txt_PeriodsinMonth.Text), Convert.ToInt32(txt_PeriodsInDays.Text), Convert.ToDouble(ntxt_ROI.Text)));

                if (cmbx_AcctType.Text == "d")
                {
                    string dtop = dtpkr_dateopen.Text;
                    DateTime dtopen = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime ndt = dtopen.AddMonths(Convert.ToInt32(txt_PeriodsinMonth.Text));
                    double dtdiff = Convert.ToDouble((Convert.ToDateTime(ndt) - Convert.ToDateTime(dtopen)).TotalDays);
                    double DepositAmt = Convert.ToDouble(ntxt_DepositAmt.Text);
                    double ROI = Convert.ToDouble(ntxt_ROI.Text);
                    ntxt_MaturityAmt.Text = Convert.ToString(Convert.ToDouble(Math.Round((dtdiff * ROI * DepositAmt) / 100)));
                }

                CalculateMaturityDate();
            }
            ntxt_DepositAmt.Focus();
        }
        protected void CalculateMaturityDate()
        {
            CalculateMaturity CalcMat = new CalculateMaturity();
            if (txt_PeriodsInDays.Text == "")
            {
                txt_PeriodsInDays.Text = "0";
            }
            if (txt_PeriodsinMonth.Text == "")
            {
                txt_PeriodsinMonth.Text = "0";
            }
            string dtop = dtpkr_dateopen.Text;
            DateTime dtopen = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//102852
            dtpkr_MaturityDate.Text = CalcMat.CalculateMaturityDate(dtopen, Convert.ToInt32(txt_PeriodsinMonth.Text), Convert.ToInt32(txt_PeriodsInDays.Text)).ToString("dd/MM/yyyy");
            //dtpkr_MaturityDate.Text = CalcMat.CalculateMaturityDate(Convert.ToDateTime(dtpkr_dateopen.Text), Convert.ToInt32(txt_PeriodsinMonth.Text), Convert.ToInt32(txt_PeriodsInDays.Text)).ToString("dd/MM/yyyy");

        }
        protected void ntxt_DepositAmt_TextChanged(object sender, EventArgs e)
        {
            if (txt_PeriodsInDays.Text == "")
            {
                txt_PeriodsInDays.Text = "0";
            }
            if (txt_PeriodsinMonth.Text == "")
            {
                txt_PeriodsinMonth.Text = "0";
            }
            CalculateMaturity CalcMat = new CalculateMaturity();
            ntxt_MaturityAmt.Text = Convert.ToString(CalcMat.MaturityCalculation(Convert.ToString(cmbx_AcctType.SelectedValue), Convert.ToDouble(ntxt_DepositAmt.Text), Convert.ToInt32(txt_PeriodsinMonth.Text), Convert.ToInt32(txt_PeriodsInDays.Text), Convert.ToDouble(ntxt_ROI.Text)));
            
            if (cmbx_AcctType.Text == "d")
            {
                string dtop = dtpkr_dateopen.Text;
                DateTime dtopen = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime ndt = dtopen.AddMonths(Convert.ToInt32(txt_PeriodsinMonth.Text));
                double dtdiff = Convert.ToDouble((Convert.ToDateTime(ndt) - Convert.ToDateTime(dtopen)).TotalDays);
                double DepositAmt = Convert.ToDouble(ntxt_DepositAmt.Text);
                double ROI = Convert.ToDouble(ntxt_ROI.Text);
                ntxt_MaturityAmt.Text = Convert.ToString(Convert.ToDouble(Math.Round((dtdiff * ROI * DepositAmt) / 100)));
            }
            
            
            
            CalculateMaturityDate();
            //dtpkr_MaturityDate.Text = CalcMat.CalculateMaturityDate(Convert.ToDateTime(dtpkr_dateopen.Text), Convert.ToInt32(txt_PeriodsinMonth.Text), Convert.ToInt32(txt_PeriodsInDays.Text)).ToString("dd/MM/yyyy");

        }

        protected void txt_PeriodsinMonth_TextChanged(object sender, EventArgs e)
        {
            //if (txt_PeriodsinMonth.Text != "")
            //{
            //    txt_PeriodsInDays.Enabled = false;
            //    txt_PeriodsinMonth.Enabled = true;
            //}
            //else
            //{
            //    txt_PeriodsInDays.Enabled = true;
            //    txt_PeriodsinMonth.Enabled = false;
            //}
            ntxt_ROI.Focus();

            if (ntxt_ROI.Text != "" && ntxt_DepositAmt.Text != "")
            {
                if (txt_PeriodsInDays.Text == "")
                {
                    txt_PeriodsInDays.Text = "0";
                }
                if (txt_PeriodsinMonth.Text == "")
                {
                    txt_PeriodsinMonth.Text = "0";
                }
                CalculateMaturity CalcMat = new CalculateMaturity();
                ntxt_MaturityAmt.Text = Convert.ToString(CalcMat.MaturityCalculation(Convert.ToString(cmbx_AcctType.SelectedValue), Convert.ToDouble(ntxt_DepositAmt.Text), Convert.ToInt32(txt_PeriodsinMonth.Text), Convert.ToInt32(txt_PeriodsInDays.Text), Convert.ToDouble(ntxt_ROI.Text)));

                if (cmbx_AcctType.Text == "d")
                {
                    string dtop = dtpkr_dateopen.Text;
                    DateTime dtopen = DateTime.ParseExact(dtop, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime ndt = dtopen.AddMonths(Convert.ToInt32(txt_PeriodsinMonth.Text));
                    double dtdiff = Convert.ToDouble((Convert.ToDateTime(ndt) - Convert.ToDateTime(dtopen)).TotalDays);
                    double DepositAmt = Convert.ToDouble(ntxt_DepositAmt.Text);
                    double ROI = Convert.ToDouble(ntxt_ROI.Text);
                    ntxt_MaturityAmt.Text = Convert.ToString(Convert.ToDouble(Math.Round((dtdiff * ROI * DepositAmt) / 100)));
                }



                CalculateMaturityDate();
            }

        }

        protected void txt_PeriodsInDays_TextChanged(object sender, EventArgs e)
        {
            //if (txt_PeriodsInDays.Text != "")
            //{
            //    txt_PeriodsinMonth.Enabled = false;
            //    txt_PeriodsInDays.Enabled = true;

            //}
            //else
            //{
            //    txt_PeriodsinMonth.Enabled = true;
            //    txt_PeriodsInDays.Enabled = false;
            //}
            ntxt_ROI.Focus();

        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)//SELECT DROP DOWN
        {
            if (cmbx_ddlsearch.SelectedIndex > 0)
            {
                txtsearchkyc.Focus();
            }

        }

        protected void txtsearchAccount_TextChanged(object sender, EventArgs e) //EDIT Account
        {
            try
            {
                objBO_Finance.Flag = 5;
                objBO_Finance.SL_CODE = txtsearchkyc.Text;
                objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                DataSet dSet = objBL_Finance.GetAccountsDetails(objBO_Finance, out SQLError);
                if (dSet.Tables[0].Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    btnsubmit1.Text = "Update";
                    btnsubmit1.CommandArgument = txtsearchkyc.Text;
                    cmbx_AcctType.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["actype"]);
                    cmbx_AcctType_SelectedIndexChanged(cmbx_AcctType, null);
                    dtpkr_dateopen.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["date_of_opening"]).ToString("dd/MM/yyyy");
                    txt_OldACNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["old_acno"]);
                    txtCBSAcNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["IFC_CODE"]);
                    if (cmbx_empname.SelectedValue != "0")
                        cmbx_empname.SelectedValue = Convert.ToString(dSet.Tables[0].Rows[0]["AGN"]);
                    else
                        cmbx_empname.SelectedValue = "0";
                    ntxt_TotApplicant.Text = Convert.ToString(dSet.Tables[1].Rows.Count);
                    if (dSet.Tables[2].Rows.Count > 0)
                    {
                        
                        cmbx_DepositScheme.DataBind();
                        cmbx_DepositScheme.SelectedValue = Convert.ToString(dSet.Tables[2].Rows[0]["DM_CODE"]);
                        cmbx_Category.SelectedValue = Convert.ToString(dSet.Tables[2].Rows[0]["CATAGORY"]);
                        //if (cmbx_IntroAcNo.SelectedValue != "0")
                        //    cmbx_IntroAcNo.SelectedValue = Convert.ToString(dSet.Tables[2].Rows[0]["INTRO_ACNO"]);
                        //else
                        //    cmbx_IntroAcNo.SelectedValue = "0";
                            txt_IntroName.Text = Convert.ToString(dSet.Tables[2].Rows[0]["INTRO_NAME"]);
                        txt_IntroAddress.Text = Convert.ToString(dSet.Tables[2].Rows[0]["INTRO_ADDRESS"]);
                        txt_IntroPhone.Text = Convert.ToString(dSet.Tables[2].Rows[0]["INTRO_PHONE"]);

                        ntxt_DepositAmt.Text = Convert.ToString(dSet.Tables[2].Rows[0]["DEPOSIT_AMOUNT"]);
                        ntxt_MaturityAmt.Text = Convert.ToString(dSet.Tables[2].Rows[0]["MATURITY_AMT"]);
                        ntxt_ROI.Text = Convert.ToString(dSet.Tables[2].Rows[0]["PERCENTAGE"]);
                        if (Convert.ToString(dSet.Tables[2].Rows[0]["DATE_OF_MATURITY"]) != "")
                        {
                            dtpkr_MaturityDate.Text = Convert.ToDateTime(dSet.Tables[2].Rows[0]["DATE_OF_MATURITY"]).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            dtpkr_MaturityDate.Text = "";
                        }
                        //cmbx_IntTransferredTo.SelectedValue = dSet.Tables[2].Rows[0]["INT_GOES_TO"].ToString();
                        txt_PeriodsInDays.Text = dSet.Tables[2].Rows[0]["DEPO_PRD_D"].ToString();
                        txt_PeriodsinMonth.Text = dSet.Tables[2].Rows[0]["DEPO_PRD_M"].ToString();
                    }
                    if (dSet.Tables[1].Rows.Count > 0)
                    {
                        ViewState["dtKYCDetails"] = dSet.Tables[1];
                        rgv_ClientKYC.DataSource = dSet.Tables[1];
                        rgv_ClientKYC.DataBind();
                    }
                    if (dSet.Tables[3].Rows.Count > 0)
                    {
                        txtNoofNominee.Text = Convert.ToString(dSet.Tables[3].Rows.Count);
                        ViewState["dtNomineeDetailsTable"] = dSet.Tables[3];
                        GVNominee.DataSource = dSet.Tables[3];
                        GVNominee.DataBind();
                    }
                    if (dSet.Tables[4].Rows.Count > 0)
                    {
                        ViewState["dtAuthDetailsTable"] = dSet.Tables[4];
                        GVIntroducer.DataSource = dSet.Tables[4];
                        GVIntroducer.DataBind();
                    }
                    int I = 0;
                    foreach (GridViewRow dataitem in GVIntroducer.Rows)
                    {
                        DropDownList cmbxAuthName = dataitem.FindControl("cmbx_AuthName") as DropDownList;
                        DropDownList cmbxStatus = dataitem.FindControl("cmbx_Status") as DropDownList;

                        cmbxAuthName.DataSource = dSet.Tables[1];
                        cmbxAuthName.DataTextField = "Name";
                        cmbxAuthName.DataValueField = "CUST_ID";
                        cmbxAuthName.DataBind();
                        //cmbxAuthName.SelectedValue = Convert.ToString(dSet.Tables[4].Rows[I]["name"]);
                        //cmbxStatus.SelectedValue = Convert.ToString(dSet.Tables[4].Rows[I]["status"]);
                        I = I + 1;

                    }
                    txtsearchkyc.Text = String.Empty;
                }
                else
                {
                    MessageBox(this, "Account Not Belongs To This Branch !");
                    
                }
            }

            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
    }
}