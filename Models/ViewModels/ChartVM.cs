using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymBusiness.Models.ViewModels
{
    public class ChartVM
    {
        public int ID { get; set; }
        public List<int> Dayes { get; set; }
        public List<double> Weight { get; set; }
        public List<History_for_player> History { get; set; }

    }
}