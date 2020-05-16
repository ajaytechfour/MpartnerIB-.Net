using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using PagedList;
using PagedList.Mvc;
using System.Data;
namespace LuminousMpartnerIB.Controllers
{
    public class SuggestionController : Controller
    {
        //
        // GET: /Suggestion/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();

        private DataTable dt = new DataTable();
        public ActionResult SuggestionList(int? page)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = "/Suggestion/SuggestionList";
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    return View(db.Suggestions.OrderByDescending(a => a.id).ToList().ToPagedList(page ?? 1, 10));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

    }
}
