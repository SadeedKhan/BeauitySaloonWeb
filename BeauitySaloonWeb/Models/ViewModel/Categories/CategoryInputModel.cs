using BeauitySaloonWeb.Customs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Categories
{
    public class CategoryInputModel
    {
        [Required]
        [StringLength(
          DataValidations.NameMaxLength,
          ErrorMessage = ErrorMessages.Name,
          MinimumLength = DataValidations.NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            DataValidations.DescriptionMaxLength,
            ErrorMessage = ErrorMessages.Description,
            MinimumLength = DataValidations.DescriptionMinLength)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        [ValidateImageFile(ErrorMessage = ErrorMessages.Image)]
        public IFormFile Image { get; set; }
    }
}