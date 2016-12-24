using System;
using System.Web.Http;
using BA.ScrumPoker.Areas.Home.Models;
using BA.ScrumPoker.MemoryData;

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
			    var client = Rooms.Instance.JoinRoom(model.Username, model.RoomId);

			    if (client == null)
			    {
				    return NotFound();
			    }

				var clientModel = new Models.ClientModel
				{
					RoomId = client.RoomId,
					ClientId = client.Id
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
				var room = Rooms.Instance.CreateRoom();

			    if (room == null)
			    {
				    return BadRequest();
			    }

				return Ok(room.RoomId);
			}
			catch (Exception) // todo catch them all
		    {
			    return BadRequest();
		    }
		}
    }
}
