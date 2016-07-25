using System.Web.Mvc;
using System.Web.Routing;

namespace Bitly.Web.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "ShortLink",
                url: "{shortPath}",
                defaults: new { controller = "Redirect", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}