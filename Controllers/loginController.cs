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

namespace TVS.Controllers
{
    public class loginController : Controller
    {
        //
        // GET: /login/

        datautility dut = new datautility();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        LuminousMpartnerIBEntities db;
        public loginController()
        {
            db = new LuminousMpartnerIBEntities();
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

            int EmpUseraccounts = 0;
            var Emp = new useraccount();

            try
            {
                if (userid == "C00001")
                {
                    EmpUseraccounts = db.UsersLists.Count(em => em.UserId.ToLower() == userid.Trim().ToLower() && em.WEB_PASSWORD == Encryptpwd);
                }
                else
                {
                    EmpUseraccounts = db.UsersLists.Count(em => em.UserId == userid.Trim() && em.WEB_PASSWORD == password);
                }


                if (EmpUseraccounts < 1)
                {

                    return Json("Invalid userid and password", JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var users = db.UsersLists.Where(u => u.UserId == userid).FirstOrDefault<UsersList>();

                    Session["userid"] = userid;
                    Session["Id"] = users.id;
                    Session["ctype"] = users.CustomerType;


                    //SqlParameter[] pram ={
                    //new SqlParameter("@mode",1),
                    //  new SqlParameter("@userid",userid.Trim()),
                    // };

                    //ds = dut.ExecuteDSProcedure("userpermission", pram);

                    //Session["permission"] = ds.Tables[1];

                    //var Profiletype = db.useraccounts.Where(u => u.userid == userid).Select(c => c.profileid).SingleOrDefault();




                    return Json("Home", JsonRequestBehavior.AllowGet);



                    //if (Profiletype == 5)
                    //{
                    //    return Json("CallingSample", JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    //    return Json("Home", JsonRequestBehavior.AllowGet);
                    //}

                }

            }
            catch (Exception exc)
            {
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
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

    }
}
