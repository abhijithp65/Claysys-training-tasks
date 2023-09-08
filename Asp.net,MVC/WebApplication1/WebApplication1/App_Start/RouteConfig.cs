using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ViewUser",
                url: "Registration/ViewUser/{id}",
                defaults: new { controller = "Registration", action = "ViewUser", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Registration",
                url: "registration/{action}",
                defaults: new { controller = "Registration", action = "Index" }
            );
        }
    }
}
