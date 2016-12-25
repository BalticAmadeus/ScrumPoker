namespace BA.ScrumPoker.Models
{
	public class ClientModel
	{
		public int ClientId { get; set; }
		public string RoomId { get; set; }
		public string Name { get; set; }
		public int? VoteValue { get; set; }
	}
}