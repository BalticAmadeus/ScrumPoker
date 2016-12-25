using BA.ScrumPoker.MemoryData;
using System.Collections.Generic;
using System.Web.Http;
using BA.ScrumPoker.Models;

namespace BA.ScrumPoker.Areas.Client.Controllers
{
	public class ClientApiController : ApiController
	{

		[HttpPost]
		[Route("api/client/vote")]
		public IHttpActionResult Client(ClientModel model)
		{
			var canVote = RoomsTheBetterOne.Instance.CanVote(model.RoomId);

			if (!canVote)
			{
				return Ok(); // todo return client data
			}

			if (!model.VoteValue.HasValue)
			{
				return BadRequest();
			}

			var client = RoomsTheBetterOne.Instance.Vote(model.RoomId, model.ClientId, model.VoteValue.Value);

			if (client == null)
			{
				return BadRequest();
			}

			return Ok();
		}

		[HttpGet]
		[Route("api/client/{roomId}/{clientId}")]
		public IHttpActionResult Client(string roomId, int clientId)
		{
			var client = RoomsTheBetterOne.Instance.GetClient(roomId, clientId);

			if (client == null)
			{
				return BadRequest();
			}

			var canVote = RoomsTheBetterOne.Instance.CanVote(client.RoomId);

			var clientItemModel = new List<VoteItemModel>();

			foreach (var item in GetFibonacci())
			{
				clientItemModel.Add(new VoteItemModel
				{
					Number = item,
					Selected = client.Voted && client.VoteValue.HasValue && client.VoteValue.Value == item
				});
			}

			var clientModel = new VoteModel
			{
				CanVote = canVote,
				Items = clientItemModel,
			};

			return Ok(clientModel);
		}

		private List<int> GetFibonacci()
		{
			return new List<int>() { 0, 1, 2, 3, 5, 8, 13, 21, 34 };
		}

	}
}
