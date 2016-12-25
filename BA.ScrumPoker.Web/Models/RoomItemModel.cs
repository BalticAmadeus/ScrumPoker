using System.Collections.Generic;

namespace BA.ScrumPoker.Models
{
	public class RoomItemModel
	{
		public bool CanVote { get; set; }
		public double? AvgScore { get; set; }
		public List<ClientModel> Clients { get; set; }
	}
}