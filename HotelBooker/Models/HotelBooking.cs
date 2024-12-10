using HotelBooker.Helpers;
using Newtonsoft.Json;

namespace HotelBooker.Models;

/// <summary>
/// Represents all bookings for a hotel.
/// </summary>
public record HotelBooking
{
    /// <summary>
    /// The hotel ID.
    /// </summary>
    public string HotelId { get; init; }


    /// <summary>
    /// Hotel's bookings
    /// </summary>
    public List<RoomBooking> RoomBookings { get; init; }
}
