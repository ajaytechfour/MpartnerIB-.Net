using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
namespace Luminous.Controllers
{
    public class FunctionalGroupController : Controller
    {
        //
        // GET: /FunctionalGroup/
        private LuminousEntities db = new LuminousEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Save(functionalgroup group, string Status)
        {

            Status = Status ?? "off";



            if (ModelState.IsValid)
            {                                
               
                if (Status.ToLower() == "on")
                {
                    group.status = 1;

                }
                else
                {
                    group.status = 0;
                }
                db.functionalgroups.AddObject(group);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    return Content("<script>alert('Function Has Been Created Sucessfully');</script>");
                }

                else
                {
                    return Content("<script>alert('Function Not Created');</script>");
                }

            }
            return View("Index");
        }

    }
}
