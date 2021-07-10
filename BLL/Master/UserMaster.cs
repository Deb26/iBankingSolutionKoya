using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL.Master
{
    public class ClsUserMaster
    {
        public static DataTable VerifyLogin(String UserName, String Password, Int32 BranchID)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@UserName", UserName);
            p[1] = new SqlParameter("@Password", Password);
            p[2] = new SqlParameter("@BranchID", BranchID);
            return DataLayer.GetInstance().ExecuteDataTable("sp_VerifyLogin", p);
        }
        public static DataTable VerifyCompany(Int32 UserID, Int32 BranchID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@UserID", UserID);
            p[1] = new SqlParameter("@BranchID", BranchID);
            return DataLayer.GetInstance().ExecuteDataTable("sp_VerifyCompany", p);
        }
        public static DataTable CheckUseronSave(String UserName)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@UserName", UserName);
            return DataLayer.GetInstance().ExecuteDataTable("sp_CheckUseronSave", p);
        }
        public static DataTable CheckUseronUpdate(String UserName, Int32 UserID)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@UserName", UserName);
            p[1] = new SqlParameter("@UserID", UserID);
            return DataLayer.GetInstance().ExecuteDataTable("sp_CheckUseronUpdate", p);
        }
        public static DataTable GetUserDetail(Int32 UserID)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@UserID", UserID);
            return DataLayer.GetInstance().ExecuteDataTable("sp_GetUserDetail", p);
        }
        public static SqlDataReader GenarateUserNo(Int32 Add)
        {
            return DataLayer.GetInstance().ExecuteReaderVer2("SELECT dbo.ufn_GenarateUserNo(" + Add + ")");
        }
        public static int InsertUser(String Name, String Address, String PinCode
            , DateTime DOB, String MobileNo, String PhoneNo, String Email, String UserName
            , String Password, Boolean IsAdmin, Boolean Active, Int32 CompanyID)
        {
            SqlParameter[] p = new SqlParameter[12];
            p[0] = new SqlParameter("@Name", Name);
            p[1] = new SqlParameter("@Address", Address);
            p[2] = new SqlParameter("@PinCode", PinCode);
            p[3] = new SqlParameter("@DOB", DOB);
            p[4] = new SqlParameter("@MobileNo", MobileNo);
            p[5] = new SqlParameter("@PhoneNo", PhoneNo);
            p[6] = new SqlParameter("@Email", Email);
            p[7] = new SqlParameter("@UserName", UserName);
            p[8] = new SqlParameter("@Password", Password);
            p[9] = new SqlParameter("@IsAdmin", IsAdmin);
            p[10] = new SqlParameter("@Active", Active);
            p[11] = new SqlParameter("@CompanyID", CompanyID);
            return DataLayer.GetInstance().ExecuteNonQuery("sp_InsertUser", p);
        }
        public static int UpdateUser(String Name, String Address, String PinCode
            , DateTime DOB, String MobileNo, String PhoneNo, String Email, String UserName
            , String Password, Boolean IsAdmin, Boolean Active, Int32 CompanyID, Int32 UserID)
        {
            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("@Name", Name);
            p[1] = new SqlParameter("@Address", Address);
            p[2] = new SqlParameter("@PinCode", PinCode);
            p[3] = new SqlParameter("@DOB", DOB);
            p[4] = new SqlParameter("@MobileNo", MobileNo);
            p[5] = new SqlParameter("@PhoneNo", PhoneNo);
            p[6] = new SqlParameter("@Email", Email);
            p[7] = new SqlParameter("@UserName", UserName);
            p[8] = new SqlParameter("@Password", Password);
            p[9] = new SqlParameter("@IsAdmin", IsAdmin);
            p[10] = new SqlParameter("@Active", Active);
            p[11] = new SqlParameter("@CompanyID", CompanyID);
            p[12] = new SqlParameter("@UserID", UserID);
            return DataLayer.GetInstance().ExecuteNonQuery("sp_UpdateUser", p);
        }
        public static int DeleteUser(Int32 UserID)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@UserID", UserID);
            return DataLayer.GetInstance().ExecuteNonQuery("sp_DeleteUser", p);
        }
        public static DataTable SearchUserList(String SearchColumn, String SearchText)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@SearchColumn", SearchColumn);
            p[1] = new SqlParameter("@SearchText", SearchText);
            return DataLayer.GetInstance().ExecuteDataTable("sp_SearchUserList", p);
        }
        public static DataTable CheckPassword(Int32 UserID, String Password)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@UserID", UserID);
            p[1] = new SqlParameter("@Password", Password);
            return DataLayer.GetInstance().ExecuteDataTable("sp_CheckPassword", p);
        }
        public static int ChangePassword(Int32 UserID, String NewPassword)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@UserID", UserID);
            p[1] = new SqlParameter("@NewPassword", NewPassword);
            return DataLayer.GetInstance().ExecuteNonQuery("sp_ChangePassword", p);
        }
        public static DataTable FillUserList()
        {
            return DataLayer.GetInstance().ExecuteDataTable("sp_FillUserList");
        }
    }
}
