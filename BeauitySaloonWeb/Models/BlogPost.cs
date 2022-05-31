using BeauitySaloonWeb.Customs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models
{
    public class BlogPost
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [MaxLength(DataValidations.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataValidations.ContentMaxLength)]
        public string Content { get; set; }

        // BlogPost can be created only in the Admin Dashboard
        // so the Author is not a User, just a string for name
        [Required]
        [MaxLength(DataValidations.NameMaxLength)]
        public string Author { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

    }
}