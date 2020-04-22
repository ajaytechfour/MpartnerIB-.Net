using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using LuminousMpartnerIB.EF;
using System.Data.Linq;
namespace LuminousMpartnerIB.Controllers
{
    public class UserVerificationPermissionController : Controller
    {
        //
        // GET: /UserVerificationPermission/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();

        private string PageUrl = "/UserVerificationPermission/index";
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
                    var Uservarification = db.UserVerifications.Select(c => new { c.UserId }).Distinct();
                    ViewBag.Userid = new SelectList(Uservarification, "", "UserId");
                    return View();
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }

            }

        }

        public JsonResult GetSapCode()
        {
            var sapcode = db.UserVerifications.Where(c => c.Status == 2).Select(c => new { c.UserId, c.CreatedOn }).Distinct().OrderByDescending(c => c.CreatedOn).ToList();


            return Json(sapcode,
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult getUserid(string Id)
        {


            try
            {
                var getdeviceid = db.UserVerifications.Where(c => c.UserId == Id && c.Status == 2).Select(c => new { c.DeviceId, c.Id }).ToList();
                return Json(getdeviceid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }



        }
        public JsonResult getblockdevice(string Id)
        {

            try
            {
                var getdeviceid = db.UserVerification_Blocked.Where(c => c.UserId == Id && c.Status == 1).GroupBy(c => c.DeviceId).Select(c => c.FirstOrDefault()).ToList();
                return Json(getdeviceid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }



        }
        public ActionResult BlockUserPermission(string Id, string deviceid)
        {

            db.ExecuteStoreCommand("Update Userverification  set Status=0,Unbolckedby='" + Session["Id"] + "' where Userid='" + Id + "' and DeviceId='" + deviceid + "'");

            return Content("<script>alert('User Updated Successfully');</script>");

        }
        public ActionResult UserPermission(List<int> data)
        {
            try
            {
                UserVerification uv;
                UserVerification_Blocked uvb = new UserVerification_Blocked();
                var count = data.Count();
                int i = 0;

                foreach (var temp in data)
                {

                    var temp1 = db.UserVerifications.Where(x => x.Id == temp).ToList();

                    foreach (var temp2 in temp1)
                    {
                        uvb.U_verid = temp2.Id.ToString();
                        uvb.UserId = temp2.UserId;
                        uvb.Otp = temp2.Otp;
                        uvb.DeviceId = temp2.DeviceId;
                        uvb.CreatedOn = temp2.CreatedOn;
                        uvb.CreatedBy = temp2.CreatedBy;
                        uvb.OSType = temp2.OSType;
                        uvb.OSVersion = temp2.OSVersion;
                        uvb.AppVersion = temp2.AppVersion;
                        uvb.UnBolckedBy = temp2.UnBolckedBy;
                        uvb.UnauthorizedUser = temp2.UnauthorizedUser;
                        uvb.Status = 1;

                        db.UserVerification_Blocked.AddObject(uvb);
                        db.SaveChanges();

                        uv = db.UserVerifications.Single(x => x.Id == temp);
                        db.UserVerifications.DeleteObject(uv);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }



            // db.ExecuteStoreCommand("Update Userverification  set Status=1,Unbolckedby='" + Session["Id"] + "' where Id='" + id + "'");

            // Auditlog_Userverificationstatus aud_ver = new Auditlog_Userverificationstatus();

            //// UserVerification uv = db.UserVerifications.Single(a => a.Id == id);

            // aud_ver.Userverification_Id = uv.Id;
            // aud_ver.UserId = uv.UserId;
            // aud_ver.Otp = uv.Otp;
            // aud_ver.DeviceId = uv.DeviceId;
            // aud_ver.CreatedOn = uv.CreatedOn;
            // aud_ver.CreatedBy = uv.CreatedBy;
            // aud_ver.ModifyOn = DateTime.Now;
            // aud_ver.ModifyBy = Session["Id"].ToString();
            // aud_ver.OSType = uv.OSType;
            // aud_ver.AppVersion = uv.AppVersion;
            // aud_ver.OSVersion = uv.OSVersion;
            // aud_ver.UnBolckedBy = uv.UnBolckedBy;
            // aud_ver.UnauthorizedUser = uv.UnauthorizedUser;
            // aud_ver.Status = 1;

            // db.Auditlog_Userverificationstatus.AddObject(aud_ver);
            // db.SaveChanges();


            // return Content("<script>alert('User Updated Successfully');location.href='../../UserverificationPermission/Index'</script>");
            return View("Index");

        }
    }
}
