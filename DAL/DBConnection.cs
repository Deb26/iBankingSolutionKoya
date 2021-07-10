using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DBConnection
    {
        public static SqlConnection sqlConn()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            return new SqlConnection(cs);
        }
    }
}
