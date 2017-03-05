namespace BA.ScrumPoker.Entities
{
	public class Client
	{
		public int ClientId { get; set; }
		public string Name { get; set; }
		public string RoomId { get; set; }
		public int? VoteValue { get; set; }
		public bool Voted => VoteValue.HasValue;
	}
}