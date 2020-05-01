using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Data;
namespace Luminous.Controllers
{
    public class DestributorController : Controller
    {
        //
        // GET: /Destributor/
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/Destributor/index";
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
               // DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if ( true /*result[0]["uview"].ToString() == "1"*/)
                {
                    return View();
                }
                //else
                //{
                //    return RedirectToAction("snotallowed", "snotallowed");
                //}
            }
        }
        public JsonResult GetRegions()
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
              //  DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if ( true /*result[0]["uview"].ToString() == "1"*/)
                {
                    var Company = (from c in db.Regions
                                   where c.status != 2 && c.status != 0
                                   select new
                                   {
                                       id = c.id,
                                       Name = c.name
                                   }).ToList();


                    return Json(Company, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }

        //public ActionResult SaveContact(DestributorList destributor, string regionst, string statusC)
        //{
        //    if (Session["userid"] == null)
        //    {
        //        return RedirectToAction("login", "login");
        //    }
        //    else
        //    {
        //        dt = Session["permission"] as DataTable;
        //        string pageUrl2 = PageUrl;
        //        DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
        //        if (result[0]["createrole"].ToString() == "1")
        //        {
        //            int regionId;
        //            if (!int.TryParse(regionst, out regionId))
        //            {
        //                ModelState.AddModelError("RegionId", "Selete Region");
        //            }
        //            if (regionst == null || regionst == "0")
        //            {
        //                ModelState.AddModelError("RegionId", "Selete Region");

        //            }
        //            if (destributor.name != null && destributor.name != "")
        //            {
        //                if (destributor.name.Length > 99)
        //                {
        //                    ModelState.AddModelError("name", "Character Should Be Less Than 100");
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("name", "Distributor Name Is Empty");
        //            }
        //            if (db.AllGroups.Any(a => a.regionName.ToLower() == destributor.name.ToLower() || a.DestributorName.ToLower() == destributor.name.ToLower() || a.DealerName.ToLower() == destributor.name.ToLower()))
        //            {
        //                ModelState.AddModelError("name", "Distributor Name Is Already Exists");

        //            }
        //            if (ModelState.IsValid)
        //            {
        //                destributor.Createtime = DateTime.Now;
        //                destributor.CreatedBy = Session["userid"].ToString();
        //                destributor.RegionId = regionId;
        //                string status = statusC ?? "off";
        //                if (status == "on")
        //                {
        //                    destributor.status = 1;
        //                }
        //                else
        //                {
        //                    destributor.status = 0;

        //                }

        //                db.DestributorLists.AddObject(destributor);
        //                db.SaveChanges();


        //            }

        //            return View("Index");
        //        }
        //        else
        //        {
        //            return RedirectToAction("snotallowed", "snotallowed");

        //        }
        //    }
        //}


        public JsonResult GetContactDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
               // DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["uview"].ToString() == "1"*/)
                {
                    var contactdetails = (from c in db.DestributorLists
                                          where c.status != 2
                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var contactDetails2 = (from c in contactdetails

                                           select new
                                           {
                                               id = c.id,
                                               RegionName = c.Region.name,
                                               Name = c.name,
                                               status = c.status == 1 ? "Active" : "Deactive",


                                           }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                    if (contactdetails.Count() % 15 == 0)
                    {
                        totalrecord = contactdetails.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (contactdetails.Count() / 15) + 1;
                    }
                    var data = new { result = contactDetails2, TotalRecord = totalrecord };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }


        public JsonResult DeleteContact(int id)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    DestributorList contactUs = db.DestributorLists.Single(a => a.id == id);
                    DestributorsHistory regionHistory = new DestributorsHistory();
                  //Save Previous Record In History
                    regionHistory.DestributorId = id;
                    regionHistory.name = contactUs.name;
                    regionHistory.Updatetime = DateTime.Now;
                    regionHistory.UpdatedBy = Session["userid"].ToString();
                    regionHistory.RegionId = contactUs.RegionId;
                    regionHistory.status = contactUs.status;
                    //Update new Record
                    contactUs.status = 2;
                    contactUs.UpdatedBy = Session["userid"].ToString();
                    contactUs.Updatetime = DateTime.Now;
                    db.DestributorsHistories.AddObject(regionHistory);
                    db.SaveChanges();
                    return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }


        }

        public ActionResult Edit(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    DestributorList cud = db.DestributorLists.Single(a => a.id == id);
                    ViewBag.status = cud.status;
                    ViewBag.RegionId = cud.RegionId;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        //public ActionResult Update(DestributorList destributor, string regionst, string statusC)
        //{
        //    if (Session["userid"] == null)
        //    {
        //        return RedirectToAction("login", "login");
        //    }
        //    else
        //    {
        //        dt = Session["permission"] as DataTable;
        //        string pageUrl2 = PageUrl;
        //        DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
        //        if (result[0]["editrole"].ToString() == "1")
        //        {
        //            int regionId;
        //            if (!int.TryParse(regionst, out regionId))
        //            {
        //                ModelState.AddModelError("RegionId", "Selete Region");
        //            }
        //            if (regionst == null || regionst == "0")
        //            {
        //                ModelState.AddModelError("RegionId", "Selete Region");

        //            }
        //            if (destributor.name != null && destributor.name != "")
        //            {
        //                if (destributor.name.Length > 99)
        //                {
        //                    ModelState.AddModelError("name", "Character Should Be Less Than 100");
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("name", "Distributor Name Is Empty");
        //            }
        //            if (db.AllGroups.Any(a => (a.regionName.ToLower() == destributor.name.ToLower() || (a.DestributorName.ToLower() == destributor.name.ToLower() && a.DestributorId != destributor.id) || a.DealerName.ToLower() == destributor.name.ToLower())))
        //            {
        //                ModelState.AddModelError("name", "Distributor Name Is Already Exists");

        //            }
        //            if (ModelState.IsValid)
        //            {
        //                DestributorList contactUs = db.DestributorLists.Single(a => a.id == destributor.id);
        //                DestributorsHistory regionHistory = new DestributorsHistory();
        //                regionHistory.DestributorId = destributor.id;
        //                regionHistory.name = contactUs.name;
        //                regionHistory.Updatetime = DateTime.Now;
        //                regionHistory.UpdatedBy = Session["userid"].ToString();
        //                regionHistory.RegionId = contactUs.RegionId;
        //                regionHistory.status = contactUs.status;

        //                contactUs.Updatetime = DateTime.Now;
        //                contactUs.UpdatedBy = Session["userid"].ToString();
        //                contactUs.RegionId = regionId;
        //                contactUs.name = destributor.name;
        //                string status = statusC ?? "off";
        //                if (status == "on")
        //                {
        //                    contactUs.status = 1;
        //                }
        //                else
        //                {
        //                    contactUs.status = 0;

        //                }
        //                db.DestributorsHistories.AddObject(regionHistory);
        //                db.SaveChanges();
        //                ViewBag.Result = "Record Updated Successfully";
        //            }
        //            return View("Edit");
        //        }
        //        else
        //        {
        //            return RedirectToAction("snotallowed", "snotallowed");
        //        }
        //    }
        //}


    }

}
