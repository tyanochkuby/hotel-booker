namespace HotelBooker.Models;

/// <summary>
/// Represents the availability query of a room in a hotel for a given date range.
/// </summary>
public record AvailabilityQuery
{
    /// <summary>
    /// Private constructor to prevent external instantiation.
    /// </summary>
    private AvailabilityQuery()
    {
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="AvailabilityQuery"/> record.
    /// </summary>
    /// <param name="hotelId">The ID of the hotel</param>
    /// <param name="roomType">The type of the hotel room</param>
    /// <param name="dateRange">The date range of the query</param>
    public AvailabilityQuery(string hotelId, string roomType, DateRange dateRange)
    {
        HotelId = hotelId;
        RoomType = roomType;
        DateRange = dateRange;
    }


    /// <summary>
    /// The ID of the hotel
    /// </summary>
    public string HotelId { get; init; }


    /// <summary>
    /// The type of the hotel room
    /// </summary>
    public string RoomType { get; init; }


    /// <summary>
    /// The date range of the query
    /// </summary>
    public DateRange DateRange { get; init; }
}