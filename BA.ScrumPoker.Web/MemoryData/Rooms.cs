using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BA.ScrumPoker.Models;
using BA.ScrumPoker.Utilities;

namespace BA.ScrumPoker.MemoryData
{
    public class Rooms
    {
        private static Rooms _instance;

        public static Rooms Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Rooms();
                return _instance;
            }
        }

        private object _sync;
        private Dictionary<string, RoomModel> _rooms;
        private Dictionary<int, ClientModel> _clients;
        private Random _rng;

        public Rooms()
        {
            _sync = new object();
            _rooms = new Dictionary<string, RoomModel>();
            _clients = new Dictionary<int, ClientModel>();
            _rng = new Random();
        }

        public RoomModel CreateRoom()
        {
            lock (_sync)
            {
                for (int i = 10; i >= 0; i--)
                {
                    string roomId = Base36Generator.GenerateString(6, _rng);
                    if (_rooms.ContainsKey(roomId))
                        continue;
                    var room = new RoomModel
                    {
                        RoomId = roomId,
                    };
                    _rooms[roomId] = room;
                    room.StartVoting();
                    return room;
                }
                return null;
            }
        }

        public ClientModel JoinRoom(string userName, string roomId)
        {
            lock (_sync)
            {
                RoomModel room;
                roomId = roomId.ToLowerInvariant();
                if (!_rooms.TryGetValue(roomId, out room))
                    return null;
                ClientModel client = null;
                for (int i = 10; i >= 0; i--)
                {
                    int clientId = _rng.Next();
                    if (_clients.ContainsKey(clientId))
                        continue;
                    client = new ClientModel
                    {
                        Id = clientId,
                        Name = userName,
                        RoomId = roomId
                    };
                    break;
                }
                if (client == null)
                    return null;
                room.Clients.Add(client);
                _clients[client.Id] = client;
                return client;
            }
        }

        public RoomModel GetRoom(string roomId)
        {
            lock (_sync)
            {
                RoomModel room;
                if (!_rooms.TryGetValue(roomId, out room))
                    return null;
                return room;
            }
        }

        public RoomModel StartVoting(string roomId)
        {
            lock (_sync)
            {
                RoomModel room;
                if (!_rooms.TryGetValue(roomId, out room))
                    return null;
                room.StartVoting();
                return room;
            }
        }

        public RoomModel StopVoting(string roomId)
        {
            lock (_sync)
            {
                RoomModel room;
                if (!_rooms.TryGetValue(roomId, out room))
                    return null;
                room.StopVoting();
                return room;
            }
        }

        public ClientModel GetClient(int clientId)
        {
            lock (_sync)
            {
                ClientModel client;
                if (!_clients.TryGetValue(clientId, out client))
                    return null;
                return client;
            }
        }

        public bool CanVote(ClientModel client)
        {
            lock (_sync)
            {
                RoomModel room;
                if (!_rooms.TryGetValue(client.RoomId, out room))
                    return false;
                return room.CanVote;
            }
        }

        public ClientModel Vote(ClientModel vote)
        {
            lock (_sync)
            {
                ClientModel client;
                if (!_clients.TryGetValue(vote.Id, out client))
                    return null;
                client.VoteValue = vote.VoteValue;
                return client;
            }
        }

	    public ClientModel Vote(int clientId, int voteValue)
	    {
			lock (_sync)
			{
				ClientModel client;
				if (!_clients.TryGetValue(clientId, out client))
					return null;
				client.VoteValue = voteValue;
				return client;
			}
		}

		public bool CanVote(string roomId)
		{
			lock (_sync)
			{
				RoomModel room;
				if (!_rooms.TryGetValue(roomId, out room))
					return false;
				return room.CanVote;
			}
		}

	}
}