using HotelBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooker.Extensions
{
    public static class BookingExtensions
    {
        public static Dictionary<string, Dictionary<string, List<DateRange>>> ToBookingDictionary(this List<Booking> bookings)
        {
            return bookings
                .GroupBy(b => b.HotelId)
                .ToDictionary(
                    hotelGroup => hotelGroup.Key,
                    hotelGroup => hotelGroup
                        .GroupBy(b => b.RoomType)
                        .ToDictionary(
                            roomGroup => roomGroup.Key,
                            roomGroup => roomGroup
                                .Select(b => new DateRange(b.Arrival, b.Departure))
                                .ToList()
                        )
                );
        }
    }
}
