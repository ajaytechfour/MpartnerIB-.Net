using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;
namespace LuminousMpartnerIB.Controllers
{
    public class CreatePermotionsController : Controller
    {
        //
        // GET: /CreatePermotions/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/CreatePermotions/Index";
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

        public JsonResult GetProductCategory()
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
                    var Company = (from c in db.PermotionTypes
                                   where c.PlTwStatus != 2 && c.PlTwStatus != 0
                                   select new
                                   {
                                       id = c.id,
                                       Name = c.PType
                                   }).ToList();
                    return Json(Company, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }

        public ActionResult SaveContact(string Alls, string rglist, string disList, string Dealist, string Descriptions, string StartDate, string EndDate, string statusC, HttpPostedFileBase[] file, string ptype, string Pcat, string PlvlOne
     , string DistriCheck, string DealCheck, HttpPostedFileBase Pdf_file, string ParentCatid, HttpPostedFileBase userupload_file)
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

                    int pcid;
                    int productCat1;
                    int ptypeid;
                    int parntcat;
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;
                    ViewBag.ProductCat = Pcat;
                    ViewBag.ProductCat1 = PlvlOne;
                    ViewBag.Ptype = ptype;
                    ViewBag.Des = Descriptions;
                    ViewBag.parntcat = ParentCatid;

                    #region Check Validation for permission
                    //if (Alls == null)
                    //{
                    //    ModelState.AddModelError("status", "Permission For Has No Value");
                    //}
                    //else
                    if (userupload_file == null)
                    {
                        Alls = Alls ?? "off";
                        if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (rglist == "" || rglist == null || rglist == "0") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                        {

                            ModelState.AddModelError("status", "Permission For Has No Value");

                        }
                        else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                        {
                            ModelState.AddModelError("status", "Check Either Distributor OR Dealer");
                        }
                    }

                    #endregion


                    #region validate ProductCategory
                    if (!int.TryParse(Pcat, out pcid))
                    {

                        ModelState.AddModelError("ProductCatId", "Select Product Category");
                    }
                    if (Pcat != null)
                    {
                        if (Pcat == "0")
                        {
                            ModelState.AddModelError("ProductCatId", "Select Product Category");
                        }
                    }
                    #endregion

                    #region Validate Productlevel One
                    if (!int.TryParse(PlvlOne, out productCat1))
                    {

                        ModelState.AddModelError("ProductLvlOneId", "Select Product Category Level One");
                    }
                    if (PlvlOne != null)
                    {
                        if (PlvlOne == "0")
                        {
                            ModelState.AddModelError("ProductLvlOneId", "Select Product Category Level One");
                        }
                    }
                    #endregion

                    #region Validate Promotion Type
                    if (!int.TryParse(ptype, out ptypeid))
                    {

                        ModelState.AddModelError("PermotionTypeId", "Select Permotion Type");
                    }
                    if (ptype != null)
                    {
                        if (ptype == "0")
                        {
                            ModelState.AddModelError("PermotionTypeId", "Select Permotion Type");
                        }
                    }
                    #endregion

                    #region Validate Parent Category
                    if (!int.TryParse(ParentCatid, out parntcat))
                    {
                        ModelState.AddModelError("ParentCatid", "Select Parent Category");

                    }
                    if (parntcat == 0)
                    {
                        ModelState.AddModelError("ParentCatid", "Select Parent Category");
                    }
                    #endregion

                    //Check Status Null value
                    string Status = statusC ?? "off";

                    #region validate Start Date
                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "Start Date Is Empty");
                    }
                    //else
                    //{
                    //    try
                    //    {
                    //        if (Convert.ToDateTime(StartDate) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    //        {
                    //            ModelState.AddModelError("StartDate", "Start Date Should Be Greater Than or Equal To Current Date");
                    //            ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                    //        }
                    //        DateTime startDate = Convert.ToDateTime(StartDate);
                    //        if (db.PermotionsLists.Any(a => a.Enddate >= startDate && a.status != 2 ))
                    //        {
                    //            ModelState.AddModelError("StartDate", "There Is already A Banner Defined In This Date");
                    //            ViewBag.StartDate = "There Is already A Banner Defined In This Date";
                    //        }
                    //    }
                    //    catch (FormatException ex)
                    //    {
                    //        ModelState.AddModelError("StartDate", "Invalid Date");
                    //        ViewBag.StartDate = "Invalid Date";
                    //    }
                    //}

                    #endregion

                    #region Validate End DAte
                    if (EndDate == null || EndDate == "")
                    {
                        ModelState.AddModelError("EndDate", "End Date Is Not Selected");
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
                                    ModelState.AddModelError("Enddate", "End Date Should Be Greater Than or Equal To Start Date");
                                    ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                                }
                            }
                            catch (FormatException ex)
                            {
                                ModelState.AddModelError("Enddate", "Invalid Date");
                                ViewBag.EndDate = "Invalid End Date";
                            }

                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("Enddate", "Invalid Start Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }

                    }
                    #endregion

                    #region Validate File
                    if (file[0] == null)
                    {

                        ModelState.AddModelError("ImageName", "Image File Is Not Uploaded");
                        ViewBag.File = "File Is Not Uploaded";
                    }
                    #endregion

                    #region Validate PDF File
                    if (Pdf_file == null)
                    {

                        ModelState.AddModelError("PDFName", "File Is Not Uploaded");
                        ViewBag.PDFFile = "PDF File Is Not Uploaded";
                    }
                    #endregion

                    #region Validate Descriptions
                    if (Descriptions == "" || Descriptions == null)
                    {
                        ModelState.AddModelError("Descriptions", "Descriptions Is Empty");

                    }
                    #endregion

                    if (ModelState.IsValid)
                    {
                        foreach (HttpPostedFileBase fileimage in file)
                        {

                            if (fileimage != null)
                            {
                                string inputfimage = "";
                                //string filename = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                                var InputFileName = Path.GetFileNameWithoutExtension(fileimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fileimage.FileName);
                                inputfimage = InputFileName.Replace(" ", string.Empty);
                                var ServerSavePath = Path.Combine(Server.MapPath("~/PermotionsImage/") + inputfimage);
                                //Save file to server folder  
                                fileimage.SaveAs(ServerSavePath);

                                PermotionsList contactusd = new PermotionsList();
                                contactusd.PermotionTypeId = ptypeid;
                                contactusd.ProductCatId = pcid;
                                contactusd.ProductLvlOneId = productCat1;
                                contactusd.Descriptions = Descriptions;
                                contactusd.createby = Session["userid"].ToString();
                                contactusd.ImageName = inputfimage;
                                contactusd.createdate = DateTime.Now;
                                contactusd.StartDate = Convert.ToDateTime(StartDate);
                                contactusd.Enddate = Convert.ToDateTime(EndDate);

                                contactusd.ParentCatid = parntcat;
                                //PDF Upload Code//

                                if (Pdf_file != null)
                                {
                                    string pdffile = "";
                                    var PDFFileName = Path.GetFileNameWithoutExtension(Pdf_file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(Pdf_file.FileName);
                                    pdffile = PDFFileName.Replace(" ", string.Empty);
                                    var PDFSavePath = Path.Combine(Server.MapPath("~/PermotionsImage/") + pdffile);
                                    //Save file to server folder  
                                    Pdf_file.SaveAs(PDFSavePath);
                                    contactusd.PDFName = pdffile;
                                }


                                string status = statusC ?? "off";
                                if (status == "on")
                                {
                                    contactusd.status = 1;
                                }
                                else
                                {
                                    contactusd.status = 0;
                                }
                                db.PermotionsLists.AddObject(contactusd);
                                db.SaveChanges();

                                if (userupload_file == null)
                                {
                                    Alls = Alls ?? "off";
                                    DealCheck = DealCheck ?? "off";
                                    DistriCheck = DistriCheck ?? "off";

                                    if (Alls.ToLower() != "on")
                                    {
                                        if (rglist != "" && rglist != null)
                                        {
                                            Regex rg = new Regex(",");
                                            string[] reglist = rg.Split(rglist);
                                            foreach (string s in reglist)
                                            {
                                                ProductAccessTable pat = new ProductAccessTable();
                                                pat.promotionid = contactusd.id;
                                                pat.RegId = int.Parse(s);
                                                pat.createdate = DateTime.Now;
                                                pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                                pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                                pat.createby = Session["userid"].ToString();
                                                db.ProductAccessTables.AddObject(pat);
                                                db.SaveChanges();

                                            }
                                        }
                                    }
                                    else
                                    {
                                        ProductAccessTable pat = new ProductAccessTable();
                                        pat.promotionid = contactusd.id;
                                        pat.AllAcess = true;
                                        db.ProductAccessTables.AddObject(pat);
                                        db.SaveChanges();
                                    }
                                    if ((Alls.ToLower() != "on") && (rglist == "" || rglist == null) && (DistriCheck != "on" || DealCheck != "on"))
                                    {
                                        ProductAccessTable pat = new ProductAccessTable();
                                        pat.promotionid = contactusd.id;
                                        pat.RegId = null;
                                        pat.createdate = DateTime.Now;
                                        pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                        pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                        pat.createby = Session["userid"].ToString();
                                        db.ProductAccessTables.AddObject(pat);
                                        db.SaveChanges();
                                    }

                                }
                                else
                                {

                                    DataTable dtupload = new DataTable();

                                    System.Data.DataTable DTimportexcel = new System.Data.DataTable();
                                    StringBuilder strValidations = new StringBuilder(string.Empty);
                                    List<UserSapcode> sapcode = new List<UserSapcode>();

                                    if (userupload_file.ContentLength > 0)
                                    {
                                        string ext = System.IO.Path.GetExtension(Path.GetFileName(userupload_file.FileName));
                                        var originalfilename = Path.GetFileName(userupload_file.FileName);
                                        if (ext == ".CSV" || ext == ".csv")
                                        {
                                            //string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"),
                                            //  string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"),
                                            string[] changeextension = originalfilename.Split('.');
                                            string xlsextension = changeextension[0] + ".csv";
                                            string path =
                                Path.Combine(Path.GetDirectoryName(xlsextension)
                                , string.Concat(Path.GetFileNameWithoutExtension(xlsextension)
                                , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                                , Path.GetExtension(xlsextension)
                                )
                                );
                                            //Path.GetFileName(FileUpload.FileName));
                                            var pathfolder = Path.Combine(Server.MapPath("~/UploadSapcode/"), path);
                                            userupload_file.SaveAs(pathfolder);
                                            dtupload = ProcessCSV(pathfolder);
                                            DataSet ds = new DataSet();

                                            ds.Tables.Add(dtupload);

                                            //  int dsRowcount = 0;

                                            if (ds.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                                {
                                                    UserSapcode scode = new UserSapcode();
                                                    scode.SAPCode = ds.Tables[0].Rows[i]["Column1"].ToString();
                                                    sapcode.Add(scode);
                                                }
                                            }
                                            foreach (var data in sapcode)
                                            {
                                                var usertype = db.UsersLists.Where(c => c.UserId == data.SAPCode).Select(c => new { c.CustomerType, c.UserId }).SingleOrDefault();
                                                ProductAccessTable pat = new ProductAccessTable();
                                                pat.promotionid = contactusd.id;
                                                pat.RegId = null;
                                                pat.createdate = DateTime.Now;
                                                pat.AllDealerAccess = false;
                                                pat.AllDestriAccess = false;
                                                if (usertype.CustomerType == "DISTY")
                                                {
                                                    pat.SpecificDealerAccess = "0";
                                                    pat.SpecificDestriAccess = usertype.UserId;
                                                }
                                                if (usertype.CustomerType == "Dealer")
                                                {
                                                    pat.SpecificDealerAccess = usertype.UserId;
                                                    pat.SpecificDestriAccess = "0";
                                                }
                                                pat.createby = Session["userid"].ToString();
                                                db.ProductAccessTables.AddObject(pat);
                                                db.SaveChanges();
                                            }
                                        }
                                    }

                                }
                            }


                        }


                        #region Commented Code
                        //    if (disList != "0" && disList != "" && disList != null)
                        //    {
                        //        Regex rg = new Regex(",");
                        //        string[] reglist = rg.Split(disList);
                        //        foreach (string s in reglist)
                        //        {
                        //            ProductAccessTable pat2 = new ProductAccessTable();
                        //            pat2.promotionid = contactusd.id;
                        //            pat2.DestributorID = int.Parse(s);
                        //            pat2.createdate = DateTime.Now;
                        //            pat2.createby = Session["userid"].ToString();
                        //            db.ProductAccessTables.AddObject(pat2);
                        //            db.SaveChanges();

                        //        }
                        //    }
                        //    else
                        //    {
                        //        ProductAccessTable pat2 = new ProductAccessTable();
                        //        pat2.promotionid = contactusd.id;
                        //        pat2.AllDestriAccess = true;
                        //        pat2.createdate = DateTime.Now;
                        //        pat2.createby = Session["userid"].ToString();
                        //        db.ProductAccessTables.AddObject(pat2);
                        //        db.SaveChanges();

                        //    }



                        //    if (Dealist != "0" && Dealist != "" && Dealist != null)
                        //    {
                        //        Regex rg = new Regex(",");
                        //        string[] reglist = rg.Split(Dealist);
                        //        foreach (string s in reglist)
                        //        {
                        //            ProductAccessTable pat3 = new ProductAccessTable();
                        //            pat3.promotionid = contactusd.id;
                        //            pat3.DealerId = int.Parse(s);
                        //            pat3.createdate = DateTime.Now;
                        //            pat3.createby = Session["userid"].ToString();
                        //            db.ProductAccessTables.AddObject(pat3);
                        //            db.SaveChanges();

                        //        }
                        //    }
                        //    else
                        //    {
                        //        ProductAccessTable pat2 = new ProductAccessTable();
                        //        pat2.promotionid = contactusd.id;
                        //        pat2.AllDealerAccess = true;
                        //        pat2.createdate = DateTime.Now;
                        //        pat2.createby = Session["userid"].ToString();
                        //        db.ProductAccessTables.AddObject(pat2);
                        //        db.SaveChanges();

                        //    }
                        //}
                        //else
                        //{
                        //    ProductAccessTable pat3 = new ProductAccessTable();
                        //    pat3.promotionid = contactusd.id;
                        //    pat3.AllAcess = true;
                        //    pat3.createdate = DateTime.Now;
                        //    pat3.createby = Session["userid"].ToString();
                        //    db.ProductAccessTables.AddObject(pat3);
                        //    db.SaveChanges();
                        //}
                        #endregion
                        //string str = Path.Combine(Server.MapPath("~/PermotionsImage/"), filename);
                        //file.SaveAs(str);
                        ViewBag.SaveStatus = "Record Saved Successfully";
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
            int? PageId = page;
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
                    int contactdetails = (from c in db.PermotionsLists
                                          where c.status != 2
                                          select c).Count();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }
                    if (contactdetails % 15 == 0)
                    {
                        totalrecord = contactdetails / 15;
                    }
                    else
                    {
                        totalrecord = (contactdetails / 15) + 1;
                    }
                    if (Session["Search"] != null)
                    {
                        var contactDetails2 = (from c in db.PermotonsListPaging(PageId ?? 1, 15)
                                               select new
                                               {
                                                   id = c.id,
                                                   PCat = c.Pname,
                                                   proCatOne = c.Name,
                                                   ProCatTwo = c.PType,

                                                   Descriptions = c.Descriptions,
                                                   ParntCat = c.ParentCategory,
                                                   StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                                                   EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                                                   status = c.status == 1 ? "Active" : "Deactive",


                                               }).Where(c => c.id.ToString().Contains(Session["Search"].ToString()) || c.PCat.Contains(Session["Search"].ToString()) || c.proCatOne.Contains(Session["Search"].ToString()) || c.ProCatTwo.Contains(Session["Search"].ToString()) || c.Descriptions.Contains(Session["Search"].ToString()) || c.status.Contains(Session["Search"].ToString()) || c.StartDate.Contains(Session["Search"].ToString()) || c.EndDate.Contains(Session["Search"].ToString()) || c.ParntCat.Contains(Session["Search"].ToString())).ToList();



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
                        var contactDetails2 = (from c in db.PermotonsListPaging(PageId ?? 1, 15)
                                               select new
                                               {
                                                   id = c.id,
                                                   PCat = c.Pname,
                                                   proCatOne = c.Name,
                                                   ProCatTwo = c.PType,

                                                   Descriptions = c.Descriptions,
                                                   ParntCat = c.ParentCategory,
                                                   StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                                                   EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                                                   status = c.status == 1 ? "Active" : "Deactive",


                                               }).ToList();


                        var data = new { result = contactDetails2, TotalRecord = totalrecord };
                        return Json(data, JsonRequestBehavior.AllowGet);
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
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    PermotionsList cud = db.PermotionsLists.Single(a => a.id == id);
                    List<ProductAccessTable> pat = db.ProductAccessTables.Where(a => a.promotionid == id).ToList();
                    ViewBag.status = cud.status;
                    ViewBag.preStartDate = Convert.ToDateTime(cud.StartDate).ToShortDateString();
                    ViewBag.PreEndDate = Convert.ToDateTime(cud.Enddate).ToShortDateString();
                    ViewBag.ProductCat = cud.ProductCatId;
                    ViewBag.ProductCat1 = cud.ProductLvlOneId;
                    ViewBag.Ptype = cud.PermotionTypeId;
                    ViewBag.Prntid = cud.ParentCatid;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public ActionResult Update(int id, string Ptype, string Pcat, string PlvlOne, HttpPostedFileBase file, string Descriptions, string StartDate, string EndDate, string statusC, string ParentCatid)
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

                    int pcid;
                    int productCat1;
                    int ptypeid;
                    int parntcat;
                    bool fileflag = false;
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;
                    ViewBag.ProductCat = Pcat;
                    ViewBag.ProductCat1 = PlvlOne;
                    ViewBag.Ptype = Ptype;
                    ViewBag.Des = Descriptions;
                    ViewBag.parntcat = ParentCatid;
                    #region Product Category
                    if (!int.TryParse(Pcat, out pcid))
                    {

                        ModelState.AddModelError("ProductCatId", "Select Product Category");
                    }
                    if (Pcat != null)
                    {
                        if (Pcat == "0")
                        {
                            ModelState.AddModelError("ProductCatId", "Select Product Category");
                        }
                    }
                    #endregion
                    #region Validate Parent Category
                    if (!int.TryParse(ParentCatid, out parntcat))
                    {
                        ModelState.AddModelError("ParentCatid", "Select Parent Category");

                    }
                    if (parntcat == 0)
                    {
                        ModelState.AddModelError("ParentCatid", "Select Parent Category");
                    }
                    #endregion
                    #region Product level One
                    if (!int.TryParse(PlvlOne, out productCat1))
                    {

                        ModelState.AddModelError("ProductLvlOneId", "Select Product Category Level One");
                    }
                    if (PlvlOne != null)
                    {
                        if (PlvlOne == "0")
                        {
                            ModelState.AddModelError("ProductLvlOneId", "Select Product Category Level One");
                        }
                    }
                    #endregion
                    #region Permotion Type
                    if (!int.TryParse(Ptype, out ptypeid))
                    {

                        ModelState.AddModelError("PermotionTypeId", "Select Permotion Type");
                    }
                    if (Ptype != null)
                    {
                        if (Ptype == "0")
                        {
                            ModelState.AddModelError("PermotionTypeId", "Select Permotion Type");
                        }
                    }
                    #endregion

                    string Status = statusC ?? "off";
                    #region Date validations
                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "Start Date Is Empty");
                    }
                    //else
                    //{
                    //    try
                    //    {

                    //        if (Convert.ToDateTime(StartDate) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    //        {
                    //            ModelState.AddModelError("StartDate", "Start Date Should Be Greater Than or Equal To Current Date");
                    //            ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                    //        }
                    //        DateTime startDate = Convert.ToDateTime(StartDate);
                    //        if (db.PermotionsLists.Any(a => a.Enddate >= startDate &&  a.status != 2 && a.id != id))
                    //        {
                    //            ModelState.AddModelError("StartDate", "There Is already A Banner Defined In This Date");
                    //            ViewBag.StartDate = "There Is already A Banner Defined In This Date";
                    //        }
                    //    }
                    //    catch (FormatException ex)
                    //    {
                    //        ModelState.AddModelError("StartDate", "Invalid Date");
                    //        ViewBag.StartDate = "Invalid Date";
                    //    }
                    //}
                    if (EndDate == null || EndDate == "")
                    {
                        ModelState.AddModelError("EndDate", "End Date Is Not Selected");
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
                                    ModelState.AddModelError("Enddate", "End Date Should Be Greater Than or Equal To Start Date");
                                    ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                                }
                            }
                            catch (FormatException ex)
                            {
                                ModelState.AddModelError("Enddate", "Invalid Date");
                                ViewBag.EndDate = "Invalid End Date";
                            }

                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("Enddate", "Invalid Start Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }
                    }

                    #endregion
                    #region File Validations
                    if (file == null)
                    {

                        // ModelState.AddModelError("ImageName", "File Is Not Uploaded");
                        fileflag = true;

                        // ViewBag.File = "File Is Not Uploaded";
                    }
                    else
                    {
                        string FileExtension = Path.GetExtension(file.FileName);
                        if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                           FileExtension.ToLower() == ".png")
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("ImageName", "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg");
                            ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                        }



                    }
                    if (Descriptions == "" || Descriptions == null)
                    {
                        ModelState.AddModelError("Descriptions", "Descriptions Is Empty");

                    }
                    #endregion


                    if (ModelState.IsValid)
                    {
                        PermotionsList contactusd = db.PermotionsLists.Single(a => a.id == id);

                        PermotionsListHistory plh = new PermotionsListHistory();
                        plh.Descriptions = contactusd.Descriptions;
                        plh.Enddate = contactusd.Enddate;
                        plh.ImageName = contactusd.ImageName;
                        plh.Modifiedby = Session["userid"].ToString();
                        plh.modifieddate = DateTime.Now;
                        plh.PermotionTypeId = contactusd.PermotionTypeId;
                        plh.ProductCatId = contactusd.ProductCatId;
                        plh.ProductLvlOneId = contactusd.ProductLvlOneId;
                        plh.promotionListId = contactusd.id;
                        plh.StartDate = contactusd.StartDate;
                        plh.status = contactusd.status;
                        db.PermotionsListHistories.AddObject(plh);





                        if (!fileflag)
                        {
                            string filename = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                            contactusd.ImageName = filename;
                            string str = Path.Combine(Server.MapPath("~/PermotionsImage/"), filename);
                            file.SaveAs(str);
                        }
                        contactusd.PermotionTypeId = ptypeid;
                        contactusd.ProductCatId = pcid;
                        contactusd.ProductLvlOneId = productCat1;
                        contactusd.Descriptions = Descriptions;
                        contactusd.createby = Session["userid"].ToString();
                        contactusd.createdate = DateTime.Now;
                        contactusd.StartDate = Convert.ToDateTime(StartDate);
                        contactusd.Enddate = Convert.ToDateTime(EndDate);
                        contactusd.ParentCatid = parntcat;
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.status = 1;
                        }
                        else
                        {
                            contactusd.status = 0;
                        }
                        db.SaveChanges();
                        ViewBag.SaveStatus = "Record Saved Successfully";
                        return View("Index");
                    }
                    return View("Edit", db.PermotionsLists.Single(a => a.id == id));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public JsonResult Delete(int id)
        {
            if (Session["userid"] == null)
            {
                return Json("login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    PermotionsList contactusd = db.PermotionsLists.Single(a => a.id == id);

                    PermotionsListHistory plh = new PermotionsListHistory();
                    plh.Descriptions = contactusd.Descriptions;
                    plh.Enddate = contactusd.Enddate;
                    plh.ImageName = contactusd.ImageName;
                    plh.Modifiedby = Session["userid"].ToString();
                    plh.modifieddate = DateTime.Now;
                    plh.PermotionTypeId = contactusd.PermotionTypeId;
                    plh.ProductCatId = contactusd.ProductCatId;
                    plh.ProductLvlOneId = contactusd.ProductLvlOneId;
                    plh.promotionListId = contactusd.id;
                    plh.StartDate = contactusd.StartDate;
                    plh.status = contactusd.status;
                    db.PermotionsListHistories.AddObject(plh);

                    contactusd.status = 2;
                    contactusd.Modifiedby = Session["userid"].ToString();
                    contactusd.modifieddate = DateTime.Now;
                    if (db.SaveChanges() > 0)
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
        private static DataTable ProcessCSV(string fileName)
        {
            //Set up our variables
            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataTable dt = new DataTable();
            DataRow row;
            // work out where we should split on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Set the filename in to our stream
            StreamReader sr = new StreamReader(fileName);

            //Read the first line and split the string at , with our regular expression in to an array
            line = sr.ReadLine();
            strArray = r.Split(line);

            //For each item in the new split array, dynamically builds our Data columns. Save us having to worry about it.
            Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));

            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                row = dt.NewRow();

                //add our current value to our data row
                row.ItemArray = r.Split(line);
                dt.Rows.Add(row);
            }

            //Tidy Streameader up
            sr.Dispose();

            //return a the new DataTable
            return dt;

        }
    }
}
