//Added New controller ConnectAssistReport for getting Assist data in excel report by Ravi  on 20-08-2018 Task Id - 4009.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.IO;
using System.Web.UI;
using System.Data;
namespace Luminous.Controllers
{
    public class ConnectAssistReportController : Controller
    {
        //
        // GET: /ConnectAssistReport/


        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();

        private string PageUrl = "/ConnectAssistReport/Index";

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
        public ActionResult getAssistReport(string startdate, string enddate)
        {
            try
            {

                if (startdate != "" && enddate != "")
                {
                    enddate = enddate + " 23:59:00";
                    DateTime stdate = Convert.ToDateTime(startdate);
                    DateTime edate = Convert.ToDateTime(enddate);
                    int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                 //   var AssistReport = db.ExecuteStoreQuery<Assist_Report>("select CreatedOn as TicketDate,Srno as ProductSerialNo,CreatedBy as DealerCode,InProcessDate as PickedOn,Flag as Status,ResolvedDate as ResolvedDate from  ConnectAssist").ToList();
                    //int downloadcount = downloaddata;
                   // int ActiveUserCount = AssistReport;
                    
                    var AssistReport = (from u in db.ConnectAssists
                                        where u.CreatedOn >= stdate && u.CreatedOn <= edate 
                                        select new
                                        {
                                            TicketDate = u.CreatedOn,
                                            ProductSerialNo = u.Srno,
                                            DealerCode = u.CreatedBy,
                                            PickedOn = u.InProcessDate,
                                            Status=u.Flag,
                                            ResolvedDate = u.ResolvedDate
                                        }).ToList();

                    if (AssistReport.Count > 0)
                    {
                        TempData.Remove("Nodataexist");
                        TempData.Remove("Emptyerror");
                        TempData.Remove("Exception");

                        var grid = new System.Web.UI.WebControls.GridView();
                        grid.DataSource = AssistReport;
                        grid.DataBind();
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachement; filename=AssistReport_" + DateTime.Now + ".xls");
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
                return Json("Some exception has occurred", JsonRequestBehavior.AllowGet);
            }
            return View("Index");
        }
    }
}
