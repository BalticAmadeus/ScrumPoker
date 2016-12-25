using System.Collections.Generic;

namespace BA.ScrumPoker.Entities
{
	public class Room
	{
		public Room()
		{
			Clients = new List<Client>();
		}

		public string RoomId { get; set; }
		public string SecretKey { get; set; }
		public bool CanVote { get; private set; }
		public List<Client> Clients { get; set; }

		public void StartVoting()
		{
			Clients.ForEach(c => c.VoteValue = null);
			CanVote = true;
		}

		public void StopVoting()
		{
			CanVote = false;
		}
	}
}