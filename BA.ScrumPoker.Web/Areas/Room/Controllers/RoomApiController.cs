using System;
using System.Web.Http;
using BA.ScrumPoker.Areas.Room.Models;
using BA.ScrumPoker.MemoryData;
using System.Linq;

namespace BA.ScrumPoker.Areas.Room.Controllers
{
	public class RoomApiController : ApiController
	{
		[HttpPost]
		[Route("api/Room/Kick")]
		public IHttpActionResult Kick(KickModel model)
		{
			var kicked = Rooms.Instance.Kick(model.RoomId, model.ClientId);

			if (kicked)
			{
				return Ok();
			}

			return BadRequest();
		}

		[HttpGet]
		[Route("api/Room/{roomId}")]
		public IHttpActionResult Room(string roomId)
		{
			var room = Rooms.Instance.GetRoom(roomId);

			if (room == null)
			{
				return NotFound();
			}

			var voters = room.Clients?.Select(item => new VoteModel
			{
				Id = item.Id,
				Name = item.Name,
				VoteValue = item.VoteValue
			}).ToList();

			double? avgScore = null;

			if (voters != null && voters.Any())
			{
				avgScore = voters.Where(x => x.VoteValue.HasValue).Average(x => x.VoteValue.Value);
			}

			var roomModel = new RoomItemModel // todo move to model -> convert method
			{
				CanVote = room.CanVote,
				AvgScore = avgScore,
				Voters = voters
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
