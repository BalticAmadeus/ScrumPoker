namespace BA.ScrumPoker.Entities
{
	public class Client
	{
		public int ClientId { get; set; }
		public string SecretKey { get; set; } // todo implement later
		public string Name { get; set; }
		public string RoomId { get; set; }
		public int? VoteValue { get; set; }

		public bool Voted => VoteValue.HasValue;
	}
}