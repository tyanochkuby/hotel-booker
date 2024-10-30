using HotelBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooker.Helpers
{
    public class DataValidator
    {
        public static bool ValidateData(List<Hotel> hotels, List<Booking> bookings)
        {
            foreach (var hotel in hotels)
            {
                var roomTypeCodes = new HashSet<string>(hotel.RoomTypes.Select(rt => rt.Code));
                foreach (var room in hotel.Rooms)
                {
                    if (!roomTypeCodes.Contains(room.RoomType))
                    {
                        Console.WriteLine($"Room with ID '{room.RoomId}' in hotel '{hotel.Id}' has an invalid RoomType code '{room.RoomType}'.");
                        return false;
                    }
                }
            }

            var hotelRoomTypes = hotels.ToDictionary(
                h => h.Id,
                h => new HashSet<string>(h.RoomTypes.Select(rt => rt.Code))
            );

            foreach (var booking in bookings)
            {
                if (!hotelRoomTypes.ContainsKey(booking.HotelId) || !hotelRoomTypes[booking.HotelId].Contains(booking.RoomType))
                {
                    Console.WriteLine($"Booking for hotel '{booking.HotelId}' has an invalid RoomType '{booking.RoomType}'.");
                    return false;
                }
            }

            return true;
        }
    }
}
