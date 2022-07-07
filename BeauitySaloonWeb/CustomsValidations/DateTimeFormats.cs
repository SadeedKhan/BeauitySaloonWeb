using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BeauitySaloonWeb.Customs
{
    public static class DateTimeFormats
    {
        public const string DateFormat = "dd-MM-yyyy";

        public const string TimeFormat = "h:mmtt";

        public const string DateTimeFormat = "dd-MM-yyyy h:mmtt";

        public static DateTime ConvertStrings(string date, string time)
        {
            string dateString = date + " " + time;
            string format = DateTimeFormat;

            DateTime dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

            return dateTime;
        }
    }
}