using Microsoft.AspNet.SignalR;
using BA.ScrumPoker.Models;

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