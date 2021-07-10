using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Collections;
using System.Globalization;
using BLL.GeneralBL;
using System.Data;
using System.Data.SqlClient;
namespace iBankingSolution.Master
{
    public partial class frmDateConfiguration : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }
        }

        protected void GetData()
        {
            DataTable dt = new DataTable();
            dt = objBL_Finance.BalanceDateConfiguration(objBO_Finance);
            if (dt.Rows.Count > 0)
            {
                txt_cashinhand.Text = Convert.ToString(dt.Rows[0]["Cash_In_Hand_Date"]);
                txt_depositeaccount.Text = Convert.ToString(dt.Rows[0]["Deposite_Balance_Date"]);
                txt_finalacdate.Text= Convert.ToString(dt.Rows[0]["Final_Account_Date"]);
                txt_generalledger.Text= Convert.ToString(dt.Rows[0]["General_Ledger_Date"]);
                txt_loanac.Text= Convert.ToString(dt.Rows[0]["Loan_Account_Date"]);
                txt_cashinhand.Enabled = false;
                txt_depositeaccount.Enabled = false;
                txt_finalacdate.Enabled = false;
                txt_generalledger.Enabled = false;
                txt_loanac.Enabled = false;            
            }

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            objBO_Finance.Cash_In_Hand_Date = Convert.ToDateTime(txt_cashinhand.Text);
            objBO_Finance.Deposite_Balance_Date = Convert.ToDateTime(txt_depositeaccount.Text);
            objBO_Finance.General_Ledger_Date = Convert.ToDateTime(txt_generalledger.Text);
            objBO_Finance.Final_Account_Date = Convert.ToDateTime(txt_finalacdate.Text);
            objBO_Finance.Loan_Account_Date=Convert.ToDateTime(txt_loanac.Text);
            int i = objBL_Finance.getBalancedateConfiguration(objBO_Finance);
            if(i>0)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Save Successfully')</script>");
                RestControl();
            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Save Unsuccessfully')</script>");
                RestControl();
            }
        }

        protected void RestControl()
        {
            txt_cashinhand.Text = string.Empty;
            txt_depositeaccount.Text = string.Empty;
            txt_finalacdate.Text = string.Empty;
            txt_generalledger.Text = string.Empty;
            txt_loanac.Text = string.Empty;
        }
    }
}