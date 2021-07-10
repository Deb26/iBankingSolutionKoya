using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using BLL;
using System.Data;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace iBankingSolution.Transaction
{
    public partial class frmCBSACOpening : System.Web.UI.Page
    {
        BO_Finance objBO_Finance = new BO_Finance();
        BL_Finance objBL_Finance = new BL_Finance();
        string SQLError = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void Tab1_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 0;

            //showCCBACOpen
        }

        protected void txtcustid_TextChanged(object sender, EventArgs e)
        {
            int NoOfSpace = 0;
            objBO_Finance.CUST_ID = txtcustid.Text;
            DataSet dtcustid = objBL_Finance.showCCBACOpen(objBO_Finance);

            NoOfSpace = Convert.ToInt32(dtcustid.Tables[0].Rows[0]["NoSpace"]);

            if (NoOfSpace == 1)
            {
                txtfname.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["MiddleName"]);
                txtmname.Text = String.Empty;
                txtlname.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["LastName"]);

            }
            else
            {

                txtfname.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["FirstName"]);
                txtmname.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["MiddleName"]);
                txtlname.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["LastName"]);
            }
            txtguardname.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["GUARDIAN_NAME"]);
            txtdob.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["DOB"]);
            txtgender.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["SEX"]);
            txtvillage.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["VILL_CODE"]);
            txtpost.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["PO_CODE"]);
            txtdist.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["DIS_CODE"]);
            txtpin.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["pin"]);

            ddlIDType.SelectedValue = Convert.ToString(dtcustid.Tables[0].Rows[0]["ID_Type"]);
            txtidno.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["ID_No"]);
            txtphoneno.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["TEL_NO"]);
            txtsocietyacno.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["SOCIETYACNO"]);
            txtsocietycode.Text = Convert.ToString(dtcustid.Tables[0].Rows[0]["SOCIETY_CODE"]);



        }

        public Boolean PostDataToDB(int n, string s)
        {
            //validate and write to database
            return false;
        }
        protected void btnssave_Click(object sender, EventArgs e)
        {
            string HtmlResult;
            string URI = "https://mdccb.org/API/saveAccountOpenDetails.php?";
            //DateTime EDATE = DateTime.ParseExact(txtdob.Text.Trim(), @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string EDATE = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd");
            string myParameters = "SocietyCode=txtsocietycode.Text & FirstName=txtfname.Text & MiddleName=txtmname.Text & LastName=txtlname.Text & GurdianName=txtguardname.Text & DOB= txtdob.Text & Gender= txtgender.Text & Village=txtvillage.Text & PostOffice= txtpost.Text & District= txtdist.Text & Pincode= txtpin.Text & IDType= ddlIDType.SelectedValue & IDNumber= txtidno.Text & Phone= txtphoneno.Text & SocietyAcNo= txtsocietyacno.Text";
           
            using (WebClient wcc = new WebClient())
            {
                wcc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                 HtmlResult = wcc.UploadString(URI,myParameters);
               
            }
            Response.Write(HtmlResult);

            //    WebRequest wrGETURL;
            //wrGETURL = WebRequest.Create(sURL);

            //wrGETURL.Method = "POST";
            //wrGETURL.ContentType = @"application/json; charset=utf-8";
            //using (var stream = new StreamWriter(wrGETURL.GetRequestStream()))
            //{
            //    var bodyContent = new
            //    {
            //        SocietyCode = Convert.ToString(txtsocietycode.Text),
            //        FirstName = Convert.ToString(txtfname.Text),
            //        MiddleName = Convert.ToString(txtmname.Text),
            //        LastName = Convert.ToString(txtlname.Text),
            //        GurdianName = Convert.ToString(txtguardname.Text),
            //        DOB = Convert.ToDateTime(txtdob.Text),
            //        Gender = Convert.ToString(txtgender.Text),
            //        Village = Convert.ToString(txtvillage.Text),
            //        PostOffice = Convert.ToString(txtpost.Text),
            //        District = Convert.ToString(txtdist.Text),
            //        Pincode = Convert.ToString(txtpin.Text),
            //        IDType = ddlIDType.SelectedValue,
            //        IDNumber = Convert.ToString(txtidno.Text),
            //        Phone = Convert.ToString(txtphoneno.Text),
            //        SocietyAcNo = Convert.ToString(txtsocietyacno.Text)

            //    }; // This will need to be changed to an actual class after finding what the specification sheet requires.

            //    var json = JsonConvert.SerializeObject(bodyContent);

            //    stream.Write(json);
            //}
            //HttpWebResponse webresponse = wrGETURL.GetResponse() as HttpWebResponse;

            //Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            // read response stream from response object
            //StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            // read string from stream data
            //string strResult = loResponseStream.ReadToEnd();
            // close the stream object
            //loResponseStream.Close();
            // close the response object
            //webresponse.Close();

            // Response.Write(HtmlResult);


        }
    }
}