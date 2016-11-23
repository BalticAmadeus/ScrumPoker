using BA.ScrumPoker.Infrasturcture;
using BA.ScrumPoker.MemoryData;
using BA.ScrumPoker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BA.ScrumPoker.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index(int id)
        {
            return View();
        }

		public JsonResult Get()
		{
			var paramValue = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

			Int32 roomId;
			if (!Int32.TryParse(paramValue, out roomId))
			{
				return new JsonResult()
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new UiResponse<string>("Incorrect room Id")
				};
			}

            var room = Rooms.GetRoom(new RoomModel { RoomId = roomId });
			if(room == null)
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
			var paramValue = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

			var room = Rooms.StartVoting(model);
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

		public JsonResult GetClients(int roomId)
		{
			var paramValue = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

			var room = Rooms.GetRoom(new RoomModel { RoomId = roomId });
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
			var paramValue = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

			var room = Rooms.StopVoting(model);
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