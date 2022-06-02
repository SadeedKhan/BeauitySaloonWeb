using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Cities
{
    public class CityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SalonsCount { get; set; }
    }
}