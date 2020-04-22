using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
namespace Luminous.Controllers
{
    public class ProductLevel3SequenceController : Controller
    {
        private LuminousEntities db = new LuminousEntities();
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
        public JsonResult GetProductLevelTwoCategory(string procatid, string procatone)
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
                    int ProcatID;
                    int.TryParse(procatid, out ProcatID);
                    int ProductlevelOneId;
                    int.TryParse(procatone, out ProductlevelOneId);
                    var Company = (from c in db.ProductLevelTwoes
                                   where c.PlTwStatus != 2 && c.PlTwStatus != 0 && c.PrductID == ProcatID && c.pc_Lvl_oneId == ProductlevelOneId
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

        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveContact(string Alls, string rglist, string disList, string Dealist,
             ProductLevelThree contactUs, string statusC, string pcId, string ProductCat1, string ProductCat2, string StartDate, string enddate,
             HttpPostedFileBase[] postedfiles, HttpPostedFileBase Brochurename, string DistriCheck, string DealCheck, string Rating, string ParentCatid, HttpPostedFileBase[] multiplepostedfiles, string MaximumChargeCurrent, string NoOfBattery, string SupportedBatteryType, string Maximumbulbload, string Technology, string NominalVoltage, string DimensionMM, string Weight_Filled_battery)
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
                    int productCat2;
                    int parntcatid;
                    //int productCat3;
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = enddate;
                    ViewBag.ProductCat = pcId;
                    ViewBag.ProductCat1 = ProductCat1;
                    ViewBag.ProductCat2 = ProductCat2;
                    ViewBag.ParentCat = ParentCatid;

                    #region Check Validation for permission

                    Alls = Alls ?? "off";
                    rglist = rglist ?? "";
                    //if ((Alls.ToLower() == "off" || Alls == "") && (rglist == "" || rglist == "0"))
                    //{
                    //    ModelState.AddModelError("CreatedBy", "Permission For Has No Value");

                    //}
                    //else if ((Alls.ToLower() == "off" || Alls == "") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    //{
                    //    ModelState.AddModelError("CreatedBy", "Check Eiter Distributor OR Dealer");
                    //}

                    if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (rglist == "" || rglist == null || rglist == "0") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    {

                        ModelState.AddModelError("createdBy", "Permission For Has No Value");

                    }
                    else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    {
                        ModelState.AddModelError("createdBy", "Check Either Distributor OR Dealer");
                    }

                    #endregion

                    #region Check Validation For Image
                    if (postedfiles[0] == null)
                    {
                        ModelState.AddModelError("File", "*");
                        ViewBag.File = "File Is Not Uploaded";
                    }
                    else
                    {
                        foreach (HttpPostedFileBase file in postedfiles)
                        {
                            string FileExtension = Path.GetExtension(file.FileName);
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

                    }


                    #endregion

                    #region Check Validation For Carousal image
                    //if (multiplepostedfiles[0] == null)
                    //{
                    //    ModelState.AddModelError("File", "*");
                    //    ViewBag.File = "File Is Not Uploaded";
                    //}
                    if (multiplepostedfiles[0] != null)
                    {
                        foreach (HttpPostedFileBase file in multiplepostedfiles)
                        {
                            string FileExtension = Path.GetExtension(file.FileName);
                            if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                               FileExtension.ToLower() == ".png")
                            {

                            }
                            else
                            {
                                ModelState.AddModelError("File", "*");
                                ViewBag.FileCarousal = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                            }

                        }

                    }


                    #endregion

                    #region Check Validation For Start Date And End Date
                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "*");
                        ViewBag.StartDate = "Start Date Is Not Selected";
                    }
                    //else
                    //{
                    //    try
                    //    {

                    //        if (Convert.ToDateTime(StartDate) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    //        {
                    //            ModelState.AddModelError("StartDate", "*");
                    //            ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                    //        }
                    //        DateTime startDate = Convert.ToDateTime(StartDate);
                    //        if (db.ProductLevelThrees.Any(a => a.enddate >= startDate && a.PlTwStatus != 2))
                    //        {
                    //            ModelState.AddModelError("StartDate", "*");
                    //            ViewBag.StartDate = "There Is already A Product Defined In This Date";
                    //        }
                    //    }
                    //    catch (FormatException ex)
                    //    {
                    //        ModelState.AddModelError("StartDate", "Invalid Date");
                    //        ViewBag.StartDate = "Invalid Date";
                    //    }
                    //}
                    if (enddate == null || enddate == "")
                    {
                        ModelState.AddModelError("EndDate", "*");
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

                                if (Convert.ToDateTime(enddate) < Convert.ToDateTime(StartDate))
                                {
                                    ModelState.AddModelError("End Date", "*");
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


                    #region Validate Product Id

                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("productCategoryid", "Select Product Category");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("productCategoryid", "Select Product Category");
                    }

                    #endregion

                    #region Validate ParentCat Id

                    if (!int.TryParse(ParentCatid, out parntcatid))
                    {
                        ModelState.AddModelError("ParentCatid", "Select Parent Category");

                    }
                    if (parntcatid == 0)
                    {
                        ModelState.AddModelError("ParentCatid", "Select Parent Category");
                    }

                    #endregion

                    #region Rating validation

                    if (Rating == null || Rating == "")
                    {
                        ModelState.AddModelError("Rating", "Rating cannot be empty");

                    }



                    #endregion

                    //#region Validate ProductLevelThree
                    //if (!int.TryParse(ProductCat3, out productCat3))
                    //{
                    //    ModelState.AddModelError("productLevelThreefId", "Select Product Category");

                    //}
                    //if (productCat3 == 0)
                    //{
                    //    ModelState.AddModelError("productLevelThreefId", "Select Product Category");
                    //}
                    //#endregion

                    #region Validate Product Category One
                    if (!int.TryParse(ProductCat1, out productCat1))
                    {
                        ModelState.AddModelError("ProductLevelOne", "Select Product level One");

                    }
                    if (productCat1 == 0)
                    {
                        ModelState.AddModelError("ProductLevelOne", "Select Product level One");
                    }

                    #endregion

                    #region Validate Product Category Two
                    if (!int.TryParse(ProductCat2, out productCat2))
                    {
                        ModelState.AddModelError("ProductLevelTwo", "Select Product Category");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("ProductLevelTwo", "Select Product Category");
                    }
                    #endregion

                    #region Check Record Existstence
                    if (db.ProductLevelThrees.Any(a => a.productCategoryid == pcid && a.ProductLevelOne == productCat1 && a.pc_Lv2_oneId == productCat2 && a.Name.ToLower() == contactUs.Name.ToLower() && a.PlTwStatus != 2))
                    {
                        ModelState.AddModelError("Name", "Product Name Already Exists");
                    }
                    #endregion

                    #region technicalspecification


                    if (pcId == "15" || pcId == "9")
                    {
                        if (MaximumChargeCurrent.ToString() == "")
                        {

                            ModelState.AddModelError("MaximumChargeCurrent", "Maximum charging current cannot be empty");

                        }
                        if (NoOfBattery.ToString() == "")
                        {

                            ModelState.AddModelError("NoOfBattery", "Number of battery cannot be empty");

                        }
                        if (SupportedBatteryType.ToString() == "")
                        {

                            ModelState.AddModelError("SupportedBatteryType", "Supported battery type cannot be empty");

                        }
                        if (Maximumbulbload.ToString() == "")
                        {

                            ModelState.AddModelError("Maximumbulbload", "Maximum bulb load cannot be empty");

                        }
                        if (Technology.ToString() == "")
                        {

                            ModelState.AddModelError("Technology", "Technology cannot be empty");

                        }
                    }

                    if (pcId == "8")
                    {
                        if (NominalVoltage.ToString() == "")
                        {

                            ModelState.AddModelError("NominalVoltage", "Nominal Voltage cannot be empty");

                        }
                        if (DimensionMM.ToString() == "")
                        {

                            ModelState.AddModelError("DimensionMM", "Dimension(in MM) cannot be empty");

                        }
                        if (Weight_Filled_battery.ToString() == "")
                        {

                            ModelState.AddModelError("Weight_Filled_battery", "Weight(Filled battery) cannot be empty");

                        }

                    }

                    #endregion

                    #region Method To Save Record
                    if (ModelState.IsValid)
                    {
                        ProductLevelThree contactusd = new ProductLevelThree();
                        contactusd.Name = contactUs.Name;
                        contactusd.productCategoryid = pcid;
                        contactusd.ProductLevelOne = productCat1;
                        contactusd.pc_Lv2_oneId = productCat2;
                        contactusd.CreatedBy = Session["userid"].ToString();
                        contactusd.CreatedDate = DateTime.Now;
                        contactusd.startDate = Convert.ToDateTime(StartDate);
                        contactusd.enddate = Convert.ToDateTime(enddate);
                        //contactusd.productLevelThreefId = productCat3;
                        contactusd.PrDiscription = contactUs.PrDiscription;
                        contactusd.PrCode = contactUs.PrCode;
                        contactusd.KeyFeature = contactUs.KeyFeature;
                        contactusd.MRP = contactUs.MRP;
                        contactusd.Warrenty = contactUs.Warrenty;
                        contactusd.Rating = contactUs.Rating;
                        contactusd.ParentCatid = contactUs.ParentCatid;
                        contactusd.MaximumChargeCurrent = contactUs.MaximumChargeCurrent;
                        contactusd.NoOfBattery = contactUs.NoOfBattery;
                        contactusd.SupportedBatteryType = contactUs.SupportedBatteryType;
                        contactusd.Maximumbulbload = contactUs.Maximumbulbload;
                        contactusd.Technology = contactUs.Technology;
                        contactusd.NominalVoltage = contactUs.NominalVoltage;
                        contactusd.DimensionMM = contactUs.DimensionMM;
                        contactusd.Weight_Filled_battery = contactUs.Weight_Filled_battery;
                        string filename = "";
                        if (Brochurename != null)
                        {
                            filename = Path.GetFileNameWithoutExtension(Brochurename.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(Brochurename.FileName);
                            contactusd.brochure = filename;
                        }
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.PlTwStatus = 1;
                        }
                        else
                        {
                            contactusd.PlTwStatus = 0;
                        }
                        db.ProductLevelThrees.AddObject(contactusd);
                        int affectedValue = db.SaveChanges();
                        if (affectedValue > 0)
                        {
                            if (Brochurename != null)
                            {
                                Brochurename.SaveAs(Server.MapPath("~/ProductImages/") + filename);
                            }
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
                                        ProductAccessTableForProduct pat = new ProductAccessTableForProduct();
                                        pat.ProductId = contactusd.id;
                                        pat.RegId = int.Parse(s);
                                        pat.CreateDate = DateTime.Now;
                                        pat.createby = Session["userid"].ToString();
                                        pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                        pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                        db.ProductAccessTableForProducts.AddObject(pat);
                                        db.SaveChanges();

                                    }
                                }
                            }
                            else
                            {
                                ProductAccessTableForProduct pat = new ProductAccessTableForProduct();
                                pat.ProductId = contactusd.id;
                                pat.AllAcess = true;
                                db.ProductAccessTableForProducts.AddObject(pat);
                                db.SaveChanges();
                            }

                            if ((Alls.ToLower() != "on") && (rglist == "" || rglist == null) && (DistriCheck != "on" || DealCheck != "on"))
                            {
                                ProductAccessTableForProduct pat = new ProductAccessTableForProduct();
                                pat.ProductId = contactusd.id;
                                pat.RegId = null;
                                pat.CreateDate = DateTime.Now;
                                pat.createby = Session["userid"].ToString();
                                pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                db.ProductAccessTableForProducts.AddObject(pat);
                                db.SaveChanges();
                            }

                            #region Commented Code

                            //    if (disList != "0" && disList != "" && disList != null)
                            //    {
                            //        Regex rg = new Regex(",");
                            //        string[] reglist = rg.Split(disList);
                            //        foreach (string s in reglist)
                            //        {
                            //            ProductAccessTableForProduct pat2 = new ProductAccessTableForProduct();
                            //            pat2.ProductId = contactusd.id;
                            //            pat2.DestributorID = int.Parse(s);
                            //            pat2.CreateDate = DateTime.Now;
                            //            pat2.createby = Session["userid"].ToString();
                            //            db.ProductAccessTableForProducts.AddObject(pat2);
                            //            db.SaveChanges();

                            //        }
                            //    }
                            //    else
                            //    {
                            //        ProductAccessTableForProduct pat2 = new ProductAccessTableForProduct();
                            //        pat2.ProductId = contactusd.id;
                            //        pat2.AllDestriAccess = true;
                            //        pat2.CreateDate = DateTime.Now;
                            //        pat2.createby = Session["userid"].ToString();
                            //        db.ProductAccessTableForProducts.AddObject(pat2);
                            //        db.SaveChanges();

                            //    }



                            //    if (Dealist != "0" && Dealist != "" && Dealist != null)
                            //    {
                            //        Regex rg = new Regex(",");
                            //        string[] reglist = rg.Split(Dealist);
                            //        foreach (string s in reglist)
                            //        {
                            //            ProductAccessTableForProduct pat3 = new ProductAccessTableForProduct();
                            //            pat3.ProductId = contactusd.id;
                            //            pat3.DealerId = int.Parse(s);
                            //            pat3.CreateDate = DateTime.Now;
                            //            pat3.createby = Session["userid"].ToString();
                            //            db.ProductAccessTableForProducts.AddObject(pat3);
                            //            db.SaveChanges();

                            //        }
                            //    }
                            //    else
                            //    {
                            //        ProductAccessTableForProduct pat2 = new ProductAccessTableForProduct();
                            //        pat2.ProductId = contactusd.id;
                            //        pat2.AllDealerAccess = true;
                            //        pat2.CreateDate = DateTime.Now;
                            //        pat2.createby = Session["userid"].ToString();
                            //        db.ProductAccessTableForProducts.AddObject(pat2);
                            //        db.SaveChanges();

                            //    }
                            //}
                            //else
                            //{
                            //    ProductAccessTableForProduct pat3 = new ProductAccessTableForProduct();
                            //    pat3.ProductId = contactusd.id;
                            //    pat3.AllAcess = true;
                            //    pat3.CreateDate = DateTime.Now;
                            //    pat3.createby = Session["userid"].ToString();
                            //    db.ProductAccessTableForProducts.AddObject(pat3);
                            //    db.SaveChanges();
                            //}
                            #endregion
                            foreach (HttpPostedFileBase file in postedfiles)
                            {
                                ProductImage productImage = new ProductImage();
                                string ImageName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                                productImage.pc_Lv3_oneId = contactusd.id;
                                productImage.PrImage = ImageName;
                                productImage.PlTwStatus = 1;
                                db.ProductImages.AddObject(productImage);
                                if (db.SaveChanges() > 0)
                                {
                                    file.SaveAs(Server.MapPath("~/ProductImages/") + ImageName);
                                    ViewBag.preStartDate = "";
                                    ViewBag.PreEndDate = "";
                                    ViewBag.ProductCat = "";
                                    ViewBag.ProductCat1 = "";
                                    ViewBag.ProductCat2 = "";


                                }

                            }

                            if (multiplepostedfiles.Length > 0)
                            {
                                foreach (HttpPostedFileBase Primagemapping in multiplepostedfiles)
                                {
                                    ProductthreeImageMapping Pimagemapping = new ProductthreeImageMapping();
                                    Pimagemapping.ProductLevelThreeid = contactusd.id;
                                    string Primage = Primagemapping.FileName.Replace(" ", string.Empty);
                                    Pimagemapping.Primage = Primage;

                                    db.ProductthreeImageMappings.AddObject(Pimagemapping);
                                    if (db.SaveChanges() > 0)
                                    {
                                        Primagemapping.SaveAs(Server.MapPath("~/ProductImages/") + Primage);



                                    }

                                }
                            }


                            ViewBag.result = "Record Saved Successfully";
                        }
                        return RedirectToAction("Index");
                    }
                    #endregion
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }

        public JsonResult GetContactDetail(int? page, string ProductLvlthreeId, string Prntcategory, string productlvl1, string procat)
        {
            int? PagId = page;
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
                      int ProcatID;
                    int.TryParse(ProductLvlthreeId, out ProcatID);
                    int Parentcatid;
                    int.TryParse(Prntcategory, out Parentcatid);
                    int Prodlvl1;
                    int.TryParse(productlvl1, out Prodlvl1);
                    int ProcategoryID;
                    int.TryParse(procat, out ProcategoryID);
                    int contactdetails = (from c in db.ProductLevelThrees
                                          where c.PlTwStatus != 2 && c.pc_Lv2_oneId == ProcatID && c.ProductLevelOne == Prodlvl1 && c.productCategoryid == ProcategoryID && c.ParentCatid == Parentcatid
                                          orderby c.OrderSequence ascending
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
                        var contactDetails2 = (from c in db.ProductLevelThreePaging().Where(c => c.Productleveltwoid == ProcatID && c.ParentCatid == Parentcatid && c.productCategoryid == ProcategoryID && c.ProductLevelOne == Prodlvl1)

                                               select new
                                               {
                                                   id = c.id,
                                                   PCat = c.ProductCategory,
                                                   Name = c.ProductLevelThreeName,
                                                   proCatOne = c.ProductLeveloneName,
                                                   ProCatTwo = c.ProductwoName,
                                                   ProductCode = c.prcode,
                                                   Descriptions = c.prDiscription,
                                                   Rating = c.Rating,
                                                   StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                                                   EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                                                   Parentcat = c.ParentCategory,
                                                   status = c.pltwstatus == 1 ? "Active" : "Deactive",
                                                   ordersequence = c.OrderSequence

                                               }).Where(c => c.id.ToString().Contains(Session["Search"].ToString()) || c.PCat.Contains(Session["Search"].ToString()) || c.Name.Contains(Session["Search"].ToString()) || c.proCatOne.Contains(Session["Search"].ToString()) || c.ProCatTwo.Contains(Session["Search"].ToString()) || c.ProductCode.Contains(Session["Search"].ToString()) || c.Descriptions.Contains(Session["Search"].ToString()) || c.status.Contains(Session["Search"].ToString()) || c.StartDate.Contains(Session["Search"].ToString()) || c.EndDate.Contains(Session["Search"].ToString()) || c.Parentcat.Contains(Session["Search"].ToString())).OrderBy(a => a.ordersequence).ToList();

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

                        var contactDetails2 = (from c in db.ProductLevelThreePaging().Where(c => c.Productleveltwoid == ProcatID && c.ParentCatid == Parentcatid && c.productCategoryid == ProcategoryID && c.ProductLevelOne == Prodlvl1)

                                               select new
                                               {
                                                   id = c.id,
                                                   PCat = c.ProductCategory,
                                                   Name = c.ProductLevelThreeName,
                                                   proCatOne = c.ProductLeveloneName,
                                                   ProCatTwo = c.ProductwoName,
                                                   ProductCode = c.prcode,
                                                   Descriptions = c.prDiscription,
                                                   Rating = c.Rating,
                                                   Parentcat = c.ParentCategory,
                                                   StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                                                   EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                                                   status = c.pltwstatus == 1 ? "Active" : "Deactive",
                                                ordersequence=c.OrderSequence

                                               }).OrderBy(a => a.ordersequence).ToList();
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
                    ProductLevelThree cud = db.ProductLevelThrees.Single(a => a.id == id);
                    ViewBag.status = cud.PlTwStatus;
                    ViewBag.preStartDate = Convert.ToDateTime(cud.startDate).ToString("dd-MM-yyyy");
                    ViewBag.PreEndDate = Convert.ToDateTime(cud.enddate).ToString("dd-MM-yyyy");
                    ViewBag.ProductCat = cud.productCategoryid;
                    ViewBag.ProductCat1 = cud.ProductLevelOne;
                    ViewBag.ProductCat2 = cud.pc_Lv2_oneId;
                    ViewBag.Prntid = cud.ParentCatid;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Update(ProductLevelThree contactUs, string statusC, string pcId, string ProductCat1, string ProductCat2, string StartDate, string enddate, HttpPostedFileBase Brochurename, string Rating, string ParentCatid, string MaximumChargeCurrent, string NoOfBattery, string SupportedBatteryType, string Maximumbulbload, string Technology, string NominalVoltage, string DimensionMM, string Weight_Filled_battery)
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
                    int productCat2;
                    int parntcat;
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = enddate;
                    ViewBag.ProductCat = pcId;
                    ViewBag.ProductCat1 = ProductCat1;
                    ViewBag.ProductCat2 = ProductCat2;
                    ViewBag.parntcat = ParentCatid;
                    ProductLevelThree pHistory;
                    #region Check Validation For Start Date And End Date
                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "*");
                        ViewBag.StartDate = "Start Date Is Not Selected";
                    }
                    //else
                    //{
                    //    try
                    //    {
                    //        if (Convert.ToDateTime(StartDate) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    //        {
                    //            ModelState.AddModelError("StartDate", "*");
                    //            ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                    //        }
                    //        DateTime startDate = Convert.ToDateTime(StartDate);
                    //        if (db.ProductLevelThrees.Any(a => a.enddate >= startDate && a.PlTwStatus != 2 && a.id != contactUs.id))
                    //        {
                    //            ModelState.AddModelError("StartDate", "*");
                    //            ViewBag.StartDate = "There Is already A Product Defined In This Date";
                    //        }
                    //    }
                    //    catch (FormatException ex)
                    //    {
                    //        ModelState.AddModelError("StartDate", "Invalid Date");
                    //        ViewBag.StartDate = "Invalid Date";
                    //    }

                    //}
                    if (enddate == null || enddate == "")
                    {
                        ModelState.AddModelError("EndDate", "*");
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

                                if (Convert.ToDateTime(enddate) < Convert.ToDateTime(StartDate))
                                {
                                    ModelState.AddModelError("End Date", "*");
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

                    #region Validate Product Id

                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("productCategoryid", "Select Product Category");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("productCategoryid", "Select Product Category");
                    }

                    #endregion

                    #region Validate Product Category One
                    if (!int.TryParse(ProductCat1, out productCat1))
                    {
                        ModelState.AddModelError("ProductLevelOne", "Select Product level One");

                    }
                    if (productCat1 == 0)
                    {
                        ModelState.AddModelError("ProductLevelOne", "Select Product level One");
                    }

                    #endregion

                    #region Validate Product Category Two
                    if (!int.TryParse(ProductCat2, out productCat2))
                    {
                        ModelState.AddModelError("ProductLevelTwo", "Select Product Category");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("ProductLevelTwo", "Select Product Category");
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


                    #region Rating validation

                    if (Rating == null || Rating == "")
                    {
                        ModelState.AddModelError("Rating", "Rating cannot be empty");

                    }



                    #endregion

                    #region Check Record Existstence
                    if (db.ProductLevelThrees.Any(a => a.productCategoryid == pcid && a.ProductLevelOne == productCat1 && a.pc_Lv2_oneId == productCat2 && a.id != contactUs.id && a.Name.ToLower() == contactUs.Name.ToLower() && a.PlTwStatus != 2))
                    {
                        ModelState.AddModelError("Name", "Product Name Already Exists");
                    }
                    #endregion

                    #region technicalspecification


                    if (pcId == "15" || pcId == "9")
                    {
                        if (MaximumChargeCurrent.ToString() == "")
                        {

                            ModelState.AddModelError("MaximumChargeCurrent", "Maximum charging current cannot be empty");

                        }
                        if (NoOfBattery.ToString() == "")
                        {

                            ModelState.AddModelError("NoOfBattery", "Number of battery cannot be empty");

                        }
                        if (SupportedBatteryType.ToString() == "")
                        {

                            ModelState.AddModelError("SupportedBatteryType", "Supported battery type cannot be empty");

                        }
                        if (Maximumbulbload.ToString() == "")
                        {

                            ModelState.AddModelError("Maximumbulbload", "Maximum bulb load cannot be empty");

                        }
                        if (Technology.ToString() == "")
                        {

                            ModelState.AddModelError("Technology", "Technology cannot be empty");

                        }
                    }

                    if (pcId == "8")
                    {
                        if (NominalVoltage.ToString() == "")
                        {

                            ModelState.AddModelError("NominalVoltage", "Nominal Voltage cannot be empty");

                        }
                        if (DimensionMM.ToString() == "")
                        {

                            ModelState.AddModelError("DimensionMM", "Dimension(in MM) cannot be empty");

                        }
                        if (Weight_Filled_battery.ToString() == "")
                        {

                            ModelState.AddModelError("Weight_Filled_battery", "Weight(Filled battery) cannot be empty");

                        }

                    }

                    #endregion

                    if (ModelState.IsValid)
                    {
                        ProductLevelThree contactusd = db.ProductLevelThrees.Single(a => a.id == contactUs.id);

                        //Save Previous Record In History
                        ProductLevelThreeHistory CUDHistory = new ProductLevelThreeHistory();
                        CUDHistory.Name = contactusd.Name;
                        CUDHistory.Pr_Lvl_3_ID = contactusd.id;
                        CUDHistory.productCategoryid = contactusd.productCategoryid;
                        CUDHistory.ProductLevelOne = contactusd.ProductLevelOne;
                        CUDHistory.pc_Lv2_oneId = contactusd.pc_Lv2_oneId;
                        CUDHistory.startDate = contactusd.startDate;
                        CUDHistory.enddate = contactusd.enddate;
                        CUDHistory.PrDiscription = contactusd.PrDiscription;
                        CUDHistory.PrCode = contactusd.PrCode;
                        CUDHistory.ModifiedDate = DateTime.Now;
                        CUDHistory.ModifiedBy = Session["userid"].ToString();
                        CUDHistory.PlTwStatus = contactusd.PlTwStatus;
                        CUDHistory.KeyFeature = contactusd.KeyFeature;
                        CUDHistory.MRP = contactusd.MRP;
                        CUDHistory.Warrenty = contactusd.Warrenty;
                        CUDHistory.brochure = contactusd.brochure;
                        CUDHistory.Rating = contactusd.Rating;
                        CUDHistory.MaximumChargeCurrent = contactusd.MaximumChargeCurrent;
                        CUDHistory.NoOfBattery = contactusd.NoOfBattery;
                        CUDHistory.SupportedBatteryType = contactusd.SupportedBatteryType;
                        CUDHistory.Maximumbulbload = contactusd.Maximumbulbload;
                        CUDHistory.Technology = contactusd.Technology;
                        CUDHistory.NominalVoltage = contactusd.NominalVoltage;
                        CUDHistory.DimensionMM = contactusd.DimensionMM;
                        CUDHistory.Weight_Filled_battery = contactusd.Weight_Filled_battery;
                        db.ProductLevelThreeHistories.AddObject(CUDHistory);



                        //Update New Record

                        contactusd.Name = contactUs.Name;
                        contactusd.productCategoryid = pcid;
                        contactusd.ProductLevelOne = productCat1;
                        contactusd.pc_Lv2_oneId = productCat2;
                        contactusd.startDate = Convert.ToDateTime(StartDate);
                        contactusd.enddate = Convert.ToDateTime(enddate);
                        contactusd.PrDiscription = contactUs.PrDiscription;
                        contactusd.PrCode = contactUs.PrCode;
                        contactusd.ModifiedDate = DateTime.Now;
                        contactusd.ModifiedBy = Session["userid"].ToString();
                        contactusd.KeyFeature = contactUs.KeyFeature;
                        contactusd.MRP = contactUs.MRP;
                        contactusd.Warrenty = contactUs.Warrenty;
                        contactusd.Rating = contactUs.Rating;
                        contactusd.ParentCatid = contactUs.ParentCatid;
                        contactusd.MaximumChargeCurrent = contactUs.MaximumChargeCurrent;
                        contactusd.NoOfBattery = contactUs.NoOfBattery;
                        contactusd.SupportedBatteryType = contactUs.SupportedBatteryType;
                        contactusd.Maximumbulbload = contactUs.Maximumbulbload;
                        contactusd.Technology = contactUs.Technology;
                        contactusd.NominalVoltage = contactUs.NominalVoltage;
                        contactusd.DimensionMM = contactUs.DimensionMM;
                        contactusd.Weight_Filled_battery = contactUs.Weight_Filled_battery;
                        string status = statusC ?? "off";
                        string filename = "";
                        if (Brochurename != null)
                        {
                            filename = Path.GetFileNameWithoutExtension(Brochurename.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(Brochurename.FileName);

                            contactusd.brochure = filename;
                        }
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
                            if (Brochurename != null)
                            {
                                Brochurename.SaveAs(Server.MapPath("~/ProductImages/") + filename);
                            }

                            ViewBag.Result = "Record Updated Successfully";
                            return Content("<script>alert('Record Updated Successfully');location.href='../../ProductLevel3/Index'</script>");
                        }



                    }
                    return View("Edit", contactUs);
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
                    ProductLevelThree contactUs = db.ProductLevelThrees.Single(a => a.id == id);
                    ProductLevelThreeHistory CUDHistory = new ProductLevelThreeHistory();
                    CUDHistory.Name = contactUs.Name;
                    CUDHistory.Pr_Lvl_3_ID = contactUs.id;
                    CUDHistory.productCategoryid = contactUs.productCategoryid;
                    CUDHistory.ProductLevelOne = contactUs.ProductLevelOne;
                    CUDHistory.pc_Lv2_oneId = contactUs.pc_Lv2_oneId;
                    CUDHistory.startDate = contactUs.startDate;
                    CUDHistory.enddate = contactUs.enddate;
                    CUDHistory.PrDiscription = contactUs.PrDiscription;
                    CUDHistory.PrCode = contactUs.PrCode;
                    CUDHistory.ModifiedDate = DateTime.Now;
                    CUDHistory.ModifiedBy = Session["userid"].ToString();

                    CUDHistory.PlTwStatus = contactUs.PlTwStatus;

                    db.ProductLevelThreeHistories.AddObject(CUDHistory);

                    contactUs.PlTwStatus = 2;
                    db.SaveChanges();
                    return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }

        }
        [HttpGet]

        public ActionResult FullProductDetails(int id)
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
                    return View(db.ProductLevelThrees.Single(a => a.PlTwStatus != 2 && a.PlTwStatus != 0 && a.id == id));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }

        }

        public ActionResult EditProductPermission(int id)
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
                    Session["id"] = id;
                    //List<ProductAccessTableForProduct> Pat = db.ProductAccessTableForProducts.Where(a => a.ProductId == id && (a.deleted != true || a.deleted == null || a.deleted == false)).ToList();
                    List<GetProductLevelFourAccessTable_Result> Pat=  db.GetProductLevelFourAccessTable(id).ToList();
                    return View(Pat);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        
        }
        public ActionResult Save(string Alls, string rglist, string DistriCheck, string DealCheck)
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
                    Alls = Alls ?? "off";
                    DealCheck = DealCheck ?? "off";
                    DistriCheck = DistriCheck ?? "off";
                    int id = Convert.ToInt32(Session["id"].ToString());
                    #region Check Validation for permission

                    
                    rglist = rglist ?? "";
                    //if ((Alls.ToLower() == "off" || Alls == "") && (rglist == "" || rglist == "0"))
                    //{
                    //    ModelState.AddModelError("RegionName", "Permission For Has No Value");

                    //}
                    //else if ((Alls.ToLower() == "off" || Alls == "") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    //{
                    //    ModelState.AddModelError("CreatedBy", "Check Eiter Distributor OR Dealer");
                    //}

                    if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (rglist == "" || rglist == null || rglist == "0") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    {

                        ModelState.AddModelError("RegionName", "Permission For Has No Value");

                    }
                    else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    {
                        ModelState.AddModelError("createdBy", "Check Either Distributor OR Dealer");
                    }

                    #endregion
                    if (ModelState.IsValid)
                    {
                        List<ProductAccessTableForProduct> patDel = db.ProductAccessTableForProducts.Where(a => a.ProductId == id).ToList();
                        foreach (var i in patDel)
                        {
                            db.DeleteObject(i);
                        }

                        if (Alls.ToLower() != "on")
                        {
                            if (rglist != "" && rglist != null)
                            {
                                Regex rg = new Regex(",");
                                string[] reglist = rg.Split(rglist);
                                foreach (string s in reglist)
                                {
                                    ProductAccessTableForProduct pat = new ProductAccessTableForProduct();
                                    pat.ProductId = id;
                                    pat.RegId = int.Parse(s);
                                    pat.CreateDate = DateTime.Now;
                                    pat.createby = Session["userid"].ToString();
                                    pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                    pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                    db.ProductAccessTableForProducts.AddObject(pat);
                                    db.SaveChanges();

                                }
                            }
                        }
                        else
                        {
                            ProductAccessTableForProduct pat = new ProductAccessTableForProduct();
                            pat.ProductId = id;
                            pat.AllAcess = true;
                            db.ProductAccessTableForProducts.AddObject(pat);
                            db.SaveChanges();
                        }
                        if ((Alls.ToLower() != "on") && (rglist == "" || rglist == null) && (DistriCheck != "on" || DealCheck != "on"))
                        {
                            ProductAccessTableForProduct pat = new ProductAccessTableForProduct();
                            pat.ProductId = id;
                            pat.RegId = null;
                            pat.CreateDate = DateTime.Now;
                            pat.createby = Session["userid"].ToString();
                            pat.AllDealerAccess = DealCheck == "off" ? false : true;
                            pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                            db.ProductAccessTableForProducts.AddObject(pat);
                            db.SaveChanges();
                        }
#region CommentedCode
                        //    if (disList != "0" && disList != "" && disList != null)
                        //    {
                        //        Regex rg = new Regex(",");
                        //        string[] reglist = rg.Split(disList);
                        //        foreach (string s in reglist)
                        //        {
                        //            ProductAccessTableForProduct pat2 = new ProductAccessTableForProduct();
                        //            pat2.ProductId = id;
                        //            pat2.DestributorID = int.Parse(s);
                        //            pat2.CreateDate = DateTime.Now;
                        //            pat2.createby = Session["userid"].ToString();
                        //            db.ProductAccessTableForProducts.AddObject(pat2);
                        //            db.SaveChanges();

                        //        }
                        //    }
                        //    else
                        //    {
                        //        ProductAccessTableForProduct pat2 = new ProductAccessTableForProduct();
                        //        pat2.ProductId = id;
                        //        pat2.AllDestriAccess = true;
                        //        pat2.CreateDate = DateTime.Now;
                        //        pat2.createby = Session["userid"].ToString();
                        //        db.ProductAccessTableForProducts.AddObject(pat2);
                        //        db.SaveChanges();

                        //    }



                        //    if (Dealist != "0" && Dealist != "" && Dealist != null)
                        //    {
                        //        Regex rg = new Regex(",");
                        //        string[] reglist = rg.Split(Dealist);
                        //        foreach (string s in reglist)
                        //        {
                        //            ProductAccessTableForProduct pat3 = new ProductAccessTableForProduct();
                        //            pat3.ProductId = id;
                        //            pat3.DealerId = int.Parse(s);
                        //            pat3.CreateDate = DateTime.Now;
                        //            pat3.createby = Session["userid"].ToString();
                        //            db.ProductAccessTableForProducts.AddObject(pat3);
                        //            db.SaveChanges();

                        //        }
                        //    }
                        //    else
                        //    {
                        //        ProductAccessTableForProduct pat2 = new ProductAccessTableForProduct();
                        //        pat2.ProductId = id;
                        //        pat2.AllDealerAccess = true;
                        //        pat2.CreateDate = DateTime.Now;
                        //        pat2.createby = Session["userid"].ToString();
                        //        db.ProductAccessTableForProducts.AddObject(pat2);
                        //        db.SaveChanges();

                        //    }
                        //}
                        //else
                        //{
                        //    ProductAccessTableForProduct pat3 = new ProductAccessTableForProduct();
                        //    pat3.ProductId = id;
                        //    pat3.AllAcess = true;
                        //    pat3.CreateDate = DateTime.Now;
                        //    pat3.createby = Session["userid"].ToString();
                        //    db.ProductAccessTableForProducts.AddObject(pat3);
                        //    db.SaveChanges();
                        //}
#endregion

                        return RedirectToAction("EditProductPermission", new { id = id });
                    }

                    return Content("<script>alert('Permisson For Has No Value');location.href='../ProductLevel3/EditProductPermission/" + id + "';</script>");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }
        [HttpPost]
        public ActionResult UpdateSequence(string ids)
        {
            var Prodsplit = ids.Split(',');
            string prodid = Prodsplit[0];
            string[] prdlvl3 = Regex.Split(prodid, @"\D+");
            int productthreeid = Convert.ToInt32(prdlvl3[1]);
            ProductLevelThree bn = db.ProductLevelThrees.Single(c => c.id == productthreeid);
            bn.OrderSequence = Convert.ToInt32(Prodsplit[1]);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
