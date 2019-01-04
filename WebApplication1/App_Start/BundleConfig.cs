using System.Web;
using System.Web.Optimization;

namespace WebApplication1
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // jQuery UI css & js
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                    "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                    "~/Content/themes/base/all.css"));

            // jQuery Confirm
            bundles.Add(new ScriptBundle("~/bundles/jquery-confirm").Include(
                    "~/Scripts/jquery-confirm.min.js"));

            bundles.Add(new StyleBundle("~/Content/jquery-confirm").Include(
                    "~/Content/jquery-confirm.min.css"));

            // jQuery Datatable
            bundles.Add(new ScriptBundle("~/bundles/jquery-datatable").Include(
                    "~/Scripts/DataTables/jquery.dataTables.min.js",
                    "~/Scripts/DataTables/dataTables.bootstrap.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/jquery-datatable").Include(
                    "~/Content/DataTables/css/jquery.dataTables.min.css"));
        }
    }
}
