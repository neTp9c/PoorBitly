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

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
               "~/scripts/angular.js",
               "~/scripts/angular-ui/ui-bootstrap.js",
               "~/scripts/angular-ui/ui-bootstrap-tpls.js",
               "~/scripts/angular/app.module.js",
               "~/scripts/angular/app.service.js",
               "~/scripts/angular/add-link/add-link.module.js",
               "~/scripts/angular/add-link/add-link.component.js",
               "~/scripts/angular/user-links/user-links.module.js",
               "~/scripts/angular/user-links/user-links.controller.js"));
        }
    }
}