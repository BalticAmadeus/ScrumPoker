using System;
using System.Collections.Generic;
using System.Web.Http;
using BA.ScrumPoker.MemoryData;
using System.Linq;
using BA.ScrumPoker.Models;

namespace BA.ScrumPoker.Areas.Room.Controllers
{
	public class RoomApiController : ApiController
	{

		[HttpGet]
		[Route("api/Room/{roomId}/{secretKey}")]
		public IHttpActionResult Room(string roomId, string secretKey)
		{
			var room = RoomsTheBetterOne.Instance.GetRoom(roomId, secretKey);

			if (room == null)
			{
				return NotFound();
			}

			var clients = new List<ClientModel>();

			foreach (var item in room.Clients.ToList())
			{
				clients.Add(new ClientModel
				{
					RoomId = item.RoomId,
					ClientId = item.ClientId,
					VoteValue = item.VoteValue,
					Name = item.Name
				});
			}

			double? avgScore = null;

			if (clients.Any())
			{
				if (clients.Count() == 1)
				{
					avgScore = clients.First().VoteValue;
				}
				else
				{
					var clientsWithValue = clients.Where(x => x.VoteValue.HasValue).ToList();

					if (clientsWithValue.Any())
					{
						avgScore = clientsWithValue.Average(x => x.VoteValue.Value);
					}
				}
			}

			var roomModel = new RoomItemModel // todo move to model -> convert method
			{
				CanVote = room.CanVote,
				AvgScore = avgScore,
				Clients = clients
			};

			return Ok(roomModel);
		}

		[HttpPost]
		[Route("api/room/stopVoting")]
		public IHttpActionResult StopVoting(RoomModel model)
		{
			return StartStopVoting(model, false);
		}

		[HttpPost]
		[Route("api/room/startVoting")]
		public IHttpActionResult StartVoting(RoomModel model)
		{
			return StartStopVoting(model, true);
		}

		private IHttpActionResult StartStopVoting(RoomModel model, bool start)
		{
			try
			{
				if (start)
				{
					RoomsTheBetterOne.Instance.StartVoting(model.RoomId, model.SecretKey);
				}
				else
				{
					RoomsTheBetterOne.Instance.StopVoting(model.RoomId, model.SecretKey);
				}

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("api/Room/Kick")]
		public IHttpActionResult Kick(KickClientModel model)
		{
			var kicked = RoomsTheBetterOne.Instance.Kick(model.RoomId, model.SecretKey, model.ClientId);

			if (kicked)
			{
				return Ok();
			}

			return BadRequest();
		}

	}
}
