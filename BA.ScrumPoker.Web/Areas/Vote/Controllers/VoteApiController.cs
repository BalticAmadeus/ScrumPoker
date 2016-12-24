using System.Web.Http;
using BA.ScrumPoker.MemoryData;
using BA.ScrumPoker.Models;

namespace BA.ScrumPoker.Areas.Vote.Controllers
{
	public class VoteApiController : ApiController
	{

		[HttpPost]
		[Route("api/StartVoting")]
		public IHttpActionResult StartVoting(string roomId)
		{
			var room = Rooms.Instance.StartVoting(roomId);

			return Voting(room);
		}

		[HttpPost]
		[Route("api/StopVoting")]
		public IHttpActionResult StopVoting(string roomId)
		{
			var room = Rooms.Instance.StopVoting(roomId);

			return Voting(room);
		}

		[HttpPost]
		[Route("api/Vote")]
		public IHttpActionResult Vote(ClientModel model)
		{
			ClientModel client = Rooms.Instance.Vote(model);

			if (client == null)
			{
				return BadRequest();
			}

			return Ok(client);
		}

		private IHttpActionResult Voting(RoomModel room)
		{
			if (room == null)
			{
				return BadRequest();
			}

			return Ok(room);
		}
	}
}
