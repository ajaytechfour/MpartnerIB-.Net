using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data;
using LuminousMpartnerIB.EF;
namespace LuminousMpartnerIB.Controllers
{
    public class MarqueeContestController : Controller
    {
        //
        // GET: /MarqueeContest/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();

        public ActionResult Index()
        {

            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/MarqueeContest/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    return View(db.ShowMarquee_Text.Where(a => a.Id == null).OrderByDescending(a => a.Id).ToList().ToPagedList(1, 1));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        //Save Marquee data

        public ActionResult Save(string Marqueetext, string Status, string StartDate, string EndDate)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/MarqueeContest/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["createrole"].ToString() == "1")
                {

                    Status = Status ?? "off";
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;
                    #region Check Validation
                    if (Marqueetext == "" || Marqueetext == null)
                    {
                        // ModelState.AddModelError("Marqueetext", "*");
                        ModelState.AddModelError("Marqueetext","Descriptions Is Required");
                        // ViewBag.StartDate = "Marquee text cannot be empty";
                    }
                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "*");
                        ViewBag.StartDate = "Start Date Is Not Selected";
                    }
                    else
                    {
                        try
                        {

                            if (Convert.ToDateTime(StartDate) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                            {
                                ModelState.AddModelError("StartDate", "Start Date Should Be Greater Than or Equal To Current Date");
                                ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                            }

                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("StartDate", "Invalid Start Date");
                            ViewBag.StartDate = "Invalid Start Date";
                        }

                    }
                    if (EndDate == null || EndDate == "")
                    {
                        ModelState.AddModelError("ExpriyDate", "*");
                        ViewBag.EndDate = "End Date Is Not Selected";
                    }
                    else
                    {
                        DateTime startDate = new DateTime();
                        try
                        {
                            startDate = Convert.ToDateTime(StartDate);
                            try
                            {
                                if (Convert.ToDateTime(EndDate) < startDate)
                                {
                                    ModelState.AddModelError("ExpriyDate", "*");
                                    ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                                }
                            }
                            catch (FormatException ex)
                            {
                                ModelState.AddModelError("ExpriyDate", "Invalid End Date");
                                ViewBag.EndDate = "Invalid End Date";
                            }
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("ExpriyDate", "Invalid End Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }

                    }
                    #endregion

                    if (ModelState.IsValid)
                    {
                        var marqueecount = db.ShowMarquee_Text.Count();
                        ShowMarquee_Text mrqtext = new ShowMarquee_Text();
                        mrqtext.Marqueetext = Marqueetext;


                        mrqtext.CreatedOn = DateTime.Now;
                        mrqtext.CreatedBy = Session["userid"].ToString();
                        mrqtext.MarqueeFlag = 1;
                        mrqtext.ContestId = 2;
                        mrqtext.Statrtdate = Convert.ToDateTime(StartDate);
                        mrqtext.Enddate = Convert.ToDateTime(EndDate);
                        if (Status.ToLower() == "on")
                        {
                            mrqtext.Status = 1;

                        }
                        else
                        {
                            mrqtext.Status = 0;
                        }
                        db.ShowMarquee_Text.AddObject(mrqtext);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {

                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                    }
                    return View("Index", db.ShowMarquee_Text.ToList().ToPagedList(1, 5));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }


        //Get Marquee data

        public JsonResult GetMarqueeDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/MarqueeContest/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    var Marqueedetails = (from c in db.ShowMarquee_Text
                                          where c.Status != 2

                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var Marqueedetails2 = (from c in Marqueedetails
                                           select new
                                           {
                                               MarqueeText = c.Marqueetext,
                                               startdate = Convert.ToDateTime(c.Statrtdate).ToShortDateString(),
                                               enddate = Convert.ToDateTime(c.Enddate).ToShortDateString(),
                                               status = c.Status == 1 ? "Enable" : "Disable",


                                               id = c.Id,


                                           }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                    if (Marqueedetails.Count() % 15 == 0)
                    {
                        totalrecord = Marqueedetails.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (Marqueedetails.Count() / 15) + 1;
                    }
                    var data = new { result = Marqueedetails2, TotalRecord = totalrecord };

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
                string pageUrl2 = "/MarqueeContest/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    try
                    {
                        ShowMarquee_Text mrqtext = db.ShowMarquee_Text.Single(a => a.Id == id && a.Status != 2);
                        ViewBag.preStartDate = Convert.ToDateTime(mrqtext.Statrtdate).ToString("dd-MM-yyyy");
                        ViewBag.PreEndDate = Convert.ToDateTime(mrqtext.Enddate).ToString("dd-MM-yyyy");
                        ViewBag.preStatus = mrqtext.Status;
                        ViewBag.mrqtext = mrqtext.Marqueetext;
                        ViewBag.Update = "";
                        return View(db.ShowMarquee_Text.Single(a => a.Id == id));
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
        public ActionResult Update(string Id, string Marqueetext, string Status, string StartDate, string EndDate)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/MarqueeContest/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {


                    #region Check Validation
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;
                    int intid = int.Parse(Id);
                    Status = Status ?? "off";

                    if (Marqueetext == null)
                    {
                        ModelState.AddModelError("Marqueetext", "Marquee Text Is Required");
                    }
                    else
                    {
                        if (Marqueetext == "")
                        {
                            ModelState.AddModelError("Marqueetext", "Marquee Text Is Required");
                        }
                        //else if (text.Length > 49)
                        //{
                        //    ModelState.AddModelError("Header_Details", "Characters In Banner Header Should Be Less Than 50");
                        //}

                    }

                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "*");
                        ViewBag.StartDate = "Start Date Is Not Selected";
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToDateTime(StartDate) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                            {
                                //ModelState.AddModelError("StartDate", "*");
                                //ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                            }
                            //DateTime startDate = Convert.ToDateTime(StartDate);
                            //if (db.Banners.Any(a => a.ExpriyDate >= startDate && a.id != intid && a.BStatus != 2 ))
                            //{
                            //    ModelState.AddModelError("StartDate", "*");
                            //    ViewBag.StartDate = "There Is already A Banner Defined In This Date";
                            //}
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("StartDate", "*");

                            ViewBag.StartDate = "Invalid Date";
                        }
                    }
                    if (EndDate == null || EndDate == "")
                    {
                        ModelState.AddModelError("ExpriyDate", "*");
                        ViewBag.EndDate = "End Date Is Not Selected";
                    }
                    else
                    {
                        DateTime startDate = new DateTime();
                        try
                        {
                            startDate = Convert.ToDateTime(StartDate);
                            try
                            {
                                if (Convert.ToDateTime(EndDate) < startDate)
                                {
                                    ModelState.AddModelError("ExpriyDate", "*");
                                    ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                                }
                            }
                            catch (FormatException ex)
                            {
                                ModelState.AddModelError("ExpriyDate", "Invalid End Date");
                                ViewBag.EndDate = "Invalid End Date";
                            }
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("ExpriyDate", "Invalid End Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }
                    }

                    #endregion

                    if (ModelState.IsValid)
                    {
                        try
                        {

                            ShowMarquee_Text mrqtext = db.ShowMarquee_Text.Single(a => a.Id == intid && a.Status != 2);

                            //Save Previous Record In History
                            ShowMarquee_Text_History mrqtextHisotry = new ShowMarquee_Text_History();
                            mrqtextHisotry.MarqueeID = mrqtext.Id;
                            mrqtextHisotry.Marqueetext = mrqtext.Marqueetext;
                            mrqtextHisotry.Statrtdate = mrqtext.Statrtdate;
                            mrqtextHisotry.Enddate = mrqtext.Enddate;
                            mrqtextHisotry.Status = mrqtext.Status;

                            mrqtextHisotry.ModifiedBy = Session["userid"].ToString();
                            mrqtextHisotry.ModifiedOn = DateTime.Now;

                            db.ShowMarquee_Text_History.AddObject(mrqtextHisotry);


                            //Save New Record
                            mrqtext.Marqueetext = Marqueetext;
                            mrqtext.Statrtdate = Convert.ToDateTime(StartDate);
                            mrqtext.Enddate = Convert.ToDateTime(EndDate);
                            mrqtext.ModifiedBy = Session["userid"].ToString();
                            mrqtext.ModifiedOn = DateTime.Now;
                            mrqtext.MarqueeFlag = 1;
                            mrqtext.ContestId = 2;
                            if (Status.ToLower() == "on")
                            {
                                mrqtext.Status = 1;
                            }
                            else
                            {
                                mrqtext.Status = 0;
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





                    ShowMarquee_Text mqtext = db.ShowMarquee_Text.Single(a => a.Id == intid);
                    //ViewBag.preStartDate = Convert.ToDateTime(banner2.stardate).ToString("dd-MM-yyyy");
                    //ViewBag.PreEndDate = Convert.ToDateTime(banner2.ExpriyDate).ToString("dd-MM-yyyy");
                    ViewBag.preStatus = mqtext.Status;

                    return View("Edit", mqtext);
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
                string pageUrl2 = "/MarqueeContest/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    try
                    {
                        ShowMarquee_Text mrqtext = db.ShowMarquee_Text.Single(a => a.Id == id);
                        //Save Previous Record In History
                        ShowMarquee_Text_History mrqtextHisotry = new ShowMarquee_Text_History();
                        mrqtextHisotry.MarqueeID = mrqtext.Id;
                        mrqtextHisotry.Marqueetext = mrqtext.Marqueetext;

                        mrqtextHisotry.Status = mrqtext.Status;
                        mrqtextHisotry.Statrtdate = mrqtext.Statrtdate;
                        mrqtextHisotry.Enddate = mrqtext.Enddate;
                        mrqtextHisotry.ModifiedBy = Session["userid"].ToString();
                        mrqtextHisotry.ModifiedOn = DateTime.Now;

                        db.ShowMarquee_Text_History.AddObject(mrqtextHisotry);

                        //Delete Record From Table
                        mrqtext.Status = 2;
                        mrqtext.ModifiedOn = DateTime.Now;
                        mrqtext.ModifiedBy = Session["userid"].ToString();
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
