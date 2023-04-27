using System.Web;
using System.Web.Optimization;

namespace QLDV
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

            bundles.Add(new StyleBundle("~/assets/css/default").Include(
                "~/assets/plugins/jquery-ui/jquery-ui.min.css",
                "~/assets/plugins/bootstrap/css/bootstrap.min.css",
                "~/assets/plugins/font-awesome/css/all.min.css",
                "~/assets/plugins/animate/animate.min.css",
                "~/assets/css/default/style.min.css",
                "~/assets/css/default/style-responsive.min.css",
                "~/assets/css/default/theme/blue.css",
                "~/assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css",
                "~/assets/plugins/DataTables/extensions/Select/css/select.bootstrap.min.css",
                "~/assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css",
                "~/assets/plugins/bootstrap-validator/dist/css/bootstrapValidator.min.css",
                "~/assets/plugins/bootstrap-datepicker/css/bootstrap-datepicker.css",
                "~/assets/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.css"));

            bundles.Add(new ScriptBundle("~/assets/js/default").Include(
                "~/assets/plugins/jquery/jquery-3.3.1.min.js",
                "~/assets/plugins/jquery/jquery-migrate-1.1.0.min.js",
                "~/assets/plugins/jquery-ui/jquery-ui.min.js",
                "~/assets/plugins/bootstrap/js/bootstrap.bundle.min.js",
                "~/assets/plugins/pace/pace.min.js",
                "~/assets/plugins/bootstrap-sweetalert/sweetalert.min.js",
                "~/assets/plugins/DataTables/media/js/jquery.dataTables.js",
                "~/assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js",
                "~/assets/plugins/DataTables/extensions/Select/js/dataTables.select.min.js",
                "~/assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js",
                "~/assets/plugins/bootstrap-validator/dist/js/bootstrapValidator.min.js",
                "~/assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/assets/js/application").Include(
                "~/assets/plugins/slimscroll/jquery.slimscroll.min.js",
                "~/assets/plugins/js-cookie/js.cookie.js",
                "~/assets/js/theme/default.min.js",
                "~/assets/js/apps.min.js"));
        }
    }
}
