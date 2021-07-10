using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Data;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace iBankingSolution.Report
{
    public partial class frmFinalAccountsSettings : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        DataTable dtDetailsSettings;
        string message;
        String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmbx_ReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbx_ReportType.SelectedIndex > 0)
            {
                objBO_Finance.Flag = 1;


                if (cmbx_ReportType.SelectedValue == "TR")
                {
                    dtDetailsSettings = objBL_Finance.FinalAccountsDetailsSettingsTR(objBO_Finance, out SQLError);
                }
                else if (cmbx_ReportType.SelectedValue == "PL")
                {
                    dtDetailsSettings = objBL_Finance.FinalAccountsDetailsSettingsPL(objBO_Finance, out SQLError);
                }
                else if (cmbx_ReportType.SelectedValue == "BS")
                {
                    dtDetailsSettings = objBL_Finance.FinalAccountsDetailsSettingsBS(objBO_Finance, out SQLError);
                }


                objBO_Finance.ReportType = cmbx_ReportType.SelectedValue;

                Session["dtDetailsSettings"] = dtDetailsSettings;

                if (dtDetailsSettings.Rows.Count > 0)
                {

                    gridSettings.DataSource = dtDetailsSettings;
                    gridSettings.DataBind();
                }

                else
                {
                    gridSettings.DataSource = null;
                    gridSettings.DataBind();
                }

            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {





        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //objBO_Finance.Flag = 1;
            //objBO_Finance.ReportType = cmbx_ReportType.SelectedValue;
            //int i = objBL_Finance.InsertUpdateFinalAccountSettings(objBO_Finance, out SQLError);
            //if (i > 0)
            //{
            //    message = "alert('Record Inserted Successfully .')";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            //}
            //else
            //{
            //    message = "alert('Something Wrong Input.')";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            //}

            //Delete from existing
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand("usp_UpdateAcctDetailsOnReporttype", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReportType", Convert.ToString(cmbx_ReportType.SelectedValue));
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            //Deletion complete
            string Msg = "";
            DataTable dt = new DataTable();
            int Count = 1;
            string SlNo = "";
            //dt.Columns.AddRange(new DataColumn[]
            //{
            //    new DataColumn("SL"), new DataColumn("R_ID"),
            //    new DataColumn("SL_HEAD"), new DataColumn("AMT"),
            //    new DataColumn("CURR"), new DataColumn("PRE"), new DataColumn("L_SLIDE")
            //});



            SqlConnection conn = new SqlConnection(strConnString);
            foreach (GridViewRow row in gridSettings.Rows)
            {





                TextBox txtHead = (TextBox)row.FindControl("txtLhead");
                TextBox txtLamt = (TextBox)row.FindControl("txtLamt");
                TextBox txtLcur = (TextBox)row.FindControl("txtLcur");
                TextBox txtLpre = (TextBox)row.FindControl("txtLpre");

                TextBox txtRhead = (TextBox)row.FindControl("txtRhead");
                TextBox txtRamt = (TextBox)row.FindControl("txtRamt");
                TextBox txtRCurr = (TextBox)row.FindControl("txtRCurr");
                TextBox txtRpre = (TextBox)row.FindControl("txtRpre");
                if (row.Cells[0].Text.Trim() != "&nbsp;")
                {
                    SlNo = row.Cells[0].Text.Trim();
                }
                else

                {
                    SlNo = "";
                }

                if (cmbx_ReportType.SelectedValue == "TR")
                {
                    SqlCommand com = new SqlCommand("insert into GET_TRADING_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SIDE) values ('" + SlNo + "','" + Count + "','" + txtHead.Text.Trim() + "','" + Convert.ToString(txtLamt.Text) + "','" + Convert.ToString(txtLcur.Text) + "','" + Convert.ToString(txtLpre.Text) + "','LEFT')", con);
                    con.Open();
                    int r = com.ExecuteNonQuery();

                    SqlCommand com2 = new SqlCommand("insert into GET_TRADING_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SIDE) values ('" + SlNo + "','" + Count + "','" + txtRhead.Text.Trim() + "','" + Convert.ToString(txtRamt.Text) + "','" + Convert.ToString(txtRCurr.Text) + "','" + Convert.ToString(txtRpre.Text) + "','RIGHT')", con);

                    int r1 = com2.ExecuteNonQuery();


                    con.Close();

                    Count = Count + 1;
                }

                else if (cmbx_ReportType.SelectedValue == "PL")
                {


                    //SqlCommand com = new SqlCommand("insert into GET_PL_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SIDE) values ('" + SlNo + "','" + Count + "','" + txtHead.Text.Trim() + "','" + txtLamt.Text.Trim() + "','" + txtLcur.Text.Trim() + "','" + txtLpre.Text.Trim() + "','LEFT')", con);
                    SqlCommand com = new SqlCommand("insert into GET_PL_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SIDE) values ('" + SlNo + "','" + Count + "','" + txtHead.Text.Trim() + "','" + Convert.ToString(txtLamt.Text) + "','" + Convert.ToString(txtLcur.Text) + "','" + Convert.ToString(txtLpre.Text) + "','LEFT')", con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    //SqlCommand com2 = new SqlCommand("insert into GET_PL_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SIDE) values ('" + SlNo + "','" + Count + "','" + txtRhead.Text.Trim() + "','" + txtLamt.Text.Trim() + "','" + txtLcur.Text.Trim() + "','" + txtLpre.Text.Trim() + "','RIGHT')", con);
                    SqlCommand com2 = new SqlCommand("insert into GET_PL_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SIDE) values ('" + SlNo + "','" + Count + "','" + txtRhead.Text.Trim() + "','" + Convert.ToString(txtRamt.Text) + "','" + Convert.ToString(txtRCurr.Text) + "','" + Convert.ToString(txtRpre.Text) + "','RIGHT')", con);

                    con.Open();
                    com2.ExecuteNonQuery();


                    con.Close();

                    Count = Count + 1;
                }

                else if (cmbx_ReportType.SelectedValue == "BS")
                {


                    //SqlCommand com = new SqlCommand("insert into GET_BAL_SHEET_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SIDE) values ('" + SlNo + "','" + Count + "','" + txtHead.Text.Trim() + "','" + txtLamt.Text.Trim() + "','" + txtLcur.Text.Trim() + "','" + txtLpre.Text.Trim() + "','LEFT')", con);
                    SqlCommand com = new SqlCommand("insert into GET_BAL_SHEET_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SIDE) values ('" + SlNo + "','" + Count + "','" + txtHead.Text.Trim() + "','" + Convert.ToString(txtLamt.Text) + "','" + Convert.ToString(txtLcur.Text) + "','" + Convert.ToString(txtLpre.Text) + "','LEFT')", con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    //SqlCommand com2 = new SqlCommand("insert into GET_BAL_SHEET_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SIDE) values ('" + SlNo + "','" + Count + "','" + txtHead.Text.Trim() + "','" + txtLamt.Text.Trim() + "','" + txtLcur.Text.Trim() + "','" + txtLpre.Text.Trim() + "','RIGHT')", con);
                    SqlCommand com2 = new SqlCommand("insert into GET_BAL_SHEET_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SIDE) values ('" + SlNo + "','" + Count + "','" + txtRhead.Text.Trim() + "','" + Convert.ToString(txtRamt.Text) + "','" + Convert.ToString(txtRCurr.Text) + "','" + Convert.ToString(txtRpre.Text) + "','RIGHT')", con);
                    con.Open();
                    com2.ExecuteNonQuery();


                    con.Close();

                    Count = Count + 1;
                }


            }


        }



        //        foreach (GridViewRow row in gridSettings.Rows)
        //        {
        //            if (row.Cells[0].Text.Trim() != "")
        //            {
        //                SlNo = row.Cells[0].Text.Trim();
        //            }
        //            else

        //            {
        //                SlNo = "";
        //            }
        //            SqlConnection conn = new SqlConnection(strConnString);

        //            SqlCommand com = new SqlCommand("insert into GET_PL_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SLIDE) values ('" + SlNo + "','" + Count + "','" + row.Cells[1].Text.Trim() + "','" + row.Cells[2].Text.Trim() + "',,'" + row.Cells[3].Text.Trim() + "','" + row.Cells[4].Text.Trim() + "','LEFT')", con);
        //            conn.Open();
        //            com.ExecuteNonQuery();

        //            SqlCommand com2 = new SqlCommand("insert into GET_PL_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SLIDE) values ('" + SlNo + "','" + Count + "','" + row.Cells[6].Text.Trim() + "','" + row.Cells[7].Text.Trim() + "',,'" + row.Cells[8].Text.Trim() + "','" + row.Cells[9].Text.Trim() + "','RIGHT')", con);
        //            conn.Open();
        //            com2.ExecuteNonQuery();


        //            con.Close();

        //            Count = Count + 1;

        //        }
        //    }
        //    else if (cmbx_ReportType.SelectedValue == "BS")
        //    {
        //        foreach (GridViewRow row in gridSettings.Rows)
        //        {
        //            if (row.Cells[0].Text.Trim() != "")
        //            {
        //                SlNo = row.Cells[0].Text.Trim();
        //            }
        //            else

        //            {
        //                SlNo = "";
        //            }
        //            SqlConnection conn = new SqlConnection(strConnString);

        //            SqlCommand com = new SqlCommand("insert into GET_BAL_SHEET_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SLIDE) values ('" + SlNo + "','" + Count + "','" + row.Cells[1].Text.Trim() + "','" + row.Cells[2].Text.Trim() + "',,'" + row.Cells[3].Text.Trim() + "','" + row.Cells[4].Text.Trim() + "','LEFT')", con);
        //            conn.Open();
        //            com.ExecuteNonQuery();

        //            SqlCommand com2 = new SqlCommand("insert into GET_BAL_SHEET_DET(SL,R_ID,SL_HEAD,AMT,CURR,PRE,L_SLIDE) values ('" + SlNo + "','" + Count + "','" + row.Cells[6].Text.Trim() + "','" + row.Cells[7].Text.Trim() + "',,'" + row.Cells[8].Text.Trim() + "','" + row.Cells[9].Text.Trim() + "','RIGHT')", con);
        //            conn.Open();
        //            com2.ExecuteNonQuery();


        //            con.Close();

        //            Count = Count + 1;

        //        }
        //    }

        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void gridSettings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = gridSettings.Rows[rowIndex];

                DataTable dtt = new DataTable();
                dtt = (DataTable)Session["dtDetailsSettings"];


                DataRow rw = dtt.NewRow();
                dtt.Rows.InsertAt(rw, rowIndex + 1);

                if (dtt.Rows.Count > 0)
                {

                    gridSettings.DataSource = dtt;
                    gridSettings.DataBind();
                }


            }
            if (e.CommandName == "Delete")
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = gridSettings.Rows[rowIndex];

                DataTable dtt = new DataTable();
                dtt = (DataTable)Session["dtDetailsSettings"];


                DataRow rw = dtt.NewRow();
                dtt.Rows.RemoveAt(rowIndex);

                if (dtt.Rows.Count > 0)
                {

                    gridSettings.DataSource = dtt;
                    gridSettings.DataBind();
                }


            }

        }

        protected void gridSettings_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}