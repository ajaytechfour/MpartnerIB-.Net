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
namespace LuminousMpartnerIB.Controllers
{
    public class ConnectPlusController : Controller
    {
        //
        // GET: /CreatePermotions/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/ConnectPlus/Index";
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
                if (true/*result[0]["uview"].ToString() == "1"*/)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
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
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true/*result[0]["uview"].ToString() == "1"*/)
                {
                    var Company = (from c in db.Card_ProviderMaster
                                   where c.Status != 2 && c.Pagename == "Connect+"
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
        public JsonResult GetRedirectPage()
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
                if (true/*result[0]["uview"].ToString() == "1"*/)
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


        public ActionResult SaveContact(string Ctype, string[] redirectpage1, string[] Title,
         HttpPostedFileBase[] bannermainimage, string StartDate, string EndDate, string statusC)
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
                if (true /*result[0]["createrole"].ToString() == "1"*/)
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
                        bannermainimage = bannermainimage.Where(w => w != null).ToArray();

                        if (Ctype == "16")
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
                            c_dynamicpage_main.CardDataFlag = "3";
                            c_dynamicpage_main.Pagename = "";
                            c_dynamicpage_main.Action1_Colour = "";
                            c_dynamicpage_main.Action1_Text = "";
                            c_dynamicpage_main.Pagename = "Connect+";

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
                            db.Card_dynamicPage.AddObject(c_dynamicpage_main);
                            db.SaveChanges();
                            parentId = Convert.ToInt32(c_dynamicpage_main.Id);




                            for (int i = 0; i < redirectpage1.Length; i++)
                            {

                                string main_image = "";





                                //Grid Four Image//
                                var InputFileName_main = Path.GetFileNameWithoutExtension(bannermainimage[i].FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(bannermainimage[i].FileName);
                                main_image = InputFileName_main.Replace(" ", string.Empty);
                                var ServerSavePath_main = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/") + main_image);
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
                                db.Card_CardData.AddObject(c_data);
                                db.SaveChanges();

                            }
                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                        if (Ctype == "15")
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
                            c_dynamicpage_main.CardDataFlag = "4";
                            c_dynamicpage_main.Pagename = "";
                            c_dynamicpage_main.Action1_Colour = "";
                            c_dynamicpage_main.Action1_Text = "";
                            c_dynamicpage_main.Pagename = "Connect+";

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
                            db.Card_dynamicPage.AddObject(c_dynamicpage_main);
                            db.SaveChanges();
                            parentId = Convert.ToInt32(c_dynamicpage_main.Id);

                            for (int i = 0; i < redirectpage1.Length; i++)
                            {
                                string main_image = "";


                                //Grid Four Image//
                                var InputFileName_main = Path.GetFileNameWithoutExtension(bannermainimage[i].FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(bannermainimage[i].FileName);
                                main_image = InputFileName_main.Replace(" ", string.Empty);
                                var ServerSavePath_main = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/") + main_image);
                                //Save file to server folder  
                                bannermainimage[i].SaveAs(ServerSavePath_main);
                                //Grid Four Image//


                                Card_CardData c_data = new Card_CardData();
                                c_data.DynamicHomePageId = parentId;
                                c_data.CardProviderId = card_id;
                                c_data.Title = Title[i];
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
                                db.Card_CardData.AddObject(c_data);
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
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["uview"].ToString() == "1"*/)
                {
                    int contactdetails = (from c in db.Card_dynamicPage
                                          where c.Status != 2 && c.Pagename == "Connect+"
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
                        var contactDetails2 = (from c in db.HomePage_Paging(PageId ?? 1, 15, "Connect+")
                                               select new
                                               {
                                                   id = c.id,

                                                   CardProvider = c.CardProviderName,

                                                   Title = c.Title,
                                                   Subtitle = c.Sub_Title,
                                                   StartDate = c.StartDate != null ? Convert.ToDateTime(c.StartDate).ToShortDateString() : "",
                                                   EndDate = c.EndDate != null ? Convert.ToDateTime(c.EndDate).ToShortDateString() : "",
                                                   status = c.status == 1 ? "Active" : "Deactive",


                                               }).Where(c => c.id.ToString().Contains(Session["Search"].ToString()) || c.status.Contains(Session["Search"].ToString()) || c.CardProvider.Contains(Session["Search"].ToString()) || c.StartDate.Contains(Session["Search"].ToString()) || c.EndDate.Contains(Session["Search"].ToString())).ToList();



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
                        var contactDetails2 = (from c in db.HomePage_Paging(PageId ?? 1, 15, "Connect+")
                                               select new
                                               {
                                                   id = c.id,

                                                   CardProvider = c.CardProviderName,

                                                   Title = c.Title,
                                                   Subtitle = c.Sub_Title,
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

                if (true /*result[0]["editrole"].ToString() == "1"*/)
                {
                    PermotionsList cud = db.PermotionsLists.Single(a => a.id == id);
                    List<ProductAccessTable> pat = db.ProductAccessTables.Where(a => a.promotionid == id).ToList();
                    Card_dynamicPage Cdp = db.Card_dynamicPage.Single(c => c.Id == id);
                    IEnumerable<string> list = db.Card_CardData.Where(x => x.DynamicHomePageId == id).Select(x => x.Title);

                    if (Cdp.CardProviderId == "15")
                    {
                        int CountLength = db.Card_CardData.Where(a => a.DynamicHomePageId == id).Count();
                        string[] arr = new string[Convert.ToInt32(CountLength)];
                        string[] arr1 = new string[Convert.ToInt32(CountLength)];
                        string[] arr2 = new string[Convert.ToInt32(CountLength)];

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


                        for (i = 0; i < Convert.ToInt32(CountLength); i++)
                        {
                            arr2[i] = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(i).Title.ToString();
                            // arr[i] = db.Card_ActionMaster.Where(x => x.Pagename == temp).First().Id.ToString();
                        }
                        ViewBag.Titlee = arr2;

                        return View(Cdp);

                    }

                    if (Cdp.CardProviderId == "16")
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
                        }
                        ViewBag.ImageName = arr1;
                        return View(Cdp);

                    }
                }


                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
            return View();
        }

        public ActionResult Update(int? id, string Ctype,

            string[] Titles, string[] redirectpage1_ConnectPlus_scroll_Image, HttpPostedFileBase[] file_Images,
            string[] redirectpage1_ConnectPlus_banner_header,


            string Ptype, string Pcat, string PlvlOne, HttpPostedFileBase file, string Descriptions, string StartDate, string EndDate, string statusC, string ParentCatid)
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
                if (true /*result[0]["editrole"].ToString() == "1"*/)
                {
                    int card_id;
                    int pcid;
                    int productCat1;
                    int ptypeid;
                    int parntcat;
                    bool fileflag = false;
                    string filename1 = "";
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;
                    ViewBag.ProductCat = Pcat;
                    ViewBag.ProductCat1 = PlvlOne;
                    ViewBag.Ptype = Ptype;
                    ViewBag.Des = Descriptions;
                    ViewBag.parntcat = ParentCatid;

                    //#region Product Category
                    //if (!int.TryParse(Pcat, out pcid))
                    //{

                    //    ModelState.AddModelError("ProductCatId", "Select Product Category");
                    //}
                    //if (Pcat != null)
                    //{
                    //    if (Pcat == "0")
                    //    {
                    //        ModelState.AddModelError("ProductCatId", "Select Product Category");
                    //    }
                    //}
                    //#endregion
                    //#region Validate Parent Category
                    //if (!int.TryParse(ParentCatid, out parntcat))
                    //{
                    //    ModelState.AddModelError("ParentCatid", "Select Parent Category");

                    //}
                    //if (parntcat == 0)
                    //{
                    //    ModelState.AddModelError("ParentCatid", "Select Parent Category");
                    //}
                    //#endregion
                    //#region Product level One
                    //if (!int.TryParse(PlvlOne, out productCat1))
                    //{

                    //    ModelState.AddModelError("ProductLvlOneId", "Select Product Category Level One");
                    //}
                    //if (PlvlOne != null)
                    //{
                    //    if (PlvlOne == "0")
                    //    {
                    //        ModelState.AddModelError("ProductLvlOneId", "Select Product Category Level One");
                    //    }
                    //}
                    //#endregion
                    //#region Permotion Type
                    //if (!int.TryParse(Ptype, out ptypeid))
                    //{

                    //    ModelState.AddModelError("PermotionTypeId", "Select Permotion Type");
                    //}
                    //if (Ptype != null)
                    //{
                    //    if (Ptype == "0")
                    //    {
                    //        ModelState.AddModelError("PermotionTypeId", "Select Permotion Type");
                    //    }
                    //}
                    //#endregion

                    //string Status = statusC ?? "off";
                    //#region Date validations
                    //if (StartDate == null || StartDate == "")
                    //{
                    //    ModelState.AddModelError("StartDate", "Start Date Is Empty");
                    //}
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
                    //if (EndDate == null || EndDate == "")
                    //{
                    //    ModelState.AddModelError("EndDate", "End Date Is Not Selected");
                    //    ViewBag.EndDate = "End Date Is Not Selected";
                    //}
                    //else
                    //{

                    //    DateTime startDate = new DateTime();
                    //    try
                    //    {
                    //        startDate = Convert.ToDateTime(StartDate);
                    //        try
                    //        {
                    //            if (Convert.ToDateTime(EndDate) < startDate)
                    //            {
                    //                ModelState.AddModelError("Enddate", "End Date Should Be Greater Than or Equal To Start Date");
                    //                ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                    //            }
                    //        }
                    //        catch (FormatException ex)
                    //        {
                    //            ModelState.AddModelError("Enddate", "Invalid Date");
                    //            ViewBag.EndDate = "Invalid End Date";
                    //        }

                    //    }
                    //    catch (FormatException ex)
                    //    {
                    //        ModelState.AddModelError("Enddate", "Invalid Start Date");
                    //        ViewBag.EndDate = "Invalid Start Date";
                    //    }
                    //}

                    // #endregion
                    //#region File Validations
                    //if (file == null)
                    //{

                    //    // ModelState.AddModelError("ImageName", "File Is Not Uploaded");
                    //    fileflag = true;

                    //    // ViewBag.File = "File Is Not Uploaded";
                    //}
                    //else
                    //{
                    //    string FileExtension = Path.GetExtension(file.FileName);
                    //    if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                    //       FileExtension.ToLower() == ".png")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("ImageName", "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg");
                    //        ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                    //    }



                    //}
                    //if (Descriptions == "" || Descriptions == null)
                    //{
                    //    ModelState.AddModelError("Descriptions", "Descriptions Is Empty");

                    //}
                    //#endregion

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

                    if (Ctype == "15")
                    {
                        var datacount = db.Card_CardData.Where(a => a.DynamicHomePageId == id).Count();
                        int j = 0;
                        for (j = 0; j < Titles.Count(); j++)
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
                                    string str = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/"), filename);
                                    file_Images[j].SaveAs(str);
                                    tempdata.Background_image = filename;
                                }

                                else
                                {
                                    if (file_Images[j] != null)
                                    {

                                        string filename = Path.GetFileNameWithoutExtension(file_Images[j].FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file_Images[j].FileName);
                                        string str = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/"), filename);
                                        file_Images[j].SaveAs(str);
                                        filename1 = filename;

                                    }

                                }


                                // var tempdata = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(j);
                                if (datacount > j)
                                {

                                    var tempdata = db.Card_CardData.Where(a => a.DynamicHomePageId == id).AsEnumerable().ElementAt(j);
                                    int redirectId_banner = Convert.ToInt32(redirectpage1_ConnectPlus_scroll_Image[j]);
                                    tempdata.DeepLink = db.Card_ActionMaster.Where(z => z.Id == redirectId_banner).Single().Pagename;
                                    tempdata.Title = Titles[j];
                                }

                                else
                                {
                                    Card_dynamicPage cdynamic = db.Card_dynamicPage.Single(a => a.Id == id);
                                    Card_CardData cdata = new Card_CardData();
                                    cdata.DynamicHomePageId = id;
                                    cdata.CardProviderId = Convert.ToInt32(Ctype);
                                    cdata.Title = Titles[j];
                                    cdata.Background_image = filename1;
                                    int redirectId_banner = Convert.ToInt32(redirectpage1_ConnectPlus_scroll_Image[j]);
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

                                    cdynamic.Pagename = "Connect+";
                                    db.Card_CardData.AddObject(cdata);
                                    //db.Card_dynamicPage.AddObject(cdynamic);
                                    // db.SaveChanges();
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

                                //string status1 = statusC ?? "off";
                                //if (status1 == "on")
                                //{
                                //    cdynamic.Status = 1;
                                //}
                                //else
                                //{
                                //    cdynamic.Status = 0;
                                //}
                                //db.SaveChanges();

                            }
                        }

                        ViewBag.SaveStatus = "Record Saved Successfully";
                        return View("Index");
                    }

                    if (Ctype == "16")
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
                                    string str = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/"), filename);
                                    file_Images[j].SaveAs(str);
                                    tempdata.Background_image = filename;
                                }

                                else
                                {
                                    if (file_Images[j] != null)
                                    {

                                        string filename = Path.GetFileNameWithoutExtension(file_Images[j].FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file_Images[j].FileName);
                                        string str = Path.Combine(Server.MapPath("~/MpartnerNewApi/CardImage/"), filename);
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

                                    cdynamic.Pagename = "Connect+";
                                    db.Card_CardData.AddObject(cdata);
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




                            }
                        }

                        ViewBag.SaveStatus = "Record Saved Successfully";
                        return View("Index");
                    }

                    else
                    {
                        return View("Edit", db.Card_dynamicPage.Single(a => a.Id == id));
                    }


                }
            }
            return RedirectToAction("snotallowed", "snotallowed");
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
                if (true /*result[0]["deleterole"].ToString() == "1"*/)
                {
                    Card_dynamicPage contactusd = db.Card_dynamicPage.Single(a => a.Id == id);

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