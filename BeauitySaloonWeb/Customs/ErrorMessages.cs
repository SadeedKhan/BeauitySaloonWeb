using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Customs
{
    public static class ErrorMessages
    {
        public const string Title = "Title must be between 5 and 60 characters.";

        public const string Content = "Content must be between 700 and 3500 characters.";

        public const string Author = "Author name must be between 2 and 40 characters.";

        public const string Name = "Name must be between 2 and 40 characters.";

        public const string Description = "Description must be between 50 and 700 characters.";

        public const string Address = "Address must be between 5 and 100 characters.";

        public const string Image = "Please select a JPG, JPEG or PNG image smaller than 1MB.";

        public const string DateTime = "Please select a valid DATE and TIME from the datepicker calendar on the left.";

        public const string Rating = "Please choose a valid number of stars from 1 to 5.";
    }
}