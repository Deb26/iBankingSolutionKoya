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
using System.Configuration;
using System.Data.SqlClient;

namespace iBankingSolution.Master
{
    public partial class frmLedgerList : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;

        public string LDG_CODE { get; private set; }
        public string NOMENCLATURE { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.CUST_ID = null;

            DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
            if (dt.Rows.Count > 0)
            {

                RepCCList.DataSource = dt;
                RepCCList.DataBind();

            }
        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int iStID = int.Parse((item.FindControl("lblLedger_Code") as Label).Text);

            Response.Redirect("~/Master/frmMasterLedger.aspx?Id=" + iStID);
        }
        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            //objBO_Finance.CUST_ID = ((Button)sender).CommandArgument;
            objBO_Finance.Flag = 3;
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int iStID = int.Parse((item.FindControl("lblLedger_Code") as Label).Text);
            objBO_Finance.CUST_ID = iStID;
            int i = objBL_Finance.InsertUpdateDeleteLedgerMaster(objBO_Finance, out SQLError);
            if (i > 0)
            {

                message = "alert('Delete Successfully')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
               
                BindGrid();
            }
            else
            {
                message = "alert('Not Deleted. Root Element Exists..')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
        }

        private static DataTable GetData(string query)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        protected void cmbx_ddlsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbx_ddlsearch.SelectedIndex > 0)
            {
                txtsearchkyc.Focus();
            }
        }

        protected void txtsearchkyc_TextChanged(object sender, EventArgs e)
        {

            if (cmbx_ddlsearch.SelectedItem.Text == "LDG_CODE")
            {
                objBO_Finance.LDG_CODE= Convert.ToInt32(txtsearchkyc.Text);
                DataSet ds = new DataSet();
                ds = objBL_Finance.SearchinAccountLedger(objBO_Finance);
                RepCCList.DataSource = ds;
                RepCCList.DataBind();
            }

           

            if (cmbx_ddlsearch.SelectedItem.Text == "NOMENCLATURE")
            {
                objBO_Finance.NOMENCLATURE = txtsearchkyc.Text;
                DataSet ds = new DataSet();
                ds = objBL_Finance.SearchforAcNOMENCLATURE(objBO_Finance);
                RepCCList.DataSource = ds;
                RepCCList.DataBind();
            }

            //if (cmbx_ddlsearch.SelectedItem.Text == "LDG_CODE")
            //{
            //    Int32 LDG_CODE = Convert.ToInt32(txtsearchkyc.Text);
            //    string SqlQuery = "SELECT LDG_CODE, NOMENCLATURE, TYPE, GROUP_NAME  from  Ledger_Master LM inner join Group_Master GM ON LM.GROUPCODE = GM.GROUP_CODE WHERE LM.LDG_CODE =" + LDG_CODE;


            //    RepCCList.DataSource = GetData(SqlQuery);
            //    RepCCList.DataBind();
            //}
            //else if (cmbx_ddlsearch.SelectedItem.Text == "NOMENCLATURE")
            //{
            //    string NOMENCLATURE = txtsearchkyc.Text;
            //    string SqlQuery = "SELECT LDG_CODE, NOMENCLATURE, TYPE, GROUP_NAME  from  Ledger_Master LM inner join Group_Master GM ON LM.GROUPCODE = GM.GROUP_CODE WHERE LM.NOMENCLATURE LIKE '" + NOMENCLATURE + "%'";


            //    RepCCList.DataSource = GetData(SqlQuery);
            //    RepCCList.DataBind();
            //}
            //else if (cmbx_ddlsearch.SelectedItem.Text == "TYPE")
            //{
            //    string TYPE = txtsearchkyc.Text;
            //    string SqlQuery = "SELECT LDG_CODE, NOMENCLATURE, TYPE, GROUP_NAME  from  Ledger_Master LM inner join Group_Master GM ON LM.GROUPCODE = GM.GROUP_CODE WHERE LM.TYPE LIKE '" + TYPE + "%'";


            //    RepCCList.DataSource = GetData(SqlQuery);
            //    RepCCList.DataBind();
            //}
        }
    }
}