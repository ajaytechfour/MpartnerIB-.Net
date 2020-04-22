using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Luminous.EF;
namespace Luminous.Controllers
{
    public class EwarrantyController : Controller
    {
        //
        // GET: /Ewarranty/
          [ValidateInput(false)]
        public ActionResult Index(string id)
        {

            //string srno = "TTJKMzY5RTMwMjMxMTE";
           // id = srno;
            WebRequest request = WebRequest.Create(
           "https://mpartnerv2.luminousindia.com/nonsapservices/api/nonsapservice/EW_SelPendingTOWithoutOTP" + "?SerialNo=" + id + "&token=pass@1234");
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            //// Display the content.  
            //Console.WriteLine(responseFromServer);

            // Clean up the streams and the response.  
            reader.Close();
            response.Close();

            try
            {

                DataTable dt = (DataTable)JsonConvert.DeserializeObject(responseFromServer, (typeof(DataTable)));

                JavaScriptSerializer js_header = new JavaScriptSerializer();
                var responsedata = js_header.Deserialize<List<Ewarranty_data>>(responseFromServer);

                Session["serno"] = id;
                ViewBag.serialno = responsedata[0].Product_Serial_Number;

                ViewBag.product_model = responsedata[0].Product_Model;

                ViewBag.customer_name = responsedata[0].Customer_Name;

                ViewBag.mod_date = responsedata[0].ModDate;

                ViewBag.isinput = responsedata[0].isInput;
            }
            catch (Exception exc)
            {
                ViewBag.serialno_notfound = id;
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveEwarranty(string userinput)
        {

            string serialno = Session["serno"].ToString();
            WebRequest request = WebRequest.Create(
           "https://mpartnerv2.luminousindia.com/nonsapservices/api/nonsapservice/EW_SaveWithoutOTP" + "?SerialNo=" + serialno + "&UserInput=" + userinput + "&EW_VerifiedBy=WEB&token=pass@1234");
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            //// Display the content.  
            //Console.WriteLine(responseFromServer);

            // Clean up the streams and the response.  
            reader.Close();
            response.Close();
            try
            {

                DataTable dt = (DataTable)JsonConvert.DeserializeObject(responseFromServer, (typeof(DataTable)));

                JavaScriptSerializer js_header = new JavaScriptSerializer();
                var responsedata = js_header.Deserialize<List<Ewarranty_data>>(responseFromServer);
                ViewBag.msg = responsedata[0].Msg;
               // ViewBag.msg = "Invalid";
            }
            catch (Exception exc)
            {
                ViewBag.msg = "Invalid Operation";
            }



            return Json(ViewBag.msg, JsonRequestBehavior.AllowGet);

        }

    }
}
