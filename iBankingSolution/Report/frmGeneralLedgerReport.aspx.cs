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
using iBankingSolution.Report.CrystalReports;
using System.Configuration;
using System.Data.SqlClient;
using BLL.Master;

namespace iBankingSolution.Report
{
    public partial class frmGeneralLedgerReport : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;

        MyDBDataContext DBConext = new MyDBDataContext();

        String vftype = "";
        String vftype2 = "";
        Int32 vfcash;
        String vftype3 = "";
        DateTime YrStartDt;
        string message = "";
        Double actop = 0;
        double TotalPayment = 0;
        double TotalReceipt = 0;

        DataTable dtledger;
        DataTable DtTransaction;
        string connStr = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                GetBranch();
                SetYearStartDate();

                dtpkr_frmDate.Text = YrStartDt.ToString("dd/MM/yyyy"); 
                dtpkr_toDate.Text = (DateTime.Now).ToString("dd/MM/yyyy");

                BindLedgerCode();
                BindLdgUsingName();
                //BindLedgerName();
            }
        }

        protected void GetBranch()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            DataTable dt = objBL_Finance.GetBranch(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {
                cmbx_Branch.DataSource = dt;
                cmbx_Branch.DataTextField = "BranchName";
                cmbx_Branch.DataValueField = "BranchID";
                cmbx_Branch.DataBind();
            }
        }

        protected void SetYearStartDate()
        {
            var curs = (from unt in DBConext.CODESANDNOs

                        select new
                        {
                            YrStDt = unt.YEAR_START_DT
                        }).FirstOrDefault();
            if (curs != null)
            {

                YrStartDt = Convert.ToDateTime(curs.YrStDt);

            }
        }
        protected void BindLedgerCode()
        {
            try
            {

                objBO_Finance.Flag = 24;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {
                    cmbx_LedgerCode.DataSource = dtledger;
                    cmbx_LedgerCode.DataValueField = "LDG_CODE";
                    cmbx_LedgerCode.DataTextField = "LDG_CODE";
                    cmbx_LedgerCode.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void BindLdgUsingName()
        {
            try
            {

                objBO_Finance.Flag = 19;
                objBO_Finance.CUST_ID = null;
                objBO_Finance.LDG_CODE = "";
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {

                    Cmbx_LedgerName.DataSource = null;
                    Cmbx_LedgerName.DataSource = dtledger;
                    Cmbx_LedgerName.DataValueField = "LDG_CODE";
                    Cmbx_LedgerName.DataTextField = "NOMENCLATURE";
               
                    Cmbx_LedgerName.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void cmbx_LedgerName_SelectedIndexChanged(object sender , EventArgs e)
        {
            BindCode();
        }
        protected void cmbx_LedgerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLedgerName();
        }

        protected void BindCode()
        {
            try
            {

                objBO_Finance.Flag = 20;
                objBO_Finance.LDG_CODE = Cmbx_LedgerName.SelectedValue;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {
                    cmbx_LedgerCode.DataSource = dtledger;
                    cmbx_LedgerCode.DataValueField = "LDG_CODE";
                    cmbx_LedgerCode.DataTextField = "LDG_CODE";
                    cmbx_LedgerCode.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void BindLedgerName()
        {
            try
            {

                objBO_Finance.Flag = 20;
                objBO_Finance.CUST_ID = null;
                objBO_Finance.LDG_CODE = cmbx_LedgerCode.SelectedValue;
                dtledger = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                ViewState["ledger"] = dtledger;
                if (dtledger.Rows.Count > 0)
                {

                    Cmbx_LedgerName.DataSource = null;
                    Cmbx_LedgerName.DataSource = dtledger;
                    Cmbx_LedgerName.DataValueField = "LDG_CODE";
                    Cmbx_LedgerName.DataTextField = "NOMENCLATURE";
                    Cmbx_LedgerName.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void GetActype()
        {
            DataSet dt = new DataSet();

            SqlConnection con = new SqlConnection(connStr);
            SqlCommand com = new SqlCommand("Proc_GetAcType", con);
            com.Parameters.AddWithValue("@LDGCODE", cmbx_LedgerCode.SelectedValue);
            com.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {
                con.Open();
                da.Fill(dt);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    vftype = Convert.ToString(dt.Tables[0].Rows[0]["type"]);
                    vfcash = Convert.ToInt32(dt.Tables[0].Rows[0]["cash_bank"]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                String msg = ex.Message;
            }
            finally
            {

            }
        }
        protected void GetFindActype2()
        {
            DataSet dt = new DataSet();

            SqlConnection con = new SqlConnection(connStr);
            SqlCommand com = new SqlCommand("Proc_GetAcType2", con);
            com.Parameters.AddWithValue("@LDGCODE", cmbx_LedgerCode.SelectedValue);
            com.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {
                con.Open();
                da.Fill(dt);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    vftype2 = Convert.ToString(dt.Tables[0].Rows[0]["FA_TYPE"]);
                    vftype3 = Convert.ToString(dt.Tables[0].Rows[0]["fa_type2"]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                String msg = ex.Message;
            }
            finally
            {

            }
        }
        Int32 NoofRows = 0;
        protected void GetTrans()
        {
            objBO_Finance.Flag = 1;

            objBO_Finance.LDG_CODE = cmbx_LedgerCode.SelectedValue;
            objBO_Finance.FDate = DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.TDate = DateTime.ParseExact(dtpkr_toDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DtTransaction = objBL_Finance.GetTrans(objBO_Finance, out SQLError);

            //ViewState["Transrecord"] = DtTransaction;
            NoofRows = DtTransaction.Rows.Count;
            if (DtTransaction.Rows.Count > 0)
            {

                RepCCList.DataSource = DtTransaction;
                RepCCList.DataBind();
            }




        }
        protected void CreateDataTable()
        {

            String SocName = "";
            String Add = "";
            String GroupName = "";

            //SocietyName and Address 
            var curs = (from cod in DBConext.CODESANDNOs

                        select new
                        {
                            SocietYName = cod.LISCENCEE_NAME,
                            Address = cod.LISCENCEE_ADDRESS1
                        }).FirstOrDefault();
            if (curs != null)
            {

                SocName = curs.SocietYName;
                Add = curs.Address;

            }
            //Group Name

            var Grp = (from gp in DBConext.GROUP_MASTERs
                       join lm in DBConext.LEDGER_MASTERs on gp.GROUP_CODE equals lm.GROUPCODE
                       where lm.LDG_CODE == Convert.ToDecimal(cmbx_LedgerCode.SelectedValue)
                       select new
                       {
                           GpNm = gp.GROUP_NAME

                       }).FirstOrDefault();
            if (Grp != null)
            {

                GroupName = Grp.GpNm;


            }
            DataTable dtPrint = new DataTable();

            dtPrint.Columns.Add("date_from", typeof(DateTime));
            dtPrint.Columns.Add("payment", typeof(Double));
            dtPrint.Columns.Add("receipt", typeof(Double));
            dtPrint.Columns.Add("Balance", typeof(Double));
            dtPrint.Columns.Add("DrCr", typeof(String));
            dtPrint.Columns.Add("LISCENCEE_NAME", typeof(String));
            dtPrint.Columns.Add("LISCENCEE_ADDRESS1", typeof(String));
            dtPrint.Columns.Add("FromDate", typeof(DateTime));
            dtPrint.Columns.Add("ToDate", typeof(DateTime));
            dtPrint.Columns.Add("ldg_code", typeof(Int32));
            dtPrint.Columns.Add("GroupName", typeof(String));



            foreach (RepeaterItem ri in RepCCList.Items)
            {

                //Find Desired Control 

                Label lblTransDate = ri.FindControl("lblTransDate") as Label;
                Label lblpayment = ri.FindControl("lblpayment") as Label;
                Label lblreceipt = ri.FindControl("lblreceipt") as Label;
                Label lblbalance = ri.FindControl("lblbalance") as Label;
                Label lblDrCr = ri.FindControl("lblDrCr") as Label;

                DataRow dr = dtPrint.NewRow();

                dr["date_from"] = lblTransDate.Text;
                dr["payment"] = lblpayment.Text != "" ? Convert.ToDecimal(lblpayment.Text) : 0;   
                dr["receipt"] = lblreceipt.Text != "" ? Convert.ToDecimal(lblreceipt.Text) : 0;  
                dr["Balance"] = lblbalance.Text != "" ? Convert.ToDecimal(lblbalance.Text) : 0;
                dr["DrCr"] = lblDrCr.Text != "" ? Convert.ToString(lblDrCr.Text) : "";
                dr["LISCENCEE_NAME"] = SocName;
                dr["LISCENCEE_ADDRESS1"] = Add;
                dr["FromDate"] = dtpkr_frmDate.Text;
                //dr["ToDate"] = dtpkr_toDate.Text;
                dr["ToDate"] = DateTime.ParseExact(dtpkr_toDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dr["ldg_code"] = cmbx_LedgerCode.SelectedValue;
                dr["GroupName"] = GroupName;

                dtPrint.Rows.Add(dr);

               

            }
            Session["print"] = dtPrint;

        }

        protected void SetUltimateBalance()
        {
            for (int item = 0; item < RepCCList.Items.Count; item++)
            {
                Label lblbalance = this.RepCCList.Items[item].FindControl("lblbalance") as Label;
                lblbalance.Text = Convert.ToString(Convert.ToDouble(lblbalance.Text) + Convert.ToDouble(txtOpeningBal.Text));
            }
            txtClosingBal.Text = Convert.ToString(Convert.ToDouble(txtClosingBal.Text) + Convert.ToDouble(txtOpeningBal.Text));
        }

        protected void SetUltimateBalanceloan()
        {
            Double OpBal = 0;
            Double CLBal = 0;
            for (int item = 0; item < RepCCList.Items.Count; item++)
            {
                Label lblbalance = this.RepCCList.Items[item].FindControl("lblbalance") as Label;


                if(txtOpeningBal.Text=="")
                {
                    OpBal = 0;
                }
                else
                {
                    OpBal = Convert.ToDouble(txtOpeningBal.Text);
                }
                lblbalance.Text = Convert.ToString(Convert.ToDouble(lblbalance.Text) - OpBal);
            }

            if (vftype3 == "Liabilities")
            {
                txtClosingBal.Text = Convert.ToString(Convert.ToDouble(txtClosingBal.Text) - Convert.ToDouble(txtOpeningBal.Text));
            }
            else
            {
                if(txtClosingBal.Text=="")
                {
                    CLBal = 0;
                }
                else
                {
                    CLBal = Convert.ToDouble(txtClosingBal.Text);
                }
                txtClosingBal.Text = Convert.ToString(CLBal);
            }
        }
        protected void LoadSLOB()
        {
            int result;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand com = new SqlCommand("LoadSLOB", con);
            com.Parameters.AddWithValue("@dt", DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            com.CommandType = CommandType.StoredProcedure;
            result = com.ExecuteNonQuery();
            con.Close();

        }
        protected void LoadLOB()
        {
            int result;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand com = new SqlCommand("loadLOB", con);
            //com.Parameters.AddWithValue("@dt", Convert.ToDateTime(dtpkr_frmDate.Text));
            com.Parameters.AddWithValue("@dt", DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            com.CommandType = CommandType.StoredProcedure;
            result = com.ExecuteNonQuery();
            con.Close();

        }
        protected void LoadBCJ()
        {
            int result;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand com = new SqlCommand("loadBCJ", con);
            com.Parameters.AddWithValue("@dt", DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            com.Parameters.AddWithValue("@ldgcode", cmbx_LedgerCode.SelectedValue);
            com.CommandType = CommandType.StoredProcedure;
            result = com.ExecuteNonQuery();
            con.Close();
        }
        protected void GetGeneralLedgerReport()
        {
            try
            {
                DataSet1 ds = new DataSet1();
                DataTable dt = ds.GeneralLedgerReport;
                objBO_Finance.Flag = 1;
                objBO_Finance.LDG_CODE = cmbx_LedgerCode.SelectedValue;
                objBO_Finance.FDate = Convert.ToDateTime(dtpkr_frmDate.Text);
                objBO_Finance.TDate = Convert.ToDateTime(dtpkr_toDate.Text);
                //objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;
                DataSet dtGeneralLedger = objBL_Finance.GeneralLedgerReport(objBO_Finance, out SQLError);
                if (dtGeneralLedger.Tables[0].Rows.Count <= 0)
                {
                    message = "alert('No Data Found')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }
                //  ViewState["OPBAL"] = dtGeneralLedger.Tables[0].Rows.Count > 0 ? dtGeneralLedger.Tables[0].Rows[0]["act_op_cr"].ToString() : "0.00";
                //rep_GeneralLedger r = new rep_GeneralLedger();
                //Telerik.Reporting.InstanceReportSource instanceReportSource = new Telerik.Reporting.InstanceReportSource();
                if (dtGeneralLedger.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dtGeneralLedger.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (dtGeneralLedger.Tables[1].Rows[0]["FA_TYPE2"].ToString().Trim().ToUpper() == "Liabilities".ToUpper()
                                || dtGeneralLedger.Tables[1].Rows[0]["FA_TYPE2"].ToString().Trim().ToUpper() == "Income".ToUpper()
                                || dtGeneralLedger.Tables[1].Rows[0]["FA_TYPE2"].ToString().Trim().ToUpper() == "Sale".ToUpper())
                            {
                                dt.Rows.Add
                                    (
                                        dtGeneralLedger.Tables[0].Rows[i]["date_from"],
                                        dtGeneralLedger.Tables[0].Rows[i]["ldg_code"],
                                        dtGeneralLedger.Tables[0].Rows[i]["receipt"],
                                        dtGeneralLedger.Tables[0].Rows[i]["payment"],
                                        dtGeneralLedger.Tables[0].Rows[i]["debit"],
                                        dtGeneralLedger.Tables[0].Rows[i]["credit"],
                                        dtGeneralLedger.Tables[0].Rows[i]["type"],
                                       Convert.ToString(Convert.ToDouble(dtGeneralLedger.Tables[1].Rows[0]["act_op_cr"].ToString().Split(' ')[0])
                                        + Convert.ToDouble(dtGeneralLedger.Tables[0].Rows[i]["receipt"])
                                        - Convert.ToDouble(dtGeneralLedger.Tables[0].Rows[i]["payment"])) + " Cr",
                                        dtGeneralLedger.Tables[0].Rows[i]["GroupName"],
                                        dtGeneralLedger.Tables[0].Rows[i]["FromDate"],
                                        dtGeneralLedger.Tables[0].Rows[i]["ToDate"],
                                         dtGeneralLedger.Tables[0].Rows[i]["LISCENCEE_NAME"],
                                          dtGeneralLedger.Tables[0].Rows[i]["LISCENCEE_ADDRESS1"]

                                    //, "", "", ""
                                    );
                            }
                            else
                            {
                                dt.Rows.Add
                                   (
                                       dtGeneralLedger.Tables[0].Rows[i]["date_from"],
                                       dtGeneralLedger.Tables[0].Rows[i]["ldg_code"],
                                       dtGeneralLedger.Tables[0].Rows[i]["receipt"],
                                       dtGeneralLedger.Tables[0].Rows[i]["payment"],
                                       dtGeneralLedger.Tables[0].Rows[i]["debit"],
                                       dtGeneralLedger.Tables[0].Rows[i]["credit"],
                                       dtGeneralLedger.Tables[0].Rows[i]["type"],
                                            Convert.ToString(Convert.ToDouble(dtGeneralLedger.Tables[1].Rows[0]["act_op_cr"].ToString().Split(' ')[0])
                                       + Convert.ToDouble(dtGeneralLedger.Tables[0].Rows[i]["payment"])
                                       - Convert.ToDouble(dtGeneralLedger.Tables[0].Rows[i]["receipt"])) + " Dr",
                                       dtGeneralLedger.Tables[0].Rows[i]["GroupName"],
                                       dtGeneralLedger.Tables[0].Rows[i]["FromDate"],
                                       dtGeneralLedger.Tables[0].Rows[i]["ToDate"],
                                       dtGeneralLedger.Tables[0].Rows[i]["LISCENCEE_NAME"],
                                       dtGeneralLedger.Tables[0].Rows[i]["LISCENCEE_ADDRESS1"]

                                   //, "", "", ""
                                   );
                            }
                        }
                        else
                        {
                            if (dtGeneralLedger.Tables[1].Rows[0]["FA_TYPE2"].ToString().Trim().ToUpper() == "Liabilities".ToUpper()
                                || dtGeneralLedger.Tables[1].Rows[0]["FA_TYPE2"].ToString().Trim().ToUpper() == "Income".ToUpper()
                                || dtGeneralLedger.Tables[1].Rows[0]["FA_TYPE2"].ToString().Trim().ToUpper() == "Sale".ToUpper())
                            {
                                dt.Rows.Add
                                    (
                                        dtGeneralLedger.Tables[0].Rows[i]["date_from"],
                                        dtGeneralLedger.Tables[0].Rows[i]["ldg_code"],
                                        dtGeneralLedger.Tables[0].Rows[i]["receipt"],
                                        dtGeneralLedger.Tables[0].Rows[i]["payment"],
                                        dtGeneralLedger.Tables[0].Rows[i]["debit"],
                                        dtGeneralLedger.Tables[0].Rows[i]["credit"],
                                        dtGeneralLedger.Tables[0].Rows[i]["type"],
                                        Convert.ToString(Convert.ToDouble(dt.Rows[i - 1]["Balance"].ToString().Split(' ')[0])
                                       + Convert.ToDouble(dtGeneralLedger.Tables[0].Rows[i]["payment"])
                                       - Convert.ToDouble(dtGeneralLedger.Tables[0].Rows[i]["receipt"])) + " Dr",
                                        dtGeneralLedger.Tables[0].Rows[i]["GroupName"],
                                        dtGeneralLedger.Tables[0].Rows[i]["FromDate"],
                                        dtGeneralLedger.Tables[0].Rows[i]["ToDate"],
                                        dtGeneralLedger.Tables[0].Rows[i]["LISCENCEE_NAME"],
                                        dtGeneralLedger.Tables[0].Rows[i]["LISCENCEE_ADDRESS1"]

                                    );
                            }
                            else
                            {
                                dt.Rows.Add
                                   (
                                       dtGeneralLedger.Tables[0].Rows[i]["date_from"],
                                       dtGeneralLedger.Tables[0].Rows[i]["ldg_code"],
                                       dtGeneralLedger.Tables[0].Rows[i]["receipt"],
                                       dtGeneralLedger.Tables[0].Rows[i]["payment"],
                                       dtGeneralLedger.Tables[0].Rows[i]["debit"],
                                       dtGeneralLedger.Tables[0].Rows[i]["credit"],
                                       dtGeneralLedger.Tables[0].Rows[i]["type"],
                                       Convert.ToString(Convert.ToDouble(dt.Rows[i - 1]["Balance"].ToString().Split(' ')[0])
                                       + Convert.ToDouble(dtGeneralLedger.Tables[0].Rows[i]["payment"])
                                       - Convert.ToDouble(dtGeneralLedger.Tables[0].Rows[i]["receipt"])) + " Dr",
                                         dtGeneralLedger.Tables[0].Rows[i]["GroupName"],
                                        dtGeneralLedger.Tables[0].Rows[i]["FromDate"],
                                        dtGeneralLedger.Tables[0].Rows[i]["ToDate"]

                                   //, "", ""
                                   );
                            }
                        }
                    }
                    Session["dtGeneraLLedgerReport"] = dt;


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


        protected void GetTotalPaymentReceipt()
        {
            DataTable dtRP = new DataTable();
            SqlConnection con = new SqlConnection(connStr);
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand com = new SqlCommand("proc_CalGlCLBal", con);
            com.Parameters.AddWithValue("@ldg_code", cmbx_LedgerCode.SelectedValue);
            com.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            com.Parameters.AddWithValue("@Todate", DateTime.ParseExact(dtpkr_toDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));

            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {
                con.Open();
                da.Fill(dtRP);
                if (dtRP.Rows.Count > 0)
                {
                    TotalPayment = Convert.ToDouble(dtRP.Rows[0]["TotalPayment"]);
                    TotalReceipt = Convert.ToDouble(dtRP.Rows[0]["TotalReceipt"]);
                }

                else
                {
                    TotalPayment = 0;
                    TotalReceipt = 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }

        }

        protected void GetAct_OpCrOther()
        {
            DataTable dtActOpcr = new DataTable();
            SqlConnection con = new SqlConnection(connStr);
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand com = new SqlCommand("proc_CalTotalReceiptPaymentOther", con);
            com.Parameters.AddWithValue("@LdgCode", cmbx_LedgerCode.SelectedValue);
            //com.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(dtpkr_frmDate.Text));
            com.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {
                con.Open();
                da.Fill(dtActOpcr);
                if (dtActOpcr.Rows.Count > 0)
                {
                    actop = Convert.ToDouble(dtActOpcr.Rows[0]["TotalRP"]);

                    DRCRType = Convert.ToString(dtActOpcr.Rows[0]["Type"]);
                }

                else
                {
                    actop = 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }


        String DRCRType = String.Empty;
        protected void GetAct_OpCr()
        {

            DataTable dtActOpcr = new DataTable();
            SqlConnection con = new SqlConnection(connStr);
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand com = new SqlCommand("proc_CalTotalReceiptPayment", con);
            com.Parameters.AddWithValue("@LdgCode", cmbx_LedgerCode.SelectedValue);
            //com.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(dtpkr_frmDate.Text));
            com.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {
                con.Open();
                da.Fill(dtActOpcr);
                if (dtActOpcr.Rows.Count > 0)
                {
                    actop = Convert.ToDouble(dtActOpcr.Rows[0]["TotalRP"]);
                    DRCRType = Convert.ToString(dtActOpcr.Rows[0]["Type"]);
                }

                else
                {
                    actop = 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }


        }
        protected void GetOpBal()
        {


            //DateTime dt = clsInitialSettings.getYsdt(DateTime.Now);
            //string formattedDate = dt.ToString("dd/MM/yyyy");


            GetAct_OpCr();
            GetTotalPaymentReceipt();
            txtOpeningBal.Text = Convert.ToString(actop);
            txtDebit.Text = Convert.ToString(TotalPayment);
            txtCredit.Text = Convert.ToString(TotalReceipt);
            txtClosingBal.Text = Convert.ToString(Convert.ToDouble(txtCredit.Text) - Convert.ToDouble(txtDebit.Text));
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {


            try
            {
                decimal Balance = 0;
                //string BalanceCrDr = "";

                if (cmbx_Branch.SelectedValue == "0")
                {
                    objBO_Finance.Flag = 2;
                }
                else
                {
                    objBO_Finance.Flag = 1;
                }
                DataSet1 ds = new DataSet1();
                //DataTable dt = ds.GeneralLedgerReport;
                //objBO_Finance.Flag = 1;
                objBO_Finance.LDG_CODE = cmbx_LedgerCode.SelectedValue;

                string dta = dtpkr_frmDate.Text;
                objBO_Finance.FDate = DateTime.ParseExact(dta, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                //objBO_Finance.FDate = Convert.ToDateTime(dtpkr_frmDate.Text);

                string dtaa = dtpkr_toDate.Text;
                objBO_Finance.TDate = DateTime.ParseExact(dtaa, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                objBO_Finance.BranchCode = cmbx_Branch.SelectedValue;
                string BalanceCr = " CR. ";
                string BalanceDr = " DR. ";
                Decimal totalReceipt = 0;
                Decimal totalPayment = 0;
                //objBO_Finance.TDate = Convert.ToDateTime(dtpkr_toDate.Text);
                DataSet dtGeneralLedger = objBL_Finance.GeneralLedgerReport(objBO_Finance, out SQLError);
                if (dtGeneralLedger.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < dtGeneralLedger.Tables[0].Rows.Count; i++)
                    //{
                        if (dtGeneralLedger.Tables[0].Rows[0]["FA_TYPE"].ToString().Trim().ToUpper() == "Liabilities".ToUpper()
                                || dtGeneralLedger.Tables[0].Rows[0]["FA_TYPE"].ToString().Trim().ToUpper() == "Income".ToUpper()
                                || dtGeneralLedger.Tables[0].Rows[0]["FA_TYPE"].ToString().Trim().ToUpper() == "Sale".ToUpper())
                        {
                            double OpeningBalance = Convert.ToDouble(dtGeneralLedger.Tables[0].Rows[0]["OpeningBalance"]);
                            double RunningBalance = OpeningBalance;

                            foreach (DataRow dr in dtGeneralLedger.Tables[0].Rows)
                            {
                                
                                double payment = Convert.ToDouble(dr["payment"]);
                                double receipt = Convert.ToDouble(dr["RECEIPT"]);
                                RunningBalance = Math.Round(((RunningBalance + receipt) - payment),2);

                               
                                if (RunningBalance >= 0)
                                {

                                    dr["Balance"] = Convert.ToString(Math.Abs((RunningBalance))) + BalanceCr;
                                }
                                else
                                {
                                    dr["Balance"] = Convert.ToString(Math.Abs((RunningBalance))) + BalanceDr;
                                }

                                string re = Convert.ToString(Convert.ToDouble(dr["payment"]));
                                string re1 = Convert.ToString(Convert.ToDouble(dr["RECEIPT"]));

                                if (re == "")
                                {
                                    re = "0";
                                }
                                if (re1 == "")
                                {
                                    re1 = "0";
                                }

                                totalPayment += Convert.ToDecimal(re);
                                totalReceipt += Convert.ToDecimal(re1);
                            }

                            lblReceipttotal.Text = totalReceipt.ToString();
                            lblPaymenttotal.Text = totalPayment.ToString();

                            lblClosingBal.Text = Convert.ToString(Math.Round(((OpeningBalance + Convert.ToDouble(totalReceipt)) - Convert.ToDouble(totalPayment)),2));
                            foreach (DataRow dr in dtGeneralLedger.Tables[0].Rows)
                            {
                                dr["ClosingBalance"] = Convert.ToString(Math.Round(((OpeningBalance + Convert.ToDouble(totalReceipt)) - Convert.ToDouble(totalPayment)), 2));

                            }

                        }
                        else
                        {
                            double OpeningBalance = Convert.ToDouble(dtGeneralLedger.Tables[0].Rows[0]["OpeningBalance"]);
                            double RunningBalance = OpeningBalance;

                            foreach (DataRow dr in dtGeneralLedger.Tables[0].Rows)
                            {
                                
                                double payment = Convert.ToDouble(dr["payment"]);
                                double receipt = Convert.ToDouble(dr["RECEIPT"]);
                                RunningBalance = Math.Round(((RunningBalance + payment) - receipt),2);


                                if (RunningBalance >= 0)
                                {

                                    dr["Balance"] = Convert.ToString(Math.Abs((RunningBalance))) + BalanceDr;

                                }
                                else
                                {
                                    dr["Balance"] = Convert.ToString(Math.Abs((RunningBalance))) + BalanceCr;

                                }



                                string re = Convert.ToString(Convert.ToDouble(dr["payment"]));
                                string re1 = Convert.ToString(Convert.ToDouble(dr["RECEIPT"]));

                                if (re == "")
                                {
                                   re = "0";
                                }
                                if (re1 == "")
                                {
                                   re1 = "0";
                                }

                                totalPayment += Convert.ToDecimal(re);
                                totalReceipt += Convert.ToDecimal(re1);
                                
                            }

                            lblReceipttotal.Text = totalReceipt.ToString();
                            lblPaymenttotal.Text = totalPayment.ToString();

                            lblClosingBal.Text = Convert.ToString(Math.Round(((OpeningBalance + Convert.ToDouble(totalPayment)) - Convert.ToDouble(totalReceipt)),2));
                            foreach (DataRow dr1 in dtGeneralLedger.Tables[0].Rows)
                            {
                                dr1["ClosingBalance"] = Convert.ToString(Math.Round(((OpeningBalance + Convert.ToDouble(totalPayment)) - Convert.ToDouble(totalReceipt)),2));
                            }
                        }



                        //DataTable dt = (DataTable)Session["dtGeneralLedger"];
                        RepCCList.DataSource = dtGeneralLedger;
                        RepCCList.DataBind();
                }
                    //lblopeningBal.Text = Convert.ToString(dtGeneralLedger.Tables[0].Rows[0]["OpeningBalance"]);
                //}

                if (dtGeneralLedger.Tables[0].Rows.Count > 0)
                {
                    lblopeningBal.Text = Convert.ToString(dtGeneralLedger.Tables[0].Rows[0]["OpeningBalance"]);
                    //lblClosingBal.Text = Convert.ToString(dtGeneralLedger.Tables[0].Rows[0]["ClosingBalance"]);
                }

                
               



                DataTable dtGenldg = new DataTable();
                dtGenldg = dtGeneralLedger.Tables[0];
                int X = dtGenldg.Rows.Count;
                Session["print"] = dtGenldg;
                
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }


            //txtClosingBal.Text = "";
            //txtOpeningBal.Text = "";
            //txtDebit.Text = "";
            //txtCredit.Text = "";
            //GetActype();
            //GetFindActype2();

            //LoadSLOB();
            //LoadLOB();
            //LoadBCJ();
            //GetTrans();

            //if (cmbx_LedgerCode.Text != "" || Cmbx_LedgerName.Text != "")
            //{

            //    if (vftype3 == "Liabilities")
            //    {
            //        GetOpBal();
            //        CalCulateBalance(); //With DR and CR Settings
            //        SetUltimateBalance();
            //        SetDRCR();

            //    }
            //    else if (vftype3 == "Assets" || vftype3 == "Asset")
            //    {
            //        GetOpBalOther();
            //        CalCulateBalance();
            //        SetUltimateBalance();
            //        SetDRCR();
            //    }
            //    else if (vftype == "Loan" && vftype2 == "Balance Sheet")
            //    {
            //        GetOpBalLoan();
            //        CalCulateBalance();
            //        SetUltimateBalanceloan();
            //        SetDRCRLoan();
            //    }

            //    else if (vftype3 == "Income")
            //    {

            //        CalCulateBalance();
            //        SetUltimateBalanceloan();
            //        SetDRCRLoan();
            //    }
            //    else if (vftype != "Deposit" && vftype != "Loan" && vftype2 == "Profit & Loss")
            //    {
            //        CalCulateBalance();
            //        GetTotalPaymentReceipt();

            //        txtDebit.Text = Convert.ToString(TotalPayment);
            //        txtCredit.Text = Convert.ToString(TotalReceipt);
            //        txtClosingBal.Text = Convert.ToString(Convert.ToDouble(txtCredit.Text) - Convert.ToDouble(txtDebit.Text));
            //        SetUltimateBalanceloan();
            //        SetDRCRLoan();
            //    }

            //    else if ((vftype2.ToUpper() == ("Trading A/C").ToUpper() && vftype3 == "Sale"))
            //    {
            //        GetOpBalForStock();
            //        CalCulateBalance();
            //        GetTotalPaymentReceipt();

            //        txtDebit.Text = Convert.ToString(TotalPayment);
            //        txtCredit.Text = Convert.ToString(TotalReceipt);
            //        txtClosingBal.Text = Convert.ToString(Convert.ToDouble(txtCredit.Text) - Convert.ToDouble(txtDebit.Text));
            //        SetUltimateBalance();
            //        SetDRCRLoan();
            //    }

            //}
            //btnDownload.Visible = true;
            //CreateDataTable();
            //DataTable dtt = (DataTable)Session["print"];

        }
        protected void GetOpBalForStock()
        {

            DataTable dtActOpcr = new DataTable();
            SqlConnection con = new SqlConnection(connStr);
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand com = new SqlCommand("proc_GeOpStockOPBal", con);
            com.Parameters.AddWithValue("@LdgCode", cmbx_LedgerCode.SelectedValue);
            //com.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(dtpkr_frmDate.Text));
            com.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {
                con.Open();
                da.Fill(dtActOpcr);
                if (dtActOpcr.Rows.Count > 0)
                {
                    actop = Convert.ToDouble(dtActOpcr.Rows[0]["OPBal"]);

                }

                else
                {
                    actop = 0;
                }
                txtOpeningBal.Text = Convert.ToString(actop);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }

        }
        protected void GetAct_OpCrloan()
        {
            DataTable dtActOpcr = new DataTable();
            SqlConnection con = new SqlConnection(connStr);
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand com = new SqlCommand("proc_GeOpBalLoan", con);
            com.Parameters.AddWithValue("@LdgCode", cmbx_LedgerCode.SelectedValue);
            //com.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(dtpkr_frmDate.Text));
            com.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {
                con.Open();
                da.Fill(dtActOpcr);
                if (dtActOpcr.Rows.Count > 0)
                {
                    actop = Convert.ToDouble(dtActOpcr.Rows[0]["TotalRP"]);
                }

                else
                {
                    actop = 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        protected void GetOpBalLoan()
        {
            GetAct_OpCrloan();
            txtOpeningBal.Text = Convert.ToString(actop);
            GetTotalPaymentReceipt();

            txtDebit.Text = Convert.ToString(TotalPayment);
            txtCredit.Text = Convert.ToString(TotalReceipt);
            txtClosingBal.Text = Convert.ToString(Convert.ToDouble(txtCredit.Text) - Convert.ToDouble(txtDebit.Text));

        }
        protected void GetOpBalOther()
        {
            GetAct_OpCrOther();
            GetTotalPaymentReceipt();
            txtOpeningBal.Text = Convert.ToString(actop);
            txtDebit.Text = Convert.ToString(TotalPayment);
            txtCredit.Text = Convert.ToString(TotalReceipt);
            txtClosingBal.Text = Convert.ToString(Convert.ToDouble(txtCredit.Text) - Convert.ToDouble(txtDebit.Text));

        }
        protected void SetBalances()
        {

        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (cmbx_LedgerCode.SelectedValue != "0")
            {
               
                DataTable dtt = (DataTable)Session["print"];
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["print"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToGeneralLedgerReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
                    string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=0,top=0,resizable=yes');";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", s, true);
                }
                else
                {
                    message = "alert('No Data Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
            else
            {
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["ReportByLdgCode"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToDetailsDepositLedgerReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
                    string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=0,top=0,resizable=yes');";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", s, true);
                }
                else
                {
                    message = "alert('No Data Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
            btnDownload.Visible = false;
        }

        

        protected void CalCulateBalance()
        {

            double BalanceAmt = 0;
            double ClosingBalance = 0;

            for (int item = 0; item < RepCCList.Items.Count; item++)
            {

                Label lblpayment = this.RepCCList.Items[item].FindControl("lblpayment") as Label;
                Label lblreceipt = this.RepCCList.Items[item].FindControl("lblreceipt") as Label;
                Label lblbalance = this.RepCCList.Items[item].FindControl("lblbalance") as Label;
                Label lbldrcr = this.RepCCList.Items[item].FindControl("lblDrCr") as Label;

                if (item == 0)
                {
                    lblbalance.Text = Convert.ToString(Convert.ToDouble(lblreceipt.Text) - Convert.ToDouble(lblpayment.Text));
                    BalanceAmt = Convert.ToDouble(lblbalance.Text);
                }
                if (item > 0)
                {
                    lblbalance.Text = Convert.ToString(Convert.ToDouble(BalanceAmt) + Convert.ToDouble(lblreceipt.Text) - Convert.ToDouble(lblpayment.Text));
                    BalanceAmt = Convert.ToDouble(lblbalance.Text);
                    if (item == RepCCList.Items.Count)
                    {
                        return;
                    }



                }

            }


        }
        protected void SetDRCRLoan()
        {
            for (int item = 0; item < RepCCList.Items.Count; item++)
            {
                Label lblbalance = this.RepCCList.Items[item].FindControl("lblbalance") as Label;
                Label lbldrcr = this.RepCCList.Items[item].FindControl("lblDrCr") as Label;
                if (Convert.ToDouble(lblbalance.Text) < 0)
                {
                    lbldrcr.Text = "Dr";
                }
                else
                {
                    lbldrcr.Text = "Cr";
                }
            }
        }
        protected void SetDRCR()
        {
            for (int item = 0; item < RepCCList.Items.Count; item++)
            {
                Label lblbalance = this.RepCCList.Items[item].FindControl("lblbalance") as Label;
                Label lbldrcr = this.RepCCList.Items[item].FindControl("lblDrCr") as Label;
                if (Convert.ToDouble(lblbalance.Text) > 0)
                {
                    lbldrcr.Text = "Cr";
                }
                else
                {
                    lbldrcr.Text = "Dr";
                }
            }
        }

    }

}
