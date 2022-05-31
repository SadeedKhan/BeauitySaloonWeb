using BeauitySaloonWeb.Customs;
using System.ComponentModel.DataAnnotations;

namespace BeauitySaloonWeb.Models.ViewModel.Appointments
{
    public class AppointmentInputModel
    {
        [Required]
        public string SalonId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        [ValidateDateString(ErrorMessage = ErrorMessages.DateTime)]
        public string Date { get; set; }

        [Required]
        [ValidateTimeString(ErrorMessage = ErrorMessages.DateTime)]
        public string Time { get; set; }
    }
}