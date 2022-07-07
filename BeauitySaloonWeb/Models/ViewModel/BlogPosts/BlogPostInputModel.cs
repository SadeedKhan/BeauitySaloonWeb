using BeauitySaloonWeb.Customs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeauitySaloonWeb.Models.ViewModel.BlogPosts
{
    public class BlogPostInputModel
    {
        [Required]
        [StringLength(
            DataValidations.TitleMaxLength,
            ErrorMessage = ErrorMessages.Title,
            MinimumLength = DataValidations.TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(
            DataValidations.ContentMaxLength,
            ErrorMessage = ErrorMessages.Content,
            MinimumLength = DataValidations.ContentMinLength)]
        public string Content { get; set; }

        [Required]
        [StringLength(
            DataValidations.NameMaxLength,
            ErrorMessage = ErrorMessages.Author,
            MinimumLength = DataValidations.NameMinLength)]
        public string Author { get; set; }
        
        [DataType(DataType.Upload)]
        [AllowHtml]
        public HttpPostedFileWrapper file { get; set; }
    }
}