using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymBusiness.Models.ViewModels
{
    public class MediaVM
    {
        [Required]
        public HttpPostedFileBase Media_Path { get; set; }
        public int? Player_id { get; set; }
        public string Coach_id { get; set; }
    }
}