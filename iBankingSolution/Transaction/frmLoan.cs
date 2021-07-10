using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;


namespace iBankingSolution.Transaction
{
    public class frmLoan
    {
        public DataTable getdatatable(SqlCommand cmd)
        {
            String strConn = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;

            SqlConnection con = new SqlConnection(strConn);
            con.ConnectionString = strConn;
            con.Open();
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            con.Dispose();
            cmd.Dispose();
            da.Dispose();
            return dt;
        }
        public SqlString FillDate(string Dt)
        {
            if (Dt == "")
            {
                return "";
            }
            else
            {
                return DateTime.Parse(Dt).ToString("dd/MM/yyyy");
            }
        }
        public Decimal NullInt(object str)
        {
            if (str == DBNull.Value || str == "")
            {
                return Convert.ToInt32(0);
            }
            else
            {
                return Convert.ToInt32(str);
            }
        }
        public Decimal NullDecimal(object str)
        {
            if (str == DBNull.Value || str == "")
            {
                return Convert.ToDecimal(0.00);
            }
            else
            {
                return Convert.ToDecimal(str);
            }
        }
        public string MASTER_MODULE_SIMPLE(int slcode, DateTime cdt, string lKey)
        {

            /////////////// declaration

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            int OdDays = 0, vftotdays = 0, NoOfInst = 0, vfCurrRestdays = 0, Duration = 0;
            bool DisbStatus = false, RepStatus = false, DisbInt = false, Repaid = false, DedInt = false;
            string Arin_Security100 = "", ErrNo = "", LoanKey = lKey, ReturnAll = "", vfodpr = "", Trtype = "", vfinstap = "";
            DateTime Arin_ODdate = DateTime.Now.Date, vfinsdt = DateTime.Now.Date, Arin_DisbDt, VFLASTDT = DateTime.Now.Date, LoanopenDt = DateTime.Now.Date, vfappldt = DateTime.Now.Date, Arin_FromDt = DateTime.Now.Date, FinanceStart = DateTime.Now.Date;
            decimal vfodintSet = 0, vfroi = 0, vfodroi = 0, vfinstamt = 0;
            decimal TempOdPrin = 0, CullPrin = 0, Arin_TotLoan = 0, Arin_intcurr = 0, Arin_intod = 0, Arin_prinOd = 0, Arin_prinCurr = 0, Arin_DisbAmt = 0, Arin_prinout = 0;
            decimal vfnetloan = 0, Arin_BalCurrInt = 0, Arin_BalOdInt = 0, Arin_BalOdPrin = 0, TransDisbamt = 0;
            decimal TempOddaysT = 0, TempArinOd = 0;

            DisbInt = false;
            //cmd.CommandText = "select c.FINANCE_START,s.DISB_INT from cn c,SECURITY_MASTER s where c.bank_code=@bnkc and s.bank_code=@bnkc";
            //cmd.Parameters.AddWithValue("@bnkc", bankcode);
            //DataTable dtCN = getdatatable(cmd);

            //if (dtCN.Rows.Count > 0)
            //{
            //    FinanceStart = Convert.ToDateTime(dtCN.Rows[0]["FINANCE_START"]);

            //    if (NullInt(dtCN.Rows[0]["DISB_INT"]) == 1)
            //    {
            //        DisbInt = true;
            //    }
            //}
            //dtCN.Dispose();

            try
            {
                cmd.CommandText = "select * from mdlloan(@slc,@dt)";
                cmd.Parameters.AddWithValue("@slc", slcode);
                cmd.Parameters.AddWithValue("@dt", cdt);
                //cmd.Parameters.AddWithValue("@bnkc", bankcode);
                DataTable dt = getdatatable(cmd);
                if (dt.Rows.Count > 0)
                {
                    vfroi = Convert.ToDecimal(dt.Rows[0]["roi"]);
                    vfodroi = Convert.ToDecimal(dt.Rows[0]["odroi"]);
                    VFLASTDT = Convert.ToDateTime(dt.Rows[0]["LAST_REPAY_DET"]);
                    vfappldt = Convert.ToDateTime(dt.Rows[0]["opening_dt"]);
                    LoanopenDt = Convert.ToDateTime(dt.Rows[0]["opening_dt"]);
                    Arin_TotLoan = Convert.ToDecimal(dt.Rows[0]["disb_amount"]);
                    vfnetloan = Convert.ToDecimal(dt.Rows[0]["sanc_amount"]);
                    vfodpr = dt.Rows[0]["odpr"].ToString();

                    vfinstap = dt.Rows[0]["INST_APPL"].ToString();
                    Duration = Convert.ToInt32(dt.Rows[0]["duration"]);
                    Arin_Security100 = dt.Rows[0]["SECURITY_100"].ToString();
                    vfodintSet = Convert.ToDecimal(dt.Rows[0]["odintset"].ToString());

                    if (vfinstap == "No")
                    {
                        NoOfInst = 0;
                    }
                    else
                    {
                        string aa = dt.Rows[0]["INST_ST_DATE"].ToString();
                        if (aa != "")
                        {
                            vfinsdt = Convert.ToDateTime(dt.Rows[0]["INST_ST_DATE"]);
                            vfinstap = dt.Rows[0]["INST_APPL"].ToString();
                            NoOfInst = Convert.ToInt32(dt.Rows[0]["no_of_inst"]);
                            vfinstamt = Convert.ToDecimal(dt.Rows[0]["INST_AMOUNT"]);
                        }

                    }

                }

                dt.Dispose();
            }
            catch (Exception en)
            {
                //alert("MDLLOAN : " + slcode + "::" + en.Message);
            }
            ErrNo = "1";
            /////////// ODPR CALCULATE
            switch (vfodpr)
            {
                case "MONTHLY":
                    OdDays = 1;
                    break;
                case "QUARTERLY":
                    OdDays = 3;
                    break;
                case "HALF YEARLY":
                    OdDays = 6;
                    break;
                case "YEARLY":
                    OdDays = 12;
                    break;
                case "18 MONTH":
                    OdDays = 18;
                    break;
                case "AFTER LAST REPAY DATE":
                    OdDays = Duration;
                    break;
                default:
                    OdDays = 0;
                    break;
            }

            vfappldt = vfappldt.AddDays(-1);
            ////////////////LOOP///////////// 
            cmd.CommandText = "select * from LOAN_TRANSACTION where sl_code=@slc AND dt>=@opendt AND dt<=@dt  order by dt,srl";
            cmd.Parameters.AddWithValue("@opendt", LoanopenDt);
            DataTable dtTran = getdatatable(cmd);
            if (dtTran.Rows.Count > 0)
            {
                for (int i = 0; i < dtTran.Rows.Count; i++)
                {
                    TransDisbamt = Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);
                    Trtype = dtTran.Rows[i]["tr_type"].ToString();
                    ////////////////// START PENING BALANCE CALC ///////////////////////////
                    if (TransDisbamt > 0 && Trtype == "o")
                    {
                        Arin_prinout = Arin_prinout + Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);
                        Arin_FromDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]).AddDays(1);
                        vftotdays = 0;
                        Arin_BalCurrInt = NullDecimal(dtTran.Rows[i]["int_curr"]);
                        Arin_BalOdInt = NullDecimal(dtTran.Rows[i]["int_od"]);
                        Arin_BalOdPrin = NullDecimal(dtTran.Rows[i]["prin_od"]);
                    }

                    ////////////////// START DISBURSEMENT CALC ///////////////////////////
                    if (TransDisbamt > 0 && Trtype == "d")
                    {
                        if (DisbStatus == true)
                        {
                            RepStatus = false;
                            vftotdays = 0;
                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                            //Arin_intcurr = Arin_intcurr + (Arin_prinout * ((vfroi / 100) * (vftotdays / 365)));
                            Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);
                            Arin_DisbAmt = Arin_DisbAmt + Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);
                            Arin_DisbDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]);
                            Arin_prinout = Arin_prinout + Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);
                            Arin_FromDt = Arin_DisbDt;
                            RepStatus = false;
                            if (DisbInt == true)
                            {
                                Arin_intcurr = Arin_intcurr + ((Arin_DisbAmt * vfroi * 1) / 36500);
                            }


                        }
                        else
                        {
                            if (Convert.ToDateTime(dtTran.Rows[i]["dt"]) > Arin_FromDt)
                            {
                                vftotdays = 0;
                                vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);
                            }
                            Arin_DisbAmt = Arin_DisbAmt + Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);
                            Arin_DisbDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]);
                            Arin_prinout = Arin_prinout + Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);

                            if (DisbInt == true)
                            {
                                Arin_FromDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]);
                                Arin_intcurr = Arin_intcurr + ((Arin_DisbAmt * vfroi * 1) / 36500);
                            }
                            else
                            {
                                Arin_FromDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]);
                            }

                            DisbStatus = true;
                            RepStatus = false;
                        }
                    }
                    ////////////////// START REPAYMENT CALC ///////////////////////////
                    if (Arin_prinout > 0 && Trtype == "r")
                    {
                        //decimal TodayCurrInt, TodayOdInt, TodayCurrPrin, TodayOdPrin, TodayAdvPrin;
                        //TodayCurrInt = NullDecimal(dtTran.Rows[i]["int_curr"]);
                        //TodayOdInt = NullDecimal(dtTran.Rows[i]["int_od"]);
                        //TodayCurrPrin = NullDecimal(dtTran.Rows[i]["prin_curr"] );
                        //TodayOdPrin = NullDecimal(dtTran.Rows[i]["prin_od"] );
                        //TodayAdvPrin = NullDecimal(dtTran.Rows[i]["prin_adv"]);

                        Repaid = true;
                        RepStatus = true;
                        vftotdays = 0;
                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);

                        DedInt = false;

                        //if( OdDays <= 0)
                        // {
                        //     break;
                        // }



                        if (NoOfInst == 0 || OdDays <= 0)
                        {
                            if (VFLASTDT >= Convert.ToDateTime(dtTran.Rows[i]["dt"]) || OdDays <= 0)
                            {

                                // CURRENT INT MODULE
                                if (Repaid == false)
                                {
                                    if (LoanKey != "r")
                                    {
                                        if (DisbInt == true)
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                        }
                                        else
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays) + 1;
                                        }
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                    }

                                }
                                else
                                {

                                    vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                }

                                if (vftotdays > 0)
                                {

                                    Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);
                                }

                                //// END CURR INT MODULE

                                if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                                {
                                    Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                    Arin_intod = Arin_intod + Arin_BalOdInt;
                                    Arin_BalCurrInt = 0;
                                    Arin_BalOdInt = 0;
                                }

                                Arin_intcurr = Math.Round(Arin_intcurr) - (NullDecimal(dtTran.Rows[i]["int_curr"]));

                            }
                            else
                            {

                                if (VFLASTDT < Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                {
                                    if (VFLASTDT > FinanceStart)
                                    {


                                        if (Arin_FromDt > VFLASTDT)
                                        {
                                            if (cdt > Arin_FromDt)
                                            {
                                                if (LoanKey != "r")
                                                {
                                                    vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                                }
                                                else
                                                {
                                                    vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                                }

                                                goto kk;  ////// REPAYMENT ALREADY DONE
                                            }

                                        }
                                        else
                                        {

                                            if (LoanKey != "r")
                                            {
                                                vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                            }
                                            else
                                            {
                                                vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);

                                            }
                                        }
                                        //'*****************END NEW CODE***************                                        
                                        Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);

                                        if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                                        {
                                            Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                            Arin_intod = Arin_intod + Arin_BalOdInt;
                                            Arin_BalCurrInt = 0;
                                            Arin_BalOdInt = 0;
                                        }
                                        Arin_intcurr = Math.Round(Arin_intcurr) - NullDecimal(dtTran.Rows[i]["int_curr"]); // deduct curr int repay
                                        DedInt = true;
                                        vfCurrRestdays = vftotdays;
                                        //  '****** end printing

                                        //'od int calculation
                                        if (LoanKey != "r")
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays);
                                        }
                                        else
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays);
                                        }
                                    kk:;

                                        Arin_intod = Arin_intod + ((Arin_prinout * (vfroi + vfodroi) * vftotdays) / 36500);

                                        if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                                        {
                                            Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                            Arin_intod = Arin_intod + Arin_BalOdInt;
                                            Arin_BalCurrInt = 0;
                                            Arin_BalOdInt = 0;
                                        }
                                    }
                                    else //' if last repay date before finance start date
                                    {
                                        Arin_intod = Arin_intod + ((Arin_prinout * (vfroi + vfodroi) * vftotdays) / 36500);

                                        if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                                        {
                                            Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                            Arin_intod = Arin_intod + Arin_BalOdInt;
                                            Arin_BalCurrInt = 0;
                                            Arin_BalOdInt = 0;
                                        }
                                    }

                                    if (DedInt == true)
                                    {
                                        Arin_intod = Math.Round(Arin_intod) - NullDecimal(dtTran.Rows[i]["int_od"]); // '* deduct curr int repay
                                    }
                                    else
                                    {
                                        Arin_intod = Math.Round(Arin_intod) - (NullDecimal(dtTran.Rows[i]["int_od"]) + NullDecimal(dtTran.Rows[i]["int_curr"])); //'* deduct curr int repay
                                    }

                                }
                            }
                            Arin_prinout = Arin_prinout - (NullDecimal(dtTran.Rows[i]["prin_curr"]) + NullDecimal(dtTran.Rows[i]["prin_od"]) + NullDecimal(dtTran.Rows[i]["prin_adv"]));// '* deduct prin repay
                        }
                        else //'  if no of installment >0
                        {

                            if (Arin_FromDt == FinanceStart)
                            {
                                CullPrin = Arin_TotLoan - Arin_prinout - CullPrin;
                            }
                            SqlCommand cmdprin = new SqlCommand();
                            cmdprin.CommandText = ("select sum(prin_curr),sum(prin_od),sum(prin_adv) from loan_repay_det where sl_code=@slc and rep_date>=@frdt and rep_date<@dt");
                            cmdprin.Parameters.AddWithValue("@frdt", Arin_FromDt);
                            cmdprin.Parameters.AddWithValue("@slc", slcode);
                            cmdprin.Parameters.AddWithValue("@dt", Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date);

                            DataTable DtCull = getdatatable(cmdprin);
                            if (DtCull.Rows.Count > 0)
                            {
                                CullPrin = CullPrin + NullDecimal(DtCull.Rows[0][0]) + NullDecimal(DtCull.Rows[0][1]) + NullDecimal(DtCull.Rows[0][2]);
                            }

                            if (VFLASTDT >= Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                            {
                                // CURRENT INT MODULE
                                if (Repaid == false)
                                {
                                    if (LoanKey != "r")
                                    {
                                        if (DisbInt == true)
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                        }
                                        else
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays) + 1;
                                        }
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                    }

                                }
                                else
                                {

                                    vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                }

                                if (vftotdays > 0)
                                {
                                    Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);
                                }

                                //// END CURR INT MODULE
                                if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                                {
                                    Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                    Arin_intod = Arin_intod + Arin_BalOdInt;
                                    Arin_BalCurrInt = 0;
                                    Arin_BalOdInt = 0;
                                }

                                Arin_intcurr = Math.Round(Arin_intcurr) - NullDecimal(dtTran.Rows[i]["int_curr"]);// '* deduct curr int repay
                                                                                                                  // Arin_intod = Arin_intod + InstOdcalcN(CDate(RsArin!dt));
                                                                                                                  /////////////////////////// OD PRINCIPLE AND INTEREST CALCULATION ///////////////////////
                                                                                                                  // DateTime FindDta;
                                int odp = OdDays, OneInst = 0;
                                string aa = "", aa1 = "";
                                decimal tempTotInst = 0, CullPrin1, printemp;
                                Boolean Finda;
                                Arin_prinOd = 0;
                                Arin_prinCurr = 0;
                                TempArinOd = 0;
                                TempOddaysT = 0;

                                tempTotInst = TempOddaysT;
                                TempOddaysT = TempOddaysT * odp;
                                DateTime TempAppldt;

                                TempAppldt = vfappldt.AddDays(1);
                                DateTime TempOdDate = DateTime.Now.Date, TempOdDate1 = DateTime.Now.Date, NearodDt, FetchOddt;
                                odp = Convert.ToInt32(TempOddaysT);
                                TempOddaysT = (TempOddaysT) * odp;
                                Arin_ODdate = (TempAppldt).AddMonths(odp - 1);
                                FetchOddt = (TempAppldt).AddMonths(odp);

                                /////////////////////////////// NEW METHOD ////////////////////
                                NearodDt = Arin_FromDt;
                                CullPrin1 = Convert.ToDecimal(((Arin_TotLoan - Arin_prinout)));
                                printemp = CullPrin;// CullPrin1;
                                TempOddaysT = Convert.ToDecimal(((Arin_TotLoan - Arin_prinout) / vfinstamt)) + 1;
                                decimal countInst1 = Math.Truncate(tempTotInst);
                                Arin_ODdate = (TempAppldt).AddMonths(Convert.ToInt32(countInst1));

                                if (Arin_ODdate > Arin_FromDt)
                                {
                                    NearodDt = Arin_ODdate;
                                }

                                aa1 = string.Format("{0:N10}", TempOddaysT);
                                string[] cut2 = aa1.Split('.');
                                aa = cut2[1].ToString();
                                aa = "." + aa;
                                Arin_ODdate = (TempAppldt).AddMonths(Convert.ToInt32(countInst1));
                                FetchOddt = (TempAppldt).AddMonths(Convert.ToInt32(countInst1));

                            AriSt1:;
                                if (NearodDt <= Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                {
                                    if (Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date >= FetchOddt)
                                    {
                                        if (FetchOddt >= NearodDt)
                                        {
                                            vftotdays = Convert.ToInt32((FetchOddt.Date - NearodDt.Date).TotalDays);
                                            TempOdPrin = ((countInst1 - 1) * vfinstamt) + (OneInst * vfinstamt) - printemp;

                                            if (TempOdPrin > 0)
                                            {
                                                Arin_intod = Arin_intod + (TempOdPrin * vftotdays * vfodroi / 36500);
                                            }
                                            else
                                            {
                                                TempOdPrin = 0;
                                            }
                                            NearodDt = FetchOddt;

                                        }

                                        FetchOddt = (FetchOddt).AddMonths(OdDays);
                                        ++OneInst;
                                        goto AriSt1;
                                    }
                                }

                                /////////////////////////////// END NEW METHOD ////////////////

                                ///////////////////NEW CODE///////////////////////////////////

                                if (NearodDt <= Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                {
                                    FetchOddt = NearodDt;
                                    if (Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date >= FetchOddt)
                                    {
                                        if (NearodDt != FetchOddt)
                                        {
                                            vftotdays = Convert.ToInt32((FetchOddt.Date - NearodDt.Date).TotalDays);
                                            Finda = false;
                                        }
                                        else
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - NearodDt.Date).TotalDays);
                                            Finda = true;
                                        }
                                        TempOdPrin = ((Math.Truncate(tempTotInst - 1) + (OneInst - 1)) * vfinstamt) + vfinstamt - printemp;

                                        if (TempOdPrin > 0)
                                        {
                                            Arin_intod = Arin_intod + (TempOdPrin * vftotdays * (vfodroi) / 36500);
                                        }
                                        else
                                        {
                                            TempOdPrin = 0;
                                        }
                                        NearodDt = FetchOddt;
                                        FetchOddt = (FetchOddt).AddMonths(OdDays);
                                        ++OneInst;
                                        if (Finda == true)
                                        {
                                            goto ss;
                                        }
                                    }
                                }
                            ss:;
                                if (NearodDt > Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                {

                                    //vftotdays = Convert.ToInt32((cdt.Date - NearodDt.Date).TotalDays);
                                    //TempOdPrin = (RsF!balance_amt) -printemp
                                }

                                Arin_prinOd = TempOdPrin;


                                /////////////////////////// END OD PRIN AND INT CALC //////////////////////////////////////
                                Arin_intod = Math.Round(Arin_intod) - NullDecimal(dtTran.Rows[i]["int_od"]); //* deduct curr int repay
                                TempOdPrin = Arin_prinOd - NullDecimal(dtTran.Rows[i]["prin_od"]);

                            }
                            else
                            {
                                if (VFLASTDT < Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                {
                                    if (Arin_FromDt > VFLASTDT)
                                    {
                                        if (Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date >= Arin_FromDt)
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                            goto a1; // '************ REPAYMENT ALREADY DONE
                                        }
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                    }

                                    Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);

                                    //Bal_OdIntCalcSIMPLE
                                    if (Arin_BalOdPrin > 0)
                                    {
                                        Arin_intod = Arin_intod + ((Arin_BalOdPrin * vfodroi * vftotdays) / 36500);
                                        Arin_prinOd = Arin_prinOd + Arin_BalOdPrin;
                                        Arin_BalOdPrin = 0;
                                    }




                                    vfCurrRestdays = vftotdays;
                                    vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays);

                                a1:;

                                    if (Arin_FromDt == FinanceStart)
                                    {

                                        Arin_intod = Arin_intod + ((Arin_prinout * (vfroi + vfodroi) * vftotdays) / 36500);


                                        if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                                        {
                                            Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                            Arin_intod = Arin_intod + Arin_BalOdInt;
                                            Arin_BalCurrInt = 0;
                                            Arin_BalOdInt = 0;
                                        }

                                        if (Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date > VFLASTDT)
                                        {// ' ******* if last repayment date over then collect only od int
                                            Arin_intod = (Math.Round(Arin_intcurr) + Math.Round(Arin_intod)) - NullDecimal(dtTran.Rows[i]["int_od"]) - NullDecimal(dtTran.Rows[i]["int_curr"]);
                                            Arin_intcurr = 0;
                                        }
                                        else
                                        {
                                            Arin_intcurr = Math.Round(Arin_intcurr) - NullDecimal(dtTran.Rows[i]["int_curr"]);// '* deduct curr int repay
                                            Arin_intod = Math.Round(Arin_intod) - NullDecimal(dtTran.Rows[i]["int_od"]);// '* deduct curr int repay
                                        }
                                    }
                                    else
                                    {
                                        Arin_intod = Arin_intod + ((Arin_prinout * (vfroi + vfodroi) * vftotdays) / 36500);
                                        Arin_prinOd = Arin_prinout;
                                        //     '****** end printing
                                        //' end od int calc
                                        if (TempOdPrin > 0)
                                        { ////////// od int calc against od prin. installment
                                            if (Arin_FromDt < VFLASTDT)
                                            {
                                                vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                                //Arin_intod = Arin_intod + (TempOdPrin * (((vfodroi) / 100) * (vftotdays / 365)));
                                                Arin_intod = Arin_intod + (TempOdPrin * vfodroi * vftotdays / 36500);

                                            }
                                        }


                                        if (Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date > VFLASTDT)
                                        {// ' ******* if last repayment date over then collect only od int
                                            Arin_intod = (Math.Round(Arin_intcurr) + Math.Round(Arin_intod)) - NullDecimal(dtTran.Rows[i]["int_od"]) - NullDecimal(dtTran.Rows[i]["int_curr"]);
                                            Arin_intcurr = 0;
                                        }
                                        else
                                        {
                                            Arin_intcurr = Math.Round(Arin_intcurr) - NullDecimal(dtTran.Rows[i]["int_curr"]);// '* deduct curr int repay
                                            Arin_intod = Math.Round(Arin_intod) - NullDecimal(dtTran.Rows[i]["int_od"]); //'* deduct curr int repay
                                        }
                                    }
                                }
                            }

                            Arin_prinout = Arin_prinout - (NullDecimal(dtTran.Rows[i]["prin_curr"]) + NullDecimal(dtTran.Rows[i]["prin_od"]) + NullDecimal(dtTran.Rows[i]["prin_adv"]));// '* deduct prin repay

                        }

                        Arin_FromDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date;
                        DisbStatus = false;
                        Repaid = false;

                    }

                }
            }


            dtTran.Dispose();

            //'******** calculate from last repayment date
            CullPrin = Arin_TotLoan - Arin_prinout - CullPrin;
            SqlCommand cmdprin1 = new SqlCommand();
            cmdprin1.CommandText = ("select isnull(sum(prin_curr),0),isnull(sum(prin_od),0),isnull(sum(prin_adv),0) from loan_repay_det where  sl_code=@slc and rep_date>=@frdt and rep_date<@dt");
            cmdprin1.Parameters.AddWithValue("@frdt", Arin_FromDt);
            cmdprin1.Parameters.AddWithValue("@slc", slcode);
            cmdprin1.Parameters.AddWithValue("@dt", cdt);

            DataTable DtCull1 = getdatatable(cmdprin1);
            if (DtCull1.Rows.Count > 0)
            {
                CullPrin = CullPrin + NullDecimal(DtCull1.Rows[0][0]) + NullDecimal(DtCull1.Rows[0][1]) + NullDecimal(DtCull1.Rows[0][2]);
            }
            DtCull1.Dispose();

            Repaid = false;
            vftotdays = 0;

            if (LoanKey != "r")
            {
                if (Arin_FromDt != VFLASTDT)
                {

                }
            }

            //            if (Arin_FromDt == cdt.Date && LoanKey == "r")
            //            {
            //                goto cend;
            //            }

            if (NoOfInst == 0 || OdDays <= 0)
            {
                if (VFLASTDT >= cdt || OdDays <= 0)
                {
                    // CURRENT INT MODULE
                    if (Repaid == false)
                    {
                        if (LoanKey != "r")
                        {
                            if (DisbInt == true)
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                            }
                            else
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays) + 1;
                            }
                        }
                        else
                        {
                            vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                        }

                    }
                    else
                    {

                        vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                    }

                    if (vftotdays > 0)
                    {
                        //Arin_intcurr = Arin_intcurr + (Arin_prinout * ((vfroi / 100) * (vftotdays / 365)));
                        Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);
                    }

                    //// END CURR INT MODULE

                    if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                    {
                        Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                        Arin_intod = Arin_intod + Arin_BalOdInt;
                        Arin_BalCurrInt = 0;
                        Arin_BalOdInt = 0;
                    }
                }
                else
                {
                    if (VFLASTDT < cdt)
                    {
                        if (VFLASTDT > FinanceStart)
                        {

                            if (Arin_FromDt > VFLASTDT)
                            {
                                if (cdt > Arin_FromDt)
                                {
                                    if (LoanKey != "r")
                                    {
                                        if (DisbInt == true)
                                        {
                                            vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                                        }
                                        else
                                        {
                                            vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays) + 1;
                                        }
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                                    }
                                    goto a; // '************ REPAYMENT ALREADY DONE
                                    //   bool repdone = false;
                                }
                            }
                            else
                            {
                                if (LoanKey != "r")
                                {
                                    if (DisbInt == true)
                                    {
                                        vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays) + 1;
                                    }

                                }
                                else
                                {
                                    vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                }
                            }

                            Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);

                            if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                            {
                                Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                Arin_intod = Arin_intod + Arin_BalOdInt;
                                Arin_BalCurrInt = 0;
                                Arin_BalOdInt = 0;
                            }

                            if (LoanKey != "r")
                            {
                                if (DisbInt == true)
                                {
                                    vftotdays = Convert.ToInt32((cdt.Date - VFLASTDT.Date).TotalDays);
                                }
                                else
                                {
                                    if (Arin_FromDt != cdt)
                                    {
                                        vftotdays = Convert.ToInt32((cdt.Date - VFLASTDT.Date).TotalDays) + 1;
                                    }
                                }
                            }
                            else
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - VFLASTDT.Date).TotalDays);
                            }
                        a:;

                            Arin_intod = Arin_intod + ((Arin_prinout * (vfroi + vfodroi) * vftotdays) / 36500);

                        }
                        else
                        {
                            if (LoanKey != "r")
                            {

                                if (DisbInt == true)
                                {
                                    vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                                }
                                else
                                {
                                    vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays) + 1;
                                }
                            }
                            else
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                            }

                            Arin_intod = Arin_intod + ((Arin_prinout * (vfroi + vfodroi) * vftotdays) / 36500);

                            if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                            {
                                Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                Arin_intod = Arin_intod + Arin_BalOdInt;
                                Arin_BalCurrInt = 0;
                                Arin_BalOdInt = 0;
                            }
                        }
                        Arin_prinOd = Arin_prinout;
                    }
                }


            }
            else
            {
                if (VFLASTDT >= cdt)
                {
                    // CURRENT INT MODULE
                    if (Repaid == false)
                    {
                        if (LoanKey != "r")
                        {
                            if (DisbInt == true)
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                            }
                            else
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays) + 1;
                            }
                        }
                        else
                        {
                            vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                        }

                    }
                    else
                    {

                        vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                    }

                    if (vftotdays > 0)
                    {
                        Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);
                    }

                    //// END CURR INT MODULE
                    if (Arin_BalCurrInt > 0)
                    {
                        Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                        Arin_BalCurrInt = 0;
                    }

                    //Arin_intod = Arin_intod + InstOdcalcN(dt);
                    /////////////// NEW OD INTEREST CALCULATION FOR C# CODE BY ARINDAM
                    int odp = OdDays, countInst = 0, OneInst = 0;
                    string aa = "", aa1 = "";
                    decimal tempTotInst = 0, CullPrin1, printemp;
                    decimal Ario = 0;
                    Boolean Finda;
                    Arin_prinOd = 0;
                    Arin_prinCurr = 0;
                    TempArinOd = 0;
                    TempOddaysT = 0;

                    tempTotInst = TempOddaysT;
                    TempOddaysT = TempOddaysT * odp;
                    DateTime TempAppldt;

                    TempAppldt = vfappldt.AddDays(1);
                    DateTime TempOdDate = DateTime.Now.Date, TempOdDate1 = DateTime.Now.Date, NearodDt, FetchOddt;
                    odp = Convert.ToInt32(TempOddaysT);
                    TempOddaysT = (TempOddaysT) * odp;
                    Arin_ODdate = (TempAppldt).AddMonths(odp - 1);
                    FetchOddt = (TempAppldt).AddMonths(odp);

                    /////////////////////////////// NEW METHOD ////////////////////
                    NearodDt = Arin_FromDt;
                    CullPrin1 = Convert.ToDecimal(((Arin_TotLoan - Arin_prinout)));
                    printemp = CullPrin1;
                    TempOddaysT = Convert.ToDecimal(((Arin_TotLoan - Arin_prinout) / vfinstamt)) + 1;
                    decimal countInst1 = Math.Truncate(tempTotInst);
                    Arin_ODdate = (TempAppldt).AddMonths(Convert.ToInt32(countInst1));

                    if (Arin_ODdate > Arin_FromDt)
                    {
                        NearodDt = Arin_ODdate;
                    }

                    aa1 = string.Format("{0:N10}", TempOddaysT);
                    string[] cut2 = aa1.Split('.');
                    aa = cut2[1].ToString();
                    aa = "." + aa;
                    Arin_ODdate = (TempAppldt).AddMonths(Convert.ToInt32(countInst1));
                    FetchOddt = (TempAppldt).AddMonths(Convert.ToInt32(countInst1));

                AriSt1:;
                    if (NearodDt <= cdt.Date)
                    {
                        if (cdt.Date >= FetchOddt)
                        {
                            if (FetchOddt >= NearodDt)
                            {
                                vftotdays = Convert.ToInt32((FetchOddt.Date - NearodDt.Date).TotalDays);
                                TempOdPrin = ((countInst1 - 1) * vfinstamt) + (OneInst * vfinstamt) - printemp;

                                if (TempOdPrin > 0)
                                {
                                    Arin_intod = Arin_intod + (TempOdPrin * vftotdays * vfodroi / 36500);
                                }
                                else
                                {
                                    TempOdPrin = 0;
                                }
                                NearodDt = FetchOddt;

                            }

                            FetchOddt = (FetchOddt).AddMonths(OdDays);
                            ++OneInst;
                            goto AriSt1;
                        }
                    }
                    /////////////////////////////// END NEW METHOD ////////////////

                    ///////////////////NEW CODE///////////////////////////////////

                    if (NearodDt <= cdt.Date)
                    {
                        FetchOddt = NearodDt;
                        if (cdt.Date >= FetchOddt)
                        {
                            if (NearodDt != FetchOddt)
                            {
                                vftotdays = Convert.ToInt32((FetchOddt.Date - NearodDt.Date).TotalDays);
                                Finda = false;
                            }
                            else
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - NearodDt.Date).TotalDays);
                                Finda = true;
                            }
                            TempOdPrin = ((Math.Truncate(tempTotInst - 1) + (OneInst - 1)) * vfinstamt) + vfinstamt - printemp;

                            if (TempOdPrin > 0)
                            {
                                Arin_intod = Arin_intod + (TempOdPrin * vftotdays * (vfodroi) / 36500);
                            }
                            else
                            {
                                TempOdPrin = 0;
                            }
                            NearodDt = FetchOddt;
                            FetchOddt = (FetchOddt).AddMonths(OdDays);
                            ++OneInst;
                            if (Finda == true)
                            {
                                goto ss;
                            }
                        }
                    }
                ss:;
                    if (NearodDt > cdt.Date)
                    {

                        //vftotdays = Convert.ToInt32((cdt.Date - NearodDt.Date).TotalDays);
                        //TempOdPrin = (RsF!balance_amt) -printemp
                    }

                    Arin_prinOd = TempOdPrin;
                    ////////////////////////////////////////////////////////////////
                    //if (Arin_ODdate < cdt.Date)
                    //                {
                    //                    TempOdDate1 = Arin_ODdate;
                    //                    TempOdDate = Arin_ODdate;
                    //                    for (int a = 1; a <= (NoOfInst - odp-1); a++)
                    //                    {   
                    //                        TempOdDate1 = TempOdDate1.AddMonths(OdDays);
                    //                        if (TempOdDate1 < cdt.Date)
                    //                        {

                    //                            Arin_prinOd = Arin_prinOd + vfinstamt;

                    //                            vftotdays = Convert.ToInt32((TempOdDate1.Date - TempOdDate.Date).TotalDays);



                    //                            TempArinOd = TempArinOd + ((Arin_prinOd * vfodroi * vftotdays) / 36500);
                    //                            TempOdDate = TempOdDate1;
                    //                            countInst = countInst + 1;
                    //                        }
                    //                        else
                    //                        {
                    //                            if (TempOdDate < cdt.Date)
                    //                            {
                    //                                if (Convert.ToDecimal(aa)>0)
                    //                                {
                    //                                    Arin_prinOd = Arin_prinOd + vfinstamt-(vfinstamt * Convert.ToDecimal(aa));
                    //                                }
                    //                                else
                    //                                {
                    //                                    Arin_prinOd = Arin_prinOd + vfinstamt;
                    //                                }

                    //                                vftotdays = Convert.ToInt32((cdt.Date - TempOdDate.Date).TotalDays);
                    //                                TempArinOd = TempArinOd + ((Arin_prinOd * vfodroi * vftotdays) / 36500);
                    //                                countInst = countInst + 1;
                    //                            }

                    //                            goto EndLAb;
                    //                        }


                    //                       // countInst = countInst + 1;
                    //                    }
                    //                }

                    //            EndLAb: ;
                    ////////////////////////// PRIN CURR CALCULATION ///////////////////////
                    if (NearodDt <= cdt.Date)
                    {
                        Arin_prinCurr = vfinstamt;
                    }
                    //countInst = countInst +0;
                    //                if Convert.ToDecimal(aa) > 0)
                    //                {
                    //                    Arin_prinCurr = Arin_prinCurr + vfinstamt - (vfinstamt * Convert.ToDecimal(aa));
                    //                }
                    //                //Arin_prinCurr = Convert.ToInt32(TempOddaysT) * vfinstamt;
                    //                Arin_prinCurr = Arin_prinCurr + (vfinstamt * countInst) - Convert.ToInt32(TempOddaysT) * vfinstamt;
                    //                //Arin_prinCurr = Arin_prinCurr + (vfinstamt * countInst) ;
                    //                Arin_prinCurr = Arin_prinCurr - Arin_prinOd;

                    //////////////////////////// END PRI CURR CALC //////////////////////
                    Arin_intod = Arin_intod + Math.Round(TempArinOd);


                    /////////////////////////// END OD PRIN AND INT CALC //////////////////////////////////////

                }
                else
                {
                    if (VFLASTDT < cdt)
                    {
                        if (Arin_FromDt > VFLASTDT)
                        {
                            if (cdt >= Arin_FromDt)
                            {
                                if (LoanKey != "r")
                                {
                                    if (DisbInt == true)
                                    {
                                        vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays) + 1;
                                    }
                                }
                                else
                                {
                                    vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                                }
                                goto a2;//   '************ REPAYMENT ALREADY DONE
                            }
                        }
                        else
                        {
                            if (LoanKey != "r")
                            {
                                if (DisbInt == true)
                                {
                                    vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                }
                                else
                                {
                                    vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays) + 1;
                                }
                            }
                            else
                            {
                                vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                            }

                        }
                        if (vftotdays > 0)
                        {
                            Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);
                        }

                        if (Arin_BalCurrInt > 0)
                        {

                            Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                            Arin_BalCurrInt = 0;
                        }
                        //Bal_OdIntCalcSIMPLE
                        if (Arin_BalOdPrin > 0)
                        {
                            Arin_intod = Arin_intod + ((Arin_BalOdPrin * vfodroi * vftotdays) / 36500);
                            Arin_prinOd = Arin_prinOd + Arin_BalOdPrin;
                            Arin_BalOdPrin = 0;
                        }

                        if (LoanKey != "r")
                        {
                            if (DisbInt == true)
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - VFLASTDT.Date).TotalDays);
                            }
                            else
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - VFLASTDT.Date).TotalDays) + 1;
                            }
                        }
                        else
                        {
                            vftotdays = Convert.ToInt32((cdt.Date - VFLASTDT.Date).TotalDays);
                        }
                    a2:;

                        if (Arin_FromDt == FinanceStart)
                        {
                            Arin_intod = Arin_intod + ((Arin_prinout * (vfroi + vfodroi) * vftotdays) / 36500);


                            if ((Arin_intcurr + Arin_BalOdInt) > 0)
                            {
                                Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                Arin_intod = Arin_intod + Arin_BalOdInt;
                                Arin_BalCurrInt = 0;
                                Arin_BalOdInt = 0;
                            }
                        }
                        else
                        {
                            if (vftotdays > 0)
                            {
                                Arin_intod = Arin_intod + ((Arin_prinout * (vfroi + vfodroi) * vftotdays) / 36500);
                            }
                        }
                        Arin_prinOd = Arin_prinout;

                        if (TempOdPrin > 0) ////////// od int calc against od prin. installment
                        {
                            if (Arin_FromDt < VFLASTDT)
                            {
                                vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                if (vftotdays > 0)
                                {
                                    Arin_intod = Arin_intod + ((TempOdPrin * vfodroi * vftotdays) / 36500);

                                }

                            }
                        }


                    }
                }
            }

            if (Arin_DisbAmt > 0)
            {

            }
            else
            {
                Arin_DisbAmt = vfnetloan;
                Arin_DisbDt = vfinsdt;
            }

            Arin_intcurr = Math.Round(Arin_intcurr);
            Arin_intod = Math.Round(Arin_intod);
            Arin_prinCurr = Math.Round(Arin_prinCurr);
            Arin_prinOd = Math.Round(Arin_prinOd);
            if (cdt > VFLASTDT)
            {
                if (OdDays > 0)
                {
                    Arin_intod = Arin_intcurr + Arin_intod;
                    Arin_intcurr = 0;
                }
            }
            if (Arin_prinout > 0)
            {
                //***************** OD INTEREST SET
                if (vfodintSet > 0)
                {
                    if ((Arin_intcurr + Arin_intod) > vfodintSet)
                    {
                        Arin_intod = vfodintSet;
                        Arin_intcurr = 0;
                    }
                }

                //'************** INTEREST NOT EXICEED TO PRINCIPLE

                if (Arin_intod > Arin_prinout)
                {
                    if (Arin_Security100 == "y")
                    {

                    }
                    else
                    {
                        Arin_intod = Arin_prinout;
                        Arin_intcurr = 0;
                    }
                }

                if (Arin_intcurr > Arin_prinout)
                {
                    if (Arin_Security100 == "y")
                    {

                    }
                    else
                    {
                        Arin_intcurr = Arin_prinout;
                        Arin_intod = 0;
                    }
                }

                //'******************************************** END
                int odp = 0;
                if (vfinstap == "Yes")
                {

                    if (vfodpr == "MONTHLY")
                    {
                        odp = 1;
                    }
                    else if (vfodpr == "QUARTERLY")
                    {
                        odp = 3;
                    }
                    else if (vfodpr == "HALF YEARLY")
                    {
                        odp = 6;
                    }
                    else if (vfodpr == "YEARLY")
                    {
                        odp = 12;
                    }

                    DateTime FindDt;
                    decimal TempOddays = 0;
                    if (Arin_TotLoan - Arin_prinout > 0)
                    {

                        if (vfinstamt > 0)
                        {
                            TempOddays = Convert.ToDecimal(((Arin_TotLoan - Arin_prinout) / vfinstamt) + 1);
                            TempOddays = TempOddays * odp;
                            DateTime TempAppldt;

                            TempAppldt = vfappldt.AddDays(1);
                            FindDt = (TempAppldt).AddMonths(odp);

                            OdDays = Convert.ToInt32(TempOddays);
                            Arin_ODdate = TempAppldt.AddMonths(OdDays);
                            /////////////// NEW OD INTEREST CALCULATION FOR C# CODE BY ARINDAM
                            DateTime TempOdDate = DateTime.Now.Date, TempOdDate1 = DateTime.Now.Date;
                            if (Arin_ODdate <= cdt.Date)
                            {
                                TempOdDate1 = Arin_ODdate;
                                TempOdDate = Arin_ODdate;
                                for (int a = 1; a <= (NoOfInst - TempOddays); a++)
                                {
                                    TempOdDate1 = TempOdDate1.AddMonths(odp);
                                    if (TempOdDate1 <= cdt.Date)
                                    {
                                        // FindODP = FindODP + 1;
                                        vftotdays = Convert.ToInt32((TempOdDate1.Date - TempOdDate.Date).TotalDays);
                                        Arin_prinOd = Arin_prinOd + vfinstamt;
                                        Arin_intod = Arin_intod + ((Arin_prinOd * vfodroi * vftotdays) / 36500);
                                        TempOdDate = TempOdDate1;
                                    }
                                    else
                                    {
                                        if (TempOdDate <= cdt.Date)
                                        {
                                            Arin_prinOd = Arin_prinOd + vfinstamt;
                                            vftotdays = Convert.ToInt32((cdt.Date - TempOdDate.Date).TotalDays);
                                            Arin_intod = Arin_intod + ((Arin_prinOd * vfodroi * vftotdays) / 36500);

                                        }

                                        goto EndL;
                                    }



                                }
                            }
                        EndL:;
                            Arin_intod = Math.Round(Arin_intod);
                        }
                    }
                }
            }

            if (NoOfInst <= 0)
            {
                Arin_ODdate = VFLASTDT;
            }
            //////////////////////////////////
            ReturnAll = (Arin_intcurr + "," + Arin_intod + "," + Arin_prinOd + "," + Arin_ODdate + "," + Arin_prinout + "," + Arin_prinCurr + "," + vfinstamt + "," + vfodpr).ToString();
            //ReturnAll = (Arin_intcurr + "," + Arin_intod + "," + Arin_prinOd ).ToString();
            // ReturnAll = "Error : ";
            return ReturnAll;
        }

        public string MASTER_MODULE_COMPOUND(int slcode, DateTime cdt, string lKey)
        {
            /////////////// declaration
            SqlCommand cmd = new SqlCommand();
            double CalPrd = 0;
            int totCurrprd = 0, calcprd = 0, totprd = 0, vfRestdays = 0, compprd = 0, OdDays = 0, vftotdays = 0, NoOfInst = 0, vfCurrRestdays = 0, Duration = 0, Compound_Calc = 0, Compound_Year = 0, Compound_Mnth = 0;
            bool DisbStatus = false, RepStatus = false, DisbInt = false, Repaid = false, DedInt = false;
            string vfrepmode = "", Arin_Security100 = "", ErrNo = "", LoanKey = lKey, ReturnAll = "", vfodpr = "", Trtype = "", vfinstap = "";
            DateTime dt_to, Arin_ODdate = DateTime.Now.Date, vfinsdt = DateTime.Now.Date, Arin_DisbDt, VFLASTDT = DateTime.Now.Date, LoanopenDt = DateTime.Now.Date, vfappldt = DateTime.Now.Date, Arin_FromDt = DateTime.Now.Date, FinanceStart = DateTime.Now.Date;
            decimal totintod = 0, totint1 = 0, totint2 = 0, vfintcur = 0, vfodintSet = 0, vfroi = 0, vfodroi = 0, vfinstamt = 0;
            decimal TempOdPrin = 0, CullPrin = 0, Arin_TotLoan = 0, Arin_intcurr = 0, Arin_intod = 0, Arin_prinOd = 0, Arin_prinCurr = 0, Arin_DisbAmt = 0, Arin_prinout = 0;
            decimal vfnetloan = 0, Arin_BalCurrInt = 0, Arin_BalOdInt = 0, Arin_BalOdPrin = 0, TransDisbamt = 0;
            DisbInt = false;
            //cmd.CommandText = "select c.FINANCE_START,s.DISB_INT from cn c,SECURITY_MASTER s where c.bank_code=@bnkc and s.bank_code=@bnkc";
            //cmd.Parameters.AddWithValue("@bnkc", bankcode);
            //DataTable dtCN = getdatatable(cmd);
            //if (dtCN.Rows.Count > 0)
            //{
            //    FinanceStart = Convert.ToDateTime(dtCN.Rows[0]["FINANCE_START"]);

            //    if (NullInt(dtCN.Rows[0]["DISB_INT"]) == 1)
            //    {
            //        DisbInt = true;
            //    }
            //}
            //dtCN.Dispose();

            try
            {
                cmd.CommandText = "select * from mdlloan(@slc,@dt)";
                cmd.Parameters.AddWithValue("@slc", slcode);
                cmd.Parameters.AddWithValue("@dt", cdt);
                //cmd.Parameters.AddWithValue("@bnkc", bankcode);
                DataTable dt = getdatatable(cmd);

                if (dt.Rows.Count > 0)
                {
                    vfrepmode = dt.Rows[0]["repay_mode"].ToString();
                    vfroi = Convert.ToDecimal(dt.Rows[0]["roi"]);
                    vfodroi = Convert.ToDecimal(dt.Rows[0]["odroi"]);
                    VFLASTDT = Convert.ToDateTime(dt.Rows[0]["LAST_REPAY_DET"]);
                    vfappldt = Convert.ToDateTime(dt.Rows[0]["opening_dt"]);
                    LoanopenDt = Convert.ToDateTime(dt.Rows[0]["opening_dt"]);
                    Arin_FromDt = LoanopenDt.Date;

                    Arin_TotLoan = Convert.ToDecimal(dt.Rows[0]["disb_amount"]);
                    vfnetloan = Convert.ToDecimal(dt.Rows[0]["sanc_amount"]);
                    vfodpr = dt.Rows[0]["odpr"].ToString();

                    vfinstap = dt.Rows[0]["INST_APPL"].ToString();
                    Duration = Convert.ToInt32(dt.Rows[0]["duration"]);
                    Arin_Security100 = dt.Rows[0]["SECURITY_100"].ToString();
                    vfodintSet = Convert.ToDecimal(dt.Rows[0]["odintset"].ToString());

                    if (vfinstap == "No")
                    {
                        NoOfInst = 0;
                    }
                    else
                    {
                        string aa = dt.Rows[0]["INST_ST_DATE"].ToString();
                        if (aa != "")
                        {
                            vfinsdt = Convert.ToDateTime(dt.Rows[0]["INST_ST_DATE"]);
                            vfinstap = dt.Rows[0]["INST_APPL"].ToString();
                            NoOfInst = Convert.ToInt32(dt.Rows[0]["no_of_inst"]);
                            vfinstamt = Convert.ToDecimal(dt.Rows[0]["INST_AMOUNT"]);
                        }

                    }

                    //// REPAY MODE CALCUATION

                    if (vfrepmode == "MONTHLY COMPOUND")
                    {
                        Compound_Year = 1200;
                        Compound_Calc = 30;
                        Compound_Mnth = 1;
                        compprd = 1;

                    }
                    else if (vfrepmode == "QUARTERLY COMPOUND")
                    {
                        Compound_Year = 400;
                        Compound_Calc = 90;
                        Compound_Mnth = 3;
                        compprd = 1;

                    }
                    else if (vfrepmode == "HALF YEARLY COMPOUND")
                    {
                        Compound_Year = 800;
                        Compound_Calc = 180;
                        Compound_Mnth = 6;
                        compprd = 1;

                    }
                    else if (vfrepmode == "YEARLY COMPOUND")
                    {
                        Compound_Year = 100;
                        Compound_Calc = 365;
                        Compound_Mnth = 12;
                        compprd = 1;
                    }
                    else
                    {
                        Compound_Calc = 0;
                        Compound_Year = 0;
                    }

                    ////////////////////////////
                }

                dt.Dispose();
            }
            catch (Exception en)
            {
                throw;
                // alert("MDLLOAN : " + slcode + "::" + en.Message);
            }
            ErrNo = "1";
            /////////// ODPR CALCULATE
            switch (vfodpr)
            {
                case "MONTHLY":
                    OdDays = 1;
                    break;
                case "QUARTERLY":
                    OdDays = 3;
                    break;
                case "HALF YEARLY":
                    OdDays = 6;
                    break;
                case "YEARLY":
                    OdDays = 12;
                    break;
                case "18 MONTH":
                    OdDays = 18;
                    break;
                case "AFTER LAST REPAY DATE":
                    OdDays = Duration;
                    break;
                default:
                    OdDays = 0;
                    break;
            }

            vfappldt = vfappldt.AddDays(-1);
            ////////////////LOOP///////////// 
            cmd.CommandText = "select * from LOAN_TRANSACTION where sl_code=@slc AND dt>=@opendt AND dt<=@dt order by dt,srl";
            cmd.Parameters.AddWithValue("@opendt", LoanopenDt);
            DataTable dtTran = getdatatable(cmd);
            if (dtTran.Rows.Count > 0)
            {
                for (int i = 0; i < dtTran.Rows.Count; i++)
                {
                    if (dtTran.Rows[i]["dt"].ToString() == "03/11/2017 00:00:00")
                    {
                        Trtype = dtTran.Rows[i]["tr_type"].ToString();
                    }
                    TransDisbamt = Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);
                    Trtype = dtTran.Rows[i]["tr_type"].ToString();
                    ////////////////// START PENING BALANCE CALC ///////////////////////////
                    if (TransDisbamt > 0 && Trtype == "o")
                    {
                        Arin_prinout = Arin_prinout + Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);
                        Arin_FromDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]).AddDays(1);
                        vftotdays = 0;
                        Arin_BalCurrInt = NullDecimal(dtTran.Rows[i]["int_curr"]);
                        Arin_BalOdInt = NullDecimal(dtTran.Rows[i]["int_od"]);
                        Arin_BalOdPrin = NullDecimal(dtTran.Rows[i]["prin_od"]);
                    }

                    ////////////////// START DISBURSEMENT CALC ///////////////////////////
                    if (TransDisbamt > 0 && Trtype == "d")
                    {
                        //decimal aa = Math.Pow(3, 4);

                        if (DisbStatus == true)
                        {
                            RepStatus = false;
                            vftotdays = 0;
                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);

                            vfRestdays = 0;
                            totprd = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(vftotdays / Compound_Calc)));

                            vfRestdays = vftotdays - (totprd * Compound_Calc);

                            CalPrd = Math.Pow((1 + (Convert.ToDouble(vfroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));
                            if (totprd > 0)
                            {
                                totint1 = (Arin_BalCurrInt + Arin_intcurr + Arin_prinout) * Convert.ToDecimal(CalPrd);
                            }
                            else
                            {
                                totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                            }

                            totint2 = totint1 * (vfroi * vfRestdays / 36500);
                            vfintcur = totint1 + totint2;
                            Arin_intcurr = Arin_intcurr + (Math.Round(vfintcur) - Arin_prinout);

                            Arin_DisbAmt = Arin_DisbAmt + Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);
                            Arin_DisbDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]);
                            Arin_prinout = Arin_prinout + Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);
                            Arin_FromDt = Arin_DisbDt.AddDays(-1);
                            RepStatus = false;
                        }
                        else
                        {
                            if (Convert.ToDateTime(dtTran.Rows[i]["dt"]) > Arin_FromDt)
                            {
                                vftotdays = 0;
                                vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);
                            }
                            Arin_DisbAmt = Arin_DisbAmt + Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);
                            Arin_DisbDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]);
                            Arin_prinout = Arin_prinout + Convert.ToDecimal(dtTran.Rows[i]["disb_amnt"]);

                            if (DisbInt == true)
                            {
                                Arin_FromDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]).AddDays(-1);
                            }
                            else
                            {
                                Arin_FromDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]);
                            }

                            DisbStatus = true;
                            RepStatus = false;
                        }
                    } // DISB END
                    if (Arin_prinout > 0 && Trtype == "r")
                    {

                        //DateTime DtTest = Convert.ToDateTime("2017-11-03").Date;
                        //if (Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date == DtTest.Date)
                        //{
                        //    Arin_intcurr = Arin_intcurr;
                        //}

                        Repaid = true;
                        RepStatus = true;
                        vftotdays = 0;
                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);


                        DedInt = false;

                        if (NoOfInst == 0 || OdDays <= 0)
                        {
                            if (VFLASTDT >= Convert.ToDateTime(dtTran.Rows[i]["dt"]) || OdDays <= 0)
                            {
                                vfRestdays = 0;
                                totprd = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(vftotdays / Compound_Calc)));
                                vfRestdays = vftotdays - (totprd * Compound_Calc);
                                totprd = 0;
                                calcprd = 0;
                                // CURRENT INT MODULE

                                if (Repaid == false)
                                {
                                    if (LoanKey != "r")
                                    {
                                        if (DisbInt == true)
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                        }
                                        else
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays) + 1;
                                        }
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                    }

                                }
                                else
                                {

                                    vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                }

                                //if (vftotdays > 0)
                                //{

                                //    Arin_intcurr = Arin_intcurr + ((Arin_prinout * vfroi * vftotdays) / 36500);
                                //}

                                totprd = 0;
                                calcprd = 0;
                                while (Arin_FromDt < Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                {
                                    if ((Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays)) / Compound_Calc >= compprd)
                                    {
                                        Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                                        totprd = totprd + 1;
                                    }
                                    else
                                    {
                                        calcprd = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                        vfRestdays = calcprd;
                                        goto ll;
                                    }
                                }
                            ll:;
                                dt_to = Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date;

                                CalPrd = Math.Pow((1 + (Convert.ToDouble(vfroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));
                                if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                                {
                                    if (totprd > 0)
                                    {
                                        totint1 = (Arin_BalCurrInt + Arin_BalOdInt + Arin_prinout) * Convert.ToDecimal(CalPrd);
                                    }
                                    else
                                    {
                                        totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                                    }
                                    Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                    Arin_intod = Arin_intod + Arin_BalOdInt;
                                    Arin_BalCurrInt = 0;
                                    Arin_BalOdInt = 0;
                                }
                                else
                                {
                                    if (totprd > 0)
                                    {
                                        totint1 = (Arin_prinout + Math.Round(Arin_intcurr)) * Convert.ToDecimal(CalPrd);
                                    }
                                    else
                                    {
                                        totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                                    }
                                }

                                //totint2 = totint1 * ((vfroi / 100) * (calcprd / 365));
                                totint2 = totint1 * ((vfroi * calcprd) / 36500);

                                vfintcur = (totint1 + totint2);
                                if (totprd > 0)
                                {
                                    Arin_intcurr = Arin_intcurr + (Math.Round(vfintcur - Arin_prinout) - Math.Round(Arin_intcurr));
                                }
                                else
                                {
                                    Arin_intcurr = Arin_intcurr + (Math.Round(vfintcur - Arin_prinout));
                                }
                                Arin_intcurr = Math.Round(Arin_intcurr) - (NullDecimal(dtTran.Rows[i]["int_curr"])); // deduct curr int repay
                                totCurrprd = totprd;
                                vfCurrRestdays = calcprd;
                                //// END INT MODULE
                            }
                            else
                            {

                                if (VFLASTDT < Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                {
                                    if (LoanKey != "r")
                                    {
                                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                    }

                                    vfRestdays = 0;
                                    totprd = Convert.ToInt32(Math.Truncate(Convert.ToDouble(vftotdays) / Convert.ToDouble(Compound_Calc)));
                                    vfRestdays = vftotdays - (totprd * Compound_Calc);
                                    totprd = 0;
                                    calcprd = 0;
                                    vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);

                                    while (Arin_FromDt < VFLASTDT.Date)
                                    {
                                        if ((Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays)) / Compound_Calc >= compprd)
                                        {
                                            Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                                            totprd = totprd + 1;
                                        }
                                        else
                                        {
                                            calcprd = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                            vfRestdays = calcprd;
                                            goto lla;
                                        }
                                    }
                                lla:;
                                    dt_to = VFLASTDT;
                                    totintod = 0;

                                    CalPrd = Math.Pow((1 + (Convert.ToDouble(vfroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));
                                    if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                                    {
                                        if (totprd > 0)
                                        {
                                            totint1 = (Arin_BalCurrInt + Arin_BalOdInt + Arin_prinout) * Convert.ToDecimal(CalPrd);
                                        }
                                        else
                                        {
                                            totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                                        }
                                        totintod = totint1;
                                        Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                        Arin_intod = Arin_intod + Arin_BalOdInt;
                                        Arin_BalCurrInt = 0;
                                        Arin_BalOdInt = 0;
                                    }
                                    else
                                    {
                                        if (totprd > 0)
                                        {
                                            totint1 = (Arin_prinout + Math.Round(Arin_intcurr)) * Convert.ToDecimal(CalPrd);
                                        }
                                        else
                                        {
                                            totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                                        }
                                        totintod = totint1;

                                    }
                                    totint2 = totint1 * ((vfroi * vfRestdays) / 36500);
                                    vfintcur = Math.Round(totint1 + totint2);
                                    if (totprd > 0)
                                    {
                                        Arin_intcurr = Arin_intcurr + (Math.Round(vfintcur - Arin_prinout) - Math.Round(Arin_intcurr));
                                    }
                                    else
                                    {
                                        Arin_intcurr = Arin_intcurr + (Math.Round(vfintcur - Arin_prinout));
                                    }
                                    Arin_intcurr = Math.Round(Arin_intcurr) - (NullDecimal(dtTran.Rows[i]["int_curr"])); // deduct curr int repay
                                    totCurrprd = totprd;
                                    vfCurrRestdays = vfRestdays;

                                    //'od int calculation
                                    if (LoanKey != "r")
                                    {
                                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays);
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays);
                                    }

                                    vfRestdays = 0;
                                    totprd = Convert.ToInt32(Math.Truncate(Convert.ToDouble(vftotdays) / Convert.ToDouble(Compound_Calc)));
                                    vfRestdays = vftotdays - (totprd * Compound_Calc);

                                    totprd = 0;
                                    calcprd = 0;
                                    vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays);
                                    Arin_FromDt = VFLASTDT;

                                    while (Arin_FromDt < Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                    {
                                        if ((Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays)) / Compound_Calc >= compprd)
                                        {
                                            Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                                            totprd = totprd + 1;
                                        }
                                        else
                                        {
                                            calcprd = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                            vfRestdays = calcprd;
                                            goto llc;
                                        }
                                    }
                                llc:;
                                    dt_to = Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date;
                                    CalPrd = Math.Pow((1 + (Convert.ToDouble(vfroi + vfodroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));

                                    if (totprd > 0)
                                    {
                                        totintod = (Arin_prinout + Math.Round(Arin_intcurr)) * Convert.ToDecimal(CalPrd);
                                    }
                                    else
                                    {
                                        totintod = Arin_prinout * Convert.ToDecimal(CalPrd);
                                    }

                                    totint2 = totintod * (((vfroi + vfodroi) * vfRestdays) / 36500);
                                    vfintcur = Math.Round(totintod + totint2);
                                    if (totprd > 0)
                                    {
                                        Arin_intod = Arin_intod + (Math.Round(vfintcur - Arin_prinout) - Math.Round(Arin_intcurr));
                                    }
                                    else
                                    {
                                        Arin_intod = Arin_intod + (Math.Round(vfintcur - Arin_prinout));
                                    }

                                    if (DedInt == true)
                                    {
                                        Arin_intod = Math.Round(Arin_intod) - NullDecimal(dtTran.Rows[i]["int_od"]); // '* deduct curr int repay
                                    }
                                    else
                                    {
                                        Arin_intod = Math.Round(Arin_intod) - (NullDecimal(dtTran.Rows[i]["int_od"]) + NullDecimal(dtTran.Rows[i]["int_curr"])); //'* deduct curr int repay
                                    }
                                    //totODprd = totprd;
                                    //vfODRestdays = vfRestdays;
                                }
                            }
                            Arin_prinout = Arin_prinout - (NullDecimal(dtTran.Rows[i]["prin_curr"]) + NullDecimal(dtTran.Rows[i]["prin_od"]) + NullDecimal(dtTran.Rows[i]["prin_adv"]));// '* deduct prin repay
                        }
                        else
                        {
                            CullPrin = Arin_TotLoan - Arin_prinout - CullPrin;
                            SqlCommand cmdprin = new SqlCommand();
                            cmdprin.CommandText = ("select sum(prin_curr),sum(prin_od),sum(prin_adv) from loan_repay_det where  sl_code=@slc and rep_date>=@frdt and rep_date<@dt");
                            cmdprin.Parameters.AddWithValue("@frdt", Arin_FromDt);
                            cmdprin.Parameters.AddWithValue("@slc", slcode);
                            cmdprin.Parameters.AddWithValue("@dt", Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date);

                            DataTable DtCull = getdatatable(cmdprin);
                            if (DtCull.Rows.Count > 0)
                            {
                                CullPrin = CullPrin + NullDecimal(DtCull.Rows[0][0]) + NullDecimal(DtCull.Rows[0][1]) + NullDecimal(DtCull.Rows[0][2]);
                            }

                            if (VFLASTDT >= Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                            {
                                vfRestdays = 0;
                                // CURRENT INT MODULE
                                if (Repaid == false)
                                {
                                    if (LoanKey != "r")
                                    {
                                        if (DisbInt == true)
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                        }
                                        else
                                        {
                                            vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays) + 1;
                                        }
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                    }

                                }
                                else
                                {

                                    vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                }

                                vfRestdays = 0;
                                totprd = Convert.ToInt32(Math.Truncate(Convert.ToDouble(vftotdays) / Convert.ToDouble(Compound_Calc)));
                                vfRestdays = vftotdays - (totprd * Compound_Calc);
                                totprd = 0;
                                calcprd = 0;
                                vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);

                                while (Arin_FromDt < Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                {
                                    if ((Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays)) / Compound_Calc >= compprd)
                                    {
                                        Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                                        totprd = totprd + 1;
                                    }
                                    else
                                    {
                                        calcprd = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                        vfRestdays = calcprd;
                                        goto lld;
                                    }
                                }
                            lld:;
                                dt_to = Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date;
                                CalPrd = Math.Pow((1 + (Convert.ToDouble(vfroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));

                                if (Arin_BalCurrInt > 0)
                                {
                                    if (totprd > 0)
                                    {
                                        totint1 = (Arin_BalCurrInt + Arin_prinout) * Convert.ToDecimal(CalPrd);
                                    }
                                    else
                                    {
                                        totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                                    }

                                    totintod = totint1;
                                    Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                    Arin_intod = Arin_intod + Arin_BalOdInt;
                                    Arin_BalCurrInt = 0;
                                    Arin_BalOdInt = 0;
                                }
                                else
                                {
                                    if (totprd > 0)
                                    {
                                        totint1 = (Arin_prinout + Math.Round(Arin_intcurr)) * Convert.ToDecimal(CalPrd);
                                    }
                                    else
                                    {
                                        totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                                    }
                                    totintod = totint1;
                                }
                                totint2 = totint1 * ((vfroi * vfRestdays) / 36500);

                                vfintcur = totint1 + totint2;
                                if (totprd > 0)
                                {
                                    Arin_intcurr = Arin_intcurr + (Math.Round(vfintcur - Arin_prinout - Math.Round(Arin_intcurr)));
                                }
                                else
                                {
                                    Arin_intcurr = Arin_intcurr + Math.Round(vfintcur - Arin_prinout);
                                }

                                Arin_intcurr = Math.Round(Arin_intcurr) - NullDecimal(dtTran.Rows[i]["int_curr"]); //deduct curr int repay
                                totCurrprd = totprd;
                                vfCurrRestdays = vfRestdays;
                                //If vfodpr = "EMI" Then
                                //    Arin_intod = Arin_intod + EMI_Arin(CDate(RsArin!dt))
                                //Else
                                //    Arin_intod = Arin_intod + CompoundN(CDate(RsArin!dt))
                                //End If
                                Arin_intod = Math.Round(Arin_intod) - NullDecimal(dtTran.Rows[i]["int_od"]); //'* deduct curr int repay
                                TempOdPrin = TempOdPrin - NullDecimal(dtTran.Rows[i]["prin_od"]);
                                //for loan repayment display
                                //totODprd = totprd;
                                //vfODRestdays = vfRestdays;
                            }
                            else
                            {
                                if (VFLASTDT < Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                {
                                    if (Arin_FromDt > VFLASTDT)
                                    {
                                        if (Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date >= Arin_FromDt)
                                        {
                                            if (LoanKey != "r")
                                            {
                                                vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                            }
                                            else
                                            {
                                                vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                            }
                                            goto arin;
                                            //GoTo a1;
                                        }
                                    }
                                    else
                                    {
                                        if (LoanKey != "r")
                                        {
                                            vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays) + 1;
                                        }
                                        else
                                        {
                                            vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                        }
                                    }

                                    vfRestdays = 0;
                                    totprd = Convert.ToInt32(Math.Truncate(Convert.ToDouble(vftotdays) / Convert.ToDouble(Compound_Calc)));
                                    vfRestdays = vftotdays - (totprd * Compound_Calc);

                                    totprd = 0;
                                    calcprd = 0;
                                    vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);

                                    while (Arin_FromDt < VFLASTDT)
                                    {
                                        if ((Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays)) / Compound_Calc >= compprd)
                                        {
                                            Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                                            totprd = totprd + 1;
                                        }
                                        else
                                        {
                                            calcprd = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                            vfRestdays = calcprd;
                                            goto lle;
                                        }
                                    }
                                lle:;
                                    dt_to = VFLASTDT.Date;

                                    CalPrd = Math.Pow((1 + (Convert.ToDouble(vfroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));

                                    if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                                    {
                                        if (totprd > 0)
                                        {
                                            totint1 = (Arin_BalCurrInt + Arin_BalOdInt + Arin_prinout) * Convert.ToDecimal(CalPrd);
                                        }
                                        else
                                        {
                                            totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                                        }
                                        Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                        Arin_BalCurrInt = 0;
                                    }
                                    else
                                    {
                                        if (totprd > 0)
                                        {
                                            totint1 = (Arin_prinout + Math.Round(Arin_intcurr)) * Convert.ToDecimal(CalPrd);
                                        }
                                        else
                                        {
                                            totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                                        }
                                    }
                                    totint2 = totint1 * ((vfroi * vfRestdays) / 36500);

                                    vfintcur = Math.Round((totint1 + totint2));

                                    if (totprd > 0)
                                    {
                                        Arin_intcurr = Arin_intcurr + Math.Round(vfintcur - Arin_prinout - Math.Round(Arin_intcurr));
                                    }
                                    else
                                    {
                                        Arin_intcurr = Arin_intcurr + Math.Round(vfintcur - Arin_prinout);
                                    }

                                    //Bal_OdIntCalc


                                    totCurrprd = totprd;
                                    vfCurrRestdays = vfRestdays;
                                    //Arin_intcurr = Math.Round(Arin_intcurr) - (NullDecimal(dtTran.Rows[i]["int_curr"]));

                                    if (LoanKey != "r")
                                    {
                                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays) + 1;
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays);
                                    }
                                arin:;
                                    vfRestdays = 0;
                                    totprd = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(vftotdays / Compound_Calc)));
                                    vfRestdays = vftotdays - (totprd * Compound_Calc);

                                    totprd = 0;
                                    calcprd = 0;
                                    vftotdays = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays);
                                    Arin_FromDt = VFLASTDT.Date;

                                    while (Arin_FromDt < Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date)
                                    {
                                        if ((Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - VFLASTDT.Date).TotalDays)) / Compound_Calc >= compprd)
                                        {
                                            Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                                            totprd = totprd + 1;
                                        }
                                        else
                                        {
                                            calcprd = Convert.ToInt32((Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date - Arin_FromDt.Date).TotalDays);
                                            vfRestdays = calcprd;
                                            goto llf;
                                        }
                                    }
                                llf:;
                                    dt_to = Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date;
                                    CalPrd = Math.Pow((1 + (Convert.ToDouble(vfroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));

                                    if (Arin_FromDt.Date == FinanceStart.Date)
                                    {

                                        if ((Arin_intcurr + Arin_BalOdInt) > 0)
                                        {
                                            if (totprd > 0)
                                            {
                                                totintod = (Arin_BalOdInt + Arin_intcurr + Arin_prinout) * Convert.ToDecimal(CalPrd);
                                            }
                                            else
                                            {
                                                totintod = Arin_prinout * Convert.ToDecimal(CalPrd);
                                            }
                                            Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                            Arin_intod = Arin_intod + Arin_BalOdInt;
                                            Arin_BalCurrInt = 0;
                                            Arin_BalOdInt = 0;
                                        }
                                        else
                                        {
                                            if (totprd > 0)
                                            {
                                                totintod = (Arin_prinout + Math.Round(Arin_intcurr) + Math.Round(Arin_intod)) * Convert.ToDecimal(CalPrd);
                                            }
                                            else
                                            {
                                                totintod = Arin_prinout * Convert.ToDecimal(CalPrd);
                                            }

                                            totint2 = totintod * (((vfroi + vfodroi) * vfRestdays) / 36500);
                                            vfintcur = Math.Round(totintod + totint2);
                                            if (totprd > 0)
                                            {
                                                Arin_intod = Arin_intod + (Math.Round(vfintcur) - Arin_prinout - Math.Round(Arin_intcurr) - Math.Round(Arin_intod));
                                            }
                                            else
                                            {
                                                Arin_intod = Arin_intod + (Math.Round(vfintcur) - Arin_prinout);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (totprd > 0)
                                        {
                                            totintod = (Arin_prinout + Math.Round(Arin_intcurr)) * Convert.ToDecimal(CalPrd);
                                        }
                                        else
                                        {
                                            totintod = Arin_prinout * Convert.ToDecimal(CalPrd);
                                        }
                                        totint2 = totintod * (((vfroi + vfodroi) * vfRestdays) / 36500);
                                        vfintcur = Math.Round(totintod + totint2);
                                        if (totprd > 0)
                                        {
                                            Arin_intod = Arin_intod + Math.Round(vfintcur - Arin_prinout - Math.Round(Arin_intcurr));
                                        }
                                        else
                                        {
                                            Arin_intod = Arin_intod + Math.Round(vfintcur - Arin_prinout);
                                        }
                                    }


                                    Arin_prinOd = Arin_prinout;
                                    //totODprd = totprd;
                                    //vfODRestdays = vfRestdays;
                                    if (TempOdPrin > 0)
                                    {
                                        if (Arin_FromDt.Date < VFLASTDT.Date)
                                        {
                                            vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                            totprd = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(vftotdays / Compound_Calc)));
                                            vfRestdays = vftotdays - (totprd * Compound_Calc);
                                            totprd = 0;
                                            calcprd = 0;
                                            vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);

                                            while (Arin_FromDt < VFLASTDT.Date)
                                            {
                                                if ((Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays)) / Compound_Calc >= compprd)
                                                {
                                                    Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                                                    totprd = totprd + 1;
                                                }
                                                else
                                                {
                                                    calcprd = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                                    vfRestdays = calcprd;
                                                    goto llg;
                                                }
                                            }
                                        llg:;
                                            dt_to = VFLASTDT.Date;

                                            CalPrd = Math.Pow((1 + (Convert.ToDouble(vfodroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));
                                            totint1 = TempOdPrin * Convert.ToDecimal(CalPrd);
                                            totintod = totint1;
                                            totint2 = totintod * ((vfodroi * vfRestdays) / 36500);
                                            vfintcur = Math.Round(totintod + totint2);
                                            Arin_intod = Arin_intod + Math.Round(vfintcur);
                                        }
                                    }

                                    if (Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date > VFLASTDT.Date)
                                    {
                                        Arin_intod = (Math.Round(Arin_intcurr) + Math.Round(Arin_intod)) - NullDecimal(dtTran.Rows[i]["int_od"]) - NullDecimal(dtTran.Rows[i]["int_curr"]);
                                        Arin_intcurr = 0;
                                    }
                                    else
                                    {
                                        Arin_intcurr = Math.Round(Arin_intcurr) - NullDecimal(dtTran.Rows[i]["int_curr"]);
                                        Arin_intod = Math.Round(Arin_intod) - NullDecimal(dtTran.Rows[i]["int_od"]);
                                    }
                                }

                            }//ELSE NO OF INSTALLATION > 0 END 
                            Arin_prinout = Arin_prinout - (NullDecimal(dtTran.Rows[i]["prin_curr"]) + NullDecimal(dtTran.Rows[i]["prin_od"]) + NullDecimal(dtTran.Rows[i]["prin_adv"]));
                        }
                        Arin_FromDt = Convert.ToDateTime(dtTran.Rows[i]["dt"]).Date;
                        DisbStatus = false;
                    }// RPAYMENT END

                }// FOR LOOP WITHIN TRANS
            }//IF TRANSACTION FOUND
             ////'******** calculate from last repayment date

            CullPrin = Arin_TotLoan - Arin_prinout - CullPrin;
            SqlCommand cmdprin1 = new SqlCommand();
            cmdprin1.CommandText = ("select sum(prin_curr),sum(prin_od),sum(prin_adv) from loan_repay_det where  sl_code=@slc and rep_date>=@frdt and rep_date<@dt");
            cmdprin1.Parameters.AddWithValue("@frdt", Arin_FromDt);
            cmdprin1.Parameters.AddWithValue("@slc", slcode);
            cmdprin1.Parameters.AddWithValue("@dt", cdt);

            DataTable DtCull1 = getdatatable(cmdprin1);

            if (DtCull1.Rows.Count > 0)
            {
                CullPrin = CullPrin + NullDecimal(DtCull1.Rows[0][0]) + NullDecimal(DtCull1.Rows[0][1]) + NullDecimal(DtCull1.Rows[0][2]);
            }

            vftotdays = 0;

            if (Arin_FromDt == cdt.Date && LoanKey == "r")
            {
                // goto cendB;
            }

            if (NoOfInst == 0 || OdDays <= 0)
            {
                if (VFLASTDT >= cdt || OdDays <= 0)
                {
                    vfRestdays = 0;

                    if (LoanKey != "r")
                    {
                        if (DisbInt == true)
                        {
                            vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                        }
                        else
                        {
                            vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays) + 1;
                        }
                    }
                    else
                    {
                        vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                    }
                    totprd = 0;
                    calcprd = 0;
                    vfRestdays = 0;

                    while (Arin_FromDt < cdt.Date)
                    {
                        if ((Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays)) / Compound_Calc >= compprd)
                        {
                            Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                            totprd = totprd + 1;
                        }
                        else
                        {
                            calcprd = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                            vfRestdays = calcprd;
                            goto llh;
                        }
                    }

                llh:;
                    dt_to = cdt.Date;

                    CalPrd = Math.Pow((1 + (Convert.ToDouble(vfroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));

                    if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                    {
                        if (totprd > 0)
                        {
                            totint1 = (Arin_BalCurrInt + Arin_BalOdInt + Arin_prinout) * Convert.ToDecimal(CalPrd);
                        }
                        else
                        {
                            totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                        }

                        totintod = totint1;
                        Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                        Arin_intod = Arin_intod + Arin_BalOdInt;
                        Arin_BalCurrInt = 0;
                        Arin_BalOdInt = 0;
                    }
                    else
                    {
                        if (totprd > 0)
                        {
                            totint1 = (Arin_prinout + Math.Round(Arin_intcurr)) * Convert.ToDecimal(CalPrd);
                        }
                        else
                        {
                            totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                        }
                        totintod = totint1;
                    }
                    totint2 = totint1 * ((vfroi * vfRestdays) / 36500);
                    vfintcur = Math.Round(totint1 + totint2);
                    if (totprd > 0)
                    {
                        Arin_intcurr = Arin_intcurr + Math.Round(vfintcur) - Arin_prinout - Math.Round(Arin_intcurr);
                    }
                    else
                    {
                        Arin_intcurr = Arin_intcurr + Math.Round(vfintcur) - Arin_prinout;
                    }
                    totCurrprd = totprd;
                    vfCurrRestdays = vfRestdays;
                }
                else
                {
                    if (VFLASTDT.Date < cdt.Date)
                    {
                        if (Arin_FromDt.Date > VFLASTDT.Date)
                        {
                            if (cdt.Date > Arin_FromDt.Date)
                            {
                                if (LoanKey != "r")
                                {
                                    if (DisbInt == true)
                                    {
                                        vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                                    }
                                    else
                                    {
                                        vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays) + 1;
                                    }
                                }
                                else
                                {
                                    vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                                }
                                goto aar;
                            }
                        }
                        else
                        {
                            if (LoanKey != "r")
                            {
                                if (DisbInt == true)
                                {
                                    vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                }
                                else
                                {
                                    vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays) + 1;
                                }
                            }
                            else
                            {
                                vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                            }
                        }
                        vfRestdays = 0;
                        totprd = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(vftotdays / Compound_Calc)));
                        vfRestdays = vftotdays - (totprd * Compound_Calc);


                        totprd = 0;
                        calcprd = 0;

                        vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                        while (Arin_FromDt < VFLASTDT.Date)
                        {
                            if ((Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays)) / Compound_Calc >= compprd)
                            {
                                Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                                totprd = totprd + 1;
                            }
                            else
                            {
                                calcprd = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                vfRestdays = calcprd;
                                goto lli;
                            }
                        }

                    lli:;
                        dt_to = VFLASTDT.Date;

                        CalPrd = Math.Pow((1 + (Convert.ToDouble(vfroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));

                        if ((Arin_BalCurrInt + Arin_BalOdInt) > 0)
                        {
                            if (totprd > 0)
                            {
                                totint1 = (Arin_BalCurrInt + Arin_BalOdInt + Arin_prinout) * Convert.ToDecimal(CalPrd);
                            }
                            else
                            {
                                totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                            }

                            totintod = totint1;
                            Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                            Arin_intod = Arin_intod + Arin_BalOdInt;
                            Arin_BalCurrInt = 0;
                            Arin_BalOdInt = 0;
                        }
                        else
                        {
                            if (totprd > 0)
                            {
                                totint1 = (Arin_prinout + Math.Round(Arin_intcurr)) * Convert.ToDecimal(CalPrd);
                            }
                            else
                            {
                                totint1 = Arin_prinout * Convert.ToDecimal(CalPrd);
                            }
                        }
                        totint2 = totint1 * ((vfroi * vfRestdays) / 36500);
                        vfintcur = Math.Round(totint1 + totint2);
                        if (totprd > 0)
                        {
                            Arin_intcurr = Arin_intcurr + Math.Round(vfintcur - Arin_prinout - Math.Round(Arin_intcurr));
                        }
                        else
                        {
                            Arin_intcurr = Arin_intcurr + Math.Round(vfintcur - Arin_prinout);
                        }

                        totCurrprd = totprd;
                        vfCurrRestdays = vfRestdays;

                        if (LoanKey != "r")
                        {
                            if (DisbInt == true)
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - VFLASTDT.Date).TotalDays);
                            }
                            else
                            {
                                vftotdays = Convert.ToInt32((cdt.Date - VFLASTDT.Date).TotalDays) + 1;
                            }
                        }
                        else
                        {
                            vftotdays = Convert.ToInt32((cdt.Date - VFLASTDT.Date).TotalDays);
                        }
                    aar:;
                        vfRestdays = 0;
                        totprd = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(vftotdays / Compound_Calc)));
                        vfRestdays = vftotdays - (totprd * Compound_Calc);

                        totprd = 0;
                        calcprd = 0;
                        vftotdays = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);

                        while (Arin_FromDt < cdt.Date)
                        {
                            if ((Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays)) / Compound_Calc >= compprd)
                            {
                                Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                                totprd = totprd + 1;
                            }
                            else
                            {
                                calcprd = Convert.ToInt32((cdt.Date - Arin_FromDt.Date).TotalDays);
                                vfRestdays = calcprd;
                                goto llj;
                            }
                        }
                    llj:;
                        dt_to = cdt.Date;

                        CalPrd = Math.Pow((1 + (Convert.ToDouble(vfroi + vfodroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));


                        if (Arin_FromDt.Date == FinanceStart.Date)
                        {

                            if ((Arin_intcurr + Arin_BalOdInt) > 0)
                            {
                                if (totprd > 0)
                                {
                                    totintod = (Arin_BalOdInt + Arin_intcurr + Arin_prinout) * Convert.ToDecimal(CalPrd);
                                }
                                else
                                {
                                    totintod = Arin_prinout * Convert.ToDecimal(CalPrd);
                                }
                                Arin_intcurr = Arin_intcurr + Arin_BalCurrInt;
                                Arin_intod = Arin_intod + Arin_BalOdInt;
                                Arin_BalCurrInt = 0;
                                Arin_BalOdInt = 0;
                            }
                            else
                            {
                                if (totprd > 0)
                                {
                                    totintod = (Arin_prinout + Math.Round(Arin_intcurr) + Math.Round(Arin_intod)) * Convert.ToDecimal(CalPrd);
                                }
                                else
                                {
                                    totintod = Arin_prinout * Convert.ToDecimal(CalPrd);
                                }

                                totint2 = totintod * (((vfroi + vfodroi) * vfRestdays) / 36500);
                                vfintcur = Math.Round(totintod + totint2);
                                if (totprd > 0)
                                {
                                    Arin_intod = Arin_intod + (Math.Round(vfintcur) - Arin_prinout - Math.Round(Arin_intcurr) - Math.Round(Arin_intod));
                                }
                                else
                                {
                                    Arin_intod = Arin_intod + (Math.Round(vfintcur) - Arin_prinout);
                                }
                            }
                        }
                        else
                        {
                            if (totprd > 0)
                            {
                                totintod = (Arin_prinout + Math.Round(Arin_intcurr)) * Convert.ToDecimal(CalPrd);
                            }
                            else
                            {
                                totintod = Arin_prinout * Convert.ToDecimal(CalPrd);
                            }

                            totint2 = totintod * (((vfroi + vfodroi) * vfRestdays) / 36500);
                            vfintcur = Math.Round(totintod + totint2);
                            if (totprd > 0)
                            {
                                Arin_intod = Arin_intod + Math.Round(vfintcur - Arin_prinout - Math.Round(Arin_intcurr));
                            }
                            else
                            {
                                Arin_intod = Arin_intod + Math.Round(vfintcur - Arin_prinout);
                            }
                        }
                        Arin_prinOd = Arin_prinout;
                        //totODprd = totprd;
                        if (TempOdPrin > 0)
                        {
                            if (Arin_FromDt.Date < VFLASTDT.Date)
                            {
                                vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                totprd = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(vftotdays / Compound_Calc)));
                                totprd = 0;
                                calcprd = 0;
                                vftotdays = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                while (Arin_FromDt < VFLASTDT.Date)
                                {
                                    if ((Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays)) / Compound_Calc >= compprd)
                                    {
                                        Arin_FromDt = Arin_FromDt.AddMonths(Compound_Mnth);
                                        totprd = totprd + 1;
                                    }
                                    else
                                    {
                                        calcprd = Convert.ToInt32((VFLASTDT.Date - Arin_FromDt.Date).TotalDays);
                                        vfRestdays = calcprd;
                                        goto llk;
                                    }
                                }

                            llk:;
                                dt_to = VFLASTDT.Date;
                                CalPrd = Math.Pow((1 + (Convert.ToDouble(vfodroi) / Convert.ToDouble(Compound_Year))), Convert.ToDouble(totprd));
                                totint1 = TempOdPrin * Convert.ToDecimal(CalPrd);
                                totintod = totint1;

                                totint2 = totintod * ((vfodroi * vfRestdays) / 36500);
                                vfintcur = Math.Round(totintod + totint2);
                                Arin_intod = Arin_intod + (Math.Round(vfintcur) - Math.Round(TempOdPrin));

                            }
                        }


                    }
                }
            }

            if (Arin_DisbAmt > 0)
            {

            }
            else
            {
                Arin_DisbAmt = vfnetloan;
                Arin_DisbDt = vfinsdt;
            }

            Arin_intcurr = Math.Round(Arin_intcurr);
            Arin_intod = Math.Round(Arin_intod);
            if (cdt.Date > VFLASTDT.Date)
            {
                if (OdDays > 0)
                {
                    Arin_intod = Arin_intcurr + Arin_intod;
                    Arin_intcurr = 0;
                }
            }

            //'*********** LAST OD DATE FINDER ******** DATED 16/11/15
            //If Arin_prinout > 0 Then

            //rs_op.Open "select * from od_intset where sl_code=" & sl_code & " and effct_date<='" & Format(CDate(dt), "dd-mmm-yyyy") & "'", con, adOpenDynamic, adLockReadOnly
            //If Not rs_op.EOF Then
            //    If (Arin_intcurr + Arin_intod) > FillNum(rs_op!int_amt) Then
            //        Arin_intod = FillNum(rs_op!int_amt)
            //        Arin_intcurr = 0
            //    End If
            //End If
            //rs_op.Close

            if (Arin_intod > Arin_prinout)
            {
                if (Arin_Security100 != "y")
                {
                    Arin_intod = Arin_prinout;
                    Arin_intcurr = 0;
                }
            }
            if (Arin_intcurr > Arin_prinout)
            {
                if (Arin_Security100 != "y")
                {
                    Arin_intcurr = Arin_prinout;
                    Arin_intod = 0;
                }
            }
            int odp = 0;
            if (vfinstap == "Yes")
            {

                if (vfodpr == "MONTHLY")
                {
                    odp = 1;
                }
                else if (vfodpr == "QUARTERLY")
                {
                    odp = 3;
                }
                else if (vfodpr == "HALF YEARLY")
                {
                    odp = 6;
                }
                else if (vfodpr == "YEARLY")
                {
                    odp = 12;
                }
                else if (vfodpr == "QUARTERLY")
                {
                    odp = 3;
                }

            }


            DateTime FindDt;
            decimal TempOddays = 0;

            if (vfinstamt > 0)
            {
                TempOddays = Convert.ToDecimal(((Arin_TotLoan - Arin_prinout) / vfinstamt) + 1);
                TempOddays = TempOddays * odp;
                DateTime TempAppldt;

                TempAppldt = vfappldt.AddDays(1);
                FindDt = (TempAppldt).AddMonths(odp);

                OdDays = Convert.ToInt32(TempOddays);
                Arin_ODdate = TempAppldt.AddMonths(OdDays);
                /////////////// NEW OD INTEREST CALCULATION FOR C# CODE BY ARINDAM
                DateTime TempOdDate = DateTime.Now.Date, TempOdDate1 = DateTime.Now.Date;
                if (Arin_ODdate <= cdt.Date)
                {
                    TempOdDate1 = Arin_ODdate;
                    TempOdDate = Arin_ODdate;
                    for (int a = 1; a <= (NoOfInst - TempOddays); a++)
                    {
                        TempOdDate1 = TempOdDate1.AddMonths(odp);
                        if (TempOdDate1 <= cdt.Date)
                        {
                            // FindODP = FindODP + 1;
                            vftotdays = Convert.ToInt32((TempOdDate1.Date - TempOdDate.Date).TotalDays);
                            Arin_prinOd = Arin_prinOd + vfinstamt;
                            Arin_intod = Arin_intod + ((Arin_prinOd * vfodroi * vftotdays) / 36500);
                            TempOdDate = TempOdDate1;
                        }
                        else
                        {
                            if (TempOdDate <= cdt.Date)
                            {
                                Arin_prinOd = Arin_prinOd + vfinstamt;
                                vftotdays = Convert.ToInt32((cdt.Date - TempOdDate.Date).TotalDays);
                                Arin_intod = Arin_intod + ((Arin_prinOd * vfodroi * vftotdays) / 36500);
                            }

                            goto EndL;
                        }

                    }
                }
            EndL:;
                Arin_intod = Math.Round(Arin_intod);
            }
            if (NoOfInst <= 0)
            {
                Arin_ODdate = VFLASTDT;
            }
            //////////////////////////////////
            ReturnAll = (Arin_intcurr + "," + Arin_intod + "," + Arin_prinOd + "," + Arin_ODdate + "," + Arin_prinout + "," + Arin_prinCurr + "," + vfinstamt + "," + vfodpr).ToString();

            //ReturnAll = (Arin_intcurr + "," + Arin_intod + "," + Arin_prinOd ).ToString();
            // ReturnAll = "Error : ";
            return ReturnAll;
        }// END MODULE
    }
}