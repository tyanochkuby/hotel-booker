using HotelBooker.Interfaces;
using HotelBooker.Models;
using System;

namespace HotelBooker.Helpers;

/// <summary>
/// Parser class for Availability
/// </summary>
public class AvailabilityParser : IAvailabilityParser
{
    /// <inheritdoc />
    public AvailabilityQuery Parse(string input)
    {
        var parts = input.Split(new[] { '(', ',', '-', ')' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 3 || parts.Length > 5)
            throw new FormatException("Input string is not in the expected format.");

        var command = parts[0].Trim();

        if (command.ToLower() != "availability")
        {
            throw new FormatException($"The command {command} wasn't recognized");
        }

        var hotelId = parts[1].Trim();
        var startDate = DateTime.ParseExact(parts[2].Trim(), "yyyyMMdd", null);
        DateTime endDate;
        string roomType;

        if (parts.Length == 4)
        {
            endDate = startDate.AddDays(1);
            roomType = parts[3].Trim();
        }
        else if (parts.Length == 5)
        {
            endDate = DateTime.ParseExact(parts[3].Trim(), "yyyyMMdd", null);
            roomType = parts[4].Trim();
        }
        else
        {
            throw new FormatException("Input string is not in the expected format.");
        }

        if (startDate > endDate)
        {
            throw new FormatException("Start date must be before end date.");
        }

        return new AvailabilityQuery(hotelId, roomType, new DateRange(startDate, endDate));
    }
}


