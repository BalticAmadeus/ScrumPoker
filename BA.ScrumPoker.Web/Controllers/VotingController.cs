using System.Web.Mvc;

namespace BA.ScrumPoker.Controllers
{
	public class VotingController : Controller
	{
		// GET: Voting
		public ActionResult Index(int id) // todo remove later
		{
            ViewBag.ClientId = id;

            return View();
		}
	}
}