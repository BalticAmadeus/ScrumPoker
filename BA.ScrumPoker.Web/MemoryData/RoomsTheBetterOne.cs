using System;
using System.Collections.Generic;
using System.Linq;
using BA.ScrumPoker.Entities;
using BA.ScrumPoker.Utilities;

namespace BA.ScrumPoker.MemoryData
{
	public class RoomsTheBetterOne
	{

		private readonly object _sync;
		private readonly List<Room> _rooms;
		private readonly Random _random;

		private static RoomsTheBetterOne _instance;

		public static RoomsTheBetterOne Instance => _instance ?? (_instance = new RoomsTheBetterOne());

		public RoomsTheBetterOne()
		{
			_sync = new object();
			_random = new Random();
			_rooms = new List<Room>();
		}

		public Room CreateRoom()
		{
			lock (_sync)
			{
				for (var i = 10; i >= 0; i--)
				{
					var roomId = Base36Generator.GenerateString(6, _random);

					if (_rooms.Any(x => x.RoomId == roomId))
					{
						continue;
					}

					var room = new Room
					{
						RoomId = roomId,
						SecretKey = Base36Generator.GenerateString(13, _random)
					};

					_rooms.Add(room);
					room.StartVoting();
					return room;
				}
				return null;
			}
		}

		public Client JoinRoom(string userName, string roomId)
		{
			lock (_sync)
			{
				roomId = roomId.ToLowerInvariant();

				if (_rooms.All(x => x.RoomId != roomId))
				{
					return null;
				}

				var room = _rooms.Single(x => x.RoomId == roomId);

				Client client = null;

				for (var i = 10; i >= 0; i--)
				{
					var clientId = _random.Next();
					if (room.Clients.Any(x => x.ClientId == clientId))
					{
						continue;
					}

					client = new Client
					{
						ClientId = clientId,
						Name = userName,
						RoomId = roomId
					};

					break;
				}

				if (client == null)
				{
					return null;
				}

				room.Clients.Add(client);

				return client;
			}
		}

		public Room GetRoom(string roomId, string secretKey)
		{
			lock (_sync)
			{
				return _rooms.SingleOrDefault(x => x.RoomId == roomId && x.SecretKey == secretKey);
			}
		}


		public Room StartVoting(string roomId, string secretKey)
		{
			lock (_sync)
			{
				var room = _rooms.SingleOrDefault(x => x.RoomId == roomId && x.SecretKey == secretKey);

				if (room == null)
				{
					return null;
				}

				room.StartVoting();
				return room;
			}
		}

		public Room StopVoting(string roomId, string secretKey)
		{
			lock (_sync)
			{
				var room = _rooms.SingleOrDefault(x => x.RoomId == roomId && x.SecretKey == secretKey);

				if (room == null)
				{
					return null;
				}

				room.StopVoting();
				return room;
			}
		}

		public Client GetClient(string roomId, int clientId)
		{
			lock (_sync)
			{
				var room = _rooms.SingleOrDefault(x => x.RoomId == roomId);

				return room?.Clients.SingleOrDefault(x => x.ClientId == clientId);
			}
		}


		public Client Vote(string roomId, int clientId, int voteValue)
		{
			lock (_sync)
			{
				var room = _rooms.SingleOrDefault(x => x.RoomId == roomId);

				var client = room?.Clients.SingleOrDefault(x => x.ClientId == clientId);

				if (client == null)
				{
					return null;
				}

				client.VoteValue = voteValue;

				return client;
			}
		}

		public bool CanVote(string roomId)
		{
			lock (_sync)
			{
				var room = _rooms.SingleOrDefault(x => x.RoomId == roomId);

				if (room == null)
				{
					return false; // todo bad decision here...
				}

				return room.CanVote;
			}
		}

		public bool Kick(string roomId, string secretKey, int clientId)
		{
			lock (_sync)
			{
				var room = _rooms.SingleOrDefault(x => x.RoomId == roomId && x.SecretKey == secretKey);

				var client = room?.Clients.SingleOrDefault(x => x.ClientId == clientId);

				if (client == null)
				{
					return false;
				}

				room.Clients.Remove(client);

				return true;
			}
		}
	}
}