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

namespace iBankingSolution.Transaction
{
    public partial class frmSHGAccountOpening : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDtOpen.Text = DateTime.Now.ToString("dd/MM/yyyy");
                SetInitalRowForAuthorizeSign();
                SetInitialRowForApplicationDetails();
                GetDepositeScheme();
                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetAccountOpeningSHGEditData();


                }
            }
        }
        protected void GetAccountOpeningSHGEditData()
        {
            try
            {
                objBO_Finance.Flag = 5;
                objBO_Finance.SL_CODE = lblDid.Text;
                DataSet dSet = objBL_Finance.GetAccountsDetails(objBO_Finance, out SQLError);
                if (dSet.Tables[0].Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    btnsubmit1.Text = "Update";
                    btnsubmit1.CommandArgument = lblDid.Text;
                    btnsubmit.CommandArgument = lblDid.Text;
                    txtDtOpen.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["date_of_opening"]).ToString("dd/MM/yyyy");
                    txtOldAcNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["old_acno"]);
                    ntxt_NoOfMembers.Text = Convert.ToString(dSet.Tables[1].Rows.Count);
                    if (dSet.Tables[2].Rows.Count > 0)
                    {
                        //objBO_Finance.Flag = 3;
                        //objBO_Finance.SCHEME_TYPE = cmbx_AcctType.SelectedValue;
                        //cmbx_DepositScheme.DataSource = objBL_Finance.GetDepositMasterRecords(objBO_Finance, out SQLError);
                        //cmbx_DepositScheme.EnableAutomaticLoadOnDemand = false;
                        //cmbx_DepositScheme.DataBind();
                        cmbx_DepositScheme.SelectedValue = Convert.ToString(dSet.Tables[2].Rows[0]["DM_CODE"]);
                        //cmbx_DepositScheme.EnableAutomaticLoadOnDemand = true;
                    }
                    if (dSet.Tables[1].Rows.Count > 0)
                    {
                        ViewState["dtGroupDetails"] = dSet.Tables[1];
                        GVApplicantDtl.DataSource = dSet.Tables[1];
                        GVApplicantDtl.DataBind();
                    }

                    if (dSet.Tables[4].Rows.Count > 0)
                    {
                        ViewState["dtAuthDetailsTable"] = dSet.Tables[4];
                        GvSignature.DataSource = dSet.Tables[4];
                        GvSignature.DataBind();
                    }

                    int Count = 0;
                    foreach (GridViewRow dataitem in GvSignature.Rows)
                    {

                        DropDownList ddlOption = (DropDownList)dataitem.FindControl("cmbx_AuthSignStatus");
                        ddlOption.SelectedValue = dSet.Tables[4].Rows[Count]["status"].ToString();
                        if (Count < 2)
                        {
                            ((TextBox)dataitem.FindControl("lbl_MemberName")).Visible = true;
                            ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).Visible = false;
                        }
                        else
                        {
                            ((TextBox)dataitem.FindControl("lbl_MemberName")).Visible = false;
                            ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).Visible = true;
                            ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).DataSource = dSet.Tables[5];
                            ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).DataBind();
                        }
                        Count++;
                    }
                }
                else
                {
                    message = "Data Not Found";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

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
        protected void GetDepositeScheme()
        {
            objBO_Finance.Flag = 3;
            objBO_Finance.SCHEME_TYPE = "s";
            cmbx_DepositScheme.DataSource = objBL_Finance.GetDepositMasterRecordsForSHG(objBO_Finance, out SQLError);
            cmbx_DepositScheme.DataTextField = "SCHEME";
            cmbx_DepositScheme.DataValueField = "DM_Code";
            cmbx_DepositScheme.DataBind();
        }
        protected void SetInitalRowForAuthorizeSign()
        {
            DataTable dtSign = new DataTable();
            DataRow drSign = null;
            //Define the Columns
            dtSign.Columns.Add(new DataColumn("Slno", typeof(Int32)));
            dtSign.Columns.Add(new DataColumn("sl_code", typeof(Int32)));
            dtSign.Columns.Add(new DataColumn("Name", typeof(string)));
            dtSign.Columns.Add(new DataColumn("Designation", typeof(string)));
            dtSign.Columns.Add(new DataColumn("Status", typeof(string)));

            drSign = dtSign.NewRow();
            drSign["Slno"] = "1";
            drSign["sl_code"] = "0";
            drSign["Name"] = String.Empty;
            drSign["Designation"] = String.Empty;
            drSign["Status"] = String.Empty;


            dtSign.Rows.Add(drSign);
            GvSignature.DataSource = dtSign;
            GvSignature.DataBind();

        }
        private void SetInitialRowForApplicationDetails()
        {
            DataTable dtApplication = new DataTable();
            DataRow dr = null;
            //Define the Columns
            dtApplication.Columns.Add(new DataColumn("SlNo", typeof(Int32)));
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
            dr["SlNo"] = "1";
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
            GVApplicantDtl.DataSource = dtApplication;
            GVApplicantDtl.DataBind();

        }
        private DataTable dtGroupDetails()
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
        private DataTable dtAuthSignatoryDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SlNo"),
                    new DataColumn("sl_code"),
                    new DataColumn("Name"),
                    new DataColumn("Designation"),
                    new DataColumn("Status")
                });
            return dt;


        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        private void MsgBox(string sMessage)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + sMessage + "');";
            msg += "</script>";
            Response.Write(msg);
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                objBO_Finance.SL_CODE = btnsubmit.CommandArgument;
                objBO_Finance.actype = "shg";
                objBO_Finance.ac_status = "Live";

                string dt = txtDtOpen.Text;
                DateTime OpenDt1 = DateTime.ParseExact(dt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.date_of_opening = OpenDt1;
                //objBO_Finance.date_of_opening = Convert.ToDateTime(txtDtOpen.Text);
                objBO_Finance.old_acno = txtOldAcNo.Text;
                objBO_Finance.dm_code = cmbx_DepositScheme.SelectedValue;
                objBO_Finance.EmpCode = "100";

                DataTable dtClientMaster = new DataTable();
                dtClientMaster.Columns.Add("CUST_ID");
                foreach (DataRow dr in (ViewState["dtGroupDetails"] as DataTable).Rows)
                {
                    dtClientMaster.Rows.Add(dr["CUST_ID"].ToString());
                }
                dtClientMaster.AcceptChanges();

                DataTable dtAuthDetailsTable = (ViewState["dtAuthDetailsTable"] as DataTable).Copy();
                int Count = 0;
                foreach (GridViewRow dataitem in GvSignature.Rows)
                {
                    var inst = dtAuthDetailsTable.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)dataitem.FindControl("lbl_SlNo")).Text).First();
                    inst.SetField("Name", Count < 2 ? ((TextBox)dataitem.FindControl("lbl_MemberName")).Text : ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).SelectedValue);
                    inst.SetField("Status", ((DropDownList)dataitem.FindControl("cmbx_AuthSignStatus")).SelectedValue);

                    Count++;
                }

                dtAuthDetailsTable.Columns.Remove("SlNo");

                dtClientMaster.AsEnumerable().Where(x => Convert.ToString(x["CUST_ID"]) == "").ToList().ForEach(dr => dr.Delete());
                dtAuthDetailsTable.AsEnumerable().Where(x => Convert.ToString(x["Name"]) == "").ToList().ForEach(dr => dr.Delete());

                objBO_Finance.dtClientMaster = dtClientMaster;
                objBO_Finance.dtAuthDetailsTable = dtAuthDetailsTable;
                objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

                int i = objBL_Finance.InsertUpdateDeleteAccountOpening(objBO_Finance, out SQLError);
                if (i > 0)
                {
                    if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                    {

                        //MessageBox(this, "Record Inserted Successfully . Allotted CUST ID is:-" + SQLError);
                        MsgBox("Record Inserted Successfully . Allotted CUST ID is:-" + SQLError);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";
                        Label1.Visible = false;
                        DivID.Visible = true;
                        Label1.Text = "Alloted CustId is:" + SQLError;
                        objBO_Finance.Flag = 1;
                        ResetControls();


                    }
                    if (btnsubmit1.Text == "Update")
                    {
                        message = "alert('Update Successfully.')";

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        btnsubmit.Text = "Save";
                        btnsubmit1.Text = "Save";

                        ResetControls();
                    }

                    else
                    {

                        //message = "alert('Something Wrong Input.')";
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    }
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
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            btnsubmit1.Text = "Save";
            txtDtOpen.Text = String.Empty;
            txtOldAcNo.Text = String.Empty;

            cmbx_DepositScheme.SelectedIndex = -1;

            SetInitialRowForApplicationDetails();
            GetDepositeScheme();
        }
        protected void GroupIDTextBox_TextChanged(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.CUST_ID = ((TextBox)sender).Text;
            GridViewRow item = ((TextBox)sender).NamingContainer as GridViewRow;
            DataSet dSet = objBL_Finance.GetSHGClientRecords(objBO_Finance, out SQLError);
            DataTable dtAuthDetailsTable = dtAuthSignatoryDetails();
            DataTable dtGroupDetails = new DataTable();
            if (Convert.ToInt32(ntxt_NoOfMembers.Text) > 0)
            {
                dtGroupDetails = ViewState["dtGroupDetails"] as DataTable;
            }
            //else
            //{
            //    dtGroupDetails.Rows.Add(1, "", "", "", "", "", "", "", "", "", "", "");

            //    ViewState["dtGroupDetails"] = dtGroupDetails;
            //    dtGroupDetails = ViewState["dtGroupDetails"] as DataTable;
            //}
            if (dSet.Tables[0].Rows.Count > 0)
            {
                var inst = dtGroupDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                inst.SetField("CUST_ID", Convert.ToString(dSet.Tables[0].Rows[0]["CUST_ID"]));
                inst.SetField("Name", Convert.ToString(dSet.Tables[0].Rows[0]["Name"]));
                inst.SetField("GUARDIAN_NAME", Convert.ToString(dSet.Tables[0].Rows[0]["Guardian_Name"]));
                inst.SetField("PO_CODE", Convert.ToString(dSet.Tables[0].Rows[0]["PO_Code"]));
                inst.SetField("PS_CODE", Convert.ToString(dSet.Tables[0].Rows[0]["PS_Code"]));
                inst.SetField("BLK_CODE", Convert.ToString(dSet.Tables[0].Rows[0]["BLK_Code"]));
                inst.SetField("VILL_CODE", Convert.ToString(dSet.Tables[0].Rows[0]["Vill_Code"]));
                inst.SetField("DIS_CODE", Convert.ToString(dSet.Tables[0].Rows[0]["DIS_CODE"]));
                inst.SetField("SEX", Convert.ToString(dSet.Tables[0].Rows[0]["Sex"]));

                dtAuthDetailsTable.Rows.Add(1, "", dSet.Tables[0].Rows[0]["Guardian_Name"], "Group Leader", "");
                dtAuthDetailsTable.Rows.Add(2, "", dSet.Tables[0].Rows[0]["Guardian"], "Ast. Group Leader", "");
            }
            else
            {
                message = "Data Not Found";

                var inst = dtGroupDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                inst.SetField("CUST_ID", "");
                inst.SetField("Name", "");
                inst.SetField("GUARDIAN_NAME", "");
                inst.SetField("PO_CODE", "");
                inst.SetField("PS_CODE", "");
                inst.SetField("BLK_CODE", "");
                inst.SetField("VILL_CODE", "");
                inst.SetField("DIS_CODE", "");
                inst.SetField("SEX", "");
            }

            GVApplicantDtl.DataSource = dtGroupDetails;
            GVApplicantDtl.DataBind();
            ViewState["dtGroupDetails"] = dtGroupDetails;

            for (int i = 0; i < dSet.Tables[1].Rows.Count; i++)
            {
                dtAuthDetailsTable.Rows.Add(i + 3, "", "", "Group Member", "");
            }

            GvSignature.DataSource = dtAuthDetailsTable;
            GvSignature.DataBind();
            ViewState["dtAuthDetailsTable"] = dtAuthDetailsTable;
            int Count = 0;
            foreach (GridViewRow dataitem in GvSignature.Rows)
            {
                if (Count < 2)
                {
                    ((TextBox)dataitem.FindControl("lbl_MemberName")).Visible = true;
                    ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).Visible = false;
                }
                else
                {
                    ((TextBox)dataitem.FindControl("lbl_MemberName")).Visible = false;
                    ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).Visible = true;
                    //((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).DataSource = dSet.Tables[1];
                    //((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).DataBind();
                }
                Count++;
            }
        }

        protected void ntxt_NoOfMembers_TextChanged(object sender, EventArgs e)
        {
            DataTable dtGroupDetailsTable = dtGroupDetails();
            DataTable dtAuthDetailsTable = dtAuthSignatoryDetails();
            int InitialRowsCount = dtGroupDetailsTable.Rows.Count;
            for (int i = 0; i < Convert.ToInt32(ntxt_NoOfMembers.Text); i++)
            {
                dtGroupDetailsTable.Rows.Add(i + 1, "", "", "", "", "", "", "", "", "", "", "");
            }

            //rgv_AuthSignatoryDetails.Enabled = Convert.ToInt32(ntxt_NoOfMembers.Value) + 0 > 1;

            GVApplicantDtl.DataSource = dtGroupDetailsTable;
            GVApplicantDtl.DataBind();
            ViewState["dtGroupDetails"] = dtGroupDetailsTable;

            //rgv_AuthSignatoryDetails.DataSource = dtAuthDetailsTable;
            //rgv_AuthSignatoryDetails.DataBind();
            //ViewState["dtAuthDetailsTable"] = dtAuthDetailsTable;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

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
                DataSet dSet = objBL_Finance.GetAccountsDetails(objBO_Finance, out SQLError);
                if (dSet.Tables[0].Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    btnsubmit1.Text = "Update";
                    btnsubmit1.CommandArgument = txtsearchkyc.Text;
                    btnsubmit.CommandArgument = txtsearchkyc.Text;
                    txtDtOpen.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["date_of_opening"]).ToString("dd/MM/yyyy");
                    txtOldAcNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["old_acno"]);
                    ntxt_NoOfMembers.Text = Convert.ToString(dSet.Tables[1].Rows.Count);
                    if (dSet.Tables[2].Rows.Count > 0)
                    {
                        //objBO_Finance.Flag = 3;
                        //objBO_Finance.SCHEME_TYPE = cmbx_AcctType.SelectedValue;
                        //cmbx_DepositScheme.DataSource = objBL_Finance.GetDepositMasterRecords(objBO_Finance, out SQLError);
                        //cmbx_DepositScheme.EnableAutomaticLoadOnDemand = false;
                        //cmbx_DepositScheme.DataBind();
                        cmbx_DepositScheme.SelectedValue = Convert.ToString(dSet.Tables[2].Rows[0]["DM_CODE"]);
                        //cmbx_DepositScheme.EnableAutomaticLoadOnDemand = true;
                    }
                    if (dSet.Tables[1].Rows.Count > 0)
                    {
                        ViewState["dtGroupDetails"] = dSet.Tables[1];
                        GVApplicantDtl.DataSource = dSet.Tables[1];
                        GVApplicantDtl.DataBind();
                    }

                    if (dSet.Tables[4].Rows.Count > 0)
                    {
                        ViewState["dtAuthDetailsTable"] = dSet.Tables[4];
                        GvSignature.DataSource = dSet.Tables[4];
                        GvSignature.DataBind();
                    }

                    int Count = 0;
                    foreach (GridViewRow dataitem in GvSignature.Rows)
                    {

                        DropDownList ddlOption = (DropDownList)dataitem.FindControl("cmbx_AuthSignStatus");
                        ddlOption.SelectedValue = dSet.Tables[4].Rows[Count]["status"].ToString();
                        if (Count < 2)
                        {
                            ((TextBox)dataitem.FindControl("lbl_MemberName")).Visible = true;
                            ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).Visible = false;
                        }
                        else
                        {
                            ((TextBox)dataitem.FindControl("lbl_MemberName")).Visible = false;
                            ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).Visible = true;
                            ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).DataSource = dSet.Tables[5];
                            ((DropDownList)dataitem.FindControl("cmbx_AuthSignatoryName")).DataBind();
                        }
                        Count++;
                    }
                }
                else
                {
                    message = "Data Not Found";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

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
    }
}