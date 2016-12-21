using System.Web.Mvc;

namespace BA.ScrumPoker.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index(string roomId)
		{
            ViewBag.RoomId = roomId;
			ViewBag.BaseUrl = 
				Request.Url.Scheme + "://" + 
				Request.Url.Authority + 
				Request.ApplicationPath.TrimEnd('/') + "/";

			return View();
		}
	}
}