using System;
using System.Globalization;

namespace BirthdayGreetingsKata.Infrastructure
{
    public static class StringExtensions
    {
        public static DateTime ToDate(this string value) =>
            DateTime.ParseExact(
                value,
                "yyyy/MM/dd",
                CultureInfo.InvariantCulture);
    }
}