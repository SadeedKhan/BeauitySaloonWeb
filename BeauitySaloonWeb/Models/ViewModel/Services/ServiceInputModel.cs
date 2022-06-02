using BeauitySaloonWeb.Customs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Services
{
    public class ServiceInputModel
    {
        [Required]
        [StringLength(
           DataValidations.NameMaxLength,
           ErrorMessage = ErrorMessages.Name,
           MinimumLength = DataValidations.NameMinLength)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(
            DataValidations.DescriptionMaxLength,
            ErrorMessage = ErrorMessages.Description,
            MinimumLength = DataValidations.DescriptionMinLength)]
        public string Description { get; set; }
    }
}