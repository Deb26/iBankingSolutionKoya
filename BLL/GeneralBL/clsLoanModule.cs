using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BusinessObject;
using BLL;
using System.Collections;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace BLL.GeneralBL
{
    //public class clsLoanModule
    //{
    //    public int vfToTEmi;
    //    public Int32 vfloantotp;
    //    public Double vfloanodint;
    //    public Double vfloancurrint;
    //    public Double vfloanintodint;
    //    public Double vfloanintcurrint;
    //    public Double vfloandemand;
    //    public Double vfloantotod;
    //    public Double vfloantotcurr;
    //    public DateTime vf_date_from_loan;
    //    public DateTime vf_date_from_loan1;
    //    public DateTime vf_date_from_loan2;
    //    public DateTime vflast_trans_date_loan;
    //    public DateTime vflast_DISB_date_loan;
    //    public String vfodpr;

    //    double totb, totp, vfopbal, FindAcBalLoan, calcODPRval, vfrep;
    //    String strConnString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;

    //    private void getCurrPrin(double vfinstamt, int vfNO_OF_INST, string vfodpr, DateTime vfappldt, DateTime vflastloandt, int insprd1, DateTime VFLASTDT)
    //    {
    //        int m;

    //        m = (vfappldt.Subtract(vflastloandt).Days / (Convert.ToInt32(365.25) / 12)) / insprd1;
    //        ////m = Convert.ToInt32(((vfappldt - vflastloandt).Days) / (double)insprd1);
    //        if (Convert.ToDateTime(vfappldt).Day > Convert.ToDateTime(vflastloandt).Day)
    //            m = m - 1;
    //        vfloantotcurr = (Convert.ToInt32(vfNO_OF_INST) - m) * vfinstamt;
    //        if (vfloantotcurr < 0)
    //            vfloantotcurr = 0;
    //        vfloantotod = vfloantotp - vfloantotcurr;
    //        if (vfloantotcurr > vfloantotp)
    //            vfloantotcurr = vfloantotp;
    //        if (vfloantotcurr < 0)
    //            vfloantotcurr = 0;
    //        vfloantotod = vfloantotp - vfloantotcurr;

    //        if (vflastloandt > VFLASTDT)
    //        {
    //            vfloantotcurr = 0;
    //            vfloantotod = vfloantotp;
    //        }

    //        if (vfodpr == "NO OD")
    //        {
    //            vfloantotcurr = vfloantotp;
    //            vfloantotod = 0;
    //        }
    //        else if (vfodpr == "AFTER LAST REPAY DATE" | vfodpr == "MONTHLY WISE")
    //        {
    //            if (vflastloandt > VFLASTDT)
    //            {
    //                vfloantotcurr = 0;
    //                vfloantotod = vfloantotp;
    //            }
    //            else
    //            {
    //                vfloantotcurr = vfloantotp;
    //                vfloantotod = 0;
    //            }
    //        }

    //        if (vfloantotcurr < 0)
    //        {
    //            vfloantotcurr = 0;
    //            vfloantotod = vfloantotp;
    //        }
    //    }

    //    public double calcODPR(long sl_code, DateTime dt, string odpr)
    //    {
    //        int insprd1;


    //        Int32 vfnetloan = 0;
    //        Int32 vfinstamt = 0;
    //        Int32 vfroi = 0;
    //        Decimal vfodroi = 0;
    //        String vfrepmode = "";
    //        String vftype = "";
    //        DateTime vflwdt;
    //        DateTime VFLASTDT;
    //        DateTime vfappldt;
    //        Int32 vfNO_OF_INST = 0;
    //        Int32 vfday = 0;
    //        Int32 vftotl = 0;
    //        String schemecode = "";


    //        SqlConnection conn = new SqlConnection(strConnString);
    //        SqlCommand cmdf = new SqlCommand("SELECT * FROM dbo.Find_Ac_Bal_Loan(@SL_code,@dt)", conn);
    //        //// cmd.CommandType=CommandType.StoredProcedure;  
    //        cmdf.Parameters.AddWithValue("@SL_CODE", "SL_code");
    //        cmdf.Parameters.AddWithValue("@AsOnDate", "dt");
    //        SqlDataAdapter daf = new SqlDataAdapter(cmdf);
    //        DataTable dtfun = new DataTable();
    //        conn.Open();
    //        daf.Fill(dtfun);
    //        conn.Close();
    //        if (dtfun.Rows.Count > 0)
    //        {
    //            vfloantotp = Convert.ToInt32(dtfun.Rows[0][0]);
    //        }
    //        else
    //        {
    //            vfloantotp = 0;
    //        }

    //        insprd1 = 1;
    //        if (vfodpr == "MONTHLY")
    //            insprd1 = 1;
    //        else if (vfodpr == "18 MONTH")
    //            insprd1 = 18;
    //        else if (vfodpr == "QUARTERLY")
    //            insprd1 = 3;
    //        else if (vfodpr == "HALF YEARLY")
    //            insprd1 = 6;
    //        else if (vfodpr == "YEARLY")
    //            insprd1 = 12;


    //        SqlConnection con = new SqlConnection(strConnString);
    //        SqlCommand cmd = new SqlCommand("select * FROM SUBLEDGER_DETAILS_LOAN where SL_CODE=" + sl_code, con);
    //        cmd.CommandType = CommandType.Text;

    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        try
    //        {
    //            con.Open();
    //            da.Fill(ds);

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                vfnetloan = Convert.ToInt32(ds.Tables[0].Rows[0]["NET_LOAN"]);
    //                vfinstamt = Convert.ToInt32(ds.Tables[0].Rows[0]["INST_AMOUNT"]);
    //                vfroi = Convert.ToInt32(ds.Tables[0].Rows[0]["roi"]);
    //                vfodroi = Convert.ToInt32(ds.Tables[0].Rows[0]["od_roi"]);
    //                vfrepmode = Convert.ToString(ds.Tables[0].Rows[0]["repay_mode"]);
    //                vftype = Convert.ToString(ds.Tables[0].Rows[0]["Type"]);
    //                vflwdt = Convert.ToDateTime(ds.Tables[0].Rows[0]["last_rep_dt"]);
    //                VFLASTDT = Convert.ToDateTime(ds.Tables[0].Rows[0]["last_rep_dt"]);

    //                vfappldt = Convert.ToDateTime(ds.Tables[0].Rows[0]["sanc_dt"]);
    //                if (vfappldt == DateTime.MinValue)

    //                {
    //                    vfappldt = Convert.ToDateTime(ds.Tables[0].Rows[0]["appl_dt"]);
    //                    vftotl = Convert.ToInt32(ds.Tables[0].Rows[0]["loan_amnt"]);
    //                }
    //                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["INST_ST_DATE"]) != DateTime.MinValue)
    //                {
    //                    DateTime vfinsdt = Convert.ToDateTime(ds.Tables[0].Rows[0]["INST_ST_DATE"]);
    //                    Int32 vfinstap = Convert.ToInt32(ds.Tables[0].Rows[0]["INST_appl"]);
    //                }

    //                schemecode = Convert.ToString(ds.Tables[0].Rows[0]["scheme_code"]);
    //                vfNO_OF_INST = Convert.ToInt32(ds.Tables[0].Rows[0]["no_of_inst"]);
    //            }
    //            con.Close();
    //        }
    //        catch (Exception ex)
    //        {

    //            string msg = ex.Message;
    //        }



    //        if (vfNO_OF_INST != 0)
    //            vfinstamt = vfnetloan / vfNO_OF_INST;
    //        else
    //        {
    //            if (vfinstamt == 0)
    //                vfinstamt = vfnetloan;
    //            vfNO_OF_INST = vfnetloan / vfinstamt;

    //            if (vfNO_OF_INST == 0)
    //                vfNO_OF_INST = 1;
    //        }
    //        vfday = vfloantotp / (vfinstamt);

    //        vfday = vfday * insprd1;
    //        calcODPRval = vfday;
    //        return calcODPRval;
    //    }

    //    public void gFindLoanDetails(long sl_code, DateTime dt)
    //    {
             
    //        vflast_trans_date_loan = vf_date_from_loan;

    //        vflast_DISB_date_loan = vflast_trans_date_loan;
    //        vf_date_from_loan2 = vf_date_from_loan;


    //        SqlConnection conn = new SqlConnection(strConnString);

    //        SqlCommand cmddisdt = new SqlCommand("Proc_Maxloan_disb_det", conn);
    //        cmddisdt.CommandType = CommandType.StoredProcedure;
    //        cmddisdt.Parameters.AddWithValue("@SL_CODE", "SL_code");
    //        cmddisdt.Parameters.AddWithValue("@AsOnDate", "dt");
    //        SqlDataAdapter dadisdt = new SqlDataAdapter(cmddisdt);
    //        DataTable dsdisdt = new DataTable();
    //        conn.Open();
    //        dadisdt.Fill(dsdisdt);
    //        conn.Close();
    //        if (dsdisdt.Rows.Count > 0)
    //        {
    //            DateTime dtt = Convert.ToDateTime(dsdisdt.Rows[0]["disb_date"]);
    //            if (dtt != DateTime.MinValue)
    //            {
    //                if ((dtt > (DateTime)vflast_trans_date_loan))
    //                {
    //                    vflast_trans_date_loan = dtt.AddDays(0);
    //                    vflast_DISB_date_loan = dtt.AddDays(0);
    //                }

    //            }


    //        }


    //        SqlCommand cmddisrp = new SqlCommand("Proc_Maxloan_repay_det", conn);
    //        cmddisrp.CommandType = CommandType.StoredProcedure;
    //        cmddisrp.Parameters.AddWithValue("@SL_CODE", "SL_code");
    //        cmddisrp.Parameters.AddWithValue("@AsOnDate", "dt");
    //        SqlDataAdapter darp = new SqlDataAdapter(cmddisrp);
    //        DataTable dtrp = new DataTable();
    //        conn.Open();
    //        darp.Fill(dtrp);
    //        conn.Close();
    //        if (dtrp.Rows.Count > 0)
    //        {
    //            DateTime dtt = Convert.ToDateTime(dtrp.Rows[0]["rep_date"]);
    //            if (dtt != DateTime.MinValue)
    //            {
    //                if ((dtt > (DateTime)vflast_trans_date_loan))
    //                {
    //                    vflast_trans_date_loan = dtt;

    //                }

    //            }


    //        }


    //        vfloancurrint = 0;
    //        vfloandemand = 0;
    //        vfloanintcurrint = 0;
    //        vfloanintodint = 0;
    //        vfloanodint = 0;
    //        vfloantotcurr = 0;
    //        vfloantotod = 0;
    //        Find_Current_Loan_Amount(sl_code, dt);
    //        if (vfloantotp == 0)
    //        {
    //            vfloancurrint = 0;
    //            vfloandemand = 0;
    //            vfloanintcurrint = 0;
    //            vfloanintodint = 0;
    //            vfloanodint = 0;
    //            vfloantotcurr = 0;
    //            vfloantotod = 0;

    //            return;
    //        }
    //        Find_Demand_Balance_Loan_Account(sl_code, dt);
    //        vfloancurrint = Round(vfloancurrint); // + vfloanintcurrint
    //        vfloanodint = Round(vfloanodint); // + vfloanintodint
    //        vfloandemand = vfloantotod;
    //    }

    //    private void Calc_Monthly_Wise(long sl_code, DateTime dt, int insprd, string inttype)
    //    {
    //        ADODB.Recordset rs_op = new ADODB.Recordset();
    //        ADODB.Recordset rs_disb = new ADODB.Recordset();
    //        ADODB.Recordset rs_sdl = new ADODB.Recordset();
    //        ADODB.Recordset rs_rep = new ADODB.Recordset();
    //        ADODB.Recordset rs_dem = new ADODB.Recordset();
    //        DateTime vfldt;
    //        double vfdisbamt;
    //        double vfrepamt;
    //        double vfnetloan;
    //        double vfinstamt;
    //        double vfroi;
    //        double vfodroi;
    //        string vfrepmode;
    //        DateTime vflwdt;
    //        DateTime vfinsdt;
    //        DateTime vfdfr;
    //        DateTime vfdemdt;
    //        double vfnoins;
    //        double vfnodem;
    //        double vftotdays;
    //        double vfprinod;
    //        DateTime vfoddt;
    //        DateTime dt_from;
    //        string vftype;
    //        DateTime dt_to;
    //        double paidloan;
    //        ADODB.Recordset rs = new ADODB.Recordset();
    //        int j;
    //        DateTime vflastloandt;
    //        DateTime vfloancheckdt;
    //        int term;
    //        int duedays;
    //        double totodpm;
    //        int schemecode;
    //        int totd;
    //        int totm;
    //        int mondiff;
    //        // '''''''''''''''''''''''''''''''''LOAN DETAILS '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    //        rs_sdl.Open("select * FROM SUBLEDGER_DETAILS_LOAN where SL_CODE=" + Conversion.Val(sl_code), con, adOpenDynamic, adLockReadOnly);
    //        if (!rs_sdl.EOF)
    //        {
    //            vfnetloan = FillNum(rs_sdl.NET_LOAN);
    //            vfinstamt = FillNum(rs_sdl.INST_AMOUNT);
    //            vfroi = FillNum(rs_sdl.roi);
    //            vfodroi = FillNum(rs_sdl.od_roi);
    //            vfrepmode = FillText(rs_sdl.repay_mode);
    //            vftype = FillText(rs_sdl.Type);
    //            vflwdt = FillText(rs_sdl.last_rep_dt);
    //            VFLASTDT = FillText(rs_sdl.last_rep_dt);
    //            vfappldt = FillText(rs_sdl.sanc_dt);
    //            if (vfappldt == "")
    //                vfappldt = FillText(rs_sdl.appl_dt);
    //            vftotl = FillNum(rs_sdl.loan_amnt);

    //            if (rs_sdl.INST_ST_DATE != "")
    //            {
    //                vfinsdt = FillText(rs_sdl.INST_ST_DATE);
    //                vfinstap = FillText(rs_sdl.INST_appl);
    //            }

    //            schemecode = FillText(rs_sdl.scheme_code);
    //            VFKAMALNO_OF_INST = FillNum(rs_sdl.no_of_inst);
    //        }
    //        rs_sdl.Close();


    //        rs_sdl.Open("Select * from loan_newdisb where sl_code=" + Conversion.Val(sl_code) + " and disb_date= (select max(disb_date) from  loan_newdisb where sl_code=" + Conversion.Val(sl_code) + " and last_due_date>'" + Strings.Format(dt, "dd-mmm-yyyy") + "')", con, adOpenDynamic, adLockReadOnly);
    //        if (rs_sdl.EOF == false)
    //        {
    //            vfappldt = FillText(rs_sdl.disb_date);
    //            VFLASTDT = FillText(rs_sdl.last_due_date);
    //            vfnetloan = FillNum(rs_sdl.sanc_amt);
    //        }
    //        rs_sdl.Close();
    //        if (Conversion.Val(vfinstamt) != 0)
    //            VFKAMALNO_OF_INST = Round(vfnetloan / vfinstamt);
    //        if (Val(VFKAMALNO_OF_INST) != 0)
    //            vfinstamt = Round(vfnetloan / VFKAMALNO_OF_INST);
    //        else
    //        {
    //            if (Conversion.Val(vfinstamt) == 0)
    //                vfinstamt = vfnetloan;
    //            VFKAMALNO_OF_INST = Conversion.Int(vfnetloan / vfinstamt);

    //            if (VFKAMALNO_OF_INST == 0)
    //                VFKAMALNO_OF_INST = 1;
    //        }

    //        vf_date_from_loan1 = vflast_trans_date_loan;
    //        if (vf_date_from_loan2 <= DateAdd("d", -1, vflast_trans_date_loan))
    //        {
    //            vfloanintcurrint = 0;
    //            vfloanintodint = 0;
    //        }

    //        rs.Open("SELECT * FROM LOAN_DUE_INT WHERE DT='" + Format(vflast_trans_date_loan, "DD-MMM-YYYY") + "'   AND SL_CODE=" + Conversion.Val(sl_code) + " ORDER BY SL DESC", con, adOpenKeyset, adLockReadOnly);
    //        if (rs.EOF == false)
    //        {
    //            vfloanintodint = vfloanintodint + FillNum(rs.Fields("OD"));
    //            vfloanintcurrint = vfloanintcurrint + FillNum(rs.Fields("CURR"));
    //        }
    //        rs.Close();

    //        insprd1 = 1;
    //        if (vfodpr == "MONTHLY")
    //            insprd1 = 1;
    //        else if (vfodpr == "18 MONTH")
    //            insprd1 = 18;
    //        else if (vfodpr == "QUARTERLY")
    //            insprd1 = 3;
    //        else if (vfodpr == "HALF YEARLY")
    //            insprd1 = 6;
    //        else if (vfodpr == "YEARLY")
    //            insprd1 = 12;
    //        else
    //            insprd1 = insprd;
    //        // maxmonthdt = 30
    //        // If Month(dt) = 1 Or Month(dt) = 2 Or Month(dt) = 4 Or Month(dt) = 6 Or Month(dt) = 8 Or Month(dt) = 1 = 9 Or Month(dt) = 11 Then
    //        // maxmonthdt = 31
    //        // ElseIf Month(dt) = 5 Or Month(dt) = 7 Or Month(dt) = 10 Or Month(dt) = 12 Then
    //        // maxmonthdt = 30
    //        // ElseIf Year(dt) Mod 4 = 0 Then
    //        // maxmonthdt = 29
    //        // Else
    //        // maxmonthdt = 28
    //        // End If
    //        vfdemdt = (DateTime)vfappldt - 1;
    //        while (vflast_trans_date_loan > vfdemdt)
    //            vfdemdt = DateAdd("M", insprd1, vfdemdt);
    //        if (dt < vfdemdt)
    //            vfdemdt = dt;
    //        getCurrPrin(vfinstamt, Val(VFKAMALNO_OF_INST), vfodpr, (DateTime)vfappldt, vfdemdt, Val(insprd1), (DateTime)VFLASTDT);
    //        totd = DateDiff("m", vflast_trans_date_loan, vfdemdt);
    //        if (vflast_DISB_date_loan == vflast_trans_date_loan)
    //        {
    //            if (vf_date_from_loan != vflast_trans_date_loan)
    //                totd = totd + 1;
    //        }
    //        if (inttype == "Normal")
    //        {
    //            if (totd > 0)
    //            {
    //                vfloancurrint = vfloantotcurr * totd * vfroi / (double)1200;
    //                vfloanodint = vfloantotod * totd * (vfroi + vfodroi) / (double)1200;
    //            }
    //        }
    //        else if (totd > 0)
    //        {
    //            vfloancurrint = (vfloantotcurr + vfloanintcurrint) * totd * vfroi / (double)1200;
    //            vfloanodint = (vfloantotod + vfloanintodint) * totd * (vfroi + vfodroi) / (double)1200;
    //        }

    //        dt_to = vfdemdt;

    //        vfdemdt = DateTime.DateAdd("M", insprd, vfdemdt);
    //        while (vfdemdt <= dt)
    //        {
    //            getCurrPrin(vfinstamt, Val(VFKAMALNO_OF_INST), vfodpr, (DateTime)vfappldt, vfdemdt, Val(insprd1), (DateTime)VFLASTDT);
    //            totd = DateTime.DateDiff("M", dt_to, vfdemdt);
    //            // If vflast_DISB_date_loan = vflast_trans_date_loan Then
    //            // If vf_date_from_loan <> vflast_trans_date_loan Then
    //            // totd = totd + 1
    //            // End If
    //            // End If
    //            if (inttype == "Normal")
    //            {
    //                vfloancurrint = vfloancurrint + (vfloantotcurr * totd * vfroi / (double)1200);
    //                vfloanodint = vfloanodint + (vfloantotod * totd * (vfroi + vfodroi) / (double)1200);
    //            }
    //            else
    //            {
    //                vfloancurrint = vfloancurrint + ((vfloantotcurr + vfloanintcurrint + vfloancurrint) * insprd * vfroi / (double)(1200));
    //                vfloanodint = vfloanodint + ((vfloantotod + vfloanintodint + vfloanodint) * insprd * (vfroi + vfodroi) / (double)(1200));
    //            }

    //            dt_to = vfdemdt;
    //            vfdemdt = DateTime.DateAdd("M", insprd, vfdemdt);
    //        }
    //        vfdemdt = DateTime.DateAdd("M", -insprd, vfdemdt);
    //        if (dt < vfdemdt)
    //            vfdemdt = dt;
    //        getCurrPrin(vfinstamt, Val(VFKAMALNO_OF_INST), vfodpr, (DateTime)vfappldt, dt, Val(insprd1), (DateTime)VFLASTDT);
    //        totd = DateTime.DateDiff("M", vfdemdt, dt);
    //        // If vflast_DISB_date_loan = vflast_trans_date_loan Then
    //        // If vf_date_from_loan <> vflast_trans_date_loan Then
    //        // totd = totd + 1
    //        // End If
    //        // End If
    //        if (inttype == "Normal")
    //        {
    //            if (totd > 0)
    //            {
    //                vfloancurrint = vfloancurrint + (vfloantotcurr * totd * vfroi / (double)1200);
    //                vfloanodint = vfloanodint + (vfloantotod * totd * (vfroi + vfodroi) / (double)1200);
    //            }
    //        }
    //        else if (totd > 0)
    //        {
    //            vfloancurrint = vfloancurrint + ((vfloantotcurr + vfloanintcurrint + vfloancurrint) * totd * vfroi / (double)1200);
    //            vfloanodint = vfloanodint + ((vfloantotod + vfloanintodint + vfloanodint) * totd * (vfroi + vfodroi) / (double)1200);
    //        }
    //        vfloancurrint = Round(vfloancurrint + vfloanintcurrint);
    //        vfloanodint = Round(vfloanodint + vfloanintodint);
    //    }

    //    public void Calc_Monthly_EMI(long sl_code, DateTime dt, int insprd, string inttype)
    //    {
    //        ADODB.Recordset rs_op = new ADODB.Recordset();
    //        ADODB.Recordset rs_disb = new ADODB.Recordset();
    //        ADODB.Recordset rs_sdl = new ADODB.Recordset();
    //        ADODB.Recordset rs_rep = new ADODB.Recordset();
    //        ADODB.Recordset rs_dem = new ADODB.Recordset();
    //        DateTime vfldt;
    //        double vfdisbamt;
    //        double vfrepamt;
    //        double vfnetloan;
    //        double vfinstamt;
    //        double vfroi;
    //        double vfodroi;
    //        string vfrepmode;
    //        DateTime vflwdt;
    //        DateTime vfinsdt;
    //        DateTime vfdfr;
    //        DateTime vfdemdt;
    //        double vfnoins;
    //        double vfnodem;
    //        double vftotdays;
    //        double vfprinod;
    //        DateTime vfoddt;
    //        DateTime dt_from;
    //        string vftype;
    //        DateTime dt_to;
    //        ADODB.Recordset rs = new ADODB.Recordset();
    //        int j;
    //        DateTime vfloancheckdt;
    //        double vftotloanpaid;
    //        ADODB.Recordset r = new ADODB.Recordset();
    //        long dueprd;
    //        // '''''''''''''''''''''''''''''''''LOAN DETAILS '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    //        rs_sdl.Open("select * FROM SUBLEDGER_DETAILS_LOAN where SL_CODE=" + Conversion.Val(sl_code), con, adOpenDynamic, adLockReadOnly);
    //        if (!rs_sdl.EOF)
    //        {
    //            vfnetloan = FillNum(rs_sdl.NET_LOAN);
    //            vfinstamt = FillNum(rs_sdl.INST_AMOUNT);
    //            vfroi = FillNum(rs_sdl.roi);
    //            vfodroi = FillNum(rs_sdl.od_roi);
    //            vfrepmode = FillText(rs_sdl.repay_mode);
    //            vftype = FillText(rs_sdl.Type);
    //            vflwdt = FillText(rs_sdl.last_rep_dt);
    //            VFLASTDT = FillText(rs_sdl.last_rep_dt);
    //            vfappldt = FillText(rs_sdl.sanc_dt);
    //            if (vfappldt == "")
    //                vfappldt = FillText(rs_sdl.appl_dt);
    //            vftotl = FillNum(rs_sdl.loan_amnt);

    //            if (rs_sdl.INST_ST_DATE != "")
    //            {
    //                vfinsdt = FillText(rs_sdl.INST_ST_DATE);
    //                vfinstap = FillText(rs_sdl.INST_appl);
    //            }
    //        }
    //        rs_sdl.Close();
    //        vftotloanpaid = Conversion.Val(vfnetloan) - Val(vfloantotp);
    //        vfloancurrint = 0;
    //        vfloandemand = 0;
    //        vfloanodint = 0;
    //        vfloantotcurr = 0;
    //        vfloantotod = 0;
    //        if (vflast_trans_date_loan > vf_date_from_loan2)
    //        {
    //            vfloanintcurrint = 0;
    //            vfloanintodint = 0;
    //        }
    //        if (sl_code == 601499)
    //            a = a;
    //        rs.Open("SELECT SUM(PRIN_ADV) AS ADV, SUM(PRIN_OD) AS OD, SUM(PRIN_CURR) AS CURR, SUM(INT_CURR) AS INT_CURR FROM LOAN_REPAY_DET WHERE SL_CODE=" + Conversion.Val(sl_code) + " and REP_DATE<='" + Strings.Format(dt, "dd-mmm-yyyy") + "'", con, adOpenForwardOnly, adLockReadOnly);
    //        if (rs.EOF == false)
    //        {
    //            vftotloanpaid1 = FillNum(rs.Fields("ADV")) + FillNum(rs.Fields("OD")) + FillNum(rs.Fields("CURR")) + FillNum(rs.Fields("INT_CURR"));
    //            vftotloanpaid2 = FillNum(rs.Fields("ADV")) + FillNum(rs.Fields("OD")) + FillNum(rs.Fields("CURR")); // + FillNum(rs.Fields("INT_CURR"))
    //        }
    //        rs.Close();
    //        rs.Open("SELECT * FROM LOAN_DUE_INT WHERE DT='" + Format(vflast_trans_date_loan, "DD-MMM-YYYY") + "'   AND SL_CODE=" + Conversion.Val(sl_code) + " ORDER BY SL DESC", con, adOpenKeyset, adLockReadOnly);
    //        if (rs.EOF == false)
    //        {
    //            vfloanintodint = vfloanintodint + FillNum(rs.Fields("OD"));
    //            vfloanintcurrint = vfloanintcurrint + FillNum(rs.Fields("CURR"));
    //        }
    //        rs.Close();
    //        rs.Open("select emi from emi where sl_code = " + Conversion.Val(sl_code), con, adOpenForwardOnly, adLockReadOnly);
    //        if (rs.EOF == false)
    //            vfToTEmi = FillNum(rs.Fields("EMI"));
    //        rs.Close();
    //        if (vfToTEmi == 0)
    //            vfToTEmi = 1;
    //        rs.Open("Select sum(emi) as emi,sum(prin) as prin,sum(ints) as ints from emi where sl_code=" + Conversion.Val(sl_code) + " and dt<='" + Strings.Format(dt, "dd-mmm-yyyy") + "'", con, adOpenKeyset, adLockReadOnly);
    //        if (rs.EOF == false)
    //        {
    //            vfloandemand = FillNum(rs.Fields("prin"));
    //            if (vfloandemand > vfloantotp)
    //                vfloandemand = vfloantotp;
    //            if (vfloandemand < 0)
    //                vfloandemand = 0;
    //            // vfloantotod = FillNum(rs.Fields("prin")) + FillNum(rs.Fields("INTS")) - vftotloanpaid
    //            // vfloantotod = FillNum(rs.Fields("prin")) - vftotloanpaid
    //            vfloantotod = FillNum(rs.Fields("EMI")) - vftotloanpaid1;
    //            // vfToTEmi = Int(vfloantotod / vfToTEmi)
    //            if (vfloantotod < 0)
    //                vfloantotod = 0;
    //            vfloantotcurr = vfloantotp - vfloantotod;
    //        }
    //        rs.Close();
    //        rs.Open("Select count(*) from emi  where sl_code=" + Conversion.Val(sl_code) + " and dt>='" + Strings.Format(dt, "dd-mmm-yyyy") + "'", con, adOpenKeyset, adLockReadOnly);
    //        if (rs.EOF == false)
    //            vfToTEmi = FillNum(rs(0)) + vfToTEmi;
    //        rs.Close();
    //        rs.Open("Select prin from emi where sl_code=" + Conversion.Val(sl_code) + " and dt='" + Strings.Format(dt, "dd-mmm-yyyy") + "'", con, adOpenKeyset, adLockReadOnly);
    //        if (rs.EOF == false)
    //            vfloandemand = vfloandemand + FillNum(rs.Fields("Prin"));
    //        else
    //        {
    //            r.Open("Select prin from emi where sl_code=" + Conversion.Val(sl_code) + " and dt = (select min(dt) from emi where sl_code=" + Conversion.Val(sl_code) + " and dt>'" + Strings.Format(dt, "dd-mmm-yyyy") + "')", con, adOpenKeyset, adLockReadOnly);
    //            if (r.EOF == false)
    //                vfloandemand = Val(vfloandemand) + FillNum(r.Fields("Prin"));
    //            r.Close();
    //        }
    //        rs.Close();
    //        if (VFLASTDT < vflast_trans_date_loan)
    //        {
    //            vfloantotcurr = 0;
    //            vfloantotod = vfloantotp;
    //            vfloandemand = vfloantotp;
    //        }
    //        if (inttype == "Normal")
    //        {
    //            TOTDAYINT = DateDiff("d", vflast_trans_date_loan, dt);
    //            vfloancurrint = vfloantotcurr * vfroi * (TOTDAYINT / (double)36500);
    //            vfloanodint = vfloantotod * (vfroi + vfodroi) * (TOTDAYINT / (double)36500);
    //            vfloancurrint = vfloancurrint + vfloanintcurrint;
    //            vfloanodint = vfloanodint + vfloanintodint;
    //        }
    //        else
    //        {
    //            TOTDAYINT = DateDiff("M", vflast_trans_date_loan, dt);
    //            if (Day(vflast_trans_date_loan) > DateTime.Day(dt))
    //                TOTDAYINT = TOTDAYINT - 1;
    //            dueprd = Int(TOTDAYINT / (double)insprd);
    //            vfloancurrint = (vfloantotcurr + vfloanintcurrint) * (Math.Pow((1 + (vfroi * (insprd / (double)1200))), dueprd));
    //            vfloanodint = (vfloantotod + vfloanintodint) * (Math.Pow((1 + ((vfroi + vfodroi) * (insprd / (double)1200))), dueprd));
    //            vfloancurrint = vfloancurrint - (vfloantotcurr + vfloanintcurrint);
    //            vfloanodint = vfloanodint - (vfloantotod + vfloanintodint);

    //            vflast_trans_date_loan = DateAdd("M", (Val(TOTDAYINT) - (Val(TOTDAYINT) - Conversion.Val(Conversion.Val(dueprd) * Conversion.Val(insprd)))), vflast_trans_date_loan);


    //            TOTDAYINT = DateDiff("d", vflast_trans_date_loan, dt);
    //            vfloancurrint = vfloancurrint + (vfloantotcurr + vfloancurrint + vfloanintcurrint) * vfroi * (TOTDAYINT / (double)36500);
    //            vfloanodint = vfloanodint + (vfloantotod + vfloanodint + vfloanintodint) * (vfroi + vfodroi) * (TOTDAYINT / (double)36500);

    //            vfloancurrint = vfloancurrint + vfloanintcurrint;
    //            vfloanodint = vfloanodint + vfloanintodint;
    //        }
    //        vfloanodint = Round(vfloanodint);
    //        vfloancurrint = Round(vfloancurrint);
    //    }

    //    private void Calc_Monthly_kcc(long sl_code, DateTime dt, int insprd, string inttype)
    //    {
    //        ADODB.Recordset rs_op = new ADODB.Recordset();
    //        ADODB.Recordset rs_disb = new ADODB.Recordset();
    //        ADODB.Recordset rs_sdl = new ADODB.Recordset();
    //        ADODB.Recordset rs_rep = new ADODB.Recordset();
    //        ADODB.Recordset rs_dem = new ADODB.Recordset();
    //        DateTime vfldt;
    //        double vfdisbamt;
    //        double vfrepamt;
    //        double vfnetloan;
    //        double vfinstamt;
    //        double vfroi;
    //        double vfodroi;
    //        string vfrepmode;
    //        DateTime vflwdt;
    //        DateTime vfinsdt;
    //        DateTime vfdfr;
    //        DateTime vfdemdt;
    //        double vfnoins;
    //        double vfnodem;
    //        double vftotdays;
    //        double vfprinod;
    //        DateTime vfoddt;
    //        DateTime dt_from;
    //        string vftype;
    //        DateTime dt_to;
    //        double paidloan;
    //        ADODB.Recordset rs = new ADODB.Recordset();
    //        int j;
    //        DateTime vflastloandt;
    //        DateTime vfloancheckdt;
    //        int term;
    //        int duedays;
    //        double totodpm;
    //        int schemecode;
    //        int totd;
    //        int totm;
    //        int mondiff;


    //        // ''''''''''''''''''''''''''''''''''LOAN DETAILS '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    //        rs_sdl.Open("select * FROM SUBLEDGER_DETAILS_LOAN where SL_CODE=" + Conversion.Val(sl_code), con, adOpenDynamic, adLockReadOnly);
    //        if (!rs_sdl.EOF)
    //        {
    //            vfnetloan = FillNum(rs_sdl.NET_LOAN);
    //            vfinstamt = FillNum(rs_sdl.INST_AMOUNT);
    //            vfroi = FillNum(rs_sdl.roi);
    //            vfodroi = FillNum(rs_sdl.od_roi);
    //            vfrepmode = FillText(rs_sdl.repay_mode);
    //            vftype = FillText(rs_sdl.Type);
    //            vflwdt = FillText(rs_sdl.last_rep_dt);
    //            VFLASTDT = FillText(rs_sdl.last_rep_dt);
    //            vfappldt = FillText(rs_sdl.sanc_dt);
    //            if (vfappldt == "")
    //                vfappldt = FillText(rs_sdl.appl_dt);
    //            vftotl = FillNum(rs_sdl.loan_amnt);

    //            if (rs_sdl.INST_ST_DATE != "")
    //            {
    //                vfinsdt = FillText(rs_sdl.INST_ST_DATE);
    //                vfinstap = FillText(rs_sdl.INST_appl);
    //            }

    //            schemecode = FillText(rs_sdl.scheme_code);
    //            VFKAMALNO_OF_INST = FillNum(rs_sdl.no_of_inst);
    //        }
    //        rs_sdl.Close();
    //        rs_sdl.Open("Select * from loan_newdisb where sl_code=" + Conversion.Val(sl_code) + " and disb_date= (select max(disb_date) from  loan_newdisb where sl_code=" + Conversion.Val(sl_code) + " and last_due_date>='" + Strings.Format(dt, "dd-mmm-yyyy") + "')", con, adOpenDynamic, adLockReadOnly);
    //        if (rs_sdl.EOF == false)
    //        {
    //            vfappldt = FillText(rs_sdl.disb_date);
    //            VFLASTDT = FillText(rs_sdl.last_due_date);
    //            vfnetloan = FillNum(rs_sdl.sanc_amt);
    //        }
    //        else
    //        {
    //            rs_op.Open("Select * from loan_newdisb where sl_code=" + Conversion.Val(sl_code) + " and disb_date= (select max(disb_date) from  loan_newdisb where sl_code=" + Conversion.Val(sl_code) + " and last_due_date<'" + Strings.Format(dt, "dd-mmm-yyyy") + "')", con, adOpenDynamic, adLockReadOnly);
    //            if (rs_op.EOF == false)
    //            {
    //                vfappldt = FillText(rs_op.disb_date);
    //                VFLASTDT = FillText(rs_op.last_due_date);
    //                vfnetloan = FillNum(rs_op.sanc_amt);
    //            }
    //            rs_op.Close();
    //        }
    //        rs_sdl.Close();
    //        if (Conversion.Val(vfinstamt) != 0)
    //            VFKAMALNO_OF_INST = Round(vfnetloan / vfinstamt);
    //        if (Val(VFKAMALNO_OF_INST) != 0)
    //            vfinstamt = Round(vfnetloan / VFKAMALNO_OF_INST);
    //        else
    //        {
    //            if (Conversion.Val(vfinstamt) == 0)
    //                vfinstamt = vfnetloan;
    //            VFKAMALNO_OF_INST = Conversion.Int(vfnetloan / vfinstamt);

    //            if (VFKAMALNO_OF_INST == 0)
    //                VFKAMALNO_OF_INST = 1;
    //        }

    //        vf_date_from_loan1 = vflast_trans_date_loan;
    //        if (vf_date_from_loan2 < DateAdd("d", -2, vflast_trans_date_loan))
    //        {
    //            vfloanintcurrint = 0;
    //            vfloanintodint = 0;
    //        }

    //        rs.Open("SELECT * FROM LOAN_DUE_INT WHERE DT='" + Format(vflast_trans_date_loan, "DD-MMM-YYYY") + "'   AND SL_CODE=" + Conversion.Val(sl_code) + " ORDER BY SL DESC", con, adOpenKeyset, adLockReadOnly);
    //        if (rs.EOF == false)
    //        {
    //            vfloanintodint = vfloanintodint + FillNum(rs.Fields("OD"));
    //            vfloanintcurrint = vfloanintcurrint + FillNum(rs.Fields("CURR"));
    //        }
    //        rs.Close();

    //        insprd1 = 1;
    //        if (vfodpr == "MONTHLY")
    //            insprd1 = 1;
    //        else if (vfodpr == "18 MONTH")
    //            insprd1 = 18;
    //        else if (vfodpr == "QUARTERLY")
    //            insprd1 = 3;
    //        else if (vfodpr == "HALF YEARLY")
    //            insprd1 = 6;
    //        else if (vfodpr == "YEARLY")
    //            insprd1 = 12;
    //        else
    //            insprd1 = insprd;
    //        // maxmonthdt = 30
    //        // If Month(dt) = 1 Or Month(dt) = 2 Or Month(dt) = 4 Or Month(dt) = 6 Or Month(dt) = 8 Or Month(dt) = 1 = 9 Or Month(dt) = 11 Then
    //        // maxmonthdt = 31
    //        // ElseIf Month(dt) = 5 Or Month(dt) = 7 Or Month(dt) = 10 Or Month(dt) = 12 Then
    //        // maxmonthdt = 30
    //        // ElseIf Year(dt) Mod 4 = 0 Then
    //        // maxmonthdt = 29
    //        // Else
    //        // maxmonthdt = 28
    //        // End If

    //        if (VFLASTDT >= dt)
    //        {
    //            getCurrPrin(vfinstamt, Val(VFKAMALNO_OF_INST), vfodpr, (DateTime)vfappldt, dt, Val(insprd1), (DateTime)VFLASTDT);
    //            totd = DateDiff("d", vflast_trans_date_loan, dt);
    //            if (totd > 0)
    //            {
    //                vfloancurrint = vfloantotcurr * totd * vfroi / (double)36500;
    //                vfloanodint = vfloantotod * totd * (vfroi + vfodroi) / (double)36500;
    //            }
    //        }
    //        else if (dt > VFLASTDT)
    //        {
    //            if (vflast_trans_date_loan > VFLASTDT)
    //            {
    //                getCurrPrin(vfinstamt, Val(VFKAMALNO_OF_INST), vfodpr, (DateTime)vfappldt, dt, Val(insprd1), (DateTime)VFLASTDT);
    //                totd = DateDiff("d", vflast_trans_date_loan, dt);
    //                if (totd > 0)
    //                {
    //                    vfloancurrint = vfloantotcurr * totd * vfroi / (double)36500;
    //                    vfloanodint = vfloantotod * totd * (vfroi + vfodroi) / (double)36500;
    //                }
    //            }
    //            else
    //            {
    //                getCurrPrin(vfinstamt, Val(VFKAMALNO_OF_INST), vfodpr, (DateTime)vfappldt, (DateTime)VFLASTDT, Val(insprd1), (DateTime)VFLASTDT);
    //                totd = DateDiff("d", vflast_trans_date_loan, VFLASTDT);
    //                if (totd > 0)
    //                {
    //                    vfloancurrint = vfloancurrint + vfloantotcurr * totd * vfroi / (double)36500;
    //                    vfloanodint = vfloanodint + vfloantotod * totd * (vfroi + vfodroi) / (double)36500;
    //                }
    //                getCurrPrin(vfinstamt, Val(VFKAMALNO_OF_INST), vfodpr, (DateTime)vfappldt, dt, Val(insprd1), (DateTime)VFLASTDT);
    //                totd = DateDiff("d", VFLASTDT, dt);
    //                if (totd > 0)
    //                {
    //                    vfloancurrint = vfloancurrint + vfloantotcurr * totd * vfroi / (double)36500;
    //                    vfloanodint = vfloanodint + vfloantotod * totd * (vfroi + vfodroi) / (double)36500;
    //                }
    //            }
    //        }
    //        vfloancurrint = Round(vfloancurrint + vfloanintcurrint);
    //        vfloanodint = Round(vfloanodint + vfloanintodint);
    //    }

    //    public void Find_Demand_Balance_Loan_Account(long sl_code, DateTime dt)
    //    {

    //        DateTime vfldt;
    //        double vfdisbamt;
    //        double vfrepamt;
    //        double vfnetloan;
    //        double vfinstamt;
    //        // Dim vfroi As Double
    //        double vfodroi;
    //        string vfrepmode;
    //        DateTime vflwdt;
    //        DateTime vfinsdt;
    //        DateTime vfdfr;
    //        DateTime vfdemdt;
    //        double vfnoins;
    //        double vfnodem;
    //        double vftotdays;
    //        double vfprinod;
    //        DateTime vfoddt;
    //        DateTime dt_from;
    //        string vftype;
    //        int vfsccode;
    //        DateTime dt_to;

    //        // ''''''''''''''''''''''''''''''''LOAN DETAILS '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    //        rs_sdl.Open("select NET_LOAN,INST_AMOUNT,ROI,OD_ROI,REPAY_MODE,LAST_REP_DT,INST_ST_DATE ,type,SCHEME_CODE,ODPR  FROM SUBLEDGER_DETAILS_LOAN where SL_CODE=" + Conversion.Val(sl_code), con, adOpenDynamic, adLockReadOnly);
    //        if (!rs_sdl.EOF)
    //        {
    //            // vfnetloan = FillNum(rs_sdl!net_loan)
    //            // vfinstamt = FillNum(rs_sdl!inst_amount)
    //            // vfroi = FillNum(rs_sdl!roi)
    //            // vfodroi = FillNum(rs_sdl!od_roi)
    //            vfrepmode = FillText(rs_sdl.repay_mode);
    //            vfodpr = FillText(rs_sdl.odpr);
    //            vftype = FillText(rs_sdl.Type);
    //            // vflwdt = FillText(rs_sdl!last_rep_dt)
    //            vfsccode = FillNum(rs_sdl.scheme_code);
    //        }
    //        rs_sdl.Close();

    //        if (vfodpr == "EMI")
    //        {
    //            if (vfrepmode == "MONTHLY")
    //                Calc_Monthly_EMI(sl_code, dt, 1, "Normal");
    //            else if (vfrepmode == "MONTHLY COMPOUND")
    //                Calc_Monthly_EMI(sl_code, dt, 1, "");
    //            else if (vfrepmode == "YEARLY" & vftype != "KCC Loan")
    //                Calc_Monthly_EMI(sl_code, dt, 12, "Normal");
    //            else if (vfrepmode == "QUARTERLY")
    //                Calc_Monthly_EMI(sl_code, dt, 3, "Normal");
    //            else if (vfrepmode == "QUARTERLY COMPOUND")
    //                Calc_Monthly_EMI(sl_code, dt, 3, "");
    //            else if (vfrepmode == "YEARLY COMPOUND")
    //                Calc_Monthly_EMI(sl_code, dt, 12, "");
    //            else if (vfrepmode == "YEARLY" & vftype == "KCC Loan")
    //            {
    //                if (vfsccode == 2)
    //                    Calc_Monthly_EMI(sl_code, dt, 1, "Normal");
    //                else if (vfsccode == 1)
    //                    Calc_Monthly_EMI(sl_code, dt, 1, "Normal");
    //            }
    //            else
    //                Calc_Monthly_EMI(sl_code, dt, 1, "Normal");
    //        }
    //        else if (vfodpr == "MONTHLY WISE")
    //        {
    //            if (vfodpr == "AFTER LAST REPAY DATE")
    //            {
    //                if (vftype != "KCC Loan")
    //                    Calc_Monthly_Wise(sl_code, dt, Interaction.IIf(Strings.InStr(vfrepmode, "QUARTERLY") > 0, 3, Interaction.IIf(Strings.InStr(vfrepmode, "YEARLY") > 0, 12, Interaction.IIf(Strings.InStr(vfrepmode, "HALF") > 0, 6, 1))), Interaction.IIf(Strings.InStr(vfrepmode, "COMPOUND") > 0, "", "Normal"));
    //                else
    //                    Calc_Monthly_Wise(sl_code, dt, Interaction.IIf(Strings.InStr(vfrepmode, "QUARTERLY") > 0, 3, 1), Interaction.IIf(Strings.InStr(vfrepmode, "COMPOUND") > 0, "", "Normal"));
    //            }
    //            else if (vfrepmode == "MONTHLY")
    //                Calc_Monthly_Wise(sl_code, dt, 1, "Normal");
    //            else if (vfrepmode == "MONTHLY COMPOUND")
    //                Calc_Monthly_Wise(sl_code, dt, 1, "");
    //            else if (vfrepmode == "YEARLY" & vftype != "KCC Loan")
    //                Calc_Monthly_Wise(sl_code, dt, 12, "Normal");
    //            else if (vfrepmode == "QUARTERLY")
    //                Calc_Monthly_Wise(sl_code, dt, 3, "Normal");
    //            else if (vfrepmode == "HALF YEARLY")
    //                Calc_Monthly_Wise(sl_code, dt, 6, "Normal");
    //            else if (vfrepmode == "QUARTERLY COMPOUND")
    //                Calc_Monthly_Wise(sl_code, dt, 3, "");
    //            else if (vfrepmode == "YEARLY COMPOUND")
    //                Calc_Monthly_Wise(sl_code, dt, 12, "");
    //            else if (vfrepmode == "YEARLY" & vftype == "KCC Loan")

    //                // If vfsccode = 2 Then
    //                Calc_Monthly_Wise(sl_code, dt, 1, "Normal");
    //            else
    //                Calc_Monthly_Wise(sl_code, dt, 1, "Normal");
    //        }
    //        else if (vfodpr == "AFTER LAST REPAY DATE")
    //        {
    //            if (vftype != "KCC Loan")
    //                Calc_Monthly(sl_code, dt, Interaction.IIf(Strings.InStr(vfrepmode, "QUARTERLY") > 0, 3, Interaction.IIf(Strings.InStr(vfrepmode, "YEARLY") > 0, 12, Interaction.IIf(Strings.InStr(vfrepmode, "HALF") > 0, 6, 1))), Interaction.IIf(Strings.InStr(vfrepmode, "COMPOUND") > 0, "", "Normal"));
    //            else
    //                Calc_Monthly_kcc(sl_code, dt, Interaction.IIf(Strings.InStr(vfrepmode, "QUARTERLY") > 0, 3, 1), Interaction.IIf(Strings.InStr(vfrepmode, "COMPOUND") > 0, "", "Normal"));
    //        }
    //        else if (vfrepmode == "MONTHLY")
    //            Calc_Monthly(sl_code, dt, 1, "Normal");
    //        else if (vfrepmode == "MONTHLY COMPOUND")
    //            Calc_Monthly(sl_code, dt, 1, "");
    //        else if (vfrepmode == "YEARLY" & vftype != "KCC Loan")
    //            Calc_Monthly(sl_code, dt, 12, "Normal");
    //        else if (vfrepmode == "QUARTERLY")
    //            Calc_Monthly(sl_code, dt, 3, "Normal");
    //        else if (vfrepmode == "QUARTERLY COMPOUND")
    //            Calc_Monthly(sl_code, dt, 3, "");
    //        else if (vfrepmode == "YEARLY COMPOUND")
    //            Calc_Monthly(sl_code, dt, 12, "");
    //        else if (vfrepmode == "YEARLY" & vftype == "KCC Loan")

    //            // If vfsccode = 2 Then
    //            Calc_Monthly(sl_code, dt, 1, "Normal");
    //        else
    //            Calc_Monthly(sl_code, dt, 1, "Normal");
    //    }

    //    private void Calc_Monthly(long sl_code, DateTime dt, int insprd, string inttype)
    //    {

    //        DateTime vfldt;
    //        double vfdisbamt;
    //        double vfrepamt;
    //        double vfnetloan;
    //        double vfinstamt;
    //        double vfroi;
    //        double vfodroi;
    //        string vfrepmode;
    //        DateTime vflwdt;
    //        DateTime vfinsdt;
    //        DateTime vfdfr;
    //        DateTime vfdemdt;
    //        double vfnoins;
    //        double vfnodem;
    //        double vftotdays;
    //        double vfprinod;
    //        DateTime vfoddt;
    //        DateTime dt_from;
    //        string vftype;
    //        DateTime dt_to;
    //        double paidloan;
    //        ADODB.Recordset rs = new ADODB.Recordset();
    //        int j;
    //        DateTime vflastloandt;
    //        DateTime vfloancheckdt;
    //        int term;
    //        int duedays;
    //        double totodpm;
    //        int schemecode;
    //        int totd;
    //        int totm;
    //        int mondiff;
    //        // '''''''''''''''''''''''''''''''''LOAN DETAILS '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    //        rs_sdl.Open("select * FROM SUBLEDGER_DETAILS_LOAN where SL_CODE=" + Conversion.Val(sl_code), con, adOpenDynamic, adLockReadOnly);
    //        if (!rs_sdl.EOF)
    //        {
    //            vfnetloan = FillNum(rs_sdl.NET_LOAN);
    //            vfinstamt = FillNum(rs_sdl.INST_AMOUNT);
    //            vfroi = FillNum(rs_sdl.roi);
    //            vfodroi = FillNum(rs_sdl.od_roi);
    //            vfrepmode = FillText(rs_sdl.repay_mode);
    //            vftype = FillText(rs_sdl.Type);
    //            vflwdt = FillText(rs_sdl.last_rep_dt);
    //            VFLASTDT = FillText(rs_sdl.last_rep_dt);
    //            vfappldt = FillText(rs_sdl.sanc_dt);
    //            if (vfappldt == "")
    //                vfappldt = FillText(rs_sdl.appl_dt);
    //            vftotl = FillNum(rs_sdl.loan_amnt);

    //            if (rs_sdl.INST_ST_DATE != "")
    //            {
    //                vfinsdt = FillText(rs_sdl.INST_ST_DATE);
    //                vfinstap = FillText(rs_sdl.INST_appl);
    //            }

    //            schemecode = FillText(rs_sdl.scheme_code);
    //            VFKAMALNO_OF_INST = FillNum(rs_sdl.no_of_inst);
    //        }
    //        rs_sdl.Close();
    //        rs_sdl.Open("Select * from loan_newdisb where sl_code=" + Conversion.Val(sl_code) + " and disb_date= (select max(disb_date) from  loan_newdisb where sl_code=" + Conversion.Val(sl_code) + " and last_due_date>'" + Strings.Format(dt, "dd-mmm-yyyy") + "')", con, adOpenDynamic, adLockReadOnly);
    //        if (rs_sdl.EOF == false)
    //        {
    //            vfappldt = FillText(rs_sdl.disb_date);
    //            VFLASTDT = FillText(rs_sdl.last_due_date);
    //            vfnetloan = FillNum(rs_sdl.sanc_amt);
    //        }
    //        rs_sdl.Close();
    //        if (Conversion.Val(vfinstamt) != 0)
    //            VFKAMALNO_OF_INST = Round(vfnetloan / vfinstamt);
    //        if (Val(VFKAMALNO_OF_INST) != 0)
    //            vfinstamt = Round(vfnetloan / VFKAMALNO_OF_INST);
    //        else
    //        {
    //            if (Conversion.Val(vfinstamt) == 0)
    //                vfinstamt = vfnetloan;
    //            VFKAMALNO_OF_INST = Conversion.Int(vfnetloan / vfinstamt);

    //            if (VFKAMALNO_OF_INST == 0)
    //                VFKAMALNO_OF_INST = 1;
    //        }

    //        vf_date_from_loan1 = vflast_trans_date_loan;
    //        if (vf_date_from_loan2 < DateAdd("d", -2, vflast_trans_date_loan))
    //        {
    //            vfloanintcurrint = 0;
    //            vfloanintodint = 0;
    //        }

    //        rs.Open("SELECT * FROM LOAN_DUE_INT WHERE DT='" + Format(vflast_trans_date_loan, "DD-MMM-YYYY") + "'   AND SL_CODE=" + Conversion.Val(sl_code) + " ORDER BY SL DESC", con, adOpenKeyset, adLockReadOnly);
    //        if (rs.EOF == false)
    //        {
    //            vfloanintodint = vfloanintodint + FillNum(rs.Fields("OD"));
    //            vfloanintcurrint = vfloanintcurrint + FillNum(rs.Fields("CURR"));
    //        }
    //        rs.Close();

    //        insprd1 = 1;
    //        if (vfodpr == "MONTHLY")
    //            insprd1 = 1;
    //        else if (vfodpr == "18 MONTH")
    //            insprd1 = 18;
    //        else if (vfodpr == "QUARTERLY")
    //            insprd1 = 3;
    //        else if (vfodpr == "HALF YEARLY")
    //            insprd1 = 6;
    //        else if (vfodpr == "YEARLY")
    //            insprd1 = 12;
    //        else
    //            insprd1 = insprd;
    //        // maxmonthdt = 30
    //        // If Month(dt) = 1 Or Month(dt) = 2 Or Month(dt) = 4 Or Month(dt) = 6 Or Month(dt) = 8 Or Month(dt) = 1 = 9 Or Month(dt) = 11 Then
    //        // maxmonthdt = 31
    //        // ElseIf Month(dt) = 5 Or Month(dt) = 7 Or Month(dt) = 10 Or Month(dt) = 12 Then
    //        // maxmonthdt = 30
    //        // ElseIf Year(dt) Mod 4 = 0 Then
    //        // maxmonthdt = 29
    //        // Else
    //        // maxmonthdt = 28
    //        // End If
    //        vfdemdt = (DateTime)vfappldt - 1;
    //        while (vflast_trans_date_loan > vfdemdt)
    //            vfdemdt = DateAdd("M", insprd1, vfdemdt);
    //        if (dt < vfdemdt)
    //            vfdemdt = dt;
    //        getCurrPrin(vfinstamt, Val(VFKAMALNO_OF_INST), vfodpr, (DateTime)vfappldt, vfdemdt, Val(insprd1), (DateTime)VFLASTDT);
    //        // totd = DateDiff("d", vflast_trans_date_loan, vfdemdt)

    //        if (inttype == "Normal")
    //        {
    //            if (totd > 0)
    //            {
    //                vfloancurrint = vfloantotcurr * totd * vfroi / (double)36500;
    //                vfloanodint = vfloantotod * totd * (vfroi + vfodroi) / (double)36500;
    //            }
    //        }
    //        else if (totd > 0)
    //        {
    //            vfloancurrint = (vfloantotcurr + vfloanintcurrint) * totd * vfroi / (double)36500;
    //            vfloanodint = (vfloantotod + vfloanintodint) * totd * (vfroi + vfodroi) / (double)36500;
    //        }

    //        dt_to = vfdemdt;

    //        vfdemdt = DateTime.DateAdd("M", insprd, vfdemdt);
    //        while (vfdemdt <= dt)
    //        {
    //            getCurrPrin(vfinstamt, Val(VFKAMALNO_OF_INST), vfodpr, (DateTime)vfappldt, vfdemdt, Val(insprd1), (DateTime)VFLASTDT);
    //            totd = DateTime.DateDiff("d", dt_to, vfdemdt);
    //            if (inttype == "Normal")
    //            {
    //                vfloancurrint = vfloancurrint + (vfloantotcurr * totd * vfroi / (double)36500);
    //                vfloanodint = vfloanodint + (vfloantotod * totd * (vfroi + vfodroi) / (double)36500);
    //            }
    //            else
    //            {
    //                vfloancurrint = vfloancurrint + ((vfloantotcurr + vfloanintcurrint + vfloancurrint) * insprd * vfroi / (double)(1200));
    //                vfloanodint = vfloanodint + ((vfloantotod + vfloanintodint + vfloanodint) * insprd * (vfroi + vfodroi) / (double)(1200));
    //            }

    //            dt_to = vfdemdt;
    //            vfdemdt = DateTime.DateAdd("M", insprd + 1, vfdemdt);
    //        }
    //        vfdemdt = DateTime.DateAdd("M", -insprd + 1, vfdemdt);
    //        if (dt < vfdemdt)
    //            vfdemdt = dt;
    //        getCurrPrin(vfinstamt, Val(VFKAMALNO_OF_INST), vfodpr, (DateTime)vfappldt, dt, Val(insprd1), (DateTime)VFLASTDT);
    //        totd = DateTime.DateDiff("d", vfdemdt, dt);

    //        if (inttype == "Normal")
    //        {
    //            if (totd > 0)
    //            {
    //                vfloancurrint = vfloancurrint + (vfloantotcurr * totd * vfroi / (double)36500);
    //                vfloanodint = vfloanodint + (vfloantotod * totd * (vfroi + vfodroi) / (double)36500);
    //            }
    //        }
    //        else if (totd > 0)
    //        {
    //            vfloancurrint = vfloancurrint + ((vfloantotcurr + vfloanintcurrint + vfloancurrint) * totd * vfroi / (double)36500);
    //            vfloanodint = vfloanodint + ((vfloantotod + vfloanintodint + vfloanodint) * totd * (vfroi + vfodroi) / (double)36500);
    //        }
    //        vfloancurrint = Round(vfloancurrint + vfloanintcurrint);
    //        vfloanodint = Round(vfloanodint + vfloanintodint);
    //    }

    //    public void Find_Current_Loan_Amount(long sl_code, DateTime dt)
    //    {

    //        DateTime dtf;
    //        vfloantotp = 0;
    //        //dtf = "01-Apr-2013";

    //        SqlConnection conn = new SqlConnection(strConnString);
    //        SqlCommand cmdLoan = new SqlCommand("SELECT * FROM dbo.Find_Current_Loan_Amount(@SL_code,@dt)", conn);
    //        // cmd.CommandType=CommandType.StoredProcedure;  
    //        cmdLoan.Parameters.AddWithValue("@SL_CODE", "SL_code");
    //        cmdLoan.Parameters.AddWithValue("@AsOnDate", "dt");
    //        SqlDataAdapter daf = new SqlDataAdapter(cmdLoan);
    //        DataTable dsLoan = new DataTable();
    //        conn.Open();
    //        daf.Fill(dsLoan);
    //        conn.Close();
    //        if (dsLoan.Rows.Count > 0)
    //        {
    //            vfloantotp = Convert.ToInt32(dsLoan.Rows[0][0]);
    //        }
    //        else
    //        {
    //            vfloantotp = 0;
    //        }


    //        vfloanintcurrint = 0;
    //        vfloanintodint = 0;



    //        SqlCommand cmdInt = new SqlCommand("Proc_CurrentInterest_OnLoanAmount", conn);
    //        cmdInt.CommandType = CommandType.StoredProcedure;
    //        cmdInt.Parameters.AddWithValue("@SL_CODE", "SL_code");
    //        cmdInt.Parameters.AddWithValue("@AsOnDate", "dt");
    //        SqlDataAdapter daInt = new SqlDataAdapter(cmdInt);
    //        DataTable dsInt = new DataTable();
    //        conn.Open();
    //        daInt.Fill(dsInt);
    //        conn.Close();
    //        if (dsLoan.Rows.Count > 0)
    //        {

    //            vfloanintcurrint = Convert.ToInt32(dsInt.Rows[0]["IntCurrent"]);
    //            vfloanintodint = Convert.ToInt32(dsInt.Rows[0]["IntOD"]);
    //        }
    //        else
    //        {
    //            vfloantotp = 0;
    //        }

    //    }




    //}
}
