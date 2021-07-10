using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using BusinessObject;
using BLL;

namespace iBankingSolution.Transaction
{
    public partial class frmInwardDetails : System.Web.UI.Page
    {
        MyDBDataContext dbContext = new MyDBDataContext();
        string socCode = String.Empty;
        decimal CspLdger;
        int suspenseLdgr;
        decimal CommLdg;
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        decimal totalAmt = 0;
        int recAffected = 0;
        String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Chilkat.Crypt2 c = new Chilkat.Crypt2();

            if (!this.IsPostBack)
            {
                dtpkr_DateAsOn.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txt_adjustedDt.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

            var PasprintDt = (from cod in dbContext.CODESANDNOs

                              select new
                              {
                                  SocityCode = cod.SOCIETY_CODE,
                                  CspAcno = cod.CSPAcNo,
                                  CommLdg = cod.CommLdg,
                                  SusLedg = cod.SUSPENSE_LDG

                              }).SingleOrDefault();

            if (PasprintDt != null)
            {
                socCode = Convert.ToString(PasprintDt.SocityCode);
                suspenseLdgr = Convert.ToInt32(PasprintDt.SusLedg);
            }


            DateTime EDATE = DateTime.ParseExact(dtpkr_DateAsOn.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //string EDATE = Convert.ToDateTime(dtpkr_DateAsOn.Text).ToString("yyyy/MM/dd");

            //String EDATE = Convert.ToDateTime(dtpkr_DateAsOn.Text).ToString("yyyy/MM/dd");
            string s = "https://www.ipksapiwbscb.net/WBSCBAPI/resources/INWARD/NEFT" + socCode.Trim() + "" + "&Entrydate=" + EDATE + "";

            string json = (new WebClient()).DownloadString(s);
            if (!json.Contains("Sorry No Data Found"))
            {

                string json1 = json.Remove(2, 55);
                var dataSet = JsonConvert.DeserializeObject<DataSet>(json1);
                double VarSlCode;
                string VarCBSAcno = "";

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    //GVInwardDetl.DataSource = dataSet.Tables[0];
                    //GVInwardDetl.DataBind();

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        VarCBSAcno = Convert.ToString(row["CBSAcNo"]);

                        var varChkcode = (from SM in dbContext.SUBLEDGER_MASTERs
                                          where SM.IFC_CODE == VarCBSAcno
                                          select new
                                          {
                                              SLCode = SM.SL_CODE 
                                               

                                          }).SingleOrDefault();
                     row.BeginEdit();
                        if (varChkcode != null)
                        {
                           
                            VarSlCode = Convert.ToDouble(varChkcode.SLCode);
                            row["CBSAcNo"] = VarSlCode;


                        }
                        else
                        {
                            row["CBSAcNo"] = suspenseLdgr;
                        }
                        row.EndEdit();


                    }

                    GVInwardDetl.DataSource = dataSet.Tables[0];
                    GVInwardDetl.DataBind();

                }
                else
                {
                    GVInwardDetl.DataSource = null;
                    GVInwardDetl.DataBind();
                }

            }
            if (json.Contains("Sorry No Data Found"))
            {
                GVInwardDetl.DataSource = null;
                GVInwardDetl.DataBind();
                //lbltotamount.Text = "";
                //NoOfRows.Text = "";
            }
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        public enum MessageType { Success, Error, Info, Warning };



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string vchNo = "";
            int vfcnt = 0;
            int result = 0;
            var PasprintDt = (from cod in dbContext.CODESANDNOs

                              select new
                              {
                                  SocityCode = cod.SOCIETY_CODE,
                                  CspAcno = cod.CSPAcNo,
                                  CommLdg = cod.CommLdg

                              }).SingleOrDefault();

            if (PasprintDt != null)
            {

                CspLdger = Convert.ToDecimal(PasprintDt.CspAcno);
                CommLdg = Convert.ToDecimal(PasprintDt.CommLdg);
            }

            string datedb = txt_adjustedDt.Text;
            DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.disb_date = timedb;

            DataSet dSet = objBL_Finance.GenVoucherNo(objBO_Finance);

            if (dSet.Tables[0].Rows.Count > 0)
            {

                vchNo = Convert.ToString(dSet.Tables[0].Rows[0]["VoucherNo"]);
            }

            foreach (var item in GVInwardDetl.Rows)
            {
                //Label SLcode = (Label)GVInwardDetl.Rows[vfcnt].FindControl("LblSocAcNo");
                Label SLcode = (Label)GVInwardDetl.Rows[vfcnt].FindControl("lblCBSAcno");
                
                Label AmtCredited = (Label)GVInwardDetl.Rows[vfcnt].FindControl("LblAmount");
                Label CommissionAmt = (Label)GVInwardDetl.Rows[vfcnt].FindControl("lblCommission");
                Label Naration = (Label)GVInwardDetl.Rows[vfcnt].FindControl("LblRemarks");

                string Cust = txt_adjustedDt.Text;
                DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        //string sql = "INSERT INTO JOURNAL_BOOK(JVOUCHER_NO,SL_CODE,AMT_DEBIT,AMT_CREDIT,J_NARRATION,T_NARRATION,JDATE_OF_ENTRY) VALUES('" + vchNo + "'," + Convert.ToInt32(SLcode.Text) + ",0," + Convert.ToDecimal(AmtCredited.Text) + ",'" + Convert.ToString(Naration.Text) + "','" + Convert.ToString(Naration.Text) + "','" + Cust1.ToString("dd-MMM-yyy") + "')";
                        command.CommandText = "INSERT INTO JOURNAL_BOOK(JVOUCHER_NO,SL_CODE,AMT_DEBIT,AMT_CREDIT,J_NARRATION,T_NARRATION,JDATE_OF_ENTRY) VALUES('" + vchNo + "'," + Convert.ToInt32(SLcode.Text) + ",0," + Convert.ToDecimal(AmtCredited.Text) + ",'" + Convert.ToString(Naration.Text) + "','" + Convert.ToString(Naration.Text) + "','" + Cust1 + "')";
                        result = command.ExecuteNonQuery();
                        recAffected = recAffected + result;

                    }
                    connection.Close();

                }

                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    connection.Open();
                    using (SqlCommand command1 = connection.CreateCommand())
                    {
                        command1.CommandText = "INSERT INTO JOURNAL_BOOK(JVOUCHER_NO,LDG_CODE,AMT_DEBIT,AMT_CREDIT,J_NARRATION,T_NARRATION,JDATE_OF_ENTRY) VALUES('" + vchNo + "'," + CspLdger + "," + Convert.ToDecimal(AmtCredited.Text) + ",0,'" + Convert.ToString(Naration.Text) + "','" + Convert.ToString(Naration.Text) + "','" + Cust1 + "')";
                        result = command1.ExecuteNonQuery();
                        recAffected = recAffected + result;

                    }
                    connection.Close();


                }

                if (CommissionAmt.Text != "0")
                {
                    using (SqlConnection connection = new SqlConnection(strConnString))
                    {
                        connection.Open();
                        using (SqlCommand command2 = connection.CreateCommand())
                        {
                            command2.CommandText = "INSERT INTO JOURNAL_BOOK(JVOUCHER_NO,LDG_CODE,AMT_DEBIT,AMT_CREDIT,J_NARRATION,T_NARRATION,JDATE_OF_ENTRY) VALUES('" + vchNo + "'," + CommLdg + ",0," + Convert.ToDecimal(CommissionAmt.Text) + ",'" + "COMMISION FROM " + Convert.ToString(Naration.Text) + "','" + "COMMISION FROM " + Convert.ToString(Naration.Text) + "','" + Cust1 + "')";
                            result = command2.ExecuteNonQuery();
                            recAffected = recAffected + result;
                        }
                        connection.Close();

                    }

                    using (SqlConnection connection = new SqlConnection(strConnString))
                    {
                        connection.Open();
                        using (SqlCommand command3 = connection.CreateCommand())
                        {
                            command3.CommandText = "INSERT INTO JOURNAL_BOOK(JVOUCHER_NO,SL_CODE,AMT_DEBIT,AMT_CREDIT,J_NARRATION,T_NARRATION,JDATE_OF_ENTRY) VALUES('" + vchNo + "'," + Convert.ToInt32(SLcode.Text) + "," + Convert.ToDecimal(CommissionAmt.Text) + ",0,'COMMISION CHARGE','COMMISION CHARGE','" + Cust1 + "')";
                            result = command3.ExecuteNonQuery();
                            recAffected = recAffected + result;
                        }
                        connection.Close();

                    }

                }

                vfcnt = vfcnt + 1;

            }

            if (recAffected > 0)
            {
                string message = "alert('Update Successfully.')";

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                GVInwardDetl.DataSource = null;
                GVInwardDetl.DataBind();
                //lbltotamount.Text = "";
                //NoOfRows.Text = "";
                dtpkr_DateAsOn.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }

            else
            {
                string message = "alert('Nothing to Update.')";

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                GVInwardDetl.DataSource = null;
                GVInwardDetl.DataBind();
                //lbltotamount.Text = "";
                //NoOfRows.Text = "";
                dtpkr_DateAsOn.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }


        }

        protected void GVInwardDetl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbldt = (Label)e.Row.FindControl("lbldt");
                Label LblAmount = (Label)e.Row.FindControl("LblAmount");
                Label Lblcommission = (Label)e.Row.FindControl("lblCommission");
                //Label LblCrediteAmt = (Label)e.Row.FindControl("lblCrAmount");

                LblAmount.Text = LblAmount.Text != "" ? LblAmount.Text : "0";

                lbldt.Text = txt_adjustedDt.Text;
                decimal Amt = Convert.ToDecimal(LblAmount.Text) * 1m;

                if (Amt > 50000)
                {
                    Lblcommission.Text = Convert.ToString((Convert.ToDecimal(LblAmount.Text) * 1m) / 100);
                    Lblcommission.Text = Math.Round(Convert.ToDecimal(Lblcommission.Text)).ToString("0.00");
                }
                //else if (Amt > 5000 && Amt < 10001)

                //    Lblcommission.Text = Convert.ToString((Convert.ToDecimal(LblAmount.Text) * 0.9m) / 100);

                //else if (Amt > 10000 && Amt < 15001)
                //    Lblcommission.Text = Convert.ToString((Convert.ToDecimal(LblAmount.Text) * 0.8m) / 100);
                //else if (Amt > 15000 && Amt < 20001)
                //    Lblcommission.Text = Convert.ToString((Convert.ToDecimal(LblAmount.Text) * 0.7m) / 100);
                //else if (Amt > 20000 && Amt < 30001)
                //    Lblcommission.Text = Convert.ToString((Convert.ToDecimal(LblAmount.Text) * 0.6m) / 100);
                //else if (Amt > 30000 && Amt < 40001)
                //    Lblcommission.Text = Convert.ToString((Convert.ToDecimal(LblAmount.Text) * 0.5m) / 100);
                //else if (Amt > 40000 && Amt < 60001)
                //    Lblcommission.Text = Convert.ToString((Convert.ToDecimal(LblAmount.Text) * 0.45m) / 100);
                //else if (Amt > 60000)
                //    Lblcommission.Text = Convert.ToString((Convert.ToDecimal(LblAmount.Text) * 0.4m) / 100);
                else
                    Lblcommission.Text = "0";
                //Lblcommission.Text = Lblcommission.Text != "" ? Lblcommission.Text : "0";
                //decimal comm = Convert.ToDecimal(Lblcommission.Text) * 1m;
                //LblCrediteAmt.Text = Convert.ToString(Amt - comm);

                totalAmt = totalAmt + Convert.ToDecimal(Amt);


            }
            //lbltotamount.Text = Convert.ToString(totalAmt);
            //NoOfRows.Text = "No of Records: " + Convert.ToString(GVInwardDetl.Rows.Count);



        }

        protected void cmbx_SelectedIndexChanged(object sender , EventArgs e)
        {
            if (cmbx_selectType.Text == "1")
            {
                btnDownload.Visible = true;
                btnUpdate.Visible = true;
                paneldetails.Visible = true;
                panelINWARDRTGS.Visible = false;
                panelINWARDRETURN_NEFT.Visible = false;
                panelINWARDNEFT.Visible = true;
            }
            else if (cmbx_selectType.Text == "2")
            {
                btnDownload.Visible = true;
                btnUpdate.Visible = true;
                paneldetails.Visible = true;
                panelINWARDRTGS.Visible = true;
                panelINWARDRETURN_NEFT.Visible = false;
                panelINWARDNEFT.Visible = false;
            }
            else if (cmbx_selectType.Text == "3")
            {

            }
            else if (cmbx_selectType.Text == "0")
            {

            }
        }
        protected void Tab1_Click(object sender, EventArgs e)
        {
            
            //btnDownload.Visible = true;
            //btnUpdate.Visible = true;
            //paneldetails.Visible = true;
            //panelINWARDRTGS.Visible = false;
            //panelINWARDRETURN_NEFT.Visible = false;
            //panelINWARDNEFT.Visible = true;

        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            //MainView.ActiveViewIndex = 1;
            //btnDownload.Visible = true;
            //btnUpdate.Visible = true;
            //paneldetails.Visible = true;
            //panelINWARDRTGS.Visible = true;
            //panelINWARDRETURN_NEFT.Visible = false;
            //panelINWARDNEFT.Visible = false;
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            //MainView.ActiveViewIndex = 2;
            btnDownload.Visible = true;
            btnUpdate.Visible = true;
            paneldetails.Visible = true;
            panelINWARDRTGS.Visible = false;
            panelINWARDRETURN_NEFT.Visible = true;
            panelINWARDNEFT.Visible = false;
        }
    }
}