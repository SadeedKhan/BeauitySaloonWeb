﻿using BeauitySaloonWeb.Customs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models
{
    public class Salon
    {
        public Salon()
        {
            this.Appointments = new HashSet<Appointment>();
            this.Services = new HashSet<SalonService>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataValidations.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        // Not Required! Allows testing/showing functionality with seeded data in Admin/Manager Area
        // For now Salons can be created only in the AdminDashboard and all of them are managed by one seeded ManagerAccount
        // This will be used when Registering a Salon becomes an option for every user
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        [MaxLength(DataValidations.AddressMaxLength)]
        public string Address { get; set; }

        public double Rating { get; set; }

        public int RatersCount { get; set; }

        public virtual ICollection<SalonService> Services { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}