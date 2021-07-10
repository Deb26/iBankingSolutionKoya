using System;              
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL;
using BLL.Login;
using BLL.Master;
using System.Collections.Specialized;
using BusinessObject;
 



namespace iBankingSolution
{
    public partial class Default : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string message;
        MyDBDataContext DBContext = new MyDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (System.Web.HttpContext.Current.Session["UserID"] != null || System.Web.HttpContext.Current.Session["BranchID"] != null)
                //{
                //    //Response.Redirect("DashBoard.aspx");
                //    //Response.Redirect("Default.aspx");
                //}
                //else
                //{
                //string vfName = "";
                //vfName = Session["SocietyName"].ToString();
                //if (Session["SocietyName"].ToString() != null)
                //{
                //    lblName.Text = Session["SocietyName"].ToString();
                //}
                //lblName2.Text="Welcome..." + Session["SocietyName"].ToString();
                var Society = (from cod in DBContext.CODESANDNOs

                                  select new
                                  {
                                      Socityname = cod.LISCENCEE_NAME 
                                   

                                  }).SingleOrDefault();

                if (Society != null)
                {
                    txtSocietyName.Text = Convert.ToString(Society.Socityname);
                    txtSocietyName.Enabled = false;
                }
                else
                {
                    txtSocietyName.Text = "";
                }

                    GetBranchList();
                    GetFinancialYear();

                    if (Request.Cookies["UserName"] != null && Request.Cookies["PassWord"] != null)
                    {
                        //txtuid.Text = Request.Cookies["UserName"].Value;
                        //Request.Form[""]= Request.Cookies["UserName"].Value;
                        //txtpwd.Attributes["value"] = Request.Cookies["PassWord"].Value;
                        //chkRemember.Checked = true;
                    }

                //}
            }
        }

        private void GetBranchList()
        {
            DataTable dt = new DataTable();
            dt = ClsCompanyMaster.FillBranchList();
            if (dt.Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = dt;
                ddlbranch.DataTextField = dt.Columns["BranchName"].ToString();
                ddlbranch.DataValueField = dt.Columns["BranchID"].ToString();
                ddlbranch.DataBind();
            }

        }

        private void GetFinancialYear()
        {

            
            DataTable dt = new DataTable();
            dt = ClsCompanyMaster.GetFinancialYear();
            if (dt.Rows.Count > 0)
            {
                ddlfyi.Items.Clear();
                ddlfyi.DataSource = dt;
                ddlfyi.DataTextField = dt.Columns["Financialyearname"].ToString();
                ddlfyi.DataValueField = dt.Columns["FinancialYearID"].ToString();
                ddlfyi.DataBind();
            }

        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        public enum MessageType { Success, Error, Info, Warning };
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string VfUserName = Request.Form["email"];

            //objBO_Finance.login_date = System.DateTime.Now;
            objBO_Finance.userri = VfUserName;
            //objBO_Finance.SOCIETY_CODE = Convert.ToInt32(ddlbranch.SelectedValue);
            //DataSet dSet = objBL_Finance.CheckLogindetails(objBO_Finance);
            //if (dSet.Tables.Count == 0)
            //{

                var VfPassword = Request.Form["password"];
                Int32 vfBrnch = Convert.ToInt32(ddlbranch.SelectedValue);

                Credentials credential = new Credentials
                {

                    UserName = VfUserName,
                    Password = VfPassword,
                    BranchId = vfBrnch,
                };
                DataTable dt = new DataTable();


                ClsUserLogin UserLogin = new ClsUserLogin();
                dt = UserLogin.UserLogin(credential);
                if (dt.Rows.Count > 0)
                {
                    if (chkRemember.Checked)
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["PassWord"].Expires = DateTime.Now.AddDays(30);
                    }
                    else
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["PassWord"].Expires = DateTime.Now.AddDays(-1);
                    }
                    Response.Cookies["UserName"].Value = VfUserName;
                    Response.Cookies["PassWord"].Value = VfPassword;

                    bool IsAdmin = false;
                    IsAdmin = Convert.ToBoolean(dt.Rows[0]["IsAdmin"].ToString());
                    Int32 UserId = Convert.ToInt32(dt.Rows[0]["UserID"].ToString());

                    if (IsAdmin == true)
                    {
                        System.Web.HttpContext.Current.Session["admin"] = dt;
                        System.Web.HttpContext.Current.Session["UserID"] = dt.Rows[0]["UserID"].ToString();
                        System.Web.HttpContext.Current.Session["BranchID"] = Convert.ToInt32(ddlbranch.SelectedValue != "" ? ddlbranch.SelectedValue : "0");
                        //System.Web.HttpContext.Current.Session["Name"] = dt.Rows[0]["Name"].ToString();
                        System.Web.HttpContext.Current.Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                        System.Web.HttpContext.Current.Session["EmpID"] = dt.Rows[0]["EmpID"].ToString();
                        System.Web.HttpContext.Current.Session["CompanyName"] = ddlbranch.SelectedItem.Text;

                    Response.Redirect("DashBoard.aspx");
                    //Response.Redirect(GetRouteUrl("DashBoard", null));


                }
                    else
                    {
                        DataTable dsCompanyVerify = new DataTable();
                        dsCompanyVerify = ClsUserMaster.VerifyCompany(UserId, Convert.ToInt32(ddlbranch.SelectedValue));
                        if (dsCompanyVerify.Rows.Count > 0)
                        {
                            System.Web.HttpContext.Current.Session["admin"] = dsCompanyVerify;
                            System.Web.HttpContext.Current.Session["UserID"] = dsCompanyVerify.Rows[0]["UserID"].ToString();
                            System.Web.HttpContext.Current.Session["BranchID"] = dsCompanyVerify.Rows[0]["BranchID"].ToString();
                            //System.Web.HttpContext.Current.Session["Name"] = dsCompanyVerify.Rows[0]["Name"].ToString();
                            System.Web.HttpContext.Current.Session["UserName"] = dsCompanyVerify.Rows[0]["UserName"].ToString();
                            System.Web.HttpContext.Current.Session["CompanyName"] = dsCompanyVerify.Rows[0]["CompanyName"].ToString();
                            System.Web.HttpContext.Current.Session["EmpID"] = dsCompanyVerify.Rows[0]["EmpID"].ToString();

                        Response.Redirect("DashBoard.aspx");
                        //Response.Redirect(GetRouteUrl("DashBoard", null));
                    }
                        else
                        {

                            ShowMessage("You have not permission login under" + ddlbranch.SelectedItem.Text + " Branch.", MessageType.Warning);
                        }
                    }
                }
                else
                {

                    ShowMessage("Invalid Login ID / PassWord !", MessageType.Error);
                }
            //}

            //else if (dSet.Tables[0].Rows.Count > 0)
            //{
            //    //if()
            //    message = "alert('Same LoginId already used. Please use Another LoginId')";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", message, true);
            //    return;
            //}
        }

        protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 vfBrnch = Convert.ToInt32(ddlbranch.SelectedValue);
        }
    }
    
}