using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.IO;
using PagedList;
using PagedList.Mvc;
using System.Data;
using System.Web.Routing;
namespace Luminous.Controllers
{
    public class LuminiousUpdateController : Controller
    {
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();
        public ActionResult Index()
        {
            RouteCollection rc = RouteTable.Routes;
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LuminiousUpdate/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    var data = db.luminious_Update.OrderByDescending(a => a.Id).ToList().ToPagedList(1, 1);
                    return View(db.luminious_Update.Where(a => a.Id == null).OrderByDescending(a => a.Id).ToList().ToPagedList(1, 1));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Save(string Title, string Textbody, string StartDate, string EndDate, string VideoURL, string Status, HttpPostedFileBase postedFile, string ParentCatid, string pcId, string ProductLvl1,
            string ProductLvl2, string ProductLvl3, string Media, string redirectpage)
        {


            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LuminiousUpdate/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["createrole"].ToString() == "1")
                {
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;
                    Status = Status ?? "off";
                    #region Check Validation

                    //#region Title Validations
                    //if (Title == null)
                    //{
                    //    ModelState.AddModelError("Title", "Title Is Required");
                    //}
                    //else
                    //{
                    //    if (Title == "")
                    //    {
                    //        ModelState.AddModelError("Title", "Title Is Required");
                    //    }
                    //    else if (Title.Length > 70)
                    //    {
                    //        ModelState.AddModelError("Title", "Characters In Title Should Be Less Than 70");
                    //    }

                    //}
                    //#endregion




                    #region Description
                    if (Textbody == null)
                    {
                        ModelState.AddModelError("Textbody", "Descriptions Is Required");
                    }
                    else
                    {
                        if (Textbody == "")
                        {
                            ModelState.AddModelError("Textbody", "Descriptions Is Required");
                        }
                        else if (Textbody.Length > 499)
                        {
                            ModelState.AddModelError("Textbody", "Characters In Discriptions Should Be Less Than 500");
                        }

                    }
                    #endregion
                    #region Date
                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "*");
                        ViewBag.StartDate = "Start Date Is Not Selected";
                    }
                    else
                    {
                        try
                        {

                            if (Convert.ToDateTime(StartDate) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                            {
                                ModelState.AddModelError("StartDate", "Start Date Should Be Greater Than or Equal To Current Date");
                                ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                            }
                            //DateTime startDate = Convert.ToDateTime(StartDate);
                            //if (db.Banners.Any(a => a.ExpriyDate >= startDate && a.BStatus != 2 && a.BStatus != 2))
                            //{
                            //    ModelState.AddModelError("StartDate", "There Is already A Banner Defined In This Date");
                            //    ViewBag.StartDate = "There Is already A Banner Defined In This Date";
                            //}
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("StartDate", "Invalid Start Date");
                            ViewBag.StartDate = "Invalid Start Date";
                        }

                    }
                    if (EndDate == null || EndDate == "")
                    {
                        ModelState.AddModelError("ExpriyDate", "*");
                        ViewBag.EndDate = "End Date Is Not Selected";
                    }
                    else
                    {
                        DateTime startDate = new DateTime();
                        try
                        {
                            startDate = Convert.ToDateTime(StartDate);
                            try
                            {
                                if (Convert.ToDateTime(EndDate) < startDate)
                                {
                                    ModelState.AddModelError("ExpriyDate", "*");
                                    ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                                }
                            }
                            catch (FormatException ex)
                            {
                                ModelState.AddModelError("ExpriyDate", "Invalid End Date");
                                ViewBag.EndDate = "Invalid End Date";
                            }
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("ExpriyDate", "Invalid End Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }

                    }

                    //if (postedFile == null)
                    //{
                    //    ModelState.AddModelError("File", "*");
                    //    ViewBag.File = "File Is Not Uploaded";
                    //}
                    //else
                    //{
                    //    string FileExtension = Path.GetExtension(postedFile.FileName);
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
                    if (postedFile != null)
                    {
                        string FileExtension = Path.GetExtension(postedFile.FileName);
                        if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                           FileExtension.ToLower() == ".png")
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("File", "*");
                            ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                        }



                    }

                    #endregion
                    #endregion

                    #region redirect validation

                    if (redirectpage == "0")
                    {
                        ModelState.AddModelError("RedirectPage", "Select Redirect Page");
                    }

                    #endregion
                    //#region Parent Category validation

                    //if (ParentCatid == "0")
                    //{
                    //    ModelState.AddModelError("ParentCatid", "Select Parent Category");
                    //}

                    //#endregion
                    #region Product level  validation
                    if (redirectpage != "5")
                    {
                        if (redirectpage == "1")
                        {
                            if (ProductLvl1 == "0")
                            {
                                ModelState.AddModelError("ProductLevelOne", "Select Product Level One");
                            }
                            if (ProductLvl2 == "0")
                            {
                                ModelState.AddModelError("ProductLeveltwo", "Select Product Level Two");
                            }
                            if (ProductLvl3 == "0")
                            {
                                ModelState.AddModelError("ProductLevelThreeId", "Select Product Level Three");
                            }
                            if (ParentCatid == "0")
                            {
                                ModelState.AddModelError("ParentCatid", "Select Parent Category");
                            }
                            if (pcId == "0")
                            {
                                ModelState.AddModelError("CategoryId", "Select Product Category");
                            }
                        }

                        if (redirectpage == "2" || redirectpage == "3")
                        {
                            if (ProductLvl1 == "0")
                            {
                                ModelState.AddModelError("ProductLevelOne", "Select Product Level One");
                            }
                            if (ParentCatid == "0")
                            {
                                ModelState.AddModelError("ParentCatid", "Select Parent Category");
                            }
                            if (pcId == "0")
                            {
                                ModelState.AddModelError("CategoryId", "Select Product Category");
                            }
                        }
                        if (redirectpage == "4")
                        {

                            if (ParentCatid == "0")
                            {
                                ModelState.AddModelError("ParentCatid", "Select Parent Category");
                            }
                            //if (pcId == "0")
                            //{
                            //    ModelState.AddModelError("CategoryId", "Select Product Category");
                            //}
                            if (Media == "0")
                            {
                                ModelState.AddModelError("Media", "Select Media");
                            }
                        }
                    }
                    #endregion
                    if (ModelState.IsValid)
                    {
                        string Imagename = "";
                        if (postedFile != null)
                        {
                            string filename = Path.GetFileNameWithoutExtension(postedFile.FileName.Trim()) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                            Imagename = filename.Replace(" ", string.Empty);
                        }
                        else
                        {
                            Imagename = "";
                        }
                        luminious_Update lmupdate = new luminious_Update();
                        lmupdate.Title = Title;

                        lmupdate.Textbody = Textbody;
                        lmupdate.StartDate = Convert.ToDateTime(StartDate);
                        lmupdate.Expirydate = Convert.ToDateTime(EndDate);
                        if (postedFile == null)
                        {
                            lmupdate.ImageFileName = "";
                        }
                        else
                        {
                            lmupdate.ImageFileName = Path.GetFileName(postedFile.FileName);
                        }

                       
                        lmupdate.ImageName = Imagename;
                        lmupdate.CreatedOn = DateTime.Now;
                        lmupdate.Createdby = Session["userid"].ToString();
                        if (Status.ToLower() == "on")
                        {
                            lmupdate.Status = 1;

                        }
                        else
                        {
                            lmupdate.Status = 0;
                        }
                        lmupdate.ParentCatid = Convert.ToInt32(ParentCatid);
                        lmupdate.ProductLevelOne = Convert.ToInt32(ProductLvl1);
                        lmupdate.ProductLeveltwo = Convert.ToInt32(ProductLvl2);
                        lmupdate.ProductLevelThreeId = Convert.ToInt32(ProductLvl3);
                        lmupdate.CategoryId = Convert.ToInt32(pcId);
                        lmupdate.Media = Convert.ToInt32(Media);
                        lmupdate.RedirectPage = Convert.ToInt32(redirectpage);
                        db.luminious_Update.AddObject(lmupdate);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            string str = "";
                            if (postedFile != null)
                            {
                                str = Path.Combine(Server.MapPath("~/LuminiousUpdateImage/"), Imagename);
                                postedFile.SaveAs(str);
                            }


                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                    }
                    return View("Index", db.luminious_Update.ToList().ToPagedList(1, 5));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }


        public JsonResult GetLuminiousDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LuminiousUpdate/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    var Luminiosdetails = (from c in db.luminious_Update
                                           where c.Status != 2
                                           select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var Bannerdetails2 = (from c in Luminiosdetails
                                          join rd in db.Redirectdatas on c.RedirectPage equals rd.Id into joinGroup
                                          from gr in joinGroup.DefaultIfEmpty()
                                          
                                          select new
                                          {
                                              Title = c.Title,
                                              TextBody = c.Textbody,
                                              status = c.Status == 1 ? "Enable" : "Disable",
                                             PageName=gr != null ? gr.PageName : null,
                                              startdate = Convert.ToDateTime(c.StartDate).ToShortDateString(),
                                              id = c.Id,
                                              expiryDate = Convert.ToDateTime(c.Expirydate).ToShortDateString(),
                                              LuminiousImage = c.ImageName
                                          }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                    if (Bannerdetails2.Count() % 15 == 0)
                    {
                        totalrecord = Bannerdetails2.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (Bannerdetails2.Count() / 15) + 1;
                    }
                    var data = new { result = Bannerdetails2, TotalRecord = totalrecord };

                    return Json(data, JsonRequestBehavior.AllowGet);
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
                string pageUrl2 = "/LuminiousUpdate/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    try
                    {
                        luminious_Update lmupdate = db.luminious_Update.Single(a => a.Id == id && a.Status != 2);
                        ViewBag.preStartDate = Convert.ToDateTime(lmupdate.StartDate).ToString("dd-MM-yyyy");
                        ViewBag.PreEndDate = Convert.ToDateTime(lmupdate.Expirydate).ToString("dd-MM-yyyy");
                        ViewBag.preStatus = lmupdate.Status;
                        ViewBag.Update = "";
                        ViewBag.ProductCat = lmupdate.CategoryId;
                        ViewBag.Productlvl1 = lmupdate.ProductLevelOne;
                        ViewBag.Productlvl2 = lmupdate.ProductLeveltwo;
                        ViewBag.Productlvl3 = lmupdate.ProductLevelThreeId;
                        ViewBag.Prntid = lmupdate.ParentCatid;
                        ViewBag.media = lmupdate.Media;
                        ViewBag.Redirectpage = lmupdate.RedirectPage;
                        return View(db.luminious_Update.Single(a => a.Id == id));
                    }
                    catch
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Update(string id, string Title, string Textbody, string StartDate, string EndDate, string VideoURL, string Status, HttpPostedFileBase postedFile, string ParentCatid, string pcId, string ProductLvl1,
            string ProductLvl2, string ProductLvl3, string Media, string redirectpage)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LuminiousUpdate/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    #region Check Validation
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;
                    int intid = int.Parse(id);
                    Status = Status ?? "off";
                    //#region Title Validations
                    //if (Title == null)
                    //{
                    //    ModelState.AddModelError("Title", "Title Is Required");
                    //}
                    //else
                    //{
                    //    if (Title == "")
                    //    {
                    //        ModelState.AddModelError("Title", "Title Is Required");
                    //    }
                    //    else if (Title.Length > 70)
                    //    {
                    //        ModelState.AddModelError("Title", "Characters In Title Should Be Less Than 70");
                    //    }

                    //}
                    //#endregion




                    #region Description
                    if (Textbody == null)
                    {
                        ModelState.AddModelError("Description", "Descriptions Is Required");
                    }
                    else
                    {
                        if (Textbody == "")
                        {
                            ModelState.AddModelError("Description", "Descriptions Is Required");
                        }
                        else if (Textbody.Length > 499)
                        {
                            ModelState.AddModelError("Description", "Characters In Discriptions Should Be Less Than 500");
                        }

                    }
                    #endregion
                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "*");
                        ViewBag.StartDate = "Start Date Is Not Selected";
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDateTime(StartDate) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                            {
                                //ModelState.AddModelError("StartDate", "*");
                                //ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                            }
                            //DateTime startDate = Convert.ToDateTime(StartDate);
                            //if (db.Banners.Any(a => a.ExpriyDate >= startDate && a.id != intid && a.BStatus != 2))
                            //{
                            //    ModelState.AddModelError("StartDate", "*");
                            //    ViewBag.StartDate = "There Is already A Banner Defined In This Date";
                            //}
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("StartDate", "*");

                            ViewBag.StartDate = "Invalid Date";
                        }
                    }
                    if (EndDate == null || EndDate == "")
                    {
                        ModelState.AddModelError("ExpriyDate", "*");
                        ViewBag.EndDate = "End Date Is Not Selected";
                    }
                    else
                    {
                        DateTime startDate = new DateTime();
                        try
                        {
                            startDate = Convert.ToDateTime(StartDate);
                            try
                            {
                                if (Convert.ToDateTime(EndDate) < startDate)
                                {
                                    ModelState.AddModelError("ExpriyDate", "*");
                                    ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                                }
                            }
                            catch (FormatException ex)
                            {
                                ModelState.AddModelError("ExpriyDate", "Invalid End Date");
                                ViewBag.EndDate = "Invalid End Date";
                            }
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("ExpriyDate", "Invalid End Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }
                    }
                    #endregion
                    #region redirect validation

                    if (redirectpage == "0")
                    {
                        ModelState.AddModelError("RedirectPage", "Select Redirect Page");
                    }

                    #endregion
                    //#region Parent Category validation

                    //if (ParentCatid == "0")
                    //{
                    //    ModelState.AddModelError("ParentCatid", "Select Parent Category");
                    //}

                    //#endregion
                    #region Product level  validation
                    if (redirectpage != "5")
                    {
                        if (redirectpage == "1")
                        {
                            if (ProductLvl1 == "0")
                            {
                                ModelState.AddModelError("ProductLevelOne", "Select Product Level One");
                            }
                            if (ProductLvl2 == "0")
                            {
                                ModelState.AddModelError("ProductLeveltwo", "Select Product Level Two");
                            }
                            if (ProductLvl3 == "0")
                            {
                                ModelState.AddModelError("ProductLevelThreeId", "Select Product Level Three");
                            }
                            if (ParentCatid == "0")
                            {
                                ModelState.AddModelError("ParentCatid", "Select Parent Category");
                            }
                            if (pcId == "0")
                            {
                                ModelState.AddModelError("CategoryId", "Select Product Category");
                            }
                        }

                        if (redirectpage == "2" || redirectpage == "3")
                        {
                            if (ProductLvl1 == "0")
                            {
                                ModelState.AddModelError("ProductLevelOne", "Select Product Level One");
                            }
                            if (ParentCatid == "0")
                            {
                                ModelState.AddModelError("ParentCatid", "Select Parent Category");
                            }
                            if (pcId == "0")
                            {
                                ModelState.AddModelError("CategoryId", "Select Product Category");
                            }
                        }
                        if (redirectpage == "4")
                        {

                            if (ParentCatid == "0")
                            {
                                ModelState.AddModelError("ParentCatid", "Select Parent Category");
                            }

                            if (Media == "0")
                            {
                                ModelState.AddModelError("Media", "Select Media");
                            }
                        }
                    }
                    #endregion
                    if (ModelState.IsValid)
                    {
                        if (postedFile == null)
                        {
                            try
                            {

                                luminious_Update LmUpdate = db.luminious_Update.Single(a => a.Id == intid && a.Status != 2);
                                luminious_UpdateHistory LmHistory = new luminious_UpdateHistory();
                                LmHistory.Lum_UpdateID = LmUpdate.Id;
                                LmHistory.Textbody = LmUpdate.Textbody.TrimEnd();
                                LmHistory.ImageFileName = LmUpdate.ImageFileName;
                                LmHistory.ImageName = LmUpdate.ImageName;
                             
                                LmHistory.Status = LmUpdate.Status;
                                LmHistory.Expirydate = LmUpdate.Expirydate;
                                LmHistory.Title = LmUpdate.Title;
                                LmHistory.ModifyBy = Session["userid"].ToString();
                                LmHistory.ModifyOn = DateTime.Now;
                                LmHistory.StartDate = LmUpdate.StartDate;
                                LmHistory.ParentCatid = LmUpdate.ParentCatid;
                                LmHistory.CategoryId = LmUpdate.CategoryId;
                                LmHistory.ProductLevelOne = LmUpdate.ProductLevelOne;
                                LmHistory.ProductLeveltwo = LmUpdate.ProductLeveltwo;
                                LmHistory.ProductLevelThreeId = LmUpdate.ProductLevelThreeId;
                                LmHistory.Media = LmUpdate.Media;
                                LmHistory.RedirectPage = LmUpdate.RedirectPage;
                                db.luminious_UpdateHistory.AddObject(LmHistory);

                                LmUpdate.Title = Title;
                              
                                LmUpdate.Textbody = Textbody;
                                LmUpdate.ParentCatid = Convert.ToInt32(ParentCatid);
                                LmUpdate.CategoryId = Convert.ToInt32(pcId);
                                LmUpdate.ProductLevelOne = Convert.ToInt32(ProductLvl1);
                                LmUpdate.ProductLeveltwo = Convert.ToInt32(ProductLvl2);
                                LmUpdate.ProductLevelThreeId = Convert.ToInt32(ProductLvl3);
                                LmUpdate.Media = Convert.ToInt32(Media);
                                LmUpdate.RedirectPage = Convert.ToInt32(redirectpage);
                                LmUpdate.StartDate = Convert.ToDateTime(StartDate);
                                LmUpdate.Expirydate = Convert.ToDateTime(EndDate);
                                LmUpdate.ModifyBy = Session["userid"].ToString();
                                LmUpdate.ModifyOn = DateTime.Now;
                                if (Status.ToLower() == "on")
                                {
                                    LmUpdate.Status = 1;

                                }
                                else
                                {
                                    LmUpdate.Status = 0;
                                }
                                int affectedRows = db.SaveChanges();
                                if (affectedRows > 0)
                                {

                                    ViewBag.Update = "Done";

                                }
                            }
                            catch
                            {
                                return View("Index");
                            }
                        }
                        else
                        {

                            string FileExtension = Path.GetExtension(postedFile.FileName);
                            if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                               FileExtension.ToLower() == ".png")
                            {

                            }
                            else
                            {
                                ModelState.AddModelError("File", "*");
                                ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                            }

                            if (ModelState.IsValid)
                            {
                                try
                                {
                                    string filename = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                                    string Imagename = filename.Replace(" ", string.Empty);
                                    luminious_Update LmUpdate = db.luminious_Update.Single(a => a.Id == intid && a.Status != 2);

                                    //Save Previous Record In History
                                    luminious_UpdateHistory LmHistory = new luminious_UpdateHistory();
                                    LmHistory.Lum_UpdateID = LmUpdate.Id;
                                    LmHistory.Textbody = LmUpdate.Textbody.TrimEnd();
                                    LmHistory.ImageFileName = LmUpdate.ImageFileName;
                                    LmHistory.ImageName = LmUpdate.ImageName;
                                    LmHistory.ParentCatid = LmUpdate.ParentCatid;
                                    LmHistory.CategoryId = LmUpdate.CategoryId;
                                    LmHistory.ProductLevelOne = LmUpdate.ProductLevelOne;
                                    LmHistory.ProductLeveltwo = LmUpdate.ProductLeveltwo;
                                    LmHistory.ProductLevelThreeId = LmUpdate.ProductLevelThreeId;
                                    LmHistory.Media = LmUpdate.Media;
                                    LmHistory.RedirectPage = LmUpdate.RedirectPage;
                                    LmHistory.Status = LmUpdate.Status;
                                    LmHistory.Expirydate = LmUpdate.Expirydate;
                                    LmHistory.Title = LmUpdate.Title;
                                    LmHistory.ModifyBy = Session["userid"].ToString();
                                    LmHistory.ModifyOn = DateTime.Now;
                                    LmHistory.StartDate = LmUpdate.StartDate;

                                    db.luminious_UpdateHistory.AddObject(LmHistory);


                                    //Save New Record



                                    LmUpdate.Title = Title;

                                    LmUpdate.Textbody = Textbody;
                                    LmUpdate.StartDate = Convert.ToDateTime(StartDate);
                                    LmUpdate.Expirydate = Convert.ToDateTime(EndDate);
                                    LmUpdate.ImageFileName = Path.GetFileName(postedFile.FileName);
                                  
                                    LmUpdate.ImageName = Imagename;
                                    LmUpdate.ParentCatid = Convert.ToInt32(ParentCatid);
                                    LmUpdate.CategoryId = Convert.ToInt32(pcId);
                                    LmUpdate.ProductLevelOne = Convert.ToInt32(ProductLvl1);
                                    LmUpdate.ProductLeveltwo = Convert.ToInt32(ProductLvl2);
                                    LmUpdate.ProductLevelThreeId = Convert.ToInt32(ProductLvl3);
                                    LmUpdate.Media = Convert.ToInt32(Media);
                                    LmUpdate.RedirectPage = Convert.ToInt32(redirectpage);

                                    LmUpdate.ModifyBy = Session["userid"].ToString();
                                    LmUpdate.ModifyOn = DateTime.Now;
                                    if (Status.ToLower() == "on")
                                    {
                                        LmUpdate.Status = 1;

                                    }
                                    else
                                    {
                                        LmUpdate.Status = 0;
                                    }

                                    if (db.SaveChanges() > 0)
                                    {
                                        ViewBag.Update = "Done";
                                        string str = Path.Combine(Server.MapPath("~/LuminiousUpdateImage/"), Imagename);
                                        postedFile.SaveAs(str);
                                    }
                                    else
                                    {
                                        return Content("<script>alert('Record Has Not Been Saved');</script>");
                                    }


                                }
                                catch
                                {
                                    return View("Index");
                                }
                            }

                        }


                    }
                    luminious_Update lmupdate2 = db.luminious_Update.Single(a => a.Id == intid);
                    //ViewBag.preStartDate = Convert.ToDateTime(banner2.stardate).ToString("dd-MM-yyyy");
                    //ViewBag.PreEndDate = Convert.ToDateTime(banner2.ExpriyDate).ToString("dd-MM-yyyy");
                    ViewBag.preStatus = lmupdate2.Status;

                    return View("Edit", lmupdate2);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
            //return RedirectToAction("Edit", new {id=id});

        }

        //public ActionResult Update()
        //{
        //    return RedirectToAction("BannerList");
        //}
        public JsonResult Delete(int id)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LuminiousUpdate/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    try
                    {
                        luminious_Update LmUpdate = db.luminious_Update.Single(a => a.Id == id);
                        //Save Previous Record In History
                        luminious_UpdateHistory LmHistory = new luminious_UpdateHistory();
                        LmHistory.Lum_UpdateID = LmUpdate.Id;
                        LmHistory.Textbody = LmUpdate.Textbody;
                        LmHistory.ImageFileName = LmUpdate.ImageFileName;
                        LmHistory.ImageName = LmUpdate.ImageName;
                      
                        //LmHistory.BannerSize = LmUpdate.BannerSize;
                        LmHistory.Status = LmUpdate.Status;
                        LmHistory.Expirydate = LmUpdate.Expirydate;
                        LmHistory.Title = LmUpdate.Title;
                        LmHistory.ParentCatid = LmUpdate.ParentCatid;
                        LmHistory.CategoryId = LmUpdate.CategoryId;
                        LmHistory.ProductLevelOne = LmUpdate.ProductLevelOne;
                        LmHistory.ProductLeveltwo = LmUpdate.ProductLeveltwo;
                        LmHistory.ProductLevelThreeId = LmUpdate.ProductLevelThreeId;
                        LmHistory.Media = LmUpdate.Media;
                        LmHistory.RedirectPage = LmUpdate.RedirectPage;

                        LmHistory.ModifyBy = Session["userid"].ToString();
                        LmHistory.ModifyOn = DateTime.Now;
                        LmHistory.StartDate = LmUpdate.StartDate;

                        db.luminious_UpdateHistory.AddObject(LmHistory);

                        //Delete Record From Table
                        LmUpdate.Status = 2;
                        LmUpdate.ModifyOn = DateTime.Now;
                        LmUpdate.ModifyBy = Session["userid"].ToString();
                        if (db.SaveChanges() > 0)
                        {
                            return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                        }

                        else
                        {
                            return Json("Record Not Deleted", JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch
                    {
                        return Json("Invalid Operation", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json("You Have No Delete Permission", JsonRequestBehavior.AllowGet);

                }
            }
        }

    }
}
