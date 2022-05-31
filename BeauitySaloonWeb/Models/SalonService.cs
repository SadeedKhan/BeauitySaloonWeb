using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models
{
    public class SalonService
    {
        public SalonService()
        {
            this.Appointments = new HashSet<Appointment>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string SalonId { get; set; }

        public virtual Salon Salon { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        // Each Salon gets all Services from its Category, but may not provide them all
        public bool Available { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}