using LuminousMpartnerIB.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuminousMpartnerIB.Controllers
{
    public class PriceController : Controller
    {
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
       // private string PageUrl = "/ProductCategory/index";

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetDistributors()
        {
            if ((Session["userid"] == null && Session["Ioslogin"] == null))
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var getParentCat = (from c in db.UsersLists
                                    where c.CustomerType == "DISTY"
                                    select new
                                    {
                                        id = c.UserId,
                                        Name = c.Dis_Name
                                    }).ToList();
                return Json(getParentCat, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetContactDetail(string id = "")
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                int contactdetails = (from c in db.Card_dynamicPage
                                      where c.Status != 2 && c.Pagename == "Price"
                                      select c).Count();

                if (id != "")
                {
                    var contactDetails2 = (from c in db.PermotonsListPagingScheme_Price_New("Price")
                                           where c.CreatedBy.ToLower() == id.ToLower()
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
                }
                else
                {
                    var contactDetails2 = (from c in db.PermotonsListPagingScheme_Price_New("Price")
                                               // where c.CreatedBy.ToLower() == id.ToLower()
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

                }

            }

        }

        [HttpGet]
        public ActionResult View(int id)
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
                    ViewBag.ProviderName = cud.CardProviderName;
                    ViewBag.PrntName = cud.Subcatname;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
    }
}



