using BA.ScrumPoker.MemoryData;
using BA.ScrumPoker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BA.ScrumPoker.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult CreateRoom()
		{
            var roomResponse = Rooms.AddRoom();

			return RedirectToActionPermanent("Index", "Room", new { id = roomResponse.RoomId });
		}

		public ActionResult Join(string userName, int roomId)
		{
            var user = new ClientModel()
            {
                Name = userName,
                RoomId = roomId
            };
            var room = Rooms.JoinRoom(user);

			if (room == null)
			{
				ModelState.AddModelError("Error", "Failed to join the room");
			}

			return RedirectToActionPermanent("Index", "Voting", new { id = user.Id });
		}
	}
}