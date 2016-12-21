using System.Web.Http;
using BA.ScrumPoker.Areas.Room.Models;
using BA.ScrumPoker.MemoryData;

namespace BA.ScrumPoker.Areas.Room.Controllers
{
	public class RoomApiController : ApiController
	{

		[HttpPut]
		[Route("api/Room")]
		public IHttpActionResult Room(JoinRoomModel model)
		{
			var client = Rooms.Instance.JoinRoom(model.Username, model.RoomId);

			if (client == null)
			{
				return NotFound();
			}

			return Ok(client.Id);
		}

		[HttpPost]
		[Route("api/Room")]
		public IHttpActionResult Room()
		{
			var room = Rooms.Instance.CreateRoom();

			if (room == null)
			{
				return BadRequest();
			}

			return Ok(room.RoomId);
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

			return Ok(room);
		}
	}
}
