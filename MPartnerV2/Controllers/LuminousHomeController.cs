using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luminous.Controllers
{
    public class LuminousHomeController : Controller
    {
        //
        // GET: /LuminousHome/

        public ActionResult Luminous()
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

    }
}
