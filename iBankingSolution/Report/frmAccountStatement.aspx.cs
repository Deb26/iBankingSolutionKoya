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

namespace iBankingSolution.Report
{

    public partial class frmAccountStatement : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        static int i;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                i = 0;
            }
        }
        protected void GenerateAccountStatement()
        {


            DataSet1 ds = new DataSet1();
            DataTable dt = ds.dsAccountStatementReport;
            decimal Balance = 0;
            string BalanceCrDr = "";
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = txt_AcctNo.Text;
            if (dtpkr_frmDate.Text != "")
            {
                objBO_Finance.FDate = DateTime.ParseExact(dtpkr_frmDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.TDate = DateTime.ParseExact(dtpkr_toDate.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                if (!string.IsNullOrEmpty(dtpkr_frmDate.Text))
                {
                    string fd = dtpkr_frmDate.Text;
                    DateTime fdt = DateTime.ParseExact(fd, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.FDate = fdt;
                }
                //objBO_Finance.FDate = string.IsNullOrEmpty(dtpkr_frmDate.Text) ? (DateTime?)null : Convert.ToDateTime(dtpkr_frmDate.Text);

                if (!string.IsNullOrEmpty(dtpkr_toDate.Text))
                {
                    string td = dtpkr_toDate.Text;
                    DateTime tdt = DateTime.ParseExact(td, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.TDate = tdt;
                }
                //objBO_Finance.TDate = string.IsNullOrEmpty(dtpkr_toDate.Text) ? (DateTime?)null : Convert.ToDateTime(dtpkr_toDate.Text);

            }

            if (dtpkr_frmDate.Text != "" && dtpkr_toDate.Text != "")
            {
                DataSet dtAcctStatement = objBL_Finance.ActStatementReport(objBO_Finance, out SQLError);
                if (dtAcctStatement.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dtAcctStatement.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            Balance = Convert.ToDecimal(dtAcctStatement.Tables[1].Rows[0]["act_op_cr"].ToString().Split(' ')[0])
                               + Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_CREDIT"]) - Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_DEBIT"]);
                            BalanceCrDr = Balance >= 0 ? " Cr." : " Dr.";
                            dt.Rows.Add
                                (


                                    dtAcctStatement.Tables[0].Rows[i]["CCB_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["CCB_DATE"],
                                    dtAcctStatement.Tables[0].Rows[i]["LDG_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["SL_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["AMT_DEBIT"],
                                    dtAcctStatement.Tables[0].Rows[i]["AMT_CREDIT"],
                                    dtAcctStatement.Tables[0].Rows[i]["NARRATION2"],
                                    dtAcctStatement.Tables[0].Rows[i]["VOUCHER_NO"],
                                    dtAcctStatement.Tables[0].Rows[i]["TYPE"],
                                    dtAcctStatement.Tables[0].Rows[i]["TR_TYPE"],

                                    Convert.ToString(Math.Abs(Balance)) + BalanceCrDr
                                , dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_NAME"],
                                dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_ADDRESS1"],
                                dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_ADDRESS2"],
                                 dtAcctStatement.Tables[1].Rows[i]["ACTYPE"],
                                 dtAcctStatement.Tables[1].Rows[i]["AC_STATUS"],
                                 dtAcctStatement.Tables[1].Rows[i]["ACT_OP_CR"],
                                 dtAcctStatement.Tables[1].Rows[i]["OLD_ACNO"],
                                 //dtAcctStatement.Tables[1].Rows[i]["SL_CODE"],
                                 dtAcctStatement.Tables[1].Rows[i]["MEMNO"],
                                 dtAcctStatement.Tables[1].Rows[i]["PAN_CARD_NO"],
                                 dtAcctStatement.Tables[1].Rows[i]["ADHAR_NO"],
                                 dtAcctStatement.Tables[1].Rows[i]["SL_NAME"],

                                 dtAcctStatement.Tables[1].Rows[i]["OPBAL"],
                                 dtAcctStatement.Tables[1].Rows[i]["CLBAL"]

                                );
                        }
                        else
                        {
                            Balance = dt.Rows[i - 1]["Balance"].ToString().Split(' ')[1].Contains("Dr") ? -Convert.ToDecimal(dt.Rows[i - 1]["Balance"].ToString().Split(' ')[0])
                               + Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_CREDIT"]) - Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_DEBIT"])
                               : Convert.ToDecimal(dt.Rows[i - 1]["Balance"].ToString().Split(' ')[0])
                               + Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_CREDIT"]) - Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_DEBIT"]);
                            BalanceCrDr = Balance >= 0 ? " Cr." : " Dr.";
                            dt.Rows.Add
                                (

                                    dtAcctStatement.Tables[0].Rows[i]["CCB_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["CCB_DATE"],
                                    dtAcctStatement.Tables[0].Rows[i]["LDG_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["SL_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["AMT_DEBIT"],
                                    dtAcctStatement.Tables[0].Rows[i]["AMT_CREDIT"],
                                    dtAcctStatement.Tables[0].Rows[i]["NARRATION2"],
                                    dtAcctStatement.Tables[0].Rows[i]["VOUCHER_NO"],
                                    dtAcctStatement.Tables[0].Rows[i]["TYPE"],
                                    dtAcctStatement.Tables[0].Rows[i]["TR_TYPE"],
                                    Convert.ToString(Math.Abs(Balance)) + BalanceCrDr,
                                dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_NAME"],
                                dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_ADDRESS1"],
                                dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_ADDRESS2"]

                                //,dtAcctStatement.Tables[1].Rows[i]["OPBAL"],
                                //dtAcctStatement.Tables[1].Rows[i]["CLBAL"]

                                );
                        }
                    }

                    Session["dtAccountStatement"] = dt;



                    lblOldAcNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["old_acno"]);
                    lblSlcode.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["SL_CODE"]);
                    lblAcStatus.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["ac_status"]);
                    lblActype.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["actype"]);

                    lblMemberNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["memno"]);
                    lblPanNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["pan_card_no"]);
                    lblAdharNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["ADHAR_NO"]);
                    lblAcHolderName.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["sl_name"]);
                    lblbalance.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(dtAcctStatement.Tables[1].Rows[0]["OPBAL"])));
                }
            }
            else if (dtpkr_frmDate.Text == "" && dtpkr_toDate.Text == "")
            {
                DataSet dtAcctStatement = objBL_Finance.AcctStatementReport(objBO_Finance, out SQLError);
                if (dtAcctStatement.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dtAcctStatement.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            Balance = Convert.ToDecimal(dtAcctStatement.Tables[1].Rows[0]["act_op_cr"].ToString().Split(' ')[0])
                               + Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_CREDIT"]) - Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_DEBIT"]);
                            BalanceCrDr = Balance >= 0 ? " Cr." : " Dr.";
                            dt.Rows.Add
                                (


                                    dtAcctStatement.Tables[0].Rows[i]["CCB_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["CCB_DATE"],
                                    dtAcctStatement.Tables[0].Rows[i]["LDG_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["SL_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["AMT_DEBIT"],
                                    dtAcctStatement.Tables[0].Rows[i]["AMT_CREDIT"],
                                    dtAcctStatement.Tables[0].Rows[i]["NARRATION2"],
                                    dtAcctStatement.Tables[0].Rows[i]["VOUCHER_NO"],
                                    dtAcctStatement.Tables[0].Rows[i]["TYPE"],
                                    dtAcctStatement.Tables[0].Rows[i]["TR_TYPE"],

                                    Convert.ToString(Math.Abs(Balance)) + BalanceCrDr
                                , dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_NAME"],
                                dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_ADDRESS1"],
                                dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_ADDRESS2"],
                                 dtAcctStatement.Tables[1].Rows[i]["ACTYPE"],
                                 dtAcctStatement.Tables[1].Rows[i]["AC_STATUS"],
                                 dtAcctStatement.Tables[1].Rows[i]["ACT_OP_CR"],
                                 dtAcctStatement.Tables[1].Rows[i]["OLD_ACNO"],
                                 //dtAcctStatement.Tables[1].Rows[i]["SL_CODE"],
                                 dtAcctStatement.Tables[1].Rows[i]["MEMNO"],
                                 dtAcctStatement.Tables[1].Rows[i]["PAN_CARD_NO"],
                                 dtAcctStatement.Tables[1].Rows[i]["ADHAR_NO"],
                                 dtAcctStatement.Tables[1].Rows[i]["SL_NAME"],

                                 dtAcctStatement.Tables[1].Rows[i]["OPBAL"],
                                 dtAcctStatement.Tables[1].Rows[i]["CLBAL"]

                                );
                        }
                        else
                        {
                            Balance = dt.Rows[i - 1]["Balance"].ToString().Split(' ')[1].Contains("Dr") ? -Convert.ToDecimal(dt.Rows[i - 1]["Balance"].ToString().Split(' ')[0])
                               + Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_CREDIT"]) - Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_DEBIT"])
                               : Convert.ToDecimal(dt.Rows[i - 1]["Balance"].ToString().Split(' ')[0])
                               + Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_CREDIT"]) - Convert.ToDecimal(dtAcctStatement.Tables[0].Rows[i]["AMT_DEBIT"]);
                            BalanceCrDr = Balance >= 0 ? " Cr." : " Dr.";
                            dt.Rows.Add
                                (

                                    dtAcctStatement.Tables[0].Rows[i]["CCB_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["CCB_DATE"],
                                    dtAcctStatement.Tables[0].Rows[i]["LDG_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["SL_CODE"],
                                    dtAcctStatement.Tables[0].Rows[i]["AMT_DEBIT"],
                                    dtAcctStatement.Tables[0].Rows[i]["AMT_CREDIT"],
                                    dtAcctStatement.Tables[0].Rows[i]["NARRATION2"],
                                    dtAcctStatement.Tables[0].Rows[i]["VOUCHER_NO"],
                                    dtAcctStatement.Tables[0].Rows[i]["TYPE"],
                                    dtAcctStatement.Tables[0].Rows[i]["TR_TYPE"],
                                    Convert.ToString(Math.Abs(Balance)) + BalanceCrDr,
                                dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_NAME"],
                                dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_ADDRESS1"],
                                dtAcctStatement.Tables[0].Rows[i]["LISCENCEE_ADDRESS2"]

                                //,dtAcctStatement.Tables[1].Rows[i]["OPBAL"],
                                //dtAcctStatement.Tables[1].Rows[i]["CLBAL"]

                                );
                        }
                    }

                    Session["dtAccountStatement"] = dt;



                    lblOldAcNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["old_acno"]);
                    lblSlcode.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["SL_CODE"]);
                    lblAcStatus.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["ac_status"]);
                    lblActype.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["actype"]);

                    lblMemberNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["memno"]);
                    lblPanNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["pan_card_no"]);
                    lblAdharNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["ADHAR_NO"]);
                    lblAcHolderName.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["sl_name"]);
                    lblbalance.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(dtAcctStatement.Tables[1].Rows[0]["OPBAL"])));
                }
                else
                {
                    lblOldAcNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["old_acno"]);
                    lblSlcode.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["SL_CODE"]);
                    lblAcStatus.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["ac_status"]);
                    lblActype.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["actype"]);

                    lblMemberNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["memno"]);
                    lblPanNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["pan_card_no"]);
                    lblAdharNo.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["ADHAR_NO"]);
                    lblAcHolderName.Text = Convert.ToString(dtAcctStatement.Tables[1].Rows[0]["sl_name"]);
                    lblbalance.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(dtAcctStatement.Tables[1].Rows[0]["OPBAL"])));

                }
            }
            
            //if (dtAcctStatement.Tables[1].Rows.Count <= 0)
            //{
            //    message = "alert('Please Check SLCode')";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            //}
            



            //DataTable dt = new DataTable();

            //DataTable dt1 = new DataTable();
            //dt1 = dtAcctStatement.Tables[1];
            //DataSet ds1 = new DataSet();

            //DataTable dtCpy1 = new DataTable();
            //DataTable dtCpy2 = new DataTable();
            //dtCpy1 = dt.Copy();
            //ds1.Tables.Add(dtCpy1);

            //dtCpy2 = dt1.Copy();

            //ds1.Tables.Add(dtCpy2);


            //Session["dtAccountStatement"] = ds1;
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (txt_AcctNo.Text != "")
            {
                //GenerateAccountStatement();
                String message = String.Empty;
                if (System.Web.HttpContext.Current.Session["dtAccountStatement"] != null)
                {
                    string url = "../ExportDownloadPage/ExportToAccountStatementReport.aspx?ID=" + ddlExportReport.SelectedItem.Text;
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

        protected void btnShow_Click(object sender, EventArgs e)
        {
            RepCCList.DataSource = null;
            RepCCList.DataBind();

            GenerateAccountStatement();

            DataTable dtt = new DataTable();
            dtt = (DataTable)Session["dtAccountStatement"];
            if (dtt != null)
            {
                if (dtt.Rows.Count > 0 || dtt != null)
                {
                    RepCCList.DataSource = dtt;
                    RepCCList.DataBind();
                    btnDownload.Visible = true;
                }
            }

            else
            {
                RepCCList.DataSource = null;
                RepCCList.DataBind();
            }

        }

        protected void RepCCList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            //DataTable dtt = (DataTable)Session["dtAccountStatement"];
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{

            //    //Reference the Repeater Item.
            //    Label balance = (Label)e.Item.FindControl("lblbalance");

            //    //Reference the Controls.
            //    if (i < dtt.Rows.Count)
            //    {
            //        balance.Text = Convert.ToString(dtt.Rows[i]["Balance"]);
            //    }
            //    i = i + 1;
            //}

        }

        protected void txt_OldAcctNo_TextChanged(object sender, EventArgs e)
        {

            objBO_Finance.old_acno = txt_OldAcctNo.Text;
            objBO_Finance.SL_CODE = "";
            DataSet dt = objBL_Finance.getSlcodebalance(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                txt_AcctNo.Text = Convert.ToString(dt.Tables[0].Rows[0]["SL_CODE"]);
            }
            else
            {
                txt_AcctNo.Text = "";
            }
        }


        protected void txt_AcctNo_TextChanged(object sender, EventArgs e)
        {
            RepCCList.DataSource = null;
            RepCCList.DataBind();


            objBO_Finance.old_acno = "";
            objBO_Finance.SL_CODE = txt_AcctNo.Text;
            DataSet dt = objBL_Finance.getSlcodebalance(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                txt_OldAcctNo.Text = Convert.ToString(dt.Tables[0].Rows[0]["OLD_ACNO"]);
            }
            else
            {
                txt_OldAcctNo.Text = "";
            }
        }
    }


}