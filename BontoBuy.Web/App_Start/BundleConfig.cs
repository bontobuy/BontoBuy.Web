using System.Web;
using System.Web.Optimization;

namespace BontoBuy.Web
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

            bundles.Add(new ScriptBundle("~/bundles/jQueryUI").Include(
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapMagnify").Include(
                        "~/Scripts/bootstrap-magnify.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/JAlert").Include(

                "~/Scripts/jAlert-v3.js",
                "~/Scripts/jAlert-functions.js"));

            //"~/Scripts/eModal.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/fileinput.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/moment-with-locales.js",
                      "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/Content/JAlert").Include(
                            "~/Content/jAlert-v3.css"));

            bundles.Add(new StyleBundle("~/Content/jQueryUI").Include(
                            "~/Content/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrapMagnify").Include(
                           "~/Content/bootstrap-magnify.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Custom.css",
                      "~/content/bootstrap-theme.css",
                      "~/Content/Login.css",
                      "~/Content/Product.css",
                      "~/Content/fileinput.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/bootstrap-datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/SupplierCSS").Include(
                "~/Content/Supplier/Site.css",
                "~/Content/bootstrap.css",
                "~/content/bootstrap-theme.css",
                "~/Content/fileinput.css",
                "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new StyleBundle("~/Content/AdminCSS").Include(
                "~/Content/Admin/Site.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/CustomJS").Include(
               "~/Scripts/bootstrap.min.js",
               "~/Scripts/bootstrap.js",
               "~/Scripts/Custom.js"));

            bundles.Add(new StyleBundle("~/Content/FontAwesome").Include(
                "~/Content/font-awesome.css"));

            //    bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
        }
    }
}