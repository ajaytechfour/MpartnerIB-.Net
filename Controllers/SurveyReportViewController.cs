using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using LuminousMpartnerIB.EF;
using System.Web.UI;
namespace LuminousMpartnerIB.Controllers
{

    public class SurveyReportViewController : Controller
    {
        //
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        // GET: /SurveyReport/
        private DataTable dt = new DataTable();

        private string PageUrl = "/SurveyReport/Index";

        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                // var data = db.NotificationSurveys.Where(x => x.ContestId == 1).Select(c=>new{c.SurveyID,c.Survey});
                var data = db.NotificationSurveys.Select(c => new { c.SurveyID, c.Survey });
                data = data.Where(x => x.Survey != null);

                return View();
                // return Json(data, JsonRequestBehavior.AllowGet);


            }
        }

        public JsonResult GetSurvey()
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var data = db.NotificationSurveys.Where(x => x.ContestId == 1).Select(c => new { c.SurveyID, c.Survey });

                var data = db.NotificationSurveys.Select(c => new { c.SurveyID, c.Survey });
                data = data.Where(x => x.Survey != null);

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }





        public ActionResult Downloaddata(string startdate, string enddate, string SurveyID)
        {
            var id = Convert.ToInt32(SurveyID);
            try
            {


                if (id != 0)
                {

                    //enddate = enddate + " 23:59:00";
                    //DateTime stdate = Convert.ToDateTime(startdate);
                    //DateTime edate = Convert.ToDateTime(enddate);
                    //int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                    //var notificationsurvey = db.NotificationSurveys.Where(u => u.StartDate >= stdate && u.Enddate <= edate && u.ContestId == 1 && u.SurveyID == id).ToList();
                    //var savesurvey = (from ns in notificationsurvey
                    //                  join sn in db.SaveNotificationSurveys on ns.SurveyID equals sn.SurveyID
                    //                  join us in db.UsersLists on sn.UserId equals us.UserId
                    //                  select new
                    //                  {
                    //                      SurveyID = ns.SurveyID,
                    //                      QuestionTitle = ns.QuestionTitle,
                    //                      SapCode = sn.UserId,
                    //                      Name = us.Dis_Name,
                    //                      CustomerType = us.CustomerType,
                    //                      State = us.Dis_State,
                    //                      OptionA = ns.OptionA,
                    //                      OptionB = ns.OptionB,
                    //                      OptionC = ns.OptionC,
                    //                      OptionD = ns.OptionD,
                    //                      OptionE = ns.OptionE,
                    //                      SelectOption = sn.Options + "_" + sn.OptionValue,
                    //                      ResponseDate = sn.CreatedOn
                    //                  }).ToList();

                    var savesurvey = db.GetSurveyRecord((int)id);

                    if (savesurvey > 0)
                    {
                        //TempData.Clear();

                        TempData.Remove("Nodataexist");

                        TempData.Remove("Emptyerror");
                        TempData.Remove("Exception");

                        var grid = new System.Web.UI.WebControls.GridView();
                        grid.DataSource = savesurvey;
                        grid.DataBind();
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachement; filename=Survey_" + DateTime.Now + ".xls");
                        Response.ContentType = "application/excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        grid.RenderControl(htw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                    else
                    {
                        TempData["Nodataexist"] = "No Data Exist";

                    }

                    //int downloadcount = downloaddata;
                    //int ActiveUserCount = ActiveUser;

                    //TempData.Remove("Nodataexist");
                    //TempData.Remove("Emptyerror");
                    //TempData.Remove("Exception");

                    //return Json(downloadcount + "," + ActiveUserCount, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception exc)
            {
                return Json("Some exception has occurred", JsonRequestBehavior.AllowGet);
            }

            return View("Index");
        }

    }
}
