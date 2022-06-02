using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Cities
{
    public class CitiesListViewModel
    {
        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}