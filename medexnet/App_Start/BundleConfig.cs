using System.Web;
using System.Web.Optimization;

namespace medexnet
{
    public class BundleConfig
    {    
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/js/jquery.min.js",
                "~/Scripts/js/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/extras").Include(              
                "~/Scripts/js/waves.js",
                "~/Scripts/js/perfect-scrollbar.jquery.min.js",
                "~/Scripts/js/sidebarmenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/calendar").Include(                            
                "~/Scripts/js/moment.min.js",
                "~/Scripts/js/fullcalendar.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/js/bootstrap.min.js",             
                "~/Scripts/js/custom.min.js",
                "~/Scripts/js/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/js/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/calendarcss").Include(
                "~/Scripts/css/calendar.css",
                "~/Scripts/css/fullcalendar.min.css"));

            bundles.Add(new StyleBundle("~/bundles/style").Include(
                "~/Scripts/css/style.min.css"));
        }
    }
}
