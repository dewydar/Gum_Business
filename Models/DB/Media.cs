namespace GymBusiness.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Media
    {
        public int ID { get; set; }

        [Required]
        public string Media_Path { get; set; }
        [ForeignKey("Player")]

        public int? Player_id { get; set; }

        [StringLength(128)]
        [ForeignKey("Coach")]

        public string Coach_id { get; set; }

        public virtual Player Player { get; set; }

        public ApplicationUser Coach { get; set; }
    }
}
