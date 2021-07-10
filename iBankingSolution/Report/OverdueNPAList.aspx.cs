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
using System.Data.SqlClient;
using System.Configuration;

namespace iBankingSolution.Report
{
    public partial class OverdueNPAList : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        DataTable dt = new DataTable();
        string SQLError = string.Empty;

        static int i;

        Double Balance;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                objBO_Finance.Flag = 7;

                DataTable dt = objBL_Finance.GetLoanSchemeMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    cmbx_CrropName.DataSource = dt;
                    cmbx_CrropName.DataTextField = "SCHEME_NAME";
                    cmbx_CrropName.DataValueField = "SCHEME_CODE";
                    cmbx_CrropName.DataBind();
                }
            }
        }

        protected void cmbx_CrropName_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlDataAdapter da2;

            DataTable dtt = new DataTable();

            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("proc_GetLoanSchemeDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SchemeCode", cmbx_CrropName.SelectedValue);
            da2 = new SqlDataAdapter();
            da2.SelectCommand = cmd;
            da2.Fill(dtt);
            con.Close();
            if (dtt.Rows.Count > 0)
            {

                txt_type.Text = Convert.ToString(dtt.Rows[0]["loan_type"]);

            }
            else
            {

                txt_type.Text = "";

            }


        }

        protected void GenerateOutStandingNPAList()
        {
            Decimal vfNormalInt = 0;
            Decimal VfODInt = 0;
            Decimal vfBalance = 0;
            Decimal vfIntDue = 0;

            dt = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString))
            using (var cmd = new SqlCommand("Proc_NPAOverdue", con))
            using (var da = new SqlDataAdapter(cmd))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchemCode", Convert.ToInt32(cmbx_CrropName.SelectedValue));
                cmd.Parameters.AddWithValue("@APPL_DT", DateTime.ParseExact(dtpkr_AsonDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                da.Fill(dt);
                ViewState["DataTable"] = dt;
            }
            DataTable ccTab = new DataTable();
            ccTab.Clear();
            ccTab.Columns.Add("LF_NO");
            ccTab.Columns.Add("sl_code");
            ccTab.Columns.Add("name");
            ccTab.Columns.Add("sanc_dt");
            ccTab.Columns.Add("net_loan");
            ccTab.Columns.Add("repay_mode");
            ccTab.Columns.Add("inst_amount");
            ccTab.Columns.Add("last_rep_dt");
            ccTab.Columns.Add("Balance");
            ccTab.Columns.Add("vfloancurrint");
            ccTab.Columns.Add("vfloanodint");
            ccTab.Columns.Add("Upto90Days");
            ccTab.Columns.Add("90Daysupto3yrs");
            ccTab.Columns.Add("3yrs4yrs");
            ccTab.Columns.Add("4yrs6yrs");
            ccTab.Columns.Add("Morethan6yrs");
            DataRow dataRow = ccTab.NewRow();
            if (dt.Rows.Count > 0)
            {




                //dataRow["sl_code"] = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Double vfintamt = Convert.ToDouble(dt.Rows[i]["INST_AMOUNT"]);

                    Double slcode = Convert.ToDouble(dt.Rows[i]["sl_code"]);
                    Double vfodroi = Convert.ToDouble(dt.Rows[i]["od_roi"]);
                    Double vfroi = Convert.ToDouble(dt.Rows[i]["roi"]);
                    DateTime aa = Convert.ToDateTime(dt.Rows[i]["last_rep_dt"]);
                    string vfinstap = Convert.ToString(dt.Rows[i]["INST_appl"]);
                    Double vfnetdisb = Convert.ToDouble(dt.Rows[i]["CASH_DISB"]);
                    Double vfsccode = Convert.ToDouble(dt.Rows[i]["scheme_code"]);
                    slcode = Convert.ToDouble(dt.Rows[i]["sl_code"]);
                    vfroi = Convert.ToDouble(dt.Rows[i]["roi"]);
                    String vfLF = Convert.ToString(dt.Rows[i]["LF_NO"]);
                    String Name = Convert.ToString(dt.Rows[i]["Name"]);
                    string SanCDt = Convert.ToString(dt.Rows[i]["sanc_dt"]);
                    Double NetLoanAmt = Convert.ToDouble(dt.Rows[i]["NET_LOAN"]);
                    Double InstalAmt = Convert.ToDouble(dt.Rows[i]["INST_AMOUNT"]);
                    String RepayMode = Convert.ToString(dt.Rows[i]["repay_mode"]);
                    string LastRepayDt = Convert.ToString(dt.Rows[i]["Overduedt"]);


                    Label lblLF_NO = (Label)(RepCCList.FindControl("lblLF_NO"));

                    Label lblslcode = (Label)(RepCCList.FindControl("lblslcode"));
                    Label lblName = (Label)(RepCCList.FindControl("lblName"));
                    Label lblsanc_dt = (Label)(RepCCList.FindControl("lblsanc_dt"));
                    Label lblnet_loan = (Label)(RepCCList.FindControl("lblnet_loan"));
                    Label Lblrepay_mode = (Label)(RepCCList.FindControl("Lblrepay_mode"));
                    Label lblinst_amount = (Label)(RepCCList.FindControl("lblinst_amount"));
                    Label Lbllast_rep_dt = (Label)(RepCCList.FindControl("Lbllast_rep_dt"));
                    Label lblBalance = (Label)(RepCCList.FindControl("lblBalance"));
                    Label lblloancurrint = (Label)(RepCCList.FindControl("lblloancurrint"));
                    Label lblvfloanodint = (Label)(RepCCList.FindControl("lblvfloanodint"));


                    //Label lblLF_NO = (Label)RepCCList.Items[i].FindControl("lblLF_NO");
                    //Label lblslcode = (Label)(RepCCList.FindControl("lblslcode"));
                    //Label lblName = (Label)(RepCCList.FindControl("lblName"));
                    //Label lblsanc_dt = (Label)(RepCCList.FindControl("lblsanc_dt"));
                    //Label lblnet_loan = (Label)(RepCCList.FindControl("lblnet_loan"));
                    //Label Lblrepay_mode = (Label)(RepCCList.FindControl("Lblrepay_mode"));
                    //Label lblinst_amount = (Label)(RepCCList.FindControl("lblinst_amount"));
                    //Label Lbllast_rep_dt = (Label)(RepCCList.FindControl("Lbllast_rep_dt"));
                    //Label lblBalance = (Label)(RepCCList.FindControl("lblBalance"));
                    //Label lblloancurrint = (Label)(RepCCList.FindControl("lblloancurrint"));
                    //Label lblvfloanodint = (Label)(RepCCList.FindControl("lblvfloanodint"));

                    //Balance = FindAcBal(slcode, txt_type.Text, dtpkr_AsonDate.Text);

                    objBO_Finance.Flag = 1;
                    objBO_Finance.SL_CODE = slcode;
                    objBO_Finance.CollectionDate = DateTime.ParseExact(dtpkr_AsonDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    DataTable dtDueDetails = objBL_Finance.GetLoanDueDetails(objBO_Finance, out SQLError);

                    if (dtDueDetails.Rows.Count > 0)
                    {
                        vfNormalInt = Convert.ToDecimal(dtDueDetails.Rows[0]["NormalInterest"]);

                        VfODInt = Convert.ToDecimal(dtDueDetails.Rows[0]["ODINTREST"]);
                        vfBalance = Convert.ToDecimal(dtDueDetails.Rows[0]["Principal"]);
                        vfIntDue = Convert.ToDecimal(dtDueDetails.Rows[0]["IntDue"]);
                    }


                    if (vfBalance > 0)
                    {

                        dataRow = ccTab.NewRow();
                        dataRow["LF_NO"] = vfLF;
                        dataRow["sl_code"] = slcode;
                        dataRow["name"] = Name;
                        dataRow["sanc_dt"] = SanCDt;
                        dataRow["net_loan"] = NetLoanAmt;
                        dataRow["repay_mode"] = RepayMode;
                        dataRow["inst_amount"] = InstalAmt;
                        dataRow["last_rep_dt"] = LastRepayDt;
                        dataRow["Balance"] = vfBalance;
                        dataRow["vfloancurrint"] = vfNormalInt;
                        dataRow["vfloanodint"] = VfODInt;
                        ccTab.Rows.Add(dataRow);


                    }
                    ViewState["DataTable"] = ccTab;
                    Session["dtNPA"] = ccTab;
                }

                RepCCList.DataSource = ccTab;
                RepCCList.DataBind();
            }


        }
        //public double FindAcBal(Double SLCODE,String Tp,string dtt)
        //{
        //    Double totp;
        //    SqlDataAdapter da2 = new SqlDataAdapter();
        //    var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }
        //    string sql = "SELECT dbo.Find_AcBal_LoanFarmAndNonFarm(" + SLCODE + ", '" + Tp + "','" + dtt + "')";
        //    SqlCommand cmd1 = new SqlCommand("SELECT dbo.Find_AcBal_LoanFarmAndNonFarm("+SLCODE+", '"+Tp+"', '"+dtt+"')", con);
        //    //SqlParameter code = new SqlParameter("@SL_CODE", SqlDbType.Decimal);
        //    //SqlParameter tp = new SqlParameter("@Type", SqlDbType.VarChar);
        //    //SqlParameter dt = new SqlParameter("@AsOnDate", SqlDbType.Date);

        //    //code.Value = SLCODE;
        //    //tp.Value = tp;
        //    //dt.Value = dtt;

        //    totp = Convert.ToDouble(cmd1.ExecuteScalar());
        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }
        //    return totp;

        //  }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            //if (cmbx_CrropName.Text != "")
            //{
            //    GenerateOutStandingNPAList();
            //    String message = String.Empty;
            //    if (System.Web.HttpContext.Current.Session["dtNPA"] != null)
            //    {
            //        string url = "../ExportDownloadPage/ExportToOutStandingNPAList.aspx?ID=" + ddlExportReport.SelectedItem.Text;
            //        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=0,top=0,resizable=yes');";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", s, true);
            //    }
            //    else
            //    {
            //        message = "alert('No Data Found')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    }
            //}


        }
        protected void RepCCList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Int32 vfDays = 0;
            Int32 vfMonth = 0;

            DataTable dtt = (DataTable)Session["dtNPA"];
            //for (int i = 0; i <= RepCCList.Items.Count; i++)
            //{

            //    Label balance = i.FindControl("lblbalance") as Label;
            //    balance.Text = dtt.Rows[ri]["Balance"];

            //}

            ////if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            ////{

            ////    //Reference the Repeater Item.
            ////    Label balance = (Label)e.Item.FindControl("lblbalance");

            ////    //Reference the Controls.
            ////    if (i < dtt.Rows.Count)
            ////    {
            ////        balance.Text = Convert.ToString(dtt.Rows[i]["Balance"]);
            ////    }
            ////    i = i + 1;
            ////}

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label balance = (Label)e.Item.FindControl("lblbalance");
                Label lblsanc_dt = (Label)e.Item.FindControl("lblsanc_dt");
                Label lblBalance = (Label)e.Item.FindControl("lblBalance");
                Label lblUpto90Days = (Label)e.Item.FindControl("lblUpto90Days");
                Label lbl90Daysupto3yrs = (Label)e.Item.FindControl("lbl90Daysupto3yrs");
                Label  lbl3yrs4yrs = (Label)e.Item.FindControl("lbl3yrs4yrs");
                Label lbl4yrs6yrs = (Label)e.Item.FindControl("lbl4yrs6yrs");
                
                DateTime dt = DateTime.ParseExact(dtpkr_AsonDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime applDt = DateTime.ParseExact(lblsanc_dt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                vfDays = (applDt - dt).Days;
                vfMonth = dt.Subtract(applDt).Days / 30;
                int Months = ((applDt - dt).Days) / 30;
                


           
                if (vfDays >= 91 && vfDays <= 1095)
                    lblUpto90Days.Text = lblBalance.Text;
                else if (vfMonth >= 36   && vfDays < 91)
                    lbl90Daysupto3yrs.Text = lblBalance.Text;
                else if (vfMonth >= 48 && vfDays < 91)
                    lbl3yrs4yrs.Text = lblBalance.Text;
                else if (vfMonth >= 72 && vfDays > 2190)
                    lbl4yrs6yrs.Text = lblBalance.Text;






            }

        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GenerateOutStandingNPAList();
            //DataTable dtt = (DataTable)ViewState["DataTable"];
            //RepCCList.DataSource = null;
            //RepCCList.DataBind();


        }
    }
}