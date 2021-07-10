using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using System.Configuration;
using System.Data.SqlClient;
using DAL;
using System.Data;

namespace BLL.Master
{

    public class clsInitialSettings
    {
        static Int32 d, m, Y;

        public static DateTime getYsdt(DateTime dt)
        {
            DataSet dt1 = new DataSet();
            SqlConnection con = DBConnection.sqlConn();
            SqlCommand com = new SqlCommand("select YEAR_START_DT  FROM codesandnos", con);

            com.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(com);
            try
            {
                con.Open();
                da.Fill(dt1);
                if (dt1.Tables[0].Rows.Count > 0)
                {
                    

                    d = Convert.ToDateTime(dt1.Tables[0].Rows[0]["YEAR_START_DT"]).Day;
                    m = Convert.ToDateTime(dt1.Tables[0].Rows[0]["YEAR_START_DT"]).Month;


                  if ((dt.Month) < m)
                    {
                        Y = dt.AddYears(-1).Year;

                    }
                    else if (dt.Month == m)
                    {
                        if (dt.Day < d)
                            Y = dt.AddYears(-1).Year;
                        else
                            Y = (dt).Year;
                    }
                    else
                    {
                        Y = (dt).Year;
                    }

                    return DateTime.Parse(Convert.ToString(d + "/" + m + "/" + Y));
                }
            }

            catch (Exception ex)
            {

            }
            finally
            {

            }
            return DateTime.Parse(Convert.ToString(d + "/" + m + "/" + Y));
        }

    }
}
