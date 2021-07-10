using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Login
{
    public class Credentials
    {
        private string _UserName;
        private string _Password;
        private int _BranchId;
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        public int BranchId
        {
            get
            {
                return this._BranchId;
            }
            set
            {
                this._BranchId = value;
            }
        }
    }
}
