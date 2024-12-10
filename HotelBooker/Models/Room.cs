using Newtonsoft.Json;

namespace HotelBooker.Models;

/// <summary>
/// Represents a room in a hotel
/// </summary>
public record Room
{
    /// <summary>
    /// The type of the room
    /// </summary>
    [JsonProperty("roomType")]
    [JsonRequired]
    public string RoomType { get; set; }


    /// <summary>
    /// The room's ID
    /// </summary>
    [JsonProperty("roomId")]
    [JsonRequired]
    public string RoomId { get; set; }
}
