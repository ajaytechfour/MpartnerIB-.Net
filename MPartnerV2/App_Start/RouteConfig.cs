using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Luminous
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "Default123",
            url: "FunctionsGroup/Save",
                //url: "{action}/{controller}/{id}",
            defaults: new { controller = "FunctionsGroup", action = "Index" },
            constraints: new {httpMethod= new HttpMethodConstraint("GET") }
        );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //url: "{action}/{controller}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}