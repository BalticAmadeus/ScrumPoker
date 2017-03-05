using System;
using System.Web.Http;
using BA.ScrumPoker.MemoryData;
using System.Collections.Generic;
using System.Linq;
using BA.ScrumPoker.Areas.Client.Models;

namespace BA.ScrumPoker.Areas.Client.Controllers
{
    public class ClientApiController : ApiController
    {
        private readonly IClient _client = Rooms.Instance;
        private readonly IRoom _room = Rooms.Instance;

        [HttpPost]
        [Route("api/Client")]
        public IHttpActionResult JoinRoom(ClientModel model)
        {
            try
            {
                var client = _client.Join(model.Name, model.RoomId);

                if (client == null)
                {
                    return NotFound();
                }

                model = ClientModel.Convert(client);

                model.CanVote = _room.CanVote(model.RoomId);

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Client")]
        public IHttpActionResult UpdateClient(ClientModel model)
        {
            if (model.VoteValue == null)
            {

                return BadRequest();
            }

            if (_room.CanVote(model.RoomId))
            {
                _client.Vote(model.RoomId, model.ClientId, model.VoteValue.Value);
            }

            return Ok();
        }

        [HttpGet]
        [Route("api/Client")]
        public IHttpActionResult GetClient(string roomId, int clientId)
        {
            var client = _client.Get(roomId, clientId);

            if (client == null)
            {
                return BadRequest();
            }

            var clientModel = ClientModel.Convert(client);

            clientModel.CanVote = Rooms.Instance.CanVote(client.RoomId);
            clientModel.VoteOptions = GetVoteOptions(client);

            return Ok(clientModel);
        }

        #region private methods

        private List<ClientVoteOptionModel> GetVoteOptions(Entities.Client client)
        {
            return GetFibonacci().Select(item => new ClientVoteOptionModel
            {
                Number = item,
                Selected = client.Voted && client.VoteValue.HasValue && client.VoteValue.Value == item
            }).ToList();
        }

        private List<int> GetFibonacci()
        {
            return new List<int>() { 0, 1, 2, 3, 5, 8, 13, 21, 34 };
        }

        #endregion
    }
}