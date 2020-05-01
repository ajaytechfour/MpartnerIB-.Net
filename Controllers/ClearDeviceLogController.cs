using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Luminous.EF;
namespace Luminous.Controllers
{
    public class ClearDevicelogController : Controller
    {
        //
        // GET: /ClearDeviceLog/
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();

        private string PageUrl = "/ClearDeviceLog/index";
        public ActionResult Index()
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
                    return View();
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }

            }

        }


        public JsonResult getUserid(string Id)
        {

            try
            {
                var getdeviceid = db.UserVerifications.Where(c => c.UserId == Id && c.Status == 1).GroupBy(c=>c.DeviceId).Select(c => c.FirstOrDefault()).ToList();
                return Json(getdeviceid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }



        }
        public ActionResult UserPermission(int Id, string Deviceid)
        {
            try
            {
                db.ExecuteStoreCommand("Update Userverification  set Status=0,Unbolckedby='" + Session["Id"] + "' where Userid='" + Id + "' and DeviceId='" + Deviceid + "'");

                return Content("<script>alert('User Updated Successfully');");
            }
            catch(Exception exc)
            {
               
                return Content("<script>alert('Not Updated Successfully');");
            }

        }
    }
}
