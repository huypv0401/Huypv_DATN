using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
using System.Web.Optimization;

namespace ASP.NET_MVC5_Bootstrap3_3_1_LESS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            var cssTransformer = new StyleTransformer();
            var jsTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            var cssBundle = new StyleBundle("~/bundles/css");
            cssBundle.Include("~/Content/bootstrap.css",
                        "~/Content/Style.css",
                        "~/Content/bootstrap-responsive.min.css",
                        "~/Content/bootstrap-theme.css",
                        "~/Content/bootstrap-theme.min.css",
                        "~/Content/bootstrap.min.css",
                     "~/Content/bootstrap-responsive.css",
                     "~/Content/HomePage.css",
                     "~/Content/jquery-ui-1.8.21.custom.css",
                     "~/Content/jqModal.css",
                     "~/Content/jquery.jqplot.min.css");
            cssBundle.Transforms.Add(cssTransformer);
            cssBundle.Orderer = nullOrderer;
            bundles.Add(cssBundle);

            var jqueryBundle = new ScriptBundle("~/bundles/jquery");
            jqueryBundle.Include("~/Scripts/jquery-{version}.js", "~/Scripts/jquery.infinitescroll.js", "~/Scripts/jquery.infinitescroll.min.js");
            jqueryBundle.Transforms.Add(jsTransformer);
            jqueryBundle.Orderer = nullOrderer;
            bundles.Add(jqueryBundle);

            var jqueryvalBundle = new ScriptBundle("~/bundles/jqueryval");
            jqueryvalBundle.Include("~/Scripts/jquery.validate-vsdoc.js",
                                    "~/Scripts/jquery.validate.js",
                                    "~/Scripts/jquery.validate.min.js",
                                    "~/Scripts/jquery.validate.unobtrusive.js",
                                    "~/Scripts/jquery.validate.unobtrusive.min.js");
            jqueryvalBundle.Transforms.Add(jsTransformer);
            jqueryvalBundle.Orderer = nullOrderer;
            bundles.Add(jqueryvalBundle);


            //<script src="~/Scripts/jquery.validate-vsdoc.js"></script>
            //<script src="~/Scripts/jquery.validate.js"></script>
            //<script src="~/Scripts/jquery.validate.min.js"></script>
            //<script src="~/Scripts/jquery.validate.unobtrusive.js"></script>
            //<script src="~/Scripts/jquery.validate.unobtrusive.min.js"></script>
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            var modernizrBundle = new ScriptBundle("~/bundles/modernizr");
            modernizrBundle.Include("~/Scripts/modernizr-*");
            modernizrBundle.Transforms.Add(jsTransformer);
            modernizrBundle.Orderer = nullOrderer;
            bundles.Add(modernizrBundle);

            var bootstrapBundle = new ScriptBundle("~/bundles/bootstrap");
            bootstrapBundle.Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js");
            bootstrapBundle.Transforms.Add(jsTransformer);
            bootstrapBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapBundle);
        }
    }
}