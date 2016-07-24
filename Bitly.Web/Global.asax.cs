using Bitly.Web.App_Start;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Bitly.Web
{
    public class MvcApplication : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.Register();
        }
    }
}