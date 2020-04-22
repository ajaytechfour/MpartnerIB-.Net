using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.IO;
using System.Web.Routing;
using System.Data;
namespace LuminousMpartnerIB.Controllers
{
    public class ProductImagesCarousalController : Controller
    {
        //
        // GET: /ProductImagesCarousal/

        // GET: /ProductImages/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/ProductLevel3/index";

        public ActionResult Index(int id)
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
                    ProductthreeImageMapping pi = new ProductthreeImageMapping();
                    List<ProductthreeImageMapping> pis = db.ProductthreeImageMappings.Where(image => image.ProductLevelThreeid == id ).ToList();
                    return View(pis);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        [HttpGet]
        public ActionResult EditImage(int id)
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
                if (result[0]["editrole"].ToString() == "1")
                {
                    ProductthreeImageMapping pi = db.ProductthreeImageMappings.Single(a => a.ProductID == id);
                    return View(pi);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }


        [HttpPost]
        public ActionResult UpdateImage(ProductthreeImageMapping pi, HttpPostedFileBase file)
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
                if (result[0]["editrole"].ToString() == "1")
                {
                    if (file == null)
                    {
                        ModelState.AddModelError("PrImage", "File Is Not Uploaded");
                        ViewBag.File = "File Is Not Uploaded";
                    }
                    else
                    {
                        string FileExtension = Path.GetExtension(file.FileName);
                        if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                           FileExtension.ToLower() == ".png")
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("PrImage", "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg");
                            ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                        }



                    }
                    if (ModelState.IsValid)
                    {
                        ProductthreeImageMapping pim = db.ProductthreeImageMappings.Single(a => a.ProductID == pi.ProductID);
                        int ProductLevelThreeId = pim.ProductLevelThreeid ?? 0;
                        //#region Insertion In Product Image History Table
                        //ProductImagesHistory pih = new ProductImagesHistory();
                        //pih.ProductImageId = pim.id;
                        //pih.PrImage = pim.PrImage;
                        //pih.pc_Lv3_oneId = pim.pc_Lv3_oneId;
                        //pih.PlTwStatus = pim.PlTwStatus;
                        //db.ProductImagesHistories.AddObject(pih);
                        //#endregion
                        #region Updation In Product Level Three
                        ProductLevelThree PLT = db.ProductLevelThrees.Single(a => a.id == ProductLevelThreeId);
                        PLT.ModifiedBy = Session["userid"].ToString();
                        PLT.ModifiedDate = DateTime.Now;
                        #endregion



                        #region Save Change For All
                        db.SaveChanges();
                        #endregion



                        string filename = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(file.FileName);
                        string Primageremovespace = filename.Replace(" ", string.Empty);
                        pim.Primage = Primageremovespace;
                        db.SaveChanges();
                        string str = Path.Combine(Server.MapPath("~/ProductImages/"), Primageremovespace);
                        file.SaveAs(str);

                        return RedirectToAction("Index/" + pi.ProductLevelThreeid);
                    }
                    return View("EditImage");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }

        }
        [HttpPost]
        public JsonResult DeleteImage(int id)
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
                if (result[0]["deleterole"].ToString() == "1")
                {
                    ProductthreeImageMapping pim = db.ProductthreeImageMappings.Single(a => a.ProductID == id);

                    //#region Insertion In Product Image History Table
                    //ProductImagesHistory pih = new ProductImagesHistory();
                    //pih.ProductImageId = pim.id;
                    //pih.PrImage = pim.PrImage;
                    //pih.pc_Lv3_oneId = pim.pc_Lv3_oneId;
                    //pih.PlTwStatus = pim.PlTwStatus;
                    //pih.ModifiedBy = Session["userid"].ToString();
                    //pih.ModifiedDate = DateTime.Now;
                    //db.ProductImagesHistories.AddObject(pih);
                    //#endregion

                    //pim.PlTwStatus = 2;
                    //pim.ModifiedBy = Session["userid"].ToString();
                    //pim.ModifiedDate = DateTime.Now;
                    int ProductLevelThreeId = pim.ProductLevelThreeid ?? 0;

                    #region Updation In Product Level Three
                    ProductLevelThree PLT = db.ProductLevelThrees.Single(a => a.id == ProductLevelThreeId);
                    PLT.ModifiedBy = Session["userid"].ToString();
                    PLT.ModifiedDate = DateTime.Now;
                    #endregion
                    db.ProductthreeImageMappings.DeleteObject(pim);
                   // db.SaveChanges();
                    db.SaveChanges();
                    int? i = pim.ProductLevelThreeid;
                    return Json(i, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }
    }
}
