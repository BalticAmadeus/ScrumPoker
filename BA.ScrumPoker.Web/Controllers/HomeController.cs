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
		public ActionResult Index(string roomId)
		{
            ViewBag.RoomId = roomId;
            return View();
		}

		public ActionResult CreateRoom()
		{
            var room = Rooms.Instance.CreateRoom();

            if (room == null)
            {
                ModelState.AddModelError("Error", "Failed to create the room");
            }

            return RedirectToActionPermanent("Index", "Room", new { id = room.RoomId });
		}

		public ActionResult Join(string userName, string roomId)
		{
            var client = Rooms.Instance.JoinRoom(userName, roomId);

			if (client == null)
			{
				ModelState.AddModelError("Error", "Failed to join the room");
			}

			return RedirectToActionPermanent("Index", "Voting", new { id = client.Id });
		}
	}
}