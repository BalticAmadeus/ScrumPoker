using System;
using System.Web.Http;
using BA.ScrumPoker.Areas.Room.Models;
using BA.ScrumPoker.MemoryData;
using System.Linq;

namespace BA.ScrumPoker.Areas.Room.Controllers
{
	public class RoomApiController : ApiController
	{

		[HttpGet]
		[Route("api/Room/{roomId}")]
		public IHttpActionResult Room(string roomId)
		{
			var room = Rooms.Instance.GetRoom(roomId);

			if (room == null)
			{
				return NotFound();
			}

			var roomModel = new RoomItemModel // todo move to model -> convert method
			{
				CanVote = room.CanVote,
				Votes = room.Clients.Select(item => new VoteModel
				{
					Id = item.Id,
					Name = item.Name,
					VoteValue = item.VoteValue
				}).ToList()
			};

			return Ok(roomModel);
		}

		[HttpPost]
		[Route("api/room/stopVoting")]
		public IHttpActionResult StopVoting(RoomModel model)
		{
			try
			{
				Rooms.Instance.StopVoting(model.RoomId);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("api/room/startVoting")]
		public IHttpActionResult StartVoting(RoomModel model)
		{
			try
			{
				Rooms.Instance.StartVoting(model.RoomId);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
	}
}
