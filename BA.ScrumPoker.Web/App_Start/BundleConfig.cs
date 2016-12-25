using System.Web;
using System.Web.Optimization;

namespace BA.ScrumPoker
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/css").Include(
					"~/Content/Voting.css",
					"~/Content/Room.css",
					"~/Content/bootstrap.css",
					"~/Content/site.css"));

			bundles.Add(new ScriptBundle("~/bundles/angularAndFriends").Include(
					"~/Scripts/jquery-{version}.js",
					"~/Scripts/modernizr-*",
					"~/Scripts/bootstrap.js",
					"~/Scripts/respond.js",
					"~/Scripts/angular.min.js",
					"~/Scripts/angular-qrcode.min.js",
					"~/Scripts/loading-bar.js",
					"~/Scripts/ngStorage.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory(
					"~/ScrumApp", "*.js", true));
		}
	}
}
