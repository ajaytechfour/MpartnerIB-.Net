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
using System.Data;
using System.Dynamic;

namespace LuminousMpartnerIB.Controllers
{
    public class HomeCardController : Controller
    {
        //
        // GET: /CreatePermotions/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/HomeCard/Index";
        string utype = string.Empty;

        public ActionResult Index(string Search)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                utype = Session["ctype"].ToString();
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

                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (utype == "Luminous")
                {
                    return RedirectToAction("Index", "HomeCardView");
                }
                else
                {
                    return View();
                }
                //else
                //{
                //    return RedirectToAction("snotallowed", "snotallowed");
                //}
            }
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
                if (true)
                {
                    var Company = (from c in db.Card_ProviderMaster
                                   where c.Status != 2 && c.Pagename == "HomePage" && c.Id != 3 && c.Id != 12
                                   select new
                                   {
                                       id = c.Id,
                                       Name = c.ProviderName
                                   }).ToList();
                    return Json(Company, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

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
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    var Company = (from c in db.Card_ActionMaster
                                   where c.Status != 2
                                   select new
                                   {
                                       id = c.Id,
                                       Name = c.Pagename
                                   }).ToList();
                    return Json(Company, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }



        public ActionResult SaveContact(string Ctype, string[] redirectpage1, HttpPostedFileBase headerimage, HttpPostedFileBase mainimage_Schemecard, string Title, string TitleColour,
            string Sub_Title, string Sub_TitleColour, HttpPostedFileBase[] mainimage, HttpPostedFileBase gridbackimage, string[] gridtitle, HttpPostedFileBase[] upload_gridchildmainimage, HttpPostedFileBase[] bannermainimage, string StartDate, string EndDate, string Action1_Colour, string Action1_Text, string statusC)
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


                    int parntcat;
                    int card_id;
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;

                    #region Validate card
                    if (!int.TryParse(Ctype, out card_id))
                    {
                        ModelState.AddModelError("CardProviderId", "Select Card Type");

                    }
                    if (Ctype == "0")
                    {
                        ModelState.AddModelError("CardProviderId", "Select Card Type");
                    }
                    #endregion

                    //Check Status Null value
                    string Status = statusC ?? "off";

                    //#region validate Start Date
                    //if (StartDate == null || StartDate == "")
                    //{
                    //    ModelState.AddModelError("StartDate", "Start Date Is Empty");
                    //}


                    //#endregion

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

                    //#region Validate File
                    //if (headerimage == null)
                    //{

                    //    ModelState.AddModelError("ImageName", "Image File Is Not Uploaded");
                    //    ViewBag.File = "File Is Not Uploaded";
                    //}
                    //#endregion


                    //#region main Image File
                    //if (main_image == null)
                    //{

                    //    ModelState.AddModelError("", "File Is Not Uploaded");
                    //    ViewBag.PDFFile = "PDF File Is Not Uploaded";
                    //}
                    //#endregion

                    if (ModelState.IsValid)
                    {
                        string numToRemove = "0";
                        redirectpage1 = redirectpage1.Where(w => w != numToRemove).ToArray();
                        if (Ctype == "1")
                        {
                            foreach (HttpPostedFileBase fileimage in mainimage)
                            {

                                if (fileimage != null)
                                {
                                    string main_image = "";

                                    //string filename = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                                    //Main Image//

                                    var InputFileName = Path.GetFileNameWithoutExtension(fileimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fileimage.FileName);
                                    main_image = InputFileName.Replace(" ", string.Empty);
                                    var ServerSavePath = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image);
                                    //Save file to server folder  
                                    fileimage.SaveAs(ServerSavePath);

                                    //End Main Image//

                                    Card_dynamicPage c_dynamicpage = new Card_dynamicPage();
                                    c_dynamicpage.CardProviderId = card_id.ToString();


                                    var getclassname = db.Card_ProviderMaster.Where(c => c.Id == card_id).Select(c => new { c.Classname, c.CardProviderName }).SingleOrDefault();
                                    c_dynamicpage.ClassName = getclassname.Classname;
                                    c_dynamicpage.CardProviderName = getclassname.CardProviderName;


                                    int redirectid = Convert.ToInt32(redirectpage1[0]);


                                    var c_deeplink = db.Card_ActionMaster.Where(c => c.Id == redirectid).Select(c => new { c.Deeplink, c.Pagename }).SingleOrDefault();

                                    c_dynamicpage.CreatedBy = Session["userid"].ToString();
                                    c_dynamicpage.CardAction_Deeplink = c_deeplink.Deeplink;
                                    c_dynamicpage.CardAction_Pagename = c_deeplink.Pagename;
                                    c_dynamicpage.CreatedOn = DateTime.Now;
                                    c_dynamicpage.Startdate = Convert.ToDateTime(StartDate);
                                    c_dynamicpage.Enddate = Convert.ToDateTime(EndDate);
                                    c_dynamicpage.ColourId = "";
                                    c_dynamicpage.ColourName = "";
                                    c_dynamicpage.Hexacode = "";
                                    c_dynamicpage.Height = "";
                                    c_dynamicpage.Width = "";
                                    c_dynamicpage.Title = "";
                                    c_dynamicpage.TitleColour = "";

                                    c_dynamicpage.Sub_Title = "";
                                    c_dynamicpage.Sub_TitleColour = "";
                                    c_dynamicpage.ImageOriginalName = fileimage.FileName;
                                    c_dynamicpage.ImageSystemName = main_image;
                                    c_dynamicpage.OriginalMainImage = "";
                                    c_dynamicpage.SystemMainImage = "";
                                    c_dynamicpage.CardDataFlag = "0";
                                    c_dynamicpage.Pagename = "";
                                    c_dynamicpage.Action1_Colour = "";
                                    c_dynamicpage.Action1_Text = "";
                                    c_dynamicpage.Pagename = "HomePage";

                                    c_dynamicpage.PdfOriginalName = "";
                                    c_dynamicpage.PdfSystemName = "";
                                    c_dynamicpage.Subcatid = "";
                                    c_dynamicpage.Subcatname = "";
                                    c_dynamicpage.OriginalMainImage = "";
                                    c_dynamicpage.SystemMainImage = "";


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
                                    //string str = Path.Combine(Server.MapPath("~/PermotionsImage/"), filename);
                                    //file.SaveAs(str);
                                }

                            }
                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                        if (Ctype == "2")
                        {


                            if (headerimage != null)
                            {
                                string main_image = "";
                                string main_image1 = "";
                                //string filename = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                                //Main Image//

                                var InputFileName = Path.GetFileNameWithoutExtension(headerimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(headerimage.FileName);
                                main_image = InputFileName.Replace(" ", string.Empty);
                                var ServerSavePath = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image);
                                //Save file to server folder  
                                headerimage.SaveAs(ServerSavePath);

                                var InputFileName1 = Path.GetFileNameWithoutExtension(mainimage[1].FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(mainimage[1].FileName);
                                main_image1 = InputFileName1.Replace(" ", string.Empty);
                                var ServerSavePath1 = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image1);
                                //Save file to server folder  
                                mainimage[1].SaveAs(ServerSavePath1);

                                //End Main Image//



                                Card_dynamicPage c_dynamicpage_header = new Card_dynamicPage();
                                c_dynamicpage_header.CardProviderId = card_id.ToString();




                                var getclassname_header = db.Card_ProviderMaster.Where(c => c.Id == card_id).Select(c => new { c.Classname, c.CardProviderName }).SingleOrDefault();
                                c_dynamicpage_header.ClassName = getclassname_header.Classname;
                                c_dynamicpage_header.CardProviderName = getclassname_header.CardProviderName;

                                int redirectid = Convert.ToInt32(redirectpage1[0]);
                                var c_deeplink = db.Card_ActionMaster.Where(c => c.Id == redirectid).Select(c => new { c.Deeplink, c.Pagename }).SingleOrDefault();

                                c_dynamicpage_header.CreatedBy = Session["userid"].ToString();
                                c_dynamicpage_header.CardAction_Deeplink = "";
                                c_dynamicpage_header.CardAction_Pagename = "";
                                c_dynamicpage_header.CreatedOn = DateTime.Now;
                                c_dynamicpage_header.Startdate = Convert.ToDateTime(StartDate);
                                c_dynamicpage_header.Enddate = Convert.ToDateTime(EndDate);
                                c_dynamicpage_header.ColourId = "";
                                c_dynamicpage_header.ColourName = "";
                                c_dynamicpage_header.Hexacode = "";
                                c_dynamicpage_header.Height = "";
                                c_dynamicpage_header.Width = "";
                                c_dynamicpage_header.Title = Title;
                                c_dynamicpage_header.TitleColour = TitleColour;

                                c_dynamicpage_header.Sub_Title = Sub_Title;
                                c_dynamicpage_header.Sub_TitleColour = Sub_TitleColour;
                                c_dynamicpage_header.ImageOriginalName = headerimage.FileName;
                                c_dynamicpage_header.ImageSystemName = main_image;
                                //c_dynamicpage_header.OriginalMainImage = 
                                // c_dynamicpage_header.SystemMainImage = 
                                c_dynamicpage_header.OriginalMainImage = "";
                                c_dynamicpage_header.SystemMainImage = "";
                                c_dynamicpage_header.CardDataFlag = "0";
                                c_dynamicpage_header.Pagename = "";
                                c_dynamicpage_header.Action1_Colour = "";
                                c_dynamicpage_header.Action1_Text = "";
                                c_dynamicpage_header.Pagename = "HomePage";
                                c_dynamicpage_header.CardAction_Deeplink = c_deeplink.Deeplink;
                                c_dynamicpage_header.CardAction_Pagename = c_deeplink.Pagename;

                                c_dynamicpage_header.PdfOriginalName = "";
                                c_dynamicpage_header.PdfSystemName = "";
                                c_dynamicpage_header.Subcatid = "";
                                c_dynamicpage_header.Subcatname = "";
                                c_dynamicpage_header.OriginalMainImage = "";
                                c_dynamicpage_header.SystemMainImage = ""; ;

                                string status = statusC ?? "off";
                                if (status == "on")
                                {
                                    c_dynamicpage_header.Status = 1;
                                }
                                else
                                {
                                    c_dynamicpage_header.Status = 0;
                                }
                                db.Card_dynamicPage.Add(c_dynamicpage_header);
                                db.SaveChanges();

                                //string str = Path.Combine(Server.MapPath("~/PermotionsImage/"), filename);
                                //file.SaveAs(str);


                            }
                            foreach (HttpPostedFileBase fileimage in mainimage)
                            {
                                if (fileimage != null)
                                {
                                    string main_image = "";
                                    string main_image1 = "";
                                    //string filename = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                                    //Main Image//



                                    var InputFileName1 = Path.GetFileNameWithoutExtension(mainimage[1].FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(mainimage[1].FileName);
                                    main_image1 = InputFileName1.Replace(" ", string.Empty);
                                    var ServerSavePath1 = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image1);
                                    //Save file to server folder  
                                    mainimage[1].SaveAs(ServerSavePath1);


                                    var InputFileName_main = Path.GetFileNameWithoutExtension(fileimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fileimage.FileName);
                                    main_image = InputFileName_main.Replace(" ", string.Empty);
                                    var ServerSavePath_main = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image);
                                    //Save file to server folder  
                                    fileimage.SaveAs(ServerSavePath_main);
                                    Card_dynamicPage c_dynamicpage_main = new Card_dynamicPage();
                                    c_dynamicpage_main.CardProviderId = "3";




                                    var getclassname_main = db.Card_ProviderMaster.Where(c => c.Id == 3).Select(c => new { c.Classname, c.CardProviderName }).SingleOrDefault();
                                    c_dynamicpage_main.ClassName = getclassname_main.Classname;
                                    c_dynamicpage_main.CardProviderName = getclassname_main.CardProviderName;


                                    int redirectid = Convert.ToInt32(redirectpage1[0]);

                                    var c_deeplink = db.Card_ActionMaster.Where(c => c.Id == redirectid).Select(c => new { c.Deeplink, c.Pagename }).SingleOrDefault();

                                    c_dynamicpage_main.CreatedBy = Session["userid"].ToString();
                                    c_dynamicpage_main.CardAction_Deeplink = c_deeplink.Deeplink;
                                    c_dynamicpage_main.CardAction_Pagename = c_deeplink.Pagename;
                                    c_dynamicpage_main.CreatedOn = DateTime.Now;
                                    c_dynamicpage_main.Startdate = Convert.ToDateTime(StartDate);
                                    c_dynamicpage_main.Enddate = Convert.ToDateTime(EndDate);
                                    c_dynamicpage_main.ColourId = "";
                                    c_dynamicpage_main.ColourName = "";
                                    c_dynamicpage_main.Hexacode = "";
                                    c_dynamicpage_main.Height = "";
                                    c_dynamicpage_main.Width = "";
                                    c_dynamicpage_main.Title = "";
                                    c_dynamicpage_main.TitleColour = "";

                                    c_dynamicpage_main.Sub_Title = "";
                                    c_dynamicpage_main.Sub_TitleColour = "";
                                    c_dynamicpage_main.ImageOriginalName = mainimage[1].FileName; 
                                    c_dynamicpage_main.ImageSystemName = main_image1;
                                    c_dynamicpage_main.OriginalMainImage = "";
                                    c_dynamicpage_main.SystemMainImage = "";
                                    c_dynamicpage_main.CardDataFlag = "0";
                                    c_dynamicpage_main.Pagename = "";
                                    c_dynamicpage_main.Action1_Colour = "";
                                    c_dynamicpage_main.Action1_Text = "";
                                    c_dynamicpage_main.Pagename = "HomePage";

                                    c_dynamicpage_main.PdfOriginalName = "";
                                    c_dynamicpage_main.PdfSystemName = "";
                                    c_dynamicpage_main.Subcatid = "";
                                    c_dynamicpage_main.Subcatname = "";
                                    c_dynamicpage_main.OriginalMainImage = "";
                                    c_dynamicpage_main.SystemMainImage = "";

                                    string status_main = statusC ?? "off";
                                    if (status_main == "on")
                                    {
                                        c_dynamicpage_main.Status = 1;
                                    }
                                    else
                                    {
                                        c_dynamicpage_main.Status = 0;
                                    }
                                    db.Card_dynamicPage.Add(c_dynamicpage_main);
                                    db.SaveChanges();

                                }

                            }

                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                        if (Ctype == "11")
                        {


                            if (headerimage != null)
                            {
                                string main_image = "";

                                //string filename = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                                //Main Image//

                                var InputFileName = Path.GetFileNameWithoutExtension(headerimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(headerimage.FileName);
                                main_image = InputFileName.Replace(" ", string.Empty);
                                var ServerSavePath = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image);
                                //Save file to server folder  
                                headerimage.SaveAs(ServerSavePath);

                                //End Main Image//

                                Card_dynamicPage c_dynamicpage_header = new Card_dynamicPage();
                                c_dynamicpage_header.CardProviderId = card_id.ToString();


                                var getclassname_header = db.Card_ProviderMaster.Where(c => c.Id == card_id).Select(c => new { c.Classname, c.CardProviderName }).SingleOrDefault();
                                c_dynamicpage_header.ClassName = getclassname_header.Classname;
                                c_dynamicpage_header.CardProviderName = getclassname_header.CardProviderName;

                                //int redirectid = Convert.ToInt32(redirectpage1);
                                //var c_deeplink = db.Card_ActionMaster.Where(c => c.Id == redirectid).Select(c => new { c.Deeplink, c.Pagename }).SingleOrDefault();

                                c_dynamicpage_header.CreatedBy = Session["userid"].ToString();
                                c_dynamicpage_header.CardAction_Deeplink = "";
                                c_dynamicpage_header.CardAction_Pagename = "";
                                c_dynamicpage_header.CreatedOn = DateTime.Now;
                                c_dynamicpage_header.Startdate = Convert.ToDateTime(StartDate);
                                c_dynamicpage_header.Enddate = Convert.ToDateTime(EndDate);
                                c_dynamicpage_header.ColourId = "";
                                c_dynamicpage_header.ColourName = "";
                                c_dynamicpage_header.Hexacode = "";
                                c_dynamicpage_header.Height = "";
                                c_dynamicpage_header.Width = "";
                                c_dynamicpage_header.Title = Title;
                                c_dynamicpage_header.TitleColour = TitleColour;

                                c_dynamicpage_header.Sub_Title = Sub_Title;
                                c_dynamicpage_header.Sub_TitleColour = Sub_TitleColour;
                                c_dynamicpage_header.ImageOriginalName = headerimage.FileName;
                                c_dynamicpage_header.ImageSystemName = main_image;
                                c_dynamicpage_header.OriginalMainImage = "";
                                c_dynamicpage_header.SystemMainImage = "";
                                c_dynamicpage_header.CardDataFlag = "0";
                                c_dynamicpage_header.Pagename = "";
                                c_dynamicpage_header.Action1_Colour = "";
                                c_dynamicpage_header.Action1_Text = "";
                                c_dynamicpage_header.Pagename = "HomePage";

                                c_dynamicpage_header.PdfOriginalName = "";
                                c_dynamicpage_header.PdfSystemName = "";
                                c_dynamicpage_header.Subcatid = "";
                                c_dynamicpage_header.Subcatname = "";
                                c_dynamicpage_header.OriginalMainImage = "";
                                c_dynamicpage_header.SystemMainImage = "";

                                string status = statusC ?? "off";
                                if (status == "on")
                                {
                                    c_dynamicpage_header.Status = 1;
                                }
                                else
                                {
                                    c_dynamicpage_header.Status = 0;
                                }
                                db.Card_dynamicPage.Add(c_dynamicpage_header);
                                db.SaveChanges();
                                //string str = Path.Combine(Server.MapPath("~/PermotionsImage/"), filename);
                                //file.SaveAs(str);


                            }
                            foreach (HttpPostedFileBase fileimage in mainimage)
                            {
                                if (fileimage != null)
                                {
                                    string main_image = "";
                                    var InputFileName_main = Path.GetFileNameWithoutExtension(fileimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fileimage.FileName);
                                    main_image = InputFileName_main.Replace(" ", string.Empty);
                                    var ServerSavePath_main = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image);
                                    //Save file to server folder  
                                    fileimage.SaveAs(ServerSavePath_main);
                                    Card_dynamicPage c_dynamicpage_main = new Card_dynamicPage();
                                    c_dynamicpage_main.CardProviderId = "3";

                                    var getclassname_main = db.Card_ProviderMaster.Where(c => c.Id == 3).Select(c => new { c.Classname, c.CardProviderName }).SingleOrDefault();
                                    c_dynamicpage_main.ClassName = getclassname_main.Classname;
                                    c_dynamicpage_main.CardProviderName = getclassname_main.CardProviderName;


                                    int redirectid = Convert.ToInt32(redirectpage1[0]);

                                    var c_deeplink = db.Card_ActionMaster.Where(c => c.Id == redirectid).Select(c => new { c.Deeplink, c.Pagename }).SingleOrDefault();

                                    c_dynamicpage_main.CreatedBy = Session["userid"].ToString();
                                    c_dynamicpage_main.CardAction_Deeplink = c_deeplink.Deeplink;
                                    c_dynamicpage_main.CardAction_Pagename = c_deeplink.Pagename;
                                    c_dynamicpage_main.CreatedOn = DateTime.Now;
                                    c_dynamicpage_main.Startdate = Convert.ToDateTime(StartDate);
                                    c_dynamicpage_main.Enddate = Convert.ToDateTime(EndDate);
                                    c_dynamicpage_main.ColourId = "";
                                    c_dynamicpage_main.ColourName = "";
                                    c_dynamicpage_main.Hexacode = "";
                                    c_dynamicpage_main.Height = "";
                                    c_dynamicpage_main.Width = "";
                                    c_dynamicpage_main.Title = "";
                                    c_dynamicpage_main.TitleColour = "";

                                    c_dynamicpage_main.Sub_Title = "";
                                    c_dynamicpage_main.Sub_TitleColour = "";
                                    c_dynamicpage_main.ImageOriginalName = fileimage.FileName;
                                    c_dynamicpage_main.ImageSystemName = main_image;
                                    c_dynamicpage_main.OriginalMainImage = "";
                                    c_dynamicpage_main.SystemMainImage = "";
                                    c_dynamicpage_main.CardDataFlag = "0";
                                    c_dynamicpage_main.Pagename = "";
                                    c_dynamicpage_main.Action1_Colour = "";
                                    c_dynamicpage_main.Action1_Text = "";
                                    c_dynamicpage_main.Pagename = "HomePage";

                                    c_dynamicpage_main.PdfOriginalName = "";
                                    c_dynamicpage_main.PdfSystemName = "";
                                    c_dynamicpage_main.Subcatid = "";
                                    c_dynamicpage_main.Subcatname = "";
                                    c_dynamicpage_main.OriginalMainImage = "";
                                    c_dynamicpage_main.SystemMainImage = "";

                                    string status_main = statusC ?? "off";
                                    if (status_main == "on")
                                    {
                                        c_dynamicpage_main.Status = 1;
                                    }
                                    else
                                    {
                                        c_dynamicpage_main.Status = 0;
                                    }
                                    db.Card_dynamicPage.Add(c_dynamicpage_main);
                                    db.SaveChanges();

                                }


                            }

                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                        if (Ctype == "6")
                        {

                            long parentId = 0;



                            foreach (HttpPostedFileBase fileimage in mainimage)
                            {
                                if (fileimage != null)
                                {
                                    string main_image = "";
                                    var InputFileName_main = Path.GetFileNameWithoutExtension(fileimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fileimage.FileName);
                                    main_image = InputFileName_main.Replace(" ", string.Empty);
                                    var ServerSavePath_main = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image);
                                    //Save file to server folder  
                                    fileimage.SaveAs(ServerSavePath_main);
                                    Card_dynamicPage c_dynamicpage_main = new Card_dynamicPage();
                                    c_dynamicpage_main.CardProviderId = card_id.ToString();

                                    var getclassname_main = db.Card_ProviderMaster.Where(c => c.Id == card_id).Select(c => new { c.Classname, c.CardProviderName }).SingleOrDefault();
                                    c_dynamicpage_main.ClassName = getclassname_main.Classname;
                                    c_dynamicpage_main.CardProviderName = getclassname_main.CardProviderName;

                                    c_dynamicpage_main.CreatedBy = Session["userid"].ToString();
                                    c_dynamicpage_main.CardAction_Deeplink = "";
                                    c_dynamicpage_main.CardAction_Pagename = "";
                                    c_dynamicpage_main.CreatedOn = DateTime.Now;
                                    c_dynamicpage_main.Startdate = Convert.ToDateTime(StartDate);
                                    c_dynamicpage_main.Enddate = Convert.ToDateTime(EndDate);
                                    c_dynamicpage_main.ColourId = "";
                                    c_dynamicpage_main.ColourName = "";
                                    c_dynamicpage_main.Hexacode = "";
                                    c_dynamicpage_main.Height = "";
                                    c_dynamicpage_main.Width = "";
                                    c_dynamicpage_main.Title = "";
                                    c_dynamicpage_main.TitleColour = "";

                                    c_dynamicpage_main.Sub_Title = "";
                                    c_dynamicpage_main.Sub_TitleColour = "";
                                    c_dynamicpage_main.ImageOriginalName = fileimage.FileName;
                                    c_dynamicpage_main.ImageSystemName = main_image;
                                    c_dynamicpage_main.OriginalMainImage = "";
                                    c_dynamicpage_main.SystemMainImage = "";
                                    c_dynamicpage_main.CardDataFlag = "1";
                                    c_dynamicpage_main.Pagename = "";
                                    c_dynamicpage_main.Action1_Colour = "";
                                    c_dynamicpage_main.Action1_Text = "";
                                    c_dynamicpage_main.Pagename = "HomePage";

                                    c_dynamicpage_main.PdfOriginalName = "";
                                    c_dynamicpage_main.PdfSystemName = "";
                                    c_dynamicpage_main.Subcatid = "";
                                    c_dynamicpage_main.Subcatname = "";
                                    c_dynamicpage_main.OriginalMainImage = "";
                                    c_dynamicpage_main.SystemMainImage = "";

                                    string status_main = statusC ?? "off";
                                    if (status_main == "on")
                                    {
                                        c_dynamicpage_main.Status = 1;
                                    }
                                    else
                                    {
                                        c_dynamicpage_main.Status = 0;
                                    }
                                    db.Card_dynamicPage.Add(c_dynamicpage_main);
                                    db.SaveChanges();
                                    parentId = Convert.ToInt32(c_dynamicpage_main.Id);
                                }

                            }
                            string grid_backgroundimage = "";
                            for (var i = 0; i < gridtitle.Length; i++)
                            {
                                string main_image = "";

                                //Grid Four Image//
                                var InputFileName_main = Path.GetFileNameWithoutExtension(upload_gridchildmainimage[i].FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(upload_gridchildmainimage[i].FileName);
                                main_image = InputFileName_main.Replace(" ", string.Empty);
                                var ServerSavePath_main = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image);
                                //Save file to server folder  
                                upload_gridchildmainimage[i].SaveAs(ServerSavePath_main);
                                //Grid Four Image//

                                if (i == 0)
                                {
                                    //Grid Four backgound Image//
                                    var InputFileName_main_bg = Path.GetFileNameWithoutExtension(gridbackimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(gridbackimage.FileName);
                                    grid_backgroundimage = InputFileName_main_bg.Replace(" ", string.Empty);
                                    var ServerSavePath_grid_backgoungimage = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + grid_backgroundimage);
                                    //Save file to server folder  
                                    gridbackimage.SaveAs(ServerSavePath_grid_backgoungimage);
                                    //Grid Four backgound Image//

                                }

                                Card_CardData c_data = new Card_CardData();
                                c_data.DynamicHomePageId = parentId;
                                c_data.CardProviderId = card_id;
                                c_data.Title = gridtitle[i];
                                c_data.Main_image = main_image;

                                var redirectvalue = Convert.ToInt32(redirectpage1[i]);
                                var c_deeplink = db.Card_ActionMaster.Where(c => c.Id == redirectvalue).Select(c => new { c.Deeplink }).SingleOrDefault();
                                c_data.DeepLink = c_deeplink.Deeplink;
                                c_data.Background_image = grid_backgroundimage;
                                c_data.Image_height = "";
                                c_data.Image_width = "";
                                c_data.CreatedOn = DateTime.Now;
                                c_data.CreatedBy = Session["userid"].ToString();
                                c_data.Status = 1;
                                db.Card_CardData.Add(c_data);
                                db.SaveChanges();
                            }

                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                        if (Ctype == "7")
                        {

                            foreach (HttpPostedFileBase fileimage in mainimage)
                            {

                                if (fileimage != null)
                                {
                                    string main_image = "";

                                    //string filename = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                                    //Main Image//

                                    var InputFileName = Path.GetFileNameWithoutExtension(fileimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fileimage.FileName);
                                    main_image = InputFileName.Replace(" ", string.Empty);
                                    var ServerSavePath = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image);
                                    //Save file to server folder  
                                    fileimage.SaveAs(ServerSavePath);

                                    //End Main Image//



                                    Card_dynamicPage c_dynamicpage = new Card_dynamicPage();
                                    c_dynamicpage.CardProviderId = card_id.ToString();


                                    var getclassname = db.Card_ProviderMaster.Where(c => c.Id == card_id).Select(c => new { c.Classname, c.CardProviderName }).SingleOrDefault();
                                    c_dynamicpage.ClassName = getclassname.Classname;
                                    c_dynamicpage.CardProviderName = getclassname.CardProviderName;

                                    int redirectid = Convert.ToInt32(redirectpage1[0]);


                                    var c_deeplink = db.Card_ActionMaster.Where(c => c.Id == redirectid).Select(c => new { c.Deeplink, c.Pagename }).SingleOrDefault();

                                    c_dynamicpage.CreatedBy = Session["userid"].ToString();
                                    c_dynamicpage.CardAction_Deeplink = c_deeplink.Deeplink;
                                    c_dynamicpage.CardAction_Pagename = c_deeplink.Pagename;
                                    c_dynamicpage.CreatedOn = DateTime.Now;
                                    c_dynamicpage.Startdate = Convert.ToDateTime(StartDate);
                                    c_dynamicpage.Enddate = Convert.ToDateTime(EndDate);
                                    c_dynamicpage.ColourId = "";
                                    c_dynamicpage.ColourName = "";
                                    c_dynamicpage.Hexacode = "";
                                    c_dynamicpage.Height = "";
                                    c_dynamicpage.Width = "";
                                    c_dynamicpage.Title = "";
                                    c_dynamicpage.TitleColour = "";

                                    c_dynamicpage.Sub_Title = "";
                                    c_dynamicpage.Sub_TitleColour = "";
                                    c_dynamicpage.ImageOriginalName = fileimage.FileName;
                                    c_dynamicpage.ImageSystemName = main_image;
                                    c_dynamicpage.OriginalMainImage = "";
                                    c_dynamicpage.SystemMainImage = "";
                                    c_dynamicpage.CardDataFlag = "0";
                                    c_dynamicpage.Pagename = "";
                                    c_dynamicpage.Action1_Colour = Action1_Colour;
                                    c_dynamicpage.Action1_Text = Action1_Text;
                                    c_dynamicpage.Pagename = "HomePage";

                                    c_dynamicpage.PdfOriginalName = "";
                                    c_dynamicpage.PdfSystemName = "";
                                    c_dynamicpage.Subcatid = "";
                                    c_dynamicpage.Subcatname = "";
                                    c_dynamicpage.OriginalMainImage = "";
                                    c_dynamicpage.SystemMainImage = "";

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

                                    //string str = Path.Combine(Server.MapPath("~/PermotionsImage/"), filename);
                                    //file.SaveAs(str);


                                }

                            }
                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }
                        if (Ctype == "8")
                        {

                            long parentId = 0;


                            Card_dynamicPage c_dynamicpage_main = new Card_dynamicPage();
                            c_dynamicpage_main.CardProviderId = card_id.ToString();

                            var getclassname_main = db.Card_ProviderMaster.Where(c => c.Id == card_id).Select(c => new { c.Classname, c.CardProviderName }).SingleOrDefault();
                            c_dynamicpage_main.ClassName = getclassname_main.Classname;
                            c_dynamicpage_main.CardProviderName = getclassname_main.CardProviderName;

                            c_dynamicpage_main.CreatedBy = Session["userid"].ToString();
                            c_dynamicpage_main.CardAction_Deeplink = "";
                            c_dynamicpage_main.CardAction_Pagename = "";
                            c_dynamicpage_main.CreatedOn = DateTime.Now;
                            c_dynamicpage_main.Startdate = Convert.ToDateTime(StartDate);
                            c_dynamicpage_main.Enddate = Convert.ToDateTime(EndDate);
                            c_dynamicpage_main.ColourId = "";
                            c_dynamicpage_main.ColourName = "";
                            c_dynamicpage_main.Hexacode = "";
                            c_dynamicpage_main.Height = "";
                            c_dynamicpage_main.Width = "";
                            c_dynamicpage_main.Title = "";
                            c_dynamicpage_main.TitleColour = "";

                            c_dynamicpage_main.Sub_Title = "";
                            c_dynamicpage_main.Sub_TitleColour = "";
                            c_dynamicpage_main.ImageOriginalName = "";
                            c_dynamicpage_main.ImageSystemName = "";
                            c_dynamicpage_main.OriginalMainImage = "";
                            c_dynamicpage_main.SystemMainImage = "";
                            c_dynamicpage_main.CardDataFlag = "2";
                            c_dynamicpage_main.Pagename = "";
                            c_dynamicpage_main.Action1_Colour = "";
                            c_dynamicpage_main.Action1_Text = "";
                            c_dynamicpage_main.Pagename = "HomePage";

                            c_dynamicpage_main.PdfOriginalName = "";
                            c_dynamicpage_main.PdfSystemName = "";
                            c_dynamicpage_main.Subcatid = "";
                            c_dynamicpage_main.Subcatname = "";
                            c_dynamicpage_main.OriginalMainImage = "";
                            c_dynamicpage_main.SystemMainImage = "";

                            string status_main = statusC ?? "off";
                            if (status_main == "on")
                            {
                                c_dynamicpage_main.Status = 1;
                            }
                            else
                            {
                                c_dynamicpage_main.Status = 0;
                            }
                            db.Card_dynamicPage.Add(c_dynamicpage_main);
                            db.SaveChanges();
                            parentId = Convert.ToInt32(c_dynamicpage_main.Id);

                            var cardprovidername_id = c_dynamicpage_main.CardProviderName + parentId;

                            var update_cardprovidername = db.Database.ExecuteSqlCommand("Update Card_dynamicPage set CardProviderName='" + cardprovidername_id + "' where id='" + parentId + "'");


                            for (int i = 0; i < redirectpage1.Length; i++)
                            {

                                string main_image = "";


                                //Grid Four Image//
                                var InputFileName_main = Path.GetFileNameWithoutExtension(bannermainimage[i].FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(bannermainimage[i].FileName);
                                main_image = InputFileName_main.Replace(" ", string.Empty);
                                var ServerSavePath_main = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/") + main_image);
                                //Save file to server folder  
                                bannermainimage[i].SaveAs(ServerSavePath_main);
                                //Grid Four Image//

                                Card_CardData c_data = new Card_CardData();
                                c_data.DynamicHomePageId = parentId;
                                c_data.CardProviderId = card_id;
                                c_data.Title = "";
                                c_data.Main_image = "";

                                var redirectvalue = Convert.ToInt32(redirectpage1[i]);
                                var c_deeplink = db.Card_ActionMaster.Where(c => c.Id == redirectvalue).Select(c => new { c.Deeplink }).SingleOrDefault();
                                c_data.DeepLink = c_deeplink.Deeplink;

                                c_data.Background_image = main_image;
                                c_data.Image_height = "";
                                c_data.Image_width = "";
                                c_data.CreatedOn = DateTime.Now;
                                c_data.CreatedBy = Session["userid"].ToString();
                                c_data.Status = 1;
                                db.Card_CardData.Add(c_data);
                                db.SaveChanges();

                            }
                            ViewBag.SaveStatus = "Record Saved Successfully";
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
            int? PageId = page;
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
                    int contactdetails = (from c in db.Card_dynamicPage
                                          where c.Status != 2 && c.CardProviderId != "3" && c.CardProviderId != "12" && c.Pagename == "HomePage"
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
                        var contactDetails2 = (from c in db.HomePage_Paging(PageId ?? 1, 15, "HomePage")

                                               select new
                                               {
                                                   id = c.id,

                                                   CardProvider = c.ProviderName,
                                                   // ProviderName=c.ProviderName,
                                                   Title = c.Title,
                                                   Subtitle = c.Sub_Title,
                                                   sequence = c.Sequence,
                                                   StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                                                   EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                                                   status = c.status == 1 ? "Active" : "Deactive",


                                               }).Where(c => c.id.ToString().Contains(Session["Search"].ToString()) || c.status.Contains(Session["Search"].ToString()) || c.CardProvider.Contains(Session["Search"].ToString()) || c.StartDate.Contains(Session["Search"].ToString()) || c.EndDate.Contains(Session["Search"].ToString())).OrderBy(c => c.sequence).ToList();



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
                        var contactDetails2 = (from c in db.HomePage_Paging(PageId ?? 1, 15, "HomePage")
                                               select new
                                               {
                                                   id = c.id,

                                                   CardProvider = c.ProviderName,

                                                   sequence = c.Sequence,
                                                   Title = c.Title,
                                                   Subtitle = c.Sub_Title,
                                                   StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                                                   EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                                                   status = c.status == 1 ? "Active" : "Deactive",


                                               }).OrderBy(c => c.sequence).ToList();


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
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    List<ProductAccessTable> pat = db.ProductAccessTables.Where(a => a.promotionid == id).ToList();

                    Card_dynamicPage Cdp = db.Card_dynamicPage.Single(c => c.Id == id);

                    Card_ActionMaster Cam;

                    Card_CardData ccd = new Card_CardData();


                    IEnumerable<string> list = db.Card_CardData.Where(x => x.DynamicHomePageId == id).Select(x => x.Title);


                    if (Cdp.CardProviderId == "6")
                    {



                        IEnumerable<string> RedirectList = db.Card_CardData.Where(x => x.DynamicHomePageId == id).Select(x => x.DeepLink);

                        ViewBag.RedirectPage1 = RedirectList.ElementAt(0);
                        ViewBag.RedirectPage2 = RedirectList.ElementAt(1);
                        ViewBag.RedirectPage3 = RedirectList.ElementAt(2);
                        ViewBag.RedirectPage4 = RedirectList.ElementAt(3);

                        ViewBag.Image1 = db.Card_CardData.Where(x => x.DynamicHomePageId == id).AsEnumerable().ElementAt(0).Main_image;
                        ViewBag.Image2 = db.Card_CardData.Where(x => x.DynamicHomePageId == id).AsEnumerable().ElementAt(1).Main_image;
                        ViewBag.Image3 = db.Card_CardData.Where(x => x.DynamicHomePageId == id).AsEnumerable().ElementAt(2).Main_image;
                        ViewBag.Image4 = db.Card_CardData.Where(x => x.DynamicHomePageId == id).AsEnumerable().ElementAt(3).Main_image;



                        ViewBag.Imagemain = db.Card_dynamicPage.Where(x => x.Id == id).Single().ImageSystemName;
                        ViewBag.ImageBack = db.Card_CardData.Where(x => x.DynamicHomePageId == id).AsEnumerable().ElementAt(0).Background_image;

                        ViewBag.Title1 = list.ElementAt(0);
                        ViewBag.Title2 = list.ElementAt(1);
                        ViewBag.Title3 = list.ElementAt(2);
                        ViewBag.Title4 = list.ElementAt(3);

                        ViewBag.cid = Cdp.CardProviderId;
                        ViewBag.status = Cdp.Status;
                        ViewBag.preStartDate = Convert.ToDateTime(Cdp.Startdate).ToShortDateString();
                        ViewBag.PreEndDate = Convert.ToDateTime(Cdp.Enddate).ToShortDateString();


                        return View(Cdp);
                    }

                    if (Cdp.CardProviderId == "1")
                    {

                        TempData["ID"] = id;
                        Cam = db.Card_ActionMaster.Single(c => c.Pagename == Cdp.CardAction_Pagename);
                        ViewBag.RedirectPage1 = Cam.Pagename;
                        ViewBag.ImageName = Cdp.ImageSystemName;
                        ViewBag.cid = Cdp.CardProviderId;
                        ViewBag.status = Cdp.Status;
                        ViewBag.preStartDate = Convert.ToDateTime(Cdp.Startdate).ToShortDateString();
                        ViewBag.PreEndDate = Convert.ToDateTime(Cdp.Enddate).ToShortDateString();

                        return View(Cdp);

                    }


                    if (Cdp.CardProviderId == "8")
                    {


                        int CountLength = db.Card_CardData.Where(a => a.DynamicHomePageId == id).Count();
                        string[] arr = new string[Convert.ToInt32(CountLength)];
                        string[] arr1 = new string[Convert.ToInt32(CountLength)];
                        int i = 0;

                        ViewBag.cid = Cdp.CardProviderId;
                        ViewBag.preStartDate = Convert.ToDateTime(Cdp.Startdate).ToShortDateString();
                        ViewBag.PreEndDate = Convert.ToDateTime(Cdp.Enddate).ToShortDateString();

                        for (i = 0; i < Convert.ToInt32(CountLength); i++)
                        {
                            var temp = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(i).DeepLink.ToString();
                            arr[i] = db.Card_ActionMaster.Where(x => x.Pagename == temp).First().Id.ToString();
                        }
                        ViewBag.Redirect = arr;


                        for (i = 0; i < Convert.ToInt32(CountLength); i++)
                        {
                            arr1[i] = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(i).Background_image.ToString();
                            // arr[i] = db.Card_ActionMaster.Where(x => x.Pagename == temp).First().Id.ToString();
                        }
                        ViewBag.Imagename = arr1;


                        // TempData["Redirect"] = arr;

                        return View(Cdp);
                    }


                    if (Cdp.CardProviderId == "7")
                    {
                        ViewBag.cid = Cdp.CardProviderId;
                        ViewBag.preStartDate = Convert.ToDateTime(Cdp.Startdate).ToShortDateString();
                        ViewBag.PreEndDate = Convert.ToDateTime(Cdp.Enddate).ToShortDateString();
                        ViewBag.Colour = Cdp.Action1_Colour;
                        ViewBag.Text = Cdp.Action1_Text;
                        Cam = db.Card_ActionMaster.Single(c => c.Pagename == Cdp.CardAction_Pagename);
                        ViewBag.RedirectPage1 = Cam.Pagename;
                        ViewBag.ImageName = Cdp.ImageSystemName;
                        return View(Cdp);
                    }

                    if (Cdp.CardProviderId == "2")
                    {

                        ViewBag.cid = Cdp.CardProviderId;
                        ViewBag.HeaderTitle = Cdp.Title;
                        ViewBag.HeaderTitlecolor = Cdp.TitleColour;
                        ViewBag.SubTitle = Cdp.Sub_Title;
                        ViewBag.SubTitleColor = Cdp.Sub_TitleColour;
                        //Cam = db.Card_ActionMaster.Single(c => c.Pagename == Cdp.CardAction_Pagename);
                        ViewBag.RedirectPage1 = Cdp.CardAction_Pagename;

                        ViewBag.preStartDate = Convert.ToDateTime(Cdp.Startdate).ToShortDateString();
                        ViewBag.PreEndDate = Convert.ToDateTime(Cdp.Enddate).ToShortDateString();

                        // ViewBag.ImageName = Cdp.ImageOriginalName;
                        ViewBag.ImageName = Cdp.ImageSystemName;
                        ViewBag.Imagename1 = db.Card_dynamicPage.Where(x=>x.Id==id+1).AsEnumerable().ElementAt(0).ImageSystemName;
                        return View(Cdp);
                    }

                }
                else
                {
                    return View();

                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult Update(int? id, string Ctype,

             string redirectpage1, HttpPostedFileBase postedFile,                                                       // Home one Image Card

             HttpPostedFileBase mainimageup, HttpPostedFileBase gridbackimage,                                          // Home Grid Card
            string first_gridtitle, HttpPostedFileBase first_upload_gridchildmainimage, string redirectpage1_Homecard,
            string second_gridtitle, HttpPostedFileBase second_upload_gridchildmainimage, string redirectpage2_Homecard,
            string third_gridtitle, HttpPostedFileBase third_upload_gridchildmainimage, string redirectpage3_Homecard,
            string four_gridtitle, HttpPostedFileBase four_upload_gridchildmainimage, string redirectpage4_Homecard,

            string StartDate, string EndDate,                                                                            // Common In all Card

            string redirectpage1_knowMore, HttpPostedFileBase mainimage_KnowMoreCard, string KnowMoreColor, string KnowMoreText, // know more card
            string HeaderTitle_Schemecard, string HeaderTitleColor_Schemecard, string HeaderSubTitle_Schemecard, string HeaderSubTitleColor_Schemecard, //Scheme Card 
            string redirectpage1_SchemeCard, HttpPostedFileBase SchemeCard_HeaderImage, HttpPostedFileBase SchemeCard_MainImage,

            string[] redirectpage1_ConnectPlus_banner_header, HttpPostedFileBase[] file_Images,           //banner                                                                                             

            string statusC, string ParentCatid)
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
                    int productCat1;
                    int ptypeid;
                    int parntcat;
                    int card_id;

                    bool fileflag = false;
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;
                    string filename1 = "";

                    string Status = statusC ?? "off";
                    #region Date validations
                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "Start Date Is Empty");
                    }
                    else
                    {
                        try
                        {

                            //if (Convert.ToDateTime(StartDate) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                            //{
                            //    ModelState.AddModelError("StartDate", "Start Date Should Be Greater Than or Equal To Current Date");
                            //    ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                            //}
                            //DateTime startDate = Convert.ToDateTime(StartDate);
                            //if (db.PermotionsLists.Any(a => a.Enddate >= startDate && a.status != 2 && a.id != id))
                            //{
                            //    ModelState.AddModelError("StartDate", "There Is already A Banner Defined In This Date");
                            //    ViewBag.StartDate = "There Is already A Banner Defined In This Date";
                            //}
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("StartDate", "Invalid Date");
                            ViewBag.StartDate = "Invalid Date";
                        }
                    }
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


                    #region Validate card
                    if (!int.TryParse(Ctype, out card_id))
                    {
                        ModelState.AddModelError("CardProviderId", "Select Card Type");

                    }
                    if (Ctype == "0")
                    {
                        ModelState.AddModelError("CardProviderId", "Select Card Type");
                    }
                    #endregion



                    if (Ctype == "1")                             // for one image card
                    {
                        int redirectpage1_int = Convert.ToInt32(redirectpage1);
                        var ca = db.Card_ActionMaster.Where(x => x.Id == redirectpage1_int).Single().Pagename;

                        if (postedFile != null)
                        {

                            // ModelState.AddModelError("ImageName", "File Is Not Uploaded");
                            fileflag = true;

                            // ViewBag.File = "File Is Not Uploaded";
                        }



                        if (fileflag)
                        {

                            string FileExtension = Path.GetExtension(postedFile.FileName);
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

                        if (ModelState.IsValid)
                        {
                          //  PermotionsList contactusd = db.PermotionsLists.Single(a => a.id == id);

                            Card_dynamicPage cdynamic = db.Card_dynamicPage.Single(a => a.Id == id);

                            if (postedFile != null)
                            {

                                string filename = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                               
                                string str = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), filename);
                                postedFile.SaveAs(str);
                                cdynamic.ImageSystemName = filename;
                                cdynamic.ImageOriginalName = postedFile.FileName;
                            }

                            //contactusd.createby = Session["userid"].ToString();
                            //contactusd.createdate = DateTime.Now;
                            //contactusd.StartDate = Convert.ToDateTime(StartDate);
                            //contactusd.Enddate = Convert.ToDateTime(EndDate);

                            cdynamic.CardAction_Pagename = ca;
                            cdynamic.CardAction_Deeplink = ca;
                            cdynamic.Startdate = Convert.ToDateTime(StartDate);
                            cdynamic.Enddate = Convert.ToDateTime(EndDate);


                            string status = statusC ?? "off";
                            if (status == "on")
                            {
                                cdynamic.Status = 1;
                            }
                            else
                            {
                                cdynamic.Status= 0;
                            }

                            db.SaveChanges();
                            ViewBag.SaveStatus = "Record Saved Successfully";


                        }




                        else
                        {
                            return View("Edit", db.Card_dynamicPage.Single(a => a.Id == id));
                        }
                    }


                    if (Ctype == "2")
                    {
                        int redirectpage1_int = Convert.ToInt32(redirectpage1_SchemeCard);
                        var ca = db.Card_ActionMaster.Where(x => x.Id == redirectpage1_int).Single().Pagename;

                        if (SchemeCard_HeaderImage != null)
                        {

                            // ModelState.AddModelError("ImageName", "File Is Not Uploaded");
                            fileflag = true;

                            // ViewBag.File = "File Is Not Uploaded";
                        }


                        if (fileflag)
                        {
                            fileflag = false;

                            string FileExtension = Path.GetExtension(SchemeCard_HeaderImage.FileName);
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

                        if (ModelState.IsValid)
                        {
                            //PermotionsList contactusd = db.PermotionsLists.Single(a => a.id == id);

                            Card_dynamicPage cdynamic = db.Card_dynamicPage.Single(a => a.Id == id);

                            if (SchemeCard_HeaderImage != null)
                            {

                                string filename2 = Path.GetFileNameWithoutExtension(SchemeCard_HeaderImage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(SchemeCard_HeaderImage.FileName);
                                //contactusd.ImageName = filename1;
                                string str1 = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), filename2);
                                SchemeCard_HeaderImage.SaveAs(str1);

                                cdynamic.ImageOriginalName = SchemeCard_HeaderImage.FileName;
                                cdynamic.ImageSystemName = filename2;
                            }


                            //cdynamic.ImageSystemName = filename1;

                            //string filename2 = Path.GetFileNameWithoutExtension(SchemeCard_MainImage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(SchemeCard_MainImage.FileName);
                            ////contactusd.ImageName = filename1;
                            //string str2 = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), filename2);
                            //SchemeCard_MainImage.SaveAs(str2);


                           // contactusd.createby = Session["userid"].ToString();
                            cdynamic.CreatedOn = DateTime.Now;
                            cdynamic.Startdate = Convert.ToDateTime(StartDate);
                            cdynamic.Enddate = Convert.ToDateTime(EndDate);

                            cdynamic.CardAction_Pagename = ca;
                            cdynamic.CardAction_Deeplink = ca;

                            cdynamic.Title = HeaderTitle_Schemecard;
                            cdynamic.TitleColour = HeaderTitleColor_Schemecard;
                            cdynamic.Sub_Title = HeaderSubTitle_Schemecard;
                            cdynamic.Sub_TitleColour = HeaderSubTitleColor_Schemecard;
                            //cdynamic.OriginalMainImage = SchemeCard_MainImage.FileName;
                            //cdynamic.SystemMainImage = filename1;


                            string status = statusC ?? "off";
                            if (status == "on")
                            {
                                cdynamic.Status = 1;
                            }
                            else
                            {
                                cdynamic.Status = 0;
                            }

                            db.SaveChanges();

                            if (SchemeCard_MainImage != null)
                            {

                                // ModelState.AddModelError("ImageName", "File Is Not Uploaded");
                                fileflag = true;

                                // ViewBag.File = "File Is Not Uploaded";
                            }

                            if (fileflag)
                            {

                                string FileExtension = Path.GetExtension(SchemeCard_MainImage.FileName);
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


                            if (ModelState.IsValid)
                            {
                                //PermotionsList contactusd1 = db.PermotionsLists.Single(a => a.id == id+1);

                                Card_dynamicPage cdynamic1 = db.Card_dynamicPage.Single(a => a.Id == id+1);
                                if (SchemeCard_MainImage != null)
                                {

                                    string filename2 = Path.GetFileNameWithoutExtension(SchemeCard_MainImage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(SchemeCard_MainImage.FileName);
                                    //contactusd.ImageName = filename2;
                                    string str2 = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), filename2);
                                    SchemeCard_MainImage.SaveAs(str2);
                                    cdynamic1.ImageOriginalName = SchemeCard_MainImage.FileName;
                                    cdynamic1.ImageSystemName = filename2;
                                }
                                //cdynamic1.ImageSystemName = filename2;

                                //contactusd1.createby = Session["userid"].ToString();
                                //contactusd1.createdate = DateTime.Now;
                                //contactusd1.StartDate = Convert.ToDateTime(StartDate);
                                //contactusd1.Enddate = Convert.ToDateTime(EndDate);

                                cdynamic1.CardAction_Pagename = ca;
                                cdynamic1.CardAction_Deeplink = ca;


                            }

                            db.SaveChanges();

                            ViewBag.SaveStatus = "Record Saved Successfully";


                        }


                        else
                        {
                            return View("Edit", db.Card_dynamicPage.Single(a => a.Id == id));
                        }
                    }



                    if (Ctype == "6")
                    {

                        int redirectpage1_Homecard_int = Convert.ToInt32(redirectpage1_Homecard);
                        int redirectpage2_Homecard_int = Convert.ToInt32(redirectpage2_Homecard);
                        int redirectpage3_Homecard_int = Convert.ToInt32(redirectpage3_Homecard);
                        int redirectpage4_Homecard_int = Convert.ToInt32(redirectpage4_Homecard);

                        var HomeCardPageName1 = db.Card_ActionMaster.Where(x => x.Id == redirectpage1_Homecard_int).Single().Pagename;
                        var HomeCardPageName2 = db.Card_ActionMaster.Where(x => x.Id == redirectpage2_Homecard_int).Single().Pagename;
                        var HomeCardPageName3 = db.Card_ActionMaster.Where(x => x.Id == redirectpage3_Homecard_int).Single().Pagename;
                        var HomeCardPageName4 = db.Card_ActionMaster.Where(x => x.Id == redirectpage4_Homecard_int).Single().Pagename;




                        if (mainimageup != null)
                        {
                            fileflag = true;

                        }

                        if (fileflag)
                        {

                            string FileExtension = Path.GetExtension(mainimageup.FileName);
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

                        if (ModelState.IsValid)
                        {

                            Card_dynamicPage cdynamic = db.Card_dynamicPage.Single(a => a.Id == id);

                            Card_CardData Ccdata0 = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(0);
                            Card_CardData Ccdata1 = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(1);
                            Card_CardData Ccdata2 = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(2);
                            Card_CardData Ccdata3 = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(3);

                            if (mainimageup != null)
                            {
                                string MainImage = Path.GetFileNameWithoutExtension(mainimageup.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(mainimageup.FileName);
                                string strMain = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), MainImage);
                                mainimageup.SaveAs(strMain);
                                cdynamic.ImageOriginalName = mainimageup.FileName;
                                cdynamic.ImageSystemName = MainImage;
                                //Ccdata0.Main_image = Chaild1;
                            }


                            if (first_upload_gridchildmainimage != null)
                            {
                               // string BackImage = Path.GetFileNameWithoutExtension(gridbackimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(gridbackimage.FileName);
                                string Chaild1 = Path.GetFileNameWithoutExtension(first_upload_gridchildmainimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(first_upload_gridchildmainimage.FileName);
                                string strMain = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), Chaild1);
                                first_upload_gridchildmainimage.SaveAs(strMain);
                                Ccdata0.Main_image = Chaild1;

                            }

                            if (second_upload_gridchildmainimage != null)
                            {
                               // string BackImage = Path.GetFileNameWithoutExtension(gridbackimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(gridbackimage.FileName);
                                string Chaild2 = Path.GetFileNameWithoutExtension(second_upload_gridchildmainimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(second_upload_gridchildmainimage.FileName);
                                string strMain = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), Chaild2);
                                second_upload_gridchildmainimage.SaveAs(strMain);
                                Ccdata1.Main_image = Chaild2;
                            }

                            if (third_upload_gridchildmainimage != null)
                            {
                               // string BackImage = Path.GetFileNameWithoutExtension(gridbackimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(gridbackimage.FileName);
                                string Chaild3 = Path.GetFileNameWithoutExtension(third_upload_gridchildmainimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(third_upload_gridchildmainimage.FileName);
                                string strMain = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), Chaild3);
                                third_upload_gridchildmainimage.SaveAs(strMain);
                                Ccdata2.Main_image = Chaild3;
                            }

                            if (four_upload_gridchildmainimage != null)
                            {
                               // string BackImage = Path.GetFileNameWithoutExtension(gridbackimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(gridbackimage.FileName);
                                string Chaild4 = Path.GetFileNameWithoutExtension(four_upload_gridchildmainimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(four_upload_gridchildmainimage.FileName);
                                string strMain = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), Chaild4);
                                four_upload_gridchildmainimage.SaveAs(strMain);
                                Ccdata3.Main_image = Chaild4;

                            }

                            if (gridbackimage != null)
                            {
                                string BackImage = Path.GetFileNameWithoutExtension(gridbackimage.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(gridbackimage.FileName);
                                string strMain = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), BackImage);
                                gridbackimage.SaveAs(strMain);
                                Ccdata0.Background_image = BackImage;
                                Ccdata1.Background_image = BackImage;
                                Ccdata2.Background_image = BackImage;
                                Ccdata3.Background_image = BackImage;



                            }






                            cdynamic.Startdate = Convert.ToDateTime(StartDate);
                            cdynamic.Enddate = Convert.ToDateTime(EndDate);


                            Ccdata0.Title = first_gridtitle;
                            // Ccdata0.Background_image = BackImage;
                            // Ccdata0.Main_image = Chaild1;
                            Ccdata0.DeepLink = HomeCardPageName1;
                            Ccdata0.CreatedOn = DateTime.Now;

                            Ccdata1.Title = second_gridtitle;
                            //  Ccdata1.Background_image = BackImage;
                            //   Ccdata1.Main_image = Chaild2;
                            Ccdata1.DeepLink = HomeCardPageName2;
                            Ccdata1.CreatedOn = DateTime.Now;


                            Ccdata2.Title = third_gridtitle;
                            // Ccdata2.Background_image = BackImage;
                            // Ccdata2.Main_image = Chaild3;
                            Ccdata2.DeepLink = HomeCardPageName3;
                            Ccdata2.CreatedOn = DateTime.Now;


                            Ccdata3.Title = four_gridtitle;
                            //  Ccdata3.Background_image = BackImage;
                            //   Ccdata3.Main_image = Chaild4;
                            Ccdata3.DeepLink = HomeCardPageName4;
                            Ccdata3.CreatedOn = DateTime.Now;

                            string status = statusC ?? "off";
                            if (status == "on")
                            {
                                cdynamic.Status = 1;
                            }
                            else
                            {
                                cdynamic.Status = 0;
                            }

                            db.SaveChanges();
                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }
                        else
                        {
                            return View("Edit", db.Card_dynamicPage.Single(a => a.Id == id));
                        }

                    }


                    if (Ctype == "8")
                    {
                        var datacount = db.Card_CardData.Where(a => a.DynamicHomePageId == id).Count();
                        int j = 0;
                        for (j = 0; j < redirectpage1_ConnectPlus_banner_header.Count(); j++)
                        {
                            if (file_Images[j] != null)
                            {
                                fileflag = true;
                            }
                            if (fileflag)
                            {
                                string FileExtension = Path.GetExtension(file_Images[j].FileName);
                                if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                                   FileExtension.ToLower() == ".png")
                                {
                                    fileflag = false;
                                }

                                else
                                {
                                    ModelState.AddModelError("ImageName", "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg");
                                    ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                                }

                            }
                            if (ModelState.IsValid)
                            {
                                Card_dynamicPage cdynamic2 = db.Card_dynamicPage.Single(a => a.Id == id);

                                if (file_Images[j] != null && j < datacount)
                                {
                                    var tempdata = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(j);
                                    // Card_dynamicPage cdynamic = db.Card_dynamicPage.Single(a => a.Id == id);
                                    string filename = Path.GetFileNameWithoutExtension(file_Images[j].FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file_Images[j].FileName);
                                    string str = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), filename);
                                    file_Images[j].SaveAs(str);
                                    tempdata.Background_image = filename;
                                }

                                else
                                {
                                    if (file_Images[j] != null)
                                    {

                                        string filename = Path.GetFileNameWithoutExtension(file_Images[j].FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file_Images[j].FileName);
                                        string str = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), filename);
                                        file_Images[j].SaveAs(str);
                                        filename1 = filename;

                                    }

                                }




                                // var tempdata = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(j);
                                if (datacount > j)
                                {

                                    var tempdata = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(j);
                                    int redirectId_banner = Convert.ToInt32(redirectpage1_ConnectPlus_banner_header[j]);
                                    tempdata.DeepLink = db.Card_ActionMaster.Where(z => z.Id == redirectId_banner).Single().Pagename;

                                }

                                else
                                {
                                    Card_dynamicPage cdynamic = db.Card_dynamicPage.Single(a => a.Id == id);
                                    Card_CardData cdata = new Card_CardData();
                                    cdata.DynamicHomePageId = id;
                                    cdata.CardProviderId = Convert.ToInt32(Ctype);
                                    cdata.Background_image = filename1;
                                    int redirectId_banner = Convert.ToInt32(redirectpage1_ConnectPlus_banner_header[j]);
                                    cdata.DeepLink = db.Card_ActionMaster.Where(z => z.Id == redirectId_banner).Single().Pagename;
                                    cdata.CreatedOn = DateTime.Now;
                                    cdata.CreatedBy = Session["userid"].ToString();
                                    string status = statusC ?? "off";
                                    if (status == "on")
                                    {
                                        cdata.Status = 1;
                                    }
                                    else
                                    {
                                        cdata.Status = 0;
                                    }

                                    cdynamic.Id = Convert.ToDecimal(id);
                                    cdynamic.CardProviderId = Ctype;

                                    cdynamic.CardProviderName = db.Card_ProviderMaster.Where(x => x.Id == card_id).Single().CardProviderName;
                                    cdynamic.ClassName = db.Card_ProviderMaster.Where(x => x.Id == card_id).Single().Classname;
                                    cdynamic.Startdate = Convert.ToDateTime(StartDate);
                                    cdynamic.Enddate = Convert.ToDateTime(EndDate);
                                    cdynamic.CreatedOn = Convert.ToDateTime(StartDate);
                                    cdynamic.CreatedBy = Session["userid"].ToString();

                                    cdynamic.Pagename = "HomePage";
                                    db.Card_CardData.Add(cdata);
                                    //db.Card_dynamicPage.AddObject(cdynamic);

                                    string status1 = statusC ?? "off";
                                    if (status1 == "on")
                                    {
                                        cdynamic.Status = 1;
                                    }
                                    else
                                    {
                                        cdynamic.Status = 0;
                                    }
                                    db.SaveChanges();
                                }

                                string status2 = statusC ?? "off";
                                if (status2 == "on")
                                {
                                    cdynamic2.Status = 1;
                                }
                                else
                                {
                                    cdynamic2.Status = 0;
                                }
                                db.SaveChanges();
                                ViewBag.SaveStatus = "Record Saved Successfully";
                                //return View("Index");
                            }

                            else
                            {
                                return View("Edit", db.Card_dynamicPage.Single(a => a.Id == id));
                            }
                        }
                        return View("Index");
                    }





                    if (Ctype == "7")
                    {

                        int redirectpage1_int = Convert.ToInt32(redirectpage1_knowMore);
                        var ca = db.Card_ActionMaster.Where(x => x.Id == redirectpage1_int).Single().Pagename;

                        if (mainimage_KnowMoreCard != null)
                        {

                            // ModelState.AddModelError("ImageName", "File Is Not Uploaded");
                            fileflag = true;

                            // ViewBag.File = "File Is Not Uploaded";
                        }

                        if (fileflag)
                        {

                            string FileExtension = Path.GetExtension(mainimage_KnowMoreCard.FileName);
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

                        if (ModelState.IsValid)
                        {
                            PermotionsList contactusd = db.PermotionsLists.Single(a => a.id == id);

                            Card_dynamicPage cdynamic = db.Card_dynamicPage.Single(a => a.Id == id);

                            if (mainimage_KnowMoreCard != null)
                            {



                                string filename = Path.GetFileNameWithoutExtension(mainimage_KnowMoreCard.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(mainimage_KnowMoreCard.FileName);

                                contactusd.ImageName = filename;
                                string str = Path.Combine(Server.MapPath("~/MpartnerIB_Api/CardImage/"), filename);
                                mainimage_KnowMoreCard.SaveAs(str);
                                cdynamic.ImageSystemName = filename;
                                cdynamic.ImageOriginalName = mainimage_KnowMoreCard.FileName;
                            }

                            cdynamic.CreatedBy = Session["userid"].ToString();
                            cdynamic.CreatedOn = DateTime.Now;
                            cdynamic.Startdate = Convert.ToDateTime(StartDate);
                            cdynamic.Enddate = Convert.ToDateTime(EndDate);

                            cdynamic.CardAction_Pagename = ca;
                            cdynamic.CardAction_Deeplink = ca;

                            cdynamic.Action1_Colour = KnowMoreColor;
                            cdynamic.Action1_Text = KnowMoreText;

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


                        }


                        else
                        {
                            return View("Edit", db.Card_dynamicPage.Single(a => a.Id == id));
                        }
                    }


                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }

            return View("Index");
        }
















        //[HttpPost]
        //public ActionResult Update(int? id, string redirectpage1, HttpPostedFileBase postedFile, string startDate, string endDate)
        //{
        //    return View();
        //}

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
                    //PermotionsList contactusd = db.PermotionsLists.Single(a => a.id == id);
                    Card_dynamicPage contactusd = db.Card_dynamicPage.Single(a => a.Id==id);

                    //PermotionsListHistory plh = new PermotionsListHistory();
                    //plh.Descriptions = contactusd.Descriptions;
                    //plh.Enddate = contactusd.Enddate;
                    //plh.ImageName = contactusd.ImageName;
                    //plh.Modifiedby = Session["userid"].ToString();
                    //plh.modifieddate = DateTime.Now;
                    //plh.PermotionTypeId = contactusd.PermotionTypeId;
                    //plh.ProductCatId = contactusd.ProductCatId;
                    //plh.ProductLvlOneId = contactusd.ProductLvlOneId;
                    //plh.promotionListId = contactusd.id;
                    //plh.StartDate = contactusd.StartDate;
                    //plh.status = contactusd.status;
                    //db.PermotionsListHistories.AddObject(plh);

                    contactusd.Status = 2;
                    contactusd.ModifiedBy= Session["userid"].ToString();
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


        [HttpPost]
        public ActionResult UpdateSequence(string ids)
        {
            var Prodsplit = ids.Split(',');
            int prodid = Convert.ToInt32(Prodsplit[0]);
            Card_dynamicPage bn = db.Card_dynamicPage.Single(c => c.Id == prodid);
            bn.Sequence = Convert.ToInt32(Prodsplit[1]);
            if (bn.CardProviderId == "2")
            {
                var scheme_mainid = prodid + 1;
                var seq = bn.Sequence + 1;
                //rajesh changes
                db.Database.ExecuteSqlCommand("Update Card_DynamicPage set sequence='" + seq + "' where id='" + scheme_mainid + "'");
            }
            db.SaveChanges();
            return RedirectToAction("Index");
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
