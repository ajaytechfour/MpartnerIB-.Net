using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Data;
using System.IO;
namespace LuminousMpartnerIB.Controllers
{
    public class ProductCategoryController : Controller
    {
        //
        // GET: /ProductCategory/

        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/ProductCategory/index";
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
        public ActionResult SaveContact(ProductCatergory contactUs, string statusC, string pcId, HttpPostedFileBase Postedfile)
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

                    int pcid;
                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("ParentCatid", "Parent Category Has Incorrect Value");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("ParentCatid", "Select Parent Category");
                    }
                    if (db.ProductCatergories.Any(a => a.PName.ToLower() == contactUs.PName.ToLower() && a.Pstatus != 2))
                    {
                        ModelState.AddModelError("PName", "Product Category Already Exists");
                    }
                    //if (db.ProductCatergories.Any(a => a.PCode.ToLower() == contactUs.PCode.ToLower() && a.Pstatus != 2))
                    //{
                    //    ModelState.AddModelError("PCode", "Product Code Already Exists");
                    //}
                    if (Postedfile == null)
                    {
                        ModelState.AddModelError("File", "");
                        ViewBag.File = "File Is Not Uploaded";
                    }
                    else
                    {
                        string FileExtension = Path.GetExtension(Postedfile.FileName);
                        if (FileExtension.ToLower() == ".pdf")
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("File", "*");
                            ViewBag.File = "File Extention Should Be In .Pdf";
                        }



                    }
                    if (ModelState.IsValid)
                    {
                        string productpdf = "";
                        string filename = Path.GetFileNameWithoutExtension(Postedfile.FileName.Trim()) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(Postedfile.FileName);
                        productpdf = filename.Replace(" ", string.Empty);
                        string Imagename = filename;
                        ProductCatergory contactusd = new ProductCatergory();
                        contactusd.PCode = "";
                        contactusd.PName = contactUs.PName;
                        contactusd.ParentCatid = pcid;
                        contactusd.PdfOriginalName = Path.GetFileName(Postedfile.FileName);
                        contactusd.PdfSystemName = productpdf;
                        contactusd.CreateDate = DateTime.Now;
                        contactusd.CreatedBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.Pstatus = 1;
                        }
                        else
                        {
                            contactusd.Pstatus = 0;
                        }

                        db.ProductCatergories.Add(contactusd);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            string str = Path.Combine(Server.MapPath("~/MpartnerNewApi/ProductPDF/"), productpdf);
                            Postedfile.SaveAs(str);
                            return Content("<script>alert('Data Successfully Submitted');location.href='../ProductCategory/Index';</script>");
                        }
                        else
                        {
                            return Content("<script>alert('Product Category Not Created');location.href='/ProductCategory/Index';</script>");
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

        public JsonResult GetContactDetail()
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
                    var contactdetails = (from c in db.ProductCatergories
                                          where c.Pstatus != 2
                                          select c).ToList();
                    int totalrecord;
                    //if (page != null)
                    //{
                    //    page = (page - 1) * 15;
                    //}

                    var contactDetails2 = (from c in contactdetails

                                           select new
                                           {
                                               ParentCat = c.ParentCategory.PCName,
                                               // PCode = c.PCode,
                                               PName = c.PName,
                                               status = c.Pstatus == 1 ? "Enable" : "Disable",
                                               id = c.id

                                           }).OrderByDescending(a => a.id).ToList();
                    //if (contactdetails.Count() % 15 == 0)
                    //{
                    //    totalrecord = contactdetails.Count() / 15;
                    //}
                    //else
                    //{
                    //    totalrecord = (contactdetails.Count() / 15) + 1;
                    //}
                    var data = new { result = contactDetails2, TotalRecord = contactDetails2.Count };

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
                    ProductCatergory contactUs = db.ProductCatergories.Single(a => a.id == id);
                    contactUs.Pstatus = 2;
                    contactUs.ModifiedDate = DateTime.Now;
                    contactUs.ModifiedBy = Session["userid"].ToString();
                    int affectedReocrds = db.SaveChanges();
                    if (affectedReocrds > 0)
                    {
                        ProductCatergoryHistory CUDHistory = new ProductCatergoryHistory();
                        CUDHistory.pid = contactUs.id;
                        CUDHistory.PCode = contactUs.PCode;
                        CUDHistory.PName = contactUs.PName;
                        CUDHistory.Pstatus = 2;

                        CUDHistory.ModifiedBy = Session["userid"].ToString();
                        CUDHistory.ModifiedDate = DateTime.Now;
                        db.ProductCatergoryHistories.Add(CUDHistory);
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
                    ProductCatergory cud = db.ProductCatergories.Single(a => a.id == id);
                    ViewBag.status = cud.Pstatus;
                    ViewBag.Pid = cud.ParentCatid;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult Update(ProductCatergory contactUs, string statusC, string pcId, HttpPostedFileBase Postedfile)
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
                    int pcid;
                    ViewBag.status = statusC;
                    ViewBag.Pid = pcId;
                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("pcid", "Parent Category Has Incorrect Value");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("pcid", "*");
                    }

                    if (db.ProductCatergories.Any(a => a.PName.ToLower() == contactUs.PName.ToLower() && a.id != contactUs.id && a.Pstatus != 2))
                    {
                        ModelState.AddModelError("PName", "Product Category Already Exists");
                    }


                    if (Postedfile != null)
                    {
                        string FileExtension = Path.GetExtension(Postedfile.FileName);
                        if (FileExtension.ToLower() == ".pdf")
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("File", "*");
                            ViewBag.File = "File Extention Should Be In .Pdf";
                        }



                    }




                    //if (db.ProductCatergories.Any(a => a.PCode.ToLower() == contactUs.PCode.ToLower() && a.id != contactUs.id && a.Pstatus != 2))
                    //{
                    //    ModelState.AddModelError("PCode", "Product Code Already Exists");
                    //}

                    if (ModelState.IsValid)
                    {
                        string productpdf = "";
                        string filename = "";
                        string Imagename = "";
                        if (Postedfile != null)
                        {

                            filename = Path.GetFileNameWithoutExtension(Postedfile.FileName.Trim()) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(Postedfile.FileName);
                            productpdf = filename.Replace(" ", string.Empty);
                            Imagename = filename;
                        }

                        ProductCatergory contactusd = db.ProductCatergories.Single(a => a.id == contactUs.id);

                        //Save Previous Record In History
                        ProductCatergoryHistory CUDHistory = new ProductCatergoryHistory();
                        CUDHistory.pid = contactusd.id;
                        CUDHistory.PCode = "";
                        CUDHistory.PName = contactusd.PName;
                        CUDHistory.Pstatus = contactusd.Pstatus;
                        CUDHistory.PdfOriginalName = contactusd.PdfOriginalName;
                        CUDHistory.PdfSystemName = contactusd.PdfSystemName;
                        CUDHistory.ModifiedBy = Session["userid"].ToString();
                        CUDHistory.ModifiedDate = DateTime.Now;
                        db.ProductCatergoryHistories.Add(CUDHistory);

                        //Save New Record In Table
                        if (Postedfile != null)
                        {
                            contactusd.PdfOriginalName = Path.GetFileName(Postedfile.FileName);
                            contactusd.PdfSystemName = productpdf;
                        }

                        contactusd.PCode = "";
                        contactusd.PName = contactUs.PName;
                        contactusd.ModifiedDate = DateTime.Now;
                        contactusd.ParentCatid = pcid;
                        contactusd.ModifiedBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.Pstatus = 1;
                        }
                        else
                        {
                            contactusd.Pstatus = 0;
                        }
                        if (db.SaveChanges() > 0)
                        {
                            if (Postedfile != null)
                            {
                                string str = Path.Combine(Server.MapPath("~/MpartnerNewApi/ProductPDF/"), filename);
                                Postedfile.SaveAs(str);
                            }
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

        public JsonResult GetParentCategory()
        {
            if ((Session["userid"] == null && Session["Ioslogin"] == null))
            {


                return Json("Login", JsonRequestBehavior.AllowGet);

            }

            else
            {
                string pageUrl2 = "";
                if (Session["Ioslogin"] != null || Session["ASMEmpCode"] != null)
                {
                    //dt = Session["permission"] as DataTable;
                    //pageUrl2 = "/CreateContest/Index";
                    //  DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                }
                else
                {
                    //dt = Session["permission"] as DataTable;
                    //pageUrl2 = PageUrl;
                }
                //  DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    var getParentCat = (from c in db.ParentCategories
                                        where c.PCStatus != 0
                                        select new
                                        {
                                            id = c.Pcid,
                                            Name = c.PCName
                                        }).ToList();
                    return Json(getParentCat, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }

    }
}
