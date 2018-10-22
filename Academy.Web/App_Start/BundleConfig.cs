using System.Web;
using System.Web.Optimization;

namespace Academy.Web
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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-lumen.css",
                      "~/Content/site.css"));

            //bundles.Add(
            //    new StyleBundle("~/Bundles/vendor/css")
            //        .Include("~/Content/themes/base/all.css", new CssRewriteUrlTransform())
            //        .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
            //        .Include("~/Content/themes/metronic/assets/global/plugins/simple-line-icons/simple-line-icons.min.css", new CssRewriteUrlTransform())
            //        .Include("~/Content/themes/metronic/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css", new CssRewriteUrlTransform())
            //        .Include("~/Content/themes/metronic/assets/global/plugins/jstree/dist/themes/default/style.min.css", new CssRewriteUrlTransform())
            //        .Include("~/Content/themes/metronic/assets/global/css/components.min.css", new CssRewriteUrlTransform())
            //        .Include("~/Content/themes/metronic/assets/global/css/plugins.min.css", new CssRewriteUrlTransform())
            //        .Include("~/Content/themes/metronic/assets/layouts/layout/css/layout.min.css", new CssRewriteUrlTransform())
            //        .Include("~/Content/themes/metronic/assets/layouts/layout/css/themes/darkblue.min.css", new CssRewriteUrlTransform())
            //        .Include("~/Content/Site.css", new CssRewriteUrlTransform())
            //        .Include("~/Content/export.css", new CssRewriteUrlTransform())
            //);

        }
    }
}
