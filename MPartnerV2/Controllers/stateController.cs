using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Web.Security;
using System.Threading;
using System.Data;
using TVS;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
namespace TVS.Controllers
{
    public class stateController : Controller
    {
        //
        // GET: /state/

        LuminousEntities db;
        DataTable dt = new DataTable();
        public stateController()
        {
            db = new LuminousEntities();
          
        }

        
        public ActionResult state()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {

                dt = Session["permission"] as DataTable;

                string pageUrl2 = "/state/state";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result.Length > 0)
                {
                    string str = result[0]["createrole"].ToString() + "," + result[0]["editrole"].ToString() + "," + result[0]["deleterole"].ToString() + "," + result[0]["uview"].ToString();

                    if (str == "0,0,0,0")
                    {
                        return RedirectToAction("mnotallowed", "mnotallowed");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("mnotallowed", "mnotallowed");
                }
                //// Display.
                //foreach (DataRow row in result)
                //{
                //    Console.WriteLine(row["ID"]);
                //}
                ////user();
                //    ViewData["suhail"] = "sahil";

                //Response.Write("<script>alert('you r not authorised user')</script>");


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

                string pageUrl2 = "/state/state";
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
            if ((Session["userid"] == null && Session["Ioslogin"] == null))
            {


                return Json("session expire", JsonRequestBehavior.AllowGet);

            }
            else
            {
                string pageUrl2 = "";
                if (Session["Ioslogin"] != null || Session["ASMEmpCode"] != null)
                {
                    dt = Session["permission"] as DataTable;
                    pageUrl2 = "/CreateContest/Index";
                    //  DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                }
                else
                {
                    dt = Session["permission"] as DataTable;
                    pageUrl2 = "/state/state";

                    //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                }
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["uview"].ToString() == "1")
                {
                    try
                    {
                        var myResults = (from data in db.allstates
                                         orderby data.statename
                                         select new
                                         {
                                             statename = data.statename,
                                             id = data.id

                                         }
          ).ToList();
                        
                        return Json(myResults, JsonRequestBehavior.AllowGet);
                    }
                    catch(Exception ex)
                    {
                        return Json(ex.Message, JsonRequestBehavior.AllowGet);
                    }
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


           string msg= message.save();
           string exists = message.exsits();
           
            if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {

                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/state/state";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["createrole"].ToString() == "1")
                {



                    var Emp = db.allstates.Count(em => em.statename == staten.Trim());

                    if (Convert.ToInt32(Emp) < 1)
                    {

                        db.AddToallstates(new allstate
                        {
                            statename = staten.Trim(),
                            createdtime = DateTime.Now,
                            createdby = Session["userid"].ToString()
                        });
                        db.SaveChanges();
                        return Json(msg, JsonRequestBehavior.AllowGet);
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

        public void user()
        {
            Session["suhail"] = "sahil";
        }
        public bool validatefields(string staten)
        {
            if (staten.Trim() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }



      
        
        public JsonResult UpdateEmployee(int empId, string empName)
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
                string pageUrl2 = "/state/state";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["editrole"].ToString() == "1")
                {


                    var query = db.allstates.Count(em => em.statename == empName);
                    var total = Convert.ToInt32(query) + 1;
                    if (Convert.ToInt32(total) > 1)
                    {
                        return Json(exists,
                             JsonRequestBehavior.AllowGet);

                    }
                    else
                    {


                        var stud = (from s1 in db.allstates where s1.id == empId select s1).FirstOrDefault();



                        db.AddToallstate_history(new allstate_history
                        {
                            statename = stud.statename,
                            updatedtime = stud.updatedtime.ToString(),
                            createdby = stud.createdby,

                            createdtime = stud.createdtime.ToString(),
                            updatedby = Session["userid"].ToString()

                        });
                        db.SaveChanges();





                        allstate Emp = db.allstates.First(em => em.id == empId);
                        Emp.statename = empName.Trim();
                        Emp.updatedtime = DateTime.Now;
                        Emp.updatedby = Session["userid"].ToString();
                        db.SaveChanges();
                        return Json(updated,
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
            string deleted = message.delete();
            if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/state/state";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["deleterole"].ToString() == "1")
                {

                    var query = db.cities.Count(em => em.stateid == empId);
                    var total = Convert.ToInt32(query) + 1;
                    if (Convert.ToInt32(total) > 1)
                    {
                        return Json("You can not delete this state............",
                             JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        var stud = (from s1 in db.allstates where s1.id == empId select s1).FirstOrDefault();



                        db.AddToallstate_history(new allstate_history
                        {
                            statename = stud.statename,
                            updatedtime = stud.updatedtime.ToString(),
                            createdby = stud.createdby,
                            deletedtime = DateTime.Now,
                            createdtime = stud.createdtime.ToString(),
                            deletedby = Session["userid"].ToString(),

                        });
                        db.SaveChanges();


                        db.DeleteObject(stud);
                        db.SaveChanges();


                        return Json(deleted,
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
