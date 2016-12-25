using System;
using System.Web.Http;
using BA.ScrumPoker.MemoryData;
using BA.ScrumPoker.Models;

namespace BA.ScrumPoker.Areas.Home.Controllers
{
	public class HomeApiController : ApiController
	{
		[HttpPut]
		[Route("api/Home/JoinRoom")]
		public IHttpActionResult JoinRoom(JoinRoomModel model)
		{
			try
			{
				var client = RoomsTheBetterOne.Instance.JoinRoom(model.Username, model.RoomId);

				if (client == null)
				{
					return NotFound();
				}

				var clientModel = new ClientModel
				{
					RoomId = client.RoomId,
					ClientId = client.ClientId
				};

				return Ok(clientModel);

			}
			catch (Exception) // todo catch them all
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("api/Home/CreateRoom")]
		public IHttpActionResult CreateRoom()
		{
			try
			{
				var room = RoomsTheBetterOne.Instance.CreateRoom();

				if (room == null)
				{
					return BadRequest();
				}

				return Ok(new RoomModel
				{
					RoomId = room.RoomId,
					SecretKey = room.SecretKey
				});
			}
			catch (Exception) // todo catch them all
			{
				return BadRequest();
			}
		}
	}
}
