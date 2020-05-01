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
    public class BannerController : Controller
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
                string pageUrl2 = "/Banner/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {                    
                  return View(db.Banners.Where(a => a.id == null).OrderByDescending(a => a.id).ToList().ToPagedList(1, 1));                   
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public JsonResult GetRedirectPage()
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Banner/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    
                    var Redirectdata = (from c in db.Redirectdatas
                                   
                                   select new
                                   {
                                       id = c.Id,
                                       Name = c.PageName
                                   }).ToList();


                    return Json(Redirectdata, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }
        public JsonResult GetMedia()
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Banner/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {

             
                    var Mediadata = (from c in db.FooterCategories
                                     where c.CatType=="Media"
                                        select new
                                        {
                                            id = c.Id,
                                            Name = c.FCategoryName
                                        }).ToList();


                    return Json(Mediadata, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }
        public ActionResult Save(string Header_Details, string Sub_Header_Details, string Banner_Details, string StartDate, string EndDate, string Status, HttpPostedFileBase postedFile, string ParentCatid, string pcId, string ProductLvl1,
            string ProductLvl2, string ProductLvl3, string Media, string redirectpage)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Banner/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["createrole"].ToString() == "1")
                {
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;
                    Status = Status ?? "off";
                    #region Check Validation

                    #region Header Details Validations
                    if (Header_Details == null)
                    {
                        ModelState.AddModelError("Header_Details", "Banner Header Is Required");
                    }
                    else {
                        if (Header_Details == "")
                        {
                            ModelState.AddModelError("Header_Details", "Banner Header Is Required");
                        }
                        else if(Header_Details.Length > 49 ) {
                            ModelState.AddModelError("Header_Details", "Characters In Banner Header Should Be Less Than 50");
                        }

                    }
                    #endregion

                    #region Sub Header
                    if (Sub_Header_Details == null)
                    {
                        ModelState.AddModelError("Sub_Header_Details", "Sub Header Is Required");
                    }
                    else
                    {
                        if (Sub_Header_Details == "")
                        {
                            ModelState.AddModelError("Sub_Header_Details", "Sub Header Is Required");
                        }
                        else if (Sub_Header_Details.Length > 99)
                        {
                            ModelState.AddModelError("Sub_Header_Details", "Characters In Sub Header Should Be Less Than 100");
                        }

                    }
                    #endregion


                    #region Banner_Details
                    if (Banner_Details == null)
                    {
                        ModelState.AddModelError("Banner_Details", "Descriptions Is Required");
                    }
                    else
                    {
                        if (Banner_Details == "")
                        {
                            ModelState.AddModelError("Banner_Details", "Descriptions Is Required");
                        }
                        else if (Banner_Details.Length > 499)
                        {
                            ModelState.AddModelError("Banner_Details", "Characters In Discriptions Should Be Less Than 500");
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
                        DateTime startDate=new DateTime();
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
                        catch(FormatException ex)
                        {
                            ModelState.AddModelError("ExpriyDate", "Invalid End Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }
                    
                    }

                    if (postedFile == null)
                    {
                        ModelState.AddModelError("File", "*");
                        ViewBag.File = "File Is Not Uploaded";
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



                    }
                    #endregion

                    #region redirect validation

                    if(redirectpage=="0")
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
                        string filename = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                        string Imagename = filename.Replace(" ", string.Empty);
                        Banner banner = new Banner();
                        banner.Header_Details = Header_Details;
                        banner.Sub_Header_Details = Sub_Header_Details;
                        banner.Banner_Details = Banner_Details;
                        banner.stardate = Convert.ToDateTime(StartDate);
                        banner.ExpriyDate = Convert.ToDateTime(EndDate);
                        banner.BannerFileName = Path.GetFileName(postedFile.FileName);
                        banner.BannerSize = postedFile.ContentLength;
                        banner.BannerImage = Imagename;
                        banner.Create_Date = DateTime.Now;
                        banner.CreatedBy = Session["userid"].ToString();
                        if (Status.ToLower() == "on")
                        {
                            banner.BStatus = 1;

                        }
                        else
                        {
                            banner.BStatus = 0;
                        }
                       banner.ParentCatid = Convert.ToInt32(ParentCatid);
                       banner.ProductLevelOne = Convert.ToInt32(ProductLvl1);
                       banner.ProductLeveltwo = Convert.ToInt32(ProductLvl2);
                       banner.ProductLevelThreeId = Convert.ToInt32(ProductLvl3);
                       banner.CategoryId = Convert.ToInt32(pcId);
                       banner.Media = Convert.ToInt32(Media);
                       banner.RedirectPage = Convert.ToInt32(redirectpage);
                        db.Banners.AddObject(banner);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            string str = Path.Combine(Server.MapPath("~/Banners/"), Imagename);
                            postedFile.SaveAs(str);
                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                    }
                   
                    return View("Index", db.Banners.ToList().ToPagedList(1, 5));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }

        //public ActionResult BannerList(int? page)
        //{

        //    if (Session["userid"] == null)
        //    {
        //        return RedirectToAction("login", "login");
        //    }
        //    else
        //    {
        //        dt = Session["permission"] as DataTable;
        //        string pageUrl2 = "/Banner/Index";
        //        DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
        //        if (result[0]["uview"].ToString() == "1")
        //        {
        //            return View(db.Banners.Where(a => a.BStatus != 2).OrderByDescending(a => a.id).ToList().ToPagedList(page ?? 1, 15));
        //        }
        //        else
        //        {
        //            return RedirectToAction("snotallowed", "snotallowed");
        //        }
        //    }

        //}
        public JsonResult GetBannerDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("login");
            }
            else
            {

             
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Banner/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    var Bannerdetails = (from c in db.Banners
                                         where c.BStatus != 2
                                         orderby c.Sequence ascending
                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var Bannerdetails2 = (from c in Bannerdetails join rd in db.Redirectdatas on c.RedirectPage equals rd.Id into joinGroup
                                          from gr in joinGroup.DefaultIfEmpty()
                                           select new
                                           {
                                               Hdetails = c.Header_Details,
                                               sHeader = c.Sub_Header_Details,
                                               status = c.BStatus == 1 ? "Enable" : "Disable",
                                               BannerDetail = c.Banner_Details,
                                               PageName = gr != null ? gr.PageName : null,
                                               startdate = Convert.ToDateTime(c.stardate).ToShortDateString(),
                                               id = c.id,
                                               expiryDate = Convert.ToDateTime(c.ExpriyDate).ToShortDateString(),
                                               bannerImage=c.BannerImage,
                                               Sequence=c.Sequence
                                           }).OrderBy(a => a.Sequence).Skip(page ?? 0).Take(15).ToList();
                    if (Bannerdetails.Count() % 15 == 0)
                    {
                        totalrecord = Bannerdetails.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (Bannerdetails.Count() / 15) + 1;
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
                string pageUrl2 = "/Banner/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    try
                    {
                        Banner banner = db.Banners.Single(a => a.id == id && a.BStatus != 2);
                        ViewBag.preStartDate = Convert.ToDateTime(banner.stardate).ToString("dd-MM-yyyy");
                        ViewBag.PreEndDate = Convert.ToDateTime(banner.ExpriyDate).ToString("dd-MM-yyyy");
                        ViewBag.preStatus = banner.BStatus;
                        ViewBag.Update = "";

                        ViewBag.ProductCat = banner.CategoryId;
                        ViewBag.Productlvl1 = banner.ProductLevelOne;
                        ViewBag.Productlvl2 = banner.ProductLeveltwo;
                        ViewBag.Productlvl3 = banner.ProductLevelThreeId;
                        ViewBag.Prntid = banner.ParentCatid;
                        ViewBag.media = banner.Media;
                        ViewBag.Redirectpage = banner.RedirectPage;
                        return View(db.Banners.Single(a => a.id == id));
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
        public ActionResult Update(string id, string Header_Details, string Sub_Header_Details, string Banner_Details, string StartDate, string EndDate, string Status, HttpPostedFileBase postedFile, string ParentCatid, string pcId, string ProductLvl1,
            string ProductLvl2, string ProductLvl3, string Media, string redirectpage)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Banner/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    #region Check Validation
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;
                    int intid = int.Parse(id);
                    Status = Status ?? "off";
                    #region Header Details Validations
                    if (Header_Details == null)
                    {
                        ModelState.AddModelError("Header_Details", "Banner Header Is Required");
                    }
                    else
                    {
                        if (Header_Details == "")
                        {
                            ModelState.AddModelError("Header_Details", "Banner Header Is Required");
                        }
                        else if (Header_Details.Length > 49)
                        {
                            ModelState.AddModelError("Header_Details", "Characters In Banner Header Should Be Less Than 50");
                        }

                    }
                    #endregion

                    #region Sub Header
                    if (Sub_Header_Details == null)
                    {
                        ModelState.AddModelError("Sub_Header_Details", "Sub Header Is Required");
                    }
                    else
                    {
                        if (Sub_Header_Details == "")
                        {
                            ModelState.AddModelError("Sub_Header_Details", "Sub Header Is Required");
                        }
                        else if (Sub_Header_Details.Length > 99)
                        {
                            ModelState.AddModelError("Sub_Header_Details", "Characters In Sub Header Should Be Less Than 100");
                        }

                    }
                    #endregion


                    #region Banner_Details
                    if (Banner_Details == null)
                    {
                        ModelState.AddModelError("Banner_Details", "Descriptions Is Required");
                    }
                    else
                    {
                        if (Banner_Details == "")
                        {
                            ModelState.AddModelError("Banner_Details", "Descriptions Is Required");
                        }
                        else if (Banner_Details.Length > 499)
                        {
                            ModelState.AddModelError("Banner_Details", "Characters In Discriptions Should Be Less Than 500");
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
                            //if (db.Banners.Any(a => a.ExpriyDate >= startDate && a.id != intid && a.BStatus != 2 ))
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
                                Banner banner = db.Banners.Single(a => a.id == intid && a.BStatus != 2);
                                BannerHistory banorHisotry = new BannerHistory();
                                banorHisotry.BannerId = banner.id;
                                banorHisotry.Banner_Details = banner.Banner_Details;
                                banorHisotry.BannerFileName = banner.BannerFileName;
                                banorHisotry.BannerImage = banner.BannerImage;
                                banorHisotry.BannerSize = banner.BannerSize;
                                banorHisotry.BStatus = banner.BStatus;
                                banorHisotry.ExpriyDate = banner.ExpriyDate;
                                banorHisotry.Header_Details = banner.Header_Details;
                                banorHisotry.Modified_By = Session["userid"].ToString();
                                banorHisotry.Modified_Date = DateTime.Now;
                                banorHisotry.stardate = banner.stardate;
                                banorHisotry.Sub_Header_Details = banner.Sub_Header_Details;
                                banorHisotry.ParentCatid = banner.ParentCatid;
                                banorHisotry.CategoryId = banner.CategoryId;
                                banorHisotry.ProductLevelOne = banner.ProductLevelOne;
                                banorHisotry.ProductLeveltwo = banner.ProductLeveltwo;
                                banorHisotry.ProductLevelThreeId = banner.ProductLevelThreeId;
                                banorHisotry.Media = banner.Media;
                                banorHisotry.RedirectPage = banner.RedirectPage;
                                
                                db.BannerHistories.AddObject(banorHisotry);
                    
                                banner.Header_Details = Header_Details;
                                banner.Sub_Header_Details = Sub_Header_Details;
                                banner.Banner_Details = Banner_Details;
                                banner.ParentCatid = Convert.ToInt32(ParentCatid);
                                banner.CategoryId = Convert.ToInt32(pcId);
                                banner.ProductLevelOne = Convert.ToInt32(ProductLvl1);
                                banner.ProductLeveltwo = Convert.ToInt32(ProductLvl2);
                                banner.ProductLevelThreeId = Convert.ToInt32(ProductLvl3);
                                banner.Media = Convert.ToInt32(Media);
                                banner.RedirectPage =Convert.ToInt32(redirectpage);
                              
                                banner.stardate = Convert.ToDateTime(StartDate);
                                banner.ExpriyDate = Convert.ToDateTime(EndDate);
                                banner.Modified_By = Session["userid"].ToString();
                                banner.Modified_Date = DateTime.Now;
                                if (Status.ToLower() == "on")
                                {
                                    banner.BStatus = 1;

                                }
                                else
                                {
                                    banner.BStatus = 0;
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
                                    Banner banner = db.Banners.Single(a => a.id == intid && a.BStatus != 2);
                                                                       
                                   //Save Previous Record In History
                                        BannerHistory banorHisotry = new BannerHistory();
                                        banorHisotry.BannerId = banner.id;
                                        banorHisotry.Banner_Details = banner.Banner_Details;
                                        banorHisotry.BannerFileName = banner.BannerFileName;
                                        banorHisotry.BannerImage = banner.BannerImage;
                                        banorHisotry.BannerSize = banner.BannerSize;
                                        banorHisotry.BStatus = banner.BStatus;
                                        banorHisotry.ExpriyDate = banner.ExpriyDate;
                                        banorHisotry.Header_Details = banner.Header_Details;
                                        banorHisotry.Modified_By = Session["userid"].ToString();
                                        banorHisotry.Modified_Date = DateTime.Now;
                                        banorHisotry.stardate = banner.stardate;
                                        banorHisotry.Sub_Header_Details = banner.Sub_Header_Details;
                                        banorHisotry.ParentCatid = banner.ParentCatid;
                                        banorHisotry.CategoryId = banner.CategoryId;
                                        banorHisotry.ProductLevelOne = banner.ProductLevelOne;
                                        banorHisotry.ProductLeveltwo = banner.ProductLeveltwo;
                                        banorHisotry.ProductLevelThreeId = banner.ProductLevelThreeId;
                                        banorHisotry.Media = banner.Media;
                                        banorHisotry.RedirectPage = banner.RedirectPage;
                                        db.BannerHistories.AddObject(banorHisotry);


                                        //Save New Record
                                        banner.Header_Details = Header_Details;
                                        banner.Sub_Header_Details = Sub_Header_Details;
                                        banner.Banner_Details = Banner_Details;
                                        banner.stardate = Convert.ToDateTime(StartDate);
                                        banner.ExpriyDate = Convert.ToDateTime(EndDate);
                                        banner.BannerFileName = Path.GetFileName(postedFile.FileName);
                                        banner.BannerSize = postedFile.ContentLength;
                                        banner.BannerImage = Imagename;
                                        banner.ParentCatid = Convert.ToInt32(ParentCatid);
                                        banner.CategoryId = Convert.ToInt32(pcId);
                                        banner.ProductLevelOne = Convert.ToInt32(ProductLvl1);
                                        banner.ProductLeveltwo = Convert.ToInt32(ProductLvl2);
                                        banner.ProductLevelThreeId = Convert.ToInt32(ProductLvl3);
                                        banner.Media = Convert.ToInt32(Media);
                                        banner.RedirectPage = Convert.ToInt32(redirectpage);
                                        banner.Modified_By = Session["userid"].ToString();
                                        banner.Modified_Date = DateTime.Now;
                                        if (Status.ToLower() == "on")
                                        {
                                            banner.BStatus = 1;

                                        }
                                        else
                                        {
                                            banner.BStatus = 0;
                                        }

                                        if (db.SaveChanges() > 0)
                                        {
                                            ViewBag.Update = "Done";
                                            string str = Path.Combine(Server.MapPath("~/Banners/"), Imagename);
                                            postedFile.SaveAs(str);
                                        }
                                        else {
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
                    Banner banner2 = db.Banners.Single(a => a.id == intid);
                    //ViewBag.preStartDate = Convert.ToDateTime(banner2.stardate).ToString("dd-MM-yyyy");
                    //ViewBag.PreEndDate = Convert.ToDateTime(banner2.ExpriyDate).ToString("dd-MM-yyyy");
                    ViewBag.preStatus = banner2.BStatus;

                    return View("Edit", banner2);
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
                string pageUrl2 = "/Banner/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    try
                    {
                        Banner banner = db.Banners.Single(a => a.id == id);
                        //Save Previous Record In History
                        BannerHistory banorHisotry = new BannerHistory();
                        banorHisotry.BannerId = banner.id;
                        banorHisotry.Banner_Details = banner.Banner_Details;
                        banorHisotry.BannerFileName = banner.BannerFileName;
                        banorHisotry.BannerImage = banner.BannerImage;
                        banorHisotry.BannerSize = banner.BannerSize;
                        banorHisotry.BStatus = banner.BStatus;
                        banorHisotry.ExpriyDate = banner.ExpriyDate;
                        banorHisotry.Header_Details = banner.Header_Details;
                        banorHisotry.ParentCatid = banner.ParentCatid;
                        banorHisotry.CategoryId = banner.CategoryId;
                        banorHisotry.ProductLevelOne = banner.ProductLevelOne;
                        banorHisotry.ProductLeveltwo = banner.ProductLeveltwo;
                        banorHisotry.ProductLevelThreeId = banner.ProductLevelThreeId;
                        banorHisotry.Media = banner.Media;
                        banorHisotry.RedirectPage = banner.RedirectPage;
                        banorHisotry.Modified_By = Session["userid"].ToString();
                        banorHisotry.Modified_Date = DateTime.Now;
                        banorHisotry.stardate = banner.stardate;
                        banorHisotry.Sub_Header_Details = banner.Sub_Header_Details;                       
                        db.BannerHistories.AddObject(banorHisotry);

                        //Delete Record From Table
                        banner.BStatus = 2;
                        banner.Modified_Date = DateTime.Now;
                        banner.Modified_By = Session["userid"].ToString();
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

      [HttpPost]
        public ActionResult UpdateSequence(string ids)
        {
          var bannersplit = ids.Split(',');
         int bannerid=Convert.ToInt32(bannersplit[0]);
         Banner bn = db.Banners.Single(c => c.id == bannerid && c.BStatus != 2);
         bn.Sequence = Convert.ToInt32(bannersplit[1]);
         db.SaveChanges();
         return RedirectToAction("Index");
        }

    }
}
