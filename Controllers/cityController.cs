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
    public class cityController : Controller
    {
        //
        // GET: /city/
        LuminousMpartnerIBEntities db;
        DataTable dt = new DataTable();
        public cityController()
        {
            db = new LuminousMpartnerIBEntities();
        }

        public ActionResult city()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {

                dt = Session["permission"] as DataTable;

                string pageUrl2 = "/city/city";

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
                        Loadstate();

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

        public List<SelectListItem> Loadstate()
        {
            var myResults = (from data in db.allstates
                             orderby data.statename
                             select new
                             {
                                 Text = data.statename,
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


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Savecity(int empId, string empName)
        {
            string exists = message.exsits();
            string save = message.save();
          
            if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
                //return json("login", "login",JsonRequestBehavior.AllowGet);
            }
            else
            {

                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/city/city";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["createrole"].ToString() == "1")
                {
                    var Emp = db.cities.Count(em => em.stateid == empId && em.cityname==empName);

                    if (Convert.ToInt32(Emp) < 1)
                    {

                        db.AddTocities(new city()
                        {
                            stateid = empId,
                            cityname = empName,

                            createdtime = DateTime.Now,
                            createdby=Session["userid"].ToString(),


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
        public JsonResult showuserinfo()
        {
            if (Session["userid"] == null)
            {


                return Json("session expire", JsonRequestBehavior.AllowGet);

            }
            else
            {
                dt = Session["permission"] as DataTable;

                string pageUrl2 = "/city/city";
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
        public JsonResult Showcity(int id)
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
                    pageUrl2 = "/city/city";

                    // DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                }
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["uview"].ToString() == "1")
                {
                    var query = from c in db.allstates.Where(c => c.id == id)
                                join o in db.cities
                           on c.id equals o.stateid
                                orderby o.cityname
                                select new
                                {
                                    statename = c.statename,
                                    cityname = o.cityname,
                                    id = o.id
                                };



                    //var serv = (from s in db.allstates
                    //            join sl in db.cities on s.id equals sl.stateid
                    //            select s).ToList();

                    return Json(query, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json("You do not have view permission", JsonRequestBehavior.AllowGet);
                }
            }
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getstate()
        {
            var myResults = (from data in db.allstates
                             orderby data.statename
                             select new
                             {
                                 Text = data.statename,
                                 Value = data.id

                             }
            ).ToList();

            return Json(myResults, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public List<SelectListItem> Loadstate1()
        {
            var myResults = (from data in db.allstates
                             orderby data.statename
                             select new
                             {
                                 Text = data.statename,
                                 Value = data.id

                             }
          ).ToList();


            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var dd in myResults)
            {
                li.Add(new SelectListItem { Text = dd.Text, Value = Convert.ToString(dd.Value) });

            }


            ViewData["country1"] = li;

            return li;
        }




        public JsonResult GetStates()
        {

            var myResults = (from data in db.allstates
                             orderby data.statename
                             select new
                             {
                                 Text = data.statename,
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
                string pageUrl2 = "/city/city";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["deleterole"].ToString() == "1")
                {
                   
                   
                        var stud = (from s1 in db.cities where s1.id == empId select s1).FirstOrDefault();



                        db.AddTocity_history(new city_history
                        {
                            stateid = stud.stateid,
                            cityname = stud.cityname,
                            updatedtime = Convert.ToString(stud.updatedtime),
                            createdby = stud.createdby,
                            deletedtime = DateTime.Now,
                            createdtime = Convert.ToString(stud.createdtime),
                            deletedby = Session["userid"].ToString(),

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



        public JsonResult UpdateEmployee(int empId, string empName, int serviceid)
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
                string pageUrl2 = "/city/city";

                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (result[0]["editrole"].ToString() == "1")
                {
                    var query = db.cities.Count(em => em.cityname == empName && em.stateid == serviceid);
                    var total = Convert.ToInt32(query) + 1;
                    if (Convert.ToInt32(total) > 1)
                    {
                        return Json(exists,
                             JsonRequestBehavior.AllowGet);

                    }
                    else
                    {


                        var stud = (from s1 in db.cities where s1.id == empId select s1).FirstOrDefault();



                        db.AddTocity_history(new city_history
                        {
                            stateid = stud.stateid,
                            cityname = stud.cityname,
                            updatedtime = Convert.ToString(stud.updatedtime),
                            createdby = stud.createdby,

                            createdtime = Convert.ToString(stud.createdtime),
                            updatedby = Session["userid"].ToString(),

                        });
                        db.SaveChanges();





                        city Emp = db.cities.First(em => em.id == empId);
                        Emp.stateid = serviceid;
                        Emp.cityname = empName;
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



    }
}