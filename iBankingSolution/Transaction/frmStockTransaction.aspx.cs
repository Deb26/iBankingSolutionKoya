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
using BLL.GeneralBL;
using System.Data.SqlClient;
using System.Configuration;

namespace iBankingSolution.stock
{
    public partial class frmStockTransaction : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string message;
        String CurSession = String.Empty;
        MyDBDataContext dbContext = new MyDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCategotyname();
                dtItemDetails();
                txt_entrydt.Text = DateTime.Now.ToString("dd/MM/yyyy");

            }

        }


        private void AddNewRowToGrid()
        {

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows.Add(drCurrentRow);

                    //add new row to DataTable   

                    //Store the current data to ViewState for future reference   

                    //ViewState["CurrentTable"] = dtCurrentTable;


                    for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                    {

                        //extract the TextBox values   
                        Label lbl_SlNo = (Label)GV_GSTVIEW.Rows[i].FindControl("lbl_SlNo") as Label;

                        //DropDownList cmbx_itemname = row.FindControl("cmbx_itemname") as DropDownList;
                        DropDownList ddlItem = GV_GSTVIEW.Rows[i].FindControl("cmbx_itemname") as DropDownList;
                        TextBox ddlUnit = GV_GSTVIEW.Rows[i].FindControl("txt_Unit") as TextBox;
                        DropDownList Unit = GV_GSTVIEW.Rows[i].FindControl("cmbxUnit") as DropDownList;
                        TextBox txtcurrstatus = GV_GSTVIEW.Rows[i].FindControl("txtcurrstatus") as TextBox;
                        TextBox txtQty = GV_GSTVIEW.Rows[i].FindControl("txtQty") as TextBox;
                        TextBox txtrateunit = GV_GSTVIEW.Rows[i].FindControl("txtrateunit") as TextBox;
                        TextBox txttotamt = GV_GSTVIEW.Rows[i].FindControl("txttotamt") as TextBox;
                        TextBox txtbatchno = GV_GSTVIEW.Rows[i].FindControl("txtbatchno") as TextBox;
                        TextBox txtexpdate = GV_GSTVIEW.Rows[i].FindControl("txtexpdate") as TextBox;
                        TextBox txthsnno = GV_GSTVIEW.Rows[i].FindControl("txthsnno") as TextBox;
                        TextBox txtcgst = GV_GSTVIEW.Rows[i].FindControl("txtcgst") as TextBox;
                        TextBox txtsgst = GV_GSTVIEW.Rows[i].FindControl("txtsgst") as TextBox;
                        TextBox txtcgstamt = GV_GSTVIEW.Rows[i].FindControl("txtcgstamt") as TextBox;
                        TextBox txtsgstamt = GV_GSTVIEW.Rows[i].FindControl("txtsgstamt") as TextBox;
                        TextBox txttotalamt = GV_GSTVIEW.Rows[i].FindControl("txttotalamt") as TextBox;
                        TextBox txtPurLdg = GV_GSTVIEW.Rows[i].FindControl("txtPurLdg") as TextBox;
                        TextBox txtSaleLdg = GV_GSTVIEW.Rows[i].FindControl("txtSaleLdg") as TextBox;


                        dtCurrentTable.Rows[i]["SlNo"] = i;
                        dtCurrentTable.Rows[i]["Code"] = ddlItem.SelectedValue;

                        dtCurrentTable.Rows[i]["Unit"] = Unit.SelectedValue;
                        dtCurrentTable.Rows[i]["CurStock"] = txtcurrstatus.Text;
                        dtCurrentTable.Rows[i]["MU"] = txtQty.Text;
                        dtCurrentTable.Rows[i]["Rate"] = txtrateunit.Text;
                        dtCurrentTable.Rows[i]["totAmt"] = txttotamt.Text;
                        dtCurrentTable.Rows[i]["BATCH_NO"] = txtbatchno.Text;

                        if (string.IsNullOrEmpty(txtexpdate.Text))
                        {
                            txtexpdate.Text = DateTime.Now.ToShortDateString();
                            dtCurrentTable.Rows[i]["EXP_DT"] = txtexpdate.Text;
                        }
                        else
                        {
                            dtCurrentTable.Rows[i]["EXP_DT"] = Convert.ToDateTime(txtexpdate.Text).ToShortDateString();
                        }


                        //dtCurrentTable.Rows[i]["EXP_DT"] = string.IsNullOrEmpty(txtexpdate.Text) ? (DateTime?)null : Convert.ToDateTime(txtexpdate.Text); //txtexpdate.Text != "" ? Convert.ToDateTime(txtexpdate.Text).ToString() : "";  
                        dtCurrentTable.Rows[i]["HSNNO"] = txthsnno.Text;
                        dtCurrentTable.Rows[i]["CGST"] = txtcgst.Text;
                        dtCurrentTable.Rows[i]["SGST"] = txtsgst.Text;
                        dtCurrentTable.Rows[i]["cgstAmt"] = txtcgstamt.Text;
                        dtCurrentTable.Rows[i]["sgstAmt"] = txtsgstamt.Text;
                        dtCurrentTable.Rows[i]["grandTotal"] = txttotalamt.Text;
                        dtCurrentTable.Rows[i]["Pur_Ldg"] = txtPurLdg.Text;
                        dtCurrentTable.Rows[i]["Sale_ldg"] = txtSaleLdg.Text;

                    }

                    //Rebind the Grid with the current data to reflect changes   
                    GV_GSTVIEW.DataSource = dtCurrentTable;
                    GV_GSTVIEW.DataBind();
                    ViewState["CurrentTable"] = dtCurrentTable;
                }
            }
            else
            {
                Response.Write("ViewState is null");

            }
            //Set Previous Data on Postbacks   
            SetPreviousData();
        }

        protected void itbnNew_Click(object sender, ImageClickEventArgs e)
        {

            txttaxableamtt.Text = Convert.ToString(ViewState["Taxableamt"]);
            AddNewRowToGrid();
            Decimal tot = 0, t = 0, nt = 0, NetTotal = 0;


            foreach (GridViewRow item in GV_GSTVIEW.Rows)
            {
                TextBox total = item.FindControl("txttotalamt") as TextBox;
                TextBox GrossTotal = item.FindControl("txttotamt") as TextBox;


                if (GrossTotal.Text == "")
                {
                    GrossTotal.Text = "0";
                }
                nt = Convert.ToDecimal(GrossTotal.Text);
                NetTotal = NetTotal + nt;

                if (total.Text == "")
                {
                    total.Text = "0";
                }

                t = Convert.ToDecimal(total.Text);
                tot = tot + t;

            }
            Label TaxableAmt = (Label)GV_GSTVIEW.FooterRow.FindControl("lblNetAmt");
            Label lblGrossAmt = (Label)GV_GSTVIEW.FooterRow.FindControl("lblGrossAmt");

            lblGrossAmt.Text = Convert.ToString(NetTotal);

            lblGrossAmt.Text = string.Format("{0:0.00}", NetTotal);

            TaxableAmt.Text = Convert.ToString(tot);
            ViewState["Taxableamt"] = TaxableAmt.Text;
            txttaxableamtt.Text = Convert.ToString(ViewState["Taxableamt"]);


            //Label1.Text = Convert.ToString(tot);
        }


        private void GrandPaidTotal()
        {
            float GTotal = 0f;

            for (int i = 0; i < GV_GSTVIEW.Rows.Count; i++)
            {
                String total = (GV_GSTVIEW.Rows[i].FindControl("txttotalamt") as TextBox).Text;

                GTotal += Convert.ToSingle(total);
            }
            txttaxableamtt.Text = GTotal.ToString();
        }
        private void SetPreviousData()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Label lbl_SlNo = (Label)GV_GSTVIEW.Rows[i].FindControl("lbl_SlNo") as Label;

                        //DropDownList cmbx_itemname = row.FindControl("cmbx_itemname") as DropDownList;
                        DropDownList ddl1 = GV_GSTVIEW.Rows[i].FindControl("cmbx_itemname") as DropDownList;
                        TextBox txtUnit = GV_GSTVIEW.Rows[i].FindControl("txt_Unit") as TextBox;
                        DropDownList Unit = GV_GSTVIEW.Rows[i].FindControl("cmbxUnit") as DropDownList;
                        TextBox txtcurrstatus = GV_GSTVIEW.Rows[i].FindControl("txtcurrstatus") as TextBox;
                        TextBox txtQty = GV_GSTVIEW.Rows[i].FindControl("txtQty") as TextBox;
                        TextBox txtrateunit = GV_GSTVIEW.Rows[i].FindControl("txtrateunit") as TextBox;
                        TextBox txttotamt = GV_GSTVIEW.Rows[i].FindControl("txttotamt") as TextBox;
                        TextBox txtbatchno = GV_GSTVIEW.Rows[i].FindControl("txtbatchno") as TextBox;
                        TextBox txtexpdate = GV_GSTVIEW.Rows[i].FindControl("txtexpdate") as TextBox;
                        TextBox txthsnno = GV_GSTVIEW.Rows[i].FindControl("txthsnno") as TextBox;
                        TextBox txtcgst = GV_GSTVIEW.Rows[i].FindControl("txtcgst") as TextBox;
                        TextBox txtsgst = GV_GSTVIEW.Rows[i].FindControl("txtsgst") as TextBox;
                        TextBox txtcgstamt = GV_GSTVIEW.Rows[i].FindControl("txtcgstamt") as TextBox;
                        TextBox txtsgstamt = GV_GSTVIEW.Rows[i].FindControl("txtsgstamt") as TextBox;
                        TextBox txttotalamt = GV_GSTVIEW.Rows[i].FindControl("txttotalamt") as TextBox;
                        TextBox PurLDG = GV_GSTVIEW.Rows[i].FindControl("txtPurLdg") as TextBox;
                        TextBox SalLDG = GV_GSTVIEW.Rows[i].FindControl("txtSaleLdg") as TextBox;



                        //Fill the DropDownList with Data
                        FillItemList(ddl1);
                        FillItemUnit(Unit);



                        if (i < dt.Rows.Count - 1)
                        {
                            for (int j = 0; j < ddl1.Items.Count; j++)
                            {
                                //int cd = Convert.ToInt32(dt.Rows[i]["Code"]);
                                if (ddl1.Items[j].Value == dt.Rows[i]["Code"].ToString())
                                {
                                    ddl1.ClearSelection();
                                    //ddl1.Items[j].Text = dt.Rows[i]["Name"].ToString();
                                    ddl1.Items[j].Selected = true;
                                    //ddl1.Items.FindByText(dt.Rows[i]["Name"].ToString()).Selected = true;
                                    break;
                                }
                            }

                            for (int j = 0; j < Unit.Items.Count; j++)
                            {
                                //int cd = Convert.ToInt32(dt.Rows[i]["Code"]);
                                if (Unit.Items[j].Value == dt.Rows[i]["Unit"].ToString())
                                {
                                    Unit.ClearSelection();
                                    Unit.Items[j].Selected = true;
                                    break;
                                }
                            }





                            //Assign the value from DataTable to the TextBox   
                            //rowIndex = Convert.ToInt32(dt.Rows.Count);
                            //lbl_SlNo.Text = i; 
                            txtUnit.Text = dt.Rows[i]["Unit"].ToString();

                            txtcurrstatus.Text = dt.Rows[i]["CurStock"].ToString();
                            txtQty.Text = dt.Rows[i]["MU"].ToString();
                            txtrateunit.Text = dt.Rows[i]["Rate"].ToString();
                            txttotamt.Text = dt.Rows[i]["totAmt"].ToString();
                            txtbatchno.Text = dt.Rows[i]["BATCH_NO"].ToString();
                            txtexpdate.Text = dt.Rows[i]["EXP_DT"].ToString();
                            txthsnno.Text = dt.Rows[i]["HSNNO"].ToString();
                            txtcgst.Text = dt.Rows[i]["CGST"].ToString();
                            txtsgst.Text = dt.Rows[i]["SGST"].ToString();
                            txtcgstamt.Text = dt.Rows[i]["cgstAmt"].ToString();

                            txtsgstamt.Text = dt.Rows[i]["sgstAmt"].ToString();
                            txttotalamt.Text = dt.Rows[i]["grandTotal"].ToString();
                            PurLDG.Text = dt.Rows[i]["Pur_Ldg"].ToString();
                            SalLDG.Text = dt.Rows[i]["Sale_ldg"].ToString();

                        }
                        rowIndex++;
                    }
                }
            }
        }
        protected void SetInitialRow()
        {
            DataTable dt1 = new DataTable();
            DataRow dr = null;
            int rowcnt = 0;
            //Define the Columns
            dt1.Columns.Add(new DataColumn("SlNo", typeof(Int32)));
            dt1.Columns.Add(new DataColumn("Code", typeof(Int32)));
            dt1.Columns.Add(new DataColumn("Name", typeof(string)));
            dt1.Columns.Add(new DataColumn("Unit", typeof(Int32)));
            dt1.Columns.Add(new DataColumn("CurStock", typeof(double)));
            dt1.Columns.Add(new DataColumn("MU", typeof(double)));
            dt1.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("totAmt", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("BATCH_NO", typeof(string)));
            dt1.Columns.Add(new DataColumn("EXP_DT", typeof(DateTime)));
            dt1.Columns.Add(new DataColumn("HSNNO", typeof(string)));
            dt1.Columns.Add(new DataColumn("CGST", typeof(double)));
            dt1.Columns.Add(new DataColumn("SGST", typeof(double)));
            dt1.Columns.Add(new DataColumn("cgstAmt", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("sgstAmt", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("grandTotal", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("Pur_Ldg", typeof(Int32)));
            dt1.Columns.Add(new DataColumn("Sale_ldg", typeof(Int32)));


            dr = dt1.NewRow();
            dr[0] = 1;
            dr[1] = 0;
            dr[2] = "";
            dr[3] = 0;
            dr[4] = 0;
            dr[5] = 0;
            dr[6] = 0;
            dr[7] = 0;
            dr[8] = 0;
            dr[9] = DateTime.Now.ToShortDateString(); ;
            dr[10] = "";
            dr[11] = 0;
            dr[12] = 0;
            dr[13] = 0;
            dr[14] = 0;
            dr[15] = 0;
            dr[16] = 0;
            dr[17] = 0;





            dt1.Rows.Add(dr);

            ViewState["CurrentTable"] = dt1;

            //Bind the Gridview   
            GV_GSTVIEW.DataSource = dt1;
            GV_GSTVIEW.DataBind();

            //After binding the gridview, we can then extract and fill the DropDownList with Data   

            DropDownList ddl1 = GV_GSTVIEW.Rows[0].FindControl("cmbx_cateName") as DropDownList;
            DropDownList ddl2 = GV_GSTVIEW.Rows[0].FindControl("cmbxUnit") as DropDownList;


            FillDropDownList();
            FillDropDownList2();






        }

        protected void getCategotyname()
        {
            DataSet ds = new DataSet();
            ds = objBL_Finance.get_Group_Item(objBO_Finance);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmbx_cateName.DataSource = ds;
                cmbx_cateName.DataValueField = "Code";
                cmbx_cateName.DataTextField = "Name";
                cmbx_cateName.DataBind();
            }
        }
        private void ResetPage()
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void ResetControls()
        {
            GV_GSTVIEW.DataSource = null;
            GV_GSTVIEW.DataBind();
            cmbx_entrytype.SelectedIndex = -1;
            //txt_entrydt.Text = DateTime.Now.ToShortDateString();
            cmbx_cateName.SelectedIndex = -1;
            cmbs_transType.SelectedIndex = -1;
            cmbx_SundryDRCR.SelectedIndex = -1;
            //cmbx_SaleLedger.SelectedIndex = -1;
            txtCode.Text = String.Empty;
            txt_name.Text = String.Empty;
            txt_address.Text = String.Empty;
            txt_gstno.Text = String.Empty;
            txt_idcardno.Text = String.Empty;
            txttaxableamtt.Text = String.Empty;
            txtdiscountper.Text = String.Empty;
            txtDisAmt.Text = String.Empty;
            totbillamtt.Text = String.Empty;

        }
        private DataTable dtItemDetails()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("SlNo"),
                    new DataColumn("Item_Code"),
                    new DataColumn("Qty"),
                    new DataColumn("Rate"),
                    new DataColumn("Unit"),
                    new DataColumn("CGST_Rate"),
                    new DataColumn("SGST_Rate"),
                    new DataColumn("IGST_Rate"),
                    new DataColumn("CGSTAmt"),
                    new DataColumn("SGSTAmt"),
                    new DataColumn("IGSTAmt"),
                    new DataColumn("DiscAmt"),
                    new DataColumn("TotalAmt"),
                    new DataColumn("PurLdg"),
                    new DataColumn("SaleLdg"),
                    new DataColumn("Amt"),
                    new DataColumn("CurrentStock"),
                    new DataColumn("HSNNO"),
                });
            for (int i = 0; i < 20; i++)
            {
                dt1.Rows.Add(dt1.Rows.Count + 1, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                    DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);
                dt1.AcceptChanges();
            }
            ViewState["dtItemDetails"] = dt1;
            return dt1;
        }
        protected void btnsubmit1_Click(object sender, EventArgs e)
        {
            //GetCurrentSession();
            //DataTable dtItemDetails = (ViewState["dtItemDetails"] as DataTable).Copy();
            DataTable dtItemDetails = (DataTable)ViewState["dtItemDetails"];
            foreach (GridViewRow item in GV_GSTVIEW.Rows)
            {
                var inst = dtItemDetails.AsEnumerable().Where(x => x.Field<string>("SlNo") == ((Label)item.FindControl("lbl_SlNo")).Text).First();
                inst.SetField("ITEM_CODE", ((DropDownList)item.FindControl("cmbx_itemname")).SelectedValue);
                inst.SetField("Qty", ((TextBox)item.FindControl("txtQty")).Text);
                inst.SetField("Rate", ((TextBox)item.FindControl("txtrateunit")).Text);
                //inst.SetField("Unit", ((TextBox)item.FindControl("txt_Unit")).Text);
                inst.SetField("Unit", ((DropDownList)item.FindControl("cmbxUnit")).SelectedValue);
                inst.SetField("Amt", ((TextBox)item.FindControl("txttotamt")).Text);
                inst.SetField("CGST_RATE", ((TextBox)item.FindControl("txtcgst")).Text);
                inst.SetField("SGST_RATE", ((TextBox)item.FindControl("txtsgst")).Text);
                inst.SetField("CGSTAmt", ((TextBox)item.FindControl("txtcgstamt")).Text);
                inst.SetField("SGSTAmt", ((TextBox)item.FindControl("txtsgstamt")).Text);
                inst.SetField("TotalAmt", ((TextBox)item.FindControl("txttotalamt")).Text);
                inst.SetField("DiscAmt", txtDisAmt.Text);
                inst.SetField("PurLdg", ((TextBox)item.FindControl("txtPurLdg")).Text);
                inst.SetField("SaleLdg", ((TextBox)item.FindControl("txtSaleLdg")).Text);

            }


            dtItemDetails.Columns.Remove("CurrentStock");
            dtItemDetails.Columns.Remove("HSNNO");
            dtItemDetails.Columns.Remove("SlNo");
            dtItemDetails.Columns.Remove("Amt");
            dtItemDetails.AcceptChanges();
            dtItemDetails.AsEnumerable().Where(x => Convert.ToString(x["Item_Code"]) == "" || Convert.ToDouble(x["Qty"]) <= 0).ToList().ForEach(dr => dr.Delete());
            dtItemDetails.AcceptChanges();

            objBO_Finance.dtItemDetails = dtItemDetails;
            objBO_Finance.EntryType = cmbx_entrytype.SelectedValue;
            objBO_Finance.TransType = cmbs_transType.SelectedValue;

            //objBO_Finance.SL_CODE = cmbx_SundryDRCR.SelectedValue;

            //objBO_Finance.EntryDate = Convert.ToDateTime(txt_entrydt.Text);

            string datedb = txt_entrydt.Text;
            DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.EntryDate = timedb;

            objBO_Finance.GROUP_CODE = cmbx_cateName.SelectedItem.Text != "" ? cmbx_cateName.SelectedItem.Text : "";

            if (cmbs_transType.SelectedItem.Text == "Cash")
            {
                //objBO_Finance.SupCode = txt_name.Text != "" ? txt_name.Text : "";
                objBO_Finance.SupCode = "0";
                objBO_Finance.SupName = txt_name.Text != "" ? txt_name.Text : "";
                objBO_Finance.Address1 = txt_address.Text != "" ? txt_address.Text : "";   
            }
            else
            {
                objBO_Finance.SupCode = txtCode.Text != "" ? txtCode.Text : "";
                objBO_Finance.SupName = "";
            }

            if (cmbs_transType.SelectedItem.Text == "Credit")
            {
                //objBO_Finance.SupName = cmbx_SundryDRCR.SelectedValue != "" ? cmbx_SundryDRCR.SelectedValue : "";
                objBO_Finance.Sale_to = cmbx_SundryDRCR.Text != "" ? Convert.ToDouble(cmbx_SundryDRCR.SelectedValue) : 0;
            }
            //else if(cmbs_transType.SelectedItem.Text == "Credit" && cmbx_entrytype.SelectedItem.Text == "Sale")
            //{
            //    objBO_Finance.Sale_to = cmbx_SaleLedger.Text != "" ? Convert.ToDouble(cmbx_SaleLedger.SelectedValue) : 0;
            //}
            else
            {
                //objBO_Finance.SupName = String.Empty;
                objBO_Finance.Sale_to = 0;
            }
            //objBO_Finance.Address1 = txt_address.Text != "" ? txt_address.Text : "";
            objBO_Finance.GSTINNo = txt_gstno.Text != "" ? txt_gstno.Text : "";
            objBO_Finance.IDNO = txt_idcardno.Text != "" ? txt_idcardno.Text : "";
            objBO_Finance.Comments = "";
            //objBO_Finance.ASSES = CurSession;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

            objBO_Finance.Flag = btnsubmit1.Text == "Save" ? 1 : 2;

            int i = objBL_Finance.InsertUpdateDeleteSTockTrans(objBO_Finance, out SQLError);
            if (i > 0)
            {
                MessageBox(this, "Record Inserted Successfully . Allotted Voucher No is:-" + SQLError);
                ResetControls();
                //    rwm_Alert.RadAlert(btn_Save.Text == "Save" ? "Save Successfully.<br /> Allotted Voucher No: " + SQLError + "" : "Update Successfully", 300, 200, "Successful", "redirect");
            }
            else
            {
                MessageBox(this, "Not Saved . Error Details:-" + SQLError);
                //rwm_Alert.RadAlert("Somthing Wrong. Error Details:  " + SQLError + "", 300, 200, "Error", "");
            }
        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {

        }

        protected void GetCurrentSession()
        {
            var curs = (from unt in dbContext.CODESANDNOs

                        select new
                        {
                            Sess = unt.Session
                        }).FirstOrDefault();
            if (curs != null)
            {

                CurSession = curs.Sess;

            }
        }

        protected void cmbx_itemname_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCurrentSession();
            GridViewRow currentrow = (GridViewRow)((DropDownList)sender).Parent.Parent;
            DropDownList cmbx_itemname = (DropDownList)currentrow.FindControl("cmbx_itemname");
            objBO_Finance.Item_Code = cmbx_itemname.SelectedValue;
            objBO_Finance.ASSES = CurSession;

            DataSet ds = new DataSet();
            ds = objBL_Finance.GSTCALC_PRODUCTDETAILS(objBO_Finance);
            ViewState["dsitem"] = ds;

            TextBox txtcurrstatus = (TextBox)currentrow.FindControl("txtcurrstatus");

            TextBox txtbatchno = (TextBox)currentrow.FindControl("txtbatchno");


            //TextBox txtexpdate = (TextBox)currentrow.FindControl("txtexpdate");

            TextBox txthsnno = (TextBox)currentrow.FindControl("txthsnno");
            TextBox CGST = (TextBox)currentrow.FindControl("txtcgst");
            TextBox SGST = (TextBox)currentrow.FindControl("txtsgst");

            TextBox PurchaseLdg = (TextBox)currentrow.FindControl("txtPurLdg");
            TextBox SalesLDG = (TextBox)currentrow.FindControl("txtSaleLdg");
            TextBox txtUnit = (TextBox)currentrow.FindControl("txt_Unit");
            TextBox txtQty = (TextBox)currentrow.FindControl("txtQty");
            DropDownList Unit = (DropDownList)currentrow.FindControl("cmbxUnit");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtcurrstatus.Text = Convert.ToString(ds.Tables[0].Rows[0]["CurrentStock"]);
                //txtexpdate.Text = Convert.ToString(ds.Tables[0].Rows[0]["EXP_DT"]);
                //txthsnno.Text = Convert.ToString(ds.Tables[0].Rows[0]["HSNNO"]);
                //txtbatchno.Text = Convert.ToString(ds.Tables[0].Rows[0]["Batch_no"]);
                CGST.Text = Convert.ToString(ds.Tables[0].Rows[0]["CGST"]);
                SGST.Text = Convert.ToString(ds.Tables[0].Rows[0]["SGST"]);
                PurchaseLdg.Text = Convert.ToString(ds.Tables[0].Rows[0]["Pur_Ldg"]);
                SalesLDG.Text = Convert.ToString(ds.Tables[0].Rows[0]["Sale_ldg"]);
                txtUnit.Text = Convert.ToString(ds.Tables[0].Rows[0]["Unit"]);
                Unit.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Unit"]);

            }

            txtQty.Focus();



        }

        protected void GetUnit(DropDownList ddl3)
        {
            //cmbx_Unit.Items.Clear();
            foreach (GridViewRow row in GV_GSTVIEW.Rows)
            {
                DropDownList cmbx_Unit = row.FindControl("cmbx_Unit") as DropDownList;

                cmbx_Unit.Items.Add(new ListItem("0", "--Select Type--"));
                cmbx_Unit.Items.Add(new ListItem("Kg", "Kilogram"));
                cmbx_Unit.Items.Add(new ListItem("g", "Gram"));
                cmbx_Unit.Items.Add(new ListItem("Ltr", "Litre"));
                cmbx_Unit.Items.Add(new ListItem("MT", "MT"));

                cmbx_Unit.Items.Add(new ListItem("Bag", "Bag"));
                cmbx_Unit.Items.Add(new ListItem("Pcs", "Pcs"));


            }


        }

        protected void FillDropDownList2()
        {
            var varUnit = (from unt in dbContext.MEASURING_UNITs

                           select new
                           {
                               Id = unt.Code,
                               Name = unt.MCode
                           }).ToList();

            if (varUnit.Count > 0)
            {
                foreach (GridViewRow row in GV_GSTVIEW.Rows)
                {
                    DropDownList cmbx_Unit = row.FindControl("cmbxUnit") as DropDownList;
                    cmbx_Unit.DataSource = varUnit.ToList();
                    cmbx_Unit.DataValueField = "Id";
                    cmbx_Unit.DataTextField = "Name";
                    cmbx_Unit.DataBind();

                }

            }
        }
        protected void FillItemUnit(DropDownList Unit)
        {

            var varUnit = (from unt in dbContext.MEASURING_UNITs

                           select new
                           {
                               Id = unt.Code,
                               Name = unt.MCode
                           }).ToList();
            if (varUnit.Count > 0)
            {
                Unit.DataSource = varUnit.ToList();
                Unit.DataValueField = "Id";
                Unit.DataTextField = "Name";
                Unit.DataBind();
                Unit.Items.Insert(0, "-Select-");



            }

        }

        protected void FillItemList(DropDownList ddl1)
        {
            objBO_Finance.MCLASS = cmbx_cateName.SelectedValue;
            DataSet ds = new DataSet();
            ds = objBL_Finance.ItemName_MCLASSWISE(objBO_Finance);
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddl1.DataSource = ds;
                ddl1.DataValueField = "Code";
                ddl1.DataTextField = "NAME";
                ddl1.DataBind();
                //cmbx_itemname.Items.Insert(0, new ListItem("--Select Item Name--", "0"));


            }
        }

        protected void FillDropDownList()
        {
            objBO_Finance.MCLASS = cmbx_cateName.SelectedValue;
            DataSet ds = new DataSet();
            ds = objBL_Finance.ItemName_MCLASSWISE(objBO_Finance);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (GridViewRow row in GV_GSTVIEW.Rows)
                {
                    DropDownList cmbx_itemname = row.FindControl("cmbx_itemname") as DropDownList;
                    cmbx_itemname.DataSource = ds;
                    cmbx_itemname.DataValueField = "Code";
                    cmbx_itemname.DataTextField = "NAME";
                    cmbx_itemname.DataBind();

                }

            }
        }

        protected void cmbx_cateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetInitialRow();
            objBO_Finance.MCLASS = cmbx_cateName.SelectedValue;
            DataSet ds = new DataSet();
            ds = objBL_Finance.ItemName_MCLASSWISE(objBO_Finance);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (GridViewRow row in GV_GSTVIEW.Rows)
                {
                    DropDownList cmbx_itemname = row.FindControl("cmbx_itemname") as DropDownList;
                    cmbx_itemname.DataSource = ds;
                    cmbx_itemname.DataValueField = "Code";
                    cmbx_itemname.DataTextField = "NAME";
                    cmbx_itemname.DataBind();

                }

            }



            if (cmbx_entrytype.Text == "Sale")
            {
                foreach (GridViewRow row in GV_GSTVIEW.Rows)
                {
                    TextBox txtexpdate = row.FindControl("txtexpdate") as TextBox;
                    TextBox txtbatchno = row.FindControl("txtbatchno") as TextBox;
                    TextBox txthsnno = row.FindControl("txthsnno") as TextBox;
                    txtbatchno.Enabled = false;
                    txtexpdate.Enabled = false;
                    txthsnno.Enabled = false;

                }
            }

            else if (cmbx_entrytype.Text == "Purchase")
            {
                foreach (GridViewRow row in GV_GSTVIEW.Rows)
                {

                    TextBox txtexpdate = row.FindControl("txtexpdate") as TextBox;
                    TextBox txtbatchno = row.FindControl("txtbatchno") as TextBox;
                    TextBox txthsnno = row.FindControl("txthsnno") as TextBox;
                    txtbatchno.Enabled = true;
                    txtexpdate.Enabled = true;
                    txthsnno.Enabled = true;
                }
            }
        }
        double tamt = 0;
        protected void txtrateunit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox TxtRateunit = (TextBox)currentrow.FindControl("txtrateunit");
            TextBox txtQty = (TextBox)currentrow.FindControl("txtQty");

            TextBox txttotamt = (TextBox)currentrow.FindControl("txttotamt");

            TextBox TxtSgst = (TextBox)currentrow.FindControl("txtsgst");
            TextBox txtcgst = (TextBox)currentrow.FindControl("txtcgst");

            TextBox txtcgstamt = (TextBox)currentrow.FindControl("txtcgstamt");
            TextBox txtsgstamt = (TextBox)currentrow.FindControl("txtsgstamt");
            TextBox txttotalamt = (TextBox)currentrow.FindControl("txttotalamt");

            txttotamt.Text = Convert.ToString(Convert.ToDouble(TxtRateunit.Text) * Convert.ToDouble(txtQty.Text));
            txtcgstamt.Text = Convert.ToString(Convert.ToDouble(txttotamt.Text) * Convert.ToDouble(txtcgst.Text) / 100);
            txtsgstamt.Text = Convert.ToString(Convert.ToDouble(txttotamt.Text) * Convert.ToDouble(TxtSgst.Text) / 100);
            txttotalamt.Text = Convert.ToString(Convert.ToDouble(txttotamt.Text) + Convert.ToDouble(txtcgstamt.Text) * 2);
            ViewState["total"] = Convert.ToDouble(txttotalamt.Text);

            Decimal tot = 0, t = 0, nt = 0, NetTotal = 0;


            foreach (GridViewRow item in GV_GSTVIEW.Rows)
            {
                TextBox total = item.FindControl("txttotalamt") as TextBox;
                TextBox GrossTotal = item.FindControl("txttotamt") as TextBox;


                if (GrossTotal.Text == "")
                {
                    GrossTotal.Text = "0";
                }
                nt = Convert.ToDecimal(GrossTotal.Text);
                NetTotal = NetTotal + nt;

                if (total.Text == "")
                {
                    total.Text = "0";
                }

                t = Convert.ToDecimal(total.Text);
                tot = tot + t;

            }
            Label TaxableAmt = (Label)GV_GSTVIEW.FooterRow.FindControl("lblNetAmt");
            Label lblGrossAmt = (Label)GV_GSTVIEW.FooterRow.FindControl("lblGrossAmt");
            lblGrossAmt.Text = Convert.ToString(NetTotal);
            TaxableAmt.Text = Convert.ToString(tot);
            ViewState["Taxableamt"] = TaxableAmt.Text;
            txttaxableamtt.Text = Convert.ToString(ViewState["Taxableamt"]);


        }

        protected void txtsgst_TextChanged(object sender, EventArgs e)
        {



        }

        protected void cmbx_entrytype_SelectedIndexChanged(object sender, EventArgs e)
        {

            panelVoucher.Visible = false;
            txttaxableamtt.Text = "";
            txtdiscountper.Text = "";
            totbillamtt.Text = "";

            if (cmbx_entrytype.Text == "Sale")
            {
                FillSalesLedger();
                cmbx_SundryDRCR.SelectedIndex = -1;
                foreach (GridViewRow row in GV_GSTVIEW.Rows)
                {
                    TextBox txtexpdate = row.FindControl("txtexpdate") as TextBox;
                    TextBox txtbatchno = row.FindControl("txtbatchno") as TextBox;
                    TextBox txthsnno = row.FindControl("txthsnno") as TextBox;
                    txtbatchno.Enabled = false;
                    txtexpdate.Enabled = false;
                    txthsnno.Enabled = false;

                }
            }

            else if (cmbx_entrytype.Text == "Purchase")
            {
                FillCreditorDebtor();
                //cmbx_SaleLedger.SelectedIndex = -1;
                foreach (GridViewRow row in GV_GSTVIEW.Rows)
                {

                    TextBox txtexpdate = row.FindControl("txtexpdate") as TextBox;
                    TextBox txtbatchno = row.FindControl("txtbatchno") as TextBox;
                    TextBox txthsnno = row.FindControl("txthsnno") as TextBox;
                    txtbatchno.Enabled = true;
                    txtexpdate.Enabled = true;
                    txthsnno.Enabled = true;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomer(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct CNAME from STOCK_TRANS where " + "CNAME like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> dist = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            dist.Add(sdr["CNAME"].ToString());
                        }
                    }
                    conn.Close();
                    return dist;
                }
            }
        }


        protected void cmbs_transType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbs_transType.Text == "Credit")
            {

                cmbx_SundryDRCR.Enabled = true;
                txtCode.Enabled = false;
                txt_name.Enabled = false;
                txt_address.Enabled = false;
            }
            else
            {
                cmbx_SundryDRCR.Enabled = false;
                txtCode.Enabled = false;

                cmbx_SundryDRCR.SelectedIndex = -1;
                txt_name.Enabled = true;
                txt_address.Enabled = true;

                txtCode.Text = String.Empty;
            }
        }

        protected void FillSalesLedger()
        {
            try
            {
                objBO_Finance.Flag = 8;
                objBO_Finance.CUST_ID = null;
                DataTable dt = objBL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
                if (dt.Rows.Count > 0)
                {
                    //cmbx_SaleLedger.DataSource = dt;
                    //cmbx_SaleLedger.DataValueField = "LDG_CODE";
                    //cmbx_SaleLedger.DataTextField = "NOMENCLATURE";
                    //cmbx_SaleLedger.DataBind();
                }
            }
            catch (Exception ex)
            { string msg = ex.Message; }
            finally
            { }

        }
        protected void FillCreditorDebtor()
        {
            var varDrCr = (from SM in dbContext.SUBLEDGER_MASTERs
                           where SM.ACTYPE == "g" && SM.SL_CODE != 990000
                           select new
                           {
                               SLCode = SM.SL_CODE,
                               NAME = SM.SL_NAME


                           }).ToList();


            if (varDrCr.Count > 0)
            {

                cmbx_SundryDRCR.DataSource = varDrCr.ToList();
                cmbx_SundryDRCR.DataTextField = "NAME";
                cmbx_SundryDRCR.DataValueField = "SLCode";
                cmbx_SundryDRCR.DataBind();
                cmbx_SundryDRCR.Items.Insert(0, new ListItem("--Select Name--", "0"));

            }
        }

        protected void cmbx_SundryDRCR_SelectedIndexChanged(object sender, EventArgs e)
        {
            var varDrCr = (from SM in dbContext.SUBLEDGER_MASTERs
                           where SM.SL_CODE == Convert.ToDecimal(cmbx_SundryDRCR.SelectedValue)
                           select new
                           {
                               GST = SM.GSTINNO,
                               ID_NO = SM.ID_NO

                           }).FirstOrDefault();
            if (varDrCr != null)
            {
                txt_gstno.Text = varDrCr.GST;
                txt_idcardno.Text = varDrCr.ID_NO;
            }
            txtCode.Text = cmbx_SundryDRCR.SelectedValue;
        }

        protected void txt_name_TextChanged(object sender, EventArgs e)
        {
            var varCust = (from ST in dbContext.STOCK_TRANs
                           where ST.CNAME == txt_name.Text
                           select new
                           {
                               GST = ST.GSTIN_NO,
                               ADDR = ST.CUSTADDRESS
                           }).FirstOrDefault();
            if (varCust != null)
            {
                txt_gstno.Text = varCust.GST;
                txt_address.Text = varCust.ADDR;


            }
        }

        protected void itbndelete_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void GV_GSTVIEW_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //DataTable dt = ViewState["CurrentTable"] as DataTable;
            //dt.Rows.RemoveAt(e.RowIndex);
            //GV_GSTVIEW.DataSource = dt;
            //GV_GSTVIEW.DataBind();
            if (GV_GSTVIEW.Rows.Count == 1)
            {
                //Nothing
            }
            else
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dt = new DataTable();
                DataRow dr = null;
                //Define the Columns

                dt.Columns.Add(new DataColumn("SlNo", typeof(Int32)));
                dt.Columns.Add(new DataColumn("Code", typeof(Int32)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Unit", typeof(string)));

                dt.Columns.Add(new DataColumn("ROL", typeof(double)));
                dt.Columns.Add(new DataColumn("MU", typeof(double)));
                dt.Columns.Add(new DataColumn("Rate", typeof(decimal)));
                dt.Columns.Add(new DataColumn("totAmt", typeof(decimal)));
                dt.Columns.Add(new DataColumn("BATCH_NO", typeof(string)));
                dt.Columns.Add(new DataColumn("EXP_DT", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("HSNNO", typeof(string)));
                dt.Columns.Add(new DataColumn("CGST", typeof(double)));
                dt.Columns.Add(new DataColumn("SGST", typeof(double)));
                dt.Columns.Add(new DataColumn("cgstAmt", typeof(decimal)));
                dt.Columns.Add(new DataColumn("sgstAmt", typeof(decimal)));
                dt.Columns.Add(new DataColumn("grandTotal", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Pur_Ldg", typeof(Int32)));
                dt.Columns.Add(new DataColumn("Sale_ldg", typeof(Int32)));

                foreach (GridViewRow row in GV_GSTVIEW.Rows)
                {
                    DropDownList ddlItem = row.FindControl("cmbx_itemname") as DropDownList;
                    TextBox ddlUnt = row.FindControl("txt_Unit") as TextBox;

                    Label lblSrl = row.FindControl("lbl_SlNo") as Label;
                    TextBox txtcurrstatus = row.FindControl("txtcurrstatus") as TextBox;
                    TextBox txtQty = row.FindControl("txtQty") as TextBox;
                    TextBox txtrateunit = row.FindControl("txtrateunit") as TextBox;
                    TextBox txttotamt = row.FindControl("txttotamt") as TextBox;
                    TextBox txtbatchno = row.FindControl("txtbatchno") as TextBox;
                    TextBox txtexpdate = row.FindControl("txtexpdate") as TextBox;
                    TextBox txthsnno = row.FindControl("txthsnno") as TextBox;
                    TextBox txtcgst = row.FindControl("txtcgst") as TextBox;
                    TextBox txtsgst = row.FindControl("txtsgst") as TextBox;
                    TextBox txtcgstamt = row.FindControl("txtcgstamt") as TextBox;
                    TextBox txtsgstamt = row.FindControl("txtsgstamt") as TextBox;
                    TextBox txttotalamt = row.FindControl("txttotalamt") as TextBox;
                    TextBox txtPurLdg = row.FindControl("txtPurLdg") as TextBox;
                    TextBox txtSaleLdg = row.FindControl("txtSaleLdg") as TextBox;
                    dr = dt.NewRow();


                    dr["SlNo"] = lblSrl.Text;
                    dr["Code"] = ddlItem.SelectedValue;
                    dr["Unit"] = ddlUnt.Text;
                    dr["ROL"] = txtcurrstatus.Text != "" ? txtcurrstatus.Text : "0";
                    dr["MU"] = txtQty.Text != "" ? txtQty.Text : "0";
                    dr["Rate"] = txtrateunit.Text != "" ? txtrateunit.Text : "0";
                    dr["totAmt"] = txttotamt.Text != "" ? txttotamt.Text : "0";
                    dr["BATCH_NO"] = txtbatchno.Text != "" ? txttotamt.Text : "";

                    if (string.IsNullOrEmpty(txtexpdate.Text))
                    {
                        txtexpdate.Text = DateTime.Now.ToShortDateString();
                        dr["EXP_DT"] = txtexpdate.Text;
                    }
                    else
                    {
                        dr["EXP_DT"] = Convert.ToDateTime(txtexpdate.Text).ToShortDateString();
                    }


                    //dtCurrentTable.Rows[i]["EXP_DT"] = string.IsNullOrEmpty(txtexpdate.Text) ? (DateTime?)null : Convert.ToDateTime(txtexpdate.Text); //txtexpdate.Text != "" ? Convert.ToDateTime(txtexpdate.Text).ToString() : "";  
                    dr["HSNNO"] = txthsnno.Text != "" ? txthsnno.Text : "";
                    dr["CGST"] = txtcgst.Text != "" ? txtcgst.Text : "0";
                    dr["SGST"] = txtsgst.Text != "" ? txtsgst.Text : "0";
                    dr["cgstAmt"] = txtcgstamt.Text != "" ? txtcgstamt.Text : "0";
                    dr["sgstAmt"] = txtsgstamt.Text != "" ? txtsgstamt.Text : "0";
                    dr["grandTotal"] = txttotalamt.Text != "" ? txttotalamt.Text : "0";
                    dr["Pur_Ldg"] = txtPurLdg.Text != "" ? txtPurLdg.Text : "0";
                    dr["Sale_ldg"] = txtSaleLdg.Text != "" ? txtSaleLdg.Text : "0";
                    dt.Rows.Add(dr);

                }
                dt.Rows[index].Delete();
                dt.AcceptChanges();
                GV_GSTVIEW.DataSource = dt;
                GV_GSTVIEW.DataBind();
                ViewState["CurrentTable"] = dt;
                SetPreviousDataAfterDelete();
            }
        }
        private void SetPreviousDataAfterDelete()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Label lbl_SlNo = (Label)GV_GSTVIEW.Rows[i].FindControl("lbl_SlNo") as Label;

                        //DropDownList cmbx_itemname = row.FindControl("cmbx_itemname") as DropDownList;
                        DropDownList ddl1 = GV_GSTVIEW.Rows[i].FindControl("cmbx_itemname") as DropDownList;
                        TextBox txtUnits = GV_GSTVIEW.Rows[i].FindControl("txt_Unit") as TextBox;

                        TextBox txtcurrstatus = GV_GSTVIEW.Rows[i].FindControl("txtcurrstatus") as TextBox;
                        TextBox txtQty = GV_GSTVIEW.Rows[i].FindControl("txtQty") as TextBox;
                        TextBox txtrateunit = GV_GSTVIEW.Rows[i].FindControl("txtrateunit") as TextBox;
                        TextBox txttotamt = GV_GSTVIEW.Rows[i].FindControl("txttotamt") as TextBox;
                        TextBox txtbatchno = GV_GSTVIEW.Rows[i].FindControl("txtbatchno") as TextBox;
                        TextBox txtexpdate = GV_GSTVIEW.Rows[i].FindControl("txtexpdate") as TextBox;
                        TextBox txthsnno = GV_GSTVIEW.Rows[i].FindControl("txthsnno") as TextBox;
                        TextBox txtcgst = GV_GSTVIEW.Rows[i].FindControl("txtcgst") as TextBox;
                        TextBox txtsgst = GV_GSTVIEW.Rows[i].FindControl("txtsgst") as TextBox;
                        TextBox txtcgstamt = GV_GSTVIEW.Rows[i].FindControl("txtcgstamt") as TextBox;
                        TextBox txtsgstamt = GV_GSTVIEW.Rows[i].FindControl("txtsgstamt") as TextBox;
                        TextBox txttotalamt = GV_GSTVIEW.Rows[i].FindControl("txttotalamt") as TextBox;
                        TextBox txtPurLdg = GV_GSTVIEW.Rows[i].FindControl("txtPurLdg") as TextBox;
                        TextBox txtSaleLdg = GV_GSTVIEW.Rows[i].FindControl("txtSaleLdg") as TextBox;
                        //Fill the DropDownList with Data
                        FillItemList(ddl1);
                        //FillItemUnit(ddl2);


                        if (i < dt.Rows.Count - 1)
                        {
                            for (int j = 0; j < ddl1.Items.Count; j++)
                            {
                                //int cd = Convert.ToInt32(dt.Rows[i]["Code"]);
                                if (ddl1.Items[j].Value == dt.Rows[i]["Code"].ToString())
                                {
                                    ddl1.ClearSelection();

                                    ddl1.Items[j].Selected = true;
                                    break;
                                }
                            }




                            //Assign the value from DataTable to the TextBox   
                            //rowIndex = Convert.ToInt32(dt.Rows.Count);
                            //lbl_SlNo.Text = i;
                            txtUnits.Text = dt.Rows[i]["Unit"].ToString();
                            txtcurrstatus.Text = dt.Rows[i]["ROL"].ToString();
                            txtQty.Text = dt.Rows[i]["MU"].ToString();
                            txtrateunit.Text = dt.Rows[i]["Rate"].ToString();
                            txttotamt.Text = dt.Rows[i]["totAmt"].ToString();
                            txtbatchno.Text = dt.Rows[i]["BATCH_NO"].ToString();
                            txtexpdate.Text = dt.Rows[i]["EXP_DT"].ToString();
                            txthsnno.Text = dt.Rows[i]["HSNNO"].ToString();
                            txtcgst.Text = dt.Rows[i]["CGST"].ToString();
                            txtsgst.Text = dt.Rows[i]["SGST"].ToString();
                            txtcgstamt.Text = dt.Rows[i]["cgstAmt"].ToString();

                            txtsgstamt.Text = dt.Rows[i]["sgstAmt"].ToString();
                            txttotalamt.Text = dt.Rows[i]["grandTotal"].ToString();

                            txtPurLdg.Text = dt.Rows[i]["Pur_Ldg"].ToString();
                            txtSaleLdg.Text = dt.Rows[i]["Sale_ldg"].ToString();

                        }
                        rowIndex++;
                    }
                }
            }
        }

        protected void btnVoucher_Click(object sender, EventArgs e)
        {
            panelVoucher.Visible = true;
            GrandPaidTotal();
        }

        protected void txtdiscountper_TextChanged(object sender, EventArgs e)
        {
            if (txtDisAmt.Text == "")
            {
                totbillamtt.Text = Convert.ToString(Convert.ToDouble(txttaxableamtt.Text) - Convert.ToDouble(txttaxableamtt.Text) * Convert.ToDouble(txtdiscountper.Text) / 100);
                txtDisAmt.Text = Convert.ToString(Convert.ToDouble(txttaxableamtt.Text) * Convert.ToDouble(txtdiscountper.Text) / 100);
            }
        }

        protected void txtDisAmt_TextChanged(object sender, EventArgs e)
        {
            if (txtdiscountper.Text == "")
            {
                totbillamtt.Text = Convert.ToString(Convert.ToDouble(txttaxableamtt.Text) - Convert.ToDouble(txtDisAmt.Text));

                double percent = (double)(Convert.ToDouble(txttaxableamtt.Text) * 100) / Convert.ToDouble(totbillamtt.Text);
                txtdiscountper.Text = percent.ToString("0.0%");
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txtUnit = (TextBox)currentrow.FindControl("txt_Unit");

            txtUnit.Focus();





        }
    }
}