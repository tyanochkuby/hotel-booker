using HotelBooker.Helpers;
using Newtonsoft.Json;

namespace HotelBooker.Models;

/// <summary>
/// Represents a booking
/// </summary>
public record Booking
{
    /// <summary>
    /// The hotel ID
    /// </summary>
    [JsonProperty("hotelId")]
    [JsonRequired]
    public string HotelId { get; set; }


    /// <summary>
    /// The arrival date
    /// </summary>
    [JsonProperty("arrival")]
    [JsonConverter(typeof(DateTimeJsonConverter))]
    [JsonRequired]
    public DateTime Arrival { get; set; }


    /// <summary>
    /// The departure date
    /// </summary>
    [JsonProperty("departure")]
    [JsonRequired]
    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime Departure { get; set; }


    /// <summary>
    /// The room type
    /// </summary>
    [JsonProperty("roomType")]
    [JsonRequired]
    public string RoomType { get; set; }


    /// <summary>
    /// The room rate
    /// </summary>
    [JsonProperty("roomRate")]
    public string RoomRate { get; set; }
}
