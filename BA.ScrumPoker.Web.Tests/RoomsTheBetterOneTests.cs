//using System.Linq;
//using BA.ScrumPoker.MemoryData;
//using NUnit.Framework;

//namespace BA.ScrumPoker.Web.Tests
//{
//	[TestFixture]
//	public class RoomsTheBetterOneTests
//	{

//		[Test]
//		public void CreateRoom_Should_Create_Room()
//		{
//			// arrange
//			var rooms = RoomsTheBetterOne.Instance;

//			// act
//			var room = rooms.CreateRoom();

//			// assert
//			Assert.IsNotNull(room);
//		}

//		[Test]
//		public void JoinRoom_Should_Create_Client_In_Room()
//		{
//			// arrange
//			var rooms = RoomsTheBetterOne.Instance;
//			var username = "TestUSername";

//			// act
//			var room = rooms.CreateRoom();
//			var client = rooms.Join(username, room.RoomId);

//			// assert
//			Assert.IsNotNull(client);
//			Assert.AreEqual(client.RoomId, room.RoomId);
//			Assert.AreEqual(client.Name, username);

//			Assert.AreEqual(room.Clients.First(), client);
//		}

//	}
//}
