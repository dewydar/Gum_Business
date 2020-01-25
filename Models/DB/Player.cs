namespace GymBusiness.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Player")]
    public partial class Player
    {
        
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Full_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Mail { get; set; }

        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date_Of_Birth { get; set; }

        [StringLength(128)]
        [ForeignKey("Coach")]
        public string Coach_Id { get; set; }
        public ApplicationUser Coach { get; set; }
        public virtual ICollection<History_for_player> History_for_player { get; set; }

        public virtual ICollection<Media> Media { get; set; }
    }
}
