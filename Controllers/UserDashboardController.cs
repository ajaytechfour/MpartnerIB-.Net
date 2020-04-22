using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.IO;
using System.Web.UI;
using System.Data;
namespace LuminousMpartnerIB.Controllers
{
    public class UserDashboardController : Controller
    {
        //
        // GET: /UserDashboard/

        //Implemenetd User Dashboard process by Ravi on 19-07-2018 Task Id - 4009
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();

        private string PageUrl = "/UserDashboard/Index";
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

                    return View();
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }

            }
        }

        public ActionResult getUserDashboard(string startdate, string enddate)
        {
            try
            {

                if (startdate != "" && enddate != "")
                {
                    enddate = enddate + " 23:59:00";
                    DateTime stdate = Convert.ToDateTime(startdate);
                    DateTime edate = Convert.ToDateTime(enddate);
                    int year= Convert.ToInt32(DateTime.Now.Year.ToString());
                    //var downloaddata = db.UserVerifications.Where(u => u.CreatedOn >= stdate && u.CreatedOn <= edate && u.OSType == ostype && u.Status == 2).Select(c=>c.UserId).ToList().Count();
                    //var ActiveUser = db.ExecuteStoreQuery<userDashboard>("select count(*) as res,Userid from   MPartnerServiceLog where Userid in ( select Userid   from MPartnerServiceLog where CreatedOn >= DATEADD(day,-150, getdate()) and year(CreatedOn)=" + year + ") and CreatedOn >= DATEADD(day,-150, getdate()) and year(CreatedOn)=" + year + " and UserId!=''  group by UserId").Count();

                    // int downloadcount = downloaddata;
                    // int ActiveUserCount = ActiveUser;


                    ////var ActiveUserdownload = db.ActiveUserReport(stdate, edate).ToList();
                    ////var grid = new System.Web.UI.WebControls.GridView();
                    ////grid.DataSource = ActiveUserdownload;
                    ////grid.DataBind();
                    ////Response.ClearContent();
                    ////Response.AddHeader("content-disposition", "attachement; filename=Active User" + DateTime.Now + ".xls");
                    ////Response.ContentType = "application/excel";
                    ////StringWriter sw = new StringWriter();
                    ////HtmlTextWriter htw = new HtmlTextWriter(sw);
                    ////grid.RenderControl(htw);
                    ////Response.Output.Write(sw.ToString());
                    ////Response.Flush();
                    ////Response.End();

                    
                    ////    TempData.Remove("Nodataexist");
                    ////    TempData.Remove("Emptyerror");
                    ////    TempData.Remove("Exception");

                        return View("Index");
                  
                }
            }
            catch(Exception exc)
            {
                return Json("Some exception has occurred", JsonRequestBehavior.AllowGet);
            }
            return View("Index");
        }

    }
}
