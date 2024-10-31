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
            var hotelRoomTypes = hotels.ToDictionary(
                h => h.Id,
                h => new HashSet<string>(h.RoomTypes.Select(rt => rt.Code))
            );

            var invalidRooms = hotels
                .SelectMany(hotel => hotel.Rooms, (hotel, room) => new { hotel, room })
                .Where(hr => !hotelRoomTypes[hr.hotel.Id].Contains(hr.room.RoomType))
                .ToList();

            if (invalidRooms.Any())
            {
                invalidRooms.ForEach(ir =>
                    Console.WriteLine($"Room with ID '{ir.room.RoomId}' in hotel '{ir.hotel.Id}' has an invalid RoomType code '{ir.room.RoomType}'."));
                return false;
            }

            var invalidBookings = bookings
                .Where(booking => !hotelRoomTypes.ContainsKey(booking.HotelId) || !hotelRoomTypes[booking.HotelId].Contains(booking.RoomType))
                .ToList();

            if (invalidBookings.Any())
            {
                invalidBookings.ForEach(ib =>
                    Console.WriteLine($"Booking for hotel '{ib.HotelId}' has an invalid RoomType '{ib.RoomType}'."));
                return false;
            }

            return true;
        }
    }
}
