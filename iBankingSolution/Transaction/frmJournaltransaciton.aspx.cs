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
    public partial class frmJournaltransaciton : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        decimal totalDrAmt = 0;
        decimal totalCrAmt = 0;
        decimal totalDr = 0;
        decimal totalCr = 0;
        string message = "";
        DataTable dtledger;
        MyDBDataContext dbContext = new MyDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            //NewGrid();
            //itbnNew.Click += new EventHandler(this.itbnNew);
            if (!IsPostBack)
            {

                SetInitialRowForJournal();
                SetInitialRowForJournal();
                BindLedger();
                dtpkr_EntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                addNewRow();

            }
        }
        protected void cmbx_LedgerSelectedIndexChanged(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (var item in rgv_JBEntryForm.Rows)
            {
                DropDownList ledger = rgv_JBEntryForm.Rows[cnt].FindControl("cmbx_Ledger") as DropDownList;
                DropDownList subledger = rgv_JBEntryForm.Rows[cnt].FindControl("cmbx_subledger") as DropDownList;
                TextBox txtsl = rgv_JBEntryForm.Rows[cnt].FindControl("txtslcode") as TextBox;
                TextBox txtold = rgv_JBEntryForm.Rows[cnt].FindControl("txt_OldAcctNo") as TextBox;

                objBO_Finance.Flag = 27;
                objBO_Finance.LDG_CODE = ledger.SelectedValue;
                DataTable dt1 = objBL_Finance.chequeslflag(objBO_Finance, out SQLError);
                if (dt1.Rows.Count > 0)
                {
                    if (Convert.ToString(dt1.Rows[0]["SL_FLAG"]) == "1")
                    {
                        txtsl.Enabled = true;
                        txtold.Enabled = true;
                        if (Convert.ToString(dt1.Rows[0]["TYPE"]) == "General")
                        {

                            subledger.Visible = true;
                            txtsl.Visible = false;

                            objBO_Finance.Flag = 28;
                            objBO_Finance.LDG_CODE = ledger.SelectedValue;
                            DataTable dtt1 = objBL_Finance.chequeslflag(objBO_Finance, out SQLError);
                            if (dtt1.Rows.Count > 0)
                            {
                                subledger.DataSource = dtt1;
                                subledger.DataValueField = "SL_CODE";
                                subledger.DataTextField = "SL_NAME";
                                subledger.DataBind();
                            }
                        }
                        else
                        {
                            subledger.Visible = false;
                            txtsl.Visible = true;
                        }
                    }
                    else
                    {

                    }
                }
                cnt = cnt + 1;
            }
        }

        protected void BindLedger()
        {
            try
            {
                DropDownList ledger = rgv_JBEntryForm.Rows[0].FindControl("cmbx_Ledger") as DropDownList;
                objBO_Finance.Flag = 6;

                objBO_Finance.CUST_ID = null;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {
                    ledger.DataSource = dtledger;
                    ledger.DataValueField = "LDG_CODE";
                    ledger.DataTextField = "NOMENCLATURE";
                    ledger.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private void MsgBox(string sMessage)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + sMessage + "');";
            msg += "</script>";
            Response.Write(msg);
        }

        private DataTable dtJBEntryDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SlNo"),
                    new DataColumn("OldAcctNo"),
                    new DataColumn("LDG_CODE"),
                    new DataColumn("sl_code"),
                    new DataColumn("Balance"),
                    new DataColumn("Narration"),
                    new DataColumn("DrAmount"),
                    new DataColumn("CrAmount")
                });
            for (int i = 0; i < 2; i++)
            {
                dt.Rows.Add(dt.Rows.Count + 1, "", DBNull.Value, DBNull.Value,0.00, "", 0.00, 0.00);
                dt.AcceptChanges();
            }
            return dt;
        }
        protected void ResetControls()
        {
            SetInitialRowForJournal();
            btnsubmit.Text = "Save";
            btnsubmit1.Text = "Save";
            dtpkr_EntryDate.Text = String.Empty;
            txt_Narration.Text = String.Empty;
            //rgv_JBEntryForm.DataSource = null;
            //rgv_JBEntryForm.DataBind();
        }
        protected void cmbx_SubLedgerSelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow item = ((DropDownList)sender).NamingContainer as GridViewRow;
            DropDownList subledger = rgv_JBEntryForm.Rows[0].FindControl("cmbx_subledger") as DropDownList;
            string SLCode = subledger.SelectedValue;
            TextBox OLDSLCode = item.FindControl("txt_OldAcctNo") as TextBox;

            if (SLCode != null)
            {
                var acno = (from sm in dbContext.SUBLEDGER_MASTERs
                            where sm.SL_CODE == Convert.ToDecimal(SLCode)
                            select new
                            {
                                AcNo = sm.OLD_ACNO

                            }).SingleOrDefault();

                if (acno != null)
                {
                    OLDSLCode.Text = Convert.ToString(acno.AcNo);
                }
                else
                {
                    message = "alert('SL CODE NOT FOUND')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    OLDSLCode.Attributes["onfocus"] = "javascript:this.select();";
                    //SLCode.Focus();
                }
            }


            string AcNum = subledger.SelectedValue;
            var ActOpBal = (from sm in dbContext.SUBLEDGER_MASTERs
                            where sm.SL_CODE == Convert.ToDecimal(AcNum)
                            select new
                            {
                                ACTOPCR = sm.ACT_OP_CR
                            }).SingleOrDefault();

            var ldgcode = (from sm in dbContext.SUBLEDGER_MASTERs
                           where sm.SL_CODE == Convert.ToDecimal(AcNum)
                           select new
                           {
                               LDGCODE = sm.LDG_CODE

                           }).SingleOrDefault();

            string AcNumb = subledger.SelectedValue;

            const string StatusInProgress = "In-Progress";
            const string StatusCompleted = "Success";
            var statuses = new[] { StatusInProgress, StatusCompleted };
            var query =
                (from t in dbContext.TRANS
                 where t.SL_CODE == Convert.ToDecimal(AcNumb)
                 group t by t.SL_CODE into g
                 select new
                 {
                     C = g.Sum(t => t.AMT_CREDIT)
                 }).SingleOrDefault();

            var AmtDebit =
                (from t in dbContext.TRANS
                 where t.SL_CODE == Convert.ToDecimal(AcNumb)
                 group t by t.SL_CODE into g
                 select new
                 {
                     D = g.Sum(t => t.AMT_DEBIT)
                 }).SingleOrDefault();


            TextBox txtBal = item.FindControl("txtBalance") as TextBox;
            string act = (Convert.ToString(Convert.ToDecimal(ActOpBal.ACTOPCR)));
            if (act == null)
            {
                act = "0";
            }
            else if (act == "0")
            {
                act = "0";
                txtBal.Text = Convert.ToString(Math.Abs((Convert.ToDecimal(act)) + ((Convert.ToDecimal(query.C)) - (Convert.ToDecimal(AmtDebit.D)))));
                //txtAvailableBalance.Text = Convert.ToString(Math.Abs((Convert.ToDecimal(act)) + ((Convert.ToDecimal(query.C)) - (Convert.ToDecimal(AmtDebit.D)))));
            }
            else if (act != "0")
            {
                txtBal.Text = Convert.ToString(Math.Abs((Convert.ToDecimal(ActOpBal.ACTOPCR)) + ((Convert.ToDecimal(query.C)) - (Convert.ToDecimal(AmtDebit.D)))));
                //txtAvailableBalance.Text = Convert.ToString(Math.Abs((Convert.ToDecimal(ActOpBal.ACTOPCR)) + ((Convert.ToDecimal(query.C)) - (Convert.ToDecimal(AmtDebit.D)))));
            }
            else
            {
                txtBal.Text = "0.00";
                //txtAvailableBalance.Text = "0.00";
            }


            //Session["tBalance"] = txtBal.Text;


            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = subledger.SelectedValue;
            DataTable dt = objBL_Finance.GetJournalDetabySL_CODE(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                txtName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
                txtGurdianName.Text = Convert.ToString(dt.Rows[0]["GUARDIAN_NAME"]);
            }
        }

        protected void ChequeBranch()
        {
            //string txt = TextBox txtslcode.Text;
            //DataSet data = objBL_Finance.MatchBranchCode(2, , Convert.ToString(Session["BranchID"]), out SQLError);
            //if (data.Tables[0].Rows.Count > 0)
            //{
            //    lblSession.Text = Convert.ToString(data.Tables[0].Rows[0]["SOCIETY_BR_CODE"]);
            //}

            //if (Convert.ToString(Session["BranchID"]) == lblSession.Text)
            //{
                
            //}
            //else
            //{
            //    MessageBox(this, "Account Not Belongs To This Branch !");
            //}
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                GenDatatable();
                ////GenDatatable();

                decimal txtDr = Convert.ToDecimal(Session["DrAmt"]);
                decimal txtCr = Convert.ToDecimal(Session["CrAmt"]);
                decimal txtBalance = Convert.ToDecimal(Session["tBalance"]);

                //if (txtBalance > txtDr)
                {
                    if (txtDr == txtCr)
                    {
                        objBO_Finance.Flag = btnsubmit.Text == "Save" ? 1 : 2;
                        objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;


                        string Cust = dtpkr_EntryDate.Text;
                        DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objBO_Finance.JBDate = Cust1;

                        objBO_Finance.NARRATION1 = Convert.ToString(txt_Narration.Text);
                        objBO_Finance.EMPCODE = Convert.ToString(Session["EmpID"]); ;
                        objBO_Finance.TERMINAL_ID = "0";
                        objBO_Finance.ENTRY_FR = 3;

                        DataTable JBEntryDetail = (DataTable)ViewState["JBEntryDetails"];

                        foreach (GridViewRow item in rgv_JBEntryForm.Rows)
                        {
                            if (Convert.ToDecimal(((TextBox)item.FindControl("ntxt_CrAmount")).Text) > 0 || Convert.ToDecimal(((TextBox)item.FindControl("ntxt_DrAmount")).Text) > 0)
                            {
                                try
                                {

                                    var inst = JBEntryDetail.AsEnumerable().Where(x => Convert.ToInt32(x.Field<Int32>("SlNo")) == Convert.ToInt32(((Label)item.FindControl("lbl_SlNo")).Text)).First();


                                    //inst.SetField("OldAcctNo", ((TextBox)item.FindControl("txt_OldAcctNo")).Text);
                                    TextBox txtslcode = (TextBox)item.FindControl("txtslcode");
                                    DropDownList sledger = (DropDownList)item.FindControl("cmbx_subledger");
                                    if (txtslcode.Text == "0")
                                    {
                                        inst.SetField("sl_code", DBNull.Value);
                                        inst.SetField("LDG_CODE", ((DropDownList)item.FindControl("cmbx_Ledger")).SelectedValue);
                                        //Session["ChqLDG_CODE"] = ((DropDownList)item.FindControl("cmbx_Ledger")).SelectedValue;
                                    }
                                    else if (txtslcode.Text == "")
                                    {
                                        inst.SetField("sl_code", ((DropDownList)item.FindControl("cmbx_subledger")).SelectedValue);
                                        inst.SetField("LDG_CODE", DBNull.Value);
                                    }
                                    else
                                    {
                                        inst.SetField("sl_code", ((TextBox)item.FindControl("txtslcode")).Text);
                                        inst.SetField("LDG_CODE", DBNull.Value);
                                    }
                                    inst.SetField("DrAmount", ((TextBox)item.FindControl("ntxt_DrAmount")).Text);
                                    inst.SetField("CrAmount", ((TextBox)item.FindControl("ntxt_CrAmount")).Text);
                                    inst.SetField("Narration", ((TextBox)item.FindControl("txt_Narration")).Text);
                                }
                                catch (Exception ex)
                                {
                                    string msg = ex.Message;
                                }
                            }
                        }
                        if (JBEntryDetail.Columns.Contains("SlNo"))
                        {
                            JBEntryDetail.Columns.Remove("SlNo");
                        }
                        if (JBEntryDetail.Columns.Contains("OldAcctNo"))
                        {
                            JBEntryDetail.Columns.Remove("OldAcctNo");
                        }
                        objBO_Finance.JBEntryDetails = JBEntryDetail;

                        totalDrAmt = Convert.ToDecimal(Session["DrAmt"]);
                        totalCrAmt = Convert.ToDecimal(Session["CrAmt"]);
                    }
                


                    if ((totalDrAmt > 0 && totalCrAmt > 0) && (totalDrAmt == totalCrAmt))
                    {
                        objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
                        int i = objBL_Finance.InsertUpdateDeleteJournalBook(objBO_Finance, out SQLError);


                        if (i > 0)
                        {
                            if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                            {
                                ResetControls();
                                //message = "alert('Save Successfully.')";
                                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                                MsgBox("Record Save Successfully . Allotted Voucher is:- " + SQLError);
                                //MessageBox(this, "Record Inserted Successfully . Allotted Voucher No is:-" + SQLError);
                                //ShowMessage("Record Inserted Successfully. Allotted Voucher No is:-" + SQLError, MessageType.Success);
                                btnsubmit.Text = "Save";
                                btnsubmit1.Text = "Save";
                                Label1.Visible = false;
                                DivID.Visible = true;
                                Label1.Text = "Alloted Voucher is:" + SQLError;

                                gv_accholder.DataSource = null;
                                gv_accholder.DataBind();
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

                            //message = "alert('Something Wrong Input.')";
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        }



                    }
                    else
                    {
                        message = "alert('Total value must be greater than zero')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    }


                }
            }
            catch (Exception ex)
            {

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
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void cmbx_Ledger_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //GridViewRow item = ((DropDownList)sender).NamingContainer as GridViewRow;



            ////GridView dr = ((DropDownList)sender).NamingContainer as GridView;
            //DropDownList ledger = item.FindControl("cmbx_Ledger") as DropDownList;

            //DropDownList subledger = ((DropDownList)item.FindControl("cmbx_SubLedger"));
            //TextBox txtSLCode = (TextBox)item.FindControl("txtslcode");
            //DropDownList subledger = rgv_JBEntryForm.Rows.FindControl("cmbx_SubLedger") as DropDownList;

            //sds_SubLedger.SelectParameters["LDG_CODE"].DefaultValue = item;
            //objBO_Finance.Flag = 7;
            //objBO_Finance.LDG_CODE = ledger.SelectedValue;

            //dt = objBL_Finance.GetSubLedgerMasterRecords(objBO_Finance, out SQLError);

            //subledger.DataSource = dt;

            //subledger.DataTextField = "sl_name";
            //subledger.DataValueField = "sl_code";
            //subledger.DataBind();



        }
        private void SetInitialRowForJournal()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            //Define the Columns

            dt.Columns.Add(new DataColumn("Slno", typeof(Int32)));
            dt.Columns.Add(new DataColumn("LDG_CODE", typeof(string)));
            dt.Columns.Add(new DataColumn("OldAcctNo", typeof(string)));
            dt.Columns.Add(new DataColumn("sl_code", typeof(string)));
            dt.Columns.Add(new DataColumn("Balance", typeof(string)));
            dt.Columns.Add(new DataColumn("DrAmount", typeof(Int32)));
            dt.Columns.Add(new DataColumn("CrAmount", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Narration", typeof(string)));

            dr = dt.NewRow();
            dr["Slno"] = "1";
            dr["LDG_CODE"] = "";
            dr["OldAcctNo"] = String.Empty;
            dr["sl_code"] = String.Empty;
            dr["Balance"] = String.Empty;
            dr["DrAmount"] = 0;
            dr["CrAmount"] = 0;
            dr["Narration"] = String.Empty;

            dt.Rows.Add(dr);

    
            rgv_JBEntryForm.DataSource = dt;
            rgv_JBEntryForm.DataBind();


        }


        protected void GenDatatable()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            //Define the Columns
            Int32 rowcnt = 0;
            dt.Columns.Add(new DataColumn("SlNo", typeof(Int32)));
            dt.Columns.Add(new DataColumn("OldAcctNo", typeof(string)));
            dt.Columns.Add(new DataColumn("LDG_CODE", typeof(string)));
            dt.Columns.Add(new DataColumn("sl_code", typeof(string)));
            //dt.Columns.Add(new DataColumn("Balance", typeof(string)));

            dt.Columns.Add(new DataColumn("Narration", typeof(string)));
            dt.Columns.Add(new DataColumn("DrAmount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CrAmount", typeof(decimal)));

            foreach (GridViewRow row in rgv_JBEntryForm.Rows)
            {

                Label lbl_SlNo = row.FindControl("lbl_SlNo") as Label;
                TextBox txtOldAcctNo = row.FindControl("txt_OldAcctNo") as TextBox;
                DropDownList Ledger = row.FindControl("cmbx_Ledger") as DropDownList;

                //DropDownList subledger = row.FindControl("cmbx_SubLedger") as DropDownList;
                TextBox slcode = row.FindControl("txtslcode") as TextBox;
                //TextBox Balance = row.FindControl("txtBalance") as TextBox;
                TextBox naration = row.FindControl("txt_Narration") as TextBox;
                TextBox ntxtDrAmount = row.FindControl("ntxt_DrAmount") as TextBox;
                TextBox ntxtCrAmount = row.FindControl("ntxt_CrAmount") as TextBox;


                dr = dt.NewRow();

                rowcnt = Convert.ToInt32(dt.Rows.Count) + 1;
                dr[0] = lbl_SlNo.Text != "" ? lbl_SlNo.Text : "0";
                dr[1] = txtOldAcctNo.Text != "" ? txtOldAcctNo.Text : "0";
                dr[2] = Ledger.SelectedValue != " 0" ? Ledger.SelectedValue : "0";

                dr[3] = slcode.Text != "" ? slcode.Text : "0";
                //dr[4] = Balance.Text != "" ? Balance.Text : "0";
                dr[4] = naration.Text != "" ? naration.Text : "";
                dr[5] = ntxtDrAmount.Text != "" ? ntxtDrAmount.Text : "0";
                dr[6] = ntxtCrAmount.Text != "" ? ntxtCrAmount.Text : "0";

                dt.Rows.Add(dr);
                ViewState["JBEntryDetails"] = dt;
            }
        }
        public void addNewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            //Define the Columns
            Int32 rowcnt = 0;
            dt.Columns.Add(new DataColumn("SlNo", typeof(Int32)));
            dt.Columns.Add(new DataColumn("OldAcctNo", typeof(string)));
            dt.Columns.Add(new DataColumn("LDG_CODE", typeof(string)));
            dt.Columns.Add(new DataColumn("sl_code", typeof(string)));
            dt.Columns.Add(new DataColumn("subledger", typeof(string)));
            dt.Columns.Add(new DataColumn("Narration", typeof(string)));
            dt.Columns.Add(new DataColumn("DrAmount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CrAmount", typeof(decimal)));

            dt.Columns.Add(new DataColumn("Balance", typeof(decimal)));

            foreach (GridViewRow row in rgv_JBEntryForm.Rows)
            {

                Label lbl_SlNo = row.FindControl("lbl_SlNo") as Label;
                TextBox txtOldAcctNo = row.FindControl("txt_OldAcctNo") as TextBox;
                DropDownList Ledger = row.FindControl("cmbx_Ledger") as DropDownList;

                TextBox slcode = row.FindControl("txtslcode") as TextBox;
                DropDownList subledger = row.FindControl("cmbx_subledger") as DropDownList;
                TextBox naration = row.FindControl("txt_Narration") as TextBox;
                TextBox ntxtDrAmount = row.FindControl("ntxt_DrAmount") as TextBox;
                TextBox ntxtCrAmount = row.FindControl("ntxt_CrAmount") as TextBox;

                TextBox txtBalance = row.FindControl("txtBalance") as TextBox;


                dr = dt.NewRow();

                rowcnt = Convert.ToInt32(dt.Rows.Count) + 1;
                dr[0] = lbl_SlNo.Text != "" ? lbl_SlNo.Text : "0";
                dr[1] = txtOldAcctNo.Text != "" ? txtOldAcctNo.Text : "0";
                dr[2] = Ledger.SelectedValue != " 0" ? Ledger.SelectedValue : "0";

                dr[3] = slcode.Text != "" ? slcode.Text : "0";
                dr[4] = subledger.SelectedValue != "" ? subledger.SelectedValue : "0";
                dr[5] = naration.Text != "" ? naration.Text : "";
                dr[6] = ntxtDrAmount.Text != "" ? ntxtDrAmount.Text : "0";
                dr[7] = ntxtCrAmount.Text != "" ? ntxtCrAmount.Text : "0";

                dr[8] = txtBalance.Text != "" ? txtBalance.Text : "0";

                dt.Rows.Add(dr);
            }
            dt.Rows.Add(rowcnt + 1, 0, 0, 0, 0, "", 0, 0, 0);

            rgv_JBEntryForm.DataSource = dt;
            rgv_JBEntryForm.DataBind();

            ViewState["JBEntryDetails"] = dt;
            int rowIndex = 0;
            DataTable dtLedgers = (DataTable)ViewState["ledger"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Setting the previous value of Ledger code 
                DropDownList ddlLedger = (DropDownList)rgv_JBEntryForm.Rows[rowIndex].Cells[1].FindControl("cmbx_Ledger");

                ddlLedger.DataSource = dtLedgers;
                ddlLedger.DataTextField = "NOMENCLATURE";
                ddlLedger.DataValueField = "LDG_CODE";
                ddlLedger.DataBind();
                //ddlLedger.Items.Insert(0, new ListItem("-- Select Ledger --", "0"));
                ddlLedger.ClearSelection();
                ddlLedger.Items.FindByValue(dt.Rows[i]["LDG_CODE"].ToString()).Selected = true;

                //Setting the previous value of sub ledger code

                //DropDownList ddlsubLedger = (DropDownList)rgv_JBEntryForm.Rows[rowIndex].Cells[1].FindControl("cmbx_SubLedger");

                objBO_Finance.Flag = 7;
                objBO_Finance.LDG_CODE = dt.Rows[i]["LDG_CODE"];

                //DataTable dtSubLedger = objBL_Finance.GetSubLedgerMasterRecords(objBO_Finance, out SQLError);


                //ddlsubLedger.DataSource = dtSubLedger;
                //ddlsubLedger.DataTextField = "sl_name";
                //ddlsubLedger.DataValueField = "sl_code";
                //ddlsubLedger.DataBind();
                //ddlsubLedger.Items.Insert(0, new ListItem("- Select -", "0"));
                //ddlsubLedger.ClearSelection();
                //ddlsubLedger.Items.FindByValue(dt.Rows[i]["sl_code"].ToString()).Selected = true;


                rowIndex = rowIndex + 1;
            }

        }
        protected void itNew_Click(object sender, ImageClickEventArgs e)
        {

            addNewRow();

        }
        decimal totalCr1 = 0;
        decimal totalDr1 = 0;
        protected void rgv_JBEntryForm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtDrAmount = (TextBox)e.Row.FindControl("ntxt_DrAmount");
                decimal drAmt = Convert.ToDecimal (txtDrAmount.Text);
                totalDr1 = totalDr1 + drAmt;

                TextBox txtCrAmount = (TextBox)e.Row.FindControl("ntxt_CrAmount");
                decimal crAmt = Convert.ToDecimal(txtCrAmount.Text);
                totalCr1 = totalCr1 + crAmt;



            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox lblTotalDebit = (TextBox)e.Row.FindControl("ntxt_TotalDrAmount");
                HiddenField HdnDrAmt = (HiddenField)e.Row.FindControl("hiddenDrAmt");
                HiddenField HdnCrAmt = (HiddenField)e.Row.FindControl("hiddenCrAmt");
                lblTotalDebit.Text = totalDr1.ToString();
                totalDrAmt = Convert.ToDecimal(lblTotalDebit.Text);
                //HdnDrAmt.Value = lblTotalDebit.Text;
                Session["DrAmt"] = lblTotalDebit.Text;
                TextBox lblTotalCredit = (TextBox)e.Row.FindControl("ntxt_TotalCrAmount");
                lblTotalCredit.Text = totalCr1.ToString();
                totalCrAmt = Convert.ToDecimal(lblTotalCredit.Text);
                Session["CrAmt"] = lblTotalCredit.Text;

            }
        }

        protected void ntxt_CrAmount_TextChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in rgv_JBEntryForm.Rows)
            {
                TextBox ntxtCrAmount = row.FindControl("ntxt_CrAmount") as TextBox;
                TextBox lblTotalCredit = rgv_JBEntryForm.FooterRow.FindControl("ntxt_TotalCrAmount") as TextBox;
                //TextBox lblTotalCredit = row.FindControl("ntxt_TotalCrAmount") as TextBox;
                decimal crAmt = Convert.ToDecimal(ntxtCrAmount.Text);
                totalCr = totalCr + crAmt;
                lblTotalCredit.Text = totalCr.ToString();
                Session["CrAmt"] = lblTotalCredit.Text;


            }

            //totalCrAmt = Convert.ToInt64(Session["CrAmt"]);
        }

        //protected void cmbx_SubLedger_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //objBO_Finance.SL_CODE = gv_accholder.Rows[0].FindControl("cmbx_SubLedger") as DropDownList;// (().Cells[6].FindControl("label1")).Text; ;
        //GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
        //Dim chkChosen As CheckBox = CType(GridView1.SelectedRow.FindControl("Checkbox1"), CheckBox)
        //DropDownList cmbx_sl_code = (DropDownList)gv_accholder.SelectedRow.FindControl("cmbx_SubLedger") as DropDownList;
        //DropDownList id = gv_accholder.Rows[e].FindControl("VoucherCode") as Label;
        //Convert.ToString(dt.Tables[0].Rows[0]["NAME"]);
        //DropDownList id = ((DropDownList)gv_accholder.Rows[0].Cells[3].FindControl("cmbx_SubLedger")) as DropDownList;

        //    DropDownList ddl = (DropDownList)sender;

        //    Label1.Text = ddl.SelectedItem.Value;

        //    GridViewRow row = (GridViewRow)ddl.NamingContainer;

        //    // Find your control
        //     DropDownList control = (DropDownList)row.FindControl("cmbx_SubLedger");

        //    Int32 id = Convert.ToInt32(control.SelectedValue);


        //    DataSet dt = new DataSet();

        //    objBO_Finance.SL_CODE = id;


        //    dt = objBL_Finance.journalaccholder(objBO_Finance);
        //    gv_accholder.DataSource = dt;
        //    gv_accholder.DataBind();
        //}

        protected void rgv_JBEntryForm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtslcode_TextChanged(object sender, EventArgs e)
        {
            GridViewRow item = ((TextBox)sender).NamingContainer as GridViewRow;
            TextBox SLCode = item.FindControl("txtslcode") as TextBox;
            TextBox OLDSLCode = item.FindControl("txt_OldAcctNo") as TextBox;
            
            if (SLCode != null)
            {
                var acno = (from sm in dbContext.SUBLEDGER_MASTERs
                            where sm.SL_CODE == Convert.ToDecimal(SLCode.Text)
                            select new
                            {
                                AcNo = sm.OLD_ACNO

                            }).SingleOrDefault();

                if (acno != null)
                {
                    OLDSLCode.Text = Convert.ToString(acno.AcNo);
                }
                else
                {
                    message = "alert('SL CODE NOT FOUND')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    OLDSLCode.Attributes["onfocus"] = "javascript:this.select();";
                    SLCode.Focus();
                }
            }


            TextBox AcNum = item.FindControl("txtslcode") as TextBox;
            var ActOpBal = (from sm in dbContext.SUBLEDGER_MASTERs
                            where sm.SL_CODE == Convert.ToDecimal(AcNum.Text)
                            select new
                            {
                                ACTOPCR = sm.ACT_OP_CR
                            }).SingleOrDefault();

            var ldgcode = (from sm in dbContext.SUBLEDGER_MASTERs
                           where sm.SL_CODE == Convert.ToDecimal(AcNum.Text)
                           select new
                           {
                               LDGCODE = sm.LDG_CODE

                           }).SingleOrDefault();

            TextBox AcNumb = item.FindControl("txtslcode") as TextBox;
       
            const string StatusInProgress = "In-Progress";
            const string StatusCompleted = "Success";
            var statuses = new[] { StatusInProgress, StatusCompleted };
            var query =
                (from t in dbContext.TRANS
                 where t.SL_CODE == Convert.ToDecimal(AcNumb.Text)
                 group t by t.SL_CODE into g
                 select new
                 {
                     C = g.Sum(t => t.AMT_CREDIT)
                 }).SingleOrDefault();

            var AmtDebit =
                (from t in dbContext.TRANS
                 where t.SL_CODE == Convert.ToDecimal(AcNumb.Text)
                 group t by t.SL_CODE into g
                 select new
                 {
                     D = g.Sum(t => t.AMT_DEBIT)
                 }).SingleOrDefault();

            TextBox txtBal = item.FindControl("txtBalance") as TextBox;
            string act = (Convert.ToString(Convert.ToDecimal(ActOpBal.ACTOPCR)));
            if (act == null)
            {
                act = "0";
                txtBal.Text = Convert.ToString(Math.Abs((Convert.ToDecimal(act)) + ((Convert.ToDecimal(query.C)) - (Convert.ToDecimal(AmtDebit.D)))));
            }
            else if (act == "0")
            {
                act = "0";
                txtBal.Text = Convert.ToString(Math.Abs((Convert.ToDecimal(act)) + ((Convert.ToDecimal(query.C)) - (Convert.ToDecimal(AmtDebit.D)))));
            }
            else if (act != "0")
            {
                txtBal.Text = Convert.ToString(Math.Abs((Convert.ToDecimal(ActOpBal.ACTOPCR)) + ((Convert.ToDecimal(query.C)) - (Convert.ToDecimal(AmtDebit.D)))));
            }
            else
            {
                txtBal.Text = "0.00";
            }


            Session["tBalance"] = txtBal.Text;


            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = ((TextBox)sender).Text;
            DataTable dt = objBL_Finance.GetJournalDetabySL_CODE(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                txtName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
                txtGurdianName.Text = Convert.ToString(dt.Rows[0]["GUARDIAN_NAME"]);
            }

            //if (((DropDownList)item.FindControl("cmbx_Ledger")).SelectedValue == Convert.ToString(ldgcode.LDGCODE))
            //{
                
            //}
            //else
            //{
            //    MessageBox(this, "Invalid Account");
            //}

            

           



            //GridViewRow item = ((TextBox)sender).NamingContainer as GridViewRow;

            //TextBox slcode = item.FindControl("txtslcode") as TextBox;

            //DataSet dt = new DataSet();

            //objBO_Finance.SL_CODE = Convert.ToDouble(slcode.Text);


            //dt = objBL_Finance.journalaccholder(objBO_Finance);
            //gv_accholder.DataSource = dt;
            //gv_accholder.DataBind();

        }

        protected void ntxt_DrAmount_TextChanged(object sender, EventArgs e)
        {
            totalDr = 0;
            foreach (GridViewRow row in rgv_JBEntryForm.Rows)
            {
                TextBox ntxtDrAmount = row.FindControl("ntxt_DrAmount") as TextBox;
                TextBox lblTotalDedit = rgv_JBEntryForm.FooterRow.FindControl("ntxt_TotalDrAmount") as TextBox;
                decimal DrAmt = Convert.ToDecimal(ntxtDrAmount.Text);
                totalDr = totalDr + DrAmt;
                lblTotalDedit.Text = totalDr.ToString();

                Session["DrAmt"] = lblTotalDedit.Text;

                foreach (GridViewRow rows in gv_accholder.Rows)
                {
                    GridViewRow item = ((TextBox)sender).NamingContainer as GridViewRow;
                    Label mainbalanc = rows.FindControl("lblbalance") as Label;
                    if (Convert.ToDouble(ntxtDrAmount.Text) > Convert.ToDouble(mainbalanc.Text))
                    {
                        message = "alert('Please Enter less amount than Balance')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                    }
                }
            }
        }

        protected void txt_OldAcctNo_TextChanged(object sender, EventArgs e)
        {
            GridViewRow item = ((TextBox)sender).NamingContainer as GridViewRow;

            TextBox OLDSLCode = item.FindControl("txt_OldAcctNo") as TextBox;
            TextBox SLCode = item.FindControl("txtslcode") as TextBox;
            var acno = (from sm in dbContext.SUBLEDGER_MASTERs
                        where sm.OLD_ACNO == Convert.ToString(OLDSLCode.Text)
                        select new
                        {
                            AcNo = sm.SL_CODE

                        }).SingleOrDefault();

            if (acno != null)
            {
                SLCode.Text = Convert.ToString(acno.AcNo);
            }
            else
            {
                message = "alert('SL CODE NOT FOUND')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                SLCode.Attributes["onfocus"] = "javascript:this.select();";
                OLDSLCode.Focus();
            }
            TextBox AcNum = item.FindControl("txtslcode") as TextBox;
            var ActOpBal = (from sm in dbContext.SUBLEDGER_MASTERs
                        where sm.SL_CODE == Convert.ToDecimal(AcNum.Text)
                            select new
                            {
                                ACTOPCR = sm.ACT_OP_CR
                            }).SingleOrDefault();

            

            var ldgcode = (from sm in dbContext.SUBLEDGER_MASTERs
                           where sm.SL_CODE == Convert.ToDecimal(AcNum.Text)
                           select new
                           {
                               LDGCODE = sm.LDG_CODE

                           }).SingleOrDefault();
            //var AmtCredit = (from t in dbContext.TRANS
            //                 where t.SL_CODE == Convert.ToDecimal(AcNumb.Text)
            //                 select new
            //                 {
            //                     AmC = t.AMT_CREDIT
            //                 }).SingleOrDefault();


            TextBox AcNumb = item.FindControl("txtslcode") as TextBox;
            // constants to avoid typos
            const string StatusInProgress = "In-Progress";
            const string StatusCompleted = "Success";
            // needed for IN clause 
            var statuses = new[] { StatusInProgress, StatusCompleted };
            // the query
            var query =
                (from t in dbContext.TRANS
                where t.SL_CODE == Convert.ToDecimal(AcNumb.Text)
                group t by t.SL_CODE into g
                select new
                {
                    //InProgress = g.Sum(t => t.Status == StatusInProgress ? 1 : 0),
                    //Completed = g.Sum(e => e.Status == StatusCompleted ? 1 : 0),
                    C = g.Sum(t => t.AMT_CREDIT)
                }).SingleOrDefault();


            //const string StatusInProgress = "In-Progress";
            //const string StatusCompleted = "Success";
            //var statuses = new[] { StatusInProgress, StatusCompleted };
            var AmtDebit =
                (from t in dbContext.TRANS
                 where t.SL_CODE == Convert.ToDecimal(AcNumb.Text)
                 group t by t.SL_CODE into g
                 select new
                 {
                     D = g.Sum(t => t.AMT_DEBIT)
                 }).SingleOrDefault();

            if (((DropDownList)item.FindControl("cmbx_Ledger")).SelectedValue == Convert.ToString(ldgcode.LDGCODE))
            {
                TextBox txtBal = item.FindControl("txtBalance") as TextBox;
                txtBal.Text = Convert.ToString((Convert.ToDecimal(ActOpBal.ACTOPCR)) + ((Convert.ToDecimal(query.C)) - (Convert.ToDecimal(AmtDebit.D))));
                Session["tBalance"] = txtBal.Text;

                objBO_Finance.Flag = 1;
                objBO_Finance.OLDAC_NO = ((TextBox)sender).Text;
                DataTable dt = objBL_Finance.GetJournalDeta(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
                    txtGurdianName.Text = Convert.ToString(dt.Rows[0]["GUARDIAN_NAME"]);
                }

            }
            else
            {
                MessageBox(this, "Invalid Account");
            }

            

            

            //txtBal.Text = Convert.ToString((Convert.ToDecimal(query.C)));

            























            //TextBox AcNumbe = item.FindControl("txtslcode") as TextBox;
            //var AmtDebit = (from t in dbContext.TRANS
            //                 where t.SL_CODE == Convert.ToDecimal(AcNumbe.Text)
            //                 select new
            //                 {
            //                     AmD = t.AMT_DEBIT
            //                 }).SingleOrDefault();
            //TextBox txtBal = item.FindControl("txtBalance") as TextBox;
            ////txtBal.Text = Convert.ToString((Convert.ToDecimal(ActOpBal.ACTOPCR)) + ((Convert.ToDecimal(AmtCredit.AmC)) - (Convert.ToDecimal(AmtDebit.AmD))));







            //objBO_Finance.Flag = 1;
            //objBO_Finance.OLDAC_NO = ((TextBox)sender).Text
            //GridViewRow item = ((TextBox)sender).NamingContainer as GridViewRow;
            //DataTable dt = objBL_Finance.GetJournalDeta(objBO_Finance, out SQLError); 
            //DataTable dtJBEntryDetails = ViewState["dtJBEntryDetails"] as DataTable;
            //////////////////////////////////////////////////////////////////////////////////
            //if (dt.Rows.Count > 0)
            //{
            //    //TextBox txtslcode = (TextBox)(dt.Rows.Cells[0].FindControl("TextBox1"));
            //    //TextBox txt_OldAcctNo = (TextBox)(dt.Rows[0]["OldAcctNo"]);

            //    //TextBox txtslcode = (TextBox)(dt.Rows[0]["sl_code"]);
            //    TextBox txtslcode = (TextBox)(dt.Rows[0].FindControl("sl_code"]));

            //var inst = JBEntryDetail.AsEnumerable().Where(x => Convert.ToInt32(x.Field<Int32>("SlNo")) == Convert.ToInt32(((Label)item.FindControl("lbl_SlNo")).Text)).First();
            //    //var inst = dtJBEntryDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
            //    //inst.SetField("OldAcctNo", Convert.ToString(dt.Rows[0]["OldAcctNo"]));
            //    //inst.SetField("sl_code", Convert.ToString(dt.Rows[0]["sl_code"]));
            //    //inst.SetField("Balance", Convert.ToString(dt.Rows[0]["Balance"]));
            //}
            //else
            //{

            //    message = "Data Not Found";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            //

            //    var inst = dtJBEntryDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
            //    inst.SetField("OldAcctNo", "");
            //    inst.SetField("sl_code", "");
            //    //inst.SetField("Balance", "");
            //}
            //////////////////////////////////////////////////////////////////
            //foreach (GridViewRow row in rgv_JBEntryForm.Rows)
            //{


            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        int rowIndex = 0;
            //        DataTable dtLedgers = (DataTable)ViewState["ledger"];
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            //Setting the previous value of Ledger code 
            //            DropDownList ddlLedger = (DropDownList)rgv_JBEntryForm.Rows[rowIndex].Cells[1].FindControl("cmbx_Ledger");

            //            ddlLedger.DataSource = dtLedgers;
            //            ddlLedger.DataTextField = "NOMENCLATURE";
            //            ddlLedger.DataValueField = "LDG_CODE";
            //            ddlLedger.DataBind();
            //            ddlLedger.Items.Insert(0, new ListItem("- Select -", "0"));
            //            ddlLedger.ClearSelection();
            //            ddlLedger.Items.FindByValue(dt.Rows[i]["LDG_CODE"].ToString()).Selected = true;
            //        }

            //        TextBox txtslcode = row.FindControl("sl_code") as TextBox;

            //        TextBox txtBalance = row.FindControl("Balance") as TextBox;
            //    }
            //}
            //rgv_JBEntryForm.DataSource = dt;
            //rgv_JBEntryForm.DataBind();

        }
    }
}