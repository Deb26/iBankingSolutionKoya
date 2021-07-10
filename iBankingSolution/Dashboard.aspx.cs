using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization;
using BusinessObject;
using BLL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using iBankingSolution.Report.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;


namespace iBankingSolution
{
    public partial class Dashboard : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        //ReportDocument rpt = new ReportDocument();

        private SqlConnection con;
        private SqlCommand com;
        private string constr, query;
        private void connection()
        {
            constr = ConfigurationManager.ConnectionStrings["DBConnect"].ToString();
            con = new SqlConnection(constr);
            con.Open();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //BindGrid();
            //BindGridTwo();
            BindTotalDeposit();
            BindTotalLoan();
            BindTotalInvestment();
            //ShowDashboardDetails();
            if (!IsPostBack)
            {
               // Bindchart();

            }
        }

        protected void BindTotalDeposit()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            DataSet dt = objBL_Finance.DashboardReportSecond(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                Repeater2.DataSource = dt;
                Repeater2.DataBind();

            }
        }
        
        protected void BindTotalLoan()
        {
            objBO_Finance.Flag = 2;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            DataSet dt = objBL_Finance.DashboardReportSecond(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                Repeater4.DataSource = dt;
                Repeater4.DataBind();

            }
        }

        protected void BindTotalInvestment()
        {
            objBO_Finance.Flag = 3;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            DataSet dt = objBL_Finance.DashboardReportSecond(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                Repeater5.DataSource = dt;
                Repeater5.DataBind();

            }
        }
        protected void BindGrid()
        {
            objBO_Finance.Flag = 2;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            DataSet dt = objBL_Finance.DashboardReportfirst(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                RepCCList.DataSource = dt;
                RepCCList.DataBind();

            }
            else
            {
                RepCCList.DataSource = null;
                RepCCList.DataBind();
            }
        }

        protected void BindGridTwo()
        {
            objBO_Finance.Flag = 1;
            objBO_Finance.BranchCode = Convert.ToString(Session["BranchID"]);
            DataSet dt = objBL_Finance.DashboardReportSecond(objBO_Finance, out SQLError);
            if (dt.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

            }
        }

        protected void ShowDashboardDetails()
        {
            try
            {
                DataSet1 ds = new DataSet1();

                DataTable dt = ds.Ds_CashAccount;
                objBO_Finance.Flag = 1;

                DataTable dtCashAccount = objBL_Finance.DashboardReport(objBO_Finance, out SQLError);
                for (int i = 0; i < dtCashAccount.Rows.Count; i++)
                {
                    dt.Rows.Add
                               (

                                   dtCashAccount.Rows[i]["Account_Number"],
                                   dtCashAccount.Rows[i]["Name"],
                                   dtCashAccount.Rows[i]["Int_Amt"],
                                   dtCashAccount.Rows[i]["Tran_SAc"]
                                   
                               );
                }

                //GVRepayHist.DataSource = dt;
                //GVRepayHist.DataBind();
                
                
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        private void Bindchart()
        {
            //connection();
            //com = new SqlCommand("evantagebsa.proc_ActypeWisePieChar", con);
            //com.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter da = new SqlDataAdapter(com);
            //DataSet ds = new DataSet();
            //da.Fill(ds);

            //DataTable ChartData = ds.Tables[0];

            ////storing total rows count to loop on each Record  
            //string[] XPointMember = new string[ChartData.Rows.Count];
            //int[] YPointMember = new int[ChartData.Rows.Count];

            //for (int count = 0; count < ChartData.Rows.Count; count++)
            //{
            //    //storing Values for X axis  
            //    XPointMember[count] = ChartData.Rows[count]["Actype"].ToString();
            //    //storing values for Y Axis  
            //    YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["Cnt"]);

            //}
            ////binding chart control  
            //Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            ////Setting width of line  
            //Chart1.Series[0].BorderWidth = 10;
            ////setting Chart type   
            //Chart1.Series[0].ChartType = SeriesChartType.Bar;


            //foreach (Series charts in Chart1.Series)
            //{
            //    foreach (DataPoint point in charts.Points)
            //    {
            //        switch (point.AxisLabel)
            //        {
            //            case "Q1": point.Color = Color.RoyalBlue; break;
            //            case "Q2": point.Color = Color.SaddleBrown; break;
            //            case "Q3": point.Color = Color.SpringGreen; break;

            //        }
            //        point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

            //    }
            //}


            ////Enabled 3D
            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            //con.Close();

        }
    }
    
}
