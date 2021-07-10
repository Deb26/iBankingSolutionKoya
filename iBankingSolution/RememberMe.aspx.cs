using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iBankingSolution
{
    public partial class RememberMe : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //    try
            //    {
            //        var UserSalt = UserService.GetUserDetailsForRememberMe(Convert.ToInt32(Session["UserId"])).FirstOrDefault();

            //        byte[] hashPwd = Convert.FromBase64String(UserSalt.Password);

            //        byte[] salt = new byte[16];
            //        Array.Copy(hashPwd, 0, salt, 0, 16);

            //        var pbkfd2 = new Rfc2898DeriveBytes(Request.Form["password"], salt, 10000);

            //        byte[] hash = pbkfd2.GetBytes(20);

            //        int Ok = 1;
            //        for (int i = 0; i < 20; i++)
            //        {
            //            if (hashPwd[i + 16] != hash[i])
            //            {
            //                Ok = 0;
            //                break;
            //            }
            //        }

            //        if (Ok == 1)
            //        {
            //            Response.Redirect("DashBoard.aspx");
            //        }
            //        else
            //        {
            //            ShowMessage("Unable to login", MessageType.Warning);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            }
        }
    }