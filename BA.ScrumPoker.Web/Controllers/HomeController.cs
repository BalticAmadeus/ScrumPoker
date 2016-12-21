using BA.ScrumPoker.MemoryData;
using System.Web.Mvc;

namespace BA.ScrumPoker.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index(string roomId)
		{
            ViewBag.RoomId = roomId;
            return View();
		}

		[HttpPost]
		public ActionResult CreateRoom()
		{
            var room = Rooms.Instance.CreateRoom();

            if (room == null)
            {
                ModelState.AddModelError("createRoomError", "Failed to create the room");
	            return View("Index");
			}

			return RedirectToActionPermanent("Index", "Room", new { id = room.RoomId });
		}

		[HttpPost]
		public ActionResult Join(string userName, string roomId)
		{
            var client = Rooms.Instance.JoinRoom(userName, roomId);

			if (client == null)
			{
				ModelState.AddModelError("joinRoomError", "Failed to join the room");
	            return View("Index");
			}

			return RedirectToActionPermanent("Index", "Voting", new { id = client.Id });
		}
	}
}