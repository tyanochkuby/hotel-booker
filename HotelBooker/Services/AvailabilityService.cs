using HotelBooker.Extensions;
using HotelBooker.Interfaces;
using HotelBooker.Models;

namespace HotelBooker.Services
{
    public record HotelRoomKey(string HotelId, string RoomType);

    public class AvailabilityService : IAvailabilityService
    {
        private Dictionary<HotelRoomKey, int> _hotelsAvailability;
        private List<HotelBooking> _bookingRanges;

        private AvailabilityService() { }
        public AvailabilityService(List<Hotel> hotels, List<Booking> bookings)
        {
            _hotelsAvailability = ToHotelDictionary(hotels);

            _bookingRanges = ToBookingList(bookings);
        }
        
        private static Dictionary<HotelRoomKey, int> ToHotelDictionary(List<Hotel> hotels)
        {
            return hotels
                .SelectMany(hotel => hotel.Rooms
                    .GroupBy(room => room.RoomType)
                    .Select(group => new KeyValuePair<HotelRoomKey, int>(
                        new HotelRoomKey(hotel.Id, group.Key),
                        group.Count())))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static List<HotelBooking> ToBookingList(List<Booking> bookings)
        {
            return bookings
                .GroupBy(b => b.HotelId)
                .Select(hotelGroup => new HotelBooking
                {
                    HotelId = hotelGroup.Key,
                    RoomBookings = hotelGroup
                        .GroupBy(b => b.RoomType)
                        .Select(roomGroup => new RoomBooking
                        {
                            RoomType = roomGroup.Key,
                            DateRanges = roomGroup
                                .Select(b => new DateRange(b.Arrival, b.Departure))
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();
        }

        public Dictionary<DateTime, int> GetDetailedAvailability(Availability availability)
        {
            var key = new HotelRoomKey(availability.HotelId, availability.RoomType);
            var detailedAvailability = new Dictionary<DateTime, int>();
            var dates = Enumerable.Range(0, (availability.DateRange.End - availability.DateRange.Start).Days)
                    .Select(offset => availability.DateRange.Start.AddDays(offset))
                    .ToList();

            if (!_hotelsAvailability.TryGetValue(key, out var roomCount))
            {
                throw new ArgumentException("Invalid hotelId or roomType");
            }

            var hotelBooking = _bookingRanges.FirstOrDefault(h => h.HotelId == availability.HotelId);
            if (hotelBooking is null)
            {
                return dates.ToDictionary(date => date, _ => roomCount);
            }

            var roomBooking = hotelBooking.RoomBookings.FirstOrDefault(r => r.RoomType == availability.RoomType);

            if (roomBooking is null)
            {
                return dates.ToDictionary(date => date, _ => roomCount);
            }

            foreach (var date in dates)
            {
                var bookedCount = roomBooking.DateRanges.Count(dateRange => dateRange.Start <= date && dateRange.End > date);
                detailedAvailability[date] = roomCount - bookedCount;
            }

            return detailedAvailability;
        }
    }
}
