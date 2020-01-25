namespace GymBusiness.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Load
    {
        
        public int ID { get; set; }

        public int Load1 { get; set; }

        public int Load2 { get; set; }

        public int Load3 { get; set; }

        public virtual ICollection<History_for_player> History_for_player { get; set; }
    }
}
