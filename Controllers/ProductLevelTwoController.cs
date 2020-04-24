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
    public class ProductLevelTwoController : Controller
    {
        //
        // GET: /ProductLevelTwo/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/ProductLevelTwo/index";
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
        public JsonResult GetProductLevelOneCategory()
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
                    //int ProcatID;
                    //int.TryParse(procatid, out ProcatID);
                    var Company = (from c in db.ProductLevelOnes
                                   where c.PlStatus != 2 && c.PlStatus != 0
                                   select new
                                   {
                                       id = c.id,
                                       Name = c.Name
                                   }).ToList();

                    return Json(Company, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }
        public JsonResult GetProductCategory(int procatid)
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
                    var Company = (from c in db.ProductCatergories
                                   where c.Pstatus != 2 && c.Pstatus != 0 && c.ParentCatid == procatid
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
        //public JsonResult GetProductLevelOneCategory()
        //{
        //    if (Session["userid"] == null)
        //    {
        //        return Json("Login", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        dt = Session["permission"] as DataTable;
        //        string pageUrl2 = PageUrl;
        //        DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
        //        if (result[0]["uview"].ToString() == "1")
        //        {
        //            //int ProcatID;
        //            //int.TryParse(procatid, out ProcatID);
        //            var Company = (from c in db.ProductLevelOne_New
        //                           where c.PlStatus != 2 && c.PlStatus != 0
        //                           select new
        //                           {
        //                               id = c.id,
        //                               Name = c.Name
        //                           }).ToList();

        //            return Json(Company, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json("snotallowed", JsonRequestBehavior.AllowGet);

        //        }
        //    }
        //}
        public ActionResult SaveContact(ProductLevelTwo contactUs, string statusC, string pcId, string ParentCatid)
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
                    int productCat1, parntcate;

                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("PrductID", "Product Category Has Incorrect Value");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("PrductID", "Select Product Category");
                    }

                    //if (!int.TryParse(ProductCat1, out productCat1))
                    //{
                    //    ModelState.AddModelError("pc_Lvl_oneId", "Product Level One Has Incorrect Value");

                    //}
                    //if (productCat1 == 0)
                    //{
                    //    ModelState.AddModelError("pc_Lvl_oneId", "Select Product Level One");
                    //}
                    if (!int.TryParse(ParentCatid, out parntcate))
                    {
                        ModelState.AddModelError("ParentCatid", "Parent Category Has Incorrect Value");

                    }
                    //if (productCat1 == 0)
                    //{
                    //    ModelState.AddModelError("ParentCatid", "Select Parent Category");
                    //}

                    //if (db.ProductLevelTwoes.Any(a => a.PrductID == pcid && a.pc_Lvl_oneId == productCat1 && a.Name.ToLower() == contactUs.Name.ToLower() && a.PlTwStatus != 2))
                    //{
                    //    ModelState.AddModelError("Name", "Product Name Already Exists");
                    //}
                    //if (Postedfile == null)
                    //{
                    //    ModelState.AddModelError("File", "");
                    //    ViewBag.File = "File Is Not Uploaded";
                    //}
                    //else
                    //{
                    //    string FileExtension = Path.GetExtension(Postedfile.FileName);
                    //    if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                    //       FileExtension.ToLower() == ".png")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("File", "*");
                    //        ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                    //    }



                    //}
                    if (ModelState.IsValid)
                    {
                        //string Prdleveltwoimage = "";
                        //string filename = Path.GetFileNameWithoutExtension(Postedfile.FileName.Trim()) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(Postedfile.FileName);
                        //Prdleveltwoimage = filename.Replace(" ", string.Empty);
                        //string Imagename = filename;
                        ProductLevelTwo contactusd = new ProductLevelTwo();
                        contactusd.Name = contactUs.Name;
                        contactusd.PrductID = pcid;
                        contactusd.pc_Lvl_oneId = 0;
                        contactusd.ImageFileName = "";
                        contactusd.ImageName = "";
                        contactusd.ParentCatid = parntcate;
                        contactusd.CreatedBy = Session["id"].ToString();
                        contactusd.CreatedDate = DateTime.Now;
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.PlTwStatus = 1;
                        }
                        else
                        {
                            contactusd.PlTwStatus = 0;
                        }
                        db.ProductLevelTwoes.Add(contactusd);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {

                            return Content("<script>alert('Product Level Two Has Been Created Sucessfully');location.href='/ProductLeveltwo/Index';</script>");
                        }

                        else
                        {
                            return Content("<script>alert('Product Level Two Not Created');location.href='/ProductLeveltwo/Index';</script>");
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
                //int totalrecord = 0;
                if (true)
                {
                    var contactdetails = (from c in db.ProductLevelTwoes
                                          where c.PlTwStatus != 2
                                          select c).ToList();

                    //if (page != null)
                    //{
                    //    page = (page - 1) * 15;
                    //}
                    //if (contactdetails.Count() % 15 == 0)
                    //{
                    //    totalrecord = contactdetails.Count() / 15;
                    //}
                    //else
                    //{

                    //    totalrecord = (contactdetails.Count() / 15) + 1;


                    //}
                    //if (Session["Search"] != null)
                    //{


                    //    var contactDetails2 = (from c in contactdetails

                    //                           select new
                    //                           {
                    //                               id = c.id,
                    //                               PCat = c.ProductCatergory.PName,
                    //                               Name = c.Name,
                    //                               // proCatOne = c.ProductLevelOne.Name,
                    //                               parntcat = c.ParentCategory.PCName,
                    //                               status = c.PlTwStatus == 1 ? "Enable" : "Disable",


                    //                           }).Where(a => a.id.ToString().Contains(Session["Search"].ToString()) || a.PCat.Contains(Session["Search"].ToString()) || a.Name.Contains(Session["Search"].ToString()) || a.status.Contains(Session["Search"].ToString())).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();

                    //    if (contactDetails2.Count() == 0)
                    //    {
                    //        var data = new { result = contactDetails2 };

                    //        return Json(data, JsonRequestBehavior.AllowGet);
                    //    }
                    //    else
                    //    {
                    //        var data = new { result = contactDetails2, TotalRecord = totalrecord };

                    //        return Json(data, JsonRequestBehavior.AllowGet);
                    //    }
                    //}
                    //else
                    //{
                        var contactDetails2 = (from c in contactdetails

                                               select new
                                               {
                                                   id = c.id,
                                                 //  PCat = c.ProductCatergory.PName,
                                                   Name = c.Name,
                                                //   proCatOne = c.ProductLevelOne.Name,
                                                  // parntcat = c.ParentCategory.PCName,
                                                   status = c.PlTwStatus == 1 ? "Enable" : "Disable",


                                               }).OrderByDescending(a => a.id).ToList();

                        var data = new { result = contactDetails2, TotalRecord = contactDetails2.Count };

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
              //  }
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
                    ProductLevelTwo cud = db.ProductLevelTwoes.Single(a => a.id == id);
                    ViewBag.status = cud.PlTwStatus;
                    ViewBag.Pid = cud.PrductID;
                    //ViewBag.P1id = cud.pc_Lvl_oneId;
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
        public ActionResult Update(ProductLevelTwo contactUs, string statusC, string pcId, string ParentCatid)
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
                    ViewBag.status = statusC;
                    ViewBag.Pid = pcId;
                    //ViewBag.P1id = ProductCat1;
                    ViewBag.Prntid = ParentCatid;

                    int pcid, prntid;

                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("PrductID", "Product Category Has Incorrect Value");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("PrductID", "Select Product Category");
                    }

                    //if (!int.TryParse(ProductCat1, out productCat1))
                    //{
                    //    ModelState.AddModelError("pc_Lvl_oneId", "Product Level One Has Incorrect Value");

                    //}
                    //if (productCat1 == 0)
                    //{
                    //    ModelState.AddModelError("pc_Lvl_oneId", "Select Product level One");
                    //}
                    if (!int.TryParse(ParentCatid, out prntid))
                    {
                        ModelState.AddModelError("ParentCatid", "Parent Category Has Incorrect Value");

                    }
                    if (prntid == 0)
                    {
                        ModelState.AddModelError("ParentCatid", "*");
                    }
                    if (db.ProductLevelTwoes.Any(a => a.PrductID == pcid && a.Name.ToLower() == contactUs.Name.ToLower() && a.id != contactUs.id && a.PlTwStatus != 2))
                    {
                        ModelState.AddModelError("Name", "Product Name With Selected Product Category Is  Already Exists");
                    }

                    //if (postedfile != null)
                    //{
                    //    string FileExtension = Path.GetExtension(postedfile.FileName);
                    //    if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                    //       FileExtension.ToLower() == ".png")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("File", "*");
                    //        ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                    //    }



                    //}
                    if (ModelState.IsValid)
                    {
                        string filename = "";
                        ProductLevelTwo contactusd = db.ProductLevelTwoes.Single(a => a.id == contactUs.id);

                        ProductLevelTwoHistory CUDHistory = new ProductLevelTwoHistory();
                        CUDHistory.ProductlevlTwoId = contactusd.id;
                        CUDHistory.ProductId = contactusd.PrductID;
                        CUDHistory.Name = contactusd.Name;
                        CUDHistory.pc_Lvl_oneId = 0;
                        CUDHistory.PlTwStatus = contactusd.PlTwStatus;
                        CUDHistory.ImageFileName = "";
                        CUDHistory.ImageName = "";
                        CUDHistory.ModifiedBy = Session["userid"].ToString();
                        CUDHistory.ModifiedDate = DateTime.Now;
                        db.ProductLevelTwoHistories.Add(CUDHistory);


                        contactusd.Name = contactUs.Name;
                        contactusd.PrductID = pcid;
                        //if (postedfile != null)
                        //{
                        //    filename = Path.GetFileNameWithoutExtension(postedfile.FileName.Trim()) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedfile.FileName);
                        //    contactusd.ImageFileName = Path.GetFileName(postedfile.FileName);
                        //    contactusd.ImageName = filename;
                        //}
                        //else
                        //{
                        contactusd.ImageFileName = "";
                        contactusd.ImageName = "";
                        // }
                        contactusd.ParentCatid = prntid;
                        contactusd.ModifiedDate = DateTime.Now;
                        contactusd.ModifiedBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.PlTwStatus = 1;
                        }
                        else
                        {
                            contactusd.PlTwStatus = 0;
                        }


                        int affectedRows = db.SaveChanges();
                        if (affectedRows > 0)
                        {
                            //if (postedfile != null)
                            //{
                            //    string str = Path.Combine(Server.MapPath("~/ProductImages/"), filename);
                            //    postedfile.SaveAs(str);
                            //}

                            ViewBag.Result = "Record Updated Successfully";


                        }


                    }
                    ProductLevelTwo pd2 = db.ProductLevelTwoes.Single(a => a.id == contactUs.id);

                    return View("Edit", pd2);
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
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    ProductLevelTwo contactUs = db.ProductLevelTwoes.Single(a => a.id == id);

                    ProductLevelTwoHistory CUDHistory = new ProductLevelTwoHistory();
                    CUDHistory.ProductlevlTwoId = contactUs.id;
                    CUDHistory.ProductId = contactUs.PrductID;
                    CUDHistory.pc_Lvl_oneId = contactUs.pc_Lvl_oneId;
                    CUDHistory.Name = contactUs.Name;
                    CUDHistory.PlTwStatus = contactUs.PlTwStatus;

                    CUDHistory.ModifiedBy = Session["userid"].ToString();
                    CUDHistory.ModifiedDate = DateTime.Now;
                    db.ProductLevelTwoHistories.Add(CUDHistory);


                    contactUs.PlTwStatus = 2;
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
