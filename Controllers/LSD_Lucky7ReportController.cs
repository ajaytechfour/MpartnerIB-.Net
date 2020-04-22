using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using LuminousMpartnerIB.EF;
using System.IO;
using System.Web.UI;
using System.Text;
namespace LuminousMpartnerIB.Controllers
{
    public class LSD_Lucky7ReportController : Controller
    {
        //
        // GET: /LSD_Lucky7Report/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DownloadLSDMaster()
        {


            var DealerData = db.LSD_Master.Where(c => c.Status == 1).ToList();
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = DealerData;
            Response.ClearContent();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachement; filename=DealerDump" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.ContentType = "application//vnd.xls";
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Buffer = false;
            grid.AllowPaging = false;
            grid.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }
        public ActionResult DownloadLSDCoupon()
        {


            var CouponData = db.Lsd_DistCouponCount.Where(c => c.Status == 1).ToList();
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = CouponData;

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachement; filename=CouponDump" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.ContentType = "application//vnd.xls";
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Buffer = false;
            grid.AllowPaging = false;
            grid.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }
        public ActionResult DownloadLSDGift()
        {


            var DealerData = db.Lsd_GiftMaster.Where(c => c.Status == 1).ToList();
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = DealerData;

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachement; filename=GiftDump" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.ContentType = "application//vnd.xls";
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Buffer = false;
            grid.AllowPaging = false;
            grid.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }

    }
}
