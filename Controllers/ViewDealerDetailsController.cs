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
    public class ViewDealerDetailsController : Controller
    {
        datautility dut = new datautility();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        LuminousMpartnerIBEntities db;
        List<SideBarMenuModel> sideBarMenuLst = new List<SideBarMenuModel>();
        string userid = string.Empty;

        public ViewDealerDetailsController()
        {
            db = new LuminousMpartnerIBEntities();
        }

        public ActionResult Index(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                UsersList sapcode = db.UsersLists.Single(a => a.id == id);


                UsersList cud = db.UsersLists.Where(x => x.Dis_Sap_Code == sapcode.Dis_Sap_Code).FirstOrDefault();

                ViewBag.SapCode = cud.Dis_Sap_Code;
                ViewBag.UserId = cud.UserId;
                ViewBag.CustomerType = cud.CustomerType;
                ViewBag.Name = cud.Dis_Name;
                ViewBag.Address = cud.Dis_Address1;
                ViewBag.City = cud.Dis_City;
                ViewBag.State = cud.Dis_State;
                ViewBag.ContactNo = cud.Dis_ContactNo;
                ViewBag.Email = cud.Dis_Email;
                ViewBag.Country = cud.Country;


                List<Dealer_Save_Image> dealerImageData = db.Dealer_Save_Image.Where(x => x.DealerID == cud.Dis_Sap_Code).ToList();

                // join od in db.Dealer_Save_Image on pd.Dis_Sap_Code equals od.DealerID
                //where pd.DealerID == cud.Dis_Sap_Code
                //select new Dealer_Save_Image
                //{
                //    DealerID = pd.DealerID,
                //    ImageName = pd.ImageName,
                //}).ToList();

                if (dealerImageData.Count() > 0)
                {
                    List<Dealer_Save_Image> dealerImageLst = dealerImageData;
                    ViewBag.dlrimage = dealerImageLst;
                    return View(dealerImageLst);
                }
                else
                {

                    return View();
                }

            }
        }

    }
}
