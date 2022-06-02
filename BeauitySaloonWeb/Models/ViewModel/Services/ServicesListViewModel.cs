using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Services
{
    public class ServicesListViewModel
    {
        public IEnumerable<ServiceViewModel> Services { get; set; }
    }
}