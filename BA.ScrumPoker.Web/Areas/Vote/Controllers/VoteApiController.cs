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
