using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Data;
namespace Luminous.Controllers
{
    public class NotificationSurveyController : Controller
    {
        //
        // GET: /NotificationSurvey/
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/NotificationSurvey/Index";
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
                    ViewBag.contest = new SelectList(db.ContestMasters.Where(c => c.Status != 0), "Id", "ContestName");

                    return View();
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }

            }

        }
        public JsonResult GetContactDetail(int? page)
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
                    var contactdetails = (from c in db.NotificationSurveys
                                          where c.Status != 0 && c.ContestId != 2
                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var contactDetails2 = (from c in contactdetails


                                           select new
                                           {
                                               Survey = c.Survey,
                                               Question = c.QuestionTitle,
                                               QuestionType = c.QuestionType,
                                               status = c.Status == 1 ? "Enable" : "Disable",
                                               id = c.SurveyID,
                                               Startdate = c.StartDate,
                                               Enddate = c.Enddate


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



        public JsonResult GetContactDetail_Contest(int? page)
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
                    var contactdetails = (from c in db.NotificationSurveys
                                          where c.Status != 0 && c.ContestId != 1
                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var contactDetails2 = (from c in contactdetails


                                           select new
                                           {

                                               Question = c.QuestionTitle,
                                               QuestionType = c.QuestionType,
                                               status = c.Status == 1 ? "Enable" : "Disable",
                                               id = c.SurveyID,
                                               Startdate = c.StartDate,
                                               Enddate = c.Enddate


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



        [HttpPost]
        public ActionResult SaveSurveyQuestion(string Survey, string Type, string QuestionTitle, string Questiontype, string OptionA, string OptionB, string OptionC, string OptionD, string OptionE, string Answer, string StartDate, string enddate, string statusC)
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
                if (result[0]["createrole"].ToString() == "1")
                {
                    ViewBag.preStartDate = StartDate;
                    ViewBag.PreEndDate = enddate;
                    ViewBag.contest = new SelectList(db.ContestMasters, "Id", "ContestName");
                    #region Check Validation For Start Date And End Date
                    if (StartDate == null)
                    {
                        ModelState.AddModelError("StartDate", "*");
                        ViewBag.StartDate = "Start Date Is Not Selected";
                    }
                    //else
                    //{
                    //    try
                    //    {

                    //        if (Convert.ToDateTime(StartDate) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    //        {
                    //            ModelState.AddModelError("StartDate", "*");
                    //            ViewBag.StartDate = "Start Date Should Be Greater Than or Equal To Current Date";
                    //        }
                    //        DateTime startDate = Convert.ToDateTime(StartDate);
                    //        if (db.ProductLevelThrees.Any(a => a.enddate >= startDate && a.PlTwStatus != 2))
                    //        {
                    //            ModelState.AddModelError("StartDate", "*");
                    //            ViewBag.StartDate = "There Is already A Product Defined In This Date";
                    //        }
                    //    }
                    //    catch (FormatException ex)
                    //    {
                    //        ModelState.AddModelError("StartDate", "Invalid Date");
                    //        ViewBag.StartDate = "Invalid Date";
                    //    }
                    //}
                    if (enddate == null || enddate == "")
                    {
                        ModelState.AddModelError("EndDate", "*");
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

                                if (Convert.ToDateTime(enddate) < startDate)
                                {
                                    ModelState.AddModelError("End Date", "*");
                                    ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                                }
                            }
                            catch (FormatException ex)
                            {
                                ModelState.AddModelError("Enddate", "Invalid Date");
                                ViewBag.EndDate = "Invalid End Date";
                            }
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("Enddate", "Invalid Start Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }
                    }

                    #endregion
                    if (Type =="")
                    {
                        if (Survey == "" || Survey == null)
                        {
                            ModelState.AddModelError("Survey", "Survey Field Cannot Be Blank");
                        }
                    }
                    if (QuestionTitle == "" || QuestionTitle == null)
                    {
                        ModelState.AddModelError("QuestionTitle", "Question Title Cannot Be Blank");
                    }
                    if (Questiontype == "" || Questiontype == null)
                    {
                        ModelState.AddModelError("QuestionType", "Please Select Survey Type");
                    }
                    if (Questiontype == "MCQ")
                    {
                        if (OptionA == "" && OptionB == "")
                        {
                            ModelState.AddModelError("OptionA", "Please Enter Atleast Two Option From Starting.");
                            ModelState.AddModelError("OptionB", "Please Enter Atleast Two Option From Starting.");
                        }
                    }
                    else
                    {
                        if (OptionA == "" && OptionB == "")
                        {
                            ModelState.AddModelError("OptionA", "Please Enter Atleast Two Option From Starting.");
                            ModelState.AddModelError("OptionB", "Please Enter Atleast Two Option From Starting.");
                        }
                    }
                    if (Answer == "" || Answer == null)
                    {

                        ModelState.AddModelError("CorrectAns", "Please Select Option.");

                    }
                    if (ModelState.IsValid)
                    {
                        NotificationSurvey Nos = new NotificationSurvey();
                        Nos.Survey = Survey;
                        Nos.QuestionTitle = QuestionTitle;
                        Nos.QuestionType = Questiontype;
                        Nos.StartDate = Convert.ToDateTime(StartDate);
                        Nos.Enddate = Convert.ToDateTime(enddate);
                        Nos.OptionA = OptionA;
                        Nos.OptionB = OptionB;
                        Nos.OptionC = OptionC;
                        Nos.OptionD = OptionD;
                        Nos.OptionE = OptionE;
                        Nos.CorrectAns = Answer;
                        Nos.CreatedOn = DateTime.Now;
                        Nos.CreatedBy = Convert.ToInt32(Session["Id"]);
                        if (Type == "")
                        {
                            Nos.ContestId = 1;
                        }
                        else
                        {
                            Nos.ContestId = Convert.ToInt32(Type);
                        }

                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            Nos.Status = 1;
                        }
                        else
                        {
                            Nos.Status = 0;
                        }

                        db.NotificationSurveys.AddObject(Nos);

                       
                        db.SaveChanges();
                        return Content("<script>alert('Record Save Successfully');location.href='../NotificationSurvey/Index';</script>");

                    }


                    return View("Index");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

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
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    NotificationSurvey cud = db.NotificationSurveys.Single(a => a.SurveyID == id);
                    ViewBag.status = cud.Status;
                 
                      if(cud.ContestId==1)
                      {
                          ViewBag.ContestData = "Survey";
                      }
                    if(cud.ContestId==2)
                      {
                          ViewBag.ContestData="Contest";
                      }
                  
                 
                    if (cud.QuestionType == "MCQ")
                    {
                        ViewBag.Questypemcq = cud.QuestionType;
                    }
                    if (cud.QuestionType == "Text")
                    {
                        ViewBag.Questypetext = cud.QuestionType;
                    }
                    if (cud.CorrectAns == "OptionA" || cud.CorrectAns == "OptionB" || cud.CorrectAns == "OptionC" || cud.CorrectAns == "OptionD" || cud.CorrectAns == "OptionE")
                    {
                        ViewBag.CorrectAns = cud.CorrectAns;
                    }
                    ViewBag.Correctans = cud.CorrectAns;
                    ViewBag.preStartDate = Convert.ToDateTime(cud.StartDate).ToString("dd-MM-yyyy");
                    ViewBag.PreEndDate = Convert.ToDateTime(cud.Enddate).ToString("dd-MM-yyyy");
                    ViewBag.contest = new SelectList(db.ContestMasters, "Id", "ContestName",cud.ContestId);
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public ActionResult Update(NotificationSurvey contactUs, string statusC, string startdate, string enddate)
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
                    int pcid;
                    ViewBag.status = statusC;
                    ViewBag.preStartDate = startdate;
                    ViewBag.PreEndDate = enddate;
                    if (contactUs.Enddate == null)
                    {
                        ModelState.AddModelError("EndDate", "*");
                        ViewBag.EndDate = "End Date Is Not Selected";
                    }
                    else
                    {
                        DateTime startDate = new DateTime();
                        try
                        {
                            startDate = Convert.ToDateTime(contactUs.StartDate);
                            try
                            {

                                if (Convert.ToDateTime(contactUs.Enddate) < startDate)
                                {
                                    ModelState.AddModelError("End Date", "*");
                                    ViewBag.EndDate = "End Date Should Be Greater Than or Equal To Start Date";
                                }
                            }
                            catch (FormatException ex)
                            {
                                ModelState.AddModelError("Enddate", "Invalid Date");
                                ViewBag.EndDate = "Invalid End Date";
                            }
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("Enddate", "Invalid Start Date");
                            ViewBag.EndDate = "Invalid Start Date";
                        }
                    }


                    if (db.NotificationSurveys.Any(a => a.Survey.ToLower() == contactUs.Survey.ToLower() && a.SurveyID != contactUs.SurveyID && a.Status != 0))
                    {
                        ModelState.AddModelError("Survey", "Survey Already Exist");
                    }
                    if (contactUs.QuestionType.ToString() == "MCQ")
                    {
                        if (contactUs.ContestId == 1)
                        {
                            if (contactUs.Survey == "" || contactUs.Survey == null)
                            {
                                ModelState.AddModelError("Survey", "Survey Field Cannot Be Blank");
                            }
                        }
                        if (contactUs.QuestionTitle.ToString() == "" || contactUs.QuestionTitle.ToString() == null)
                        {
                            ModelState.AddModelError("QuestionTitle", "Question Title Cannot Be Blank");
                        }
                        if (contactUs.QuestionType.ToString() == "" || contactUs.QuestionType.ToString() == null)
                        {
                            ModelState.AddModelError("QuestionType", "Please Select Survey Type");
                        }
                        if (contactUs.QuestionType.ToString() == "MCQ")
                        {
                            if (contactUs.OptionA == null && contactUs.OptionB == null)
                            {
                                ModelState.AddModelError("OptionA", "Please Enter Atleast Two Option From Starting.");
                                ModelState.AddModelError("OptionB", "Please Enter Atleast Two Option From Starting.");
                            }
                        }
                        else
                        {
                            if (contactUs.OptionA == null && contactUs.OptionB == null)
                            {
                                ModelState.AddModelError("OptionA", "Please Enter Atleast Two Option From Starting.");
                                ModelState.AddModelError("OptionB", "Please Enter Atleast Two Option From Starting.");
                            }
                        }
                        if (contactUs.CorrectAns == null || contactUs.CorrectAns == null)
                        {

                            ModelState.AddModelError("CorrectAns", "Please Select Option.");

                        }
                    }
                    if (contactUs.StartDate == null)
                    {
                        ModelState.AddModelError("StartDate", "*");
                        ViewBag.StartDate = "Start Date Is Not Selected";
                    }
                    if (contactUs.Enddate == null)
                    {
                        ModelState.AddModelError("EndDate", "*");
                        ViewBag.EndDate = "End Date Is Not Selected";
                    }
                    //if (db.ProductCatergories.Any(a => a.PCode.ToLower() == contactUs.PCode.ToLower() && a.id != contactUs.id && a.Pstatus != 2))
                    //{
                    //    ModelState.AddModelError("PCode", "Product Code Already Exists");
                    //}
                    ViewBag.contest = new SelectList(db.ContestMasters, "Id", "ContestName", contactUs.ContestId);
                    if (ModelState.IsValid)
                    {
                        NotificationSurvey contactusd = db.NotificationSurveys.Single(a => a.SurveyID == contactUs.SurveyID);

                        //Save Previous Record In History
                        NotificationSurveyHistory CUDHistory = new NotificationSurveyHistory();
                        CUDHistory.SurveyID = contactusd.SurveyID;
                        CUDHistory.Survey = contactusd.Survey;
                        CUDHistory.QuestionType = contactusd.QuestionType;
                        CUDHistory.QuestionTitle = contactusd.QuestionTitle;
                        CUDHistory.OptionA = contactusd.OptionA;
                        CUDHistory.OptionB = contactusd.OptionB;
                        CUDHistory.OptionC = contactusd.OptionC;
                        CUDHistory.OptionD = contactusd.OptionD;
                        CUDHistory.OptionE = contactusd.OptionE;
                        CUDHistory.CorrectAns = contactusd.CorrectAns;
                        CUDHistory.ModifyBy = Convert.ToInt32(Session["Id"].ToString());
                        CUDHistory.ModifyOn = DateTime.Now;
                        CUDHistory.Status = contactUs.Status;
                        db.NotificationSurveyHistories.AddObject(CUDHistory);

                        //Save New Record In Table
                        contactusd.Survey = contactUs.Survey;
                        contactusd.QuestionTitle = contactUs.QuestionTitle;
                        contactusd.QuestionType = contactUs.QuestionType;
                        contactusd.OptionA = contactUs.OptionA;
                        contactusd.OptionB = contactUs.OptionB;
                        contactusd.OptionC = contactUs.OptionC;
                        contactusd.OptionD = contactUs.OptionD;
                        contactusd.OptionE = contactUs.OptionE;
                        contactusd.CorrectAns = contactUs.CorrectAns;
                        contactusd.ModifyOn = DateTime.Now;
                        contactusd.StartDate = contactUs.StartDate;
                        contactusd.Enddate = contactUs.Enddate;
                        contactusd.ModifyBy = Convert.ToInt32(Session["Id"].ToString());
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.Status = 1;
                        }
                        else
                        {
                            contactusd.Status = 0;
                        }
                        if (db.SaveChanges() > 0)
                        {
                            ViewBag.Result = "Record Updated Successfully";
                        }
                    }
                    NotificationSurvey nots = db.NotificationSurveys.Single(a => a.SurveyID == contactUs.SurveyID);
                   return View("Edit", nots);
                  
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        [HttpPost]
        public JsonResult DeleteContact(int id)
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
                    NotificationSurvey contactUs = db.NotificationSurveys.Single(a => a.SurveyID == id);
                    //Save Previous Record In History


                    NotificationSurveyHistory CUDHistory = new NotificationSurveyHistory();
                    CUDHistory.SurveyID = contactUs.SurveyID;
                    CUDHistory.Survey = contactUs.Survey;
                    CUDHistory.QuestionTitle = contactUs.QuestionTitle;
                    CUDHistory.QuestionType = contactUs.QuestionType;
                    CUDHistory.OptionA = contactUs.OptionA;
                    CUDHistory.OptionB = contactUs.OptionB;
                    CUDHistory.OptionC = contactUs.OptionC;
                    CUDHistory.OptionD = contactUs.OptionD;
                    CUDHistory.OptionE = contactUs.OptionE;
                    CUDHistory.CorrectAns = contactUs.CorrectAns;
                    CUDHistory.ModifyBy = Convert.ToInt32(Session["Id"].ToString());
                    CUDHistory.ModifyOn = DateTime.Now;

                    db.NotificationSurveyHistories.AddObject(CUDHistory);
                    //Update New Record
                    contactUs.Status = 0;
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
    }
}
