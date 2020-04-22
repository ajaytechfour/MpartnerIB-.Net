using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Luminous.EF;
using System.IO;
using System.Web.UI;

namespace Luminous.Controllers
{
    public class UserActiveController : Controller
    {
        //
        // GET: /UserActive/

        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();

        private string PageUrl = "/UserActive/Index";
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
        public ActionResult Downloaddata(string startdate, string enddate)
        {

            try
            {
                if (startdate != "" && enddate != "")
                {
                    enddate = enddate + " 23:59:00";
                    DateTime stdate = Convert.ToDateTime(startdate);
                    DateTime edate = Convert.ToDateTime(enddate);
                    db.CommandTimeout = 1000;

                    var downloaddata = db.spGetVisitReport(stdate, edate).ToList();

                    if (downloaddata.Count > 0)
                    {
                        TempData.Remove("Nodataexist");
                        TempData.Remove("Emptyerror");
                        TempData.Remove("Exception");

                        var grid = new System.Web.UI.WebControls.GridView();
                        grid.DataSource = downloaddata;
                        grid.DataBind();
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachement; filename=User Visit Report" + DateTime.Now + ".xls");
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

                }
                else
                {
                    TempData["Emptyerror"] = "Start Date or End Date Cannot Be Empty";

                }

            }
            catch (Exception exc)
            {
                TempData["Exception"] = exc;
                return View("Index");
            }

            return View("Index");

        }

    }
}
