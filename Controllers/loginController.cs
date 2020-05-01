using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Web.Security;
using System.Threading;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace TVS.Controllers
{
    public class loginController : Controller
    {
        //
        // GET: /login/
        
        datautility dut = new datautility();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        LuminousEntities db;
        public loginController()
        {
            db = new LuminousEntities();
        }
        public ActionResult Index()
        {

           
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult login()
        {

           
                return View();
           
        }


        
        public JsonResult validatelogin(string userid, string password)
        {

           string Encryptpwd = dut.Encrypt(password);

           try
           {
               var Emp = db.useraccounts.Count(em => em.userid == userid.Trim() && em.password == Encryptpwd && em.status == "1");
               // var Emp = db.useraccounts.Count(em => em.userid == userid.Trim() && em.password == password && em.status == "1");
               if (Convert.ToInt32(Emp) < 1)
               {


                   return Json("Invalid userid and password", JsonRequestBehavior.AllowGet);
               }
               else
               {
                 
                   SqlParameter[] pram ={
                    new SqlParameter("@mode",1), 
                      new SqlParameter("@userid",userid.Trim()),
                 };



                //   ds = dut.ExecuteDSProcedure("userpermission", pram);
                   Session["userid"] = userid;
                   var id = db.useraccounts.Where(u => u.userid == userid).Select(c => c.id).SingleOrDefault();
                   var Profiletype = db.useraccounts.Where(u => u.userid == userid).Select(c => c.profileid).SingleOrDefault();
                   Session["Id"] = id;
                  // Session["permission"] = ds.Tables[1];
                   if (Profiletype == 5)
                   {
                       return Json("CallingSample/Index", JsonRequestBehavior.AllowGet);
                   }
                   else
                   {
                       return Json("LuminousHome/Luminous", JsonRequestBehavior.AllowGet);
                   }
                  
               }
           }
           catch(Exception exc) {
               Exception emsg = exc.InnerException;
               string msg = emsg.Message;
               return Json(msg, JsonRequestBehavior.AllowGet);
            }

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
            return Json(rows,JsonRequestBehavior.AllowGet);
        }
        
    }
}
