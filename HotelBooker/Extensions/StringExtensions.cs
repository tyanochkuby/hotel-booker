using HotelBooker.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelBooker.Extensions
{
    public static class StringExtensions
    {
        const string AVAILABILITY_PATTERN = @"^\s*Availability\(\s*(\w+)\s*,\s*(\d{8})\s*(?:-\s*(\d{8})\s*)?,\s*(\w+)\s*\)\s*$";
        public static Availability ToAvailability(this string input)
        {
            var match = Regex.Match(input, AVAILABILITY_PATTERN);

            if (!match.Success)
                throw new FormatException("Input string is not in the expected format.");

            var hotelId = match.Groups[1].Value;
            var roomType = match.Groups[4].Value;

            var startDate = DateTime.ParseExact(match.Groups[2].Value, "yyyyMMdd", null);
            DateTime endDate = match.Groups[3].Success
                ? DateTime.ParseExact(match.Groups[3].Value, "yyyyMMdd", null)
                : startDate.AddDays(1);

            if (startDate > endDate)
            {
                throw new FormatException("Start date must be before end date.");
            }

            return new Availability(hotelId, roomType, new DateRange(startDate, endDate));
        }
    }
}
