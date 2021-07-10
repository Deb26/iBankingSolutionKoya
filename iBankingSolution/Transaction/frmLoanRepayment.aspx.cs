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
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;


namespace iBankingSolution.Transaction
{
    public partial class frmLoanRepayment : System.Web.UI.Page
    {
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;

        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;

        public Double DemandPrincipalOverdue = 0;
        public Double DemandPrincipalCurrent = 0;
        public Double DemandOverdueInterest = 0;
        public Double DemandCurrentInterest = 0;
        public Decimal DemandPrincipalOutstanding = 0;
        public Double DemandDueInterest = 0;
        public String ODPrin = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtpkr_CollectionDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                
                GetLoanAccountNo();
                GetCashBook();

                if (HttpContext.Current.Request.QueryString["Id"] != null)
                {
                    // lblDid.Text = HttpContext.Current.Request.QueryString["Id"];
                    //GetLoanRepaymentEditData();


                }

            }
        }
        protected void GetCashBook()
        {
            objBO_Finance.Flag = 1;
            DataTable dt = objBL_Finance.GetCashBookLedger(objBO_Finance);

            if (dt.Rows.Count > 0)
            {
                txt_CashBook.Text = Convert.ToString(dt.Rows[0]["sl_name"]);
            }
            else
            {
                txt_CashBook.Text = String.Empty;
            }
        }
        protected void GetLoanAccountNo()
        {

            objBO_Finance.Flag = 1;

            DataTable dt = objBL_Finance.GetsubLedgerDetailsLoan(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_AccountNo.DataSource = dt;
                cmbx_AccountNo.DataTextField = "SL_CODE";
                cmbx_AccountNo.DataValueField = "SL_CODE";

                cmbx_AccountNo.DataBind();
                cmbx_AccountNo.Items.Insert(0, "-- Select a Value --");

            }

        }
        private void MsgBox(string sMessage)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + sMessage + "');";
            msg += "</script>";
            Response.Write(msg);
        }

        protected void DevideTotCollection()
        {
            Decimal TotAmtCollection = Convert.ToDecimal(ntxt_TotalCollection.Text);
            Decimal DemandDueInt = Convert.ToDecimal(ntxt_DemandDueInterest.Text);
            Decimal DemandODInt = Convert.ToDecimal(ntxt_DemandOverdueInterest.Text);
            Decimal DemandCurrInt = Convert.ToDecimal(ntxt_DemandCurrentInterest.Text);
            Decimal DemandCurrPrincipal = Convert.ToDecimal(ntxt_DemandPrincipalCurrent.Text);

            Decimal C_DueInt = TotAmtCollection >= DemandDueInt ? DemandDueInt : TotAmtCollection;
            TotAmtCollection = TotAmtCollection - C_DueInt;
            Decimal C_ODInt = TotAmtCollection >= DemandODInt ? DemandODInt : TotAmtCollection;
            TotAmtCollection = TotAmtCollection - C_ODInt;
            Decimal C_CurrInt = TotAmtCollection >= DemandCurrInt ? DemandCurrInt : TotAmtCollection;
            TotAmtCollection = TotAmtCollection - C_CurrInt;

            //ntxt_CollectionPrincipalCurrent.Text = Convert.ToString(TotAmtCollection);
            ntxt_CollectionCurrentInterest.Text = Convert.ToString(C_CurrInt);
            ntxt_CollectionOverdueInterest.Text = Convert.ToString(C_ODInt);
            ntxt_CollectionDueInterest.Text = Convert.ToString(DemandCurrInt + DemandDueInt + DemandODInt - C_CurrInt - C_DueInt - C_ODInt);
            //ntxt_CollectionPrincipalOutstanding.Text = Convert.ToString(DemandCurrPrincipal - TotAmtCollection);


        }
        protected void ResetControls()
        {
            btnsubmit.Text = "Save";
            btnsubmit1.Text = "Save";
            cmbx_AccountNo.SelectedIndex = -1;
            //dtpkr_CollectionDate.Text = String.Empty;
            ///rdobtn_ReceivedType.SelectedIndex = -1;
            //cmbx_BankLedger.SelectedIndex = -1;
            //ntxt_CollectionPrincipalOutstanding.Text = String.Empty;
            //ntxt_CollectionPrincipalOverdue.Text = String.Empty;
            //ntxt_CollectionPrincipalCurrent.Text = String.Empty;
            ntxt_CollectionDueInterest.Text = String.Empty;
            ntxt_CollectionOverdueInterest.Text = String.Empty;
            ntxt_CollectionCurrentInterest.Text = String.Empty;
            GVMemberDetail.DataSource = null;
            GVMemberDetail.DataBind();
            GVRepayHist.DataSource = null;
            GVRepayHist.DataBind();
            txt_lrdate.Text = String.Empty;
            txt_roii.Text = String.Empty;
            txt_daysdemand.Text = String.Empty;
            txt_odroi.Text = String.Empty;
            lblsocietyname.Text = String.Empty;
            lblReceipt.Text = String.Empty;
            lblHead.Text = String.Empty;
            lblsysAc.Text = String.Empty;
            lblName.Text = String.Empty;
            lblRepayDt.Text = String.Empty;
            lblAmtPaid.Text = String.Empty;
            lblCurPrin.Text = String.Empty;
            lblODPrin.Text = String.Empty;
            lblcurInt.Text = String.Empty;
            lblODInt.Text = String.Empty;
            ntxt_DemandCurrInt.Text = String.Empty;
            ntxt_DemandODInt.Text = String.Empty;
            ntxt_DemandDueInt.Text = String.Empty;
            ntxt_DemandPrincipalOutstanding.Text = String.Empty;
            ntxt_DemandPrincipalOverdue.Text = String.Empty;
            ntxt_DemandPrincipalCurrent.Text = String.Empty;
            ntxt_DemandDueInterest.Text = String.Empty;
            ntxt_DemandOverdueInterest.Text = String.Empty;
            ntxt_DemandCurrentInterest.Text = String.Empty;
            ntxt_ActualBalance.Text = String.Empty;
            txt_CashBook.Text = String.Empty;
            ntxt_TotalCollection.Text = String.Empty;
            txtAccountHead.Text = String.Empty;
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                string Cust = dtpkr_CollectionDate.Text;
                DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (rdobtn_ReceivedType.SelectedValue == "A/c Adjustment")
                {
                    double bal = Double.Parse(ntxt_TotalCollection.Text);
                    double acbal = Double.Parse(txtAccountBalance.Text);

                    if (bal > acbal)
                    {
                        MessageBox(this, "You Have Not Sufficient Balance In Your Account");
                    }
                    else
                    {
                        objBO_Finance.Flag = 1;
                        objBO_Finance.SL_CODE = cmbx_AccountNo.SelectedValue;
                        //objBO_Finance.CollectionDate = Convert.ToDateTime(dtpkr_CollectionDate.Text);


                        objBO_Finance.CollectionDate = Cust1;


                        objBO_Finance.ReceivedType = rdobtn_ReceivedType.SelectedValue;
                        if (cmbx_LdgCode.SelectedValue != "")
                        {

                            objBO_Finance.BankLedger = cmbx_LdgCode.SelectedValue;
                        }
                        else
                        {
                            objBO_Finance.BankLedger = "0";
                        }
                        if (cmbx_SavingsAcNo.SelectedValue != "")
                        {
                            objBO_Finance.SB_SL_CODE = cmbx_SavingsAcNo.SelectedValue;
                        }
                        else
                        {
                            objBO_Finance.SB_SL_CODE = "0";
                        }
                        objBO_Finance.INS_TYPE = rdobtn_ReceivedType.SelectedValue;
                        objBO_Finance.CollectionPrincipalOutstanding = Convert.ToDouble(ntxt_DemandPrincipalOutstanding.Text) - Convert.ToDouble(ntxt_DemandPrincipalCurrent.Text); //Convert.ToDouble(ntxt_CollectionPrincipalOutstanding.Text);
                        objBO_Finance.CollectionPrincipalOverdue = 0;//Convert.ToDouble(ntxt_CollectionPrincipalOverdue.Text);
                        objBO_Finance.CollectionPrincipalCurrent = Convert.ToDouble(ntxt_DemandPrincipalCurrent.Text);

                        objBO_Finance.CollectionDueInterest = Convert.ToDouble(ntxt_CollectionDueInterest.Text);
                        objBO_Finance.CollectionOverdueInterest = Convert.ToDouble(ntxt_CollectionOverdueInterest.Text);
                        objBO_Finance.CollectionCurrentInterest = Convert.ToDouble(ntxt_DemandCurrentInterest.Text);
                        objBO_Finance.DemandCurrentInterest = Convert.ToDouble(ntxt_DemandCurrInt.Text) - Convert.ToDouble(ntxt_DemandCurrentInterest.Text);
                        objBO_Finance.DemandOverdueInterest = Convert.ToDouble(ntxt_CollectionOverdueInterest.Text);
                        i = objBL_Finance.InsertUpdateDeleteLoanRepayment(objBO_Finance, out SQLError);
                        if (i > 0)
                        {
                            if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                            {
                                MsgBox("Record Save Successfully . Allotted Voucher is:- " + SQLError);

                                ///MessageBox(this, "Record Save Successfully . Allotted Voucher is:-" + SQLError);

                                //message = "alert('Save Successfully.')";
                                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                                btnsubmit.Text = "Save";
                                btnsubmit1.Text = "Save";
                                Label1.Visible = false;
                                DivID.Visible = true;
                                Label1.Text = "Alloted Voucher is:" + SQLError;
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

                            else
                            {

                                //message = "alert('Something Wrong Input.')";
                                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                            }
                        }
                    }
                }
                if (rdobtn_ReceivedType.SelectedValue == "Cash")
                {
                    objBO_Finance.Flag = 1;
                    objBO_Finance.SL_CODE = cmbx_AccountNo.SelectedValue;
                    //objBO_Finance.CollectionDate = Convert.ToDateTime(dtpkr_CollectionDate.Text);

                    //string Cust = dtpkr_CollectionDate.Text;
                    //DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.CollectionDate = Cust1;


                    objBO_Finance.ReceivedType = rdobtn_ReceivedType.SelectedValue;
                    if (cmbx_LdgCode.SelectedValue != "")
                    {

                        objBO_Finance.BankLedger = cmbx_LdgCode.SelectedValue;
                    }
                    else
                    {
                        objBO_Finance.BankLedger = "0";
                    }
                    if (cmbx_SavingsAcNo.SelectedValue != "")
                    {
                        objBO_Finance.SB_SL_CODE = cmbx_SavingsAcNo.SelectedValue;
                    }
                    else
                    {
                        objBO_Finance.SB_SL_CODE = "0";
                    }
                    objBO_Finance.INS_TYPE = rdobtn_ReceivedType.SelectedValue;
                    objBO_Finance.CollectionPrincipalOutstanding = Convert.ToDouble(ntxt_DemandPrincipalOutstanding.Text) - Convert.ToDouble(ntxt_DemandPrincipalCurrent.Text); //Convert.ToDouble(ntxt_CollectionPrincipalOutstanding.Text);
                    objBO_Finance.CollectionPrincipalOverdue = 0;//Convert.ToDouble(ntxt_CollectionPrincipalOverdue.Text);
                    objBO_Finance.CollectionPrincipalCurrent = Convert.ToDouble(ntxt_DemandPrincipalCurrent.Text);

                    objBO_Finance.CollectionDueInterest = Convert.ToDouble(ntxt_CollectionDueInterest.Text);
                    objBO_Finance.CollectionOverdueInterest = Convert.ToDouble(ntxt_CollectionOverdueInterest.Text);
                    objBO_Finance.CollectionCurrentInterest = Convert.ToDouble(ntxt_DemandCurrentInterest.Text);
                    objBO_Finance.DemandCurrentInterest = Convert.ToDouble(ntxt_DemandCurrInt.Text) - Convert.ToDouble(ntxt_DemandCurrentInterest.Text);
                    objBO_Finance.DemandOverdueInterest = Convert.ToDouble(ntxt_CollectionOverdueInterest.Text);
                    i = objBL_Finance.InsertUpdateDeleteLoanRepayment(objBO_Finance, out SQLError);
                    if (i > 0)
                    {
                        if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                        {
                            ResetControls();
                            MsgBox("Record Save Successfully . Allotted Voucher is:- " + SQLError);

                            //MessageBox(this, "Record Save Successfully . Allotted Voucher is:-" + SQLError);

                            //message = "alert('Save Successfully. Allotted Voucher is:-')"+ SQLError;
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                            btnsubmit.Text = "Save";
                            btnsubmit1.Text = "Save";
                            Label1.Visible = false;
                            DivID.Visible = true;
                            Label1.Text = "Alloted Voucher is:" + SQLError;

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

                        else
                        {

                            //message = "alert('Something Wrong Input.')";
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        }
                    }
                }
                if (rdobtn_ReceivedType.SelectedValue == "Bank")
                {
                    objBO_Finance.Flag = 1;
                    objBO_Finance.SL_CODE = cmbx_AccountNo.SelectedValue;
                    //objBO_Finance.CollectionDate = Convert.ToDateTime(dtpkr_CollectionDate.Text);

                    //string Cust = dtpkr_CollectionDate.Text;
                    //DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.CollectionDate = Cust1;


                    objBO_Finance.ReceivedType = rdobtn_ReceivedType.SelectedValue;
                    if (cmbx_LdgCode.SelectedValue != "")
                    {

                        objBO_Finance.BankLedger = cmbx_LdgCode.SelectedValue;
                    }
                    else
                    {
                        objBO_Finance.BankLedger = "0";
                    }
                    if (cmbx_SavingsAcNo.SelectedValue != "")
                    {
                        objBO_Finance.SB_SL_CODE = cmbx_SavingsAcNo.SelectedValue;
                    }
                    else
                    {
                        objBO_Finance.SB_SL_CODE = "0";
                    }
                    objBO_Finance.INS_TYPE = rdobtn_ReceivedType.SelectedValue;
                    objBO_Finance.CollectionPrincipalOutstanding = Convert.ToDouble(ntxt_DemandPrincipalOutstanding.Text) - Convert.ToDouble(ntxt_DemandPrincipalCurrent.Text); //Convert.ToDouble(ntxt_CollectionPrincipalOutstanding.Text);
                    objBO_Finance.CollectionPrincipalOverdue = 0;//Convert.ToDouble(ntxt_CollectionPrincipalOverdue.Text);
                    objBO_Finance.CollectionPrincipalCurrent = Convert.ToDouble(ntxt_DemandPrincipalCurrent.Text);

                    objBO_Finance.CollectionDueInterest = Convert.ToDouble(ntxt_CollectionDueInterest.Text);
                    objBO_Finance.CollectionOverdueInterest = Convert.ToDouble(ntxt_CollectionOverdueInterest.Text);
                    objBO_Finance.CollectionCurrentInterest = Convert.ToDouble(ntxt_DemandCurrentInterest.Text);
                    objBO_Finance.DemandCurrentInterest = Convert.ToDouble(ntxt_DemandCurrInt.Text) - Convert.ToDouble(ntxt_DemandCurrentInterest.Text);
                    objBO_Finance.DemandOverdueInterest = Convert.ToDouble(ntxt_CollectionOverdueInterest.Text);
                    i = objBL_Finance.InsertUpdateDeleteLoanRepayment(objBO_Finance, out SQLError);
                    if (i > 0)
                    {
                        if (btnsubmit.Text == "Save" || btnsubmit1.Text == "Save")
                        {
                            ResetControls();
                            MsgBox("Record Save Successfully . Allotted Voucher is:- " + SQLError);

                            //MessageBox(this, "Record Save Successfully . Allotted Voucher is:-" + SQLError);

                            //message = "alert('Save Successfully. Allotted Voucher is:-')"+ SQLError;
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                            btnsubmit.Text = "Save";
                            btnsubmit1.Text = "Save";
                            Label1.Visible = false;
                            DivID.Visible = true;
                            Label1.Text = "Alloted Voucher is:" + SQLError;

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

                        else
                        {

                            //message = "alert('Something Wrong Input.')";
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        }
                    }
                }
                if (rdobtn_ReceivedType.SelectedValue == "")
                {
                    message = "alert('Please Select Received Type !!')";
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }


        protected void rdobtn_ReceivedType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdobtn_ReceivedType.SelectedValue == "A/c Adjustment")
            {
                DataTable data = objBL_Finance.GetLinkedAcNo(1, cmbx_AccountNo.SelectedValue, out SQLError);
                cmbx_SavingsAcNo.Items.Clear();
                cmbx_SavingsAcNo.DataSource = data;
                cmbx_SavingsAcNo.DataTextField = "SL_CODE";
                cmbx_SavingsAcNo.DataValueField = "SL_CODE";
                cmbx_SavingsAcNo.DataBind();
                cmbx_SavingsAcNo.Items.Insert(0, "A/c No");
                cmbx_SavingsAcNo.Visible = true;
                cmbx_LdgCode.Visible = false;
                ntxt_ActualBalance.Text = "0";
            }
            else if (rdobtn_ReceivedType.SelectedValue == "Bank")
            {
                DataTable data = objBL_Finance.GetLinkedAcNo(2, cmbx_AccountNo.SelectedValue, out SQLError);
                cmbx_LdgCode.Items.Clear();
                cmbx_LdgCode.DataSource = data;
                cmbx_LdgCode.DataTextField = "LDG_CODE";
                cmbx_LdgCode.DataValueField = "LDG_CODE";
                cmbx_LdgCode.DataBind();
                cmbx_LdgCode.Items.Insert(0, "Ledger Code");
                cmbx_SavingsAcNo.Visible = false;
                cmbx_LdgCode.Visible = true;
                txtAccountBalance.Text = "0.00";
                ntxt_ActualBalance.Text = Convert.ToString(ViewState["ActualBalance"]);
            }
            else if (rdobtn_ReceivedType.SelectedValue == "Cash")
            {
                txtAccountBalance.Text = "0.00";
                cmbx_SavingsAcNo.Visible = false;
                cmbx_LdgCode.Visible = false;
                ntxt_ActualBalance.Text = Convert.ToString(ViewState["ActualBalance"]);
            }
        }

        private void IntCalculation(DataSet DSetLoanAcctDetails)
        {
            double DateDiffInDays = 0;
            try
            {

                DateTime dtupto;
                int value = 0;
                DateTime dtCollDt;
                DateTime dt;

                 
                dtupto = Convert.ToDateTime(DSetLoanAcctDetails.Tables[1].Rows[0]["DATE_UPTO"]);
                //DateTime dtColDt = Convert.ToDateTime(dtpkr_CollectionDate.Text);
                string CollDate = dtpkr_CollectionDate.Text;
                dtCollDt = DateTime.ParseExact(CollDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //dtCollDt = DateTime.ParseExact(CollDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                //value = DateTime.Compare(dtupto, dtCollDt);

                if (DSetLoanAcctDetails.Tables[1].Rows.Count > 0)
                {
                    if (value > 0)
                    {
                        message = "alert('Already Paid upto Selected Date..')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        return;

                    }
                }

                else
                {

                    message = "alert('Correct ')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }
                //if (DSetLoanAcctDetails.Tables[1].Rows.Count > 0)
                //{

                //    if (Convert.ToDateTime(DSetLoanAcctDetails.Tables[1].Rows[0]["DATE_UPTO"]) >= Convert.ToDateTime(dtpkr_CollectionDate.Text))
                //    //if (Convert.ToDateTime(DSetLoanAcctDetails.Tables[1].Rows[0]["LAST_REP_DT"]) >= Convert.ToDateTime(dtpkr_CollectionDate.SelectedDate))
                //    {

                //        message = "alert('Already Paid upto Selected Date..')";
                //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                //        return;
                //    }
                //}

                DateTime LastDemandDate;
                double Interest = 0, OD_Interest = 0;
                if (DSetLoanAcctDetails.Tables[2].Rows.Count > 0)
                {
                    ViewState["ActualBalance"] = DSetLoanAcctDetails.Tables[1].Rows[0]["PRIN_OUT"];
                    ntxt_TotalCollection.Text = "0";

                    ntxt_DemandPrincipalOutstanding.Text = Convert.ToString(DSetLoanAcctDetails.Tables[1].Rows[0]["PRIN_OUT"]);
                    ntxt_DemandPrincipalCurrent.Text = Convert.ToString(DSetLoanAcctDetails.Tables[2].Rows[0]["PRIN_OUT"]);
                    ntxt_DemandPrincipalOverdue.Text = "0";

                    ntxt_DemandDueInterest.Text = Convert.ToString(DSetLoanAcctDetails.Tables[2].Rows[0]["INT_DUE"]);
                    //ntxt_DemandDueInterest.DbValue = DSetLoanAcctDetails.Tables[1].Rows[0]["INT_DUE"];

                    //ntxt_CollectionPrincipalOutstanding.Text = "0";
                    //ntxt_CollectionPrincipalCurrent.Text = Convert.ToString(DSetLoanAcctDetails.Tables[2].Rows[0]["PRIN_OUT"]);
                    //ntxt_CollectionPrincipalOverdue.Text = "0";
                    LastDemandDate = Convert.ToDateTime(DSetLoanAcctDetails.Tables[2].Rows[0]["DATE_UPTO"]);
                }
                else
                {
                    if (DSetLoanAcctDetails.Tables[1].Rows.Count > 0)
                    {
                        ViewState["ActualBalance"] = DSetLoanAcctDetails.Tables[1].Rows[0]["PRIN_OUT"];
                        ntxt_TotalCollection.Text = "0";
                        //ntxt_DemandPrincipalOutstanding.Text = Convert.ToString(DSetLoanAcctDetails.Tables[1].Rows[0]["PRIN_OUT"]);
                        ntxt_DemandPrincipalOutstanding.Text = Convert.ToString(DSetLoanAcctDetails.Tables[1].Rows[0]["PRIN_OUT"]);
                        ntxt_DemandPrincipalCurrent.Text = Convert.ToString(DSetLoanAcctDetails.Tables[1].Rows[0]["PRIN_OUT"]);
                        ntxt_DemandPrincipalOverdue.Text = "0";

                        ntxt_DemandDueInterest.Text = "0";

                        //ntxt_CollectionPrincipalOutstanding.Text = "0";
                        //ntxt_CollectionPrincipalCurrent.Text = Convert.ToString(DSetLoanAcctDetails.Tables[1].Rows[0]["PRIN_OUT"]);
                        //ntxt_CollectionPrincipalOverdue.Text = "0";

                        LastDemandDate = Convert.ToDateTime(DSetLoanAcctDetails.Tables[1].Rows[0]["DATE_UPTO"]);
                        double intr = 0;
                        if (DSetLoanAcctDetails.Tables[1].Rows.Count > 0)
                        {
                            intr = Convert.ToDouble(DSetLoanAcctDetails.Tables[1].Rows[0]["INT_CURR"]);
                        }
                        else
                        {
                            intr = 0;
                        }

                        Interest = Convert.ToDouble(DSetLoanAcctDetails.Tables[1].Rows[0]["INT_CURR"]) + Convert.ToDouble(DSetLoanAcctDetails.Tables[1].Rows[0]["INT_OD"]);
                        Interest = Convert.ToDouble(DSetLoanAcctDetails.Tables[1].Rows[0]["INT_CURR"]) + Convert.ToDouble(DSetLoanAcctDetails.Tables[1].Rows[0]["INT_OD"]);
                    }
                    else
                    {

                        message = "alert('No OutStanding for this Account.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                        return;
                    }
                }

                ntxt_DemandPrincipalCurrent.Text = ntxt_DemandPrincipalOutstanding.Text;

                DateTime LastRepDate = Convert.ToDateTime(DSetLoanAcctDetails.Tables[1].Rows[0]["DATE_UPTO"]);

                //double DepositAmt = Convert.ToDouble(ntxt_DemandPrincipalCurrent.Text);
                double DepositAmt = Convert.ToDouble(ntxt_DemandPrincipalOutstanding.Text);
                string RepayMode = Convert.ToString(DSetLoanAcctDetails.Tables[0].Rows[0]["REPAY_MODE"]);
                double ROI = Convert.ToDouble(DSetLoanAcctDetails.Tables[0].Rows[0]["ROI"]);
                double OD_ROI = Convert.ToDouble(DSetLoanAcctDetails.Tables[0].Rows[0]["OD_ROI"]) + ROI;


                ////    DateDiffInDays = ((Convert.ToDateTime(dtpkr_CollectionDate.Text) > LastRepDate ? LastRepDate : Convert.ToDateTime(dtpkr_CollectionDate.Text)) - LastDemandDate).TotalDays;

                //if (Convert.ToDateTime(dtpkr_CollectionDate.Text) > Convert.ToDateTime(DSetLoanAcctDetails.Tables[1].Rows[0]["DATE_UPTO"]))
                //{
                //    DateDiffInDays = (Convert.ToDateTime(dtpkr_CollectionDate.Text) - Convert.ToDateTime(DSetLoanAcctDetails.Tables[1].Rows[0]["DATE_UPTO"])).TotalDays;
                //}


                //double DateDiffInDays = ((Convert.ToDateTime(dtpkr_CollectionDate.Text) > LastRepDate ? LastRepDate : LastDemandDate) - Convert.ToDateTime(dtpkr_CollectionDate.Text)).TotalDays;
                DateDiffInDays = DateDiffInDays < 0 ? 0 : DateDiffInDays;

                //double OD_DateDiffInDays = 0;
                

                if (RepayMode == "MONTHLY" || RepayMode == "YEARLY")

                {
                    string dtcc = dtpkr_CollectionDate.Text;
                    DateTime dtcol = DateTime.ParseExact(dtcc, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string convertedDate = dtcol.ToString("MM/dd/yyyy");
                    DateTime sdtt = Convert.ToDateTime(convertedDate);


                    DateTime OD_DT = (Convert.ToDateTime(DSetLoanAcctDetails.Tables[11].Rows[0]["OD_DT"]));

                    if (sdtt > OD_DT)
                    {
                       
                        string dtc = dtpkr_CollectionDate.Text;
                        DateTime dtcollection = DateTime.ParseExact(dtc, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        double OD_DateDiffInDays = (Convert.ToDateTime(DSetLoanAcctDetails.Tables[0].Rows[0]["LAST_REP_DT"]) - dtcollection).TotalDays;

                        
                        OD_DateDiffInDays = (dtcollection - Convert.ToDateTime(DSetLoanAcctDetails.Tables[0].Rows[0]["LAST_REP_DT"])).TotalDays;

                        OD_Interest = Math.Round((DepositAmt * OD_ROI * OD_DateDiffInDays) / 36500);
                        DateDiffInDays = (Convert.ToDateTime(DSetLoanAcctDetails.Tables[11].Rows[0]["OD_DT"]) - Convert.ToDateTime(DSetLoanAcctDetails.Tables[9].Rows[0]["DATE_FR_INT_CAL"])).TotalDays;
                        Interest = Math.Round((DepositAmt * ROI * DateDiffInDays) / 36500);

                    }
                    else
                    {
                        string dtc = dtpkr_CollectionDate.Text;
                        DateTime dtcollection = DateTime.ParseExact(dtc, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        DateDiffInDays = ( dtcollection - Convert.ToDateTime(DSetLoanAcctDetails.Tables[9].Rows[0]["DATE_FR_INT_CAL"])).TotalDays;
                        Interest = Math.Round((DepositAmt * ROI * DateDiffInDays) / 36500);
                    }
                   
                }
                

                if (RepayMode == "QUARTERLY COMPOUND")
                {

                    if (Convert.ToDateTime(dtpkr_CollectionDate.Text) > Convert.ToDateTime(DSetLoanAcctDetails.Tables[0].Rows[0]["LAST_REP_DT"]))
                    {
                        string dtc = dtpkr_CollectionDate.Text;
                        DateTime dtcollection = DateTime.ParseExact(dtc, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        double totprd = 0;
                        double remdate = 0;

                        DateTime dtfrintcal = Convert.ToDateTime(DSetLoanAcctDetails.Tables[0].Rows[0]["LAST_REP_DT"]);
                        while (dtfrintcal < dtcollection)
                        {

                            if (Math.Abs(((dtfrintcal.Year - dtcollection.Year) * 12) + dtfrintcal.Month - dtcollection.Month) >= 3)
                            {
                                dtfrintcal = dtfrintcal.AddMonths(3);
                                totprd = totprd + 1;
                            }
                            else
                            {
                                remdate = Math.Abs(((dtfrintcal - dtcollection).TotalDays));
                                break;
                            }

                        }
                        double QuarterInterest = DepositAmt * Math.Pow((1 + OD_ROI / 400), totprd);

                        double RemainingInterest = ((QuarterInterest) * OD_ROI * remdate) / 36500;

                        double totint = Math.Round(QuarterInterest + RemainingInterest);
                        OD_Interest = Math.Round(totint - DepositAmt);
                        DateTime dtlrep = Convert.ToDateTime(DSetLoanAcctDetails.Tables[0].Rows[0]["LAST_REP_DT"]);
                        DateTime dtfrint = Convert.ToDateTime(DSetLoanAcctDetails.Tables[9].Rows[0]["DATE_FR_INT_CAL"]);
                        double totprd1 = 0;
                        double remdate1 = 0;
                        while (dtfrint < dtlrep)
                        {

                            if (Math.Abs(((dtfrint.Year - dtcollection.Year) * 12) + dtfrint.Month - dtcollection.Month) >= 3)
                            {
                                dtfrint = dtfrint.AddMonths(3);
                                totprd1 = totprd1 + 1;
                            }
                            else
                            {
                                remdate1 = Math.Abs(((dtfrint - dtlrep).TotalDays));
                                break;
                            }
                        }
                        //DateDiffInDays = (Convert.ToDateTime(DSetLoanAcctDetails.Tables[0].Rows[0]["LAST_REP_DT"]) - Convert.ToDateTime(DSetLoanAcctDetails.Tables[9].Rows[0]["DATE_FR_INT_CAL"])).TotalDays;
                        double curInt = DepositAmt * Math.Pow((1 + ROI / 400), totprd1);

                        double remainingInterest = ((curInt) * ROI * remdate1) / 36500;

                        double Totint = Math.Round(curInt + remainingInterest);
                        Interest = Math.Round(Totint - DepositAmt);

                    }
                    
                    else
                    {
                        string dtc = dtpkr_CollectionDate.Text;
                        DateTime dtcollection = DateTime.ParseExact(dtc, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);



                        double totprd = 0;
                        double remdate = 0;

                        DateTime dtfrintcal = Convert.ToDateTime(DSetLoanAcctDetails.Tables[9].Rows[0]["DATE_FR_INT_CAL"]);
                        while (dtfrintcal < dtcollection)
                        {

                            if (Math.Abs(((dtfrintcal.Year - dtcollection.Year) * 12) + dtfrintcal.Month - dtcollection.Month) >= 3)
                            {
                                dtfrintcal = dtfrintcal.AddMonths(3);
                                totprd = totprd + 1;
                            }
                            else
                            {
                                remdate = Math.Abs(((dtfrintcal - dtcollection).TotalDays));
                                break;
                            }



                        }
                        double QuarterInterest = DepositAmt * Math.Pow((1 + ROI / 400), totprd);

                        double RemainingInterest = ((QuarterInterest) * ROI * remdate) / 36500;

                        double totint = Math.Round(QuarterInterest + RemainingInterest);
                        Interest = Math.Round(totint - DepositAmt);
                    }
                       
                }

     
                ntxt_DemandODInt.Text = Convert.ToString(OD_Interest);
                ntxt_DemandCurrentInterest.Text = Convert.ToString(Interest);
                ntxt_DemandOverdueInterest.Text = Convert.ToString(OD_Interest);
                if (DSetLoanAcctDetails.Tables[10].Rows.Count > 0)
                {
                    double INTDUE = Convert.ToDouble(DSetLoanAcctDetails.Tables[10].Rows[0]["cuu"]);
                    double dueInt = 0;
                    if (INTDUE > 0)
                    {
                        dueInt = INTDUE;
                    }
                    else
                    {
                        dueInt = 0;
                    }
                    ntxt_CollectionCurrentInterest.Text = Convert.ToString(dueInt);

                    if (dueInt > 0)
                    {
                        ntxt_CollectionCurrentInterest.Text = Convert.ToString(dueInt);
                    }
                    else
                    {
                        ntxt_CollectionCurrentInterest.Text = "0";
                    }
                    ntxt_DemandCurrInt.Text = Convert.ToString(Interest + dueInt);

                    if(Convert.ToDouble(ntxt_DemandCurrInt.Text) > 0)
                    {
                        ntxt_DemandCurrInt.Text = Convert.ToString(Interest + dueInt);
                    }
                    else
                    {
                        ntxt_DemandCurrInt.Text = "0";
                    }
                }
                else
                {
                    ntxt_CollectionCurrentInterest.Text = "0";
                }
                //ntxt_CollectionOverdueInterest.Text = Convert.ToString(OD_Interest);
                if (DSetLoanAcctDetails.Tables[10].Rows.Count > 0)
                {
                    ntxt_CollectionOverdueInterest.Text = Convert.ToString(DSetLoanAcctDetails.Tables[10].Rows[0]["OD"]);
                    //ntxt_CollectionDueInterest.Text = Convert.ToString(Convert.ToDouble(DSetLoanAcctDetails.Tables[10].Rows[0]["cuu"]) + Convert.ToDouble(DSetLoanAcctDetails.Tables[10].Rows[0]["OD"]));
                }
                else
                {
                    ntxt_CollectionOverdueInterest.Text = "0";
                    ntxt_CollectionDueInterest.Text = "0";
                }
               
                //ntxt_DemandODInt.Text = Convert.ToString(OD_Interest);
                if (OD_Interest > 0)
                {
                    ntxt_DemandODInt.Text = Convert.ToString(OD_Interest);
                }
                else
                {
                    ntxt_DemandODInt.Text = "0";
                }
                ntxt_CollectionDueInterest.Text = "0";
                ntxt_TotalCollection.Text = Convert.ToString(DepositAmt + Convert.ToDouble(ntxt_CollectionCurrentInterest.Text) + OD_Interest);
                ViewState["DSetLoanAcctDetails"] = DSetLoanAcctDetails;

                ////catch (Exception ex)
                ////{

                ////    //message = ex.Message;
                ////    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                ////    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
                ////}

                lblRepayDt.Text = dtpkr_CollectionDate.Text;
                lblAmtPaid.Text = ntxt_TotalCollection.Text;

                //lblCurPrin.Text = ntxt_CollectionPrincipalCurrent.Text;
                lblODPrin.Text = ntxt_DemandPrincipalOverdue.Text;
                lblcurInt.Text = ntxt_DemandCurrentInterest.Text;
                //ntxt_DemandCurrInt.Text = ntxt_DemandCurrentInterest.Text;
                lblODInt.Text = ntxt_DemandOverdueInterest.Text;

                DemandPrincipalOverdue = ntxt_DemandPrincipalOverdue.Text != "" ? Convert.ToDouble(ntxt_DemandPrincipalOverdue.Text) : 0;
                DemandPrincipalCurrent = ntxt_DemandPrincipalCurrent.Text != "" ? Convert.ToDouble(ntxt_DemandPrincipalCurrent.Text) : 0;
                DemandOverdueInterest = ntxt_DemandOverdueInterest.Text != "" ? Convert.ToDouble(ntxt_DemandOverdueInterest.Text) : 0;
                DemandCurrentInterest = ntxt_DemandCurrentInterest.Text != "" ? Convert.ToDouble(ntxt_DemandCurrentInterest.Text) : 0;
                DemandPrincipalOutstanding = ntxt_DemandPrincipalOutstanding.Text != "" ? Convert.ToDecimal(ntxt_DemandPrincipalOutstanding.Text) : 0;
                DemandDueInterest = ntxt_DemandDueInterest.Text != "" ? Convert.ToDouble(ntxt_DemandDueInterest.Text) : 0;
                if (DemandDueInterest > 0)
                {
                    DemandDueInterest = Convert.ToDouble(ntxt_DemandDueInterest.Text);
                }
                else 
                {
                    DemandDueInterest = 0;
                }
                ntxt_DemandCurrInt.Text = Convert.ToString(Convert.ToDouble(ntxt_CollectionCurrentInterest.Text) + Convert.ToDouble(ntxt_DemandCurrentInterest.Text));
                if (Convert.ToDouble(ntxt_DemandCurrInt.Text) > 0)
                {
                    ntxt_DemandCurrInt.Text = Convert.ToString(Convert.ToDouble(ntxt_CollectionCurrentInterest.Text) + Convert.ToDouble(ntxt_DemandCurrentInterest.Text));
                }
                else
                {
                    ntxt_DemandCurrInt.Text = "0";
                }

                ntxt_DemandCurrentInterest.Text = Convert.ToString(Convert.ToDouble(Interest) + Convert.ToDouble(ntxt_CollectionCurrentInterest.Text));
                ntxt_DemandOverdueInterest.Text = Convert.ToString(Convert.ToDouble(OD_Interest) + Convert.ToDouble(ntxt_CollectionOverdueInterest.Text));

            }
            catch (Exception ex)
            {
                //throw;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
            }
        }
        protected void dtpkr_CollectionDate_TextChanged(object sender, EventArgs e)
        {
            //if (cmbx_AccountNo.SelectedValue != "")
            if (txtAccountHead.Text == "")
            {
                //MessageBox(this, "Select Account Number.");
            }
            else
            {
                //objBO_Finance.Flag = 3;
                //objBO_Finance.SL_CODE = cmbx_AccountNo.SelectedValue;
                //objBO_Finance.ENTRYDATE = dtpkr_CollectionDate.Text;
                //DataSet dSet = objBL_Finance.GetAccountBalance(objBO_Finance, out SQLError);
                //if (dSet.Tables[0].Rows.Count > 0)
                //{

                //    txtAccountBalance.Text = Convert.ToString(dSet.Tables[0].Rows[0]["BALANCE"]);
                //}
                //else
                //{
                //    txtAccountBalance.Text = "0";
                //}
                if (rdobtn_ReceivedType.SelectedValue == "A/c Adjustment")
                {
                    DataTable data = objBL_Finance.GetLinkedAcNo(1, cmbx_AccountNo.SelectedValue, out SQLError);
                    cmbx_SavingsAcNo.Items.Clear();
                    cmbx_SavingsAcNo.DataSource = data;
                    cmbx_SavingsAcNo.DataTextField = "SL_CODE";
                    cmbx_SavingsAcNo.DataValueField = "SL_CODE";
                    cmbx_SavingsAcNo.DataBind();
                }
                else if (rdobtn_ReceivedType.SelectedValue == "Bank")
                {
                    DataTable data = objBL_Finance.GetLinkedAcNo(2, cmbx_AccountNo.SelectedValue, out SQLError);
                    cmbx_LdgCode.Items.Clear();
                    cmbx_LdgCode.DataSource = data;
                    cmbx_LdgCode.DataTextField = "LDG_CODE";
                    cmbx_LdgCode.DataValueField = "LDG_CODE";
                    cmbx_LdgCode.DataBind();
                }
                objBO_Finance.Flag = 1;
                objBO_Finance.SL_CODE = cmbx_AccountNo.SelectedValue;

                //objBO_Finance.CollectionDate = Convert.ToDateTime(dtpkr_CollectionDate.Text);

                string ColDt = dtpkr_CollectionDate.Text;
                DateTime ParseDT = DateTime.ParseExact(ColDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.CollectionDate = ParseDT;
                //objBO_Finance.CollectionDate = DateTime.ParseExact(dtpkr_CollectionDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //objBO_Finance.CollectionDate = DateTime.ParseExact(dtpkr_CollectionDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //DateTime ParseDT1 = DateTime.Parse(dtpkr_CollectionDate.Text, new CultureInfo("en-CA"));      // DateTime.ParseExact(dtpkr_CollectionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //objBO_Finance.CollectionDate = ParseDT1;



                DataSet DSetLoanAcctDetails = objBL_Finance.GetLoanAcctDetails(objBO_Finance, out SQLError);


                ViewState["DSetLoanAcctDetails"] = DSetLoanAcctDetails;

                if (DSetLoanAcctDetails.Tables[3].Rows.Count > 0)
                {
                    txtAccountHead.Text = Convert.ToString(DSetLoanAcctDetails.Tables[3].Rows[0]["scheme_name"]);
                }

                if (DSetLoanAcctDetails.Tables[4].Rows.Count > 0)
                {
                    GVMemberDetail.DataSource = DSetLoanAcctDetails.Tables[4];
                    GVMemberDetail.DataBind();
                }
                else
                {
                    GVMemberDetail.DataSource = null;
                    GVMemberDetail.DataBind();
                }

                if (DSetLoanAcctDetails.Tables[5].Rows.Count > 0)
                {
                    GVRepayHist.DataSource = DSetLoanAcctDetails.Tables[5];
                    GVRepayHist.DataBind();
                }
                else
                {
                    GVRepayHist.DataSource = null;
                    GVRepayHist.DataBind();
                }

                if (DSetLoanAcctDetails.Tables[6].Rows.Count > 0)
                {
                    lblRepayDt.Text = String.Empty;
                    lblAmtPaid.Text = String.Empty;

                    lblCurPrin.Text = String.Empty;
                    lblODPrin.Text = String.Empty;
                    lblcurInt.Text = String.Empty;
                    lblODInt.Text = String.Empty;

                    txt_lrdate.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["maxRepayDt"]);
                    txt_daysdemand.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["DemandDays"]);
                    txt_roii.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["roi"]);
                    txt_odroi.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["OD_ROI"]);
                    lblsocietyname.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["SocietyName"]);
                    lblReceipt.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["ReceiptNo"]);
                    lblHead.Text = Convert.ToString(DSetLoanAcctDetails.Tables[3].Rows[0]["scheme_name"]); ;
                    lblsysAc.Text = cmbx_AccountNo.Text;
                    lblName.Text = Convert.ToString(DSetLoanAcctDetails.Tables[4].Rows[0]["Name"]);
                    ntxt_DemandPrincipalOutstanding.Text = Convert.ToString(DSetLoanAcctDetails.Tables[1].Rows[0]["PRIN_OUT"]);

                }


                else
                {
                    txt_lrdate.Text = String.Empty;
                    txt_daysdemand.Text = String.Empty;
                    txt_roii.Text = String.Empty;
                    txt_odroi.Text = String.Empty;
                }



                IntCalculation(DSetLoanAcctDetails);
                
            }
            


            //DateTime ParseDT1 = DateTime.Parse(dtpkr_CollectionDate.Text, new CultureInfo("en-CA"));      // DateTime.ParseExact(dtpkr_CollectionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //objBO_Finance.CollectionDate = ParseDT1;


            //DataSet DSetLoanAcctDetails = objBL_Finance.GetLoanAcctDetails(objBO_Finance, out SQLError);

            //ViewState["DSetLoanAcctDetails"] = DSetLoanAcctDetails;

            //if (DSetLoanAcctDetails.Tables[3].Rows.Count > 0)
            //{
            //    txtAccountHead.Text = Convert.ToString(DSetLoanAcctDetails.Tables[3].Rows[0]["scheme_name"]);
            //}

            //if (DSetLoanAcctDetails.Tables[4].Rows.Count > 0)
            //{
            //    GVMemberDetail.DataSource = DSetLoanAcctDetails.Tables[4];
            //    GVMemberDetail.DataBind();
            //}
            //else
            //{
            //    GVMemberDetail.DataSource = null;
            //    GVMemberDetail.DataBind();
            //}

            //if (DSetLoanAcctDetails.Tables[5].Rows.Count > 0)
            //{
            //    GVRepayHist.DataSource = DSetLoanAcctDetails.Tables[5];
            //    GVRepayHist.DataBind();
            //}
            //else
            //{
            //    GVRepayHist.DataSource = null;
            //    GVRepayHist.DataBind();
            //}

            //if (DSetLoanAcctDetails.Tables[6].Rows.Count > 0)
            //{
            //    lblRepayDt.Text = String.Empty;
            //    lblAmtPaid.Text = String.Empty;

            //    lblCurPrin.Text = String.Empty;
            //    lblODPrin.Text = String.Empty;
            //    lblcurInt.Text = String.Empty;
            //    lblODInt.Text = String.Empty;

            //    txt_lrdate.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["maxRepayDt"]);
            //    txt_daysdemand.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["DemandDays"]);
            //    txt_roii.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["roi"]);
            //    txt_odroi.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["OD_ROI"]);
            //    lblsocietyname.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["SocietyName"]);
            //    lblReceipt.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["ReceiptNo"]);
            //    lblHead.Text = Convert.ToString(DSetLoanAcctDetails.Tables[3].Rows[0]["scheme_name"]); ;
            //    lblsysAc.Text = cmbx_AccountNo.Text;
            //    lblName.Text = Convert.ToString(DSetLoanAcctDetails.Tables[4].Rows[0]["Name"]);
            //    ntxt_DemandPrincipalOutstanding.Text = Convert.ToString(DSetLoanAcctDetails.Tables[1].Rows[0]["PRIN_OUT"]);

            //}


            //else
            //{
            //    txt_lrdate.Text = String.Empty;
            //    txt_daysdemand.Text = String.Empty;
            //    txt_roii.Text = String.Empty;
            //    txt_odroi.Text = String.Empty;
            //}



            //IntCalculation(DSetLoanAcctDetails);
        }

        protected void cmbx_AccountNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //objBO_Finance.Flag = 3;
            //objBO_Finance.SL_CODE = cmbx_AccountNo.SelectedValue;
            //objBO_Finance.ENTRYDATE = dtpkr_CollectionDate.Text;
            //DataSet dSet = objBL_Finance.GetAccountBalance(objBO_Finance, out SQLError);
            //if (dSet.Tables[0].Rows.Count > 0)
            //{

            //    txtAccountBalance.Text = Convert.ToString(dSet.Tables[0].Rows[0]["BALANCE"]);
            //}
            //else
            //{
            //    txtAccountBalance.Text = "0";
            //}

            if (rdobtn_ReceivedType.SelectedValue == "A/c Adjustment")
            {
                DataTable data = objBL_Finance.GetLinkedAcNo(1, cmbx_AccountNo.SelectedValue, out SQLError);
                cmbx_SavingsAcNo.Items.Clear();
                cmbx_SavingsAcNo.DataSource = data;
                cmbx_SavingsAcNo.DataTextField = "SL_CODE";
                cmbx_SavingsAcNo.DataValueField = "SL_CODE";
                cmbx_SavingsAcNo.DataBind();
            }
            else if (rdobtn_ReceivedType.SelectedValue == "Bank")
            {
                DataTable data = objBL_Finance.GetLinkedAcNo(2, cmbx_AccountNo.SelectedValue, out SQLError);
                cmbx_LdgCode.Items.Clear();
                cmbx_LdgCode.DataSource = data;
                cmbx_LdgCode.DataTextField = "LDG_CODE";
                cmbx_LdgCode.DataValueField = "LDG_CODE";
                cmbx_LdgCode.DataBind();
            }
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = cmbx_AccountNo.SelectedValue;

            //objBO_Finance.CollectionDate = Convert.ToDateTime(dtpkr_CollectionDate.Text);

            //string ColDt = dtpkr_CollectionDate.Text;
            //DateTime ParseDT = DateTime.ParseExact(ColDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.CollectionDate = DateTime.ParseExact(dtpkr_CollectionDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime ParseDT1 = DateTime.Parse(dtpkr_CollectionDate.Text, new CultureInfo("en-CA"));      // DateTime.ParseExact(dtpkr_CollectionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //objBO_Finance.CollectionDate = ParseDT1;



            DataSet DSetLoanAcctDetails = objBL_Finance.GetLoanAcctDetails(objBO_Finance, out SQLError);


            ViewState["DSetLoanAcctDetails"] = DSetLoanAcctDetails;

            if (DSetLoanAcctDetails.Tables[3].Rows.Count > 0)
            {
                txtAccountHead.Text = Convert.ToString(DSetLoanAcctDetails.Tables[3].Rows[0]["scheme_name"]);
            }

            if (DSetLoanAcctDetails.Tables[4].Rows.Count > 0)
            {
                GVMemberDetail.DataSource = DSetLoanAcctDetails.Tables[4];
                GVMemberDetail.DataBind();
            }
            else
            {
                GVMemberDetail.DataSource = null;
                GVMemberDetail.DataBind();
            }

            if (DSetLoanAcctDetails.Tables[5].Rows.Count > 0)
            {
                GVRepayHist.DataSource = DSetLoanAcctDetails.Tables[5];
                GVRepayHist.DataBind();
            }
            else
            {
                GVRepayHist.DataSource = null;
                GVRepayHist.DataBind();
            }

            if (DSetLoanAcctDetails.Tables[6].Rows.Count > 0)
            {
                lblRepayDt.Text = String.Empty;
                lblAmtPaid.Text = String.Empty;

                lblCurPrin.Text = String.Empty;
                lblODPrin.Text = String.Empty;
                lblcurInt.Text = String.Empty;
                lblODInt.Text = String.Empty;

                txt_lrdate.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["maxRepayDt"]);
                txt_daysdemand.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["DemandDays"]);
                txt_roii.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["roi"]);
                txt_odroi.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["OD_ROI"]);
                lblsocietyname.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["SocietyName"]);
                lblReceipt.Text = Convert.ToString(DSetLoanAcctDetails.Tables[6].Rows[0]["ReceiptNo"]);
                lblHead.Text = Convert.ToString(DSetLoanAcctDetails.Tables[3].Rows[0]["scheme_name"]); ;
                lblsysAc.Text = cmbx_AccountNo.Text;
                lblName.Text = Convert.ToString(DSetLoanAcctDetails.Tables[4].Rows[0]["Name"]);

                ntxt_DemandPrincipalOutstanding.Text = Convert.ToString(DSetLoanAcctDetails.Tables[1].Rows[0]["PRIN_OUT"]);

            }


            else
            {
                txt_lrdate.Text = String.Empty;
                txt_daysdemand.Text = String.Empty;
                txt_roii.Text = String.Empty;
                txt_odroi.Text = String.Empty;
            }



            IntCalculation(DSetLoanAcctDetails);

            //IntCalculationGTS();
        }
        protected void IntCalculationGTS()
        {
            Decimal vfNormalInt = 0;
            Decimal VfODInt = 0;
            Decimal vfPrincipal = 0;
            Decimal vfIntDue = 0;
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = cmbx_AccountNo.SelectedValue;
            objBO_Finance.CollectionDate = DateTime.ParseExact(dtpkr_CollectionDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DataTable dtDueDetails = objBL_Finance.GetLoanDueDetails(objBO_Finance, out SQLError);

            if (dtDueDetails.Rows.Count > 0)
            {
                vfNormalInt = Convert.ToDecimal(dtDueDetails.Rows[0]["NormalInterest"]);

                VfODInt = Convert.ToDecimal(dtDueDetails.Rows[0]["ODINTREST"]);
                vfPrincipal = Convert.ToDecimal(dtDueDetails.Rows[0]["Principal"]);
                vfIntDue = Convert.ToDecimal(dtDueDetails.Rows[0]["IntDue"]);
            }

            ntxt_DemandPrincipalCurrent.Text = Convert.ToString(vfPrincipal);
            ntxt_DemandCurrentInterest.Text = Convert.ToString(vfNormalInt);
            ntxt_DemandPrincipalOverdue.Text = "0";
            ntxt_DemandPrincipalOverdue.Enabled = false;
            ntxt_DemandOverdueInterest.Text = Convert.ToString(VfODInt);
            ntxt_DemandCurrInt.Text = Convert.ToString(vfNormalInt);
            ntxt_DemandODInt.Text = Convert.ToString(VfODInt);
            ntxt_DemandDueInt.Text = Convert.ToString(vfIntDue);
            ntxt_DemandDueInterest.Text = Convert.ToString(vfIntDue);
            ntxt_CollectionCurrentInterest.Text = Convert.ToString(vfNormalInt);
            ntxt_CollectionOverdueInterest.Text = Convert.ToString(VfODInt);
            ntxt_CollectionDueInterest.Text = Convert.ToString(vfIntDue);
            ntxt_TotalCollection.Text = Convert.ToString(Convert.ToDouble(VfODInt) + Convert.ToDouble(vfNormalInt) + Convert.ToDouble(vfPrincipal));

            //ntxt_CollectionPrincipalOutstanding.Text = Convert.ToString(vfPrincipal);
            lblRepayDt.Text = dtpkr_CollectionDate.Text;
            lblAmtPaid.Text = ntxt_TotalCollection.Text;

            //lblCurPrin.Text = ntxt_CollectionPrincipalCurrent.Text;
            lblODPrin.Text = ntxt_DemandPrincipalOverdue.Text;
            lblcurInt.Text = ntxt_DemandCurrentInterest.Text;
            lblODInt.Text = ntxt_DemandOverdueInterest.Text;

            DemandPrincipalOverdue = ntxt_DemandPrincipalOverdue.Text != "" ? Convert.ToDouble(ntxt_DemandPrincipalOverdue.Text) : 0;
            DemandPrincipalCurrent = ntxt_DemandPrincipalCurrent.Text != "" ? Convert.ToDouble(ntxt_DemandPrincipalCurrent.Text) : 0;
            DemandOverdueInterest = ntxt_DemandOverdueInterest.Text != "" ? Convert.ToDouble(ntxt_DemandOverdueInterest.Text) : 0;
            DemandCurrentInterest = ntxt_DemandCurrentInterest.Text != "" ? Convert.ToDouble(ntxt_DemandCurrentInterest.Text) : 0;
            DemandPrincipalOutstanding = ntxt_DemandPrincipalOutstanding.Text != "" ? Convert.ToDecimal(ntxt_DemandPrincipalOutstanding.Text) : 0;
            DemandDueInterest = ntxt_DemandDueInterest.Text != "" ? Convert.ToDouble(ntxt_DemandDueInterest.Text) : 0;


        }
        protected void ntxt_TotalCollection_TextChanged(object sender, EventArgs e)
        {
            Textchanged();
        }

        protected void ntxt_DemandDueInterest_TextChanged(object sender, EventArgs e)
        {
            //Textchanged();
        }

        protected void ntxt_DemandOverdueInterest_TextChanged(object sender, EventArgs e)
        {
            //Textchanged();
        }

        protected void ntxt_DemandCurrentInterest_TextChanged(object sender, EventArgs e)
        {
            //Textchanged();
        }

        protected void ntxt_DemandPrincipalOverdue_TextChanged(object sender, EventArgs e)
        {
            //Textchanged();
        }
        protected void Textchanged()
        {
            //ntxt_TotalCollection.Text = Convert.ToString(Convert.ToDecimal(ntxt_DemandPrincipalOverdue.Text) + Convert.ToDecimal(ntxt_DemandPrincipalCurrent.Text) + Convert.ToDecimal(ntxt_DemandOverdueInterest.Text) + Convert.ToDecimal(ntxt_DemandCurrentInterest.Text));
            //lblAmtPaid.Text = ntxt_TotalCollection.Text;
        }

        protected void Select_Cmbx_SavingsAcNo(object sender , EventArgs e)
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = cmbx_SavingsAcNo.SelectedValue;
            string CollDate = dtpkr_CollectionDate.Text;
            DateTime dtCollDt = DateTime.ParseExact(CollDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.ENTRYDATE = Convert.ToString(dtCollDt);
            DataSet dSet = objBL_Finance.GetAccountBalance(objBO_Finance, out SQLError);
            if (dSet.Tables[0].Rows.Count > 0)
            {

                txtAccountBalance.Text = Convert.ToString(dSet.Tables[0].Rows[0]["BALANCE"]);
            }
            else
            {
                txtAccountBalance.Text = "0";
            }
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {

            String AmtPaid = "";
            String Name = "";
            DateTime RepayDt;
            String sysAc = "";
            String Head = "";
            String Receipt = "";
            String societyname = "";

            lblCurPrin.Text = Request.Form[HiddenCurPrin.UniqueID];
            lblODPrin.Text = Request.Form[HiddenODPrin.UniqueID];
            lblcurInt.Text = Request.Form[HiddencurInt.UniqueID];
            lblODInt.Text = Request.Form[HiddenODInt.UniqueID];


            AmtPaid = lblAmtPaid.Text;
            RepayDt = Convert.ToDateTime(lblRepayDt.Text);
            Name = lblName.Text;
            sysAc = lblsysAc.Text;
            Head = lblHead.Text;
            Receipt = lblReceipt.Text;
            societyname = lblsocietyname.Text;

            //Creating the datatable from the above strings

            DataTable dtPrint = new DataTable();

            dtPrint.Columns.Add("SocietyName", typeof(string));
            dtPrint.Columns.Add("Receipt", typeof(string));
            dtPrint.Columns.Add("A/C Head", typeof(string));
            dtPrint.Columns.Add("System A/CNo", typeof(string));
            dtPrint.Columns.Add("Name", typeof(string));
            dtPrint.Columns.Add("Repay Date", typeof(DateTime));
            dtPrint.Columns.Add("Amount Paid", typeof(Int32));
            dtPrint.Columns.Add("Curr Prin", typeof(Int32));
            dtPrint.Columns.Add("OD Prin", typeof(Int32));
            dtPrint.Columns.Add("Curr Int", typeof(Int32));
            dtPrint.Columns.Add("OD Int", typeof(Int32));


            // Create a DataRow  and add to the DataTable
            DataRow dr = dtPrint.NewRow();
            dr["SocietyName"] = societyname;
            dr["Receipt"] = Receipt;
            dr["A/C Head"] = Head;
            dr["System A/CNo"] = sysAc;
            dr["Name"] = Name;
            dr["Repay Date"] = RepayDt;
            dr["Amount Paid"] = AmtPaid;
            dr["Curr Prin"] = lblCurPrin.Text != "" ? Convert.ToDecimal(lblCurPrin.Text) : 0;
            dr["OD Prin"] = lblODPrin.Text != "" ? Convert.ToDecimal(lblODPrin.Text) : 0;
            dr["Curr Int"] = lblcurInt.Text != "" ? Convert.ToDecimal(lblcurInt.Text) : 0;
            dr["OD Int"] = lblODInt.Text != "" ? Convert.ToDecimal(lblODInt.Text) : 0;

            dtPrint.Rows.Add(dr);


            builder = new SqlConnectionStringBuilder(strConn);
            ReportDocument crystalReport = new ReportDocument();
            DataTable dt = new DataTable();
            dt = dtPrint;


            if (dt.Rows.Count > 0)
            {
                Response.Clear();
                crystalReport = new ReportDocument();

                crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptRepaymentPrint.rpt"));
                crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptRepaymentPrint.rpt");

                crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                crystalReport.SetDataSource(dt);

                crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "Repayment");

                Response.End();
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);

            }
        }
    }
}