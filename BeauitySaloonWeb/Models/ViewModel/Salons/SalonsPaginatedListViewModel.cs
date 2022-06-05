using BeauitySaloonWeb.CustomsValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Salons
{
    public class SalonsPaginatedListViewModel
    {
        public PaginatedList<SalonViewModel> Salons { get; set; }
    }
}