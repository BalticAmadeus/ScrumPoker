using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BA.ScrumPoker.Areas.Room.Models;
using BA.ScrumPoker.MemoryData;

namespace BA.ScrumPoker.Areas.Room.Controllers
{
    public class RoomApiController : ApiController
    {

        private readonly IRoom _room = Rooms.Instance;

        [HttpGet]
        [Route("api/Room")]
        public IHttpActionResult GetRoom(string roomId, string secretKey)
        {
            var room = _room.Get(roomId, secretKey);

            if (room == null)
            {
                return NotFound();
            }

            var clients = RoomClientModel.Convert(room.Clients);

            return Ok(RoomModel.Convert(room.CanVote, GetAvgScore(clients), clients));
        }

        [HttpPut]
        [Route("api/Room")]
        public IHttpActionResult UpdateRoom(RoomModel model)
        {
            try
            {
                if (model.Voting)
                {
                    Rooms.Instance.StartVoting(model.RoomId, model.SecretKey);
                }
                else
                {
                    Rooms.Instance.StopVoting(model.RoomId, model.SecretKey);
                }

                return Ok(RoomModel.Convert(_room.Get(model.RoomId, model.SecretKey)));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/Room")]
        public IHttpActionResult CreateRoom()
        {
            try
            {
                var room = _room.Create();

                if (room == null)
                {
                    return BadRequest();
                }

                return Ok(RoomModel.Convert(room));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private double? GetAvgScore(List<RoomClientModel> clients)
        {

            double? avgScore = null;

            if (!clients.Any())
            {
                return avgScore;
            }

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

            return avgScore;
        }

    }
}