using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_Demo
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
               name: "User",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Book",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Book", action = "Books", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Category",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Category", action = "CategoryList", id = UrlParameter.Optional }
           );
        }
    }
}
