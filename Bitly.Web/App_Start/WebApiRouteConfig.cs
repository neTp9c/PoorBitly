using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Bitly.Web
{
    public static class WebApiRouteConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}
