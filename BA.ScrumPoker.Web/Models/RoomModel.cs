﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BA.ScrumPoker.Models
{
    public class RoomModel
    {
        public string RoomId { get; set; }
        public bool CanVote { get; private set; }
        public List<ClientModel> Clients { get; set; }

        public void StartVoting()
        {
            Clients.ForEach(c => c.VoteValue = null);
            CanVote = true;
        }

        public void StopVoting()
        {
            CanVote = false;
        }

        public RoomModel()
        {
            Clients = new List<ClientModel>();
        }
    }
}