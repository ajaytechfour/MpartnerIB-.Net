using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.Models;

namespace Luminous.Controllers
{
    public class LuminousHomeController : MultiLanguageController
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

        [HttpPost]
        public ActionResult Index(RegistrationModel r)
        {
            return View(r);
        }

    }
}
