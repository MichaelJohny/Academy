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


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/moment.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                "~/Scripts/site.js",
                "~/Scripts/toastr.js",
                "~/Scripts/Select/select2.min.js",
                "~/Scripts/Table/main.js",
                "~/Scripts/popper.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap-lumen.css",
                "~/Content/site.css",
                "~/Content/toastr.css",
                "~/Content/bootstrap-datetimepicker.min.css",
                "~/Content/Select/select2.min.css",
                "~/Content/Table/animate.css",
                "~/Content/Table/main.css",
                "~/Content/Table/util.css",
                "~/Content/perfect-selector/perfect-scrollbar.css"));

            bundles.Add(
                new StyleBundle("~/Bundles/vendor/css")
                    .Include("~/Content/datetimepicker.css", new CssRewriteUrlTransform())
                    .Include("~/Content/daterangepicker.css", new CssRewriteUrlTransform())
                    .Include("~/Content/angular-material.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/material-datetimepicker.min.css", new CssRewriteUrlTransform())
            );

            bundles.Add(
              new ScriptBundle("~/Bundles/angular")
                  .Include(
                      "~/Scripts/angular/angular.min.js",
                      "~/Scripts/angular/angular-sanitize.min.js",
                      "~/Scripts/angular/angular-animate.min.js",
                      "~/Scripts/angular/angular-areas.js",
                      "~/Scripts/angular/angular-aria.min.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                      "~/Scripts/angular/angular-material.min.js",
                      "~/Scripts/angular/angular-cookies.min.js",
                      "~/Scripts/angular/bootstrap-colorpicker-module.min.js",
                      "~/Scripts/angular/ng-virtual-keyboard.js",
                      "~/Scripts/angular/ng-file-upload-shim.min.js",
                      "~/Scripts/angular/ng-file-upload.min.js",
                      "~/Scripts/angular/angular-material-datetimepicker.js",
                      "~/Scripts/angular/angular-dropdownMultiselect.min.js",
                      "~/Scripts/excel/ng-csv.js",
                      "~/Scripts/moment-with-locales.min.js",
                      "~/Scripts/daterangepicker.js"
                      ));
        }
    }
}
