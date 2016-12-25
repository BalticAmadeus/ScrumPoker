using System.Collections.Generic;

namespace BA.ScrumPoker.Models
{
	public class VoteModel
	{
		public bool CanVote { get; set; }
		public List<VoteItemModel> Items { get; set; }
	}
}