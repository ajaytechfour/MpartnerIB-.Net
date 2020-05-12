using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data;
using LuminousMpartnerIB.EF;
using TVS;
using LuminousMpartnerIB.Models;

namespace LuminousMpartnerIB.Controllers
{
    public class CreateSalesEmployeesViewController : Controller
    {
        datautility dut = new datautility();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        LuminousMpartnerIBEntities db;
        List<SideBarMenuModel> sideBarMenuLst = new List<SideBarMenuModel>();
        string userid = string.Empty;
        string utype = string.Empty;

        public CreateSalesEmployeesViewController()
        {
            db = new LuminousMpartnerIBEntities();
        }

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

        public JsonResult GetGridData(string id = "")
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (id != "")
                {
                    var getGrid = from vs in db.UsersLists
                                  where vs.CustomerType == "SalesEmployee" && vs.CreatedBY == id
                                  orderby vs.CustomerType
                                  select new
                                  {
                                      id = vs.id,
                                      UserId = vs.UserId,
                                      SapCode=vs.Dis_Sap_Code,
                                      CustomerType = vs.CustomerType,
                                      Name = vs.Dis_Name,
                                      Address = vs.Dis_Address1,
                                      Country=vs.Country,
                                      City = vs.Dis_City,
                                      State = vs.Dis_State,
                                      ContactNo = vs.Dis_ContactNo,
                                      Email = vs.Dis_Email,
                                  };

                    var data = new { result = getGrid, TotalRecord = getGrid.Count() };
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    var getGrid = from vs in db.UsersLists
                                  where vs.CustomerType == "SalesEmployee"
                                  orderby vs.CustomerType
                                  select new
                                  {
                                      id = vs.id,
                                      UserId = vs.UserId,
                                      SapCode = vs.Dis_Sap_Code,
                                      CustomerType = vs.CustomerType,
                                      Name = vs.Dis_Name,
                                      Address = vs.Dis_Address1,
                                      Country = vs.Country,
                                      City = vs.Dis_City,
                                      State = vs.Dis_State,
                                      ContactNo = vs.Dis_ContactNo,
                                      Email = vs.Dis_Email,
                                  };

                    var data = new { result = getGrid, TotalRecord = getGrid.Count() };
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
