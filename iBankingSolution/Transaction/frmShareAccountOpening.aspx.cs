using System;
using System.Data;
using BusinessObject;
using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iBankingSolution.Transaction
{
    public partial class frmShareAccountOpening : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtpkr_dateopen.Text = DateTime.Now.ToString("dd/MM/yyyy");
                SetInitialRowForApplicationDetails();
                SetInitalRowForNominee();
                BindLedger();

                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    GetAccountOpeningEditData();


                }

            }
        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)//FOR SHOWING MESSAGE
        {

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        protected void BindLedger()//Select Ledger
        {
            try
            {
                objBO_Finance.Flag = 18;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_Ledger.DataSource = dt;
                    cmbx_Ledger.DataValueField = "NOMENCLATURE";
                    cmbx_Ledger.DataTextField = "NOMENCLATURE";
                    cmbx_Ledger.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void SetInitialRowForApplicationDetails()//THIS INITIAL ROWS FOR APPLICATION DETAILS
        {
            DataTable dtApplication = new DataTable();
            DataRow dr = null;

            dtApplication.Columns.Add(new DataColumn("Slno", typeof(Int32)));
            dtApplication.Columns.Add(new DataColumn("CUST_ID", typeof(Int32)));
            dtApplication.Columns.Add(new DataColumn("Name", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("GUARDIAN_NAME", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("VILL_CODE", typeof(string)));
            dtApplication.Columns.Add(new DataColumn("DIS_CODE", typeof(string)));


            dr = dtApplication.NewRow();
            dr["Slno"] = "1";
            dr["CUST_ID"] = 0;
            dr["Name"] = String.Empty;
            dr["GUARDIAN_NAME"] = String.Empty;
            dr["VILL_CODE"] = String.Empty;
            dr["DIS_CODE"] = String.Empty;

            dtApplication.Rows.Add(dr);
            gv_ClientDetails.DataSource = dtApplication;
            gv_ClientDetails.DataBind();

        }
        protected void SetInitalRowForNominee()//THIS INITIAL ROWS FOR NOMINEE DETAILS
        {
            DataTable dtNominee = new DataTable();
            DataRow drNominee = null;
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
        private DataTable dtApplicantDetails()//DATA TABLE FOR APPLICATION DETAILS
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SlNo"),
                    new DataColumn("CUST_ID"),
                    new DataColumn("Name"),
                    new DataColumn("GUARDIAN_NAME"),
                    new DataColumn("VILL_CODE"),
                    new DataColumn("DIS_CODE"),

                });
            return dt;
        }
        private DataTable DTNoimeeDetails()//DATA TABLE FOR NOMINEE
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

        protected void ntxt_TotApplicant_TextChanged(object sender, EventArgs e) //FOR APPLICATION DETAILS
        {
            DataTable dt = dtApplicantDetails();
            for (int i = dt.Rows.Count; i < Convert.ToInt32(ntxt_TotApplicant.Text); i++)
            {
                {
                    dt.Rows.Add(i + 1, "", "", "", "", "");
                }

                gv_ClientDetails.DataSource = dt;
                gv_ClientDetails.DataBind();
                ViewState["dtApplicantDetails"] = dt;

            }

        }
        protected void txtNoofNominee_TextChanged(object sender, EventArgs e) //FOR NOMINEE
        {
            DataTable dtNomineeDetailsTable = DTNoimeeDetails();
            for (int i = 0; i < Convert.ToInt32(txtNoofNominee.Text); i++)
            {
                dtNomineeDetailsTable.Rows.Add(i + 1, "", "", "", "", "", "", "", "");
            }

            GVNominee.DataSource = dtNomineeDetailsTable;
            GVNominee.DataBind();
            ViewState["dtNomineeDetailsTable"] = dtNomineeDetailsTable;


        }

        protected void CUSTCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 8;
            objBO_Finance.CUST_ID = ((TextBox)sender).Text;
            GridViewRow item = ((TextBox)sender).NamingContainer as GridViewRow;
            DataTable dt = objBL_Finance.GetKYCDetailsRecords(objBO_Finance, out SQLError);
            DataTable dtApplicantDetails = ViewState["dtApplicantDetails"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                var inst = dtApplicantDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                inst.SetField("CUST_ID", Convert.ToString(dt.Rows[0]["CUST_ID"]));
                inst.SetField("Name", Convert.ToString(dt.Rows[0]["Name"]));
                inst.SetField("GUARDIAN_NAME", Convert.ToString(dt.Rows[0]["Guardian_Name"]));
                inst.SetField("VILL_CODE", Convert.ToString(dt.Rows[0]["Vill_Code"]));
                inst.SetField("DIS_CODE", Convert.ToString(dt.Rows[0]["Dist_Code"]));

            }
            else
            {

                message = "Data Not Found";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

                var inst = dtApplicantDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                inst.SetField("CUST_ID", "");
                inst.SetField("Name", "");
                inst.SetField("GUARDIAN_NAME", "");
                inst.SetField("VILL_CODE", "");
                inst.SetField("DIS_CODE", "");

            }
            dtApplicantDetails.AcceptChanges();
            gv_ClientDetails.DataSource = dtApplicantDetails;
            gv_ClientDetails.DataBind();
            ViewState["dtApplicantDetails"] = dtApplicantDetails;

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            DataTable dtNomineeDetailsTable = new DataTable();
            try
            {
                objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;
                objBO_Finance.SL_CODE = lblDid.Text;
                objBO_Finance.SL_CODE = btnsubmit.CommandArgument;
                objBO_Finance.SL_CODE = btnsubmit1.CommandArgument;
                objBO_Finance.actype = "sh";
                objBO_Finance.ac_status = "Live";
                objBO_Finance.NOMENCLATURE = cmbx_Ledger.SelectedValue;

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
                objBO_Finance.PAYDIVACNO = "0";
                DataTable dtClientMaster = new DataTable();
                dtClientMaster.Columns.Add("CUST_ID");
                foreach (DataRow dr in (ViewState["dtApplicantDetails"] as DataTable).Rows)
                {
                    dtClientMaster.Rows.Add(dr["CUST_ID"].ToString());
                }
                dtClientMaster.AcceptChanges();

                objBO_Finance.dtClientMaster = dtClientMaster;

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
                if (dtNomineeDetailsTable.Rows.Count > 0)
                {
                    dtNomineeDetailsTable.Columns.Remove("SlNo");
                }
                dtNomineeDetailsTable.AsEnumerable().Where(x => Convert.ToString(x["nomn_Name"]) == "").ToList().ForEach(dr => dr.Delete());
                objBO_Finance.dtNomineeDetailsTable = dtNomineeDetailsTable;
                objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                int i = objBL_Finance.InsertUpdateShareAccountOpening(objBO_Finance, out SQLError);
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

                        //ResetControls();
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

            }
            finally
            {

            }
        }
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            btnsubmit1.Text = "Save";
            dtpkr_dateopen.Text = String.Empty;
            txt_OldACNo.Text = String.Empty;
            cmbx_Ledger.SelectedIndex = -1;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void GetAccountOpeningEditData()//Share Account Edit
        {
            try
            {
                objBO_Finance.Flag = 11;
                objBO_Finance.SL_CODE = lblDid.Text;
                DataSet dSet = objBL_Finance.GetAccountsDetails(objBO_Finance, out SQLError);
                if (dSet.Tables[0].Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    btnsubmit1.Text = "Update";
                    btnsubmit.CommandArgument = lblDid.Text;
                    btnsubmit1.CommandArgument = lblDid.Text;
                    dtpkr_dateopen.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["date_of_opening"]).ToString("dd/MM/yyyy");
                    txt_OldACNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["old_acno"]);
                    ntxt_TotApplicant.Text = Convert.ToString(dSet.Tables[1].Rows.Count);

                    if (dSet.Tables[1].Rows.Count > 0)
                    {
                        cmbx_Ledger.SelectedValue = Convert.ToString(dSet.Tables[1].Rows[0]["NOMENCLATURE"]);
                    }

                    if (dSet.Tables[2].Rows.Count > 0)
                    {
                        ViewState["dtApplicantDetails"] = dSet.Tables[2];
                        gv_ClientDetails.DataSource = dSet.Tables[2];
                        gv_ClientDetails.DataBind();
                    }

                    if (dSet.Tables[3].Rows.Count > 0)
                    {
                        txtNoofNominee.Text = Convert.ToString(dSet.Tables[3].Rows.Count);
                        ViewState["dtNomineeDetailsTable"] = dSet.Tables[3];
                        GVNominee.DataSource = dSet.Tables[3];
                        GVNominee.DataBind();
                    }

                }
                else
                {
                    message = "alert('Data Not Found')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }
            }

            catch (Exception ex)
            {

            }
            finally
            {

            }
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
                objBO_Finance.Flag = 11;
                objBO_Finance.SL_CODE = txtsearchkyc.Text;
                DataSet dSet = objBL_Finance.GetAccountsDetails(objBO_Finance, out SQLError);
                if (dSet.Tables[0].Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    btnsubmit1.Text = "Update";
                    btnsubmit.CommandArgument = txtsearchkyc.Text;
                    btnsubmit1.CommandArgument = txtsearchkyc.Text;
                    dtpkr_dateopen.Text = Convert.ToDateTime(dSet.Tables[0].Rows[0]["date_of_opening"]).ToString("dd/MM/yyyy");
                    txt_OldACNo.Text = Convert.ToString(dSet.Tables[0].Rows[0]["old_acno"]);
                    ntxt_TotApplicant.Text = Convert.ToString(dSet.Tables[1].Rows.Count);

                    if (dSet.Tables[1].Rows.Count > 0)
                    {
                        cmbx_Ledger.SelectedValue = Convert.ToString(dSet.Tables[1].Rows[0]["NOMENCLATURE"]);
                    }

                    if (dSet.Tables[2].Rows.Count > 0)
                    {
                        ViewState["dtApplicantDetails"] = dSet.Tables[2];
                        gv_ClientDetails.DataSource = dSet.Tables[2];
                        gv_ClientDetails.DataBind();
                    }

                    if (dSet.Tables[3].Rows.Count > 0)
                    {
                        txtNoofNominee.Text = Convert.ToString(dSet.Tables[3].Rows.Count);
                        ViewState["dtNomineeDetailsTable"] = dSet.Tables[3];
                        GVNominee.DataSource = dSet.Tables[3];
                        GVNominee.DataBind();
                    }

                }
                else
                {
                    message = "alert('Data Not Found')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }
            }

            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
    }
}