using System.Collections.Generic;

namespace BA.ScrumPoker.Areas.Room.Models
{
	public class RoomItemModel
	{
		public bool CanVote { get; set; }

		public List<VoteModel> Votes { get; set; }
	}
}