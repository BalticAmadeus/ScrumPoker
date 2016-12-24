namespace BA.ScrumPoker.Models
{
    public class ClientModel
    {
		public int Id { get; set; }
		public string Name { get; set; }
        public string RoomId { get; set; }
        public int? VoteValue { get; set; }

        public bool IHaveVoted
        {
            get
            {
                return VoteValue.HasValue;
            }
            set
            {
                // do nothing, leaving here to not confuse serializers (maybe)
            }
        }

    }
}