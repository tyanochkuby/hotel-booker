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
            var bookingDict = new Dictionary<string, Dictionary<string, List<DateRange>>>();

            foreach (var booking in bookings)
            {
                if (!bookingDict.ContainsKey(booking.HotelId))
                    bookingDict[booking.HotelId] = new Dictionary<string, List<DateRange>>();

                if (!bookingDict[booking.HotelId].ContainsKey(booking.RoomType))
                    bookingDict[booking.HotelId][booking.RoomType] = new List<DateRange>();

                bookingDict[booking.HotelId][booking.RoomType].Add(new DateRange(booking.Arrival, booking.Departure));
            }

            return bookingDict;
        }
    }
}
