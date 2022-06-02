using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.SalonServices
{
    public class SalonServiceSimpleViewModel
    {
        public string SalonId { get; set; }

        public int ServiceId { get; set; }

        public bool Available { get; set; }
    }
}