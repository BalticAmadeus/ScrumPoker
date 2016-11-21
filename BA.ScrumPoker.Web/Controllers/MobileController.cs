using BA.ScrumPoker.Web.MemoryData;
using BA.ScrumPoker.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BA.ScrumPoker.Web.Controllers
{
    public class MobileController : Controller
    {
        public ActionResult Connect()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Connect(ClientModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Rooms.JoinRoom(model);

            return View("Room",  model );
        }


        public JsonResult CanVote(int roomId)
        {
            var canVote = Rooms.CanVote(new ClientModel { RoomId = roomId });
           
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = canVote
            };
        }

    }
}