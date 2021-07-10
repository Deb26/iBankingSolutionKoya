using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace iBankingSolution
{
    public class Global : System.Web.HttpApplication
    {
   
        protected void Application_Start(object sender, EventArgs e)
        {

            
            Application["BasePage"] = ConfigurationManager.AppSettings["BasePage"];

            //RegisterRoutes(RouteTable.Routes);
        }
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    //Convert.ToString(routes.MapPageRoute("default", "", "~/default.aspx"));
        //    Convert.ToString(routes.MapPageRoute("Dashboard", "", "~/Dashboard.aspx"));
        //    Convert.ToString(routes.MapPageRoute("frmKYCList", "", "~/Master/frmKYCList.aspx"));
        //    Convert.ToString(routes.MapPageRoute("MasterSocietySetup", "", "~/Master/frmMasterSocietySetup.aspx"));
        //}
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}