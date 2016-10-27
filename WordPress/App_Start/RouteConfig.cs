using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WordPress
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Category",
                url: "category/{*id}",
                defaults: new {controller = "WordPress", action = "Category", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "Post",
                url: "post/{*id}",
                defaults: new { controller = "WordPress", action = "Post", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Content",
                url: "content/{*id}",
                defaults: new { controller = "WordPress", action = "Page", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Author",
                url: "author/{*id}",
                defaults: new {controller = "WordPress", action = "Author", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{*id}",
                defaults: new { controller = "WordPress", action = "Index", id = "home" }
            );


        }
    }
}
