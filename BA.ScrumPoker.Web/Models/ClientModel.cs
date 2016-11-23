using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BA.ScrumPoker.Models
{
    public class ClientModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int RoomId { get; set; }

        public int? VoteValue { get; set; }

        public int Id { get; set; }
    }
}