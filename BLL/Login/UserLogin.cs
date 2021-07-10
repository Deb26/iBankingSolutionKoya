using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BLL.Master;

namespace BLL.Login
{
    public class ClsUserLogin : IUserLogin
    {
        #region IUserLogin Members

        public DataTable UserLogin(Credentials credentials)
        {

            DataTable dt = new DataTable();
            dt = ClsUserMaster.VerifyLogin(credentials.UserName, credentials.Password, credentials.BranchId);
            return dt;
        }

        #endregion
    }
}
