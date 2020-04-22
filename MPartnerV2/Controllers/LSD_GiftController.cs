using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data;
using Luminous.EF;
using System.IO;
using System.Web.UI;
namespace Luminous.Controllers
{
    public class LSD_GiftController : Controller
    {
        //
        // GET: /LSDMaster/
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();
        public ActionResult Index(string Search)
        {

            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                if (Search != null && Search != "")
                {
                    Session["Search"] = Search;

                }
                else
                {
                    Session.Remove("Search");
                }
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_Gift/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    return View(db.Lsd_GiftMaster.Where(a => a.GiftId == null).OrderByDescending(a => a.GiftId).ToList().ToPagedList(1, 1));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }


        public ActionResult Save(string giftname, string giftdesc, string Status, HttpPostedFileBase postedFile)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_Gift/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["createrole"].ToString() == "1")
                {
                    Status = Status ?? "off";

                    #region Check Validation
                    if (giftname == "" || giftname == null)
                    {
                        // ModelState.AddModelError("Marqueetext", "*");
                        ModelState.AddModelError("GiftName", "Gift Name Is Required");
                        // ViewBag.StartDate = "Marquee text cannot be empty";
                    }
                    if (giftdesc == "" || giftdesc == null)
                    {
                        // ModelState.AddModelError("Marqueetext", "*");
                        ModelState.AddModelError("GiftDesc", "Gift Description Is Required");
                        // ViewBag.StartDate = "Marquee text cannot be empty";
                    }
                    if (postedFile == null)
                    {
                        ModelState.AddModelError("File", "*");
                        ViewBag.File = "File Is Not Uploaded";
                    }
                    else
                    {
                        string FileExtension = Path.GetExtension(postedFile.FileName);
                        if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".png")
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("File", "*");
                            ViewBag.File = "File Extention Should Be In .Jpeg,.Png, .jpg";
                        }



                    }
                   

                    #endregion

                    if (ModelState.IsValid)
                    {
                        string filename = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                        string Imagename = filename.Replace(" ", string.Empty);
                        Lsd_GiftMaster giftmaster = new Lsd_GiftMaster();
                        giftmaster.GiftName = giftname;
                        giftmaster.GiftDesc = giftdesc;
                        giftmaster.GiftImage=Imagename;

                        if (Status.ToLower() == "on")
                        {
                            giftmaster.Status = 1;

                        }
                        else
                        {
                            giftmaster.Status = 0;
                        }
                        giftmaster.CreatedOn = DateTime.Now;
                        giftmaster.createdBy = Session["userid"].ToString();
                        db.Lsd_GiftMaster.AddObject(giftmaster);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            string str = Path.Combine(Server.MapPath("~/LSDImage/"), Imagename);
                            postedFile.SaveAs(str);

                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                    }
                    return View("Index", db.Lsd_GiftMaster.ToList().ToPagedList(1, 5));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }

        //LS Details//


        public JsonResult GetLSDGift(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_gift/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    var Lsddetails = (from c in db.Lsd_GiftMaster
                                      where  c.Status!=2

                                      select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }


                    if (Session["Search"] != null)
                    {
                        var Lsddetails2 = (from c in Lsddetails


                                           select new
                                           {
                                               GiftName = c.GiftName,
                                               Gitdescription = c.GiftDesc,

                                               status = c.Status == 1 ? "Enable" : "Disable",


                                               id = c.GiftId,


                                           }).Where(a => a.GiftName.Contains(Session["Search"].ToString())).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                        if (Lsddetails.Count() % 15 == 0)
                        {
                            totalrecord = Lsddetails.Count() / 15;
                        }
                        else
                        {
                            totalrecord = (Lsddetails.Count() / 15) + 1;
                        }
                        var data = new { result = Lsddetails2, TotalRecord = totalrecord };

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var Lsddetails2 = (from c in Lsddetails


                                           select new
                                           {
                                               GiftName = c.GiftName,
                                               Gitdescription = c.GiftDesc,

                                               status = c.Status == 1 ? "Enable" : "Disable",


                                               id = c.GiftId,


                                           }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                        if (Lsddetails.Count() % 15 == 0)
                        {
                            totalrecord = Lsddetails.Count() / 15;
                        }
                        else
                        {
                            totalrecord = (Lsddetails.Count() / 15) + 1;
                        }
                        var data = new { result = Lsddetails2, TotalRecord = totalrecord };

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }

        //Edit LSD details//

        public ActionResult Edit(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_Gift/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    try
                    {
                        Lsd_GiftMaster lsdgift = db.Lsd_GiftMaster.Single(a => a.GiftId == id);
                        ViewBag.giftname = lsdgift.GiftName;
                        ViewBag.giftdesc = lsdgift.GiftDesc;
                        ViewBag.previousimage = lsdgift.GiftImage;
                        ViewBag.preStatus = lsdgift.Status;
                        //ViewBag.bundlecode = lsdedit.BundleCode;
                       // ViewBag.giftid = lsdedit.GiftId;
                        ViewBag.Update = "";
                        return View(db.Lsd_GiftMaster.Single(a => a.GiftId == id));
                    }
                    catch
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        //Edit LSD Update//

        [HttpPost]
        public ActionResult Update(string GiftId, string giftname, string giftdesc, string Status, HttpPostedFileBase postedFile)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_gift/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {

                    int intid = int.Parse(GiftId);
                    Status = Status ?? "off";
                    #region Check Validation
                    if (giftname == "" || giftname == null)
                    {
                        // ModelState.AddModelError("Marqueetext", "*");
                        ModelState.AddModelError("GiftName", "Gift Name Is Required");
                        // ViewBag.StartDate = "Marquee text cannot be empty";
                    }
                    if (giftdesc == "" || giftdesc == null)
                    {
                        // ModelState.AddModelError("Marqueetext", "*");
                        ModelState.AddModelError("GiftDesc", "Gift Description Is Required");
                        // ViewBag.StartDate = "Marquee text cannot be empty";
                    }


                    string FileExtension = Path.GetExtension(postedFile.FileName);
                    if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" ||
                       FileExtension.ToLower() == ".png")
                    {

                    }
                    else
                    {
                        ModelState.AddModelError("File", "*");
                        ViewBag.File = "File Extention Should Be In .Jpeg,.Png, .jpg";
                    }
                   
                 
                   

                   
                    #endregion

                    if (ModelState.IsValid)
                    {
                        if (postedFile == null)
                        {
                            try
                            {


                                Lsd_GiftMaster gift = db.Lsd_GiftMaster.Single(a => a.GiftId == intid);

                                //Save Previous Record In History
                                Lsd_GiftMaster_History giftMHisotry = new Lsd_GiftMaster_History();
                                giftMHisotry.GiftName = gift.GiftName;
                                giftMHisotry.GiftImage = gift.GiftName;
                                giftMHisotry.GiftDesc = gift.GiftDesc;
                                giftMHisotry.CreatedOn = gift.CreatedOn;
                                giftMHisotry.GiftId_his = gift.GiftId;
                                giftMHisotry.createdBy = Session["userid"].ToString();
                                giftMHisotry.Status = gift.Status;

                                giftMHisotry.ModifiedBy = Session["userid"].ToString();
                                giftMHisotry.ModifiedOn = DateTime.Now;

                                db.Lsd_GiftMaster_History.AddObject(giftMHisotry);


                                //Save New Record

                                gift.GiftName = giftname;
                                gift.GiftDesc = giftdesc;



                                if (Status.ToLower() == "on")
                                {
                                    gift.Status = 1;

                                }
                                else
                                {
                                    gift.Status = 0;
                                }

                                gift.ModifiedBy = Session["userid"].ToString();
                                gift.ModifiedOn = DateTime.Now;
                                if (db.SaveChanges() > 0)
                                {
                                    ViewBag.Update = "Done";

                                }
                                else
                                {
                                    return Content("<script>alert('Record Has Not Been Saved');</script>");
                                }


                            }
                            catch
                            {
                                return View("Index");
                            }

                        }
                        else
                        {


                            if (ModelState.IsValid)
                            {
                                try
                                {

                                    string filename = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                                    string Imagename = filename.Replace(" ", string.Empty);
                                    Lsd_GiftMaster gift = db.Lsd_GiftMaster.Single(a => a.GiftId == intid && a.Status != 0);

                                    //Save Previous Record In History
                                    Lsd_GiftMaster_History giftMHisotry = new Lsd_GiftMaster_History();
                                    giftMHisotry.GiftName = gift.GiftName;
                                    giftMHisotry.GiftImage = gift.GiftName;
                                    giftMHisotry.GiftDesc = gift.GiftDesc;
                                    giftMHisotry.CreatedOn = gift.CreatedOn;
                                    giftMHisotry.GiftId_his = gift.GiftId;
                                    giftMHisotry.createdBy = Session["userid"].ToString();
                                    giftMHisotry.Status = gift.Status;

                                    giftMHisotry.ModifiedBy = Session["userid"].ToString();
                                    giftMHisotry.ModifiedOn = DateTime.Now;

                                    db.Lsd_GiftMaster_History.AddObject(giftMHisotry);


                                    //Save New Record
                                    gift.GiftImage = Imagename;
                                    gift.GiftName = giftname;
                                    gift.GiftDesc = giftdesc;



                                    if (Status.ToLower() == "on")
                                    {
                                        gift.Status = 1;

                                    }
                                    else
                                    {
                                        gift.Status = 0;
                                    }

                                    gift.ModifiedBy = Session["userid"].ToString();
                                    gift.ModifiedOn = DateTime.Now;
                                    if (db.SaveChanges() > 0)
                                    {
                                        ViewBag.Update = "Done";
                                        string str = Path.Combine(Server.MapPath("~/LSDImage/"), Imagename);
                                        postedFile.SaveAs(str);
                                    }
                                    else
                                    {
                                        return Content("<script>alert('Record Has Not Been Saved');</script>");
                                    }


                                }
                                catch
                                {
                                    return View("Index");
                                }
                            }
                        }
                    }




                    Lsd_GiftMaster gmaster = db.Lsd_GiftMaster.Single(a => a.GiftId == intid);
                    //ViewBag.preStartDate = Convert.ToDateTime(banner2.stardate).ToString("dd-MM-yyyy");
                    //ViewBag.PreEndDate = Convert.ToDateTime(banner2.ExpriyDate).ToString("dd-MM-yyyy");
                    ViewBag.preStatus = gmaster.Status;

                    return View("Edit", gmaster);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }

            //return RedirectToAction("Edit", new {id=id});

        }

        public JsonResult Delete(int id)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_Gift/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    try
                    {
                        Lsd_GiftMaster lm = db.Lsd_GiftMaster.Single(a => a.GiftId == id);

                        //Save Previous Record In History
                        Lsd_GiftMaster_History gMHisotry = new Lsd_GiftMaster_History();
                        gMHisotry.GiftName = lm.GiftName;
                        gMHisotry.GiftDesc = lm.GiftDesc;
                        gMHisotry.GiftImage = lm.GiftImage;
                       
                        gMHisotry.GiftId_his = lm.GiftId;

                        gMHisotry.Status = lm.Status;

                        gMHisotry.ModifiedBy = Session["userid"].ToString();
                        gMHisotry.ModifiedOn = DateTime.Now;

                        db.Lsd_GiftMaster_History.AddObject(gMHisotry);

                        //Delete Record From Table
                        lm.Status = 2;
                        lm.ModifiedOn = DateTime.Now;
                        lm.ModifiedBy = Session["userid"].ToString();
                        if (db.SaveChanges() > 0)
                        {
                            return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                        }

                        else
                        {
                            return Json("Record Not Deleted", JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch
                    {
                        return Json("Invalid Operation", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json("You Have No Delete Permission", JsonRequestBehavior.AllowGet);

                }
            }
        }

        public ActionResult ExportToExcel()
        {
           

            var DealerData = db.Lsd_GiftMaster.Where(c=>c.Status==1).ToList();
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = DealerData;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachement; filename=Lucky7GiftDump" + DateTime.Now + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }
    }
}
