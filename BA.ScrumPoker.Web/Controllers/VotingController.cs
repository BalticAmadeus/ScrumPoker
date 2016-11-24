﻿using BA.ScrumPoker.Infrasturcture;
using BA.ScrumPoker.MemoryData;
using BA.ScrumPoker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BA.ScrumPoker.Controllers
{
	public class VotingController : Controller
	{
		// GET: Voting
		public ActionResult Index(int id)
		{
            ViewBag.RoomId = id;
            return View();
		}


		public JsonResult Get(int id)
		{
			var user = Rooms.GetClient(id);
			if (user == null)
			{
				return new JsonResult()
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new UiResponse<string>("Failed to find user")
				};
			}

            var config = new VotingConfigurationModel()
            {
                Name = "Fibonacci",
                Numbers = new List<int>() { 0,1,2,3,5,8,13,21,34 }
            };

            var canVote = Rooms.CanVote(user);

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new UiResponse<VotingViewModel>(new VotingViewModel()
                {
                    Client = user,
					VotingConfiguration = VotingConfiguration.Convert(config),
					CanIVote = canVote
				})
			};
		}

		public JsonResult GetUpdates(ClientModel model)
		{
			var user = Rooms.GetClient(model.Id);
			if (user == null)
			{
				return new JsonResult()
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new UiResponse<string>("Failed to find user")
				};
			}

			var canVote = Rooms.CanVote(user);

			return new JsonResult()
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = new UiResponse<VotingViewModel>(new VotingViewModel()
				{
					Client = user,
					CanIVote = canVote
				})
			};
		}

		public JsonResult Vote(ClientModel model)
		{
			Rooms.Vote(model);

			return new JsonResult()
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = new UiResponse<ClientModel>(model) // FIXME Should return updated model
			};
		}

		public JsonResult CanIVote(ClientModel model)
		{
			var canVote = Rooms.CanVote(model);

			return new JsonResult()
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = new UiResponse<bool>(canVote)
			};
		}

	}
}