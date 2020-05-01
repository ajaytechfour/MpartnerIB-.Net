using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.Models;

namespace Luminous.Controllers
{
    public class CustomerModuleController : Controller
    {
        // GET: CustomerModule
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new LanguageManage().SetLanguage(lang);
            return RedirectToAction("Luminous", "LuminousHome");
            // return View();
        }
    }
}