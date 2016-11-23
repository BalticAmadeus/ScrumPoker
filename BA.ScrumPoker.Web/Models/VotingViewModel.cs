using System.Collections.Generic;
using System.Linq;

namespace BA.ScrumPoker.Models
{
	public class VotingViewModel
	{
		public ClientModel Client { get; set; }
		public VotingConfiguration VotingConfiguration { get; set; }

		public bool CanIVote { get; set; }
	}

	public class VotingConfiguration
	{
		public string Name { get; set; }
		public List<SelectedNumber> Selection { get; set; }

		public VotingConfiguration()
		{
			Selection = new List<SelectedNumber>();
		}

		public static VotingConfiguration Convert(VotingConfigurationModel model)
		{
			return new VotingConfiguration()
			{
				Name = model.Name,
				Selection = model.Numbers.Select(x => new SelectedNumber() { Number = x, IsSelected = false }).ToList()
			};
		}
	}

	public class SelectedNumber
	{
		public int Number { get; set; }
		public bool IsSelected { get; set; }
	}
}