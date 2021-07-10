using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using BLL;
using BusinessObject;
using System.Reflection;

namespace iBankingSolution.Report
{
    public partial class frmPassbookPrint : System.Web.UI.Page
    {
        String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnectionStringBuilder builder;
        MyDBDataContext dbContext = new MyDBDataContext();
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void getPassbookHead()
        {

            var headdata = (from cd in dbContext.CODESANDNOs
                            select new
                            {
                                Name = cd.LISCENCEE_NAME,
                                ifs = cd.IFSCCODE,
                                micr = cd.MICRNo,
                                Regd = cd.RegdNo,
                                RegdDt = cd.RegdDate,
                                bank = cd.SocietyBankName,
                                brnch = cd.SocietyBankBranch,
                                add = cd.SocietyBankBranchAddress,
                                currac = cd.CBSACNO

                            }).SingleOrDefault();



            if (headdata != null)
            {
                SocietyName = headdata.Name;
                IFSC = headdata.ifs;
                MICR = headdata.micr;
                BankName = headdata.bank;
                BranchName = headdata.brnch;
                BranchAddress = headdata.add;
                CBSAcNo = headdata.currac;
                RegdNo = headdata.Regd;
                RegdDate = Convert.ToDateTime(headdata.RegdDt).ToShortDateString();
            }

            var cust = (from CR in dbContext.CLIENT_REGISTERs
                        join CM in dbContext.CLIENT_MASTERs on CR.CUST_ID equals CM.CUST_ID
                        join SM in dbContext.SUBLEDGER_MASTERs on CM.SL_CODE equals SM.SL_CODE
                        where CM.SL_CODE == Convert.ToInt32(txtNewAcNo.Text) && SM.ACTYPE == "s"
                        select new
                        {
                            CustName = CR.NAME,
                            father = CR.GUARDIAN_NAME,
                            vill = CR.VILL_CODE,
                            dist = CR.DIST_CODE,
                            slcode = SM.SL_CODE




                        }).ToList();

            if (cust.Count > 0)
            {
                CustomerName = cust[0].CustName;
                GurdianName = cust[0].father;
                Village = cust[0].vill;
                Dist = cust[0].dist;
                Mobile = "";
                SocietyAcNo = Convert.ToString(cust[0].slcode);


            }


        }
        String SocietyName = "";
        String CustomerName = "";
        String GurdianName = "";
        String Village = "";
        String Dist = "";
        String Mobile = "";
        String IFSC = "";
        String RegdNo = "";
        String RegdDate;
        String MICR = "";
        String SocietyAcNo;
        String BranchName = "";
        String BranchAddress = "";
        String CBSAcNo = "";
        String BankName = "";
        String transactionDate;
        Decimal Debitamount;
        Decimal Creditamount;
        String narattion = "";
        DateTime PassbookPrintDate;
        Int32 Linenum = 0;

        protected void getPassbookTransaction()
        {
            //var PasprintDt = (from cod in dbContext.CODESANDNOs

            //                  select new
            //                  {
            //                      pbkPrintDt = cod.PRINTPASSBKDATE

            //                  }).SingleOrDefault();

            //var cust = (from trns in dbContext.TRANS_VIEWs

            //            where trns.acno == Convert.ToInt32(txtNewAcNo.Text)
            //            && trns.dt >= vfopdt && trns.dt <= System.DateTime.Now
            //            orderby trns.dt
            //            select new
            //            {

            //                transdt = trns.dt,
            //                debitamt = trns.debit,
            //                creditamt = trns.credit,
            //                nar = trns.narration


            //            }).ToList();

            //if (cust.Count > 0)
            //{
            //    transactionDate = Convert.ToDateTime(cust[0].transdt).ToString("dd/MM/yyyy");
            //    Debitamount = Convert.ToDecimal(cust[0].debitamt);
            //    Creditamount = Convert.ToDecimal(cust[0].creditamt);
            //    narattion = Convert.ToString(cust[0].nar);
            //}

        }
        protected void btnPrintLicense_Click(object sender, EventArgs e)
        {

            getPassbookHead();



            //Creating the datatable from the above strings

            DataTable dtPrint = new DataTable();

            dtPrint.Columns.Add("SocietyName", typeof(string));
            dtPrint.Columns.Add("CustomerName", typeof(string));
            dtPrint.Columns.Add("GurdianName", typeof(string));
            dtPrint.Columns.Add("Village", typeof(string));
            dtPrint.Columns.Add("Dist", typeof(string));
            dtPrint.Columns.Add("Mobile", typeof(string));
            dtPrint.Columns.Add("IFSC", typeof(string));
            dtPrint.Columns.Add("RegdNo", typeof(string));
            dtPrint.Columns.Add("RegdDate", typeof(string));
            dtPrint.Columns.Add("MICR", typeof(string));
            dtPrint.Columns.Add("BankName", typeof(string));
            dtPrint.Columns.Add("BranchName", typeof(string));
            dtPrint.Columns.Add("BranchAddress", typeof(string));
            dtPrint.Columns.Add("BranchPO", typeof(string));
            dtPrint.Columns.Add("BranchDist", typeof(string));
            dtPrint.Columns.Add("BranchPin", typeof(string));
            dtPrint.Columns.Add("SocietyAcNo", typeof(string));
            dtPrint.Columns.Add("CBSAcNo", typeof(string));

            //////// Create a DataRow  and add to the DataTable
            DataRow dr = dtPrint.NewRow();
            dr["SocietyName"] = SocietyName;
            dr["CustomerName"] = CustomerName;
            dr["GurdianName"] = GurdianName;
            dr["Village"] = Village;
            dr["Dist"] = Dist;
            dr["Mobile"] = Mobile;
            dr["IFSC"] = IFSC;
            dr["RegdNo"] = RegdNo;
            dr["RegdDate"] = RegdDate;
            dr["MICR"] = MICR;
            dr["BankName"] = BankName;
            dr["BranchName"] = BranchName;
            dr["BranchAddress"] = BranchAddress;
            dr["BranchPO"] = "";
            dr["BranchDist"] = "";
            dr["BranchPin"] = "";
            dr["SocietyAcNo"] = SocietyAcNo;
            dr["CBSAcNo"] = CBSAcNo;

            dtPrint.Rows.Add(dr);


            builder = new SqlConnectionStringBuilder(strConn);
            ReportDocument crystalReport = new ReportDocument();
            DataTable dt = new DataTable();
            dt = dtPrint;


            if (dt.Rows.Count > 0)
            {

                Response.Clear();
                ReportDocument rpt = new ReportDocument();



                //////////rpt.Load(Server.MapPath(@"~/Report/CrystalReports/rptPassbookHeading.rpt"));
                ////////rpt.Load(Server.MapPath(@"~/Report/CrystalReports/rptPassbookHeading.rpt"));
                ////////rpt.FileName = Server.MapPath(@"~/Report/CrystalReports/rptPassbookHeading.rpt");

                ////////rpt.SetDataSource(dt);
                ////////CrystalReportViewer1.ReportSource = rpt;
                ////////CrystalReportViewer1.RefreshReport();


                //following code is commented for download option if required
                Response.Clear();
                crystalReport = new ReportDocument();

                crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptPassbookHeading.rpt"));
                crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptPassbookHeading.rpt");

                crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                crystalReport.SetDataSource(dt);

                crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "Passheading");
                //crystalReport.PrintOptions.PrinterName = "your printer name";
                crystalReport.PrintToPrinter(1, true, 0, 0);
                //crystalReport.PrintToPrinter(nCopies, true, startPageN, endPageN);
                Response.End();
             
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);

            }



        }

        protected void btnPrintPassbook_Click(object sender, EventArgs e)
        {

            //GeneratePassBookPrint();

            var PasprintDt = (from cod in dbContext.CODESANDNOs

                              select new
                              {
                                  pbkPrintDt = cod.PRINTPASSBKDATE

                              }).SingleOrDefault();

            if (PasprintDt != null)
            {
                PassbookPrintDate = Convert.ToDateTime(PasprintDt.pbkPrintDt);
            }
            //getPassbookTransaction();
            objBO_Finance.SL_CODE = txtNewAcNo.Text.Trim();


            //DateTime timedb = DateTime.ParseExact(PassbookPrintDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //objBO_Finance.DATE_UPTO = timedb;

            objBO_Finance.DATE_UPTO = PassbookPrintDate;

            DataTable dset = objBL_Finance.GetPassbookTransaction(objBO_Finance);
            //DataTable dt1 = new DataTable();
            //DataTable dt2 = new DataTable();
            //dt1 = dset.Tables[0];
            //dt2 = dset.Tables[1];


            //if (dset.Rows.Count > 0)
            //{
            //    //transactionDate = Convert.ToDateTime(dset.Rows[0]["DT"]).ToString("dd/MM/yyyy");
            //    //Debitamount = Convert.ToDecimal(dset.Rows[0]["DEBIT"]);
            //    //Creditamount = Convert.ToDecimal(dset.Rows[0]["CREDIT"]);
            //    //narattion = Convert.ToString(dset.Rows[0]["NARRATION"]);
            //    //Linenum = Convert.ToInt32(dset.Rows[0]["LineNum"]);

            //    //gridPassBk.DataSource = dset;
            //    //gridPassBk.DataBind();

            //}

            builder = new SqlConnectionStringBuilder(strConn);
            ReportDocument crystalReport = new ReportDocument();

            if (dset.Rows.Count > 0)
            {
                //    gridPassBk.DataSource = dset;
                //    gridPassBk.DataBind();
                //    //Response.Clear();
                //}
                //ReportDocument rpt = new ReportDocument();


                ////////////rpt.Load(Server.MapPath(@"~/Report/CrystalReports/rptPassbookHeading.rpt"));
                //////////rpt.Load(Server.MapPath(@"~/Report/CrystalReports/rptPassbookHeading.rpt"));
                //////////rpt.FileName = Server.MapPath(@"~/Report/CrystalReports/rptPassbookHeading.rpt");

                //////////rpt.SetDataSource(dt);
                //////////CrystalReportViewer1.ReportSource = rpt;
                //////////CrystalReportViewer1.RefreshReport();


                //  following code is commented for download option if required
                Response.Clear();
                crystalReport = new ReportDocument();

                crystalReport.Load(Server.MapPath(@"~/Report/CrystalReports/rptPassbookTransaction.rpt"));
                crystalReport.FileName = Server.MapPath(@"~/Report/CrystalReports/rptPassbookTransaction.rpt");

                crystalReport.SetDatabaseLogon(builder.UserID, builder.Password, builder.InitialCatalog, builder.DataSource);
                //crystalReport.Database.Tables[0].SetDataSource(dt1);
                //crystalReport.Database.Tables[1].SetDataSource(dt2);

                crystalReport.SetDataSource(dset);

                crystalReport.ExportToHttpResponse(ExportFormatType.RichText, Response, true, "PassTrans");
                //crystalReport.PrintOptions.PrinterName = "your printer name";
                crystalReport.PrintToPrinter(1, true, 0, 0);
                Response.End();
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);

            }



        }

        protected void txtOldAcNo_TextChanged(object sender, EventArgs e)
        {
            var acno = (from sm in dbContext.SUBLEDGER_MASTERs
                        where sm.OLD_ACNO == Convert.ToString(txtOldAcNo.Text)
                        select new
                        {
                            AcNo = sm.SL_CODE

                        }).SingleOrDefault();

            if (acno != null)
            {
                txtNewAcNo.Text = Convert.ToString(acno.AcNo);
            }
        }

        protected void txtNewAcNo_TextChanged(object sender, EventArgs e)
        {
            var OLDAcno = (from sm in dbContext.SUBLEDGER_MASTERs
                           where sm.SL_CODE == Convert.ToDecimal(txtNewAcNo.Text)
                           select new
                           {
                               AcNo = sm.OLD_ACNO

                           }).SingleOrDefault();

            if (OLDAcno != null)
            {
                txtOldAcNo.Text = Convert.ToString(OLDAcno.AcNo);
            }


        }


        Int32 Line = 0;
        DateTime PDate;
        Double bala = 0;
        Int32 n = 0;
        Double Tot = 0;
        Int32 TotRows = 0;
        DateTime PassbookPrintdate;


        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }
        protected void GeneratePassBookPrint()
        {

            //SqlConnection conn = new SqlConnection(strConn);
            //SqlDataAdapter da = new SqlDataAdapter();
            //SqlCommand cmd = conn.CreateCommand();
            //string SQL = "select * from ps_print where sl_code=" + txtNewAcNo.Text;
            //cmd.CommandText = SQL;
            //da.SelectCommand = cmd;
            //DataSet ds = new DataSet();

            /////conn.Open();
            //da.Fill(ds);
            ///conn.Close();

            //return ds;
            String cr = "";
            var Pasprint = (from cod in dbContext.PS_PRINTs
                            where cod.SL_CODE == Convert.ToDecimal(txtNewAcNo.Text)
                            group cod by 1 into g
                            select new
                            {

                                line = g.Max(x => x.LINE),
                                PDt = g.Max(x => x.P_DATE),
                                T = g.Max(x => x.T),
                                b = g.Max(x => x.BAL)
                            }).FirstOrDefault();

            if (Pasprint != null)
            {
                Line = Convert.ToInt32(Pasprint.line) + 1;
                PDate = Convert.ToDateTime(Pasprint.PDt);

                n = 2;
                bala = Convert.ToDouble(Pasprint.b);
                Tot = bala;
                TotRows = TotRows + Line - 1;

            }

            else

            {
                n = 1;
                var SubM = (from sm in dbContext.SUBLEDGER_MASTERs
                            where sm.SL_CODE == Convert.ToDecimal(txtNewAcNo.Text)
                            select new
                            {
                                PDt = sm.DATE_OF_OPENING,
                            }).SingleOrDefault();

                if (SubM != null)
                {
                    Line = 36;



                    PDate = Convert.ToDateTime(SubM.PDt);
                }

            }

            //Section 2 Printing the Details

            //Get he Passbook Print date from the codesandnos table
            var Pasprintdate = (from cods in dbContext.CODESANDNOs

                                select new
                                {

                                    PPDT = cods.PRINTPASSBKDATE

                                }).SingleOrDefault();

            if (Pasprintdate != null)
            {

                PassbookPrintdate = Convert.ToDateTime(Pasprintdate.PPDT);
            }
            //Getting the Act_Op_cr and Act_op_dr from the subledger_master table
            var SubMa = (from sm in dbContext.SUBLEDGER_MASTERs
                         where sm.SL_CODE == Convert.ToDecimal(txtNewAcNo.Text)
                         select new
                         {


                             OptCR = sm.ACT_OP_CR,
                             OptDR = sm.ACT_OP_DR

                         }).SingleOrDefault();

            if (SubMa != null)
            {
                if (n == 1)
                {

                    bala = Convert.ToDouble(SubMa.OptCR) - Convert.ToDouble(SubMa.OptDR);
                    Tot = bala;
                }
            }


            //Selecting the details from the trans_view

            var Trans = (from TR in dbContext.TRANS_VIEWs
                         where TR.acno == Convert.ToDecimal(txtNewAcNo.Text)
                         && TR.dt < PassbookPrintdate
                         group TR by 1 into g
                         select new
                         {
                             TotalCr = g.Sum(x => x.credit),
                             TotalDr = g.Sum(x => x.debit)
                         }).FirstOrDefault();

            if (Trans != null)
            {
                if (n == 1)
                {


                    bala = Convert.ToDouble(Trans.TotalCr) - Convert.ToDouble(Trans.TotalDr);
                    Tot = bala;
                }
            }

            if (Line == 35)
            {
                TotRows = TotRows + 1;
                Double tt = Convert.ToDouble(gridPassBk.Rows) + 1;
                Line = Line + 1;
            }
            if (bala < 0)
            {
                cr = "Dr.";
            }
            else if (bala > 0)
            {
                cr = "Cr.";
            }
            else
            {
                cr = "-";
            }
            //gridPassBk.Rows[TotRows + 1].Cells[5].Text = "Carried Forward :";
            //gridPassBk.Rows[TotRows + 1].Cells[6].Text = bala + cr;


            if (Line == 36)
            {
                //string Id = GridView1.DataKeys[TotRows %= GridView1.PageSize][0].ToString();

                //GridView1.Rows[TotRows + 1].Cells[0].Text = "SLNo";
                //GridView1.Rows[TotRows + 1].Cells[1].Text = "DATE";
                //GridView1.Rows[TotRows + 1].Cells[2].Text = "NARRATION";
                //GridView1.Rows[TotRows + 1].Cells[3].Text = "WITHDRAWALS";
                //GridView1.Rows[TotRows + 1].Cells[4].Text = "DEPOSIT";
                //GridView1.Rows[TotRows + 1].Cells[5].Text = "BALANCE";
                //GridView1.Rows[TotRows + 1].Cells[6].Text = "";
                Line = 1;
                TotRows = TotRows + 1;
                TotRows = TotRows + 1;
                if (bala < 0)
                {
                    cr = "Dr.";
                }
                else if (bala > 0)
                {
                    cr = "Cr.";
                }
                else
                {
                    cr = "-";
                }

                //gridPassBk.Rows[TotRows].Cells[4].Text = "Borrowed Forward :";
                //gridPassBk.Rows[TotRows].Cells[6].Text = bala + cr;
            }

            var Transv = (from TRV in dbContext.TRANS_VIEWs
                          where TRV.acno == Convert.ToDecimal(txtNewAcNo.Text)
                          && TRV.dt >= PassbookPrintdate && TRV.dt <= System.DateTime.Now
                          orderby TRV.dt
                          select new
                          {

                              dtt = TRV.dt,
                              dr = TRV.debit,
                              cr = TRV.credit,
                              narration = TRV.narration,
                              et = TRV.et,
                              pp = TRV.P,
                              eti = TRV.eti,
                              tt = TRV.tt

                          }).ToList();

            if (Transv != null)
            {

                DataTable dt = LINQResultToDataTable(Transv);

                for (int i = 1; i <= dt.Rows.Count; i++)

                {

                    if (Convert.ToString(dt.Rows[0]["pp"]) == "y")
                    {
                        if (Line == 17)
                        {
                            TotRows = TotRows + 3;
                            //grdExport.Columns[9].ItemStyle.Width = 1000;

                            Line = Line + 2;
                        }
                        else
                        {
                            TotRows = TotRows + 1;
                        }

                        Label lblTransDate = (Label)gridPassBk.Rows[i].FindControl("lblTransDate");

                        lblTransDate.Text = Convert.ToString(dt.Rows[0]["dtt"]);

                        DateTime d = Convert.ToDateTime(dt.Rows[0]["dtt"]);


                    }
                    GridView1.Rows[i].Cells[1].Text = Convert.ToString(dt.Rows[0]["dtt"]);

                    if (Convert.ToString(dt.Rows[0]["tt"]) == "j")

                        if (Convert.ToString(dt.Rows[0]["narration"]) != "")

                            GridView1.Rows[i].Cells[2].Text = Convert.ToString(dt.Rows[0]["narration"]);
                        else
                            GridView1.Rows[i].Cells[2].Text = Convert.ToString(dt.Rows[0]["et"]) + " " + Convert.ToString(dt.Rows[0]["narration"]);

                    else if (Convert.ToString(dt.Rows[0]["tt"]) == "ccb")

                        GridView1.Rows[i].Cells[2].Text = Convert.ToString(dt.Rows[0]["it"]) + " " + Convert.ToString(dt.Rows[0]["narration"]);
                    else if (Convert.ToString(dt.Rows[0]["tt"]) == "cr")

                        GridView1.Rows[i].Cells[2].Text = "Cheque No : " + " " + Convert.ToString(dt.Rows[0]["narration"]);
                    else if (Convert.ToString(dt.Rows[0]["tt"]) == "ji")

                        GridView1.Rows[i].Cells[2].Text = "Interest Transferred For FD & MIS & QIS A/cs";
                    else if (Convert.ToString(dt.Rows[0]["tt"]) == "jc")

                        GridView1.Rows[i].Cells[2].Text = "Cheque Dishonour Charges Paid";



                    GridView1.Rows[i].Cells[3].Text = Convert.ToString(dt.Rows[0]["debit"]);
                    GridView1.Rows[i].Cells[4].Text = Convert.ToString(dt.Rows[0]["credit"]);




                }




            }
            //Trans_view details complete

            //Printing Details complete




        }

        protected void gridPassBk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                 
                Label lblUnitQuantity = (Label)e.Row.FindControl("lblUnitQuantity");
                 
            }
        }
    }
}