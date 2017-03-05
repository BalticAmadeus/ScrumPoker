using System;
using System.Web.Http;
using BA.ScrumPoker.Areas.KickClient.Models;
using BA.ScrumPoker.MemoryData;

namespace BA.ScrumPoker.Areas.KickClient.Controllers
{
    public class KickApiController : ApiController
    {

        private readonly IRoom _room = Rooms.Instance;

        [HttpPost]
        [Route("api/kick")]
        public IHttpActionResult Kick(KickClientModel model)
        {
            try
            {
                _room.Kick(model.RoomId, model.SecretKey, model.ClientId);

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}