using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI;
using LuminousMpartnerIB.EF;

namespace LuminousMpartnerIB.Controllers
{
    public class DealerContestController : Controller
    {
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();

        //
        // GET: /DealerContest/

        public ActionResult Index(string Error)
        {
            if (Error != null)
            {

                TempData["ERR"] = "No Data Available";
            }
            return View(db.InsertContexts.OrderByDescending(c => c.CreatedOn).ToList());
        }

        //
        // GET: /DealerContest/Details/5

        public ActionResult Details(int id = 0)
        {
            InsertContext insertcontext = db.InsertContexts.Single(i => i.Id == id);
            if (insertcontext == null)
            {
                return HttpNotFound();
            }
            return View(insertcontext);
        }

        //
        // GET: /DealerContest/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DealerContest/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InsertContext insertcontext)
        {
            if (ModelState.IsValid)
            {
                db.InsertContexts.AddObject(insertcontext);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insertcontext);
        }

        //
        // GET: /DealerContest/Edit/5

        public ActionResult Edit(string Type, string NOdata, int? id = 0)
        {
            //if (Contestid != 0)
            //{
            //    //var nextImage = db.InsertContexts.Where(i => i.Id > Contestid).OrderBy(i => i.Id).First();
            //    var nextImage = db.InsertContexts.Where(i => i.Id > Contestid).OrderBy(i => i.Id).First();
            //    id = nextImage.Id;
            //}
            if (NOdata != null)
            {
                //ModelState.AddModelError(string.Empty, "Access for this program already exists.");

                return RedirectToAction("Index", new { Error = "Error" });
            }
            
            InsertContext insertcontext = db.InsertContexts.Single(i => i.Id == id);
            if (insertcontext == null)
            {
                return HttpNotFound();
            }
            return View(insertcontext);
        }

        //
        // POST: /DealerContest/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InsertContext insertcontext)
        {
            if (ModelState.IsValid)
            {
                db.InsertContexts.Attach(insertcontext);
                db.ObjectStateManager.ChangeObjectState(insertcontext, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insertcontext);
        }

        //
        // GET: /DealerContest/Delete/5

        public ActionResult Delete(int id = 0)
        {
            InsertContext insertcontext = db.InsertContexts.Single(i => i.Id == id);
            if (insertcontext == null)
            {
                return HttpNotFound();
            }
            return View(insertcontext);
        }

        //
        // POST: /DealerContest/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InsertContext insertcontext = db.InsertContexts.Single(i => i.Id == id);
            db.InsertContexts.DeleteObject(insertcontext);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public string Update(int Id,string ImageEncode, string Rating, string DealerName, string DealerCity, string DealerPhone, string DealerEmail, string DistributorCode, string EmpCode, string DealerId, string CreatedBy, string CreatedOn)
        {
            try
            {
                if (Id != 0 && Rating != null)
                {
                    //db.ExecuteStoreCommand("Update InsertContext set Rating='" + Rating + "' where Id='" + Id + "'");
                    //db.ExecuteStoreCommand("Insert into Insertcontexthistory(ImageEncode,Rating,DealerName,DealerCity,DealerPhone,DealerEmail,DistributorCode,EmpCode,DealerId,CreatedBy,CreatedOn) values('" + ImageEncode + "','" + Rating + "','" + DealerName + "','" + DealerCity + "','" + DealerPhone + "','" + DealerEmail + "','" + DistributorCode + "','" + EmpCode + "','" + DealerId + "','" + CreatedBy + "','" + CreatedOn + "')");
                    //db.SaveChanges();
                    String RatingBy = Session["Id"].ToString();
                    db.UpdateInsertContextRating(Id, Rating, ImageEncode, DealerName, DealerCity, DealerPhone, DealerEmail, DistributorCode, EmpCode, DealerId, CreatedBy, RatingBy);
                    return "Point Allocation Update Successfully";
                }
                else
                {
                    return "Point Allocation Not Updated";
                }
            }
            catch(Exception exc)
            {
                return "Some exception has occurred";
            }

            
        }
        public ActionResult GetDealerData()
        {
            //var DealerId = db.InsertContexts.Where(r=>r.Rating==null).Min(r =>r.Id);
          
         
            //return RedirectToAction("Edit", new {id=DealerId});
            var DelID = db.InsertContexts.Where(r => r.Rating == null || r.Rating == "").ToList();
            if (DelID.Count == 0)
            {
                return RedirectToAction("Edit", new { NOdata = "Nodata" });
            }
            else
            {
                var DealerId = DelID.Min(r => r.Id);
                return RedirectToAction("Edit", new { id = DealerId });
            }
        }
        public string GetNextImage(int Contestid, string ImageEncode, string Rating, string DealerName, string DealerCity, string DealerPhone, string DealerEmail, string DistributorCode, string EmpCode, string DealerId, string CreatedBy, string CreatedOn)
        {
            try
            {
                //db.ExecuteStoreCommand("Update InsertContext set Rating='" + Rating + "' where Id='" + Contestid + "'");
                //db.ExecuteStoreCommand("Insert into Insertcontexthistory(ImageEncode,Rating,DealerName,DealerCity,DealerPhone,DealerEmail,DistributorCode,EmpCode,DealerId,CreatedBy,CreatedOn) values('" + ImageEncode + "','" + Rating + "','" + DealerName + "','" + DealerCity + "','" + DealerPhone + "','" + DealerEmail + "','" + DistributorCode + "','" + EmpCode + "','" + DealerId + "','" + CreatedBy + "','" + CreatedOn + "')");
                //db.SaveChanges();
                String RatingBy = Session["Id"].ToString();
                db.UpdateInsertContextRating(Contestid, Rating, ImageEncode, DealerName, DealerCity, DealerPhone, DealerEmail, DistributorCode, EmpCode, DealerId, CreatedBy, RatingBy);
                var nextImage = db.InsertContexts.Where(i => i.Id > Contestid && i.Rating == null || i.Rating == "").OrderBy(i => i.Id).First();
                var imgnext = nextImage.Id.ToString();
                //return RedirectToAction("Edit", new { id = imgnext });
                return imgnext;
            }
            catch(Exception exc)
            {
                return "No Data";
            }
        }
        public ActionResult ExportToExcel()
        {
            var DealerData = db.InsertContexts.OrderBy(c => c.CreatedOn).ToList();
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = DealerData;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachement; filename=dealer" + DateTime.Now + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}