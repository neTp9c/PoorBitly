using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Bitly.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css").Include(
               "~/content/bootstrap.css",
               "~/content/bootstrap-theme.css",
               "~/content/site.css"));
        }
    }
}