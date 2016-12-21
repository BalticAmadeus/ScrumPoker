using BA.ScrumPoker.Infrasturcture;
using BA.ScrumPoker.MemoryData;
using BA.ScrumPoker.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BA.ScrumPoker.Controllers
{
	public class VotingController : Controller
	{
		// GET: Voting
		public ActionResult Index(int id)
		{
            ViewBag.ClientId = id;
            return View();
		}

		public JsonResult Get(int id)
		{
			var client = Rooms.Instance.GetClient(id);

			if (client == null)
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

            var canVote = Rooms.Instance.CanVote(client);

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new UiResponse<VotingViewModel>(new VotingViewModel()
                {
                    Client = client,
					VotingConfiguration = VotingConfiguration.Convert(config),
					CanIVote = canVote
				})
			};
		}

		public JsonResult GetUpdates(ClientModel model)
		{
			var client = Rooms.Instance.GetClient(model.Id);

			if (client == null)
			{
				return new JsonResult()
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new UiResponse<string>("Failed to find user")
				};
			}

			var canVote = Rooms.Instance.CanVote(client);

			return new JsonResult()
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = new UiResponse<VotingViewModel>(new VotingViewModel()
				{
					Client = client,
					CanIVote = canVote
				})
			};
		}

		public JsonResult Vote(ClientModel model)
		{
			ClientModel client = Rooms.Instance.Vote(model);

            if (client == null)
            {
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new UiResponse<string>("Failed to cast vote")
                };
            }

            return new JsonResult()
			{
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = new UiResponse<ClientModel>(client)
			};
		}
	}
}