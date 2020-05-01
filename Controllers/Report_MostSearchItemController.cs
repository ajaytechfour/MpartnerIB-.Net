using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Luminous.EF;
using System.Web.UI;
using System.IO;
namespace Luminous.Controllers
{
    public class Report_MostSearchItemController : Controller
    {
        //
        // GET: /Report_MostSearchItem/

        private LuminousEntities db = new LuminousEntities();

        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {

                return View();


            }
        }


        public ActionResult Downloaddata(string startdate, string enddate)
        {
            try
            {


                if (startdate != "" && enddate != "")
                {
                    enddate = enddate + " 23:59:00";
                    DateTime stdate = Convert.ToDateTime(startdate);
                    DateTime edate = Convert.ToDateTime(enddate);
                    int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                    var notificationsurvey = db.NotificationSurveys.Where(u => u.StartDate >= stdate && u.Enddate <= edate && u.ContestId == 1).ToList();
                    var most_searchitem = db.spMostSearchItem(stdate, edate).ToList();

                    if (most_searchitem.Count > 0)
                    {
                        TempData.Remove("Nodataexist");
                        TempData.Remove("Emptyerror");
                        TempData.Remove("Exception");

                        var grid = new System.Web.UI.WebControls.GridView();
                        grid.DataSource = most_searchitem;
                        grid.DataBind();
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachement; filename=MostSearchItem" + DateTime.Now + ".xls");
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
