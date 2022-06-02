using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Appointments
{
    public class AppointmentsListViewModel
    {
        public IEnumerable<AppointmentViewModel> Appointments { get; set; }
    }
}