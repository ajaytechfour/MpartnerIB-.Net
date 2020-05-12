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
    public class ViewDealerController : Controller
    {
        datautility dut = new datautility();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        LuminousMpartnerIBEntities db;
        List<SideBarMenuModel> sideBarMenuLst = new List<SideBarMenuModel>();
        string userid = string.Empty;

        public ViewDealerController()
        {
            db = new LuminousMpartnerIBEntities();
        }

        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                return View();
            }
        }

        public JsonResult GetGridData()
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var getGrid = from vs in db.UsersLists
                              where vs.CustomerType == "DEALER"
                              orderby vs.CustomerType
                              select new
                              {
                                  id = vs.id,
                                  UserId = vs.UserId,
                                  SapCode = vs.Dis_Sap_Code,
                                  CustomerType = vs.CustomerType,
                                  Name = vs.Dis_Name,
                                  Address = vs.Dis_Address1,
                                  City = vs.Dis_City,
                                  State = vs.Dis_State,
                                  ContactNo = vs.Dis_ContactNo,
                                  Email = vs.Dis_Email,
                                  country=vs.Country,
                              };


                if (getGrid != null)
                {


                    var data = new { result = getGrid, TotalRecord = getGrid.Count() };
                    return Json(data, JsonRequestBehavior.AllowGet);
                    // }
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }

            }
        }                     
      

    }
}
