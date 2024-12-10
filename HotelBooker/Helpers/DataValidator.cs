using HotelBooker.Interfaces;
using HotelBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooker.Helpers;

/// <summary>
/// Provides methods to validate hotel related data.
/// </summary>
public class DataValidator : IDataValidator
{
    /// <inheritdoc />
    public void ValidateAvailability(AvailabilityQuery availability)
    {
        if (string.IsNullOrEmpty(availability.HotelId))
        {
            throw new ArgumentException("HotelId cannot be null or empty");
        }

        if (string.IsNullOrEmpty(availability.RoomType))
        {
            throw new ArgumentException("RoomType cannot be null or empty");
        }

        if (availability.DateRange.Start > availability.DateRange.End)
        {
            throw new ArgumentException("Start date must be before end date");
        }
    }

    /// <inheritdoc />
    public void ValidateHotelAndBookings(List<Hotel> hotels, List<Booking> bookings)
    {
        if (hotels == null || !hotels.Any())
        {
            throw new ArgumentException("Hotels list cannot be null or empty");
        }

        if (bookings == null)
        {
            throw new ArgumentException("Bookings list cannot be null");
        }
    }
}
