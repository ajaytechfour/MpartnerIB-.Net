using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Web.Security;
using System.Threading;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net;
using System.Xml;
namespace TVS.Controllers
{
    public class IOSLoginController : Controller
    {
        //
        // GET: /login/

        datautility dut = new datautility();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        LuminousMpartnerIBEntities db;
        public IOSLoginController()
        {
            db = new LuminousMpartnerIBEntities();
        }
        public ActionResult Index()
        {



            return RedirectToAction("IOSLogin");

        }

        public ActionResult IOSLogin()
        {


            return View("IOSLogin");

        }



        public JsonResult validatelogin(string userid)
        {

            //string Encryptpwd = dut.Encrypt(password);
            var Emp = db.UsersLists.Where(em => em.UserId == userid.Trim() && em.isActive == 1).Select(c => new { c.Dis_Name, c.Dis_Sap_Code, c.CustomerType }).SingleOrDefault();

            if (Emp != null)
            {
                Session["Dis_DelName"] = Emp.Dis_Name;


            }
            try
            {

                //Calling CreateSOAPWebRequest method    
                HttpWebRequest request = CreateSOAPWebRequest();

                XmlDocument SOAPReqBody = new XmlDocument();
                SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
 <soap:Body>
    <sscMDisCreateOtp  xmlns=""http://tempuri.org/"">
  <DisID>" + userid + @"</DisID>
      <imeinumber>99444656</imeinumber>
      <osversion>6.0.1</osversion>
      <devicename>ISO</devicename>
      <appversion>2.2</appversion>
      
    </sscMDisCreateOtp >
  </soap:Body>
</soap:Envelope>");




                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                //Geting response from request    
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        //reading stream    
                        var ServiceResult = rd.ReadToEnd();
                        
                        if (ServiceResult.Contains("OTP Created Successfully"))
                        {
                            ServiceResult = "OTP Created Successfully";
                            Session["Ioslogin"] = ServiceResult;
                            Session["SapCode"] = userid;
                            return Json(ServiceResult, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            ServiceResult = "OTP Not Created";
                            return Json(ServiceResult, JsonRequestBehavior.AllowGet);
                        }
                       
                        //writting stream result on console    
                        //  Console.WriteLine(ServiceResult);  
                        // Console.ReadLine();  
                    }
                  
                    //if (Convert.ToInt32(Emp) < 1)
                    //{


                    //    return Json("Invalid userid and password", JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{

                    //    SqlParameter[] pram ={
                    //     new SqlParameter("@mode",1), 
                    //       new SqlParameter("@userid",userid.Trim()),
                    //  };



                    //    ds = dut.ExecuteDSProcedure("userpermission", pram);
                    //    Session["userid"] = userid;
                    //    var id = db.useraccounts.Where(u => u.userid == userid).Select(c => c.id).SingleOrDefault();
                    //    Session["Id"] = id;

                    return Json("IOSLogin/IOSLogin", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception exc)
            {
                Exception emsg = exc.InnerException;
                string msg = emsg.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult VerifyOTP(string password)
        {

            //string Encryptpwd = dut.Encrypt(password);

            try
            {
                string sapcode = "";
                if(Session["Ioslogin"]!=null)
                {
                    sapcode = Session["SapCode"].ToString();

                    // Contest code //

                    var contestdetails = (from c in db.InsertContexts
                                          where c.Status != 2 && c.DealerId == sapcode

                                          select c).ToList();
                    if (contestdetails.Count() == 2)
                    {
                        Session["Count2"] = "Count2";
                    }
                    // Contest code end //
                }
                //Calling CreateSOAPWebRequest method    
                HttpWebRequest request = CreateSOAPWebRequest_VerifyOTP();

                XmlDocument SOAPReqBody = new XmlDocument();
                SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
 <soap:Body>

    <sscMDisVarifyOtp  xmlns=""http://tempuri.org/"">
 <DisID>" + sapcode + @"</DisID>
      <imeinumber>99444656</imeinumber>
      <osversion>6.0.1</osversion>
      <devicename>ISO</devicename>
      <otp>" + password + @"</otp>
      
    </sscMDisVarifyOtp >
  </soap:Body>
</soap:Envelope>");




                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                //Geting response from request    
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        //reading stream    
                        var ServiceResult = rd.ReadToEnd();

                        if (ServiceResult.Contains("Varified Successfully"))
                        {
                            ServiceResult = "Varified Successfully";
                            
                            Session["IosOtpvarify"] = "OTP Successfull";

                            SqlParameter[] pram ={
                    new SqlParameter("@mode",1), 
                      new SqlParameter("@userid","ContestUser"),
                 };
                            ds = dut.ExecuteDSProcedure("userpermission", pram);
                            Session["userid"] = sapcode;
                            var id = db.useraccounts.Where(u => u.userid == "ContestUser").Select(c => c.id).SingleOrDefault();
                            Session["Id"] = id;
                            Session["permission"] = ds.Tables[1];
                            return Json(ServiceResult, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            ServiceResult = "OTP Not Created";
                            return Json(ServiceResult, JsonRequestBehavior.AllowGet);
                        }

                        //writting stream result on console    
                        //  Console.WriteLine(ServiceResult);  
                        // Console.ReadLine();  
                    }
                    //LuminousMpartnerIB.ServiceReference1.sscMDisCreateOtpRequest abc = new LuminousMpartnerIB.ServiceReference1.sscMDisCreateOtpRequest();
                    //abc.Body.appversion = "2.2";
                    //abc.Body.devicename = "Andoid";
                    //abc.Body.imeinumber = "123";
                    //abc.Body.osversion = "6.0.1";
                    //abc.Body.DisID = "9999998";

                    // var Emp = db.useraccounts.Count(em => em.userid == userid.Trim() && em.password == Encryptpwd && em.status == "1");
                    // var Emp = db.useraccounts.Count(em => em.userid == userid.Trim() && em.password == password && em.status == "1");
                    //if (Convert.ToInt32(Emp) < 1)
                    //{


                    //    return Json("Invalid userid and password", JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{

                    //    SqlParameter[] pram ={
                    //     new SqlParameter("@mode",1), 
                    //       new SqlParameter("@userid",userid.Trim()),
                    //  };



                    //    ds = dut.ExecuteDSProcedure("userpermission", pram);
                    //    Session["userid"] = userid;
                    //    var id = db.useraccounts.Where(u => u.userid == userid).Select(c => c.id).SingleOrDefault();
                    //    Session["Id"] = id;

                    return Json("IOSLogin/IOSLogin", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception exc)
            {
                Exception emsg = exc.InnerException;
                string msg = emsg.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

        }


        public HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request    
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://sapservice.LuminousMpartnerIBindia.com/Lumb2bWebService/service.asmx");
            //SOAPAction    
            Req.Headers.Add(@"SOAPAction:http://tempuri.org/sscMDisCreateOtp");
            //Content_type    
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method    
            Req.Method = "POST";
            //return HttpWebRequest    
            return Req;
        }
        public HttpWebRequest CreateSOAPWebRequest_VerifyOTP()
        {
            //Making Web Request    
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://sapservice.LuminousMpartnerIBindia.com/Lumb2bWebService/service.asmx");
            //SOAPAction    
            Req.Headers.Add(@"SOAPAction:http://tempuri.org/sscMDisVarifyOtp");
            //Content_type    
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method    
            Req.Method = "POST";
            //return HttpWebRequest    
            return Req;
        }
        public JsonResult menudata()
        {

            dt = (DataTable)Session["permission"];
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            serializer.Serialize(rows);
            Session["permissionnew"] = rows;
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

    }
}
