using System.Web;
using System.Web.Optimization;

namespace BA.ScrumPoker
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
					 "~/Scripts/angular.min.js",
					 "~/Scripts/angular-qrcode.min.js",
					 "~/Scripts/loading-bar.js",
					 "~/Scripts/ngStorage.min.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					"~/Content/Voting.css",
					"~/Content/Room.css",
					"~/Content/bootstrap.css",
					"~/Content/site.css"));

			bundles.Add(new ScriptBundle("~/bundles/app")
				.IncludeDirectory("~/ScrumApp", "*.js", true));
		}
	}
}
