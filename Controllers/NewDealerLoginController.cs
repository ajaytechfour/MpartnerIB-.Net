using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
using LuminousMpartnerIB.EF;
namespace TVS.Controllers
{
    public class NewDealerLoginController : Controller
    {
        //
        // GET: /login/

        datautility dut = new datautility();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        LuminousMpartnerIBEntities db;
        public NewDealerLoginController()
        {
            db = new LuminousMpartnerIBEntities();
        }
        public ActionResult Index()
        {



            return RedirectToAction("NewDealerLogin");

        }

        public ActionResult NewDealerLogin()
        {


            return View("NewDealerlogin");

        }



        public JsonResult validatelogin(string userid, string mobileno)
        {

            //string Encryptpwd = dut.Encrypt(password);

            // var Emp = db.UsersLists.Where(em => em.UserId == userid.Trim() && em.isActive == 1).Select(c => new { c.Dis_Name, c.Dis_Sap_Code,c.CustomerType }).SingleOrDefault();

            ////  if(Emp!=null)
            //  {
            //      Session["Dis_DelName"] = Emp.Dis_Name;


            //  }

            try
            {


                SendSMS sm = new SendSMS();
                var data = sm.BikerLogInCreateOtp(userid, mobileno);





                Session["ASMEmpCode"] = userid.ToString();
                Session["mobileno"] = mobileno.ToString();

                // Contest code //
                string Asmempcode = Session["ASMEmpCode"].ToString();
                string Mobileno = Session["mobileno"].ToString();
                var contestdetails = (from c in db.InsertContexts
                                      where c.Status != 2 && c.FSE_DealerMobileNo == Mobileno

                                      select c).ToList();
                if (contestdetails.Count() == 2)
                {
                    Session["Count2"] = "Count2";
                }
                else
                {
                    Session["Count2"] = null;
                }
                // Contest code end //



                if (data.des == "OTP Created Successfully")
                {
                    return Json("OTP Created Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("OTP Not Verify", JsonRequestBehavior.AllowGet);
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
                if (Session["Ioslogin"] != null)
                {
                    sapcode = Session["SapCode"].ToString();
                }

                SendSMS verifyotp = new SendSMS();
                var getverifymessage = verifyotp.OTPAuthentication(Session["ASMEmpCode"].ToString(), Session["mobileno"].ToString(), password);

                if (getverifymessage.des == "WRONG OTP")
                {
                    return Json("Wrong OTP", JsonRequestBehavior.AllowGet);
                }
                //Calling CreateSOAPWebRequest method    
               
                else
                {
                    SqlParameter[] pram ={
                    new SqlParameter("@mode",1), 
                      new SqlParameter("@userid","ContestUser"),
                 };
                    ds = dut.ExecuteDSProcedure("userpermission", pram);
                    Session["userid"] = sapcode;
                    var id = db.useraccounts.Where(u => u.userid == "ContestUser").Select(c => c.id).SingleOrDefault();
                    Session["Id"] = id;
                    Session["permission"] = ds.Tables[1];
                    return Json("Varified Successfully", JsonRequestBehavior.AllowGet);
                }



              
              
              
                 
                     

                       
                       
                       

                
                  
                    return Json("IOSLogin/IOSLogin", JsonRequestBehavior.AllowGet);

              
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

        //public void SentSms(string phoneno,string empcode)
        //{
        //    try
        //    {
        //        SendSMS sm = new SendSMS();
        //        sm.BikerLogInCreateOtp(empcode, phoneno);
        //    }
        //    catch (Exception exc)
        //    {

        //    }
        //}

    }
}
