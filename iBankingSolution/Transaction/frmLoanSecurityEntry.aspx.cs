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
    public partial class frmLoanSecurityEntry : System.Web.UI.Page
    {
        MyDBDataContext dbContext = new MyDBDataContext();
        //DateTime YrStartDt;
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSLCODE();
            }
        }

        protected void BindSLCODE()
        {
            try
            {

                DataTable dt = objBL_Finance.getrecordbyslcode(objBO_Finance);
                if (dt.Rows.Count > 0)
                {
                    cmbx_Slcode.DataSource = dt;
                    cmbx_Slcode.DataValueField = "SL_CODE";
                    cmbx_Slcode.DataTextField = "SL_CODE";
                    cmbx_Slcode.DataBind();
                    cmbx_Slcode.Items.Insert(0, new ListItem("--SELECT--"));
                }
            }
            catch (Exception ex)
            { string msg = ex.Message; }
            finally
            { }

        }

        //korbaname operation
        protected void setinitialrowkarba()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("KARBADATE", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("KARBANO", typeof(String)));
            dt.Columns.Add(new DataColumn("KarbaValue", typeof(decimal)));
            dt.Columns.Add(new DataColumn("KarbaValid", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("KarbaAcre", typeof(string)));
            dt.Columns.Add(new DataColumn("CreditLiValue", typeof(decimal)));
            dt.Columns.Add(new DataColumn("KarbaSees", typeof(string)));
            //dt.Columns.Add(new DataColumn("KarbaUpto", typeof(DateTime)));


            dr = dt.NewRow();

            //dt.Rows.Add(dr);


            dt.Rows.Add(null, String.Empty, 0, null, string.Empty, 0, string.Empty);

            gv_loantype.DataSource = dt;
            gv_loantype.DataBind();
            ViewState["CurrentTable"] = dt;
        }

        private void SetPreviousDatakarba()

        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)

            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)

                {

                    for (int i = 0; i < dt.Rows.Count; i++)

                    {


                        TextBox box1 = (TextBox)gv_loantype.Rows[rowIndex].Cells[1].FindControl("txtKARBADATE");

                        TextBox box2 = (TextBox)gv_loantype.Rows[rowIndex].Cells[2].FindControl("txtKARBANO");

                        TextBox box3 = (TextBox)gv_loantype.Rows[rowIndex].Cells[3].FindControl("txtKarbaValue");

                        //TextBox box3 = (TextBox)gv_loantype.Rows[rowIndex].Cells[3].FindControl("txtKarbaUpto");

                        TextBox box4 = (TextBox)gv_loantype.Rows[rowIndex].Cells[4].FindControl("txtKarbaValid");

                        TextBox box5 = (TextBox)gv_loantype.Rows[rowIndex].Cells[5].FindControl("txtKarbaAcre");

                        TextBox box6 = (TextBox)gv_loantype.Rows[rowIndex].Cells[6].FindControl("txtcreditlivalue");

                        TextBox box7 = (TextBox)gv_loantype.Rows[rowIndex].Cells[7].FindControl("txtKarbaSees");


                        box1.Text = dt.Rows[i]["KARBADATE"].ToString();

                        box2.Text = dt.Rows[i]["KARBANO"].ToString();

                        box3.Text = dt.Rows[i]["KarbaValue"].ToString();

                        box4.Text = dt.Rows[i]["KarbaValid"].ToString();

                        box5.Text = dt.Rows[i]["KarbaAcre"].ToString();

                        box6.Text = dt.Rows[i]["CreditLiValue"].ToString();

                        box7.Text = dt.Rows[i]["KarbaSees"].ToString();

                        rowIndex++;

                    }

                }

            }

        }


        private void AddNewRowToGridKarba()

        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)

                {

                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

                    {

                        //extract the TextBox values


                        TextBox box1 = (TextBox)gv_loantype.Rows[rowIndex].Cells[1].FindControl("txtKARBADATE");

                        TextBox box2 = (TextBox)gv_loantype.Rows[rowIndex].Cells[2].FindControl("txtKARBANO");

                        //TextBox box3 = (TextBox)gv_loantype.Rows[rowIndex].Cells[3].FindControl("txtKarbaUpto");

                        TextBox box3 = (TextBox)gv_loantype.Rows[rowIndex].Cells[3].FindControl("txtKarbaValue");

                        TextBox box4 = (TextBox)gv_loantype.Rows[rowIndex].Cells[4].FindControl("txtKarbaValid");

                        TextBox box5 = (TextBox)gv_loantype.Rows[rowIndex].Cells[5].FindControl("txtKarbaAcre");

                        TextBox box6 = (TextBox)gv_loantype.Rows[rowIndex].Cells[6].FindControl("txtcreditlivalue");

                        TextBox box7 = (TextBox)gv_loantype.Rows[rowIndex].Cells[7].FindControl("txtKarbaSees");

                        drCurrentRow = dtCurrentTable.NewRow();

                        //drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["KARBADATE"] = string.IsNullOrEmpty(box1.Text) ? System.DateTime.Now : Convert.ToDateTime(box1.Text);

                        dtCurrentTable.Rows[i - 1]["KARBANO"] = box2.Text;

                        dtCurrentTable.Rows[i - 1]["KarbaValue"] = string.IsNullOrEmpty(box3.Text) ? 0 : Convert.ToDecimal(box3.Text);

                        dtCurrentTable.Rows[i - 1]["KarbaValid"] = string.IsNullOrEmpty(box4.Text) ? System.DateTime.Now : Convert.ToDateTime(box4.Text);

                        //dtCurrentTable.Rows[i - 1]["KarbaUpto"] = string.IsNullOrEmpty(box3.Text) ? System.DateTime.Now : Convert.ToDateTime(box3.Text);

                        dtCurrentTable.Rows[i - 1]["KarbaAcre"] = box5.Text;



                        dtCurrentTable.Rows[i - 1]["CreditLiValue"] = string.IsNullOrEmpty(box6.Text) ? 0 : Convert.ToDecimal(box6.Text);

                        dtCurrentTable.Rows[i - 1]["KarbaSees"] = box7.Text;
                        rowIndex++;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;

                    gv_loantype.DataSource = dtCurrentTable;

                    gv_loantype.DataBind();

                }
            }
            else

            {

                Response.Write("ViewState is null");

            }
            //Set Previous Data on Postbacks

            SetPreviousDatakarba();

        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGridKarba();
        }

        //DropDownList list operation

        protected void ddlloantype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlloantype.SelectedItem.Text == "KARBARNAMA")
            {
                setinitialrowkarba();
                KDEpanel.Visible = true;
                FixDepoPanel.Visible = false;
                DCPanel.Visible = false;
                RDPanel.Visible = false;
                MisPanel.Visible = false;
                DDPanel.Visible = false;
              

            }
            else
            {
                KDEpanel.Visible = false;
                FixDepoPanel.Visible = false;
                DCPanel.Visible = false;
                RDPanel.Visible = false;
                MisPanel.Visible = false;
                DDPanel.Visible = false;
            }

            if (ddlloantype.SelectedItem.Text == "LAND AND BUILDING DETAILS")
            {
                setinitialrowlandbuilding();
                LBDPanel.Visible = true;
                FixDepoPanel.Visible = false;
                DCPanel.Visible = false;
                RDPanel.Visible = false;
                MisPanel.Visible = false;
                DDPanel.Visible = false;
            }
            else
            {
                FixDepoPanel.Visible = false;
                DCPanel.Visible = false;
                RDPanel.Visible = false;
                MisPanel.Visible = false;
                DDPanel.Visible = false;
                LBDPanel.Visible = false;
            }

            if (ddlloantype.SelectedItem.Text == "LIC")
            {
                setinitialrowLicGis();
                LICGICPanel.Visible = true;
                FixDepoPanel.Visible = false;
                DCPanel.Visible = false;
                RDPanel.Visible = false;
                MisPanel.Visible = false;
                DDPanel.Visible = false;
            }
            else
            {
                LICGICPanel.Visible = false;
                FixDepoPanel.Visible = false;
                DCPanel.Visible = false;
                RDPanel.Visible = false;
                MisPanel.Visible = false;
                DDPanel.Visible = false;
            }

            if (ddlloantype.SelectedItem.Text == "HYPOTHICATION")
            {
                setinitialrowHypoth();
                HypothPanel.Visible = true;
                FixDepoPanel.Visible = false;
                DCPanel.Visible = false;
                RDPanel.Visible = false;
                MisPanel.Visible = false;
                DDPanel.Visible = false;
            }
            else
            {
                HypothPanel.Visible = false;
                FixDepoPanel.Visible = false;
                DCPanel.Visible = false;
                RDPanel.Visible = false;
                MisPanel.Visible = false;
                DDPanel.Visible = false;
            }

            if (ddlloantype.SelectedItem.Text == "KVP/NSC/LIC AND OTHER BONDS")
            {
                setinitialrowKvpNsc();
                KvpNscPanel.Visible = true;
                FixDepoPanel.Visible = false;
                DCPanel.Visible = false;
                RDPanel.Visible = false;
                MisPanel.Visible = false;
                DDPanel.Visible = false;
            }
            else
            {
                KvpNscPanel.Visible = false;
                FixDepoPanel.Visible = false;
                DCPanel.Visible = false;
                RDPanel.Visible = false;
                MisPanel.Visible = false;
                DDPanel.Visible = false;
            }



            if (ddlloantype.SelectedItem.Text == "FIXED DEPOSITS" || ddlloantype.SelectedItem.Text == "DEPOSITS CERTIFICATE" || ddlloantype.SelectedItem.Text == "RECURRING DEPOSITS" || ddlloantype.SelectedItem.Text == "MIS DEPOSITS" || ddlloantype.SelectedItem.Text == "DAILY DEPOSITS")
            {
                //setinitialrowFixDepo();

                objBO_Finance.TYPE = ddlloantype.SelectedItem.Text;
                DataTable dt = new DataTable();
                dt = objBL_Finance.LoanSecurityDepoDaily(objBO_Finance);

                if (ddlloantype.SelectedItem.Text == "FIXED DEPOSITS")

                {
                    FixDepoPanel.Visible = true;
                    DCPanel.Visible = false;
                    RDPanel.Visible = false;
                    MisPanel.Visible = false;
                    DDPanel.Visible = false;
                    gv_FixDepo.DataSource = dt;
                    gv_FixDepo.DataBind();
                }


                else if (ddlloantype.SelectedItem.Text == "DEPOSITS CERTIFICATE")
                {
                    //setinitialrowDepoCert();
                    DCPanel.Visible = true;
                    RDPanel.Visible = false;
                    MisPanel.Visible = false;
                    DDPanel.Visible = false;
                    FixDepoPanel.Visible = false;
                    gv_DepoCert.DataSource = dt;
                    gv_DepoCert.DataBind();
                }

                else if (ddlloantype.SelectedItem.Text == "RECURRING DEPOSITS")
                {
                    //setinitialrowRecurDepo();
                    RDPanel.Visible = true;
                    MisPanel.Visible = false;
                    DDPanel.Visible = false;
                    FixDepoPanel.Visible = false;
                    DCPanel.Visible = false;
                    gv_recurdepo.DataSource = dt;
                    gv_recurdepo.DataBind();
                }

                else if (ddlloantype.SelectedItem.Text == "MIS DEPOSITS")
                {
                    //setinitialrowMisDepo();
                    MisPanel.Visible = true;
                    DDPanel.Visible = false;
                    FixDepoPanel.Visible = false;
                    DCPanel.Visible = false;
                    RDPanel.Visible = false;
                    gv_MisDepo.DataSource = dt;
                    gv_MisDepo.DataBind();
                }
                else if (ddlloantype.SelectedItem.Text == "DAILY DEPOSITS")
                {
                    //setinitialrowMisDepo();
                    DDPanel.Visible = true;
                    FixDepoPanel.Visible = false;
                    DCPanel.Visible = false;
                    RDPanel.Visible = false;
                    MisPanel.Visible = false;
                    gv_DD.DataSource = dt;
                    gv_DD.DataBind();
                }



            }
        }

        //Start of land building operation

        protected void setinitialrowlandbuilding()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("MujaNo", typeof(String)));
            dt.Columns.Add(new DataColumn("GlNo", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("DagNo", typeof(decimal)));
            dt.Columns.Add(new DataColumn("KhataNo", typeof(string)));
            dt.Columns.Add(new DataColumn("TotalLand", typeof(decimal)));
            dt.Columns.Add(new DataColumn("LandValue", typeof(decimal)));


            dr = dt.NewRow();

            //dt.Rows.Add(dr);



            dt.Rows.Add(string.Empty, null, 0, string.Empty, 0, 0);
            //dt.Rows.Add(string.Empty, System.DateTime.Now.ToShortDateString(),0,string.Empty,0,0);

            gv_LBD.DataSource = dt;
            gv_LBD.DataBind();
            ViewState["CurrentTable"] = dt;


        }

        private void SetPreviousDatalandbuilding()

        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)

            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)

                {

                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        TextBox box1 = (TextBox)gv_LBD.Rows[rowIndex].Cells[1].FindControl("txtMujaNo");

                        TextBox box2 = (TextBox)gv_LBD.Rows[rowIndex].Cells[2].FindControl("txtGlNo");

                        TextBox box3 = (TextBox)gv_LBD.Rows[rowIndex].Cells[3].FindControl("txtDagNo");

                        TextBox box4 = (TextBox)gv_LBD.Rows[rowIndex].Cells[4].FindControl("txtKhataNo");

                        TextBox box5 = (TextBox)gv_LBD.Rows[rowIndex].Cells[5].FindControl("txtTotalLand");

                        TextBox box6 = (TextBox)gv_LBD.Rows[rowIndex].Cells[6].FindControl("txtLandValue");


                        box1.Text = dt.Rows[i]["MujaNo"].ToString();

                        box2.Text = dt.Rows[i]["GlNo"].ToString();

                        box3.Text = dt.Rows[i]["DagNo"].ToString();

                        box4.Text = dt.Rows[i]["KhataNo"].ToString();

                        box5.Text = dt.Rows[i]["TotalLand"].ToString();

                        box6.Text = dt.Rows[i]["LandValue"].ToString();

                        rowIndex++;

                    }

                }

            }

        }

        private void AddNewRowToGridlandbuilding()

        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)

                {

                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

                    {

                        //extract the TextBox values
                        TextBox box1 = (TextBox)gv_LBD.Rows[rowIndex].Cells[1].FindControl("txtMujaNo");

                        TextBox box2 = (TextBox)gv_LBD.Rows[rowIndex].Cells[2].FindControl("txtGlNo");

                        TextBox box3 = (TextBox)gv_LBD.Rows[rowIndex].Cells[3].FindControl("txtDagNo");

                        TextBox box4 = (TextBox)gv_LBD.Rows[rowIndex].Cells[4].FindControl("txtKhataNo");

                        TextBox box5 = (TextBox)gv_LBD.Rows[rowIndex].Cells[5].FindControl("txtTotalLand");

                        TextBox box6 = (TextBox)gv_LBD.Rows[rowIndex].Cells[6].FindControl("txtLandValue");


                        drCurrentRow = dtCurrentTable.NewRow();

                        //drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["MujaNo"] = box1.Text;

                        dtCurrentTable.Rows[i - 1]["GlNo"] = string.IsNullOrEmpty(box2.Text) ? System.DateTime.Now : Convert.ToDateTime(box2.Text);

                        dtCurrentTable.Rows[i - 1]["DagNo"] = string.IsNullOrEmpty(box3.Text) ? 0 : Convert.ToDecimal(box3.Text);

                        dtCurrentTable.Rows[i - 1]["KhataNo"] = box4.Text;

                        dtCurrentTable.Rows[i - 1]["TotalLand"] = string.IsNullOrEmpty(box5.Text) ? 0 : Convert.ToDecimal(box5.Text);

                        dtCurrentTable.Rows[i - 1]["LandValue"] = string.IsNullOrEmpty(box6.Text) ? 0 : Convert.ToDecimal(box6.Text);
                        ;


                        rowIndex++;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;

                    gv_LBD.DataSource = dtCurrentTable;

                    gv_LBD.DataBind();

                }
            }
            else

            {

                Response.Write("ViewState is null");

            }
            //Set Previous Data on Postbacks

            SetPreviousDatalandbuilding();

        }

        protected void ButtonAddLBD_Click(object sender, EventArgs e)
        {
            AddNewRowToGridlandbuilding();
        }

        //start of LicGic Operation

        protected void setinitialrowLicGis()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("PolNo", typeof(string)));
            dt.Columns.Add(new DataColumn("SamAssu", typeof(string)));
            dt.Columns.Add(new DataColumn("SUMValue", typeof(decimal)));
            dt.Columns.Add(new DataColumn("AssineeDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("IssueOfice", typeof(string)));



            dr = dt.NewRow();

            //dt.Rows.Add(dr);

            //for (int i = 0; i < 1; i++)
            //{

            dt.Rows.Add(String.Empty, string.Empty, 0, null, string.Empty);
            //dt.Rows.Add(String.Empty,string.Empty,0, System.DateTime.Now.ToShortDateString(), string.Empty);
            //}
            gv_LicGic.DataSource = dt;
            gv_LicGic.DataBind();
            ViewState["CurrentTable"] = dt;


        }

        private void SetPreviousDataLicGis()

        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)

            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)

                {

                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        TextBox box1 = (TextBox)gv_LicGic.Rows[rowIndex].Cells[1].FindControl("txtPolNo");

                        TextBox box2 = (TextBox)gv_LicGic.Rows[rowIndex].Cells[2].FindControl("txtSamAssu");

                        TextBox box3 = (TextBox)gv_LicGic.Rows[rowIndex].Cells[3].FindControl("txtSUMValue");

                        TextBox box4 = (TextBox)gv_LicGic.Rows[rowIndex].Cells[4].FindControl("txtAssineeDate");

                        TextBox box5 = (TextBox)gv_LicGic.Rows[rowIndex].Cells[5].FindControl("txtIssueOfice");




                        box1.Text = dt.Rows[i]["PolNo"].ToString();

                        box2.Text = dt.Rows[i]["SamAssu"].ToString();

                        box3.Text = dt.Rows[i]["SUMValue"].ToString();

                        box4.Text = dt.Rows[i]["AssineeDate"].ToString();

                        box5.Text = dt.Rows[i]["IssueOfice"].ToString();


                        rowIndex++;

                    }

                }

            }

        }

        private void AddNewRowToGridLicGis()

        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)

                {

                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

                    {

                        //extract the TextBox values
                        TextBox box1 = (TextBox)gv_LicGic.Rows[rowIndex].Cells[1].FindControl("txtPolNo");

                        TextBox box2 = (TextBox)gv_LicGic.Rows[rowIndex].Cells[2].FindControl("txtSamAssu");

                        TextBox box3 = (TextBox)gv_LicGic.Rows[rowIndex].Cells[3].FindControl("txtSUMValue");

                        TextBox box4 = (TextBox)gv_LicGic.Rows[rowIndex].Cells[4].FindControl("txtAssineeDate");

                        TextBox box5 = (TextBox)gv_LicGic.Rows[rowIndex].Cells[5].FindControl("txtIssueOfice");





                        drCurrentRow = dtCurrentTable.NewRow();

                        //drCurrentRow["RowNumber"] = i + 1;
                        // string.IsNullOrEmpty(box5.Text) ? 0 : Convert.ToDecimal(box5.Text);

                        //string.IsNullOrEmpty(box6.Text) ? System.DateTime.Now : Convert.ToDateTime(box6.Text);

                        dtCurrentTable.Rows[i - 1]["PolNo"] = box1.Text;

                        dtCurrentTable.Rows[i - 1]["SamAssu"] = box2.Text;

                        dtCurrentTable.Rows[i - 1]["SUMValue"] = string.IsNullOrEmpty(box3.Text) ? 0 : Convert.ToDecimal(box3.Text);

                        dtCurrentTable.Rows[i - 1]["AssineeDate"] = string.IsNullOrEmpty(box4.Text) ? System.DateTime.Now : Convert.ToDateTime(box4.Text);

                        dtCurrentTable.Rows[i - 1]["IssueOfice"] = box5.Text;

                        rowIndex++;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;

                    gv_LicGic.DataSource = dtCurrentTable;

                    gv_LicGic.DataBind();

                }
            }
            else

            {

                Response.Write("ViewState is null");

            }
            //Set Previous Data on Postbacks

            SetPreviousDataLicGis();

        }

        protected void ButtonAddLicGis_Click(object sender, EventArgs e)
        {
            AddNewRowToGridLicGis();
        }


        //start of Hypothesis operation

        protected void setinitialrowHypoth()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Perticular", typeof(string)));
            dt.Columns.Add(new DataColumn("VehicleNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ModelNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Cheseno", typeof(string)));
            dt.Columns.Add(new DataColumn("ValueHyp", typeof(decimal)));



            dr = dt.NewRow();

            //dt.Rows.Add(dr);

            //for (int i = 0; i < 1; i++)
            //{


            dt.Rows.Add(string.Empty, string.Empty, string.Empty, string.Empty, 0);
            //}
            gv_Hypoth.DataSource = dt;
            gv_Hypoth.DataBind();
            ViewState["CurrentTable"] = dt;


        }

        private void SetPreviousDataHypoth()

        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)

            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)

                {

                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        TextBox box1 = (TextBox)gv_Hypoth.Rows[rowIndex].Cells[1].FindControl("txtPerticular");

                        TextBox box2 = (TextBox)gv_Hypoth.Rows[rowIndex].Cells[2].FindControl("txtVehicleNo");

                        TextBox box3 = (TextBox)gv_Hypoth.Rows[rowIndex].Cells[3].FindControl("txtModelNo");

                        TextBox box4 = (TextBox)gv_Hypoth.Rows[rowIndex].Cells[4].FindControl("txtCheseno");

                        TextBox box5 = (TextBox)gv_Hypoth.Rows[rowIndex].Cells[5].FindControl("txtValueHyp");




                        box1.Text = dt.Rows[i]["Perticular"].ToString();

                        box2.Text = dt.Rows[i]["VehicleNo"].ToString();

                        box3.Text = dt.Rows[i]["ModelNo"].ToString();

                        box4.Text = dt.Rows[i]["Cheseno"].ToString();

                        box5.Text = dt.Rows[i]["ValueHyp"].ToString();


                        rowIndex++;

                    }

                }

            }

        }

        private void AddNewRowToGridHypoth()

        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)

                {

                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

                    {

                        //extract the TextBox values
                        TextBox box1 = (TextBox)gv_Hypoth.Rows[rowIndex].Cells[1].FindControl("txtPerticular");

                        TextBox box2 = (TextBox)gv_Hypoth.Rows[rowIndex].Cells[2].FindControl("txtVehicleNo");

                        TextBox box3 = (TextBox)gv_Hypoth.Rows[rowIndex].Cells[3].FindControl("txtModelNo");

                        TextBox box4 = (TextBox)gv_Hypoth.Rows[rowIndex].Cells[4].FindControl("txtCheseno");

                        TextBox box5 = (TextBox)gv_Hypoth.Rows[rowIndex].Cells[5].FindControl("txtValueHyp");





                        drCurrentRow = dtCurrentTable.NewRow();

                        //drCurrentRow["RowNumber"] = i + 1;


                        dtCurrentTable.Rows[i - 1]["Perticular"] = box1.Text;

                        dtCurrentTable.Rows[i - 1]["VehicleNo"] = box2.Text;

                        dtCurrentTable.Rows[i - 1]["ModelNo"] = box3.Text;

                        dtCurrentTable.Rows[i - 1]["Cheseno"] = box4.Text;

                        dtCurrentTable.Rows[i - 1]["ValueHyp"] = string.IsNullOrEmpty(box5.Text) ? 0 : Convert.ToDecimal(box5.Text);

                        rowIndex++;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;

                    gv_Hypoth.DataSource = dtCurrentTable;

                    gv_Hypoth.DataBind();

                }
            }
            else

            {

                Response.Write("ViewState is null");

            }
            //Set Previous Data on Postbacks

            SetPreviousDataHypoth();

        }

        protected void ButtonAddHypoth_Click(object sender, EventArgs e)
        {
            AddNewRowToGridHypoth();
        }

        //Start of KvpNsc Operation
        protected void setinitialrowKvpNsc()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Typee", typeof(string)));
            dt.Columns.Add(new DataColumn("CertNo", typeof(string)));
            dt.Columns.Add(new DataColumn("IssueDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("IssueOffice", typeof(string)));
            dt.Columns.Add(new DataColumn("MatValue", typeof(decimal)));
            dt.Columns.Add(new DataColumn("MatDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FaceValue", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PledgeDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Remarkss", typeof(string)));


            dr = dt.NewRow();

            //dt.Rows.Add(dr);

            //for (int i = 0; i < 1; i++)
            //{

            dt.Rows.Add(string.Empty, string.Empty, null, string.Empty, 0, null, 0, null, string.Empty);
            //dt.Rows.Add(string.Empty, string.Empty, System.DateTime.Now.ToShortDateString(), string.Empty,0, System.DateTime.Now.ToShortDateString(),0, System.DateTime.Now.ToShortDateString(), string.Empty);
            //}
            gv_KvpNsc.DataSource = dt;
            gv_KvpNsc.DataBind();
            ViewState["CurrentTable"] = dt;


        }

        private void SetPreviousDataKvpNsc()

        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)

            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)

                {

                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        TextBox box1 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[1].FindControl("txtTypee");

                        TextBox box2 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[2].FindControl("txtCertNo");

                        TextBox box3 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[3].FindControl("txtIssueDate");

                        TextBox box4 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[4].FindControl("txtIssueOffice");

                        TextBox box5 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[5].FindControl("txtMatValue");

                        TextBox box6 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[6].FindControl("txtMatDate");

                        TextBox box7 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[7].FindControl("txtFaceValue");

                        TextBox box8 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[8].FindControl("txtPledgeDate");

                        TextBox box9 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[9].FindControl("txtRemarkss");




                        box1.Text = dt.Rows[i]["Typee"].ToString();

                        box2.Text = dt.Rows[i]["CertNo"].ToString();

                        box3.Text = dt.Rows[i]["IssueDate"].ToString();

                        box4.Text = dt.Rows[i]["IssueOffice"].ToString();

                        box5.Text = dt.Rows[i]["MatValue"].ToString();

                        box6.Text = dt.Rows[i]["MatDate"].ToString();

                        box7.Text = dt.Rows[i]["FaceValue"].ToString();

                        box8.Text = dt.Rows[i]["PledgeDate"].ToString();

                        box9.Text = dt.Rows[i]["Remarkss"].ToString();


                        rowIndex++;

                    }

                }

            }

        }

        private void AddNewRowToGridKvpNsc()

        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)

                {

                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

                    {

                        //extract the TextBox values
                        TextBox box1 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[1].FindControl("txtTypee");

                        TextBox box2 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[2].FindControl("txtCertNo");

                        TextBox box3 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[3].FindControl("txtIssueDate");

                        TextBox box4 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[4].FindControl("txtIssueOffice");

                        TextBox box5 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[5].FindControl("txtMatValue");

                        TextBox box6 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[6].FindControl("txtMatDate");

                        TextBox box7 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[7].FindControl("txtFaceValue");

                        TextBox box8 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[8].FindControl("txtPledgeDate");

                        TextBox box9 = (TextBox)gv_KvpNsc.Rows[rowIndex].Cells[9].FindControl("txtRemarkss");


                        drCurrentRow = dtCurrentTable.NewRow();

                        //drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Typee"] = box1.Text;

                        dtCurrentTable.Rows[i - 1]["CertNo"] = box2.Text;

                        dtCurrentTable.Rows[i - 1]["IssueDate"] = string.IsNullOrEmpty(box3.Text) ? System.DateTime.Now : Convert.ToDateTime(box3.Text);

                        dtCurrentTable.Rows[i - 1]["IssueOffice"] = box4.Text;

                        dtCurrentTable.Rows[i - 1]["MatValue"] = string.IsNullOrEmpty(box5.Text) ? 0 : Convert.ToDecimal(box5.Text);

                        dtCurrentTable.Rows[i - 1]["MatDate"] = string.IsNullOrEmpty(box6.Text) ? System.DateTime.Now : Convert.ToDateTime(box6.Text);

                        dtCurrentTable.Rows[i - 1]["FaceValue"] = string.IsNullOrEmpty(box7.Text) ? 0 : Convert.ToDecimal(box7.Text);

                        dtCurrentTable.Rows[i - 1]["PledgeDate"] = string.IsNullOrEmpty(box8.Text) ? System.DateTime.Now : Convert.ToDateTime(box8.Text);

                        dtCurrentTable.Rows[i - 1]["Remarkss"] = box9.Text;

                        rowIndex++;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;

                    gv_KvpNsc.DataSource = dtCurrentTable;

                    gv_KvpNsc.DataBind();

                }
            }
            else

            {

                Response.Write("ViewState is null");

            }
            //Set Previous Data on Postbacks

            SetPreviousDataKvpNsc();

        }

        protected void ButtonAddKvpNsc_Click(object sender, EventArgs e)
        {
            AddNewRowToGridKvpNsc();
        }


         

        protected void ButtonAddFixDepo_Click(object sender, EventArgs e)
        {
            //AddNewRowToGridFixDepo();
        }

         

        protected void ButtonAddDepoCert_Click(object sender, EventArgs e)
        {
            // AddNewRowToGridDepoCert();
        }

         

        protected void ButtonAddRecurdepo_Click(object sender, EventArgs e)
        {
            //AddNewRowToGridRecurDepo();
        }

         

        protected void ButtonAddMisDepo_Click(object sender, EventArgs e)
        {
            //AddNewRowToGridMisDepo();
        }

         

        protected void DeleteRowHandler(object sender, CommandEventArgs e)
        {

            if (ViewState["CurrentTable"] != null)
            {
                GridViewRow row = ((GridViewRow)((LinkButton)sender).Parent.Parent);
                DataTable dt = new DataTable();
                DataRow dr = null;
                //dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("DailyAccNo", typeof(decimal)));
                dt.Columns.Add(new DataColumn("DepAmt", typeof(decimal)));
                dt.Columns.Add(new DataColumn("MatDate", typeof(DateTime)));
                for (int i = 0; i < gv_DD.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[1] = ((TextBox)gv_DD.Rows[i].Cells[1].FindControl("txtDailyAccNo")).Text;
                    dr[2] = ((TextBox)gv_DD.Rows[i].Cells[2].FindControl("txtDepAmt")).Text;
                    dr[3] = ((TextBox)gv_DD.Rows[i].Cells[3].FindControl("txtMatDate")).Text;
                    dt.Rows.Add(dr);
                }
                dt.Rows.RemoveAt(row.RowIndex);
                ViewState["CurrentTable"] = dt;
                gv_DD.DataSource = dt;
                gv_DD.DataBind();
            }
        }

        protected void ButtonAddDailyDepo_Click(object sender, EventArgs e)
        {
            //AddNewRowToGridDailyDepo();
        }

        protected void btnsubmit1_Click(object sender, EventArgs e)
        {
            int i = 0;
            int result = 0;

            //KARBARNAMA
            if (ddlloantype.SelectedItem.Text == "KARBARNAMA")
            {
                foreach (GridViewRow row in gv_loantype.Rows)
                {


                    TextBox txtKARBANO = (TextBox)row.FindControl("txtKARBANO");
                    TextBox txtKARBADATE = (TextBox)row.FindControl("txtKARBADATE");
                    //TextBox txtKarbaUpto = (TextBox)row.FindControl("txtKarbaUpto");

                    TextBox txtKarbaAcre = (TextBox)row.FindControl("txtKarbaAcre");
                    TextBox txtKarbaValue = (TextBox)row.FindControl("txtKarbaValue");
                    TextBox txtKarbaValid = (TextBox)row.FindControl("txtKarbaValid");

                    TextBox txtcreditlivalue = (TextBox)row.FindControl("txtcreditlivalue");
                    TextBox txtKarbaSees = (TextBox)row.FindControl("txtKarbaSees");


                    //LAND AND BUILDING DETAILS




                    objBO_Finance.TYPE = ddlloantype.SelectedItem.Text != "" ? ddlloantype.SelectedItem.Text : "";
                    objBO_Finance.SL_CODE = cmbx_Slcode.Text != "" ? Convert.ToInt32(cmbx_Slcode.Text) : 0;



                    objBO_Finance.K_NO = txtKARBANO.Text != "" ? Convert.ToDecimal(txtKARBANO.Text) : 0;

                    string datedb = txtKARBADATE.Text;
                    DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.K_DATE = timedb;


                    objBO_Finance.K_ACRE = txtKarbaAcre.Text != "" ? txtKarbaAcre.Text : "";
                    objBO_Finance.K_VALUE = txtKarbaValue.Text != "" ? Convert.ToDecimal(txtKarbaValue.Text) : 0;
                    //objBO_Finance.K_VALID = Convert.ToDateTime(txtKarbaValid.Text);
                    string datedb1 = txtKarbaValid.Text;
                    DateTime timedb1 = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.K_VALID = timedb1;

                    objBO_Finance.CREDIT_LIMIT_VALUE = txtcreditlivalue.Text != "" ? Convert.ToDecimal(txtcreditlivalue.Text) : 0;
                    objBO_Finance.K_SEES = txtKarbaSees.Text != "" ? txtKarbaSees.Text : "";




                    result = objBL_Finance.insertLoanSecurityDetails(objBO_Finance, out SQLError);


                }

                i = i + 1;



                if (result > 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Kabar Save Successfully')</script>");

                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Kabar Unable to Save')</script>");
                }
            }


            if (ddlloantype.SelectedItem.Text == "LAND AND BUILDING DETAILS")
            {
                foreach (GridViewRow row in gv_LBD.Rows)
                {

                    TextBox txtMujaNo = (TextBox)row.FindControl("txtMujaNo");

                    TextBox txtGlNo = (TextBox)row.FindControl("txtGlNo");

                    TextBox txtDagNo = (TextBox)row.FindControl("txtDagNo");

                    TextBox txtKhataNo = (TextBox)row.FindControl("txtKhataNo");

                    TextBox txtTotalLand = (TextBox)row.FindControl("txtTotalLand");

                    TextBox txtLandValue = (TextBox)row.FindControl("txtLandValue");

                    objBO_Finance.TYPE = ddlloantype.SelectedItem.Text != "" ? ddlloantype.SelectedItem.Text : "";
                    objBO_Finance.SL_CODE = cmbx_Slcode.Text != "" ? Convert.ToInt32(cmbx_Slcode.Text) : 0;

                    objBO_Finance.MUJA_NO = txtMujaNo.Text != "" ? txtMujaNo.Text : "";
                    objBO_Finance.GL_NO = txtGlNo.Text != "" ? txtGlNo.Text : "";
                    objBO_Finance.DAG_NO = txtDagNo.Text != "" ? txtDagNo.Text : "";
                    objBO_Finance.KH_NO = txtKhataNo.Text != "" ? txtKhataNo.Text : "";
                    objBO_Finance.TOTAL_LAND = txtTotalLand.Text != "" ? Convert.ToDecimal(txtTotalLand.Text) : 0;
                    objBO_Finance.VALUE_OF_LAND = txtLandValue.Text != "" ? Convert.ToDecimal(txtLandValue.Text) : 0;

                    result = objBL_Finance.insertLoanSecurityLandBuilding(objBO_Finance, out SQLError);

                }
                i = i + 1;



                if (result > 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Land Save Successfully')</script>");

                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Land Unable to Save')</script>");
                }

            }


            //LIC
            if (ddlloantype.SelectedItem.Text == "LIC")
            {
                foreach (GridViewRow row in gv_LicGic.Rows)
                {
                    TextBox txtPolNo = (TextBox)row.FindControl("txtPolNo");

                    TextBox txtSamAssu = (TextBox)row.FindControl("txtSamAssu");

                    TextBox txtSUMValue = (TextBox)row.FindControl("txtSUMValue");

                    TextBox txtAssineeDate = (TextBox)row.FindControl("txtAssineeDate");

                    TextBox txtIssueOfice = (TextBox)row.FindControl("txtIssueOfice");


                    objBO_Finance.TYPE = ddlloantype.SelectedItem.Text != "" ? ddlloantype.SelectedItem.Text : "";
                    objBO_Finance.SL_CODE = cmbx_Slcode.Text != "" ? Convert.ToInt32(cmbx_Slcode.Text) : 0;

                    objBO_Finance.POL_NO = txtPolNo.Text != "" ? txtPolNo.Text : "";
                    objBO_Finance.SUM_ASSU = txtSamAssu.Text != "" ? txtSamAssu.Text : "";
                    objBO_Finance.SUM_VALUE = txtSUMValue.Text != "" ? Convert.ToDecimal(txtSUMValue.Text) : 0;
                    //objBO_Finance.ASSINEE_DATE=txtAssineeDate.Text


                    string datedb2 = txtAssineeDate.Text;
                    DateTime timedb2 = DateTime.ParseExact(datedb2, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.ASSINEE_DATE = timedb2;

                    objBO_Finance.ISS_OFF = txtIssueOfice.Text != "" ? txtIssueOfice.Text : "";

                    result = objBL_Finance.insertLoanSecurityLIC(objBO_Finance, out SQLError);
                }
                i = i + 1;

                if (result > 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('LIC Save Successfully')</script>");

                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('LIC Unable to Save')</script>");
                }
            }
            //HYPOTHICATION
            if (ddlloantype.SelectedItem.Text == "HYPOTHICATION")
            {
                foreach (GridViewRow row in gv_Hypoth.Rows)
                {
                    TextBox txtPerticular = (TextBox)row.FindControl("txtPerticular");

                    TextBox txtVehicleNo = (TextBox)row.FindControl("txtVehicleNo");

                    TextBox txtModelNo = (TextBox)row.FindControl("txtModelNo");

                    TextBox txtCheseno = (TextBox)row.FindControl("txtCheseno");

                    TextBox txtValueHyp = (TextBox)row.FindControl("txtValueHyp");


                    objBO_Finance.TYPE = ddlloantype.SelectedItem.Text != "" ? ddlloantype.SelectedItem.Text : "";
                    objBO_Finance.SL_CODE = cmbx_Slcode.Text != "" ? Convert.ToInt32(cmbx_Slcode.Text) : 0;

                    objBO_Finance.PERTI = txtPerticular.Text != "" ? txtPerticular.Text : "";
                    objBO_Finance.VEH_NO = txtVehicleNo.Text != "" ? txtVehicleNo.Text : "";
                    objBO_Finance.MODEL_NO = txtModelNo.Text != "" ? txtModelNo.Text : "";
                    objBO_Finance.CHASE_NO = txtCheseno.Text != "" ? txtCheseno.Text : "";
                    objBO_Finance.VALUE_OF_HYP = txtValueHyp.Text != "" ? Convert.ToDecimal(txtValueHyp.Text) : 0;

                    result = objBL_Finance.insertLoanSecurityHYPOTHICATION(objBO_Finance, out SQLError);
                }
                i = i + 1;

                if (result > 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('HYPOTH Save Successfully')</script>");
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('HYPOTH Unable to Save')</script>");
                }

            }//KVP/NSC/LIC AND OTHER BONDS

            if (ddlloantype.SelectedItem.Text == "KVP/NSC/LIC AND OTHER BONDS")
            {
                foreach (GridViewRow row in gv_KvpNsc.Rows)
                {
                    TextBox txtTypee = (TextBox)row.FindControl("txtTypee");

                    TextBox txtCertNo = (TextBox)row.FindControl("txtCertNo");

                    TextBox txtIssueDate = (TextBox)row.FindControl("txtIssueDate");

                    TextBox txtIssueOffice = (TextBox)row.FindControl("txtIssueOffice");

                    TextBox txtMatValue = (TextBox)row.FindControl("txtMatValue");

                    TextBox txtMatDate = (TextBox)row.FindControl("txtMatDate");

                    TextBox txtFaceValue = (TextBox)row.FindControl("txtFaceValue");

                    TextBox txtPledgeDate = (TextBox)row.FindControl("txtPledgeDate");

                    TextBox txtRemarkss = (TextBox)row.FindControl("txtRemarkss");

                    objBO_Finance.TYPE = ddlloantype.SelectedItem.Text != "" ? ddlloantype.SelectedItem.Text : "";
                    objBO_Finance.SL_CODE = cmbx_Slcode.Text != "" ? Convert.ToInt32(cmbx_Slcode.Text) : 0;

                    objBO_Finance.Sec_Type = txtTypee.Text != "" ? txtTypee.Text : "";
                    objBO_Finance.CERT_NO = txtCertNo.Text != "" ? txtCertNo.Text : "";
                    //objBO_Finance.ISS_DT=txtIssueDate.Text !=
                    string datedb3 = txtIssueDate.Text;
                    DateTime timedb3 = DateTime.ParseExact(datedb3, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.ISS_DT = timedb3;

                    objBO_Finance.ISS_OFF = txtIssueOffice.Text != "" ? txtIssueOffice.Text : "";
                    objBO_Finance.MAT_VAL = txtMatValue.Text != "" ? Convert.ToDecimal(txtMatValue.Text) : 0;

                    string datedb4 = txtMatDate.Text;
                    DateTime timedb4 = DateTime.ParseExact(datedb3, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.MAT_DT = timedb4;

                    objBO_Finance.FACE_VAL = txtFaceValue.Text != "" ? Convert.ToDecimal(txtFaceValue.Text) : 0;
                    // objBO_Finance.PLEDG_DATE=txtPledgeDate.Text!=""?

                    string datedb5 = txtPledgeDate.Text;
                    DateTime timedb5 = DateTime.ParseExact(datedb3, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objBO_Finance.PLEDG_DATE = timedb5;

                    objBO_Finance.REMARKS = txtRemarkss.Text != "" ? txtRemarkss.Text : "";




                    result = objBL_Finance.insertLoanSecurityKVP(objBO_Finance, out SQLError);
                }
                i = i + 1;

                if (result > 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('KVP Save Successfully')</script>");
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('KVP Unable to Save')</script>");
                }

            }

            if (ddlloantype.SelectedItem.Text == "FIXED DEPOSITS")
            {
                foreach (GridViewRow row in gv_FixDepo.Rows)
                {
                    TextBox txtFDAccNo = (TextBox)row.FindControl("txtFDAccNo");

                    TextBox txtDepAmt = (TextBox)row.FindControl("txtDepAmt");

                    TextBox txtMatDate = (TextBox)row.FindControl("txtMatDate");

                    CheckBox fchbx = (CheckBox)row.FindControl("FD_CheckBox");

                    if (fchbx.Checked == true)
                    {

                        objBO_Finance.TYPE = ddlloantype.SelectedItem.Text != "" ? ddlloantype.SelectedItem.Text : "";
                        objBO_Finance.SL_CODE = cmbx_Slcode.Text != "" ? Convert.ToInt32(cmbx_Slcode.Text) : 0;

                        objBO_Finance.FD_ACC_NO = txtFDAccNo.Text != "" ? Convert.ToDecimal(txtFDAccNo.Text) : 0;
                        objBO_Finance.DEP_AMT = txtDepAmt.Text != "" ? Convert.ToDecimal(txtDepAmt.Text) : 0;


                        string datedb4 = txtMatDate.Text;
                        DateTime timedb4 = DateTime.ParseExact(datedb4, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objBO_Finance.MAT_DT = timedb4;

                        result = objBL_Finance.insertLoanSecurityFD(objBO_Finance, out SQLError);
                    }
                }
                i = i + 1;

                if (result > 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('FD Save Successfully')</script>");
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('FD Unable to Save')</script>");
                }
            }

            if (ddlloantype.SelectedItem.Text == "DEPOSITS CERTIFICATE")
            {
                foreach (GridViewRow row in gv_DepoCert.Rows)
                {

                    TextBox txtDCAccNo = (TextBox)row.FindControl("txtDCAccNo");

                    TextBox txtDepAmt = (TextBox)row.FindControl("txtDepAmt");

                    TextBox txtMatDate = (TextBox)row.FindControl("txtMatDate");

                    CheckBox dcchbx = (CheckBox)row.FindControl("DC_CheckBox");

                    if (dcchbx.Checked == true)
                    {

                        objBO_Finance.TYPE = ddlloantype.SelectedItem.Text != "" ? ddlloantype.SelectedItem.Text : "";
                        objBO_Finance.SL_CODE = cmbx_Slcode.Text != "" ? Convert.ToInt32(cmbx_Slcode.Text) : 0;

                        objBO_Finance.DC_ACC_NO = txtDCAccNo.Text != "" ? Convert.ToDecimal(txtDCAccNo.Text) : 0;
                        objBO_Finance.DEP_AMT = txtDepAmt.Text != "" ? Convert.ToDecimal(txtDepAmt.Text) : 0;


                        string datedb4 = txtMatDate.Text;
                        DateTime timedb4 = DateTime.ParseExact(datedb4, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objBO_Finance.MAT_DT = timedb4;

                        result = objBL_Finance.insertLoanSecurityDC(objBO_Finance, out SQLError);
                    }
                }
                i = i + 1;

                if (result > 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('DC Save Successfully')</script>");
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('DC Unable to Save')</script>");
                }
            }

            if (ddlloantype.SelectedItem.Text == "RECURRING DEPOSITS")
            {
                foreach (GridViewRow row in gv_recurdepo.Rows)
                {
                    TextBox txtRDAccNo = (TextBox)row.FindControl("txtRDAccNo");

                    TextBox txtDepAmt = (TextBox)row.FindControl("txtDepAmt");

                    TextBox txtMatDate = (TextBox)row.FindControl("txtMatDate");

                    CheckBox chbx = (CheckBox)row.FindControl("RD_CheckBox");
                    if (chbx.Checked == true)
                    {
                        objBO_Finance.TYPE = ddlloantype.SelectedItem.Text != "" ? ddlloantype.SelectedItem.Text : "";
                        objBO_Finance.SL_CODE = cmbx_Slcode.Text != "" ? Convert.ToInt32(cmbx_Slcode.Text) : 0;

                        objBO_Finance.RD_ACC_NO = txtRDAccNo.Text != "" ? Convert.ToDecimal(txtRDAccNo.Text) : 0;
                        objBO_Finance.DEP_AMT = txtDepAmt.Text != "" ? Convert.ToDecimal(txtDepAmt.Text) : 0;


                        // string datedb4 = txtMatDate.Text.Substring(1,txtMatDate.Text.Length-10);

                        string datedb4 = txtMatDate.Text;
                        DateTime timedb4 = DateTime.ParseExact(datedb4, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objBO_Finance.MAT_DT = timedb4;

                        result = objBL_Finance.insertLoanSecurityRD(objBO_Finance, out SQLError);

                    }

                }
                i = i + 1;

                if (result > 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('RD Save Successfully')</script>");
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('RD Unable to Save')</script>");
                }
            }

            if (ddlloantype.SelectedItem.Text == "MIS DEPOSITS")
            {
                foreach (GridViewRow row in gv_MisDepo.Rows)
                {
                    TextBox txtMisAccNo = (TextBox)row.FindControl("txtMisAccNo");

                    TextBox txtDepAmt = (TextBox)row.FindControl("txtDepAmt");

                    TextBox txtMatDate = (TextBox)row.FindControl("txtMatDate");

                    CheckBox mischbx = (CheckBox)row.FindControl("MIS_CheckBox");

                    if (mischbx.Checked == true)
                    {

                        objBO_Finance.TYPE = ddlloantype.SelectedItem.Text != "" ? ddlloantype.SelectedItem.Text : "";
                        objBO_Finance.SL_CODE = cmbx_Slcode.Text != "" ? Convert.ToInt32(cmbx_Slcode.Text) : 0;

                        objBO_Finance.MIS_ACC_NO = txtMisAccNo.Text != "" ? Convert.ToDecimal(txtMisAccNo.Text) : 0;
                        objBO_Finance.DEP_AMT = txtDepAmt.Text != "" ? Convert.ToDecimal(txtDepAmt.Text) : 0;


                        string datedb4 = txtMatDate.Text;
                        DateTime timedb4 = DateTime.ParseExact(datedb4, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objBO_Finance.MAT_DT = timedb4;

                        result = objBL_Finance.insertLoanSecurityMIS(objBO_Finance, out SQLError);
                    }

                }
                i = i + 1;

                if (result > 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('MIS Save Successfully')</script>");
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('MIS Unable to Save')</script>");
                }
            }

            if (ddlloantype.SelectedItem.Text == "DAILY DEPOSITS")
            {
                foreach (GridViewRow row in gv_DD.Rows)
                {
                    TextBox txtDailyAccNo = (TextBox)row.FindControl("txtDailyAccNo");

                    TextBox txtDepAmt = (TextBox)row.FindControl("txtDepAmt");

                    TextBox txtMatDate = (TextBox)row.FindControl("txtMatDate");

                    CheckBox ddchbx = (CheckBox)row.FindControl("DD_CheckBox");

                    if (ddchbx.Checked == true)

                    {

                        objBO_Finance.TYPE = ddlloantype.SelectedItem.Text != "" ? ddlloantype.SelectedItem.Text : "";
                        objBO_Finance.SL_CODE = cmbx_Slcode.Text != "" ? Convert.ToInt32(cmbx_Slcode.Text) : 0;

                        objBO_Finance.DAILY_ACC_NO = txtDailyAccNo.Text != "" ? Convert.ToDecimal(txtDailyAccNo.Text) : 0;
                        objBO_Finance.DEP_AMT = txtDepAmt.Text != "" ? Convert.ToDecimal(txtDepAmt.Text) : 0;


                        string datedb4 = txtMatDate.Text;
                        DateTime timedb4 = DateTime.ParseExact(datedb4, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objBO_Finance.MAT_DT = timedb4;

                        result = objBL_Finance.insertLoanSecurityDAILY(objBO_Finance, out SQLError);
                    }

                }
                i = i + 1;

                if (result > 0)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Daily Save Successfully')</script>");
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Daily Unable to Save')</script>");
                }
            }






        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmLoanDetailEdit.aspx");
        }

       
    }
}
