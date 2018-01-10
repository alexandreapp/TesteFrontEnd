using System.Web;
using System.Web.Optimization;

namespace ProjetoWeb1
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/plugins/jQuery/jquery-2.2.3.min.js",
                        "~/plugins/datatables/jquery.dataTables.min.js",
                        "~/Scripts/jquery-{version}.js"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender com ela. Após isso, quando você estiver
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/plugins/datatables/dataTables.bootstrap.min.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/plugins/datatables/dataTables.bootstrap.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
