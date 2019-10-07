using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Models
{
    public class NewsMessage
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public string MessageDetail { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string AddedBy { get; set; }
        [Required]
        public string Product { get; set; }
        public string ModifyedBy { get; set; }
        public DateTime ModifyedDate { get; set; }
    }
}
