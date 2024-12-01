using HotelBooker.Interfaces;
using HotelBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooker.Helpers
{
    public class DataValidator : IDataValidator
    {
        public void ValidateAvailability(Availability availability)
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

        public void ValidateHotelAndBookings(List<Hotel> hotels, List<Booking> bookings)
        {
            if (hotels == null || !hotels.Any())
            {
                throw new ArgumentException("Hotels list cannot be null or empty");
            }

            if (bookings == null || !bookings.Any())
            {
                throw new ArgumentException("Bookings list cannot be null or empty");
            }
        }
    }
}
