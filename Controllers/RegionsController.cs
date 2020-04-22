using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Data;
namespace LuminousMpartnerIB.Controllers
{
    public class RegionsController : Controller
    {
        //
        // GET: /Regions/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/Regions/index";
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
        public ActionResult SaveContact(Region region, string statusC)
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
                    if (region.RegionCode != null && region.RegionCode != "")
                    {
                        if (region.RegionCode.Length > 29)
                        {
                            ModelState.AddModelError("RegionCode", "Character Should Be Less Than 30");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("RegionCode", "Region Code Is Empty");
                    }
                    if (db.Regions.Any(a => a.RegionCode.ToLower() == region.RegionCode.ToLower() && a.status !=2))
                    {
                        ModelState.AddModelError("RegionCode", "Region Code Is Already Exists");

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
                        ModelState.AddModelError("name", "Region Name Is Empty");
                    }
                    if (db.AllGroups.Any(a => a.regionName.ToLower() == region.name.ToLower() || a.DestributorName.ToLower() == region.name.ToLower() || a.DealerName.ToLower() == region.name.ToLower()))
                    {
                        ModelState.AddModelError("name", "Name Is Already Exists");

                    }
                    if (ModelState.IsValid)
                    {
                        region.Createtime = DateTime.Now;
                        region.createBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            region.status = 1;
                        }
                        else
                        {
                            region.status = 0;

                        }

                        db.Regions.AddObject(region);
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
                    var contactdetails = (from c in db.Regions
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
                                               RegionCode = c.RegionCode,
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


        [HttpGet]
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
                    Region cud = db.Regions.Single(a => a.id == id);
                    ViewBag.status = cud.status;

                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
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
                    Region contactUs = db.Regions.Single(a => a.id == id);
                    RegionsHistroy regionHistory = new RegionsHistroy();
                    regionHistory.RegionId = id;
                    regionHistory.name = contactUs.name;
                    regionHistory.Updatetime = DateTime.Now;
                    regionHistory.UpdatedBy = Session["userid"].ToString();
                    regionHistory.status = contactUs.status;
                    contactUs.status = 2;
                    contactUs.UpdatedBy = Session["userid"].ToString();
                    contactUs.Updatetime = DateTime.Now;


                    db.RegionsHistroys.AddObject(regionHistory);
                    db.SaveChanges();
                    return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }


        }


        public ActionResult Update(Region region, string statusC)
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
                    ViewBag.status = statusC;
                    if (region.name != null && region.name != "")
                    {
                        if (region.name.Length > 99)
                        {
                            ModelState.AddModelError("name", "Character Should Be Less Than 100");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("name", "Region Name Is Empty");
                    }
                    if (region.RegionCode != null && region.RegionCode != "")
                    {
                        if (region.RegionCode.Length > 29)
                        {
                            ModelState.AddModelError("RegionCode", "Character Should Be Less Than 30");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("RegionCode", "Region Code Is Empty");
                    }
                    if (db.Regions.Any(a => a.RegionCode.ToLower() == region.RegionCode.ToLower() && a.id != region.id && a.status!=2))
                    {
                        ModelState.AddModelError("RegionCode", "Region Code Is Already Exists");

                    }
                    if (db.AllGroups.Any(a => ((a.regionName.ToLower() == region.name.ToLower() && a.regionID != region.id) || a.DestributorName.ToLower() == region.name.ToLower() || a.DealerName.ToLower() == region.name.ToLower())))
                    {

                        ModelState.AddModelError("name", "Name Is Already Exists");


                    }
                    if (ModelState.IsValid)
                    {
                        Region contactusd = db.Regions.Single(a => a.id == region.id);
                        RegionsHistroy regionHistory = new RegionsHistroy();
                        regionHistory.RegionId = contactusd.id;
                        regionHistory.name = contactusd.name;
                        regionHistory.Updatetime = DateTime.Now;
                        regionHistory.UpdatedBy = Session["userid"].ToString();
                        regionHistory.status = contactusd.status;
                        regionHistory.RegionCode = contactusd.RegionCode;



                        contactusd.name = region.name;
                        contactusd.RegionCode = region.RegionCode;
                        contactusd.Updatetime = DateTime.Now;
                        contactusd.UpdatedBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.status = 1;
                        }
                        else
                        {
                            contactusd.status = 0;
                        }


                        ViewBag.Result = "Record Updated Successfully";


                        db.RegionsHistroys.AddObject(regionHistory);
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
