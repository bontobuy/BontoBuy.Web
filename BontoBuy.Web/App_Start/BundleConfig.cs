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
                      "~/Scripts/fileinput.js"));

            bundles.Add(new StyleBundle("~/Content/JAlert").Include(
                            "~/Content/jAlert-v3.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/content/Custom.css",
                      "~/content/bootstrap-theme.css",
                      "~/Content/Login.css",
                      "~/Content/Product.css",
                      "~/Content/fileinput.css"));

            bundles.Add(new StyleBundle("~/Content/SupplierCSS").Include(
                "~/Content/Supplier/Site.css",
                "~/Content/bootstrap.css",
                "~/content/bootstrap-theme.css",
                "~/Content/fileinput.css"));

            bundles.Add(new StyleBundle("~/Content/AdminCSS").Include(
                "~/Content/Admin/Site.css",
                "~/Content/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/CustomJS").Include(
               "~/Scripts/bootstrap.min.js",
               "~/Scripts/bootstrap.js",
               "~/Scripts/Custom.js"));

            //    bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
        }
    }
}