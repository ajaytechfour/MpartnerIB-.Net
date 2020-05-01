using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity;
namespace Luminous.Controllers
{
    public class DealersController : Controller
    {
        //
        // GET: /Dealers/
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/Dealers/index";
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
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public JsonResult GetDestributors(String RegionId)
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
                if (result[0]["uview"].ToString() == "1")
                {
                    int regionId = int.Parse(RegionId);

                    var Company = (from c in db.DestributorLists
                                   where c.status != 2 && c.status != 0 && c.RegionId == regionId
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


        public JsonResult GetDestributorsByMultiPleids(string[] RegionId)
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
                if (result[0]["uview"].ToString() == "1")
                {
                    if (RegionId != null && RegionId[0] != null && RegionId[0] != "")
                    {
                        Regex rg = new Regex(",");
                        string[] idsstring = rg.Split(RegionId[0]);
                        int?[] regid = new int?[idsstring.Length];

                        for (int i = 0; i < idsstring.Length; i++)
                        {
                            regid[i] = int.Parse(idsstring[i]);
                        }

                        var Company = (from c in db.DestributorLists
                                       where c.status != 2 && c.status != 0 && regid.Contains(c.RegionId)
                                       select new
                                       {
                                           id = c.id,
                                           Name = c.name
                                       }).ToList();


                        return Json(Company, JsonRequestBehavior.AllowGet);
                    }
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }


        public JsonResult GetAllDestributors()
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
                if (result[0]["uview"].ToString() == "1")
                {
                    var Company = (from c in db.DestributorLists
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

        public JsonResult GetAllDealer()
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
                if (result[0]["uview"].ToString() == "1")
                {

                    var Company = (from c in db.Dealers
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

        public JsonResult GetAllDealerByMultiPleIds(string[] RegionId)
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
                if (result[0]["uview"].ToString() == "1")
                {
                    if (RegionId != null && RegionId[0] != null && RegionId[0] != "")
                    {

                        Regex rg = new Regex(",");
                        string[] idsstring = rg.Split(RegionId[0]);
                        int?[] regid = new int?[idsstring.Length];

                        for (int i = 0; i < idsstring.Length; i++)
                        {
                            regid[i] = int.Parse(idsstring[i]);
                        }

                        var Company = (from c in db.Dealers
                                       where c.status != 2 && c.status != 0 && regid.Contains(c.DestributorId)
                                       select new
                                       {
                                           id = c.id,
                                           Name = c.name
                                       }).ToList();



                        return Json(Company, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);
                }

            }
        }
        public ActionResult SaveContact(Dealer region, string regionst, string Destri, string statusC)
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
                if (result[0]["createrole"].ToString() == "1")
                {
                    int regID;
                    int DesId;

                    if (!int.TryParse(Destri, out DesId))
                    {
                        ModelState.AddModelError("DestributorId", "Distributor Not Selected");
                    }
                    else if (Destri == "0")
                    {
                        ModelState.AddModelError("DestributorId", "Distributor Not Selected");
                    }

                    if (!int.TryParse(regionst, out regID))
                    {
                        ModelState.AddModelError("RegionId", "Region Not Selected");
                    }
                    else if (regionst == "0")
                    {
                        ModelState.AddModelError("RegionId", "Region Not Selected");
                    }
                    if (region.name != null && region.name != "")
                    {
                        if (region.name.Length > 99)
                        {
                            ModelState.AddModelError("name", "Character Should Be Less Than 100");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("name", "Dealer Name Is Empty");
                    }
                   if (db.AllGroups.Any(a => a.regionName.ToLower() == region.name.ToLower() || a.DestributorName.ToLower() == region.name.ToLower() || a.DealerName.ToLower() == region.name.ToLower()))
                    {
                        ModelState.AddModelError("name", "Name Is Already Exists");

                    }
                    if (ModelState.IsValid)
                    {
                        region.createtime = DateTime.Now;
                        region.createdBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            region.status = 1;
                        }
                        else
                        {
                            region.status = 0;

                        }
                        region.RegionId = regID;
                        region.DestributorId = DesId;

                        db.Dealers.AddObject(region);
                        db.SaveChanges();


                    }

                    return View("Index");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }
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
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    var contactdetails = (from c in db.Dealers
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
                                               DestriName = c.Destributor.name,
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
                    Dealer contactUs = db.Dealers.Single(a => a.id == id);
                    //Save Previous Record In History
                    DealersHistory regionHistory = new DealersHistory();
                    regionHistory.Dealerid = id;
                    regionHistory.name = contactUs.name;
                    regionHistory.Updatetime = DateTime.Now;
                    regionHistory.UpdatedBy = Session["userid"].ToString();
                    regionHistory.RegionId = contactUs.RegionId;
                    regionHistory.status = contactUs.status;
                    regionHistory.DestributorId = contactUs.DestributorId;
                    //Update New Record
                    contactUs.status = 2;
                    contactUs.UpdatedBy = Session["userid"].ToString();
                    contactUs.Updatetime = DateTime.Now;
                    db.DealersHistories.AddObject(regionHistory);
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
                    Dealer cud = db.Dealers.Single(a => a.id == id);
                    ViewBag.status = cud.status;
                    ViewBag.RegionId = cud.RegionId;
                    ViewBag.DestId = cud.DestributorId;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult Update(Dealer region, string regionst, string Destri, string statusC)
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

                    int regID;
                    int DesId;

                    if (!int.TryParse(Destri, out DesId))
                    {
                        ModelState.AddModelError("DestributorId", "Distributor Not Selected");
                    }
                    else if (Destri == "0")
                    {
                        ModelState.AddModelError("DestributorId", "Distributor Not Selected");
                    }

                    if (!int.TryParse(regionst, out regID))
                    {
                        ModelState.AddModelError("RegionId", "Region Not Selected");
                    }
                    else if (regionst == "0")
                    {
                        ModelState.AddModelError("RegionId", "Region Not Selected");
                    }
                    if (region.name != null && region.name != "")
                    {
                        if (region.name.Length > 99)
                        {
                            ModelState.AddModelError("name", "Character Should Be Less Than 100");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("name", "Dealer Name Is Empty");
                    }
                    if (db.AllGroups.Any(a => (a.regionName.ToLower() == region.name.ToLower() || a.DestributorName.ToLower() == region.name.ToLower() || (a.DealerName.ToLower() == region.name.ToLower() && a.DealersID != region.id))))
                    {
                        ModelState.AddModelError("name", "Name Is Already Exists");

                    }
                    if (ModelState.IsValid)
                    {

                        Dealer contactUs = db.Dealers.Single(a => a.id == region.id);
                        DealersHistory regionHistory = new DealersHistory();
                        regionHistory.Dealerid = contactUs.id;
                        regionHistory.name = contactUs.name;
                        regionHistory.Updatetime = DateTime.Now;
                        regionHistory.UpdatedBy = Session["userid"].ToString();
                        regionHistory.RegionId = contactUs.RegionId;
                        regionHistory.status = contactUs.status;
                        regionHistory.DestributorId = contactUs.DestributorId;


                      
                        contactUs.Updatetime = DateTime.Now;
                        contactUs.name = region.name;
                        contactUs.Updatetime = DateTime.Now;
                        contactUs.UpdatedBy = Session["userid"].ToString();
                        contactUs.DestributorId = DesId;
                        contactUs.RegionId = regID;
                        contactUs.name = region.name;
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactUs.status = 1;
                        }
                        else
                        {
                            contactUs.status = 0;
                        }


                        ViewBag.Result = "Record Updated Successfully";


                        db.DealersHistories.AddObject(regionHistory);
                        db.SaveChanges();

                    }
                    return View("Edit");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

    }
}
