using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Data.Entity;

namespace Luminous.Controllers
{
    public class PermotionsController : Controller
    {
        //
        // GET: /Permotions/
        private LuminousEntities db = new LuminousEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveContact(PermotionType contactUs, string statusC)
        {

            if (contactUs.PType != null)
            {
                if (contactUs.PType.Length > 99)
                {
                    ModelState.AddModelError("PType", "Permotion Type Is Empty");
                }
            }
            else {
                ModelState.AddModelError("PType", "Permotion Type Is Empty");
            }

            if (db.PermotionTypes.Any(a => a.PType.ToLower() == contactUs.PType.ToLower() && a.PlTwStatus != 2))
            {
                ModelState.AddModelError("PType", "Product Type Is Already Exists");
            }

            if (ModelState.IsValid)
            {
                PermotionType contactusd = new PermotionType();
                contactusd.PType = contactUs.PType;              
                contactusd.CreatedDate = DateTime.Now;
                contactusd.CreatedBy = Session["userid"].ToString();
                string status = statusC ?? "off";
                if (status == "on")
                {
                    contactusd.PlTwStatus = 1;
                }
                else
                {
                    contactusd.PlTwStatus = 0;
                }

                db.PermotionTypes.AddObject(contactusd);
                db.SaveChanges();


            }
            return View("Index");
        }

        public JsonResult GetContactDetail(int? page)
        {
            var contactdetails = (from c in db.PermotionTypes
                                  where c.PlTwStatus != 2
                                  select c).ToList();
            int totalrecord;
            if (page != null)
            {
                page = (page - 1) * 15;
            }

            var contactDetails2 = (from c in contactdetails

                                   select new
                                   {
                                       
                                       PName = c.PType,
                                       status = c.PlTwStatus== 1 ? "Enable" : "Disable",
                                       id = c.id

                                   }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(5).ToList();
            if (contactdetails.Count() % 15 == 0)
            {
                totalrecord = contactdetails.Count() / 15;
            }
            else
            {
                totalrecord = (contactdetails.Count() / 15) + 1;
            }
            var data = new { result = contactDetails2, TotalRecord = totalrecord };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteContact(int id)
        {
            PermotionType contactUs = db.PermotionTypes.Single(a => a.id == id);
            PermotionTypeHistory CUDHistory = new PermotionTypeHistory();
            CUDHistory.PerTypeId = contactUs.id;
            CUDHistory.PType = contactUs.PType;
            CUDHistory.PlTwStatus = contactUs.PlTwStatus;
            CUDHistory.ModifiedBy = Session["userid"].ToString();
            CUDHistory.ModifiedDate = DateTime.Now;
            contactUs.PlTwStatus = 2;
            contactUs.ModifiedBy = Session["userid"].ToString();
            contactUs.ModifiedDate = DateTime.Now;
            db.PermotionTypeHistories.AddObject(CUDHistory);
            db.SaveChanges();
            return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PermotionType cud = db.PermotionTypes.Single(a => a.id == id);
            ViewBag.status = cud.PlTwStatus;
            return View(cud);
        }

        public ActionResult Update(PermotionType contactUs, string statusC)
        {

            if (contactUs.PType != null)
            {
                if (contactUs.PType.Length > 99)
                {
                    ModelState.AddModelError("PType", "Permotion Type Is Empty");
                }
            }
            else
            {
                ModelState.AddModelError("PType", "Permotion Type Is Empty");
            }

            if (db.PermotionTypes.Any(a => a.PType.ToLower() == contactUs.PType.ToLower() && a.PlTwStatus != 2 && a.id != contactUs.id ))
            {
                ModelState.AddModelError("PType", "Product Name Already Exists");
            }
            if (ModelState.IsValid)
            {
                PermotionType ptype = db.PermotionTypes.Single(a => a.id == contactUs.id);
                PermotionTypeHistory CUDHistory = new PermotionTypeHistory();
                CUDHistory.PerTypeId = ptype.id;
                CUDHistory.PType = ptype.PType;
                CUDHistory.PlTwStatus = ptype.PlTwStatus;
                CUDHistory.ModifiedBy = Session["userid"].ToString();
                CUDHistory.ModifiedDate = DateTime.Now;
                ptype.ModifiedBy = Session["userid"].ToString();
                ptype.ModifiedDate = DateTime.Now;
                ptype.PType = contactUs.PType;
                string status = statusC ?? "off";
                if (status == "on")
                {
                    ptype.PlTwStatus = 1;
                }
                else
                {
                    ptype.PlTwStatus = 0;
                }
                db.PermotionTypeHistories.AddObject(CUDHistory);
                db.SaveChanges();
                ViewBag.Result = "Record Updated Successfully";
            }
            return View("Edit");
        }
    }
}
