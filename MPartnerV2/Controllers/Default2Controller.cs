using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;

namespace Luminous.Controllers
{
    public class Default2Controller : Controller
    {
        private LuminousEntities db = new LuminousEntities();

        //
        // GET: /Default2/

        public ActionResult Index()
        {
            return View(db.Banners.ToList());
        }

        //
        // GET: /Default2/Details/5

        public ActionResult Details(int id = 0)
        {
            Banner banner = db.Banners.Single(b => b.id == id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        //
        // GET: /Default2/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default2/Create

        [HttpPost]
        public ActionResult Create(Banner banner)
        {
            if (ModelState.IsValid)
            {
                db.Banners.AddObject(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        //
        // GET: /Default2/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Banner banner = db.Banners.Single(b => b.id == id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        //
        // POST: /Default2/Edit/5

        [HttpPost]
        public ActionResult Edit(Banner banner)
        {
            if (ModelState.IsValid)
            {
                db.Banners.Attach(banner);
                db.ObjectStateManager.ChangeObjectState(banner, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        //
        // GET: /Default2/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Banner banner = db.Banners.Single(b => b.id == id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        //
        // POST: /Default2/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Banner banner = db.Banners.Single(b => b.id == id);
            db.Banners.DeleteObject(banner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}