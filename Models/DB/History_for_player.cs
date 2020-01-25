namespace GymBusiness.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class History_for_player
    {
        public int ID { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime Datetime { get; set; }
        [ForeignKey("Player")]
        
        public int? Player_id { get; set; }

        public double Weight { get; set; }
        [ForeignKey("Load")]
        public int? Loads_id { get; set; }

        public virtual Load Load { get; set; }

        public virtual Player Player { get; set; }
    }
}
