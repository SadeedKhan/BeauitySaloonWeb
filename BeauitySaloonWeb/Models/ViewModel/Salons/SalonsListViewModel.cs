using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Salons
{
    public class SalonsListViewModel
    {
        public IEnumerable<SalonViewModel> Salons { get; set; }
    }
}