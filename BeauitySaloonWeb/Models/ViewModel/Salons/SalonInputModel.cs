﻿using BeauitySaloonWeb.Customs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Models.ViewModel.Salons
{
    public class SalonInputModel
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
        public int CityId { get; set; }

        [Required]
        [StringLength(
            DataValidations.AddressMaxLength,
            ErrorMessage = ErrorMessages.Address,
            MinimumLength = DataValidations.AddressMinLength)]
        public string Address { get; set; }

        [DataType(DataType.Upload)]
        [ValidateImageFile(ErrorMessage = ErrorMessages.Image)]
        public IFormFile Image { get; set; }
    }
}