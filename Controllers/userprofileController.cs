using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Web.Security;
using System.Threading;
using System.Data;
namespace TVS.Controllers
{
    public class userprofileController : Controller
    {
        //
        // GET: /userprofile/
        DataTable dt = new DataTable();
        LuminousMpartnerIBEntities db;
        public userprofileController()
        {
            db = new LuminousMpartnerIBEntities();
        }


        public ActionResult userprofile()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {

                dt = Session["permission"] as DataTable;

                string pageUrl2 = "/userprofile/userprofile";

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
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }


            }

           
            
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

                string pageUrl2 = "/userprofile/userprofile";
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
        public JsonResult Showstate()
        { if (Session["userid"] == null)
            {


                return Json("session expire", JsonRequestBehavior.AllowGet);
               
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/userprofile/userprofile";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["uview"].ToString() == "1")
                {
            
            return Json(db.allprofiles.ToList(), JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json("You do not have view permission", JsonRequestBehavior.AllowGet);
                }
            }
        }

        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Savestate(string staten)
        {
        
           if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {

                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/userprofile/userprofile";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["createrole"].ToString() == "1")
                {

                    var Emp = db.allprofiles.Count(em => em.profilename == staten.Trim());

                    if (Convert.ToInt32(Emp) < 1)
                    {

                        db.AddToallprofiles(new allprofile
                        {
                            profilename = staten.Trim(),
                            createdtime = DateTime.Now,
                            createdby = Session["userid"].ToString()
                        });
                        db.SaveChanges();
                        return Json("Record has Saved", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Profile Name Exists.", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("You do not have create permission on this action", JsonRequestBehavior.AllowGet);
                }
            }
        
        }

       
       

      
        
        public JsonResult UpdateEmployee(int empId, string empName)
        {
            
            if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/userprofile/userprofile";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["editrole"].ToString() == "1")
                {
                    var query = db.allprofiles.Count(em => em.profilename == empName);
                    var total = Convert.ToInt32(query) + 1;
                    if (Convert.ToInt32(total) > 1)
                    {
                        return Json("Record has exists............",
                             JsonRequestBehavior.AllowGet);

                    }
                    else
                    {


                        var stud = (from s1 in db.allprofiles where s1.id == empId select s1).FirstOrDefault();



                        db.AddToallprofile_history(new allprofile_history
                        {
                            profilename = stud.profilename,
                            updatedtime = stud.updatedtime.ToString(),
                            createdby = stud.createdby,

                            createdtime = stud.createdtime.ToString(),
                            updatedby = Session["userid"].ToString()

                        });
                        db.SaveChanges();





                        allprofile Emp = db.allprofiles.First(em => em.id == empId);
                        Emp.profilename = empName.Trim();
                        Emp.updatedtime = DateTime.Now;
                        Emp.updatedby = Session["userid"].ToString();
                        db.SaveChanges();
                        return Json("Record has updated sucessfully..",
                              JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("You do not have update permission on this action", JsonRequestBehavior.AllowGet);
                }
            }
        }



   


        public JsonResult Deletestate(int empId)
        {

             if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/userprofile/userprofile";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["deleterole"].ToString() == "1")
                {


                    var query = db.useraccounts.Count(em => em.profileid == empId);
                    var total = Convert.ToInt32(query) + 1;
                    if (Convert.ToInt32(total) > 1)
                    {
                        return Json("You can not delete this Profile............",
                             JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        var stud = (from s1 in db.allprofiles where s1.id == empId select s1).FirstOrDefault();



                        db.AddToallprofile_history(new allprofile_history
                        {
                            profilename = stud.profilename,
                            updatedtime = stud.updatedtime.ToString(),
                            createdby = stud.createdby,
                            deletedtime = DateTime.Now,
                            createdtime = stud.createdtime.ToString(),
                            deletedby = Session["userid"].ToString(),
                           

                        });
                        db.SaveChanges();


                        db.DeleteObject(stud);
                        db.SaveChanges();


                        return Json("Record has deleted sucessfully..",
                              JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("You do not have delete permission on this action", JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
