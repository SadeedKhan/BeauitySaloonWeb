using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Salons
{
    public class SalonsSimpleListViewModel
    {
        public IEnumerable<SalonSimpleViewModel> Salons { get; set; }
    }
}