using System.Collections.Generic;
using System.Linq;

namespace BA.ScrumPoker.Areas.Room.Models
{
    public class RoomClientModel
    {
        public string Name { get; set; }
        public int ClientId { get; set; }
        public bool Voted { get; set; }
        public int? VoteValue { get; set; }

        public static RoomClientModel Convert(Entities.Client client)
        {
            return new RoomClientModel
            {
                ClientId = client.ClientId,
                VoteValue = client.VoteValue,
                Name = client.Name,
                Voted = client.Voted
            };
        }

        public static List<RoomClientModel> Convert(List<Entities.Client> clients)
        {
            return clients.Select(Convert).ToList();
        }
    }
}