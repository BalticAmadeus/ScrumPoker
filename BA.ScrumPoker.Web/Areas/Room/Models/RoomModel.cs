using System;
using System.Collections.Generic;

namespace BA.ScrumPoker.Areas.Room.Models
{
    public class RoomModel
    {
        public string RoomId { get; set; }
        public string SecretKey { get; set; }
        public bool Voting { get; set; }
        public bool CanVote { get; set; }
        public double? AvgScore { get; set; }
        public List<RoomClientModel> Clients { get; set; }

        public static RoomModel Convert(Entities.Room room)
        {
            return new RoomModel
            {
                RoomId = room.RoomId,
                SecretKey = room.SecretKey,
                Voting = room.CanVote
            };
        }

        public static RoomModel Convert(bool canVote, double? avgScore, List<RoomClientModel> clients)
        {
            return new RoomModel 
            {
                CanVote = canVote,
                AvgScore = avgScore,
                Clients = clients
            };
        }
    }
}