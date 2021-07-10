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
namespace iBankingSolution.MasterPages
{
    public partial class ProjectBM : System.Web.UI.MasterPage
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string message;
        MyDBDataContext dbContext = new MyDBDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("/Default.aspx");
            }
            var PasprintDt = (from cod in dbContext.CODESANDNOs

                              select new
                              {
                                  LISCENCEE_NAME = cod.LISCENCEE_NAME


                              }).SingleOrDefault();

            if (PasprintDt != null)
            {
                marqsocietyname.Text = Convert.ToString(PasprintDt.LISCENCEE_NAME);

            }

        }
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            string VfUserName = Convert.ToString(Session["UserName"]);
            objBO_Finance.userri = VfUserName;
            int i = objBL_Finance.DeletebyUserId(objBO_Finance);
            if (i > 0)
            {
                message = "alert('Successfully Logged Out')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            }


            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.HttpContext.Current.Session.Clear();
            //System.Web.HttpContext.Current.Session["CompanyName"] = null;
            //System.Web.HttpContext.Current.Session["Name"] = null;
            System.Web.HttpContext.Current.Session["UserID"] = null;
            System.Web.HttpContext.Current.Session["BranchID"] = null;
            System.Web.HttpContext.Current.Session["IsAdmin"] = null;
            System.Web.HttpContext.Current.Session["UserName"] = null;
            System.Web.HttpContext.Current.Response.Redirect("/LogOut.aspx");

        }

        protected void LinkSocietySetup_Click(object sender, EventArgs e)
        {
           //Response.Redirect(GetRouteUrl("MasterSocietySetup" , null));
        }
    }
}