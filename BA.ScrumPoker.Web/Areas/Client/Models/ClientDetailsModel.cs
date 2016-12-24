using System.Collections.Generic;

namespace BA.ScrumPoker.Areas.Client.Models
{
	public class ClientDetailsModel
	{
		public bool CanVote { get; set; }
		public string RoomId { get; set; }
		public List<ClientItemModel> Items { get; set; }
	} 
}