using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using LuminousMpartnerIB.EF;
using System.IO;
using System.Web.UI;
namespace LuminousMpartnerIB.Controllers
{
    public class UserDownloadController : Controller
    {
        //
        // GET: /UserVerificationPermission/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();

        private string PageUrl = "/UserDownload/Index";
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
        public ActionResult Downloaddata(string startdate, string enddate, string ostype)
        {

            try
            {
                if (startdate != "" && enddate != "")
                {
                    enddate = enddate + " 11:59:00";
                    DateTime stdate = Convert.ToDateTime(startdate);
                    DateTime edate = Convert.ToDateTime(enddate);
                    var downloaddata = (from u in db.UserVerifications
                                        where u.CreatedOn >= stdate && u.CreatedOn <= edate && u.OSType == ostype && u.Status==2
                                        select new
                                        {
                                            Sap_Code = u.UserId,
                                            OSType = u.OSType,
                                            Download_Date = u.CreatedOn
                                        }).ToList();
                    if (downloaddata.Count > 0)
                    {
                        TempData.Remove("Nodataexist");
                        TempData.Remove("Emptyerror");
                        TempData.Remove("Exception");

                        var grid = new System.Web.UI.WebControls.GridView();
                        grid.DataSource = downloaddata;
                        grid.DataBind();
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachement; filename=UserDownload" + DateTime.Now + ".xls");
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
                TempData["Exception"] = "Some Error Has Occured";
                return View("Index");
            }

            return View("Index");

        }

    }
}
