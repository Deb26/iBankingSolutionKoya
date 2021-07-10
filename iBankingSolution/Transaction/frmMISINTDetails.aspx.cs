using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BusinessObject;
using BLL;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace iBankingSolution.Transaction
{
    public partial class frmMISINTDetails : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                dtpkr_EntryDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dtpkr_TransferDt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
        }

        protected void GVMTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void button_Show(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 1;
            string edt = dtpkr_EntryDate.Text;
            DateTime eddt = DateTime.ParseExact(edt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.FDate = eddt;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            int i = objBL_Finance.MIDINTDT(objBO_Finance, out SQLError);

            if (i > 0)
            {
                objBO_Finance.Flag = 1;
                string Cust = dtpkr_EntryDate.Text;
                DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.TDate = Cust1;
                DataSet dSet = objBL_Finance.GETMISDETAILS(objBO_Finance, out SQLError);
                if (dSet.Tables[0].Rows.Count > 0)
                {
                    int cnt = 0;
                    foreach (var item in GVMTransaction.Rows)
                    {
                        string intgoesto = Convert.ToString(dSet.Tables[0].Rows[0]["INT_GOES_TO"]);
                        if (intgoesto == null || intgoesto == "")
                        {
                            TextBox txt_intgoesto = GVMTransaction.Rows[cnt].FindControl("txt_intgoesto") as TextBox;
                            txt_intgoesto.Enabled = true;
                        }
                        else
                        {
                            TextBox txt_intgoesto = GVMTransaction.Rows[cnt].FindControl("txt_intgoesto") as TextBox;
                            txt_intgoesto.Enabled = false;
                        }
                        cnt = cnt + 1;
                    }

                    GVMTransaction.DataSource = dSet;
                    GVMTransaction.DataBind();
                }
                else
                {
                    GVMTransaction.DataSource = null;
                    GVMTransaction.DataBind();
                }
            }

        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            int i = 0;

            foreach (var item in GVMTransaction.Rows)
            {
                CheckBox chkSelect = GVMTransaction.Rows[cnt].FindControl("chkSelect") as CheckBox;
                if (chkSelect.Checked == true)
                {
                    TextBox accountNo = GVMTransaction.Rows[cnt].FindControl("txt_accountno") as TextBox;
                    TextBox intamt = GVMTransaction.Rows[cnt].FindControl("txt_intamt") as TextBox;
                    TextBox slcodeintgoesto = GVMTransaction.Rows[cnt].FindControl("txt_intgoesto") as TextBox;

                    objBO_Finance.Flag = 1;
                    objBO_Finance.SL_CODE = accountNo.Text;
                    objBO_Finance.INT_AMNT = Convert.ToDouble(intamt.Text);
                    objBO_Finance.SB_SL_CODE = slcodeintgoesto.Text;
                    string OpenDt = dtpkr_TransferDt.Text;
                    DateTime OpenDt1 = DateTime.ParseExact(OpenDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.date_of_opening = OpenDt1;
                    objBO_Finance.EMPCODE = Convert.ToString(Session["EmpID"]);

                    i = objBL_Finance.insertupdatemisinterestdetails(objBO_Finance, out SQLError);
                }

                cnt = cnt + 1;
            }
            if (i > 0)
            {
                MessageBox(this, "Record Save Successfully . Allotted Voucher is:-" + SQLError);
            }
            else
            {
                String message = "alert('Unable to Update')";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
            }
        }
    }
}