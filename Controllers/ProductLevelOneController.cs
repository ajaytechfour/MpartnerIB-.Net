using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Data;
using LuminousMpartnerIB.EF;

namespace Luminous.Controllers
{
    public class ProductLevelOneController : Controller
    {
        //
        // GET: /ProductLevelOne/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/ProductLevel3/index";
        public ActionResult Index(string Search)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                if (Search != null && Search != "")
                {
                    Session["Search"] = Search;

                }
                else
                {
                    Session.Remove("Search");
                }
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

        public JsonResult GetProductCategory() {
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
                    var Company = (from c in db.ProductCatergories
                                   where c.Pstatus != 2 && c.Pstatus != 0
                                   select new
                                   {
                                       id = c.id,
                                       Name = c.PName
                                   }).ToList();
                    return Json(Company, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }
        public ActionResult SaveContact(ProductLevelOne contactUs, string statusC, string pcId, string ParentCatid)
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
                    int pcid, prntcatid;

                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("pcid", "Product Category Has Incorrect Value");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("pcid", "Select Product Category");
                    }
                    if (!int.TryParse(ParentCatid, out prntcatid))
                    {
                        ModelState.AddModelError("ParentCatid", "Parent Category Has Incorrect Value");

                    }
                    if (prntcatid == 0)
                    {
                        ModelState.AddModelError("ParentCatid", "Select Parent Category");
                    }
                    if (contactUs.Name == null)
                    {
                        ModelState.AddModelError("Name", "Product Level One Has No Value");
                    }
                    else if (contactUs.Name.Length > 99)
                    {
                        ModelState.AddModelError("Name", "Character Should Be Less Than 100 ");
                    }
                    if (db.ProductLevelOnes.Any(a => a.pcId == pcid && a.Name.ToLower() == contactUs.Name.ToLower() && a.PlStatus != 2))
                    {
                        ModelState.AddModelError("Name", "Product Level One Is Already Exists");
                    }
                    if (ModelState.IsValid)
                    {
                        ProductLevelOne contactusd = new ProductLevelOne();
                        contactusd.Name = contactUs.Name;
                        contactusd.pcId = pcid;
                        contactusd.ParentCatid = prntcatid;
                        contactusd.CreatedBy = Session["userid"].ToString();
                        contactusd.CreatedDate = DateTime.Now;
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.PlStatus = 1;
                        }
                        else
                        {
                            contactusd.PlStatus = 0;
                        }
                        db.ProductLevelOnes.Add(contactusd);
                        int affectedValue = db.SaveChanges();
                        if (affectedValue > 0)
                        {
                            //ViewBag.result = "Record Saved Successfully";
                            return Content("<script>alert('Data Successfully Submitted');location.href='../ProductLevelOne/Index';</script>");
                        }
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
                    var contactdetails = (from c in db.ProductLevelOnes
                                          where c.PlStatus != 2
                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    if (contactdetails.Count() % 15 == 0)
                    {
                        totalrecord = contactdetails.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (contactdetails.Count() / 15) + 1;
                    }
                    if (Session["Search"] != null)
                    {
                        var contactDetails2 = (from c in contactdetails

                                               select new
                                               {
                                                   id = c.id,
                                                   //PCat = c.ProductCatergory.PName,
                                                   Name = c.Name,
                                                   //prntcat = c.ParentCategory.PCName,
                                                   status = c.PlStatus == 1 ? "Enable" : "Disable",


                                               }).Where(a => a.PCat.Contains(Session["Search"].ToString()) || a.Name.Contains(Session["Search"].ToString()) || a.status.Contains(Session["Search"].ToString()) || a.id.ToString().Contains(Session["Search"].ToString()) || a.prntcat.Contains(Session["Search"].ToString())).OrderByDescending(a => a.id).Skip(page ?? 0).ToList();
                        if (contactDetails2.Count == 0)
                        {
                            var data = new { result = contactDetails2 };
                            return Json(data, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var data = new { result = contactDetails2, TotalRecord = totalrecord };
                            return Json(data, JsonRequestBehavior.AllowGet);
                        }



                    }
                    else
                    {
                        var contactDetails2 = (from c in contactdetails

                                               select new
                                               {
                                                   id = c.id,
                                                   //PCat = c.ProductCatergory.PName,
                                                   Name = c.Name,
                                                   prntcat = c.ParentCategory.PCName,
                                                   status = c.PlStatus == 1 ? "Enable" : "Disable",


                                               }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                        var data = new { result = contactDetails2, TotalRecord = totalrecord };

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    //var data = new { result = contactDetails2, TotalRecord = totalrecord };

                    //return Json(data, JsonRequestBehavior.AllowGet);
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
                    ProductLevelOne cud = db.ProductLevelOnes.Single(a => a.id == id);
                    ViewBag.status = cud.PlStatus;
                    ViewBag.Pid = cud.pcId;
                    ViewBag.Prntid = cud.ParentCatid;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        [HttpPost]
        public ActionResult Update(ProductLevelOne contactUs, string statusC, string pcId, string ParentCatid)
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
                    int pcid, prntid;
                    ViewBag.status = statusC;
                    ViewBag.Pid = pcId;
                    ViewBag.Prntid = ParentCatid;
                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("pcid", "Product Category Has Incorrect Value");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("pcid", "*");
                    }

                    if (!int.TryParse(ParentCatid, out prntid))
                    {
                        ModelState.AddModelError("ParentCatid", "Parent Category Has Incorrect Value");

                    }
                    if (prntid == 0)
                    {
                        ModelState.AddModelError("ParentCatid", "*");
                    }

                    if (db.ProductLevelOnes.Any(a => a.pcId == pcid && a.Name.ToLower() == contactUs.Name.ToLower() && a.id != contactUs.id && a.PlStatus != 2))
                    {
                        ModelState.AddModelError("Name", "Product Level One With Selected Product Category Is  Already Exists");
                    }
                    if (ModelState.IsValid)
                    {
                        ProductLevelOne contactusd = db.ProductLevelOnes.Single(a => a.id == contactUs.id);
                        ProductLevelOneHistory CUDHistory = new ProductLevelOneHistory();
                        CUDHistory.PlvlOneId = contactusd.id;
                        CUDHistory.pcId = contactusd.pcId;
                        CUDHistory.Name = contactusd.Name;
                        CUDHistory.PlStatus = contactusd.PlStatus;
                        CUDHistory.ModifiedBy = Session["userid"].ToString();
                        CUDHistory.ModifiedDate = DateTime.Now;


                        contactusd.Name = contactUs.Name;
                        contactusd.pcId = pcid;
                        contactusd.ParentCatid = prntid;
                        contactusd.ModifiedDate = DateTime.Now;
                        contactusd.ModifiedBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.PlStatus = 1;
                        }
                        else
                        {
                            contactusd.PlStatus = 0;
                        }

                        db.ProductLevelOneHistories.Add(CUDHistory);
                        int affectedRows = db.SaveChanges();
                        if (affectedRows > 0)
                        {
                            ViewBag.Result = "Record Updated Successfully";

                        }
                    }

                    return View("Edit");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        [HttpPost]
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
                    ProductLevelOne contactUs = db.ProductLevelOnes.Single(a => a.id == id);
                    //Save Previous Record In History

                    
                    ProductLevelOneHistory CUDHistory = new ProductLevelOneHistory();
                    CUDHistory.PlvlOneId = contactUs.id;
                    CUDHistory.pcId = contactUs.pcId;
                    CUDHistory.Name = contactUs.Name;
                    CUDHistory.PlStatus = contactUs.PlStatus;
                    CUDHistory.ModifiedBy = Session["userid"].ToString();
                    CUDHistory.ModifiedDate = DateTime.Now;

                    db.ProductLevelOneHistories.Add(CUDHistory);                   
                    //Update New Record
                    contactUs.PlStatus = 2;
                    int affectedReocrds = db.SaveChanges();
                    if (affectedReocrds > 0)
                    {
                        return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Record Not Deleted", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }
    }
}
