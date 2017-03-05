using System;
using System.Collections.Generic;
using System.Linq;
using BA.ScrumPoker.Entities;
using BA.ScrumPoker.Utilities;

namespace BA.ScrumPoker.MemoryData
{
    public class Rooms : IClient, IRoom
    {

        private readonly object _sync;
        private readonly List<Room> _rooms;
        private readonly Random _random;

        private static Rooms _instance;

        public static Rooms Instance => _instance ?? (_instance = new Rooms());

        public Rooms()
        {
            _sync = new object();
            _random = new Random();
            _rooms = new List<Room>();
        }

        #region room

        public Room StartVoting(string roomId, string secretKey)
        {
            lock (_sync)
            {
                var room = Get(roomId, secretKey);

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
                var room = Get(roomId, secretKey);

                if (room == null)
                {
                    return null;
                }

                room.StopVoting();
                return room;
            }
        }

        public Room Get(string roomId, string secretKey)
        {
            lock (_sync)
            {
                return _rooms.SingleOrDefault(x => x.RoomId == roomId && x.SecretKey == secretKey);
            }
        }

        public Room Get(string roomId)
        {
            lock (_sync)
            {
                return _rooms.SingleOrDefault(x => x.RoomId == roomId);
            }
        }

        public Room Create()
        {
            lock (_sync)
            {
                for (var i = 10; i >= 0; i--)
                {
                    var roomId = Base36Generator.GenerateString(6, _random);

                    if (RoomExist(roomId))
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

        public bool CanVote(string roomId)
        {
            lock (_sync)
            {
                var room = Get(roomId);

                if (room == null)
                {
                    throw new NullReferenceException(nameof(room));
                }

                return room.CanVote;
            }
        }

        public void Kick(string roomId, string secretKey, int clientId)
        {
            lock (_sync)
            {
                var room = Get(roomId, secretKey);

                var client = Get(roomId, clientId);

                if (client == null)
                {
                    return;
                }

                room.Clients.Remove(client);

            }
        }

        private bool RoomExist(string roomId)
        {
            return _rooms.Any(x => x.RoomId == roomId);
        }

        #endregion

        #region client

        private bool ClientExist(int clientId, string roomId)
        {
            var client = Get(roomId)?.Clients.Where(x => x.ClientId == clientId);

            if (client == null)
            {
                return false;
            }

            if (client.Any())
            {
                return true;
            }

            return false;
        }

        public Client Join(string userName, string roomId)
        {
            lock (_sync)
            {
                roomId = roomId.ToLowerInvariant();

                var room = Get(roomId);

                if (room == null)
                {
                    return null;
                }

                Client client = null;

                for (var i = 10; i >= 0; i--)
                {
                    var clientId = _random.Next();

                    if (ClientExist(clientId, roomId))
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

        public Client Get(string roomId, int clientId)
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

        #endregion
    }
}