using System.Web;
using System.Web.Optimization;

namespace GreenPro.AdminInterface
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/jquery-ui.js",
                "~/Scripts/gridmvc.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/metisMenu.js",
                      "~/Scripts/sb-admin-2.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/jquery-ui.css",
                "~/Content/jquery-ui.theme.css",
                 "~/Content/gridmvc.css",
                      "~/Content/bootstrap.css",
                       "~/Content/metisMenu.css",
                        "~/Content/timeline.css",
                       "~/Content/sb-admin-2.css",
                       "~/Content/font-awesome.css",
                       "~/Content/site.css"));
            
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
