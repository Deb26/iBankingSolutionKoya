using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class DataFinanceLayer
    {
        CommonDataBaseOperation cdo = new CommonDataBaseOperation();

        /// <summary>
        /// Get Module and Page Name By Individual UserGetKYCDetailsRecordListrec
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetModuleAndPageByUser(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            return cdo.GetDataTable("usp_GetModuleAndPageByUser", InParameters, out SQLError);
        }

        public DataTable GetBranchDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            return cdo.GetDataTable("usp_GetBranchDetails", InParameters, out SQLError);
        }

        public DataTable ValidateUser(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Username", objBO_Finance.UserId);
            InParameters.Add("@Password", objBO_Finance.UPassword);
            InParameters.Add("@BranchCode", objBO_Finance.BranchCode);
            return cdo.GetDataTable("usp_ValidateUser", InParameters);
        }
        public DataSet GetSlCode(BO_Finance objBO_Finance, out string SQLError)
        {

            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);

            return cdo.GetDataSet("getSL_CODE", InParameters, out SQLError);
        }

        public DataSet GetInterestcal(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@PERCENTAGE", objBO_Finance.ROI);
            InParameters.Add("@ASONDATE", objBO_Finance.AsOnDate);
            return cdo.GetDataSet("getInterest", InParameters, out SQLError);

        }

        public DataSet GetMaturityAmt(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@ASONDATE", objBO_Finance.AsOnDate);

            return cdo.GetDataSet("usp_GetMaturityAmountBySL_CODE", InParameters, out SQLError);
        }

        public DataSet GetFAReportData(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ReportType", objBO_Finance.ReportType);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);
            InParameters.Add("@Ldg_Code", objBO_Finance.LDG_CODE);
            InParameters.Add("@PrevYear", objBO_Finance.PrevYear);
            return cdo.GetDataSet("usp_GetFAReport", InParameters, out SQLError);
        }

        public DataTable StockDetailsReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@item_group", objBO_Finance.GROUP_NAME);
            InParameters.Add("@Session", objBO_Finance.ASSES);
            InParameters.Add("@ITEMCODE", objBO_Finance.Item_Code);
            InParameters.Add("@CGSTRate", objBO_Finance.Stock_Rate);
            InParameters.Add("@Transtype", objBO_Finance.TransType);
            return cdo.GetDataTable("usp_StockDetailsReport", InParameters, out SQLError);
        }

        public DataSet GetInvoiceDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@VoucherNo", objBO_Finance.VOUCHER_NO);
            return cdo.GetDataSet("usp_GetInvoiceDetails", InParameters, out SQLError);
        }

        public DataTable TrailBalanceReport(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@Flag", objBO_Finance.Flag);
            p[1] = new SqlParameter("@FDate", objBO_Finance.FDate);
            p[2] = new SqlParameter("@TDate", objBO_Finance.TDate);
            DataTable testTab = DataLayer.GetInstance().ExecuteDataTable("usp_RepTrailBalanceLiab", p);
            return testTab;
            //return cdo.GetDataTable("usp_RepTrailBalance", InParameters, out SQLError);
        }

        public DataTable GetFAReportDataTR(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ReportType", objBO_Finance.ReportType);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);
            InParameters.Add("@Ldg_Code", objBO_Finance.LDG_CODE);
            InParameters.Add("@PrevYear", objBO_Finance.PrevYear);
            return cdo.GetDataTable("usp_GetFAReportTrading", InParameters, out SQLError);
        }


        public DataTable GetFAReportDataPL(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ReportType", objBO_Finance.ReportType);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);
            InParameters.Add("@Ldg_Code", objBO_Finance.LDG_CODE);
            InParameters.Add("@PrevYear", objBO_Finance.PrevYear);
            return cdo.GetDataTable("usp_GetFAReportPLAccount", InParameters, out SQLError);
        }
        public DataTable FinalAccountSettingsBalacesheet(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ReportType", objBO_Finance.ReportType);
            //InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("usp_GetFAReportSettingsDetailsBalanceSheet", InParameters, out SQLError);
        }

        public DataTable FinalAccountSettingsProfitLoss(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ReportType", objBO_Finance.ReportType);
            //InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("usp_GetFAReportSettingsDetailsPLAc", InParameters, out SQLError);
        }

        public DataTable FinalAccountSettingsTrading(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ReportType", objBO_Finance.ReportType);
            //InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("usp_GetFAReportSettingsDetailsTradingAc", InParameters, out SQLError);
        }

        public DataTable GetFAReportDataBS(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ReportType", objBO_Finance.ReportType);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);
            InParameters.Add("@Ldg_Code", objBO_Finance.LDG_CODE);
            InParameters.Add("@PrevYear", objBO_Finance.PrevYear);
            return cdo.GetDataTable("usp_GetFAReportBalanceSheet", InParameters, out SQLError);
        }

        public DataTable TrailBalanceReportAssets(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);
            return cdo.GetDataTable("usp_RepTrailBalanceAssets", InParameters, out SQLError);
            //return cdo.GetDataSet("usp_RepTrailBalanceAssets", InParameters, out SQLError); 
            //usp_RepTrailBalanceAssetsExp 

        }
        public DataTable YearStartEndDt(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@Flag", objBO_Finance.Flag);
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Flag", objBO_Finance.Flag);
            DataTable testTab = DataLayer.GetInstance().ExecuteDataTable("usp_GetYearStEndDt", p);
            return testTab;

        }


        public int DeleteLoanRepaymentDisbur(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@VoucherNo", objBO_Finance.VOUCHER_NO);
            InParameters.Add("@EnstrumentNo", objBO_Finance.INS_NO);

            return cdo.InsertUpdateDelete("usp_DeleteLoanRepaymentDisbDetail", InParameters, out SQLError);
        }


        public int InsertUpdateDeleteLoanRepayment(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@CollectionDate", objBO_Finance.CollectionDate);
            InParameters.Add("@ReceivedType", objBO_Finance.ReceivedType);
            InParameters.Add("@BankLedger", objBO_Finance.BankLedger);
            InParameters.Add("@INS_TYPE", objBO_Finance.INS_TYPE);
            InParameters.Add("@SB_SL_CODE", objBO_Finance.SB_SL_CODE);

            //InParameters.Add("@DemandPrincipalOutstanding", objBO_Finance.DemandPrincipalOutstanding);
            //InParameters.Add("@DemandPrincipalOverdue", objBO_Finance.DemandPrincipalOverdue);
            //InParameters.Add("@DemandPrincipalCurrent", objBO_Finance.DemandPrincipalCurrent);
            //InParameters.Add("@DemandPenalInterest", objBO_Finance.DemandPenalInterest);
            InParameters.Add("@DemandOverdueInterest", objBO_Finance.DemandOverdueInterest);
            InParameters.Add("@DemandCurrentInterest", objBO_Finance.DemandCurrentInterest);

            InParameters.Add("@CollectionPrincipalOutstanding", objBO_Finance.CollectionPrincipalOutstanding);
            InParameters.Add("@CollectionPrincipalOverdue", objBO_Finance.CollectionPrincipalOverdue);
            InParameters.Add("@CollectionPrincipalCurrent", objBO_Finance.CollectionPrincipalCurrent);
            InParameters.Add("@CollectionDueInterest", objBO_Finance.CollectionDueInterest);
            InParameters.Add("@CollectionOverdueInterest", objBO_Finance.CollectionOverdueInterest);
            InParameters.Add("@CollectionCurrentInterest", objBO_Finance.CollectionCurrentInterest);

            return cdo.InsertUpdateDelete("usp_InsertUpdateLoanRepayment", InParameters, out SQLError);
        }

        public DataSet GetLoanAcctDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@CollectionDate", objBO_Finance.CollectionDate);
            return cdo.GetDataSet("usp_GetLoanRepaymentDetails", InParameters, out SQLError);
        }

        public DataTable GetAllKCCListDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@CUSTID", objBO_Finance.CUST_ID);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);


            return cdo.GetDataTable("usp_GetKYCDetailsByCUSTCode", InParameters, out SQLError);
        }

        public DataTable GetLinkedAcNo(int flag, string sl_code, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@sl_code", sl_code);
            return cdo.GetDataTable("usp_GetLinkedAcNo", InParameters, out SQLError);
        }
        public DataSet GetAccountBalance(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@ENTRYDATE", objBO_Finance.ENTRYDATE);
            return cdo.GetDataSet("usp_GetLinkedAcNoBalance", InParameters, out SQLError);
        }

        public int InsertUpdateFinalAcctSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ReportType", objBO_Finance.ReportType);
            InParameters.Add("@R_ID", objBO_Finance.R_ID);
            InParameters.Add("@dtFinalAccountDetails", objBO_Finance.dtFASettings);
            return cdo.InsertUpdateDelete("usp_InsertUpdateFinalAcctSet", InParameters, out SQLError);
        }

        public DataTable SubLedgerDetailsReports(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ldg_code", objBO_Finance.LDG_CODE);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            return cdo.GetDataTable("usp_RepSubledger_DetailsNew ", InParameters, out SQLError);
        }

        public DataTable GetAllTransDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@VoucherType", objBO_Finance.VoucherType);
            InParameters.Add("@VoucherNo", objBO_Finance.VOUCHER_NO);
            return cdo.GetDataTable("usp_GetAllTransDetails", InParameters, out SQLError);
        }

        public DataTable GETALLVOUCHER(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@VoucherType", objBO_Finance.VoucherType);
            InParameters.Add("@TDATE", objBO_Finance.TDate);
            return cdo.GetDataTable("usp_GetAllVOUCHERNODATEWISE", InParameters, out SQLError);
        }

        public DataTable GetLoanAccountDetailLoan(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            //InParameters.Add("@DT", objBO_Finance.CollectionDate);
            //InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.GetDataTable("proc_LoanAccountDetails", InParameters, out SQLError);
        }
        public DataTable GetLoanDueDetailsLoan(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@DT", objBO_Finance.CollectionDate);

            //InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.GetDataTable("PROC_GTSLOAN", InParameters, out SQLError);
        }


        public DataSet GetLoanAccountDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            //InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.GetDataSet("usp_GetLoanAccountDetails", InParameters, out SQLError);
        }
        public int InsertDepositReturnTable(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@TDATE", objBO_Finance.TDate);

            return cdo.InsertUpdateDelete("usp_MISDEPOSITRETURN", InParameters, out SQLError);
        }
        public int InsertYearTradingBalance(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@TDATE", objBO_Finance.FDate);
            InParameters.Add("@Balance", objBO_Finance.Balance);

            return cdo.InsertUpdateDelete("USP_INSERTYEARTRADINGBALANCE", InParameters, out SQLError);
        }
        public DataSet GETDEPOSITRETURN(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            return cdo.GetDataSet("usp_MISDEPOSITRETURN", InParameters, out SQLError);
        }
        public DataSet GenerateVoucherNo(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();

            InParameters.Add("@VDate", objBO_Finance.disb_date);
            //InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.GetDataSet("proc_GenerateVoucher", InParameters);
        }

        
        public DataSet RepaymentDisbDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@CollectionDate", objBO_Finance.CollectionDate);

            return cdo.GetDataSet("usp_GetLoanRepaymentDisbDetail", InParameters, out SQLError);
        }

        public DataSet GETMISDETAILS(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@TDATE", objBO_Finance.TDate);
            return cdo.GetDataSet("usp_GETMISINTDETAILS", InParameters, out SQLError);
        }



        public DataSet GetLoanDetailsList(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SchemeCode", objBO_Finance.SCHEME_CODE);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            //InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.GetDataSet("usp_GetLoanDetailListNew", InParameters, out SQLError);
        }

        public DataSet GeneralLedgerReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ldg_code", objBO_Finance.LDG_CODE);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);
            InParameters.Add("@BRANCHID", objBO_Finance.BranchCode);
            return cdo.GetDataSet("usp_RepGeneralLedger", InParameters, out SQLError);
        }
        public DataTable CashAccountReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@BRANCHID", objBO_Finance.BranchCode);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);
            //return cdo.GetDataTable("usp_RepCashAccount", InParameters, out SQLError);
            return cdo.GetDataTable("usp_RepCashAccountNew", InParameters, out SQLError);

        }
        //public DataSet CashAccountReport(BO_Finance objBO_Finance, out string SQLError)
        //{
        //    Hashtable InParameters = new Hashtable();
        //    InParameters.Add("@Flag", objBO_Finance.Flag);
        //    InParameters.Add("@BRANCHID", objBO_Finance.BranchCode);
        //    InParameters.Add("@FDate", objBO_Finance.FDate);
        //    InParameters.Add("@TDate", objBO_Finance.TDate);
        //    return cdo.GetDataSet("sp_CashAccountReport", InParameters, out SQLError);
        //}

        public DataTable DashboardReport (BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            //InParameters.Add("@ENTRYDATE", objBO_Finance.ENTRYDATE);
            return cdo.GetDataTable("usp_GetDashboardDetails", InParameters, out SQLError);
        }


        public DataTable FindInstrumentNoLoanRepay(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);

            InParameters.Add("@VOUCHERNO", objBO_Finance.VOUCHER_NO);
            return cdo.GetDataTable("USP_GetInsNoLoanRepayDet", InParameters, out SQLError);

        }

        public int ModifyTransaction(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@VoucherType", objBO_Finance.VoucherType);
            InParameters.Add("@VoucherNo", objBO_Finance.VOUCHER_NO);
            InParameters.Add("@CCBCodeNew", objBO_Finance.CCBCode);
            InParameters.Add("@dtTransactionDetais", objBO_Finance.dtTransDetails);
            return cdo.InsertUpdateDelete("usp_ModifyTransaction", InParameters, out SQLError);
        }
        public int ModifyTransactionNew(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@VoucherType", objBO_Finance.VoucherType);

            InParameters.Add("@VoucherNo", objBO_Finance.VOUCHER_NO);
            InParameters.Add("@LdgCode", objBO_Finance.LDG_CODE);
            InParameters.Add("@SlCode", objBO_Finance.SL_CODE);
            InParameters.Add("@Amt_Debit", objBO_Finance.DebitAmount);
            InParameters.Add("@Amt_Credit", objBO_Finance.CreditAmount);
            InParameters.Add("@VoucherDate", objBO_Finance.VoucherDate);
            InParameters.Add("@PreviousVoucherDate", objBO_Finance.PrevVoucherDate);
            InParameters.Add("CCBCode", objBO_Finance.CCBCode);

            return cdo.InsertUpdateDelete("usp_ModifyTransactionNew", InParameters, out SQLError);
        }

        public int BankLDGBalanceUpdate(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            //InParameters.Add("@VoucherNo", objBO_Finance.NOMENCLATURE);
            InParameters.Add("@ACT_OP_CR", objBO_Finance.ACT_OP_CR);
            InParameters.Add("@ACT_OP_DR", objBO_Finance.ACT_OP_DR);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
          
            return cdo.InsertUpdateDelete("usp_UpdateBranchLedgerBalance", InParameters, out SQLError);
        }

        public int SocietyVoucherUpdate(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@VOUCHERNO", objBO_Finance.VOUCHER_NO);
            InParameters.Add("@LDGCODE", objBO_Finance.LDG_CODE);
            InParameters.Add("@SLCODE", objBO_Finance.SL_CODE);
            InParameters.Add("@AMTDEBIT", objBO_Finance.AMT_DEBIT);
            InParameters.Add("@AMTCREDIT", objBO_Finance.AMT_CREDIT);
            InParameters.Add("@REMARKS", objBO_Finance.REMARKS);
            InParameters.Add("@TDATE", objBO_Finance.date_of_opening);
            InParameters.Add("@ccbnewcode", objBO_Finance.COACode);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);

            return cdo.InsertUpdateDelete("PROC_UPDATEVOUCHERDETAILS", InParameters, out SQLError);
        }

        public int insertupdatemisinterestdetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@FLAG", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@IntAmt", objBO_Finance.INT_AMNT);
            InParameters.Add("@SB_ACNO", objBO_Finance.SB_SL_CODE);
            InParameters.Add("@Tdate", objBO_Finance.date_of_opening);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            
            return cdo.InsertUpdateDelete("usp_UPDATEMISINTDETAILS", InParameters, out SQLError);
        }

        public int updateNEFTRTGS(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@REMARKS", objBO_Finance.REMARKS);
            InParameters.Add("@ACNO", objBO_Finance.SB_ACNO);
            InParameters.Add("@ENTRYDT", objBO_Finance.ENTRYDATE);

            return cdo.InsertUpdateDelete("usp_InsertUpdateNEFTRTGS", InParameters, out SQLError);
        }

        public int datFILEUPLOAD(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACCOUNTNO", objBO_Finance.intro_acno);
            InParameters.Add("@TRANSACTION", objBO_Finance.Transaction);
            InParameters.Add("@NAME", objBO_Finance.NAME);
            InParameters.Add("@BALANCE", objBO_Finance.Balance);
            InParameters.Add("@COLLECTIONDATE", objBO_Finance.CCB_DATE);
            InParameters.Add("@COLLECTION", objBO_Finance.collection);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);

            return cdo.InsertUpdateDelete("usp_datFileContent", InParameters, out SQLError);
        }
        public int MIDINTDT(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@FLAG", objBO_Finance.Flag);
            InParameters.Add("@branchid", objBO_Finance.BranchCode);
            
            return cdo.InsertUpdateDelete("usp_MISInterestDetailsNew", InParameters, out SQLError);
        }
        public int INSERTCASHIER(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@ACCOUNTNO", objBO_Finance.lf_acno);
            InParameters.Add("@BALANCE", objBO_Finance.Balance);
            InParameters.Add("@COLLECTIONDATE", objBO_Finance.DTSCust);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);

            return cdo.InsertUpdateDelete("usp_INSERTCASHIERBOOK", InParameters, out SQLError);
        }

        public int UpdateTransactionComments(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@VoucherType", objBO_Finance.VoucherType);
            InParameters.Add("@VoucherNo", objBO_Finance.VOUCHER_NO);
            InParameters.Add("CCBCode", objBO_Finance.CCBCode);

            InParameters.Add("@AdminComments", objBO_Finance.Comments);
            InParameters.Add("@UpdateFlag", objBO_Finance.SL_FLAG);

            return cdo.InsertUpdateDelete("usp_InsertUpdateTransactionComments", InParameters, out SQLError);
        }



        public DataTable searchclient(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameter = new Hashtable();

            InParameter.Add("@CUST_ID", objBO_Finance.CUST_ID);
            InParameter.Add("@NAME", objBO_Finance.NAME);

            return cdo.GetDataTable("CLIENT_SEARCH", InParameter, out SQLError);
        }

        public DataSet GetAllVoucherNo(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@VoucherDate", objBO_Finance.CCB_DATE);
            InParameters.Add("@VoucherType", objBO_Finance.VoucherType);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.GetDataSet("usp_GetVoucherNo", InParameters, out SQLError);

        }

        public int InvestementAccountClosing(BO_Finance objBO_Finance , out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@MaturityInstruction", objBO_Finance.MaturityInstruction);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@RenewalPeriodsInMonth", objBO_Finance.RenewalPeriodsInMonth);
            InParameters.Add("@RenewalPeriodsInDays", objBO_Finance.RenewalPeriodsInDays);
            InParameters.Add("@RenewalROI", objBO_Finance.RenewalROI);
            InParameters.Add("@RenewalDepositAmt", objBO_Finance.RenewalDepositAmt);
            InParameters.Add("@RenewalMaturityDate", objBO_Finance.RenewalMaturityDate);
            InParameters.Add("@Maturityamount", objBO_Finance.Maturityamount);
            InParameters.Add("@InterestCreditedTillDate", objBO_Finance.InterestCreditedTillDate);
            InParameters.Add("@InterestAdjusted", objBO_Finance.InterestAdjusted);
            InParameters.Add("@WithdrawlDate", objBO_Finance.WithdrawlDate);
            InParameters.Add("@LDGTRF", objBO_Finance.LDGTRF);
            InParameters.Add("@INTPAIDLDG_CODE", objBO_Finance.INTPAIDLDG_CODE);
            //InParameters.Add("@Maturityamount", objBO_Finance.Maturityamount);
            InParameters.Add("@ACTYPE", objBO_Finance.actype);
            InParameters.Add("@Empcode", objBO_Finance.EMPCODE);
            InParameters.Add("@BranchID", objBO_Finance.BranchID);
            return cdo.InsertUpdateDelete("usp_InsertUpdateInvsetmentClosing", InParameters, out SQLError);
        }

        public int InsertUpdateDeleteAccountClosing(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@DEPOSIT_AMNT", objBO_Finance.DEPOSIT_AMNT);
            InParameters.Add("@Pen_ROI", objBO_Finance.Pen_ROI);
            InParameters.Add("@Applied_Int", objBO_Finance.Applied_Int);
            InParameters.Add("@MaturityInstruction", objBO_Finance.MaturityInstruction);
            InParameters.Add("@SB_ACNO", objBO_Finance.SB_ACNO);
            InParameters.Add("@TakeInt", objBO_Finance.TakeInt);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@RenewalDate", objBO_Finance.RenewalDate);
            InParameters.Add("@RenewalAdjustmentDate", objBO_Finance.RenewalAdjustmentDate);
            InParameters.Add("@RenewalPeriodsInMonth", objBO_Finance.RenewalPeriodsInMonth);
            InParameters.Add("@RenewalPeriodsInDays", objBO_Finance.RenewalPeriodsInDays);
            InParameters.Add("@RenewalROI", objBO_Finance.RenewalROI);
            InParameters.Add("@RenewalDepositAmt", objBO_Finance.RenewalDepositAmt);
            InParameters.Add("@RenewalMaturityDate", objBO_Finance.RenewalMaturityDate);
            InParameters.Add("@RenewalMaturityAmt", objBO_Finance.RenewalMaturityAmt);
            InParameters.Add("@InterestCreditedTillDate", objBO_Finance.InterestCreditedTillDate);
            InParameters.Add("@InterestAdjusted", objBO_Finance.InterestAdjusted);
            InParameters.Add("@PenalInterest", objBO_Finance.PenalInterest);
            InParameters.Add("@WithdrawlDate", objBO_Finance.WithdrawlDate);
            InParameters.Add("@Maturityamount", objBO_Finance.Maturityamount);
            InParameters.Add("@ACTYPE", objBO_Finance.actype);
            InParameters.Add("@Empcode", objBO_Finance.EMPCODE);
            InParameters.Add("@BranchID", objBO_Finance.BranchID);
            

            return cdo.InsertUpdateDelete("usp_InsertUpdateAccountClosing", InParameters, out SQLError);
        }

        public int ShareAccountClosing(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@Pen_ROI", objBO_Finance.Pen_ROI);
            InParameters.Add("@MaturityInstruction", objBO_Finance.MaturityInstruction);
            InParameters.Add("@SB_ACNO", objBO_Finance.SB_ACNO);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@PenalInterest", objBO_Finance.PenalInterest);
            InParameters.Add("@WithdrawlDate", objBO_Finance.WithdrawlDate);
            InParameters.Add("@Maturityamount", objBO_Finance.Maturityamount);
            InParameters.Add("@ACTYPE", objBO_Finance.actype);
            InParameters.Add("@Empcode", objBO_Finance.EMPCODE);
            InParameters.Add("@BranchID", objBO_Finance.BranchID);


            return cdo.InsertUpdateDelete("usp_InsertUpdateAccountClosingShare", InParameters, out SQLError);
        }

        public DataSet DayBookReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            //return cdo.GetDataSet("sp_DayBookReport", InParameters, out SQLError);
            return cdo.GetDataSet("sp_DayBookReport", InParameters, out SQLError);
            //return cdo.GetDataSet("evantagebsa.sp_NewCheckDayBookReport", InParameters, out SQLError);



        }

        public DataSet GetUploadFileusingAcNo(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@filecode", objBO_Finance.BranchCode);
            return cdo.GetDataSet("geaccountNO", InParameters, out SQLError);
        }

        public DataSet GETBALANCEBYACNO(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@filecode", objBO_Finance.BranchCode);
            InParameters.Add("@acno", objBO_Finance.SL_CODE);
            return cdo.GetDataSet("getBalanceUsingACNO", InParameters, out SQLError);
        }

        public DataSet CashBookReport (BO_Finance objBO_Finance , out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.GetDataSet("sp_CashBookReport", InParameters, out SQLError);
        }

        public DataTable chequeslflag(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LDGCODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("usp_GetLedgerMasterDetails", InParameters, out SQLError);
        }
        public int InsertUpdateLoanDisbursement(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@disb_date", objBO_Finance.disb_date);
            InParameters.Add("@disb_amnt", objBO_Finance.disb_amnt);
            InParameters.Add("@ins_no", objBO_Finance.INS_NO);
            InParameters.Add("@INS_TYPE", objBO_Finance.INS_TYPE);
            InParameters.Add("@CASH", objBO_Finance.CASH);
            InParameters.Add("@MAT", objBO_Finance.MAT);
            InParameters.Add("@INS", objBO_Finance.INS);
            InParameters.Add("@TRANSFER_TO", objBO_Finance.TRANSFER_TO);
            InParameters.Add("@EmpCode", objBO_Finance.EmpCode);
            InParameters.Add("@NewRepayDate", objBO_Finance.NewRepayDate);
            InParameters.Add("@NewROI", objBO_Finance.NewROI);
            InParameters.Add("@NewODROI", objBO_Finance.NewODROI);
            InParameters.Add("@NewDisburseAmt", objBO_Finance.NewDisburseAmt);
            InParameters.Add("@NewDisbFlag", objBO_Finance.NewDisbFlag);

            InParameters.Add("@SHAMT", objBO_Finance.SHAREAMT);
            InParameters.Add("@SH_SL_CODE", objBO_Finance.SHARESLCODE);
            InParameters.Add("@CROPAMT", objBO_Finance.CROPAMT);
            InParameters.Add("@MISCAMT", objBO_Finance.MISAMT);

            return cdo.InsertUpdateDelete("usp_InsertUpdateLoanDisbursement", InParameters, out SQLError);
        }

        public DataSet AcctStatementReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@FROMDATE", objBO_Finance.FDate);
            InParameters.Add("@ToDATE", objBO_Finance.TDate);
            //return cdo.GetDataSet("proc_ReportAcctStatementdemo", InParameters, out SQLError);
            //return cdo.GetDataSet("usp_RepAcctStatement", InParameters, out SQLError);
            return cdo.GetDataSet("proc_ReportAcctStatement", InParameters, out SQLError);
        }

        public DataSet StatementReport (BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@FROMDATE", objBO_Finance.FDate);
            InParameters.Add("@ToDATE", objBO_Finance.TDate);
            return cdo.GetDataSet("proc_ReportAcctStatementDateWise", InParameters, out SQLError);
        }

        public int GetbalanceUpdate(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@old_acno", objBO_Finance.old_acno);
            InParameters.Add("@NAME", objBO_Finance.NAME);
            InParameters.Add("@ACT_OP_DR", objBO_Finance.ACT_OP_DR);
            InParameters.Add("@ACT_OP_CR", objBO_Finance.ACT_OP_CR);
            InParameters.Add("@LF_ACNO", objBO_Finance.lf_acno);
            InParameters.Add("@REC_INT", objBO_Finance.rec_int);
            return cdo.InsertUpdateDelete("proc_Balance_update", InParameters, out SQLError);
        }


        public DataSet GetSlcodebalance(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@old_acno", objBO_Finance.old_acno);
            return cdo.GetDataSet("proc_GetSLCode_Balance", InParameters, out SQLError);
        }

        public int InsertUpdateDeleteJournalBook(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@JBDate", objBO_Finance.JBDate);
            InParameters.Add("@VOUCHER_NO", objBO_Finance.VOUCHER_NO);
            InParameters.Add("@NARRATION1", objBO_Finance.NARRATION1);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            //InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            //InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            InParameters.Add("@TERMINAL_NO", objBO_Finance.TERMINAL_ID);
            //InParameters.Add("@AMT_DEBIT", objBO_Finance.AMT_DEBIT);
            //InParameters.Add("@AMT_CREDIT", objBO_Finance.AMT_CREDIT);
            //InParameters.Add("@T_NARRATION", objBO_Finance.T_NARRATION);
            InParameters.Add("@ENTRY_FR", objBO_Finance.ENTRY_FR);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            InParameters.Add("@dtJBEntryDetails", objBO_Finance.JBEntryDetails);

            return cdo.InsertUpdateDelete("usp_InsertJournalBook", InParameters, out SQLError);
        }

        public double GetOPCLBAL(int flag, string SL_CODE, DateTime? AsOnDate, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@SL_CODE", SL_CODE);
            InParameters.Add("@FDate", AsOnDate);
            InParameters.Add("@TDate", AsOnDate);
            DataTable dt = cdo.GetDataTable("usp_GetOPCLBAL", InParameters, out SQLError);
            return dt.Rows.Count > 0 ? Convert.ToDouble(dt.Rows[0]["BALANCE"]) : 0;
        }

        public double GetMaturityBal (int flag , string SL_CODE , DateTime? AsOnDate , out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@SL_CODE", SL_CODE);
            InParameters.Add("@FDate", AsOnDate);
            InParameters.Add("@TDate", AsOnDate);
            DataTable dt = cdo.GetDataTable("Proc_DetailListSavingDeposit", InParameters, out SQLError);
            return dt.Rows.Count > 0 ? Convert.ToDouble(dt.Rows[0]["ClosingBalance"]) : 0;
        }

        public DataTable GetAllAcctNo(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            //InParameters.Add("@text", text);
            return cdo.GetDataTable("usp_GetAllAcctNo", InParameters, out SQLError);
        }
        public DataTable GetClientList(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@CustID", objBO_Finance.CUST_ID);
            return cdo.GetDataTable("usp_rptGetClientList", InParameters, out SQLError);
        }

        public int InsertUpdateDeleteInvestmentOpening(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@RECIPT_NO", objBO_Finance.RECIPT_NO);
            InParameters.Add("@CERTIFICATE_NO", objBO_Finance.CERTIFICATE_NO);
            InParameters.Add("@DATE_OF_PURCHASE", objBO_Finance.DATE_OF_PURCHASE);
            InParameters.Add("@PRD_MONTH", objBO_Finance.PRD_MONTH);
            InParameters.Add("@PRD_DAYS", objBO_Finance.PRD_DAYS);
            InParameters.Add("@ROI", objBO_Finance.ROI);
            InParameters.Add("@DEPOSIT_AMNT", objBO_Finance.DEPOSIT_AMNT);
            InParameters.Add("@MATURITY_AMNT", objBO_Finance.MATURITY_AMNT);
            InParameters.Add("@MATURITY_DATE", objBO_Finance.MATURITY_DATE);
            InParameters.Add("@AC_NO", objBO_Finance.AC_NO);
            InParameters.Add("@TERMINAL_ID", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            InParameters.Add("@AC_STATUS", objBO_Finance.AC_STATUS);
            InParameters.Add("@BANK_NAME", objBO_Finance.BANK_NAME);
            InParameters.Add("@SCHEME_CODE", objBO_Finance.SCHEME_CODE);
            InParameters.Add("@LDG_CODE" , objBO_Finance.LDG_CODE);
            InParameters.Add("@DATE_OF_OPENING", objBO_Finance.date_of_opening);
            InParameters.Add("@CERT_TYPE", objBO_Finance.CERT_TYPE);
            InParameters.Add("@INV_TYPE", objBO_Finance.INV_TYPE);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.InsertUpdateDelete("usp_InsertUpdateInvestmentOpening", InParameters, out SQLError);
        }

        public DataSet GetInvestmentAccountDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_Code", objBO_Finance.SL_CODE);
            return cdo.GetDataSet("usp_GetInvestmentAccountDetails", InParameters, out SQLError);
        }
        public DataSet Check_Logindetails(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@login_date", objBO_Finance.login_date);
            InParameters.Add("@userri", objBO_Finance.userri);
            InParameters.Add("@society_code", objBO_Finance.SOCIETY_CODE);
            return cdo.GetDataSet("proc_Check_Login", InParameters);
        }
        public int Stock_Updateform(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();

            SqlParameter[] p = new SqlParameter[5];
            //InParameters.Add("@Flag", objBO_Finance.Flag);
            //InParameters.Add("@MCLASS", objBO_Finance.MCLASS);
            p[0] = new SqlParameter("@ASON", objBO_Finance.ASON);
            p[1] = new SqlParameter("@TOT_QTY", objBO_Finance.TOT_QTY);
            p[2] = new SqlParameter("@Item_Code", objBO_Finance.Item_Code);
            p[3] = new SqlParameter("@STOCK_RATE", objBO_Finance.Stock_Rate);
            p[4] = new SqlParameter("@Stock_Value", objBO_Finance.Stock_Value);
            int i = DataLayer.GetInstance().ExecuteNonQuery("proc_Stock_Update", p);
            return i;

            //InParameters.Add("@ASON", objBO_Finance.ASON);
            //InParameters.Add("@TOT_QTY", objBO_Finance.TOT_QTY);
            //InParameters.Add("@Item_Code", objBO_Finance.Item_Code);
            //InParameters.Add("@Stock_Rate", objBO_Finance.Stock_Rate);
            //InParameters.Add("@Stock_Value", objBO_Finance.Stock_Value);

            //return cdo.InsertUpdateDelete("proc_Stock_Update", InParameters);
        }

        public DataSet getStockDetails(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@MCLASS", objBO_Finance.MCLASS);
            InParameters.Add("@ASON", objBO_Finance.ASON);
            return cdo.GetDataSet("proc_StockDetails", InParameters);
        }
        public int DeletebyUserId(BO_Finance objBO_Finance)
        {
            //Hashtable InParameters = new Hashtable();
            SqlParameter[] p = new SqlParameter[2];

            p[0] = new SqlParameter("@userri", objBO_Finance.userri);
            p[1] = new SqlParameter("@SocietyCode", objBO_Finance.SOCIETY_CODE);

            int i = DataLayer.GetInstance().ExecuteNonQuery("proc_deleteuserbyuserri", p);
            return i;
        }

        public DataSet GetInvestmentAccountHoldersDetailsBySL_CODE (object flag , string SL_CODE , DateTime? EntryDate , out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@SL_CODE", SL_CODE);
            InParameters.Add("ENTRYDATE", EntryDate);
            return cdo.GetDataSet("usp_GetInvestmentAccountHoldersDetailsBySL_CODE", InParameters, out SQLError);
        }

        public DataSet GetAccountHoldersDetailsBySL_CODE(object flag, string SL_CODE, string BRANCHID, DateTime? EntryDate, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@SL_CODE", SL_CODE);
            InParameters.Add("@BRANCHID", BRANCHID);
            InParameters.Add("@ENTRYDATE", EntryDate);
            return cdo.GetDataSet("usp_GetAccountHoldersDetailsBySL_CODE", InParameters, out SQLError);
        }

        public DataSet GetAccountHoldersDetailsBySL_CODEOLD (object flag , string OLD_ACNO , string BRANCHID, DateTime? EntryDate , out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@OLD_ACNO", OLD_ACNO);
            InParameters.Add("@BRANCHID", BRANCHID);
            InParameters.Add("@ENTRYDATE", EntryDate);
            return cdo.GetDataSet("usp_GetAccountHoldersDetailsBySL_CODE", InParameters, out SQLError);
        }

        public DataSet GetAccountDetailsForNEFT(object flag, string SL_CODE, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@SL_CODE", SL_CODE);
            return cdo.GetDataSet("usp_GETBANKDETAILS", InParameters, out SQLError);
        }
        public DataSet GetBranchNameByBranchCode (object flag , string BRANCHID , out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@BRANCHID", BRANCHID);
            return cdo.GetDataSet("usp_GetBranchName", InParameters, out SQLError);
        }

        public DataSet GetAcNo(object flag, string NAME, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@NAME", NAME);
            return cdo.GetDataSet("usp_GetLedgerMasterDetails", InParameters, out SQLError);
        }


        public DataSet MatchBranchCode (object flag , string SL_CODE , string BRANCHID , out string SQLError )
        {

            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@SL_CODE", SL_CODE);
            InParameters.Add("@BRANCHID", BRANCHID);
            return cdo.GetDataSet("usp_GetBranchName", InParameters, out SQLError);
        }

        public int InsUpDenomination(BO_Finance objBO_Finance)
        {

            SqlParameter[] p = new SqlParameter[15];

            p[0] = new SqlParameter("@SL_CODE", objBO_Finance.SL_CODE);
            p[1] = new SqlParameter("@rs2000", objBO_Finance.rs2000);
            p[2] = new SqlParameter("@rs500", objBO_Finance.rs500);
            p[3] = new SqlParameter("@rs200", objBO_Finance.rs200);
            p[4] = new SqlParameter("@rs100", objBO_Finance.rs100);
            p[5] = new SqlParameter("@rs50", objBO_Finance.rs50);
            p[6] = new SqlParameter("@rs20	", objBO_Finance.rs20);
            p[7] = new SqlParameter("@rs10", objBO_Finance.rs10);
            p[8] = new SqlParameter("@rs5", objBO_Finance.rs5);
            p[9] = new SqlParameter("@rs2", objBO_Finance.rs2);
            p[10] = new SqlParameter("@rs1", objBO_Finance.rs1);
            p[10] = new SqlParameter("@DenomTotalAmount", objBO_Finance.DenomTotalAmount);
            p[10] = new SqlParameter("@DENO_TYPE", objBO_Finance.DENO_TYPE);
            p[10] = new SqlParameter("@VOUCHERNO", objBO_Finance.VOUCHER_NO);
            p[10] = new SqlParameter("@ENTRY_DATE", objBO_Finance.EntryDate);
            int i = DataLayer.GetInstance().ExecuteNonQuery("proc_InsertUpdate_Denomination", p);
            return i;

        }
        public DataSet GetSHGClientRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@CUST_ID", objBO_Finance.CUST_ID);
            InParameters.Add("@dtGROUPID", objBO_Finance.dtGroupID);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.GetDataSet("usp_GetSHGClientRecords", InParameters, out SQLError);
        }

        public DataSet GetSavingsInterest (BO_Finance objBO_Finance , out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@DM_CODE", objBO_Finance.DM_CODE);
            return cdo.GetDataSet("prc_InterestCalculation", InParameters, out SQLError);
        }
        public DataTable DetailsDepositReports(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@actype", objBO_Finance.actype);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            return cdo.GetDataTable("usp_DetailsDepositReports", InParameters, out SQLError);
        }

        public DataTable SavingsDetailsListReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACTYPE", objBO_Finance.actype);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("Proc_DetailListSavingDeposit", InParameters, out SQLError);
        }

        public DataTable HomeSavingsDetailsListReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACTYPE", objBO_Finance.actype);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("Proc_DetailListHomeSavingDeposit", InParameters, out SQLError);
        }

        public DataTable ShareDividendCalculation(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACTYPE", objBO_Finance.ACTYPE);
            InParameters.Add("@ASONDATE", objBO_Finance.AsOnDate);
            return cdo.GetDataTable("PROC_DividendCal", InParameters, out SQLError);
        }

        public DataSet ShareDetailReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACTYPE", objBO_Finance.ACTYPE);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            return cdo.GetDataSet("Proc_DetailListShare", InParameters, out SQLError);
        }

        public DataSet BRSDetailsReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);
            return cdo.GetDataSet("usp_RepBRSReport", InParameters, out SQLError);
        }

        public DataSet GetNEFTGrid(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@FLAG", objBO_Finance.Flag);
            InParameters.Add("@ENTRYDT", objBO_Finance.DTSCust);
           
            return cdo.GetDataSet("usp_GETBANKDETAILS", InParameters, out SQLError);
        }

        public DataSet GetIFSCCODE(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@BANKNAME", objBO_Finance.BANKNAME);
            InParameters.Add("@BRANCHNAME", objBO_Finance.BRANCHNAME);
            return cdo.GetDataSet("usp_GETBANKDETAILS", InParameters, out SQLError);
        }
        public DataSet GenerateReportForFarm (BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@FLAG", objBO_Finance.Flag);
            InParameters.Add("@SCHEMECODE", objBO_Finance.SCHEME_CODE);
            InParameters.Add("@ASONDATE", objBO_Finance.AsOnDate);
            //return cdo.GetDataSet("usp_NPARPT", InParameters, out SQLError);
            return cdo.GetDataSet("proc_NPARPT", InParameters, out SQLError);
        }

        public DataTable GetSecurityDetails(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();

            InParameters.Add("@TYPE", objBO_Finance.TYPE);

            return cdo.GetDataTable("proc_Get_Security_details", InParameters);
        }
        public DataTable SHGDetailsListReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACTYPE", objBO_Finance.actype);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("Proc_DetailListshgDeposit", InParameters, out SQLError);
        }

        public DataTable LoanListDetailsReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SCHEMECODE", objBO_Finance.SCHEME_CODE);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            //return cdo.GetDataTable("proc_LoanDetailList", InParameters, out SQLError); 
            return cdo.GetDataTable("proc_UpdatedLoanDetailList", InParameters, out SQLError);
        }


        public DataTable FixedDepositsDetailsListReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACTYPE", objBO_Finance.actype);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("Proc_DetailListFixedDeposit", InParameters, out SQLError);
        }
        public DataTable MisDetailsListDepositReports(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACTYPE", objBO_Finance.actype);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("Proc_DetailListMisDeposit", InParameters, out SQLError);
        }



        public DataTable DepositeCertificateListReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACTYPE", objBO_Finance.actype);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("Proc_DetailListDepositCirtificate", InParameters, out SQLError);
        }
        public DataTable RecurringDepositListReport(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACTYPE", objBO_Finance.actype);
            InParameters.Add("@AsOnDate", objBO_Finance.AsOnDate);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("Proc_DetailListRecurringDeposit", InParameters, out SQLError);
        }

        public DataTable FinalAccountSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ReportType", objBO_Finance.ReportType);
            //InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("usp_GetFAReportSettingsDetails", InParameters, out SQLError);
        }


        public int InsertUpdateDeleteItemMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ITEM_CODE", objBO_Finance.Item_Code);
            InParameters.Add("@NAME", objBO_Finance.NAME);
            InParameters.Add("@ROL", objBO_Finance.ROL);
            InParameters.Add("@MU", objBO_Finance.MU);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            InParameters.Add("@MCLASS", objBO_Finance.MCLASS);
            InParameters.Add("@PUR_LDG", objBO_Finance.PUR_LDG);
            InParameters.Add("@SALE_LDG", objBO_Finance.SALE_LDG);
            InParameters.Add("@TOT_QTY", objBO_Finance.TOT_QTY);
            InParameters.Add("@HSNNO", objBO_Finance.HSNNO);
            InParameters.Add("@CGST", objBO_Finance.CGST);
            InParameters.Add("@SGST", objBO_Finance.SGST);
            return cdo.InsertUpdateDelete("usp_InsertUpdateItemMaster", InParameters, out SQLError);
        }

        public int InsertUpdateDeleteStockTrans(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@EntryType", objBO_Finance.EntryType);
            InParameters.Add("@TransType", objBO_Finance.TransType);
            InParameters.Add("@EntryDate", objBO_Finance.EntryDate);
            InParameters.Add("@GROUP_CODE", objBO_Finance.GROUP_CODE);
            InParameters.Add("@SupCode", objBO_Finance.SupCode);
            //InParameters.Add("@SupName", objBO_Finance.SupName);
            //InParameters.Add("@SupName", objBO_Finance.SupName);
            InParameters.Add("@Sale_to", objBO_Finance.Sale_to);

            InParameters.Add("@Address1", objBO_Finance.Address1);
            InParameters.Add("@GSTINNo", objBO_Finance.GSTINNo);
            InParameters.Add("@IDNO", objBO_Finance.IDNO);
            InParameters.Add("@Comments", objBO_Finance.Comments);
            InParameters.Add("@dtItemDetails", objBO_Finance.dtItemDetails);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);

            return cdo.InsertUpdateDelete("usp_InsertUpdateStockTrans", InParameters, out SQLError);
        }

        public int InsertUpdateDeleteSubLedgerMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@sl_name", objBO_Finance.sl_Name);
            InParameters.Add("@actype", objBO_Finance.actype);
            InParameters.Add("@date_of_opening", objBO_Finance.date_of_opening);
            InParameters.Add("@last_tran_date", objBO_Finance.last_tran_date);
            InParameters.Add("@ldg_code", objBO_Finance.LDG_CODE);
            InParameters.Add("@act_op_dr", objBO_Finance.ACT_OP_DR);
            InParameters.Add("@act_op_cr", objBO_Finance.ACT_OP_CR);
            InParameters.Add("@empcode", objBO_Finance.EmpCode);
            InParameters.Add("@terminal_id", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@MEM_NO", objBO_Finance.MemNo);
            InParameters.Add("@GSTINNO", objBO_Finance.GSTINNo);
            InParameters.Add("@Address1", objBO_Finance.Address1);
            return cdo.InsertUpdateDelete("usp_InsertUpdateSubLedgerMaster", InParameters, out SQLError);
        }

        public int InsertSubLedgerMasters(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@sl_name", objBO_Finance.sl_Name);
            InParameters.Add("@actype", objBO_Finance.actype);
            InParameters.Add("@date_of_opening", objBO_Finance.date_of_opening);
            InParameters.Add("@last_tran_date", objBO_Finance.last_tran_date);
            InParameters.Add("@ldg_code", objBO_Finance.LDG_CODE);
            InParameters.Add("@act_op_dr", objBO_Finance.ACT_OP_DR);
            InParameters.Add("@act_op_cr", objBO_Finance.ACT_OP_CR);
            InParameters.Add("@empcode", objBO_Finance.EmpCode);
            InParameters.Add("@terminal_id", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@MEM_NO", objBO_Finance.MemNo);
            InParameters.Add("@GSTINNO", objBO_Finance.GSTINNo);
            InParameters.Add("@Address1", objBO_Finance.Address1);
            return cdo.InsertUpdateDelete("usp_InsertSubLedgerMaster", InParameters, out SQLError);
        }





        public DataTable GetSubLedgerMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@context", objBO_Finance.Context);
            return cdo.GetDataTable("usp_GetSubLedgerMasterRecords", InParameters, out SQLError);
        }
        public DataTable GetLedgerDtl(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();

            InParameters.Add("@LDGCODE", objBO_Finance.LDG_CODE);

            return cdo.GetDataTable("Proc_GetLedgerDetails", InParameters, out SQLError);
        }


        public int InsertUpdateDeleteCashierCounterBook(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@CCB_DATE", objBO_Finance.CCB_DATE);
            InParameters.Add("@VOUCHER_NO", objBO_Finance.VOUCHER_NO);
            InParameters.Add("@NARRATION1", objBO_Finance.NARRATION1);
            InParameters.Add("@NARRATION2", objBO_Finance.NARRATION2);
            InParameters.Add("@PAYMENT_RECEIPT", objBO_Finance.PAYMENT_RECEIPT);
            InParameters.Add("@TYPEOFINSTRUMENT", objBO_Finance.TYPEOFINSTRUMENT);
            InParameters.Add("@INSTRUMENTNO", objBO_Finance.INSTRUMENTNO);
            InParameters.Add("@DT_ON_INS", objBO_Finance.DT_ON_INS);
            InParameters.Add("@DAILY_SRL", objBO_Finance.DAILY_SRL);
            InParameters.Add("@ENTRY_FR", objBO_Finance.ENTRY_FR);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            InParameters.Add("@TERMINAL_NO", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@dtLedgerDetails", objBO_Finance.dtLedgerDetails);
            return cdo.InsertUpdateDelete("usp_InsertUpdateCashierCounterBook", InParameters, out SQLError);
        }

        public int InsertUpdateDeleteCashPayOthers(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@NARATION", objBO_Finance.NARRATION1);
            InParameters.Add("@CCB_DATE", objBO_Finance.CCB_DATE);
            InParameters.Add("@VOUCHER_NO", objBO_Finance.VOUCHER_NO);
            InParameters.Add("@BRANCHID", objBO_Finance.BranchCode);
            InParameters.Add("@TR_TYPE", objBO_Finance.TR_TYPE);
            InParameters.Add("@AMT_DEBIT", objBO_Finance.AMT_DEBIT);
            InParameters.Add("@AMT_CREDIT", objBO_Finance.AMT_CREDIT);
            InParameters.Add("@EmpCode", objBO_Finance.EmpCode);

            //InParameters.Add("@Flag", objBO_Finance.Flag = 1);
            //InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE = 63907);
            //InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE = null);
            //InParameters.Add("@NARATION", objBO_Finance.NARRATION1 = null);
            //InParameters.Add("@CCB_DATE", objBO_Finance.CCB_DATE = null);
            //InParameters.Add("@VOUCHER_NO", objBO_Finance.VOUCHER_NO = null);
            //InParameters.Add("@BRANCHID", objBO_Finance.BranchCode = '100');
            //InParameters.Add("@TR_TYPE", objBO_Finance.TR_TYPE = 'ra');
            //InParameters.Add("@AMT_DEBIT", objBO_Finance.AMT_DEBIT = null);
            //InParameters.Add("@AMT_CREDIT", objBO_Finance.AMT_CREDIT = null);
            //InParameters.Add("@EmpCode", objBO_Finance.EmpCode = null);
            return cdo.InsertUpdateDelete("usp_InsertUpdateCashPayOthers", InParameters, out SQLError);
        }

        public DataSet GetAccountsDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.GetDataSet("usp_GetAccountDetails", InParameters, out SQLError);
        }
        public DataSet Getjournalaccholder(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            return cdo.GetDataSet("proc_journal_accholder", InParameters);
        }

        public DataSet GetSHGAccountDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@LOAN_TYPE", objBO_Finance.LOAN_TYPE);
            return cdo.GetDataSet("usp_GetAccountDetails", InParameters, out SQLError);
        }

        public DataSet GetAccountListShg (BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@actype", objBO_Finance.actype);
            return cdo.GetDataSet("usp_GetAccountDetails", InParameters, out SQLError);
        }

        public DataSet InvestementAccountDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@actype", objBO_Finance.actype);
            return cdo.GetDataSet("usp_GetAccountDetails", InParameters, out SQLError);
        }

        public DataSet GetLoanAccountsDetail(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@actype", objBO_Finance.actype);
            return cdo.GetDataSet("usp_GetAccountDetails", InParameters, out SQLError);
        }

        public DataSet DashboardReportfirst(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@branchid", objBO_Finance.BranchCode);
            return cdo.GetDataSet("usp_MISInterestDetails", InParameters, out SQLError);
        }

        public DataSet DashboardReportSecond(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@BRANCHID", objBO_Finance.BranchCode);
            return cdo.GetDataSet("usp_Ledger_Balance_AsOn", InParameters, out SQLError);
        }

        public int InsertUpdateDeleteLoanAccountOpening(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@actype", objBO_Finance.actype);
            InParameters.Add("@date_of_opening", objBO_Finance.date_of_opening);
            InParameters.Add("@last_tran_date", objBO_Finance.last_tran_date);
            InParameters.Add("@old_acno", objBO_Finance.old_acno);
            InParameters.Add("@lf_acno", objBO_Finance.lf_acno);                                                                             
            InParameters.Add("@EmpCode", objBO_Finance.EmpCode);
            InParameters.Add("@TERMINAL_ID", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@SCHEME_CODE", objBO_Finance.SCHEME_CODE);
            InParameters.Add("@AC_STATUS", objBO_Finance.AC_STATUS);
            InParameters.Add("@APPL_DT", objBO_Finance.APPL_DT);
            InParameters.Add("@LOAN_AMNT", objBO_Finance.LOAN_AMNT);
            InParameters.Add("@SUBSIDY", objBO_Finance.SUBSIDY);
            InParameters.Add("@NET_LOAN", objBO_Finance.NET_LOAN);
            InParameters.Add("@CASH_DISB", objBO_Finance.CASH_DISB);
            InParameters.Add("@ROI", objBO_Finance.ROI);
            InParameters.Add("@OD_ROI", objBO_Finance.OD_ROI);
            InParameters.Add("@DURATION", objBO_Finance.DURATION);
            InParameters.Add("@REPAY_MODE", objBO_Finance.REPAY_MODE);
            InParameters.Add("@NO_OF_INST", objBO_Finance.NO_OF_INST);
            InParameters.Add("@INST_APPL", objBO_Finance.INST_APPL);
            InParameters.Add("@INST_ST_DATE", objBO_Finance.INST_ST_DATE);
            InParameters.Add("@INST_AMOUNT", objBO_Finance.INST_AMOUNT);
            InParameters.Add("@SANC_DATE", objBO_Finance.SANC_DATE);
            InParameters.Add("@SANC_PER", objBO_Finance.SANC_PER);
            InParameters.Add("@SANC_DT", objBO_Finance.SANC_DT);
            InParameters.Add("@LF_NO", objBO_Finance.LF_NO);
            InParameters.Add("@FIRST_DISB_DT", objBO_Finance.FIRST_DISB_DT);
            InParameters.Add("@P_CODE", objBO_Finance.P_CODE);
            InParameters.Add("@LOAN_TYPE", objBO_Finance.LOAN_TYPE);
            InParameters.Add("@LAST_REP_DT", objBO_Finance.LAST_REP_DT);
            InParameters.Add("@ASSES", objBO_Finance.ASSES);
            InParameters.Add("@APP_AMOUNT", objBO_Finance.APP_AMOUNT);
            InParameters.Add("@ODPR", objBO_Finance.ODPR);
            InParameters.Add("@dtClientMaster", objBO_Finance.dtClientMaster);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            return cdo.InsertUpdateDelete("usp_InsertUpdateLoanAccountOpening", InParameters, out SQLError);
        }

        public DataTable GetItemMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@Item_Code", objBO_Finance.Item_Code);
            return cdo.GetDataTable("usp_GetItemMasterRecords", InParameters, out SQLError);
        }

        /// <summary>
        /// Insert Update Delete Operation for KYC Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteKYCMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@CUSTID", objBO_Finance.CUST_ID);
            InParameters.Add("@Name", objBO_Finance.Name);
            InParameters.Add("@FName", objBO_Finance.FName);
            InParameters.Add("@MName", objBO_Finance.MName);
            InParameters.Add("@LName", objBO_Finance.LName);
            InParameters.Add("@GuardianName", objBO_Finance.GuardianName);
            InParameters.Add("@Guardian", objBO_Finance.Guardian);
            //InParameters.Add("@SurName", objBO_Finance.SurName);
            //InParameters.Add("@MidName", objBO_Finance.MidName);
            InParameters.Add("@POCode", objBO_Finance.POCode);
            InParameters.Add("@PSCode", objBO_Finance.PSCode);
            InParameters.Add("@BLKCode", objBO_Finance.BLKCode);
            InParameters.Add("@DISCode", objBO_Finance.DISCode);
            InParameters.Add("@CLStatus", objBO_Finance.CLStatus);
            InParameters.Add("@Nominee", objBO_Finance.Nominee);
            InParameters.Add("@Sex", objBO_Finance.Sex);
            InParameters.Add("@RELCode", objBO_Finance.RELCode);
            InParameters.Add("@PROFCode", objBO_Finance.PROFCode);
            InParameters.Add("@DTSCust", objBO_Finance.DTSCust);
            InParameters.Add("@MemNo", objBO_Finance.MemNo);
            InParameters.Add("@Age", objBO_Finance.Age);
            InParameters.Add("@VillCode", objBO_Finance.VillCode);
            InParameters.Add("@CatCode", objBO_Finance.CatCode);
            InParameters.Add("@SpCatCode", objBO_Finance.SpCatCode);
            InParameters.Add("@TelNo", objBO_Finance.TelNo);
            InParameters.Add("@LandHolding", objBO_Finance.LandHolding);
            InParameters.Add("@AcStatus", objBO_Finance.AcStatus);
            InParameters.Add("@EntryDate", objBO_Finance.EntryDate);
            //InParameters.Add("@TrailTOT", objBO_Finance.TrailTOT);
            //InParameters.Add("@TerminalID", objBO_Finance.TerminalID);
            InParameters.Add("@EmpCode", objBO_Finance.EmpCode);
            InParameters.Add("@PictPathq", objBO_Finance.PictPath);
            InParameters.Add("@SignPath", objBO_Finance.SignPath);
            InParameters.Add("@GPCode", objBO_Finance.GPCode);
            InParameters.Add("@SUCode", objBO_Finance.SUCode);
            InParameters.Add("@EduCode", objBO_Finance.EduCode);
            InParameters.Add("@PANCardNo", objBO_Finance.PANCardNo);
            InParameters.Add("@RationCardNo", objBO_Finance.RationCardNo);
            InParameters.Add("@VotarCardNo", objBO_Finance.VotarCardNo);
            InParameters.Add("@PassportNo", objBO_Finance.PassportNo);
            InParameters.Add("@ClientType", objBO_Finance.ClientType);
            InParameters.Add("@BPLNo", objBO_Finance.BPLNo);
            InParameters.Add("@MonthlyIncome", objBO_Finance.MonthlyIncome);
            InParameters.Add("@DOB", objBO_Finance.DOB);
            InParameters.Add("@PINCode", objBO_Finance.PINCode);
            InParameters.Add("@LandType", objBO_Finance.LandType);
            InParameters.Add("@CloseDT", objBO_Finance.CloseDT);
            InParameters.Add("@Cause", objBO_Finance.Cause);
            InParameters.Add("@ReciCER", objBO_Finance.ReciCER);
            InParameters.Add("@SchCER", objBO_Finance.SchCER);
            InParameters.Add("@AdharNo", objBO_Finance.AdharNo);
            InParameters.Add("@KCCCard", objBO_Finance.KCCCard);
            //InParameters.Add("@FSTName", objBO_Finance.FSTName);
            //InParameters.Add("@LSTName", objBO_Finance.LSTName);
            InParameters.Add("@BranchCode", objBO_Finance.BranchCode);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);

            return cdo.InsertUpdateDelete("usp_InsertUpdateKYCMaster", InParameters, out SQLError);
        }

        public int InsertUpdateNEFTRTGS(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@ACNO", objBO_Finance.SB_SL_CODE);
            InParameters.Add("@SL_NAME", objBO_Finance.sl_Name);
            InParameters.Add("@AMOUNT", objBO_Finance.APP_AMOUNT);
            InParameters.Add("@COMMISION", objBO_Finance.COMM);
            InParameters.Add("@TOTALAMT", objBO_Finance.AMOUNT);
            InParameters.Add("@BENEFACNO", objBO_Finance.AC_NO);
            InParameters.Add("@BENEFNAME", objBO_Finance.NAME);
            InParameters.Add("@IFSCODE", objBO_Finance.IFSC);
            InParameters.Add("@BANKNAME", objBO_Finance.BANKNAME);
            InParameters.Add("@BRANCHNAME", objBO_Finance.BRANCHNAME);
            InParameters.Add("@ADDRESS", objBO_Finance.Address1);
            InParameters.Add("@BENEFIADDRESS", objBO_Finance.Address2);
            InParameters.Add("@MOBILENO", objBO_Finance.MODEL_NO);
            InParameters.Add("@ENTRYDT", objBO_Finance.DTSCust);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            InParameters.Add("@BranchCode", objBO_Finance.BranchCode);

            return cdo.InsertUpdateDelete("usp_InsertUpdateNEFTRTGS", InParameters, out SQLError);
        }

        public int InsertFileData(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@FILE_NAME", objBO_Finance.Name);
            InParameters.Add("@FILE_PATH", objBO_Finance.SignPath);
            InParameters.Add("@FILE_TYPE", objBO_Finance.PictPath);
            InParameters.Add("@USER_ID", objBO_Finance.UserName);
            InParameters.Add("@ENTRY_DT", objBO_Finance.DTSCust);
            return cdo.InsertUpdateDelete("usp_INSERTFILEDETAILS", InParameters, out SQLError);
        }
        public int InsertUpdateInitialSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LISCENCEE_NAME", objBO_Finance.LISCENCEE_NAME);
            InParameters.Add("@LISCENCEE_ADDRESS1", objBO_Finance.LISCENCEE_ADDRESS1);
            InParameters.Add("@LISCENCEE_ADDRESS2", objBO_Finance.LISCENCEE_ADDRESS2);
            InParameters.Add("@STATE_CODE", objBO_Finance.STATE_CODE);
            InParameters.Add("@DIST_CODE", objBO_Finance.DIST_CODE);
            InParameters.Add("@SOCIETY_CODE", objBO_Finance.SOCIETY_CODE);
            InParameters.Add("@SOCIETY_BR_CODE", objBO_Finance.SOCIETY_BR_CODE);
            InParameters.Add("@YEAR_START_DT", objBO_Finance.YEAR_START_DT);
            InParameters.Add("@YEAR_END_DT", objBO_Finance.YEAR_END_DT);
            InParameters.Add("@GSTINNO", objBO_Finance.GSTINNo);
            InParameters.Add("@cinvestAmt", objBO_Finance.CROPAMT);
            InParameters.Add("@MiscellaneousAmt", objBO_Finance.MISAMT);
            InParameters.Add("@BANKNAME", objBO_Finance.BANK_NAME);
            InParameters.Add("@BANKBRNAME", objBO_Finance.BankBranchName);
            InParameters.Add("@BANKBRADD", objBO_Finance.BankBranchAddress);
            InParameters.Add("@IFCSCCODE", objBO_Finance.IFSC);
            InParameters.Add("@MICR", objBO_Finance.MICR);
            InParameters.Add("@SOCIETYACNO", objBO_Finance.SocietyAcno);
            InParameters.Add("@REGDNO", objBO_Finance.RegdNo);
            InParameters.Add("@REGDDATE", objBO_Finance.RegdDate);
            InParameters.Add("@PASSBKPRINTDT", objBO_Finance.PassbookprintDate);
            InParameters.Add("@CSPAcNo", objBO_Finance.CSPAcno);
            InParameters.Add("@CommLdg", objBO_Finance.CommLedger);
            InParameters.Add("@SuspLdg", objBO_Finance.SuspenseLdg);
            InParameters.Add("@dtBranchDetails", objBO_Finance.BranchDetails);
            return cdo.InsertUpdateDelete("usp_InsertUpdateInitialSettings", InParameters, out SQLError);
        }

        public int InsertInitialSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LISCENCEE_NAME", objBO_Finance.LISCENCEE_NAME);
            InParameters.Add("@LISCENCEE_ADDRESS1", objBO_Finance.LISCENCEE_ADDRESS1);
            InParameters.Add("@LISCENCEE_ADDRESS2", objBO_Finance.LISCENCEE_ADDRESS2);
            InParameters.Add("@STATE_CODE", objBO_Finance.STATE_CODE);
            InParameters.Add("@DIST_CODE", objBO_Finance.DIST_CODE);
            InParameters.Add("@SOCIETY_CODE", objBO_Finance.SOCIETY_CODE);
            InParameters.Add("@SOCIETY_BR_CODE", objBO_Finance.SOCIETY_BR_CODE);
            InParameters.Add("@YEAR_START_DT", objBO_Finance.YEAR_START_DT);
            InParameters.Add("@YEAR_END_DT", objBO_Finance.YEAR_END_DT);
            InParameters.Add("@GSTINNO", objBO_Finance.GSTINNo);
            InParameters.Add("@BANKNAME", objBO_Finance.BANK_NAME);
            InParameters.Add("@BANKBRNAME", objBO_Finance.BankBranchName);
            InParameters.Add("@BANKBRADD", objBO_Finance.BankBranchAddress);
            InParameters.Add("@IFCSCCODE", objBO_Finance.IFSC);
            InParameters.Add("@MICR", objBO_Finance.MICR);
            InParameters.Add("@SOCIETYACNO", objBO_Finance.SocietyAcno);
            InParameters.Add("@REGDNO", objBO_Finance.RegdNo);
            InParameters.Add("@REGDDATE", objBO_Finance.RegdDate);
            InParameters.Add("@PASSBKPRINTDT", objBO_Finance.PassbookprintDate);
            InParameters.Add("@CSPAcNo", objBO_Finance.CSPAcno);
            InParameters.Add("@CommLdg", objBO_Finance.CommLedger);
            InParameters.Add("@SuspLdg", objBO_Finance.SuspenseLdg);

            InParameters.Add("@dtBranchDetails", objBO_Finance.BranchDetails);

            return cdo.InsertUpdateDelete("usp_InsertInitialSettings", InParameters, out SQLError);
        }

        public DataSet GetInitialSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            return cdo.GetDataSet("usp_GetInitialSettings", InParameters, out SQLError);
        }
        public DataTable GetPassbookTrans(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@SLCODE", objBO_Finance.SL_CODE);
            InParameters.Add("@VFOPDT", objBO_Finance.DATE_UPTO);

            return cdo.GetDataTable("PROC_SETPASSBOOKPRINT_NEW", InParameters);
        }
        //public DataSet GetPassbookTrans(BO_Finance objBO_Finance)
        //{
        //    Hashtable InParameters = new Hashtable();
        //    InParameters.Add("@SLCODE", objBO_Finance.SL_CODE);
        //    InParameters.Add("@VFOPDT", objBO_Finance.DATE_UPTO);

        //    return cdo.GetDataSet("PROC_SETPASSBOOKPRINT", InParameters);
        //}



        public DataSet loanBalanceUpdate(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);

            return cdo.GetDataSet("proc_Loan_balance_update", InParameters, out SQLError);
        }
        public int InsertUpdateDeleteAccountSHG(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@CUST_ID", objBO_Finance.CUST_ID);
            InParameters.Add("@NAME", objBO_Finance.NAME);
            InParameters.Add("@GUARDIAN_NAME", objBO_Finance.GuardianName);
            InParameters.Add("@Guardian", objBO_Finance.Guardian);
            InParameters.Add("@PO_CODE", objBO_Finance.POCode);
            InParameters.Add("@PS_CODE", objBO_Finance.PSCode);
            InParameters.Add("@BLK_CODE", objBO_Finance.BLKCode);
            InParameters.Add("@DIS_CODE", objBO_Finance.DISCode);
            InParameters.Add("@CL_STATUS", objBO_Finance.CLStatus);
            InParameters.Add("@DTSCust", objBO_Finance.DTSCust);
            InParameters.Add("@VILL_CODE", objBO_Finance.VillCode);
            InParameters.Add("@Cat_Code", objBO_Finance.CatCode);
            InParameters.Add("@Sp_Cat_Code", objBO_Finance.SpCatCode);
            InParameters.Add("@Tel_No", objBO_Finance.TelNo);
            InParameters.Add("@ac_status", objBO_Finance.ac_status);
            InParameters.Add("@TERMINAL_ID", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            InParameters.Add("@PictPath", objBO_Finance.PictPath);
            InParameters.Add("@SignPath", objBO_Finance.SignPath);
            InParameters.Add("@GP_Code", objBO_Finance.GPCode);
            InParameters.Add("@SU_Code", objBO_Finance.SUCode);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            InParameters.Add("@dtSHGMemberDetails", objBO_Finance.dtSHGMemberDetails);
            return cdo.InsertUpdateDelete("usp_InsertUpdateSHG", InParameters, out SQLError);
        }


        public int UpdateAccountSHG(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@CUST_ID", objBO_Finance.CUST_ID);
            InParameters.Add("@NAME", objBO_Finance.NAME);
            InParameters.Add("@GUARDIAN_NAME", objBO_Finance.GuardianName);
            InParameters.Add("@Guardian", objBO_Finance.Guardian);
            InParameters.Add("@PO_CODE", objBO_Finance.POCode);
            InParameters.Add("@PS_CODE", objBO_Finance.PSCode);
            InParameters.Add("@BLK_CODE", objBO_Finance.BLKCode);
            InParameters.Add("@DIS_CODE", objBO_Finance.DISCode);
            InParameters.Add("@CL_STATUS", objBO_Finance.CLStatus);
            InParameters.Add("@DTSCust", objBO_Finance.DTSCust);
            InParameters.Add("@VILL_CODE", objBO_Finance.VillCode);
            InParameters.Add("@Cat_Code", objBO_Finance.CatCode);
            InParameters.Add("@Sp_Cat_Code", objBO_Finance.SpCatCode);
            InParameters.Add("@Tel_No", objBO_Finance.TelNo);
            InParameters.Add("@ac_status", objBO_Finance.ac_status);
            InParameters.Add("@TERMINAL_ID", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            InParameters.Add("@PictPath", objBO_Finance.PictPath);
            InParameters.Add("@SignPath", objBO_Finance.SignPath);
            InParameters.Add("@GP_Code", objBO_Finance.GPCode);
            InParameters.Add("@SU_Code", objBO_Finance.SUCode);
            InParameters.Add("@dtSHGMemberDetails", objBO_Finance.dtSHGMemberDetails);
            return cdo.InsertUpdateDelete("usp_UpdateSHG", InParameters, out SQLError);

        }
        public DataTable GetLoanSchemeMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SCHEME_CODE", objBO_Finance.SCHEME_CODE);
            return cdo.GetDataTable("usp_GetLoanSchemeMasterRecords", InParameters, out SQLError);
        }

        public DataTable GetBranch(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@BRANCHID", objBO_Finance.BranchCode);
            return cdo.GetDataTable("usp_BRANCHDETAILS", InParameters, out SQLError);
        }

        public DataTable GetBank (BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            return cdo.GetDataTable("usp_GETBANKDETAILS", InParameters, out SQLError);
        }

        public DataTable GetBankBranch(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@BANKNAME", objBO_Finance.BANKNAME);
            return cdo.GetDataTable("usp_GETBANKDETAILS", InParameters, out SQLError);
        }

        public DataTable GetBankBranchBYIFSC(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@IFSCCODE", objBO_Finance.IFSCCODE);
            return cdo.GetDataTable("usp_GETBANKDETAILS", InParameters, out SQLError);
        }

        public DataTable GetBankBYIFSC(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@IFSCCODE", objBO_Finance.IFSCCODE);
            return cdo.GetDataTable("usp_GETBANKDETAILS", InParameters, out SQLError);
        }

        public DataTable GetItemDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            return cdo.GetDataTable("usp_ItemDetails", InParameters, out SQLError);
        }

        public DataTable GetTotalDepositSHG (BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@CUST_ID", objBO_Finance.CUST_ID);
            InParameters.Add("@ENTRYDATE", objBO_Finance.ENTRYDATE);
            return cdo.GetDataTable("usp_GetSHGLOANDETAILS", InParameters, out SQLError);
        }
        public DataTable GetsubLedgerDetailsLoans(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);

            return cdo.GetDataTable("usp_GetSubLedgerDetailsLoan", InParameters, out SQLError);
        }
        public DataTable GetCashBookLedgerName(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);

            return cdo.GetDataTable("proc_POPCashBook", InParameters);
        }



        public DataTable GetUserMasterRecord(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@UserID", objBO_Finance.UserId);
            return cdo.GetDataTable("usp_GetUserMasterRecords", InParameters, out SQLError);
        }

        public DataTable chequeUser(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@UserName", objBO_Finance.UserName);
            return cdo.GetDataTable("usp_GetChequeUser", InParameters, out SQLError);
        }

        public DataTable GetRecords (BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@NAME", objBO_Finance.NAME);
            return cdo.GetDataTable("usp_GetRecords", InParameters, out SQLError);
        }



        /// <summary>
        ///Get Deposit Scheme Details
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetDepositMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@DM_CODE", objBO_Finance.DM_CODE);
            InParameters.Add("@SCHEME_TYPE", objBO_Finance.SCHEME_TYPE);
            return cdo.GetDataTable("usp_GetDepositMasterRecords", InParameters, out SQLError);
        }

        public DataTable GetEmpName(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            return cdo.GetDataTable("usp_GetDepositMasterRecords", InParameters, out SQLError);
        }
        public DataTable GetDepositMasterRecordsForSHG (BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SCHEME_TYPE", objBO_Finance.SCHEME_TYPE);
            return cdo.GetDataTable("usp_GetDepositMasterRecords", InParameters, out SQLError);
        }

        public DataTable GetAccountNos(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            return cdo.GetDataTable("usp_GetAccountNo", InParameters, out SQLError);
        }

        public DataSet GetAccountNo(object flag, string SL_CODE, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", flag);
            InParameters.Add("@SL_CODE", SL_CODE);
            return cdo.GetDataSet("usp_GetSLCODE", InParameters, out SQLError);
        }

        public int InsertUpdateShareAccountOpening(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@actype", objBO_Finance.actype);
            InParameters.Add("@NOMENCLATURE", objBO_Finance.NOMENCLATURE);
            InParameters.Add("@date_of_opening", objBO_Finance.date_of_opening);
            InParameters.Add("@last_tran_date", objBO_Finance.last_tran_date);
            InParameters.Add("@old_acno", objBO_Finance.old_acno);
            InParameters.Add("@lf_acno", objBO_Finance.lf_acno);
            InParameters.Add("@empcode", objBO_Finance.EmpCode);
            InParameters.Add("@terminal_id", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@PAYDIVACNO", objBO_Finance.PAYDIVACNO);
            InParameters.Add("@dtClientMaster", objBO_Finance.dtClientMaster);
            InParameters.Add("@dtNomineeDetailsTable", objBO_Finance.dtNomineeDetailsTable);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);

            return cdo.InsertUpdateDelete("usp_InsertUpdateShareAccountOpening", InParameters, out SQLError);
        }

        public int InsertUpdateFinalAccountSettings (BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ReportType", objBO_Finance.ReportType);
            InParameters.Add("@accountsettings", objBO_Finance.ReportType);
            return cdo.InsertUpdateDelete("UpdateDelAcctDetailsOnReporttype", InParameters, out SQLError);
        }

        public int InsertUpdateDeleteAccountOpening(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            InParameters.Add("@actype", objBO_Finance.actype);
            InParameters.Add("@date_of_opening", objBO_Finance.date_of_opening);
            InParameters.Add("@last_tran_date", objBO_Finance.last_tran_date);
            InParameters.Add("@old_acno", objBO_Finance.old_acno);
            InParameters.Add("@lf_acno", objBO_Finance.lf_acno);
            InParameters.Add("@empcode", objBO_Finance.EmpCode);
            InParameters.Add("@terminal_id", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@dm_code", objBO_Finance.dm_code);
            InParameters.Add("@operation", objBO_Finance.operation);
            InParameters.Add("@intro_acno", objBO_Finance.intro_acno);
            InParameters.Add("@intro_name", objBO_Finance.intro_name);
            InParameters.Add("@intro_address", objBO_Finance.intro_address);
            InParameters.Add("@intro_phone", objBO_Finance.intro_phone);
            InParameters.Add("@deposit_amount", objBO_Finance.deposit_amount);
            InParameters.Add("@depo_prd_m", objBO_Finance.depo_prd_m);
            InParameters.Add("@percentage", objBO_Finance.percentage);
            InParameters.Add("@int_goes_to", objBO_Finance.int_goes_to);
            InParameters.Add("@date_of_maturity", objBO_Finance.date_of_maturity);
            InParameters.Add("@maturity_amt", objBO_Finance.maturity_amt);
            InParameters.Add("@MATURITYAMT", objBO_Finance.MATURITYAMT);
            InParameters.Add("@FROMDATE", objBO_Finance.FROMDATE);
            InParameters.Add("@FROMDT", objBO_Finance.FROMDT);
            InParameters.Add("@ENDDATE", objBO_Finance.ENDDATE);
            InParameters.Add("@int_type", objBO_Finance.int_type);
            InParameters.Add("@category", objBO_Finance.category);
            InParameters.Add("@last_wt_date", objBO_Finance.last_wt_date);
            InParameters.Add("@dtClientMaster", objBO_Finance.dtClientMaster);
            InParameters.Add("@dtAuthDetailsTable", objBO_Finance.dtAuthDetailsTable);
            InParameters.Add("@dtNomineeDetailsTable", objBO_Finance.dtNomineeDetailsTable);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            InParameters.Add("@ifccode", objBO_Finance.IFCCODE);
            InParameters.Add("@Emplocode", objBO_Finance.EMPCODE);

            return cdo.InsertUpdateDelete("usp_InsertUpdateAccountOpening", InParameters, out SQLError);
        }
        public int UpdateLoanAcctsBal(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@Flag", objBO_Finance.Flag);
            //InParameters.Add("@sl_code", objBO_Finance.SL_CODE);
            //InParameters.Add("@PRINCUR", objBO_Finance.PRINCUR);
            //InParameters.Add("@prin_od", objBO_Finance.prin_od);
            //InParameters.Add("@INT_CUR", objBO_Finance.INT_CUR);
            //InParameters.Add("@int_od", objBO_Finance.int_od);
            ////InParameters.Add("@DATE_UPTO", objBO_Finance.DATE_UPTO);
            //InParameters.Add("@date_upto", objBO_Finance.DATE_UPTO);
            //InParameters.Add("@Update_date", objBO_Finance.DATE_UPTO);
            //InParameters.Add("@ADV_COL", objBO_Finance.ADV_COL);
            //InParameters.Add("@Prin_out", objBO_Finance.Prin_out);
            //InParameters.Add("@day_eno", objBO_Finance.day_eno);
            //InParameters.Add("@date_fr_int", objBO_Finance.date_fr_int);

            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@PRINCUR", objBO_Finance.PRINCUR);
            InParameters.Add("@prin_od", objBO_Finance.prin_od);
            InParameters.Add("@INT_CUR", objBO_Finance.INT_CUR);
            InParameters.Add("@int_od", objBO_Finance.int_od);
            //InParameters.Add("@DATE_UPTO", objBO_Finance.DATE_UPTO);
            InParameters.Add("@date_upto", objBO_Finance.DATE_UPTO);
            InParameters.Add("@Update_date", objBO_Finance.DATE_UPTO);
            //InParameters.Add("@ADV_COL", objBO_Finance.ADV_COL);
            InParameters.Add("@Prin_out", objBO_Finance.Prin_out);
            //InParameters.Add("@day_eno", objBO_Finance.day_eno);
            //InParameters.Add("@date_fr_int", objBO_Finance.date_fr_int);



            return cdo.InsertUpdateDelete("usp_UpdateLoanUpdateBalance", InParameters, out SQLError);
        }




        /// <summary>
        /// Get KYC Details Individual User
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetKYCDetailsRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@CUSTID", objBO_Finance.CUST_ID);
            InParameters.Add("@BranchID", objBO_Finance.BranchID);
            return cdo.GetDataTable("usp_GetKYCDetailsByCUSTCode", InParameters, out SQLError);


        }

        public DataTable GetJournalDeta(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@OLDAC_NO", objBO_Finance.OLDAC_NO);
            return cdo.GetDataTable("usp_GetJournalDetails", InParameters, out SQLError);
        }

        public DataTable GetJournalDetabySL_CODE(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            return cdo.GetDataTable("usp_GetJournalDetailsBYSL_CODE", InParameters, out SQLError);
        }

        public DataTable GetKYCDetailsRecordsList(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@CUSTID", objBO_Finance.CUST_ID);
            //return cdo.GetDataTable("usp_GetKYCDetailsByCUSTCode", InParameters, out SQLError);
            return cdo.GetDataTable("GetallKycDetails", InParameters, out SQLError);

        }

        /// <summary>
        /// Get Nominee Details Individual User
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetNomineeDetailsBySLCode(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SLCode", objBO_Finance.CUSTCode);
            return cdo.GetDataTable("usp_GetNomineeDetailsBySLCode", InParameters, out SQLError);
        }

        /// <summary>
        /// Get Auth Details Individual User
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetAuthSignBySLCode(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SLCode", objBO_Finance.CUSTCode);
            return cdo.GetDataTable("usp_GetAuthSignBySLCode", InParameters, out SQLError);
        }

        /// <summary>
        /// Insert Update Delete Operation for Group Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteGroupMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@GROUP_CODE", objBO_Finance.GROUP_CODE);
            InParameters.Add("@GROUP_NAME", objBO_Finance.GROUP_NAME);
            InParameters.Add("@GROUP_TYPE", objBO_Finance.GROUP_TYPE);
            InParameters.Add("@LINK", objBO_Finance.LINK);
            InParameters.Add("@FA_TYPE", objBO_Finance.FA_TYPE);
            InParameters.Add("@FA_TYPE2", objBO_Finance.FA_TYPE2);
            InParameters.Add("@NESTING_LEVEL", objBO_Finance.NESTING_LEVEL);
            InParameters.Add("@GINDEX", objBO_Finance.GINDEX);
            InParameters.Add("@D", objBO_Finance.D);
            InParameters.Add("@A", objBO_Finance.A);
            InParameters.Add("@P", objBO_Finance.P);
            InParameters.Add("@OTHERS", objBO_Finance.OTHERS);
            InParameters.Add("@AINDEX", objBO_Finance.AINDEX);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            InParameters.Add("@TERMINAL_ID", objBO_Finance.TERMINAL_ID);
            
            return cdo.InsertUpdateDelete("usp_InsertUpdateGroupMaster", InParameters, out SQLError);
        }

        /// <summary>
        /// Get Group Master Records
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetGroupMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@GroupCode", objBO_Finance.GROUP_CODE);

            return cdo.GetDataTable("usp_GetGroupMasterDetails", InParameters, out SQLError);
        }

        public DataTable GetLEDGERMasterRecords(BO_Finance objBO_Finance , out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LDGCODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("usp_GetLedgerMasterDetails", InParameters, out SQLError);
        }

        public DataTable GetInterestPaybleRecords (BO_Finance objBO_Finance , out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LDGCODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("usp_GetLedgerMasterDetails", InParameters, out SQLError);
        }
        public DataTable GetnterestIssuedRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@Flag", objBO_Finance.Flag);
            //InParameters.Add("@GroupCode", objBO_Finance.GROUP_CODE);

            return cdo.GetDataTable("usp_GetnterestIssuedRecords", InParameters, out SQLError);
        }

        public DataTable GetnterestReceiableRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@Flag", objBO_Finance.Flag);
            //InParameters.Add("@GroupCode", objBO_Finance.GROUP_CODE);

            return cdo.GetDataTable("usp_GetnterestReceivedRecords", InParameters, out SQLError);
        }



        /// <summary>
        /// Insert Update Delete Operation for Ledger Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteLedgerMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            InParameters.Add("@NOMENCLATURE", objBO_Finance.NOMENCLATURE);
            InParameters.Add("@GROUPCODE", objBO_Finance.GROUP_CODE);
            InParameters.Add("@SL_FLAG", objBO_Finance.SL_FLAG);
            InParameters.Add("@COST_FLAG", objBO_Finance.COST_FLAG);
            InParameters.Add("@LINDEX", objBO_Finance.LINDEX);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@CASH_BANK", objBO_Finance.CASH_BANK);
            InParameters.Add("@INT_PAID", objBO_Finance.INT_PAID);
            InParameters.Add("@ACT_OP_DR", objBO_Finance.ACT_OP_DR);
            InParameters.Add("@ACT_OP_CR", objBO_Finance.ACT_OP_CR);
            InParameters.Add("@INT_PAYBLE", objBO_Finance.INT_PAYBLE);
            InParameters.Add("@RBI_CODE", objBO_Finance.RBI_CODE);
            InParameters.Add("@ADD_WITH", objBO_Finance.ADD_WITH);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            InParameters.Add("@TERMINAL_ID", objBO_Finance.TERMINAL_ID);
            return cdo.InsertUpdateDelete("usp_InsertUpdateLedgerMaster", InParameters, out SQLError);
        }

        /// <summary>
        /// Get Ledger Master Records
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetLedgerMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@LDGCODE", objBO_Finance.LDG_CODE);
            return cdo.GetDataTable("usp_GetLedgerMasterDetails", InParameters, out SQLError);
        }

        //public DataTable GetSubLedgerRecords(BO_Finance objBO_Finance, out string SQLError)
        //{
        //    Hashtable InParameters = new Hashtable();
        //    InParameters.Add("@Flag", objBO_Finance.Flag);
        //    InParameters.Add("@LDGCODE", objBO_Finance.LDG_CODE);
        //    return cdo.GetDataTable("usp_GetLedgerMasterDetails", InParameters, out SQLError);
        //}
        public DataTable GetTransrecord(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@FLAG", objBO_Finance.Flag);
            InParameters.Add("@ldg_code", objBO_Finance.LDG_CODE);
            InParameters.Add("@fromDate", objBO_Finance.FDate);
            InParameters.Add("@Todate", objBO_Finance.TDate);
            return cdo.GetDataTable("Proc_GetTrans", InParameters, out SQLError);

        }
        public DataSet GetShowCCBACOpen(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Cust_ID", objBO_Finance.CUST_ID);
            return cdo.GetDataSet("proc_CCBAC_Open", InParameters);

        }



        /// <summary>
        /// Insert Update Delete Operation for Loan Scheme Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteLoanSchemeMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@SCHEME_CODE", objBO_Finance.SCHEME_CODE);
            InParameters.Add("@SCHEME_NAME", objBO_Finance.SCHEME_NAME);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            InParameters.Add("@LOAN_TERM", objBO_Finance.LOAN_TERM);
            InParameters.Add("@INST_APPL", objBO_Finance.INST_APPL);
            InParameters.Add("@REPAY_MODE", objBO_Finance.REPAY_MODE);
            InParameters.Add("@MON_PRD", objBO_Finance.MON_PRD);
            InParameters.Add("@SHR_AMNT", objBO_Finance.SHR_AMNT);
            InParameters.Add("@SUB_AMNT", objBO_Finance.SUB_AMNT);
            InParameters.Add("@ACT_CODE", objBO_Finance.ACT_CODE);
            InParameters.Add("@LOAN_TYPE", objBO_Finance.LOAN_TYPE);
            InParameters.Add("@ODPRIN_LDG_CODE", objBO_Finance.ODPRIN_LDG_CODE);
            InParameters.Add("@ODINT_LDG_CODE", objBO_Finance.ODINT_LDG_CODE);
            InParameters.Add("@ODINTR_LDG_CODE", objBO_Finance.ODINTR_LDG_CODE);
            InParameters.Add("@NPA_APP", objBO_Finance.NPA_APP);
            InParameters.Add("@OD_APP", objBO_Finance.OD_APP);
            InParameters.Add("@SANC_APP", objBO_Finance.SANC_APP);
            InParameters.Add("@SANC_PER", objBO_Finance.SANC_PER);
            return cdo.InsertUpdateDelete("usp_InsertUpdateLoanSchemeMaster", InParameters, out SQLError);
        }


        /// <summary>
        /// Insert Update Delete Operation for User Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteUserMasters(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@EmpCode", objBO_Finance.EmpCode);
            InParameters.Add("@UserName ", objBO_Finance.UserName);
            InParameters.Add("@UPassword", objBO_Finance.UPassword);
            InParameters.Add("@BranchID", objBO_Finance.BranchCode);
            InParameters.Add("@Department", objBO_Finance.DEPT);
            InParameters.Add("@Designation", objBO_Finance.DESIG);
            InParameters.Add("@Phone", objBO_Finance.PHONE);
            InParameters.Add("@Email", objBO_Finance.Email);
            InParameters.Add("@Validtill", objBO_Finance.ValidTill);
            InParameters.Add("@IsAdmin", objBO_Finance.IsAdmin);
            InParameters.Add("@Address", objBO_Finance.Address1);
            InParameters.Add("@Auth", objBO_Finance.Auth);
            InParameters.Add("@EduCode", objBO_Finance.EduCode);
           
            return cdo.InsertUpdateDelete("usp_InsertUpdateUserMaster", InParameters, out SQLError);
        }

        /// <summary>
        /// Insert Update Delete Operation for Deposit Scheme Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteDepositSchemeMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@DM_CODE", objBO_Finance.DM_CODE);
            InParameters.Add("@SCHEME", objBO_Finance.SCHEME);
            InParameters.Add("@INT_TYPE", objBO_Finance.INT_TYPE);
            InParameters.Add("@COMP_PRD", objBO_Finance.COMP_PRD);
            InParameters.Add("@LDG_CODE", objBO_Finance.LDG_CODE);
            InParameters.Add("@LAST_INTEREST_CALCULATION_DATE", objBO_Finance.LAST_INTEREST_CALCULATION_DATE);
            InParameters.Add("@NEXT_INTEREST_CALCULATION_DATE", objBO_Finance.NEXT_INTEREST_CALCULATION_DATE);
            InParameters.Add("@INT_CAL_DATE_STYLE", objBO_Finance.INT_CAL_DATE_STYLE);
            InParameters.Add("@SCHEME_TYPE", objBO_Finance.SCHEME_TYPE);
            InParameters.Add("@MINBAL_CASH", objBO_Finance.MINBAL_CASH);
            InParameters.Add("@MINBAL_CHQ", objBO_Finance.MINBAL_CHQ);
            InParameters.Add("@MAX_TRAN_PER_MONTH", objBO_Finance.MAX_TRAN_PER_MONTH);
            InParameters.Add("@CHARGES_MINBAL_FALL", objBO_Finance.CHARGES_MINBAL_FALL);
            InParameters.Add("@MIN_TRANPER_MONTH", objBO_Finance.MIN_TRANPER_MONTH);
            InParameters.Add("@INOPERATED_DAYS", objBO_Finance.INOPERATED_DAYS);
            InParameters.Add("@INOPERATED_MONTH", objBO_Finance.INOPERATED_MONTH);
            InParameters.Add("@INOPERATED_YR", objBO_Finance.INOPERATED_YR);
            InParameters.Add("@UNCLAIMED_DAYS", objBO_Finance.UNCLAIMED_DAYS);
            InParameters.Add("@UNCLAIMED_MONTH", objBO_Finance.UNCLAIMED_MONTH);
            InParameters.Add("@UNCLAIMED_YR", objBO_Finance.UNCLAIMED_YR);
            InParameters.Add("@AC_OPEN_AMNT", objBO_Finance.AC_OPEN_AMNT);
            InParameters.Add("@AC_CLOS_CHRG", objBO_Finance.AC_CLOS_CHRG);
            InParameters.Add("@MIN_CASH_DEP", objBO_Finance.MIN_CASH_DEP);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            InParameters.Add("@TERMINAL_ID", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@CHEQUE_FACILITY", objBO_Finance.CHEQUE_FACILITY);
            InParameters.Add("@INT_AMNT", objBO_Finance.INT_AMNT);
            InParameters.Add("@MIN_DAY", objBO_Finance.MIN_DAY);
            InParameters.Add("@MAX_WITH", objBO_Finance.MAX_WITH);
            InParameters.Add("@MAX_WITH_MON", objBO_Finance.MAX_WITH_MON);
            InParameters.Add("@MINDAYTR", objBO_Finance.MINDAYTR);
            return cdo.InsertUpdateDelete("usp_InsertUpdateDepositSchemeMaster", InParameters, out SQLError);
        }


        /// <summary>
        /// Insert Update Delete Operation for Employee Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteEmployeeMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@EMPCODE", objBO_Finance.EMPCODE);
            InParameters.Add("@NAME", objBO_Finance.Name);
            InParameters.Add("@ADDR", objBO_Finance.Address1);
            InParameters.Add("@PIN", objBO_Finance.PINCode);
            InParameters.Add("@PHONE", objBO_Finance.PHONE);
            InParameters.Add("@DEPT", objBO_Finance.DEPT);
            InParameters.Add("@DESIG", objBO_Finance.DESIG);
            InParameters.Add("@DOJ", objBO_Finance.DOJ);
            InParameters.Add("@FNAME", objBO_Finance.FNAME);
            InParameters.Add("@ARSANO", objBO_Finance.ARSANO);
            InParameters.Add("@EDU_QUF", objBO_Finance.EDU_QUF);
            InParameters.Add("@ARCS", objBO_Finance.ARCS);
            InParameters.Add("@DATE_OF_RETIR", objBO_Finance.DATE_OF_RETIR);
            InParameters.Add("@ARSADT", objBO_Finance.ARSADT);
            InParameters.Add("@DATE_OF_BIRTH", objBO_Finance.DOB);
            InParameters.Add("@REP_TO", objBO_Finance.REP_TO);
            InParameters.Add("@M_STATUS", objBO_Finance.M_STATUS);
            InParameters.Add("@DOB", objBO_Finance.DOB);
            InParameters.Add("@SEX", objBO_Finance.SEX);
            InParameters.Add("@TERMINAL_ID", objBO_Finance.TERMINAL_ID);
            InParameters.Add("@EMP_CODE", objBO_Finance.EmpCode);
            InParameters.Add("@PICTPATH", objBO_Finance.PictPath);
            InParameters.Add("@SIGNPATH", objBO_Finance.SignPath);
            return cdo.InsertUpdateDelete("usp_InsertUpdateEmployeeMaster", InParameters, out SQLError);
        }

        /// <summary>
        /// Get Employee Master Records
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetEmployeeMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@EmpCode", objBO_Finance.EmpCode);
            return cdo.GetDataTable("usp_GetEmployeeMasterRecords", InParameters, out SQLError);
        }
        public DataSet SearchInKyc(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@CUST_ID", objBO_Finance.CUST_ID);

            return cdo.GetDataSet("proc_SerchforKYC", InParameters);
        }

        public DataSet SearchInSHG(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@CUST_ID", objBO_Finance.CUST_ID);

            return cdo.GetDataSet("proc_SerchforSHG", InParameters);
        }

        public DataSet SearchInSHGName(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@NAME", objBO_Finance.NAME);

            return cdo.GetDataSet("proc_SerchforSHGName", InParameters);
        }



        public DataSet SearchforAccountGroupMaster(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@GROUP_CODE ", objBO_Finance.GROUP_CODE);

            return cdo.GetDataSet("proc_SearchforAccountGroupMaster", InParameters);
        }

        public DataSet SearchforAccountGroupMasterName(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@GROUP_NAME ", objBO_Finance.GROUP_NAME);

            return cdo.GetDataSet("proc_SearchforAccountGroupMasterName", InParameters);
        }

        public DataSet SearchinAccountLedger(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@LDG_CODE ", objBO_Finance.LDG_CODE);

            return cdo.GetDataSet("proc_SearchforAccountLedger", InParameters);
        }

        public DataSet SearchforAcNOMENCLATURE(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@NOMENCLATURE", objBO_Finance.NOMENCLATURE);

            return cdo.GetDataSet("proc_SearchforAcNOMENCLATURE", InParameters);
        }

        public DataSet SearchforKYCName(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@NAME", objBO_Finance.NAME);

            return cdo.GetDataSet("proc_SearchforKYCName", InParameters);
        }
        public DataSet get_Group_Item(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();


            return cdo.GetDataSet("proc_Group_Item", InParameters);
        }
        public DataSet ItemName_MCLASSWISE(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@MCLASS", objBO_Finance.MCLASS);

            return cdo.GetDataSet("proc_ItemName_MCLASSWISE", InParameters);
        }

        public DataSet GSTCALC_PRODUCTDETAILS(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@ItemCode", objBO_Finance.Item_Code);
            InParameters.Add("@Session", objBO_Finance.ASSES);

            return cdo.GetDataSet("proc_GSTCALC_PRODUCT", InParameters);
        }
        public int excelFileUpload(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Flag", objBO_Finance.Flag);
            InParameters.Add("@ACCOUNTNO", objBO_Finance.SL_CODE);
            InParameters.Add("@Balance", objBO_Finance.Balance);
            return cdo.InsertUpdateDelete("USP_UPDATEBALANCEUSINGEXCEL", InParameters, out SQLError);
        }
        public DataTable dailyscrollbydate(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@FLAG", objBO_Finance.Flag);
            InParameters.Add("@CCB_DATE", objBO_Finance.CCB_DATE);
            InParameters.Add("@branchid", objBO_Finance.BranchCode);
            return cdo.GetDataTable("proc_DailyScroll_AsOnDate", InParameters);
        }
        public DataTable getrecordbyslcode(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            return cdo.GetDataTable("proc_LoanSecurity_SLCODEtype", InParameters);
        }
        public DataTable LoanSecurityDepoDaily(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@Type", objBO_Finance.TYPE);
            return cdo.GetDataTable("proc_LoanSecirity_DepoDetails", InParameters);
        }
        public int insertLoanSecurityDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@LOAN_SEC_ID", objBO_Finance.LOAN_SEC_ID);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@K_NO", objBO_Finance.K_NO);
            InParameters.Add("@K_DATE", objBO_Finance.K_DATE);
            InParameters.Add("@K_ACRE", objBO_Finance.K_ACRE);
            InParameters.Add("@K_VALUE", objBO_Finance.K_VALUE);
            InParameters.Add("@K_VALID", objBO_Finance.K_VALID);
            InParameters.Add("@CREDIT_LIMIT_VALUE", objBO_Finance.CREDIT_LIMIT_VALUE);
            InParameters.Add("@K_SEES", objBO_Finance.K_SEES);




            return cdo.InsertUpdateDelete("proc_InsertLoan_Security", InParameters, out SQLError);
        }
        public int insertLoanSecurityLandBuilding(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@LOAN_SEC_ID", objBO_Finance.LOAN_SEC_ID);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            //InParameters.Add("@SUB_TYPE", objBO_Finance.SUB_TYPE);
            InParameters.Add("@MUJA_NO", objBO_Finance.MUJA_NO);
            InParameters.Add("@VALUE_OF_LAND", objBO_Finance.VALUE_OF_LAND);
            InParameters.Add("@GL_NO", objBO_Finance.GL_NO);
            InParameters.Add("@KH_NO", objBO_Finance.KH_NO);
            //InParameters.Add("@PLOT_NO", objBO_Finance.PLOT_NO);
            //InParameters.Add("@LAND_TYPE", objBO_Finance.LAND_TYPE);
            InParameters.Add("@TOTAL_LAND", objBO_Finance.TOTAL_LAND);
            InParameters.Add("@DAG_NO", objBO_Finance.DAG_NO);


            return cdo.InsertUpdateDelete("proc_InsertLoan_Security_LandBuilding", InParameters, out SQLError);
        }
        public int insertLoanSecurityLIC(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@LOAN_SEC_ID", objBO_Finance.LOAN_SEC_ID);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@POL_NO", objBO_Finance.POL_NO);
            InParameters.Add("@SUM_ASSU", objBO_Finance.SUM_ASSU);
            InParameters.Add("@SUM_VALUE", objBO_Finance.SUM_VALUE);
            InParameters.Add("@ASSINEE_DATE", objBO_Finance.ASSINEE_DATE);
            InParameters.Add("@ISS_OFF", objBO_Finance.ISS_OFF);


            return cdo.InsertUpdateDelete("proc_InsertLoan_Security_LIC", InParameters, out SQLError);
        }
        public int insertLoanSecurityHYPOTHICATION(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@LOAN_SEC_ID", objBO_Finance.LOAN_SEC_ID);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@PERTI", objBO_Finance.PERTI);
            InParameters.Add("@VEH_NO", objBO_Finance.VEH_NO);
            InParameters.Add("@MODEL_NO", objBO_Finance.MODEL_NO);
            InParameters.Add("@CHASE_NO", objBO_Finance.CHASE_NO);
            InParameters.Add("@VALUE_OF_HYP", objBO_Finance.VALUE_OF_HYP);

            return cdo.InsertUpdateDelete("proc_InsertLoan_Security_HYPOTHICATION", InParameters, out SQLError);

        }

        public int insertLoanSecurityKVP(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@LOAN_SEC_ID", objBO_Finance.LOAN_SEC_ID);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@Sec_Type", objBO_Finance.Sec_Type);
            InParameters.Add("@CERT_NO", objBO_Finance.CERT_NO);
            InParameters.Add("@ISS_DT", objBO_Finance.ISS_DT);
            InParameters.Add("@ISS_OFF", objBO_Finance.ISS_OFF);
            InParameters.Add("@FACE_VAL", objBO_Finance.FACE_VAL);
            InParameters.Add("@MAT_VAL", objBO_Finance.MAT_VAL);
            InParameters.Add("@MAT_DT", objBO_Finance.MAT_DT);
            InParameters.Add("@PLEDG_DATE", objBO_Finance.PLEDG_DATE);
            InParameters.Add("@REMARKS", objBO_Finance.REMARKS);

            return cdo.InsertUpdateDelete("proc_InsertLoan_Security_KVP", InParameters, out SQLError);

        }
        public int insertLoanSecurityDC(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@LOAN_SEC_ID", objBO_Finance.LOAN_SEC_ID);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@DC_ACC_NO", objBO_Finance.DC_ACC_NO);
            InParameters.Add("@DEP_AMT", objBO_Finance.DEP_AMT);

            InParameters.Add("@MAT_DT", objBO_Finance.MAT_DT);

            return cdo.InsertUpdateDelete("proc_InsertLoan_Security_DC", InParameters, out SQLError);

        }

        public int insertLoanSecurityFD(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@LOAN_SEC_ID", objBO_Finance.LOAN_SEC_ID);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@FD_ACC_NO", objBO_Finance.FD_ACC_NO);
            InParameters.Add("@DEP_AMT", objBO_Finance.DEP_AMT);

            InParameters.Add("@MAT_DT", objBO_Finance.MAT_DT);
            return cdo.InsertUpdateDelete("proc_InsertLoan_Security_FD", InParameters, out SQLError);

        }



        public int insertLoanSecurityRD(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@LOAN_SEC_ID", objBO_Finance.LOAN_SEC_ID);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@RD_ACC_NO", objBO_Finance.RD_ACC_NO);
            InParameters.Add("@DEP_AMT", objBO_Finance.DEP_AMT);

            InParameters.Add("@MAT_DT", objBO_Finance.MAT_DT);

            return cdo.InsertUpdateDelete("proc_InsertLoan_Security_RD", InParameters, out SQLError);

        }

        public int insertLoanSecurityMIS(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@LOAN_SEC_ID", objBO_Finance.LOAN_SEC_ID);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@MIS_ACC_NO", objBO_Finance.MIS_ACC_NO);
            InParameters.Add("@DEP_AMT", objBO_Finance.DEP_AMT);

            InParameters.Add("@MAT_DT", objBO_Finance.MAT_DT);

            return cdo.InsertUpdateDelete("proc_InsertLoan_Security_MIS", InParameters, out SQLError);

        }

        public int insertLoanSecurityDAILY(BO_Finance objBO_Finance, out string SQLError)
        {
            Hashtable InParameters = new Hashtable();
            //InParameters.Add("@LOAN_SEC_ID", objBO_Finance.LOAN_SEC_ID);
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            InParameters.Add("@TYPE", objBO_Finance.TYPE);
            InParameters.Add("@DAILY_ACC_NO", objBO_Finance.DAILY_ACC_NO);
            InParameters.Add("@DEP_AMT", objBO_Finance.DEP_AMT);

            InParameters.Add("@MAT_DT", objBO_Finance.MAT_DT);

            return cdo.InsertUpdateDelete("proc_InsertLoan_Security_DAILY", InParameters, out SQLError);

        }

        public int BalancedateConfiguration(BO_Finance objBO_Finance)
        {
            SqlParameter[] p = new SqlParameter[5];

            p[0] = new SqlParameter("@Cash_In_Hand_Date", objBO_Finance.Cash_In_Hand_Date);
            p[1] = new SqlParameter("@Deposite_Balance_Date", objBO_Finance.Deposite_Balance_Date);
            p[2] = new SqlParameter("@General_Ledger_Date", objBO_Finance.General_Ledger_Date);
            p[3] = new SqlParameter("@Final_Account_Date", objBO_Finance.Final_Account_Date);
            p[4] = new SqlParameter("@Loan_Account_Date", objBO_Finance.Loan_Account_Date);

            int i = DataLayer.GetInstance().ExecuteNonQuery("proc_Date_config", p);
            return i;

        }

        public DataTable BalanceDateConfiguration(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();

            return cdo.GetDataTable("proc_Date_Configuration", InParameters);
        }

        public DataTable LoanCollectionForm(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@SCHEME_CODE", objBO_Finance.SCHEME_CODE);
            InParameters.Add("@FDate", objBO_Finance.FDate);
            InParameters.Add("@TDate", objBO_Finance.TDate);

            return cdo.GetDataTable("PROC_LOANCOLLECTION_REPORT", InParameters);
        }

        public DataTable LoanCollectionFormbySchemeCode(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();

            return cdo.GetDataTable("proc_LoanCollectbySchemeCode", InParameters);
        }
        public DataTable LoanSecurityKVPNSCReport(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            return cdo.GetDataTable("PROC_LOANSECURITY_KVPNSCLIC", InParameters);
        }
        public DataTable LoanSecurityKarbaReport(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();
            InParameters.Add("@SL_CODE", objBO_Finance.SL_CODE);
            return cdo.GetDataTable("proc_LoanSecurity_DetailsReport", InParameters);
        }
        public DataTable LoanSecurityKarbaSlCode(BO_Finance objBO_Finance)
        {
            Hashtable InParameters = new Hashtable();

            return cdo.GetDataTable("proc_LoanSecurity_KarbaSLCODE", InParameters);
        }
    }
}
