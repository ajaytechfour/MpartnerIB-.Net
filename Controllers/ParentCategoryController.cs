using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using LuminousMpartnerIB.EF;
namespace LuminousMpartnerIB.Controllers
{
    public class ParentCategoryController : Controller
    {
        //
        // GET: /ParentCategory/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/ParentCategory/index";
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }


        public ActionResult SaveContact(ParentCategory Pcategory, string statusC)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {

                    if (db.ParentCategories.Any(a => a.PCName.ToLower() == Pcategory.PCName.ToLower() && a.PCStatus != 0))
                    {
                        ModelState.AddModelError("PCName", "Parent Category Already Exists");
                    }
                    if (Pcategory.PCName == "" || Pcategory.PCName == null)
                    {
                        ModelState.AddModelError("PCName", "Parent Category Is Required");
                    }
                    if (ModelState.IsValid)
                    {
                        ParentCategory pcat = new ParentCategory();
                        pcat.PCName = Pcategory.PCName;
                      
                        pcat.CreatedOn = DateTime.Now;
                        pcat.CreatedBy = Session["Id"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            pcat.PCStatus = 1;
                        }
                        else
                        {
                            pcat.PCStatus = 0;
                        }

                        db.ParentCategories.Add(pcat);
                        db.SaveChanges();
                        return Content("<script>alert('Data Successfully Submitted');location.href='../ParentCategory/Index';</script>");



                    }
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }

        public JsonResult GetParentCategoryDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    var Parentcat = (from c in db.ParentCategories
                                          where c.PCStatus != 0
                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var getParentCat = (from c in Parentcat

                                           select new
                                           {

                                               PCName = c.PCName,
                                               status = c.PCStatus == 1 ? "Enable" : "Disable",
                                               id = c.Pcid

                                           }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                    if (Parentcat.Count() % 15 == 0)
                    {
                        totalrecord = Parentcat.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (Parentcat.Count() / 15) + 1;
                    }
                    var data = new { result = getParentCat, TotalRecord = totalrecord };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

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
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    ParentCategory contactUs = db.ParentCategories.Single(a => a.Pcid == id);
                    contactUs.PCStatus = 0;
                    contactUs.ModifyOn = DateTime.Now;
                    contactUs.ModifyBy = Session["Id"].ToString();
                    int affectedReocrds = db.SaveChanges();
                    if (affectedReocrds > 0)
                    {
                        ParentCategoryhistory CUDHistory = new ParentCategoryhistory();
                        CUDHistory.Pcid= contactUs.Pcid;
                      
                        CUDHistory.PCName = contactUs.PCName;
                        CUDHistory.PCStatus = 0;

                        CUDHistory.ModifyBy = Session["Id"].ToString();
                        CUDHistory.ModifyOn = DateTime.Now;
                        db.ParentCategoryhistories.Add(CUDHistory);
                        db.SaveChanges();
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    ParentCategory cud = db.ParentCategories.Single(a => a.Pcid == id);
                    ViewBag.status = cud.PCStatus;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult Update(ParentCategory parentcate, string statusC)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    if (db.ParentCategories.Any(a => a.PCName.ToLower() == parentcate.PCName.ToLower() && a.Pcid != parentcate.Pcid && a.PCStatus != 0))
                    {
                        ModelState.AddModelError("PCName", "Product Category Already Exists");
                    }
                    

                    if (ModelState.IsValid)
                    {
                        ParentCategory parentcatogry = db.ParentCategories.Single(a => a.Pcid == parentcate.Pcid);

                        //Save Previous Record In History
                        ParentCategoryhistory CUDHistory = new ParentCategoryhistory();
                        CUDHistory.Pcid = parentcatogry.Pcid;
                      
                        CUDHistory.PCName = parentcatogry.PCName;
                        CUDHistory.PCStatus = parentcatogry.PCStatus;
                        CUDHistory.ModifyBy = Session["Id"].ToString();
                        CUDHistory.ModifyOn = DateTime.Now;
                        db.ParentCategoryhistories.Add(CUDHistory);

                        //Save New Record In Table
                      
                        parentcatogry.PCName = parentcate.PCName;
                        parentcatogry.ModifyOn = DateTime.Now;
                        parentcatogry.ModifyBy = Session["Id"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            parentcatogry.PCStatus = 1;
                        }
                        else
                        {
                            parentcatogry.PCStatus = 0;
                        }
                        if (db.SaveChanges() > 0)
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

    }
}
