using BeauitySaloonWeb.Customs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Cities
{
    public class CityInputModel
    {
        [Required]
        [StringLength(
            DataValidations.NameMaxLength,
            ErrorMessage = ErrorMessages.Name,
            MinimumLength = DataValidations.NameMinLength)]
        public string Name { get; set; }
    }
}