using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using BusinessObject;
namespace BLL
{
    public class BL_Finance
    {
        DataFinanceLayer objDL_Finance = new DataFinanceLayer();

        /// <summary>
        /// Get Module and Page Name By Individual User
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetModuleAndPageByUser(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetModuleAndPageByUser(objBO_Finance, out SQLError);
        }
        public DataSet GetSlcodes(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetSlCode(objBO_Finance, out SQLError);
        }

        public DataSet GetnterestCalc(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetInterestcal(objBO_Finance, out SQLError);
        }

        public DataSet GetMaturityAmt(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetMaturityAmt(objBO_Finance, out SQLError);
        }

        public DataTable GetBranchDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetBranchDetails(objBO_Finance, out SQLError);
        }

        public DataTable ValidateUser(BO_Finance objBO_Finance)
        {
            return objDL_Finance.ValidateUser(objBO_Finance);
        }

        public DataSet GetInvoiceDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetInvoiceDetails(objBO_Finance, out SQLError);
        }


        public DataSet GetFAReportData(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetFAReportData(objBO_Finance, out SQLError);
        }

        public DataTable StockDetailsReport(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.StockDetailsReport(objBO_Finance, out SQLError);
        }

        public DataTable GetLoanAccountDetailLoans(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetLoanAccountDetailLoan(objBO_Finance, out SQLError);
        }

        public DataTable GetLoanDueDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetLoanDueDetailsLoan(objBO_Finance, out SQLError);
        }

        public DataSet GetLoanAccountDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetLoanAccountDetails(objBO_Finance, out SQLError);
        }

        public DataSet GenVoucherNo(BO_Finance objBO_Finance)
        {
            return objDL_Finance.GenerateVoucherNo(objBO_Finance);
        }

        public DataSet GetAccountBalance(BO_Finance objBO_Finance , out string SQLError)
        {
            return objDL_Finance.GetAccountBalance(objBO_Finance , out SQLError);
        }
        public DataSet GetRepaymentDisbDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.RepaymentDisbDetails(objBO_Finance,out SQLError);
        }

        public DataSet GETMISDETAILS(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GETMISDETAILS(objBO_Finance, out SQLError);
        }

        public DataSet GetLoanDetailsList(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetLoanDetailsList(objBO_Finance, out SQLError);
        }

        //public DataTable TrailBalanceReport(BO_Finance objBO_Finance, out string SQLError)
        //{
        //    //return objDL_Finance.TrailBalanceReport(objBO_Finance, out SQLError);
        //    return objDL_Finance.TrailBalanceReport(objBO_Finance);
        //}
        public DataTable TrailBalanceReportDev(BO_Finance objBO_Finance)
        {
            //return objDL_Finance.TrailBalanceReport(objBO_Finance, out SQLError);
            return objDL_Finance.TrailBalanceReport(objBO_Finance);
        }
        public DataTable GetPLReportData(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetFAReportDataPL(objBO_Finance, out SQLError);
        }

        public DataTable GetTRReportData(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetFAReportDataTR(objBO_Finance, out SQLError);
        }

        public DataTable FinalAccountsDetailsSettingsPL(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.FinalAccountSettingsProfitLoss(objBO_Finance, out SQLError);
        }

        public DataTable FinalAccountsDetailsSettingsBS(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.FinalAccountSettingsBalacesheet(objBO_Finance, out SQLError);
        }

        public DataTable FinalAccountsDetailsSettingsTR(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.FinalAccountSettingsTrading(objBO_Finance, out SQLError);
        }

        public DataTable GetBSReportData(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetFAReportDataBS(objBO_Finance, out SQLError);
        }

        public DataTable TrailBalanceReportAssets(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.TrailBalanceReportAssets(objBO_Finance, out SQLError);
        }

        public DataTable GetYearStartEndDt(BO_Finance objBO_Finance)
        {
            return objDL_Finance.YearStartEndDt(objBO_Finance);
        }




        public int InsertUpdateDeleteLoanRepayment(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteLoanRepayment(objBO_Finance, out SQLError);
        }

        public int  DeleteLoanRepaymentDisbursement(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.DeleteLoanRepaymentDisbur(objBO_Finance, out SQLError);
        }

        public DataTable GetAllTransDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetAllTransDetails(objBO_Finance, out SQLError);
        }

        public DataTable GETALLVOUCHER(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GETALLVOUCHER(objBO_Finance, out SQLError);
        }

        public DataSet GeneralLedgerReport(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GeneralLedgerReport(objBO_Finance, out SQLError);
        }

        public DataTable CashAccountReport(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.CashAccountReport(objBO_Finance, out SQLError);
        }

        //public DataSet CashAccountReport(BO_Finance objBO_Finance, out string SQLError)
        //{
        //    return objDL_Finance.CashAccountReport(objBO_Finance, out SQLError);
        //}

        public DataTable DashboardReport(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.DashboardReport(objBO_Finance, out SQLError);
        }

        public DataTable GetInstrumentNoLoanRepay(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.FindInstrumentNoLoanRepay(objBO_Finance, out SQLError);
        }

        public DataTable DetailsDepositReports(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.DetailsDepositReports(objBO_Finance, out SQLError);
        }
        public DataTable SavingsDetailsDepositReports(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.SavingsDetailsListReport(objBO_Finance, out SQLError);
        }

        public DataTable HomeSavingsDetailsDepositReports(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.HomeSavingsDetailsListReport(objBO_Finance, out SQLError);
        }

        public DataTable ShareDividendCalculation(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.ShareDividendCalculation(objBO_Finance, out SQLError);
        }

        public DataSet ShareDetailReport(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.ShareDetailReport(objBO_Finance, out SQLError);
        }

        public DataSet BRSDetailsReport(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.BRSDetailsReport(objBO_Finance, out SQLError);
        }

        public DataSet GetNEFTGrid(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetNEFTGrid(objBO_Finance, out SQLError);
        }

        public DataSet GetIFSCCODE(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetIFSCCODE(objBO_Finance, out SQLError);
        }

        public DataSet GenerateReportForFarm(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GenerateReportForFarm(objBO_Finance, out SQLError);
        }

        public DataTable GetSecurityDtl(BO_Finance objBO_Finance)
        {
            return objDL_Finance.GetSecurityDetails(objBO_Finance);
        }
        public DataTable SHGDetailsDepositReports(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.SHGDetailsListReport(objBO_Finance, out SQLError);
        }
        public DataTable LoanDetailListReports(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.LoanListDetailsReport(objBO_Finance, out SQLError);
        }




        public DataTable FixedDepositDetailsReports(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.FixedDepositsDetailsListReport(objBO_Finance, out SQLError);
        }
        public DataTable MisDetailsListReports(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.MisDetailsListDepositReports(objBO_Finance, out SQLError);
        }


        public DataTable DepositCirtificateDetailsReports(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.DepositeCertificateListReport(objBO_Finance, out SQLError);
        }
        public DataTable RecurringDepositeDetailsReports(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.RecurringDepositListReport(objBO_Finance, out SQLError);
        }

        public DataTable FinalAccountsDetailsSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.FinalAccountSettings(objBO_Finance, out SQLError);
        }


        public int InsertUpdateFinalAcctSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateFinalAcctSettings(objBO_Finance, out SQLError);
        }

        public DataSet DayBookReport(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.DayBookReport(objBO_Finance, out SQLError);
        }

        public DataSet GetUploadFileusingAcNo(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetUploadFileusingAcNo(objBO_Finance, out SQLError);
        }

        public DataSet GETBALANCEBYACNO(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GETBALANCEBYACNO(objBO_Finance, out SQLError);
        }

        public DataSet CashBookReport(BO_Finance objBO_Finance , out string SQLError)
        {
            return objDL_Finance.CashBookReport(objBO_Finance, out SQLError);
        }

        public DataTable GetLinkedAcNo(int flag, string sl_code, out string SQLError)
        {
            return objDL_Finance.GetLinkedAcNo(flag, sl_code, out SQLError);
        }

        public DataSet AcctStatementReport(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.AcctStatementReport(objBO_Finance, out SQLError);
        }

        public DataSet ActStatementReport (BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.StatementReport(objBO_Finance, out SQLError);
        }
        public DataSet getSlcodebalance(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetSlcodebalance(objBO_Finance, out SQLError);
        }
        //public DataTable GetAllAcctNo(object flag, string text)
        public DataTable GetAllAcctNo(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetAllAcctNo(objBO_Finance, out SQLError);
        }

        public DataTable GetClientLists(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetClientList(objBO_Finance, out SQLError);
        }

        public int getBalanceUpdate(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetbalanceUpdate(objBO_Finance, out SQLError);
        }


        public DataSet GetAccountHoldersDetailsBySL_CODE(object flag, string SL_CODE, string BRANCHID , DateTime? EntryDate, out string SQLError)
        {
            return objDL_Finance.GetAccountHoldersDetailsBySL_CODE(flag, SL_CODE, BRANCHID , EntryDate, out SQLError);
        }

        public DataSet GetAccountHoldersDetailsBySL_CODEOLD (object flag , string OLD_ACNO , string BRANCHID, DateTime? EntryDate, out string SQLError)
        {
            return objDL_Finance.GetAccountHoldersDetailsBySL_CODEOLD(flag, OLD_ACNO, BRANCHID , EntryDate, out SQLError);
        }

        public DataSet GetAccountDetailsForNEFT (object flag , string SL_CODE , out string SQLError)
        {
            return objDL_Finance.GetAccountDetailsForNEFT(flag, SL_CODE, out SQLError);
        }

        public DataSet GetBranchNameByBranchCode(object flag , string  BRANCHID , out string SQLError)
        {
            return objDL_Finance.GetBranchNameByBranchCode(flag, BRANCHID, out SQLError);
        }

        public DataSet GetAcNo(object flag, string NAME, out string SQLError)
        {
            return objDL_Finance.GetAcNo(flag, NAME, out SQLError);
        }

        public DataSet MatchBranchCode(object flag , string SL_CODE , string BRANCHID , out string SQLError)
        {
            return objDL_Finance.MatchBranchCode(flag, SL_CODE , BRANCHID, out SQLError);
        }
        public DataSet GetInvestmentAccountHoldersDetailsBySL_CODE (object flag , string SL_CODE , DateTime? EntryDate , out string SQLError)
        {
            return objDL_Finance.GetInvestmentAccountHoldersDetailsBySL_CODE(flag, SL_CODE, EntryDate, out SQLError);
        }
        public int InsertUpdate_Denomination(BO_Finance objBO_Finance)
        {
            return objDL_Finance.InsUpDenomination(objBO_Finance);
        }

        public DataSet GetLoanAcctDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetLoanAcctDetails(objBO_Finance, out SQLError);
        }
        public DataTable chequeslflag(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.chequeslflag(objBO_Finance, out SQLError);
        }

        public int InsertUpdateLoanDisbursement(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateLoanDisbursement(objBO_Finance, out SQLError);
        }
        public DataTable searchclient(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.searchclient(objBO_Finance, out SQLError);
        }
        public int ModifyTransaction(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.ModifyTransaction(objBO_Finance, out SQLError);
        }

        public int ModifyTransactionNew(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.ModifyTransactionNew(objBO_Finance, out SQLError);
        }

        public int BankLDGBalanceUpdate(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.BankLDGBalanceUpdate(objBO_Finance, out SQLError);
        }

        public int SocietyVoucherUpdate(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.SocietyVoucherUpdate(objBO_Finance, out SQLError);
        }

        public int insertupdatemisinterestdetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertupdatemisinterestdetails(objBO_Finance, out SQLError);
        }

        public int datFILEUPLOAD(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.datFILEUPLOAD(objBO_Finance, out SQLError);
        }

        public int MIDINTDT(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.MIDINTDT(objBO_Finance, out SQLError);
        }

        public int INSERTCASHIER(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.INSERTCASHIER(objBO_Finance, out SQLError);
        }

        public int updateNEFTRTGS(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.updateNEFTRTGS(objBO_Finance, out SQLError);
        }


        public int ModifyTransactionComments(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.UpdateTransactionComments(objBO_Finance, out SQLError);
        }



        public DataSet getVoucherNo(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetAllVoucherNo(objBO_Finance, out SQLError);
        }
        /// <summary>
        /// Insert Update Delete Operation for KYC Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteKYCMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteKYCMaster(objBO_Finance, out SQLError);
        }

        public int InsertUpdateNEFTRTGS(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateNEFTRTGS(objBO_Finance, out SQLError);
        }

        public int InsertFileData(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertFileData(objBO_Finance, out SQLError);
        }

        public DataTable GetAllKCCList(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetAllKCCListDetails(objBO_Finance, out SQLError);
        }



        public int InsertUpdateDeleteInvestmentOpening(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteInvestmentOpening(objBO_Finance, out SQLError);
        }

        public int InsertUpdateDeleteItemMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteItemMaster(objBO_Finance, out SQLError);
        }

        public int InsertUpdateDeleteSubLedgerMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteSubLedgerMaster(objBO_Finance, out SQLError);
        }
        public int InsertSubLedgerMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertSubLedgerMasters(objBO_Finance, out SQLError);
        }


        public int InsertUpdateDeleteJournalBook(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteJournalBook(objBO_Finance, out SQLError);
        }

        public DataTable GetItemMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetItemMasterRecords(objBO_Finance, out SQLError);
        }

        public DataSet GetAccountNo(object flag, string SL_CODE, out string SQLError)
        {
            return objDL_Finance.GetAccountNo(flag, SL_CODE, out SQLError);
        }

        public DataTable GetAccountNos(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetAccountNos(objBO_Finance, out SQLError);
        }

        //public DataSet GetAccountHoldersDetailsBySL_CODEOLD(object flag, string OLD_ACNO, DateTime? EntryDate, out string SQLError)
        //{
        //    return objDL_Finance.GetAccountHoldersDetailsBySL_CODEOLD(flag, OLD_ACNO, EntryDate, out SQLError);
        //}


        public DataTable SubLedgerDetailsReports(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.SubLedgerDetailsReports(objBO_Finance, out SQLError);
        }

        public DataTable GetSubLedgerMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetSubLedgerMasterRecords(objBO_Finance, out SQLError);
        }
        public DataTable GetLedgerDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetLedgerDtl(objBO_Finance, out SQLError);
        }

        public int InsertUpdateDeleteAccountClosing(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteAccountClosing(objBO_Finance, out SQLError);
        }

        public int ShareAccountClosing(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.ShareAccountClosing(objBO_Finance, out SQLError);
        }

        public int InvestmentAccountClosing(BO_Finance objBO_Finance , out string SQLError)
        {
            return objDL_Finance.InvestementAccountClosing(objBO_Finance, out SQLError);
        }
        public int InsertUpdateDeleteCashierCounterBook(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteCashierCounterBook(objBO_Finance, out SQLError);
        }

        public int InsertUpdateDeleteCashPayOthers(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteCashPayOthers(objBO_Finance, out SQLError);
        }
        /// <summary>
        /// Get KYC Details Individual User
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetKYCDetailsRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetKYCDetailsRecords(objBO_Finance, out SQLError);
        }

        public DataTable GetJournalDeta(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetJournalDeta(objBO_Finance, out SQLError);
        }

        public DataTable GetJournalDetabySL_CODE(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetJournalDetabySL_CODE(objBO_Finance, out SQLError);
        }

        //kyc List
        public DataTable GetKYCDetailsRecordListrec(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetKYCDetailsRecordsList(objBO_Finance, out SQLError);
        }

        public DataTable GetLoanSchemeMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetLoanSchemeMasterRecords(objBO_Finance, out SQLError);
        }

        public DataTable GetBranch(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetBranch(objBO_Finance, out SQLError);
        }

        public DataTable GetBank(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetBank(objBO_Finance, out SQLError);
        }

        public DataTable GetBankBranch(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetBankBranch(objBO_Finance, out SQLError);
        }
        public DataTable GetBankBranchBYIFSC(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetBankBranchBYIFSC(objBO_Finance, out SQLError);
        }
        public DataTable GetBankBYIFSC(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetBankBYIFSC(objBO_Finance, out SQLError);
        }

        public DataTable GetItemDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetItemDetails(objBO_Finance, out SQLError);
        }

        public DataTable GetTotalDepositSHG (BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetTotalDepositSHG(objBO_Finance, out SQLError);
        }
        public DataTable GetsubLedgerDetailsLoan(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetsubLedgerDetailsLoans(objBO_Finance, out SQLError);
        }
        public DataTable GetCashBookLedger(BO_Finance objBO_Finance)
        {
            return objDL_Finance.GetCashBookLedgerName(objBO_Finance);
        }


        public DataTable GetUserMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetUserMasterRecord(objBO_Finance, out SQLError);
        }

        public DataTable chequeUser(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.chequeUser(objBO_Finance, out SQLError);
        }

        public DataTable GetRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetRecords(objBO_Finance, out SQLError);
        }

        public DataSet GetInvestmentAccountDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetInvestmentAccountDetails(objBO_Finance, out SQLError);
        }
        public DataSet CheckLogindetails(BO_Finance objBO_Finance)
        {
            return objDL_Finance.Check_Logindetails(objBO_Finance);
        }

        public int DeletebyUserId(BO_Finance objBO_Finance)
        {
            return objDL_Finance.DeletebyUserId(objBO_Finance);
        }
        public int Stock_Updateform(BO_Finance objBO_Finance)
        {
            return objDL_Finance.Stock_Updateform(objBO_Finance);
        }

        public DataSet getStockDetails(BO_Finance objBO_Finance)
        {
            return objDL_Finance.getStockDetails(objBO_Finance);
        }



        /// <summary>
        /// Get Nominee Details Individual User
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetNomineeDetailsBySLCode(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetNomineeDetailsBySLCode(objBO_Finance, out SQLError);
        }

        /// <summary>
        /// Get Auth Details Individual User
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetAuthSignBySLCode(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetAuthSignBySLCode(objBO_Finance, out SQLError);
        }

        public DataSet GetSHGClientRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetSHGClientRecords(objBO_Finance, out SQLError);
        }


        public DataSet GetSavingsInterest (BO_Finance objBO_Finance , out string SQLError)
        {
            return objDL_Finance.GetSavingsInterest(objBO_Finance, out SQLError);
        }
        /// <summary>
        /// Insert Update Delete Operation for Group Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteGroupMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteGroupMaster(objBO_Finance, out SQLError);
        }

        /// <summary>
        ///Get Deposit Scheme Details
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetDepositMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetDepositMasterRecords(objBO_Finance, out SQLError);
        }

        public DataTable GetEmpName (BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetEmpName(objBO_Finance, out SQLError);
        }

        public DataTable GetDepositMasterRecordsForSHG (BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetDepositMasterRecordsForSHG(objBO_Finance, out SQLError);
        }
        /// <summary>
        /// Get Group Master Records
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetGroupMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetGroupMasterRecords(objBO_Finance, out SQLError);
        }

        public DataTable GetLEDGERMasterRecords (BO_Finance objBO_Finance , out string SQLError)
        {
            return objDL_Finance.GetLEDGERMasterRecords (objBO_Finance, out SQLError);
        }

        public DataTable GetInterestPaybleRecords (BO_Finance objBO_Finance , out string SQLError)
        {
            return objDL_Finance.GetInterestPaybleRecords(objBO_Finance, out SQLError);
        }
        public DataTable GetnterestIssued(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetnterestIssuedRecords(objBO_Finance, out SQLError);
        }

        public DataTable GetnterestReceived(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetnterestReceiableRecords(objBO_Finance, out SQLError);
        }


        

        /// <summary>
        /// Insert Update Delete Operation for Ledger Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteLedgerMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteLedgerMaster(objBO_Finance, out SQLError);
        }

        /// <summary>
        /// Get Ledger Master Records
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetLedgerMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetLedgerMasterRecords(objBO_Finance, out SQLError);
        }

        //public DataTable GetSubLedgerRecords(BO_Finance objBO_Finance , out string SQLError)
        //{
        //    return objDL_Finance.GetSubLedgerRecords(objBO_Finance, out SQLError);
        //}
        public DataTable GetTrans(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetTransrecord(objBO_Finance, out SQLError);

        }
        public DataSet showCCBACOpen(BO_Finance objBO_Finance)
        {
            return objDL_Finance.GetShowCCBACOpen(objBO_Finance);

        }



        public int InsertUpdateDeleteLoanAccountOpening(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteLoanAccountOpening(objBO_Finance, out SQLError);
        }

        /// <summary>
        /// Insert Update Delete Operation for Loan Scheme Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteLoanSchemeMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteLoanSchemeMaster(objBO_Finance, out SQLError);
        }

        public int InsertUpdateDeleteUserMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteUserMasters(objBO_Finance, out SQLError);
        }

        public int InsertUpdateDeleteSTockTrans(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteStockTrans(objBO_Finance, out SQLError);
        }

        public double GetOPCLBAL(int Flag, string SL_CODE, DateTime? AsOnDate, out string SQLError)
        {
            return objDL_Finance.GetOPCLBAL(Flag, SL_CODE, AsOnDate, out SQLError);
        }

        public double GetMaturityBal (int Flag , string SL_CODE , DateTime? AsOnDate , out string SQLError)
        {
            return objDL_Finance.GetMaturityBal(Flag, SL_CODE, AsOnDate, out SQLError);
        }

        /// <summary>
        /// Insert Update Delete Operation for Deposit Scheme Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteDepositSchemeMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteDepositSchemeMaster(objBO_Finance, out SQLError);
        }


        /// <summary>
        /// Insert Update Delete Operation for Employee Master
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public int InsertUpdateDeleteEmployeeMaster(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteEmployeeMaster(objBO_Finance, out SQLError);
        }

        /// <summary>
        /// Get Employee Master Records
        /// </summary>
        /// <param name="objBO_Finance">Property Value Required: Flag</param>
        /// <param name="SQLError">Get SQL Exception if any</param>
        public DataTable GetEmployeeMasterRecords(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetEmployeeMasterRecords(objBO_Finance, out SQLError);
        }

        public int InsertUpdateDeleteAccountOpening(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteAccountOpening(objBO_Finance, out SQLError);
        }

        public int InsertUpdateShareAccountOpening(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateShareAccountOpening(objBO_Finance, out SQLError);
        }

        public int InsertUpdateFinalAccountSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateFinalAccountSettings(objBO_Finance, out SQLError);
        }

        public int UpdateLoanAcctsBalance(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.UpdateLoanAcctsBal(objBO_Finance, out SQLError);
        }
        public DataSet LoanbalUpdate(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.loanBalanceUpdate(objBO_Finance, out SQLError);
        }


        public int InsertUpdateDeleteAccountSHG(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateDeleteAccountSHG(objBO_Finance, out SQLError);
        }
        public int UpdateSHGAccount(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.UpdateAccountSHG(objBO_Finance, out SQLError);
        }

        public DataSet GetInitialSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetInitialSettings(objBO_Finance, out SQLError);
        }

        public DataTable GetPassbookTransaction(BO_Finance objBO_Finance)
        {
            return objDL_Finance.GetPassbookTrans(objBO_Finance);
        }
        //public DataSet GetPassbookTransaction(BO_Finance objBO_Finance)
        //{
        //    return objDL_Finance.GetPassbookTrans(objBO_Finance);
        //}
        public int InsertUpdateInitialSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertUpdateInitialSettings(objBO_Finance, out SQLError);
        }
        public int InsertInitialSettings(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertInitialSettings(objBO_Finance, out SQLError);
        }
        public int InsertDepositReturnTable(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertDepositReturnTable(objBO_Finance, out SQLError);
        }
        public DataSet GETDEPOSITRETURN(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GETDEPOSITRETURN(objBO_Finance, out SQLError);
        }
        public int InsertYearTradingBalance(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InsertYearTradingBalance(objBO_Finance, out SQLError);
        }

        public DataSet GetAccountsDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetAccountsDetails(objBO_Finance, out SQLError);
        }
        //public DataSet GetSancByRecords(BO_Finance objBO_Finance, out string SQLError)
        //{
        //    return objDL_Finance.GetSancByRecords(objBO_Finance, out SQLError);
        //}
        public DataSet journalaccholder(BO_Finance objBO_Finance)
        {
            return objDL_Finance.Getjournalaccholder(objBO_Finance);
        }

        public DataSet GetInvestementAccountsDetail(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.InvestementAccountDetails(objBO_Finance, out SQLError);
        }

        public DataSet GetLoanAccountsDetail(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetLoanAccountsDetail(objBO_Finance, out SQLError);
        }

        public DataSet DashboardReportfirst(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.DashboardReportfirst(objBO_Finance, out SQLError);
        }

        public DataSet DashboardReportSecond(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.DashboardReportSecond(objBO_Finance, out SQLError);
        }

        public DataSet GetAccountDetailsShg(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetSHGAccountDetails(objBO_Finance, out SQLError);
        }

        public DataSet GetAccountListShg (BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.GetAccountListShg(objBO_Finance, out SQLError);
        }

        public DataSet SearchInKyc(BO_Finance objBO_Finance)
        {
            return objDL_Finance.SearchInKyc(objBO_Finance);
        }

        public DataSet SearchInSHG(BO_Finance objBO_Finance)
        {
            return objDL_Finance.SearchInSHG(objBO_Finance);
        }

        public DataSet SearchInSHGName(BO_Finance objBO_Finance)
        {
            return objDL_Finance.SearchInSHGName(objBO_Finance);
        }

        public DataSet SearchforAccountGroupMaster(BO_Finance objBO_Finance)
        {
            return objDL_Finance.SearchforAccountGroupMaster(objBO_Finance);
        }

        public DataSet SearchforAccountGroupMasterName(BO_Finance objBO_Finance)
        {
            return objDL_Finance.SearchforAccountGroupMasterName(objBO_Finance);
        }


        public DataSet SearchinAccountLedger(BO_Finance objBO_Finance)
        {
            return objDL_Finance.SearchinAccountLedger(objBO_Finance);
        }

        public DataSet SearchforAcNOMENCLATURE(BO_Finance objBO_Finance)
        {
            return objDL_Finance.SearchforAcNOMENCLATURE(objBO_Finance);
        }

        public DataSet SearchforKYCName(BO_Finance objBO_Finance)
        {
            return objDL_Finance.SearchforKYCName(objBO_Finance);
        }
        public DataSet get_Group_Item(BO_Finance objBO_Finance)
        {
            return objDL_Finance.get_Group_Item(objBO_Finance);
        }
        public DataSet ItemName_MCLASSWISE(BO_Finance objBO_Finance)
        {
            return objDL_Finance.ItemName_MCLASSWISE(objBO_Finance);
        }
        public DataSet GSTCALC_PRODUCTDETAILS(BO_Finance objBO_Finance)
        {
            return objDL_Finance.GSTCALC_PRODUCTDETAILS(objBO_Finance);
        }
        public int excelFileUpload(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.excelFileUpload(objBO_Finance, out SQLError);
        }
        public DataTable dailyscrollbydate(BO_Finance objBO_Finance)
        {
            return objDL_Finance.dailyscrollbydate(objBO_Finance);
        }
        public DataTable getrecordbyslcode(BO_Finance objBO_Finance)
        {
            return objDL_Finance.getrecordbyslcode(objBO_Finance);
        }
        public DataTable LoanSecurityDepoDaily(BO_Finance objBO_Finance)
        {
            return objDL_Finance.LoanSecurityDepoDaily(objBO_Finance);
        }
        public int insertLoanSecurityLIC(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertLoanSecurityLIC(objBO_Finance, out SQLError);
        }
        public int insertLoanSecurityHYPOTHICATION(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertLoanSecurityHYPOTHICATION(objBO_Finance, out SQLError);
        }
        //insertLoanSecurityKVP
        public int insertLoanSecurityKVP(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertLoanSecurityKVP(objBO_Finance, out SQLError);
        }
        public int insertLoanSecurityFD(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertLoanSecurityFD(objBO_Finance, out SQLError);
        }
        public int insertLoanSecurityDetails(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertLoanSecurityDetails(objBO_Finance, out SQLError);
        }
        public int insertLoanSecurityLandBuilding(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertLoanSecurityLandBuilding(objBO_Finance, out SQLError);
        }
        public int insertLoanSecurityDC(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertLoanSecurityDC(objBO_Finance, out SQLError);
        }
        public int insertLoanSecurityRD(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertLoanSecurityRD(objBO_Finance, out SQLError);
        }
        public int insertLoanSecurityMIS(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertLoanSecurityMIS(objBO_Finance, out SQLError);
        }
        public int insertLoanSecurityDAILY(BO_Finance objBO_Finance, out string SQLError)
        {
            return objDL_Finance.insertLoanSecurityDAILY(objBO_Finance, out SQLError);
        }
        public DataTable BalanceDateConfiguration(BO_Finance objBO_Finance)
        {
            return objDL_Finance.BalanceDateConfiguration(objBO_Finance);
        }

        public int getBalancedateConfiguration(BO_Finance objBO_Finance)
        {
            return objDL_Finance.BalancedateConfiguration(objBO_Finance);
        }

        public DataTable LoanCollectionForm(BO_Finance objBO_Finance)
        {
            return objDL_Finance.LoanCollectionForm(objBO_Finance);
        }


        public DataTable LoanCollectionFormbySchemeCode(BO_Finance objBO_Finance)
        {
            return objDL_Finance.LoanCollectionFormbySchemeCode(objBO_Finance);
        }
        public DataTable LoanSecurityKarbaSlCode(BO_Finance objBO_Finance)
        {
            return objDL_Finance.LoanSecurityKarbaSlCode(objBO_Finance);
        }

        public DataTable LoanSecurityKarbaReport(BO_Finance objBO_Finance)
        {
            return objDL_Finance.LoanSecurityKarbaReport(objBO_Finance);
        }
        public DataTable LoanSecurityKVPNSCReport(BO_Finance objBO_Finance)
        {
            return objDL_Finance.LoanSecurityKVPNSCReport(objBO_Finance);
        }
    }
}
