using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmbassyAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //"GetAllNews",
            //"api/embassyAPI/GetAllNews",
            //defaults: new { controller = "embassyAPI", action = "GetAllNews", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //"OfficialLogoandApplicationforEndorsments",
            //"api/embassyAPI/GetAllNewsLogo",
            //defaults: new { controller = "embassyAPI", action = "GetAllNewsLogo", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
