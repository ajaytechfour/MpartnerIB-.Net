using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;


namespace Luminous.Controllers
{
    public class ProductLevel3Controller : MultiLanguageController
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
              //  DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["uview"].ToString() == "1"*/)
                {
                    return View();
                }
                //else
                //{
                //    return RedirectToAction("snotallowed", "snotallowed");
                //}
            }
        }
        public JsonResult GetProductLevelTwoCategory(string procatid)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
               // DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["uview"].ToString() == "1"*/)
                {
                    int ProcatID;
                    int.TryParse(procatid, out ProcatID);
                    //int ProductlevelOneId;
                    //int.TryParse(procatone, out ProductlevelOneId);
                    var Company = (from c in db.ProductLevelTwoes
                                   where c.PlTwStatus != 2 && c.PlTwStatus != 0 && c.PrductID == ProcatID
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
            HttpPostedFileBase Brochurename, string DistriCheck, string DealCheck, string Rating, string ParentCatid, HttpPostedFileBase[] multiplepostedfiles, string MaximumChargeCurrent, string NoOfBattery, string SupportedBatteryType, string Maximumbulbload, string Technology, string NominalVoltage, string DimensionMM,
          string Weight_Filled_battery, HttpPostedFileBase userupload_file,
                string RatedCapacity,

                string FilledWeight,
                string DCOutputVoltage,
                string DCOutputCurrent,
                string MaxSupportedPanelpower,
                string MaxSolarPanelVoltage,
                string VA,
                string NoofCells,
                string PeakPowerPMax,
                string RatedModuleVoltage,
                string MaximumPowerVoltage,
                string MaximumPowerCurrent,
                string NominalDCOutputVoltage,
                string MaxDCOutputCurrent,
                string Noof12VBatteriesinSeries,
                string SolarLength,
                string SolarWidth,
                string Heightuptofloattop,
                string DryWeight,
                string RatedACpower,
                string OperatingVoltage,
                string ChargeControllerRating,
                string NominalBatterybankvoltage,
                string InputVoltageWorkingRange,
                string OutputVoltageWorkingRange,
                string MainsACLowCut,
                string MainACLowCutRecovery,
                string[] redirectpage,
                string[] inputval
                )
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
               // DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["createrole"].ToString() == "1"*/)
                {
                    int pcid;
                    // int productCat1;
                    int productCat2;
                    int parntcatid;
                    //int productCat3;
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = enddate;
                    ViewBag.ProductCat = pcId;
                    ViewBag.ProductCat1 = ProductCat1;
                    ViewBag.ProductCat2 = ProductCat2;
                    ViewBag.ParentCat = ParentCatid;

                    int j = 0;
                    string[] item = new string[redirectpage.Count()];
                    var column = db.GetColumnNames().ToList();

                    for (j = 0; j < redirectpage.Count(); j++)
                    {
                        var temp = column.Find(x => x.ORDINAL_POSITION == Convert.ToInt32(redirectpage[j]));
                        item[j] = temp.COLUMN_NAME;

                    }


                    #region Check Validation for permission

                    if (userupload_file == null)
                    {
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
                    }

                    #endregion

                    //#region Check Validation For Image
                    //if (postedfiles[0] == null)
                    //{
                    //    ModelState.AddModelError("File", "*");
                    //    ViewBag.File = "File Is Not Uploaded";
                    //}
                    //else
                    //{
                    //    foreach (HttpPostedFileBase file in postedfiles)
                    //    {
                    //        string FileExtension = Path.GetExtension(file.FileName);
                    //        if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                    //           FileExtension.ToLower() == ".png")
                    //        {

                    //        }
                    //        else
                    //        {
                    //            ModelState.AddModelError("File", "*");
                    //            ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                    //        }

                    //    }

                    //}


                    //#endregion

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

                    //#region Rating validation

                    //if (Rating == null || Rating == "")
                    //{
                    //    ModelState.AddModelError("Rating", "Rating cannot be empty");

                    //}



                    //#endregion

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

                    //#region Validate Product Category One
                    //if (!int.TryParse(ProductCat1, out productCat1))
                    //{
                    //    ModelState.AddModelError("ProductLevelOne", "Select Product level One");

                    //}
                    //if (productCat1 == 0)
                    //{
                    //    ModelState.AddModelError("ProductLevelOne", "Select Product level One");
                    //}

                    //#endregion

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

                    //#region Check Record Existstence
                    //if (db.ProductLevelThrees.Any(a => a.productCategoryid == pcid && a.pc_Lv2_oneId == productCat2 && a.Name.ToLower() == contactUs.Name.ToLower() && a.PlTwStatus != 2))
                    //{
                    //    ModelState.AddModelError("Name", "Product Name Already Exists");
                    //}
                    //#endregion

                    //#region technicalspecification


                    //if (pcId == "15" || pcId == "9")
                    //{
                    //    if (MaximumChargeCurrent.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("MaximumChargeCurrent", "Maximum charging current cannot be empty");

                    //    }
                    //    if (NoOfBattery.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("NoOfBattery", "Number of battery cannot be empty");

                    //    }
                    //    if (SupportedBatteryType.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("SupportedBatteryType", "Supported battery type cannot be empty");

                    //    }
                    //    if (Maximumbulbload.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("Maximumbulbload", "Maximum bulb load cannot be empty");

                    //    }
                    //    if (Technology.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("Technology", "Technology cannot be empty");

                    //    }
                    //}

                    //if (pcId == "8")
                    //{
                    //    if (NominalVoltage.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("NominalVoltage", "Nominal Voltage cannot be empty");

                    //    }
                    //    if (DimensionMM.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("DimensionMM", "Dimension(in MM) cannot be empty");

                    //    }
                    //    if (Weight_Filled_battery.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("Weight_Filled_battery", "Weight(Filled battery) cannot be empty");

                    //    }

                    //}

                    //#endregion

                    #region Method To Save Record
                    if (ModelState.IsValid)
                    {
                        ProductLevelThree contactusd = new ProductLevelThree();

                        for (int i = 0; i < item.Count(); i++)
                        {

                            if (item[i] == "PrDiscription")
                            {
                                contactusd.PrDiscription = inputval[i];
                            }

                            if (item[i] == "productLevelThreefId")
                            {
                                contactusd.productLevelThreefId = Convert.ToInt32(inputval[i]);
                            }

                            if (item[i] == "MRP")
                            {
                                contactusd.MRP = Convert.ToDecimal(inputval[i]);
                            }

                            if (item[i] == "brochure")
                            {
                                contactusd.brochure = inputval[i];
                            }
                            if (item[i] == "Rating")
                            {
                                contactusd.Rating = inputval[i];
                            }

                            if (item[i] == "OrderSequence")
                            {
                                contactusd.OrderSequence = Convert.ToInt32(inputval[i]);
                            }

                            if (item[i] == "MaximumChargeCurrent")
                            {
                                contactusd.MaximumChargeCurrent = inputval[i];
                            }

                            if (item[i] == "NoOfBattery")
                            {
                                contactusd.NoOfBattery = inputval[i];
                            }
                            if (item[i] == "SupportedBatteryType")
                            {
                                contactusd.SupportedBatteryType = inputval[i];
                            }

                            if (item[i] == "Maximumbulbload")
                            {
                                contactusd.Maximumbulbload = inputval[i];
                            }

                            if (item[i] == "Technology")
                            {
                                contactusd.Technology = inputval[i];
                            }

                            if (item[i] == "NominalVoltage")
                            {
                                contactusd.NominalVoltage = inputval[i];
                            }
                            if (item[i] == "DimensionMM")
                            {
                                contactusd.DimensionMM = inputval[i];
                            }

                            if (item[i] == "Weight_Filled_battery")
                            {
                                contactusd.Weight_Filled_battery = inputval[i];
                            }

                            if (item[i] == "RatedCapacity")
                            {
                                contactusd.RatedCapacity = inputval[i];
                            }

                            if (item[i] == "FilledWeight")
                            {
                                contactusd.FilledWeight = inputval[i];
                            }
                            if (item[i] == "DCOutputVoltage")
                            {
                                contactusd.DCOutputVoltage = inputval[i];
                            }

                            if (item[i] == "DCOutputCurrent")
                            {
                                contactusd.DCOutputCurrent = inputval[i];
                            }

                            if (item[i] == "MaxSupportedPanelpower ")
                            {
                                contactusd.MaxSupportedPanelpower = inputval[i];
                            }

                            if (item[i] == "MaxSolarPanelVoltage")
                            {
                                contactusd.MaxSolarPanelVoltage = inputval[i];
                            }
                            if (item[i] == "VA")
                            {
                                contactusd.VA = inputval[i];
                            }

                            if (item[i] == "NoofCells")
                            {
                                contactusd.NoofCells = inputval[i];
                            }

                            if (item[i] == "PeakPowerPMax")
                            {
                                contactusd.PeakPowerPMax = inputval[i];
                            }

                            if (item[i] == "RatedModuleVoltage")
                            {
                                contactusd.RatedModuleVoltage = inputval[i];
                            }
                            if (item[i] == "MaximumPowerVoltage")
                            {
                                contactusd.MaximumPowerVoltage = inputval[i];
                            }

                            if (item[i] == "MaximumPowerCurrent")
                            {
                                contactusd.MaximumPowerCurrent = inputval[i];
                            }

                            if (item[i] == "NominalDCOutputVoltage")
                            {
                                contactusd.NominalDCOutputVoltage = inputval[i];
                            }

                            if (item[i] == "MaxDCOutputCurrent")
                            {
                                contactusd.MaxDCOutputCurrent = inputval[i];
                            }
                            if (item[i] == "Noof12VBatteriesinSeries")
                            {
                                contactusd.Noof12VBatteriesinSeries = inputval[i];
                            }

                            if (item[i] == "Weight_Filled_battery")
                            {
                                contactusd.Weight_Filled_battery = inputval[i];
                            }

                            if (item[i] == "SolarLength")
                            {
                                contactusd.SolarLength = inputval[i];
                            }

                            if (item[i] == "SolarWidth")
                            {
                                contactusd.SolarWidth = inputval[i];
                            }
                            if (item[i] == "Heightuptofloattop")
                            {
                                contactusd.Heightuptofloattop = inputval[i];
                            }

                            if (item[i] == "DryWeight")
                            {
                                contactusd.DryWeight = inputval[i];
                            }
                            if (item[i] == "RatedACpower")
                            {
                                contactusd.RatedACpower = inputval[i];
                            }

                            if (item[i] == "OperatingVoltage")
                            {
                                contactusd.OperatingVoltage = inputval[i];
                            }

                            if (item[i] == "ChargeControllerRating")
                            {
                                contactusd.ChargeControllerRating = inputval[i];
                            }

                            if (item[i] == "NominalBatterybankvoltage")
                            {
                                contactusd.NominalBatterybankvoltage = inputval[i];
                            }
                            if (item[i] == "InputVoltageWorkingRange")
                            {
                                contactusd.InputVoltageWorkingRange = inputval[i];
                            }
                            if (item[i] == "OutputVoltageWorkingRange")
                            {
                                contactusd.OutputVoltageWorkingRange = inputval[i];
                            }

                            if (item[i] == "MainsACLowCut")
                            {
                                contactusd.MainsACLowCut = inputval[i];
                            }

                            if (item[i] == "MainACLowCutRecovery")
                            {
                                contactusd.MainACLowCutRecovery = inputval[i];
                            }
                        }



                        contactusd.Name = contactUs.Name;
                        contactusd.productCategoryid = pcid;
                        contactusd.ProductLevelOne = Convert.ToInt32(ProductCat1);
                        contactusd.pc_Lv2_oneId = productCat2;
                        contactusd.CreatedBy = Session["userid"].ToString();
                        contactusd.CreatedDate = DateTime.Now;
                        contactusd.startDate = Convert.ToDateTime(StartDate);
                        contactusd.enddate = Convert.ToDateTime(enddate);
                        //contactusd.productLevelThreefId = productCat3;
                        // contactusd.PrDiscription = contactUs.PrDiscription;
                        contactusd.PrCode = contactUs.PrCode;
                        contactusd.KeyFeature = contactUs.KeyFeature;
                        // contactusd.MRP = contactUs.MRP;
                        contactusd.Warrenty = contactUs.Warrenty;
                        contactusd.Rating = contactUs.Rating;

                        contactusd.ParentCatid = contactUs.ParentCatid;
                        //contactusd.MaximumChargeCurrent = contactUs.MaximumChargeCurrent;
                        //contactusd.NoOfBattery = contactUs.NoOfBattery;
                        //contactusd.SupportedBatteryType = contactUs.SupportedBatteryType;
                        //contactusd.Maximumbulbload = contactUs.Maximumbulbload;
                        //contactusd.Technology = contactUs.Technology;
                        //contactusd.NominalVoltage = contactUs.NominalVoltage;
                        //contactusd.DimensionMM = contactUs.DimensionMM;
                        //contactusd.Weight_Filled_battery = contactUs.Weight_Filled_battery;

                        //contactusd.RatedCapacity = contactUs.RatedCapacity;
                        //contactusd.FilledWeight = contactUs.FilledWeight;
                        //contactusd.DCOutputVoltage = contactUs.DCOutputVoltage;
                        //contactusd.DCOutputCurrent = contactUs.DCOutputCurrent;
                        //contactusd.MaxSupportedPanelpower = contactUs.MaxSupportedPanelpower;
                        //contactusd.MaxSolarPanelVoltage = contactUs.MaxSolarPanelVoltage;
                        //contactusd.VA = contactUs.VA;
                        //contactusd.NoofCells = contactUs.NoofCells;
                        //contactusd.PeakPowerPMax = contactUs.PeakPowerPMax;
                        //contactusd.RatedModuleVoltage = contactUs.RatedModuleVoltage;
                        //contactusd.MaximumPowerVoltage = contactUs.MaximumPowerVoltage;
                        //contactusd.MaximumPowerCurrent = contactUs.MaximumPowerCurrent;
                        //contactusd.NominalDCOutputVoltage = contactUs.NominalDCOutputVoltage;
                        //contactusd.MaxDCOutputCurrent = contactUs.MaxDCOutputCurrent;
                        //contactusd.Noof12VBatteriesinSeries = contactUs.Noof12VBatteriesinSeries;
                        //contactusd.SolarLength = contactUs.SolarLength;
                        //contactusd.SolarWidth = contactUs.SolarWidth;
                        //contactusd.Heightuptofloattop = contactUs.Heightuptofloattop;
                        //contactusd.DryWeight = contactUs.DryWeight;
                        //contactusd.RatedACpower = contactUs.RatedACpower;
                        //contactusd.OperatingVoltage = contactUs.OperatingVoltage;
                        //contactusd.ChargeControllerRating = contactUs.ChargeControllerRating;
                        //contactusd.NominalBatterybankvoltage = contactUs.NominalBatterybankvoltage;
                        //contactusd.InputVoltageWorkingRange = contactUs.InputVoltageWorkingRange;
                        //contactusd.OutputVoltageWorkingRange = contactUs.OutputVoltageWorkingRange;
                        //contactusd.MainsACLowCut = contactUs.MainsACLowCut;
                        //contactusd.MainACLowCutRecovery = contactUs.MainACLowCutRecovery;


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

                                            ProductAccessTableForProduct pat = new ProductAccessTableForProduct();
                                            pat.ProductId = contactusd.id;
                                            pat.RegId = null;
                                            pat.CreateDate = DateTime.Now;
                                            pat.createby = Session["userid"].ToString();
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
                                            db.ProductAccessTableForProducts.AddObject(pat);
                                            db.SaveChanges();



                                        }
                                    }
                                }

                            }


                            //foreach (HttpPostedFileBase file in postedfiles)
                            //{
                            //    ProductImages_New productImage = new ProductImages_New();
                            //    string ImageName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                            //    productImage.pc_Lv3_oneId = contactusd.id;
                            //    productImage.PrImage = ImageName;
                            //    productImage.PlTwStatus = 1;
                            //    db.ProductImages_New.AddObject(productImage);
                            //    if (db.SaveChanges() > 0)
                            //    {
                            //        file.SaveAs(Server.MapPath("~/ProductImages/") + ImageName);
                            //        ViewBag.preStartDate = "";
                            //        ViewBag.PreEndDate = "";
                            //        ViewBag.ProductCat = "";
                            //        ViewBag.ProductCat1 = "";
                            //        ViewBag.ProductCat2 = "";


                            //    }

                            //}

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


        public JsonResult Column_Name()
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
              //  DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["uview"].ToString() == "1"*/)
                {
                    var data = db.GetColumnNamess();


                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }



        }

        public JsonResult GetContactDetail()
        {
            //int? PagId = page;
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
               // DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["uview"].ToString() == "1"*/)
                {
                    int contactdetails = (from c in db.ProductLevelThrees
                                          where c.PlTwStatus != 2
                                          select c).Count();
                    //int totalrecord;
                    //if (page != null)
                    //{
                    //    page = (page - 1) * 15;
                    //}
                    //if (contactdetails % 15 == 0)
                    //{
                    //    totalrecord = contactdetails / 15;
                    //}
                    //else
                    //{
                    //    totalrecord = (contactdetails / 15) + 1;
                    //}
                    //if (Session["Search"] != null)
                    //{
                    //    var contactDetails2 = (from c in db.ProductLevelThreePaging(PagId ?? 1, 100)

                    //                           select new
                    //                           {
                    //                               id = c.id,
                    //                               PCat = c.ProductCategory,
                    //                               Name = c.ProductLevelThreeName,
                    //                               proCatOne = c.ProductLeveloneName,
                    //                               ProCatTwo = c.ProductwoName,
                    //                               ProductCode = c.prcode,
                    //                               //  Descriptions = c.prDiscription,
                    //                               Rating = c.Rating,
                    //                               StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                    //                               EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                    //                               Parentcat = c.ParentCategory,
                    //                               status = c.pltwstatus == 1 ? "Active" : "Deactive",


                    //                           }).Where(c => c.id.ToString().Contains(Session["Search"].ToString()) || c.PCat.Contains(Session["Search"].ToString()) || c.Name.Contains(Session["Search"].ToString()) || c.proCatOne.Contains(Session["Search"].ToString()) || c.ProCatTwo.Contains(Session["Search"].ToString()) || c.ProductCode.Contains(Session["Search"].ToString()) || c.status.Contains(Session["Search"].ToString()) || c.StartDate.Contains(Session["Search"].ToString()) || c.EndDate.Contains(Session["Search"].ToString()) || c.Parentcat.Contains(Session["Search"].ToString())).ToList();

                    //    if (contactDetails2.Count == 0)
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
                    var contactDetails2 = (from c in db.ProductLevelThreePaging()

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
                                               StartDate = c.StartDate,
                                               EndDate = c.EndDate,
                                               status = c.pltwstatus == 1 ? "Active" : "Deactive",


                                           }).ToList();
                    var data = new { result = contactDetails2, TotalRecord = contactDetails2.Count() };

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
              //  DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");

                if (true /*result[0]["editrole"].ToString() == "1"*/)
                {
                    ProductLevelThree cud = db.ProductLevelThrees.Single(a => a.id == id);

                    int i = 0;

                    var temp1 = db.GetColumnNamess().ToList();

                    int[] arr = new int[temp1.Count()];
                    string[] arr1 = new string[temp1.Count()];

                    //int[] arr;
                    //string[] arr1;



                    var temp = db.ProductLevelThrees.Where(a => a.id == id).ToList();

                    ViewBag.primage = db.ProductthreeImageMappings.Where(x => x.ProductLevelThreeid == id).AsEnumerable().ElementAt(0).Primage;


                    if (temp.AsEnumerable().ElementAt(0).brochure != null)
                    {
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "brochure");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).brochure;

                    }
                    if (temp.AsEnumerable().ElementAt(0).MaximumChargeCurrent != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "MaximumChargeCurrent");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).MaximumChargeCurrent;

                    }
                    if (temp.AsEnumerable().ElementAt(0).NoOfBattery != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "NoOfBattery");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).NoOfBattery;

                    }
                    if (temp.AsEnumerable().ElementAt(0).SupportedBatteryType != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "SupportedBatteryType");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).SupportedBatteryType;

                    }
                    if (temp.AsEnumerable().ElementAt(0).Maximumbulbload != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "Maximumbulbload");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).Maximumbulbload;

                    }
                    if (temp.AsEnumerable().ElementAt(0).Technology != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "Technology");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).Technology;

                    }
                    if (temp.AsEnumerable().ElementAt(0).NominalVoltage != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "NominalVoltage");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).NominalVoltage;

                    }
                    if (temp.AsEnumerable().ElementAt(0).DimensionMM != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "DimensionMM");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).DimensionMM;

                    }
                    if (temp.AsEnumerable().ElementAt(0).Weight_Filled_battery != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "Weight_Filled_battery");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).Weight_Filled_battery;
                    }
                    if (temp.AsEnumerable().ElementAt(0).RatedCapacity != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "RatedCapacity");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).RatedCapacity;
                    }
                    if (temp.AsEnumerable().ElementAt(0).FilledWeight != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "FilledWeight");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).FilledWeight;
                    }
                    if (temp.AsEnumerable().ElementAt(0).DCOutputVoltage != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "DCOutputVoltage");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).DCOutputVoltage;
                    }
                    if (temp.AsEnumerable().ElementAt(0).DCOutputCurrent != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "DCOutputCurrent");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).DCOutputCurrent;
                    }
                    if (temp.AsEnumerable().ElementAt(0).MaxSupportedPanelpower != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "MaxSupportedPanelpower");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).MaxSupportedPanelpower;
                    }
                    if (temp.AsEnumerable().ElementAt(0).MaxSolarPanelVoltage != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "MaxSolarPanelVoltage");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).MaxSolarPanelVoltage;
                    }
                    if (temp.AsEnumerable().ElementAt(0).VA != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "VA");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).VA;
                    }
                    if (temp.AsEnumerable().ElementAt(0).NoofCells != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "NoofCells");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).NoofCells;
                    }
                    if (temp.AsEnumerable().ElementAt(0).PeakPowerPMax != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "PeakPowerPMax");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).PeakPowerPMax;
                    }
                    if (temp.AsEnumerable().ElementAt(0).RatedModuleVoltage != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "RatedModuleVoltage");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).RatedModuleVoltage;
                    }
                    if (temp.AsEnumerable().ElementAt(0).MaximumPowerVoltage != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "MaximumPowerVoltage");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).MaximumPowerVoltage;
                    }
                    if (temp.AsEnumerable().ElementAt(0).MaximumPowerCurrent != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "MaximumPowerCurrent");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).MaximumPowerCurrent;
                    }
                    if (temp.AsEnumerable().ElementAt(0).NominalDCOutputVoltage != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "NominalDCOutputVoltage");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).NominalDCOutputVoltage;
                    }
                    if (temp.AsEnumerable().ElementAt(0).MaxDCOutputCurrent != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "MaxDCOutputCurrent");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).MaxDCOutputCurrent;
                    }
                    if (temp.AsEnumerable().ElementAt(0).Noof12VBatteriesinSeries != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "Noof12VBatteriesinSeries");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).Noof12VBatteriesinSeries;
                    }
                    if (temp.AsEnumerable().ElementAt(0).SolarLength != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "SolarLength");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).SolarLength;
                    }
                    if (temp.AsEnumerable().ElementAt(0).SolarWidth != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "SolarWidth");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).SolarWidth;
                    }
                    if (temp.AsEnumerable().ElementAt(0).Heightuptofloattop != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "Heightuptofloattop");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).Heightuptofloattop;
                    }
                    if (temp.AsEnumerable().ElementAt(0).DryWeight != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "DryWeight");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).DryWeight;
                    }
                    if (temp.AsEnumerable().ElementAt(0).RatedACpower != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "RatedACpower");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).RatedACpower;
                    }
                    if (temp.AsEnumerable().ElementAt(0).OperatingVoltage != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "OperatingVoltage");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).OperatingVoltage;
                    }
                    if (temp.AsEnumerable().ElementAt(0).ChargeControllerRating != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "ChargeControllerRating");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).ChargeControllerRating;
                    }
                    if (temp.AsEnumerable().ElementAt(0).NominalBatterybankvoltage != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "NominalBatterybankvoltage");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).NominalBatterybankvoltage;
                    }
                    if (temp.AsEnumerable().ElementAt(0).InputVoltageWorkingRange != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "InputVoltageWorkingRange");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).InputVoltageWorkingRange;
                    }
                    if (temp.AsEnumerable().ElementAt(0).OutputVoltageWorkingRange != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "OutputVoltageWorkingRange");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).OutputVoltageWorkingRange;
                    }
                    if (temp.AsEnumerable().ElementAt(0).MainsACLowCut != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "MainsACLowCut");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).MainsACLowCut;
                    }
                    if (temp.AsEnumerable().ElementAt(0).MainACLowCutRecovery != null)
                    {
                        i++;
                        var temp2 = temp1.Find(x => x.COLUMN_NAME == "MainACLowCutRecovery");
                        arr[i] = Convert.ToInt32(temp2.ORDINAL_POSITION);
                        arr1[i] = temp.AsEnumerable().ElementAt(0).MainACLowCutRecovery;
                    }
                    ViewBag.redirectId = arr;
                    ViewBag.redirectvalue = arr1;


                    //ViewBag.redirect = arr;
                    //}

                    //}
                    //}





                    ViewBag.status = cud.PlTwStatus;
                    ViewBag.preStartDate = Convert.ToDateTime(cud.startDate).ToShortDateString();
                    ViewBag.PreEndDate = Convert.ToDateTime(cud.enddate).ToShortDateString(); ;


                    //if (cud.ProductLevelOne == null)
                    //{
                    //    ViewBag.ProductCat1 = "NA";
                    //}
                    //else
                    //{
                    //    ViewBag.ProductCat1 = cud.ProductLevelOne;
                    //    //ViewBag.ProductCat1 = cud.productCategoryid;

                    //}

                    ViewBag.ProductCat = cud.productCategoryid;
                    ViewBag.ProductCat1 = cud.ProductLevelOne;
                    ViewBag.ProductCat2 = cud.pc_Lv2_oneId;
                    ViewBag.Prntid = cud.ParentCatid;
                    ViewBag.Warranty = cud.Warrenty;
                    ViewBag.Rating = cud.Rating;


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
        public ActionResult Update(int? id, ProductLevelThree contactUs, string statusC, string pcId, string ProductCat1, string ProductCat2, string StartDate, string enddate, HttpPostedFileBase Brochurename, string Rating, string ParentCatid, string MaximumChargeCurrent, string NoOfBattery, string SupportedBatteryType, string Maximumbulbload, string Technology, string NominalVoltage, string DimensionMM, string Weight_Filled_battery,
            HttpPostedFileBase[] multiplepostedfiles,

            string RatedCapacity,
                string FilledWeight,
                string DCOutputVoltage,
                string DCOutputCurrent,
                string MaxSupportedPanelpower,
                string MaxSolarPanelVoltage,
                string VA,
                string NoofCells,
                string PeakPowerPMax,
                string RatedModuleVoltage,
                string MaximumPowerVoltage,
                string MaximumPowerCurrent,
                string NominalDCOutputVoltage,
                string MaxDCOutputCurrent,
                string Noof12VBatteriesinSeries,
                string SolarLength,
                string SolarWidth,
                string Heightuptofloattop,
                string DryWeight,
                string RatedACpower,
                string OperatingVoltage,
                string ChargeControllerRating,
                string NominalBatterybankvoltage,
                string InputVoltageWorkingRange,
                string OutputVoltageWorkingRange,
                string MainsACLowCut,
                string MainACLowCutRecovery,
                string[] redirectpage,
                string[] inputval
               )
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
               // DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["editrole"].ToString() == "1"*/)
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

                    string[] item = new string[redirectpage.Count()];

                    var column = db.GetColumnNames().ToList();

                    for (int i = 0; i < redirectpage.Count(); i++)
                    {
                        var temp = column.Find(x => x.ORDINAL_POSITION == Convert.ToInt32(redirectpage[i]));
                        item[i] = temp.COLUMN_NAME;
                    }

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


                    //#region Rating validation

                    //if (Rating == null || Rating == "")
                    //{
                    //    ModelState.AddModelError("Rating", "Rating cannot be empty");

                    //}



                    //#endregion

                    #region Check Record Existstence
                    if (db.ProductLevelThrees.Any(a => a.productCategoryid == pcid && a.ProductLevelOne == productCat1 && a.pc_Lv2_oneId == productCat2 && a.id != contactUs.id && a.Name.ToLower() == contactUs.Name.ToLower() && a.PlTwStatus != 2))
                    {
                        ModelState.AddModelError("Name", "Product Name Already Exists");
                    }
                    #endregion

                    //#region technicalspecification


                    //if (pcId == "15" || pcId == "9")
                    //{
                    //    if (MaximumChargeCurrent.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("MaximumChargeCurrent", "Maximum charging current cannot be empty");

                    //    }
                    //    if (NoOfBattery.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("NoOfBattery", "Number of battery cannot be empty");

                    //    }
                    //    if (SupportedBatteryType.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("SupportedBatteryType", "Supported battery type cannot be empty");

                    //    }
                    //    if (Maximumbulbload.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("Maximumbulbload", "Maximum bulb load cannot be empty");

                    //    }
                    //    if (Technology.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("Technology", "Technology cannot be empty");

                    //    }
                    //}

                    //if (pcId == "8")
                    //{
                    //    if (NominalVoltage.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("NominalVoltage", "Nominal Voltage cannot be empty");

                    //    }
                    //    if (DimensionMM.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("DimensionMM", "Dimension(in MM) cannot be empty");

                    //    }
                    //    if (Weight_Filled_battery.ToString() == "")
                    //    {

                    //        ModelState.AddModelError("Weight_Filled_battery", "Weight(Filled battery) cannot be empty");

                    //    }

                    //}

                    //#endregion

                    if (ModelState.IsValid)
                    {
                        ProductLevelThree contactusd = db.ProductLevelThrees.Single(a => a.id == contactUs.id);

                        //var datcount = db.ProductLevelThrees.Where(x => x.id == contactusd.id).Count();

                        int j = 0;
                        for (j = 0; j < item.Count(); j++)
                        {
                            if (item[j] == "brochure")
                            {
                                contactusd.brochure = inputval[j];
                            }
                            if (item[j] == "MaximumChargeCurrent")
                            {
                                contactusd.MaximumChargeCurrent = inputval[j];
                            }
                            if (item[j] == "NoOfBattery")
                            {
                                contactusd.NoOfBattery = inputval[j];
                            }
                            if (item[j] == "SupportedBatteryType")
                            {
                                contactusd.SupportedBatteryType = inputval[j];
                            }
                            if (item[j] == "Maximumbulbload")
                            {
                                contactusd.Maximumbulbload = inputval[j];
                            }
                            if (item[j] == "Technology")
                            {
                                contactusd.Technology = inputval[j];
                            }
                            if (item[j] == "NominalVoltage")
                            {
                                contactusd.NominalVoltage = inputval[j];
                            }
                            if (item[j] == "DimensionMM")
                            {
                                contactusd.DimensionMM = inputval[j];
                            }
                            if (item[j] == "Weight_Filled_battery")
                            {
                                contactusd.Weight_Filled_battery = inputval[j];
                            }
                            if (item[j] == "RatedCapacity")
                            {
                                contactusd.RatedCapacity = inputval[j];
                            }
                            if (item[j] == "FilledWeight")
                            {
                                contactusd.FilledWeight = inputval[j];
                            }
                            if (item[j] == "DCOutputVoltage")
                            {
                                contactusd.DCOutputVoltage = inputval[j];
                            }
                            if (item[j] == "DCOutputCurrent")
                            {
                                contactusd.DCOutputCurrent = inputval[j];
                            }
                            if (item[j] == "MaxSupportedPanelpower")
                            {
                                contactusd.MaxSupportedPanelpower = inputval[j];
                            }
                            if (item[j] == "MaxSolarPanelVoltage")
                            {
                                contactusd.MaxSolarPanelVoltage = inputval[j];
                            }
                            if (item[j] == "VA")
                            {
                                contactusd.VA = inputval[j];
                            }
                            if (item[j] == "NoofCells")
                            {
                                contactusd.NoofCells = inputval[j];
                            }
                            if (item[j] == "PeakPowerPMax")
                            {
                                contactusd.PeakPowerPMax = inputval[j];
                            }
                            if (item[j] == "RatedModuleVoltage")
                            {
                                contactusd.RatedModuleVoltage = inputval[j];
                            }
                            if (item[j] == "MaximumPowerVoltage")
                            {
                                contactusd.MaximumPowerVoltage = inputval[j];
                            }
                            if (item[j] == "MaximumPowerCurrent")
                            {
                                contactusd.MaximumPowerCurrent = inputval[j];
                            }
                            if (item[j] == "NominalDCOutputVoltage")
                            {
                                contactusd.NominalDCOutputVoltage = inputval[j];
                            }
                            if (item[j] == "MaxDCOutputCurrent")
                            {
                                contactusd.MaxDCOutputCurrent = inputval[j];
                            }
                            if (item[j] == "Noof12VBatteriesinSeries")
                            {
                                contactusd.Noof12VBatteriesinSeries = inputval[j];
                            }
                            if (item[j] == "SolarLength")
                            {
                                contactusd.SolarLength = inputval[j];
                            }
                            if (item[j] == "SolarWidth")
                            {
                                contactusd.SolarWidth = inputval[j];
                            }
                            if (item[j] == "Heightuptofloattop")
                            {
                                contactusd.Heightuptofloattop = inputval[j];
                            }
                            if (item[j] == "DryWeight")
                            {
                                contactusd.DryWeight = inputval[j];
                            }
                            if (item[j] == "RatedACpower")
                            {
                                contactusd.RatedACpower = inputval[j];
                            }
                            if (item[j] == "OperatingVoltage")
                            {
                                contactusd.OperatingVoltage = inputval[j];
                            }
                            if (item[j] == "ChargeControllerRating")
                            {
                                contactusd.ChargeControllerRating = inputval[j];
                            }
                            if (item[j] == "NominalBatterybankvoltage")
                            {
                                contactusd.NominalBatterybankvoltage = inputval[j];
                            }
                            if (item[j] == "InputVoltageWorkingRange")
                            {
                                contactusd.InputVoltageWorkingRange = inputval[j];
                            }
                            if (item[j] == "OutputVoltageWorkingRange")
                            {
                                contactusd.OutputVoltageWorkingRange = inputval[j];
                            }
                            if (item[j] == "MainsACLowCut")
                            {
                                contactusd.MainsACLowCut = inputval[j];
                            }
                            if (item[j] == "MainACLowCutRecovery")
                            {
                                contactusd.MainACLowCutRecovery = inputval[j];
                            }

                        }
                        if (multiplepostedfiles[0]!=null)
                        {
                            foreach (HttpPostedFileBase Primagemapping in multiplepostedfiles)
                            {
                                ProductthreeImageMapping img = db.ProductthreeImageMappings.Single(x => x.ProductLevelThreeid == id);
                                string Primage = Primagemapping.FileName.Replace(" ", string.Empty);
                                img.Primage = Primage;
                                Primagemapping.SaveAs(Server.MapPath("~/ProductImages/") + Primage);
                            }
                            //string filename1 = Path.GetFileNameWithoutExtension(multiplepostedfiles.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(multiplepostedfiles.FileName);
                            //string str1 = Path.Combine(Server.MapPath("~/ProductImages/"), filename1);
                            //multiplepostedfiles.SaveAs(str1);


                            //img.Primage = multiplepostedfiles.FileName;
                            //db.SaveChanges();
                        }

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

                        try
                        {
                            db.SaveChanges();
                        }
                        catch(Exception ex)
                        {

                        }


                        //Update New Record


                        contactusd.ParentCatid = contactUs.ParentCatid;
                        contactusd.productCategoryid = pcid;
                        contactusd.pc_Lv2_oneId = productCat2;
                        contactusd.ProductLevelOne = Convert.ToInt32(productCat1);
                        contactusd.Name = contactUs.Name;
                        contactusd.PrCode = contactUs.PrCode;
                        contactusd.KeyFeature = contactUs.KeyFeature;
                        contactusd.Warrenty = contactUs.Warrenty;
                        contactusd.Rating = contactUs.Rating;
                        contactusd.startDate = Convert.ToDateTime(StartDate);
                        contactusd.enddate = Convert.ToDateTime(enddate);
                        //contactusd.PrDiscription = contactUs.PrDiscription;


                        contactusd.ModifiedDate = DateTime.Now;
                        contactusd.ModifiedBy = Session["userid"].ToString();
                        ////contactusd.MRP = contactUs.MRP;


                        //contactusd.MaximumChargeCurrent = contactUs.MaximumChargeCurrent;
                        ////contactusd.NoOfBattery = contactUs.NoOfBattery;
                        //contactusd.SupportedBatteryType = contactUs.SupportedBatteryType;
                        //contactusd.Maximumbulbload = contactUs.Maximumbulbload;
                        //contactusd.Technology = contactUs.Technology;
                        //contactusd.NominalVoltage = contactUs.NominalVoltage;
                        //contactusd.DimensionMM = contactUs.DimensionMM;
                        //contactusd.Weight_Filled_battery = contactUs.Weight_Filled_battery;

                        //contactusd.RatedCapacity = contactUs.RatedCapacity;
                        //contactusd.FilledWeight = contactUs.FilledWeight;
                        //contactusd.DCOutputVoltage = contactUs.DCOutputVoltage;
                        //contactusd.DCOutputCurrent = contactUs.DCOutputCurrent;
                        //contactusd.MaxSupportedPanelpower = contactUs.MaxSupportedPanelpower;
                        //contactusd.MaxSolarPanelVoltage = contactUs.MaxSolarPanelVoltage;
                        //contactusd.VA = contactUs.VA;
                        //contactusd.NoofCells = contactUs.NoofCells;
                        //contactusd.PeakPowerPMax = contactUs.PeakPowerPMax;
                        //contactusd.RatedModuleVoltage = contactUs.RatedModuleVoltage;
                        //contactusd.MaximumPowerVoltage = contactUs.MaximumPowerVoltage;
                        //contactusd.MaximumPowerCurrent = contactUs.MaximumPowerCurrent;
                        //contactusd.NominalDCOutputVoltage = contactUs.NominalDCOutputVoltage;
                        //contactusd.MaxDCOutputCurrent = contactUs.MaxDCOutputCurrent;
                        //contactusd.Noof12VBatteriesinSeries = contactUs.Noof12VBatteriesinSeries;
                        //contactusd.SolarLength = contactUs.SolarLength;
                        //contactusd.SolarWidth = contactUs.SolarWidth;
                        //contactusd.Heightuptofloattop = contactUs.Heightuptofloattop;
                        //contactusd.DryWeight = contactUs.DryWeight;
                        //contactusd.RatedACpower = contactUs.RatedACpower;
                        //contactusd.OperatingVoltage = contactUs.OperatingVoltage;
                        //contactusd.ChargeControllerRating = contactUs.ChargeControllerRating;
                        //contactusd.NominalBatterybankvoltage = contactUs.NominalBatterybankvoltage;
                        //contactusd.InputVoltageWorkingRange = contactUs.InputVoltageWorkingRange;
                        //contactusd.OutputVoltageWorkingRange = contactUs.OutputVoltageWorkingRange;
                        //contactusd.MainsACLowCut = contactUs.MainsACLowCut;
                        //contactusd.MainACLowCutRecovery = contactUs.MainACLowCutRecovery;

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

                        int affectedRows=0;
                        try
                        {
                            affectedRows = db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                        }
                        
                       // int affectedRows = db.SaveChanges();
                        if (affectedRows > 0)
                        {
                            if (Brochurename != null)
                            {
                                Brochurename.SaveAs(Server.MapPath("~/ProductImages/") + filename);
                            }

                            ViewBag.Result = "Record Updated Successfully";
                            return Content("<script>alert('Record Updated Successfully');location.href='../../ProductLevel3/Index'</script>");
                        }

                        //db.SaveChanges();
                        //ViewBag.SaveStatus = "Record Saved Successfully";

                    }
                    else
                    {
                        return View("Edit", contactUs);
                        //return Content("<script>alert('Record Not Updated');location.href='../../ProductLevel3/Index'</script>");
                    }

                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }

            }
            return View("Index");

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
               // DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["deleterole"].ToString() == "1"*/)
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
               // DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["uview"].ToString() == "1"*/)
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
               // DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["editrole"].ToString() == "1"*/)
                {
                    Session["id"] = id;
                    //List<ProductAccessTableForProduct> Pat = db.ProductAccessTableForProducts.Where(a => a.ProductId == id && (a.deleted != true || a.deleted == null || a.deleted == false)).ToList();
                    List<GetProductLevelFourAccessTable_Result> Pat = db.GetProductLevelFourAccessTable(id).ToList();
                    return View(Pat);
                }
                //else
                //{
                //    return RedirectToAction("snotallowed", "snotallowed");
                //}
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
              //  DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["createrole"].ToString() == "1"*/)
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
