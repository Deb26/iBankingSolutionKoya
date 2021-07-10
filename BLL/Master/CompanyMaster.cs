using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL.Master
{
    public class ClsCompanyMaster
    {
        public static DataTable FillBranchList()
        {
            return DataLayer.GetInstance().ExecuteDataTable("sp_FillBranchList"); //
        }
        public static DataTable CheckCompanyonSave(String Company)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Company", Company);
            return DataLayer.GetInstance().ExecuteDataTable("sp_CheckCompanyonSave", p);
        }

        public static DataTable GetFinancialYear()
        {

            return DataLayer.GetInstance().ExecuteDataTable("proc_FillFinancialYear");

        }
        public static DataTable CheckCompanyonUpdate(String Company, Int32 CompanyID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Company", Company);
            p[1] = new SqlParameter("@CompanyID", CompanyID);
            return DataLayer.GetInstance().ExecuteDataTable("sp_CheckCompanyonUpdate", p);
        }
        public static DataTable SearchCompanyList(String SearchColumn, String SearchText)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@SearchColumn", SearchColumn);
            p[1] = new SqlParameter("@SearchText", SearchText);
            return DataLayer.GetInstance().ExecuteDataTable("sp_SearchCompanyList", p);
        }
        public static DataTable GetCompanyDetail(Int32 CompanyID)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@CompanyID", CompanyID);
            return DataLayer.GetInstance().ExecuteDataTable("sp_GetCompanyDetail", p);
        }
        public static SqlDataReader GenarateCompanyNo(Int32 Add)
        {
            return DataLayer.GetInstance().ExecuteReaderVer2("SELECT dbo.ufn_GenarateCompanyNo(" + Add + ")");
        }
        public static int InsertCompany(String CompanyName, String CompanyRegdAddress, String CompanyPincode, String CompanyWorkOffice
            , String CompanyEmail, String CompanyWebSite, String CompanyPhno, String CompanyFaxno, String VATNo, String TINNo
            , String CSTNo, String RegistrationNo, String PANNo, String ServiceTaxNo, String ContactPersonName, String ContactPersonPhno
            , String ContactPersonMobNo, String ContactPersonEmail, String ShortCode, String IEC, String ExporterRef)
        {
            SqlParameter[] p = new SqlParameter[21];
            p[0] = new SqlParameter("@CompanyName", CompanyName);
            p[1] = new SqlParameter("@CompanyRegdAddress", CompanyRegdAddress);
            p[2] = new SqlParameter("@CompanyPincode", CompanyPincode);
            p[3] = new SqlParameter("@CompanyWorkOffice", CompanyWorkOffice);
            p[4] = new SqlParameter("@CompanyEmail", CompanyEmail);
            p[5] = new SqlParameter("@CompanyWebSite", CompanyWebSite);
            p[6] = new SqlParameter("@CompanyPhno", CompanyPhno);
            p[7] = new SqlParameter("@CompanyFaxno", CompanyFaxno);
            p[8] = new SqlParameter("@VATNo", VATNo);
            p[9] = new SqlParameter("@TINNo", TINNo);
            p[10] = new SqlParameter("@CSTNo", CSTNo);
            p[11] = new SqlParameter("@RegistrationNo", RegistrationNo);
            p[12] = new SqlParameter("@PANNo", PANNo);
            p[13] = new SqlParameter("@ServiceTaxNo", ServiceTaxNo);
            p[14] = new SqlParameter("@ContactPersonName", ContactPersonName);
            p[15] = new SqlParameter("@ContactPersonPhno", ContactPersonPhno);
            p[16] = new SqlParameter("@ContactPersonMobNo", ContactPersonMobNo);
            p[17] = new SqlParameter("@ContactPersonEmail", ContactPersonEmail);
            p[18] = new SqlParameter("@ShortCode", ShortCode);
            p[19] = new SqlParameter("@IEC", IEC);
            p[20] = new SqlParameter("@ExporterRef", ExporterRef);
            return DataLayer.GetInstance().ExecuteNonQuery("sp_InsertCompany", p);
        }
        public static int UpdateCompany(String CompanyName, String CompanyRegdAddress, String CompanyPincode, String CompanyWorkOffice
            , String CompanyEmail, String CompanyWebSite, String CompanyPhno, String CompanyFaxno, String VATNo, String TINNo
            , String CSTNo, String RegistrationNo, String PANNo, String ServiceTaxNo, String ContactPersonName, String ContactPersonPhno
            , String ContactPersonMobNo, String ContactPersonEmail, Int32 CompanyID, String ShortCode, String IEC, String ExporterRef)
        {
            SqlParameter[] p = new SqlParameter[22];
            p[0] = new SqlParameter("@CompanyName", CompanyName);
            p[1] = new SqlParameter("@CompanyRegdAddress", CompanyRegdAddress);
            p[2] = new SqlParameter("@CompanyPincode", CompanyPincode);
            p[3] = new SqlParameter("@CompanyWorkOffice", CompanyWorkOffice);
            p[4] = new SqlParameter("@CompanyEmail", CompanyEmail);
            p[5] = new SqlParameter("@CompanyWebSite", CompanyWebSite);
            p[6] = new SqlParameter("@CompanyPhno", CompanyPhno);
            p[7] = new SqlParameter("@CompanyFaxno", CompanyFaxno);
            p[8] = new SqlParameter("@VATNo", VATNo);
            p[9] = new SqlParameter("@TINNo", TINNo);
            p[10] = new SqlParameter("@CSTNo", CSTNo);
            p[11] = new SqlParameter("@RegistrationNo", RegistrationNo);
            p[12] = new SqlParameter("@PANNo", PANNo);
            p[13] = new SqlParameter("@ServiceTaxNo", ServiceTaxNo);
            p[14] = new SqlParameter("@ContactPersonName", ContactPersonName);
            p[15] = new SqlParameter("@ContactPersonPhno", ContactPersonPhno);
            p[16] = new SqlParameter("@ContactPersonMobNo", ContactPersonMobNo);
            p[17] = new SqlParameter("@ContactPersonEmail", ContactPersonEmail);
            p[18] = new SqlParameter("@CompanyID", CompanyID);
            p[19] = new SqlParameter("@ShortCode", ShortCode);
            p[20] = new SqlParameter("@IEC", IEC);
            p[21] = new SqlParameter("@ExporterRef", ExporterRef);
            return DataLayer.GetInstance().ExecuteNonQuery("sp_UpdateCompany", p);
        }
        public static int DeleteCompany(Int32 CompanyID)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@CompanyID", CompanyID);
            return DataLayer.GetInstance().ExecuteNonQuery("sp_DeleteCompany", p);
        }
    }
}
