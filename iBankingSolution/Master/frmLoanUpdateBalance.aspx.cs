using BLL;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iBankingSolution.Master
{
    public partial class frmLoanUpdateBalance : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        String message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }

        }
        protected void SetFromToDate()
        {

            DataTable DtStartDt = new DataTable();
            objBO_Finance.Flag = 1;
            DateTime dtfr;
            DateTime dtTo;

            DtStartDt = objBL_Finance.GetYearStartEndDt(objBO_Finance);

            if (DtStartDt.Rows.Count > 0)
            {
                dtpkr_FormedOn.Text = Convert.ToString(DtStartDt.Rows[0]["STARTDT"]);

            }


        }
        protected void txtloanacno_TextChanged(object sender, EventArgs e)
        {

            //SetFromToDate();
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = txtloanacno.Text;

            DataSet dt = objBL_Finance.LoanbalUpdate(objBO_Finance, out SQLError);
            
            if (dt.Tables[0].Rows.Count > 0)
            {
                txtloanacno.Text = Convert.ToString(dt.Tables[0].Rows[0]["SL_CODE"]);
                //txtprincurr.Text = Convert.ToString(dt.Tables[0].Rows[0]["PRIN_CUR"]);
                txtprinoutstan.Text= Convert.ToString(dt.Tables[0].Rows[0]["PRIN_OUT"]);
                //txtlprinod.Text = Convert.ToString(dt.Tables[0].Rows[0]["PrinOverdue"]);
                txtintod.Text = Convert.ToString(dt.Tables[0].Rows[0]["intOverdue"]);
                txtintcurr.Text = Convert.ToString(dt.Tables[0].Rows[0]["intcurr"]);
                ///dtpkr_FormedOn.Text = Convert.ToString(dt.Tables[0].Rows[0]["date_upto"]);
                dtpkr_FormedOn.Text = Convert.ToDateTime(dt.Tables[0].Rows[0]["date_upto"]).ToString("dd/MM/yyyy");


                objBO_Finance.Flag = 2;
                objBO_Finance.SL_CODE = txtloanacno.Text;

                DataSet dtt = objBL_Finance.LoanbalUpdate(objBO_Finance, out SQLError);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = dtt;
                    GridView2.DataBind();
                }

            }
            

            else
            {
                message = "alert('Please enter valid SL_CODE.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.SL_CODE = txtloanacno.Text;
            objBO_Finance.PRINCUR = 0;
            objBO_Finance.prin_od = 0;
            objBO_Finance.INT_CUR = Convert.ToDouble(txtintcurr.Text);
            objBO_Finance.int_od = Convert.ToDouble(txtintod.Text);
            objBO_Finance.Prin_out = Convert.ToDouble(txtprinoutstan.Text);
            ///objBO_Finance.DATE_UPTO = Convert.ToDateTime(dtpkr_FormedOn.Text);
            string OpenDt = dtpkr_FormedOn.Text;
            DateTime OpenDt1 = DateTime.ParseExact(OpenDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.DATE_UPTO = OpenDt1;

            //string UpdateDt = txtupdatedate.Text;
            //DateTime dtpker = DateTime.ParseExact(UpdateDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //objBO_Finance.FDate = dtpker;



            objBO_Finance.FDate = System.DateTime.Now;

            int i = objBL_Finance.UpdateLoanAcctsBalance(objBO_Finance, out SQLError);

            if (i > 0)
            {
                message = "alert('Update Successfully.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);

                ResetControl();
            }
            else
            {
                message = "alert('Update UnSuccessful.')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }      
        }

        protected void ResetControl()
        {
            txtloanacno.Text = string.Empty;
            //txtprincurr.Text = string.Empty;
            txtprinoutstan.Text = string.Empty;
            txtintod.Text = string.Empty;
            txtintcurr.Text = string.Empty;
            //txtlprinod.Text = string.Empty;
            dtpkr_FormedOn.Text = string.Empty;
            GridView2.DataSource = null;
            GridView2.DataBind();
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            txtloanacno_TextChanged(sender, e);
        }
    }
}