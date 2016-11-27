using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BA.ScrumPoker.Models;

using BA.ScrumPoker.Utilities;
namespace BA.ScrumPoker.MemoryData
{
    public static class Rooms
    {

        private static List<RoomModel> AvailableRooms { get; set; }

        private static void Init()
        {
            if (AvailableRooms == null)
                AvailableRooms = new List<RoomModel>();

        }

        public static List<RoomModel> GetRooms()
        {
            Init();
            return AvailableRooms;
        }

        private static string GetUniqueRoomId()
        {
            Random rd = new Random();
            var roomNumber = Base36Generator.GenerateString(length: 8);

            if (Rooms.GetRooms().Any(x => x.RoomId == roomNumber))
                return GetUniqueRoomId();

            return roomNumber;

        }

        public static RoomModel AddRoom()
        {
            Init();

            var newRoom = new RoomModel
            {
                RoomId = GetUniqueRoomId(),
                Clients = new List<ClientModel>()
            };

            AvailableRooms.Add(newRoom);


            return newRoom;
        }

        public static RoomModel GetRoom(RoomModel model)
        {
            Init();

            var room = AvailableRooms.FirstOrDefault(x => x.RoomId == model.RoomId);
            return room;
        }

        public static RoomModel StartVoting(RoomModel room)
        {
            room = Rooms.GetRoom(room);
            room.StartVoting();
            return room;
        }

        public static RoomModel StopVoting(RoomModel room)
        {
            room = Rooms.GetRoom(room);
            room.StopVoting();
            return room;
        }

        public static RoomModel JoinRoom(ClientModel model)
        {
            Init();

            var room = AvailableRooms.FirstOrDefault(x => x.RoomId == model.RoomId);
            if (room == null)
                return null;

            Random rd = new Random();
            model.Id = rd.Next(9999);
            room.Clients.Add(model);

            return room;
        }

        public static RoomModel RemoveUser(ClientModel model)
        {
            Init();

            var room = AvailableRooms.FirstOrDefault(x => x.RoomId == model.RoomId);
            if (room == null)
                return null;

            room.Clients.Remove(model);
            return room;
        }

        public static void Vote(ClientModel model)
        {
            Init();

            var room = AvailableRooms.FirstOrDefault(x => x.RoomId == model.RoomId);
            if (room == null)
                return;

            var user = room.Clients.FirstOrDefault(x => x.Id == model.Id);
            if (user == null)
                return;

            user.VoteValue = model.VoteValue;

        }

        public static bool CanVote(ClientModel model)
        {
            Init();

            var room = AvailableRooms.FirstOrDefault(x => x.RoomId == model.RoomId);
            if (room == null)
                return false;

            return room.CanVote;
        }

        public static ClientModel GetClient(int clientId)
        {
            var user = AvailableRooms.SelectMany(x => x.Clients).SingleOrDefault(x => x.Id == clientId);
            return user;
        }
    }
}