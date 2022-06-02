using BeauitySaloonWeb.Customs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Appointments
{
    public class AppointmentRatingViewModel
    {
        
        public string Id { get; set; }

        public string SalonId { get; set; }

        public string SalonName { get; set; }

        public string SalonCategoryName { get; set; }

        public string SalonCityName { get; set; }

        public string SalonAddress { get; set; }

        public string SalonImageUrl { get; set; }

        public bool? IsSalonRatedByTheUser { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = ErrorMessages.Rating)]
        public int RateValue { get; set; }
    }
}