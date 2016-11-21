using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BA.ScrumPoker.Web.Models
{
    public class ClientModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public int RoomId { get; set; }

        public int Estimation { get; set; }

        public int UserId { get; set; }
    }
}