using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.IO;
using PagedList;
using PagedList.Mvc;
using System.Data;
using System.Web.Routing;
namespace Luminous.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Media/
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/Media/Index";
        public ActionResult Index()
        {
            RouteCollection rc = RouteTable.Routes;
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Banner/Index";
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
        public JsonResult GetCategory()
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
                if (result[0]["uview"].ToString() == "1")
                {
                    var Company = (from c in db.FooterCategories
                                   where c.CatType == "Media"
                                   select new
                                   {
                                       id = c.Id,
                                       Name = c.FCategoryName
                                   }).ToList();
                    return Json(Company, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }

        public ActionResult Save(string VideoName, string Url, string Status, string ParentCatid)
        {


            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Media/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["createrole"].ToString() == "1")
                {
                    if (VideoName == null || VideoName == "")
                    {
                        ModelState.AddModelError("VideoName", "Description Cannot be empty");
                        ViewBag.videoname = "Description Cannot Be Empty";
                    }
                    if (Url == null || Url == "")
                    {
                        ModelState.AddModelError("Url", "URL Cannot be empty");
                        ViewBag.videoname = "Description Cannot Be Empty";
                    }
                    if (ParentCatid == null || ParentCatid == "0")
                    {
                        ModelState.AddModelError("LabelId", "Select Category");
                        ViewBag.videoname = "Description Cannot Be Empty";
                    }
                    Status = Status ?? "off";


                    if (ModelState.IsValid)
                    {

                        MediaData Media = new MediaData();
                        Media.VideoName = VideoName;
                        Media.Url = Url;
                        Media.LabelId = Convert.ToInt32(ParentCatid);
                        Media.CreatedOn = DateTime.Now;
                        Media.CreatedBy = Convert.ToInt32(Session["Id"]);
                        Media.PageFlag = "Gallery";
                        if (Status.ToLower() == "on")
                        {
                            Media.Status = 1;

                        }
                        else
                        {
                            Media.Status = 0;
                        }
                        db.MediaDatas.AddObject(Media);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {

                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                    }
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }


        public JsonResult GetMediaDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Media/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    var Mediadetails = (from c in db.MediaDatas
                                        where c.Status != 2
                                        select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }


                    var MediaDetail2 = (from c in Mediadetails
                                        join sw in db.FooterCategories on c.LabelId equals sw.Id
                                        select new
                                        {
                                            Id = c.Id,
                                            Category = sw.FCategoryName,
                                            Description = c.VideoName,
                                            URL = c.Url,
                                            status = c.Status == 1 ? "Enable" : "Disable"
                                        }).OrderByDescending(a => a.Id).Skip(page ?? 0).Take(15).ToList();
                    if (Mediadetails.Count() % 15 == 0)
                    {
                        totalrecord = Mediadetails.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (Mediadetails.Count() / 15) + 1;
                    }
                    var data = new { result = MediaDetail2, TotalRecord = totalrecord };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }
        public ActionResult Edit(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Media/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    try
                    {
                        MediaData media = db.MediaDatas.Single(a => a.Id == id && a.Status != 2);

                        ViewBag.preStatus = media.Status;
                        ViewBag.Prntid = media.LabelId;
                        ViewBag.Update = "";
                        return View(db.MediaDatas.Single(a => a.Id == id));
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
        [HttpPost]
        public ActionResult Update(string id, string VideoName, string Url, string Status, string ParentCatid)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Media/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    if (VideoName == null || VideoName == "")
                    {
                        ModelState.AddModelError("VideoName", "Description Cannot be empty");
                        ViewBag.videoname = "Description Cannot Be Empty";
                    }
                    if (Url == null || Url == "")
                    {
                        ModelState.AddModelError("Url", "URL Cannot be empty");
                        ViewBag.videoname = "Description Cannot Be Empty";
                    }
                    if (ParentCatid == null || ParentCatid == "0")
                    {
                        ModelState.AddModelError("LabelId", "Select Category");
                        ViewBag.videoname = "Description Cannot Be Empty";
                    }

                    int intid = int.Parse(id);
                    Status = Status ?? "off";





                    if (ModelState.IsValid)
                    {
                        try
                        {

                            MediaData media = db.MediaDatas.Single(a => a.Id == intid && a.Status != 2);

                            //Save Previous Record In History
                            MediaDataHistory mediaHisotry = new MediaDataHistory();
                            mediaHisotry.VideoName = media.VideoName;
                            mediaHisotry.Url = media.Url;
                            mediaHisotry.Status = media.Status;
                            mediaHisotry.MediaDataId = media.Id;
                            mediaHisotry.LabelId = Convert.ToInt32(ParentCatid);
                            mediaHisotry.ModifyBy = Convert.ToInt32(Session["Id"]);
                            mediaHisotry.ModifyOn = DateTime.Now;

                            db.MediaDataHistories.AddObject(mediaHisotry);


                            //Save New Record
                            media.VideoName = VideoName;
                            media.Url = Url;

                            media.LabelId = Convert.ToInt32(ParentCatid);
                            media.ModifyBy = Convert.ToInt32(Session["Id"]);
                            media.ModifyOn = DateTime.Now;
                            if (Status.ToLower() == "on")
                            {
                                media.Status = 1;

                            }
                            else
                            {
                                media.Status = 0;
                            }

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





                    MediaData banner2 = db.MediaDatas.Single(a => a.Id == intid);
                    //ViewBag.preStartDate = Convert.ToDateTime(banner2.stardate).ToString("dd-MM-yyyy");
                    //ViewBag.PreEndDate = Convert.ToDateTime(banner2.ExpriyDate).ToString("dd-MM-yyyy");
                    ViewBag.preStatus = banner2.Status;

                    return View("Edit", banner2);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
            //return RedirectToAction("Edit", new {id=id});

        }

        //public ActionResult Update()
        //{
        //    return RedirectToAction("BannerList");
        //}
        public JsonResult Delete(int id)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Banner/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    try
                    {
                        MediaData media = db.MediaDatas.Single(a => a.Id == id);
                        //Save Previous Record In History
                        MediaDataHistory MediaHisotry = new MediaDataHistory();
                        MediaHisotry.VideoName = media.VideoName;
                        MediaHisotry.Url = media.Url;
                        MediaHisotry.LabelId = Convert.ToInt32(media.LabelId);
                        MediaHisotry.ModifyBy = Convert.ToInt32(Session["Id"]);
                        MediaHisotry.ModifyOn = DateTime.Now;

                        db.MediaDataHistories.AddObject(MediaHisotry);

                        //Delete Record From Table
                        media.Status = 2;
                        media.ModifyOn = DateTime.Now;
                        media.ModifyBy = Convert.ToInt32(Session["Id"]);
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

    }
}
