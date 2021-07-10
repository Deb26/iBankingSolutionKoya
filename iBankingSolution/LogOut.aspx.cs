using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.Login;
using BLL.Master;
using System.Collections.Specialized;
using BusinessObject;

namespace iBankingSolution
{
    public partial class LogOut : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string message;
        protected void Page_Load(object sender, EventArgs e)
        {
            string VfUserName = Convert.ToString(Session["UserName"]);
            objBO_Finance.userri = VfUserName;
            int i = objBL_Finance.DeletebyUserId(objBO_Finance);
            if (i > 0)
            {
                message = "alert('Successfully Logged Out')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }

            Session.Abandon();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}