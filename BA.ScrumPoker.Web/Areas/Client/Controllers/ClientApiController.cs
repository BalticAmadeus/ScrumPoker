using BA.ScrumPoker.MemoryData;
using System.Collections.Generic;
using System.Web.Http;
using BA.ScrumPoker.Areas.Client.Models;

namespace BA.ScrumPoker.Areas.Client.Controllers
{
	public class ClientApiController : ApiController
	{

		[HttpPost]
		[Route("api/client/vote")]
		public IHttpActionResult Client(ClientModel model)
		{
			var canVote = Rooms.Instance.CanVote(model.RoomId);

			if (!canVote)
			{
				return Ok(); // todo return client data
			}

			var client = Rooms.Instance.Vote(model.ClientId, model.Number);

			if (client == null)
			{
				return BadRequest();
			}

			return Ok();
		}

		[HttpGet]
		[Route("api/client/{clientId}")]
		public IHttpActionResult Client(int clientId)
		{
			var client = Rooms.Instance.GetClient(clientId);

			if (client == null)
			{
				return BadRequest();
			}

			var canVote = Rooms.Instance.CanVote(client.RoomId);

			var clientItemModel = new List<ClientItemModel>();

			foreach (var item in GetFibonacci())
			{
				clientItemModel.Add(new ClientItemModel
				{
					Number = item,
					Selected = client.IHaveVoted && client.VoteValue.HasValue && client.VoteValue.Value == item
				});
			}

			var clientModel = new ClientDetailsModel
			{
				CanVote = canVote,
				Items = clientItemModel,
				RoomId = client.RoomId
			};

			return Ok(clientModel);
		}

		private List<int> GetFibonacci()
		{
			return new List<int>() { 0, 1, 2, 3, 5, 8, 13, 21, 34 };
		}

	}
}
