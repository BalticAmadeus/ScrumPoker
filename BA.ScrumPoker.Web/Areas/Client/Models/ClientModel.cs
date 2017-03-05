using System.Collections.Generic;
using BaClient = BA.ScrumPoker.Entities.Client;

namespace BA.ScrumPoker.Areas.Client.Models
{
	public class ClientModel
	{
		public int ClientId { get; set; }
		public string RoomId { get; set; }
		public string Name { get; set; }
		public int? VoteValue { get; set; }
        public bool CanVote { get; set; }
        public List<ClientVoteOptionModel> VoteOptions { get; set; }

	    public static ClientModel  Convert(BaClient client)
	    {
	        return new ClientModel
	        {
                ClientId = client.ClientId,
                RoomId = client.RoomId,
                Name = client.Name,
                VoteValue = client.VoteValue
	        };
	    }
	}
}