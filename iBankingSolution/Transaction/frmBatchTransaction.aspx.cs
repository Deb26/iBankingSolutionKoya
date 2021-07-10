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
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;

namespace iBankingSolution.Transaction
{
    public partial class frmBatchTransaction : System.Web.UI.Page
    {
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;

        MyDBDataContext dbContext = new MyDBDataContext();
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        string Filename = string.Empty;
        string excelConnectionString;
        OleDbConnection excelConnection;
        string FilePath;
        string Sheetname;
        System.Data.DataTable dtExcelData;
        Boolean data = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //dtpkr_CollectionDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                GetLedgerCodes();
                Divldg.Visible = false;
            }
        }
        protected void GetLedgerCodes()
        {
            var _result = (from a in dbContext.LEDGER_MASTERs
                           where !(from b in dbContext.GROUP_MASTERs
                                   where b.GROUP_CODE == 12 || b.GROUP_CODE == 13 || b.GROUP_CODE == 14 || b.GROUP_CODE == 30
                                   select b.GROUP_CODE)
                                     .Contains(a.LDG_CODE)
                           select new
                           {
                               ID = a.LDG_CODE,
                               Name = a.NOMENCLATURE
                           }).ToList();

            if (_result != null)
            {

                Cmbx_Ledger.DataSource = _result.ToList();
                Cmbx_Ledger.DataTextField = "Name";
                Cmbx_Ledger.DataValueField = "ID";
                Cmbx_Ledger.DataBind();
                Cmbx_Ledger.Items.Insert(0, "--Select Ledger--");
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            ImportExcel();

            //Code Checking
            CheckSlCodes();
            //Code Checking Complete
        }
        protected void ClearFields()
        {
            gridAct.DataSource = null;
            gridAct.DataBind();
            cmbx_Type.SelectedIndex = -1;
            Cmbx_Ledger.SelectedIndex = -1;
            Cmbx_TransType.SelectedIndex = -1;
            txtRemarks.Text = "";
            dtpkr_frmDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            DdlSheetNames.SelectedIndex = -1;
            lblListShow.Text = "";


        }
        private void MsgBox(string sMessage)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + sMessage + "');";
            msg += "</script>";
            Response.Write(msg);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string datedb = dtpkr_frmDate.Text;
            DateTime timedb = DateTime.ParseExact(datedb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBO_Finance.disb_date = timedb;
            string vchNo = "";
            DataSet dSet = objBL_Finance.GenVoucherNo(objBO_Finance);

            if (dSet.Tables[0].Rows.Count > 0)
            {

                vchNo = Convert.ToString(dSet.Tables[0].Rows[0]["VoucherNo"]);
            }

            foreach (GridViewRow row in gridAct.Rows)
            {
                if (cmbx_Type.SelectedItem.Text == "Cash" && Cmbx_TransType.SelectedItem.Text == "Debit")
                {
                    CASHIER_COUNTER_BOOK ObjCCB = new CASHIER_COUNTER_BOOK();
                    decimal Code = Convert.ToDecimal(dbContext.CASHIER_COUNTER_BOOKs.Max(p => p.CCB_CODE) + 1);
                    ObjCCB.CCB_CODE = Code;
                    ObjCCB.CCB_DATE = timedb;
                    ObjCCB.VOUCHER_NO = vchNo;
                    ObjCCB.LDG_CODE = null;
                    ObjCCB.SL_CODE = 990000;
                    ObjCCB.NARRATION1 = txtRemarks.Text;
                    ObjCCB.NARRATION2 = txtRemarks.Text;

                    ObjCCB.AMT_DEBIT = Convert.ToDecimal(row.Cells[2].Text);
                    ObjCCB.AMT_CREDIT = 0;
                    ObjCCB.PAYMENT_RECEIPT = "r";
                    ObjCCB.TYPEOFINSTRUMENT = "voucher";
                    ObjCCB.DT_ON_INS = timedb;
                    ObjCCB.ENTRY_TIME = System.DateTime.Now;
                    ObjCCB.ENTRY_DATE = System.DateTime.Now;
                    ObjCCB.EMPCODE = Convert.ToDecimal(Session["UserID"]);
                    ObjCCB.SOCIETY_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjCCB.ENTRY_FR = "7";
                    ObjCCB.DAILY_SRL = 0;
                    ObjCCB.TERMINAL_NO = 0;
                    dbContext.CASHIER_COUNTER_BOOKs.InsertOnSubmit(ObjCCB);
                    // executes the appropriate commands to implement the changes to the database  
                    dbContext.SubmitChanges();


                    CASHIER_COUNTER_BOOK ObjCCB1 = new CASHIER_COUNTER_BOOK();
                    decimal Code1 = Convert.ToDecimal(dbContext.CASHIER_COUNTER_BOOKs.Max(p => p.CCB_CODE) + 1);
                    ObjCCB1.CCB_CODE = Code1;
                    ObjCCB1.CCB_DATE = timedb;
                    ObjCCB.VOUCHER_NO = vchNo;
                    ObjCCB1.LDG_CODE = null;
                    ObjCCB1.VOUCHER_NO = vchNo;
                    ObjCCB1.SL_CODE = Convert.ToDecimal(row.Cells[1].Text); //SLCODE
                    ObjCCB1.NARRATION1 = txtRemarks.Text;
                    ObjCCB1.NARRATION2 = txtRemarks.Text;
                    ObjCCB1.AMT_DEBIT = 0;
                    ObjCCB1.AMT_CREDIT = Convert.ToDecimal(row.Cells[2].Text);
                    ObjCCB1.PAYMENT_RECEIPT = "r";
                    ObjCCB1.TYPEOFINSTRUMENT = "voucher";
                    ObjCCB1.DT_ON_INS = timedb;
                    ObjCCB1.ENTRY_TIME = System.DateTime.Now;
                    ObjCCB1.ENTRY_DATE = System.DateTime.Now;
                    ObjCCB1.EMPCODE = Convert.ToDecimal(Session["UserID"]);
                    ObjCCB1.SOCIETY_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjCCB1.ENTRY_FR = "7";
                    ObjCCB1.DAILY_SRL = 0;
                    ObjCCB1.TERMINAL_NO = 0;

                    dbContext.CASHIER_COUNTER_BOOKs.InsertOnSubmit(ObjCCB1);
                    // executes the appropriate commands to implement the changes to the database  
                    dbContext.SubmitChanges();

                    MsgBox("Saved Successfully..");

                    //string message = "alert('Saved Successfully.')";
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);



                }
                else if (cmbx_Type.SelectedItem.Text == "Cash" && Cmbx_TransType.SelectedItem.Text == "Credit")
                {
                    //Debit Part
                    CASHIER_COUNTER_BOOK ObjCCB = new CASHIER_COUNTER_BOOK();
                    decimal Code1 = Convert.ToDecimal(dbContext.CASHIER_COUNTER_BOOKs.Max(p => p.CCB_CODE) + 1);
                    ObjCCB.CCB_CODE = Code1;
             
                    ObjCCB.CCB_DATE = timedb;
                    ObjCCB.VOUCHER_NO = vchNo;
                    ObjCCB.LDG_CODE = null;

                    ObjCCB.SL_CODE = Convert.ToDecimal(row.Cells[1].Text);
                    ObjCCB.NARRATION1 = txtRemarks.Text;
                    ObjCCB.NARRATION2 = txtRemarks.Text;

                    ObjCCB.AMT_DEBIT = 0;
                    ObjCCB.AMT_CREDIT = Convert.ToDecimal(row.Cells[2].Text);
                    ObjCCB.PAYMENT_RECEIPT = "p";
                    ObjCCB.TYPEOFINSTRUMENT = "voucher";
                    ObjCCB.DT_ON_INS = timedb;
                    ObjCCB.ENTRY_TIME = System.DateTime.Now;
                    ObjCCB.ENTRY_DATE = System.DateTime.Now;
                    ObjCCB.EMPCODE = Convert.ToDecimal(Session["UserID"]);
                    ObjCCB.SOCIETY_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjCCB.ENTRY_FR = "7";
                    ObjCCB.DAILY_SRL = 0;
                    ObjCCB.TERMINAL_NO = 0;
                    dbContext.CASHIER_COUNTER_BOOKs.InsertOnSubmit(ObjCCB);
                    // executes the appropriate commands to implement the changes to the database  
                    dbContext.SubmitChanges();

                    //Credit Part
                    CASHIER_COUNTER_BOOK ObjCCB1 = new CASHIER_COUNTER_BOOK();
                    decimal Code2 = Convert.ToDecimal(dbContext.CASHIER_COUNTER_BOOKs.Max(p => p.CCB_CODE) + 1);
                    ObjCCB1.CCB_CODE = Code2;
                   
                    ObjCCB1.CCB_DATE = timedb;
                    ObjCCB.VOUCHER_NO = vchNo;
                    ObjCCB1.LDG_CODE = null;

                    ObjCCB1.SL_CODE = 990000; //SLCODE
                    ObjCCB1.NARRATION1 = txtRemarks.Text;
                    ObjCCB1.NARRATION2 = txtRemarks.Text;
                    ObjCCB1.AMT_DEBIT = Convert.ToDecimal(row.Cells[2].Text);
                    ObjCCB1.AMT_CREDIT = 0;
                    ObjCCB1.PAYMENT_RECEIPT = "P";
                    ObjCCB1.TYPEOFINSTRUMENT = "voucher";
                    ObjCCB1.DT_ON_INS = timedb;
                    ObjCCB1.ENTRY_TIME = System.DateTime.Now;
                    ObjCCB1.ENTRY_DATE = System.DateTime.Now;
                    ObjCCB1.EMPCODE = Convert.ToDecimal(Session["UserID"]);
                    ObjCCB1.SOCIETY_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjCCB1.ENTRY_FR = "7";
                    ObjCCB1.DAILY_SRL = 0;
                    ObjCCB1.TERMINAL_NO = 0;

                    dbContext.CASHIER_COUNTER_BOOKs.InsertOnSubmit(ObjCCB1);
                    // executes the appropriate commands to implement the changes to the database  
                    dbContext.SubmitChanges();
                    MsgBox("Saved Successfully..");
                    //string message = "alert('Saved Successfully.')";
                    ////ClearFields();
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }

                //Journal transaction

                else if (cmbx_Type.SelectedItem.Text == "Journal" && Cmbx_TransType.SelectedItem.Text == "Debit")
                {
                    //Debit Part
                    JOURNAL_BOOK ObjJournal = new JOURNAL_BOOK();
                    ObjJournal.JDATE_OF_ENTRY = timedb;
                    ObjJournal.LDG_CODE = Convert.ToDecimal(Cmbx_Ledger.SelectedValue);
                    ObjJournal.SL_CODE = null;
                    ObjJournal.JVOUCHER_NO = vchNo;
                    ObjJournal.J_NARRATION = txtRemarks.Text;
                    ObjJournal.T_NARRATION = txtRemarks.Text;

                    ObjJournal.AMT_DEBIT = Convert.ToDecimal(row.Cells[2].Text);
                    ObjJournal.AMT_CREDIT = 0;
                    ObjJournal.PAYMENT_RECEIPT = "p";
                    ObjJournal.ENTRY_TYPE = "Adjustment";

                    ObjJournal.ENTRY_TIME = System.DateTime.Now;
                    ObjJournal.ENTRY_DATE = System.DateTime.Now;
                    ObjJournal.EMPCODE = Convert.ToDecimal(Session["UserID"]);
                    ObjJournal.SOCIETY_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjJournal.SOCIETY_BR_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjJournal.ENTRY_FR = "7";
                    ObjJournal.DAILY_SRL = 0;
                    ObjJournal.TERMINAL_NO = 0;
                    dbContext.JOURNAL_BOOKs.InsertOnSubmit(ObjJournal);
                    dbContext.SubmitChanges();

                    //Credit Part
                    JOURNAL_BOOK ObjJournal1 = new JOURNAL_BOOK();
                    ObjJournal1.JDATE_OF_ENTRY = timedb;
                    ObjJournal1.LDG_CODE = null;
                    ObjJournal1.SL_CODE = Convert.ToDecimal(row.Cells[1].Text);
                    ObjJournal.JVOUCHER_NO = vchNo;
                    ObjJournal1.J_NARRATION = txtRemarks.Text;
                    ObjJournal1.T_NARRATION = txtRemarks.Text;
                    ObjJournal1.AMT_DEBIT = 0;
                    ObjJournal1.AMT_CREDIT = Convert.ToDecimal(row.Cells[2].Text);
                    ObjJournal1.PAYMENT_RECEIPT = "p";
                    ObjJournal1.ENTRY_TYPE = "Adjustment";

                    ObjJournal1.ENTRY_TIME = System.DateTime.Now;
                    ObjJournal1.ENTRY_DATE = System.DateTime.Now;
                    ObjJournal1.EMPCODE = Convert.ToDecimal(Session["UserID"]);
                    ObjJournal1.SOCIETY_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjJournal.SOCIETY_BR_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjJournal1.ENTRY_FR = "7";
                    ObjJournal1.DAILY_SRL = 0;
                    ObjJournal1.TERMINAL_NO = 0;

                    dbContext.JOURNAL_BOOKs.InsertOnSubmit(ObjJournal1);
                    // executes the appropriate commands to implement the changes to the database  
                    dbContext.SubmitChanges();

                    MsgBox("Saved Successfully..");
                    //string message = "alert('Saved Successfully.')";

                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }


                else if (cmbx_Type.SelectedItem.Text == "Journal" && Cmbx_TransType.SelectedItem.Text == "Credit")
                {
                    //Debit Part
                    JOURNAL_BOOK ObjJournal = new JOURNAL_BOOK();


                    ObjJournal.JDATE_OF_ENTRY = timedb;
                    ObjJournal.LDG_CODE = null;
                    ObjJournal.SL_CODE = Convert.ToDecimal(row.Cells[1].Text);
                    ObjJournal.JVOUCHER_NO = vchNo;
                    ObjJournal.J_NARRATION = txtRemarks.Text;
                    ObjJournal.T_NARRATION = txtRemarks.Text;

                    ObjJournal.AMT_DEBIT = 0;
                    ObjJournal.AMT_CREDIT = Convert.ToDecimal(row.Cells[2].Text);
                    ObjJournal.PAYMENT_RECEIPT = "r";
                    ObjJournal.ENTRY_TYPE = "Adjustment";

                    ObjJournal.ENTRY_TIME = System.DateTime.Now;
                    ObjJournal.ENTRY_DATE = System.DateTime.Now;
                    ObjJournal.EMPCODE = Convert.ToDecimal(Session["UserID"]);
                    ObjJournal.SOCIETY_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjJournal.SOCIETY_BR_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjJournal.ENTRY_FR = "7";
                    ObjJournal.DAILY_SRL = 0;
                    ObjJournal.TERMINAL_NO = 0;
                    dbContext.JOURNAL_BOOKs.InsertOnSubmit(ObjJournal);
                    dbContext.SubmitChanges();

                    //Credit Part
                    JOURNAL_BOOK ObjJournal1 = new JOURNAL_BOOK();
                    ObjJournal1.JDATE_OF_ENTRY = timedb;
                    ObjJournal1.LDG_CODE = Convert.ToDecimal(row.Cells[1].Text);
                    ObjJournal1.SL_CODE = null;
                    ObjJournal.JVOUCHER_NO = vchNo;
                    ObjJournal1.J_NARRATION = txtRemarks.Text;
                    ObjJournal1.T_NARRATION = txtRemarks.Text;
                    ObjJournal1.AMT_DEBIT = Convert.ToDecimal(row.Cells[2].Text);
                    ObjJournal1.AMT_CREDIT = 0;
                    ObjJournal1.PAYMENT_RECEIPT = "r";
                    ObjJournal1.ENTRY_TYPE = "Adjustment";

                    ObjJournal1.ENTRY_TIME = System.DateTime.Now;
                    ObjJournal1.ENTRY_DATE = System.DateTime.Now;
                    ObjJournal1.EMPCODE = Convert.ToDecimal(Session["UserID"]);
                    ObjJournal1.SOCIETY_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjJournal.SOCIETY_BR_CODE = Convert.ToDecimal(Session["BranchID"]);
                    ObjJournal1.ENTRY_FR = "7";
                    ObjJournal1.DAILY_SRL = 0;
                    ObjJournal1.TERMINAL_NO = 0;

                    dbContext.JOURNAL_BOOKs.InsertOnSubmit(ObjJournal1);
                    // executes the appropriate commands to implement the changes to the database  
                    dbContext.SubmitChanges();

                    MsgBox("Saved Successfully..");
                    //string message = "alert('Saved Successfully.')";

                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
                }





            }
            ClearFields();

        }

        protected void CheckSlCodes()
        {
            System.Data.DataTable dt = Session["dtTable"] as System.Data.DataTable;
            if (txtRemarks.Text == "")
            {
                MessageBox(this, "Remarks Box Can not be left Blank");
            }
            int j = 0;
            foreach (GridViewRow row in gridAct.Rows)
            {
                var _result = (from a in dbContext.SUBLEDGER_MASTERs
                               where a.SL_CODE == Convert.ToDecimal(row.Cells[1].Text)
                               && a.AC_STATUS == "Live"
                               select new
                               {
                                   ID = a.SL_CODE,

                               }).SingleOrDefault();

                if (_result == null)
                {

                    /*MessageBox(this, "Account No Does Not Exists . SLCode is:-" + row.Cells[1].Text)*/
                    ;

                    row.Cells[0].BackColor = System.Drawing.Color.DarkOrange;
                    row.Cells[1].BackColor = System.Drawing.Color.DarkOrange;
                    row.Cells[2].BackColor = System.Drawing.Color.DarkOrange;
                    data = true;
                    dt.Rows[j].Delete();
                    gridAct.DataSource = dt;
                    gridAct.DataBind();
                    lblListShow.Text = " Total Records: " + gridAct.Rows.Count;

                }

                j = j + 1;

            }
        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        public void ImportExcel()
        {

            try
            {
                dtExcelData = new System.Data.DataTable();

                string Filename = Convert.ToString(Session["Filename"]);
                FilePath = Convert.ToString(Session["FilePath"]);


                if (Filename != "")
                {

                    //string FilePath = Convert.ToString(Session["FilePath"]);


                    excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Persist Security Info=False;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";
                    //Create Connection to Excel work book
                    excelConnection = new OleDbConnection(excelConnectionString);


                    //new code
                    Sheetname = DdlSheetNames.SelectedItem.Text;


                    using (OleDbConnection excel_con = new OleDbConnection(excelConnectionString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();





                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + Sheetname + "]", excel_con))
                        {


                            oda.Fill(dtExcelData);

                            Session["dtTable"] = dtExcelData;
                            gridAct.DataSource = dtExcelData;
                            gridAct.DataBind();
                            lblListShow.Text = " Total Records: " + gridAct.Rows.Count;



                        }
                        excel_con.Close();



                    }


                    ClientScript.RegisterStartupScript(this.GetType(), "popup", String.Format("<script> alert ('Data Uploaded Successfuly!!!');</script>"));
                    btnSave.Visible = true;




                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "popup", String.Format("<script> alert ('Problem in Uploading..');</script>"));
            }
            finally
            {

            }

            //Delete the Uploaded file after upload completed

            FileInfo file = new FileInfo(FilePath);
            if (file.Exists)
            {
                file.Delete();
            }
            //complete code



        }
        protected void btnSheet_Click(object sender, EventArgs e)
        {
            DdlSheetNames.DataSource = null;
            DdlSheetNames.DataBind();

            if (companyUpload.FileName != "")
            {
                string FolderName = "ExcelFiles";
                Filename = companyUpload.FileName;

                Session["Filename"] = companyUpload.FileName;

                string RightNm = Filename.Substring(Filename.LastIndexOf('.') + 1);
                int FileLen = RightNm.Length;



                string LeftNm = Filename.Substring(0, Convert.ToInt32(Filename.Length) - (FileLen + 1)).ToString();


                Filename = LeftNm + DateTime.Now.ToString("ss") + "." + RightNm;
                bool folderExists = Directory.Exists(Server.MapPath(FolderName));
                if (!folderExists)
                    Directory.CreateDirectory(Server.MapPath(FolderName));

                string path = Server.MapPath(FolderName) + "\\" + Filename;
                Session["FilePath"] = path;
                companyUpload.PostedFile.SaveAs(path);


                //get the sheet name
                //Create Connection to Excel work book
                excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Persist Security Info=False;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";

                excelConnection = new OleDbConnection(excelConnectionString);
                excelConnection.Open();
                System.Data.DataTable Sheets = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                int rowcnt = 0;
                DdlSheetNames.Items.Add("-Select-");
                foreach (DataRow dr in Sheets.Rows)
                {
                    string sht = dr[2].ToString().Replace("'", "");
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter("select * from [" + sht + "]", excelConnection);

                    rowcnt = rowcnt + 1;
                    DdlSheetNames.Items.Add(sht);
                }
                excelConnection.Close();
                //
            }

        }
        protected void cmbx_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbx_Type.SelectedItem.Text == "Journal")
            {
                Divldg.Visible = true;
            }
            else
            {
                Divldg.Visible = false;
            }
        }
    }
}