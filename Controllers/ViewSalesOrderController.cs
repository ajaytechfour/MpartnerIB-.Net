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
    public class ViewSalesOrderController : Controller
    {
        //
        // GET: /ContactUs/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/FAQ/Index";
        string utype = string.Empty;
        public ActionResult Index()
        {
           
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                utype = Session["ctype"].ToString();
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = "/FAQ/Index";
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                var data = db.FAQs.OrderByDescending(a => a.Id).ToList().ToPagedList(1, 1);
                if (utype == "Luminous")
                {
                    return RedirectToAction("Index", "FAQView");
                }
                else
                {
                    return View(data);
                    //return RedirectToAction("snotallowed", "snotallowed");
                }
            }

        }
        public ActionResult SaveContact(string QuestionName, string Answer, string Status, string StartDate, string EndDate)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = "/FAQ/Index";
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    Status = Status ?? "off";
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = EndDate;

                    #region Check Validation

                    if (QuestionName == null || QuestionName == "")
                    {
                        ModelState.AddModelError("QuestionName", "Question is required");
                    }
                    if (Answer == null || Answer == "")
                    {
                        ModelState.AddModelError("Answer", "Answer is required");
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
                        ModelState.AddModelError("Enddate", "*");
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
                                    ModelState.AddModelError("Enddate", "*");
                                    ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                                }
                            }
                            catch (FormatException ex)
                            {
                                ModelState.AddModelError("Enddate", "Invalid End Date");
                                ViewBag.EndDate = "Invalid End Date";
                            }
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("Enddate", "Invalid End Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }

                    }
                    #endregion
                    if (ModelState.IsValid)
                    {
                        FAQ obj_faq = new FAQ();
                        obj_faq.QuestionName = QuestionName;
                        obj_faq.Answer = Answer;
                        obj_faq.CreatedOn = DateTime.Now;
                        obj_faq.CreatedBy = Session["userid"].ToString();
                        string status = Status ?? "off";
                        if (status == "on")
                        {
                            obj_faq.Status = 1;
                        }
                        else
                        {
                            obj_faq.Status = 0;
                        }
                        obj_faq.StartDate = Convert.ToDateTime(StartDate);
                        obj_faq.EndDate = Convert.ToDateTime(EndDate);

                        db.FAQs.Add(obj_faq);
                        db.SaveChanges();
                        return Content("<script>alert('Data Successfully Submitted');location.href='../FAQ/Index';</script>");

                    }
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }

        public JsonResult GetFAQDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    var contactdetails = (from c in db.FAQs
                                          where c.Status != 2
                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var contactDetails2 = (from c in contactdetails
                                           select new
                                           {
                                               quesname = c.QuestionName,
                                               answer = c.Answer,
                                               status = c.Status == 1 ? "Enable" : "Disable",
                                               startdate = Convert.ToDateTime(c.StartDate).ToShortDateString(),
                                               enddate = Convert.ToDateTime(c.EndDate).ToShortDateString(),



                                               id = c.Id,
                                           }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
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
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }

        public void EditContact(int id)
        {

        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    FAQ faq = db.FAQs.Single(a => a.Id == id);
                    //Save Previous Data In History Table
                    FAQ_History CUDHistory = new FAQ_History();
                    CUDHistory.FAQ_ParentId = faq.Id;
                    CUDHistory.QuestionName = faq.QuestionName;
                    CUDHistory.Answer = faq.Answer;
                    CUDHistory.Status = 2;
                    CUDHistory.StartDate = faq.StartDate;
                    CUDHistory.EndDate = faq.EndDate;

                    CUDHistory.ModifiedBy = Session["userid"].ToString();
                    CUDHistory.ModifiedOn = DateTime.Now;
                    db.FAQ_History.Add(CUDHistory);

                    //Update Data With New Value
                    faq.Status = 2;
                    faq.ModifiedBy = Session["userid"].ToString();
                    faq.ModifiedOn = DateTime.Now;


                    int affectedReocrds = db.SaveChanges();
                    if (affectedReocrds > 0)
                    {
                        return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Record Not Deleted", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    FAQ cud = db.FAQs.Single(a => a.Id == id);
                    ViewBag.status = cud.Status;
                    ViewBag.preStartDate = Convert.ToDateTime(cud.StartDate).ToString("dd-MM-yyyy");
                    ViewBag.PreEndDate = Convert.ToDateTime(cud.EndDate).ToString("dd-MM-yyyy");
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult Update(int Id, string QuestionName, string Answer, string Status, string StartDate, string EndDate)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //Status = Status ?? "off";
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {


                    if (QuestionName == null || QuestionName == "")
                    {
                        ModelState.AddModelError("QuestionName", "Question is required");
                    }
                    if (Answer == null || Answer == "")
                    {
                        ModelState.AddModelError("Answer", "Answer is required");
                    }
                    if (StartDate == null || StartDate == "")
                    {
                        ModelState.AddModelError("StartDate", "*");
                        ViewBag.StartDate = "Start Date Is Not Selected";
                    }
                    //else
                    //{
                    //    try
                    //    {

                    //        if (Convert.ToDateTime(StartDate) < Convert.ToDateTime())
                    //        {
                    //            ModelState.AddModelError("StartDate", "Start Date Should Be Greater Than or Equal To Current Date");
                    //            ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                    //        }

                    //    }
                    //    catch (FormatException ex)
                    //    {
                    //        ModelState.AddModelError("StartDate", "Invalid Start Date");
                    //        ViewBag.StartDate = "Invalid Start Date";
                    //    }

                    //}
                    if (EndDate == null || EndDate == "")
                    {
                        ModelState.AddModelError("Enddate", "*");
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
                                    ModelState.AddModelError("Enddate", "*");
                                    ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                                }
                            }
                            catch (FormatException ex)
                            {
                                ModelState.AddModelError("Enddate", "Invalid End Date");
                                ViewBag.EndDate = "Invalid End Date";
                            }
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("Enddate", "Invalid End Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }

                    }

                    if (ModelState.IsValid)
                    {
                        FAQ obj_faq = db.FAQs.Single(a => a.Id == Id);

                        //Save Record In History table
                        FAQ_History CUDHistory = new FAQ_History();
                        CUDHistory.FAQ_ParentId = obj_faq.Id;
                        CUDHistory.QuestionName = obj_faq.QuestionName;
                        CUDHistory.Answer = obj_faq.Answer;
                        CUDHistory.Status = obj_faq.Status;
                        CUDHistory.StartDate = obj_faq.StartDate;
                        CUDHistory.EndDate = obj_faq.EndDate;

                        CUDHistory.ModifiedBy = Session["userid"].ToString();
                        CUDHistory.ModifiedOn = DateTime.Now;
                        db.FAQ_History.Add(CUDHistory);

                        //Save Record
                        obj_faq.QuestionName = QuestionName;
                        obj_faq.Answer = Answer;
                        obj_faq.ModifiedOn = DateTime.Now;
                        obj_faq.ModifiedBy = Session["userid"].ToString();
                        obj_faq.StartDate = Convert.ToDateTime(StartDate);
                        obj_faq.EndDate = Convert.ToDateTime(EndDate);
                        if (Status.ToLower() == "on")
                        {
                            obj_faq.Status = 1;
                        }
                        else
                        {
                            obj_faq.Status = 0;
                        }


                        ViewBag.Update = "Done";
                        db.SaveChanges();
                    }


                    return View("Edit", db.FAQs.Single(a => a.Id == Id));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }


    }
}
