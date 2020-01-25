using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace GymBusiness.Models.ViewModels
{
    public class HistoryLoadVM
    {
        public List<Load> Load { get; set; }
        public List<History_for_player> History { get; set; }
    }
}