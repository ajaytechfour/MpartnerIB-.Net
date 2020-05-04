using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.IO;
using PagedList;
using PagedList.Mvc;
using System.Data;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Text;
using Luminous.Controllers;

namespace LuminousMpartnerIB.Controllers
{
    public class CreatePermotionPrice_NewController : MultiLanguageController
    {
        //
        // GET: /CreatePermotions/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/CreatePermotionPrice_New/Index";
        string utype = string.Empty;
        public ActionResult Index(string Search)
        {
            utype = Session["ctype"].ToString();
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
                if (utype == "Luminous")
                {
                    return RedirectToAction("Index", "Price");
                }
                else
                {
                    return View();
                    //return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public ActionResult PriceList()
        {
            return View();
        }

        public JsonResult GetCardProvider()
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
                if (true/*result[0]["uview"].ToString() == "1"*/)
                {
                    var Company = (from c in db.Card_ProviderMaster
                                   where c.Status != 2 && c.Pagename == "PriceList"
                                   select new
                                   {
                                       id = c.Id,
                                       Name = c.CardProviderName
                                   }).ToList();
                    return Json(Company, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }



        public ActionResult SaveContact(string Alls, string rglist, string disList, string Dealist, string Ctype, HttpPostedFileBase header_image, string Title, string TitleColour,
            string Sub_Title, string Sub_TitleColour, HttpPostedFileBase main_image, string StartDate, string EndDate, string statusC,
            string DistriCheck, string DealCheck, HttpPostedFileBase Pdf_file, HttpPostedFileBase userupload_file, string ParentCatid, string ParentCatname)
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
                if (true/*result[0]["createrole"].ToString() == "1"*/)
                {


                    int parntcat;
                    int card_id;
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;

                    ViewBag.parntcat = ParentCatid;

                    #region Check Validation for permission
                    //if (Alls == null)
                    //{
                    //    ModelState.AddModelError("status", "Permission For Has No Value");
                    //}
                    //else

                    if (userupload_file == null)
                    {
                        //Rajesh
                        //Alls = Alls ?? "off";
                        //if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (rglist == "" || rglist == null || rglist == "0") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                        //{

                        //    ModelState.AddModelError("status", "Permission For Has No Value");

                        //}
                        //else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                        //{
                        //    ModelState.AddModelError("status", "Check Either Distributor OR Dealer");
                        //}
                        //End Rajesh
                    }

                    #endregion








                    #region Validate Parent Category
                    if (!int.TryParse(ParentCatid, out parntcat))
                    {
                        ModelState.AddModelError("Subcatid", "Select Parent Category");

                    }
                    if (parntcat == 0)
                    {
                        ModelState.AddModelError("Subcatid", "Select Parent Category");
                    }
                    #endregion

                    #region Validate card
                    if (!int.TryParse(Ctype, out card_id))
                    {
                        ModelState.AddModelError("CardProviderId", "Select Card Type");

                    }
                    if (parntcat == 0)
                    {
                        ModelState.AddModelError("CardProviderId", "Select Card Type");
                    }
                    if (Title == "")
                    {
                        ModelState.AddModelError("Title", "Title is Required");

                    }
                    if (Sub_Title == "")
                    {
                        ModelState.AddModelError("Sub_Title", "Sub Title is Required");

                    }
                    if (TitleColour == "")
                    {
                        ModelState.AddModelError("TitleColour", "Title Colour is Required");

                    }
                    if (Sub_TitleColour == "")
                    {
                        ModelState.AddModelError("Sub_TitleColour", "Sub Title Colour is Required");

                    }


                    if (header_image == null)
                    {
                        ModelState.AddModelError("File", "*");
                        ViewBag.HFile = "File Is Not Uploaded";
                    }
                    else
                    {
                        string FileExtension = Path.GetExtension(header_image.FileName);
                        if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".png")
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("File", "*");
                            ViewBag.HFile = "File Extention Should Be In .Jpeg,.Png, .jpg";
                        }



                    }

                    if (main_image == null)
                    {
                        ModelState.AddModelError("File", "*");
                        ViewBag.MFile = "File Is Not Uploaded";
                    }
                    else
                    {
                        string FileExtension = Path.GetExtension(main_image.FileName);
                        if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".png")
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("File", "*");
                            ViewBag.MFile = "File Extention Should Be In .Jpeg,.Png, .jpg";
                        }



                    }


                    if (Pdf_file != null)
                    {
                        string FileExtension_pdf = Path.GetExtension(Pdf_file.FileName);
                        if (FileExtension_pdf.ToLower() == ".pdf" || FileExtension_pdf.ToLower() == ".PDF")
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("File", "*");
                            ViewBag.PFile = "File Extention Should Be In .pdf,.PDF";
                        }

                    }




                    #endregion

                    //Check Status Null value
                    string Status = statusC ?? "off";

                    #region validate Start Date
                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "Start Date Is Empty");
                    }


                    #endregion

                    #region Validate End DAte
                    if (EndDate == null || EndDate == "")
                    {
                        ModelState.AddModelError("EndDate", "End Date Is Empty");

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





                    //#region Validate Descriptions
                    //if (Descriptions == "" || Descriptions == null)
                    //{
                    //    ModelState.AddModelError("Descriptions", "Descriptions Is Empty");

                    //}
                    //#endregion

                    if (ModelState.IsValid)
                    {

                        if (main_image != null)
                        {
                            string mainimage = "";
                            string headerimage = "";
                            string pdffile = "";
                            //string filename = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                            //Main Image//
                            var InputFileName = Path.GetFileNameWithoutExtension(main_image.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(main_image.FileName);
                            mainimage = InputFileName.Replace(" ", string.Empty);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/") + mainimage);
                            //Save file to server folder  
                            main_image.SaveAs(ServerSavePath);

                            //End Main Image//

                            //Header Image//
                            var HeaderFileName = Path.GetFileNameWithoutExtension(header_image.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(header_image.FileName);
                            headerimage = HeaderFileName.Replace(" ", string.Empty);
                            var ServerSavePath_Header = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/") + headerimage);
                            //Save file to server folder  
                            header_image.SaveAs(ServerSavePath_Header);

                            //End Header Image//

                            Card_dynamicPage c_dynamicpage = new Card_dynamicPage();
                            c_dynamicpage.CardProviderId = card_id.ToString();




                            var getclassname = db.Card_ProviderMaster.Where(c => c.Id == card_id).Select(c => new { c.Classname, c.CardProviderName }).SingleOrDefault();
                            c_dynamicpage.ClassName = getclassname.Classname;
                            c_dynamicpage.CardProviderName = getclassname.CardProviderName;
                            c_dynamicpage.CardAction_Deeplink = "Price";
                            c_dynamicpage.CardAction_Pagename = "Price";
                            c_dynamicpage.CreatedBy = Session["userid"].ToString();

                            c_dynamicpage.CreatedOn = DateTime.Now;
                            c_dynamicpage.Startdate = Convert.ToDateTime(StartDate);
                            c_dynamicpage.Enddate = Convert.ToDateTime(EndDate);

                            c_dynamicpage.Subcatid = ParentCatid.ToString();
                            var pcatid = Convert.ToInt32(ParentCatid);
                            c_dynamicpage.Subcatname = db.ParentCategories.Where(c => c.Pcid == pcatid).Select(c => c.PCName).SingleOrDefault();
                            c_dynamicpage.Title = Title;
                            c_dynamicpage.TitleColour = TitleColour;
                            c_dynamicpage.Sub_Title = Sub_Title;
                            c_dynamicpage.ColourId = "";
                            c_dynamicpage.ColourName = "";
                            c_dynamicpage.Hexacode = "";
                            c_dynamicpage.Height = "";

                            c_dynamicpage.CardActionId = "";
                            c_dynamicpage.Width = "";
                            c_dynamicpage.Action1_Colour = "";
                            c_dynamicpage.Action1_Text = "";
                            c_dynamicpage.Sub_TitleColour = Sub_TitleColour;
                            c_dynamicpage.ImageOriginalName = header_image.FileName;
                            c_dynamicpage.ImageSystemName = headerimage;
                            c_dynamicpage.OriginalMainImage = main_image.FileName;
                            c_dynamicpage.SystemMainImage = mainimage;
                            c_dynamicpage.CardDataFlag = "0";
                            c_dynamicpage.Pagename = "Price";
                            //PDF Upload Code//

                            if (Pdf_file != null)
                            {
                                //PDF Image//
                                var PdfFileName = Path.GetFileNameWithoutExtension(Pdf_file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(Pdf_file.FileName);
                                pdffile = PdfFileName.Replace(" ", string.Empty);
                                var ServerSavePath_Pdf = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/") + pdffile);
                                //Save file to server folder  
                                Pdf_file.SaveAs(ServerSavePath_Pdf);

                                //End PDF Image//
                                c_dynamicpage.PdfOriginalName = Pdf_file.FileName;
                                c_dynamicpage.PdfSystemName = pdffile;

                            }


                            string status = statusC ?? "off";
                            if (status == "on")
                            {
                                c_dynamicpage.Status = 1;
                            }
                            else
                            {
                                c_dynamicpage.Status = 0;
                            }
                            db.Card_dynamicPage.Add(c_dynamicpage);
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
                                            Price_SchemeAccessTable pat = new Price_SchemeAccessTable();
                                            pat.promotionid = Convert.ToInt32(c_dynamicpage.Id);
                                            pat.RegId = int.Parse(s);
                                            pat.Pagename = "Price";
                                            pat.createdate = DateTime.Now;
                                            pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                            pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                            pat.createby = Session["userid"].ToString();
                                            db.Price_SchemeAccessTable.Add(pat);
                                            db.SaveChanges();

                                        }
                                    }
                                }
                                else
                                {
                                    Price_SchemeAccessTable pat = new Price_SchemeAccessTable();
                                    pat.promotionid = Convert.ToInt32(c_dynamicpage.Id);
                                    pat.AllAcess = true;
                                    pat.Pagename = "Price";
                                    pat.createdate = DateTime.Now; 
                                    pat.createby = Session["userid"].ToString();
                                    db.Price_SchemeAccessTable.Add(pat);
                                    db.SaveChanges();
                                }
                                if ((Alls.ToLower() != "on") && (rglist == "" || rglist == null) && (DistriCheck != "on" || DealCheck != "on"))
                                {
                                    Price_SchemeAccessTable pat = new Price_SchemeAccessTable();
                                    pat.promotionid = Convert.ToInt32(c_dynamicpage.Id);
                                    pat.RegId = null;
                                    pat.createdate = DateTime.Now;
                                    pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                    pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                    pat.Pagename = "Price";
                                    pat.createby = Session["userid"].ToString();
                                    db.Price_SchemeAccessTable.Add(pat);
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
                                            Price_SchemeAccessTable pat = new Price_SchemeAccessTable();
                                            pat.promotionid = Convert.ToInt32(c_dynamicpage.Id);
                                            pat.RegId = null;
                                            pat.createdate = DateTime.Now;
                                            pat.AllDealerAccess = false;
                                            pat.AllDestriAccess = false;
                                            if (usertype.CustomerType == "DISTY")
                                            {
                                                pat.SpecificDealerAccess = "0";
                                                pat.SpecificDestriAccess = usertype.UserId;
                                                pat.Pagename = "Price";
                                            }
                                            if (usertype.CustomerType == "Dealer")
                                            {
                                                pat.SpecificDealerAccess = usertype.UserId;
                                                pat.SpecificDestriAccess = "0";
                                                pat.Pagename = "Price";
                                            }
                                            pat.createby = Session["userid"].ToString();
                                            db.Price_SchemeAccessTable.Add(pat);
                                            db.SaveChanges();
                                        }
                                    }
                                }

                            }
                        }






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
                if (true/*result[0]["uview"].ToString() == "1"*/)
                {
                    int contactdetails = (from c in db.Card_dynamicPage
                                          where c.Status != 2 && c.Pagename == "Price"
                                          select c).Count();
                    int totalrecord;
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
                    //    var contactDetails2 = (from c in db.PermotonsListPagingScheme_Price_New(PageId ?? 1, 15,"Price")
                    //                           select new
                    //                           {
                    //                               id = c.id,
                    //                               ParntCat = c.Subcatname,
                    //                               CardProvider = c.CardProviderName,

                    //                               Title = c.Title,
                    //                               Subtitle = c.Sub_Title,
                    //                               StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                    //                               EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                    //                               status = c.status == 1 ? "Active" : "Deactive",


                    //                           }).Where(c => c.id.ToString().Contains(Session["Search"].ToString()) || c.status.Contains(Session["Search"].ToString()) || c.CardProvider.Contains(Session["Search"].ToString()) || c.StartDate.Contains(Session["Search"].ToString()) || c.EndDate.Contains(Session["Search"].ToString()) || c.ParntCat.Contains(Session["Search"].ToString())).ToList();



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
                    //var contactDetails2 = (from c in db.PermotonsListPagingScheme_Price_New(PageId ?? 1, 15, "Price")
                    //                       select new
                    //                       {
                    //                           id = c.id,
                    //                           ParntCat = c.Subcatname,
                    //                           CardProvider = c.CardProviderName,

                    //                           Title = c.Title,
                    //                           Subtitle = c.Sub_Title,
                    //                           StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                    //                           EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                    //                           status = c.status == 1 ? "Active" : "Deactive",


                    //                       }).ToList();



                    //var temp1= contactDetails2.Where(x=>x.StartDate!=null).

                    //var temp = contactDetails2.ToList();


                    var contactDetails2 = (from c in db.PermotonsListPagingScheme_Price_New("Price")
                                           select new
                                           {
                                               id = c.id,
                                               ParntCat = c.Subcatname,
                                               CardProvider = c.CardProviderName,

                                               Title = c.Title,
                                               Subtitle = c.Sub_Title,
                                               StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                                               EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                                               status = c.status == 1 ? "Active" : "Deactive",
                                           }).OrderByDescending(c => c.id).ToList();

                    var data = new { result = contactDetails2, TotalRecord = contactDetails2.Count };
                    return Json(data, JsonRequestBehavior.AllowGet);
                    // }
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
                if (true/*result[0]["editrole"].ToString() == "1"*/)
                {
                    Card_dynamicPage cud = db.Card_dynamicPage.Single(a => a.Id == id);
                    List<Price_SchemeAccessTable> pat = db.Price_SchemeAccessTable.Where(a => a.promotionid == id).ToList();
                    ViewBag.status = cud.Status;
                    ViewBag.preStartDate = Convert.ToDateTime(cud.Startdate).ToShortDateString();
                    ViewBag.PreEndDate = Convert.ToDateTime(cud.Enddate).ToShortDateString();
                    ViewBag.Providerid = cud.CardProviderId;
                    ViewBag.Prntid = cud.Subcatid;
                    ViewBag.HeaderImageName = cud.ImageSystemName;
                    ViewBag.MainImageName = cud.SystemMainImage;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public ActionResult Update(int id, string Ctype, HttpPostedFileBase header_image, string Title, string TitleColour,
            string Sub_Title, string Sub_TitleColour, HttpPostedFileBase main_image, string StartDate, string EndDate, string statusC,
            string DistriCheck, string DealCheck, HttpPostedFileBase Pdf_file, HttpPostedFileBase userupload_file, string ParentCatid, string ParentCatname)
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
                if (true/*result[0]["editrole"].ToString() == "1"*/)
                {
                    int parntcat;
                    int card_id;
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;

                    ViewBag.parntcat = ParentCatid;

                    #region Validate Parent Category
                    if (!int.TryParse(ParentCatid, out parntcat))
                    {
                        ModelState.AddModelError("Subcatid", "Select Parent Category");

                    }
                    if (parntcat == 0)
                    {
                        ModelState.AddModelError("Subcatid", "Select Parent Category");
                    }
                    #endregion

                    #region Validate card
                    if (!int.TryParse(Ctype, out card_id))
                    {
                        ModelState.AddModelError("CardProviderId", "Select Card Type");

                    }
                    if (parntcat == 0)
                    {
                        ModelState.AddModelError("CardProviderId", "Select Card Type");
                    }
                    if (Title == "")
                    {
                        ModelState.AddModelError("Title", "Title is Required");

                    }
                    if (Sub_Title == "")
                    {
                        ModelState.AddModelError("Sub_Title", "Sub Title is Required");

                    }
                    if (TitleColour == "")
                    {
                        ModelState.AddModelError("TitleColour", "Title Colour is Required");

                    }
                    if (Sub_TitleColour == "")
                    {
                        ModelState.AddModelError("Sub_TitleColour", "Sub Title Colour is Required");

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
                    #region Validate File


                    //string FileExtension = Path.GetExtension(header_image.FileName);
                    //if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".png")
                    //{

                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("File", "*");
                    //    ViewBag.HFile = "File Extention Should Be In .Jpeg,.Png, .jpg";
                    //}






                    //string FileExtensionM = Path.GetExtension(main_image.FileName);
                    //if (FileExtensionM.ToLower() == ".jpeg" || FileExtensionM.ToLower() == ".jpg" || FileExtensionM.ToLower() == ".png")
                    //{

                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("File", "*");
                    //    ViewBag.MFile = "File Extention Should Be In .Jpeg,.Png, .jpg";
                    //}







                    //string FileExtensionP = Path.GetExtension(Pdf_file.FileName);
                    //if (FileExtensionP.ToLower() == ".pdf" || FileExtensionP.ToLower() == ".PDF")
                    //{

                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("File", "*");
                    //    ViewBag.PFile = "File Extention Should Be In .pdf,.PDF";
                    //}






                    #endregion

                    if (ModelState.IsValid)
                    {
                        Card_dynamicPage contactusd = db.Card_dynamicPage.Single(a => a.Id == id);

                        Card_dynamicPage_History plh = new Card_dynamicPage_History();

                        plh.CardProviderId = contactusd.Id.ToString();


                        plh.Parent_Card_Id = Convert.ToInt32(contactusd.Id);


                        plh.ClassName = contactusd.ClassName;
                        plh.CardProviderName = contactusd.CardProviderName;
                        plh.CardAction_Deeplink = "Price";
                        plh.CardAction_Pagename = "Price";
                        plh.CreatedBy = Session["userid"].ToString();

                        plh.CreatedOn = DateTime.Now;
                        plh.Startdate = Convert.ToDateTime(StartDate);
                        plh.Enddate = Convert.ToDateTime(EndDate);

                        plh.Subcatid = contactusd.Subcatid.ToString();

                        plh.Subcatname = contactusd.Subcatname;
                        plh.Title = contactusd.Title;
                        plh.TitleColour = contactusd.TitleColour;
                        plh.Sub_Title = contactusd.Sub_Title;
                        plh.ColourId = "";
                        plh.ColourName = "";
                        plh.Hexacode = "";
                        plh.Height = "";

                        plh.CardActionId = "";
                        plh.Width = "";
                        plh.Action1_Colour = "";
                        plh.Action1_Text = "";
                        plh.Sub_TitleColour = contactusd.Sub_TitleColour;
                        plh.ImageOriginalName = contactusd.ImageOriginalName;
                        plh.ImageSystemName = contactusd.ImageSystemName;
                        plh.OriginalMainImage = contactusd.OriginalMainImage;
                        plh.SystemMainImage = contactusd.SystemMainImage;
                        plh.CardDataFlag = "0";
                        plh.Pagename = "Price";

                        plh.Enddate = contactusd.Enddate;


                        plh.PdfOriginalName = contactusd.PdfOriginalName;

                        plh.PdfSystemName = contactusd.PdfSystemName;
                        plh.ModifiedBy = Session["userid"].ToString();
                        plh.ModifiedOn = DateTime.Now;

                        plh.Startdate = contactusd.Startdate;
                        plh.Status = contactusd.Status;
                        db.Card_dynamicPage_History.Add(plh);

                        string mainimage = "";
                        string headerimage = "";
                        string pdffile = "";
                        //string filename = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                        //Main Image//
                        if (main_image != null)
                        {
                            var InputFileName = Path.GetFileNameWithoutExtension(main_image.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(main_image.FileName);
                            mainimage = InputFileName.Replace(" ", string.Empty);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/") + mainimage);
                            //Save file to server folder  
                            main_image.SaveAs(ServerSavePath);
                            contactusd.SystemMainImage = mainimage;
                            contactusd.OriginalMainImage = main_image.FileName;
                        }


                        //End Main Image//

                        //Header Image//
                        if (header_image != null)
                        {
                            var HeaderFileName = Path.GetFileNameWithoutExtension(header_image.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(header_image.FileName);
                            headerimage = HeaderFileName.Replace(" ", string.Empty);
                            var ServerSavePath_Header = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/") + headerimage);
                            //Save file to server folder  
                            header_image.SaveAs(ServerSavePath_Header);
                            contactusd.ImageSystemName = headerimage;
                            contactusd.ImageOriginalName = header_image.FileName;
                        }

                        //End Header Image//


                        if (Pdf_file != null)
                        {
                            //PDF Image//
                            var PdfFileName = Path.GetFileNameWithoutExtension(Pdf_file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(Pdf_file.FileName);
                            pdffile = PdfFileName.Replace(" ", string.Empty);
                            var ServerSavePath_Pdf = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/") + pdffile);
                            //Save file to server folder  
                            Pdf_file.SaveAs(ServerSavePath_Pdf);

                            //End PDF Image//
                            contactusd.PdfOriginalName = Pdf_file.FileName;
                            contactusd.PdfSystemName = pdffile;

                        }


                        contactusd.CardProviderId = card_id.ToString();




                        var getclassname = db.Card_ProviderMaster.Where(c => c.Id == card_id).Select(c => new { c.Classname, c.CardProviderName }).SingleOrDefault();
                        contactusd.ClassName = getclassname.Classname;
                        contactusd.CardProviderName = getclassname.CardProviderName;
                        contactusd.CardAction_Deeplink = "Price";
                        contactusd.CardAction_Pagename = "Price";
                        contactusd.CreatedBy = Session["userid"].ToString();

                        contactusd.CreatedOn = DateTime.Now;
                        contactusd.Startdate = Convert.ToDateTime(StartDate);
                        contactusd.Enddate = Convert.ToDateTime(EndDate);

                        contactusd.Subcatid = ParentCatid.ToString();
                        var pcatid = Convert.ToInt32(ParentCatid);
                        contactusd.Subcatname = db.ParentCategories.Where(c => c.Pcid == pcatid).Select(c => c.PCName).SingleOrDefault();
                        contactusd.Title = Title;
                        contactusd.TitleColour = TitleColour;
                        contactusd.Sub_Title = Sub_Title;
                        contactusd.ColourId = "";
                        contactusd.ColourName = "";
                        contactusd.Hexacode = "";
                        contactusd.Height = "";

                        contactusd.CardActionId = "";
                        contactusd.Width = "";
                        contactusd.Action1_Colour = "";
                        contactusd.Action1_Text = "";
                        contactusd.Sub_TitleColour = Sub_TitleColour;

                        contactusd.CardDataFlag = "0";
                        contactusd.Pagename = "Price";
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.Status = 1;
                        }
                        else
                        {
                            contactusd.Status = 0;
                        }
                        db.SaveChanges();
                        ViewBag.SaveStatus = "Record Saved Successfully";
                        return View("Index");
                    }
                    return View("Edit", db.Card_dynamicPage.Single(a => a.Id == id));
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
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    Card_dynamicPage contactusd = db.Card_dynamicPage.Single(a => a.Id == id);

                    Card_dynamicPage_History plh = new Card_dynamicPage_History();

                    plh.CardProviderId = contactusd.Id.ToString();


                    plh.Parent_Card_Id = Convert.ToInt32(contactusd.Id);


                    plh.ClassName = contactusd.ClassName;
                    plh.CardProviderName = contactusd.CardProviderName;
                    plh.CardAction_Deeplink = "Price";
                    plh.CardAction_Pagename = "Price";
                    plh.CreatedBy = Session["userid"].ToString();

                    plh.CreatedOn = DateTime.Now;
                    plh.Startdate = contactusd.Startdate;
                    plh.Enddate = contactusd.Enddate;

                    plh.Subcatid = contactusd.Subcatid.ToString();

                    plh.Subcatname = contactusd.Subcatname;
                    plh.Title = contactusd.Title;
                    plh.TitleColour = contactusd.TitleColour;
                    plh.Sub_Title = contactusd.Sub_Title;
                    plh.ColourId = "";
                    plh.ColourName = "";
                    plh.Hexacode = "";
                    plh.Height = "";

                    plh.CardActionId = "";
                    plh.Width = "";
                    plh.Action1_Colour = "";
                    plh.Action1_Text = "";
                    plh.Sub_TitleColour = contactusd.Sub_TitleColour;
                    plh.ImageOriginalName = contactusd.ImageOriginalName;
                    plh.ImageSystemName = contactusd.ImageSystemName;
                    plh.OriginalMainImage = contactusd.OriginalMainImage;
                    plh.SystemMainImage = contactusd.SystemMainImage;
                    plh.CardDataFlag = "0";
                    plh.Pagename = "Price";

                    plh.Enddate = contactusd.Enddate;


                    plh.PdfOriginalName = contactusd.PdfOriginalName;

                    plh.PdfSystemName = contactusd.PdfSystemName;
                    plh.ModifiedBy = Session["userid"].ToString();
                    plh.ModifiedOn = DateTime.Now;

                    plh.Startdate = contactusd.Startdate;
                    plh.Status = contactusd.Status;
                    db.Card_dynamicPage_History.Add(plh);


                    contactusd.Status = 2;
                    contactusd.ModifiedBy = Session["userid"].ToString();
                    contactusd.ModifiedOn = DateTime.Now;
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