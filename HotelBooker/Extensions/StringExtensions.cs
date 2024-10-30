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
        public static Availability ToAvailability(this string input)
        {
            // Define regex pattern to capture hotel ID, date(s), and room type
            var pattern = @"^\s*Availability\(\s*(\w+)\s*,\s*(\d{8})\s*(?:-\s*(\d{8})\s*)?,\s*(\w+)\s*\)\s*$";
            var match = Regex.Match(input, pattern);

            if (!match.Success)
                throw new FormatException("Input string is not in the expected format.");

            // Parse hotel ID and room type
            var hotelId = match.Groups[1].Value;
            var roomType = match.Groups[4].Value;

            // Parse start date and optional end date
            var startDate = DateTime.ParseExact(match.Groups[2].Value, "yyyyMMdd", null);
            DateTime? endDate = match.Groups[3].Success
                ? DateTime.ParseExact(match.Groups[3].Value, "yyyyMMdd", null)
                : null;

            if (endDate.HasValue && startDate > endDate)
            {
                throw new FormatException("Start date must be before end date.");
            }

            return endDate switch
            {
                null => new Availability(hotelId, roomType, new DateRange(startDate, startDate.AddDays(1))),
                _ => new Availability(hotelId, roomType, new DateRange(startDate, endDate.Value))
            };
        }
    }
}
