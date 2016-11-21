using BA.ScrumPoker.Web.MemoryData;
using BA.ScrumPoker.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BA.ScrumPoker.Web.Controllers
{
    public class RoomController : Controller
    {
        public ActionResult CreateRoom()
        {
            var room = Rooms.AddRoom(); ;
            return RedirectToAction("Room", new { roomId = room .RoomId});
        }

        public ActionResult Room(int roomId)
        {
            var model = Rooms.GetRoom(new RoomModel { RoomId = roomId });
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Vote(ClientModel model)
        {
            var canVote = Rooms.CanVote(model);
            if (!canVote)
            {
                ModelState.AddModelError("Error", "Voting has not started yet!");
                model.Estimation = 0;
                return View("~/Views/Mobile/Room.cshtml", model);
            }
            Rooms.Vote(model);
            return View("~/Views/Mobile/Room.cshtml", model);
        }
                
        public JsonResult GetRoom(RoomModel model)
        {

            var room = Rooms.GetRoom(model);
            if (room == null)
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = null
                };

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = room
            };
        }
       
        public ActionResult StopVoting(int roomId)
        {
            var room = Rooms.StopVoting(new RoomModel { RoomId = roomId });
            return View("Room", room);
        }
        
        public ActionResult StartVoting(int roomId)
        {
            var room = Rooms.StartVoting(new RoomModel { RoomId = roomId });
            return View("Room", room);
        }       

    }
}