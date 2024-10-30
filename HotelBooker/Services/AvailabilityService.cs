using HotelBooker.Extensions;
using HotelBooker.Interfaces;
using HotelBooker.Models;

namespace HotelBooker.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private Dictionary<string, Dictionary<string, int>> _hotelsAvailability;
        private Dictionary<string, Dictionary<string, List<DateRange>>> _bookingRanges;

        private AvailabilityService() { }
        public AvailabilityService(List<Hotel> hotels, List<Booking> bookings)
        {
            _hotelsAvailability = hotels.ToDictionary(
                hotel => hotel.Id,
                hotel => hotel.ToRoomCountDictionary()
            );

            _bookingRanges = bookings.ToBookingDictionary();
        }

        public int GetCountAvailable(Availability availability)
        {
            int roomCount = _hotelsAvailability[availability.Code][availability.RoomType];
            List<DateRange> bookedDates = _bookingRanges[availability.Code][availability.RoomType];

            return roomCount - bookedDates.Count(dateRange => dateRange.Overlaps(availability.DateRange));
        }

    }
}
