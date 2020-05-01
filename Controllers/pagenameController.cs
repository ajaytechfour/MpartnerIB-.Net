using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Web.Security;
using System.Threading;
using System.Data;
namespace TVS.Controllers
{
    public class pagenameController : Controller
    {
        //
        // GET: /userprofile/
        DataTable dt = new DataTable();
        LuminousEntities db;
        public pagenameController()
        {
            db = new LuminousEntities();
        }


        public ActionResult pagename()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {

                dt = Session["permission"] as DataTable;

                string pageUrl2 = "/pagename/pagename";

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

                string pageUrl2 = "/pagename/pagename";
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
        {
             if (Session["userid"] == null)
            {


                return Json("session expire", JsonRequestBehavior.AllowGet);
               
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/pagename/pagename";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["uview"].ToString() == "1")
                {

                    return Json(db.allpages.ToList(), JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json("You do not have view permission", JsonRequestBehavior.AllowGet);
                }
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Savestate(string staten, string stateurl)
        {
            if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {

                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/pagename/pagename";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["createrole"].ToString() == "1")
                {

                    var Emp = db.allpages.Count(em => em.pagename == staten.Trim());

                    if (Convert.ToInt32(Emp) < 1)
                    {

                        db.AddToallpages(new allpage
                        {
                            pagename = staten.Trim(),
                            pageurl = stateurl,
                            createdtime = DateTime.Now,
                            createdby = Session["userid"].ToString(), 
                        });
                        db.SaveChanges();
                        return Json("Record has Saved", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Page Name Exists.", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json("You do not have create permission on this action", JsonRequestBehavior.AllowGet);
                }
            }
                


        }





        public JsonResult UpdateEmployee(int empId, string empName, string stateurl)
        {
            if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/pagename/pagename";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["editrole"].ToString() == "1")
                {
                    var query = db.allpages.Count(em => em.pagename == empName);
                    var total = Convert.ToInt32(query) + 1;
                    if (Convert.ToInt32(total) > 1)
                    {
                        return Json("Page Name has exists............",
                             JsonRequestBehavior.AllowGet);

                    }
                    else
                    {


                        var stud = (from s1 in db.allpages where s1.id == empId select s1).FirstOrDefault();







                        allpage Emp = db.allpages.First(em => em.id == empId);
                        Emp.pagename = empName.Trim();
                        Emp.pageurl = stateurl.Trim();
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
                string pageUrl2 = "/pagename/pagename";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["deleterole"].ToString() == "1")
                {
                    var stud = (from s1 in db.allpages where s1.id == empId select s1).FirstOrDefault();


                    db.AddToallpagehistories(new allpagehistory
                    {
                        pagename = stud.pagename,
                        pageurl = stud.pageurl,
                        createdtime = stud.createdtime,

                        createdby = stud.createdby,
                        updatedby=stud.updatedby,
                        updatedtime=stud.updatedtime,
                        deletedby = Session["userid"].ToString(),
                        deletedtime=DateTime.Now
                    });
                    db.SaveChanges();


                    db.DeleteObject(stud);
                    db.SaveChanges();


                    return Json("Record has deleted sucessfully..",
                          JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("You do not have delete permission on this action", JsonRequestBehavior.AllowGet);
                }
            }
        
            
        }
    }
}
