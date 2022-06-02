using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Appointments
{
    public class AppointmentViewModel
    {
        public string Id { get; set; }

        public DateTime DateTime { get; set; }

        public string UserEmail { get; set; }

        public string SalonId { get; set; }

        public string SalonName { get; set; }

        public string SalonCityName { get; set; }

        public string SalonAddress { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public bool? Confirmed { get; set; }

        public bool? IsSalonRatedByTheUser { get; set; }
    }
}