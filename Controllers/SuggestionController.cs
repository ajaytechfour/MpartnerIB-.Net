using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using PagedList;
using PagedList.Mvc;
using System.Data;
namespace Luminous.Controllers
{
    public class SuggestionController : MultiLanguageController
    {
        // controller
        // GET: /Suggestion/
        private LuminousEntities db = new LuminousEntities();

        private DataTable dt = new DataTable();
        public ActionResult SuggestionList(int? page)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Suggestion/SuggestionList";
              //  DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true /*result[0]["uview"].ToString() == "1"*/)
                {
                    //return View(db.Suggestions.OrderByDescending(a => a.id).ToList().ToPagedList(page ?? 1, 10));
                    return View();
                }
                //else
                //{
                //    return RedirectToAction("snotallowed", "snotallowed");
                //}
            }
        }

    }
}
