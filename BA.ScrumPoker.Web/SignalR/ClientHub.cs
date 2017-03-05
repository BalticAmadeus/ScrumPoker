using BA.ScrumPoker.Areas.Room.Models;
using Microsoft.AspNet.SignalR;

namespace BA.ScrumPoker.SignalR
{
	public class ClientHub: Hub
	{
		public void Send(RoomModel room)
		{
			Clients.All.broadcastMessage(room);
		}
	}
}