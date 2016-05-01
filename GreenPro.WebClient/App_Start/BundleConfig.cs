using System.Web;
using System.Web.Optimization;

namespace GreenPro.WebClient
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/gridmvc.js",
                "~/Scripts/gridmvc-ext.js",
                "~/scripts/moment.min.js",

                     "~/scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/main.js",
                      "~/Scripts/ladda-bootstrap/ladda.min.js",
                      "~/Scripts/ladda-bootstrap/spin.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/gridmvc.css",
                 "~/Content/style.css",
                  "~/content/responsive.css",
                      "~/Content/bootstrap.css",
                       "~/content/responsive.css",
                       "~/Content/bootstrap-datetimepicker.min.css",
                       "~/Content/ladda-bootstrap/ladda-themeless.min.css"





                      ));


        }
    }
}
