﻿using BA.ScrumPoker.Infrasturcture;
using BA.ScrumPoker.MemoryData;
using BA.ScrumPoker.Models;
using System.Collections.Generic;
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

		public JsonResult Get(string id)
		{
            var room = Rooms.Instance.GetRoom(id);

			if (room == null)
			{
				return new JsonResult()
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new UiResponse<string>("Room not found")
				};
			}

			return new JsonResult()
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = new UiResponse<RoomModel>(room)
			};
		}

		public JsonResult StartVoting(RoomModel model)
		{
			var room = Rooms.Instance.StartVoting(model?.RoomId);

			if (room == null)
			{
				return new JsonResult()
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new UiResponse<string>("Failed to start voting")
				};
			}

			return new JsonResult()
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = new UiResponse<RoomModel>(room)
			};
		}

		public JsonResult GetClients(string roomId)
		{
			var room = Rooms.Instance.GetRoom(roomId);

			if (room == null)
			{
				return new JsonResult()
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new UiResponse<string>("Room not found")
				};
			}

			return new JsonResult()
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = new UiResponse<List<ClientModel>>(room.Clients)
			};
		}

		public JsonResult StopVoting(RoomModel model)
		{
			var room = Rooms.Instance.StopVoting(model?.RoomId);

			if (room == null)
			{
				return new JsonResult()
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new UiResponse<string>("Room not found")
				};
			}

			return new JsonResult()
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = new UiResponse<RoomModel>(room)
			};
		}
	}
}