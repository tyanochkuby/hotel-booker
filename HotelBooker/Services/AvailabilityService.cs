using HotelBooker.Interfaces;
using HotelBooker.Models;

namespace HotelBooker.Services;


/// <summary>
/// Service to get availability of rooms in hotels
/// </summary>
public class AvailabilityService : IAvailabilityService
{
    /// <summary>
    /// Key for the dictionary of hotel availability
    /// </summary>
    /// <param name="HotelId">Id of the hotel</param>
    /// <param name="RoomType">Type of the room</param>
    private record HotelRoomKey(string HotelId, string RoomType);


    /// <summary>
    /// Dictionary of hotel availability
    /// </summary>
    private Dictionary<HotelRoomKey, int> _hotelRoomsCounts;


    /// <summary>
    /// List of booking ranges
    /// </summary>
    private List<HotelBooking> _bookingRanges;


    /// <summary>
    /// Private constructor to prevent external instantiation.
    /// </summary>
    private AvailabilityService() { }


    /// <summary>
    /// Constructor for the availability service
    /// </summary>
    /// <param name="hotels">List of hotels</param>
    /// <param name="bookings">List of bookings</param>
    public AvailabilityService(List<Hotel> hotels, List<Booking> bookings)
    {
        _hotelRoomsCounts = ToHotelDictionary(hotels);

        _bookingRanges = ToBookingList(bookings);
    }


    /// <summary>
    /// Converts a list of hotels to a aictionary with count of rooms in specified hotel and roomtype
    /// </summary>
    /// <param name="hotels">List of hotels</param>
    /// <returns>Dictionary with count of rooms in specified hotel and roomtype</returns>
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


    /// <summary>
    /// Converts a list of bookings to a list of hotel bookings
    /// </summary>
    /// <param name="bookings">List of bookings</param>
    /// <returns>List of Hotel-Booking pairs</returns>
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


    /// <inheritdoc/>
    /// <exception cref="ArgumentException"></exception>
    public int GetTotalAvailability(AvailabilityQuery availability)
    {
        var key = new HotelRoomKey(availability.HotelId, availability.RoomType);

        if (!_hotelRoomsCounts.TryGetValue(key, out var roomCount))
        {
            throw new ArgumentException("Invalid hotelId or roomType");
        }

        var hotelBooking = _bookingRanges.FirstOrDefault(h => h.HotelId == availability.HotelId);
        if (hotelBooking is null)
        {
            return roomCount;
        }

        var roomBooking = hotelBooking.RoomBookings.FirstOrDefault(r => r.RoomType == availability.RoomType);
        if (roomBooking is null)
        {
            return roomCount;
        }

        var bookedCount = roomBooking.DateRanges
            .Count(dateRange => dateRange.Start < availability.DateRange.End && dateRange.End > availability.DateRange.Start);

        return roomCount - bookedCount;
    }
}
