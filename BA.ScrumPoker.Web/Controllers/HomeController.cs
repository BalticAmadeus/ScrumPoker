using System.Web.Mvc;

namespace BA.ScrumPoker.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.BaseUrl = 
				Request.Url.Scheme + "://" + 
				Request.Url.Authority + 
				Request.ApplicationPath.TrimEnd('/') + "/";

		    ViewBag.RoomId = Request.Params["roomId"];

			return View();
		}
	}
}