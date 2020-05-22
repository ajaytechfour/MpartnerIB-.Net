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
    public class FAQViewController : MultiLanguageController
    {
        //
        // GET: /ContactUs/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/FAQ/Index";

        public ActionResult Index()
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
                var data = db.FAQs.OrderByDescending(a => a.Id).ToList().ToPagedList(1, 1);
                if (true)
                {
                    return View(data);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }

        }

        public JsonResult GetFAQDetail(int? page, string id = "")
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


                    if (id != "")
                    {
                        var distname = db.UsersLists.Where(x => x.Dis_Sap_Code == id).FirstOrDefault();
                        Session["seldistributor"] = distname.Dis_Name;

                        var contactDetails2 = (from c in contactdetails
                                               where c.CreatedBy == id.ToString()
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
                        var contactDetails2 = (from c in contactdetails
                                               where c.CreatedBy == id.ToString()
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

                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }



        [HttpGet]
        public ActionResult View(int id)
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



    }
}
