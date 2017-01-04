using BA.ScrumPoker.MemoryData;
using BA.ScrumPoker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IO = System.IO;

namespace BA.ScrumPoker.Controllers
{
	public class HomeController : Controller
	{
        public const string OfficialVersion = "1";

        public static string VersionString { get { return _versionString;  } }
        private static string _versionString = GetVersionString();

        private static string GetVersionString()
        {
            var s = new StringBuilder();
            s.Append("v").Append(OfficialVersion);
#if DEBUG
            s.Append("+ UNSTABLE");
#endif
            // We don't set build numbers so this is a workaround: hash the dll itself
            try
            {
                string fileName = typeof(HomeController).Assembly.Location;
                if (!IO.File.Exists(fileName))
                    throw new IO.FileNotFoundException();
                var md = HashAlgorithm.Create("SHA1");
                byte[] hash;
                using (IO.FileStream fs = IO.File.Open(fileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read))
                {
                    hash = md.ComputeHash(fs);
                }
                string code = BitConverter.ToString(hash).Replace("-", "").Substring(0, 8).ToLower();
                s.Append(" build ").Append(code);
            }
            catch (Exception)
            {
                // Best effort
            }
            return s.ToString();
        }

		public ActionResult Index(string roomId)
		{
            ViewBag.RoomId = roomId;
            ViewBag.VersionString = VersionString;
            return View();
		}

		public ActionResult CreateRoom()
		{
            var room = Rooms.Instance.CreateRoom();

            if (room == null)
            {
                ModelState.AddModelError("Error", "Failed to create the room");
            }

            return RedirectToActionPermanent("Index", "Room", new { id = room.RoomId });
		}

		public ActionResult Join(string userName, string roomId)
		{
            var client = Rooms.Instance.JoinRoom(userName, roomId);

			if (client == null)
			{
				ModelState.AddModelError("Error", "Failed to join the room");
			}

			return RedirectToActionPermanent("Index", "Voting", new { id = client.Id });
		}
	}
}