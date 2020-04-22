using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
using System.Text;
using System.Web.Script.Services;
namespace TVS.Controllers
{
    public class userController : Controller
    {
        //
        // GET: /user/

        DataTable dt = new DataTable();
        LuminousMpartnerIBEntities db;
        datautility dut = new datautility();

        public userController()
        {

            db = new LuminousMpartnerIBEntities();
        }

        public ActionResult Register()
         {
             if (Session["userid"] == null)
             {
                 return RedirectToAction("login", "login");
             }
             else
             {

                 dt = Session["permission"] as DataTable;

                 string pageUrl2 = "/user/register";

                 DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                 if (result.Length > 0)
                 {
                     string str = result[0]["createrole"].ToString() + "," + result[0]["editrole"].ToString() + "," + result[0]["deleterole"].ToString() + "," + result[0]["uview"].ToString();

                     if (str == "0,0,0,0")
                     {
                         return RedirectToAction("snotallowed", "snotallowed");
                     }
                     else
                     {
                         Loadstate();
                         return View();
                     }
                 }
                 else
                 {
                     return RedirectToAction("snotallowed", "snotallowed");
                 }


             }
        }


        public List<SelectListItem> Loadstate()
        {
            var myResults = (from data in db.allprofiles
                             select new
                             {
                                 Text = data.profilename,
                                 Value = data.id

                             }
          ).ToList();


            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var dd in myResults)
            {
                li.Add(new SelectListItem { Text = dd.Text, Value = Convert.ToString(dd.Value) });

            }


            ViewData["country"] = li;

            return li;
        }


        public JsonResult Getprofiles()
        {

            var myResults = (from data in db.allprofiles
                             select new
                             {
                                 Text = data.profilename,
                                 Value = data.id

                             }
           ).ToList();


            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var dd in myResults)
            {

                li.Add(new SelectListItem { Text = dd.Text, Value = Convert.ToString(dd.Value) });


            }

            return Json(new SelectList(li, "Value", "Text"));

        }





        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult showuserinfo()
        {
            if (Session["userid"] == null)
            {


                return Json("session expire", JsonRequestBehavior.AllowGet);

            }
            else
            {
                dt = Session["permission"] as DataTable;

                string pageUrl2 = "/user/register";
                //   string pageUrl2 = System.Web.HttpContext.Current.Request.Url.AbsolutePath;

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result.Length > 0)
                {
                    string str = result[0]["createrole"].ToString() + "," + result[0]["editrole"].ToString() + "," + result[0]["deleterole"].ToString() + "," + result[0]["uview"].ToString();


                    return Json(str, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("0,0,0,0", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult save(string email, string username, string password, string status ,int profileid)
        {
            string save = message.save();
            string exists = message.exsits();

             if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {

                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/user/register";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["createrole"].ToString() == "1")
                {


                    var Emp = db.useraccounts.Count(em => em.userid == username.Trim());

                    if (Convert.ToInt32(Emp) < 1)
                    {
                        db.AddTouseraccounts(new useraccount
                            {
                                userid = username.Trim(),
                                password = dut.Encrypt(password),
                                email = email,
                                status = status,
                                profileid = profileid,
                                createtime=DateTime.Now,
                                createdby = Session["userid"].ToString(), 

                            });

                        db.SaveChanges();
                        return Json(save, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(exists, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("You do not have create permission on this action", JsonRequestBehavior.AllowGet);
                }
            }
        }

        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Showstate()
        {
            if (Session["userid"] == null)
            {


                return Json("session expire", JsonRequestBehavior.AllowGet);
               
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/user/register";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["uview"].ToString() == "1")
                {

                    var query = from c in db.useraccounts
                                join o in db.allprofiles
                           on c.profileid equals o.id
                                select new
                                {
                                    userid = c.userid,
                                    password =c.password,
                                    email = c.email,
                                    status = c.status,
                                    profileid = c.profileid,
                                    profilename = o.profilename,
                                    id = c.id
                                };


                    return Json(query, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json("You do not have view permission", JsonRequestBehavior.AllowGet);
                }
            }
        }

        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(User u)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        db.AddToregisters(new register
        //        {
        //            username=u.username

        //        });

        //        db.SaveChanges();
        //         ModelState.Clear();
        //            u = null;
        //          ViewBag.Message = "Successfully Registration Done";
               
        //    }
        //    return View(u);
        //}



        public JsonResult Deletestate(int empId)
        {

            string deleted = message.delete();
            if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/user/register";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["deleterole"].ToString() == "1")
                {


                    var stud = (from s1 in db.useraccounts where s1.id == empId select s1).FirstOrDefault();


                    db.AddTouseraccounthistories(new useraccounthistory()
                    {
                        userid = stud.userid,
                        password = stud.password,
                        status = stud.status,
                        approvallevel = stud.approvallevel,
                        assignedto = stud.assignedto,
                        profileid = stud.profileid,
                        email = stud.email,
                        createdby = stud.createdby,
                        createtime = stud.createtime,
                        updatedby = stud.updatedby,
                        updatedtime = stud.updatedtime,
                        approvalcreatedby = stud.approvalcreatedby,
                        approvalcreattime = stud.approvalcreattime,
                        deletedby = Session["userid"].ToString(),
                        deletedtime=DateTime.Now,
                        
                       
                    });

                    db.SaveChanges();


                    db.DeleteObject(stud);
                    db.SaveChanges();


                    return Json(deleted,
                          JsonRequestBehavior.AllowGet);


                }

                else
                {
                    return Json("You do not have delete permission on this action", JsonRequestBehavior.AllowGet);
                }
            }
        }


        public JsonResult UpdateEmployee(int empId, string userid,string password,string  email,string status,int profile)
        {
            string exists = message.exsits();
            string updated = message.update();
             if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/user/register";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["editrole"].ToString() == "1")
                {


                    var Emp1 = db.useraccounts.Where(e => e.userid == userid.Trim() && e.id != empId).Count(em => em.userid == userid.Trim());

                    if (Convert.ToInt32(Emp1) < 1)
                    {

                        useraccount Emp = db.useraccounts.First(em => em.id == empId);
                        db.AddTouseraccounthistories(new useraccounthistory()
                        {
                            userid = Emp.userid,
                            password = Emp.password,
                            status = Emp.status,
                            approvallevel = Emp.approvallevel,
                            assignedto = Emp.assignedto,
                            profileid = Emp.profileid,
                            email = Emp.email,
                            createdby = Emp.createdby,
                            createtime = Emp.createtime,
                            updatedby = Emp.updatedby,
                            updatedtime = Emp.updatedtime,
                            approvalcreatedby = Emp.approvalcreatedby,
                            approvalcreattime = Emp.approvalcreattime,


                        });

                       //b.SaveChanges();



                        
                       
                        Emp.userid = userid;
                        Emp.password = dut.Encrypt(password);//sachin 29-04-2015 putting Encrypt function in password update function
                        Emp.email = email;
                        Emp.status = status;
                        Emp.profileid = profile;
                        Emp.updatedtime = DateTime.Now;
                        Emp.updatedby = Session["userid"].ToString();
                        try
                        {
                            
                            db.SaveChanges();
                        }
                        catch(Exception ex) {
                            Exception str = ex.InnerException;
                            updated = "Error in Updation";

                        }
                        return Json(updated,
                              JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(exists, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("You do not have update permission on this action", JsonRequestBehavior.AllowGet);
                }
            
                   
            }
        }


        public JsonResult showuserinfo(string password)
        {
          
            if (Session["userid"] == null)
            {


                return Json("session expire", JsonRequestBehavior.AllowGet);

            }
            else
            {
                dt = Session["permission"] as DataTable;

                string pageUrl2 = "/user/register";
                //   string pageUrl2 = System.Web.HttpContext.Current.Request.Url.AbsolutePath;

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result.Length > 0)
                {
                    string str = result[0]["createrole"].ToString() + "," + result[0]["editrole"].ToString() + "," + result[0]["deleterole"].ToString() + "," + result[0]["uview"].ToString();


                    return Json(str, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("0,0,0,0", JsonRequestBehavior.AllowGet);
                }
            }
        }


        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Showpassword(string Password)
        {
           
            return Json("Welcome", JsonRequestBehavior.AllowGet);
        }





       

    }
}
