using System.Web.Mvc;

namespace BA.ScrumPoker.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index(string id)
        {
            ViewBag.RoomId = id;
            ViewBag.BaseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
            return View();
        }
	}
}