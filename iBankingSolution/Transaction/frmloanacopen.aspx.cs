using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using IBANK.Model;

namespace IBANK
{
    public partial class frmloanacopen : System.Web.UI.Page
    {
        sqlconnection scon = new sqlconnection();
        HttpContext hc = HttpContext.Current;
        string url = "frmsearch.aspx?fr=frmloanacopen&caption=Enter loan account no : ";
        private List<Client> lstcl
        {
            get
            {
                if (ViewState["lstcl"] == null)
                    ViewState["lstcl"] = new List<Client>();
                return (List<Client>)ViewState["lstcl"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (scon.CheckFormPermission("frmloanacopen.aspx", Session["user_level"].ToString()) == false)
                {
                    Response.Redirect("//");
                }
               
                 schemenameshow();
                 repaymodeshow();
                 loantypeshow();

                 lblnoofinstt.Visible = false;
                 txtnoofinstt.Visible = false;
                 lblfirstoddet.Visible = false;
                 txtfirstoddet.Visible = false;
                 lblinsttamt.Visible = false;
                 txtinsttamt.Visible = false;
                 txtdateofopening.Text = DateTime.Now.ToString("dd/MM/yyyy");


                 if (Session["FULLY_ONLINE"].ToString() == "y")
                 {
                     txtdateofopening.Visible = false;
                     lbldateofopening.Visible = false;
                 }


                 try
                 {
                     getData();
                 }
                 catch { }
            }

        }
        protected void hdnValue_ValueChanged(object sender, EventArgs e)
        {
            string hdnvalue = ((HiddenField)sender).Value;

            if (hdnvalue.Contains("|"))
            {
                string[] arr = hdnvalue.Split('|');
                SqlCommand cmd = new SqlCommand(@"select CUST_ID as CustID,c.Name as Name,GUARDIAN_NAME as G_Name,v.name as Vill,cl_status from CLIENT_REGISTER c 
                    left outer join VILLAGE v on c.VILL_CODE=v.code where CUST_ID=@cust_id  and c.bank_code=@bank_code and v.bank_code=@bank_code");
                cmd.Parameters.AddWithValue("@cust_id", arr[0]);
                cmd.Parameters.AddWithValue("@bank_code", Session["bank_code"]);
                DataTable dt = scon.getdatatable(cmd);
                if (dt.Rows.Count > 0)
                {
                    foreach (Client c in lstcl)
                    {
                        if (c.custid == dt.Rows[0]["CustID"].ToString())
                        {
                            scon.alert("Cust id already exist");
                            txtcustid.Text = "";
                            hdnValue.Value = "";
                            hdnvalue = "";
                            return;
                        }
                    }
                    Client cl = new Client();
                    cl.custid = dt.Rows[0]["CustID"].ToString();
                    cl.name = dt.Rows[0]["Name"].ToString();
                    cl.gname = dt.Rows[0]["G_Name"].ToString();
                    cl.vill = dt.Rows[0]["Vill"].ToString();
                    cl.cl_status = dt.Rows[0]["cl_status"].ToString() == "m" ? "Member" : "Non-member";
                    lstcl.Add(cl);
                }
                else
                {
                    return;
                }
                grdappl.DataSource = lstcl;
                grdappl.DataBind();
                txtcustid.Text = "";
                hdnValue.Value = "";
                hdnvalue = "";
            }
        }
        

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            count = 10;
            sqlconnection scon = new sqlconnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select top 10 convert(varchar(10),CUST_ID) + '  |   '+Name+ '  |   '+GUARDIAN_NAME as Name  from CLR where bank_code=@bank_code and (Name like @name+'%' or Cust_ID like @name+'%')";
            cmd.Parameters.AddWithValue("@name", prefixText);
            cmd.Parameters.AddWithValue("@bank_code", HttpContext.Current.Session["bank_code"]);
            List<string> customers = new List<string>(count);
            DataTable dtCust = scon.getdatatable(cmd);
            for (int i = 0; i < dtCust.Rows.Count; i++)
            {
                customers.Add(dtCust.Rows[i][0].ToString());
            }
            return customers;

        }

        private void schemenameshow()
        {
            SqlCommand cmd = new SqlCommand("select scheme_name,scheme_code from loan_scheme_master where bank_code=@bank_code order by scheme_name ");
            cmd.Parameters.AddWithValue("@bank_code", Session["bank_code"]);

            DataTable dtscheme = scon.getdatatable(cmd);
            cmbselectloanscheme.DataTextField = "scheme_name";
            cmbselectloanscheme.DataValueField = "scheme_code";
            cmbselectloanscheme.DataSource = dtscheme;
            cmbselectloanscheme.DataBind();
            cmbselectloanscheme.Items.Insert(0, new ListItem("--Select--", "0"));
            cmbselectloanscheme.Items[0].Selected = true;

        }

        //show the loantype in the page load swarup
        private void loantypeshow()
        {
            SqlCommand cmd = new SqlCommand("select distinct loan_term from loan_scheme_master where bank_code=@bank_code");
            cmd.Parameters.AddWithValue("@bank_code",Session["bank_code"]);

            DataTable dtscheme = scon.getdatatable(cmd);
            cmbloantype.DataTextField = "loan_term";
            cmbloantype.DataValueField = "loan_term";
            cmbloantype.DataSource = dtscheme;
            cmbloantype.DataBind();
        }

        //show the reapy mode in pageload swarup
        private void repaymodeshow()
        {
            //SqlCommand cmd = new SqlCommand("select  distinct REPAY_MODE from loan_scheme_master where bank_code=@bank_code");
            //cmd.Parameters.AddWithValue("@bank_code",Session["bank_code"]);

            //DataTable dtrepay = scon.getdatatable(cmd);
            //cmbreapymode.DataTextField = "REPAY_MODE";

            //cmbreapymode.DataValueField = "REPAY_MODE";
            //cmbreapymode.DataSource = dtrepay;
            //cmbreapymode.DataBind();
            cmbreapymode.Items.Add("MONTHLY");
            cmbreapymode.Items.Add("QUARTERLY");
            cmbreapymode.Items.Add("HALF YEARLY");
            cmbreapymode.Items.Add("YEARLY");
            cmbreapymode.Items.Add("MONTHLY COMPOUND");
            cmbreapymode.Items.Add("QUARTERLY COMPOUND");
            cmbreapymode.Items.Add("HALF YEARLY COMPOUND");
            cmbreapymode.Items.Add("YEARLY COMPOUND");


            cmbreapymode.Items.Insert(0, new ListItem("--Select--", ""));
            cmbreapymode.Items[0].Selected = true;
        }


        protected void cmbselectloanscheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select  loan_term,repay_mode from loan_scheme_master where SCHEME_CODE=@scode and bank_code=@bank_code");
            cmd.Parameters.AddWithValue("@scode", cmbselectloanscheme.SelectedValue);
            cmd.Parameters.AddWithValue("@bank_code",Session["bank_code"]);
            DataTable dtschemetype = scon.getdatatable(cmd);
            cmbloantype.DataTextField = "loan_term";
            cmbloantype.DataValueField = "loan_term";
            cmbloantype.DataSource = dtschemetype;
            cmbloantype.Text = dtschemetype.Rows[0]["loan_term"].ToString();

            cmbreapymode.SelectedIndex = cmbreapymode.Items.IndexOf(cmbreapymode.Items.FindByValue(dtschemetype.Rows[0]["repay_mode"].ToString()));

            
        }

        //show the another field when user select instype=yes  swarup
        protected void cmbinsttapply_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadinst();
        }

        protected void cmbreapymode_SelectedIndexChanged(object sender, EventArgs e)
        {
           // cmbinsttapply.SelectedValue = "0";

        }

        protected void txtdateofopening_TextChanged(object sender, EventArgs e)
        {
           
        

        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }

        protected void txtloavperiod_TextChanged(object sender, EventArgs e)
        {

           //show the last_repay date calculate  according to the period of month....start swarup

            var date=  txtdateofopening.Text.ToString();  // you text box date
            var d = date.Split('/');
            int day = Convert.ToInt16(d[0]);
            int month = Convert.ToInt16(d[1]);
            int year = Convert.ToInt16(d[2]);

            DateTime newdate = new DateTime(year, month, day).AddMonths(int.Parse(txtloanprdmon.Text));

            txtlastreapydet.Text = newdate.ToString("dd/MM/yyyy");

        }



        protected void btnclientdel_Click(object sender, EventArgs e)
        {
            Button img = (Button)sender;
            GridViewRow gr = (GridViewRow)img.NamingContainer;
            lstcl.RemoveAt(gr.RowIndex);
            grdappl.DataSource = lstcl;
            grdappl.DataBind();
        }

       
        protected void btnsave_Click(object sender, EventArgs e)
        {

            if (scon.FillNum(txtrateofinterest) <= 0 || scon.FillNum(txtrateofinterest) >= 100)
            {
                scon.alert("ROI MISMACH!!");
                return;
            }

            if (scon.FillNum(txtodroi) < 0 || scon.FillNum(txtodroi) >= 100)
            {

                scon.alert("OD MISMACH!!");
                return;
            }


            string id = null;

            if (Session["FULLY_ONLINE"].ToString() == "y")
            {
                txtdateofopening.Text = scon.FillDate(DateTime.Now.ToString()).ToString();
            }


            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SP_SUBLEDGER_DETAILS_LOAN";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@action", hdnAction.Value);
                cmd.Parameters.AddWithValue("@APPL_DT", scon.FillDate(txtdateofopening));
                cmd.Parameters.AddWithValue("@LOAN_CASE_NO", txtloancaseno.Text);
                cmd.Parameters.AddWithValue("@LF_NO", txtlfno.Text);
                cmd.Parameters.AddWithValue("@scheme_code", Convert.ToInt32(cmbselectloanscheme.SelectedValue));
                cmd.Parameters.AddWithValue("@LOAN_AMNT", scon.FillDecimal(txttotloan.Text));
                cmd.Parameters.AddWithValue("@NO_OF_INST", scon.FillNum(txtnoofinstt));
                cmd.Parameters.AddWithValue("@REPAY_MODE", cmbreapymode.SelectedValue);
                cmd.Parameters.AddWithValue("@ROI", scon.FillDecimal(txtrateofinterest.Text));
                cmd.Parameters.AddWithValue("@INST_APPL", cmbinsttapply.SelectedValue);
                cmd.Parameters.AddWithValue("@OD_ROI", scon.FillDecimal(txtodroi.Text));
                cmd.Parameters.AddWithValue("@INST_AMOUNT", scon.FillDecimal(txtinsttamt.Text));
                cmd.Parameters.AddWithValue("@ODPR", cmboverduemode.SelectedItem.Text);              
                cmd.Parameters.AddWithValue("@DURATION", Convert.ToInt32(txtloanprdmon.Text));
                cmd.Parameters.AddWithValue("@LAST_REP_DT", scon.FillDate(txtlastreapydet));
                cmd.Parameters.AddWithValue("@MON_PRD", scon.FillNum(txtmonperiod));

                cmd.Parameters.AddWithValue("@INST_ST_DATE", scon.FillDate(txtfirstoddet));
                
                cmd.Parameters.AddWithValue("@NET_LOAN", scon.FillNum(txttotloan));
                cmd.Parameters.AddWithValue("@CASH_DISB", scon.FillNum(txttotloan));
                cmd.Parameters.AddWithValue("@BANK_CODE", Session["BANK_CODE"]);
                cmd.Parameters.AddWithValue("@AC_STATUS", "Live");
                cmd.Parameters.AddWithValue("@OLD_ACNO", txtlfno.Text);
                cmd.Parameters.AddWithValue("@EMPCODE", Session["emp_code"]);
                cmd.Parameters.AddWithValue("@DOO", scon.FillDate(txtdateofopening));
                id = hdnAction.Value == "UPDATE" ? scon.Decrypt(hc.Request["key"]) : scon.getAc("lno", Session["bank_code"].ToString(), Session["branch_code"].ToString());
                cmd.Parameters.AddWithValue("@sl_code", (id));
                scon.excutesql(cmd);

                SqlCommand cmddel = new SqlCommand();
                cmddel.CommandText = "delete from client_master where sl_code=@slcode and bank_code=@bankcode";
                cmddel.Parameters.AddWithValue("@slcode", id);
                cmddel.Parameters.AddWithValue("@bankcode", Session["bank_code"]);
                scon.excutesql(cmddel);
                foreach(Client cl in lstcl)
                {
                    SqlCommand cmmd = new SqlCommand();
                    cmmd.CommandText = "insert into client_master(CUST_ID,sl_code,bank_code)values(@cid,@slcode,@bankcode)";
                    cmmd.Parameters.AddWithValue("@cid", cl.custid);
                    cmmd.Parameters.AddWithValue("@slcode", id);
                    cmmd.Parameters.AddWithValue("@bankcode", Session["bank_code"]);
                    scon.excutesql(cmmd);
                }


                if (hdnAction.Value == "UPDATE")
                {
                    Response.Redirect("frmmsg.aspx?fr=frmloanacopen&msg=The loan account is modified successfully");
                }
                else
                {
                    Response.Redirect("frmmsg.aspx?fr=frmloanacopen&msg=The loan account is created successfully.The loan account number generated is : " + id);
                }

            }
            catch(Exception ex)
            {
                scon.postAlert(ex.Message);
            }

           

        }

        
        protected void txtnoofinstt_TextChanged(object sender, EventArgs e)
        {
          

        }


        protected void getData()                
        {
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from SM where SL_CODE=@slcode and bank_code=@bank_code ";
            cmd.Parameters.AddWithValue("@slcode", scon.Decrypt(hc.Request["key"]));
            cmd.Parameters.AddWithValue("@bank_code", Session["bank_code"]);
            DataTable dtClient = scon.getdatatable(cmd);

            if (dtClient.Rows.Count <= 0 || dtClient == null)
            {
                Response.Redirect(url + "&error=Invalid loan account no.");
                return;
            }
            hdnAction.Value = "UPDATE";
            string Odt = "",lastDt="";
            //txtdateofopening.Text = scon.FillDate(dtClient.Rows[0]["DATE_OF_OPENING"].ToString()).ToString();
            Odt = scon.FillDate(dtClient.Rows[0]["DATE_OF_OPENING"].ToString()).ToString();
            txtdateofopening.Text = Odt.Substring(0, 10);
            txtlfno.Text = dtClient.Rows[0]["OLD_ACNO"].ToString();

            cmd.CommandText = "select * from sdl where sl_code=@slcode and bank_code=@bank_code";
            DataTable dtsdl = scon.getdatatable(cmd);
            txtloancaseno.Text = dtsdl.Rows[0]["loan_case_no"].ToString();
            cmbselectloanscheme.SelectedIndex = cmbselectloanscheme.Items.IndexOf(cmbselectloanscheme.Items.FindByValue(dtsdl.Rows[0]["scheme_code"].ToString()));
            cmbreapymode.SelectedIndex = cmbreapymode.Items.IndexOf(cmbreapymode.Items.FindByValue(dtsdl.Rows[0]["repay_mode"].ToString()));
            cmbinsttapply.SelectedIndex = cmbinsttapply.Items.IndexOf(cmbinsttapply.Items.FindByValue(dtsdl.Rows[0]["inst_appl"].ToString()));
            cmboverduemode.SelectedIndex = cmboverduemode.Items.IndexOf(cmboverduemode.Items.FindByText(dtsdl.Rows[0]["ODPR"].ToString()));
            loadinst();
            lastDt = scon.FillDate(dtsdl.Rows[0]["last_rep_dt"].ToString()).ToString();
            txtlastreapydet.Text = lastDt.Substring(0, 10);
            txttotloan.Text = dtsdl.Rows[0]["loan_amnt"].ToString();
            txtrateofinterest.Text = dtsdl.Rows[0]["roi"].ToString();
            txtodroi.Text = dtsdl.Rows[0]["od_roi"].ToString();
            txtloanprdmon.Text = dtsdl.Rows[0]["duration"].ToString();
            txtmonperiod.Text = dtsdl.Rows[0]["mon_prd"].ToString();
            txtnoofinstt.Text = dtsdl.Rows[0]["no_of_inst"].ToString();
            txtfirstoddet.Text = dtsdl.Rows[0]["INST_ST_DATE"].ToString();
            txtinsttamt.Text = dtsdl.Rows[0]["inst_amount"].ToString();

            //find the custid details in edit swarup

            cmd.CommandText = "select loan_term from lsm where SCHEME_CODE=@scode and bank_code=@bank_code";
            cmd.Parameters.AddWithValue("@scode", dtsdl.Rows[0]["scheme_code"].ToString());
            DataTable dtrepay = scon.getdatatable(cmd);
            cmbloantype.SelectedIndex = cmbloantype.Items.IndexOf(cmbloantype.Items.FindByValue(dtrepay.Rows[0]["loan_term"].ToString()));



            //Application Details....//
            lstcl.Clear();
            cmd.CommandText = "select * from cm_sm where bank_code=@bank_code and sl_code=@slcode";
            DataTable dtpdet = scon.getdatatable(cmd);
            if (dtpdet.Rows.Count > 0)
            {
                foreach (DataRow row in dtpdet.Rows)
                {
                    Client cl = new Client();
                    cl.custid = row["cust_id"].ToString();
                    cl.name = row["name"].ToString();
                    cl.gname = row["guardian_name"].ToString();
                    cl.vill = row["vill_name"].ToString();
                    cl.cl_status = row["cl_status"].ToString() == "m" ? "Member" : "Non-Member";
                    lstcl.Add(cl);
                }
                grdappl.DataSource = lstcl;
                grdappl.DataBind();
            }  
                       
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmloanacopen.aspx");
        }

        protected void cmboverduemode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmboverduemode.SelectedValue == "AFTER LAST REPAY DATE" ||  cmboverduemode.SelectedValue == "NO OD" ||   cmboverduemode.SelectedValue == "EMI"  )
            {
                // cmbinsttapply.Text = "No";
                cmbinsttapply.SelectedIndex = 2;
                cmbinsttapply.Enabled = false;
            }
            else
            {
                cmbinsttapply.SelectedIndex = 1;
                cmbinsttapply.Enabled = true;
            }
            loadinst();

           
           

            if (cmbinsttapply.SelectedValue == "Yes")
            {
                if (cmboverduemode.SelectedValue == "1")
                {
                    int calculateinstamt = 0;
                    calculateinstamt = (scon.FillNum(txtloanprdmon.Text) / scon.FillNum(cmboverduemode.SelectedValue));
                    txtnoofinstt.Text = calculateinstamt.ToString();
                    txtinsttamt.Text = (scon.FillNum(txttotloan.Text) / scon.FillNum(txtnoofinstt.Text)).ToString();
                    datecalc();
                }

                else if (cmboverduemode.SelectedValue == "3")
                {

                    int calculateinstamt = 0;
                    calculateinstamt = (scon.FillNum(txtloanprdmon.Text) / scon.FillNum(cmboverduemode.SelectedValue));
                    txtnoofinstt.Text = calculateinstamt.ToString();
                    txtinsttamt.Text = (scon.FillNum(txttotloan.Text) / scon.FillNum(txtnoofinstt.Text)).ToString();
                    datecalc();



                }

                else if (cmboverduemode.SelectedValue == "6")
                {
                    int calculateinstamt = 0;
                    calculateinstamt = (scon.FillNum(txtloanprdmon.Text) / scon.FillNum(cmboverduemode.SelectedValue));
                    txtnoofinstt.Text = calculateinstamt.ToString();
                    txtinsttamt.Text = (scon.FillNum(txttotloan.Text) / scon.FillNum(txtnoofinstt.Text)).ToString();
                    datecalc();


                }

                else if (cmboverduemode.SelectedValue == "12")
                {
                    int calculateinstamt = 0;
                    calculateinstamt = (scon.FillNum(txtloanprdmon.Text) / scon.FillNum(cmboverduemode.SelectedValue));
                    txtnoofinstt.Text = calculateinstamt.ToString();
                    txtinsttamt.Text = (scon.FillNum(txttotloan.Text) / scon.FillNum(txtnoofinstt.Text)).ToString();
                    datecalc();
                }
                else
                {
                    txtnoofinstt.Text = "";
                    txtinsttamt.Text = "";
                }
                //if (cmboverduemode.SelectedValue == "12")
                //{
                //    int calculateinstamt = 0;
                //    calculateinstamt = (scon.FillNum(txtloanprdmon.Text) / scon.FillNum(cmboverduemode.SelectedValue));
                //    txtnoofinstt.Text = calculateinstamt.ToString();
                //    datecalc();
                //}

            }


            

        }

        protected void txttotloan_TextChanged(object sender, EventArgs e)
        {

            if (cmbinsttapply.SelectedValue == "Yes")
            {
                if (scon.FillNum(txtnoofinstt.Text) > 0)
                {
                    decimal calculateinstamt = 0;
                    calculateinstamt = (scon.FillNum(txttotloan) / scon.FillNum(txtnoofinstt.Text));
                    txtinsttamt.Text = calculateinstamt.ToString("#.##");
                }
            }
        }

        //calculate the First Od Date based on period on month swarup
        public void datecalc()
        {
            var date = txtdateofopening.Text;  // your text box date
            var d = date.Split('/');
            int day = Convert.ToInt16(d[0]);
            int month = Convert.ToInt16(d[1]);
            int year = Convert.ToInt16(d[2]);

            DateTime newdate = new DateTime(year, month, day).AddMonths(int.Parse(cmboverduemode.SelectedValue));

            txtfirstoddet.Text = newdate.ToString();
            int calculateinstamt = 0;
            calculateinstamt = (scon.FillNum(txtloanprdmon.Text) / scon.FillNum(cmboverduemode.SelectedValue));
            txtnoofinstt.Text = calculateinstamt.ToString();
        }


        public void loadinst()
        {
            txtnoofinstt.Text = "";
            txtfirstoddet.Text = "";
            txtinsttamt.Text = "";
            if (cmbinsttapply.SelectedValue == "Yes")
            {
                lblnoofinstt.Visible = true;
                txtnoofinstt.Visible = true;
                lblfirstoddet.Visible = true;
                txtfirstoddet.Visible = true;
                lblinsttamt.Visible = true;
                txtinsttamt.Visible = true;
            }
            else
            {
                lblnoofinstt.Visible = false;
                txtnoofinstt.Visible = false;
                lblfirstoddet.Visible = false;
                txtfirstoddet.Visible = false;
                lblinsttamt.Visible = false;
                txtinsttamt.Visible = false;
            }
        }

      


    }
}