using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BA.ScrumPoker.Areas.Room.Models
{
	public class VoteModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? VoteValue { get; set; }
	}
}