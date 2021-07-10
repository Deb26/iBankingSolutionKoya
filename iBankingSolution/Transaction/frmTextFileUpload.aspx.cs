using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using BusinessObject;
using BLL;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace iBankingSolution.Transaction
{
    public partial class frmTextFileUpload : System.Web.UI.Page
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
                dtpkr_EntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttdt.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
        }

        protected void btn_Show(object sender, EventArgs e)
        {

            string csvPath = Server.MapPath("~/UploadFiles/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(csvPath);
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string contentType = FileUpload1.PostedFile.ContentType;

            Session["FileName"] = filename;
            Session["ContentType"] = contentType;
            Session["Path"] = csvPath;

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Account No", typeof(int)),
                new DataColumn("Total Transaction", typeof(double)),
                new DataColumn("Customer Name",typeof(string)),
                new DataColumn("Balance",typeof(double)),
                new DataColumn("Collection Date",typeof(string)),
                new DataColumn("Collection",typeof(double)), });

            string csvData = File.ReadAllText(csvPath);
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;

                        
                    }
                }
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();

            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    string lbldate = Convert.ToString(row.Cells[4].Text);
            //    String[] strdt = lbldate.Split('.');
            //    DateTime Cust1 = DateTime.ParseExact(strdt[0] + "/" + strdt[1] + "/" + "20" + strdt[2], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //}
        }


        protected void btn_upload(object sender, EventArgs e)
        {
            string contentType1 = FileUpload1.PostedFile.ContentType;

            SqlConnection con = new SqlConnection(strConn);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            //assign Destination table name  
            objbulk.DestinationTableName = "tblTest";




            //string csvPath = Server.MapPath("~/UploadFiles/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            //FileUpload1.SaveAs(csvPath);
            //string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //string contentType = FileUpload1.PostedFile.ContentType;

            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Account No", typeof(int)),
            //new DataColumn("Total Transaction", typeof(double)),
            //new DataColumn("Customer Name",typeof(string)),
            //new DataColumn("Balance",typeof(double)),
            //new DataColumn("Collection Date",typeof(string)),
            //new DataColumn("Collection",typeof(double)), });

            //string csvData = File.ReadAllText(csvPath);
            //foreach (string row in csvData.Split('\n'))
            //{
            //    if (!string.IsNullOrEmpty(row))
            //    {
            //        dt.Rows.Add();
            //        int i = 0;
            //        foreach (string cell in row.Split(','))
            //        {
            //            dt.Rows[dt.Rows.Count - 1][i] = cell;
            //            i++;
            //        }
            //    }
            //}

            //GridView1.DataSource = dt;
            //GridView1.DataBind();



                objBO_Finance.Flag = 1;
                //objBO_Finance.Name = filename;
                objBO_Finance.Name = Convert.ToString(Session["FileName"]);
                //objBO_Finance.SignPath = csvPath;
                objBO_Finance.SignPath = Convert.ToString(Session["Path"]);
                //objBO_Finance.PictPath = contentType;
                objBO_Finance.PictPath = Convert.ToString(Session["ContentType"]);

                objBO_Finance.UserName = Convert.ToString(Session["UserID"]);
                string Cust = txttdt.Text;
                DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objBO_Finance.DTSCust = Cust1;


                int j = objBL_Finance.InsertFileData(objBO_Finance, out SQLError);
                if (j > 0)
                {

                    Session["fileID"] = Convert.ToString(SQLError);
                    MessageBox(this, "Record Inserted Successfully .Allotted ID Number is:-" + SQLError);


                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        string lblacno = Convert.ToString(row.Cells[0].Text).Trim() != "&nbsp;" ? Convert.ToString(row.Cells[0].Text).Trim() : "";
                        string lbltransaction = Convert.ToString(row.Cells[1].Text).Trim() != "&nbsp;" ? Convert.ToString(row.Cells[1].Text).Trim() : "";
                        string lblname = Convert.ToString(row.Cells[2].Text).Trim() != "&nbsp;" ? Convert.ToString(row.Cells[2].Text).Trim() : "";
                        string lblbalance = Convert.ToString(row.Cells[3].Text).Trim() != "&nbsp;" ? Convert.ToString(row.Cells[3].Text).Trim() : "";
                        string lbldate = Convert.ToString(row.Cells[4].Text);
                        string lblcollection = Convert.ToString(row.Cells[5].Text).Trim() != "&nbsp;" ? Convert.ToString(row.Cells[5].Text).Trim() : "";


                        objBO_Finance.Flag = 1;
                        objBO_Finance.intro_acno = lblacno;
                        objBO_Finance.Transaction = Convert.ToDouble(lbltransaction);
                        objBO_Finance.NAME = lblname;
                        objBO_Finance.Balance = Convert.ToDouble(lblbalance);

                        //string Cust = lbldate;
                        String[] strdt = lbldate.Split('.');

                        Cust1 = DateTime.ParseExact(strdt[0] + "/" + strdt[1] + "/" + "20" + strdt[2], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        string OpenDt = dtpkr_EntryDate.Text;
                        DateTime OpenDt1 = DateTime.ParseExact(OpenDt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        objBO_Finance.date_of_opening = OpenDt1;


                        objBO_Finance.CCB_DATE = Convert.ToDateTime(Cust1);

                        objBO_Finance.collection = Convert.ToDouble(lblcollection);
                        objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);



                        int k = objBL_Finance.datFILEUPLOAD(objBO_Finance, out SQLError);

                    }

                }
                else
                {

                    String message = "alert('Something Went Wrong')";
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", message, true);
                }
           
            

        }
        public static void MessageBox(System.Web.UI.Page page, string strMsg)
        {

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }
        protected void btn_Voucher(object senedr, EventArgs e)
        {
            objBO_Finance.BranchCode = Convert.ToString(Session["fileID"]);
            DataSet getdata = objBL_Finance.GetUploadFileusingAcNo(objBO_Finance, out SQLError);
            DataTable gdt = new DataTable();
            gdt = getdata.Tables[0];
            int X = gdt.Rows.Count;
            Session["dtgdata"] = gdt;

            DataTable dtt = (DataTable)Session["dtgdata"];
            if (dtt.Rows.Count > 0)
            {

                RepCCList.DataSource = dtt;
                RepCCList.DataBind();

                foreach (RepeaterItem item in RepCCList.Items)
                {
                    Label accountno = (Label)item.FindControl("lblacno");
                    string re = accountno.Text;

                    objBO_Finance.BranchCode = Convert.ToString(Session["fileID"]);
                    objBO_Finance.SL_CODE = re;
                    DataSet dtbal = objBL_Finance.GETBALANCEBYACNO(objBO_Finance, out SQLError);

                    DataTable dbal = new DataTable();
                    dbal = dtbal.Tables[0];
                    int Y = dbal.Rows.Count;
                    Session["dtttBal"] = dbal;
                    DataTable dtt1 = (DataTable)Session["dtttBal"];
                    if (dtt1.Rows.Count > 0)
                    {
                        Repeater1.DataSource = dtt1;
                        Repeater1.DataBind();

                        foreach (RepeaterItem item1 in Repeater1.Items)
                        {
                            Label lblbal = (Label)item1.FindControl("lblbal");
                            string ra2 = lblbal.Text;

                            Label lbla = (Label)item1.FindControl("lblsl");
                            string ra1 = lbla.Text;

                            

                            string Cust = dtpkr_EntryDate.Text;
                            DateTime Cust1 = DateTime.ParseExact(Cust, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            objBO_Finance.DTSCust = Cust1;
                            objBO_Finance.lf_acno = ra1;
                            objBO_Finance.Balance = Convert.ToDouble(ra2);
                            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);

                            int l = objBL_Finance.INSERTCASHIER(objBO_Finance, out SQLError);
                            if (l > 0)
                            {
                                MessageBox(this, "Record Inserted Successfully .");
                            }
                        }
                    }
                }
            }
        }
        protected void Get_File(object sender, EventArgs e)
        {

        }
    }
}