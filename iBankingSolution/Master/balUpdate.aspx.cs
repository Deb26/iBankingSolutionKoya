using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Web.Configuration;
using System.IO;

namespace iBankingSolution.Master
{
    public partial class balUpdate : System.Web.UI.Page
    {

        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        SqlConnection Myconn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString);
        private object m_objOpt = System.Reflection.Missing.Value;
        OleDbConnection excelConnection;
        string excelConnectionString;
        OleDbCommand cmd;
        string Filename;
        string FilePath;
        string Sheetname;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
           
            objBO_Finance.SL_CODE = txtAcNo.Text;
            objBO_Finance.old_acno = txtOldAcNo.Text;
            objBO_Finance.NAME = txtname.Text;
            objBO_Finance.ACT_OP_CR = Convert.ToDouble(txtcredit.Text);
            objBO_Finance.ACT_OP_DR = Convert.ToDouble(txtdebit.Text);
            objBO_Finance.lf_acno = txtlpnum.Text;
            objBO_Finance.rec_int = Convert.ToDouble(txtintamt.Text);

            txtdebit_TextChanged(sender, e);
            txtcredit_TextChanged(sender, e);

            int i = objBL_Finance.getBalanceUpdate(objBO_Finance, out SQLError);

            if (i > 0)
            {
                message = "alert('Save Successfully.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
            else 
            {
                message = "alert('Save UnSuccessfully.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
            RestControl();
        }

        protected void txtAcNo_TextChanged(object sender, EventArgs e)
        {
            txtOldAcNo.Text = "";

            txtOldAcNo_TextChanged(sender, e);
            
        }

        protected void txtOldAcNo_TextChanged(object sender, EventArgs e)
        {
            //objBO_Finance.Flag = 1;

            if (txtOldAcNo.Text != "")
            {
                txtAcNo.Text = "";

                objBO_Finance.old_acno = txtOldAcNo.Text;
                objBO_Finance.SL_CODE = "";
            }
            else if (txtAcNo.Text != "")
            {
                objBO_Finance.old_acno = "";
                objBO_Finance.SL_CODE = txtAcNo.Text;
            }

            DataSet dt = objBL_Finance.getSlcodebalance(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                txtOldAcNo.Text = Convert.ToString(dt.Tables[0].Rows[0]["OLD_ACNO"]);
                txtAcNo.Text = Convert.ToString(dt.Tables[0].Rows[0]["SL_CODE"]);
                txtname.Text = Convert.ToString(dt.Tables[0].Rows[0]["name"]);
                txtcredit.Text = Convert.ToString(dt.Tables[0].Rows[0]["ACT_OP_CR"]);
                txtdebit.Text = Convert.ToString(dt.Tables[0].Rows[0]["ACT_OP_DR"]);
                txtlpnum.Text = Convert.ToString(dt.Tables[0].Rows[0]["LF_ACNO"]);
                txtintamt.Text = Convert.ToString(dt.Tables[0].Rows[0]["REC_INT"]);
            }
       

        }

        protected void txtdebit_TextChanged(object sender, EventArgs e)
        {
            if(Convert.ToDouble(txtdebit.Text) > 0)
            {
              
                txtcredit.Text = "0.00";
               
            }
        }

        protected void txtcredit_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtcredit.Text) > 0)
            {
                txtdebit.Text = "0.00";
              
            }
        }

        protected void RestControl()
        {
            txtAcNo.Text = String.Empty;
            txtOldAcNo.Text = string.Empty;
            txtcredit.Text = string.Empty;
            txtdebit.Text = string.Empty;
            txtintamt.Text = string.Empty;
            txtlpnum.Text = string.Empty;
            txtname.Text = string.Empty;

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            RestControl();
        }
        protected void buttonShow_Click(object sender, EventArgs e)
        {
            
            string ConStr = "";
            string ext = Path.GetExtension(companyUpload.FileName).ToLower();
            string path = Server.MapPath("~/UploadFiles/" + companyUpload.FileName);
            companyUpload.SaveAs(path);
            if (ext.Trim() == ".xls")
            {
                ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (ext.Trim() == ".xlsx")
            {
                ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Persist Security Info=False;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";

                //@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Persist Security Info=False;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'"
            }
            string query = "SELECT * FROM [Sheet1$]";
            OleDbConnection OLODBconn = new OleDbConnection(ConStr);
            OLODBconn.Open();
            //if (conn.State == ConnectionState.Closed)
            //{

            //}
            OleDbCommand cmd = new OleDbCommand(query, OLODBconn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            OLODBconn.Close();
            int i = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                string lblacno = Convert.ToString(row.Cells[0].Text).Trim() != "&nbsp;" ? Convert.ToString(row.Cells[0].Text).Trim() : "";
                string lblamount = Convert.ToString(row.Cells[1].Text).Trim() != "&nbsp;" ? Convert.ToString(row.Cells[1].Text).Trim() : "";

                objBO_Finance.Flag = 1;
                objBO_Finance.SL_CODE = lblacno;
                objBO_Finance.Balance = Convert.ToDouble(lblamount);

                 i = objBL_Finance.excelFileUpload(objBO_Finance, out SQLError);

            }
            
            if (i > 0)
            {
                MessageBox(this, "Record Inserted Successfully .");
            }
            else
            {
                MessageBox(this, "Something Went Wrong .");
            }
        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
    }
}