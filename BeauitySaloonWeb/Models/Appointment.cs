using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string SalonId { get; set; }

        public virtual Salon Salon { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        public virtual SalonService SalonService { get; set; }

        // The Salon can Confirm or Decline an appointment
        public bool? Confirmed { get; set; }

        // For every past (and confirmed) appointment the User can Rate the Salon
        // But rating can be given only once for each appointment
        public bool? IsSalonRatedByTheUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}